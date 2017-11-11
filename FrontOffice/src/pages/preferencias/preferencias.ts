import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { ToastController } from 'ionic-angular';
import { Storage } from '@ionic/storage';

@IonicPage()
@Component({
  selector: 'page-preferencias',
  templateUrl: 'preferencias.html',
})
export class PreferenciasPage {

  preferenciasEnLista: any; //Aquí se guardarán los items de preferencias.
  preferenciasEnBusqueda: any; //Aquí se irán guardando los que se traigan de la Base de datos.
  idUsuario: any;
  

  constructor(public navCtrl: NavController, public navParams: NavParams, public toastCtrl: ToastController,
    public restapiService: RestapiService,private storage: Storage) {

    this.cargarListas();

  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad PreferenciasPage');
  }

  aviso(str, preferencias) {

    var posicionIndex;

    if (str == "agregado") {

      this.preferenciasEnLista.push( preferencias );
      posicionIndex = this.preferenciasEnBusqueda.indexOf( preferencias );
      this.preferenciasEnBusqueda.splice( posicionIndex, 1);
      const toast = this.toastCtrl.create({
        message: preferencias + ' fue agregada exitosamente',
        showCloseButton: true,
        closeButtonText: 'Ok'
      });
      toast.present();

    } else {

      //Eliminando del array de lista
      posicionIndex = this.preferenciasEnLista.indexOf( preferencias );
      this.preferenciasEnLista.splice( posicionIndex, 1);

      const toast = this.toastCtrl.create({
        message: 'La categoria ' + preferencias.Nombre + ' fue eliminada exitosamente',
        showCloseButton: true,
        closeButtonText: 'Ok'
      });
      toast.present();

    }
  }


    cargarListas(){

      this.storage.get('id').then((val) => {
        this.idUsuario = val;
        this.inicializarListas();
      });

    }

    inicializarListas( ){

      this.restapiService.buscarPreferencias( this.idUsuario )
      .then(data => {
        this.preferenciasEnLista = data;
      });
    }


    filtrarPreferencias(ev: any) {
        //this.inicializarListas(1);
        //Este será el valor que uno escribe en el search bar
        let val = ev.target.value;

        // Si está vació no va a filtrar.
        if (val && val.trim() != '') {
          this.preferenciasEnBusqueda = this.preferenciasEnBusqueda.filter((item) => {
            return (item.toLowerCase().indexOf(val.toLowerCase()) > -1);
          })
        }
      }


}

class Categoria{

  Id: any ;
  Nombre: any;
  Descripcion : any;
  Estatus: any;
  CategoriaSupeior: any;

  constructor(id: number, nombre: string, descripcion: string, status: boolean, categoriaSuperior: Categoria){

    this.Id = id;
    this.Nombre = nombre;
    this.Descripcion = descripcion;
    this.Estatus = status;
    this.CategoriaSupeior = categoriaSuperior;

  }

}
