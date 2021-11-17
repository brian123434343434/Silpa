<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="Consultar_Formularios.aspx.cs" Inherits="EIA_Consultar_Formularios" Title="Consultar Formularios EIA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="stilesLarge" style="text-align:center">
        <!--<asp:ScriptManager ID="scmManejador" runat="server">
        </asp:ScriptManager>-->
        <div class="div-titulo">
            <asp:Label ID="Label6" runat="server" SkinID="titulo_principal_blanco" Text="Consulta de Formularios EIA">
            </asp:Label>
            <br />
            <br />
        </div>
        <asp:UpdatePanel ID="uppPanel" runat="server" >
            <ContentTemplate >            
        <table width="80%">
            <tr>
                <td align="center" >
                    <asp:Panel id="uppConsultaReporte" runat="server" width="100%" >            
                    <fieldset>
                    <legend>Datos de Busqueda</legend>
                    <table style="WIDTH: 100%">
                    <tbody>
                    <tr>
                        <td style="WIDTH: 40%">
                            <asp:Label id="lblNumeroVital" runat="server" Font-Bold="True" SkinID="etiqueta_negra" Text="Número VITAL :"></asp:Label>
                        </td>
                        <td style="WIDTH: 60%">
                            <asp:DropDownList id="cboNumeroVital" runat="server" Width="99%" BorderWidth="1px" BorderStyle="Solid"  >
                            </asp:DropDownList> 
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label id="lblFechaInicial" runat="server" Font-Bold="True" SkinID="etiqueta_negra" Text="Fecha Desde (dd/mm/aaaa):"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox id="txtFechaInicial" runat="server" SkinID="texto_corto" MaxLength="10"></asp:TextBox>
                            <cc1:CalendarExtender id="calFechaInicial" runat="server" TargetControlID="txtFechaInicial" Format="dd/MM/yyyy" >
                            </cc1:CalendarExtender> 
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label id="lblFechaFinal" runat="server" Font-Bold="True" SkinID="etiqueta_negra" Text="Fecha Hasta (dd/mm/aaaa):">
                            </asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox id="txtFechaFinal" runat="server" SkinID="texto_corto" MaxLength="10"></asp:TextBox> 
                            <asp:CompareValidator id="covCompararFechas" runat="server" Width="1px" Height="13px" Type="Date" Operator="GreaterThan" ErrorMessage='El valor del campo "Fecha Hasta", debe ser posterior al valor del campo "Fecha Desde".' Display="Dynamic" ControlToValidate="txtFechaFinal" ControlToCompare="txtFechaInicial">*</asp:CompareValidator> 
                            <cc1:CalendarExtender id="calFechaFinal" runat="server" TargetControlID="txtFechaFinal" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender> 
                        </td>
                    </tr>
                    </tbody>
                    </table>
                    </fieldset>                                                                                  
                    <table>
                    <tbody>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Button id="btnConsultar" onclick="btnConsultar_Click" runat="server" Font-Bold="True" SkinID="boton_copia" Text="Consultar"></asp:Button>
                        </td>
                    </tr>
                    </tbody>
                    </table>
                    </asp:Panel> 
                    <asp:Panel id="pnConsulta" runat="server" width="100%">
                    <table>
                    <tbody>
                    <tr>
                        <td>
                            <asp:GridView id="grdReporte" runat="server" SkinID="Grilla_Simple" AutoGenerateColumns="False" OnRowCommand="grdReporte_RowCommand" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="grdReporte_PageIndexChanging" PageSize="5">
                            <Columns>
                                <asp:BoundField HeaderText="Número Vital" DataField="NUMERO_VITAL" />
                                <asp:BoundField HeaderText="Fecha de Creación" DataField="FECHA_CREACION" />
                                <asp:BoundField HeaderText="Estado" DataField="ESTADO" />                                        
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' Text="Ver" ID="lbVerDetalles" runat="server" CommandName="DETALLE"></asp:LinkButton>
                                        <asp:Label ID="lblEipId" runat="server" Text='<%# Bind("EIP_ID") %>' Visible="false"></asp:Label>                                                
                                    </ItemTemplate>
                                </asp:TemplateField>                                        
                            </Columns>
                            </asp:GridView> 
                        </td>
                    </tr>
                    <tr>
                        <td>
                        <asp:Panel id="pnlReporte" runat="server" Visible="False" Width="278px">
                            <asp:Label style="TEXT-ALIGN: center" id="lblMensajeError" runat="server" Visible="False" SkinID="etiqueta_roja_error" Text="Existe un error con el reporte, por favor comuniquese con  el administrador" Width="545px">
                            </asp:Label>
                        </asp:Panel> 
                        </td>
                    </tr>
                 </tbody>
                 </table>
                 </asp:Panel> 
            </td>
        </tr>
        </table>
        </ContentTemplate>                        
        </asp:UpdatePanel>        

    </div>    
   
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
</asp:Content>

