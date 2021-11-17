<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ConsultaSalvoconductoCiudadano.aspx.cs" Inherits="Salvoconducto_SeguiminetoRutasSalvoconducto" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <script src="../jquery/jquery.js"></script>
    <link href="../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../jquery/EstiloGris/jquery-ui.js"></script>
    <script src="../jquery/jquery.numeric.js"></script>
    <link href="../App_Themes/skin/StyleREDDS.css" rel="stylesheet" />
     <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css" />

   <style type ="text/css">
        .CentrarTexto{
            text-align:center;
        }

        .FormatoTexto{
            font-weight:bold;
            color: #31708f;
            border-color: #bce8f1;
        }

        .AlinearDescripcion
        {
            text-align:center;
            vertical-align:central;
            width:130px;
            font-weight:bold;
            color: #31708f;
            border-color: #bce8f1;
        }


        .alinearTitulos{
            text-align:center;
            vertical-align:central;
            width:130px;
            font-weight:bold;
            color: #31708f;
            border-color: #bce8f1;
            background-color: #d9edf7;
            vertical-align:middle !important;

        }

        .alinearSubTitulos{
            text-align:center;
            vertical-align:central;
            font-weight:bold;
            background-color: #d9edf7;
            color: #31708f;
            vertical-align:middle !important;
        }

        .alinearTexto{
            text-align:center;
            vertical-align:central;
            font-weight:bold;
        }
        
        .AnchoAltoCheck{
            Width:20px;
            Height:20px;
        }


        </style>

    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="Consulta Salvoconducto" SkinID="titulo_principal_blanco"></asp:Label>
        &nbsp;
    <a href="#" id="hrCerrarVentana" visible="false" runat="server" onclick="window.close();return false;">Salir</a>
    </div>
        <div class="div-contenido">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableHistory ="false">
        </asp:ScriptManager>
            <div class="contact_form" id="dContactForm">
                <asp:UpdatePanel ID="UpdDatosBasicos" runat="server">
                    <ContentTemplate>
                        <table width="50%" border="0">
                            <tr style="width: 100px">
                                <td style="text-align: left; vertical-align: middle">
                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 150px;" for="TxtNumeroSalvoconducto">Numero Salvoconducto:</label>
                                </td>
                                <td style="text-align: left; vertical-align: middle;">
                                    <asp:TextBox ID="TxtNumeroSalvoconducto" runat="server" MaxLength="150" Width="220px"></asp:TextBox><asp:RequiredFieldValidator ID="RFVNumeroSalvoconducto" Display="Dynamic" runat="server" ControlToValidate="TxtNumeroSalvoconducto" ValidationGroup="seguimiento" ErrorMessage="Debe Ingresar Numero Salvoconducto">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
<%--                            <tr>
                                <td style="text-align: left; vertical-align: middle;">
                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 150px;" for="TxtNumeroSalvoconducto">Codigo Seguridad:</label>
                                </td>
                                <td style="text-align: left; vertical-align: middle;">
                                    <asp:TextBox ID="TxtCodigoSeguridad" runat="server" MaxLength="50" Width="220px" Style="text-transform: uppercase"></asp:TextBox><asp:RequiredFieldValidator ID="RFVCodigoSeguridad" Display="Dynamic" runat="server" ControlToValidate="TxtCodigoSeguridad" ValidationGroup="seguimiento" ErrorMessage="Debe Codigo Seguridad Salvoconducto">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>

                            <tr>
                                <td style="text-align: left; vertical-align: middle;">
                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 150px;" for="TxtNumeroSalvoconducto">Documento Solicitante:</label>
                                </td>
                                <td style="text-align: left; vertical-align: middle;">
                                    <asp:TextBox ID="TxtDocumentoSolicitante" runat="server" MaxLength="20" Width="220px"></asp:TextBox><asp:RequiredFieldValidator ID="RFVDocumentoSolicitante" Display="Dynamic" runat="server" ControlToValidate="TxtDocumentoSolicitante" ValidationGroup="seguimiento" ErrorMessage="Debe Ingresar el documento del titular del salvoconducto">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>

                            <tr>
                                <td style="text-align: left; vertical-align: middle;">
                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 150px;" for="ImgCaptcha">CAPTCHA:</label>
                                </td>
                                <td>
                                     <asp:Image ID="ImgCaptcha" runat="server" Height="55px" ImageUrl="~/CaptchaNew.aspx" Width="220px" BorderColor="#666666" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; vertical-align: middle;">
                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 150px;" for="txtCodigoVerificacion">Codigo Captcha:</label>
                                </td>
                                <td>
                                     <asp:TextBox runat="server" ID="txtCodigoVerificacion"></asp:TextBox><asp:RequiredFieldValidator ID="RfvCaptcha" Display="Dynamic" runat="server" ControlToValidate="txtCodigoVerificacion" ValidationGroup="seguimiento" ErrorMessage="Debe digitar el valor captcha">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>

                            <tr>
                                <td align="left" colspan="2">
                                    <asp:Button ID="BtnValidarSalvoconducto" runat="server" Text="Validar" ValidationGroup="seguimiento" OnClick="BtnValidarSalvoconducto_Click" />
                                    <asp:Button ID="BtnNuevaBusqueda" runat="server" Text="Nueva Busqueda" ValidationGroup="seguimiento" OnClick="BtnNuevaBusqueda_Click" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:ValidationSummary ID="valResumenUsuario" runat="server" ValidationGroup="seguimiento" DisplayMode="List" ShowSummary="true" />
                                    <asp:Label runat="server" ID="lblCaptchaMessage" Style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: red; width: 150px;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
    </div>

</asp:Content>