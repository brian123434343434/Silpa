<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="AdquisicionDocumentos.aspx.cs" Inherits="AdquisicionDocumentos_AdquisicionDocumentos" Title="Untitled Page" %>

<asp:Content ID="Content3" ContentPlaceHolderID="headPlaceHolder" runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        table {
            /*border: 1px solid #000;*/
        }
        table tr td {
            border: 0px solid #ddd !important;
            padding: 4px;
        }
        .Button{
            background-color: #ddd;
        }
    </style>

    <div class="div-titulo">
        <asp:Label ID="lbl_titulo_principal" runat="server" SkinID="titulo_principal_blanco" Text="Adquisición de Documentos"></asp:Label>
    </div>

    <%--<div class="div-contenido2">--%>
    <div class="table-responsive">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%;">
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lblEntidad" runat="server" SkinID="etiqueta_negra" Width="150px" Text="Entidad Externa"></asp:Label>
                                <asp:DropDownList ID="cboEntidad" runat="server" SkinID="lista_desplegable" AutoPostBack="True" OnSelectedIndexChanged="cboEntidad_SelectedIndexChanged">
                                    <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTexto" runat="server" Text="Seleccione el documento que requiere solicitar para su trámite" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="grdDocumentos" runat="server" Width="100%" DataKeyNames="DOC_ID" 
                                    AutoGenerateColumns="False" CellPadding="4" CellSpacing="4" 
                                    GridLines="None" PageSize="1" ForeColor="#333333" 
                                    OnPageIndexChanging="grdDocumentos_PageIndexChanging" 
                                    OnRowDataBound="grdDocumentos_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Codigo Documento" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("DOC_ID") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entidad">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEntidad" runat="server" Text='<%# Bind("ENTIDAD") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre Documento">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkNombre" runat="server" NavigateUrl='<%# Bind("ENLACE") %>' Text='<%# Bind("NOMBRE") %>' CssClass="a_green"></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Imagen">
                                            <ItemTemplate>
                                                <asp:Image ID="imgDocumento" runat="server" Width="188px"></asp:Image>
                                                <asp:HiddenField ID="hdfImagen" runat="server" Value='<%# Eval("IMAGEN_URL") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle Font-Size="9pt" ForeColor="#000000" BackColor="#E3EAEB" BorderWidth="1px" BorderColor="#BBBBBB" BorderStyle="Solid" />
                                    <FooterStyle Font-Size="9pt" BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></FooterStyle>
                                    <PagerStyle Font-Size="9pt" HorizontalAlign="Center" BackColor="#666666" ForeColor="White"></PagerStyle>
                                    <SelectedRowStyle Font-Size="9pt" BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                    <HeaderStyle Font-Size="12pt" BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                    <EditRowStyle Font-Size="9pt" BackColor="#7C6F57"></EditRowStyle>
                                    <AlternatingRowStyle Font-Size="9pt" BackColor="White"></AlternatingRowStyle>
                                </asp:GridView>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

