using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BackOffice_COCO_TRIP.Datos.Entidades
{
  /// <summary>
  /// Clase que representa la entidad localidad
  /// </summary>
  public class Localidad : Entidad
  {
    private string nombre; // Nombre de la localidad
    private string descripcion; /// Descripcion de la localidad
    private string coordenadas; /// Coordenadas de la localidad

    [JsonProperty(PropertyName = "nombre")]
    [Required(ErrorMessage = "Ingrese un nombre")]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "El nombre debe ser mayor a 1 caracter y menor a 30 ")]
    public string Nombre { get => nombre; set => nombre = value; }

    [JsonProperty(PropertyName = "descripcion")]
    [Required(ErrorMessage = "Ingrese una descripcion")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "La descripcion debe ser mayor a 4 caracteres" +
      " y menor a 200")]
    public string Descripcion { get => descripcion; set => descripcion = value; }

    [JsonProperty(PropertyName = "coordenadas")]
    [Required(ErrorMessage = "Ingrese unas coordenadas")]
    [RegularExpression(@"((\-?\d+(\.\d+)?),\s*(\-?\d+(\.\d+)?))", ErrorMessage = "Ingrese unas coordenadas validas")]
    public string Coordenadas { get => coordenadas; set => coordenadas = value; }

    /// <summary>
    /// Metodo constructor sin el identificador unico de la localidad
    /// </summary>
    /// <param name="_nombre">Nombre de la localidad</param>
    /// <param name="_descripcion">Descripcion de la localidad</param>
    /// <param name="_coordenadas">Coordenadas de la localidad</param>
    public Localidad(string _nombre, string _descripcion, string _coordenadas)
    {
      nombre = _nombre;
      descripcion = _descripcion;
      coordenadas = _coordenadas;

    }

    /// <summary>
    /// Metodo constructor con el identificador unico de la localidad
    /// </summary>
    /// <param name="id">Identificador unico de la localidad</param>
    /// <param name="_nombre">Nombre de la localidad</param>
    /// <param name="_descripcion">Descripcion de la localidad</param>
    /// <param name="_coordenadas">Coordenadas de la localidad</param>
    public Localidad(int id, string _nombre, string _descripcion, string _coordenadas)
    {
      this.Id = id;
      nombre = _nombre;
      descripcion = _descripcion;
      coordenadas = _coordenadas;

    }

    /// <summary>
    /// Metodo constructor solo con el identificador unico de la localidad
    /// </summary>
    /// <param name="id">Identificador unico de la localidad</param>
    public Localidad(int id)
    {
      this.Id = id;
    }

    /// <summary>
    /// Metodo constructor vacio
    /// </summary>
    public Localidad()
    {

    }

  }
}
