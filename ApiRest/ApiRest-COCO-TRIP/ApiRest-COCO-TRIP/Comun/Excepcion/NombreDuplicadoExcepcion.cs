using System;

namespace ApiRest_COCO_TRIP.Comun.Excepcion
{
    /// <summary>
    /// Clase que encapsula la informaci�n de "Exception"
    /// se genera cuando una duplicidad en los nombre sobre la operaci�n que se realiza     .
    /// </summary>
    public class NombreDuplicadoExcepcion : Exception
    {
        private Exception excepcion;
        private DateTime fechaHora; //Hora y fecha de cuando se genero la excepci�n.
        private string datosAsociados;
        private string mensaje;       //Breve descripci�n de la excepci�n genereda con parametro del metodo con la que se ocasiono.

        /// <summary>
        /// Getters y Setters del atributo "excepcion".
        /// </summary>
        public Exception Excepcion { get => excepcion; set => excepcion = value; }

        /// <summary>
        /// Getters y Setters del atributo "fechaHora". 
        /// </summary>
        public DateTime FechaHora { get => fechaHora; set => fechaHora = value; }

        /// <summary>
        /// Getters y Setters del atributo "mensaje".
        /// </summary>
        public string Mensaje { get => mensaje; set => mensaje = value; }
        public string DatosAsociados { get => datosAsociados; set => datosAsociados = value; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="excepcion">Excepci�n generada del tipo "Exception"</param>
        public NombreDuplicadoExcepcion(Exception excepcion)
        {
            this.excepcion = excepcion;
            fechaHora = DateTime.Now;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="excepcion">Excepci�n generada del tipo "Exception"</param>
        /// <param name="mensaje">Breve mensaje referenciando como se genero la excepcion, incluir parametros del metodo</param>
        public NombreDuplicadoExcepcion(Exception excepcion, string mensaje)
        {
            this.excepcion = excepcion;
            this.mensaje = mensaje;
            fechaHora = DateTime.Now;
        }

        public NombreDuplicadoExcepcion(string parametro)
        {
            mensaje = $" {parametro} ";
        } 
    }
}
