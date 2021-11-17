<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/plantillas/SILPA.master"
    CodeFile="Auditoria.aspx.cs" Inherits="Auditoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="div-titulo">
        <asp:Label ID="lbl_titulo_principal" runat="server" SkinID="titulo_principal_blanco"
            Text="Adquisición de Documentos"></asp:Label><br />
        <br />
    </div>
    <div class="div-contenido2">
        <asp:ScriptManager id="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel id="UpdatePanel1" runat="server">
            <contenttemplate>
<table cellpadding="2" style="width:100%">
            <tr>
                <td style="width:50%">
                    &nbsp;</td>
                <td style="width:50%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEntidad0" runat="server" SkinID="etiqueta_negra" Text="Fecha"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblEntidad4" runat="server" SkinID="etiqueta_negra" 
                        Text="Inicial" Width="40px"></asp:Label>
&nbsp;<asp:TextBox ID="txtFechaIni" runat="server" MaxLength="10"></asp:TextBox><asp:Label ID="Label1" 
                        runat="server" SkinID="etiqueta_negra" Text="Final" Width="40px"></asp:Label>
                    <asp:TextBox ID="txtFechaFin" runat="server" MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEntidad6" runat="server" SkinID="etiqueta_negra" 
                        Text="Login del Usuario"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLoginUsuario" runat="server" MaxLength="50" Width="36%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEntidad7" runat="server" SkinID="etiqueta_negra" 
                        Text="Identificación"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIdentificacion" runat="server" MaxLength="20" Width="36%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEntidad8" runat="server" SkinID="etiqueta_negra" 
                        Text="Nombre del Usuario"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNombreUsuario" runat="server" MaxLength="50" Width="36%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEntidad5" runat="server" SkinID="etiqueta_negra" 
                        Text="Autoridad Ambiental Relacionada"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboAutoridadAmbiental" runat="server" Width="36%">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="txtModulo" runat="server" SkinID="etiqueta_negra" 
                        Text="Módulo"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboModulo" runat="server" Width="36%">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEntidad9" runat="server" SkinID="etiqueta_negra" 
                        Text="Acción Realizada"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboAccionRealizada" runat="server" Width="36%">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEntidad10" runat="server" SkinID="etiqueta_negra" 
                        Text="Detalle de la Acción Realizada"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDetalleAccion" runat="server" MaxLength="4000" Width="36%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" align="center" >
                    <asp:Button ID="btnBuscar" runat="server" 
                        Text="Buscar" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" align="center" >
                    <asp:GridView id="grdResultado" runat="server" Width="100%" 
                        AutoGenerateColumns="False" CellPadding="4" GridLines="None" 
                                                ForeColor="#333333">
<RowStyle BackColor="#E3EAEB"></RowStyle>
<Columns>
<asp:BoundField DataField="AUD_FECHA" HeaderText="Fecha"></asp:BoundField>
<asp:BoundField DataField="AUD_LOGIN_USUARIO" HeaderText="Login Usuario"></asp:BoundField>
    <asp:BoundField DataField="AUD_IDENTIF_USUARIO" 
        HeaderText="Identificación Usuario" />
    <asp:BoundField DataField="AUD_NOMBRE_USUARIO" HeaderText="Nombre Usuario" />
    <asp:BoundField DataField="AUD_AUTORIDAD_AMBIENTAL_RELACIONADA" 
        HeaderText="Autoridad Ambiental Relacionada" />
    <asp:BoundField DataField="AUD_MODULO" HeaderText="Módulo" />
    <asp:BoundField DataField="AUD_ACCION_REALIZADA" 
        HeaderText="Acción Realizada" />
    <asp:BoundField DataField="AUD_DETALLE_ACCION_REALIZADA" 
        HeaderText="Detalle de la Acción Realizada" />
</Columns>

<FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></FooterStyle>

<PagerStyle HorizontalAlign="Center" BackColor="#666666" ForeColor="White"></PagerStyle>

<SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

<HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></HeaderStyle>

<EditRowStyle BackColor="#7C6F57"></EditRowStyle>

<AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
</asp:GridView> 
                </td>
            </tr>
        </table>
</contenttemplate>
        </asp:UpdatePanel>
        &nbsp;</div>
</asp:Content>
