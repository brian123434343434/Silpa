using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccessoDatos.Generico
{
    /// <summary>
    /// Información relacionada con requerimiento de pago
    /// </summary>
    public class RequerimientoPagoIdentity: EntidadSerializable
    {
        #region Declaración de Variables
        /// <summary>
        /// Concepto o descripción
        /// </summary>
        private string _concepto;
        /// <summary>
        /// Número SILPA
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
        /// Código de identificación del trámite
        /// </summary>
        private string _codigoIdentificacionTramite;
        /// <summary>
        /// Número del documento de cobro
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
        /// Valor en Números
        /// </summary>
        private decimal _valorNumeros;
        /// <summary>
        /// Código de barras del documento de cobro (si aplica)
        /// </summary>
        private string _codigoBarras;
        /// <summary>
        /// Número de referencia
        /// </summary>
        private string _numeroReferencia;
        /// <summary>
        /// Fecha de expedición del documento de cobro
        /// </summary>
        private DateTime _fechaExpedicion;

        /// <summary>
        /// Ruta de acceso del documento adjunto (si aplica)
        /// </summary>
        private string _urlDocumentoAdjunto;
        
        #endregion

        #region Declaración de Propiedades
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
        /// - Confirmación de la necesidad de Información Adicional
        /// </summary>
        /// <param name="numeroDocumentoCobro">Número de documento de cobro</param>
        /// <param name="valorLetras">Valor en letras</param>
        /// <param name="valorNumeros">Valor en números</param>
        /// <param name="codigoBarras">Carácteres del código de barras del documento de cobro</param>
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
        /// <param name="valorNumeros">Valor a cobrar en números</param>
        /// <param name="numeroReferencia">Número de Referencia</param>
        /// <param name="fechaExpedicion">Fecha de expedición del documento de cobro</param>
        /// <param name="codigoBarras">Carácteres de código de barras (si aplica)</param>
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
        /// <param name="valorNumeros">Valor en números</param>
        /// <param name="numeroReferencia">Número de referencia</param>
        /// <param name="fechaExpedicion">Fecha de expedición</param>
        /// <param name="codigoBarras">Código de Barras</param>
        /// <param name="urlDocumentoAdjunto">Documento adjunto (no requerido)</param>
        /// <param name="codigoIDTramite">Código de identificación del Trámite</param>
        /// <param name="numeroSILPA">Número SILPA de la solicitud</param>
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
        /// Constructor vacío
        /// </summary>
        public RequerimientoPagoIdentity() { }
        #endregion
    }
}
