/*

    Funciones de JavaScript

*/


function fntMensajeRecurso()
{
  var msg = "Recurso de reposición registrado exitosamente.";
  alert(msg);
}


// funcion de prueba
function fntMessageBox()
{
  var msg = "funciona";
  alert(msg);
}


//boton cancelar de la pagina CambiarClave.aspx
function LimpiarCambiarClave()
{
    var ctlTxtUsuario   = Object();  
    
    //confirm("Clave cambiada con exito");
//    var ctlTxtClave  = Object();  
//    var ctlTxtNewClave = Object();
//    var ctlTxtCofirmClave = Object();
    
  //  ctlTxtUsuario       = document.all('ctl00_ContentPlaceHolder1_txtUsuario');  
    //ctlTxtUsuario.focus();
//    ctlTxtClave         = document.all('ctl00_ContentPlaceHolder1_txtClave');  
//    ctlTxtNewClave      = document.all('ctl00_ContentPlaceHolder1_txtNewClave');  
//    ctlTxtCofirmClave   = document.all('ctl00_ContentPlaceHolder1_txtCofirmClave');
    
//    ctlTxtUsuario.value = "";
//    ctlTxtClave.value = "";
//    ctlTxtNewClave.value = "";
//    ctlTxtCofirmClave.value = "";
    
    
}


/// deshabilita
function deshabilitarBoton()
{
    //var msg = "funciona 3";
    //alert(msg);
    var ctlBtnAceptar   = Object();  
    var ctlBtnCancelar  = Object();  
    var ctlLstAutoridad = Object();
    
    ctlBtnAceptar  = document.all('ctl00_ContentPlaceHolder1_btnAceptar');  
    ctlBtnCancelar  = document.all('ctl00_ContentPlaceHolder1_btnCancelar');  
    ctlLstAutoridad  = document.all('ctl00_ContentPlaceHolder1_cboAutoridadAmbiental');  
    //ctlBtnAceptar.Click();
    //ctlBtnAceptar.Hidden = true;
    //cboLstAutoridad.disabled = true;
    ctlBtnAceptar.style.visibility = 'hidden';
    ctlBtnCancelar.style.visibility = 'hidden';
}

function deshabilitarBotonTDREIA()
{
    var ctlBtnSolicitarTDREIA  = Object();  
    ctlBtnSolicitarTDREIA  = document.all('ctl00_ContentPlaceHolder1_btnSolicitarTDREIA');
    ctlBtnSolicitarTDREIA.style.visibility = 'hidden';
}

function deshabilitarBotonSolicitarDAA()
{
    var ctlBtnSolicitarDAA  = Object();  
    ctlBtnSolicitarDAA  = document.all('ctl00_ContentPlaceHolder1_btnSolicitarDAA');
    ctlBtnSolicitarDAA.style.visibility = 'hidden';
}


/// funcion de prueba
function fntSolicitudTDR()
{
  var msg = "Solicitud TDR enviada correctamente";
  alert(msg);
  window.close();
}

/// funcion de prueba
function fntConflicto()
{
  var msg = "Se ha detectado que su proyecto tiene jurisdicción en varias autoridades ambientales, por tal razón usted tiene las siguientes opciones:\n  1. Enviar solicitud al MAVDT para que este defina la AA que dará trámite a su solicitud y proyecto.\n  2. Seleccione la AA a la cual desea enviar su solicitud.";
  confirm(msg);
}

function fntMessageBox(msg)
{
  alert(msg);
}
/*----------------------------------------------------------
 funcion para limpiar controles de la pagina 
    RadicacionDocumentos - puede hacerce una generica-
----------------------------------------------------------*/
function fntLimpiarDatosRadicacion()
{
    if (document.activeElement.id == "ctl00_ContentPlaceHolder1_btnLimpiarDatos")
    {

        var ctltxtNumeroSilpa           = Object();  
        var ctltxtActoAdministrativo    = Object();  
        var ctltxtRadicadoAA            = Object();  
        var ctlfldRadicarDocumento      = Object();  
        //var ctllbMensajeRadicacion      = Object();  
        var ctltxtMensajeRadicacion     = Object();
        //var ctlpnlMensaje               = Object();
        
        ctltxtNumeroSilpa           = document.all('ctl00_ContentPlaceHolder1_txtNumeroSilpa');  
        ctltxtActoAdministrativo    = document.all('ctl00_ContentPlaceHolder1_txtActoAdministrativo');  
        ctltxtRadicadoAA            = document.all('ctl00_ContentPlaceHolder1_txtRadicadoAA');  
        ctlfldRadicarDocumento      = document.all('ctl00_ContentPlaceHolder1_fldRadicarDocumento');  
        //ctllbMensajeRadicacion      = document.all('ctl00_ContentPlaceHolder1_lbMensajeRadicacion');  
        ctltxtMensajeRadicacion     = document.all('ctl00_ContentPlaceHolder1_txtMensajeRadicacion');  
        //ctlpnlMensaje               = document.all('ctl00_ContentPlaceHolder1_pnlMensaje');  
        
        /// Limpiando controles
        ctltxtNumeroSilpa.value         = "";
        ctltxtActoAdministrativo.value  = "";
        ctltxtRadicadoAA.value          = "";
        ctlfldRadicarDocumento.value    = "";
        //ctllbMensajeRadicacion.value    = "";
        //ctltxtMensajeRadicacion.visible = false;
        //ctltxtMensajeRadicacion.visible = false;
        
        //ctltxtMensajeRadicacion.style.visibility='hidden'; 
        
        //        ctltxtMensajeRadicacion.disabled = false;
        //        ctltxtMensajeRadicacion.value   = "";
        
//       ctllbMensajeRadicacion.value =""; 
//       ctllbMensajeRadicacion.visible = false;
//       ctlpnlMensaje.style.visibility = "hidden";
        
       ///  
       ctltxtMensajeRadicacion.disabled = false;
       ctltxtMensajeRadicacion.value = "";
       ctltxtMensajeRadicacion.visible = false;
       ctltxtMensajeRadicacion.style.visibility = "hidden";
       ctltxtMensajeRadicacion.disabled = true;
    }
}


