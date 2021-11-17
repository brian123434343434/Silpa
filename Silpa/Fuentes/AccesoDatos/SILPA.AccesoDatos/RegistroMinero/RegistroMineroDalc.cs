using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;

namespace SILPA.AccesoDatos.RegistroMinero
{
    public class RegistroMineroDalc
    {
        private Configuracion objConfiguracion;

        public RegistroMineroDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public void InsertarRegistroMinero(ref RegistroMineroIdentity registro)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                // mardila 05/04/2010 guardamos la ocurrencia
                object[] parametros = new object[]
                       {                            
                            0,
                            registro.TipoRegistroMineroID,
                            registro.NroActoAdministrativo,
                            registro.FechaActoAdministrativo,
                            registro.NroExpediente,
                            registro.Vigencia,
                            registro.EstadoID,
                            registro.CodigoTituloMinero,
                            registro.AreaHectareas,
                            registro.NombreMina,
                            registro.Archivo,
                            registro.FechaExpiracion,
                            registro.SectorId,
                            registro.NombreProyecto
                        };

                DbCommand cmd = db.GetStoredProcCommand("SP_CREAR_REGISTRO_MINERO", parametros);
                db.ExecuteNonQuery(cmd);
                string _idRegistroMinero = cmd.Parameters["@P_REGISTROMINERO_ID"].Value.ToString();
                registro.RegistroMineroID = Int32.Parse(_idRegistroMinero);
        }

        public void InsertarOperador(ref RegistroMineroIdentity registro)
        {
            foreach (TitularIdentity titular in registro.LstTitulares)
	        {
        		SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                    {
                           registro.RegistroMineroID,
                           titular.NombreTitular,
                           titular.Nroidentificacion
                    };
                DbCommand cmd = db.GetStoredProcCommand("SP_INSERTAR_TITULAR_REGISTRO_MINERO", parametros);
                db.ExecuteNonQuery(cmd);
	        }           
        }
        public void InsertarMinerales(ref RegistroMineroIdentity registro)
        {
            foreach (MineralIdentity mineral in registro.LstMinerales)
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                    {
                           registro.RegistroMineroID,
                           mineral.MineralID
                    };
                DbCommand cmd = db.GetStoredProcCommand("SP_INSERTAR_MINERAL_REGISTRO_MINERO", parametros);
                db.ExecuteNonQuery(cmd);
            }
        }
        public void InsertarUbicaciones(ref RegistroMineroIdentity registro)
        {
            foreach (UbicacionIdentity ubicacion in registro.LstUbicacion)
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                    {
                           ubicacion.DepartamentoID,
                           ubicacion.MunicipioID,
                           registro.RegistroMineroID
                    };
                DbCommand cmd = db.GetStoredProcCommand("SP_INSERTAR_UBICACION_REGISTRO_MINERO", parametros);
                db.ExecuteNonQuery(cmd);
            }
        }
        public void EliminarMinerales(ref RegistroMineroIdentity registro)
        {
             SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[]
                {
                       registro.RegistroMineroID
                };
            DbCommand cmd = db.GetStoredProcCommand("SP_ELIMINAR_MINERALES_REGISTRO_MINERO", parametros);
            db.ExecuteNonQuery(cmd);
         }
        public void EliminarOperadores(ref RegistroMineroIdentity registro)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[]
                {
                       registro.RegistroMineroID
                };
            DbCommand cmd = db.GetStoredProcCommand("SP_ELIMINAR_TITULARES_REGISTRO_MINERO", parametros);
            db.ExecuteNonQuery(cmd);
        }
        public void EliminarUbicaciones(ref RegistroMineroIdentity registro)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[]
                {
                       registro.RegistroMineroID
                };
            DbCommand cmd = db.GetStoredProcCommand("SP_ELIMINAR_UBICACIONES_REGISTRO_MINERO", parametros);
            db.ExecuteNonQuery(cmd);
        }
        

    }
}
