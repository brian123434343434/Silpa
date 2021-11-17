using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.ValidadoresBPM;
using System.Data;
using SILPA.LogicaNegocio.Generico;

namespace SILPA.LogicaNegocio.ValidadoresBPM
{
    public class TipoDato
    {
        public TipoDatoIdentity Identity;
        private TipoDatoDalc Dalc;

        /// <summary>
        /// Objeto configuración
        /// </summary>
        private Configuracion _objConfiguracion;

        /// <summary>
        /// Constructor
        /// </summary>
        public TipoDato()
        {
            _objConfiguracion = new Configuracion();
            Identity = new TipoDatoIdentity();
            Dalc = new TipoDatoDalc();
        }

        /// <summary>
        /// Método para consultar el listado de tipos de datos
        /// </summary>
        /// <returns>Conjunto de datos con el listado de los tipos de dato</returns>
        public DataSet ListarTipoDato()
        {
            return Dalc.ListarTipoDato();
        }

        /// <summary>
        /// Inserta un tipo de dato de FormBuilder en la base de datos
        /// </summary>
        /// <param name="_descripcion">Descripción del tipo de dato</param>        
        public void InsertarTipoDato(string _descripcion)
        {
            try
            {
                this.Identity.DescripcionTipoDato=_descripcion;
                this.Dalc.InsertarTipoDato(ref this.Identity);
            }
            catch (Exception e)
            {
                throw new Exception("Mensaje de error al insertar el tipo de dato: " + e.Message);
            }
        }

        /// <summary>
        /// Método que edita un tipo de dato existente
        /// </summary>
        /// <param name="_codigo">Identificador del tipo de dato</param>
        /// <param name="_descripcion">Descripción del tipo de dato</param>        
        public void EditarTipoDato(int _codigo, string _descripcion)
        {
            try
            {
                this.Identity.IdTipoDato=_codigo;
                this.Identity.DescripcionTipoDato=_descripcion;                
                this.Dalc.EditarTipoDato(ref this.Identity);
            }
            catch (Exception e)
            {
                throw new Exception("Mensaje de error al editar el tipo de dato: " + e.Message);
            }
        }
    }
}
