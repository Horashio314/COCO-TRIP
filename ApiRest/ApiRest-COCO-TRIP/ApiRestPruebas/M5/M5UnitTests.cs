using System;
using System.Net;
using System.Web.Http;
using ApiRest_COCO_TRIP.Models.Dato;
using ApiRest_COCO_TRIP.Models;
using ApiRest_COCO_TRIP.Controllers;
using NUnit.Framework;
using Npgsql;
using System.Collections.Generic;

namespace ApiRestPruebas
{

  [TestFixture]
  class M5UnitTests
  {
    private M5Controller controller;
    private Itinerario itinerario;
    private Itinerario it;
    private Boolean x;
    private DateTime fechaini;
    private DateTime fechafin;
    private int id_usuario;
    private List<Itinerario> itinerarios_usuario;
    [OneTimeSetUp]
    protected void OTSU()
    {
      controller = new M5Controller();
      
    }

    /// <summary>
    /// Prueba de caso exitoso en AgregarItinerario
    /// </summary>
    [Test]
    public void Prueba_AgregarItinerario()
    {
      itinerario = new Itinerario("Michel", 2);
      it = controller.AgregarItinerario(itinerario);
      Assert.AreEqual(57, it.Id);//siempre poner el numero del id que se va a agregar para esta prueba
      Assert.AreEqual("Michel", it.Nombre);
    }

    /// <summary>
    /// Prueba de casos borde(excepciones) en AgregarItinerario
    /// </summary>
    [Test]
    public void Prueba_FalloAgregarItinerario()
    {
      Assert.Catch<HttpResponseException>(Excepcion_Agregar);
      Assert.Catch<HttpResponseException>(Excepcion_Agregar2);
    }

    /// <summary>
    /// Metodo utilizados para casos borde(excepciones)
    /// </summary>
    public void Excepcion_Agregar()
    {
      itinerario = null;
      controller.AgregarItinerario(itinerario);
    }

    /// <summary>
    /// Metodo utilizados para casos borde(excepciones)
    /// </summary>
    public void Excepcion_Agregar2()
    {
      itinerario = new Itinerario("Michel", 0);
      controller.AgregarItinerario(itinerario);
    }

    [Test]
    public void Prueba_EliminarItinerario()
    {
      x = controller.EliminarItinerario(47);
      Assert.True(x);
    }

    [Test]
    public void Prueba_FalloEliminarItinerario()
    {
      x = controller.EliminarItinerario(4);
      Assert.False(x);
    }

    [Test]
    public void Prueba_ModificarItinerario()
    {
      DateTime fechaini = new DateTime(2022, 05, 28);
      DateTime fechafin = new DateTime(2030, 05, 28);
      Itinerario itinerario = new Itinerario(4, "Epco", fechaini, fechafin, 2);
      it = controller.ModificarItinerario(itinerario);
      Assert.AreEqual("Epco", it.Nombre);
      Assert.AreEqual(fechaini,it.FechaInicio);
      Assert.AreEqual(fechafin, it.FechaFin);
    }

    /*   [Test]
       public void Prueba_AgregarEvento_It()
       {
         Itinerario itinerario = new Itinerario(9);
         Evento ev = new Evento
         {
           Id = 1
         };
         fechaini = new DateTime(2017, 11, 15);
         fechafin = new DateTime(2017, 11, 18);
         M5Controller controller = new M5Controller();
         Boolean x = controller.AgregarItem_It("Evento",itinerario.Id, ev.Id,fechaini, fechafin);
         Assert.True(x);
       }*/

    [Test]
    public void Prueba_AgregarActividad_It()
    {
      itinerario = new Itinerario(6);
      Actividad ac = new Actividad
      {
        Id = 2
      };
      fechaini = new DateTime(2017, 11, 15);
      fechafin = new DateTime(2017, 11, 18);
      x = controller.AgregarItem_It("Actividad",itinerario.Id,ac.Id,fechaini, fechafin);
      Assert.True(x);
    }

    [Test]
    public void Prueba_AgregarLugar_It()
    {
      itinerario = new Itinerario(6);
      LugarTuristico lt = new LugarTuristico()
      {
        Id = 1
      };
      fechaini = new DateTime(2017,11,15);
      fechafin = new DateTime(2017,11,18);
      x = controller.AgregarItem_It("Lugar Turistico",itinerario.Id, lt.Id,fechaini,fechafin);
      Assert.True(x);
    }


    /* [Test]
    public void PruebaConsultarItinerarios()
    {
      Assert.Catch<NpgsqlException>(ExcepcionItinerarioNull);
    }

    public void ExcepcionItinerarioNull()
    {
      id_usuario = null;
      controller.ConsultarItinerarios(id_usuario);
    } */

    [Test]
    public void ConsultarItinerarioIsEmpty()
    {
      //id de usuario que no existe
      id_usuario = 50;
      Assert.IsEmpty(controller.ConsultarItinerarios(id_usuario));
    }
   
    [Test]
    public void Prueba_EliminarLugarIt()
    {
      //x = controller.EliminarItem_It(4,12);
      //Assert.True(x);
    }

    [Test]
    public void Prueba_EliminarActividad()
    {
      x = controller.EliminarItem_It("Actividad", 4, 1);
      Assert.True(x);
    }
  }
}
