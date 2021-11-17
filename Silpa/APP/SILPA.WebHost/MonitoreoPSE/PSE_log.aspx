<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PSE_log.aspx.cs" Culture="Auto" UICulture="Auto" MasterPageFile="~/plantillas/SILPA.master" Inherits="MonitoreoPSE_PSE_log" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        table {
            /*border: 1px solid #000;*/
        }
        table tr td {
            border: 0px solid #ddd !important;
            padding: 4px;
        }
        .Button{
            background-color: #ddd;
        }
    </style>

    <div class="div-titulo">
        <asp:Label ID="lbl_titulo_principal" runat="server" SkinID="titulo_principal_blanco" Text="Monitoreo de Pago Electrónico"></asp:Label><br />
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True">
    </asp:ScriptManager>

    <%--<div class="div-contenido2">--%>
    <div class="table-responsive" style="text-align: center !important; vertical-align: top !important;">
        <table border="0">
            <tr>
                <td>Código de la Transacción</td>
                <td>
                    <asp:TextBox ID="txtCodTransaccion" runat="server" MaxLength="8" Width="60%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEntidad0" runat="server" SkinID="etiqueta_negra" Text="Fecha"></asp:Label>
                    &nbsp;de la Transacción
                </td>
                <td>
                    <asp:Label ID="lblEntidad4" runat="server" SkinID="etiqueta_negra" Text="Inicial" Width="40px"></asp:Label>&nbsp;
                    <asp:TextBox ID="txtTransaccionFechaIni" runat="server" MaxLength="10" Width="20%"
                        ToolTip="La fecha debe estar en formato dd/mm/aaaa"></asp:TextBox>
                    <cc1:CalendarExtender ID="calFechaTransIni" runat="server" TargetControlID="txtTransaccionFechaIni"></cc1:CalendarExtender>
                    <asp:Label ID="Label1" runat="server" SkinID="etiqueta_negra" Text="Final" Width="40px"></asp:Label>
                    <asp:TextBox ID="txtTransaccionFechaFin" runat="server" MaxLength="10" Width="20%"
                        ToolTip="La fecha debe estar en formato dd/mm/aaaa"></asp:TextBox>
                    <cc1:CalendarExtender ID="calFechaTransFin" runat="server" TargetControlID="txtTransaccionFechaFin"></cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>Banco en el cual se Realiza la Transacción</td>
                <td>
                    <asp:TextBox ID="txtCodigoBanco" runat="server" MaxLength="50" Width="60%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Autoridad Ambiental</td>
                <td>
                    <asp:DropDownList ID="cboAutoridadAmbiental" runat="server" Width="61%" OnSelectedIndexChanged="cboAutoridadAmbiental_SelectedIndexChanged">
                        <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Número SILPA</td>
                <td>
                    <asp:TextBox ID="txtNumSILPA" runat="server" MaxLength="50" Width="60%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Número del Expediente</td>
                <td>
                    <asp:TextBox ID="txtNumExpediente" runat="server" MaxLength="50" Width="60%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Número de Referencia</td>
                <td>
                    <asp:TextBox ID="txtNumReferencia" runat="server" MaxLength="50" Width="60%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Fecha de Expedición del Documento de Cobro</td>
                <td>
                    <asp:Label ID="lblEntidad5" runat="server" SkinID="etiqueta_negra" Text="Inicial" Width="40px"></asp:Label>&nbsp;
                    <asp:TextBox ID="txtExpedicionFechaIni" runat="server" MaxLength="10" Width="20%"
                        ToolTip="La fecha debe estar en formato dd/mm/aaaa"></asp:TextBox>
                    <cc1:CalendarExtender ID="calFechaExpIni" runat="server" TargetControlID="txtExpedicionFechaIni"></cc1:CalendarExtender>
                    <asp:Label ID="Label2" runat="server" SkinID="etiqueta_negra" Text="Final" Width="40px"></asp:Label>
                    <asp:TextBox ID="txtExpedicionFechaFin" runat="server" MaxLength="10" Width="20%"
                        ToolTip="La fecha debe estar en formato dd/mm/aaaa"></asp:TextBox>
                    <cc1:CalendarExtender ID="calFechaExpFin" runat="server" TargetControlID="txtExpedicionFechaFin"></cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>Fecha de vencimiento del Documento de Cobro</td>
                <td>
                    <asp:Label ID="lblEntidad6" runat="server" SkinID="etiqueta_negra" Text="Inicial" Width="40px"></asp:Label>&nbsp;
                    <asp:TextBox ID="txtVencimientoFechaIni" runat="server" MaxLength="10" Width="20%"
                        ToolTip="La fecha debe estar en formato dd/mm/aaaa"></asp:TextBox>
                    <cc1:CalendarExtender ID="calFechaVenIni" runat="server" TargetControlID="txtVencimientoFechaIni"></cc1:CalendarExtender>
                    <asp:Label ID="Label3" runat="server" SkinID="etiqueta_negra" Text="Final" Width="40px"></asp:Label>
                    <asp:TextBox ID="txtVencimientoFechaFin" runat="server" MaxLength="10" Width="20%"
                        ToolTip="La fecha debe estar en formato dd/mm/aaaa"></asp:TextBox>
                    <cc1:CalendarExtender ID="calFechaVenFin" runat="server" TargetControlID="txtVencimientoFechaFin"></cc1:CalendarExtender>
                        
                </td>
            </tr>
            <tr>
                <td>Número de Identificación del Solicitante</td>
                <td>
                    <asp:TextBox ID="txtNumIdentifSolicitante" runat="server" MaxLength="50" Width="60%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Valor (Entregado por la PSE)</td>
                <td>
                    <asp:TextBox ID="txtValorEntregado" runat="server" MaxLength="50" Width="60%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding-top: 20px; padding-bottom: 10px; padding-left: 10px; padding-right: 20px; text-align: center; vertical-align: middle;">
                    <asp:Button ID="btnBuscar" runat="server" Text="Consultar" OnClick="btnBuscar_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding-bottom: 10px; text-align: left; vertical-align: top;">
                    <asp:Panel ID="pnConsulta" runat="server" ScrollBars="Auto" Width="800px" >
                        <asp:GridView ID="grdResultado" runat="server" Width="100%" AutoGenerateColumns="False"
                            CellPadding="4" GridLines="None" ForeColor="#333333">
                            <RowStyle BackColor="#E3EAEB"></RowStyle>
                            <Columns>
                                <asp:BoundField DataField="COB_TRANSACCION" HeaderText="Código Transacción"></asp:BoundField>
                                <asp:BoundField DataField="COB_FECHA_TRANSACCION" HeaderText="Fecha Transacción">
                                </asp:BoundField>
                                <asp:BoundField DataField="COB_BANCO" HeaderText="Nombre Banco"></asp:BoundField>
                                <asp:BoundField DataField="AUT_NOMBRE" HeaderText="Autoridad Ambiental"></asp:BoundField>
                                <asp:BoundField DataField="COB_NUMERO_SILPA" HeaderText="Numero SILPA"></asp:BoundField>
                                <asp:BoundField DataField="COB_NUMERO_EXPEDIENTE" HeaderText="Numero Expediente">
                                </asp:BoundField>
                                <asp:BoundField DataField="COB_REFERENCIA" HeaderText="Numero Referencia"></asp:BoundField>
                                <asp:BoundField DataField="COB_FECHA_EXPEDICION" HeaderText="Fecha Expediente"></asp:BoundField>
                                <asp:BoundField DataField="COB_FECHA_VENCIMIENTO" HeaderText="Fecha Vencimiento">
                                </asp:BoundField>
                                <asp:BoundField DataField="PER_NUMERO_IDENTIFICACION" HeaderText="Candidato"></asp:BoundField>
                                <asp:BoundField DataField="COB_VALOR" HeaderText="Valor"></asp:BoundField>
                                <asp:BoundField DataField="ECO_DESCRIPCION" HeaderText="Estado Transacción"></asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></FooterStyle>
                            <PagerStyle HorizontalAlign="Center" BackColor="#666666" ForeColor="White"></PagerStyle>
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></HeaderStyle>
                            <EditRowStyle BackColor="#7C6F57"></EditRowStyle>
                            <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                        </asp:GridView>

                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
