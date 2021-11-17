using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccessoDatos.Generico
{
    /// <summary>
    /// Informaci�n relacionada con requerimiento de pago
    /// </summary>
    public class RequerimientoPagoIdentity: EntidadSerializable
    {
        #region Declaraci�n de Variables
        /// <summary>
        /// Concepto o descripci�n
        /// </summary>
        private string _concepto;
        /// <summary>
        /// N�mero SILPA
        /// </summary>
        private string _numeroSILPA;
        /// <summary>
        /// Departamento
        /// </summary>
        private string _departamento;
        /// <summary>
        /// Municipio
        /// </summary>
        private string _municipio;
        /// <summary>
        /// Ciudad
        /// </summary>
        private string _ciudad;
        /// <summary>
        /// C�digo de identificaci�n del tr�mite
        /// </summary>
        private string _codigoIdentificacionTramite;
        /// <summary>
        /// N�mero del documento de cobro
        /// </summary>
        private string _numeroDocumentoCobro;
        /// <summary>
        /// Tipo de documento de Cobro
        /// </summary>
        private string _tipoDocumentoCobro;
        /// <summary>
        /// Valor en letras
        /// </summary>
        private string _valorLetras;
        /// <summary>
        /// Valor en N�meros
        /// </summary>
        private decimal _valorNumeros;
        /// <summary>
        /// C�digo de barras del documento de cobro (si aplica)
        /// </summary>
        private string _codigoBarras;
        /// <summary>
        /// N�mero de referencia
        /// </summary>
        private string _numeroReferencia;
        /// <summary>
        /// Fecha de expedici�n del documento de cobro
        /// </summary>
        private DateTime _fechaExpedicion;

        /// <summary>
        /// Ruta de acceso del documento adjunto (si aplica)
        /// </summary>
        private string _urlDocumentoAdjunto;
        
        #endregion

        #region Declaraci�n de Propiedades
        public string Concepto
        {
            get { return _concepto; }
            set { _concepto = value; }
        }
        public string Ciudad
        {
            get { return _ciudad; }
            set { _ciudad = value; }
        }
        public string Municipio
        {
            get { return _municipio; }
            set { _municipio = value; }
        }
        public string Departamento
        {
            get { return _departamento; }
            set { _departamento = value; }
        }
        public string CodigoIdentificacionTramite
        {
            get { return _codigoIdentificacionTramite; }
            set { _codigoIdentificacionTramite = value; }
        }
        public string TipoDocumentoCobro
        {
            get { return _tipoDocumentoCobro; }
            set { _tipoDocumentoCobro = value; }
        }
        public string NumeroSILPA
        {
            get { return _numeroSILPA; }
            set { _numeroSILPA = value; }
        }
        public string NumeroDocumentoCobro
        {
            get { return _numeroDocumentoCobro; }
            set { _numeroDocumentoCobro = value; }
        }
        public string ValorLetras
        {
            get { return _valorLetras; }
            set { _valorLetras = value; }
        }
        public decimal ValorNumeros
        {
            get { return _valorNumeros; }
            set { _valorNumeros = value; }
        }
        public string CodigoBarras
        {
            get { return _codigoBarras; }
            set { _codigoBarras = value; }
        }
        public string NumeroReferencia
        {
            get { return _numeroReferencia; }
            set { _numeroReferencia = value; }
        }
        public DateTime FechaExpedicion
        {
            get { return _fechaExpedicion; }
            set { _fechaExpedicion = value; }
        }
        public string UrlDocumentoAdjunto
        {
            get { return _urlDocumentoAdjunto; }
            set { _urlDocumentoAdjunto = value; }
        }
        #endregion
        #region Constructores
        /// <summary>
        /// Constructor para web services:
        /// - Otorgamiento del permiso
        /// - Confirmaci�n de la necesidad de Informaci�n Adicional
        /// </summary>
        /// <param name="numeroDocumentoCobro">N�mero de documento de cobro</param>
        /// <param name="valorLetras">Valor en letras</param>
        /// <param name="valorNumeros">Valor en n�meros</param>
        /// <param name="codigoBarras">Car�cteres del c�digo de barras del documento de cobro</param>
        public RequerimientoPagoIdentity(string numeroDocumentoCobro, string valorLetras,
            decimal valorNumeros, string codigoBarras)
        {
            this.NumeroDocumentoCobro = numeroDocumentoCobro;
            this.ValorLetras = valorLetras;
            this.ValorNumeros = ValorNumeros;
            this.CodigoBarras = codigoBarras;
        }
        /// <summary>
        /// Constructor para web service Liquidar permiso ambiental
        /// </summary>
        /// <param name="valorNumeros">Valor a cobrar en n�meros</param>
        /// <param name="numeroReferencia">N�mero de Referencia</param>
        /// <param name="fechaExpedicion">Fecha de expedici�n del documento de cobro</param>
        /// <param name="codigoBarras">Car�cteres de c�digo de barras (si aplica)</param>
        /// <param name="urlDocumentoAdjunto">Documento adjunto (si aplica)</param>
        public RequerimientoPagoIdentity(decimal valorNumeros, string numeroReferencia,
            DateTime fechaExpedicion, string codigoBarras, string urlDocumentoAdjunto,
            string numeroDC)
        {
            this._numeroDocumentoCobro = numeroDC;
            this.ValorNumeros = valorNumeros;
            this.NumeroReferencia = numeroReferencia;
            this.FechaExpedicion = fechaExpedicion;
            this.CodigoBarras = codigoBarras;
            this.UrlDocumentoAdjunto = urlDocumentoAdjunto;
        }
        /// <summary>
        /// Constructor para usar en documento de cobro y pagos PSE
        /// </summary>
        /// <param name="valorNumeros">Valor en n�meros</param>
        /// <param name="numeroReferencia">N�mero de referencia</param>
        /// <param name="fechaExpedicion">Fecha de expedici�n</param>
        /// <param name="codigoBarras">C�digo de Barras</param>
        /// <param name="urlDocumentoAdjunto">Documento adjunto (no requerido)</param>
        /// <param name="codigoIDTramite">C�digo de identificaci�n del Tr�mite</param>
        /// <param name="numeroSILPA">N�mero SILPA de la solicitud</param>
        public RequerimientoPagoIdentity(decimal valorNumeros, string numeroReferencia,
            DateTime fechaExpedicion, string codigoBarras, string urlDocumentoAdjunto, string codigoIDTramite, string numeroSILPA, string tipoDocumentoCobro)
        {
            this.TipoDocumentoCobro = tipoDocumentoCobro;
            this.NumeroSILPA = numeroSILPA;
            this.CodigoIdentificacionTramite = codigoIDTramite;
            this.ValorNumeros = valorNumeros;
            this.NumeroReferencia = numeroReferencia;
            this.FechaExpedicion = fechaExpedicion;
            this.CodigoBarras = codigoBarras;
            this.UrlDocumentoAdjunto = urlDocumentoAdjunto;
        }
        /// <summary>
        /// Constructor vac�o
        /// </summary>
        public RequerimientoPagoIdentity() { }
        #endregion
    }
}
