using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Models.Peticion;
using Newtonsoft.Json.Linq;

namespace BackOffice_COCO_TRIP.Negocio.Componentes.Comandos
{
  public class ComandoEditarLocalidad : Comando
  {
    private LocalidadEvento localidad;
    private String resultado;
    public ComandoEditarLocalidad(LocalidadEvento localidad) {
      this.localidad = localidad;
    }
    public override void Execute()
    {
      PeticionM8_Localidad peticion = new PeticionM8_Localidad();
      JObject respuesta = peticion.Put(localidad);
      if (respuesta.Property("dato") == null)
      {


        resultado= "Ocurrio un error durante la comunicacion, revise su conexion a internet";

      }
      else
      {
        resultado= "Se hizo con exito";
      }

    }

    public override object GetResult()
    {
      return resultado;
    }
  }
}
