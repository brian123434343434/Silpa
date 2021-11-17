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
    public class DetalleCobroDalc
    {

        private Configuracion objConfiguracion;

        public DetalleCobroDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public DetalleCobroDalc(ref DetalleCobroIdentity objIdentity, decimal? idCobro)
        {
            objConfiguracion = new Configuracion();
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.IDDetalleCobro, objIdentity.Descripcion, idCobro };
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_DETALLE_COBRO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                objIdentity.IDDetalleCobro = Convert.ToDecimal(dsResultado.Tables[0].Rows[0]["CCO_ID"]);
                objIdentity.Descripcion = Convert.ToString(dsResultado.Tables[0].Rows[0]["CCO_DESCRIPCION"]);
                objIdentity.Valor = Convert.ToDecimal(dsResultado.Tables[0].Rows[0]["CCO_VALOR"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }


        }

        public void ObtenerDetalle(ref DetalleCobroIdentity objIdentity, decimal? idCobro)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.IDDetalleCobro, objIdentity.Descripcion, idCobro };
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_DETALLE_COBRO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                objIdentity.IDDetalleCobro = Convert.ToDecimal(dsResultado.Tables[0].Rows[0]["CCO_ID"]);
                objIdentity.Descripcion = Convert.ToString(dsResultado.Tables[0].Rows[0]["CCO_DESCRIPCION"]);
                objIdentity.Valor = Convert.ToDecimal(dsResultado.Tables[0].Rows[0]["CCO_VALOR"]);
               
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        public DataSet ListarDetalles(DetalleCobroIdentity objIdentity, decimal? idCobro)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.IDDetalleCobro, objIdentity.Descripcion, idCobro};
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_DETALLE_COBRO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                return null;
            }
        }

        /// <summary>
        /// Lista los detalles pertenecientes a un cobro
        /// </summary>
        /// <param name="idCobro">ID del Cobro</param>
        /// <returns></returns>
        public DataSet DSListarDetalles(decimal? idCobro)
        {
            //  GenericDalc.cm
            objConfiguracion = new Configuracion();

            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { null, null, idCobro};

                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_DETALLE_COBRO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                return null;
            }
        }

        /// <summary>
        /// Retorna los detalles de un cobro
        /// </summary>
        /// <param name="idCobro">id del Cobro</param>
        /// <returns></returns>
        public List<DetalleCobroIdentity> ListarDetalles(decimal? idCobro)
        {
            //  GenericDalc.cm
            objConfiguracion = new Configuracion();

            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { null, null,idCobro };

                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_DETALLE_COBRO", parametros);
                DataSet ds = db.ExecuteDataSet(cmd);
                List<DetalleCobroIdentity> _listaDetalles = new List<DetalleCobroIdentity>();
                DetalleCobroIdentity _detalleCobro = new DetalleCobroIdentity();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        _detalleCobro=new DetalleCobroIdentity();
                        _detalleCobro.IDDetalleCobro = Convert.ToDecimal(dr["CCO_ID"]);
                        _detalleCobro.Descripcion = Convert.ToString(dr["CCO_DESCRIPCION"]);
                        _detalleCobro.Valor=Convert.ToDecimal(dr["CCO_VALOR"]);
                        _listaDetalles.Add(_detalleCobro);
                        _detalleCobro=null;
                    }
                }
                return _listaDetalles;

            }
            catch (SqlException sqle)
            {
                return null;
            }
        }

        public void InsertarDetalle(ref DetalleCobroIdentity objIdentity, decimal idCobro)
        {
            DbCommand objCommand = null;

            try
            {
                SqlDatabase objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("GEN_INSERT_DETALLE_COBRO");
                objDataBase.AddInParameter(objCommand, "@P_CCO_DESCRIPCION", DbType.String, objIdentity.Descripcion);
                objDataBase.AddInParameter(objCommand, "@P_CCO_VALOR", DbType.Decimal, objIdentity.Valor);
                objDataBase.AddInParameter(objCommand, "@P_COB_ID", DbType.Decimal, idCobro);
                objDataBase.ExecuteNonQuery(objCommand);

            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Insertar Detalle.";
                throw new Exception(strException, ex);
            }
        }


    }
}
