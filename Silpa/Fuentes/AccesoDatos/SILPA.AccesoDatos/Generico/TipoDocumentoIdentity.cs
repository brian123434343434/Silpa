using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Generico
{
    public class TipoDocumentoIdentity
    {

        #region Atributos
        /// <summary>
        /// Identificador del Tipo de Documento
        /// </summary>
        private int _id;

        /// <summary>
        /// Nombre del tipo de Documento
        /// </summary>
        private string _nombre;

        /// <summary>
        /// Id del parámtero de BPM
        /// </summary>
        private int? _idParametroBPM;

        private int? _id_flujoNotElec;
        #endregion

        private bool _habilitadoReposicion;
        public bool HabilitadoReposicion
        {
            get { return _habilitadoReposicion; }
            set { _habilitadoReposicion = value; }
        }
        #region Propiedades
        public int? ParametroBPM
        {
            get { return _idParametroBPM; }
            set { _idParametroBPM = value; }
        }
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public int? IdFlujoNotElec
        {
            get { return _id_flujoNotElec; }
            set { _id_flujoNotElec = value; }
        }

        #endregion
    }
}
