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

namespace SILPA.AccesoDatos.RUIA
{
    public class RelacionSancionDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public RelacionSancionDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Metodo que inserta las opciones de una sancion
        /// </summary>
        /// <param name="objIdentity">Objeto con los datos de las opciones de la sancion aplicada</param>
        public void InsertarOpciones(ref RelacionSancionIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                // mardila 05/04/2010 se agrega la sanción aplicada
                object[] parametros = new object[]
                       {
                            objIdentity.IdSancion,objIdentity.IdTipoSancion,objIdentity.IdOpcionSancion, objIdentity.SancionAplicada
                        };

                DbCommand cmd = db.GetStoredProcCommand("RUH_CREAR_OPCION_SANCION", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Insertar Opciones de una Sanción.";
                throw new Exception(strException, ex);
            }
        }

        /// <summary>
        /// Método que retorna las opciones de una sanción aplicada
        /// </summary>
        /// <param name="_idSancion">Identificador de la Sanción</param>
        /// <returns>Conjunto de Datos: [RSA_ID_SANCION] - [TIS_NOMBRE] - [RSA_ID_TIPO_SANCION]
        ///  - [OPS_NOMBRE_OPCION]</returns>
        public DataSet ListaOpcionesSancion(Nullable<Int64> _idSancion)
        {           
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { _idSancion };
                DbCommand cmd = db.GetStoredProcCommand("RUH_LISTA_OPCIONES_SANCION", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);            
        }

        /// <summary>
        /// AEGB
        /// procedimiento que elimina todas las opciones de una sancion
        /// </summary>
        /// <param name="_idSancion"></param>
        public void EliminarOpciones(long _idSancion)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            // mardila 05/04/2010 se agrega la sanción aplicada
            object[] parametros = new object[]
                       {
                           _idSancion
                        };

            DbCommand cmd = db.GetStoredProcCommand("RUH_ELIMINAR_RELACION_SANCION", parametros);
            db.ExecuteNonQuery(cmd);

        }
    }
}
