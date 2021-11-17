<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="PublicacionesExterno.aspx.cs" Inherits="Informacion_PublicacionesExterno" Title="Publicaciones" %>
<%@ Register TagPrefix="CP" TagName="Publicaciones" Src="~/Informacion/ConsultaPublicacion.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <asp:ScriptManager ID="scmManejador" runat="server" EnableScriptGlobalization="true">
        </asp:ScriptManager>       
        <CP:Publicaciones ID="Publicacion" runat= "server" /> 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
</asp:Content>

