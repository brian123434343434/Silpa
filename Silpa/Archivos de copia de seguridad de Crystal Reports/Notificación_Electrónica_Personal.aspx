<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="Notificación_Electrónica_Personal.aspx.cs" Inherits="LicenciasAmbientales_NotificacionElectronica_Notificación_Electrónica_Personal"
    Title="Untitled Page" %>

<%@ Register Src="~/controles/ctlNotificacionElectronicaPersonal.ascx" TagName="Ctl_Notificacion_Electronica_Personal"
    TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="div-titulo">
        <asp:Label ID="lbl_titulo_principal" runat="server" Text="NOTIFICACIÓN ELECTRÓNICA PERSONAL"
            SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="div-contenido">
        <%--BODY--%>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br />
        &nbsp;&nbsp;
        <uc2:Ctl_Notificacion_Electronica_Personal ID="Ctl_Notificacion_Electronica_Personal1"
            runat="server"></uc2:Ctl_Notificacion_Electronica_Personal>
        <iframe src="../documentos/294190_2008318LIQUIDACION_REF.pdf" width="600" height="500"
            scrolling="auto" frameborder="1" />
    </div>
</asp:Content>
