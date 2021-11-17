using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos;
using System.Collections;
using SILPA.AccesoDatos.Generico;
using SILPA.Comun;
using System.Data;
using SILPA.AccesoDatos.Parametrizacion;

namespace SILPA.LogicaNegocio.Parametrizacion
{
    public class Parametrizacion
    {
        public Parametrizacion()
        {
        }

        public List<SILPA.AccesoDatos.Parametrizacion.TipoTramite> Tramites()
        {
            SILPA.AccesoDatos.Parametrizacion.TipoTramiteDalc tramite = new SILPA.AccesoDatos.Parametrizacion.TipoTramiteDalc();
            try
            {
                return tramite.Listar();
            }
            finally
            {
                tramite = null;
            }
        }

        public SILPA.AccesoDatos.Parametrizacion.TipoTramite Tramites(int param)
        {
            SILPA.AccesoDatos.Parametrizacion.TipoTramiteDalc tramite = new SILPA.AccesoDatos.Parametrizacion.TipoTramiteDalc();
            try
            {
                return tramite.ListarTramites(param);
            }
            finally
            {
                tramite = null;
            }
        }

        /// <summary>
        /// Lista los Tipos de documentos que se entregan al Servicio de Tipos de Documentos
        /// </summary>
        /// <returns>Lista de TipoDocumentoType en xml</returns>
        public string Documentos()
        {
            SILPA.AccesoDatos.Generico.TipoDocumentoDalc tipoDocumentoDalc = new SILPA.AccesoDatos.Generico.TipoDocumentoDalc();
            List<TipoDocumentoIdentity> listaDocumentos = new List<TipoDocumentoIdentity>();
            listaDocumentos = tipoDocumentoDalc.ListarTiposDeDocumento();
            TipoDocumentoType[] listaTipoDocumentoType = new TipoDocumentoType[listaDocumentos.Count];
            TipoDocumentoType tipoDocumentoType;
            XmlSerializador serializador = new XmlSerializador();
            int conteo = 0;
            foreach (TipoDocumentoIdentity documento in listaDocumentos)
            {
                tipoDocumentoType = new TipoDocumentoType();
                tipoDocumentoType.id = documento.ID;
                tipoDocumentoType.nombre = documento.Nombre;
                listaTipoDocumentoType[conteo] = tipoDocumentoType;
                conteo++;
            }
            return serializador.serializar(listaTipoDocumentoType);
        }

        /// <summary>
        /// Lista los parámetros del tipo dado
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public DataSet listarParametrosBPM(int tipo)
        {
            BpmParametros bpmparametros = new BpmParametros();
            return bpmparametros.ListarBmpParametros(tipo, null);

        }

        public bool insertarTipoDocumento(string nombre, int? parametro)
        {
            TipoDocumentoDalc tipoDocumentoDalc = new TipoDocumentoDalc();
            TipoDocumentoIdentity tipoDocumento = new TipoDocumentoIdentity();
            tipoDocumento.Nombre = nombre;
            tipoDocumento.ParametroBPM = parametro;
            tipoDocumentoDalc.InsertarTipoDocumento(tipoDocumento);

            //LINEA DE CODIGO ADICIONADA AL MOMENTO DE INCLUIR EL CAMPO EN BASE DE DATOS
            tipoDocumento.HabilitadoReposicion = true;
            return true;
        }


        /// <summary>
        /// Actualiza el Tiempo de espera para consultar estados de notificación en PDI
        /// </summary>
        /// <param name="p">Tiempo</param>
        public void actualizarTiempoNotificacion(string p, int activo)
        {
            TiempoNotificacionDalc _TiempoNotificacionDalc = new TiempoNotificacionDalc();
            _TiempoNotificacionDalc.Actualizar(p, activo);
        }

        /// <summary>
        /// Consulta el Tiempo actual de espera para consultar estados de notificación en PDI
        /// </summary>
        /// <returns>Tiempo</returns>
        public string obtenerTiempoNotificacion(out int activo)
        {
            try
            {
	            activo = 0;
	            TiempoNotificacionDalc _TiempoNotificacionDalc = new TiempoNotificacionDalc();
	            return _TiempoNotificacionDalc.ObtenerTiempo(out activo);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al consultar el tiempo actual de espera para consultar estados de notificación en PDI.";
                throw new Exception(strException, ex);
            }
        }

        /// <summary>
        /// Retorna el valor del parametro indicado
        /// </summary>
        /// <param name="p_intIdParametro">int con el id del parametro</param>
        /// <param name="p_strNombreParametro">string con el nombre del parametro</param>
        /// <returns>string con el valor del parametro encontrado</returns>
        public string ObtenerValorParametroGeneral(int p_intIdParametro, string p_strNombreParametro)
        {
            ParametroEntity objParametro = null;
            ParametroDalc objParametroDalc = null;

            //Cargar datos de busqueda
            objParametro = new ParametroEntity();
            objParametro.IdParametro = p_intIdParametro;
            objParametro.NombreParametro = p_strNombreParametro;

            //Obtener valor
            objParametroDalc = new ParametroDalc();
            objParametroDalc.obtenerParametros(ref objParametro);

            return objParametro.Parametro;
        }
    }
}
