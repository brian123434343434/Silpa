using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data.SqlClient;

namespace SILPA.AccesoDatos.Generico
{
    public class ConceptoCobroDalc
    {
        private Configuracion objConfiguracion;

         public ConceptoCobroDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public ConceptoCobroDalc(ref ConceptoIdentity objIdentity)
        {
            objConfiguracion = new Configuracion();
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.IDConcepto };
                DbCommand cmd = db.GetStoredProcCommand("GEN_LST_CONCEPTO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                objIdentity.IDConcepto = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["CON_ID"]);
                objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["CON_NOMBRE"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }


        }

        public void ObtenerConcepto(ref ConceptoIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.IDConcepto };
                DbCommand cmd = db.GetStoredProcCommand("GEN_LST_CONCEPTO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                objIdentity.IDConcepto = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["CON_ID"]);
                objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["CON_NOMBRE"]);
               
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Obtener Concepto.";
                throw new Exception(strException, ex);
            }
        }

        public DataSet ListarConceptos(ConceptoIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.IDConcepto  };
                DbCommand cmd = db.GetStoredProcCommand("GEN_LST_CONCEPTO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                return null;
            }
        }

        //public DataSet ListarConceptos(Nullable<decimal> idCobro)
        //{
        //    //  GenericDalc.cm
        //    objConfiguracion = new Configuracion();

        //    try
        //    {
        //        SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

        //        object[] parametros = new object[] { intId, intRegion };

        //        DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_Concepto_COBRO", parametros);
        //        DataSet dsResultado = db.ExecuteDataSet(cmd);
        //        return (dsResultado);

        //    }
        //    catch (SqlException sqle)
        //    {
        //        return null;
        //    }
        //}
    }
}
