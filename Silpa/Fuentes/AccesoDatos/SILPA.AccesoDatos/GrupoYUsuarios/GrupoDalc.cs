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
    public class GrupoDalc
    {
        Configuracion objConfiguracion;

        public GrupoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public void ConsultaGrupo(ref GrupoEntity grupo)
        {
            List<GrupoEntity> lista = new List<GrupoEntity>();
            lista = ConsultaListaGrupo();
            foreach (GrupoEntity g in lista)
            {
                if (g.Id == grupo.Id)
                {
                    grupo.Name = g.Name;
                    grupo.Enabled = g.Enabled;
                    grupo.Code = g.Code; 
                    return;
                }
            }
        }

        public List<GrupoEntity> ConsultaListaGrupo()
        {
            GrupoEntity objItemLista;
            List<GrupoEntity> lista = new List<GrupoEntity>();   
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTAR_GRUPO");
            try
            {
                IDataReader dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    objItemLista = new GrupoEntity();
                    objItemLista.Id = Convert.ToInt32(dr["ID"]);
                    objItemLista.Code = dr["CODE"].ToString();
                    objItemLista.Name = dr["NAME"].ToString();
                    if (dr["ENABLED"].ToString() == "T")
                        objItemLista.Enabled = true;
                    else
                        objItemLista.Enabled = false;
                    lista.Add(objItemLista);
                }
                return lista;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd = null;
                }
            }
        }

        public DataSet ConsultarGrupo(string grupoId)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { grupoId };

            DbCommand cmd = db.GetStoredProcCommand("BAS_CONSULTAR_GRUPO", parametros);

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
