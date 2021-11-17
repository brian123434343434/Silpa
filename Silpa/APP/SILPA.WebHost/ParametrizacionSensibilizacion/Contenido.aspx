<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="Contenido.aspx.cs" Inherits="Contenido" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="div-titulo">
    
    <asp:Label ID="lbl_titulo" SkinID="texto" runat="server" Text="Consulta de Trámites"></asp:Label>
    </div>
    <div class="div-contenido">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnl_datos_consulta" runat="server" Width="500px">
                    <table style="width:500px;">
                    <tr>
                        <td colspan="2" style="height: 21px">
                            <asp:Label ID="lbl_subtitulo" runat="server" Text="Contenido" SkinID="titulo_principal"></asp:Label>
                        </td>
                        <td colspan="1" style="width: 88px; height: 21px;">
                        </td>
                        <td colspan="1" style="height: 21px; width: 4px;">
                        </td>
                    </tr>
                        <tr>
                            <td style="width: 16%; text-align: left">
                                Identificador:</td>
                            <td style="text-align:left">
                                <asp:TextBox ID="TxtId" runat="server" ReadOnly="True" Width="244px"></asp:TextBox></td>
                            <td style="text-align: left">
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 16%; text-align: left">
                                Tipo de Contenido:</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TxtModulo" runat="server" Width="243px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqDescripcion" runat="server" ControlToValidate="TxtModulo"
                                    ErrorMessage="Este campo de requerido para la opertación"></asp:RequiredFieldValidator></td>
                            <td style="text-align: left">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 16%; text-align: left">
                            </td>
                            <td style="text-align: left">
                                &nbsp;
                                <asp:RadioButton ID="ChkInactivo" runat="server" GroupName="ActivoInactivo"
                                    Text="Inactivo" />
                                <asp:RadioButton ID="ChkActivo" runat="server" GroupName="ActivoInactivo" Text="Activo" /></td>
                            <td style="text-align: left">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:right">
                                <asp:Button ID="BtnAceptar" runat="server" Text="Aceptar" OnClick="BtnAceptar_Click" />
                                <asp:Button ID="BtnCancelar" runat="server" Text="Salir" OnClick="BtnCancelar_Click" />
                               </td>
                            <td colspan="1" style="width: 88px; text-align: right">
                            </td>
                            <td colspan="1" style="text-align: right; width: 4px;">
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center" colspan="2">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                    <ProgressTemplate>
                                        <asp:Image ID="img_progress" runat="server" 
                                            ImageUrl="~/App_Themes/Img/ajax-loader.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                            <td colspan="1" style="width: 88px; text-align: center">
                            </td>
                            <td colspan="1" style="text-align: center; width: 4px;">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                    <asp:Label ID="lbl_resultado_error" runat="server" SkinID="etiqueta_roja_error" 
                        style="text-align: center" 
                        Text="No se han encontrado datos" 
                        Visible="False"></asp:Label></td>
                            <td colspan="1" style="width: 88px; text-align: center">
                            </td>
                            <td colspan="1" style="text-align: center; width: 4px;">
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                &nbsp;
            </ContentTemplate>
        
        </asp:UpdatePanel>
    </div>
</asp:Content>

