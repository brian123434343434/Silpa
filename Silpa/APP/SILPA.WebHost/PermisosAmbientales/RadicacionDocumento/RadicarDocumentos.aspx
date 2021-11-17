<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RadicarDocumentos.aspx.cs" Inherits="LicenciasAmbientales_LPA_05_Anexar_Documentacion_Soporte_Solicitante" MasterPageFile="~/plantillas/SILPA.master" enableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="div-titulo">
            <asp:Label ID="lbl_titulo_principal" SkinID="titulo_principal_blanco" runat="server" Text="SOLICITUD DE PERMISOS"></asp:Label>
</div>
<div class="div-contenido">
<p><asp:Label ID="LBL_REG_INF_RAD" runat="server" Text="Adjuntar documentos requeridos" Font-Bold="True" SkinID="titulo_principal"></asp:Label></p>
        <table id="Tabla1">           
            <tr>
                <td colspan="1" style="width: 339px; height: 19px;">
                    &nbsp;</td>
                <td colspan="1" style="width: 258px; height: 19px;">
                    </td>
                <td colspan="1" style="width: 210px; height: 19px;">
                    </td>
                <td colspan="4" style="height: 19px">
                    </td>
            </tr>
            <tr>
                <td align="right" rowspan="1" style="width: 339px">
                </td>
                <td align="left" rowspan="1" style="width: 258px">
                </td>
                <td rowspan="1" style="width: 210px">
                </td>
                <td rowspan="1" style="width: 149px">
                </td>
            </tr>
            <tr>
                <td style="width: 339px" align="right">
                    &nbsp;<asp:Label ID="Label1" runat="server" Text="Número SILPA" SkinID="etiqueta_negra"></asp:Label></td>
                <td style="width: 258px" align="left">
                    <asp:TextBox ID="txtNumeroSilpa" runat="server" Width="200px" SkinID="texto"></asp:TextBox></td>
                <td style="width: 210px">
                    </td>
                <td style="width: 149px">
                    </td>
            </tr>
            <tr>
                <td style="width: 339px; height: 21px" align="right">
                    &nbsp;<asp:Label ID="Label2" runat="server" Text="Acto Administrativo " SkinID="etiqueta_negra"></asp:Label></td>
                <td style="width: 258px; height: 21px" align="left">
                    <asp:TextBox
                        ID="txtActoAdministrativo" runat="server" Width="200px" SkinID="texto"></asp:TextBox></td>
                <td style="width: 210px; height: 21px">
                </td>
                <td style="width: 149px; height: 21px">
                </td>
            </tr>
            <tr>
                <td rowspan="1" style="width: 339px" align="right">
                <asp:Label ID="Label6" runat="server" Text="Numero RadicadoAA" Width="116px" SkinID="etiqueta_negra"></asp:Label></td>
                <td style="width: 258px" align="left">
                    <asp:TextBox ID="txtRadicadoAA" runat="server" Width="200px" SkinID="texto"></asp:TextBox></td>
                <td rowspan="1" style="width: 210px">
                </td>
                <td rowspan="1" style="width: 149px">
                </td>
            </tr>
            <tr>
                <td align="right" rowspan="1" style="width: 339px; height: 28px">
                    <asp:Label ID="LBL_ADJ_DOC" runat="server" Text="Adjuntar Documento:" Font-Bold="True" SkinID="etiqueta_negra"></asp:Label></td>
                <td style="height: 28px" colspan="3">
                    <asp:FileUpload ID="fldRadicarDocumento" runat="server" Width="292px" /></td>
            </tr>
            <tr>
                <td rowspan="1" style="width: 339px; height: 18px">
                </td>
                <td style="width: 258px; height: 18px">
        <asp:Button ID="btnRadicarDocumento" runat="server" Text="Radicar Documento" SkinID="boton" OnClick="btnRadicarDocumento_Click" Width="290px" /></td>
                <td rowspan="1" style="width: 210px; height: 18px">
                </td>
                <td rowspan="1" style="width: 149px; height: 18px">
                </td>
            </tr>
            <tr>
                <td colspan="4" rowspan="1">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" rowspan="1">
                </td>
            </tr>
            <tr>
                <td rowspan="1" style="width: 339px; height: 19px;">
        </td>
                <td style="width: 258px; height: 19px;">
                    &nbsp;</td>
                <td rowspan="1" style="width: 210px; height: 19px;">
                    </td>
                <td rowspan="1" style="width: 149px; height: 19px;">
                    </td>
            </tr>
            <tr>
                <td rowspan="1" style="width: 339px;">
                    <asp:Label ID="LBL_NUM_RAD" runat="server" Text="Número de Radicación:" Visible="False" SkinID="etiqueta_negra"></asp:Label>
                    <asp:Label ID="Label3" runat="server" Text="Ciudad Corporacion" Width="116px" Visible="False" SkinID="etiqueta_negra"></asp:Label>
                    <asp:TextBox ID="txtNumeroRadicacion" runat="server" Width="135px" Visible="False" SkinID="texto"></asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:TextBox ID="txtCiudadCorporacion" SkinID="texto" runat="server" Width="79px" Visible="False"></asp:TextBox>
                    <asp:Label ID="Label5" runat="server" SkinID="etiqueta_negra" Text="Nombre Solicitante" Width="113px" Visible="False"></asp:Label><asp:TextBox ID="txtNombreSolicitante" runat="server" Width="176px" Visible="False" SkinID="texto"></asp:TextBox><asp:Label ID="Label7" runat="server" Text="Ubicacion Documento" Width="128px" Visible="False" SkinID="etiqueta_negra"></asp:Label><asp:TextBox
                        ID="txtUbicacionDocumento" SkinID="texto" runat="server" Width="176px" Visible="False"></asp:TextBox></td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Nit Corporacion" Width="91px" Visible="False"></asp:Label><asp:TextBox ID="txtNitCorporacion" runat="server" Width="176px" Visible="False"></asp:TextBox><asp:Label ID="Label10" runat="server" Text="Nombre Corporacion" Width="116px" Visible="False"></asp:Label><asp:TextBox ID="txtNombreCorporacion" runat="server" Width="176px" Visible="False"></asp:TextBox><asp:Label ID="Label9" runat="server" Text="Direccion Corporacion" Width="127px" Visible="False"></asp:Label><asp:TextBox ID="txtDireccionCorporacion" runat="server" Width="176px" Visible="False"></asp:TextBox><asp:Label ID="Label11" runat="server" Text="Identificacion Solicitante" Width="135px" Visible="False"></asp:Label><asp:TextBox ID="txtIdentificacionSolicitante" runat="server" Width="176px" Visible="False"></asp:TextBox><asp:Label ID="Label8" runat="server" Text="Telefono Corporacion" Width="122px" Visible="False"></asp:Label><asp:TextBox
                        ID="txtTelefonoCorporacion" runat="server" Width="176px" Visible="False"></asp:TextBox></td>
                <td rowspan="1">
                </td>
                <td rowspan="1">
                </td>
            </tr>
        </table>
        <br />
</div>	       

</asp:Content>