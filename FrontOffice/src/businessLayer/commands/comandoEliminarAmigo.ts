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
 * Solicita al servicio web eliminar el amigo asociado al usuario
 */
export class ComandoEliminarAmigo extends Comando
{
    private id : number;
    private nombreUsuario : string;

    private exito: boolean;

    private servicio: RestapiService;

    public constructor(nombreUsuario : string, id : number)
    {
        super();

        this.id = id;
        this.nombreUsuario = nombreUsuario;
    }

    public execute(): void 
    {
        this.servicio.eliminarAmigo(this.nombreUsuario, this.id)
        .then(datos => 
        {
            this.exito = true;
        }
        , error =>
        {
            this.exito = false;
        });
    }

    public return() 
    {
        throw new Error("Method not implemented.");
    }

    public isSuccess(): boolean 
    {
        return this.exito;
    }
}