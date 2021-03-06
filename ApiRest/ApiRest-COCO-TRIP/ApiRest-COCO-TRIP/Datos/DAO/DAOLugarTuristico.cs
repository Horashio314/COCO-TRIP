﻿using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using System.Data;
using NpgsqlTypes;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using Npgsql;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Reflection;
using NLog;
using System.Net.Sockets;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
	public class DAOLugarTuristico : DAO, IDAOLugarTuristico
	{
		private LugarTuristico _lugarTuristico;
		private NpgsqlDataReader _datos;
		private static Logger log = LogManager.GetCurrentClassLogger();


		public override List<Entidad> ConsultarLista(Entidad objeto)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Metodo que se trae toda la lista de lugares turisticos
		/// </summary>
		/// <returns>Lista de lugares turisticos completa</returns>
		public List<Entidad> ConsultarTodaLaLista()
		{

			List<Entidad> lugaresTuristicos = new List<Entidad>(); // Estos new me hacen ruido aqui.
			 
			try
			{
				StoredProcedure("ConsultarLugaresTuristico");
				_datos = Comando.ExecuteReader();

				while (_datos.Read())
				{
					LugarTuristico lugarTuristico = FabricaEntidad.CrearEntidadLugarTuristico();
					lugarTuristico.Id = _datos.GetInt32(0);
					lugarTuristico.Nombre = _datos.GetString(1);
					lugarTuristico.Costo = _datos.GetDouble(2);
					lugarTuristico.Descripcion = _datos.GetString(3);
					lugarTuristico.Direccion = _datos.GetString(4);
					lugarTuristico.Correo = _datos.GetString(5);
					lugarTuristico.Telefono = _datos.GetInt64(6);
					lugarTuristico.Latitud = _datos.GetDouble(7);
					lugarTuristico.Longitud = _datos.GetDouble(8);
					lugarTuristico.Activar = _datos.GetBoolean(9);
					lugaresTuristicos.Add(lugarTuristico);
				}

				return lugaresTuristicos;

			}
			catch (NullReferenceException e)
			{

				log.Error(e.Message);
				throw new ReferenciaNulaExcepcion(e, "Parametros de entrada nulos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch (InvalidCastException e)
			{

				log.Error("Casteo invalido en:"
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
				throw new CasteoInvalidoExcepcion(e, "Ocurrio un casteo invalido en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch (NpgsqlException e)
			{

				log.Error("Ocurrio un error en la base de datos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
				throw new BaseDeDatosExcepcion(e, "Ocurrio un error en la base de datos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch (SocketException e)
			{

				log.Error("Ocurrio un error en la base de datos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
				throw new SocketExcepcion(e, "Ocurrio un error en la base de datos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch (Exception e)
			{

				log.Error("Ocurrio un error desconocido: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
				throw new Excepcion(e, "Ocurrio un error desconocido en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			finally
			{
				Desconectar();
			}


		}

		public override Entidad ConsultarPorId(Entidad objeto)
		{
			LugarTuristico lugarTuristico = FabricaEntidad.CrearEntidadLugarTuristico();

			try
			{
				StoredProcedure("ConsultarLugarTuristico");
				Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, objeto.Id);
				_datos = Comando.ExecuteReader();
				
				while (_datos.Read())
				{
					lugarTuristico.Id = objeto.Id;
					lugarTuristico.Nombre = _datos.GetString(0);
					lugarTuristico.Costo = _datos.GetDouble(1);
					lugarTuristico.Descripcion = _datos.GetString(2);
					lugarTuristico.Direccion = _datos.GetString(3);
					lugarTuristico.Correo = _datos.GetString(4);
					lugarTuristico.Telefono = _datos.GetInt64(5);
					lugarTuristico.Latitud = _datos.GetDouble(6);
					lugarTuristico.Longitud = _datos.GetDouble(7);
					lugarTuristico.Activar = _datos.GetBoolean(8);

				}

				log.Info("Lugar turistico:" + lugarTuristico);

				return lugarTuristico;

			}
			catch (NullReferenceException e)
			{

				log.Error(e.Message);
				throw new ReferenciaNulaExcepcion(e, "Parametros de entrada nulos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch (InvalidCastException e)
			{

				log.Error("Casteo invalido en:"
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
				throw new CasteoInvalidoExcepcion(e, "Ocurrio un casteo invalido en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch (NpgsqlException e)
			{

				log.Error("Ocurrio un error en la base de datos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
				throw new BaseDeDatosExcepcion(e, "Ocurrio un error en la base de datos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch (SocketException e)
			{

				log.Error("Ocurrio un error en la base de datos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
				throw new SocketExcepcion(e, "Ocurrio un error en la base de datos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch (Exception e)
			{

				log.Error("Ocurrio un error desconocido: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
				throw new Excepcion(e, "Ocurrio un error desconocido en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			finally
			{
				Desconectar();
			}
		}

		public override void Eliminar(Entidad objeto)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Insertar un lugar turistico en la base de datos
		/// </summary>
		/// <param name="objeto">Objeto lugar turistico</param>
		public override void Insertar(Entidad objeto)
		{
			int success = 0 ;
			_lugarTuristico = (LugarTuristico)objeto;
			
			try
			{

				StoredProcedure("insertarlugarturistico");
				// Seteando los parametros al SP
				Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, _lugarTuristico.Nombre);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Numeric, _lugarTuristico.Costo);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, _lugarTuristico.Descripcion);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, _lugarTuristico.Direccion);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, _lugarTuristico.Correo);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Bigint, _lugarTuristico.Telefono);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Numeric, _lugarTuristico.Latitud);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Numeric, _lugarTuristico.Longitud);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Boolean, _lugarTuristico.Activar);
				// Ejecucion
				success = Comando.ExecuteNonQuery();

			}
			catch(NullReferenceException e)
			{

				log.Error(e.Message);
				throw new ReferenciaNulaExcepcion(e, "Parametros de entrada nulos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch(InvalidCastException e)
			{

				log.Error("Casteo invalido en:"
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
				throw new CasteoInvalidoExcepcion(e, "Ocurrio un casteo invalido en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch(NpgsqlException e)
			{

				log.Error("Ocurrio un error en la base de datos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
				throw new BaseDeDatosExcepcion(e, "Ocurrio un error en la base de datos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch(SocketException e)
			{

				log.Error("Ocurrio un error en la base de datos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
				throw new SocketExcepcion(e, "Ocurrio un error en la base de datos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch(Exception e)
			{

				log.Error("Ocurrio un error desconocido: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
				throw new Excepcion(e, "Ocurrio un error desconocido en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			finally
			{
				Desconectar();
			}
		}


        /// <summary>
        /// Actualiza la informacion de un lugar turistico
        /// </summary>
        /// <param name="lugar">Entidad lugar turistico</param>
        /// <returns></returns>
        public override void Actualizar(Entidad lugar)
        {
            _lugarTuristico = (LugarTuristico)lugar;
            try
            {
                StoredProcedure("ActualizarLugarTuristico");
                Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, _lugarTuristico.Id);
                Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, _lugarTuristico.Nombre);
                Comando.Parameters.AddWithValue(NpgsqlDbType.Numeric, _lugarTuristico.Costo);
                Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, _lugarTuristico.Descripcion);
                Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, _lugarTuristico.Direccion);
                Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, _lugarTuristico.Correo);
                Comando.Parameters.AddWithValue(NpgsqlDbType.Bigint, _lugarTuristico.Telefono);
                Comando.Parameters.AddWithValue(NpgsqlDbType.Numeric, _lugarTuristico.Latitud);
                Comando.Parameters.AddWithValue(NpgsqlDbType.Numeric, _lugarTuristico.Longitud);
                Comando.Parameters.AddWithValue(NpgsqlDbType.Boolean, _lugarTuristico.Activar);
                // Ejecucion
                Comando.ExecuteNonQuery();
            }
            catch (NullReferenceException e)
            {

                log.Error(e.Message);
                throw new ReferenciaNulaExcepcion(e, "Parametros de entrada nulos en: "
                + GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

            }
            catch (InvalidCastException e)
            {

                log.Error("Casteo invalido en:"
                + GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
                throw new CasteoInvalidoExcepcion(e, "Ocurrio un casteo invalido en: "
                + GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

            }
            catch (NpgsqlException e)
            {

                log.Error("Ocurrio un error en la base de datos en: "
                + GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
                throw new BaseDeDatosExcepcion(e, "Ocurrio un error en la base de datos en: "
                + GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

            }
            catch (SocketException e)
            {

                log.Error("Ocurrio un error en la base de datos en: "
                + GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
                throw new SocketExcepcion(e, "Ocurrio un error en la base de datos en: "
                + GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

            }
            catch (Exception e)
            {

                log.Error("Ocurrio un error desconocido: "
                + GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
                throw new Excepcion(e, "Ocurrio un error desconocido en: "
                + GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

            }
            finally
            {
                Desconectar();
            }
        }

		/// <summary>
		/// Metodo para actualizar el estado de un lugar turistico
		/// </summary>
		/// <param name="objeto">Objeto lugar turistico</param>
		public void ActualizarEstado(Entidad objeto)
		{
			try
			{
				StoredProcedure("activarlugarturistico");
				Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, objeto.Id);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Boolean, ((LugarTuristico)objeto).Activar);
				Comando.ExecuteNonQuery();
			}
			catch (NullReferenceException e)
			{

				log.Error(e.Message);
				throw new ReferenciaNulaExcepcion(e, "Parametros de entrada nulos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch (InvalidCastException e)
			{

				log.Error("Casteo invalido en:"
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
				throw new CasteoInvalidoExcepcion(e, "Ocurrio un casteo invalido en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch (NpgsqlException e)
			{

				log.Error("Ocurrio un error en la base de datos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
				throw new BaseDeDatosExcepcion(e, "Ocurrio un error en la base de datos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch (SocketException e)
			{

				log.Error("Ocurrio un error en la base de datos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
				throw new SocketExcepcion(e, "Ocurrio un error en la base de datos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch (Exception e)
			{

				log.Error("Ocurrio un error desconocido: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
				throw new Excepcion(e, "Ocurrio un error desconocido en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			finally
			{
				Desconectar();
			}
		}

		/// <summary>
		/// Para hacer la conexion y crear el stored procedure
		/// </summary>
		/// <param name="sp"></param>
		private void StoredProcedure(string sp)
		{
			Conectar();
			Comando = SqlConexion.CreateCommand();
			Comando.CommandType = CommandType.StoredProcedure;
			Comando.CommandText = sp;
		}


	}
}