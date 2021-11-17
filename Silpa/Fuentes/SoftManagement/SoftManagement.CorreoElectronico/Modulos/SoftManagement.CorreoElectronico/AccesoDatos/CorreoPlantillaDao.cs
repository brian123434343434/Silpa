using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace SoftManagement.CorreoElectronico.AccesoDatos
{
    public static class CorreoPlantillaDao
    {
        public static Entidades.CorreoPlantilla  ConsultarPlantillaCorreo(int plantillaCorreoId)
        {
            try
            {
	            Database db;
	            DbCommand command;
	            Entidades.CorreoPlantilla correoPlantilla = null;
	            if (String.IsNullOrEmpty(CorreoElectronicoConfig.Conexion))
	                db = DatabaseFactory.CreateDatabase();
	            else
	                db = DatabaseFactory.CreateDatabase(CorreoElectronicoConfig.Conexion);
	
	            command = db.GetStoredProcCommand(CorreoElectronicoConfig.ProcedimientoConsultarPlantilla);
	            db.AddInParameter(command, "P_CORREO_PLANTILLA_ID", DbType.Int32, plantillaCorreoId);
	            using (IDataReader reader = db.ExecuteReader(command))
	            {
	                if (reader.Read())
	                {
	                    correoPlantilla = new Entidades.CorreoPlantilla(plantillaCorreoId, reader["DE"].ToString(), reader["CC"].ToString(), reader["CCO"].ToString(), reader["PLANTILLA"].ToString(), reader["ASUNTO"].ToString(), Convert.ToInt32(reader["CORREO_SERVIDOR_ID"]));
	                }
	            }
	            return correoPlantilla;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Consultar Plantilla de Correo.";
                throw new Exception(strException, ex);
            }
        }
    }
}
