using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    /// <summary>
    /// Comando que elimina un itinerario
    /// </summary>
    public class ComandoEliminarItinerario : Comando
  {
    private Itinerario itinerario;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="id">Id del itinerario</param>
        public ComandoEliminarItinerario(int id)
    {
      itinerario = FabricaEntidad.CrearEntidadItinerario();
      itinerario.Id = id;
    }

    public override void Ejecutar()
    {
      DAOItinerario dAOItinerario = FabricaDAO.CrearDAOItinerario();
      dAOItinerario.Eliminar(itinerario);
    }

    public override Entidad Retornar()
    {
      throw new NotImplementedException();
    }

    public override List<Entidad> RetornarLista()
    {
      throw new NotImplementedException();
    }
  }
}
