using SILPA.AccesoDatos.Expedientes;
using SoftManagement.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Expedientes
{
    public class ExpedienteSila
    {
        public ExpedienteSilaDALC _expedienteSilaDALC;
        public ExpedienteSilaEntity _expedienteSilaEntity;

        public ExpedienteSila()
        {
            _expedienteSilaDALC = new ExpedienteSilaDALC();
            _expedienteSilaEntity = new ExpedienteSilaEntity();
        }

        #region SILA

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametroBusqueda"></param>
        /// <returns></returns>
        public List<ExpedienteSilaEntity> ConsultarDetalleExpedienteSILA(string parametroBusqueda)
        {
            try
            {
                return _expedienteSilaDALC.ConsultarDetalleExpedienteSILA(parametroBusqueda);
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, "DetalleExpedienteSILA-ConsultaPublica:" + ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista las instancias de uno o varios acto
        /// </summary>
        /// <param name="int_tar_id">Identificador de la tarea</param>
        public DataTable ListarInstanciasActos(string str_exp_id, string str_tar_id, string str_nombre, string str_etapa, string str_tipos_acto)
        {
            try
            {
                return _expedienteSilaDALC.ListarInstanciasActos(str_exp_id, str_tar_id, str_nombre, str_etapa, str_tipos_acto);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "BuscarCampo-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Nuevo proceso para obtener los documentos por etapas pasasndo por la validacion de notificacion donde se verifica si requiere notificacion o si ya se notifico
        /// </summary>
        /// <param name="str_exp_id"></param>
        /// <param name="str_etapa"></param>
        /// <returns></returns>
        public DataTable ListarInstanciasActosValidaNotificacion(string str_exp_id, string str_etapa)
        {
            try
            {
                return _expedienteSilaDALC.ListarInstanciasActosValidaNotificacion(str_exp_id,str_etapa);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "BuscarCampo-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        public DataSet ListarEtapas(string str_exp_id)
        {
            try
            {
                return _expedienteSilaDALC.ListarEtapas(str_exp_id);
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, "DetalleExpedienteSILA-ConsultaPublica:" + ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public DataTable ListarExpedientesAsociados(string str_exp_id)
        {
            try
            {
                return _expedienteSilaDALC.ListarExpedientesAsociados(str_exp_id);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ListarExpedientesAsociads-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        public DataTable ArchivosForest(string str_exp_id)
        {
            try
            {
                return _expedienteSilaDALC.ArchivosForest(str_exp_id);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ArchivosForest-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }


        public DataTable ListarDocumentos(int int_dird_id)
        {
            try
            {
                return _expedienteSilaDALC.ListarDocumentos(int_dird_id);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ListarDocumentos-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        public DataSet listarDocumentosDS(int int_dird_id)
        {
            try
            {
                return _expedienteSilaDALC.listarDocumentosDS(int_dird_id);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ListarDocumentosDS-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        public DataTable ListarDirectoriosDocumentoSila(int int_tar_id)
        {
            try
            {
                return _expedienteSilaDALC.ListarDirectoriosDocumentoSila(int_tar_id);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ListarDirectoriosDocumentoSila-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }


        public DataTable ListarDirectoriosDocumentoTareaSila(int int_tar_id)
        {
            try
            {
                return _expedienteSilaDALC.ListarDirectoriosDocumentoTareaSila(int_tar_id);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "listarDirectoriosDocumento-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }
        #endregion

        #region SILAMC

        public List<ExpedienteSilaEntity> ConsultarDetalleExpedienteSilaMC(string parametroBusqueda)
        {
            try
            {
                return _expedienteSilaDALC.ConsultarDetalleExpedienteSilaMC(parametroBusqueda);
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, "DetalleExpedienteSILAMC-ConsultaPublica:" + ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public DataSet ListarEtapasSilaMC(string str_exp_id)
        {
            try
            {
                return _expedienteSilaDALC.ListarEtapasSilaMC(str_exp_id);
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, "DetalleExpedienteSILAMC-ConsultaPublica:" + ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public DataTable ListarExpedientesAsociadosSilaMC(string str_exp_id)
        {
            try
            {
                return _expedienteSilaDALC.ListarExpedientesAsociadosSilaMC(str_exp_id);
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, "DetalleExpedienteSILAMC-ConsultaPublica:" + ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public DataTable ListarInstanciasActosSilaMC(string str_exp_id, string str_tar_id, string str_nombre, string str_etapa, string str_tipos_acto)
        {
            try
            {
                return _expedienteSilaDALC.ListarInstanciasActosSilaMC(str_exp_id, str_tar_id, str_nombre, str_etapa, str_tipos_acto);
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, "DetalleExpedienteSILAMC-ConsultaPublica:" + ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public DataTable ListarDocumentosSilaMC(int int_dird_id)
        {
            try
            {
                return _expedienteSilaDALC.ListarDocumentosSilaMC(int_dird_id);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ListarDocumentosSilaMC-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        public DataSet ListarDocumentosDSSilaMC(int int_dird_id)
        {
            try
            {
                return _expedienteSilaDALC.ListarDocumentosDSSilaMC(int_dird_id);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "listarDocumentosDSSilaMC-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        public DataTable ListarDirectoriosDocumentoSilaMC(int int_dird_id)
        {
            try
            {
                return _expedienteSilaDALC.ListarDirectoriosDocumentoSilaMC(int_dird_id);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "ListarDirectoriosDocumentoSilaMC-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }

        public DataTable ListarDirectoriosDocumentoTareaSILAMC(int int_tar_id)
        {
            try
            {
                return _expedienteSilaDALC.ListarDirectoriosDocumentoTareaSILAMC(int_tar_id);
            }
            catch (Exception sqle)
            {
                SMLog.Escribir(Severidad.Critico, "listarDirectoriosDocumento-ConsultaPublica:" + sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }
        #endregion


    }
}