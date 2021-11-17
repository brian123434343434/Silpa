using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Utilidades
{
    public class UbicacionIdentity
    {

        /// <summary>
        /// Constructor vacio de la clase ubicación
        /// </summary>
        public UbicacionIdentity() {}

        #region Declaracion de variables

        /// <summary>
        /// Identificador de la ubicación
        /// </summary>
        private int _idUbicacion;

        /// <summary>
        /// Identificador del vereda
        /// </summary>
        private int _idVereda;

        /// <summary>
        /// Identificador del expediente 
        /// </summary>
        private int _idExpediente;

        /// <summary>
        /// Identificador del departamento
        /// </summary>
        private int _idDepartamento;

        /// <summary>
        /// Identificador del municipio
        /// </summary>
        private int _idmunicipio;

        /// <summary>
        /// Identificador del corregimiento
        /// </summary>
        private int _idCorregimiento;

        /// <summary>
        /// Identificador de la Cuenca
        /// </summary>
        private int _idCuenca;

        /// <summary>
        /// Identificadores de la ubicacion separados por (,)
        /// </summary>
        private string _ubicacionID;

        /// <summary>
        /// Nombres de la ubicacion separados por (,)
        /// </summary>
        private string _ubicacionNombre;
        #endregion

        #region Declaracion de Propiedades

        public int IdUbicacion
        {
            get { return _idUbicacion; }
            set { _idUbicacion = value; }
        }

        public string UbicacionID
        {
            get { return _ubicacionID; }
            set { _ubicacionID = value; }
        }
        public string UbicacionNombre
        {
            get { return _ubicacionNombre; }
            set { _ubicacionNombre = value; }
        }

        public int IdCuenca
        {
            get { return _idCuenca; }
            set { _idCuenca = value; }
        }

        public int IdCorregimiento
        {
            get { return _idCorregimiento; }
            set { _idCorregimiento = value; }
        }

        public int IdVereda
        {
            get { return _idVereda; }
            set { _idVereda = value; }
        }

        public int Idmunicipio
        {
            get { return _idmunicipio; }
            set { _idmunicipio = value; }
        }

        public int IdDepartamento
        {
            get { return _idDepartamento; }
            set { _idDepartamento = value; }
        }

        public int IdExpediente
        {
            get { return _idExpediente; }
            set { _idExpediente = value; }
        }
        #endregion 

    }
}
