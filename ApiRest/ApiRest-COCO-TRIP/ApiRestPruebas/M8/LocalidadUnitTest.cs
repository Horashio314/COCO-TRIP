using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Negocio.Command;
using ApiRest_COCO_TRIP.Negocio.Fabrica;
using NUnit.Framework;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Controllers;
using System;

namespace ApiRestPruebas.M8
{
    /// <summary>
    /// Clase que realiza las pruebas unitarias a todos los metodos referentes a localidades
    /// </summary>
    [TestFixture]
    class LocalidadUnitTest
    {
        private Entidad localidad;
        private DAO dao;
        private List<Entidad> lista;
        private Comando comando;
        private M8_LocalidadEventoController controlador;
        private Dictionary<string, object> esperado = new Dictionary<string, object>();
        private Dictionary<string, object> respuesta = new Dictionary<string, object>();

        /// <summary>
        /// Metodo que inicializa las variables necesarias para correr las pruebas unitarias de localidades
        /// </summary>
        [SetUp]
        public void SetUpLocalidad()
        {
            localidad = FabricaEntidad.CrearEntidadLocalidad();
            ((LocalidadEvento)localidad).Nombre = "Test";
            ((LocalidadEvento)localidad).Descripcion = "Test Localidad";
            ((LocalidadEvento)localidad).Coordenadas = "0.2 , 0.1";
            dao = FabricaDAO.CrearDAOLocalidad();
            dao.Insertar(localidad);
            lista = dao.ConsultarLista(null);
            foreach (Entidad entidad in lista)
            {
                if (((LocalidadEvento)entidad).Nombre.Equals(((LocalidadEvento)localidad).Nombre))
                    localidad.Id = entidad.Id;
            }

        }

        /// <summary>
        /// Metodo que realiza las pruebas unitarias sobre el metodo insertar del DAOLocalidad
        /// </summary>
        [Test]
        public void TestInsertarLocalidad()
        {
            dao.Eliminar(localidad);
            Assert.DoesNotThrow(() =>
            {
                dao.Insertar(localidad);
            });

            localidad.Id += 1;
            dao.Eliminar(localidad);
            ((LocalidadEvento)localidad).Nombre = null;
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                dao.Insertar(localidad);
            });

