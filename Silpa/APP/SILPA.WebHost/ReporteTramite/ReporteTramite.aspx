<%@ Page Language="C#" MasterPageFile="~/ReporteTramite/ResourcesCP/master/ConsultaPublicaSILPA.master" AutoEventWireup="true" Theme="skin"
    CodeFile="ReporteTramite.aspx.cs" Inherits="ReporteTramite" Title="Mis Tramites" %>

<%@ Register TagPrefix="CP" TagName="MisTramites" Src="~/ReporteTramite/MisTramites.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" runat="Server">
    
    <link href="ResourcesCP/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="ResourcesCP/jquery/jquery-ui.css" rel="stylesheet" />
    <script src="ResourcesCP/jquery/3.2.1/jquery.min.js" type="text/javascript"></script>
    <script src="ResourcesCP/3.3.7/js/bootstrap.min.js"></script>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        table {
            /*border: 1px solid #000;*/
        }

            table tr td {
                border: 0px solid #ddd !important;
                padding: 4px;
            }

        .Button {
            background-color: #ddd;
        }

    </style>
    <div class="input-group input-group-lg">
        <span class="input-group-btn">
            <button title="Inicio" id="homeIcon" class="btn btn-default btn-group-lg" type="button" onclick="location.href ='../../ventanillasilpa/';"><span class="glyphicon glyphicon-home pull-right"></span></button>
        </span>
    </div>
    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" SkinID="titulo_principal_blanco" Text="CONSULTA DE REPORTE DE TRÁMITE"></asp:Label>
    </div>
    <div class="stilesLarge">
        <div>
            <CP:MisTramites ID="idMisTramites" runat="server" />
        </div>
    </div>
</asp:Content>
