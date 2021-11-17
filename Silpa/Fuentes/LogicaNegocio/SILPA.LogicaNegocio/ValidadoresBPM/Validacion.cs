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
    public class Validacion
    {
        public ValidacionIdentity Identity;
        private ValidacionDalc Dalc;

        /// <summary>
        /// Objeto configuraci�n
        /// </summary>
        private Configuracion _objConfiguracion;

        /// <summary>
        /// Constructor
        /// </summary>
        public Validacion()
        {
            _objConfiguracion = new Configuracion();
            Identity = new ValidacionIdentity();
            Dalc = new ValidacionDalc();
        }

        /// <summary>
        /// M�todo para consultar el listado de validaciones
        /// </summary>
        /// <returns>Conjunto de datos con el listado de las validaciones</returns>
        public DataSet ListarValidacion()
        {
            return Dalc.ListarValidacion();
        }

        /// <summary>
        /// Inserta una validacion de FormBuilder en la base de datos
        /// </summary>
        /// <param name="_descripcion">Descripci�n de la validaci�n</param>
        /// <param name="_sentencia">Sentencia de la validaci�n</param>
        public void InsertarValidacion(string _descripcion, string _sentencia)
        {
            try
            {
                this.Identity.DescripcionValidacion=_descripcion;
                this.Identity.SentenciaValidacion=_sentencia;
                this.Dalc.InsertarValidacion(ref this.Identity);
            }
            catch (Exception e)
            {
                throw new Exception("Mensaje de error al insertar la validacion: " + e.Message);
            }
        }

        /// <summary>
        /// M�etodo que edita una validaci�n existente
        /// </summary>
        /// <param name="_codigo">Identificador de la validaci�n</param>
        /// <param name="_descripcion">Descripci�n de la validaci�n</param>
        /// <param name="_sentencia">Sentencia de la validaci�n</param>
        public void EditarValidacion(int _codigo, string _descripcion, string _sentencia)
        {
            try
            {
                this.Identity.IdValidacion=_codigo;
                this.Identity.DescripcionValidacion=_descripcion;
                this.Identity.SentenciaValidacion=_sentencia;
                this.Dalc.EditarValidacion(ref this.Identity);
            }
            catch (Exception e)
            {
                throw new Exception("Mensaje de error al editar la validacion: " + e.Message);
            }
        }

    }
}
