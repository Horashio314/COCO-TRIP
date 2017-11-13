import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams,Platform, ActionSheetController,AlertController } from 'ionic-angular';
import { VisualizarPerfilPublicoPage } from '../visualizarperfilpublico/visualizarperfilpublico';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { Storage } from '@ionic/storage';

/**
 * Generated class for the AgregarAmigoPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-buscar-amigo',
  templateUrl: 'buscar-amigo.html',
})
export class BuscarAmigoPage {

  toggled: boolean;
  searchTerm: String = '';
  items:string[];
  idUsuario: any;
  lista:any;
  showList:any;
  showBar:any;
  constructor( public navCtrl: NavController, 
      public navParams: NavParams,public platform: Platform,
      public actionsheetCtrl: ActionSheetController,
      public alerCtrl: AlertController,
      public restapiService: RestapiService,
      private storage: Storage ) {
      //this.cargarListas();       
  }

  ionViewWillEnter() {
    this.showBar = true;
    this.showList = false;
  }

  buscar(ev){
    this.showList = true;
    this.storage.get('id').then((val) =>{
      if(ev.target.value){
        var dato = ev.target.value;
      } 
      this.restapiService.buscaramigo(dato, val)
      .then(data => {
      this.lista = data;
      });
    });
    
  }
  

  /*ionViewDidLeave(){
    this.lista=false;
  }*/

  /*cargarListas(){
  this.storage.get('id').then((val) => {
  this.idUsuario = val;
  this.inicializarListas();
  });
    }

  inicializarListas( ){
                
  this.restapiService.buscaramigo( this.idUsuario )
  .then(data => {
  this.lista = data;
  });
    }*/

 
  Visualizarpublico(nombre){
        this.navCtrl.push(VisualizarPerfilPublicoPage,{
          nombreUsuario : nombre
        });
        this.showBar = false;
      }

  doConfirm() {
      let confirm = this.alerCtrl.create({
        title: 'Recomendar app?',
        message: 'Desea recomendar la app a su amigo?',
        buttons: [
          {
            text: 'No',
            handler: () => {
              console.log('No clicked');
            }
          },
          {
            text: 'Si',
            handler: () => {
              console.log('Si clicked');
            }
          }
        ]
      });
      confirm.present()
    }

}
