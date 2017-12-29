﻿using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using Newtonsoft.Json.Linq;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using ApiRest_COCO_TRIP.Datos.Singleton;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
	public class ComandoAgregarLugarTuristico : Comando
	{

		private Entidad _lugarTuristico;
		private Entidad _horario;
		private Entidad _foto;
		private Entidad _actividad;
		private IDAOLugarTuristico iDAO;
		
		JObject _datos;
		private Log log;
		/// <summary>
		/// Creo el comando con la lista de datos ya deseralizada
		/// </summary>
		/// <param name="datos">JSON de Lugar turistico</param>
		public ComandoAgregarLugarTuristico(JObject datos)
		{

			_lugarTuristico = FabricaEntidad.CrearEntidadLugarTuristico();
			_datos = datos;
			_lugarTuristico = _datos.ToObject<LugarTuristico>();
			iDAO = FabricaDAO.CrearLugarTuristico();
			log = Log.ObtenerInstancia();

		}

		/// <summary>
		/// Inserta un lugar turistico en la bsae de datos
		/// </summary>
		public override void Ejecutar()
		{
			try
			{
				iDAO.Insertar(_lugarTuristico);
			}
			catch ( Exception e)
			{
				log.ApiRestError( e.ToString(), "Error");

			}

		}

		public override Entidad Retornar()
		{
			throw new NotImplementedException();
		}

		public override List<Entidad> RetornarLista()
		{
			throw new NotImplementedException();
		}
	}
}