import { Component, ViewChild } from '@angular/core';
import { IonicPage, NavController, NavParams, Slides, reorderArray, AlertController, ToastController, ModalController, LoadingController } from 'ionic-angular';
import { CalendarioPage } from '../calendario/calendario';
import * as moment from 'moment';
import { EventosCalendarioService } from '../../services/eventoscalendario';
import { HttpCProvider } from '../../providers/http-c/http-c';
import { TranslateService } from '@ngx-translate/core';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import 'rxjs/add/observable/throw';
import { Storage } from '@ionic/storage';
import { FabricaComando } from '../../businessLayer/factory/fabricaComando';
import { FabricaEntidad } from '../../dataAccessLayer/factory/fabricaEntidad';
import { Itinerario } from '../../dataAccessLayer/domain/itinerario';
import { FabricaDAO } from '../../businessLayer/factory/fabricaDao';


@IonicPage()
@Component({
  selector: 'page-itinerario',
  templateUrl: 'itinerario.html',
})
export class ItinerarioPage {
  @ViewChild(Slides) slides: Slides;

  //***************************** DECLARACION DE VARIABLES ***********************
  base_url = 'http://localhost:8082';
  noFoto = '/Images/empty-image.png';
  items = [];
  edit = false;
  delete= false;
  count = 0;
  minDate= new Date().toISOString();
  its: any;
  newitinerario: any;
  users: any;
  _notif={
    correo: {},
    push: {}
  };
  toast: any;
  datos:any;
  IdUsuario: any;
  list = false;
  nuevoViejo = true;
  originalEventDates = Array();
  eventDatesAsInt = Array();
  noIts = false;
  public loading = this.loadingCtrl.create({
    content: 'Please wait...'
  });
  //************************* FIN DE DECLARACION DE VARIABLES ********************

  ///Constructor de la clase ItinerarioPage
  constructor
  (
    public navCtrl: NavController,
    public navParams: NavParams,
    private modalCtrl: ModalController,
    public alertCtrl: AlertController,
    public itinerarios: EventosCalendarioService,
    public httpc: HttpCProvider,
    public loadingCtrl: LoadingController,
    public toastCtrl: ToastController,
    private translateService: TranslateService,
    private storage: Storage
  )
  {

  }
/**************************************************************************************
/*************************** METODOS DE LA CLASE **************************************
/**************************************************************************************

 /** Metodo: calendar
      Descripcion: Metodo que redirecciona a la vista de calendario, enviando
        los itinerarios del usuario loggeado.
      Parametros de entrada: no aplica
      Parametros de salida: no aplica

      Autores:
        Arguelles, Marialette
        Jraiche, Michel
        Orrillo, Horacio
  **/
  public calendar()
  {
    this.navCtrl.push(CalendarioPage, {itinerarios: this.its});
  }

  /** Metodo: reorderItems
      Descripcion: Metodo que re-ordena los items de un itinerario segun la
        preferencia del usuario
      Parametros de entrada: indexes
      Parametros de salida: no aplica

      Autores:
        Arguelles, Marialette
        Jraiche, Michel
        Orrillo, Horacio
   **/
  public reorderItems(indexes)
  {
    this.items = reorderArray(this.items, indexes);
  }

  /** Metodo: crearIngles
      Descripcion: Metodo que crea la modal de crear un nuevo itinerario en ingles,
        segun el idioma de la aplicacion
      Parametros de entrada: no aplica
      Parametros de salida: no aplica

      Autores:
        Arguelles, Marialette
        Jraiche, Michel
        Orrillo, Horacio
   **/
  public crearIngles()
  {
    const alert = this.alertCtrl.create({
      title: 'New Itinerary',
      inputs: [
        {
          name: 'Nombre',
          placeholder: 'Name'
        }
      ],
      buttons: [
        {
          text: 'CANCEL',
          role: 'cancel',
          handler: data => {
          }
        },
        {
          text: 'CREATE',
          handler: data => {
            if (data.Nombre!= '' && data.Nombre!= undefined) {
              if (this.its == undefined) this.its=Array();
              let name = data.Nombre;
              let newitinerario ={ Nombre:data.Nombre, IdUsuario: this.IdUsuario }
              console.log("salio del comando");
              this.httpc.agregarItinerario(newitinerario).then(
                data =>{
                  if (data ==0 || data==-1){
                    this.loading.dismiss();
                    this.realizarToast("Sorry, your itinerary wasn't created. Please, try later :(");
                  }else{
                    this.loading.dismiss();
                    this.noIts=false;
                    this.datos = data;
                    this.its.push({
                      Id: this.datos.Id,
                      Nombre: name,
                      Items_agenda: Array()
                    })
                  }
                }
              )
            } else {
              return false;
            }
          }
        }
      ]
    });
    alert.present();
  }

