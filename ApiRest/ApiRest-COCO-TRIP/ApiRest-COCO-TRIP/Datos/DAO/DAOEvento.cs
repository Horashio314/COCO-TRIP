using ApiRest_COCO_TRIP.Comun.Excepcion;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Reflection;
using ApiRest_COCO_TRIP.Datos.Entity;
using NpgsqlTypes;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
  /**
  * <summary>Clase que recibe todas las peticiones relacionadas a eventos</summary>
  **/


  public class DAOEvento : DAO
  {

    private NpgsqlParameter parametro;
    private NpgsqlDataReader leerDatos;
    private Entidad evento;
    private List<Entidad> lista;


    public DAOEvento()
    {
      parametro = new NpgsqlParameter();
      lista = new List<Entidad>();
    }
    public override void Actualizar(Entidad objeto)
    {
      Conectar();
      try
      {

        evento = (Evento)objeto;
        Conectar();
        Comando = SqlConexion.CreateCommand();
        Comando.CommandText = "actualizarEventoPorId";
        Comando.CommandType = CommandType.StoredProcedure;

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer;
        parametro.Value = evento.Id;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = ((Evento)evento).Nombre;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = ((Evento)evento).Descripcion;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Double;
        parametro.Value = ((Evento)evento).Precio;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Timestamp;
        parametro.Value = ((Evento)evento).FechaInicio;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Timestamp;
        parametro.Value = ((Evento)evento).FechaFin;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Time;
        parametro.Value = ((Evento)evento).HoraInicio;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Time;
        parametro.Value = ((Evento)evento).HoraFin;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = ((Evento)evento).Foto;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer;
        parametro.Value = ((Evento)evento).IdLocalidad ;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer;
        parametro.Value = ((Evento)evento).IdCategoria;
        Comando.Parameters.Add(parametro);

        leerDatos = Comando.ExecuteReader();
        leerDatos.Close();

      }
      catch (NpgsqlException e)
      {
        BaseDeDatosExcepcion ex = new BaseDeDatosExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }
      catch (InvalidCastException e)
      {
        CasteoInvalidoExcepcion ex = new CasteoInvalidoExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }

      catch (InvalidOperationException e)
      {
        OperacionInvalidaException ex = new OperacionInvalidaException(e);
        ex.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw ex;
      }

      finally
      {
        Desconectar();
      }
    }

    public override List<Entidad> ConsultarLista(Entidad objeto)
    {
      
      lista = new List<Entidad>();
      Entidad categoria = (Categoria)objeto;
      try
      {
        Conectar();
        Comando = SqlConexion.CreateCommand();
        Comando.CommandText = "ConsultarEventoPorIdCategoria";
        Comando.CommandType = CommandType.StoredProcedure;

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer;
        parametro.Value = categoria.Id;
        Comando.Parameters.Add(parametro);

        leerDatos = Comando.ExecuteReader();
        while (leerDatos.Read())
        {
          evento = FabricaEntidad.CrearEntidadEvento();
          DateTime horaInicio = new DateTime();
          horaInicio.AddHours(leerDatos.GetTimeSpan(6).Hours);
          horaInicio.AddMinutes(leerDatos.GetTimeSpan(6).Minutes);

          DateTime horaFin = new DateTime();
          horaFin.AddHours(leerDatos.GetTimeSpan(7).Hours);
          horaFin.AddMinutes(leerDatos.GetTimeSpan(7).Minutes);

          ((Evento)evento).Id = leerDatos.GetInt32(0);
          ((Evento)evento).Nombre = leerDatos.GetString(1);
          ((Evento)evento).Descripcion = leerDatos.GetString(2);
          ((Evento)evento).Precio = leerDatos.GetDouble(3);
          ((Evento)evento).FechaInicio = leerDatos.GetDateTime(4);
          ((Evento)evento).FechaFin = leerDatos.GetDateTime(5);
          ((Evento)evento).HoraInicio = horaInicio;
          ((Evento)evento).HoraFin = horaFin;
          ((Evento)evento).Foto = leerDatos.GetString(8);
          ((Evento)evento).IdLocalidad = leerDatos.GetInt32(9);
          lista.Add(evento);
        }
      }
      catch (NpgsqlException e)
      {
        BaseDeDatosExcepcion ex = new BaseDeDatosExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }
      catch (InvalidCastException e)
      {
        CasteoInvalidoExcepcion ex = new CasteoInvalidoExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }

      catch (InvalidOperationException e)
      {
        OperacionInvalidaException ex = new OperacionInvalidaException(e);
        ex.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw ex;
      }
      finally
      {
        Desconectar();
      }
      return lista;
    }

    public override Entidad ConsultarPorId(Entidad objeto)
    {

      try
      {
        evento = (Evento)objeto;
        Conectar();
        Comando = SqlConexion.CreateCommand();
        Comando.CommandText = "ConsultarEventoPorIdEvento";
        Comando.CommandType = CommandType.StoredProcedure;

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer;
        parametro.Value = evento.Id;
        Comando.Parameters.Add(parametro);

        leerDatos = Comando.ExecuteReader();
        leerDatos.Read();

        evento.Id = leerDatos.GetInt32(0);
        ((Evento)evento).Nombre = leerDatos.GetString(1);
        ((Evento)evento).Descripcion = leerDatos.GetString(2);
        ((Evento)evento).Precio = leerDatos.GetInt64(3);
        ((Evento)evento).FechaInicio = leerDatos.GetDateTime(4);
        ((Evento)evento).FechaFin = leerDatos.GetDateTime(5);
        DateTime horaInicio = new DateTime();
        horaInicio.AddHours(leerDatos.GetTimeSpan(6).Hours);
        horaInicio.AddMinutes(leerDatos.GetTimeSpan(6).Minutes);
        ((Evento)evento).HoraInicio = horaInicio;
        DateTime horaFin = new DateTime();
        horaFin.AddHours(leerDatos.GetTimeSpan(7).Hours);
        horaFin.AddMinutes(leerDatos.GetTimeSpan(7).Minutes);
        ((Evento)evento).HoraFin = horaFin;
        ((Evento)evento).Foto = leerDatos.GetString(8);
        ((Evento)evento).IdLocalidad = leerDatos.GetInt32(9);
        ((Evento)evento).IdCategoria = leerDatos.GetInt32(10);
        leerDatos.Close();
      }
      catch (NpgsqlException e)
      {
        BaseDeDatosExcepcion ex = new BaseDeDatosExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }
      catch (InvalidCastException e)
      {
        CasteoInvalidoExcepcion ex = new CasteoInvalidoExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }

      catch (InvalidOperationException e)
      {
        OperacionInvalidaException ex = new OperacionInvalidaException(e);
        ex.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw ex;
      }
      finally
      {
        Desconectar();
      }
      return evento;
    }

    public override void Eliminar(Entidad objeto)
    {
      Conectar();
      try
      {

        evento = (Evento)objeto;
        Conectar();
        Comando = SqlConexion.CreateCommand();
        Comando.CommandText = "eliminareventoporid";
        Comando.CommandType = CommandType.StoredProcedure;

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer;
        parametro.Value = evento.Id;
        Comando.Parameters.Add(parametro);
        leerDatos = Comando.ExecuteReader();
        leerDatos.Close();
      }
      catch (NpgsqlException e)
      {
        BaseDeDatosExcepcion ex = new BaseDeDatosExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }
      catch (InvalidCastException e)
      {
        CasteoInvalidoExcepcion ex = new CasteoInvalidoExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }

      catch (InvalidOperationException e)
      {
        OperacionInvalidaException ex = new OperacionInvalidaException(e);
        ex.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw ex;
      }

      finally
      {
        Desconectar();
      }
    }

    public override void Insertar(Entidad objeto)
    {
      Conectar();
      try
      {

        evento = (Evento)objeto;
        Conectar();
        Comando = SqlConexion.CreateCommand();
        Comando.CommandText = "InsertarEvento";
        Comando.CommandType = CommandType.StoredProcedure;

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = ((Evento)evento).Nombre;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = ((Evento)evento).Descripcion;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Double;
        parametro.Value = ((Evento)evento).Precio;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Timestamp;
        parametro.Value = ((Evento)evento).FechaInicio;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Timestamp;
        parametro.Value = ((Evento)evento).FechaFin;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Time;
        parametro.Value = ((Evento)evento).HoraInicio.Hour + ":" + ((Evento)evento).HoraInicio.Minute + ":00";
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Time;
        parametro.Value = ((Evento)evento).HoraFin.Hour + ":" + ((Evento)evento).HoraFin.Minute + ":00";
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Varchar;
        parametro.Value = ((Evento)evento).Foto;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer;
        parametro.Value = ((Evento)evento).IdLocalidad;
        Comando.Parameters.Add(parametro);

        parametro = new NpgsqlParameter();
        parametro.NpgsqlDbType = NpgsqlDbType.Integer;
        parametro.Value = ((Evento)evento).IdCategoria;
        Comando.Parameters.Add(parametro);

        leerDatos = Comando.ExecuteReader();
        leerDatos.Close();
      }
      catch (NpgsqlException e)
      {
        BaseDeDatosExcepcion ex = new BaseDeDatosExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }
      catch (InvalidCastException e)
      {
        CasteoInvalidoExcepcion ex = new CasteoInvalidoExcepcion(e);
        ex.NombreMetodos = this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name;
        throw ex;
      }

      catch (InvalidOperationException e)
      {
        OperacionInvalidaException ex = new OperacionInvalidaException(e);
        ex.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw ex;
      }

      finally
      {
       Desconectar();
      }
    }
  }
}