/*
----------------------------------------------------------
  funcion para verificar que se adjunte un archivo
  RadicacionDocumentos - puede hacerce una generica-
----------------------------------------------------------
*/

function fntDebeAdjuntarArchivo()
{
    if (document.activeElement.id == "ctl00_ContentPlaceHolder1_btnRadicarDocumento")
    {
        var ctlfldRadicarDocumento      = Object();  
        ctlfldRadicarDocumento          = document.all('ctl00_ContentPlaceHolder1_fldRadicarDocumento');  
        
        if(ctlfldRadicarDocumento.value == null || ctlfldRadicarDocumento.value == "" )
        {
            alert('Debe seleccionar una archivo para adjuntar... !');
        }
    }
}


function cancelarRUIA()
{
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosSancion_cboTipoFalta').value = 1;
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosSancion_txtLugarConcurrencia').value = "";
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosSancion_chkPrincipal').checked = false;
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosSancion_cboOpcionesPrincipal').value = 1;
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosSancion_cboOpcionesPrincipal').disabled = true;
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosSancion_chkAccesoria').checked = false;
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosSancion_pnlOpcionesAccesoria').disabled = true;
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosSancion_chkMulta').checked = false;
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosSancion_chkDemolicion').checked = false;
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosSancion_chkTrabajoComunitario').checked = false;
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosSancion_chkSuspension').checked = false;
    
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosSancion_txtDescripcionNorma').value = "";
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosSancion_txtNumeroExpediente').value = "";
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosSancion_txtNumeroActo').value = "";
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosSancion_txtFechaExpedicion').value = "";
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosSancion_txtFechaEjecutoria').value = "";
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosSancion_txtFechaEjecucion').value = "";
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosSancion_txtFechaDesde').value = "";
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosSancion_txtFechaHasta').value = "";
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosSancion_txtDescripcionDesfijacion').value = "";
    
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosNatural_txtPrimerNombre').value = "";
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosNatural_txtSegundoNombre').value = "";
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosNatural_txtPrimerApellido').value = "";
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosNatural_txtSegundoApellido').value = "";
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosNatural_cboTipoDocumento').value = 1;
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosNatural_txtNumeroDocumento').value = "";
    
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosNatural_cboDepartamentoNatural').value = 5;
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosNatural_cboMunicipioNatural').value = 5001;
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosJuidica_txtRazonSocial').value = "";
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosJuidica_txtNit').value = "";
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosJuidica_txtPrimerNombreRepresentante').value = "";
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosJuidica_txtSegundoNombreRepresentante').value = "";
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosJuidica_txtPrimerApellidoRepresentante').value = "";
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosJuidica_txtSegundoApellidoRepresentante').value = "";
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosJuidica_cboTipoDocumentoJuridica').value = 1;
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosJuidica_txtNumeroDocumentoRepresentante').value = "";
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosJuidica_cboDepartamentoJuridica').value = 5;
    document.all('ctl00_ContentPlaceHolder1_tbcContenedor_tabDatosJuidica_cboMunicipioJuridica').value = 5001;
    
       
    return false;
}


