<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="PresentarRecurso.aspx.cs" Inherits="Presentar_Recurso" Title="Presentar Recurso" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Recurso/Controles/RegistrarRecursoReposicion.ascx" TagPrefix="rer" TagName="RegistrarRecursoReposicion" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>
    <div class='burbujaAyuda'></div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        table {
            /*border: 1px solid #000;*/
        }
        table tr td {
            border: 0px solid #ddd !important;
            padding: 4px;
        }
        .Button{
            background-color: #ddd;
        }
    </style>

    <script src="../jquery/jquery.js" type="text/javascript"></script>
    <link href="../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../jquery/EstiloGris/jquery-ui.js"  type="text/javascript"></script>
    <link href="../jquery/keypad/jquery.keypad.css" rel="stylesheet" />
    <script src="../jquery/keypad/jquery.plugin.js" type="text/javascript"></script>
    <script src="../jquery/keypad/jquery.keypadmodal.js" type="text/javascript"></script> 
    <link href="../NotificacionElectronica/css/FormularioNotificacionElectronica.css" rel="stylesheet" />

    <asp:ScriptManager ID="scmManager" runat="server"></asp:ScriptManager>
    
    <table class="TablaTituloSeccionNotElec">
        <tr>
            <td class="div-titulo">
                <a href="#" id="hrfCerrarVentana" runat="server" onclick="window.close();return false;" class="Salir">Salir [X]</a>
            </td>
        </tr>
        <tr>
            <td class="div-titulo">
                <asp:Label ID="lblTitulo" runat="server" Text="RECURSO DE REPOSICIÓN" SkinID="titulo_principal_blanco"></asp:Label>
            </td>
        </tr>
    </table>

    <asp:UpdatePanel runat="server" ID="upnlBuscar" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="TablaNotElec">
                <tr>
                    <td colspan="4" class="TituloSeccionNotElec">Filtro de Búsqueda</td>
                </tr>
                <tr>
                    <td class="LabelFormularioBusquedaNotElec">
                        Número VITAL:
                    </td>
                    <td class="CamposFormularioBusquedaNotElec">
                        <asp:TextBox runat="server" ID="txtNumeroVital" ClientIDMode="Static"></asp:TextBox>                                        
                    </td>
                    <td class="LabelFormularioBusquedaNotElec">
                        Número de Expediente:
                    </td>
                    <td class="CamposFormularioBusquedaNotElec">
                        <asp:TextBox runat="server" ID="txtExpediente" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="LabelFormularioBusquedaNotElec">
                        Número Acto Administrativo:
                    </td>
                    <td class="CamposFormularioBusquedaNotElec">
                        <asp:TextBox runat="server" ID="txtNumeroActo" ClientIDMode="Static"></asp:TextBox>
                    </td>
                    <td class="LabelFormularioBusquedaNotElec">
                        Autoridad Ambiental:
                    </td>
                    <td class="CamposFormularioBusquedaNotElec">
                        <asp:DropDownList ID="cboAutoridadAmbiental" runat="server" SkinID="lista_desplegableupdate" />&nbsp;&nbsp;
                        <asp:RequiredFieldValidator ID="rfvAutoridadAmbiental" runat="server" ControlToValidate="cboAutoridadAmbiental" ErrorMessage="Debe seleccionar la autoridad ambiental." Text="*" ValidationGroup="RecursoBuscar" InitialValue="-1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="LabelFormularioBusquedaNotElec">
                        Fecha Acto Administrativo Desde:
                    </td>
                    <td class="CamposFormularioBusquedaNotElec">
                        <asp:TextBox ID="txtFechaDesde" runat="server" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFechaDesde" runat="server" ControlToValidate="txtFechaDesde" ErrorMessage="Ingrese la Fecha Desde la cual desea buscar." Text="*" ValidationGroup="RecursoBuscar" InitialValue=""></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rexFechaDesde" runat="server" ControlToValidate="txtFechaDesde" ErrorMessage="Formato de fecha no valido para la Fecha Desde." ValidationExpression="^\d{2}\/\d{2}\/\d{4}" Width="3px" Text="*" ValidationGroup="RecursoBuscar"></asp:RegularExpressionValidator>
                        <cc1:CalendarExtender ID="calFechaDesde" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaDesde"/>
                    </td>
                    <td class="LabelFormularioBusquedaNotElec">
                        Fecha Acto Administrativo Hasta:
                    </td>
                    <td class="CamposFormularioBusquedaNotElec">
                        <asp:TextBox ID="txtFechaHasta" runat="server" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFechaHasta" runat="server" ControlToValidate="txtFechaHasta" ErrorMessage="Ingrese la Fecha Hasta la cual desea buscar." Text="*" ValidationGroup="RecursoBuscar" InitialValue=""></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rexFechaHasta" runat="server" ControlToValidate="txtFechaHasta" ErrorMessage="Formato de fecha no valido para la Fecha Hasta." ValidationExpression="^\d{2}\/\d{2}\/\d{4}" Width="3px" Text="*" ValidationGroup="RecursoBuscar"></asp:RegularExpressionValidator>
                        <cc1:CalendarExtender ID="calFechaHasta" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaHasta" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table class="TablaBotonesFormularioNotElec">
                            <tr>
                                <td>
                                    <asp:HiddenField runat="server" ID="hdfNumeroVital" />
                                    <asp:HiddenField runat="server" ID="hdfExpediente" />
                                    <asp:HiddenField runat="server" ID="hdfNumeroActo" />
                                    <asp:HiddenField runat="server" ID="hdfAutoridadAmbiental" />
                                    <asp:HiddenField runat="server" ID="hdfFechaDesde" />
                                    <asp:HiddenField runat="server" ID="hdfFechaHasta" />
                                    <asp:Button runat="server" ID="cmdBuscar" ValidationGroup="RecursoBuscar" Text="Buscar" ClientIDMode="Static" OnClick="cmdBuscar_Click"/>
                                    <asp:ValidationSummary ID="valRecursoBuscar" runat="server" ValidationGroup="RecursoBuscar" ShowMessageBox="true" ShowSummary="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cmdBuscar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upplBuscar" runat="server" AssociatedUpdatePanelID="upnlBuscar">
        <ProgressTemplate>  
            <div id="ModalProgressContainer">
                <div>
                    <p>Procesando...</p>
                    <p><asp:Image ID="imagePanelBuscar" runat="server" SkinId="procesando"/></p>
                </div>
            </div>                         
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel runat="server" ID="upnlMensaje" UpdateMode="Conditional">
        <ContentTemplate>
            <table runat="server" visible="false" id="tblMensaje" class="TablaMensajeErrorNotElec">
                <tr>
                    <td class="MensajeErrorNotElec">
                        <asp:Literal runat="server" ID="lblMensaje"></asp:Literal>
                        <br />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="upnlConsultaRecursos" UpdateMode="Conditional">
        <ContentTemplate>
            <div runat="server" class="table-responsive DivTablaFormularioNotElec" id="divResultadoBusqueda">        
                <table style="width:100%;">
                    <tr>
                        <td>
                            <asp:GridView  ID="grdActosRecursos" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="true" PageSize="10" 
                                        SkinID="GrillaListaNotificacionElectronica"  ShowHeaderWhenEmpty="true" 
                                        OnPageIndexChanging="grdActosRecursos_PageIndexChanging" 
                                        EmptyDataText="No se encontraron actos administrativos que cumplan con los parámetros de búsqueda especificados." 
                                        DataKeyNames="ActoNotificacionID, PersonaID, EstadoActualID, AutoridadID, EstadoFuturoID, FlujoID, IdentificacionUsuario, EstadoPersonaActoID">                                
                                <Columns>
                                    <asp:TemplateField HeaderText="AUTORIDAD AMBIENTAL" ItemStyle-CssClass="ItemNotificacionCentrado">
                                        <ItemTemplate>
                                            <asp:literal ID="ltlAutoridad" runat="server" Text='<%# Eval("Autoridad")  %>'></asp:literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NÚMERO VITAL" ItemStyle-CssClass="ItemNotificacion">
                                        <ItemTemplate>
                                            <asp:literal ID="ltlNúmeroVital" runat="server" Text='<%# Eval("NumeroVITAL")  %>'></asp:literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NÚMERO EXPEDIENTE" ItemStyle-CssClass="ItemNotificacion">
                                        <ItemTemplate>
                                            <asp:literal ID="ltlExpediente" runat="server" Text='<%# Eval("Expediente")  %>'></asp:literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NÚMERO ACTO ADMINISTRATIVO" ItemStyle-CssClass="ItemNotificacionCentrado">
                                        <ItemTemplate>
                                            <asp:literal ID="ltlNumeroActo" runat="server" Text='<%# Eval("NumeroActoAdministrativo")  %>'></asp:literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FECHA ACTO ADMINISTRATIVO" ItemStyle-CssClass="ItemNotificacionCentrado">
                                        <ItemTemplate>
                                            <asp:literal ID="ltlFechaActo" runat="server" Text='<%# (Eval("FechaActoAdministrativo") != System.DBNull.Value ? Convert.ToDateTime(Eval("FechaActoAdministrativo")).ToString("dd/MM/yyyy") : "-") %>'></asp:literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FECHA NOTIFICACION" ItemStyle-CssClass="ItemNotificacionCentrado">
                                        <ItemTemplate>
                                            <asp:literal ID="ltlFechaNotificacion" runat="server" Text='<%# (Eval("FechaNotificacion") != System.DBNull.Value ? Convert.ToDateTime(Eval("FechaNotificacion")).ToString("dd/MM/yyyy") : "-") %>'></asp:literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DOCUMENTO ACTO ADMINISTRATIVO" ItemStyle-CssClass="ItemNotificacionCentrado">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgDescargarDocumentoANLA" BorderWidth="0" ImageUrl="~/images/documento.png" Visible='<%# Convert.ToInt32(Eval("AutoridadID")) == 144 %>' CommandArgument='<%#Eval("ActoNotificacionID") %>' OnClick="imgDescargarDocumentoANLA_Click" />
                                            <asp:ImageButton runat="server" ID="imgDescargarDocumento" BorderWidth="0" ImageUrl="~/images/documento.png" Visible='<%# Convert.ToInt32(Eval("AutoridadID")) != 144 %>' CommandArgument='<%#Eval("RutaDocumento") %>' OnClick="imgDescargarDocumento_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                    <asp:TemplateField HeaderText="INTERPONER RECURSO DE REPOSICIÓN" ItemStyle-CssClass="ItemNotificacionCentrado">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgInterponerRecurso" BorderWidth="0" ImageUrl="~/images/radicar.png" CommandArgument='<%# Container.DisplayIndex %>' OnClick="imgInterponerRecurso_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                             <table class="TablaBotonesFormularioNotElec">
                                <tr>
                                    <td>
                                        <asp:Button runat="server" ID="cmdMostrarTodos" Text="Mostrar Todo" ClientIDMode="Static" OnClick="cmdMostrarTodos_Click"/>
                                        <asp:Button runat="server" ID="cmdMostrarPaginado" Text="Mostrar Paginado" ClientIDMode="Static" OnClick="cmdMostrarPaginado_Click"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cmdMostrarTodos" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="cmdMostrarPaginado" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="uppConsultaRecursos" runat="server" AssociatedUpdatePanelID="upnlConsultaRecursos">
        <ProgressTemplate>  
            <div id="ModalProgressContainer">
                <div>
                    <p>Procesando...</p>
                    <p><asp:Image ID="imagePanelConsultaRecursos" runat="server" SkinId="procesando"/></p>
                </div>
            </div>                         
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    
    <input type="button" runat="server" id="cmdModalMensajeOkHide" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeModalMensajeOk" runat="server" PopupControlID="dvModalMensajeOk" TargetControlID="cmdModalMensajeOkHide" BehaviorID="mpeModalMensajeOk" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <div id="dvModalMensajeOk" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalFormularioNot">
        <asp:UpdatePanel runat="server" ID="upnlModalMensajeOk" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="TablaFormularioNotElec">
                    <tr>
                        <td colspan="2" class="TituloSeccionNotElec">
                            <asp:Literal runat="server" ID="ltlTituloModalMensajeOk"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalImagenesNotElec">
                            <asp:Image runat="server" ID="imgOkModalMensajeOk" ImageUrl="~/App_Themes/Img/chulo_verde.png" Width="39px" />
                        </td>
                        <td class="ModalTextoNotElec">
                            <asp:Literal runat="server" ID="ltlMensajeModalMensajeOk"></asp:Literal>
                        </td>
                    </tr> 
                </table>
                <table class="TablaBotonesFormularioNotElec">
                    <tr>
                        <td>
                            <asp:Button ID="cmdAceptarModalMensajeOk" runat="server" Text="Aceptar" CssClass="boton" CausesValidation="false" OnClick="cmdAceptarModalMensajeOk_Click"/>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdAceptarModalMensajeOk" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>        
        <asp:UpdateProgress ID="uppModalMensajeOk" runat="server" AssociatedUpdatePanelID="upnlModalMensajeOk">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imagePanelModalMensajeOk" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>    


    <input type="button" runat="server" id="cmdPresentarRecursoHide" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeModalPresentarRecurso" runat="server" PopupControlID="dvModalPresentarRecursos" TargetControlID="cmdPresentarRecursoHide" BehaviorID="mpeModalPresentarRecurso" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <div id="dvModalPresentarRecursos" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalFormularioNot">
        <rer:RegistrarRecursoReposicion runat="server" ID="ctrRegistrarRecursoReposicion" ValidationGroup="ModalPresentarRecurso" />
        <asp:UpdatePanel runat="server" ID="upnlModalPresentarRecursoBotones" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="TablaBotonesFormularioNotElec">
                    <tr>
                        <td>
                            <asp:Button ID="cmdEnviarPresentarRecurso" runat="server" Text="Enviar" CssClass="boton" CausesValidation="true" ValidationGroup="ModalPresentarRecurso" OnClick="cmdEnviarPresentarRecurso_Click" ClientIDMode="Static"/>
                            <asp:Button ID="cmdCancelarPresentarRecurso" runat="server" Text="Cancelar" CssClass="boton" CausesValidation="false" OnClick="cmdCancelarPresentarRecurso_Click" ClientIDMode="Static" />                            
                            <asp:ValidationSummary runat="server" ID="valModalPresentarRecurso" ValidationGroup="ModalPresentarRecurso" ShowSummary="false" ShowMessageBox="true" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>                
                <asp:AsyncPostBackTrigger ControlID="cmdEnviarPresentarRecurso" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="cmdCancelarPresentarRecurso" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>        
        <asp:UpdateProgress ID="uppModalPresentarRecursoBotones" runat="server" AssociatedUpdatePanelID="upnlModalPresentarRecursoBotones">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgUpdateProgresModalPresentarRecursoBotones" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

</asp:Content>
