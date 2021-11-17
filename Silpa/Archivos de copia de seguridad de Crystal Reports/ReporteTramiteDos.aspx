<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ReporteTramiteDos.aspx.cs" Inherits="ReporteTramiteDos" Title="Mis Trámites" %>
<%@ Register TagPrefix="CP" TagName="MisTramites" Src="~/ReporteTramite/ReporteTramiteDetalles.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>    
     <div class="div-titulo">
    <asp:Label ID="Label11" runat="server" SkinID="titulo_principal_blanco" Text="Estado de Trámite"></asp:Label>
    </div>
    <div>
    <CP:MisTramites ID="MisTramites" runat="server" />
    </div>        
</asp:Content>

