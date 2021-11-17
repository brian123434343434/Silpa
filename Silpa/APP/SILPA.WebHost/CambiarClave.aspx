<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="CambiarClave.aspx.cs" Inherits="CambiarClave" Title="Cambiar Contraseña" %>

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

    <script type="text/javascript" language="Javascript" src="./js/JScript.js"></script>
    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="CAMBIAR CONTRASEÑA"
            SkinID="titulo_principal_blanco"></asp:Label>
    </div>

    <%--<div class="div-contenido">--%>
    <div class="table-responsive">
        <asp:ScriptManager ID="scmManejador" runat="server"></asp:ScriptManager>

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
                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario:" SkinID="etiqueta_negra"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtUsuario" runat="server" SkinID="texto" MaxLength="30"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ControlToValidate="txtUsuario"
                        ErrorMessage="El campo Usuario es requerido">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblClave" runat="server" Text="Contraseña:" SkinID="etiqueta_negra"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtClave" runat="server" SkinID="texto" MaxLength="30" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvClave" runat="server" ControlToValidate="txtClave"
                        ErrorMessage="El campo Contraseña es requerido">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNewClave" runat="server" Text="Contraseña Nueva:" SkinID="etiqueta_negra"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtNewClave" runat="server" SkinID="texto" MaxLength="30" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txtNewClave"
                        ErrorMessage="El campo clave es requerido">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" ID="cmpClave" ControlToValidate="txtNewClave"
                        ControlToCompare="txtClave" Operator="NotEqual" Type="String" ErrorMessage="La nueva contraseña debe ser diferente a la anterior">*</asp:CompareValidator>
                    <%--<asp:RegularExpressionValidator ID="revClave" runat="server" ControlToValidate="txtNewClave"
                        ErrorMessage="La contraseña: debe ser mínimo de 8 caracteres, alfanumérico con altas - bajas y  almenos un caracter especial" ValidationExpression="^.*(?=.{6,})(?=.*[a-z])(?=.*[A-Z])(?=.*[\d\W]).*$"
                        Width="2px">*</asp:RegularExpressionValidator>--%>
                 </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCofirmClave" runat="server" Text="Confirmar Contraseña Nueva:" SkinID="etiqueta_negra"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCofirmClave" runat="server" SkinID="texto" MaxLength="30" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvconfirmar" runat="server" ControlToValidate="txtCofirmClave"
                        ErrorMessage="El campo confirmar contraseña es requerido">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" ID="cmpConfirmar" ControlToValidate="txtCofirmClave"
                        ControlToCompare="txtNewClave" Operator="Equal" Type="String" ErrorMessage="No coincide la contraseña de confirmaciòn">*</asp:CompareValidator>
                </td>
            </tr>
        </table>
        <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
            <tr>
                <td style="padding: 10px; text-align: left; vertical-align: top;">
                    <asp:ValidationSummary ID="valResumenUsuario" runat="server" />
                    <asp:Label ID="lblErrorVal" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center; vertical-align: middle;">
                    <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                        <tr>
                            <td style="padding-top: 20px; padding-bottom: 10px; padding-left: 10px; padding-right: 20px; text-align: right; vertical-align: middle;">
                                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click"/>
                            </td>
                            <td style="padding-top: 20px; padding-bottom: 10px; padding-left: 10px; padding-right: 20px; text-align: right; vertical-align: middle;">
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                                    CausesValidation="False" 
                                    OnClick="btnCancelar_Click" 
                                    UseSubmitBehavior="False"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
