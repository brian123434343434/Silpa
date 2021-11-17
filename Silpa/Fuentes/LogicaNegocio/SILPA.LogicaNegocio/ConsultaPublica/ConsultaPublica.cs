using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos.ConsultaPublica;
using SoftManagement.Log;
using System.Data;
using System.Data.SqlClient;

namespace SILPA.LogicaNegocio.ConsultaPublica
{
    public class ConsultaPublica
    {
        public ConsultaPublicaEntity _consultaPublicaEntity;
        public ConsultaPublicaDaLC _consultaPublicaDaLC;

        /// <summary>
        /// Objeto configuración
        /// </summary>
        private Configuracion _objConfiguracion;

        /// <summary>
        /// Constructor
        /// </summary>
        public ConsultaPublica()
        {
            _objConfiguracion = new Configuracion();
            _consultaPublicaEntity = new ConsultaPublicaEntity();
            _consultaPublicaDaLC = new ConsultaPublicaDaLC();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numeroSilpa"></param>
        /// <returns></returns>
        public DataTable ConsultarExpediente(string numeroSilpa)
        {
            try
            {
                ConsultaPublicaDaLC obj = new ConsultaPublicaDaLC();
                return obj.ConsultarExpediente(numeroSilpa);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "BuscarCampo-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }


        public DataTable ConsultarExpedienteEIA(string IdSolicitud)
        {
            try
            {
                ConsultaPublicaDaLC obj = new ConsultaPublicaDaLC();
                return obj.ConsultarExpedienteEIA(IdSolicitud);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "BuscarEIA-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }


        /// <summary>
        /// Lista las publicaciones existententes en la base de datos segun los registros que apliquen.
        /// </summary>
        /// <param name="parametroBusqueda">Parametro de busqueda requerido por el SP</param>
        /// <returns>Listado de  publicaciones existententes</returns>
        public List<ConsultaPublicaEntity> BuscarCampo(string parametroBusqueda)
        {
            try
            {
                ConsultaPublicaDaLC obj = new ConsultaPublicaDaLC();
                return obj.BuscarCampo(parametroBusqueda);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "BuscarCampo-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Lista los tramites existententes en la base de datos segun los registros que apliquen.
        /// </summary>
        /// <param name="parametroBusqueda">Parametro de busqueda requerido por el SP</param>
        /// <param name="pagesize">tamaño de paginas</param>
        /// <param name="pageNumber">numero de pagina a mostrar</param>
        /// <returns>lista de expedientes por tramite</returns>
        public List<ConsultaPublicaEntity> BuscarCampoPaginado(string parametroBusqueda, int pagesize, int pageNumber, string tipoBusqueda)
        {
            try
            {
                ConsultaPublicaDaLC obj = new ConsultaPublicaDaLC();
                if (parametroBusqueda != null)
                {

                    return obj.BuscarCampoPaginado(parametroBusqueda, pagesize, pageNumber, tipoBusqueda);
                }
                
                else
                    return new List<ConsultaPublicaEntity>();
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "BuscarCampoPG-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametroBusqueda"></param>
        /// <returns></returns>
        public List<ConsultaPublicaEntity> BuscarCamposEspecifico(string parametroBusqueda, int pagesize, int pageNumber, string tipoBusqueda)
        {
            try
            {
                ConsultaPublicaDaLC obj = new ConsultaPublicaDaLC();
                if (parametroBusqueda != null)
                    return obj.BuscarCamposEspecifico(parametroBusqueda, pagesize, pageNumber, tipoBusqueda);
                else
                    return new List<ConsultaPublicaEntity>();
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "BuscarCamposEspecifico-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }



        public string BuscarSiExisteRegistroEnBodega(int talSolId, string solNumSilpa, string origen)
        {
            try
            {
                ConsultaPublicaDaLC obj = new ConsultaPublicaDaLC();
                return obj.BuscarSiExisteRegistroEnBodega(talSolId, solNumSilpa, origen);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "BuscarSiExisteRegistroEnBodega-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        public int InsertarRegistroEnBodegaVsBodDataSila(int tarSolId, string solNumSilpa, string secId, string SecNombre, string secPadreId, string nombreSecPadre,
                                                    string autNombre, string traNombre, string expediente, string nombreProyecto, string tarFechaCreacion,
                                                    string tarFechaFinalizacion, string municipio, string departamento, string nombreCompleto, string solIdSolicitante,
                                                    string origen, string numeroDocumento) 
        {
            try
            {
                var registro = new ConsultaPublicaEntity() { 
                TAR_SOL_ID = tarSolId,
                SOL_NUM_SILPA = solNumSilpa,
                ORIGEN = origen,
                SEC_NOMBRE = SecNombre,
                NOMBRE_SEC_PADRE = nombreSecPadre,
                AUT_NOMBRE = autNombre,
                TRA_NOMBRE = traNombre,
                EXPEDIENTE = expediente,
                NOMBRE_PROYECTO = nombreProyecto,
                TAR_FECHA_CREACION = tarFechaCreacion,
                TAR_FECHA_FINALIZACION = tarFechaFinalizacion,
                MUNICIPIO = municipio,
                NOMBRE_COMPLETO = nombreCompleto
                };


                //Se reciben los parametros como cadenas, pero se intenta la conversion a entero, ya que en la base de datos se alamcenan en este tipo de dato.
                if (!string.IsNullOrEmpty(secId)) {
                    try
                    {registro.SEC_ID = int.Parse(secId);}
                    catch {}                
                }

                if (!string.IsNullOrEmpty(secPadreId))
                {
                    try
                    {registro.SEC_PADRE_ID = int.Parse(secPadreId);}
                    catch{}
                }

                if (!string.IsNullOrEmpty(departamento))
                {
                    try
                    { registro.DEPARTAMENTO = int.Parse(departamento).ToString(); }
                    catch { }
                }
                if (!string.IsNullOrEmpty(solIdSolicitante))
                {
                    try
                    { registro.SOL_ID_SOLICITANTE = int.Parse(solIdSolicitante).ToString(); }
                    catch { }
                }

                if (!string.IsNullOrEmpty(numeroDocumento))
                {
                    try
                    { registro.NUM_DOCUMENTO = int.Parse(numeroDocumento).ToString(); }
                    catch { }
                }
                

                ConsultaPublicaDaLC obj = new ConsultaPublicaDaLC();
                return obj.InsertarRegistroEnBodegaVsBodDataSila(registro);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "InsertarRegistroEnBodega-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }
    }
}
