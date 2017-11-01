import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
import { IonicApp, IonicErrorHandler, IonicModule } from 'ionic-angular';
import { NgCalendarModule } from 'ionic2-calendar';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { Facebook } from '@ionic-native/facebook'
import { CloudSettings, CloudModule } from '@ionic/cloud-angular';
import { CocoTrip } from './app.component';

import { HomePage } from '../pages/home/home';
import { ListPage } from '../pages/list/list';
import { LoginPage } from '../pages/login/login';
import { PerfilPage } from '../pages/perfil/perfil';
import { AmistadesGruposPage } from '../pages/amistades-grupos/amistades-grupos';
import { EventosActividadesPage } from '../pages/eventos-actividades/eventos-actividades';
import { ItinerarioPage } from '../pages/itinerario/itinerario';
import { ChatPage } from '../pages/chat/chat';
import { ConversacionPage } from '../pages/chat/conversacion/conversacion';
import { RegisterPage } from '../pages/register/register';
import { EditProfilePage } from '../pages/edit-profile/edit-profile';
import { PreferenciasPage } from "../pages/preferencias/preferencias";
import { CalendarioPage } from '../pages/calendario/calendario';
import { ConfigPage } from '../pages/config/config';
import { BorrarCuentaPage } from '../pages/borrar-cuenta/borrar-cuenta';
import { ChangepassPage } from '../pages/changepass/changepass';
import { AmigosPage } from '../pages/amistades-grupos/amigos/amigos';
import { GruposPage } from '../pages/amistades-grupos/grupos/grupos';
import { NotificacionesPage } from '../pages/amistades-grupos/notificaciones/notificaciones';
import { VisualizarPerfilPage } from '../pages/VisualizarPerfil/VisualizarPerfil';
import { VisualizarPerfilPublicoPage } from '../pages/visualizarperfilpublico/visualizarperfilpublico';
import { CrearGrupoPage } from '../pages/crear-grupo/crear-grupo';
import { CrearGrupo2Page } from '../pages/crear-grupo2/crear-grupo2';
import { DetalleGrupoPage } from '../pages/detalle-grupo/detalle-grupo';
import { AgregarAmigoPage } from '../pages/agregar-amigo/agregar-amigo';


const cloudSettings: CloudSettings = {
  'core': {
    'app_id': 'abd7650b'
  },
  'auth': {
    'google': {
      'webClientId': '383153901052-8u7q4i0as9thogb2im0bu8ut7u52l2ud.apps.googleusercontent.com'
    }
  }
}

import { AmigosPage } from '../pages/amistades-grupos/amigos/amigos';
import { GruposPage } from '../pages/amistades-grupos/grupos/grupos';
import { NotificacionesPage } from '../pages/amistades-grupos/notificaciones/notificaciones';

import { VisualizarPerfilPage } from '../pages/VisualizarPerfil/VisualizarPerfil';
import { VisualizarPerfilPublicoPage } from '../pages/visualizarperfilpublico/visualizarperfilpublico';

import { CrearGrupoPage } from '../pages/crear-grupo/crear-grupo';

import { SeleccionarIntegrantesPage } from '../pages/seleccionar-integrantes/seleccionar-integrantes';
import { DetalleGrupoPage } from '../pages/detalle-grupo/detalle-grupo';
import { CalendarModule } from "ion2-calendar";
import { EventosCalendarioService } from '../services/eventoscalendario'
import { BuscarAmigoPage } from '../pages/buscar-amigo/buscar-amigo';

@NgModule({
  declarations: [
    CocoTrip,
    HomePage,
    ListPage,
    LoginPage,
    PerfilPage,
    AmistadesGruposPage,
    EventosActividadesPage,
    ItinerarioPage,
    ChatPage,
    ConversacionPage,
    AmigosPage,
    GruposPage,
    NotificacionesPage,
    VisualizarPerfilPage,
    VisualizarPerfilPublicoPage,
    CrearGrupoPage,
    SeleccionarIntegrantesPage,
    DetalleGrupoPage,
    BuscarAmigoPage,
    RegisterPage,
    ChatPage,
    EditProfilePage,
    ChangepassPage,
    ConfigPage,
    BorrarCuentaPage,
    PreferenciasPage,
    CalendarioPage
  ],
  imports: [
    NgCalendarModule,
    BrowserModule,
    IonicModule.forRoot(CocoTrip),
    CloudModule.forRoot(cloudSettings),
    CalendarModule
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    CocoTrip,
    HomePage,
    ListPage,
    LoginPage,
    PerfilPage,
    AmistadesGruposPage,
    EventosActividadesPage,
    ItinerarioPage,
    ChatPage,
    ConversacionPage,
    AmigosPage,
    GruposPage,
    NotificacionesPage,
    VisualizarPerfilPage,
    VisualizarPerfilPublicoPage,
    CrearGrupoPage,
    SeleccionarIntegrantesPage,
    DetalleGrupoPage,
    BuscarAmigoPage,
    RegisterPage,
    EditProfilePage,
    ChangepassPage,
    ConfigPage,
    BorrarCuentaPage,
    PreferenciasPage,
    CalendarioPage
  ],
  providers: [
    StatusBar,
    SplashScreen,
    {provide: ErrorHandler, useClass: IonicErrorHandler},
    Facebook,
    EventosCalendarioService
  ]
})
export class AppModule {}
