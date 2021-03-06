using NUnit.Framework;
using ApiRest_COCO_TRIP.Models;
using ApiRest_COCO_TRIP.Models.Excepcion;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Controllers;
using Npgsql;
using System;

namespace ApiRestPruebas.M2
{
  [TestFixture]
  class M2UnitTest
  {

    private Usuario usuario;
    private Categoria categoria, categoria2;
    private int posicionDelElemento;
    private M2_PerfilPreferenciasController apiRest;
    private PeticionPerfil peticion;
    private int idUsuario;
    private bool probar;
    NpgsqlDataReader pgread;

    [SetUp]
    public void SetUp() {
      
      //Creando los objetos vacios
      usuario = new Usuario();
      categoria = new Categoria();
      categoria2 = new Categoria();
      peticion = new PeticionPerfil();
      apiRest = new M2_PerfilPreferenciasController();
     
      //Inicializando categoria
      categoria.Id = 1;
      categoria.Nombre = "Deporte";
      categoria.Descripcion = "Los deportes son geniales";
      categoria.Nivel = 1;
      categoria.Estatus = true;

      categoria2.Id = 2;
      categoria2.Nombre = "Deporte";
      categoria2.Descripcion = "Los deportes son geniales";
      categoria2.Nivel = 2;
      categoria2.Estatus = true;

      usuario.NombreUsuario = "Hola";

    }

    [Test]
    [Category("Objeto")]
    public void AgregandoObjetoCategoriaAlList()
    {

      usuario.AgregarPreferencia( categoria  );
      usuario.AgregarPreferencia( categoria2 );

      Assert.IsNotNull(  usuario              );
      Assert.IsNotNull(  categoria            );
      Assert.IsNotNull(  categoria2           );
      Assert.IsNotNull(  usuario.Preferencias );

      Assert.IsNotEmpty( usuario.Preferencias               );
      Assert.AreEqual  ( categoria  , usuario.Preferencias[0] );
      Assert.AreEqual  ( categoria2 , usuario.Preferencias[1] );

    }

    [Test]
    [Category("Objeto")]
    public void BusquedaObjetoCategoriaDelListEncontrado()
    {

      usuario.AgregarPreferencia( categoria  );
      usuario.AgregarPreferencia( categoria2 );
      posicionDelElemento = usuario.BusquedaDePreferencia( categoria );

      Assert.IsNotNull( usuario               );
      Assert.IsNotNull( categoria             );
      Assert.IsNotNull( usuario.Preferencias  );

      Assert.IsNotEmpty( usuario.Preferencias    );
      Assert.AreEqual  ( 0 , posicionDelElemento );

    }

    [Test]
    [Category("Objeto")]
    public void BusquedaObjetoCategoriaDelListNoEncontrado()
    {

      usuario.AgregarPreferencia(categoria);
      posicionDelElemento = usuario.BusquedaDePreferencia(categoria2);

      Assert.IsNotNull( usuario              );
      Assert.IsNotNull( categoria            );
      Assert.IsNotNull( usuario.Preferencias );

      Assert.IsNotEmpty( usuario.Preferencias     );
      Assert.AreEqual  ( -1, posicionDelElemento  );

    }

    [Test]
    [Category("Insertar")]
    public void AgregarPreferencia()
    {

      int count = 1;
      ConexionBase conexion = new ConexionBase();
      List<Categoria> lista = new List<Categoria>();

      //lista = apiRest.AgregarPreferencias(1, 1);
      conexion.Conectar();
      NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(*) cantidad " +
                    "FROM preferencia where pr_usuario = 1 and pr_categoria = 1", conexion.SqlConexion);
      pgread = command.ExecuteReader();

      while(pgread.Read())
        count = pgread.GetInt32(0);

      conexion.Desconectar();
      Assert.AreEqual( count , lista.Count );
      
    }

    [Test]
    [Category("Eliminar")]
    public void EliminarPrefencia()
    {

      int count = 0;
      ConexionBase conexion = new ConexionBase();
      List<Categoria> lista = new List<Categoria>();

      //lista = apiRest.EliminarPreferencias( 1 , 1);
      conexion.Conectar();
      NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(*) cantidad " +
                    "FROM preferencia where pr_usuario = 1 and pr_categoria = 1", conexion.SqlConexion);
      pgread = command.ExecuteReader();

      while (pgread.Read())
        count = pgread.GetInt32(0);