  /** Metodo: crear
      Descripcion: Metodo que redirije a metodos de creacion de modal de nuevos
        itinerarios segun el idioma de la aplicacion.
      Parametros de entrada: no aplica
      Parametros de salida: no aplica

      Autores:
        Arguelles, Marialette
        Jraiche, Michel
        Orrillo, Horacio
   **/
  public crear()
  { this.edit=false;
    this.delete=false;
    if (this.translateService.currentLang == 'es') this.crearEspanol();
    else this.crearIngles();
  }

  /** Metodo: crearEspanol
      Descripcion: Metodo que crea la modal de crear un nuevo itinerario en espanol,
        segun el idioma de la aplicacion
      Parametros de entrada: no aplica
      Parametros de salida: no aplica

      Autores:
        Arguelles, Marialette
        Jraiche, Michel
        Orrillo, Horacio
   **/
  public crearEspanol()
  {
    const alert = this.alertCtrl.create({
      title: 'Nuevo Itinerario',
      inputs: [
        {
          name: 'Nombre',
          placeholder: 'Nombre'
        }
      ],
      buttons: [
        {
          text: 'Cancelar',
          role: 'cancel',
          handler: data => {
          }
        },
        {
          text: 'Crear',
          handler: data => {
            if (data.Nombre!= '' && data.Nombre!= undefined) {
              if (this.its == undefined) this.its=Array();
              let name = data.Nombre;
              let newitinerario ={ Nombre:data.Nombre, IdUsuario: this.IdUsuario }
              let itinerario: Itinerario;
              this.httpc.agregarItinerario(newitinerario).then(
                data =>{
                  if (data ==0 || data==-1){
                    this.loading.dismiss();
                    this.realizarToast('No se pudo agregar el itinerario. Por favor intente mas tarde :(');
                  }else{
                    this.loading.dismiss();
                    this.noIts=false;
                    this.datos = data;
                    this.its.push({
                      Id: this.datos.Id,
                      Nombre: name,
                      Items_agenda: Array()
                    })
                  }
                }
              )
            } else {
              return false;
            }
          }
        }
      ]
    });
    alert.present();
  }

  /** Metodo: presentConfirm
      Descripcion: Metodo para confirmar la eliminacion de un itinerario
      Parametros de entrada: no aplica
      Parametros de salida: no aplica

      Autores:
        Arguelles, Marialette
        Jraiche, Michel
        Orrillo, Horacio
   **/
  public presentConfirm(idit, index)
  {
    if (this.translateService.currentLang == 'es'){
    const alert = this.alertCtrl.create({
    title: 'Por favor, confirmar',
    message: '¿Desea borrar este itinerario?',
    buttons: [
      {
        text: 'Cancelar',
        role: 'cancel',
        handler: () => {
        }
      },
      {
        text: 'Aceptar',
        handler: () => {
          this.presentLoading();
          this.httpc.eliminarItinerario(idit).then(data => {
            if (data==0 || data==-1){
              this.loading.dismiss();
              this.delete= !this.delete;
              console.log("hubo un error");
            }else{
              this.loading.dismiss();
              this.delete= !this.delete;
              console.log(idit);
              this.eliminarItinerario(idit, index);
            }
          });
          }
        }
      ]
    });
    alert.present();
  }
  else
  {
    const alert = this.alertCtrl.create({
      title: 'Please, confirm',
      message: '¿Do you want to delete this itinirary?',
      buttons: [
        {
          text: 'Cancel',
          role: 'cancel',
          handler: () => {
          }
        },
        {
          text: 'Accept',
          handler: () => {
            this.presentLoading();
            this.httpc.eliminarItinerario(idit).then(data => {
              if (data==0 || data==-1){
                this.loading.dismiss();
                this.delete= !this.delete;
                console.log("hubo un error");
              }else{
                this.loading.dismiss();
                this.delete= !this.delete;
                this.eliminarItinerario(idit, index);
              }
            });
            }
          }
        ]
      });
      alert.present();
    }
  }

