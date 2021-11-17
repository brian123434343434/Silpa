<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ReestablecerClave.aspx.cs" Inherits="ReestablecerClave" Title="Reestablecer Contraseña" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
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

        .divWaiting
        {
	        background-color:Gray;
            /*background-color: #FAFAFA;*/
	        filter:alpha(opacity=70);
	        /*opacity:0.7;*/
            position: absolute;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            text-align: center; top: 0; left: 0;
            height: 100%;
            width: 100%;
            padding-top:20%;
        } 
    </style>

    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="¿OLVIDÓ SU CONTRASEÑA?" SkinID="titulo_principal_blanco"></asp:Label>
    </div>

    <%--<div class="div-contenido">--%>
    <div class="table-responsive">
        <asp:ScriptManager ID="scmManejador" runat="server">
        </asp:ScriptManager>

        <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
            <tr>
                <td colspan="2" class="titleUpdate"></td>
            </tr>
            <tr>
                <th colspan="2">Información del Usuario</th>
            </tr>
            <tr>
                <td colspan="2" class="titleUpdate"></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario:" SkinID="etiqueta_negra"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUsuario" runat="server" SkinID="texto" MaxLength="30"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ControlToValidate="txtUsuario"
                        ErrorMessage="El campo usuario es requerido">*</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
            <tr>
                <td style="padding: 10px; text-align: left; vertical-align: top;">
                    <asp:ValidationSummary ID="valResumenUsuario" runat="server" />
                    <asp:Label ID="lblError" runat="server" Text="" SkinID="etiqueta_roja_error"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center; vertical-align: middle;">
                    <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                        <tr>
                            <td style="padding-top: 20px; padding-bottom: 10px; padding-left: 10px; padding-right: 20px; text-align: right; vertical-align: middle;">
                                <asp:Button ID="btnResClave" SkinID="boton_copia" runat="server" Text="Restablecer contraseña" OnClick="btnResClave_Click" />
                            </td>
                            <td style="padding-top: 20px; padding-bottom: 10px; padding-left: 10px; padding-right: 20px; text-align: right; vertical-align: middle;">
                                <asp:Button ID="btnCancelar" SkinID="boton_copia" runat="server" Text="Cancelar"
                                    CausesValidation="False" 
                                    OnClientClick="javascript:history.back();" 
                                    OnClick="btnCancelar_Click"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
