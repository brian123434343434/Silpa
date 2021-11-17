<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrficha10Insersion.ascx.cs" Inherits="ResumenEIA_Fichas_ctrficha10Insersion" %>
<style type="text/css">
    .style1
    {
        width: 575px;
        text-align: left;
    }
</style>
<table style="width:100%;">
                <tr>
                    <td colspan="3">
                        <asp:Label ID="lblEtiqueta" runat="server" Text="Label" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="lblPrograma" runat="server" Text="Programa" Enabled="False" 
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td class="style1">
                        <asp:TextBox ID="txtPrograma" runat="server" Width="472px" Enabled="False" 
                            SkinID="texto_sintamano"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="lblElementosSeguimiento" runat="server" 
                            Text="Elementos de Seguimento" Enabled="False" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td class="style1" >
                        <asp:TextBox ID="txtElementosSeguimiento" runat="server" Width="472px" 
                            Enabled="False" SkinID="texto_sintamano"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="lblUbicacion" runat="server" Text="Ubicacion" Enabled="False" 
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td class="style1" >
                        <asp:TextBox ID="txtUbicacion" runat="server" Width="472px" Enabled="False" 
                            SkinID="texto_sintamano"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="lblActividad" runat="server" Text="Actividad a Realizar" 
                            Enabled="False" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td class="style1" >
                        <asp:TextBox ID="txtActividad" runat="server" Width="472px" Enabled="False" 
                            SkinID="texto_sintamano"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="lblIndicadores" runat="server" 
                            Text="Indicadores de Seguimiento y Monitoreo" Enabled="False" 
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td class="style1" >
                        <asp:TextBox ID="txtIndicadores" runat="server" Width="472px" Enabled="False" 
                            SkinID="texto_sintamano"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td >
                        &nbsp;</td>
                    <td >
                        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" Enabled="False" 
                            SkinID="boton_copia" onclick="btnRegistrar_Click" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>