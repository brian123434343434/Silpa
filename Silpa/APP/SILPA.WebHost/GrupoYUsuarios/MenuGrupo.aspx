<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="MenuGrupo.aspx.cs" Inherits="GrupoYUsuarios_MenuGrupo" Title="Grupo y Menu" %>

<asp:Content ID="Content3" ContentPlaceHolderID="headPlaceHolder" runat="Server">
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

    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" SkinID="titulo_principal_blanco" Text="GRUPO Y MENU"></asp:Label>
    </div>

    <asp:ScriptManager ID="scmManejador" runat="server" EnableScriptGlobalization="true"></asp:ScriptManager>

    <div class="table-responsive">
        <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
            <tr>
                <td>Nombre:</td>
                <td>
                    <asp:TextBox ID="txtNombreMenu" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%;">
                        <tr>
                            <td style="padding-right: 15px; text-align: right; vertical-align: middle;">
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click"></asp:Button>
                            </td>
                            <td style="padding-left: 15px; text-align: left; vertical-align: middle;">
                                <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div style="text-align: left; vertical-align: top; margin: 0; padding: 0;">
            <asp:GridView OnRowCommand="grdMenus_rowcommand" ID="grdMenus" runat="server" SkinID="Grilla" OnPageIndexChanging="grdMenus_PageIndexChanging" Width="100%" ForeColor="Black" BorderStyle="Solid" BorderWidth="1px" BorderColor="#999999" BackColor="White" GridLines="Vertical" CellPadding="0" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="Menú">
                        <ItemTemplate>
                            <asp:Label ID="lblMenuId" Text='<%# Eval("GMO_ID") %>' runat="server" Style="display: none;"></asp:Label>
                            <asp:Label ID="Label1" Text='<%# Eval("GMO_NOMBRE_MENU") %>' runat="server" SkinID="etiqueta_negra10N"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="GMO_ESTADO" HeaderText="Estado" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" />
                    <asp:ButtonField CommandName="edit" Text="Editar" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center"></asp:ButtonField>
                    <asp:ButtonField CommandName="desactivate" Text="Desactivar" ItemStyle-Width="65px" ItemStyle-HorizontalAlign="Center"></asp:ButtonField>
                    <asp:ButtonField CommandName="activate" Text="Activar" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"></asp:ButtonField>
                </Columns>
                <FooterStyle BackColor="#CCCCCC"></FooterStyle>
                <PagerStyle HorizontalAlign="Center" BackColor="#999999" ForeColor="Black"></PagerStyle>
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White"></HeaderStyle>
                <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
