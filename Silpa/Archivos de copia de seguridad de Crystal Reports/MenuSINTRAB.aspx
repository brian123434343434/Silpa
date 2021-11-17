<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/plantillas/SILPASinMenuFlash.master" CodeFile="MenuSINTRAB.aspx.cs" Inherits="SINTRAB_MenuSINTRAB" %>


<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    	<meta http-equiv="content-type" content="text/html; charset=utf-8" />
	<meta name="viewport" content="initial-scale=1.0">
 <link href="../App_Themes/skin/EstiloMenuSINTRAB.css" rel="stylesheet" />
  <style type="text/css">._css3m{display:none}</style>
</asp:Content>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="Accesos Menu SINTRAB" SkinID="titulo_principal_blanco"></asp:Label>
        &nbsp;
        <a href="#" id="hrCerrarVentana" visible="false" runat="server" onclick="window.close();return false;">Salir</a>
    </div>


    <div id="menu">
         <input type="checkbox" id="css3menu-switcher" class="c3m-switch-input">
         <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
    </div>

     <link href="../App_Themes/skin/EstiloMenuSINTRAB.css" rel="stylesheet" />

</asp:Content>

