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
    public class ValidacionCampo
    {
        public ValidacionCampoIdentity Identity;
        private ValidacionCampoDalc Dalc;

        /// <summary>
        /// Objeto configuración
        /// </summary>
        private Configuracion _objConfiguracion;

        public ValidacionCampo()
        {
            _objConfiguracion = new Configuracion();
            Identity = new ValidacionCampoIdentity();
            Dalc = new ValidacionCampoDalc();
        }

        /// <summary>
        /// Método para consultar el listado de campos y sus validaciones
        /// </summary>
        /// <returns>Conjunto de datos con el listado de campos y validaciones de FormBuilder</returns>
        public DataSet ListarValidacionCampo()
        {
            return Dalc.ListarValidacionCampo();
        }

        /// <summary>
        /// Inserta un campo y su validacion de FormBuilder en la base de datos
        /// </summary>
        /// <param name="_codigoCampo">Código del campo</param>
        /// <param name="_codigoValidacion">Código del tipo de validación</param>
        /// <param name="_activo">Si está activo o no la validación</param>
        public void InsertarValidacionCampo(string _codigoCampo, int _codigoValidacion, string _activo)
        {
            try
            {
                this.Identity.IdCampo = _codigoCampo;
                this.Identity.IdValidacion=_codigoValidacion;
                this.Identity.ActivoValidacionCampo = _activo;
                this.Dalc.InsertarValidacionCampo(ref this.Identity);
            }
            catch (Exception e)
            {
                throw new Exception("Mensaje de error al insertar la validacion con el campo: " + e.Message);
            }
        }

        
        public void EditarValidacionCampo(int _idValidacionCampo, string _idCampo, int _idValidacion,
            string _fechaInsercion, string _activo)
        {
            try
            {
                this.Identity.IdValidacionCampo = _idValidacionCampo;
                this.Identity.IdCampo = _idCampo;
                this.Identity.IdValidacion = _idValidacion;
                this.Identity.FechaInsercion = _fechaInsercion;
                this.Identity.ActivoValidacionCampo = _activo;
                this.Dalc.EditarValidacionCampo(ref this.Identity);
            }
            catch (Exception e)
            {
                throw new Exception("Mensaje de error al editar la validacion con el campo: " + e.Message);
            }
        }
    }
}