/*
----------------------------------------------------------
  funcion para precargar los datos del vocero desde 
  los datos de persona natural
  se usa en  audiencia pública
----------------------------------------------------------
*/
	function fntPrecargarVocero()
	{
		
		// Persona natural
		var ctlTxtPrimerApellido   = Object();  
		var ctlTxtSegundoApellido = Object();  
		var ctlTxtPrimerNombre = Object();  
		var ctlTxtSegundoNombre = Object();  
		
		var ctlTxtPrimerApellidoVocero   = Object();  
		var ctlTxtSegundoApellidoVocero = Object();  
		var ctlTxtPrimerNombreVocero = Object();  
		var ctlTxtSegundoNombreVocero = Object();  
		
		
		var ctlTxtNumeroIdentificacion = Object();  
		var ctlTxtNumeroIdentificacionVocero = Object();  

		var ctlTxtCorreoPersona = Object();  
		var ctlTtxtCorreoVocero = Object();  

		var ctlTcboTipoDocumentoPersona = Object();  
		var ctlTcboTipoDocumentoVocero = Object();  
		
		var ctlCboMunicipioOrigenPersona = Object();
		var ctlCboMunicipioOrigenVocero = Object();
		

		// Los Controles de persona natural:
		ctlTxtPrimerApellido  = document.all('ctl00_ContentPlaceHolder1_tbAudienciaPublica_tabDatosPersona_txtPrimerApellidoPersona');  
		ctlTxtSegundoApellido = document.all('ctl00_ContentPlaceHolder1_tbAudienciaPublica_tabDatosPersona_txtSegundoApellidoPersona');  
		ctlTxtPrimerNombre = document.all('ctl00_ContentPlaceHolder1_tbAudienciaPublica_tabDatosPersona_txtPrimerNombrePersona');  
		ctlTxtSegundoNombre = document.all('ctl00_ContentPlaceHolder1_tbAudienciaPublica_tabDatosPersona_txtSegundoNombrePersona');  
		
		ctlTxtNumeroIdentificacion = document.all('ctl00_ContentPlaceHolder1_tbAudienciaPublica_tabDatosPersona_txtNumeroIdentificacionPersona');  
		ctlTxtCorreoPersona = document.all('ctl00_ContentPlaceHolder1_tbAudienciaPublica_tabDatosPersona_txtCorreoPersona');  
		
		ctlTcboTipoDocumentoPersona = document.all('ctl00_ContentPlaceHolder1_tbAudienciaPublica_tabDatosPersona_cboTipoDocumentoPersona');  
		
		ctlCboMunicipioOrigenPersona = document.all('ctl00_ContentPlaceHolder1_tbAudienciaPublica_tabDatosPersona_cboMunicipioOrigenPersona');  

		// la lista de tipos de persona:
		//ctlLstTipoPersona = document.all('ctl00_ContentPlaceHolder1_cboTipoPersona');  

		// los controles del vocero:
		ctlTxtPrimerNombreVocero   = document.all('ctl00_ContentPlaceHolder1_tbAudienciaPublica_tbVocero_txtPrimerNombreVocero');  
		ctlTxtSegundoNombreVocero = document.all('ctl00_ContentPlaceHolder1_tbAudienciaPublica_tbVocero_txtSegundoNombreVocero');  
		ctlTxtPrimerApellidoVocero = document.all('ctl00_ContentPlaceHolder1_tbAudienciaPublica_tbVocero_txtPrimerApellidoVocero');  
		ctlTxtSegundoApellidoVocero= document.all('ctl00_ContentPlaceHolder1_tbAudienciaPublica_tbVocero_txtSegundoApellidoVocero');  
		
		ctlTxtNumeroIdentificacionVocero = document.all('ctl00_ContentPlaceHolder1_tbAudienciaPublica_tbVocero_txtNumeroIdentificacionVocero');  
		ctlTtxtCorreoVocero = document.all('ctl00_ContentPlaceHolder1_tbAudienciaPublica_tbVocero_txtCorreoVocero');  
		
		ctlTcboTipoDocumentoVocero = document.all('ctl00_ContentPlaceHolder1_tbAudienciaPublica_tbVocero_cboTipoDocumentoVocero');  
		
		ctlCboMunicipioOrigenVocero = document.all('ctl00_ContentPlaceHolder1_tbAudienciaPublica_tbVocero_cboMunicipioOrigenVocero');  
		
		// Asignacion de los valores:
		ctlTxtPrimerApellidoVocero.value  =  ctlTxtPrimerApellido.value; 
		ctlTxtSegundoApellidoVocero.value   = ctlTxtSegundoApellido.value;
		ctlTxtPrimerNombreVocero.value   = ctlTxtPrimerNombre.value;
		ctlTxtSegundoNombreVocero.value   = ctlTxtSegundoNombre.value;
		
		ctlTxtNumeroIdentificacionVocero.value = ctlTxtNumeroIdentificacion.value;
		ctlTtxtCorreoVocero.value = ctlTxtCorreoPersona.value;
		
		ctlTcboTipoDocumentoVocero.value = ctlTcboTipoDocumentoPersona.value;
		
		ctlCboMunicipioOrigenVocero.value = ctlCboMunicipioOrigenPersona.value;
		
	}
