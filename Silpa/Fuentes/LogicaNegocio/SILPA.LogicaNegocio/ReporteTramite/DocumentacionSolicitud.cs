using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.ReporteTramite;
using System.Data;
using SILPA.LogicaNegocio.Generico;
namespace SILPA.LogicaNegocio.ReporteTramite
{
    public class DocumentacionSolicitud
    {

        public  DocumentacionSolicitudEntity DocumentacionEntity;
        private DocumentacionSolicitudDalc  DocumentacionDalc;
               
        
        /// <summary>
        /// Objeto configuración
        /// </summary>
        private Configuracion _objConfiguracion;

        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentacionSolicitud()
        {
            _objConfiguracion = new Configuracion();
            DocumentacionDalc = new DocumentacionSolicitudDalc();
            DocumentacionEntity = new  DocumentacionSolicitudEntity();

        }


        public DataSet ListarDocumentacionSolicitud(string strNumExpediente)
        {
           DocumentacionSolicitudDalc obj = new DocumentacionSolicitudDalc();
           DataSet dsDatosDocumentacion = new DataSet();
           dsDatosDocumentacion = obj.ListarDocumentacionSolicitud(strNumExpediente);
           return dsDatosDocumentacion;
       }

       public DataSet ListarDocumentacionSolicitudFus(string strNumExpediente)
       {
           DocumentacionSolicitudDalc obj = new DocumentacionSolicitudDalc();
           DataSet dsDatosDocumentacion = new DataSet();
           dsDatosDocumentacion = obj.ListarDocumentacionSolicitudFUS(strNumExpediente);
           return dsDatosDocumentacion;
       }

        public DataSet ListarDocumentacionSolicitudFUSxPerfil(string strNumExpediente, int idUsuario)
       {
           DocumentacionSolicitudDalc obj = new DocumentacionSolicitudDalc();
           DataSet dsDatosDocumentacion = new DataSet();
           dsDatosDocumentacion = obj.ListarDocumentacionSolicitudFUSxPerfil(strNumExpediente, idUsuario);
           return dsDatosDocumentacion;
       }
   }
}
