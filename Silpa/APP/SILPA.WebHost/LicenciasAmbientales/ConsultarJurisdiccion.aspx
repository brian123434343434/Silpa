<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ConsultarJurisdiccion.aspx.cs" Inherits="LicenciasAmbientales_ConsultarJurisdiccion" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" SkinID="titulo_principal_blanco" Text="JURISDICCIONES"></asp:Label>
</div>
<div class="div-contenido">
    <asp:ScriptManager ID="scmManejador" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="uppPanelJurisdiccion" runat="server">
        <ContentTemplate>
            <div style="text-align: center">
            <table  style="width: 70%; border: solid 1px #D8D8D8">
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblDepartamento" runat="server" SkinID="etiqueta_negra" Text="Departamento:"></asp:Label></td>
                                <td align="left">
                                    <asp:DropDownList ID="cboDepartamento" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboDepartamento_SelectedIndexChanged"
                                        SkinID="lista_desplegable">
                                        <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblMunicipio" runat="server" SkinID="etiqueta_negra" Text="Municipio:"></asp:Label></td>
                                <td align="left">
                                    <asp:DropDownList ID="cboMunicipio" runat="server" SkinID="lista_desplegable">
                                        <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" SkinID="boton_copia"
                                        Text="Buscar" /></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Text="El municipio no está aosciado a una jurisdicción"
                                        Visible="False"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Panel ID="pnlJurisdicciones" runat="server" Height="200px" ScrollBars="Both" Width="520px">
                                        <asp:GridView ID="grdJurisdicciones" runat="server" AutoGenerateColumns="False" SkinID="Grilla_simple" Width="500px">
                                            <Columns>
                                                <asp:BoundField DataField="JUR_ID" HeaderText="C&#243;digo" />
                                                <asp:BoundField DataField="AUT_NOMBRE" HeaderText="Autoridad Ambiental" />
                                                <asp:BoundField DataField="MUN_NOMBRE" HeaderText="Municipio" />
                                                <asp:BoundField DataField="FECHA_INSERCION" HeaderText="Fecha de Inserci&#243;n" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>        
</div>

</asp:Content>

