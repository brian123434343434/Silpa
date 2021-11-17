<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ReasignarSolicitud.aspx.cs" Inherits="Administracion_Tablasbasicas_ReasignarSolicitud" %>
<asp:Content ID="phHeadReasignarSolicitud" ContentPlaceHolderID="headPlaceHolder" Runat="Server">   
    <div class='burbujaAyuda'></div>
</asp:Content>
<asp:Content ID="phReasignarSolicitud" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../../jquery/jquery.js" type="text/javascript"></script>
    <link href="../../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../../jquery/EstiloGris/jquery-ui.js"  type="text/javascript"></script>
    <script src="../../js/Ayuda.js" type="text/javascript"></script>
    <link href="../css/Administracion.css" rel="stylesheet" />

    <asp:ScriptManager ID="scmManager" runat="server"></asp:ScriptManager>

    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="REASIGNACIÓN DE SOLICITUDES" SkinID="titulo_principal_blanco"></asp:Label>
    </div>

    <asp:UpdatePanel runat="server" ID="upnlBuscar" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="buscar-seccion">
                <table>
                    <tbody>
                        <tr>
                            <td class="label-formulario">
                                Número VITAL:
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtNumeroVital" ClientIDMode="Static" MaxLength="20" class="form-control"></asp:TextBox>  
                                <asp:RequiredFieldValidator runat="server" ID="rfvNumeroVital" ControlToValidate="txtNumeroVital" ErrorMessage="Debe ingresar e número VITAL sobre el cual realizará la búsqueda" ValidationGroup="Buscar">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="botones">
                                <asp:Button runat="server" ID="cmdBuscar" ValidationGroup="Buscar" Text="Buscar" ClientIDMode="Static" OnClick="cmdBuscar_Click"/>
                                <asp:Button runat="server" ID="cmdLimpiar" CausesValidation="false" Text="Limpiar" ClientIDMode="Static" OnClick="cmdLimpiar_Click"/>
                                <asp:ValidationSummary ID="valBuscar" runat="server" ValidationGroup="Buscar" ShowMessageBox="true" ShowSummary="false" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>            
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cmdBuscar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="cmdLimpiar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="uppBuscar" runat="server" AssociatedUpdatePanelID="upnlBuscar">
        <ProgressTemplate>  
            <div id="ModalProgressContainer">
                <div>
                    <p>Procesando...</p>
                    <p><asp:Image ID="imgUpdateProgressBuscar" runat="server" SkinId="procesando"/></p>
                </div>
            </div>                         
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel runat="server" ID="upnlMensaje" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="contact_form" id="divMensaje" runat="server">  
                <div class="Table">
                    <div>
                        <div class="CellMensaje">
                            <asp:Label runat="server" ID="lblMensaje"></asp:Label>
                        </div>
                    </div>
                </div>            
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="upnlConsultaSolicitudes" UpdateMode="Conditional">
        <ContentTemplate>
            <div runat="server" id="dvNoExisteDatos" class="mensaje-informativo-busqueda">
                No se encontro información de una solicitud relacionada al Número VITAL especificado
            </div>
            <div runat="server" id="dvDatosSolicitud" class="seccion">
                <div class="titulo-seccion">INFORMACIÓN DE LA SOLICITUD</div>
                <div class="container">
                    <div class="row-seccion">
                        <div class="col-lg-6">
                            <div class="row-seccion">
                                <div class="label-formulario">
                                    Solicitud No.:
                                </div>
                                <div class="campos-formulario">
                                    <asp:Literal runat="server" ID="ltlSolicitudID"></asp:Literal>
                                </div>
                            </div>                                                  
                        </div>
                        <div class="col-lg-6">
                            <div class="row-seccion">
                                <div class="label-formulario">
                                    Autoridad Ambiental:
                                </div>
                                <div class="campos-formulario">
                                    <asp:Literal runat="server" ID="ltlAutoridadAmbiental"></asp:Literal>
                                </div>
                            </div>                                                  
                        </div>
                    </div>                    
                    <div class="row-seccion">
                        <div class="col-lg-6">
                            <div class="row-seccion">
                                <div class="label-formulario">
                                    Tipo de Trámite:
                                </div>
                                <div class="campos-formulario">
                                    <asp:Literal runat="server" ID="ltlTipoTramite"></asp:Literal>
                                </div>
                            </div>                                                  
                        </div>
                        <div class="col-lg-6">
                            <div class="row-seccion">
                                <div class="label-formulario">
                                    Número VITAL:
                                </div>
                                <div class="campos-formulario">
                                    <asp:Literal runat="server" ID="ltlNumeroVital"></asp:Literal>
                                </div>
                            </div>                                                  
                        </div>
                    </div>
                    <div class="row-seccion">
                        <div class="col-lg-6">
                            <div class="row-seccion">
                                <div class="label-formulario">
                                    Solicitante:
                                </div>
                                <div class="campos-formulario">
                                    <asp:Literal runat="server" ID="ltlSolicitante"></asp:Literal>
                                </div>
                            </div>                                                  
                        </div>
                        <div class="col-lg-6">
                            <div class="row-seccion">
                                <div class="label-formulario">
                                    Fecha Solicitud:
                                </div>
                                <div class="campos-formulario">
                                    <asp:Literal runat="server" ID="ltlFechaSolicitud"></asp:Literal>
                                </div>
                            </div>                                                  
                        </div>
                    </div>
                    <div class="botones">
                        <asp:HiddenField runat="server" ID="hdfAutoridadID" />
                        <asp:HiddenField runat="server" ID="hdfNumeroVital" />
                        <asp:Button runat="server" ID="cmdReasignar" Text="Reasignar" ClientIDMode="Static" OnClick="cmdReasignar_Click" />
                    </div>
                </div>                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="uppConsultaSolicitudes" runat="server" AssociatedUpdatePanelID="upnlConsultaSolicitudes">
        <ProgressTemplate>  
            <div id="ModalProgressContainer">
                <div>
                    <p>Procesando...</p>
                    <p><asp:Image ID="imgUpdateProgressConsultaSolicitudes" runat="server" SkinId="procesando"/></p>
                </div>
            </div>                         
        </ProgressTemplate>
    </asp:UpdateProgress>

    <input type="button" runat="server" id="cmdReasignarSolicitudHide" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeModalReasignarSolicitud" runat="server" PopupControlID="dvModalReasignarSolicitud" TargetControlID="cmdReasignarSolicitudHide" BehaviorID="mpeModalReasignarSolicituds" BackgroundCssClass="modal">
    </cc1:ModalPopupExtender>
    <div id="dvModalReasignarSolicitud" style="display:none;" runat="server" clientidmode="Static" class="modal-dialog">        
        <asp:UpdatePanel runat="server" ID="upnlModalReasignarSolicitud" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content">
                  <div class="modal-header">
                    <h6 class="modal-title">REASIGNAR SOLICITUD</h6>
                  </div>
                  <div class="modal-body">
                      <div class="row-modal">
                          <div class="label-formulario">
                            Nueva Autoridad Ambiental:
                          </div>
                          <div class="campos-formulario">
                            <asp:DropDownList runat="server" ID="cboAutoridadAmbiental"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvAutoridadAmbiental" ControlToValidate="cboAutoridadAmbiental" InitialValue="-1" ErrorMessage="Debe seleccionar la autoridad ambiental a la cual reasiganará la solicitud." ValidationGroup="ReasignarSolicitud">*</asp:RequiredFieldValidator>
                          </div>
                      </div>
                      
                  </div>
                  <div class="modal-footer">
                    <asp:Button ID="cmdModalReasignarSolicitudAceptar" runat="server" ClientIDMode="Static" Text="Reasignar" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="ReasignarSolicitud" OnClientClick="return confirm('¿Se encuentra seguro de realizar la reasignación de la solicitud?');" OnClick="cmdModalReasignarSolicitudAceptar_Click" />
                    <asp:Button ID="cmdModalReasignarSolicitudCancelar" runat="server" ClientIDMode="Static" Text="Cancelar" CssClass="boton" CausesValidation="false" OnClick="cmdModalReasignarSolicitudCancelar_Click"  />
                    <asp:ValidationSummary runat="server" ID="valReasignarSolicitud" ValidationGroup="ReasignarSolicitud" ShowSummary="false" ShowMessageBox="true" />  
                  </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdModalReasignarSolicitudAceptar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="cmdModalReasignarSolicitudCancelar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="uppVerDocumentoActoAdministrativo" runat="server" AssociatedUpdatePanelID="upnlModalReasignarSolicitud">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgUpdateProgresModalReasignarSolicitud" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <input type="button" runat="server" id="cmdReasignacionCorrectaHide" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeReasignacionCorrecta" runat="server" PopupControlID="dvReasignacionCorrecta" TargetControlID="cmdReasignacionCorrectaHide" BehaviorID="mpeReasignacionCorrectas" BackgroundCssClass="modal">
    </cc1:ModalPopupExtender>
    <div id="dvReasignacionCorrecta" style="display:none;" runat="server" clientidmode="Static" class="modal-dialog">
        <asp:UpdatePanel runat="server" ID="upnlReasignacionCorrecta" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content">
                  <div class="modal-header">
                    <h6 class="modal-title">REASIGNAR SOLICITUD</h6>
                  </div>
                  <div class="modal-body">
                      <div class="row-modal">
                          <div class="img-resultado">
                              <asp:Image runat="server" ID="imgIconoReasignacionCorrecta" ImageUrl="~/App_Themes/Img/chulo_verde.png" /> 
                          </div>
                          <div class="texto-resultado">Se realizo la reasignación de la solicitud a la nueva autoridad ambiental de manera correcta.</div>
                      </div>                      
                  </div>
                  <div class="modal-footer">
                    <asp:Button runat="server" ID="cmdAceptarReasignacionCorrecta" CausesValidation="false" Text="Aceptar" ClientIDMode="Static" OnClick="cmdAceptarReasignacionCorrecta_Click"/>
                  </div>
                </div>
                
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdAceptarReasignacionCorrecta" EventName="Click" />                                                        
            </Triggers>
        </asp:UpdatePanel>

        <asp:UpdateProgress ID="uppReasignacionCorrecta" runat="server" AssociatedUpdatePanelID="upnlReasignacionCorrecta">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgReasignacionCorrecta" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

</asp:Content>

