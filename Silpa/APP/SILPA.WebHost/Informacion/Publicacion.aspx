<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="Publicacion.aspx.cs" Inherits="Informacion_Publicacion" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
        <asp:Label ID="lbl_titulo_principal" SkinID="titulo_principal_blanco" runat="server" Text="PUBLICACIÓN"></asp:Label>
    </div>

	<%--<div class="div-contenido">--%>
    <div class="table-responsive">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
        </asp:ScriptManager>

        <asp:UpdatePanel ID="updDatosPub" runat="server">
            <ContentTemplate>
                <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Acto Administrativo:" SkinID="etiqueta_negra"></asp:Label></td>
                        <td>
                            <asp:DropDownList SkinID="lista_desplegable" ID="ddlActoAdmin" runat="server" Width="205px" OnSelectedIndexChanged="ddlActoAdmin_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblNumDoc" runat="server" Text="Número del Documento:" SkinID="etiqueta_negra"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtNumeroDoc" SkinID="texto" runat="server" Width="200px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" SkinID="etiqueta_negra" Text="Fecha de Expedición del Documento:"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtFechaExp" SkinID="texto" runat="server" Width="200px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy/MM/dd"
                                TargetControlID="txtFechaExp">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" SkinID="etiqueta_negra" Text="Título de la Publicación:"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtTituloPublicacion" SkinID="texto" runat="server" Width="200px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" SkinID="etiqueta_negra" Text="Texto de la Publicación:"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtTextoPublicacion" runat="server" Height="99px" SkinID="texto" Width="200px" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFechaPub" runat="server" Text="Fecha de Publicación:" SkinID="etiqueta_negra"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtFechaPub" runat="server" Width="175px" SkinID="texto"></asp:TextBox>
                
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFechaPub" Format="yyyy/MM/dd">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAdjuntarDoc" runat="server" Text="Adjuntar Documento:" SkinID="etiqueta_negra"></asp:Label></td>
                        <td>
                            <asp:FileUpload ID="fupArchivo" runat="server" /></td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <table style="width: 601px">
            <tr>
                <td style="width: 296px; height: 26px; text-align: center">
                    <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" SkinID="boton" OnClientClick="return alert('Se ha publicado el documento')" OnClick="btnAceptar_Click" /></td>
                <td colspan="2" style="height: 26px; text-align: center">
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" SkinID="boton" /></td>
            </tr>
        </table>
    </div>
</asp:Content>

