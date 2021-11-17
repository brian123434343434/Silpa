<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrFicha11.ascx.cs" Inherits="ResumenEIA_Fichas_ctrFicha11" %>
<style type="text/css">
   
    .style7
    {
        text-align: right;
    }
   
</style>
<table style="width:100%;">
    <tr>
        <td colspan="2" class="style6">
            11. plan de inversion del 1%<br />
        </td>
    </tr>
    <tr>
         <td class="titleUpdate" colspan="2">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2" class="style6">
            </td>
    </tr>
    <tr>
    <td class="titleUpdate" colspan="2">
            </td>
    </tr>
    <tr>
        <td colspan="2" class="style6">           
            11.1           
            Elementos y Costos Considerados para Estimar la Inversión</td>
    </tr>
        <td class="titleUpdate" colspan="2">
            </td>
    <tr>
        <td colspan="2" style="text-align: center" >
            <asp:TextBox ID="txtElementos" runat="server" Height="132px" TextMode="MultiLine" 
                Width="716px" SkinID="texto_sintamano"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style7" colspan="2">
            <asp:Button ID="btnplaninversion" runat="server" 
                Text="Guardar Plan de Inversion" onclick="btnplaninversion_Click" 
                style="text-align: right" SkinID="boton_copia" 
                />
        </td>
    </tr>
    <tr>
      <td class="titleUpdate" colspan="2">
            </td>
    </tr>
    <tr>
        <td >
            11.2 Proyectos de Inversión&nbsp;&nbsp;&nbsp;&nbsp; </td>
        <td align="right">
            <asp:Button ID="btnProyectoIns" runat="server" 
                Text="Agregar Proyecto de inversion" onclick="btnProyectoIns_Click" 
                style="text-align: right" SkinID="boton_copia" Enabled="False" 
                />
        </td>
    </tr>
    <tr>
       <td class="titleUpdate" colspan="2">
            </td>
    </tr>
    <tr>
        <td  colspan="2">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style7" colspan="2">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
           <asp:PlaceHolder ID="plhInsersion" runat="server" Visible="False">    
            <table style="width:100%;">
                <tr>
                    <td >
                        <asp:Label ID="lblCodigo" runat="server" Text="Código" SkinID="etiqueta_negra" 
                            Visible="False"></asp:Label>
                    </td>
                    <td >
                        <asp:TextBox ID="txtCodigo" runat="server" SkinID="texto_sintamano" 
                            Visible="False"></asp:TextBox>
                    </td>
                    <td >
                        <asp:Label ID="lblNombreProyecto" runat="server" Text="Nombre del Proyecto" 
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td >
                        <asp:TextBox ID="txtNombreProyecto" runat="server" Width="213px" 
                            SkinID="texto_sintamano"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="lblValorInversion" runat="server" 
                            Text="Valor de la inversión (Col $)" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td >
                        <asp:TextBox ID="txtValorInversion" runat="server" Width="267px" 
                            SkinID="texto_sintamano"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revValorInversion" runat="server" 
                            ControlToValidate="txtValorInversion" 
                            ErrorMessage="debe ingresar valores numericos" ValidationGroup="ficha11">*</asp:RegularExpressionValidator>
                    </td>
                    <td >
                        <asp:Label ID="lblPtgeInversion" runat="server" Text="% de la Inversión Total " 
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td >
                        <asp:TextBox ID="txtPtgeInversion" runat="server" Width="211px" 
                            SkinID="texto_sintamano"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="lblDescripcion" runat="server" Text="Descripción" 
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td >
                        <asp:TextBox ID="txtDescripcion" runat="server" Width="264px" 
                            SkinID="texto_sintamano"></asp:TextBox>
                    </td>
                    <td >
                        <asp:Label ID="lblCuencaHidrografica" runat="server" Text="Cuenca Hidrográfica" 
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td >
                        <asp:TextBox ID="txtCuencaHidrografica" runat="server" Width="208px" 
                            SkinID="texto_sintamano"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" >
                        &nbsp;</td>
                </tr>
                <tr align="center">
        <td  colspan="2">
                        <asp:Button ID="btnInsertar" runat="server" Text="Insertar " 
                            onclick="btnInsertar_Click" SkinID="boton_copia" />   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                            onclick="btnCancelar_Click" SkinID="boton_copia" />
     </td>                    
    </tr>
            </table>
          </asp:PlaceHolder>
           
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center">
            <asp:GridView ID="grvProyectos" runat="server" AutoGenerateColumns="False" 
                onrowdeleting="grvProyectos_RowDeleting" SkinID="Grilla_simple">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" />
                    <asp:BoundField DataField="EPO_ID" HeaderText="Codigo" />
                    <asp:BoundField DataField="EPO_NOMBRE_PROYECTO" 
                        HeaderText="Nombre del Proyecto" />
                    <asp:BoundField AccessibleHeaderText="Valor de la inversión (Col $)" 
                        DataField="EPO_NOMBRE_PROYECTO" HeaderText="Valor de la inversión (Col $)" />
                    <asp:BoundField AccessibleHeaderText="% de la Inversión Total " 
                        DataField="EPO_PORC_INVERSION_TOTAL" HeaderText="% de la Inversión Total " />
                    <asp:BoundField AccessibleHeaderText="Descripción" DataField="EPO_DESCRIPCION" 
                        HeaderText="Descripción" />
                    <asp:BoundField AccessibleHeaderText="Cuenca Hidrográfica" 
                        DataField="EPO_CUENCA_HIDROGRAFICA" HeaderText="Cuenca Hidrográfica" />
                </Columns>
                <EmptyDataTemplate>
                    no se han ingresado registros
                </EmptyDataTemplate>
            </asp:GridView>
        </td>
    </tr>
</table>

