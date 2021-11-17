<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPASinMenu.master" AutoEventWireup="true"
    CodeFile="insertarDocumento.aspx.cs" Inherits="Administracion_AdministracionDocumentos_insertarDocumento"
    Title="Untitled Page" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="div-contenido">
        <cc1:ToolkitScriptManager ID="ScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblTipoDocumento" SkinID="etiqueta_negra" runat="server" Text="Tipo de Documento:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTipoDocumento" SkinID="texto" runat="server" CausesValidation="True"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="*" ControlToValidate="txtTipoDocumento" runat="server" ErrorMessage="*Debe escribir un Nombre"></asp:RequiredFieldValidator>
                        
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblParametro" SkinID="etiqueta_negra" runat="server" Text="Parámetro/Actividad BPM"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlParametro" SkinID="lista_desplegable" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 135px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center" style="height: 26px">
                            <asp:Button ID="btnGuardar" SkinID="boton_copia" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
