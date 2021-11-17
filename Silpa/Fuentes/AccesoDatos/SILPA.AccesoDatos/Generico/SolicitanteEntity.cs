using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    public class SolicitanteEntity
    {
        #region Atributos

        /// <summary>
        /// Identificador del solicitante
        /// </summary>
        protected int int_id;
        /// <summary>
        /// Primer nombre del solicitante
        /// </summary>
        protected string str_primer_nombre;
        /// <summary>
        /// Segundo nombre del solicitante
        /// </summary>
        protected string str_segundo_nombre;
        /// <summary>
        /// Primer apellido del solicitante
        /// </summary>
        protected string str_primer_apellido;
        /// <summary>
        /// Segundo apellido del solicitante
        /// </summary>
        protected string str_segundo_apellido;
        /// <summary>
        /// Razon social del solicitante
        /// </summary>
        protected string str_razon_social;
        /// <summary>
        /// Nombre completo del solicitante
        /// </summary>
        protected string str_nombre_completo;
        /// <summary>
        /// Identificacion del solicitante
        /// </summary>
        protected string str_numero_identificacion;
        /// <summary>
        /// Tipo identificacion del solicitante
        /// </summary>
        protected int int_tipo_identificacion;
        /// <summary>
        /// Lugar de expedicion del documento del solicitante
        /// </summary>
        protected string str_lugar_expedicion;
        /// <summary>
        /// Telefono del solicitante
        /// </summary>
        protected string str_telefono;
        /// <summary>
        /// Celular del solicitante
        /// </summary>
        protected string str_celular;
        /// <summary>
        /// Fax del solicitante
        /// </summary>
        protected string str_fax;
        /// <summary>
        /// Correo electronico del solicitante
        /// </summary>
        protected string str_correo_electronico;
        /// <summary>
        /// Tipo de persona del solicitante
        /// </summary>
        protected int int_tipo_persona;
        /// <summary>
        /// Tarjeta profesional del apoderado del solicitante
        /// </summary>
        protected string str_tarjeta_profesional;
        /// <summary>
        /// Identificacion del solicitante padre del apoderado o del representante legal
        /// </summary>
        protected int int_sol_padre_id;
        /// <summary>
        /// Autoridad ambiental del solicitante
        /// </summary>
        protected int int_aut_id;
        /// <summary>
        /// Tipo de solicitante
        /// </summary>
        protected int int_tipo_solicitante;
        /// <summary>
        /// Especifica si el solitante esta activo
        /// </summary>
        protected bool b_activo;
        /// <summary>
        /// Especifica si el solitante es de sila o vital
        /// </summary>
        protected bool b_sila;

        /// <summary>
        /// Trámites a los que aplica el solicitante
        /// </summary>
        protected object col_tramites;

        /// <summary>
        /// Direccion correspondencia del solicitante 
        /// </summary>
        protected string str_direccion_correspondencia;

        /// <summary>
        /// Direccion domicilio del solicitante
        /// </summary>
        protected string str_direccion_domicilio;

        /// <summary>
        /// Tramites del solicitante
        /// </summary>
        protected string str_tramite;

        protected bool _esNotificacionElectronica;

        protected bool _esNotificacionElectronica_AA;

        protected bool _esNotificacionElectronica_EXP;

        #region Documentacion Atributo
        /// <summary>
        /// Número del expediente en vital para el tercero interviniente
        /// </summary>
        #endregion
        protected string str_numero_vital;

        protected string str_identificacion_notificacion;


        #endregion

        #region Propiedades

        /// <summary>
        /// Identificador del solicitante
        /// </summary>
        public int ID
        {
            get { return this.int_id; }
            set { this.int_id = value; }
        }
        /// <summary>
        /// Primer nombre del solicitante
        /// </summary>
        public string PrimerNombre
        {
            get { return this.str_primer_nombre; }
            set { this.str_primer_nombre = value; }
        }
        /// <summary>
        /// Segundo nombre del solicitante
        /// </summary>
        public string SegundoNombre
        {
            get { return this.str_segundo_nombre; }
            set { this.str_segundo_nombre = value; }
        }
        /// <summary>
        /// Primer apellido del solicitante
        /// </summary>
        public string PrimerApellido
        {
            get { return this.str_primer_apellido; }
            set { this.str_primer_apellido = value; }
        }
        /// <summary>
        /// Segundo apellido del solicitante
        /// </summary>
        public string SegundoApellido
        {
            get { return this.str_segundo_apellido; }
            set { this.str_segundo_apellido = value; }
        }
        /// <summary>
        /// Razon social del solicitante
        /// </summary>
        public string RazonSocial
        {
            get { return this.str_razon_social; }
            set { this.str_razon_social = value; }
        }
        /// <summary>
        /// Nombre completo del solicitante
        /// </summary>
        public string NombreCompleto
        {
            get { return this.str_nombre_completo; }
            set { this.str_nombre_completo = value; }
        }
        /// <summary>
        /// Identificacion del solicitante
        /// </summary>
        public string NumeroIdentificacion
        {
            get { return this.str_numero_identificacion; }
            set { this.str_numero_identificacion = value; }
        }
        /// <summary>
        /// Tipo identificacion del solicitante
        /// </summary>
        public int TipoIdentificacion
        {
            get { return this.int_tipo_identificacion; }
            set { this.int_tipo_identificacion = value; }
        }
        /// <summary>
        /// Lugar de expedicion del documento del solicitante
        /// </summary>
        public string LugarExpedicion
        {
            get { return this.str_lugar_expedicion; }
            set { this.str_lugar_expedicion = value; }
        }
        /// <summary>
        /// Telefono del solicitante
        /// </summary>
        public string Telefono
        {
            get { return this.str_telefono; }
            set { this.str_telefono = value; }
        }
        /// <summary>
        /// Celular del solicitante
        /// </summary>
        public string Celular
        {
            get { return this.str_celular; }
            set { this.str_celular = value; }
        }
        /// <summary>
        /// Fax del solicitante
        /// </summary>
        public string Fax
        {
            get { return this.str_fax; }
            set { this.str_fax = value; }
        }
        /// <summary>
        /// Correo electronico del solicitante
        /// </summary>
        public string CorreoElectronico
        {
            get { return this.str_correo_electronico; }
            set { this.str_correo_electronico = value; }
        }
        /// <summary>
        /// Tipo de persona del solicitante
        /// </summary>
        public int TipoPersona
        {
            get { return this.int_tipo_persona; }
            set { this.int_tipo_persona = value; }
        }
        /// <summary>
        /// Tarjeta profesional del apoderado del solicitante
        /// </summary>
        public string TarjetaProfesional
        {
            get { return this.str_tarjeta_profesional; }
            set { this.str_tarjeta_profesional = value; }
        }
        /// <summary>
        /// Identificacion del solicitante padre del apoderado o del representante legal
        /// </summary>
        public int SolPadreId
        {
            get { return this.int_sol_padre_id; }
            set { this.int_sol_padre_id = value; }
        }
        /// <summary>
        /// Autoridad ambiental del solicitante
        /// </summary>
        public int AutId
        {
            get { return this.int_aut_id; }
            set { this.int_aut_id = value; }
        }
        /// <summary>
        /// Tipo de solicitante
        /// </summary>
        public int TipoSolicitante
        {
            get { return this.int_tipo_solicitante; }
            set { this.int_tipo_solicitante = value; }
        }
        /// <summary>
        /// Especifica si el solicitante esta activo
        /// </summary>
        public bool Activo
        {
            get { return this.b_activo; }
            set { this.b_activo = value; }
        }
        /// <summary>
        /// Especifica si el solitante es de sila o vital
        /// </summary>
        public bool Sila
        {
            get { return this.b_sila; }
            set { this.b_sila = value; }
        }

        /// <summary>
        /// Trámites a los que aplica el solicitante
        /// </summary>
        public object Tramites
        {
            get { return this.col_tramites; }
            set { this.col_tramites = value; }
        }

        /// <summary>
        /// Direccion correspondencia del solicitante
        /// </summary>
        public string DireccionCorrespondencia
        {
            get { return this.str_direccion_correspondencia; }
            set { this.str_direccion_correspondencia = value; }
        }

        /// <summary>
        /// Direccion domicilio del solicitante
        /// </summary>
        public string DireccionDomicilio
        {
            get { return this.str_direccion_domicilio; }
            set { this.str_direccion_domicilio = value; }
        }

        /// <summary>
        /// Tramites del solicitante
        /// </summary>
        public string Tramite
        {
            get { return this.str_tramite; }
            set { this.str_tramite = value; }
        }

        #region Documentacion Propiedad
        /// <summary>
        /// Número expediente en vital para el tercero interviniente
        /// </summary>
        #endregion
        public string NumeroVital
        {
            get { return this.str_numero_vital; }
            set { this.str_numero_vital = value; }
        }
        public string IdentificacionNotificacion
        {
            get { return this.str_identificacion_notificacion; }
            set { this.str_identificacion_notificacion = value; }
        }

        public bool EsNotificacionElectronica
        {
            get { return this._esNotificacionElectronica; }
            set { this._esNotificacionElectronica = value; }
        }

        public bool EsNotificacionElectronica_AA
        {
            get { return this._esNotificacionElectronica_AA; }
            set { this._esNotificacionElectronica_AA = value; }
        }

        public bool EsNotificacionElectronica_EXP
        {
            get { return this._esNotificacionElectronica_EXP; }
            set { this._esNotificacionElectronica_EXP = value; }
        }


        #endregion
    }
}
