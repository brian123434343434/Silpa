using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using SILPA.Comun;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;


namespace SILPA.AccesoDatos.GrupoYUsuarios
{
    public class OpcionDalc
    {
        Configuracion objConfiguracion;

        public OpcionDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public void ConsultarOpcion(ref OpcionEntity opcion)
        {
            List<OpcionEntity> lista = new List<OpcionEntity>();
            lista = ConsultarListaOpcion();
            foreach (OpcionEntity m in lista)
            {
                if (m.Id == opcion.Id)
                {
                    opcion.Opcion = m.Opcion;
                    opcion.Http = m.Http;
                    opcion.Valor = m.Valor;
                    opcion.Activo = m.Activo;  
                    return;
                }
            }
        }

        public List<OpcionEntity> ConsultarListaOpcion()
        {
            OpcionEntity objItemLista;
            List<OpcionEntity> lista = new List<OpcionEntity>();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("LISTAR_OPCION");
            try
            {
                IDataReader dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    objItemLista = new OpcionEntity();
                    objItemLista.Id = Convert.ToInt32(dr["OPC_ID"]);
                    objItemLista.Valor = dr["OPC_VALOR"].ToString(); 
                    objItemLista.Opcion = dr["OPC_OPCION"].ToString();
                    objItemLista.Http = dr["OPC_HTTP"].ToString();
                    objItemLista.Activo = Convert.ToBoolean(dr["OPC_ACTIVO"]);
                    lista.Add(objItemLista);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (cmd != null)
                {
                    cmd = null;
                }
            }
        }

        public DataSet ListarOpcion(string opcionId)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { opcionId };

            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTAR_OPCION", parametros);

            try
            {
                return db.ExecuteDataSet(cmd);
                //return Int32.Parse(cmd.Parameters["ID"].Value.ToString());
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

    }
}
