import { Comando } from './comando';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { catProd, catService, catErr } from '../../logs/config';

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
 * Solicita al servicio web la lista de usuarios asociados a la busqueda
 */
export class ComandoBuscarAmigo extends Comando
{
    private id : number;
    private nombre : string;

    private exito: boolean;
    private listaUsuarios: any;

    public constructor(nombre : string, id : number, 
        private servicio?: RestapiService)
    {
        super();

        this.nombre = nombre;
        this.id = id;
    }

    public execute(): void 
    {
        this.servicio.buscarAmigos(this.nombre, this.id)
        .then(datos => 
        {
            this.exito = true;
            this.listaUsuarios = datos;
            catProd.info('BuscarAmigos exitoso. Datos: ' + datos);
        }
        , error =>
        {
            this.exito = false;
            this.listaUsuarios = error;
            catErr.info('Fallo de BuscarAmigos. Datos: ' + error);
        });
    }

    public return() 
    {
        return this.listaUsuarios;
    }

    public isSuccess(): boolean 
    {
        return this.exito;
    }
}