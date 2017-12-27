import { Entidad } from './entidad';
//****************************************************************************************************//
//*****************************************CLASE MENSAJE MODULO 3*************************************//
//****************************************************************************************************//

/**
 * Autores:
 * Joaquin Camacho
 * Jose Herrera
 * Sabina Quiroga
 */

/**
 * Descripcion de la clase:
 * Entidad que contiene los datos de los usuarios
 */
export class Usuario extends Entidad
{
    private nombreUsuario: string;
    private nombre: string;
    private apellido: string;
    private foto: string;

    constructor () //Por ahora vacio
    {
        super();
    } 

    /**
     * Retorna el nombre de usuario
     */
    get NombreUsuario() : string 
    {
        return this.nombreUsuario;
    }

    /**
     * Establece el nombre de usuario
     */
    set NombreUsuario(nombreUsuario : string) 
    {
        this.nombreUsuario = nombreUsuario;
    }
    
    /**
     * Retorna el nombre
     */
    get Nombre() : string 
    {
        return this.nombre;
    }

    /**
     * Establece el nombre
     */
    set Nombre(nombre : string) 
    {
        this.nombre = nombre;
    }

    /**
     * Retorna el apellido
     */
    get Apellido() : string 
    {
        return this.apellido;
    }

    /**
     * Establece el apellido
     */
    set Apellido(apellido : string) 
    {
        this.apellido = apellido;
    }

    /**
     * Retorna la ruta de la foto
     */
    get Foto() : string 
    {
        return this.foto;
    }

    /**
     * Establece el apellido
     */
    set Foto(foto : string)
    {
        this.foto = foto;
    }
}