using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos.RecursoIdentity;

namespace SILPA.AccesoDatos.Salvoconducto
{
    public class EspecimenDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public EspecimenDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public void InsertarEspecimen(ref EspecimenIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                        {                    
                            objIdentity.idSalvoconducto,
                            objIdentity.IdentificacionEspecimen, objIdentity.NombreCientifico,
                            objIdentity.NombreComun, objIdentity.DescripcionEspecimen, 
                            objIdentity.CantidadEspecimen, objIdentity.UnidadMedidaId,
                            objIdentity.DimensionesEspecimen,objIdentity.RecursoId
                        };

                DbCommand cmd = db.GetStoredProcCommand("SUN_CREAR_ESPECIMEN", parametros);
                db.ExecuteNonQuery(cmd);                
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        public void InsertarTransporte(ref TransporteIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                        {                    
                            objIdentity.idSalvoconducto,
                            objIdentity.ModoTransporte, objIdentity.NombreEmpresa,
                            objIdentity.TipoVehiculo, objIdentity.Matricula, 
                            objIdentity.TipoIdentificacionRes, objIdentity.NumeroIdentificacion,
                            objIdentity.NombreResponsable
                        };

                DbCommand cmd = db.GetStoredProcCommand("SUN_CREAR_TRANSPORTE", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        public void InsertarRecurso(string codigoExpediente, string numeroVital, ref RecursosIdentity rec)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                        {  
                            codigoExpediente,
                            numeroVital,
                            rec.RecursoId,
                            rec.TipoRecurso,
                            rec.NombreRecurso,
                            rec.DescripcionRecurso,
                            rec.CantidadRecurso, 
                            rec.AreaHidrograficaId,
                            rec.ZonaHidrograficaId,
                            rec.SubZonaHidrograficaId,
                            rec.NombreCientifico
                        };

                DbCommand cmd = db.GetStoredProcCommand("SUN_CREAR_RECURSO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
    }
}
