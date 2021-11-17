using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Configuration;
using SILPA.AccesoDatos.DAA;
using SILPA.LogicaNegocio.Generico;
using SoftManagement.Log;

namespace SILPA.LogicaNegocio.GenerarSalvoConducto
{
    public class SalvoConducto
    {
        /// <summary>
        /// objeto de configuracion
        /// </summary>
        private Configuracion _objConfiguracion;

        public SalvoConducto()
        {
            _objConfiguracion = new Configuracion();

        }



        /// <summary>
        /// Crea un proceso de Notificación
        /// </summary>
        /// <param name="xmlDatos"></param>
        public void CrearProceso(string xmlDatos)
        {
            try
            {
                SILPA.AccesoDatos.GenerarSalvoConducto.SalActoAdministrativo _xmlSalActoAdministrativo = new SILPA.AccesoDatos.GenerarSalvoConducto.SalActoAdministrativo();
                SolicitudDAAEIAIdentity solicitud = new SolicitudDAAEIAIdentity();
                SolicitudDAAEIADalc _solicitudDalc = new SolicitudDAAEIADalc();
                Comun.TraficoDocumento traficoArchivos = new TraficoDocumento();
                XmlSerializador _ser = new XmlSerializador();
                _xmlSalActoAdministrativo = (SILPA.AccesoDatos.GenerarSalvoConducto.SalActoAdministrativo)_ser.Deserializar(new NotificacionType(), xmlDatos);



                List<byte[]> listaArchivos = new List<byte[]>();
                listaArchivos.Add(_xmlSalActoAdministrativo.DatosArchivo);
                List<string> nombres = new List<string>();
                nombres.Add(_xmlSalActoAdministrativo.NombreArchivo);
                string ruta = "";

                solicitud = _solicitudDalc.ObtenerSolicitud(null, null, _xmlSalActoAdministrativo.NumeroVital);

                // Insertar SalvoConducto

                AccesoDatos.Documento.MisTramitesDocumentosDALC _rutas = new AccesoDatos.Documento.MisTramitesDocumentosDALC();
                _rutas.InsertarDocumentos(_xmlSalActoAdministrativo.AutoridadAmbiental.ToString(), _xmlSalActoAdministrativo.NumeroVital, _xmlSalActoAdministrativo.NumeroActo, ruta);

                /*Se crea la respuesta para tomar datos relacionados  con la publicación */
                WSRespuesta xmlRespuesta = new WSRespuesta();
                xmlRespuesta.Exito = true;
                xmlRespuesta.IdExterno = "";
                xmlRespuesta.IdSilpa = _xmlSalActoAdministrativo.NumeroVital;


            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Informativo, "----Hubo un error Creando proceso Notificacion: " + ex.Message);

                throw;
            }
        }
    }
}
