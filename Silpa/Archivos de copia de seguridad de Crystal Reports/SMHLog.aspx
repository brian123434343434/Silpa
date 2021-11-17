<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SMHLog.aspx.cs" Inherits="Auditoria_SMHLog"
    MasterPageFile="~/plantillas/SILPA.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="div-titulo">
        <asp:Label ID="lbl_titulo_principal" runat="server" SkinID="titulo_principal_blanco"
            Text="Consulta de LOG"></asp:Label><br />
        <br />
    </div>
    <div class="div-contenido2">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table cellpadding="2" style="width: 100%">
            <tr>
                <td style="width: 30%">
                    &nbsp;</td>
                <td style="width: 70%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFecha" runat="server" SkinID="etiqueta_negra" Text="Fecha"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblFechaIni" runat="server" SkinID="etiqueta_negra" Text="Inicial"
                        Width="40px"></asp:Label>
                    &nbsp;<asp:TextBox ID="txtFechaIni" runat="server" MaxLength="10" Width="20%" ToolTip="La fecha debe estar en formato dd/mm/aaaa"></asp:TextBox>
                    
                    <cc1:CalendarExtender ID="calFechaInicial" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaIni">
                    </cc1:CalendarExtender>
                    
                    <asp:Label ID="lblFechaFin" runat="server" SkinID="etiqueta_negra" Text="Final" Width="40px"></asp:Label>
                    <asp:TextBox ID="txtFechaFin" runat="server" MaxLength="10" Width="20%" ToolTip="La fecha debe estar en formato dd/mm/aaaa"></asp:TextBox>
                    <cc1:CalendarExtender ID="calFechaFinal" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaFin">
                    </cc1:CalendarExtender>                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblusuario" runat="server" SkinID="etiqueta_negra" Text="Usuario"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUsuario" runat="server" MaxLength="50" Width="60%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMaquina" runat="server" SkinID="etiqueta_negra" Text="Maquina"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMaquina" runat="server" MaxLength="20" Width="60%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEntidad5" runat="server" SkinID="etiqueta_negra" Text="Autoridad Ambiental Relacionada"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboSeveridad" runat="server" Width="61%" OnSelectedIndexChanged="cboSeveridad_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    &nbsp;<asp:UpdatePanel ID="UPbuscar" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />                
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>          
                                    </td>
                                </tr>
                            </table>
                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:UpdatePanel ID="UPauditoria" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="grdResultado" runat="server" Width="100%" ForeColor="#333333" GridLines="None"
                                CellPadding="4" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grdResultado_PageIndexChanging">
                                <RowStyle BackColor="#E3EAEB"></RowStyle>
                                <Columns>
                                    <asp:BoundField DataField="MAQUINA" HeaderText="Maquina"></asp:BoundField>
                                    <asp:BoundField DataField="MENSAJE" HeaderText="Mensaje"></asp:BoundField>
                                    <asp:BoundField DataField="SEVERIDAD" HeaderText="Severidad"></asp:BoundField>
                                    <asp:BoundField DataField="FECHA_REGISTRO" HeaderText="Fecha Registro"></asp:BoundField>
                                    <asp:BoundField DataField="USUARIO" HeaderText="Usuario"></asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></FooterStyle>
                                <PagerStyle HorizontalAlign="Center" BackColor="#666666" ForeColor="White"></PagerStyle>
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                <EditRowStyle BackColor="#7C6F57"></EditRowStyle>
                                <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                  
                </td>
            </tr>
        </table>
        &nbsp;</div>
</asp:Content>
