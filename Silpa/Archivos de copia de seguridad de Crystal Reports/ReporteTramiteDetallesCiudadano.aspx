<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReporteTramiteDetallesCiudadano.aspx.cs" Inherits="ReporteTramite_ReporteTramiteDetallesCiudadano" MasterPageFile="~/ReporteTramite/ResourcesCP/master/ConsultaPublicaSILPA.master" %>

<%@ Register TagPrefix="CP" TagName="MisTramites" Src="~/ReporteTramite/ReporteTramiteDetalles.ascx" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="ResourcesCP/CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="ResourcesCP/jquery/3.2.1/jquery.min.js" type="text/javascript"></script>
    <script src="ResourcesCP/JS/bootstrap.min.js"></script>
    <link href="ResourcesCP/jquery/jquery-ui.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="input-group input-group-lg">
        <span class="input-group-btn">
            <button title="Inicio" id="homeIcon" class="btn btn-default btn-group-lg" type="button" onclick="location.href ='../../ventanillasilpa/';"><span class="glyphicon glyphicon-home pull-right"></span></button>
        </span>
    </div>
    <div class="div-titulo">
        <asp:Label ID="Label11" runat="server" SkinID="titulo_principal" Text="Estado de Trámite"></asp:Label>
        <br />
        <br />
    </div>
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <CP:MisTramites ID="FormDetalles" runat="server" />
    </div>
</asp:Content>

