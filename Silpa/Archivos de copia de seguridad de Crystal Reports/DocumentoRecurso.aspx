<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="DocumentoRecurso.aspx.cs" Inherits="Recurso_DocumentoRecurso" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
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

    <asp:ScriptManager ID="scmManejadorRecurso" runat="server"></asp:ScriptManager>

    <div class="table-responsive">
        <cc1:TabContainer ID="tbcContenedor" runat="server" ActiveTabIndex="0" Width="568px">
            <cc1:TabPanel ID="tabDatosDocumento" runat="server" HeaderText="Datos Documento">
                <ContentTemplate>
                    <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                        <tr>
                            <td>
                                <asp:Label ID="lblNumeroSILPA" runat="server" Text="Documento" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:FileUpload ID="fupDocumento" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNumeroRadicado" runat="server" Text="Número de Radicado:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumeroRadicado" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                                <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" Text="Aceptar" SkinID="boton_copia" />
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" SkinID="boton_copia" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </div>
</asp:Content>
