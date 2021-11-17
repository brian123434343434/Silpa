<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="Fijacion.aspx.cs" Inherits="Informacion_Fijacion" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="div-titulo">
        <asp:Label ID="lbl_titulo_principal" SkinID="titulo_principal_blanco" runat="server"
            Text="FIJACIÓN"></asp:Label>
    </div>
    <div class="div-contenido">
        <asp:ScriptManager ID="scmManejador" runat="server" EnableScriptGlobalization="true">
        </asp:ScriptManager>
        <br />
        <table style="width: 547px; height: 45px">
            <tr>
                <td style="width: 287px">
                    <asp:Label ID="lblTipoDocumento" SkinID="etiqueta_negra" runat="server" Text="Tipo de Documento:"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:DropDownList SkinID="lista_desplegable" ID="cboTipoDocumento" runat="server" Width="205px">
                        <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                        <asp:ListItem Value="1">Auto</asp:ListItem>
                        <asp:ListItem Value="2">Resoluci&#243;n</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 287px">
                    <asp:Label ID="lblNumeroDocumento" runat="server" SkinID="etiqueta_negra" Text="Número de Documento:"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtNumeroDocumento" SkinID="texto" runat="server" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 287px">
                    <asp:Label ID="lblFechaExpedicionDocumento" runat="server" SkinID="etiqueta_negra" Text="Fecha de Expedición del Documento:"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtFechaExpedicion" SkinID="texto" runat="server" Width="200px"></asp:TextBox>
                    <cc1:CalendarExtender ID="calFechaExpedicion" runat="server" TargetControlID="txtFechaExpedicion"
                        Format="yyyy/MM/dd">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 287px">
                    <asp:Label ID="lblDescripcionDocumento" SkinID="etiqueta_negra" runat="server" Text="Descripción del Documento:"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtDescripcionDocumento" runat="server" SkinID="texto" TextMode="MultiLine" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 287px">
                    <asp:Label ID="lblFechaFijación" runat="server" Text="Fecha de Fijación:" SkinID="etiqueta_negra"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtFechaFijacion" runat="server" SkinID="texto" Width="200px"></asp:TextBox>
                    <cc1:CalendarExtender ID="calFechafijacion" runat="server" TargetControlID="txtFechaFijacion"
                        Format="yyyy/MM/dd">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 287px">
                    <asp:Label ID="lblFechaDesfijacion" runat="server" Text="Fecha de Desfijación:" SkinID="etiqueta_negra"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtFechaDesfijacion" runat="server" SkinID="texto" Width="200px"></asp:TextBox>
                    <cc1:CalendarExtender ID="calFechaDesfijacion" runat="server" TargetControlID="txtFechaDesfijacion"
                        Format="yyyy/MM/dd">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 287px">
                    <asp:Label ID="lblAdjuntarDocumento" runat="server" Text="Documento Adjunto:" SkinID="etiqueta_negra"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:FileUpload ID="uplAdjuntarDocumento" runat="server" Width="210px" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table style="width: 548px">
            <tr>
                <td style="width: 284px; text-align: center">
                    <asp:Button ID="btnAceptar" runat="server" SkinID="boton" Text="Aceptar" 
                        OnClientClick="return alert('Se fijó el Documento')" 
                        onclick="btnAceptar_Click" />
                </td>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="btnCancelar" SkinID="boton" runat="server" Text="Cancelar" />
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
