﻿using System;
using System.Text.RegularExpressions;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Datos.Entity;

namespace ApiRest_COCO_TRIP.Comun.Validaciones
{
    /// <summary>
    /// Clase destinada a validaciones necesarias en el programa.
    /// </summary>
    public class ValidacionString
    {
        public ValidacionString()
        {

        }

        /// <summary>
        /// Metodo que valida la longitud del nombre de la categoria, entre 5-20,
        /// </summary>
        /// <param name="input">Nombre de la categoria que se requiere validar.</param>
        /// <returns>Devuelve "true", si la longitud esta entre 5 y 20, "false" en caso contrario.</returns>
        public static bool ValidarLongitudInputNombreCategoria(String input)
        {
            if ((input.Length >= 5) && (input.Length <= 20))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Metodo que valida la longitud de la descripcion de la categoria, entre 5-100,
        /// </summary>
        /// /// <param name="input">Descripcion de la categoria que se requiere validar.</param>
        /// <returns>Devuelve "true", si la longitud esta entre 5 y 100, "false" en caso contrario.</returns>
        public static bool ValidarLongitudInputDescripcionCategoria(String input)
        {
            if ((input.Length >= 5) && (input.Length <= 100))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Metodo que valida que el parametro de entrada este compuesta unicamente de
        /// mayusculas, minusculas, puntos, comas y espacios en blanco,
        /// </summary>
        /// <param name="input">Cadena de caracteres que se requiere verificar.</param>
        /// <returns>Retorna "true", si cumple con no tener caracteres especiales, y "false" en caso contrario.</returns>
        public static bool ValidarCaracteresEspeciales(String input)
        {
            if (Regex.Match(input, @"^[a-zA-Z \,\.]+$").Success)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Metodo que valida si la instancia del tipo categoria cumple con las condiciones
        /// para realizar la insercion en la base de datos.
        /// </summary>
        /// <param name="categoria">Instancia del tipo categoria que se requiere verificar.</param>
        /// <exception cref="ParametrosInvalidosExcepcion">Los parametros de la instancia no cumple con las condiciones para la informacion de una categoria</exception>
        public static void ValidarCategoria(Entidad categoria)
        {
            if (!ValidarLongitudInputDescripcionCategoria(((Categoria)categoria).Descripcion) ||  //Validacion de campos de Nombre y Descripcion
            !ValidarCaracteresEspeciales(((Categoria)categoria).Descripcion) ||      //Deben tener al menos 5 caracteres, y solo se permiten 
                                                                                    //letras, puntos, comas y espacios
            !ValidarLongitudInputNombreCategoria(((Categoria)categoria).Nombre) ||
            !ValidarCaracteresEspeciales(((Categoria)categoria).Nombre))
            {
                if (!ValidarLongitudInputDescripcionCategoria(((Categoria)categoria).Descripcion) ||
                    !ValidarCaracteresEspeciales(((Categoria)categoria).Descripcion))
                     throw new ParametrosInvalidosExcepcion("Descripcion", 
                        "La descripcion ingresada debe tener al menos 5 caracteres y maximo 100. Solo se permiten letras y espacios");
                else 
                    if (!ValidarLongitudInputNombreCategoria(((Categoria)categoria).Nombre) ||
                        !ValidarCaracteresEspeciales(((Categoria)categoria).Nombre))
                        throw new ParametrosInvalidosExcepcion("Nombre",
                          "El nombre ingresado debe tener al menos 5 caracteres y maximo 20. Solo se permiten letras y espacios");
            }
        }
    }
}