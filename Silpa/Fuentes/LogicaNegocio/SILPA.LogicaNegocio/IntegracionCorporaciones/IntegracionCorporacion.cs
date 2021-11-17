using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos.IntegracionCorporaciones.Dalc;
using SILPA.AccesoDatos.IntegracionCorporaciones.Entidades;
using SILPA.LogicaNegocio.Generico;
using SILPA.LogicaNegocio.IntegracionCorporaciones.Entidades;
using SILPA.LogicaNegocio.Excepciones;
using SILPA.LogicaNegocio.ServiciosREST;
using SILPA.LogicaNegocio.ServiciosREST.Entidades;
using SoftManagement.Log;

namespace SILPA.LogicaNegocio.IntegracionCorporaciones
{
    public class IntegracionCorporacion
    {

        #region Objetos

            /// <summary>
            /// Instancia del objeto
            /// </summary>
            private static IntegracionCorporacion Instancia {get; set;}

            /// <summary>
            /// Listado de credenciales
            /// </summary>
            private List<TokenAutoridadEntity> TokenAutoridades { get; set; }

        #endregion


        #region Creadoras

            /// <summary>
            /// Creadora
            /// </summary>
            private IntegracionCorporacion() 
            {
                //Crear listado
                this.TokenAutoridades = new List<TokenAutoridadEntity>();
            }

        #endregion


        #region Metodos Privados

            
            /// <summary>
            /// Verifica si el token se encuentra activo
            /// </summary>
            /// <param name="p_objIntegracion">IntegracionCorporacionEntity con la informacion de integracion de la entidad</param>
            /// <returns>bool indicando si el token se encuentra activo</returns>
            private bool TokenActivo(IntegracionCorporacionEntity p_objIntegracion)
            {
                ServicioREST objServicio = null;
                RespuestaServicioEntity objRespuestaServicio = null;
                bool blnTokenActivo = false;

                //Verificar si el token se encuentra registrado
                if (this.TokenAutoridades != null && this.TokenAutoridades.Count > 0 && this.TokenAutoridades.Where(token => token.AutoridadID == p_objIntegracion.AutoridadID).ToList().Count() > 0)
                {
                    //Verificar si el token se encuentra activo
                    objServicio = new ServicioREST();
                    objRespuestaServicio = objServicio.Ejecutar(MetodosConexionREST.POST, p_objIntegracion.ServicioverificacionToken, null, (this.TokenAutoridades.Where(token => token.AutoridadID == p_objIntegracion.AutoridadID).ToList())[0].Token);
                    
                    //Verificar si respuesta OK
                    if (objRespuestaServicio.CodigoRespuesta == CodigosRespuestaREST.OK)
                        blnTokenActivo = (bool)objRespuestaServicio.ObjetoRespuesta;
                    else
                        throw new Exception("Error en la verifiaccion del token codigo de respuesta " + objRespuestaServicio.CodigoRespuesta.ToString());
                }

                return blnTokenActivo;
            }


            /// <summary>
            /// Realiza autenticacion y registra el token
            /// </summary>
            /// <param name="p_objIntegracion">IntegracionCorporacionEntity con la informacion de la integracion</param>
            private void ObtenerToken(IntegracionCorporacionEntity p_objIntegracion)
            {
                ServicioREST objServicio = null;
                RespuestaServicioEntity objRespuestaServicio = null;
                TokenAutoridadEntity objTokenAutoridad = null;

                //Generar token
                objServicio = new ServicioREST();
                objRespuestaServicio = objServicio.Ejecutar(MetodosConexionREST.POST, p_objIntegracion.ServicioAutorizacion, null, p_objIntegracion.Credenciales);

                //Verificar si respuesta OK
                if (objRespuestaServicio.CodigoRespuesta == CodigosRespuestaREST.OK)
                {
                    //Cargar el token
                    objTokenAutoridad = new TokenAutoridadEntity
                    {
                        AutoridadID = p_objIntegracion.AutoridadID,
                        Token = (string)objRespuestaServicio.ObjetoRespuesta
                    };
                    
                    //Verificar si se encuentra registrado un token
                    if (this.TokenAutoridades != null && this.TokenAutoridades.Count > 0 && this.TokenAutoridades.Where(token => token.AutoridadID == p_objIntegracion.AutoridadID).ToList().Count() > 0)
                    {
                        //Quitar el registro
                        this.TokenAutoridades.Remove((this.TokenAutoridades.Where(token => token.AutoridadID == p_objIntegracion.AutoridadID).ToList())[0]);

                        //Adicionar el nuevo rigistro
                        this.TokenAutoridades.Add(objTokenAutoridad);
                    }
                    else if (this.TokenAutoridades != null)
                        this.TokenAutoridades.Add(objTokenAutoridad);
                    else
                    {
                        this.TokenAutoridades = new List<TokenAutoridadEntity>();
                        this.TokenAutoridades.Add(objTokenAutoridad);
                    }
                        
                }
                else
                    throw new Exception("Error en la autenticacion del servicio " + objRespuestaServicio.CodigoRespuesta.ToString());
            }


