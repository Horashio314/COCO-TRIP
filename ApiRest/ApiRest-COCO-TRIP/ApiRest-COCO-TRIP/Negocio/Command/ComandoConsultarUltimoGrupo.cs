using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.Singleton;
using ApiRest_COCO_TRIP.Datos.DAO;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  /// <summary>
  /// Metodo para obtener el identificador del ultimo grupo agregado de un usuario
  /// </summary>
  public class ComandoConsultarUltimoGrupo : Comando
  {
    private Usuario usuario;
    private Grupo grupo;

    private DAOGrupo datos;
    private Archivo archivo;

    public ComandoConsultarUltimoGrupo(int idUsuario)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      grupo = FabricaEntidad.CrearEntidadGrupo();

      usuario.Id = idUsuario;
    }

    public override void Ejecutar()
    {
      datos = FabricaDAO.CrearDAOGrupo();
      archivo = Archivo.ObtenerInstancia();
      grupo = (Grupo) datos.ConsultarUltimoGrupo (usuario);

      if (archivo.ExisteArchivo(Archivo.FotoGrupo + grupo.Id + Archivo.Extension))
      {
        grupo.RutaFoto = Archivo.Ruta + Archivo.FotoGrupo + grupo.Id + Archivo.Extension;
      }
    }

    public override Entidad Retornar()
    {
      return grupo;
    }

    public override List<Entidad> RetornarLista()
    {
      throw new System.NotImplementedException();
    }
  }

}
