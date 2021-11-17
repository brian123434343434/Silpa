using SILPA.AccesoDatos.ServiciosREST.Dalc;
using SILPA.AccesoDatos.ServiciosREST.Entidades;
using SILPA.Comun;
using SILPA.LogicaNegocio.ServiciosREST.Entidades;
using SoftManagement.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.ServiciosREST
{
    public class ServicioIntegracionRest
    {
        #region Objetos

            /// <summary>
            /// Instancia del objeto
            /// </summary>
            private static ServicioIntegracionRest Instancia {get; set;}

            /// <summary>
            /// Listado de token
            /// </summary>
            private List<TokenIntegracionAutoridadEntity> TokenAutoridades { get; set; }

        #endregion


        #region Propiedades

            /// <summary>
            /// Informacion del servicio solicitado
            /// </summary>
            public IntegracionCorporacionRESTEntity Servicio { get; set; }


            /// <summary>
            /// Token del servicio
            /// </summary>
            public string TokenServicio { get; set; }
            

        #endregion


        #region Creadoras

            /// <summary>
            /// Creadora
            /// </summary>
            private ServicioIntegracionRest() 
            {
                //Crear listado
                this.TokenAutoridades = new List<TokenIntegracionAutoridadEntity>();
            }

        #endregion


        #region Metodos Privados

            
            /// <summary>
            /// Verifica si el token se encuentra activo
            /// </summary>
            /// <param name="p_objIntegracion">ServicioIntegracionRestEntity con la informacion de integracion de la entidad</param>
            /// <returns>bool indicando si el token se encuentra activo</returns>
            protected bool TokenActivo(IntegracionCorporacionRESTEntity p_objIntegracionCorporacionRESTEntity)
            {
                ServicioREST objServicio = null;
                RespuestaServicioEntity objRespuestaServicio = null;
                bool blnTokenActivo = false;

                //Verificar si el token se encuentra registrado
                if (this.TokenAutoridades != null && this.TokenAutoridades.Count > 0 && this.TokenAutoridades.Where(token => token.AutoridadID == p_objIntegracionCorporacionRESTEntity.AutoridadID && token.ServicioID == p_objIntegracionCorporacionRESTEntity.IntegracionCorporacionRESTID).ToList().Count() > 0)
                {
                    //Verificar si el token se encuentra activo
                    objServicio = new ServicioREST();
                    objRespuestaServicio = objServicio.Ejecutar(MetodosConexionREST.POST, p_objIntegracionCorporacionRESTEntity.URLServicioverificacionToken, null, (this.TokenAutoridades.Where(token => token.AutoridadID == p_objIntegracionCorporacionRESTEntity.AutoridadID && token.ServicioID == p_objIntegracionCorporacionRESTEntity.IntegracionCorporacionRESTID).ToList())[0].Token);
                    
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
            /// <param name="p_objIntegracion">ServicioIntegracionRestEntity con la informacion de la integracion</param>
            protected void ObtenerToken(IntegracionCorporacionRESTEntity p_objIntegracionCorporacionRESTEntity)
            {
                ServicioREST objServicio = null;
                RespuestaServicioEntity objRespuestaServicio = null;
                TokenIntegracionAutoridadEntity objTokenAutoridad = null;

                //Generar token
                objServicio = new ServicioREST();
                objRespuestaServicio = objServicio.Ejecutar(MetodosConexionREST.POST, p_objIntegracionCorporacionRESTEntity.URLServicioAutorizacion, null, p_objIntegracionCorporacionRESTEntity.Credenciales);

                //Verificar si respuesta OK
                if (objRespuestaServicio.CodigoRespuesta == CodigosRespuestaREST.OK)
                {
                    //Cargar el token
                    objTokenAutoridad = new TokenIntegracionAutoridadEntity
                    {
                        AutoridadID = p_objIntegracionCorporacionRESTEntity.AutoridadID,
                        ServicioID = p_objIntegracionCorporacionRESTEntity.IntegracionCorporacionRESTID,
                        Token = (string)objRespuestaServicio.ObjetoRespuesta
                    };
                    
                    //Verificar si se encuentra registrado un token
                    if (this.TokenAutoridades != null && this.TokenAutoridades.Count > 0 && this.TokenAutoridades.Where(token => token.AutoridadID == p_objIntegracionCorporacionRESTEntity.AutoridadID && token.ServicioID == p_objIntegracionCorporacionRESTEntity.IntegracionCorporacionRESTID).ToList().Count() > 0)
                    {
                        //Quitar el registro
                        this.TokenAutoridades.Remove((this.TokenAutoridades.Where(token => token.AutoridadID == p_objIntegracionCorporacionRESTEntity.AutoridadID && token.ServicioID == p_objIntegracionCorporacionRESTEntity.IntegracionCorporacionRESTID).ToList())[0]);

                        //Adicionar el nuevo rigistro
                        this.TokenAutoridades.Add(objTokenAutoridad);
                    }
                    else if (this.TokenAutoridades != null)
                        this.TokenAutoridades.Add(objTokenAutoridad);
                    else
                    {
                        this.TokenAutoridades = new List<TokenIntegracionAutoridadEntity>();
                        this.TokenAutoridades.Add(objTokenAutoridad);
                    }
                        
                }
                else
                    throw new Exception("Error en la autenticacion del servicio " + objRespuestaServicio.CodigoRespuesta.ToString());
            }


            /// <summary>
            /// Obtiene la informacion del servicio especificado
            /// </summary>
            /// <param name="p_intServicioID">int con el identificador del servicio</param>
            private static IntegracionCorporacionRESTEntity ObtenerInformacionServicio(int p_intServicioID)
            {
                IntegracionCorporacionRESTDalc objIntegracion;
                IntegracionCorporacionRESTEntity objIntegracionCorporacionRESTEntity;

                //Realizar la consulta de integracion
                objIntegracion = new IntegracionCorporacionRESTDalc();
                objIntegracionCorporacionRESTEntity = objIntegracion.ObtenerServicio(p_intServicioID);

                //Validar que se obtenga la informacion
                if (objIntegracionCorporacionRESTEntity == null)
                    throw new Exception("No se obtuvo informacion del servicio " + p_intServicioID.ToString());

                return objIntegracionCorporacionRESTEntity;
            }


            /// <summary>
            /// Generar una copia del objeto actual
            /// </summary>
            /// <returns>ServicioIntegracionRest servicios integrales</returns>
            private ServicioIntegracionRest Copy(IntegracionCorporacionRESTEntity p_objIntegracionCorporacionRESTEntity)
            {
                return new ServicioIntegracionRest
                {
                    Servicio = p_objIntegracionCorporacionRESTEntity,
                    TokenServicio = (this.TokenAutoridades.Where(token => token.AutoridadID == p_objIntegracionCorporacionRESTEntity.AutoridadID && token.ServicioID == p_objIntegracionCorporacionRESTEntity.IntegracionCorporacionRESTID).ToList())[0].Token
                };

            }

        #endregion


        #region Metodos Publicos

            /// <summary>
            /// Retorna una nueva instancia del objeto
            /// </summary>
            /// <returns>ServicioIntegracionRest con la instancia del objeto</returns>
            public static ServicioIntegracionRest GetInstance(int p_intServicioID)
            {
                IntegracionCorporacionRESTEntity objServicio;
                ServicioIntegracionRest objInstanciaNueva;

                try
                {
                    //Verifica si existe instancia, sino la crea
                    if (Instancia == null)
                        Instancia = new ServicioIntegracionRest();

                    //Obtenga informacion del servicio
                    objServicio = ObtenerInformacionServicio(p_intServicioID);

                    //Verificar si el token
                    if (!Instancia.TokenActivo(objServicio))
                        Instancia.ObtenerToken(objServicio);

                    //Duplicar objeto
                    objInstanciaNueva = Instancia.Copy(objServicio);

                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "ServicioIntegracionRest :: GetInstance -> Error obteniendo instancia del objeto pare el servicio " + p_intServicioID.ToString() + " : " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objInstanciaNueva;
            }

        #endregion

    }
}
