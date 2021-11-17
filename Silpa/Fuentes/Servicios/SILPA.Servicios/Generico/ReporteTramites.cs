using System;
using System.Collections.Generic;
using System.Text;
using SILPA.LogicaNegocio.ReporteTramite;
using SILPA.AccesoDatos.ReporteTramite;

namespace SILPA.Servicios.Generico
{
    public class ReporteTramites
    {
        public ReporteTramite objRepTra= new ReporteTramite();

        public bool CrearMisTramites(string strParametrosXml)
        {
            try
            {
	            MisTramitesIndentity ObjMisTramites = new MisTramitesIndentity();
	            ObjMisTramites = (MisTramitesIndentity)ObjMisTramites.Deserializar(strParametrosXml);
	            return objRepTra.CrearMisTramites(ObjMisTramites.NumeroVital, ObjMisTramites.FechaCreacion, ObjMisTramites.Descripcion, ObjMisTramites.PathDocumento, ObjMisTramites.IdExpediente, ObjMisTramites.EtaNombre, ObjMisTramites.ActReposicion, ObjMisTramites.AddNombre, ObjMisTramites.ActoEjec, ObjMisTramites.ActoNot,ObjMisTramites.TipoDocumento,ObjMisTramites.DescripcionActo);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Crear Trámites.";
                throw new Exception(strException, ex);
            }
        }


        /// <summary>
        /// Modifica los datos relacionados a un expediente de un tramite
        /// </summary>
        /// <param name="strParametrosXml">string con la informacion anterior del tramite</param>
        /// <param name="strTramiteNuevo">string con la informacion del nuevo tramite</param>
        /// <returns>bool indicando si se realizo</returns>
        public bool ModificarDatosExpedienteTramite(string strTramiteAnterior, string strTramiteNuevo)
        {
            try
            {
                MisTramitesIndentity ObjMisTramiteAnterior = new MisTramitesIndentity();
                MisTramitesIndentity ObjMisTramiteNuevo = new MisTramitesIndentity();
                ObjMisTramiteAnterior = (MisTramitesIndentity)ObjMisTramiteAnterior.Deserializar(strTramiteAnterior);
                ObjMisTramiteNuevo = (MisTramitesIndentity)ObjMisTramiteNuevo.Deserializar(strTramiteNuevo);

                //Ajustar el expediente
                return objRepTra.ModificarDatosExpedienteTramite(ObjMisTramiteAnterior.IdExpediente, ObjMisTramiteNuevo.IdExpediente, ObjMisTramiteNuevo.ActoNot, ObjMisTramiteNuevo.TipoDocumento, ObjMisTramiteNuevo.Descripcion, ObjMisTramiteNuevo.EtaNombre, ObjMisTramiteNuevo.AddNombre, ObjMisTramiteNuevo.DescripcionActo);
                
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Crear Trámites.";
                throw new Exception(strException, ex);
            }
        }
    }
}
