<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="Consultar_Formularios.aspx.cs" Inherits="EIA_Consultar_Formularios" Title="Consultar Formularios EIA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
             <div class="stilesLarge">
        <!--<asp:ScriptManager ID="scmManejador" runat="server">
        </asp:ScriptManager>-->
        <div class="div-titulo">
            <asp:Label ID="Label6" runat="server" SkinID="titulo_principal_blanco" Text="Consulta de Formularios EIA">
            </asp:Label>
            <br />
            <br />
        </div>
        <asp:UpdatePanel ID="uppPanel" runat="server">
            <ContentTemplate>
&nbsp;<asp:Panel id="uppConsultaReporte" runat="server" Width="500px">
                <fieldset>
                    <legend>Datos de Busqueda</legend>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 40%">
                                <asp:Label ID="lblNumeroVital" runat="server" Text="Número VITAL :" SkinID="etiqueta_negra"
                                    Font-Bold="True" ></asp:Label>
                            </td >
                            <td style="width: 60%">
                                <asp:DropDownList ID="cboNumeroVital" runat="server"
                                        BorderStyle="Solid" BorderWidth="1px" Width="99%"></asp:DropDownList>
                            </td>                                        
                        </tr>                                                                                                                                                 
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="lblFechaInicial" runat="server" Font-Bold="True" SkinID="etiqueta_negra"
                                    Text="Fecha Desde (dd/mm/aaaa):"></asp:Label><br />
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaInicial" runat="server" MaxLength="10" SkinID="texto_corto"></asp:TextBox><cc1:CalendarExtender
                                    ID="calFechaInicial" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaInicial">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="lblFechaFinal" runat="server" Font-Bold="True" SkinID="etiqueta_negra" 
                                    Text="Fecha Hasta (dd/mm/aaaa):" ></asp:Label><br /> 
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaFinal" runat="server" MaxLength="10" SkinID="texto_corto"></asp:TextBox>
                                <asp:CompareValidator ID="covCompararFechas" runat="server" ControlToCompare="txtFechaInicial" 
                                    ControlToValidate="txtFechaFinal" Display="Dynamic" ErrorMessage='El valor del campo "Fecha Hasta", debe ser posterior al valor del campo "Fecha Desde".'
                                    Operator="GreaterThan" Type="Date" Height="13px" Width="1px">*</asp:CompareValidator>
                                <cc1:CalendarExtender ID="calFechaFinal" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaFinal">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                </fieldset>                  
                <table style="width: 900px; text-align:center">
                        <tr>
                            <td colspan="2" style="text-align: center; height: 31px;" align="left" >
                                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" SkinID="boton_copia"
                                    Font-Bold="True" OnClick="btnConsultar_Click" /></td>
                        </tr>
                </table>
                </asp:Panel> <asp:Panel id="pnConsulta" runat="server" width="100%"><TABLE><TBODY><TR><TD style="WIDTH: 750px; HEIGHT: 110px">&nbsp;<asp:GridView id="grdReporte" runat="server" SkinID="Grilla_Simple" PageSize="5" OnPageIndexChanging="grdReporte_PageIndexChanging" AllowSorting="True" AllowPaging="True" OnRowCommand="grdReporte_RowCommand" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField HeaderText="Número Vital" DataField="NUMERO_VITAL" />
                                        <asp:BoundField HeaderText="Fecha de Creación" DataField="FECHA_CREACION" />
                                        <asp:BoundField HeaderText="Estado" DataField="ESTADO" />                                        
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' Text="Ver" ID="lbVerDetalles" runat="server" CommandName="DETALLE"></asp:LinkButton>
                                                <asp:Label ID="lbNumeroVital" runat="server" Text='<%# Bind("NUMERO_VITAL") %>' Visible="false"></asp:Label>                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        
                                    </Columns>
                            </asp:GridView> </TD></TR><TR><TD><asp:Panel id="pnlReporte" runat="server" Width="278px" Visible="False">&nbsp;<asp:Label style="TEXT-ALIGN: center" id="lblMensajeError" runat="server" SkinID="etiqueta_roja_error" Width="545px" Text="Existe un error con el reporte, por favor comuniquese con  el administrador" Visible="False"></asp:Label></asp:Panel> </TD></TR></TBODY></TABLE></asp:Panel> 
</ContentTemplate>                        
        </asp:UpdatePanel>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>    
   
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
</asp:Content>

