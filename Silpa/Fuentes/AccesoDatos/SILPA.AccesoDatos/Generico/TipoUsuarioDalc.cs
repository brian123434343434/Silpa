using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using SILPA.Comun;
using SoftManagement.Log;

namespace SILPA.AccesoDatos.Generico
{
    public class TipoUsuarioDalc
    {
        private Configuracion objConfiguracion;
        public TipoUsuarioDalc() { objConfiguracion = new Configuracion(); }

        public List<TipoUsuarioIdentity> ObtenerUsuarios()
        {
            //Persona de Prueba mientras se implementa el método
            TipoUsuarioIdentity objIdentity = new TipoUsuarioIdentity();
            List<TipoUsuarioIdentity> _listaUsuarios = new List<TipoUsuarioIdentity>();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_TIPO_USUARIOS");
            DataSet dsResultado = db.ExecuteDataSet(cmd);            

            foreach (DataRow drResultado in dsResultado.Tables[0].Rows)            
            {
                objIdentity = new TipoUsuarioIdentity();
                objIdentity.IdTipoUsuario = Convert.ToInt32(drResultado["ID_TIPO_USUARIO"]);
                objIdentity.TipoUsuario = Convert.ToString(drResultado["DESCRIPCION_TIPO_USUARIO"]);
                objIdentity.WorkFlowId = Convert.ToInt32(drResultado["WORK_FLOW_ROLL"]);

                _listaUsuarios.Add(objIdentity);
            }

            return _listaUsuarios;
        }
        public void ActivarUsuario(TipoUsuarioIdentity indentity)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { indentity.IdPersona, indentity.IdTipoUsuario, 10 };
            DbCommand cmd = db.GetStoredProcCommand("GEN_ACTIVAR_PERSONA",parametros); 
            int i = db.ExecuteNonQuery(cmd);
            int result  = (int)db.GetParameterValue(cmd, "P_RESP");
            if (result!=0)
            {     
                Severidad sev = new Severidad();                
                SMLog.Escribir(Severidad.Informativo, "Error activando persona Error No. --- " + result.ToString() + "----");                
            }
        }

    }
}
