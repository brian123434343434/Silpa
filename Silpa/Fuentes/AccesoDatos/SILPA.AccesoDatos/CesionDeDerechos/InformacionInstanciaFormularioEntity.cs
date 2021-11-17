using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.CesionDeDerechos
{
    /// <summary>
    /// Clase que contiene el nodo central
    /// </summary>
    public class InformacionInstanciaFormularioEntity : EntidadSerializable
    {
        /// <summary>
        /// Atributo y propiedades de identificador del la Instancia del formulario.
        /// </summary>
        private int _idFormInstance;
        public int IdFormInstance
        {
            get { return _idFormInstance; }
            set { _idFormInstance = value; }
        }

        /// <summary>
        /// Atributo y propiedades de identificador del la Instancia del formulario Padre o predecesor.
        /// </summary>
        private int _idFormInstancePadre;
        public int IdFormInstancePadre
        {
            get { return _idFormInstancePadre; }
            set { _idFormInstancePadre = value; }
        }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public InformacionInstanciaFormularioEntity()
        {
            _idFormInstancePadre = -1;
        }

        
    }
}
