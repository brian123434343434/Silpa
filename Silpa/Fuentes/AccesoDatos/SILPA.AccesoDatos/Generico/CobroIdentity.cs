using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    [Serializable]
    public class CobroIdentity : EntidadSerializable
    {
        #region Atributos

        private decimal _idCobro;

        private string _numSILPA;

        private string _numExpediente;

        private string _numReferencia;

        private DateTime _fechaExpedicion;

        private DateTime _fechaPagoOportuno;

        private DateTime _fechaVencimiento;

        private int _intTipoTramite;

        private ConceptoIdentity _conceptoCobro;

        private List<DetalleCobroIdentity> _listaConceptoCobro;

        private string _codigoBarras;

        private string _rutaArchivo;

        private long _numSolicitud;

        private string _indicadorProcesoField;

        private string _numDocumentoField;

        private int _estadoCobroField;

        private int _transaccionField;

        private int _solicitanteID;

        private int _autoridadAmbientalID;

        private string _fechaTransaccionField;

        private DateTime _fechaTransaccionBancaria;

        private string _bancoField;

        private string _fechaRecordacionField;

        private Int32 _valor;

        private string _origenCobro;

        private string _origenLlamadoPSE;

        private string _llamadoServicioPSE;

        private string _resultadollamadoServicioPSE;

        private string _estadoPSE;

        private DateTime _fechaTransaccionPSE;

        private List<PermisoCobroIdentity> _listaPermisos;

        #endregion

        #region Propiedades

        public decimal IdCobro
        {
            get { return _idCobro; }
            set { _idCobro = value; }
        }
        public string NumSILPA
        {
            get { return _numSILPA; }
            set { _numSILPA = value; }
        }
        public string NumExpediente
        {
            get { return _numExpediente; }
            set { _numExpediente = value; }
        }
        public string NumReferencia
        {
            get { return _numReferencia; }
            set { _numReferencia = value; }
        }
        public DateTime FechaExpedicion
        {
            get { return _fechaExpedicion; }
            set { _fechaExpedicion = value; }
        }
        public DateTime FechaPagoOportuno
        {
            get { return _fechaPagoOportuno; }
            set { _fechaPagoOportuno = value; }
        }
        public DateTime FechaVencimiento
        {
            get { return _fechaVencimiento; }
            set { _fechaVencimiento = value; }
        }
        public ConceptoIdentity ConceptoCobro
        {
            get { return _conceptoCobro; }
            set { _conceptoCobro = value; }
        }
        public List<DetalleCobroIdentity> ListaConceptoCobro
        {
            get { return _listaConceptoCobro; }
            set { _listaConceptoCobro = value; }
        }
        public string CodigoBarras
        {
            get { return _codigoBarras; }
            set { _codigoBarras = value; }
        }
        public string RutaArchivo
        {
            get { return _rutaArchivo; }
            set { _rutaArchivo = value; }
        }
        public long NumSolicitud
        {
            get { return _numSolicitud; }
            set { _numSolicitud = value; }
        }
        public string NumDocumentoField
        {
            get { return _numDocumentoField; }
            set { _numDocumentoField = value; }
        }
        public string IndicadorProcesoField
        {
            get { return _indicadorProcesoField; }
            set { _indicadorProcesoField = value; }
        }

        /// <comentarios/>
        public int EstadoCobro
        {
            get
            {
                return this._estadoCobroField;
            }
            set
            {
                this._estadoCobroField = value;
            }
        }

        /// <comentarios/>
        public int Transaccion
        {
            get
            {
                return this._transaccionField;
            }
            set
            {
                this._transaccionField = value;
            }
        }

        public int SolicitanteID
        {
            get
            {
                return this._solicitanteID;
            }
            set
            {
                this._solicitanteID = value;
            }
        }

        public int AutoridaAmbientalID
        {
            get
            {
                return this._autoridadAmbientalID;
            }
            set
            {
                this._autoridadAmbientalID = value;
            }
        }

        /// <comentarios/>
        public string FechaTransaccion
        {
            get
            {
                return this._fechaTransaccionField;
            }
            set
            {
                this._fechaTransaccionField = value;
            }
        }

        /// <comentarios/>
        public DateTime FechaTransaccionBancaria
        {
            get
            {
                return this._fechaTransaccionBancaria;
            }
            set
            {
                this._fechaTransaccionBancaria = value;
            }
        }

        /// <comentarios/>
        public string Banco
        {
            get
            {
                return this._bancoField;
            }
            set
            {
                this._bancoField = value;
            }
        }

        /// <comentarios/>
        public string FechaRecordacion
        {
            get
            {
                return this._fechaRecordacionField;
            }
            set
            {
                this._fechaRecordacionField = value;
            }
        }

        /// <comentarios/>
        public Int32 Valor
        {
            get
            {
                return this._valor;
            }
            set
            {
                this._valor = value;
            }
        }

        /// <comentarios/>
        public string OrigenLlamadoPSE
        {
            get
            {
                return this._origenLlamadoPSE;
            }
            set
            {
                this._origenLlamadoPSE = value;
            }
        }

        /// <comentarios/>
        public string ServicioLlamadoPSE
        {
            get
            {
                return this._llamadoServicioPSE;
            }
            set
            {
                this._llamadoServicioPSE = value;
            }
        }

        /// <comentarios/>
        public string ResultadoServicioLlamadoPSE
        {
            get
            {
                return this._resultadollamadoServicioPSE;
            }
            set
            {
                this._resultadollamadoServicioPSE = value;
            }
        }

        /// <comentarios/>
        public string EstadoPSE
        {
            get
            {
                return this._estadoPSE;
            }
            set
            {
                this._estadoPSE = value;
            }
        }

        /// <comentarios/>
        public DateTime FechaTransaccionPSE
        {
            get
            {
                return this._fechaTransaccionPSE;
            }
            set
            {
                this._fechaTransaccionPSE = value;
            }
        }

        /// <summary>
        /// Identificador del tipo de documento
        /// </summary>
        private int idTipoDocumento;
        public int IdTipoDocumento
        {
            get { return this.idTipoDocumento; }
            set { this.idTipoDocumento = value; }

        }

        /// <summary>
        /// Origen del cobro
        /// </summary>
        public string OrigenCobro
        {
            get { return this._origenCobro; }
            set { this._origenCobro = (value != null ? value.Trim() : value); }

        }

        public List<PermisoCobroIdentity> Permisos
        {
            get { return this._listaPermisos; }
            set { this._listaPermisos = value; }

        }

        #endregion

        public CobroIdentity() { }
    }
}
