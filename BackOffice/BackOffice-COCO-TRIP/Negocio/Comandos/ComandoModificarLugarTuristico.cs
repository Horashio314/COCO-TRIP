using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using BackOffice_COCO_TRIP.Datos.DAO;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;

namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  public class ComandoModificarLugarTuristico : Comando
  {
    private Entidad lugarTuristico = FabricaEntidad.GetLugarTuristico();
    private ArrayList resultado = new ArrayList();
    IDAOCategoria dao = FabricaDAO.GetDAOCategoria();
    
    public override void Execute()
    {
      //try
      //{
      //  JObject respuesta = dao.Put(categoria);
      //  resultado.Add(respuesta);
      //}
      //catch (Exception e)
      //{
      //  throw e;
      //}


    }
    public override ArrayList GetResult()
    {
      return resultado;
    }
   

    public override void SetPropiedad(object propiedad)
    {
      //categoria = (Categoria)propiedad;
    }
  }
}