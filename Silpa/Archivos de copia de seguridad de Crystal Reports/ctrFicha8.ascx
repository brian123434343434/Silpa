<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrFicha8.ascx.cs" Inherits="ResumenEIA_Fichas_ctrFicha8" %>
<style type="text/css">
    .style1
    {
        background-color: #CCE6E6;
    }
    .style2
    {
        background-color: #F0F0F0;
    }
</style>
<table style="width:100%;">
    <tr>
        <td colspan="4">
            8. PROGRAMA DE SEGUIMIENTO Y MONITOREO</td>
    </tr>
    <tr class="titleUpdate">
        <td colspan="4">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="4">
            </td>
    </tr>
    <tr>
        <td colspan="4" class="titleUpdate">
           </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Seleccione medio   " 
                SkinID="etiqueta_negra"></asp:Label>
&nbsp;<asp:DropDownList ID="cboMedio" runat="server" SkinID="lista_desplegable" 
                AutoPostBack="True" onselectedindexchanged="cboMedio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="btnAgregarMedio" runat="server" Text="agregar medio" 
                SkinID="boton_copia" onclick="btnAgregarMedio_Click" />
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="4" class="titleUpdate">
            </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:PlaceHolder ID="phlFormulario" runat="server" Visible="False">
            <table style="width:100%;">
                <tr>
                    <td colspan="5" style="text-align: center">
                        <asp:Label ID="lblMedio" runat="server" Text="Label" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="text-align: center" class="titleUpdate">
                        </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNombre" runat="server" SkinID="etiqueta_negra" Text="Nombre"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNombre" runat="server" SkinID="texto_sintamano" Width="90%"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Label ID="lblEtapa" runat="server" SkinID="etiqueta_negra" 
                            Text="Etapa de Aplicacion"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cboEtapaAplicacion" runat="server" 
                            SkinID="lista_desplegable">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5" style="text-align: center">
                        </td>
                </tr>
                 <tr>
        <td class="style1" style="text-align: center" colspan="2">
            <asp:Label ID="lblItem" runat="server" SkinID="etiqueta_negra" 
                style="text-align: center" Text="Items"></asp:Label>
        </td>
        <td colspan="3" class="style1" style="text-align: center">
            <asp:Label ID="lblDescripcion" runat="server" SkinID="etiqueta_negra" 
                style="text-align: center" Text="Descripcion"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style2" colspan="2">
            <asp:Label ID="lblPrograma" runat="server" SkinID="etiqueta_negra" 
                style="text-align: center" Text="Programa"></asp:Label>
        </td>
        <td colspan="3" class="style2" style="text-align: center">
            <asp:TextBox ID="txtPrograma" runat="server" SkinID="texto_sintamano" 
                Width="70%" style="text-align: left"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style2" colspan="2">
            <asp:Label ID="lblElementosSeguimiento" runat="server" SkinID="etiqueta_negra" 
                style="text-align: center" Text="Elementos de Seguimiento"></asp:Label>
        </td>
        <td colspan="3" class="style2" style="text-align: center">
            <asp:TextBox ID="txtElementosSeguimiento" runat="server" 
                SkinID="texto_sintamano" Width="70%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style2" colspan="2">
            <asp:Label ID="lblUbicacion" runat="server" SkinID="etiqueta_negra" 
                style="text-align: center" Text="Ubicacion"></asp:Label>
        </td>
        <td colspan="3" class="style2" style="text-align: center">
            <asp:TextBox ID="txtUbicacion" runat="server" SkinID="texto_sintamano" 
                Width="70%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style2" colspan="2">
            <asp:Label ID="lblActividadARealizar" runat="server" SkinID="etiqueta_negra" 
                style="text-align: center" Text="Actividad a Realizar"></asp:Label>
        </td>
        <td colspan="3" class="style2" style="text-align: center">
            <asp:TextBox ID="txtActividadARealizar" runat="server" SkinID="texto_sintamano" 
                Width="70%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style2" colspan="2">
            <asp:Label ID="lblIndicadoresSeguimiento" runat="server" 
                SkinID="etiqueta_negra" style="text-align: center" 
                Text="Indicadores de Seguimiento y Monitoreo"></asp:Label>
        </td>
        <td colspan="3" class="style2" style="text-align: center">
            <asp:TextBox ID="txtIndicadoresSeguimiento" runat="server" 
                SkinID="texto_sintamano" Width="70%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="5" style="text-align: center">
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar " SkinID="boton_copia" 
                onclick="btnAgregar_Click" />&nbsp;&nbsp;
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                SkinID="boton_copia" onclick="btnCancelar_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="5" style="text-align: center" class="titleUpdate">
            </td>
    </tr>
            </table>
             </asp:PlaceHolder>
            
           
        </td>
    </tr>
   <tr>
   <td colspan="4"></td>
   </tr>
   
   <tr>
   <td colspan="4" style="text-align: center">
       <asp:GridView ID="grvProgramaSeguimiento" runat="server" 
           AutoGenerateColumns="False" 
           onrowdeleting="grvProgramaSeguimiento_RowDeleting" SkinID="Grilla_simple">
           <Columns>
               <asp:CommandField ShowDeleteButton="True" />
               <asp:BoundField HeaderText="Medio" DataField="ETM_TIPO_MEDIO" />
               <asp:BoundField AccessibleHeaderText="Nombre" DataField="EPS_NOMBRE_PROG" 
                   HeaderText="Nombre" />
               <asp:BoundField 
                   HeaderText="Etapa aplicacion" DataField="EEA_ETAPA_APLICACION_PROY" />
               <asp:BoundField DataField="EPS_PROGRAMA" HeaderText="Programa" />
               <asp:BoundField DataField="EPS_ELEMENTOS_SEGUIMIENTO" 
                   HeaderText="Elementos de Seguimiento" />
               <asp:BoundField DataField="EPS_UBICACION" HeaderText="Ubicacion" />
               <asp:BoundField DataField="EPS_ACTIVIDAD" HeaderText="Actividad" />
               <asp:BoundField DataField="EPS_INDICADORES" 
                   HeaderText="Indicadores de seguimiento y Monitoreo" />
           </Columns>
       </asp:GridView>
       </td>
   </tr>
   
   <tr>
   <td colspan="4" style="text-align: center">&nbsp;</td>
   </tr>
   
   <tr>
   <td colspan="4" style="text-align: center">&nbsp;</td>
   </tr>
   
</table>
