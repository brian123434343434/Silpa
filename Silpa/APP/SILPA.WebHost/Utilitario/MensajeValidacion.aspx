<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="MensajeValidacion.aspx.cs" Inherits="Utilitario_MensajeValidacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>
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
        .Button{
            background-color: #ddd;
        }
    </style>

    <div style="text-align: center; vertical-align: middle; line-height: 4em; width: 100%;
        height: 50px; background-image: url(../App_Themes/Img/iconos/titulo.bmp);
        background-repeat: no-repeat; background-position: center">
        <asp:Label ID="lbl_titulo_principal" runat="server" Text="Error al Validar el Token de inicio de sesion"
            SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="div-contenido">
        <asp:Label ID="LblMensaje" runat="server" Height="45px" Text="Label" Width="811px"></asp:Label>
        <asp:Button ID="BtnBack" runat="server" OnClick="BtnBack_Click" Text="Aceptar" /></div>
</asp:Content>
