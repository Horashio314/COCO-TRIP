import { Component, ViewChild, NgZone } from '@angular/core';
import { Platform, ActionSheetController, Events, Content } from 'ionic-angular';
import { IonicPage, NavParams, NavController } from 'ionic-angular';
import { AlertController } from 'ionic-angular';
import { VisualizarPerfilPage } from '../../VisualizarPerfil/VisualizarPerfil';
import * as moment from 'moment';
import { Firebase } from '@ionic-native/firebase';
import { RestapiService } from '../../../providers/restapi-service/restapi-service';
import { ChatProvider } from '../../../providers/chat/chat';
import { Storage } from '@ionic/storage';
import { TranslateService } from '@ngx-translate/core';
import { Mensaje } from '../../../dataAccessLayer/domain/mensaje';
import { FabricaComando } from '../../../businessLayer/factory/fabricaComando';

/**
 * Generated class for the ConversacionGrupoPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-conversacion-grupo',
  templateUrl: 'conversacion-grupo.html',
})
export class ConversacionGrupoPage {
  @ViewChild('content') content: Content;
  conversacion: any;
  nuevoMensaje: any;
  /*nombreUsuario:any={
    idGrupo: 'idGrupo'
  }*/
  idAmigo: any;
  idGrupo: any;
  idUsuario: any;
  usuario: any = {
    NombreUsuario: 'NombreUsuario'
  };
  todosLosMensajes = [];
  //nombreUsuario: string;

  constructor(public navCtrl: NavController, public navParams: NavParams,
  public actionsheetCtrl: ActionSheetController, public alertCtrl: AlertController,
  public platform: Platform, private firebase: Firebase , public chatService: ChatProvider,
  public events: Events, public zone: NgZone, private storage: Storage,public restapiService: RestapiService) {
  }

  ionViewWillEnter() {
 
    this.idGrupo = this.navParams.get('idGrupo');
    
    
   
    this.storage.get('id').then((val) => { 
      
            this.idUsuario = val;
            // hacemos la llamada al apirest con el id obtenido
            this.restapiService.ObtenerDatosUsuario(this.idUsuario).then(data => {
              if(data != 0)
              {  
                this.usuario = data;
              }
            });
      
          });
    
          this.conversacion = this.chatService.conversacion; //Añade y muestra los mensajes de cada conversación
          this.scrollto();
          this.idUsuario =
          this.events.subscribe('nuevoMensaje', () => {
            this.todosLosMensajes = [];
            this.zone.run(() => {
              this.todosLosMensajes = this.chatService.mensajesConversacion;
            })
          })
  
   }

   agregarMensajeGrupo() {
    /*this.chatService.agregarNuevoMensajeGrupo(this.nuevoMensaje,this.idGrupo,this.usuario.NombreUsuario);
      this.content.scrollToBottom();
      this.nuevoMensaje = '';*/

      let entidad: Mensaje;
      entidad = new Mensaje(this.nuevoMensaje,this.usuario.NombreUsuario,"",this.idGrupo);
      let comando = FabricaComando.crearComandoCrearMensajeGrupo();
      comando._entidad = entidad;
      comando.execute();
      this.content.scrollToBottom();
      this.nuevoMensaje = '';

  }

  ionViewDidEnter() {
    
    this.chatService.obtenerMensajesConversacionGrupo(this.idGrupo);
    
  }

  pressEvent(idMensaje){
    if(idMensaje!=-1){
      let actionSheet = this.actionsheetCtrl.create({
        title: 'Opciones',
        cssClass: 'action-sheets-basic-page',
        buttons: [
          {
            text: 'Eliminar Chat',
            role: 'destructive', // aplica color rojo de alerta
            icon: !this.platform.is('ios') ? 'trash' : null,
            handler: () => {
              let decision = this.alertCtrl.create({
                message: '¿Borrar este chat?',
                buttons: [
                  {
                    text: 'Sí',
                    handler: () => {
                      this.chatService.eliminarMensajeGrupo(this.idGrupo,idMensaje,this.usuario.NombreUsuario);
                    }
                  },
                  {
                    text: 'No',
                    handler: () => {
                      console.log('Decisión de eliminar negativa');
                    }
                  }
                ]
              });
              decision.present()
            }
          },
          {
            text: 'Cancelar',
            role: 'cancel', //coloca el botón siempre en el último lugar.
            icon: !this.platform.is('ios') ? 'close' : null,
            handler: () => {
              console.log('Cancelar ActionSheet');
            }
          }
        ]
      });
      actionSheet.present();
    }
  }


  scrollto() {
    setTimeout(() => {
      this.content.scrollToBottom();
    }, 1000);
  }
  

}
