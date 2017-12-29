import { Comando } from './comando';
import { RestapiService } from '../../providers/restapi-service/restapi-service';

/**
 * Autores:
 * Joaquin Camacho
 * Jose Herrera
 * Joaquin Camacho
 */

//*************************************************************************************//
//*************************************MODULO 3****************************************//
//*************************************************************************************//

/**
 * Solicita al servicio web la lista de amigos asociado al usuario
 */
export class ComandoListaAmigos extends Comando
{
    private id : number;

    private exito: boolean;
    private listaAmigos: any;

    private servicio: RestapiService;

    public constructor(id : number)
    {
        super();

        this.id = id;
    }

    public execute(): void 
    {
        this.servicio.listaAmigos(this.id)
        .then(datos => 
        {
            this.exito = true;
            this.listaAmigos = datos;
        }
        , error =>
        {
            this.exito = false;
            this.listaAmigos = error;
        });
    }

    public return() 
    {
        return this.listaAmigos;
    }

    public isSuccess(): boolean 
    {
        return this.exito;
    }
}