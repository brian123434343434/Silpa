<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/plantillas/SILPA.master" CodeFile="FormaCorporacion.aspx.cs" Inherits="Corporaciones_FormaCorporacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>
    <link href="css/Corporaciones.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript">
        window.history.forward();
    </script>

    <asp:ScriptManager ID="scmManager" runat="server"></asp:ScriptManager>

    <input type="button" runat="server" id="cmdErrorProcesoHide" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeErrorProceso" runat="server" PopupControlID="dvErrorProceso" TargetControlID="cmdErrorProcesoHide" BehaviorID="mpeErrorProcesos" BackgroundCssClass="ModalBackgroundCorporaciones">
    </cc1:ModalPopupExtender>
    <div id="dvErrorProceso" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalCorporaciones">
        <asp:UpdatePanel runat="server" ID="upnlErrorProceso" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="TablaFormularioCorporaciones">
                    <tr>
                        <td colspan="2" class="TituloSeccionCorporaciones">
                            VITAL
                        </td>
                    </tr>
                    <tr>
                        <td class="ImagenesModalCorporaciones">
                            <asp:Image runat="server" ID="imgIconoErrorProceso" ImageUrl="~/images/error.png" />
                        </td>
                        <td class="TextoModalErrorCorporaciones">
                            <asp:Literal runat="server" ID="ltlErrorProceso"></asp:Literal>
                        </td>
                    </tr>                        
                </table>
                <table class="TablaBotonesFormularioCorporaciones">
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="cmdAceptarErrorProceso" CausesValidation="false" Text="Aceptar" ClientIDMode="Static" OnClick="cmdAceptarErrorProceso_Click"/>
                            <asp:Button runat="server" ID="cmdAceptarErrorProcesoSincronico" CausesValidation="false" Text="Aceptar" ClientIDMode="Static" OnClick="cmdAceptarErrorProceso_Click"/>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdAceptarErrorProceso" EventName="Click" />                                                        
                <asp:PostBackTrigger ControlID="cmdAceptarErrorProcesoSincronico" />
            </Triggers>
        </asp:UpdatePanel>

        <asp:UpdateProgress ID="uppErrorProceso" runat="server" AssociatedUpdatePanelID="upnlErrorProceso">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgErrorProceso" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

</asp:Content>
