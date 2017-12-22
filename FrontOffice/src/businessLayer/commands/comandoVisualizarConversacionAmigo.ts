import { FabricaDAO } from '../factory/fabricaDao';
import { Entidad } from '../../dataAccessLayer/domain/entidad';
import  { Comando } from './comando';
import { DAOChat } from '../../dataAccessLayer/dao/daoChat';
import { Events } from 'ionic-angular';

export class ComandoVisualizarConversacionAmigo extends Comando {
    public _events : Events;
    

    public execute(): void {
        console.log("ENTRANDO EN EXECUTE DE COMANDO VISUALIZAR MENSAJE");
        let DAO = FabricaDAO.crearFabricaDAOChat();
        DAO.visualizarLista(this._entidad, this._events);
    }

    
    get getEntidad():Entidad {
        return this._entidad;
    }

    set setEntidad(entidad:Entidad) {
        this._entidad = entidad;
    }
    
    set setEvents(events:Events) {
        this._events = events;
    }
}