            /// <summary>
            /// Obtener el listado de opciones del menu
            /// </summary>
            /// <param name="p_intAutoridadID">int con el identifiacdor de la autoridad</param>
            /// <param name="p_strUrl">string con la URL para obtener menu</param>
            /// <param name="p_strRolesUsuarioAutenticado">string con los roles del usuario autenticado</param>
            /// <returns>List con las opciones del menu</returns>
            private List<OpcionMenuEntity> ObtenerOpcionesMenu(int p_intAutoridadID, string p_strUrl, string p_strRolesUsuarioAutenticado)
            {
                ServicioREST objServicio = null;
                RespuestaServicioEntity objRespuestaServicio = null;
                List<OpcionMenuEntity> objLstOpciones = null;

                //Generar token
                objServicio = new ServicioREST();
                objRespuestaServicio = objServicio.Ejecutar<List<OpcionMenuEntity>>(MetodosConexionREST.POST, p_strUrl, (this.TokenAutoridades.Where(token => token.AutoridadID == p_intAutoridadID).ToList())[0].Token, p_strRolesUsuarioAutenticado);

                //Verificar si respuesta OK
                if (objRespuestaServicio.CodigoRespuesta == CodigosRespuestaREST.OK)
                    objLstOpciones = (List<OpcionMenuEntity>)objRespuestaServicio.ObjetoRespuesta;
                else
                    throw new Exception("Error obteniendo el listado de opciones del menu " + objRespuestaServicio.CodigoRespuesta.ToString());

                return objLstOpciones;
            }


            /// <summary>
            /// Obtener la informacion de acceso al sitio web dada una opcion
            /// </summary>
            /// <param name="p_intAutoridadID">int con el identificador de la autoridad ambiental</param>
            /// <param name="p_strUrl">string con la URL</param>
            /// <param name="p_objFormaAcceso">FormaSolicitudAccesoWebEntity con la informcion para obtener el acceso</param>
            /// <returns>RespuestaSolicitudAccesoWeb con la respuesta a la solicitud</returns>
            private RespuestaSolicitudAccesoWebEntity ObtenerAccesoWeb(int p_intAutoridadID, string p_strUrl, FormaSolicitudAccesoWebEntity p_objFormaAcceso)
            {
                ServicioREST objServicio = null;
                RespuestaServicioEntity objRespuestaServicio = null;
                RespuestaSolicitudAccesoWebEntity objRespuestaSolicitudAccesoWeb = null;

                //Generar token
                objServicio = new ServicioREST();
                objRespuestaServicio = objServicio.Ejecutar<RespuestaSolicitudAccesoWebEntity>(MetodosConexionREST.POST, p_strUrl, (this.TokenAutoridades.Where(token => token.AutoridadID == p_intAutoridadID).ToList())[0].Token, p_objFormaAcceso);

                //Verificar si respuesta OK
                if (objRespuestaServicio.CodigoRespuesta == CodigosRespuestaREST.OK)
                    objRespuestaSolicitudAccesoWeb = (RespuestaSolicitudAccesoWebEntity)objRespuestaServicio.ObjetoRespuesta;
                else
                    throw new Exception("Error obteniendo acceso " + objRespuestaServicio.CodigoRespuesta.ToString());

                return objRespuestaSolicitudAccesoWeb;
            }


