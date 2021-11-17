<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ModuloContenido.aspx.cs" Inherits="ModuloContenido" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="div-titulo">
    
    <asp:Label ID="lbl_titulo" SkinID="titulo_principal_blanco" runat="server" Text="Consulta de Trámites" ></asp:Label>
    </div>
    <div class="div-contenido">
        &nbsp;<table>
            <tr>
                <td colspan="2" style="height:50px" >
                            <asp:Label ID="lbl_subtitulo" runat="server" Text="Modulo" SkinID="titulo_principal"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 149px">
                    Identificador:</td>
                <td style="width: 486px">
                                <asp:TextBox ID="TxtId" runat="server" ReadOnly="True" SkinID="texto"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 149px">
                    Modulos:</td>
                <td style="width: 486px">
                    <asp:DropDownList ID="CboModulo" runat="server" SkinID="lista_desplegable">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 149px">
                                Tipo de Contenido:</td>
                <td style="width: 486px">
                    <asp:DropDownList ID="CboTipoContenido" runat="server" SkinID="lista_desplegable">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 149px; height: 60px;">
                                Archivo:</td>
                <td style="width: 486px; height: 60px;">
                    <asp:FileUpload ID="FlpArchivo" runat="server" /><br />
                    <asp:TextBox ID="TxtArchivos" runat="server" Width="250px"></asp:TextBox>
                    <asp:Button ID="BtnEditar" runat="server" OnClick="BtnEditar_Click" Text="Editar Archivo" /></td>
            </tr>
            <tr>
                <td style="width: 149px">
                    Estado:</td>
                <td style="width: 486px">
                                <asp:RadioButton ID="ChkInactivo" runat="server" GroupName="ActivoInactivo"
                                    Text="Inactivo"  SkinID="radio"/><asp:RadioButton ID="ChkActivo" runat="server" GroupName="ActivoInactivo" Text="Activo" SkinID="radio" /></td>
            </tr>
            <tr>
                <td style="width: 149px">
                </td>
                <td align="right" style="width: 486px">
                                <asp:Button ID="BtnAceptar" runat="server" Text="Aceptar" OnClick="BtnAceptar_Click" SkinID="boton" /><asp:Button ID="BtnCancelar" runat="server" Text="Salir" OnClick="BtnCancelar_Click" SkinID="boton" /></td>
            </tr>
            <tr>
                <td style="width: 149px">
                </td>
                <td align="center" style="width: 486px">
                    <asp:Label ID="lbl_resultado_error" runat="server" SkinID="etiqueta_roja_error" 
                        style="text-align: center" 
                        Text="No se han encontrado datos" 
                        Visible="False"></asp:Label></td>
            </tr>
        </table>
        &nbsp;
    </div>
</asp:Content>