  /** Metodo: presentConfirmItem
      Descripcion: Metodo para confirmar la eliminacion de un item dado un
        itinerario
      Parametros de entrada: no aplica
      Parametros de salida: no aplica

      Autores:
        Arguelles, Marialette
        Jraiche, Michel
        Orrillo, Horacio
   **/
  public presentConfirmItem(id_itinerario, evento, index)
  {
    if (this.translateService.currentLang == 'es'){
    const alert = this.alertCtrl.create({
    title: 'Por favor, confirmar',
    message: '¿Desea borrar este elemento?',
    buttons: [
    {
      text: 'Cancelar',
      role: 'cancel',
      handler: () => {
      }
    },
    {
      text: 'Aceptar',
      handler: () => {
        this.presentLoading();
        console.log(evento.Tipo);
        console.log(id_itinerario);
        console.log(evento.Id);
        this.httpc.eliminarItem(evento.Tipo,id_itinerario, evento.Id).then(data=>{
          if (data==0 || data==-1){
            this.loading.dismiss();
            console.log("ERROR:: no se pudo eliminar el item");
          }else {
            this.loading.dismiss();
            console.log("data");
            console.log(data);
            this.eliminarItem(id_itinerario, evento.Id, index);
          }
        });
          }
        }
      ]
    });
    alert.present();
  }
  else
  {
    const alert = this.alertCtrl.create({
      title: 'Please, confirm',
      message: '¿Do you wish to delete this item?',
      buttons: [
      {
        text: 'Cancel',
        role: 'cancel',
        handler: () => {
        }
      },
      {
        text: 'Accept',
        handler: () => {
          this.presentLoading();
          this.httpc.eliminarItem(evento.Tipo,id_itinerario, evento.Id).then(data=>{
            if (data==0 || data==-1){
              this.loading.dismiss();
              console.log("ERROR:: no se pudo eliminar el item");
            }else {
              this.loading.dismiss();
              this.eliminarItem(id_itinerario, evento.Id, index);
            }
          });
            }
          }
        ]
      });
      alert.present();
    }
}

  /** Metodo: eliminar
      Descripcion: Metodo para habilitar opciones de eliminacion del modulo
        itinerario
      Parametros de salida: no aplica
      Parametros de entrada: no aplica

      Autores:
        Arguelles, Marialette
        Jraiche, Michel
        Orrillo, Horacio
   **/
  public eliminar()
  {
    this.delete = !this.delete;
    this.edit = false;
  }

  /** Metodo: eliminarItinerario
      Descripcion: Metodo para eliminar un itinerario en pantalla
      Parametros de salida: id, Id del itinerario a eliminarItem
                            index, posicion en el arreglo de itinerarios en memoria
      Parametros de entrada: no aplica

      Autores:
        Arguelles, Marialette
        Jraiche, Michel
        Orrillo, Horacio
   **/
  public eliminarItinerario(id, index)
  {
     let eliminado = this.its.filter(item => item.Id === id)[0];
     var removed_elements = this.its.splice(index, 1);
     if (this.its.length == 0){
       this.noIts = true;
       console.log("no its")
     }
   }

   /** Metodo: eliminarItem
       Descripcion: Metodo para eliminar un item de un itinerario en pantalla
       Parametros de salida: id_itinerario, Id del itinerarios
                              id_evento, Id del item que se quiere eliminar
                              index, posicion del item en el arreglo de items de
                                ese itinerario en memoria
       Parametros de entrada: no aplica

       Autores:
         Arguelles, Marialette
         Jraiche, Michel
         Orrillo, Horacio
    **/
  public eliminarItem(id_itinerario, id_evento, index)
  {
    let iti_e_eliminado = this.its.filter(item => item.Id === id_itinerario)[0];
    var removed_elements = iti_e_eliminado.Items_agenda.splice(index, 1);
  }

