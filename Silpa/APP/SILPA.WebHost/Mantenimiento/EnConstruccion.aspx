<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPASinMenu.master" AutoEventWireup="true" CodeFile="EnConstruccion.aspx.cs" Inherits="Mantenimiento_EnConstruccion" Title="Página en Construcción" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="div-titulo">
        <asp:Label ID="Label1" runat="server" Text="PAGINA EN CONSTRUCCION" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <center>
        <div class="div-contenido">
            <img src="../App_Themes/Img/EnConstruccion.jpg" width="175" height="175" />
        </div> 
    </center>
    
</asp:Content>
