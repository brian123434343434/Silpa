<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPASinMenu.master" AutoEventWireup="true" CodeFile="Salir.aspx.cs" Inherits="Salir" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="div-contenido">

    <asp:Label ID="lblMensaje" runat="server" Text="Su sesión ha Terminado" SkinID="etiqueta_negra"></asp:Label>
    <br />
    <br />
    
    <asp:HyperLink ID="hlk_inicio" runat="server" Text="Regresar a Página Principal"></asp:HyperLink>
</div>
</asp:Content>