  /** Metodo: editar
      Descripcion: Metodo para habilitar opciones de modificacion del modulo
        itinerario
      Parametros de salida: no aplica
      Parametros de entrada: no aplica

      Autores:
        Arguelles, Marialette
        Jraiche, Michel
        Orrillo, Horacio
   **/
  public editar()
  {
    this.edit = !this.edit;
    this.delete = false;
    for(var i = 0;i< this.its.length;i++) {
      this.its[i].edit = this.its[i].Nombre;
    }
  }

  /** Metodo: done
      Descripcion: Metodo para deshabilitar las opciones de modificacion, asi como
        realizar la modificacion en el backend
      Parametros de salida: no aplica
      Parametros de entrada: no aplica

      Autores:
        Arguelles, Marialette
        Jraiche, Michel
        Orrillo, Horacio
   **/
  public done()
  {
    console.log("Entro a done");
    this.delete=false;
    if (this.translateService.currentLang == 'es'){
    for(var i = 0;i< this.its.length;i++) {
      this.its[i].edit = this.its[i].Nombre;
      if (this.its[i].FechaInicio > this.its[i].FechaFin)
      {
        this.realizarToast('Fecha inicio debe ser menor a fecha fin');
        this.edit=true;
      }
      else
      {
        console.log(this.its[i].Nombre);
        this.edit = false;
        let moditinerario ={Id:this.its[i].id, Nombre:this.its[i].Nombre,FechaInicio:this.its[i].FechaInicio,FechaFin:this.its[i].FechaFin,IdUsuario:this.IdUsuario}
        this.httpc.modificarItinerario(moditinerario).then(data=>{
          console.log("data: ");
          console.log(data);
        })
      }
    }
  }
  else
  {
    for( i = 0;i< this.its.length;i++) {
      this.its[i].edit = this.its[i].Nombre;
      if (this.its[i].FechaInicio > this.its[i].FechaFin)
      {
        this.realizarToast('Start date must be less than end date');
        this.edit=true;
      }
      else
      {
        this.edit = false;
        let moditinerario ={Id:this.its[i].Id, Nombre:this.its[i].Nombre,FechaInicio:this.its[i].FechaInicio,FechaFin:this.its[i].FechaFin,IdUsuario:this.IdUsuario}
        this.httpc.modificarItinerario(moditinerario).then(data=>{
        })
      }
    }
  }
}

  /** Metodo: doneDeleting
      Descripcion: Metodo para deshabilitar las opciones de eliminacion del modulo
        itinerario
      Parametros de salida: no aplica
      Parametros de entrada: no aplica

      Autores:
        Arguelles, Marialette
        Jraiche, Michel
        Orrillo, Horacio
   **/
  public doneDeleting()
  {
    this.edit = false;
    this.delete = false;
  }

  /** Metodo: ordenar
      Descripcion: Metodo para ordenar la lista de items de un itinerario por fecha,
        de mas reciente a mas antiguo, y viceversa.
      Parametros de salida: no aplica
      Parametros de entrada: no aplica
ionview
      Autores:
        Arguelles, Marialette
        Jraiche, Michel
        Orrillo, Horacio
   **/
  public ordenar()
  {
    this.nuevoViejo = !this.nuevoViejo;
    var dates = Array();
    if (this.nuevoViejo == true){
     for(var i = 0;i< this.its.length;i++) {
       this.its[i].Items_agenda.sort(function(a,b){
            return new Date(b.FechaInicio).getTime() - new Date(a.FechaInicio).getTime();
         });
       }
    }else{
      for(i = 0;i< this.its.length;i++) {
        this.its[i].Items_agenda.sort(function(a,b){
             return new Date(a.FechaInicio).getTime() - new Date(b.FechaInicio).getTime();
          });
      }
    }
  }

  /** Metodo: ordenarIt
      Descripcion: Metodo para ordenar la lista de itinerarios del usuario por fecha,
        de mas reciente a mas antiguo, y viceversa.
      Parametros de salida: no aplica
      Parametros de entrada: no aplica

      Autores:
        Arguelles, Marialette
        Jraiche, Michel
        Orrillo, Horacio
   **/
  public ordenarIt()
  {
    this.nuevoViejo = !this.nuevoViejo;
    if (this.nuevoViejo == true){
        this.its.sort(function(a,b){
          return new Date(b.FechaInicio).getTime() - new Date(a.FechaInicio).getTime();
        });
    }else{
      this.its.sort(function(a,b){
        return new Date(a.FechaInicio).getTime() - new Date(b.FechaInicio).getTime();
      });
    }
  }

