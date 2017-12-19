import { Mensaje } from '../domain/mensaje';
import { Entidad } from '../domain/entidad';
import { DAO } from './dao';
import { ChatProvider } from './../../providers/chat/chat';
import firebase from 'firebase';


export class DAOChat extends DAO {
    
    public agregar(entidad : Entidad): Entidad{
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(null);
        mensaje.setId = chat.agregarNuevoMensajeAmigo(mensaje.getMensaje,mensaje.getUsuario,mensaje.getAmigo); 
        return mensaje;
    }

    public agregarMensajeGrupo(entidad : Entidad): Entidad{
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(null);
        chat.agregarNuevoMensajeGrupo(mensaje.getMensaje,mensaje.getidGrupo,mensaje.getUsuario);
        return null;
    }

    visualizar() : Entidad{
        return null;
        
    }
    
        
    
    eliminar(entidad : Entidad) : boolean{
        let respuesta : boolean;
        respuesta = false;
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(null);
        respuesta = chat.eliminarMensajeAmigo(mensaje.getUsuario,mensaje.getAmigo,mensaje.getId);
        return respuesta;
    }

    eliminarMensajeGrupo(entidad : Entidad) : boolean{
        let respuesta : boolean;
        respuesta = false;
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(null);
        respuesta = chat.eliminarMensajeGrupo(mensaje.getidGrupo,mensaje.getId,mensaje.getUsuario); 
        return respuesta;
    }
        

    modificar(entidad : Entidad) : Entidad{
        return null;
    }
    modificarMensajeGrupo(entidad : Entidad) : Entidad{
        return null;
    }
    
        
    

}