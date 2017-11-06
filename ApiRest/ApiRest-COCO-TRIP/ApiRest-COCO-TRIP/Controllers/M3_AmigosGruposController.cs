using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ApiRest_COCO_TRIP.Models;
using Npgsql;
using System.Data;
using Newtonsoft.Json;

namespace ApiRest_COCO_TRIP.Controllers
{
  public class M3_AmigosGruposController : ApiController
  {

    int cmkdf;

    Usuario usuario;
    PeticionAmigoGrupo peticion;

    // GET api/<controller>/<action>/id 
    [HttpGet]
    public string AgregarAmigo(String idUsuario1, String idUsuario2)
    {
      peticion = new PeticionAmigoGrupo();
      peticion.AgregarAmigosBD(Convert.ToInt32(idUsuario1), Convert.ToInt32(idUsuario2));
      return "1";
    }

    [HttpGet]
    public Usuario VisualizarPerfilAmigo(String nombreUsuario)
    {
      peticion = new PeticionAmigoGrupo();
      return peticion.VisualizarPerfilAmigoBD(nombreUsuario);
    }



  }
}
