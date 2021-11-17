<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrFicha9.ascx.cs" Inherits="ResumenEIA_Fichas_crtFicha9" %>
<%@ Register src="ctrFicha2.ascx" tagname="ctrFicha2" tagprefix="uc1" %>
<style type="text/css">
    .style2
    {
        text-align: left;
    }
    .style3
    {
        width: 169px;
    }
    .style4
    {
        width: 211px;
    }
    </style>
<table style="width:100%;">
    <tr>
        <td colspan="3">
                9. PLAN DE CONTINGENCIAS
        </td>
    </tr>
    <td class="titleUpdate" colspan="3" width="100%">
                &nbsp;</td>
    <tr>
    <td  colspan="3" width="100%">
                </td>
    </tr>
    <tr>   
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td style="text-align: right">
            &nbsp;<asp:Button ID="btnIngresar" runat="server" Text="Agregar Plan de contingencia" 
                onclick="btnIngresar_Click" SkinID="boton_copia" EnableViewState="False" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:PlaceHolder ID="phlInsercion" runat="server" Visible="False">  
            <table style="width:100%;">
                <tr>
                    <td class="style2">
                        </td>
                    <td >
                        </td>
                    <td class="style4">
                        </td>
                    <td class="style3">
                        </td>
                    <td>
                        </td>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="Label2" runat="server" SkinID="etiqueta_negra" Text="Código" 
                            style="text-align: left" EnableViewState="False"></asp:Label>
                    </td>
                    <td >
                        <asp:TextBox ID="txtCodigo" runat="server" Width="139px" 
                            EnableViewState="False"></asp:TextBox>
                    </td>
                    <td class="style4">
                        &nbsp;</td>
                    <td class="style3">
                        &nbsp;</td>
                    <td style="text-align: left">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  colspan="2">
                        <asp:Label ID="Label3" runat="server" SkinID="etiqueta_negra" 
                            Text="Principales Riesgos Establecidos" EnableViewState="False"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:TextBox ID="txtPrinRiesgos" runat="server" Width="214px" 
                            EnableViewState="False"></asp:TextBox>
                    </td>
                    <td class="style3">
                        <asp:Label ID="Label4" runat="server" SkinID="etiqueta_negra" 
                            Text="Areas de Mayor Riesgo" EnableViewState="False"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtAreasRiesgos" runat="server" Width="214px" 
                            EnableViewState="False"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  colspan="2">
                        <asp:Label ID="Label5" runat="server" SkinID="etiqueta_negra" 
                            Text="Medidas  para Manejo de Principales Riesgos" EnableViewState="False"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:TextBox ID="txtMedidasRiesgos" runat="server" Width="214px" 
                            EnableViewState="False"></asp:TextBox>
                    </td>
                    <td class="style3">
                        <asp:Label ID="Label6" runat="server" SkinID="etiqueta_negra" 
                            Text="Puntos de control previstos (si aplica)" EnableViewState="False"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtPuntosControl" runat="server" Width="214px" 
                            EnableViewState="False"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  colspan="2">
                        <asp:Label ID="Label7" runat="server" SkinID="etiqueta_negra" 
                            Text="Soporte Externo Previsto" EnableViewState="False"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:TextBox ID="txtSoporteExterno" runat="server" Width="214px" 
                            EnableViewState="False"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td style="text-align: left">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2" colspan="3">
                        &nbsp;</td>
                    <td colspan="3">
                        <asp:Button ID="btnAgregar" runat="server" style="text-align: center" 
                            Text="Agregar" SkinID="boton_copia" onclick="btnAgregar_Click" 
                            EnableViewState="False" />&nbsp;
                        <asp:Button ID="btnCancelar" runat="server" style="text-align: center" 
                            Text="Cancelar" onclick="btnCancelar_Click" SkinID="boton_copia" 
                            EnableViewState="False" />
                    </td>
                </tr>
            </table>           
           </asp:PlaceHolder>
            
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:GridView ID="grvPlanManejo" runat="server" AutoGenerateColumns="False" 
                onrowdeleting="grvPlanManejo_RowDeleting" EnableViewState="False" 
                onselectedindexchanged="grvPlanManejo_SelectedIndexChanged" 
                SkinID="Grilla_simple">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" />
                    <asp:BoundField AccessibleHeaderText="Código" HeaderText="Código" 
                        DataField="EPC_ID" />
                    <asp:BoundField AccessibleHeaderText="Principales Riesgos Establecidos" 
                        HeaderText="Principales Riesgos Establecidos" 
                        DataField="EPC_PRINC_RIESG_EST" />
                    <asp:BoundField AccessibleHeaderText="Areas de Mayor Riesgo" 
                        HeaderText="Areas de Mayor Riesgo" DataField="EPC_AREAS_MAYOR_RIESG" />
                    <asp:BoundField AccessibleHeaderText="Medidas  para Manejo de Principales Riesgos" 
                        HeaderText="Medidas  para Manejo de Principales Riesgos" 
                        DataField="EPC_MEDIDAS_MANEJO" />
                    <asp:BoundField AccessibleHeaderText="Puntos de control previstos (si aplica)" 
                        HeaderText="Puntos de control previstos (si aplica)" 
                        DataField="EPC_PTOS_CONTROL" />
                    <asp:BoundField AccessibleHeaderText="Soporte Externo Previsto" 
                        HeaderText="Soporte Externo Previsto" DataField="EPC_SOPORTE_EXTERNO" />
                </Columns>
                <EmptyDataTemplate>
                    No se han ingresado registros para plan de contigencias
                </EmptyDataTemplate>
            </asp:GridView>
        </td>
    </tr>
</table>