            /// <summary>
            /// Cerrar sesion en sitio externo
            /// </summary>
            /// <param name="p_intAutoridadID">int con el identificador de la autoridad ambiental</param>
            /// <param name="p_strUrl">string con la URL</param>
            /// <param name="p_objFormaFinalizar">FormaFinalizarSesionEntity con la informcion para finalizar la sesion</param>
            private void FinalizarSesionWeb(int p_intAutoridadID, string p_strUrl, FormaFinalizarSesionEntity p_objFormaFinalizar)
            {
                ServicioREST objServicio = null;
                RespuestaServicioEntity objRespuestaServicio = null;

                //Generar token
                objServicio = new ServicioREST();
                objRespuestaServicio = objServicio.Ejecutar(MetodosConexionREST.POST, p_strUrl, (this.TokenAutoridades.Where(token => token.AutoridadID == p_intAutoridadID).ToList())[0].Token, p_objFormaFinalizar);

                //Verificar si respuesta OK
                if (objRespuestaServicio.CodigoRespuesta != CodigosRespuestaREST.OK)
                    throw new Exception("Error cerrando sesion " + objRespuestaServicio.CodigoRespuesta.ToString());
            }


        #endregion


        #region Metodos Publicos

            /// <summary>
            /// Retorna una nueva instancia del objeto
            /// </summary>
            /// <returns>IntegracionCorporacion con la instancia del objeto</returns>
            public static IntegracionCorporacion GetInstance()
            {
                //Verifica si existe instancia, sino la crea
                if (Instancia == null)
                    Instancia = new IntegracionCorporacion();

                return Instancia;
            }

            /// <summary>
            /// Obtiene el listado de autoridades integradas
            /// </summary>
            /// <returns>List con la informacion de las autoridades integradas</returns>
            public List<IntegracionCorporacionEntity> ObtenerCorporacionesIntegradas()
            {
                IntegracionCorporacionDalc objIntegracionCorporacionDalc = null;
                List<IntegracionCorporacionEntity> objLstIntegracionesAutoridades = null;

                try
                {
                    //Obtener el listado
                    objIntegracionCorporacionDalc = new IntegracionCorporacionDalc();
                    objLstIntegracionesAutoridades = objIntegracionCorporacionDalc.ObtenerListaAutoridadesIntegradas();

                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "IntegracionCorporacion :: ObtenerCorporacionesIntegradas -> Error obteniendo el listado de corporaciones: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw new IntegracionCorporacionException("IntegracionCorporacion :: ObtenerCorporacionesIntegradas -> Error obteniendo el listado de corporaciones: " + exc.Message, exc.InnerException);

                }

                return objLstIntegracionesAutoridades;
            }


            /// <summary>
            /// Obtener el listado de menus de una autoridad
            /// </summary>
            /// <param name="p_intAutoridadID">int con el identifiacdor de las autoridades</param>
            /// <param name="p_strRolesUsuarioAutenticado">string con los roles del usuario autenticado</param>
            /// <returns>List con el listado de opciones de menu</returns>
            public List<OpcionMenuEntity> ObtenerMenusEntidad(int p_intAutoridadID, string p_strRolesUsuarioAutenticado)
            {
                IntegracionCorporacionDalc objIntegracionCorporacionDalc = null;
                IntegracionCorporacionEntity objIntegracion = null;
                List<OpcionMenuEntity> objLstOpciones = null;

                try
                {
                    //Obtener la configuracion de la autoridad
                    objIntegracionCorporacionDalc = new IntegracionCorporacionDalc();
                    objIntegracion = objIntegracionCorporacionDalc.ObtenerAutoridadIntegrada(p_intAutoridadID); 

                    //verificar que se obtenga datos
                    if(objIntegracion != null && objIntegracion.Activo)
                    {
                        //Si el token para la autoridad no se encuentra activo
                        if (!this.TokenActivo(objIntegracion))
                        {
                            objIntegracion.Credenciales.Clave = EnDecript.DesencriptarDesplazamiento(objIntegracion.Credenciales.Clave);
                            this.ObtenerToken(objIntegracion);
                        }

                        //Obtener el listado de menus
                        objLstOpciones = this.ObtenerOpcionesMenu(p_intAutoridadID, objIntegracion.ServicioMenu, p_strRolesUsuarioAutenticado);
                        
                    }
                    else
                    {
                        throw new Exception("No se tiene configuracion de la autoridad " + p_intAutoridadID.ToString());
                    }

                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "IntegracionCorporacion :: ObtenerMenusEntidad -> Error obteniendo el menu de la corporacion " + p_intAutoridadID.ToString() + " : " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw new IntegracionCorporacionException("IntegracionCorporacion :: ObtenerMenusEntidad -> Error obteniendo el menu de la corporacion: " + exc.Message, exc.InnerException);

                }

                return objLstOpciones;
            }


