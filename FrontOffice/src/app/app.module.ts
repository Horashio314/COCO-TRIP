import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
import { IonicApp, IonicErrorHandler, IonicModule } from 'ionic-angular';

import { CocoTrip } from './app.component';
import { HomePage } from '../pages/home/home';
import { ListPage } from '../pages/list/list';
import { LoginPage } from '../pages/login/login';
import { PerfilPage } from '../pages/perfil/perfil';
import { AmistadesGruposPage } from '../pages/amistades-grupos/amistades-grupos';
import { EventosActividadesPage } from '../pages/eventos-actividades/eventos-actividades';
import { ItinerarioPage } from '../pages/itinerario/itinerario';
import { ChatPage } from '../pages/chat/chat';

import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';

import { AmigosPage } from '../pages/amistades-grupos/amigos/amigos';
import { GruposPage } from '../pages/amistades-grupos/grupos/grupos';
import { NotificacionesPage } from '../pages/amistades-grupos/notificaciones/notificaciones';

import { VisualizarPerfilPage } from '../pages/VisualizarPerfil/VisualizarPerfil';
import { VisualizarPerfilPublicoPage } from '../pages/visualizarperfilpublico/visualizarperfilpublico';

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
    AmigosPage,
    GruposPage,
    NotificacionesPage,
    VisualizarPerfilPage,
    VisualizarPerfilPublicoPage
  ],
  imports: [
    BrowserModule,
    IonicModule.forRoot(CocoTrip),
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
    AmigosPage,
    GruposPage,
    NotificacionesPage,
    VisualizarPerfilPage,
    VisualizarPerfilPublicoPage
  ],
  providers: [
    StatusBar,
    SplashScreen,
    {provide: ErrorHandler, useClass: IonicErrorHandler}
  ]
})
export class AppModule {}
