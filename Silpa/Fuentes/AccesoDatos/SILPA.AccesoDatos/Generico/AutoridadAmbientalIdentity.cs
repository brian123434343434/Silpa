using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    /// <summary>
    ///  clase que define los datos de la autoridad ambiental
    /// </summary>
    [Serializable]
    public class AutoridadAmbientalIdentity : EntidadSerializable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AutoridadAmbientalIdentity() { }

        #region Declaracion de variables ...

        /*
         *
         */


        private int _idAutoridad;
        private string _nombre;
        private bool _activo;
        private string _direccion;        
        private string _telefono;
        private string _fax;
        private string _email;
        private string _NIT;
        private bool _cargue;
        private bool _base;
        private string _servicioRadicacion;
        private bool _radicacionAutomatica;
        private string _gs1_Code;
        private string _correoCorrespondencia;
        private int _consecutivo;
        private int _anio;
        private string _ppe_certificate_sub;
        private string _ppe_url;
        private string _ppe_code;
        private string _razon_social;
        private string _emailCorrespondencia;
        private string _NombreLargo;
        private string _nombreBanco;
        private string _numeroCuenta;
        private string _tipoCuenta;
        private string _nitTitularCuenta;
        private string _titularCuenta;



        #endregion

        #region Declaracion de propiedades ...

        public int IdAutoridad { get { return this._idAutoridad; } set { this._idAutoridad = value; } }
        public string Nombre { get { return this._nombre; } set { this._nombre = value; } }
        public bool Activo { get { return this._activo; } set { this._activo = value; } }
        public string Direccion { get { return this._direccion; } set { this._direccion = value; } }
        public string Telefono { get { return this._telefono; } set { this._telefono = value; } }
        public string Fax { get { return this._fax; } set { this._fax = value; } }
        public string Email { get { return this._email; } set { this._email = value; } }
        public string NIT { get { return _NIT; } set { _NIT = value; } }
        public bool Cargue { get { return _cargue; } set { _cargue = value; } }
        public bool Base { get { return _base; } set { _base = value; } }
        public string ServicioRadicacion { get { return this._servicioRadicacion; } set { this._servicioRadicacion = value; } }
        /// <summary>
        /// Determina si la autoridad ambiental tiene sistema automático para 
        /// la radicación sincrónica de los documentos adjuntos por el usuario
        /// </summary>
        public bool RadicacionAutomatica { get { return this._radicacionAutomatica; } set { this._radicacionAutomatica = value; } }
        /// <summary>
        /// url del servicio de radicacon de la AA
        /// </summary>
        public string urlServicioRadicacion { get { return this._fax; } set { this._fax = value; } }
        public string CorreoCorrespondencia { get { return _correoCorrespondencia; } set { _correoCorrespondencia = value; } }
        public string Gs1_Code { get { return _gs1_Code; } set { _gs1_Code = value; } }
        public int Consecutivo { get { return _consecutivo; } set { _consecutivo = value; } }
        public int Anio { get { return _anio; } set { _anio = value; } }
        public string Ppe_certificate_sub { get { return _ppe_certificate_sub; } set { _ppe_certificate_sub = value; } }
        public string Ppe_url { get { return _ppe_url; } set { _ppe_url = value; } }
        public string Ppe_code { get { return _ppe_code; } set { _ppe_code = value; } }
        public string Razon_social { get { return _razon_social; } set { _razon_social = value; } }
        /// <summary>
        /// correo electronico para correspondencia
        /// </summary>
        public string EmailCorrespondencia
        {
            get { return this._emailCorrespondencia; }
            set { this._emailCorrespondencia = value; }
        }

        public string Nombre_Largo { get { return _NombreLargo; } set { _NombreLargo = value; } }

        public string NombreBanco { get { return _nombreBanco; } set { _nombreBanco = value; } }

        public string NumeroCuneta { get { return _numeroCuenta; } set { _numeroCuenta = value; } }


        public string TipoCuenta { get { return _tipoCuenta; } set { _tipoCuenta = value; } }

        public string NitTitularCuenta { get { return _nitTitularCuenta; } set { _nitTitularCuenta = value; } }

        public string TitularCuenta { get { return _titularCuenta; } set { _titularCuenta = value; } }

        public string CorreoSalvoconducto { get; set;  }


        #endregion


    }
}
