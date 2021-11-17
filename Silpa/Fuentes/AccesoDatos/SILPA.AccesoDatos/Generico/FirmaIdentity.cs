using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    public class FirmaIdentity : EntidadSerializable
    {

        #region Declaraci�n de Variables
        /// <summary>
        /// C�digo �nico de Identificaci�n de Entidad P�blica
        /// </summary>
        private string _strCodigoEntidad;
        /// <summary>
        /// Dependencia de la Entidad
        /// </summary>
        private string _strDependenciaEntidad;
        /// <summary>
        /// Identificador de la Dependencia
        /// </summary>
        private string _strIdentificacionDependencia;
        /// <summary>
        /// Nombre de la Dependencia
        /// </summary>
        private string _strNombreDependencia;
        /// <summary>
        /// Identificador Nacional de la Persona (Funcionario Notificante)
        /// </summary>
        private string _strIdentificacionFuncionario;
        /// <summary>
        /// Tipo de Identificaci�n Nacional de la Persona (Funcionario Notificante)
        /// </summary>
        private string _strTipoIdentificacion;
        /// <summary>
        /// Grupo N�mero de Identificaci�n Nacional
        /// </summary>
        private string _strGrupoIdentificacion;
        #endregion

        #region Declaraci�n de Propiedades
        public string CodigoEntidad
        {
            get { return _strCodigoEntidad; }
            set { _strCodigoEntidad = value; }
        }
        public string DependenciaEntidad
        {
            get { return _strDependenciaEntidad; }
            set { _strDependenciaEntidad = value; }
        }
        public string IdentificacionDependencia
        {
            get { return _strIdentificacionDependencia; }
            set { _strIdentificacionDependencia = value; }
        }
        public string NombreDependencia
        {
            get { return _strNombreDependencia; }
            set { _strNombreDependencia = value; }
        }
        public string IdentificacionFuncionario
        {
            get { return _strIdentificacionFuncionario; }
            set { _strIdentificacionFuncionario = value; }
        }
        public string TipoIdentificacion
        {
            get { return _strTipoIdentificacion; }
            set { _strTipoIdentificacion = value; }
        }
        public string GrupoIdentificacion
        {
            get { return _strGrupoIdentificacion; }
            set { _strGrupoIdentificacion = value; }
        }
        #endregion

        #region Declaraci�n de Constructores
        public FirmaIdentity()
        {
        }
        #endregion

    }
}
