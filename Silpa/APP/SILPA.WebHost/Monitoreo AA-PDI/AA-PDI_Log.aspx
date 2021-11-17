<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AA-PDI_Log.aspx.cs" Inherits="Monitoreo_AA_PDI_AA_PDI_Log" Culture="Auto" UICulture="Auto" MasterPageFile="~/plantillas/SILPA.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="headPlaceHolder" runat="Server">
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
        <asp:Label ID="lbl_titulo_principal" runat="server" SkinID="titulo_principal_blanco" Text="Monitoreo de Web Services"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True">
        </asp:ScriptManager>
    </div>

    <%--<div class="div-contenido2">--%>
    <div class="table-responsive">
        <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
            <tr>
                <td>
                    <asp:Label ID="lblEntidad0" runat="server" SkinID="etiqueta_negra" Text="Fecha"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblEntidad4" runat="server" SkinID="etiqueta_negra" Text="Inicial" Width="40px"></asp:Label>
                    <asp:TextBox ID="txtFechaIni" runat="server" MaxLength="10" Width="20%" ToolTip="La fecha debe estar en formato dd/mm/aaaa"></asp:TextBox>
                    <cc1:CalendarExtender ID="calFechaIni" runat="server" TargetControlID="txtFechaIni"></cc1:CalendarExtender>
                    <asp:Label ID="Label1" runat="server" SkinID="etiqueta_negra" Text="Final" Width="40px"></asp:Label>
                    <asp:TextBox ID="txtFechaFin" runat="server" MaxLength="10" Width="20%" ToolTip="La fecha debe estar en formato dd/mm/aaaa"></asp:TextBox>
                    <cc1:CalendarExtender ID="calFechaFin" runat="server" TargetControlID="txtFechaFin"></cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>Nombre WS</td>
                <td>
                    <asp:DropDownList ID="cobNombreWS" runat="server" Width="100%"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Metodo</td>
                <td>
                    <asp:DropDownList ID="cobMetodo" runat="server" Width="100%">
                        <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Acción Realizada</td>
                <td>
                    <asp:DropDownList ID="cboSeveridad" runat="server" Width="100%"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Mensaje</td>
                <td>
                    <asp:TextBox ID="txtMensaje" runat="server" MaxLength="50" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>No Vital</td>
                <td>
                    <asp:TextBox ID="txtNoVital" runat="server" MaxLength="50" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Autoridad Ambiental</td>
                <td>
                    <asp:DropDownList ID="cboAutoridadAmbiental" runat="server" Width="100%">
                        <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>IdPadre</td>
                <td>
                    <asp:TextBox ID="txtIdPadre" runat="server" MaxLength="50" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%;">
                        <tr>
                            <td style="padding-right: 15px; text-align: right; vertical-align: middle;">
                                <asp:Button ID="btnBuscar" runat="server" SkinID="boton_copia" Text="Consultar" OnClick="btnBuscar_Click" />
                            </td>
                            <td style="padding-left: 15px; text-align: left; vertical-align: middle;">
                                <asp:ImageButton ID="imb_exporta_excel" runat="server" Width="68px" Height="16px"
                                    ToolTip="Exportar listado a MS Excel" BorderWidth="0px" ImageUrl="../App_Themes/Img/exportar_excel.gif"
                                    OnClick="imb_exporta_excel_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnConsulta" runat="server" Width="100%" ScrollBars="Auto" style="text-align: left; vertical-align: top; margin: 0; padding: 0;">
            <asp:GridView ID="grdResultado" runat="server" 
                Width="100%" 
                AllowPaging="True"
                AllowSorting="True"
                EmptyDataText="No existen datos registrados en ésta tabla"
                OnPageIndexChanging="grdResultado_PageIndexChanging"
                AutoGenerateColumns="False"
                CellPadding="4" CellSpacing="2" 
                GridLines="None"
                ForeColor="#333333">
                <RowStyle BackColor="#E3EAEB"></RowStyle>
                <Columns>
                    <asp:BoundField DataField="LOGWS_ID" HeaderText="ID"></asp:BoundField>
                    <asp:BoundField DataField="FECHA_REGISTRO" HeaderText="Fecha"></asp:BoundField>
                    <asp:BoundField DataField="NOMBREWS" HeaderText="Nombre WS" />
                    <asp:BoundField DataField="NOMBRE_METODO" HeaderText="Metodo" />
                    <asp:BoundField DataField="SEVERIDAD" HeaderText="Acci&#243;n Realizada" />
                    <asp:TemplateField HeaderText="Mensaje">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("MENSAJE") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("MENSAJE") %>' SkinID="etiqueta_negra"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="AUTORIDAD_AMBIENTAL" HeaderText="Autoridad Ambiental" />
                    <asp:BoundField DataField="ID_PADRE" HeaderText="IdPadre" />
                </Columns>
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></FooterStyle>
                <PagerStyle HorizontalAlign="Center" BackColor="#666666" ForeColor="White"></PagerStyle>
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></HeaderStyle>
                <EditRowStyle BackColor="#7C6F57"></EditRowStyle>
                <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
            </asp:GridView>
        </asp:Panel>
    </div>
</asp:Content>
