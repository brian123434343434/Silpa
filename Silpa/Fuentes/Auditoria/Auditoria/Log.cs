using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;


namespace Auditoria
{

    public class Log
    {
        private string login_usuario = string.Empty;
        private string identificacion_usuario = string.Empty;
        private string nombre_usuario = string.Empty;
        private string autoridad_ambiental = string.Empty;
        private string modulo = string.Empty;
        private int accion_realizada = -1;
        private string detalle_accion_realizada = string.Empty;


        /// <summary>
        /// Login de autenticación del usuario que ingreso al sistema.
        /// </summary>
        public string Login_Usuario
        {
            get { return login_usuario; }
            set { login_usuario = value; }
        }

        /// <summary>
        /// Identificación del usuario que ingreso al sistema.
        /// </summary>
        public string Identificacion_Usuario
        {
            get { return identificacion_usuario; }
            set { identificacion_usuario = value; }
        }

        /// <summary>
        /// Nombre Completo del usuario que ingreso al sistema.
        /// </summary>
        public string Nombre_Usuario
        {
            get { return nombre_usuario; }
            set { nombre_usuario = value; }
        }

        /// <summary>
        /// Nombre de la Autoridad Ambiental.
        /// </summary>
        public string Autoridad_Ambiental
        {
            get { return autoridad_ambiental; }
            set { autoridad_ambiental = value; }
        }

        /// <summary>
        /// Modulo del sistema que genera la auditoria.
        /// </summary>
        public string Modulo
        {
            get { return modulo; }
            set { modulo = value; }
        }

        /// <summary>
        /// Código  de la acción realizada: (1)Almacenar, (2)Consultar, (3)Editar, (4)Eliminar.
        /// </summary>
        public int Accion_Realizada
        {
            get { return accion_realizada; }
            set { accion_realizada = value; }
        }

        /// <summary>
        /// Descripción de la acción realizada.
        /// </summary>
        public string Detalle_Accion_Realizada
        {
            get { return detalle_accion_realizada; }
            set { detalle_accion_realizada = value; }
        }

        /// <summary>
        /// Almacenar auditoria.
        /// </summary>
        public void Almacenar()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("SS_TRA_ADI_AUDITORIA");

                db.AddInParameter(cmd, "@STR_AUD_LOGIN_USUARIO", DbType.String, login_usuario.Trim());
                db.AddInParameter(cmd, "@STR_AUD_IDENTIF_USUARIO", DbType.String, identificacion_usuario.Trim());
                db.AddInParameter(cmd, "@STR_AUD_NOMBRE_USUARIO", DbType.String, nombre_usuario.Trim());
                db.AddInParameter(cmd, "@STR_AUD_AUTORIDAD_AMBIENTAL_RELACIONADA", DbType.String, autoridad_ambiental.Trim());
                db.AddInParameter(cmd, "@STR_AUD_MODULO", DbType.String, modulo.Trim());
                db.AddInParameter(cmd, "@STR_AUD_ACCION_REALIZADA", DbType.String, ValidarAccion(accion_realizada));
                db.AddInParameter(cmd, "@STR_AUD_DETALLE_ACCION_REALIZADA", DbType.String, detalle_accion_realizada.Trim());
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// Busqueda auditoria.
        /// </summary>
        public DataSet Buscar(string strFechaInicial, string strFechaFinal)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("SS_LST__AUDITORIA");

                db.AddInParameter(cmd, "@STR_FECHA_INI", DbType.String, Formatfecha(strFechaInicial.Trim()));
                db.AddInParameter(cmd, "@STR_FECHA_FIN", DbType.String, Formatfecha(strFechaFinal.Trim()));
                db.AddInParameter(cmd, "@STR_AUD_LOGIN_USUARIO", DbType.String, login_usuario.Trim());
                db.AddInParameter(cmd, "@STR_AUD_IDENTIF_USUARIO", DbType.String, identificacion_usuario.Trim());
                db.AddInParameter(cmd, "@STR_AUD_NOMBRE_USUARIO", DbType.String, nombre_usuario.Trim());
                db.AddInParameter(cmd, "@STR_AUD_AUTORIDAD_AMBIENTAL_RELACIONADA", DbType.String, autoridad_ambiental.Trim());
                db.AddInParameter(cmd, "@STR_AUD_MODULO", DbType.String, modulo.Trim());
                db.AddInParameter(cmd, "@STR_AUD_ACCION_REALIZADA", DbType.String, ValidarAccion(accion_realizada));
                db.AddInParameter(cmd, "@STR_AUD_DETALLE_ACCION_REALIZADA", DbType.String, detalle_accion_realizada.Trim());
                return db.ExecuteDataSet(cmd);
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        /// <summary>
        /// Listar los filtros disponibles.
        /// </summary>
        public DataSet ListarFiltrosDisponibles(int iOpcion)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("SS_LST__GRUPO_FILTRO_AUDITORIA");
                db.AddInParameter(cmd, "@OPCION", DbType.Int16, iOpcion);
                return db.ExecuteDataSet(cmd);
            }
            catch (Exception e)
            {
                throw e;
            }

        }       

        private string ValidarAccion(int sDato)
        {
            string strAccion = string.Empty;

            if (sDato == 1)
                strAccion = "ALMACENAR";

            if (sDato == 2)
                strAccion = "CONSULTAR";

            if (sDato == 3)
                strAccion = "EDITAR";

            if (sDato == 4)
                strAccion = "ELIMINAR";

            return strAccion;
        }

        private String Formatfecha(string sFecha)
        {
            if (sFecha.Trim() != "" && sFecha != null)
            {
                DateTime DTfecha = DateTime.Parse(sFecha);
                sFecha = DTfecha.Year.ToString() + (DTfecha.Month.ToString().Length == 1 ? "0" + DTfecha.Month.ToString() : DTfecha.Month.ToString()) + (DTfecha.Day.ToString().Length == 1 ? "0" + DTfecha.Day.ToString() : DTfecha.Day.ToString());
            }
            else
            {
                sFecha = "";
            }

            return sFecha;
        }

    }
}
