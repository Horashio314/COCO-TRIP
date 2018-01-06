﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    public class ComandoBorrarUsuario : Comando
    {
        private Entidad entidad;
        private DAO dao;

        public ComandoBorrarUsuario(Entidad entidad)
        {
            this.entidad = entidad;
        }

        public override void Ejecutar()
        {
            dao = FabricaDAO.CrearDAOUsuario();
            entidad.Id = ((DAOUsuario)dao).ConsultarPorNombre(entidad).Id;
            string passAct = ((Usuario)entidad).Clave;
            entidad = ((DAOUsuario)dao).ObtenerPassword(entidad);
            if (!(passAct == ((Usuario)entidad).Clave) || entidad.Id == 0)
            {
                entidad = null;
                return;
            }
            dao.Eliminar(entidad);
        }

        public override Entidad Retornar()
        {
            return entidad;
        }

        public override List<Entidad> RetornarLista()
        {
            throw new NotImplementedException();
        }
    }
}