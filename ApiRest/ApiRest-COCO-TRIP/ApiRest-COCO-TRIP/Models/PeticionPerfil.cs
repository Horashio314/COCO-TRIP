using System.Data;
using Npgsql;

namespace ApiRest_COCO_TRIP.Models
{
  public class PeticionPerfil
  {

    public ConexionBase conexion;

    public PeticionPerfil()
    {

      conexion = new ConexionBase();

    }

    /// <summary>
    /// Metodo para consultar el id del usuario a través de su nombre de usuario
    /// </summary>
    /// <param name="nombreUsuario">Nombre de usuario</param>
    /// <returns>Id del usuario, si retorna -1 es que no existe</returns>
    public int ConsultarIdDelUsuario( string nombreUsuario )
    {

      int id;
      id = -1; //Coloco -1 para hacer una comparación más abajo y saber si entró ene l ciclo o no.

      conexion.Conectar();
      ConexionBase con = new ConexionBase();
      con.Conectar();
      NpgsqlCommand comm = new NpgsqlCommand("consultarusuariosolonombre", con.SqlConexion);
      comm.CommandType = CommandType.StoredProcedure;
      comm.Parameters.AddWithValue( NpgsqlTypes.NpgsqlDbType.Varchar , nombreUsuario );
      NpgsqlDataReader pgread = comm.ExecuteReader();

      while (pgread.Read())
      {

        id = pgread.GetInt32(0);

      }
      conexion.Desconectar();

      return id;

    }

    /// <summary>
    /// Metodo para consultar el id de la categoria a través de su nombre
    /// </summary>
    /// <param name="nombreCategoria">Nombre de la categoría</param>
    /// <returns>Id de la categoria </returns>
    public int ConsultarIdDeCategoria(string nombreCategoria)
    {

      int id;
      id = 1; //Coloco -1 para hacer una comparación más abajo y saber si entró ene l ciclo o no.

      /*ConexionBase conexion = new ConexionBase();
      conexion.Conectar();
      ConexionBase con = new ConexionBase();
      con.Conectar();
      NpgsqlCommand comm = new NpgsqlCommand("consultarusuariosolonombre", con.SqlConexion);
      comm.CommandType = CommandType.StoredProcedure;
      comm.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, nombreCategoria);
      NpgsqlDataReader pgread = comm.ExecuteReader();

      while (pgread.Read())
      {

        id = pgread.GetInt32(0);

      }
      conexion.Desconectar();

      return id;*/

      return id;

    }

    /// <summary>
    /// Se agrega la preferencia a la base de datos
    /// </summary>
    /// <param name="idUsuario">Id del usuario </param>
    /// <param name="idCategoria">Id de la categoria</param>
    public void AgregarPreferencia(int idUsuario, int idCategoria)
    {

      NpgsqlCommand command;
      NpgsqlDataReader pgread;

      conexion.Conectar();
      command = new NpgsqlCommand("agregarPreferencia", conexion.SqlConexion);
      command.CommandType = CommandType.StoredProcedure;
      command.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, idUsuario);
      pgread = command.ExecuteReader();
      pgread.Read();
      conexion.Desconectar();

    }
  }
}
