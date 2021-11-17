<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="EstadoTramite.aspx.cs" Inherits="EstadoTramite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="div-titulo">
        <asp:Label ID="lbl_titulo" runat="server" Text="Consulta de Estado de Trámite" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="div-contenido">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_numero_silpa" runat="server" Text="Número SILPA:" SkinID="etiqueta_negra"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_numero_silpa" SkinID="texto" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            <asp:Button ID="btn_consultar" runat="server" Text="Consultar" SkinID="boton" OnClick="btn_consultar_Click" />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lbl_estado" runat="server" Text="" SkinID="etiqueta_roja_error"></asp:Label>
                <asp:Panel ID="Panel1" runat="server" Visible="false">
                    <asp:Label ID="lbl_tramite" runat="server" Text="Su trámite a cumplido con las siguientes actividades:"></asp:Label>
                    <asp:TreeView ID="tv_proceso" runat="server" ImageSet="BulletedList" ShowExpandCollapse="False">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <Nodes>
                            <asp:TreeNode Text="Liquidación" Expanded="True">
                                <asp:TreeNode Text="Solicitud de Liquidación" />
                                <asp:TreeNode Text="Generación de Liquidación" />
                                <asp:TreeNode Text="Generación de Documento de Cobro" />
                                <asp:TreeNode Text="Pago de Documento de Cobro" />
                            </asp:TreeNode>
                            <asp:TreeNode Text="Permiso" Expanded="True">
                                <asp:TreeNode Text="Solicitud de Permiso Para Consesión de Aguas Subterráneas" />
                            </asp:TreeNode>
                        </Nodes>
                        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="0px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                    </asp:TreeView>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--BODY--%>
        <%--/BODY--%>
    </div>
</asp:Content>
