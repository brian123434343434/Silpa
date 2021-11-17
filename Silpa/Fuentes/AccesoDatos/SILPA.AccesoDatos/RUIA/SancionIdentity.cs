using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.RUIA
{
    public class SancionIdentity : EntidadSerializable
    {
        public SancionIdentity()
        {
        }

        #region Definicion de los atributos

        /// <summary>
        /// Identificador de la sanción
        /// </summary>
        private long _idSancion;

        /// <summary>
        /// Identificador del tipo de persona
        /// </summary>
        private int _tipoPersona;

        /// <summary>
        /// Identificador del tipo de falta
        /// </summary>
        private int _idFalta;

        /// <summary>
        /// Lugar de concurrencia de la sancion
        /// </summary>
        private string _motivoModificacion;

        /// <summary>
        /// Descripcion de la norma 
        /// </summary>
        private string _descripcionNorma;

        /// <summary>
        /// Número del Expediente de la sanción
        /// </summary>
        private string _numeroExpediente;

        /// <summary>
        /// Número del acto de la sanción
        /// </summary>
        private string _numeroActo;

        /// <summary>
        /// Fecha de expedición del acto administrativo
        /// </summary>
        private string _fechaExpedicion;

        /// <summary>
        /// Fecha de ejecutoria del acto administrativo
        /// </summary>
        private string _fechaEjecutoria;

        /// <summary>
        /// Fecha de ejecución o cumplimiento de la sanción
        /// </summary>
        private string _fechaEjecucion;

        /// <summary>
        /// Razon social en caso de ser Persona Jurídica
        /// </summary>
        private string _razonSocial;

        /// <summary>
        /// Nit de la razón social
        /// </summary>
        private string _nit;

        /// <summary>
        /// Primer nombre del sancionado
        /// </summary>
        private string _primerNombre;

        /// <summary>
        /// Segundo nombre del sancionado
        /// </summary>
        private string _segundoNombre;

        /// <summary>
        /// Primer apellido del sancionado
        /// </summary>
        private string _primerApellido;

        /// <summary>
        /// Segundo apellido del sancionado
        /// </summary>
        private string _segundoApellido;

        /// <summary>
        /// Identificador del tipo de identificación
        /// </summary>
        private int _idTipoIdentificacion;

        /// <summary>
        /// Número de identificación del sancionado
        /// </summary>
        private string _numeroIdentificacion;
        
        /// <summary>
        /// Identificador del municipio del documento
        /// </summary>
        private int? _idMunicipio;

        /// <summary>
        /// Identificador del departamento del documento
        /// </summary>
        private int _idDpto;

        /// <summary>
        /// Identificador de la Autoridad Ambiental
        /// </summary>
        private int? _idAutoridad;

        ///// <summary>
        ///// Fecha desde donde inicia la sanción
        ///// </summary>
        //private string _fechaDesde;

        ///// <summary>
        ///// Fecha donde finaliza la sanción
        ///// </summary>
        //private string _fechaHasta;

        /// <summary>
        /// Descripción de la desfijación de la publicación
        /// </summary>
        private string _descripcionDesfijacion;

        /// <summary>
        /// Identificador de la sancion principal
        /// </summary>
        private int _idSancionPrincipal;

        /// <summary>
        /// Descripcion de la sancion principal
        /// </summary>
        private string _sancionPrincipal;


        /// <summary>
        /// Tipo Documento //dependiendo de los documentos vital para que no se repitan
        /// </summary>
        private string _tipoDocumento;


        private int _depId;
        private int _munId;
        private int _corId;
        private int _verId;

        /// <summary>
        /// 30-sep-2010 - aegb : incidencia 2284
        /// Indica que el reporte de RUIA esta en trámite de modificación
        /// </summary>
        private int? _tramiteModificacion;

        private string observaciones;
        #endregion

        #region Propiedades....
        public int VerId
        {
            get { return _verId; }
            set { _verId = value; }
        }

        public int CorId
        {
            get { return _corId; }
            set { _corId = value; }
        }

        public int MunId
        {
            get { return _munId; }
            set { _munId = value; }
        }

        public int DepId
        {
            get { return _depId; }
            set { _depId = value; }
        }

        public Int64 IdSancion
        {
            get { return _idSancion; }
            set { _idSancion = value; }
        }

        public int TipoPersona
        {
            get { return _tipoPersona; }
            set { _tipoPersona = value; }
        }

        public int IdFalta
        {
            get { return _idFalta; }
            set { _idFalta = value; }
        }

        public string MotivoModificacion
        {
            get { return _motivoModificacion; }
            set { _motivoModificacion = value; }
        }

        public string DescripcionNorma
        {
            get { return _descripcionNorma; }
            set { _descripcionNorma = value; }
        }

        public string NumeroExpediente
        {
            get { return _numeroExpediente; }
            set { _numeroExpediente = value; }
        }

        public string NumeroActo
        {
            get { return _numeroActo; }
            set { _numeroActo = value; }
        }

        public string FechaExpedicion
        {
            get { return _fechaExpedicion; }
            set { _fechaExpedicion = value; }
        }

        public string FechaEjecutoria
        {
            get { return _fechaEjecutoria; }
            set { _fechaEjecutoria = value; }
        }

        public string FechaEjecucion
        {
            get { return _fechaEjecucion; }
            set { _fechaEjecucion = value; }
        }

        public string RazonSocial
        {
            get { return _razonSocial; }
            set { _razonSocial = value; }
        }

        public string Nit
        {
            get { return _nit; }
            set { _nit = value; }
        }

        public string PrimerNombre
        {
            get { return _primerNombre; }
            set { _primerNombre = value; }
        }

        public string SegundoNombre
        {
            get { return _segundoNombre; }
            set { _segundoNombre = value; }
        }

        public string PrimerApellido
        {
            get { return _primerApellido; }
            set { _primerApellido = value; }
        }

        public string SegundoApellido
        {
            get { return _segundoApellido; }
            set { _segundoApellido = value; }
        }

        public int IdTipoIdentificacion
        {
            get { return _idTipoIdentificacion; }
            set { _idTipoIdentificacion = value; }
        }

        public string NumeroIdentificacion
        {
            get { return _numeroIdentificacion; }
            set { _numeroIdentificacion = value; }
        }

        public int? IdMunicipio
        {
            get { return _idMunicipio; }
            set { _idMunicipio = value; }
        }
        public int IdDpto
        {
            get { return _idDpto; }
            set { _idDpto = value; }
        }

        public int? IdAutoridad
        {
            get { return _idAutoridad; }
            set { _idAutoridad = value; }
        }

        public string TipoDocumento
        {
            get { return _tipoDocumento; }
            set { _tipoDocumento = value; }
        }

        //public string FechaDesde
        //{
        //    get { return _fechaDesde; }
        //    set { _fechaDesde = value; }
        //}

        //public string FechaHasta
        //{
        //    get { return _fechaHasta; }
        //    set { _fechaHasta = value; }
        //}

        public string DescripcionDesfijacion
        {
            get { return _descripcionDesfijacion; }
            set { _descripcionDesfijacion = value; }
        }
        public int IdSancionPrincipal
        {
            get { return _idSancionPrincipal; }
            set { _idSancionPrincipal = value; }
        }
        public string SancionPrincipal
        {
            get { return _sancionPrincipal; }
            set { _sancionPrincipal = value; }
        }
        // 30-sep-2010 - aegb : incidencia 2284
        public int? TramiteModificacion
        {
            get { return _tramiteModificacion; }
            set { _tramiteModificacion = value; }
        }

        public string Observaciones
        {
            get { return this.observaciones; }
            set { this.observaciones = value; }
        }

        public string UsuarioModifica { get; set; }

        #endregion
    }
}
