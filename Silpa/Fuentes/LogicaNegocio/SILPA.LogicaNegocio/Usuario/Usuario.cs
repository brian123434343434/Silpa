using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.Usuario;
using SILPA.AccesoDatos.Generico;
using SILPA.Comun;
using System.Data.Common;
using System.Data;

namespace SILPA.LogicaNegocio.Usuario
{
    public class Usuario
    {
        public CredTipoUsuario ComUsuarioTipoIdentity;
        public CredencialEntity ComUsuarioIdentity;

        /// <summary>
        /// 
        /// Constructor
        /// </summary>
        public Usuario()
        {
        }

        public void ActivarUsuario(string strXmlDatos)
        {
            ComUsuarioTipoIdentity = new CredTipoUsuario();
            ComUsuarioTipoIdentity = (CredTipoUsuario)ComUsuarioTipoIdentity.Deserializar(strXmlDatos);
            //Se envia correo al usuario de aprobacion
            TipoUsuarioIdentity TipoUsuario = new TipoUsuarioIdentity();
            TipoUsuario.IdTipoUsuario = int.Parse(ComUsuarioTipoIdentity.IdTipoUsuario);
            TipoUsuario.IdPersona = int.Parse(ComUsuarioTipoIdentity.IdPersona);
            TipoUsuarioDalc objTipoUsuario = new TipoUsuarioDalc();
            
            objTipoUsuario.ActivarUsuario(TipoUsuario);
        }

        public void EnviarComunicacionAprobacionUsuario(string strXmlDatos)
        {
            ComUsuarioIdentity = new CredencialEntity();
            ComUsuarioIdentity = (CredencialEntity)ComUsuarioIdentity.Deserializar(strXmlDatos);
            //Se envia correo al usuario de aprobacion
            PersonaIdentity persona = new PersonaIdentity();
            persona.PersonaId = long.Parse(ComUsuarioIdentity.personaId);
            PersonaDalc objPersona = new PersonaDalc();
            objPersona.ObtenerPersona(ref persona);
            //Si la comunicacion es de aprobacion - MIRM
            if (ComUsuarioIdentity.motivoRechazo != null)
            {
                if (ComUsuarioIdentity.motivoRechazo == string.Empty)
                {
                    Configuracion _configuration = new Configuracion();
                    //Se envia correo al administrador avisando que Lo active
                    ICorreo.Correo.EnviarCorreoAprobacionUsuario(persona, _configuration.CuentaControl);
                    //Envia la contraseña al usuario
                    ICorreo.Correo.EnviarCorreoAprobacionUsuario(ComUsuarioIdentity.motivoRechazo, persona);
                }
                else
                {
                    //Se envia correo al solicitante
                    ICorreo.Correo.EnviarCorreoAprobacionUsuario(ComUsuarioIdentity.motivoRechazo, persona);                    
                }
            }
            else
            {
                ComUsuarioIdentity.motivoRechazo = "";
                Configuracion _configuration = new Configuracion();
                //Se envia correo al administrador avisando que Lo active
                ICorreo.Correo.EnviarCorreoAprobacionUsuario(persona, _configuration.CuentaControl);
                //Envia la contraseña al usuario
                ICorreo.Correo.EnviarCorreoAprobacionUsuario(ComUsuarioIdentity.motivoRechazo, persona);            
            }            
             
           //se actualiza la informacion en la bd
            UsuarioDalc _objAprobar = new UsuarioDalc();
            _objAprobar.ActualizarAprobacionUsuario(ComUsuarioIdentity);
        }

        /// <summary>
        /// 12-jul-2010 - aegb
        /// Consulta la compañia (entidad) a la que pertenece el usuario en security
        /// </summary>
        /// <param name="usuario"></param>
        public DataTable ConsultarUsuarioCompania(string usuario)
        {
            UsuarioDalc _objUsuario = new UsuarioDalc();
            return _objUsuario.ConsultarUsuarioCompania(usuario).Tables[0];
        }


        public string ConsultarUsuarioVisitanteNo(string ip)
        {
            UsuarioDalc _objUsuario = new UsuarioDalc();
            return _objUsuario.ConsultarUsuarioVisitanteNo(ip);
        }

        /// <summary>
        /// 11-oct-2010 - aegb
        /// Consulta la compañia (entidad) a la que pertenece el usuario del sistema en security
        /// </summary>
        /// <param name="usuario"></param>
        public DataTable ConsultarUsuarioSistemaCompania(string usuario)
        {
            UsuarioDalc _objUsuario = new UsuarioDalc();
            return _objUsuario.ConsultarUsuarioSistemaCompania(usuario).Tables[0];
        }
    }
}
