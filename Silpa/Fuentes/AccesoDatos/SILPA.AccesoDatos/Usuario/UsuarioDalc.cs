using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace SILPA.AccesoDatos.Usuario
{
   public class UsuarioDalc
    {
       private Configuracion objConfiguracion;

       public UsuarioDalc() { objConfiguracion = new Configuracion(); }

       public void ActualizarAprobacionUsuario(CredencialEntity objEntity)
       {
           objConfiguracion = new Configuracion();

           int rechazado = 0;

           //23-jun-2010 - aegb
           if (objEntity.motivoRechazo != string.Empty) { rechazado = 2; }

           SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
           object[] parametros = new object[] { long.Parse(objEntity.personaId), rechazado};
           DbCommand cmd = db.GetStoredProcCommand("BAS_ACTUALIZAR_APROBACION_USUARIO", parametros);
           db.ExecuteNonQuery(cmd);
       }

       /// <summary>
       /// 12-jul-2010 - aegb
       /// Consulta la compañia (entidad) a la que pertenece el usuario en security
       /// </summary>
       /// <param name="usuario"></param>
       public DataSet ConsultarUsuarioCompania(string usuario)
       {
           objConfiguracion = new Configuracion();
          
           SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
           object[] parametros = new object[] { usuario };
           DbCommand cmd = db.GetStoredProcCommand("BAS_CONSULTAR_USUARIO_AUTORIDAD", parametros);
           db.ExecuteNonQuery(cmd);
           return db.ExecuteDataSet(cmd);
       }

       /// <summary>
       /// 27-oct-2010 - hava
       /// 
       /// </summary>
       /// <param name="usuario"></param>
       public DataSet ConsultarUsuarioByIdUserApp(long idAppUser)
       {
           objConfiguracion = new Configuracion();

           SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
           object[] parametros = new object[] { idAppUser };
           DbCommand cmd = db.GetStoredProcCommand("BAS_CONSULTAR_USUARIO_POR_IDAPPUSUARIO", parametros);
           //db.ExecuteNonQuery(cmd);
           return db.ExecuteDataSet(cmd);
       }


       /// <summary>
       /// 11-oct-2010 - aegb
       /// Consulta la compañia (entidad) a la que pertenece el usuario del sistema en security
       /// </summary>
       /// <param name="usuario"></param>
       public DataSet ConsultarUsuarioSistemaCompania(string usuario)
       {
           objConfiguracion = new Configuracion();

           SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
           object[] parametros = new object[] { usuario };
           DbCommand cmd = db.GetStoredProcCommand("BAS_CONSULTAR_USUARIO_SISTEMA_AUTORIDAD", parametros);
           db.ExecuteNonQuery(cmd);
           return db.ExecuteDataSet(cmd);
       }

       public string ConsultarUsuarioVisitanteNo(string ip)
       {
           objConfiguracion = new Configuracion();
           SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
           object[] parametros = new object[] { ip };
           DbCommand cmd = db.GetStoredProcCommand("BAS_CONSULTAR_USUARIO_VISITANTE_NUMERO",parametros);
           return db.ExecuteScalar(cmd).ToString();
       }
       public void VolverAGenerarContrasenasEncriptadas(string newLlave)
       {
           List<UsuarioIdentity> lstusuarios = new List<UsuarioIdentity>();
           objConfiguracion = new Configuracion();
           SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
           DbCommand cmd = db.GetStoredProcCommand("BAS_CONSULTA_ALL_USUARIOS");
           using (IDataReader reader = db.ExecuteReader(cmd))
           {
               while (reader.Read())
               {
                   UsuarioIdentity usuario = new UsuarioIdentity();
                   usuario.Id = int.Parse(reader["ID"].ToString());
                   usuario.Cod = reader["Code"].ToString();
                   usuario.Password = reader["Password"].ToString();
                   lstusuarios.Add(usuario);
               }
           }
           if (lstusuarios.Count > 0)
           {
               foreach (UsuarioIdentity usuario in lstusuarios)
               {
                   string passwordDesen = "";
                   string passNewEncript = "";
                   passwordDesen = EnDecript.Desencriptar(usuario.Password);
                   //passNewEncript = EnDecript.Encriptar(passwordDesen.ToString(), newLlave.ToString());
                   CambiarPasswordEncript(usuario, passNewEncript);

                   
               }
           }
       }
       public void CambiarPasswordEncript(UsuarioIdentity usuario, string newPassword)
       {
           objConfiguracion = new Configuracion();
           SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
           object[] parametros = new object[] { newPassword, usuario.Id };
           DbCommand cmd = db.GetStoredProcCommand("BAS_UPDATE_PASSWORD_USUARIO", parametros);
           db.ExecuteNonQuery(cmd);

       }
    }
}
