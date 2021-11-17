<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="RealizarBusqueda.aspx.cs" Inherits="Impersonalizacion_RealizarBusqueda" Title="Impersonalización" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" runat="Server">
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
        .Button {
            background-color: #ddd;
        }
    </style>

    <div class="div-titulo">
        <asp:Label ID="Label3" runat="server" SkinID="titulo_principal_blanco" Text="IMPERSONALIZACIÓN"></asp:Label>
    </div>

    <div class="div-contenido">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
            <tr>
                <td>
                    <asp:Label ID="lblTipoId" runat="server" Text="Tipo de Identificación: " Font-Bold="true" SkinID="etiqueta_negra"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboTipoIdentificacion" runat="server" Width="240px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Número de Identificación: " Font-Bold="true" SkinID="etiqueta_negra"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNumeroIdentificacion" runat="server" Width="184px"></asp:TextBox>&nbsp;
                    &nbsp;
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="88px" OnClick="btnBuscar_Click" 
                        style="padding: 3px 3px !important; margin: 0 !important;" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Nombre / Razón Social: " Font-Bold="true" SkinID="etiqueta_negra"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblNombre" runat="server" SkinID="etiqueta_negra" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
            </tr>
            <%--<tr>
                    <td>
                        <asp:Label ID="lblSectorQueja" runat="server" SkinID="etiqueta_negra" Text="Sector:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cboSectorQueja" runat="server" SkinID="lista_desplegable">
                        </asp:DropDownList>
                    </td>
            </tr>--%>
            <tr>
                <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                    <asp:Button ID="btnIniciar" runat="server" SkinID="boton_copia" Text="Iniciar" 
                        OnClick="btnIniciar_Click" Width="120px" Visible="False" 
                        style="padding: 3px 3px !important; margin: 0 !important;" />
                </td>
            </tr>
        </table>
            <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    </div>
</asp:Content>
