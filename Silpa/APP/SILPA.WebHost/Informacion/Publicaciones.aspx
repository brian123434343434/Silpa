<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/plantillas/SILPA.master"  MaintainScrollPositionOnPostback="true" ValidateRequest ="false"
    CodeFile="Publicaciones.aspx.cs" Inherits="Informacion_Publicaciones" Title="Publicaciones" %>

<%@ Register TagPrefix="CP" TagName="Publicaciones" Src="~/Informacion/ConsultaPublicacion.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>
    <link href="../Tramites/css/CertificadoUPME.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="PUBLICACIONES" SkinID="titulo_principal_blanco"></asp:Label>  
    </div>

    <%--<div class="div-contenido">--%>
    <div class="table-responsive">
        <asp:ScriptManager ID="scmManejador" runat="server" EnableScriptGlobalization="true">
        </asp:ScriptManager>       
        <CP:Publicaciones ID="Publicacion" runat= "server" /> 
    </div>

      <input type="button" runat="server" id="cmdMensajeTercero" style="display:none;" />
        <cc1:ModalPopupExtender ID="mpeMensajeTercero" runat="server" PopupControlID="dvMensajeTercero" TargetControlID="cmdMensajeTercero" BehaviorID="mpeMensajeTerceros" BackgroundCssClass="ModalBackgroundAutoliquidacion">
        </cc1:ModalPopupExtender>
        <div id="dvMensajeTercero" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalAutoliquidacion">
            <asp:UpdatePanel runat="server" ID="upnlMensajeTercero" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="DivTablaFormularioAutoliquidacion">
                        <asp:Image runat="server" ID="imgIconoMensajeOk" ImageUrl="~/App_Themes/Img/TercerInvtervinicente.JPG" CssClass="ImageFull"/>
                    </div>
                    
                    <%--<table class="TablaFormularioAutoliquidacion">
                        <tr>
                            <td colspan="2" class="TituloSeccionAutoliquidacion">
                                Tercer Interviniente
                            </td>
                        </tr>
                        <tr>
                            <td class="ModalImagenes">
                                
                            </td>
                            <td class="ModalTextoTerminos">
                                <asp:Literal runat="server" ID="ltlMensajeOk"></asp:Literal>
                            </td>
                        </tr>                        
                    </table>--%>
                    <table class="TablaBotonesFormularioAutoliquidacion">
                        <tr>
                            <td>
                                <asp:Button runat="server" ID="btnAceptarMensajeTercero" CausesValidation="false" Text="Aceptar" ClientIDMode="Static" OnClick="btnAceptarMensajeTerceo_Click"/>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAceptarMensajeTercero" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>

            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upnlMensajeTercero">
                <ProgressTemplate>  
                    <div id="ModalProgressContainer">
                        <div>
                            <p>Procesando...</p>
                            <p><asp:Image ID="imgMensajeOkProcesando" runat="server" SkinId="procesando"/></p>
                        </div>
                    </div>                         
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>


</asp:Content>