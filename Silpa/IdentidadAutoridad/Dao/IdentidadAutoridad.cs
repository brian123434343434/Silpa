using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;  

namespace IdentidadAutoridad.Dao
{
    public class IdentidadAutoridad
    {
        private string _cadena;

        public IdentidadAutoridad(string cadena)
        {
            _cadena = cadena; 
        }

        public Entidades.IdentidadAutoridad Consultar(int id)
        {
            SqlDatabase db = new SqlDatabase(_cadena);
            Entidades.IdentidadAutoridad identidad;  
            DbCommand cmd;
            object[] obj = new object[] { id };
            cmd = db.GetStoredProcCommand(Identidad.CadenaValor("NombreProcedimiento"), obj);
            try
            {
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                identidad = new global::IdentidadAutoridad.Entidades.IdentidadAutoridad(); 
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow r in dsResultado.Tables[0].Rows)
                    {
                        identidad.Id = Convert.ToInt32(r["IDE_ID"]);
                        identidad.IdAA = Convert.ToInt32(r["AUT_ID"]);
                        identidad.Usuario = r["IDE_USUARIO"].ToString();
                        identidad.Password = r["IDE_PASSWORD"].ToString();  
                    }
                }
                return identidad;  
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
                db = null;
            }
        }
    }
}
