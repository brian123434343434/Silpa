using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Servicios.IntegracionCorporacion.Entidades;
using SILPA.AccesoDatos.Generico;
using Silpa.Workflow.Entidades;
using Silpa.Workflow.AccesoDatos;

namespace SILPA.Servicios.IntegracionCorporacion
{
    public class IntegracionCorporacionFacade
    {
        #region Metodos Publicos

            
            /// <summary>
            /// Consultar la informacion de usuario de inicio de sesion por web
            /// </summary>
            /// <param name="p_strUsuario">string con el identificador del usuario</param>
            /// <returns>string con la informacion del usuario</returns>
            public string ObtenerDatosUsuario(string p_strUsuarioID)
            {
                PersonaDalc objPersonaDalc = null;
                PersonaIdentity objPersona = null;
                InformacionUsuarioEntity objInformacionUsuario = null;
                DatosUsuario objDatosUsuario = null;

                try
                {
                    //Verificar que se especifique la  información
                    if (!string.IsNullOrEmpty(p_strUsuarioID))
                    {
                        //Obtener el nombre de usuario
                        objPersonaDalc = new PersonaDalc();
                        objPersona = objPersonaDalc.BuscarPersonaByUserId(p_strUsuarioID);

                        //Verificar que e usuario exista
                        if (objPersona != null)
                        {

                            //Obtiene la informacion del usuario
                            objDatosUsuario = ApplicationUserDao.ConsultarDatosUsuario(objPersona.IdUsuario);

                            //Verificar que se obtenga la informacion
                            if (objDatosUsuario != null)
                            {
                                //Cargar los datos del usuario
                                objInformacionUsuario = new InformacionUsuarioEntity()
                                {
                                    UsuarioID = Convert.ToInt64(p_strUsuarioID),
                                    NombreUsuario = objDatosUsuario.NombreUsuario,
                                    FechaUltimoLogin = objDatosUsuario.UltimoLogin

                                };
                            }
                            else
                            {
                                throw new Exception("Usuario no existente");
                            }
                        }
                        else
                        {
                            throw new Exception("Usuario no existente");
                        }
                    }
                    else
                    {
                        throw new Exception("No se especifico usuario a verificar");
                    }
                }
                catch (Exception exc)
                {
                    throw new Exception("Se presento error durante el proceso de consulta de la sesion del usuario", exc);
                }

                return objInformacionUsuario.GetXml();
            }

        #endregion
    }
}
