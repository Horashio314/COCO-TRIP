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
 * Solicita la peticion de amistad del usuario asociado al identificador con el usuario asociado al nombre
 */
export class ComandoAgregarAmigo extends Comando
{
    private id : number;
    private nombreUsuario : string;

    private exito: boolean;

    private servicio: RestapiService;

    public constructor(id : number, nombreUsuario : string)
    {
        super();

        this.id = id;
        this.nombreUsuario = nombreUsuario;
    }

    public execute(): void 
    {
        this.servicio.agregarAmigo(this.id, this.nombreUsuario)
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