  /** Metodo: agregarItem
      Descripcion: Metodo para ir a la vista de buscador de items
      Parametros de salida: no aplica
      Parametros de entrada: iti, objeto itinerario al cual se le agregara el item seleccionado

      Autores:
        Arguelles, Marialette
        Jraiche, Michel
        Orrillo, Horacio
   **/
  public agregarItem(iti)
  {
    if (((iti.FechaInicio=='0001-01-01T00:00:00')||(iti.FechaFin=='0001-01-01T00:00:00'))||((iti.FechaInicio==null)||(iti.FechaFin==null)))
    {
      if (this.translateService.currentLang == 'es'){
        this.realizarToast('El itinerario aun no tiene ambas fechas, por favor ingreselas y vuelvalo a intentar');
      }else{
        this.realizarToast('The itinerary does not have both dates yet, please select them and try again');
      }
    }
    else
    {
      let modal = this.modalCtrl.create('ItemModalPage', {itinerario: iti});
      modal.present();
      modal.onDidDismiss(data => {
      if (data) {
        let eventoData = data;
        let itinerario_nuevo = data.itinerario;
        eventoData.Id = data.evento_nuevo.Id;
        eventoData.Nombre = data.evento_nuevo.Nombre;
        eventoData.Tipo = data.evento_nuevo.Tipo;
        eventoData.Foto = data.evento_nuevo.Foto;
        eventoData.FechaInicio = data.evento_nuevo.FechaInicio;
        eventoData.FechaFin = data.evento_nuevo.FechaFin;
        for(var i = 0;i< this.its.length;i++) {
          if (this.its[i].Nombre == itinerario_nuevo.Nombre) {
            //si el itinerario no tiene eventos, se inicializa el arreglo eventos
            if (this.its[i].Items_agenda == undefined){
              this.its[i].Items_agenda = Array();
            }
            this.its[i].Items_agenda.push(eventoData);
          }
        }
      }
    })
  }
}

  /** Metodo: verItem
      Descripcion: Metodo para presentar la modal para ver detalle de un item del itinerario
      Parametros de salida: no aplica
      Parametros de entrada: evento, item que se desea detallar
                             itinerario, itinerario al que pertenece el item que se quiere detallar

      Autores:
        Arguelles, Marialette
        Jraiche, Michel
        Orrillo, Horacio
   **/
  public verItem(evento, itinerario)
  {
    //Si el click no es en eliminar, entra
    if (this.delete == false){
      let evento1;
      this.presentLoading();
      this.httpc.verItem(evento.id,evento.Tipo).then(data =>{
        if (data== 0 || data == -1){
          this.loading.dismiss();
          if (this.translateService.currentLang == 'es'){
            this.realizarToast('Servicio no disponible. Por favor intente mas tarde :(');
          }else{
            this.realizarToast('Service not currently available. Please try again later');
          }
        }else{
          this.loading.dismiss();
          evento1 = data;
          let modal = this.modalCtrl.create('ConsultarItemModalPage', {evento: evento, itinerario: itinerario, evento1: evento1});
          modal.present();
          modal.onDidDismiss(data => {
          if (data) {

          }
          })
        }
      });
    }
  }

  /** Metodo: goToSlide
      Descripcion: Metodo para redirigirse a un itinerario del carrusel
      Parametros de salida: no aplica
      Parametros de entrada: index, posicion del itinerario en el carrusel

      Autores:
        Arguelles, Marialette
        Jraiche, Michel
        Orrillo, Horacio
   **/
  public goToSlide(index)
  {
    this.list=false;
    setTimeout(() => {
      this.slides.slideTo(index, 500);
    }, 500);
  }

  /** Metodo: listar
      Descripcion: Metodo para listar todos los itinerarios de un usuario, deshabilitando
        la vista de carrusel
      Parametros de salida: no aplica
      Parametros de entrada: no aplica

      Autores:
        Arguelles, Marialette
        Jraiche, Michel
        Orrillo, Horacio
   **/
  public listar()
  {
    for(var i = 0;i< this.its.length;i++) {
      if (this.its[i].Items_agenda == undefined){
        this.its[i].Items_agenda = Array();
      }
    }
    this.edit=false;
    this.delete=false;
    if(this.list==true){
      this.list = false;
    }
    else{
      this.list=true;
    }
  }

