import { DAOChat } from '../../dataAccessLayer/dao/daoChat';
import {catService,catProd} from "../../logs/config";
//****************************************************************************************************//
//**********************************Fabrica Dao de MODULO 6*************************************//
//****************************************************************************************************//

/**
 * Autores:
 * Mariangel Perez
 * Oswaldo Lopez
 * Aquiles Pulido
 */

/**
 * Descripcion de la clase:
 * Fabrica Dao
 * 
 */
export class FabricaDAO{
/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar DAOChat
 * 
 */

    public static crearFabricaDAOChat():DAOChat{
    catProd.info("Entrando en el metodo DAOChat de fabricaDao"); 
        return new DAOChat();
    }

}