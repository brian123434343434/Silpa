using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Data;

namespace SILPA.LogicaNegocio.Generico
{
    /// <summary>
    /// clase que define la autoridad Ambiental en su comportamiento
    /// </summary>
    [Serializable]
    public partial class AutoridadAmbiental
    {
        
        /// <summary>
        /// clase que contiene el estado de la Entidad
        /// </summary>
        public AutoridadAmbientalIdentity objAutoridadIdentity;

        /// Constructor vacío de la clase
        /// </summary>
        public AutoridadAmbiental() 
        {
            objAutoridadIdentity = new AutoridadAmbientalIdentity();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="intIdAutoridad">Int: identificador de la autoridad ambiental</param>
        public AutoridadAmbiental(int intIdAutoridad)
        {
            objAutoridadIdentity = new AutoridadAmbientalIdentity();
            objAutoridadIdentity.IdAutoridad = intIdAutoridad;

            AutoridadAmbientalDalc objDalc= new AutoridadAmbientalDalc();
            //objDalc.ObtenerAutoridadById(ref this.objAutoridadIdentity);
            // HAVA: 13 - dic - 2010
            // Consulta la AA sin filtro de Ventanilla Integrada.
            objDalc.ObtenerAutoridadNoIntegradaById(ref this.objAutoridadIdentity);
        }

        public void ObtenerAutoridadAmbiental(int intIdAutoridad)
        {
            objAutoridadIdentity = new AutoridadAmbientalIdentity();
            objAutoridadIdentity.IdAutoridad = intIdAutoridad;

            AutoridadAmbientalDalc objDalc = new AutoridadAmbientalDalc();
            //objDalc.ObtenerAutoridadById(ref this.objAutoridadIdentity);

            // HAVA: 13 - dic - 2010
            // Consulta la AA sin filtro de Ventanilla Integrada.
            objDalc.ObtenerAutoridadNoIntegradaById(ref this.objAutoridadIdentity);
        }

        /// <summary>
        /// Lista las autoridades ambientales para una solicitud a partir de si instancia de proceso
        /// </summary>
        /// <param name="int64IdProcessInstance">Identificador de la instacia de proceso</param>
        /// <returns>
        /// DataSet -> Campos [AUT_ID - AUT_NOMBRE ]
        /// </returns>
        public DataSet ListarAutoridadesAmbientalesByUbicacion(int int64IdProcessInstance)
        {
            AutoridadAmbientalDalc obj = new AutoridadAmbientalDalc();
            return obj.ListarAAXUbicacion(int64IdProcessInstance);
        }

        /// <summary>
        /// Idnetificador del municipio
        /// </summary>
        /// <param name="_idMunicipio">Conjunto de datos de la jurisdicción: [MUN_ID] - [AUT_ID] - [AUT_NOMBRE]</param>
        public DataSet ListarAAXJurisdiccion(Nullable<int> _idMunicipio)
        {
            AutoridadAmbientalDalc obj = new AutoridadAmbientalDalc();
            return obj.ListarAAXJurisdiccion(_idMunicipio);
        }

        /// <summary>
        /// Lista las autoridades ambientales para una solicitud a partir de si instancia de proceso
        /// </summary>
        /// <param name="int64IdProcessInstance">Identificador de la instacia de proceso</param>
        /// <returns>
        /// DataSet -> Campos [AUT_ID - AUT_NOMBRE ]
        /// </returns>
        /// <remarks>Aplica solo para formularios de solicitudes diferentes a DAA y que aplica AA</remarks>
        public DataSet ListarAutoridadesAmbientalesOtros(int int64IdProcessInstance)
        {
            AutoridadAmbientalDalc obj = new AutoridadAmbientalDalc();
            return obj.ListarAAXUbicacionOtros(int64IdProcessInstance);
        }

        /// <summary>
        /// Obtiene el identificador de la Autoridad Ambiental  MAVDT
        /// </summary>
        /// <param name="idParametro">int: identificador del parametro en la tabla silpa:pre.dbo.gen_parametro</param>
        /// <returns>Identificador de la autoridad ambiental MAVDT</returns>
        public int ObtenerIdAutoridadAmbientalMAVDT(int idParametro) 
        { 
            //AutoridadAmbiental aDalc = new AutoridadAmbientalDalc();
            //return aDalc.ObtenerIdAutoridadAmbientalMAVDT(idParametro);
            AutoridadAmbientalDalc aDalc = new AutoridadAmbientalDalc();
            return aDalc.ObtenerIdAutoridadMAVDT(idParametro);
        }

        public DataSet ListarAutoridadAmbientalXNumeroVital(string str_NumeroVital)
        {
            AutoridadAmbientalDalc obj = new AutoridadAmbientalDalc();
            return obj.ListarAutoridadAmbientalXNumeroVital(str_NumeroVital);
        }

		/// <summary>
        /// Lista las autoridades ambientales en la BD. Pueden listarse todas o una en particular.
        /// </summary>
        /// <param name="IntId" >Con este valor se lista las autoridades ambientales con el identificador, si es null se listan todas</param>
        /// <returns>DataSet con los registros y las siguientes columnas: AUT_ID, AUT_NOMBRE, AUT_DIRECCION, AUT_TELEFONO, AUT_FAX, 
        /// AUT_CORREO, AUT_NIT, AUT_CARGUE, APLICA_RADICACION, AUT_ACTIVO</returns>
        public DataSet ListarAutoridadAmbiental(Nullable<int> IntId)
        {
            AutoridadAmbientalDalc obj = new AutoridadAmbientalDalc();
            return obj.ListarAutoridadAmbiental(IntId);
        }

		/// <summary>
        /// Lista las autoridades ambientales que se relacionan a permisos
        /// </summary>
        /// <returns>DataSet con los registros y las siguientes columnas: AUT_ID, AUT_NOMBRE, AUT_DIRECCION, AUT_TELEFONO, AUT_FAX, 
        /// AUT_CORREO, AUT_NIT, AUT_CARGUE, APLICA_RADICACION, AUT_ACTIVO</returns>
        public DataSet ListarAutoridadAmbientalPermisos()
        {
            AutoridadAmbientalDalc obj = new AutoridadAmbientalDalc();
            return obj.ListarAutoridadAmbientalPermisos();
        }

        /// <summary>
        /// Retornar el correo de una autoridad ambiental
        /// </summary>
        /// <param name="intIdAutoridad">int con el identificador de la autoridad</param>
        /// <returns>string con el correo de la autoridad</returns>
        public string ObtenerCorreoAutoridadAmbiental(int intIdAutoridad)
        {
            AutoridadAmbientalIdentity objAutoridad = new AutoridadAmbientalIdentity();
            objAutoridad.IdAutoridad = intIdAutoridad;

            AutoridadAmbientalDalc objDalc = new AutoridadAmbientalDalc();
            objDalc.ObtenerAutoridadById(ref objAutoridad);

            string strEmail = (objAutoridad != null ? (!string.IsNullOrEmpty(objAutoridad.Email) ? objAutoridad.Email : objAutoridad.CorreoCorrespondencia) : "");

            return strEmail;
        }
    }
}
