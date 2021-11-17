<%@ Page Language="C#" MasterPageFile="~/ReporteTramite/ResourcesCP/master/ConsultaPublicaSILPA.master" AutoEventWireup="true" CodeFile="DescargarDocumentos.aspx.cs" Inherits="ReporteTramite_DescargarDocumentos" Title="Descarga de Documentos" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" runat="Server">
    <link href="ResourcesCP/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="ResourcesCP/jquery/jquery-ui.css" rel="stylesheet" />
    <script src="ResourcesCP/jquery/3.2.1/jquery.min.js" type="text/javascript"></script>
    <script src="ResourcesCP/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="div-titulo">
        <asp:Label ID="Label1" runat="server" SkinID="titulo_principal_blanco" Text="Lista de documentos"></asp:Label>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />

    <div class="table-responsive" style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">

        <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%;">
            <tr>
                <td style="text-align: center; border: 0px solid #ddd !important;">
                    <asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:GridView ID="grdVerDocumentos" runat="server" CssClass="table-striped table-bordered table-condensed"
                                AllowPaging="True" AutoGenerateColumns="False"
                                CellSpacing="2" CellPadding="10" SkinID="Grilla_simple_peq" PageSize="10"
                                DataKeyNames="NombreArchivo,Ubicacion"
                                GridLines="None"
                                OnPageIndexChanging="grdVerDocumentos_PageIndexChanging"
                                OnRowCommand="grdVerDocumentos_RowCommand" Width="99%" EnableSortingAndPagingCallbacks="True">
                                <RowStyle ForeColor="#000000" VerticalAlign="Middle" BorderWidth="1" BorderColor="#dddddd" BorderStyle="Solid"></RowStyle>
                                <SelectedRowStyle Font-Bold="True" ForeColor="White" BackColor="#EE7402" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#005695"></HeaderStyle>
                                <Columns>
                                    <asp:BoundField DataField="NombreArchivo" HeaderText="Nombre Archivo" ItemStyle-Width="80%" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDescargar" Text="Descargar" OnClick="lnkDescargar_Click" CommandArgument="<%# Container.DisplayIndex %>"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    <asp:Button ID="btnAtras" runat="server" OnClick="btnAtras_Click" Text="Atras" Height="25px" Width="100px" />
                </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="lblMensajeRuta" runat="server" Style="display: none"></asp:Label>
    <asp:Label ID="lblMensajeError" runat="server" SkinID="etiqueta_roja_error" Style="text-align: center"></asp:Label>
</asp:Content>

