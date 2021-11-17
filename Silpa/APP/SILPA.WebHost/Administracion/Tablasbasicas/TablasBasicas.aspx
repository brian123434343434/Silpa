<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="TablasBasicas.aspx.cs" Inherits="Administracion_Tablasbasicas_TablasBasicas" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceHolder" runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>
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

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>

    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="TABLAS BASICAS" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="table-responsive">
        <asp:UpdatePanel ID="updConsultar" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlMaestro" runat="server" Width="100%">
                    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
                        <tr>
                            <td>
                                <asp:Label ID="lblNombreParametro" SkinID="etiqueta_negra" runat="server" Text="Descripción"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombreParametro" SkinID="texto" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                                <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%;">
                                    <tr>
                                        <td style="padding-right: 15px; text-align: right; vertical-align: middle;">
                                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" SkinID="boton_copia" OnClick="btnBuscar_Click" />
                                        </td>
                                        <td style="padding-left: 15px; text-align: left; vertical-align: middle;">
                                            <asp:Button ID="btCancelar" runat="server" Text="Cancelar" SkinID="boton_copia" OnClick="btCancelar_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlConsultar" runat="server" Visible="true" Width="100%">
                    <asp:GridView ID="grdDatos" runat="server"
                        AutoGenerateColumns="False"
                        Width="100%"
                        AllowPaging="True"
                        SkinID="Grilla" 
                        CellPadding="4" CellSpacing="2"
                        AllowSorting="True" EmptyDataText="No existen datos registrados en ésta tabla" OnPageIndexChanging="grdDatos_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Nombre Tabla">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkModificar" runat="server" PostBackUrl='<%# Bind("URL_TABLA") %>' Text='<%# Bind("NOMBRE_TABLA") %>'>Modificar Registro</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>


