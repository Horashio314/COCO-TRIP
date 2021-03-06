using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiRest_COCO_TRIP.Models;
using Npgsql;
using System.Data;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Web.Http.Cors;
using ApiRest_COCO_TRIP.Models.Dato;
using ApiRest_COCO_TRIP.Models.Excepcion;
using ApiRest_COCO_TRIP.Models.BaseDeDatos;

namespace ApiRest_COCO_TRIP.Controllers
{
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class M1_LoginController : ApiController
  {
    private Usuario usuario;
    private PeticionLogin peticion;
    private string url= "http://192.168.0.105:8091";

    /// <summary>
    /// Consulta un usuario con los datos recibidos (correo y clave)
    /// </summary>
    /// <param name="datos">datos del usuario. Formato JSON</param>
    /// <returns>El id del usuario(0 si no existe). Formato JSON</returns>
    [HttpPost]
    public int IniciarSesionCorreo(String datos)
    {
      usuario = JsonConvert.DeserializeObject<Usuario>(datos);
      peticion = new PeticionLogin();
      try
      {
        usuario.Id = peticion.ConsultarUsuarioCorreo(usuario);
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      return usuario.Id;
    }

    /// <summary>
    /// Consulta un usuario con los datos recibidos (usuario y clave)
    /// </summary>
    /// <param name="datos">datos del usuario. Formato JSON</param>
    /// <returns>El id del usuario(0 si no existe). Formato JSON</returns>
    [HttpPost]
    public int IniciarSesionUsuario(String datos)
    {
      usuario = JsonConvert.DeserializeObject<Usuario>(datos);
      peticion = new PeticionLogin();
      try
      {
        usuario.Id = peticion.ConsultarUsuarioNombre(usuario);
      }
      catch (NpgsqlException e)
      {
        //throw new HttpResponseException(HttpStatusCode.InternalServerError);
        throw e;
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      return usuario.Id;
    }

    /// <summary>
    /// Verifica que un usuario al iniciar sesion con una red social
    /// este registrado, si no lo esta, se registran sus datos de la red social
    /// </summary>
    /// <param name="datos">datos del usuario. Formato JSON</param>
    /// <returns>El id del usuario. Formato JSON</returns>
    [HttpPost]
    public int IniciarSesionSocial(String datos)
    {
      usuario = JsonConvert.DeserializeObject<Usuario>(datos);
      peticion = new PeticionLogin();
      try
      {
        usuario.Id = peticion.ConsultarUsuarioSocial(usuario);
        if (usuario.Id == 0)
          usuario.Id = peticion.InsertarUsuarioFacebook(usuario);

      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }

      return usuario.Id;

    }

    /// <summary>
    /// Verifica que un usuario este registrado, si no lo esta, se registran sus datos
    /// </summary>
    /// <param name="datos">datos del usuario. Formato JSON</param>
    /// <returns>El id del usuario(-1 o -2 si hubo un error). Formato JSON</returns>
    [HttpPost]
     public int RegistrarUsuario(String datos)
    {
      usuario = JsonConvert.DeserializeObject<Usuario>(datos);
      peticion = new PeticionLogin();
      string clave;
      //usuario.Foto = "";
      try
      {
        clave = usuario.Clave;
        usuario.Id = peticion.ConsultarUsuarioSocial(usuario);
        if (usuario.Id == 0)
        {
          usuario.Id = peticion.ConsultarUsuarioSoloNombre(usuario);
          if (usuario.Id == 0)
          {
            usuario.Id = peticion.InsertarUsuario(usuario);
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            string uri = url+"/api/M1_Login/ValidarUsuario/?email=" + usuario.Correo + "&" + "id=" + usuario.Id;
            mail.From = new MailAddress("cocotrip17@gmail.com");
            mail.To.Add(usuario.Correo);
            mail.Subject = "Registro Cocotrip";
            mail.Body = "Querido Usuario, hemos recibido una solicitud para registrarse en cocotrip, ingrese al siguiente link para completar su proceso de registro: "+uri; 

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("cocotrip17", "arepascocotrip");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
          }
          else
          {
            usuario.Id = -3;
          }
        }
        else
        {
          if(usuario.Valido)
          {
            usuario.Id = -2;
          }
          else
          {
            if(usuario.Clave==null)
            {
              int idusr = usuario.Id;
              usuario.Id = peticion.ConsultarUsuarioSoloNombre(usuario);
              usuario.Clave = clave;
              if (usuario.Id == 0)
              {
                usuario.Id = idusr;
                peticion.ActualizarUsuario(usuario);// aqui hay que colocar actualizar
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                string uri = url + "/api/M1_Login/ValidarUsuario/?email=" + usuario.Correo + "&" + "id=" + usuario.Id;
                mail.From = new MailAddress("cocotrip17@gmail.com");
                mail.To.Add(usuario.Correo);
                mail.Subject = "Registro Cocotrip";
                mail.Body = "Querido Usuario, hemos recibido una solicitud para registrarse en cocotrip, ingrese al siguiente link para completar su proceso de registro: " + uri;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("cocotrip17", "arepascocotrip");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
              }
              else
              {
                usuario.Id = -3;
              }
            }
            else
            {
             usuario.Id = -4;//tienes que validar la cuenta mediante el correo 
            }
          }
          
        }
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }

      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }

      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      return usuario.Id;
    }

    /// <summary>
    /// Envia un correo al usuario con su contrasena
    /// </summary>
    /// <param name="datos">datos del usuario. Formato JSON</param>
    /// <returns>Una respuesta Http confirmando que se envio el correo. Formato JSON</returns>
    [HttpPost]
    public HttpStatusCode CorreoRecuperar(String datos)
    {
      usuario = JsonConvert.DeserializeObject<Usuario>(datos);
      peticion = new PeticionLogin();
      try
      {
        usuario.Clave = peticion.RecuperarContrasena(usuario);
        if (usuario.Clave.Equals(""))
          throw new HttpResponseException(HttpStatusCode.NoContent);
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

        mail.From = new MailAddress("cocotrip17@gmail.com");
        mail.To.Add(usuario.Correo);
        mail.Subject = "Recuperar contrasena";
        mail.Body = "Querido Usuario, hemos recibido una solicitud para recuperar la contrasena de tu cuenta en cocotrip, esta es: " + usuario.Clave;

        SmtpServer.Port = 587;
        SmtpServer.Credentials = new System.Net.NetworkCredential("cocotrip17", "arepascocotrip");
        SmtpServer.EnableSsl = true;

        SmtpServer.Send(mail);

      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }

      catch (ArgumentNullException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }

      catch (HttpResponseException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      return HttpStatusCode.OK;

    }

    /// <summary>
    /// Valida un usuario segun su id y correo
    /// </summary>
    /// <param name="email">Correo del usuario</param>
    /// /// <param name="datos">id del usuario</param>
    /// <returns>Un mensaje informando al usuario que ha sido validado</returns>
    [HttpGet]
    public String ValidarUsuario(String email, int id)
    {
      usuario = new Usuario();
      usuario.Correo = email;
      usuario.Id = id;
      peticion = new PeticionLogin();
      try
      {
        peticion.ValidarUsuario(usuario);
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }

      return "Usuario validado";

    }
    //codigo de Pedro Garcia
    [HttpGet]
    public List<EventoPreferencia> EventoSegunPreferencias(int idUsuario) {
         peticion = new PeticionLogin();
         List<EventoPreferencia> listaEvento = new List<EventoPreferencia>();
         DateTime fechaActual = DateTime.Now;
      try
         {
           listaEvento = peticion.ConsultarEventosSegunPreferencias(idUsuario,fechaActual);
           return listaEvento;
         }
         catch (NpgsqlException e)
         {
             throw e;
         }
         catch (FormatException e)
         {
             throw e;
         }



     }
    [HttpGet]
    public List<LugarTuristicoPreferencia> LugarTuristicoSegunPreferencias(int idUsuario) {
      List<LugarTuristicoPreferencia> ltp = new List<LugarTuristicoPreferencia>();
      
      try
      {
        peticion = new PeticionLogin();
        ltp = peticion.ConsultarLugarTuristicoSegunPreferencias(idUsuario);
        return ltp;
      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }



    }
  }

  }

