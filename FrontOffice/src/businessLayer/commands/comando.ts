import { Entidad } from '../../dataAccessLayer/domain/entidad';
//****************************************************************************************************//
//*****************************************CLASE COMANDO MODULO 6*************************************//
//****************************************************************************************************//

/**
 * Autores:
 * Mariangel Perez
 * Oswaldo Lopez
 * Aquiles Pulido
 */

/**
 * Descripcion de la clase:
 * 
 * Clase abstracta comando
 */

export abstract class Comando 
{
    
    _entidad : Entidad;

    /**
     * Ejecuta la logica de negocios del comando
     */
    public abstract execute(): any;

    /**
     * Retorna el resultado de la ejecucion
     */
    public abstract return(): any;
    
}