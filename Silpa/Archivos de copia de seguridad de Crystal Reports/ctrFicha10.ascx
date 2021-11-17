<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrFicha10.ascx.cs" Inherits="ResumenEIA_Fichas_ctrFicha10" %>
<%--<%@ Register src="ctrficha10Insersion.ascx" tagname="ctrficha10Insersion" tagprefix="uc1" %>--%>
<table style="width:100%;">
    <tr>
        <td colspan="3">
            10. PLAN DE ABANDONO Y RESTAURACION FINAL</td>
    </tr>
    <tr>
           <td class="titleUpdate" colspan="3"> &nbsp;</td> 
    </tr>
    <tr>
     <td colspan="3">
    </tr>
    <tr>
           <td class="titleUpdate" colspan="3"> </td> 
    </tr>
    <tr>
        <td  colspan="2">
            Seleccione Opcion</td>
        <td>
            <asp:DropDownList ID="cboPlan" runat="server" AutoPostBack="True" 
                onselectedindexchanged="cbo_SelectedIndexChanged" style="height: 22px" 
                Width="100px">
            </asp:DropDownList>
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <asp:Button ID="btnPlanAbandono" runat="server" 
                Text="Agregar plan de abandono" onclick="btnPlanAbandono_Click" 
                SkinID="boton_copia" />
&nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td >
        </td>
        <td >
        </td>
        <td >
        </td>
    </tr>
    <tr>
        <td  colspan="3">
        <asp:PlaceHolder ID="plhitems" runat="server" Visible="False"   >   
            &nbsp;<table style="width:100%;">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblEtiqueta" runat="server" Text="Label" SkinID="etiqueta_negra" 
                            Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center" >
                        <asp:Label ID="lblEtiqueta0" runat="server" Text="Item" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td  style="text-align: center">
                        <asp:Label ID="lblEtiqueta1" runat="server" Text="Descripcion" 
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="lblPrograma" runat="server" Text="Programa" 
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td style="text-align: center" >
                        <asp:TextBox ID="txtPrograma" runat="server" Width="472px" 
                            SkinID="texto_sintamano"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="lblElementosSeguimiento" runat="server" 
                            Text="Elementos de Seguimento" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td style="text-align: center"  >
                        <asp:TextBox ID="txtElementosSeguimiento" runat="server" Width="472px" 
                            SkinID="texto_sintamano"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="lblUbicacion" runat="server" Text="Ubicacion" 
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td style="text-align: center"  >
                        <asp:TextBox ID="txtUbicacion" runat="server" Width="472px" 
                            SkinID="texto_sintamano"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="lblActividad" runat="server" Text="Actividad a Realizar" 
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td style="text-align: center"  >
                        <asp:TextBox ID="txtActividad" runat="server" Width="472px" 
                            SkinID="texto_sintamano"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="lblIndicadores" runat="server" 
                            Text="Indicadores de Seguimiento y Monitoreo" 
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td style="text-align: center"  >
                        <asp:TextBox ID="txtIndicadores" runat="server" Width="472px" 
                            SkinID="texto_sintamano"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td >
                        &nbsp;</td>
                    <td style="text-align: center" >
                        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" 
                            SkinID="boton_copia" onclick="btnRegistrar_Click" />
                    &nbsp;
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                            SkinID="boton_copia" onclick="btnCancelar_Click" />
                    </td>
                </tr>
            </table>
         </asp:PlaceHolder> 
            </td>
    </tr>
    <tr>
        <td  colspan="3">
            
            &nbsp;</td>
    </tr>
    <tr>
        <td  colspan="3">
            
            <asp:GridView ID="grvProgramas" runat="server" AutoGenerateColumns="False" 
                onrowdeleting="grvProgramas_RowDeleting" 
                onselectedindexchanged="grvProgramas_SelectedIndexChanged" 
                SkinID="Grilla_simple">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" />
                    <asp:BoundField DataField="ETM_TIPO_PLAN" HeaderText="Tipo Plan" />
                    <asp:BoundField DataField="ETM_TIPO_PLAN" HeaderText="Plan" />
                    <asp:BoundField HeaderText="Programa" DataField="EPS_PROGRAMA" />
                    <asp:BoundField HeaderText="Elementos de Seguimiento" 
                        DataField="EPS_ELEMENTOS_SEGUIMIENTO" />
                    <asp:BoundField HeaderText="Ubicacion" DataField="EPS_UBICACION" />
                    <asp:BoundField HeaderText="Actividad a Realizar" DataField="EPS_ACTIVIDAD" />
                    <asp:BoundField HeaderText="Indicadores de Seguimiento y Monitoreo" 
                        DataField="EPS_INDICADORES" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td  colspan="3">
            
            &nbsp;</td>
    </tr>
    </table>
