<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPASinMenuFlash.master" AutoEventWireup="true" CodeFile="RegistroActivarUsuario.aspx.cs" Inherits="RegistroActivarUsuario" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="scmManager" runat="server"></asp:ScriptManager>

    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="REGISTRO DE USUARIO - ACTIVACIÓN" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <asp:UpdatePanel runat="server" ID="upnlRegistroUsuario" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="TableResultadoModalNot">  
                <div class="RowResultadoModalNot">
                    <div class="CellResultadoModalNot">
                        <div class="TableFormularioNot">
                            <div class="RowFormularioNot">
                                <div class="CellFormularioNot">
                                    <div class="TableMensajesNot">
                                        <div class="RowMensajesNot">
                                            <div class="CellMensajesNot"><asp:Image runat="server" ID="imgActivacionUsuario" ImageUrl="~/App_Themes/Img/chulo_verde.png" Width="39px"/></div>
                                            <div class="CellMensajesNot"><asp:Literal runat="server" ID="ltlMensajeActivacionUsuario"></asp:Literal></div>
                                        </div>                                            
                                    </div>
                                </div>
                            </div>                               
                        </div>
                    </div>
                </div>
                <div class="RowResultadoModalNot">
                    <div class="CellButtonModal">
                        <asp:Button ID="cmdAceptarResultadoDatosPersona" runat="server" ClientIDMode="Static" Text="Aceptar" CssClass="boton" CausesValidation="false" OnClick="cmdAceptarResultadoDatosPersona_Click" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cmdAceptarResultadoDatosPersona" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="uppModalResultadoDatosPersona" runat="server" AssociatedUpdatePanelID="upnlRegistroUsuario">
        <ProgressTemplate>  
            <div id="ModalProgressContainer">
                <div>
                    <p>Procesando...</p>
                    <p><asp:Image ID="imgUpdateProgresModalResultadoDatosPersona" runat="server" SkinId="procesando"/></p>
                </div>
            </div>                         
        </ProgressTemplate>
    </asp:UpdateProgress>

</asp:Content>