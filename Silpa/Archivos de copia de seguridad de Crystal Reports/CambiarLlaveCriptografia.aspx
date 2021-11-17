<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="CambiarLlaveCriptografia.aspx.cs" Inherits="Administracion_Tablasbasicas_CambiarLlaveCriptografia" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="CAMBIO LLAVE CRIPTOGRAFO" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
     <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="div-contenido">
    <table width="100%">
        <tr>
            <td>
                 <asp:UpdatePanel ID="updConsultar" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td style="width:20%;">
                                    <asp:Label ID="lblLlaveActual" SkinID="etiqueta_negra" runat="server" Text="Valor Actual Llave"></asp:Label>
                                </td>
                                <td style="width:80%;" align="left">
                                    <asp:TextBox ID="txtLlaveActuall" SkinID="texto" runat="server" Width="80%" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:20%;">
                                    <asp:Label ID="Label1" SkinID="etiqueta_negra" runat="server" Text="Nuevo Valor Llave"></asp:Label>
                                </td>
                                <td style="width:80%;" align="left">
                                    <asp:TextBox ID="txtNewLlave" SkinID="texto" runat="server" Width="80%"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="rfvtxtNewLlave" runat="server" ControlToValidate="txtNewLlave" ErrorMessage="Debe Ingresar la nueva Llave" Display="Dynamic" ValidationGroup="Llave">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" SkinID="boton" OnClick="btnGuardar_Click" ValidationGroup="Llave" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                 </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
   
</asp:Content>

