<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="InsertarJurisidiccion.aspx.cs" Inherits="LicenciasAmbientales_InsertarJurisidiccion" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" SkinID="titulo_principal_blanco" Text="INSERTAR JURISDICCIÓN"></asp:Label>
</div>
<div class="div-contenido">
    <asp:ScriptManager ID="scmManejador" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="uppPanelJurisdiccion" runat="server">
        <ContentTemplate>
            <div style="text-align: center">
            <table  style="width: 70%; border: solid 1px #D8D8D8">
                <tr>
                    <td>
                        <table>
                        <tr>
                            <td align="left">                    
                                <asp:Label ID="lblAutoridadAmbiental" SkinID="etiqueta_negra" runat="server" Text="Autoridad Ambiental:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="cboAutoridadAmbiental" runat="server" SkinID="lista_desplegable">
                                </asp:DropDownList>
                            </td>
                        </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblDepartamento" runat="server" SkinID="etiqueta_negra" Text="Departamento:"></asp:Label></td>
                                <td align="left">
                                    <asp:DropDownList ID="cboDepartamento" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboDepartamento_SelectedIndexChanged"
                                        SkinID="lista_desplegable">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblMunicipio" runat="server" SkinID="etiqueta_negra" Text="Municipio:"></asp:Label></td>
                                <td align="left">
                                    <asp:DropDownList ID="cboMunicipio" runat="server" SkinID="lista_desplegable">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" SkinID="boton_copia"
                                        Text="Agregar" /></td>
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

