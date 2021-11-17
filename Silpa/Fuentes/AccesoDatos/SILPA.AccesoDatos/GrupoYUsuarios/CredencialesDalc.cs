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
    public class CredencialesDalc
    {
        Configuracion objConfiguracion;

        public CredencialesDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public List<CredencialesEntity> ConsultaCredenciales()
        {
            CredencialesEntity objItemLista;
            List<CredencialesEntity> lista = new List<CredencialesEntity>();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("IDE_LISTA_CREDENCIALES");
            try
            {
                IDataReader dr = db.ExecuteReader(cmd);
                while (dr.Read())
                {
                    objItemLista = new CredencialesEntity();
                    
                    Generico.AutoridadAmbientalDalc aut = new AutoridadAmbientalDalc();
                    Generico.AutoridadAmbientalIdentity autE = new AutoridadAmbientalIdentity();
                    autE.IdAutoridad = Convert.ToInt32(dr["AUT_ID"]);
                    aut.ObtenerAutoridadById(ref autE);
                    objItemLista.Autoridad = autE; 

                    objItemLista.Login = dr["CODE"].ToString();
                    objItemLista.Password = dr["PASSWORD"].ToString();
                    
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

        public CredencialesEntity ConsultaCredencial(string login)
        {
            CredencialesEntity objItemLista;
            List<CredencialesEntity> lista = new List<CredencialesEntity>();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] obj = { login };
            DbCommand cmd = db.GetStoredProcCommand("IDE_LISTA_CREDENCIALES", obj);
            try
            {
                IDataReader dr = db.ExecuteReader(cmd);
                objItemLista = new CredencialesEntity();
                while (dr.Read())
                {
                    Generico.AutoridadAmbientalDalc aut = new AutoridadAmbientalDalc();
                    Generico.AutoridadAmbientalIdentity autE = new AutoridadAmbientalIdentity();
                    autE.IdAutoridad = Convert.ToInt32(dr["AUT_ID"]);
                    aut.ObtenerAutoridadById(ref autE);
                    objItemLista.Autoridad = autE;
                    objItemLista.Login = dr["CODE"].ToString();
                    objItemLista.Password = dr["PASSWORD"].ToString();

                    break;
                }
                return objItemLista;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd = null;
                }
            }
        }

        public CredencialesEntity ConsultaCredencial(string login, string ser)
        {
            CredencialesEntity objItemLista;
            List<CredencialesEntity> lista = new List<CredencialesEntity>();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] obj = { login };
            DbCommand cmd = db.GetStoredProcCommand("IDE_LISTA_CREDENCIALES_ENTIDADES", obj);
            try
            {
                IDataReader dr = db.ExecuteReader(cmd);
                objItemLista = new CredencialesEntity();
                while (dr.Read())
                {
                    Generico.AutoridadAmbientalDalc aut = new AutoridadAmbientalDalc();
                    Generico.AutoridadAmbientalIdentity autE = new AutoridadAmbientalIdentity();
                    autE.IdAutoridad = Convert.ToInt32(dr["AUT_ID"]);
                    aut.ObtenerAutoridadById(ref autE);
                    objItemLista.Autoridad = autE;
                    objItemLista.Login = dr["CODE"].ToString();
                    objItemLista.Password = dr["PASSWORD"].ToString();

                    break;
                }
                return objItemLista;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd = null;
                }
            }
        }
    }
}