  /** Metodo: ordenarIt
      Descripcion: Metodo para mostrar loading spinner
      Parametros de salida: no aplica
      Parametros de entrada: no aplica

      Autores:
        Arguelles, Marialette
        Jraiche, Michel
        Orrillo, Horacio
   **/
  public presentLoading()
  {
      this.loading = this.loadingCtrl.create({
      content: 'Please wait...',
      dismissOnPageChange: true
    });
    this.loading.present();
  }

  /** Metodo: ionViewWillEnter
      Descripcion: Funcion propia de ionic que se ejecuta antes de mostrar un vista
      Parametros de salida: no aplica
      Parametros de entrada: no aplica
   **/
  public ionViewWillEnter()
  {
    this.presentLoading();
     this.storage.get('id').then((val) => {
       this.IdUsuario = val;
      //Se consultan todos los itinerarios, con sus items respectivos, de un usuario
    this.httpc.loadItinerarios(this.IdUsuario)
    .then(data => {
      if (data == -1){
        this.loading.dismiss();
        if (this.translateService.currentLang == 'es'){
        this.realizarToast('Servicio no disponible. Por favor intente mas tarde :(');
      }else{
        this.realizarToast('Service not currently available. Please try again later');
      }
      }else{
        this.its = data;
       // console.log(data); 
        console.log(this.its);
       
        this.loading.dismiss();
        if (this.its.length == 0){
          this.noIts = true;
        }
      }
    });
  });
  }

  /** Metodo: realizarToast
      Descripcion: Metodo para mostrar un mensaje en pantalla al usuario
      Parametros de salida: no aplica
      Parametros de entrada: no aplica

      Autores:
        Arguelles, Marialette
        Jraiche, Michel
        Orrillo, Horacio
   **/
  public realizarToast(mensaje)
  {
      this.toast = this.toastCtrl.create({
        message: mensaje,
        duration: 3000,
        position: 'middle'
      });
      this.toast.present();
  }

  /** Metodo: getTipoItem
      Descripcion: Metodo para obtener el tipo de item de un itinerario dado un objeto item
      Parametros de salida: no aplica
      Parametros de entrada: evento, objeto item del cual se quiere saber el tipo

      Autores:
        Arguelles, Marialette
        Jraiche, Michel
        Orrillo, Horacio
   **/
  public getTipoItem(evento)
  {
    if (evento.Costo == undefined && evento.Precio==undefined){
      let actividad = 'Actividad';
      return actividad;
    }else
      if (evento.Costo != undefined){
        let lugar = 'Lugar Turistico';
        return lugar;
      }else{
        let evento = 'Evento';
        return evento;
      }
  }

  /** Metodo: configurarNotificaciones
      Descripcion: Metodo para redirigirse a la vista de configuracion
      Parametros de salida: no aplica
      Parametros de entrada: no aplica

      Autores:
        Arguelles, Marialette
        Jraiche, Michel
        Orrillo, Horacio
   **/
  public configurarNotificaciones()
  { this.presentLoading();
    this.storage.get('id').then((val) => {
    this.IdUsuario = val;
    this.storage.get( this.IdUsuario.toString() ).then((ok) => {
              //verificamos que posee configuracion previa de idioma
              if(ok != null || ok != undefined){
                this.translateService.use(ok);
              }
              this.httpc.getNotificacionesConfig(this.IdUsuario)
              .then(data =>{
                if(data==-1|| data==0){
                  this.loading.dismiss();
                  this.realizarToast('Error. Por favor intente mas tarde :(');
                }else{
                  this.loading.dismiss();
                  this._notif.correo =data;
                  this._notif.push=false;
                  console.log(data);
                  let modal = this.modalCtrl.create('ConfigNotificacionesItiPage', {config: this._notif, itinerarios: this.its});
                  modal.present();
                  modal.onDidDismiss(data => {
                    if (data) {
                    }
                  })
                }
              })
            });
  });

  }

}
