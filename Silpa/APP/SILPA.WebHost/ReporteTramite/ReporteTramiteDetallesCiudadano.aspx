<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReporteTramiteDetallesCiudadano.aspx.cs" Inherits="ReporteTramite_ReporteTramiteDetallesCiudadano" MasterPageFile="~/ReporteTramite/ResourcesCP/master/ConsultaPublicaSILPA.master" %>--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReporteTramiteDetallesCiudadano.aspx.cs" Inherits="ReporteTramite_ReporteTramiteDetallesCiudadano" MasterPageFile="~/plantillas/SILPABuscador.master" %>

<%@ Register TagPrefix="CP" TagName="MisTramites" Src="~/ReporteTramite/ReporteTramiteDetalles.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<link href="ResourcesCP/CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="ResourcesCP/jquery/3.2.1/jquery.min.js" type="text/javascript"></script>
    <script src="ResourcesCP/JS/bootstrap.min.js"></script>
    <link href="ResourcesCP/jquery/jquery-ui.css" rel="stylesheet" />--%>
    <link rel="stylesheet" type="text/css" href="../Resources/Buscador/css/buscadorVITAL.css" />
    
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/jquery.datetimepicker.css" rel="stylesheet" />

    <script src='<%= ResolveClientUrl("~/Resources/jquery/1.11.2/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/jquery/fontsize/js/jquery.jfontsize-1.0.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/5.0.1/js/bootstrap.min.js") %>' type="text/javascript"></script>
    
   <%-- <div class="input-group input-group-lg">
        <span class="input-group-btn">
            <button title="Inicio" id="homeIcon" class="btn btn-default btn-group-lg" type="button" onclick="location.href ='../../ventanillasilpa/';"><span class="glyphicon glyphicon-home pull-right"></span></button>
        </span>
    </div>--%>
    <div class="titulo_pagina">
        <asp:Label ID="Label11" runat="server" SkinID="titulo_principal" Text="Estado de Trámite"></asp:Label>
    </div>
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <CP:MisTramites ID="FormDetalles" runat="server" />
    </div>
</asp:Content>

