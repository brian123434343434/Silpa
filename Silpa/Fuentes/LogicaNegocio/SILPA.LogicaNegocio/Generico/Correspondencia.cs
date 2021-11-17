using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.Correspondencia;
using System.Data;

namespace SILPA.LogicaNegocio.Generico
{
    /// <summary>
    /// clase que encapsula la logica del negocio de la correspondencia
    /// </summary>
    public class Correspondencia
    {

        private CorrespondenciaDalc _objCorresDalc;

        /// <summary>
        /// ubicacion url del servicio
        /// </summary>
        private string _url;
        public string Url
        {
            get { return this._url; }
            set { this._url = value; }
        }


        public Correspondencia() { }

        /// <summary>
        /// Lista los estados de la correspondencia
        /// </summary>
        /// <param name="strVisible">Parametro que indica si esta activo o inactivo el estado</param>
        /// <returns>DataSet: con el listado de los estados</returns>
        public DataSet ListarEstados(string strVisible) 
        {
            _objCorresDalc = new CorrespondenciaDalc();
            return _objCorresDalc.ListarEstadosCorrespondencia(strVisible); 
        }
        
        /// <summary>
        /// Obtiene los registros de radicación para entregarselos silamc
        /// </summary>
        /// <param name="strAutoridadAmbiental"></param>
        /// <param name="strNumeroRadicado"></param>
        /// <param name="intIdRemitente"></param>
        /// <param name="dteFechaDesde"></param>
        /// <param name="dteFechaHasta"></param>
        /// <param name="intEstadoId"></param>
        /// <param name="strAsunto"></param>
        /// <returns></returns>
        public DataSet ConsultarMovimientosSilpa
            (int intAutoridadAmbiental, string strNumeroRadicado, string strNumeroSilpa,
                int intIdRemitente, string dteFechaDesde,
                string dteFechaHasta, int intEstadoId, string strAsunto
            )
        {
            _objCorresDalc = new CorrespondenciaDalc();
            return _objCorresDalc.ListarMovimientoRadicacion
                (intAutoridadAmbiental, strNumeroRadicado, strNumeroSilpa, 
                intIdRemitente, dteFechaDesde,dteFechaHasta, intEstadoId, strAsunto);
        }

        
        //public DataSet ConsultarMovimientosSilpaPorID(Int64 intIdRadicacion)
        //{
        //    _objCorresDalc = new CorrespondenciaDalc();
        //    //return _objCorresDalc.ConsultarMovimientoSilpaxId(Int64 intIdRadicacion);
        //}


        /// <summary>
        /// Método que obtiene el listado de las radicaciones desde silpa
        /// </summary>
        /// <param name="intExpediente">identificador del expediente</param>
        /// <param name="intIdAA">identificador de la Autoridad</param>
        /// <returns>Dataset: conjjunto de datos de la consulta</returns>
        public DataSet CorresPondenciaPorExpediente(string numeroSilpa, int autId)
        {
            //_objCorresDalc = new CorrespondenciaDalc();
            //return _objCorresDalc.CorresPondenciaPorExpediente(intExpediente, intIdAA);
            CorrespondenciaSilpaDalc _objCorrespondencia = new CorrespondenciaSilpaDalc();
            return _objCorrespondencia.CorresPondenciaPorExpediente(numeroSilpa, autId);
            //55
        }


    }
}
