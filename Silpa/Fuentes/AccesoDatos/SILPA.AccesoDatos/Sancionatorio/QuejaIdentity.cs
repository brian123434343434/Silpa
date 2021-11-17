using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Sancionatorio
{
    public class QuejaIdentity : EntidadSerializable
    {
        public QuejaIdentity()
        {
        }

        #region Definicion de los atributos

        /// <summary>
        /// Identificador de lqa queja
        /// </summary>
        private Int64 _idQueja;

        /// <summary>
        /// Numero SILPA
        /// </summary>
        private string _numeroSilpa;


        /// <summary>
        /// Número vital completo
        /// </summary>
        private string _numeroVital;
                       
        /// <summary>
        /// Descripcion de la queja
        /// </summary>
        private string _descripcionQueja;

        /// <summary>
        /// Identificador del municipio de la queja
        /// </summary>
        private int _idMunicipio;

        /// <summary>
        /// Identificador de la ubicacion de la queja
        /// </summary>
        private Nullable<int> _IdUbicacion;

        /// <summary>
        /// Identificador del corregimiento de la queja
        /// </summary>
        private Nullable<int> _idCorregimiento;

        /// <summary>
        /// Identificador de la vereda de la queja
        /// </summary>
        private Nullable<int> _idVereda;

        /// <summary>
        /// Identificador del área hidrográfica de la queja
        /// </summary>
        private Nullable<int> _idAreaHidrografica;

        /// <summary>
        /// Identificador de la zona hidrográfica de la queja
        /// </summary>
        private Nullable<int> _idZonaHidrografica;

        /// <summary>
        /// Identificador de la subzona hidrográfica de la queja
        /// </summary>
        private Nullable<int> _idSubZonaHidrografica;

        /// <summary>
        /// Identificador de la Autoridad Ambiental de la queja
        /// </summary>
        private Nullable<int> _idAutoridadAmbiental;

        /// <summary>
        /// Identificador del sector de la queja
        /// </summary>
        private Nullable<int> _idSector;

        /// <summary>
        /// Estado de la queja
        /// </summary>
        private bool _activoQueja;
        
        #endregion


        #region Propiedades....

        public Int64 IdQueja
        {
            get { return _idQueja; }
            set { _idQueja = value; }
        }

        public string NumeroSilpa
        {
            get { return _numeroSilpa; }
            set { _numeroSilpa = value; }
        }

        public string NumeroVital
        {
            get { return _numeroVital; }
            set { _numeroVital = value; }
        }

        public string DescripcionQueja
        {
            get { return _descripcionQueja; }
            set { _descripcionQueja = value; }
        }

        public int IdMunicipio
        {
            get { return _idMunicipio; }
            set { _idMunicipio = value; }
        }

        public Nullable<int> IdUbicacion
        {
            get { return _IdUbicacion; }
            set { _IdUbicacion = value; }
        }

        public Nullable<int> IdCorregimiento
        {
            get { return _idCorregimiento; }
            set { _idCorregimiento = value; }
        }

        public Nullable<int> IdVereda
        {
            get { return _idVereda; }
            set { _idVereda = value; }
        }

        public Nullable<int> IdAreaHidrografica
        {
            get { return _idAreaHidrografica; }
            set { _idAreaHidrografica = value; }
        }

        public Nullable<int> IdZonaHidrografica
        {
            get { return _idZonaHidrografica; }
            set { _idZonaHidrografica = value; }
        }

        public Nullable<int> IdSubZonaHidrografica
        {
            get { return _idSubZonaHidrografica; }
            set { _idSubZonaHidrografica = value; }
        }

        public Nullable<int> IdAutoridadAmbiental
        {
            get { return _idAutoridadAmbiental; }
            set { _idAutoridadAmbiental = value; }
        }

        public Nullable<int> IdSector
        {
            get { return _idSector; }
            set { _idSector = value; }
        }

        public bool ActivoQueja
        {
            get { return _activoQueja; }
            set { _activoQueja = value; }
        }

        #endregion
    }
}
