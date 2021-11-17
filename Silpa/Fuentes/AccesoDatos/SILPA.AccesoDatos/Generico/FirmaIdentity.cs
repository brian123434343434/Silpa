using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    public class FirmaIdentity : EntidadSerializable
    {

        #region Declaración de Variables
        /// <summary>
        /// Código Único de Identificación de Entidad Pública
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
        /// Tipo de Identificación Nacional de la Persona (Funcionario Notificante)
        /// </summary>
        private string _strTipoIdentificacion;
        /// <summary>
        /// Grupo Número de Identificación Nacional
        /// </summary>
        private string _strGrupoIdentificacion;
        #endregion

        #region Declaración de Propiedades
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

        #region Declaración de Constructores
        public FirmaIdentity()
        {
        }
        #endregion

    }
}
