<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="Parametrizacion.aspx.cs" Inherits="Parametrizacion" EnableEventValidation="false" %>
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
                            <td style="width: 48%; text-align: left">
                                <asp:HyperLink ID="HlkAdicionarModulo" runat="server" NavigateUrl="~/ParametrizacionSensibilizacion/Modulo.aspx?modo=0">Adicionar Modulo</asp:HyperLink></td>
                            <td style="width: 324px; text-align: left">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 48%; text-align: left">
                                <asp:HyperLink ID="HlkAdicionarContenido" runat="server" NavigateUrl="~/ParametrizacionSensibilizacion/Contenido.aspx?modo=0">Adicionar Contenido</asp:HyperLink>
                            </td>
                            <td style="width: 324px; text-align: left">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 48%; text-align: left">
                                <asp:LinkButton ID="BtnModModulo" runat="server" OnClick="BtnModModulo_Click">Modificar Modulo</asp:LinkButton></td>
                            <td style="width: 324px; text-align: left">
                                <asp:DropDownList ID="CboModulo" runat="server" Width="210px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 48%; text-align: left">
                                <asp:LinkButton ID="BtnModContenido" runat="server" OnClick="BtnModContenido_Click">ModificarContenido</asp:LinkButton></td>
                            <td style="width: 324px; text-align: left">
                                <asp:DropDownList ID="CboContenido" runat="server" Width="210px">
                                </asp:DropDownList></td>
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
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center; height: 132px;">
                    <asp:GridView ID="dgv_resultado" runat="server" style="text-align: center" Width="400px" OnRowCommand="dgv_resultado_RowCommand" OnSelectedIndexChanged="dgv_resultado_SelectedIndexChanged" OnRowEditing="dgv_resultado_RowEditing" >
                        <Columns>
                            <asp:ButtonField ButtonType="Image" CommandName="Edit" HeaderText="Editar"
                                ImageUrl="~/ParametrizacionSensibilizacion/Imagenes/imagesCAX7DC3E.jpg" />
                            <asp:ButtonField ButtonType="Image" CommandName="Add" HeaderText="Adicionar"
                                ImageUrl="~/ParametrizacionSensibilizacion/Imagenes/imagesCA67TDJY.jpg" />
                        </Columns>
                    </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: right">
                                <asp:Button ID="BtnSalir" runat="server" Text="Salir" /></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                    <asp:Label ID="lbl_resultado_error" runat="server" SkinID="etiqueta_roja_error" 
                        style="text-align: center" 
                        Text="No se han encontrado datos" 
                        Visible="False"></asp:Label></td>
                        </tr>
                    </table>
                </asp:Panel>
                &nbsp;
            </ContentTemplate>
        
        </asp:UpdatePanel>
    </div>
</asp:Content>

