<%@ Application Language="C#" %>

<script runat="server">
    //SILPA.Servicios.Comunicacion.ComunicacionVisitaFachada _cvf = new SILPA.Servicios.Comunicacion.ComunicacionVisitaFachada();
    //SILPA.Servicios.Generico.VerificarEstado.VerificarEstado _ver = new SILPA.Servicios.Generico.VerificarEstado.VerificarEstado();
    //SILPA.Servicios.Comunicacion.ComunicacionEEFachada _com = new SILPA.Servicios.Comunicacion.ComunicacionEEFachada();
    //SILPA.Servicios.ImpresionFUS.ImpresionFUSFachada _imp = new SILPA.Servicios.ImpresionFUS.ImpresionFUSFachada();    
    
    void Application_Start(object sender, EventArgs e) 
    {
        // Código que se ejecuta al iniciarse la aplicación
        //System.Threading.Thread proceso1 = new System.Threading.Thread(_cvf.EnviarProceso);
        //System.Threading.Thread procesoVerificar = new System.Threading.Thread(_ver.EnviarProceso);
        //System.Threading.Thread procesoComunicacion = new System.Threading.Thread(_com.EnviarProceso);
        //System.Threading.Thread procesoImprimir = new System.Threading.Thread(_imp.IniciarProceso);      
        //proceso1.Start();
        //procesoVerificar.Start();
        //procesoComunicacion.Start();
        //procesoImprimir.Start(); 
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
