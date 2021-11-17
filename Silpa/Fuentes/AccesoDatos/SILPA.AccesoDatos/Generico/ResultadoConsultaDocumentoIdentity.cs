using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Generico
{
    public class ConsultaDocumentoIdentity
    {
        private string strNumeroSilpa = "";
        private DateTime fecha;
        private int intIdSolicitante = 0;
        private string strSolicitante = "";
        private int eex_id = 0;
        private string eex_nombre = "";
        private string path = "";
        private string fileName = "";
        private string fullName = "";
        private string urlName = "";

        public ConsultaDocumentoIdentity() { }
        public ConsultaDocumentoIdentity(string _strNumeroSilpa, Nullable<DateTime> _fecha, Nullable<int> _intIdSolicitante, string _strSolicitante,
            Nullable<int> _eex_id, string _eex_nombre, string _path, string _fileName)
        {

            strNumeroSilpa = (_strNumeroSilpa != null ? _strNumeroSilpa : "0");
            fecha = (_fecha != null ? _fecha.Value : System.DateTime.Now);
            intIdSolicitante = (_intIdSolicitante != null ? _intIdSolicitante.Value : 0);
            strSolicitante = (_strSolicitante != null ? _strSolicitante : "");
            eex_id = (_eex_id != null ? _eex_id.Value : 0);
            eex_nombre = (_eex_nombre != null ? _eex_nombre : "");
            path = (_path != null ? _path : "");
            fileName = (_fileName != null ? _fileName : "");

        }

        /// <summary>
        /// Atributo y propiedad que representa el PATH del archivo
        /// </summary>
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        /// <summary>
        /// Atributo y propiedad que representa el nombre completo del archivo PATH + name
        /// </summary>
        public string FullName
        {
            get { return path + fileName; }
        }

        /// <summary>
        /// Atributo y propiedad que representa el PATH del archivo
        /// </summary>
        public string UrlName
        {
            get { return urlName; }
            set { urlName = value; }
        }

        /// <summary>
        /// Atributo y propiedad que representa el PATH del archivo
        /// </summary>
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        /// <summary>
        /// Atributo y propiedad que representa el nombre de la entidad externa
        /// </summary>
        public string EexNombre
        {
            get { return eex_nombre; }
            set { eex_nombre = value; }
        }

        /// <summary>
        /// Atributo y propiedad que representa el nombre del solicitante
        /// </summary>
        public string Solicitante
        {
            get { return strSolicitante; }
            set { strSolicitante = value; }
        }

        /// <summary>
        /// Atributo y propiedad que representa el nombre del archivo
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        /// <summary>
        /// Atributo y propiedad que representa el numero silpa asociado al archivo
        /// </summary>
        public string NumeroSilpa
        {
            get { return strNumeroSilpa; }
            set { strNumeroSilpa = value; }
        }

        /// <summary>
        /// Atributo y propiedad que representa el ID del solicitante
        /// </summary>
        public int IdSolicitante
        {
            get { return intIdSolicitante; }
            set { intIdSolicitante = value; }
        }

        /// <summary>
        /// Atributo y propiedad que representa el numero de la entidad externa
        /// </summary>
        public int EexId
        {
            get { return eex_id; }
            set { eex_id = value; }
        }
    }
}