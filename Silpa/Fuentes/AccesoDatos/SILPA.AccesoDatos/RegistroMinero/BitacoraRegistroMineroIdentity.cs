using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace SILPA.AccesoDatos.RegistroMinero
{
    public class BitacoraRegistroMineroIdentity
    {
        private Configuracion objConfiguracion;

        public string DatoNuevo
        {
            get;
            set;
        }

        public string DatoAnterior
        {
            get;
            set;
        }


        public string Campo
        {
            get;
            set;
        }

        public string Tabla
        {
            get;
            set;
        }

        public string Descripcion
        {
            get;
            set;
        }

        public DateTime? FechaAccion
        {
            get;
            set;
        }

        public int? AutoridadID
        {
            get;
            set;
        }

        public int? UsuarioID
        {
            get;
            set;
        }

        public int RegistroMineroID
        {
            get;
            set;
        }

        public string Accion
        {
            get;
            set;
        }
        public int LogRegsitroID { get; set; }

        public BitacoraRegistroMineroIdentity()
        {
            objConfiguracion = new Configuracion();
        }

        public void Insertar()
        {
            Database db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("SP_INSERTAR_BITACORA_REGISTRO_MINERO");
            db.AddInParameter(cmd, "P_REGISTRO_MINERO_ID", DbType.Int32, RegistroMineroID);
            db.AddInParameter(cmd, "P_ACCION", DbType.String, Accion);
            db.AddInParameter(cmd, "P_USUARIO_ID", DbType.Int32, UsuarioID);
            db.AddInParameter(cmd, "P_AUTORIDAD_ID", DbType.Int32, AutoridadID);
            db.AddInParameter(cmd, "P_DESCRIPCION", DbType.String, Descripcion);
            db.AddInParameter(cmd, "P_TABLA", DbType.String, Tabla);
            db.AddInParameter(cmd, "P_CAMPO", DbType.String, Campo);
            db.AddInParameter(cmd, "P_DATO_ANTERIOR", DbType.String, DatoAnterior);
            db.AddInParameter(cmd, "P_DATO_NUEVO", DbType.String, DatoNuevo);
            db.ExecuteNonQuery(cmd);
        }


    }
}
