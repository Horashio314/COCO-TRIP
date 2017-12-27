using ApiRest_COCO_TRIP.Datos.DAO;

namespace ApiRest_COCO_TRIP.Datos.Fabrica
{
  /// <summary>
  /// Fabrica que instancia los DAO
  /// </summary>
  public class FabricaDAO
  {
    /// <summary>
    /// Retorna la instancia de DAOUsuario
    /// </summary>
    /// <returns>Grupo</returns>
    public static DAOUsuario CrearDAOUsuario()
    {
      return new DAOUsuario();
    }

    /// <summary>
    /// Retorna la instancia de DAOGrupo
    /// </summary>
    /// <returns>Grupo</returns>
    public static DAOGrupo CrearDAOGrupo()
    {
      return new DAOGrupo();
    }

    /// <summary>
    /// Retorna la instancia de DAOAmigo
    /// </summary>
    /// <returns>Grupo</returns>
    public static DAOAmigo CrearDAOAmigo()
    {
      return new DAOAmigo();
    }

    /// <summary>
    /// Retorna la instancia de DAOCategoria
    /// </summary>
    /// <returns>Grupo</returns>
    public static DAOCategoria CrearDAOCategoria()
    {
      return new DAOCategoria();
      /// Retorna la instancia de DAOItinerario
      /// </summary>
      /// <returns>DAOItinerario</returns>
    }
    public static DAOItinerario CrearDAOItinerario()
    {
      return new DAOItinerario();
    }
    
    /// Retorna la instancia de DAOAgenda
    /// </summary>
    /// <returns>DAOAgenda</returns>
    public static DAOAgenda CrearDAOAgenda()
    {
      return new DAOAgenda();
    }

    /// Retorna una nueva instancia de DAOLocalidadEvento
    /// </summary>
    /// <returns>DAOLocalidadEvento</returns>
    public static DAOLocalidadEvento CrearDAOLocalidad()
    {
      return new DAOLocalidadEvento();
    }

    /// Retorna una nueva instancia de DAOEvento
    /// </summary>
    /// <returns>DAOEvento</returns>
    public static DAOEvento CrearDAOEvento()
    {
      return new DAOEvento();
    }

    /// Retorna la instancia de DAONotificacion
    /// </summary>
    /// <returns>DAONotificacion</returns>
    public static DAONotificacion CrearDAONotifiacacion()
    {
      return new DAONotificacion();
    }
  }
}