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
    public class RegistroMineroIdentity : EntidadSerializable
    {
        private Configuracion objConfiguracion;

        public RegistroMineroIdentity()
        {
            objConfiguracion = new Configuracion();
        }
        public RegistroMineroIdentity(int registroMineroID)
        {
            this.RegistroMineroID = registroMineroID;
            objConfiguracion = new Configuracion();
        }

        public int RegistroMineroID { get; set; }
        public int TipoRegistroMineroID { get; set; }
        public string NombreTipoRegistroMinero { get; set; }
        public string NroActoAdministrativo { get; set; }
        public DateTime? FechaActoAdministrativo { get; set; }
        public string NroExpediente { get; set; }
        public string Vigencia { get; set; }
        public DateTime? FechaVigencia { get; set; }
        public int EstadoID { get; set; }
        public string NombreEstado { get; set; }
        public string CodigoTituloMinero { get; set; }
        public List<MineralIdentity> LstMinerales { get; set; }
        public decimal? AreaHectareas { get; set; }
        public string NombreMina { get; set; }
        public List<UbicacionIdentity> LstUbicacion { get; set; }
        public string Archivo { get; set; }
        public List<Localizacion> LstLocalizaciones { get; set; }
        public int SectorId { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public string NombreProyecto { get; set; }
        public int AutoridadAmbiental { get; set; }
        public string NombreAutoridadAmbiental { get; set; }
        public List<TitularIdentity> LstTitulares { get; set; }
        public string Observaciones { get; set; }


        public void Insertar()
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
                            TipoRegistroMineroID,
                            NroActoAdministrativo,
                            FechaActoAdministrativo,
                            NroExpediente,
                            Vigencia,
                            FechaVigencia,
                            EstadoID,
                            CodigoTituloMinero,
                            AreaHectareas,
                            NombreMina,
                            Archivo,
                            FechaExpiracion,
                            SectorId,
                            NombreProyecto,
                            AutoridadAmbiental,
                            Observaciones
                        };

                    DbCommand cmd = db.GetStoredProcCommand("SP_CREAR_REGISTRO_MINERO", parametros);
                    db.ExecuteNonQuery(cmd, tran);
                    string _idRegistroMinero = cmd.Parameters["@P_REGISTROMINERO_ID"].Value.ToString();
                    RegistroMineroID = Int32.Parse(_idRegistroMinero);

                    object[] paramElimin = new object[]
                {
                       RegistroMineroID
                };
                    DbCommand cmdEliMin = db.GetStoredProcCommand("SP_ELIMINAR_MINERALES_REGISTRO_MINERO", paramElimin);
                    db.ExecuteNonQuery(cmdEliMin, tran);

                    object[] paramEliTitular = new object[]
                {
                       RegistroMineroID
                };
                    DbCommand cmdEliTitular = db.GetStoredProcCommand("SP_ELIMINAR_TITULARES_REGISTRO_MINERO", paramEliTitular);
                    db.ExecuteNonQuery(cmdEliTitular, tran);

                    object[] paramEliUbicacion = new object[]
                {
                       RegistroMineroID
                };
                    DbCommand cmdEliUbica = db.GetStoredProcCommand("SP_ELIMINAR_UBICACIONES_REGISTRO_MINERO", paramEliUbicacion);
                    db.ExecuteNonQuery(cmdEliUbica, tran);

                    foreach (TitularIdentity titular in LstTitulares)
                    {
                        object[] parTitular = new object[]
                    {
                           RegistroMineroID,
                           titular.NombreTitular,
                           titular.Nroidentificacion
                    };
                        DbCommand cmdTitular = db.GetStoredProcCommand("SP_INSERTAR_TITULAR_REGISTRO_MINERO", parTitular);
                        db.ExecuteNonQuery(cmdTitular, tran);
                    }
                    foreach (MineralIdentity mineral in LstMinerales)
                    {
                        DbCommand cmdMineral = db.GetStoredProcCommand("SP_INSERTAR_MINERAL_REGISTRO_MINERO");
                        db.AddInParameter(cmdMineral, "P_REGISTRO_MINERO_ID", DbType.Int32, RegistroMineroID);
                        db.AddInParameter(cmdMineral, "P_MINERAL_ID", DbType.Int32, mineral.MineralID);
                        db.ExecuteNonQuery(cmdMineral, tran);
                    }
                    foreach (UbicacionIdentity ubicacion in LstUbicacion)
                    {
                        DbCommand cmdUbicacion = db.GetStoredProcCommand("SP_INSERTAR_UBICACION_REGISTRO_MINERO");
                        db.AddInParameter(cmdUbicacion, "P_DEPARTAMENTO_ID", DbType.Int32, ubicacion.DepartamentoID);
                        db.AddInParameter(cmdUbicacion, "P_MUNICIPIO_ID", DbType.Int32, ubicacion.MunicipioID);
                        db.AddInParameter(cmdUbicacion, "P_REGISTRO_MINERO_ID", DbType.Int32, RegistroMineroID);
                        db.ExecuteNonQuery(cmdUbicacion, tran);
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally { conx.Close(); }
            }
        }

        public void Actualizar()
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
                            RegistroMineroID,
                            TipoRegistroMineroID,
                            NroActoAdministrativo,
                            FechaActoAdministrativo,
                            NroExpediente,
                            Vigencia,
                            FechaVigencia,
                            EstadoID,
                            CodigoTituloMinero,
                            AreaHectareas,
                            NombreMina,
                            Archivo,
                            FechaExpiracion,
                            SectorId,
                            NombreProyecto,
                            AutoridadAmbiental,
                            Observaciones
                        };

                    DbCommand cmd = db.GetStoredProcCommand("SP_ACTUALIZAR_REGISTRO_MINERO", parametros);
                    db.ExecuteNonQuery(cmd, tran);
                    object[] paramElimin = new object[]
                    {
                           RegistroMineroID
                    };
                    DbCommand cmdEliMin = db.GetStoredProcCommand("SP_ELIMINAR_MINERALES_REGISTRO_MINERO", paramElimin);
                    db.ExecuteNonQuery(cmdEliMin, tran);

                    object[] paramEliTitular = new object[]
                    {
                           RegistroMineroID
                    };
                    DbCommand cmdEliTitular = db.GetStoredProcCommand("SP_ELIMINAR_TITULARES_REGISTRO_MINERO", paramEliTitular);
                    db.ExecuteNonQuery(cmdEliTitular, tran);

                    object[] paramEliUbicacion = new object[]
                    {
                           RegistroMineroID
                    };
                    DbCommand cmdEliUbica = db.GetStoredProcCommand("SP_ELIMINAR_UBICACIONES_REGISTRO_MINERO", paramEliUbicacion);
                    db.ExecuteNonQuery(cmdEliUbica, tran);

                    foreach (TitularIdentity titular in LstTitulares)
                    {
                        object[] parTitular = new object[]
                    {
                           RegistroMineroID,
                           titular.NombreTitular,
                           titular.Nroidentificacion
                    };
                        DbCommand cmdTitular = db.GetStoredProcCommand("SP_INSERTAR_TITULAR_REGISTRO_MINERO", parTitular);
                        db.ExecuteNonQuery(cmdTitular, tran);
                    }
                    foreach (MineralIdentity mineral in LstMinerales)
                    {
                        DbCommand cmdMineral = db.GetStoredProcCommand("SP_INSERTAR_MINERAL_REGISTRO_MINERO");
                        db.AddInParameter(cmdMineral, "P_REGISTRO_MINERO_ID", DbType.Int32, RegistroMineroID);
                        db.AddInParameter(cmdMineral, "P_MINERAL_ID", DbType.Int32, mineral.MineralID);
                        db.ExecuteNonQuery(cmdMineral, tran);
                    }
                    foreach (UbicacionIdentity ubicacion in LstUbicacion)
                    {
                        DbCommand cmdUbicacion = db.GetStoredProcCommand("SP_INSERTAR_UBICACION_REGISTRO_MINERO");
                        db.AddInParameter(cmdUbicacion, "P_DEPARTAMENTO_ID", DbType.Int32, ubicacion.DepartamentoID);
                        db.AddInParameter(cmdUbicacion, "P_MUNICIPIO_ID", DbType.Int32, ubicacion.MunicipioID);
                        db.AddInParameter(cmdUbicacion, "P_REGISTRO_MINERO_ID", DbType.Int32, RegistroMineroID);
                        db.ExecuteNonQuery(cmdUbicacion, tran);
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally { conx.Close(); }
            }
        }

        public void Consultar(bool ConsultarTodo)
        {
            Database db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { this.RegistroMineroID };
            DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_REGISTRO_MINERO_POR_ID", parametros);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    this.TipoRegistroMineroID = Convert.ToInt32(reader["TIPO_REGISTRO_MINERO_ID"]);
                    this.NombreTipoRegistroMinero = reader["NOMBRE_TIPO_REGISTRO_MINERO"].ToString();
                    this.NroActoAdministrativo = reader["NRO_ACTOADMINISTRATIVO"].ToString();
                    this.FechaActoAdministrativo = Convert.ToDateTime(reader["FECHA_ACTOADMINISTRATIVO"]);
                    this.NroExpediente = reader["NRO_EXPEDIENTE"].ToString();
                    this.Vigencia = reader["VIGENCIA"].ToString();

                    if (reader["FECHA_VIGENCIA"].ToString() != "")
                    {
                        this.FechaVigencia = Convert.ToDateTime(reader["FECHA_VIGENCIA"]);
                    }

                    this.EstadoID = Convert.ToInt32(reader["ESTADO_ID"]);
                    this.NombreEstado = reader["NOMBRE_ESTADO"].ToString();
                    this.CodigoTituloMinero = reader["CODIGO_TITULOMINERO"].ToString();
                    this.AreaHectareas = Convert.ToDecimal(reader["AREA_HECTAREAS"]);
                    this.NombreMina = reader["NOMBRE_MINA"].ToString();
                    this.Archivo = reader["ARCHIVO"].ToString();
                    this.FechaExpiracion = Convert.ToDateTime(reader["FECHA_EXPEDICION"]);
                    this.SectorId = Convert.ToInt32(reader["SECTOR_ID"]);
                    this.NombreProyecto = reader["NOMBRE_PROYECTO"].ToString();
                    this.AutoridadAmbiental = Convert.ToInt32(reader["AUTORIDAD_AMB_ID"]);
                    this.NombreAutoridadAmbiental = reader["NOMBRE_AUTORIDAD"].ToString();
                    this.Observaciones = reader["OBSERVACIONES"].ToString();
                }
            }
            if (ConsultarTodo)
            {
                ConsultaUbicaciones();
                ConsultaTitulares();
                ConsultaMinerales();
                ConsultaLocalizaciones();
            }
        }
        public void ConsultaUbicaciones()
        {
            List<UbicacionIdentity> lstUbicaciones = new List<UbicacionIdentity>();
            Database db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { this.RegistroMineroID };
            DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_UBICACIONES_REGISTRO_MINERO", parametros);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    UbicacionIdentity ubicacion = new UbicacionIdentity();
                    ubicacion.DepartamentoID = Convert.ToInt32(reader["DEPARTAMENTO_ID"]);
                    ubicacion.NombreDepartamento = reader["NOMBRE_DEPARTAMENTO"].ToString();
                    ubicacion.MunicipioID = Convert.ToInt32(reader["MUNICIPIO_ID"]);
                    ubicacion.NombreMunicipio = reader["NOMBRE_MUNICIPIO"].ToString();
                    lstUbicaciones.Add(ubicacion);
                }
            }
            this.LstUbicacion = lstUbicaciones;
        }
        public void ConsultaTitulares()
        {
            List<TitularIdentity> lstTitulares = new List<TitularIdentity>();
            Database db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { this.RegistroMineroID };
            DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_TITULARES_REGISTRO_MINERO", parametros);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    TitularIdentity titular = new TitularIdentity();
                    titular.NombreTitular = reader["NOMBRE_TITULAR"].ToString();
                    titular.Nroidentificacion = reader["INDETIFICACION_TITULAR"].ToString();
                    titular.RegistroMineriaID = this.RegistroMineroID;
                    titular.TitularID = Convert.ToInt32(reader["TITULAR_REGISTRO_MINERO_ID"]);
                    lstTitulares.Add(titular);
                }
            }
            this.LstTitulares = lstTitulares;
        }

        public void ConsultaMinerales()
        {
            List<MineralIdentity> lstMinerales = new List<MineralIdentity>();
            Database db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { this.RegistroMineroID };
            DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_MINERALES_REGISTRO_MINERO", parametros);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    MineralIdentity mineral = new MineralIdentity();
                    mineral.MineralID = Convert.ToInt32(reader["MINERAL_ID"]);
                    mineral.NombreMineral = reader["NOMBRE_MINERAL"].ToString();
                    lstMinerales.Add(mineral);
                }
            }
            this.LstMinerales = lstMinerales;
        }

        public void ConsultaLocalizaciones()
        {
            LstLocalizaciones = new List<Localizacion>();
            Database db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { this.RegistroMineroID };
            DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_LOCALIZACIONES_REGISTRO_MINERO", parametros);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Localizacion localizacion = new Localizacion();
                    localizacion.LocalizacionID = Convert.ToInt32(reader["LOC_ID"]);
                    localizacion.NombreLocalizacion = reader["LOC_NOMBRE"].ToString();
                    localizacion.RegistroMineriaID = this.RegistroMineroID;
                    localizacion.EnuGeometria = (Geometria)Convert.ToInt32(reader["GEO_ID"]);
                    localizacion.EnuOrigen = (Origen)Convert.ToInt32(reader["ORI_ID"]);
                    localizacion.CargarCoordenadas();
                    LstLocalizaciones.Add(localizacion);
                }
            }
        }


        public DataTable ConsultaRegistroMineria(int? tipoRegistroID, int? autoridadAmbientalID, string titular, string nroIdentificacion,
            string codTituloMinero, string nombreProyecto, string nombreMina, int? departamentoID, int? municipioID)
        {
            Database db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_REGISTRO_MINERO_POR_FILTRO");
            db.AddInParameter(cmd, "P_TIPO_REGISTRO_MINERO_ID", DbType.Int32, tipoRegistroID);
            db.AddInParameter(cmd, "P_AUTORIDAD_AMB_ID", DbType.Int32, autoridadAmbientalID);
            db.AddInParameter(cmd, "P_NOMBRE_TITULAR", DbType.String, titular);
            db.AddInParameter(cmd, "P_IDENTIFICACION_TITULAR", DbType.String, nroIdentificacion);
            db.AddInParameter(cmd, "P_CODIGO_TITULOMINERO", DbType.String, codTituloMinero);
            db.AddInParameter(cmd, "P_NOMBRE_PROYECTO", DbType.String, nombreProyecto);
            db.AddInParameter(cmd, "P_NOMBRE_MINA", DbType.String, nombreMina);
            db.AddInParameter(cmd, "P_DEPARTAMENTO_ID", DbType.Int32, departamentoID);
            db.AddInParameter(cmd, "P_MUNICIPIO_ID", DbType.Int32, municipioID);
            return db.ExecuteDataSet(cmd).Tables[0];
        }

        



    }
}
