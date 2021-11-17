using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;


namespace SILPA.AccesoDatos.Sancionatorio
{
    public class PersonaQuejaDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public PersonaQuejaDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Método que almacena los datos de la relación Persona - Queja en la base de datos
        /// </summary>
        /// <param name="objIdentity">Objeto con los datos de la persona</param>
        public void InsertarPersonaQueja(ref PersonaQuejaIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                    {
                        objIdentity.IdQueja,
                        objIdentity.IdTipoPersona,
                        objIdentity.PrimerNombre,
                        objIdentity.SegundoNombre,
                        objIdentity.PrimerApellido,
                        objIdentity.SegundoApellido,
                        objIdentity.IdTipoIdentificacion,
                        objIdentity.NumeroIdentificacion,
                        objIdentity.IdMunicipioOrigen,
                        objIdentity.Direccion,
                        objIdentity.IdMunicipio,
                        objIdentity.IdCorregimiento,
                        objIdentity.IdVereda,
                        objIdentity.Telefono,
                        objIdentity.CorreoElectronico
                    };
                DbCommand cmd = db.GetStoredProcCommand("SAN_CREAR_PERSONA_QUEJA", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Lista las quejas asociadas al un numero SILPA
        /// </summary>
        /// <param name="strNumSILPA">Numero silpa</param>
        /// <returns>DataSet con los registros y las siguientes columnas: [QUE_ID_QUEJA], [NUMERO_SILPA],[QUE_DESCRIPCION],
        /// [QUE_MUN_ID],[QUE_UBI_ID],[QUE_COR_ID],[QUE_VER_ID],[QUE_AHI_ID],[QUE_ZHI_ID],[QUE_SHI_ID],[QUE_AUT_ID]
        /// ,[QUE_SEC_ID],[QUE_ACTIVO] FROM [SILPA_PRE].[dbo].[SAN_QUEJA]</returns>
        public DataSet ListarQuejasXNumSILPA(string strNumSILPA)
        {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { strNumSILPA };
                DbCommand cmd = db.GetStoredProcCommand("SAN_CONSULTAR_QUEJAS_NUM_SILPA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
        }
    }
}
