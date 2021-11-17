<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PSE_Log.aspx.cs" Inherits="Monitoreo_AA_PDI_PSE_Log" MasterPageFile="~/plantillas/SILPA.master" %>

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
        <asp:Label ID="lbl_titulo_principal" runat="server" SkinID="titulo_principal_blanco" Text="Auditoria de PSE"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True">
        </asp:ScriptManager>
    </div>

    <div class="table-responsive">
        <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
            <tr>
                <td>
                    <asp:Label ID="lblEntidad0" runat="server" SkinID="etiqueta_negra" Text="Fecha"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblEntidad4" runat="server" SkinID="etiqueta_negra" Text="Inicial" Width="40px"></asp:Label>&nbsp;
                    <asp:TextBox ID="txtFechaIni" runat="server" MaxLength="10" Width="20%" ToolTip="La fecha debe estar en formato dd/mm/aaaa"></asp:TextBox>&nbsp;
                    <cc1:CalendarExtender ID="calFechaIni" runat="server" TargetControlID="txtFechaIni"></cc1:CalendarExtender>
                    <asp:Label ID="Label1" runat="server" SkinID="etiqueta_negra" Text="Final" Width="40px"></asp:Label>&nbsp;
                    <asp:TextBox ID="txtFechaFin" runat="server" MaxLength="10" Width="20%" ToolTip="La fecha debe estar en formato dd/mm/aaaa"></asp:TextBox>
                    <cc1:CalendarExtender ID="calFechaFin" runat="server" TargetControlID="txtFechaFin"></cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEntidad6" runat="server" SkinID="etiqueta_negra" Text="Login del Usuario"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLoginUsuario" runat="server" MaxLength="50" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEntidad7" runat="server" SkinID="etiqueta_negra" Text="Identificación"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIdentificacion" runat="server" MaxLength="20" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEntidad8" runat="server" SkinID="etiqueta_negra" Text="Nombre del Usuario"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNombreUsuario" runat="server" MaxLength="50" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEntidad5" runat="server" SkinID="etiqueta_negra" Text="Autoridad Ambiental Relacionada"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboAutoridadAmbiental" runat="server" Width="100%">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="txtModulo" runat="server" SkinID="etiqueta_negra" Text="Módulo"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboModulo" runat="server" Width="100%">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEntidad9" runat="server" SkinID="etiqueta_negra" Text="Acción Realizada"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboAccionRealizada" runat="server" Width="100%">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="1">ALMACENAR</asp:ListItem>
                        <asp:ListItem Value="2">CONSULTAR</asp:ListItem>
                        <asp:ListItem Value="3">EDITAR</asp:ListItem>
                        <asp:ListItem Value="4">ELIMINAR</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEntidad10" runat="server" SkinID="etiqueta_negra" Text="Detalle de la Acción Realizada"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDetalleAccion" runat="server" MaxLength="4000" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                </td>
            </tr>
        </table>
        <div style="text-align: left; vertical-align: top; margin: 0; padding: 0;">
            <asp:GridView ID="grdResultado" runat="server" Width="100%" AutoGenerateColumns="False"
                CellPadding="4" CellSpacing="2" GridLines="None" ForeColor="#333333">
                <RowStyle BackColor="#E3EAEB"></RowStyle>
                <Columns>
                    <asp:BoundField DataField="AUD_FECHA" HeaderText="Fecha"></asp:BoundField>
                    <asp:BoundField DataField="AUD_LOGIN_USUARIO" HeaderText="Login Usuario"></asp:BoundField>
                    <asp:BoundField DataField="AUD_IDENTIF_USUARIO" HeaderText="Identificación Usuario" />
                    <asp:BoundField DataField="AUD_NOMBRE_USUARIO" HeaderText="Nombre Usuario" />
                    <asp:BoundField DataField="AUD_AUTORIDAD_AMBIENTAL_RELACIONADA" HeaderText="Autoridad Ambiental Relacionada" />
                    <asp:BoundField DataField="AUD_MODULO" HeaderText="Módulo" />
                    <asp:BoundField DataField="AUD_ACCION_REALIZADA" HeaderText="Acción Realizada" />
                    <asp:BoundField DataField="AUD_DETALLE_ACCION_REALIZADA" HeaderText="Detalle de la Acción Realizada" />
                </Columns>
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></FooterStyle>
                <PagerStyle HorizontalAlign="Center" BackColor="#666666" ForeColor="White"></PagerStyle>
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></HeaderStyle>
                <EditRowStyle BackColor="#7C6F57"></EditRowStyle>
                <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

