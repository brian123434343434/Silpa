using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using SILPA.Servicios.Generico.RadicarDocumento;
using SILPA.Servicios;
using SILPA.AccesoDatos.Notificacion;
using System.Collections.Generic;
using SILPA.Comun;
using System.Text;


/// <summary>
/// Summary description for Pruebas
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Pruebas : System.Web.Services.WebService
{

    public Pruebas()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "Metodo de test del servicio")]
    public void PruebaNotificacionEntrada(string xmlDatos)
    {
            SILPA.Comun.XmlSerializador _objSer = new XmlSerializador();
            NotificacionConsultaType _xmlConsulta = new NotificacionConsultaType();
            _xmlConsulta = (NotificacionConsultaType)_objSer.Deserializar(new NotificacionConsultaType(), xmlDatos);

            /// lógica para consulta
            /// ....
            /// ....
            SILPA.AccesoDatos.Notificacion.NotificacionDalc dalc = new NotificacionDalc();
            List<NotificacionEntity> not = new List<NotificacionEntity>();
            not = dalc.ObtenerActoPorAA(_xmlConsulta.numActoAdministrativoNotificacion, _xmlConsulta.numProcesoAdministracion);
            NotificacionEntity item1 = new NotificacionEntity();
            item1 = not[not.Count - 1];
            
            SILPA.LogicaNegocio.Notificacion.Notificacion noti = new SILPA.LogicaNegocio.Notificacion.Notificacion();
            noti.ActualizarEstado(item1.ListaPersonas[0], item1);
            //return not.ConsultarNotificacion(xml);
        
    }

    [WebMethod]
    public string PruebaActividadesRadicables(string Client, Int64 UserID, Int64 activityInstanceID,
                                             Int64 processInstanceID, string entryDataType,
                                             string entryData, string idEntryData)
    {
        RadicacionDocumentoFachada objRadFachada = new RadicacionDocumentoFachada();
        objRadFachada.VerificarActividadRadicable(Client, UserID, activityInstanceID, processInstanceID, entryDataType,
                                                  entryData, idEntryData);
        return "*";
    }

    [WebMethod]
    public string PruebasAvanceActividades(string numeroSILPA)
    {
        //SILPA.Servicios.ArctividadBPMFachada.DeterminarAvanceActividad(numeroSILPA);
        return "*";
    }

    [WebMethod(Description = "[Consume un servicio del sistema de notificación en línea por medio de PDI para entregar los datos para el ejecutoriar el acto administrativo de un proceso de notificación]", MessageName = "EjecutoriarActo")]
    public string EjecutoriarActo(string documento)
    {
        bool respuesta = false;
        SILPA.LogicaNegocio.Notificacion.Notificacion _objNotificacion = new SILPA.LogicaNegocio.Notificacion.Notificacion();
        string eje = _objNotificacion.Ejecutoria(documento, out respuesta);
        return eje;
        //WSPQ03 componente = new WSPQ03();
        //return componente.ComponenteNot("ejecutoriar", documento);
       


        //
    }

    [WebMethod(Description = "[Consume un servicio del sistema de notificación en línea por medio de PDI para entregar los datos para el ejecutoriar el acto administrativo de un proceso de notificación]", MessageName = "Actualizar")]
    public string Actualizar()
    {
        //SILPA.Servicios.NotificacionFachada not = new SILPA.Servicios.NotificacionFachada();
        //SILPA.LogicaNegocio.Notificacion.Notificacion notejec = new SILPA.LogicaNegocio.Notificacion.Notificacion();
        //List<string> lista = new List<string>();
        //lista = not.ActualizarProcesos();
        //StringBuilder str = new StringBuilder();
        //foreach (string pe in lista)
        //{
        //    str.Append(pe);
        //    str.Append("\n");
        //}
        //return str.ToString();
        NotificacionFachada not = new NotificacionFachada();
        return not.ComponenteNot("consultar", "",false);


        //
    }


    [WebMethod]
    public string testUrl(string url)
    {
        SILPA.LogicaNegocio.Notificacion.Notificacion not = new SILPA.LogicaNegocio.Notificacion.Notificacion();
        return not.TestPDI(url).ToString();
    }


}

