<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ConsultarSalvoconductoWeb.aspx.cs" Inherits="Salvoconducto_ConsultarSalvoconductoWeb" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" SkinID="titulo_principal_blanco" Text="CONSULTA DE SALVOCONDUCTO" meta:resourcekey="lblTituloPrincipalResource1"></asp:Label>
    </div>
    <div class="div-contenido" style="text-align: center">
        <asp:ScriptManager ID="scmManejador" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="uppPanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="text-align: center">
                    <table style="border-right: #d8d8d8 1px solid; border-top: #d8d8d8 1px solid; border-left: #d8d8d8 1px solid; width: 80%; border-bottom: #d8d8d8 1px solid">
                        <tbody>
                            <tr>
                                <td align="left">
                                    <asp:Panel ID="pnlConsultaSalvoconducto" runat="server" Width="800px">
                                        <table style="width: 700px">
                                            <tbody>
                                                <tr>
                                                    <td width="40%">
                                                        <asp:Label ID="lblNumeroSalvoConducto" runat="server" SkinID="etiqueta_negra" Text="Número de Salvoconducto:"></asp:Label>
                                                    </td>
                                                    <td width="60%">
                                                        <asp:TextBox ID="txtNumeroSalvoconducto" runat="server" SkinID="texto"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">

                                                        <asp:Panel runat="server" ID="pnAdfmin">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td width="40%">
                                                                        <asp:Label ID="lblFechaInicial" runat="server" SkinID="etiqueta_negra"
                                                                            Text="Fecha Inicial Desde (dd/mm/aaaa):"></asp:Label>
                                                                    </td>
                                                                    <td width="60%">
                                                                        <asp:TextBox ID="txtFechaInicial" runat="server" MaxLength="10" SkinID="texto_corto"></asp:TextBox>
                                                                        <cc1:CalendarExtender
                                                                            ID="calFechaInicial" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaInicial">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label1" runat="server" SkinID="etiqueta_negra"
                                                                            Text="Fecha Inicial Hasta (dd/mm/aaaa):"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFechaInicial2" runat="server" MaxLength="10" SkinID="texto_corto"></asp:TextBox>
                                                                        <cc1:CalendarExtender
                                                                            ID="calFechaInicial2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaInicial2">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label2" runat="server" SkinID="etiqueta_negra"
                                                                            Text="Tipo Salvoconducto:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboTipoSalv" runat="server" SkinID="lista_desplegable"></asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblAut" runat="server" SkinID="etiqueta_negra"
                                                                            Text="Autoridad Ambiental:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboTipoAA" runat="server" SkinID="lista_desplegable"></asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <asp:Button ID="btnConsultar" OnClick="btnConsultar_Click" runat="server" SkinID="boton_copia" Text="Buscar" meta:resourcekey="btnConsultarResource1"></asp:Button>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblNumeroObligatorio" runat="server" Text="El número de salvoconducto es obligatorio" Visible="False" ForeColor="Red" meta:resourcekey="lblNumeroObligatorioResource1"></asp:Label>&nbsp;</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Panel ID="pnlSalvoconductos" runat="server" Width="400px" Visible="False">
                                        <asp:GridView Style="text-align: center" ID="grdSalvoconductos" runat="server" SkinID="Grilla_simple_peq" Width="400px" AutoGenerateColumns="False" meta:resourcekey="grdSalvoconductosResource1" OnRowCommand="grdSalvoconductos_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="EXP_NUMERO" HeaderText="N&#250;mero de Expediente"></asp:BoundField>
                                                <asp:BoundField DataField="SAV_NUMERO" HeaderText="N&#250;mero de Salvoconducto"></asp:BoundField>
                                                <asp:BoundField DataField="SAV_FECHA_DESDE" DataFormatString="{0:yyyy/MM/dd}" HeaderText="Fecha Inicial"></asp:BoundField>
                                                <asp:BoundField DataField="SAV_FECHA_HASTA" DataFormatString="{0:yyyy/MM/dd}" HeaderText="Fecha Final"></asp:BoundField>
                                                <asp:BoundField DataField="TSA_NOMBRE" HeaderText="Tipo Salvoconducto"></asp:BoundField>
                                                <asp:BoundField DataField="AUT_NOMBRE" HeaderText="Autoridad Ambiental"></asp:BoundField>
                                                <asp:TemplateField HeaderText="Fecha" SortExpression="COB_FECHA_EXPEDICION" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("SAV_ID") %>' SkinID="etiqueta_negra"></asp:Label>
                                                        <asp:Label ID="lblProcessInstance" runat="server" Text='<%# Eval("IDPROCESSINSTANCE") %>' SkinID="etiqueta_negra"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:ButtonField Text="Detalles"></asp:ButtonField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label Style="text-align: center" ID="lblMensajeError" runat="server" SkinID="etiqueta_roja_error" Text="No se encontró ningún resultado para esta consulta." Visible="False" meta:resourcekey="lblMensajeErrorResource1"></asp:Label>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>



                <div style="text-align: center">
                    <table id="TbDetalle" runat="server" style="border-right: #d8d8d8 1px solid; border-top: #d8d8d8 1px solid; border-left: #d8d8d8 1px solid; width: 80%; border-bottom: #d8d8d8 1px solid">
                        <tr>
                            <td align="center">
                                <div>
                                    <asp:Label ID="lblTituloEspecimen" runat="server" SkinID="titulo_principal" Text="ESPECIMENES APROBADOS" meta:resourcekey="lblTituloEspecimen"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView Style="text-align: Left" ID="grdSalvoconductoEspecimen" runat="server" SkinID="Grilla_simple_peq" Width="400px" AutoGenerateColumns="False" meta:resourcekey="grdSalvoconductosResource1">
                                    <Columns>
                                        <asp:BoundField DataField="ESP_NOMBRE_CIENTIFICO" HeaderText="Nombre Cientifico"></asp:BoundField>
                                        <asp:BoundField DataField="ESP_NOMBRE_COMUN" HeaderText="Nombre Comun"></asp:BoundField>
                                        <asp:BoundField DataField="ESP_DESCRIPCION" HeaderText="Descripción"></asp:BoundField>
                                        <asp:BoundField DataField="ESP_IDENTIFICACION" HeaderText="Identificación"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Cantidad">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_esp_cantidad" runat="server" EnableTheming="false" Text='<%# Eval("ESP_CANTIDAD","{0:N2}") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ESP_DIMENSIONES" HeaderText="Dimensiones"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>



                <table id="Table1" runat="server" style="border-right: #d8d8d8 1px solid; border-top: #d8d8d8 1px solid; border-left: #d8d8d8 1px solid; width: 80%; border-bottom: #d8d8d8 1px solid">
                    <tr>
                        <td>
                            <asp:Label ID="lblTituloDetalles" runat="server" SkinID="titulo_principal" Text="DATOS SALVOCONDUCTO"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView Style="text-align: Left" ID="grdDetalleSalvoconductos" runat="server" SkinID="Grilla_simple_peq" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="VALORCAMPO" HeaderText="Campo" meta:resourceKey="BoundFieldResource1"></asp:BoundField>
                                    <asp:BoundField DataField="VALOR" HeaderText="Valor" meta:resourceKey="BoundFieldResource2"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>


            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

