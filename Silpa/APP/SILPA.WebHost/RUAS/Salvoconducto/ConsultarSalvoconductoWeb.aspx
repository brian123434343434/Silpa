<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ConsultarSalvoconductoWeb.aspx.cs" Inherits="Salvoconducto_ConsultarSalvoconductoWeb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="div-titulo">
    <asp:Label ID="lbl_titulo" SkinID="texto" runat="server" Text="Consulta de Salvoconducto"></asp:Label>
    </div>
    <div class="div-contenido">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnl_datos_consulta" runat="server" Width="400px">
                    <table style="width:400px;">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lbl_subtitulo" runat="server" Text="Datos de Salvoconducto" SkinID="titulo_principal"></asp:Label>
                        </td>
                    </tr>
                        <tr>
                            <td style="text-align:left">
                                <asp:Label ID="lbl_numero" runat="server" Text="Número de Salvoconducto:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                            <td style="text-align:right">
                                <asp:TextBox ID="txt_numero" runat="server" SkinID="texto"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:right">
                                <asp:Button ID="btn_consultar" runat="server" Text="Consular" SkinID="boton" 
                                    onclick="btn_consultar_Click" />
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
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnl_resultado" runat="server" Visible="false" Width="400px">
                    <asp:GridView ID="dgv_resultado" runat="server" style="text-align: center" 
                        ShowHeader="False" Width="400px">
                    </asp:GridView>
                    <asp:Label ID="lbl_resultado_error" runat="server" SkinID="etiqueta_roja_error" 
                        style="text-align: center" 
                        Text="No se encontró ningún resultado para el número de Salvoconducto ingresado, o no se ha Expedido Documento aún." 
                        Visible="False"></asp:Label>
                </asp:Panel>
            </ContentTemplate>
        
        </asp:UpdatePanel>
    </div>
</asp:Content>

