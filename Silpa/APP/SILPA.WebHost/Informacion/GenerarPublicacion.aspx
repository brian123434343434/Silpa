<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="GenerarPublicacion.aspx.cs" Inherits="Informacion_GenerarPublicacion"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="div-titulo">
        <asp:Label ID="lbl_titulo_principal" SkinID="titulo_principal_blanco" runat="server"
            Text="INFORMACIÓN DE PUBLICACIÓN"></asp:Label>
    </div>
    <div class="div-contenido">
        <table style="width: 350px; height: 30px; left: 184px;">
            <tr>
                <td colspan="2" style="text-align: left; height: 30px;">
                    <asp:Label ID="lblTipoPublicacion" runat="server" SkinID="etiqueta_negra" Text="Tipo de Comunicación:"></asp:Label>
                </td>
                <td style="width: 67px; text-align: left; height: 30px;">
                    <asp:DropDownList ID="cboTipoPublicacion" runat="server" SkinID="lista_desplegable"
                        Width="200px">
                        <asp:ListItem Value="0">Activa</asp:ListItem>
                        <asp:ListItem Value="1">Pasiva</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left;">
                    <asp:Label ID="lblVigencia" SkinID="etiqueta_negra" runat="server" Text="Tipo de Publicación:"></asp:Label>
                </td>
                <td style="width: 67px; text-align: left;">
                    <asp:DropDownList ID="cboVigencia" runat="server" Width="200px" SkinID="lista_desplegable">
                        <asp:ListItem Value="0">Fijaci&#243;n</asp:ListItem>
                        <asp:ListItem Value="1">Publicaci&#243;n</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left;">
                    <asp:Label ID="Label1" SkinID="etiqueta_negra" runat="server" Text="Tipo de Trámite:"></asp:Label>
                </td>
                <td style="width: 67px; text-align: left;">
                    <asp:DropDownList ID="cboTramite" runat="server" Width="200px" SkinID="lista_desplegable">
                        <asp:ListItem Value="0">Licencia Ambiental</asp:ListItem>
                        <asp:ListItem Value="1">Permiso de Concesión de Aguas Superficiales</asp:ListItem>
                        <asp:ListItem Value="2">Permiso de Concesión de Aguas Subterráneas</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <table style="width: 514px">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblSeleccion" runat="server" Text="Seleccione los perfiles que tendrán acceso a la publicación"
                        SkinID="etiqueta_negra"></asp:Label>
                    <listadoble:lista ID="lista" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="btnAceptar" runat="server" SkinID="boton" OnClick="btnAceptar_Click" Text="Aceptar" />
                </td>
                <td style="text-align: center">
                    <asp:Button ID="btnCancelar" SkinID="boton" runat="server" Text="Cancelar" />
                </td>
            </tr>
        </table>
        <br />
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
    </div>
</asp:Content>
