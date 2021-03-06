using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using Npgsql;
using System.Data;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
  public class DAOItinerario : DAO
  {
    private Itinerario itinerario;
    private NpgsqlDataReader respuesta;
    private NpgsqlCommand comando;
        /// <summary>
        /// Metodo encargado de actualizar los datos de un itinerario
        /// </summary>
        /// <param name="objeto">Metodo que contiene el id del itinerario y los datos nuevos</param>
        public override void Actualizar(Entidad objeto)
    {
      try
      {
        Itinerario itinerario = (Itinerario)objeto;
        base.Conectar();
        comando = new NpgsqlCommand("mod_itinerario", base.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, itinerario.Id);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, itinerario.Nombre);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, itinerario.FechaInicio);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, itinerario.FechaFin);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, itinerario.IdUsuario);
        respuesta = comando.ExecuteReader();
        respuesta.Read();
        base.Desconectar();
      }
      catch (NpgsqlException e)
      {
        base.Desconectar();
        throw e;
      }
      catch (InvalidCastException e)
      {
        base.Desconectar();
        throw e;
      }
      catch (Exception e)
      {
        base.Desconectar();
        throw e;
      }
    }
        /// <summary>
        /// Metodo que retorna la lista de itinerarios de un usuario
        /// </summary>
        /// <param name="objeto">objeto con el id del usuario</param>
        public override List<Entidad> ConsultarLista(Entidad objeto)
    {
      List<Entidad> itinerarios = new List<Entidad>(); // Lista de itinerarios de un usuario
      Usuario usuario = (Usuario)objeto;
      try
      {
        base.Conectar();
        comando = new NpgsqlCommand("consultar_itinerarios", base.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, usuario.Id);
        respuesta = comando.ExecuteReader();
        int auxiliar = 0;

        //Recorremos los registros devueltos
        while (respuesta.Read())
        {
          //Creo el objeto itinerario
          Itinerario iti;
          if (!respuesta.IsDBNull(3) && (!respuesta.IsDBNull(4)))
          {
            iti = new Itinerario(respuesta.GetInt32(0), respuesta.GetString(2), respuesta.GetDateTime(3), respuesta.GetDateTime(4),
              respuesta.GetInt32(1), respuesta.GetBoolean(7));
          }
          else
          {
            iti = new Itinerario(respuesta.GetInt32(0), respuesta.GetString(2), respuesta.GetInt32(1), respuesta.GetBoolean(7));
          }
          //Se revisa si el registro de itinerario en la base ya se encuentra en la lista de itinerarios del usuario
          if (itinerarios.Count == 0) itinerarios.Add(iti);
          foreach (Itinerario itinerario in itinerarios)
          {
            if (itinerario.Id == iti.Id) auxiliar = 1;
          }
          if (auxiliar != 1) itinerarios.Add(iti);
          auxiliar = 0;

          //Agregamos los eventos, actividades y lugares a la lista correspondiente
          //Si existe lugar turistico en este registro
          if (!respuesta.IsDBNull(8))
          {
            dynamic lugar = new System.Dynamic.ExpandoObject();
            lugar.Id = respuesta.GetInt32(8);
            lugar.Nombre = respuesta.GetString(9);
            lugar.Descripcion = respuesta.GetString(10);
            lugar.Costo = respuesta.GetDouble(11);
            lugar.Tipo = "Lugar Turistico";
            if ((!respuesta.IsDBNull(5)) && (!respuesta.IsDBNull(6)))
            {
              lugar.FechaInicio = respuesta.GetDateTime(5);
              lugar.FechaFin = respuesta.GetDateTime(6);
            }
            Itinerario itin = (Itinerario)itinerarios[itinerarios.Count - 1];
            itin.Items_agenda.Add(lugar);
            //Posible error
            //itinerarios[itinerarios.Count - 1] = itin;
            itinerarios.RemoveAt(itinerarios.Count - 1);
            itinerarios.Add(itin);
          }
          //Si existe actividad en este registro
          if (!respuesta.IsDBNull(12))
          {
            dynamic actividad = new System.Dynamic.ExpandoObject();
            actividad.Id = respuesta.GetInt32(12);
            actividad.Nombre = respuesta.GetString(13);
            actividad.Descripcion = respuesta.GetString(14);
            actividad.Duracion = respuesta.GetTimeSpan(15);
            actividad.Foto = respuesta.GetString(16);
            actividad.Tipo = "Actividad";
            if ((!respuesta.IsDBNull(5)) && (!respuesta.IsDBNull(6)))
            {
              actividad.FechaInicio = respuesta.GetDateTime(5);
              actividad.FechaFin = respuesta.GetDateTime(6);
            }
            Itinerario itin = (Itinerario)itinerarios[itinerarios.Count - 1];
            itin.Items_agenda.Add(actividad);
            //Posible error
            //itinerarios[itinerarios.Count - 1] = itin;
            itinerarios.RemoveAt(itinerarios.Count - 1);
            itinerarios.Add(itin);
          }
          //Si existe evento en este registro
          if (!respuesta.IsDBNull(17))
          {
            dynamic evento = new System.Dynamic.ExpandoObject();
            evento.Id = respuesta.GetInt32(17);
            evento.Nombre = respuesta.GetString(18);
            evento.Descripcion = respuesta.GetString(19);
            evento.Precio = respuesta.GetInt32(20);
            evento.FechaInicio = respuesta.GetDateTime(21);
            evento.FechaFin = respuesta.GetDateTime(22);
            evento.HoraInicio = respuesta.GetTimeSpan(23);
            evento.HoraFin = respuesta.GetTimeSpan(24);
            evento.Foto = respuesta.GetString(25);
            evento.Tipo = "Evento";
            Itinerario itin = (Itinerario)itinerarios[itinerarios.Count - 1];
            itin.Items_agenda.Add(evento);
            //Posible error
            //itinerarios[itinerarios.Count - 1] = itin;
            itinerarios.RemoveAt(itinerarios.Count - 1);
            itinerarios.Add(itin);
          }
        }
        base.Desconectar();
        return itinerarios;
      }
      catch (NpgsqlException sql)
      {
        base.Desconectar();
        throw sql;
      }
      catch (ArgumentException arg)
      {
        base.Desconectar();
        throw arg;
      }
      catch (InvalidCastException cast)
      {
        base.Desconectar();
        throw cast;
      }
      catch (Exception e)
      {
        base.Desconectar();
        throw e;
      }
    }


    public override Entidad ConsultarPorId(Entidad objeto)
    {
      throw new NotImplementedException();
    }
        /// <summary>
        /// Elimina el itinerario con el id
        /// </summary>
        /// <param name="objeto">objeto con el id del itinerario</param>
        public override void Eliminar(Entidad objeto)
    {
      try
      {
        Itinerario itinerario = (Itinerario)objeto;
        base.Conectar();
        comando = new NpgsqlCommand("del_itinerario", base.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, itinerario.Id);
        respuesta = comando.ExecuteReader();
        respuesta.Read();
        Boolean resp = respuesta.GetBoolean(0);
        base.Desconectar();
        //return resp;
      }
      catch (NpgsqlException e)
      {
        base.Desconectar();
        throw e;
      }
      catch (Exception e)
      {
        base.Desconectar();
        throw e;
      }
    }
        /// <summary>
        /// Objeto itinerario cn sus datos a inserta
        /// </summary>
        /// <param name="objeto">itinerario</param>
        public override void Insertar(Entidad objeto)
    {
      try
      {
        itinerario = (Itinerario)objeto;
        base.Conectar();
        comando = new NpgsqlCommand("add_itinerario", base.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, itinerario.Nombre);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, itinerario.IdUsuario);
        respuesta = comando.ExecuteReader();
        respuesta.Read();
        itinerario.Id = respuesta.GetInt16(0);
        itinerario.Nombre = respuesta.GetString(1);
        base.Desconectar();
      }
      catch (NpgsqlException e)
      {
        base.Desconectar();
        throw e;
      }
      catch (InvalidCastException e)
      {
        base.Desconectar();
        throw e;
      }
      catch (NullReferenceException e)
      {
        base.Desconectar();
        throw e;
      }
      catch (Exception e)
      {
        base.Desconectar();
        throw e;
      }
    }
    /// <summary>
    /// Activar o desactivar la visibilidad del itinerario en el calendario
    /// </summary>
    /// <param name="idusuario">id del usuario al cual le pertenecen los itinerarios</param>
    /// <param name="iditinerario">id del itinerario a Activar/Desactivar</param>
    /// <param name="visible">parametro que determina si se activa(true) o desactiva(false) el itinerario en el calendario</param>
    /// <returns>true si se Activo/Desactivo exitosamente, false de lo contrario</returns>
    public Boolean SetVisible(int idusuario, int iditinerario, Boolean visible)
    {
      Boolean visible_sql = false;
      try
      {
        base.Conectar();
        comando = new NpgsqlCommand("setVisible", base.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, idusuario);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Boolean, visible);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, iditinerario);
        respuesta = comando.ExecuteReader();
        respuesta.Read();
        visible_sql = respuesta.GetBoolean(0);
        base.Desconectar();
        return visible_sql;
      }
      catch (NpgsqlException sql)
      {
        base.Desconectar();
        throw sql;
      }
      catch (InvalidCastException cast)
      {
        base.Desconectar();
        throw cast;
      }
      catch (Exception e)
      {
        base.Desconectar();
        throw e;
      }
    }

    /// <summary>
    ///  Consulta las actividades por nombre, o similiares.
    /// </summary>
    /// <param name="busqueda">Palabra cuya similitud se busca en el nombre de la actividad que se esta buscando.</param>
    /// <returns>Retorna una lista con las actividades que tengan coincidencia.</returns>
    public List<Entidad> ConsultarActividades(Entidad objeto)
    {
      List<Entidad> list_actividades = new List<Entidad>();
      Actividad actividad = (Actividad)objeto;
      try
      {
        base.Conectar();
        comando = new NpgsqlCommand("consultar_actividades", base.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, actividad.Nombre);
        respuesta = comando.ExecuteReader();

        //Se recorre los registros devueltos.
        while (respuesta.Read())
        {
          Actividad actividads = new Actividad();
          actividads.Id = respuesta.GetInt32(0);
          actividads.Nombre = respuesta.GetString(1);
          //actividad.Foto = pgread.GetString(2);


          list_actividades.Add(actividads);
        }

        base.Desconectar();
        return list_actividades;
      }
      catch (NpgsqlException e)
      {
        base.Desconectar();
        throw e;
      }
      catch (Exception e)
      {
        base.Desconectar();
        throw e;
      }
    }

    /// <summary>
    /// Consulta los eventos por nombre, o similiares.
    /// </summary>
    /// <param name="busqueda">Palabra cuya similitud se busca en el nombre del evento que se esta buscando.</param>
    /// <returns>Retorna una lista con los eventos que tengan coincidencia.</returns>
    public List<Entidad> ConsultarEventos(Entidad objeto)
    {
      List<Entidad> list_eventos = new List<Entidad>();
      Evento evento = (Evento)objeto;
      try
      {
        base.Conectar();
        comando = new NpgsqlCommand("consultar_eventos", base.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, evento.Nombre);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, evento.FechaInicio);
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Date, evento.FechaFin);
        respuesta = comando.ExecuteReader();

        //Se recorre los registros devueltos.
        while (respuesta.Read())
        {
          Evento eventos = new Evento(respuesta.GetInt32(0), respuesta.GetString(1));
          evento.Foto = respuesta.GetString(2);
          list_eventos.Add(eventos);
        }

        base.Desconectar();
        return list_eventos;
      }
      catch (NpgsqlException e)
      {
        base.Desconectar();
        throw e;
      }
      catch (Exception e)
      {
        base.Desconectar();
        throw e;
      }
    }

    /// <summary>
    /// Consulta los lugares turisticos por nombre, o similiares.
    /// </summary>
    /// <param name="busqueda">Palabra cuya similitud se busca en el nombre del lugar turistico que se esta buscando.</param>
    /// <returns>Retorna una lista con los lugares turisticos que tengan coincidencia.</returns>
    public List<Entidad> ConsultarLugarTuristico(Entidad objeto)
    {
      LugarTuristico lugarTuristico = (LugarTuristico)objeto;
      List<Entidad> list_lugaresturisticos = new List<Entidad>();
      try
      {
        base.Conectar();
        comando = new NpgsqlCommand("consultar_lugarturistico", base.SqlConexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, lugarTuristico.Nombre);
        respuesta = comando.ExecuteReader();

        //Se recorre los registros devueltos.
        while (respuesta.Read())
        {
          LugarTuristico lugarTuristicos = new LugarTuristico();
          lugarTuristicos.Id = respuesta.GetInt32(0);
          lugarTuristicos.Nombre = respuesta.GetString(1);

          list_lugaresturisticos.Add(lugarTuristicos);
        }

        base.Desconectar();
        return list_lugaresturisticos;
      }
      catch (NpgsqlException e)
      {
        base.Desconectar();
        throw e;
      }
      catch (Exception e)
      {
        base.Desconectar();
        throw e;
      }
    }
  }
}
