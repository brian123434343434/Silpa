<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrFicha5Insersion.ascx.cs" Inherits="ResumenEIA_Fichas_ctrFicha5Insersion" %>


<style type="text/css">
    .style1
    {
        width: 800px;
    }
    </style>


<table bordercolor="#E6E6E6" >
    <tr>
        <td colspan="2" style="text-align: center; ">
            &nbsp;</td>
        <td style="text-align: center; " class="style1" >
            &nbsp;</td>
        <td style="text-align: center; " >
            &nbsp;</td>
    </tr>
    <tr>
        <td  valign="top" >
                        <asp:Label ID="lblCodigoMapa" runat="server" SkinID="etiqueta_negra" 
                            Text="Codigo del Mapa" Visible="False" Width="150px"></asp:Label>
                        <asp:Label ID="lblNro" runat="server" SkinID="etiqueta_negra" Text="Codigo" 
                            Visible="False"></asp:Label>            
                        </td>
        <td  valign="top" >
                            <asp:TextBox ID="txtCodigoMapa" runat="server" SkinID="texto_sintamano" 
                                Visible="False"></asp:TextBox>
                            <asp:TextBox ID="txtCodigo" runat="server" SkinID="texto_sintamano" 
                                Visible="False"></asp:TextBox>
                            </td>
        <td  valign="top" class="style1" >
                            <asp:Label ID="lblUnidadIntervenir" runat="server" SkinID="etiqueta_negra" 
                                Text="Unidad Geotecnica a intervenir" Width="200px"></asp:Label>
                            <asp:Label ID="lblFuentes" runat="server" SkinID="etiqueta_negra" 
                                Text="Fuentes de Emisiones de Gases, Vapores o Ruido" Visible="False" 
                                Width="200px"></asp:Label>
                            <asp:Label ID="lblcomponenteAfectado" runat="server" SkinID="etiqueta_negra" 
                                Text="Componente a ser afectado" Visible ="False" Width="168px"></asp:Label>
                            
                            <asp:Label ID="lblDimension" runat="server" SkinID="etiqueta_negra" 
                                Text="Dimension socioeconomica" Visible="False" Width="166px"></asp:Label>
                            </td>
        <td valign="top" >
                            <asp:TextBox ID="txtUnidadAIntervenir" runat="server" SkinID="texto_sintamano" 
                                Visible="False"></asp:TextBox>
                            <asp:TextBox ID="txtDimension" runat="server" SkinID="texto_sintamano" 
                                Visible="False"></asp:TextBox>
                            <asp:TextBox ID="txtcomponenteAfectado" runat="server" SkinID="texto_sintamano" 
                                ontextchanged="txtcomponenteAfectado_TextChanged" Visible="False"></asp:TextBox>
                            <asp:TextBox ID="txtFuentes" runat="server" SkinID="texto_sintamano" 
                                Visible="False"></asp:TextBox>
                            </td>
    </tr>
    <tr>
        <td  >
                            <asp:Label ID="lblInfraestructura" runat="server" SkinID="etiqueta_negra" 
                                Text="infraestructura del proyecto que la intervienen" Visible="False" 
                                Width="274px"></asp:Label>
                            <asp:Label ID="lblInfraestructuraGenera" runat="server" SkinID="etiqueta_negra" 
                                Text="Infraestructura del proyecto que la Genera" Visible="False" 
                                Width="264px"></asp:Label>
                            <asp:Label ID="lblActividadesProyAfectan" runat="server" SkinID="etiqueta_negra" Text="Actividades del Proyecto que la Afectan" Visible="False"></asp:Label>
                            <asp:Label ID="lblActividadesProyInterviene" runat="server" SkinID="etiqueta_negra" Text="Actividades del Proyecto que lo Interviene" Visible="False"></asp:Label>
                            </td>
        <td >
                            <asp:TextBox ID="txtInfraestructura" runat="server" SkinID="texto_sintamano" 
                                Visible="False" ></asp:TextBox>
                            <asp:TextBox ID="txtInfraestructuraGenera" runat="server" 
                                SkinID="etiqueta_negra" Visible="False"  ></asp:TextBox>
                            <asp:TextBox ID="txtActividadesProyAfectan" runat="server" 
                                SkinID="etiqueta_negra" Visible="False" ></asp:TextBox>
                            <asp:TextBox ID="txtActividadesProyInterviene" runat="server" 
                                SkinID="etiqueta_negra" Visible="False" ></asp:TextBox>
                            </td>
        <td class="style1" >
                            <asp:Label ID="lblArea" runat="server" SkinID="etiqueta_negra" 
                                Text="Area a intervenir" Visible="False" Width="100px"></asp:Label></td>
        <td >
                            <asp:TextBox ID="txtArea" runat="server" SkinID="texto_sintamano" Visible="False"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td  >
                            <asp:Label ID="lblptge" runat="server" SkinID="etiqueta_negra" 
                                Text="% area total a intervenir"></asp:Label></td>
        <td colspan="1" >
                            <asp:TextBox ID="txtptge" runat="server" SkinID="texto_sintamano" ></asp:TextBox></td>
        <td class="style1" >
                            <asp:Label ID="lblImpacto" runat="server" SkinID="etiqueta_negra" Text="Impacto potencial al generar" Visible="False"></asp:Label></td>
        <td >
                            <asp:TextBox ID="txtImpacto" runat="server" SkinID="texto_sintamano" Visible="False"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td  >
                            <asp:Label ID="lblTipoImpacto" runat="server" Text="Tipo de Impacto" 
                                Visible="False" SkinID="etiqueta_negra"></asp:Label></td>
        <td colspan="1" >
                            <asp:DropDownList ID="cboTipoImpacto" runat="server" SkinID="lista_desplegable" 
                                Visible="False" Height="16px" >
                            </asp:DropDownList></td>
        <td style="text-align: center"  >
          <asp:Button ID="btnRegistrar" runat="server"  SkinID="boton_copia" 
                Text="Agregar" onclick="btnRegistrar_Click" Visible="true" />
        </td>
        <td style="text-align: center">
          <asp:Button ID="btnRegistrar0" runat="server"  SkinID="boton_copia" 
                Text="Cancelar" onclick="btnRegistrar_Click" Visible="true" 
                ValidationGroup="Tab5" style="text-align: center"/>
        </td>
    </tr>
    <tr>
        <td  >
                            &nbsp;</td>
        <td colspan="1" >
                            &nbsp;</td>
        <td class="style1" >
                            &nbsp;</td>
        <td >
                            &nbsp;</td>
    </tr>
    <tr>
        <td style="text-align: center"  >
            &nbsp;</td>
        <td colspan="1" style="text-align: center" >
            &nbsp;</td>
        <td class="style1" >
                            &nbsp;</td>
        <td >
                            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2" >
            &nbsp;</td>
        <td class="style1" >
            &nbsp;</td>
    </tr>
</table>