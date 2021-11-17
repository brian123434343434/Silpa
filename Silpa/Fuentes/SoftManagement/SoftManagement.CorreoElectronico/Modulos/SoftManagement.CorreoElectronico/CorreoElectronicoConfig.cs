using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace SoftManagement.CorreoElectronico
{
    public static class CorreoElectronicoConfig
    {
        public static string Conexion
        {
            get
            {
                return ConfigurationManager.AppSettings["CORREO_CONEXION"];
            }
        }

        public static string ProcedimientoInsertarCorreo
        {
            get
            {
                return Recursos.ProcedimientoInsertarCorreo;
            }
        }

        public static string ProcedimientoActualizarCorreo
        {
            get
            {
                return Recursos.ProcedimientoActualizarCorreo;
            }
        }

        public static string ProcedimientoConsultarPlantilla
        {
            get
            {
                return Recursos.ProcedimientoConsultarPlantilla;
            }
        }

        public static string ProcedimientoConsultarServidor
        {
            get
            {
                return Recursos.ProcedimientoConsultarServidor;
            }
        }

        public static string ProcedimientoConsultarCorreosAEnviar
        {
            get
            {
                return Recursos.ProcedimientoConsultarCorreosEnviar;
            }
        }

        public static string ProcedimientoInsertarCorreoInhabilitado
        {
            get
            {
                return Recursos.ProcedimientoInsertarCorreoInhabilitado;
            }
        }

        public static string CryptoProvider
        {
            get
            {
                return ConfigurationManager.AppSettings["CRYPTO_PROVIDER"];
            }

        }


        public static int DiasInhabilitar
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["DIAS_INHABILITAR"]); }
        }

        public static string CuentaControl
        {
            get { return ConfigurationManager.AppSettings["CUENTA_CONTROL"].ToString(); }
        }
        public static int PlantillaAcuseEnvio
        {
            get { return Convert.ToInt32( ConfigurationManager.AppSettings["PLANTILLA_ACUSE_ENVIO"]); }
        }
        public static int PlantillaErrorEnvio
        {
            get { return Convert.ToInt32( ConfigurationManager.AppSettings["PLANTILLA_ERROR_ENVIO"]); }
        }

        
    }
}