            /// <summary>
            /// Obtener el acceso a la pagina web
            /// </summary>
            /// <param name="p_intAutoridadID">int con el identificador de la autoridad ambiental</param>
            /// <param name="p_objSolicitudAcceso">FormaSolicitudAccesoWebEntity con la informacion para acceder a la solicitud</param>
            /// <returns>RespuestaSolicitudAccesoWeb con lainformacion para acceso a la solicitud</returns>
            public RespuestaSolicitudAccesoWebEntity ObtenerAccesoPaginaWeb(int p_intAutoridadID,  FormaSolicitudAccesoWebEntity p_objSolicitudAcceso)
            {
                IntegracionCorporacionDalc objIntegracionCorporacionDalc = null;
                IntegracionCorporacionEntity objIntegracion = null;
                RespuestaSolicitudAccesoWebEntity objRespuestaSolicitudAccesoWeb = null;

                try
                {
                    //Obtener la configuracion de la autoridad
                    objIntegracionCorporacionDalc = new IntegracionCorporacionDalc();
                    objIntegracion = objIntegracionCorporacionDalc.ObtenerAutoridadIntegrada(p_intAutoridadID); 

                    //verificar que se obtenga datos
                    if(objIntegracion != null && objIntegracion.Activo)
                    {

                        //Si el token para la autoridad no se encuentra activo
                        if (!this.TokenActivo(objIntegracion))
                        {
                            objIntegracion.Credenciales.Clave = EnDecript.DesencriptarDesplazamiento(objIntegracion.Credenciales.Clave);
                            this.ObtenerToken(objIntegracion);
                        }

                        //Crear sesion remota
                        objRespuestaSolicitudAccesoWeb = this.ObtenerAccesoWeb(p_intAutoridadID, objIntegracion.ServicioCrearSesion, p_objSolicitudAcceso);
                        
                    }
                    else
                    {
                        throw new Exception("No se tiene configuracion de la autoridad " + p_intAutoridadID.ToString());
                    }

                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "IntegracionCorporacion :: ObtenerAccesoPaginaWeb -> Error obteniendo acceso a la pagina web " + p_intAutoridadID.ToString() + " : " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw new IntegracionCorporacionException("IntegracionCorporacion :: ObtenerAccesoPaginaWeb -> Error obteniendo acceso a la pagina web: " + exc.Message, exc.InnerException);

                }

                return objRespuestaSolicitudAccesoWeb;
            }


            /// <summary>
            /// Finalizar sesion en el sitio remoto
            /// </summary>
            /// <param name="p_intAutoridadID">int con el identificador de la autoridad ambiental</param>
            /// <param name="p_objFormaFinalizar">FormaFinalizarSesionEntity con la informacion requerida para finalizar la sesion</param>
            public void FinalizarSesionWeb(int p_intAutoridadID, FormaFinalizarSesionEntity p_objFormaFinalizar)
            {
                IntegracionCorporacionDalc objIntegracionCorporacionDalc = null;
                IntegracionCorporacionEntity objIntegracion = null;

                try
                {
                    //Obtener la configuracion de la autoridad
                    objIntegracionCorporacionDalc = new IntegracionCorporacionDalc();
                    objIntegracion = objIntegracionCorporacionDalc.ObtenerAutoridadIntegrada(p_intAutoridadID);

                    //verificar que se obtenga datos
                    if (objIntegracion != null && objIntegracion.Activo)
                    {

                        //Si el token para la autoridad no se encuentra activo
                        if (!this.TokenActivo(objIntegracion))
                        {
                            objIntegracion.Credenciales.Clave = EnDecript.DesencriptarDesplazamiento(objIntegracion.Credenciales.Clave);
                            this.ObtenerToken(objIntegracion);
                        }

                        //Cerrar sesiones
                        this.FinalizarSesionWeb(p_intAutoridadID, objIntegracion.ServicioCerrarSesion, p_objFormaFinalizar);

                    }
                    else
                    {
                        throw new Exception("No se tiene configuracion de la autoridad " + p_intAutoridadID.ToString());
                    }

                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "IntegracionCorporacion :: FinalizarSesionWeb -> Error finalizando sesion " + p_intAutoridadID.ToString() + " : " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw new IntegracionCorporacionException("IntegracionCorporacion :: FinalizarSesionWeb -> Error finalizando sesion: " + exc.Message, exc.InnerException);

                }
            }

        #endregion

    }
}