      conexion.Desconectar();
      Assert.AreEqual(lista.Count, count);

    }

    // Usuario con id 7 agregado previo a la PU
    [TestCase(7,"Ronald","Navas","1993-27-02","M")]
    [Category("Modificar")]
    public void Model_ModificarDatosUsuario(int idUsuario, string nombre, string apellido, string fecha, string genero)
    {
      DateTime fechaConveritda = Convert.ToDateTime(fecha);
      peticion.ModificarDatos(idUsuario,nombre, apellido, fecha, genero);
      usuario = peticion.ObtenerDatosUsuario(idUsuario);
      Assert.AreEqual(nombre, usuario.Nombre);
      Assert.AreEqual(apellido, usuario.Apellido);
      Assert.AreEqual(fechaConveritda, usuario.FechaNacimiento);
      Assert.AreEqual(genero, usuario.Genero);
    }

    // Usuario con id 15 agregado previo a la PU
    [TestCase(7,"ronald", "navas")]
    [TestCase(15, "gianfranco", "verrocchi")]
    [Category("Usuario")]
    public void Model_ObtenerDatosUsuario(int idUsuario, string nombre, string apellido)
    {
      usuario = peticion.ObtenerDatosUsuario(idUsuario);
      Assert.AreEqual(nombre, usuario.Nombre);
      Assert.AreEqual(apellido, usuario.Apellido);
    }

    [Test]
    [Category("Consultar")]
    public void ConsultarPreferencias()
    {

      List<Categoria> preferencias = new List<Categoria>();

      //preferencias = apiRest.BuscarPreferencias(1);
      Assert.AreEqual(categoria.Nombre, preferencias[0].Nombre);

    }

    [TestCase(7, "ronald", "navas")]
    [Category("Usuario")]
    public void ObtenerDatosUsuario(int idUsuario, string nombre, string apellido)
    {
      //usuario = apiRest.ObtenerDatosUsuario(idUsuario);
      Assert.AreEqual(nombre, usuario.Nombre);
      Assert.AreEqual(apellido, usuario.Apellido);
    }

    /* Esta Prueba Unitaria confirma si en efecto se cambia la contraseña, se pasan como paramatros el username,
        la contraseña actual y la contraseña nueva*/
    [Test]
    public void CambiarPass()
    {
      string username, passActual, passNueva;
      username = "conexion";
      passActual = "123";
      passNueva = "HOLA";
      probar = apiRest.CambiarPass(username, passActual, passNueva);
      Assert.AreEqual(true, probar);
    }

    [Test]
    [Category("Consultar")]
    public void BuscarPreferenciasDeUsuarioSegunParteDelString()
    {
      string consulta;
      string consultaCompleta;
      List<Categoria> lista = new List<Categoria>();
      consulta = "turis";
      consultaCompleta = "Turismo";

      //lista = apiRest.BuscarCategorias( 1, consulta);
      Assert.AreEqual(consultaCompleta, lista[0].Nombre);

    }

    /*Esta Prueba Unitaria confirma si se puede obtener la contraseña, para ello se pasan como parametros el username*/
    [Test]
    public void ObtenerPass()
    {
      string username, pass, passobtenida;
      username = "jelo";
      pass = "123";

      passobtenida = peticion.ObtenerPassword(username);
      Assert.AreEqual(pass, passobtenida);
    }

    /*Esta Prueba Unitaria confirma si puede consultar el id del usario, para ello se pasa como parametro el username*/
    [Test]
    public void ConsultarIdTest()
    {
      int test, testing;
      string username;
      testing = 2;
      username = "jelo";

      test = peticion.ConsultarIdDelUsuario(username);
      Assert.AreEqual(test, testing);
    }

    /*Esta Prueba Unitaria confirma si se borra el usuario, para ello se pasa como parametros el username y la contraseña*/
    [Test]
    public void BorrarUsuarioTest()
    {
      string username, pass;
      username = "jelo";
      pass = "wwe";

      probar = apiRest.BorrarUsuario(username, pass);
      Assert.AreEqual(true, probar);

    }

    [Test]
    [Category("Excepciones")]
    public void ExcepcionAgregarUsuario()
    {

      Assert.Throws(typeof(BaseDeDatosExcepcion), AgregarUsuarioEx);


    }

    public void AgregarUsuarioEx()
    {
      apiRest.AgregarPreferencias(-1, -1);
    }


    [TearDown]
    public void TearDown() {

      usuario    = null;
      categoria  = null;
      categoria2 = null;
      pgread = null;

    }

  }
}
