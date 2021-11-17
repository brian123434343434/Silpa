using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;

namespace SILPA.AccesoDatos.RegistroMinero
{
    [Serializable]
    public class Localizacion : EntidadSerializable
    {
        public int LocalizacionID { get; set; }
        public string NombreLocalizacion { get; set; }
        public int RegistroMineriaID { get; set; }
        public Geometria EnuGeometria { get; set; }
        public Origen EnuOrigen { get; set; }
        public List<Coordenada> LstCoordenadas { get; set; }
        
        private Configuracion objConfiguracion;

        public Localizacion()
        {
            objConfiguracion = new Configuracion();
            LstCoordenadas = new List<Coordenada>();
        }

        public void EliminarCoordenadas()
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[]
                {
                       LocalizacionID
                };
            DbCommand cmd = db.GetStoredProcCommand("SP_ELIMINAR_COORDENADAS_LOCALIZACION", parametros);
            db.ExecuteNonQuery(cmd);
        }

        public void EliminarLocalizacion()
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[]
                {
                       this.LocalizacionID
                };
            DbCommand cmd = db.GetStoredProcCommand("SP_ELIMINAR_LOCALIZACION_REGISTRO_MINERO", parametros);
            db.ExecuteNonQuery(cmd);
        }

        public void InsertarLocalizacion()
        {
            Database db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            using (DbConnection conx = db.CreateConnection())
            {
                conx.Open();
                DbTransaction tran = conx.BeginTransaction();
                try
                {
                    object[] parametros = new object[]
                    {
                           0,
                           NombreLocalizacion,
                           RegistroMineriaID,
                           (Int32)this.EnuGeometria,
                           (Int32)this.EnuOrigen
                    };
                    DbCommand cmd = db.GetStoredProcCommand("SP_INSERTAR_LOCALIZACION_REGISTRO_MINERO", parametros);
                    db.ExecuteNonQuery(cmd, tran);
                    this.LocalizacionID = Convert.ToInt32(db.GetParameterValue(cmd, "@P_LOC_ID"));

                    foreach (Coordenada coordenada in LstCoordenadas)
                    {
                        object[] paramcoordenada = new object[]
                        {
                              LocalizacionID,
                              coordenada.CoordenadaNorte,
                              coordenada.CoordenadaEste
                        };
                        DbCommand cmdCoordenada = db.GetStoredProcCommand("SP_INSERTAR_COORDENADA_LOCALIZACION", paramcoordenada);
                        db.ExecuteNonQuery(cmdCoordenada, tran);
                    }
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
                finally { conx.Close(); }
            }
        }

        public void ActualizarLocalizacion()
        {
            Database db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            using (DbConnection conx = db.CreateConnection())
            {
                conx.Open();
                DbTransaction tran = conx.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("SP_ACTUALIZAR_LOCALIZACION_REGISTRO_MINERO");
                    db.AddInParameter(cmd, "P_LOC_ID", DbType.Int32, LocalizacionID);
                    db.AddInParameter(cmd, "P_LOC_NOMBRE", DbType.String, NombreLocalizacion);
                    db.AddInParameter(cmd, "P_REGISTRO_MINERO_ID", DbType.Int32, RegistroMineriaID);
                    db.AddInParameter(cmd, "P_GEO_ID", DbType.Int32, (Int32)this.EnuGeometria);
                    db.AddInParameter(cmd, "P_ORI_ID", DbType.Int32, (Int32)this.EnuOrigen);
                    db.ExecuteNonQuery(cmd, tran);
                    
                    EliminarCoordenadas();

                    foreach (Coordenada coordenada in LstCoordenadas)
                    {
                        object[] paramcoordenada = new object[]
                        {
                              LocalizacionID,
                              coordenada.CoordenadaNorte,
                              coordenada.CoordenadaEste
                        };
                        DbCommand cmdCoordenada = db.GetStoredProcCommand("SP_INSERTAR_COORDENADA_LOCALIZACION", paramcoordenada);
                        db.ExecuteNonQuery(cmdCoordenada, tran);
                    }
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
                finally { conx.Close(); }
            }
        }

        public void ConsultarLocalizacion()
        {
            Database db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { this.LocalizacionID };
            DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_LOCALIZACION", parametros);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    LocalizacionID = Convert.ToInt32(reader["LOC_ID"]);
                    NombreLocalizacion = reader["LOC_NOMBRE"].ToString();
                    RegistroMineriaID = Convert.ToInt32(reader["REGISTRO_MINERO_ID"]);
                    EnuGeometria = (Geometria)Convert.ToInt32(reader["GEO_ID"]);
                    EnuOrigen = (Origen)Convert.ToInt32(reader["ORI_ID"]);
                    CargarCoordenadas();
                }
            }
        }
        public void CargarCoordenadas()
        {
            this.LstCoordenadas = new List<Coordenada>();
            Database db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { this.LocalizacionID };
            DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_COORDENADAS_LOCALIZACION", parametros);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Coordenada coordenada = new Coordenada();
                    coordenada.LocalizacionID = this.LocalizacionID;
                    coordenada.CoordenadaEste = Convert.ToDouble(reader["COOR_ESTE"]);
                    coordenada.CoordenadaNorte = Convert.ToDouble(reader["COO_NORTE"]);
                    this.LstCoordenadas.Add(coordenada);
                }
            }
        }

    }
    
}
