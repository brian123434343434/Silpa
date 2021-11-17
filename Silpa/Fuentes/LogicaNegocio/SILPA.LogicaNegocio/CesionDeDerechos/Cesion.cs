using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SILPA.Comun;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Comunicacion;
using System.Data;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.CesionDeDerechos;
using System.Xml.Serialization;

namespace SILPA.LogicaNegocio.CesionDeDerechos
{
    public class Cesion
    {
        /// <summary>
        /// Atributo y propiedad que representa el Id de numero Vital id  de proceso en SILPA
        /// </summary>
        private string _numeroVital;
        public string NumeroVital
        {
            get { return _numeroVital; }
            set { _numeroVital = value; }
        }

        /// <summary>
        /// Atributo y propiedad con el Id del usuario que se va a cambiar por el nuevo Cesionario
        /// </summary>
        private string _usuario;
        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        /// <summary>
        /// Atributo y propiedad con el ID del usuario nuevo cesionario del proceso
        /// </summary>
        private string _usuarioCesionario;
        public string UsuarioCesionario
        {
            get { return _usuarioCesionario; }
            set { _usuarioCesionario = value; }
        }

        /// <summary>
        /// Constructor standar de la clase
        /// </summary>
        public Cesion()
        { 
        }

        public Cesion(string vital)
        {
            this._numeroVital = vital;
        }

        public Cesion(string vital, string usu, string cesionario)
        {
            this._numeroVital = vital;
            this._usuario = usu;
            this._usuarioCesionario = cesionario;  
        }

        public void Ejecutar()
        {
            SILPA.AccesoDatos.CesionDeDerechos.Cesion ces = new SILPA.AccesoDatos.CesionDeDerechos.Cesion();
            try
            {
                ces.EjecutarCesion(_numeroVital, _usuario, _usuarioCesionario);
            }
            finally
            {
                ces = null;
            }
        }

        public string ConsultarIdUsuarioPorSilpa()
        {
            SILPA.AccesoDatos.CesionDeDerechos.Cesion ces = new SILPA.AccesoDatos.CesionDeDerechos.Cesion();
            try
            {
                return ces.ConsultarIdPorSilpa(_numeroVital);  
            }
            finally
            {
                ces = null;
            }
        }

        /// <summary>
        /// Obtiene el listado de los numero vital de cesión de derechos asociado a una persona 
        /// </summary>
        /// <param name="idPersona">long identificador del solicitante </param>
        /// <param name="idAutoridad">int: identificador de la autoridad</param>
        /// <returns>List<PersonaCesionIdentity></returns>
        public string ObtenerNumeroVitalSecionPersona(long idPersona, int idAutoridad) 
        {
            SILPA.AccesoDatos.CesionDeDerechos.Cesion objCesion = new SILPA.AccesoDatos.CesionDeDerechos.Cesion();
            List<PersonaCesionIdentity> listVitalPersona = objCesion.ListarNumeroVitalPersonaCesion(idPersona, idAutoridad);

            MemoryStream memoryStream = new MemoryStream();
            XmlSerializer serializador = new XmlSerializer(typeof(List<PersonaCesionIdentity>));
            serializador.Serialize(memoryStream, listVitalPersona);
            
            return System.Text.UTF8Encoding.UTF8.GetString(memoryStream.ToArray());
        }

    }
}
