<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/plantillas/SILPALimpia.master" CodeFile="DetalleDocumentos.aspx.cs" Inherits="Informacion_DetalleDocumentos" %>

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

    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" SkinID="titulo_principal_blanco"
            Text="DETALLE DE DOCUMENTO DE LA PUBLICACION"></asp:Label>
    </div>

    <div class="table-responsive" style="text-align: center !important; vertical-align: top !important;">
        <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%;">
            <tr>
                <td style="text-align: left; vertical-align: top;">
                    <asp:GridView ID="grdDetalleDocumento" CellPadding="4" CellSpacing="2"
                        runat="server" AutoGenerateColumns="False" SkinID="Grilla"
                        OnRowCommand="grdDetalleDocumento_RowCommand" DataKeyNames="NombreArchivo,Ubicacion">
                        <Columns>
                            <asp:BoundField DataField="NombreArchivo" HeaderText="Archivo" />
                            <asp:ButtonField CommandName="Descargar" ShowHeader="True" Text="Descargar" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <table border="0" style="left: 0 !important; margin: 0 !important; padding: 5px !important; width: 100%;">
            <tr>
                <td style="padding-top: 20px; padding-bottom: 10px; padding-left: 10px; padding-right: 20px; text-align: center; vertical-align: middle;">
                    <asp:Label ID="lblExisteDocumento" runat="server" Text="No hay documentos adjuntos..." Visible="False" ForeColor="Red" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 20px; padding-bottom: 10px; padding-left: 10px; padding-right: 20px; text-align: center; vertical-align: middle;">
                    <asp:Button ID="btnRegresar" runat="server" OnClick="Button1_Click" Text="Regresar" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