            ((LocalidadEvento)localidad).Nombre = "Test";
            ((LocalidadEvento)localidad).Coordenadas = null;
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                dao.Insertar(localidad);
            });

            ((LocalidadEvento)localidad).Coordenadas = "0.2, 0.3";
            ((LocalidadEvento)localidad).Descripcion = null;
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                dao.Insertar(localidad);
            });


        }

        /// <summary>
        /// Metodo que realiza las pruebas unitarias sobre el metodo Eliminar del DAOLocalidad
        /// </summary>
        [Test]
        public void TestEliminarLocalidad()
        {
            Assert.DoesNotThrow(() =>
            {
                dao.Eliminar(localidad);
            });

            Assert.DoesNotThrow(() =>
            {
                dao.Eliminar(localidad);
            });
            int id = localidad.Id + 1;
            dao.Insertar(localidad);
            localidad = FabricaEntidad.CrearEntidadLocalidad();

            Assert.DoesNotThrow(() =>
            {
                dao.Eliminar(localidad);
            });
            localidad.Id = id;
        }

        /// <summary>
        /// Metodo que realiza las pruebas unitarias sobre el metodo Consultar del DAOLocalidad
        /// </summary>
        [Test]
        public void TestConsultarLocalidad()
        {
            Entidad prueba = FabricaEntidad.CrearEntidadLocalidad();
            prueba.Id = localidad.Id;
            prueba = dao.ConsultarPorId(prueba);

            Assert.AreEqual(prueba.Id, localidad.Id);
            Assert.AreEqual(((LocalidadEvento)prueba).Nombre, ((LocalidadEvento)localidad).Nombre);
            Assert.AreEqual(((LocalidadEvento)prueba).Descripcion, ((LocalidadEvento)localidad).Descripcion);
            Assert.AreEqual(((LocalidadEvento)prueba).Coordenadas, ((LocalidadEvento)localidad).Coordenadas);

            localidad = FabricaEntidad.CrearEntidadLocalidad();

            Assert.Throws<OperacionInvalidaExcepcion>(() =>
            {
                dao.ConsultarPorId(localidad);
            });

            localidad.Id = prueba.Id;
        }

        /// <summary>
        /// Metodo que realiza las pruebas unitarias sobre el metodo Actualizar del DAOLocalidad
        /// </summary>
        [Test]
        public void TestActualizarLocalidad()
        {
            ((LocalidadEvento)localidad).Nombre = "Test2";
            ((LocalidadEvento)localidad).Descripcion = "Test2";
            ((LocalidadEvento)localidad).Coordenadas = "0,0";
            Assert.DoesNotThrow(() =>
            {
                dao.Actualizar(localidad);
            });

            Entidad prueba = dao.ConsultarPorId(localidad);
            Assert.AreEqual(((LocalidadEvento)localidad).Nombre, ((LocalidadEvento)prueba).Nombre);
            Assert.AreEqual(((LocalidadEvento)localidad).Descripcion, ((LocalidadEvento)prueba).Descripcion);
            Assert.AreEqual(((LocalidadEvento)localidad).Coordenadas, ((LocalidadEvento)prueba).Coordenadas);
            int id = localidad.Id;
            localidad.Id = 0;
            Assert.DoesNotThrow(() =>
            {
                dao.Actualizar(localidad);
            });

            localidad.Id = id;

            ((LocalidadEvento)localidad).Nombre = null;
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                dao.Actualizar(localidad);
            });

            ((LocalidadEvento)localidad).Nombre = "test";
            ((LocalidadEvento)localidad).Descripcion = null;
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                dao.Actualizar(localidad);
            });

            ((LocalidadEvento)localidad).Descripcion = "test";
            ((LocalidadEvento)localidad).Coordenadas = null;
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                dao.Actualizar(localidad);
            });

        }

        /// <summary>
        /// Metodo que realiza las pruebas unitarias sobre el comando  AgregarLocalidad
        /// </summary>
        [Test]
        public void TestComandoAgregarLocalidad()
        {

            dao.Eliminar(localidad);
            comando = FabricaComando.CrearComandoAgregarLocalidad(localidad);

            Assert.DoesNotThrow(() =>
            {
                comando.Ejecutar();
            });
            localidad.Id += 1;
            dao.Eliminar(localidad);
            ((LocalidadEvento)localidad).Nombre = null;

            comando = FabricaComando.CrearComandoAgregarLocalidad(localidad);
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                comando.Ejecutar();
            });
            ((LocalidadEvento)localidad).Nombre = "Test";
            ((LocalidadEvento)localidad).Descripcion = null;
            comando = FabricaComando.CrearComandoAgregarLocalidad(localidad);
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                comando.Ejecutar();
            });

            ((LocalidadEvento)localidad).Descripcion = "Test";
            ((LocalidadEvento)localidad).Coordenadas = null;
            comando = FabricaComando.CrearComandoAgregarLocalidad(localidad);
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                comando.Ejecutar();
            });

            localidad = FabricaEntidad.CrearEntidadLocalidad();
            comando = FabricaComando.CrearComandoAgregarLocalidad(localidad);
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                comando.Ejecutar();
            });

        }

        /// <summary>
        /// Metodo que realiza las pruebas unitarias sobre el comando  ConsultarLocalidad
        /// </summary>
        [Test]
        public void TestComandoConsultarLocalidad()
        {
            comando = FabricaComando.CrearComandoConsultarLocalidad(localidad.Id);

            Assert.DoesNotThrow(() =>
            {
                comando.Ejecutar();
            });
            Entidad prueba = comando.Retornar();
            Assert.AreEqual(((LocalidadEvento)localidad).Nombre, ((LocalidadEvento)prueba).Nombre);
            Assert.AreEqual(((LocalidadEvento)localidad).Descripcion, ((LocalidadEvento)prueba).Descripcion);
            Assert.AreEqual(((LocalidadEvento)localidad).Coordenadas, ((LocalidadEvento)prueba).Coordenadas);

            localidad.Id = 0;
            comando = FabricaComando.CrearComandoConsultarLocalidad(localidad.Id);

            Assert.Throws<OperacionInvalidaExcepcion>(() =>
            {
                comando.Ejecutar();
            });

            localidad = FabricaEntidad.CrearEntidadLocalidad();
            comando = FabricaComando.CrearComandoConsultarLocalidad(localidad.Id);
            Assert.Throws<OperacionInvalidaExcepcion>(() =>
            {
                comando.Ejecutar();
            });
            localidad.Id = prueba.Id;
        }

        /// <summary>
        /// Metodo que realiza las pruebas unitarias sobre el comando  ConsultarLocalidades
        /// </summary>
        [Test]
        public void TestComandoConsultarLocalidades()
        {
            Entidad prueba = localidad;
            comando = FabricaComando.CrearComandoConsultarLocalidades();
            Assert.DoesNotThrow(() =>
            {
                comando.Ejecutar();
            });
            foreach (Entidad entidad in comando.RetornarLista())
            {
                if (entidad.Id == localidad.Id)
                {
                    Assert.AreEqual(((LocalidadEvento)localidad).Nombre, ((LocalidadEvento)entidad).Nombre);
                    Assert.AreEqual(((LocalidadEvento)localidad).Descripcion, ((LocalidadEvento)entidad).Descripcion);
                    Assert.AreEqual(((LocalidadEvento)localidad).Coordenadas, ((LocalidadEvento)entidad).Coordenadas);
                }

            }

        }

        /// <summary>
        /// Metodo que realiza las pruebas unitarias sobre el comando  EliminarLocalidad
        /// </summary>
        [Test]
        public void TestComandoEliminarLocalidad()
        {

            comando = FabricaComando.CrearComandoEliminarLocalidad(localidad.Id);
            Assert.DoesNotThrow(() =>
            {
                comando.Ejecutar();
            });

            localidad.Id = 0;
            comando = FabricaComando.CrearComandoEliminarLocalidad(localidad.Id);
            Assert.DoesNotThrow(() =>
            {
                comando.Ejecutar();
            });

            localidad = FabricaEntidad.CrearEntidadLocalidad();
            comando = FabricaComando.CrearComandoEliminarLocalidad(localidad.Id);
            Assert.DoesNotThrow(() =>
            {
                comando.Ejecutar();
            });

        }

        /// <summary>
        /// Metodo que realiza las pruebas unitarias sobre el comando  ActualizarLocalidad
        /// </summary>
        [Test]
        public void TestComandoActualizarLocalidad()
        {
            ((LocalidadEvento)localidad).Nombre = "Test2";
            comando = FabricaComando.CrearComandoModificarLocalidad(localidad);
            Assert.DoesNotThrow(() =>
            {
                comando.Ejecutar();
            });
            Assert.AreEqual(((LocalidadEvento)localidad).Nombre,
              ((LocalidadEvento)dao.ConsultarPorId(localidad)).Nombre);

            ((LocalidadEvento)localidad).Descripcion = "Test2";
            comando = FabricaComando.CrearComandoModificarLocalidad(localidad);
            Assert.DoesNotThrow(() =>
            {
                comando.Ejecutar();
            });
            Assert.AreEqual(((LocalidadEvento)localidad).Descripcion,
              ((LocalidadEvento)dao.ConsultarPorId(localidad)).Descripcion);

            ((LocalidadEvento)localidad).Coordenadas = "0.2, 0.02";
            comando = FabricaComando.CrearComandoModificarLocalidad(localidad);
            Assert.DoesNotThrow(() =>
            {
                comando.Ejecutar();
            });
            Assert.AreEqual(((LocalidadEvento)localidad).Coordenadas,
              ((LocalidadEvento)dao.ConsultarPorId(localidad)).Coordenadas);


            ((LocalidadEvento)localidad).Nombre = null;
            comando = FabricaComando.CrearComandoModificarLocalidad(localidad);
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                comando.Ejecutar();
            });

            ((LocalidadEvento)localidad).Nombre = "Test";
            ((LocalidadEvento)localidad).Descripcion = null;
            comando = FabricaComando.CrearComandoModificarLocalidad(localidad);
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                comando.Ejecutar();
            });

            ((LocalidadEvento)localidad).Descripcion = "Test";
            ((LocalidadEvento)localidad).Coordenadas = null;
            comando = FabricaComando.CrearComandoModificarLocalidad(localidad);
            Assert.Throws<CasteoInvalidoExcepcion>(() =>
            {
                comando.Ejecutar();
            });
        }

        //COMO NO SON PRUEBAS UNITARIAS SE COMENTAN
        /*
    [Test]
    public void TestControladorAgregarLocalidad()
    {
      dao.Eliminar(localidad);
      controlador = new M8_LocalidadEventoController();
      localidad.Id += 1;

      respuesta = (Dictionary<string, object>)controlador.AgregarLocalidadEvento((LocalidadEvento)localidad);
      esperado.Add("dato", "Se ha creado una localidad");
      Assert.AreEqual(respuesta, esperado);
      esperado = new Dictionary<string, object>();
      controlador = new M8_LocalidadEventoController();

      dao.Eliminar(localidad);
      localidad.Id += 1;
 
      ((LocalidadEvento)localidad).Nombre = null;

      respuesta = (Dictionary<string, object>)controlador.AgregarLocalidadEvento((LocalidadEvento)localidad);
      esperado.Add("Error", "Specified cast is not valid.");
      Assert.AreEqual(respuesta, esperado);
      esperado = new Dictionary<string, object>();
      controlador = new M8_LocalidadEventoController();

      ((LocalidadEvento)localidad).Nombre = "Test";
      ((LocalidadEvento)localidad).Descripcion = null;

      respuesta = (Dictionary<string, object>)controlador.AgregarLocalidadEvento((LocalidadEvento)localidad);
      esperado.Add("Error", "Specified cast is not valid.");
      Assert.AreEqual(respuesta, esperado);
      esperado = new Dictionary<string, object>();
      controlador = new M8_LocalidadEventoController();

      ((LocalidadEvento)localidad).Descripcion = "Test";
      ((LocalidadEvento)localidad).Coordenadas = null;

      respuesta = (Dictionary<string, object>)controlador.AgregarLocalidadEvento((LocalidadEvento)localidad);
      esperado.Add("Error", "Specified cast is not valid.");
      Assert.AreEqual(respuesta, esperado);
      esperado = new Dictionary<string, object>();
      controlador = new M8_LocalidadEventoController();

      localidad = FabricaEntidad.CrearEntidadLocalidad();

      respuesta = (Dictionary<string, object>)controlador.AgregarLocalidadEvento((LocalidadEvento)localidad);
      esperado.Add("Error", "Specified cast is not valid.");
      Assert.AreEqual(respuesta, esperado);
      esperado = new Dictionary<string, object>();
      controlador = new M8_LocalidadEventoController();
    }

    [Test]
    public void TestControladorEliminarLocalidad()
    {
      controlador = new M8_LocalidadEventoController();
      respuesta = (Dictionary<string, object>)controlador.EliminarLocalidadEvento(localidad.Id);
      esperado.Add("dato", "Se ha eliminado una localidad");
      Assert.AreEqual(respuesta, esperado);
      esperado = new Dictionary<string, object>();
      controlador = new M8_LocalidadEventoController();

      dao.Insertar(localidad);
      int id = localidad.Id + 1;

      localidad.Id = 0;

      respuesta = (Dictionary<string, object>)controlador.EliminarLocalidadEvento(localidad.Id);
      esperado.Add("dato", "Se ha eliminado una localidad");
      Assert.AreEqual(respuesta, esperado);
      esperado = new Dictionary<string, object>();
      controlador = new M8_LocalidadEventoController();

      localidad = FabricaEntidad.CrearEntidadLocalidad();

      respuesta = (Dictionary<string, object>)controlador.EliminarLocalidadEvento(localidad.Id);
      esperado.Add("dato", "Se ha eliminado una localidad");
      Assert.AreEqual(respuesta, esperado);
      esperado = new Dictionary<string, object>();
      controlador = new M8_LocalidadEventoController();

      localidad.Id = id;

    }

    [Test]
    public void TestControladorConsultarLocalidad()
    {
      Object prueba;
      Entidad entidad;
      controlador = new M8_LocalidadEventoController();
      respuesta = (Dictionary<string, object>)controlador.ConsultarLocalidadEvento(localidad.Id);
      respuesta.TryGetValue("dato",out prueba);
      entidad = (LocalidadEvento)prueba;
      Assert.AreEqual(((LocalidadEvento)localidad).Nombre, ((LocalidadEvento)entidad).Nombre);
      Assert.AreEqual(((LocalidadEvento)localidad).Descripcion, ((LocalidadEvento)entidad).Descripcion);
      Assert.AreEqual(((LocalidadEvento)localidad).Coordenadas, ((LocalidadEvento)entidad).Coordenadas);
      esperado = new Dictionary<string, object>();
      controlador = new M8_LocalidadEventoController();

      int id = localidad.Id;
      localidad = FabricaEntidad.CrearEntidadLocalidad();


      respuesta = (Dictionary<string, object>)controlador.ConsultarLocalidadEvento(localidad.Id);
      esperado.Add("Error", "Operation is not valid due to the current state of the object.");
      Assert.AreEqual(respuesta, esperado);
      esperado = new Dictionary<string, object>();
      controlador = new M8_LocalidadEventoController();

      localidad.Id = id;
    }

    [Test]
    public void TestControladorConsultarLocalidades()
    {
      Object prueba;
      List<Entidad> entidades;
      controlador = new M8_LocalidadEventoController();
      respuesta = (Dictionary<string, object>)controlador.ListaLocalidadEventos();
      respuesta.TryGetValue("dato", out prueba);
      entidades = (List<Entidad>)prueba;

      foreach (Entidad entidad in entidades)
      {
        if (entidad.Id == localidad.Id)
        {
          Assert.AreEqual(((LocalidadEvento)localidad).Nombre, ((LocalidadEvento)entidad).Nombre);
          Assert.AreEqual(((LocalidadEvento)localidad).Descripcion, ((LocalidadEvento)entidad).Descripcion);
          Assert.AreEqual(((LocalidadEvento)localidad).Coordenadas, ((LocalidadEvento)entidad).Coordenadas);
        }

      }

    }

    [Test]
    public void TestControladorActualizarLocalidad()
    {
      controlador = new M8_LocalidadEventoController();
      ((LocalidadEvento)localidad).Nombre = "Test2";
      respuesta = (Dictionary<string, object>)controlador.ActualizarLocalidadEvento((LocalidadEvento)localidad);
      esperado.Add("dato", "Se ha modificado una localidad");
      Assert.AreEqual(respuesta, esperado);
      Assert.AreEqual(((LocalidadEvento)localidad).Nombre,
      ((LocalidadEvento)dao.ConsultarPorId(localidad)).Nombre);
      esperado = new Dictionary<string, object>();
      controlador = new M8_LocalidadEventoController();

      ((LocalidadEvento)localidad).Descripcion = "Test2";
      respuesta = (Dictionary<string, object>)controlador.ActualizarLocalidadEvento((LocalidadEvento)localidad);
      esperado.Add("dato", "Se ha modificado una localidad");
      Assert.AreEqual(respuesta, esperado);
      Assert.AreEqual(((LocalidadEvento)localidad).Descripcion,
      ((LocalidadEvento)dao.ConsultarPorId(localidad)).Descripcion);
      esperado = new Dictionary<string, object>();
      controlador = new M8_LocalidadEventoController();

      ((LocalidadEvento)localidad).Coordenadas = "0.2, 0.02";
      respuesta = (Dictionary<string, object>)controlador.ActualizarLocalidadEvento((LocalidadEvento)localidad);
      esperado.Add("dato", "Se ha modificado una localidad");
      Assert.AreEqual(respuesta, esperado);
      Assert.AreEqual(((LocalidadEvento)localidad).Coordenadas,
      ((LocalidadEvento)dao.ConsultarPorId(localidad)).Coordenadas);
      esperado = new Dictionary<string, object>();
      controlador = new M8_LocalidadEventoController();
    


      ((LocalidadEvento)localidad).Nombre = null;
      respuesta = (Dictionary<string, object>)controlador.ActualizarLocalidadEvento((LocalidadEvento)localidad);
      esperado.Add("Error", "Specified cast is not valid.");
      Assert.AreEqual(respuesta, esperado);
      esperado = new Dictionary<string, object>();
      controlador = new M8_LocalidadEventoController();

      ((LocalidadEvento)localidad).Nombre = "Test";
      ((LocalidadEvento)localidad).Descripcion = null;
      respuesta = (Dictionary<string, object>)controlador.ActualizarLocalidadEvento((LocalidadEvento)localidad);
      esperado.Add("Error", "Specified cast is not valid.");
      Assert.AreEqual(respuesta, esperado);
      esperado = new Dictionary<string, object>();
      controlador = new M8_LocalidadEventoController();

      ((LocalidadEvento)localidad).Descripcion = "Test";
      ((LocalidadEvento)localidad).Coordenadas = null;
      respuesta = (Dictionary<string, object>)controlador.ActualizarLocalidadEvento((LocalidadEvento)localidad);
      esperado.Add("Error", "Specified cast is not valid.");
      Assert.AreEqual(respuesta, esperado);
      esperado = new Dictionary<string, object>();
      controlador = new M8_LocalidadEventoController();
    }
    */

        /// <summary>
        /// Metodo que "limpia" todos los registros creados por las pruebas
        /// </summary>
        [TearDown]
        public void TearDownLocalidad()
        {
            dao.Eliminar(localidad);
        }

    }
}
