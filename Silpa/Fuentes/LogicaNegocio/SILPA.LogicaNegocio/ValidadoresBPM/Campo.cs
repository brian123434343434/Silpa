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
    public class Campo
    {
        public CampoIdentity Identity;
        private CampoDalc Dalc;

        /// <summary>
        /// Objeto configuración
        /// </summary>
        private Configuracion _objConfiguracion;

        public Campo()
        {
            _objConfiguracion = new Configuracion();
            Identity = new CampoIdentity();
            Dalc = new CampoDalc();
        }

        /// <summary>
        /// Método para consultar el listado de campos
        /// </summary>
        /// <returns>Conjunto de datos con el listado de campos de FormBuilder</returns>
        public DataSet ListarCampo()
        {
            return Dalc.ListarCampo();
        }

        /// <summary>
        /// Inserta un campo de FormBuilder en la base de datos
        /// </summary>
        /// <param name="_codigo">Código del campo</param>
        /// <param name="_descripcion">Descripción del campo</param>
        /// <param name="_codigoTipoDato">Tipo de dato del campo</param>
        public void InsertarCampo(string _codigo, string _descripcion, int _codigoTipoDato)
        {
            try
            {
                this.Identity.IdCampo = _codigo;
                this.Identity.DescripcionCampo = _descripcion;
                this.Identity.IdTipoDato = _codigoTipoDato;
                this.Dalc.InsertarCampo(ref this.Identity);
            }
            catch (Exception e)
            {
                throw new Exception("Mensaje de error al insertar el campo: " + e.Message);
            }
        }

        /// <summary>
        /// Método que edita un campo y la validación existente
        /// </summary>
        /// <param name="_codigo">Código del campo</param>
        /// <param name="_descripcion">Descripción del campo</param>
        /// <param name="_codigoTipoDato">Tipo de dato del campo</param>
        public void EditarCampo(string _codigo, string _descripcion, int _codigoTipoDato)
        {
            try
            {
                this.Identity.IdCampo = _codigo;
                this.Identity.DescripcionCampo = _descripcion;
                this.Identity.IdTipoDato = _codigoTipoDato;
                this.Dalc.EditarCampo(ref this.Identity);
            }
            catch (Exception e)
            {
                throw new Exception("Mensaje de error al editar el tipo de dato: " + e.Message);
            }
        }

    }
}
