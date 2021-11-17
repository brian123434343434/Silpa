<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="RegistrarRecurso.aspx.cs" Inherits="Recurso_RegistrarRecurso" Title="Untitled Page" %>

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

    <script type="text/javascript" src="../js/JScript.js"></script>

    <div class="div-titulo">
        <asp:Label ID="lblNumeroSilpaReal" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblTituloPrincipal" runat="server" SkinID="titulo_principal_blanco" Text="RECURSO DE REPOSICIÓN"></asp:Label>
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <div class="table-responsive">
        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
            <tr>
                <td>
                    <asp:UpdatePanel ID="upd_pnl1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <cc1:TabContainer ID="tbcContenedor" runat="server" ActiveTabIndex="0">
                        <cc1:TabPanel runat="server" HeaderText="Datos de Recurso" ID="tabDatosRecurso">
                            <ContentTemplate>
                                <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; width: 100%;">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblNombreProy" runat="server" Text="Nombre Proyecto:" SkinID="etiqueta_negra"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNombreProyDato" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblNumeroSILPA" runat="server" Text="Número VITAL:" SkinID="etiqueta_negra"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNumeroSILPADato" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblNumeroExpediente" runat="server" Text="Número de Expediente:" SkinID="etiqueta_negra"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNumeroExpedienteDato" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="Número Acto Administrativo:" SkinID="etiqueta_negra"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNumeroActoDato" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblFechaActo" runat="server" SkinID="etiqueta_negra">Fecha de Acto Administrativo:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFechaActoDato" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblFechaNoti" runat="server" SkinID="etiqueta_negra">Fecha de Notificacion:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFechaNotiDato" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;">
                                                <asp:Label ID="lblDescripcion" runat="server" SkinID="etiqueta_negra">Descripción</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDescripcion" runat="server" SkinID="texto" TextMode="MultiLine" Rows="8" Width="100%" style="resize: none;"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="padding: 10px; text-align: left; vertical-align: middle;">
                                                <asp:Label ID="lblMensajeAgregar" runat="server" Text='Para agregar documentos a su recurso, por favor dar clic sobre el botón "Agregar"'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 0; margin: 0; text-align: left; vertical-align: top;" colspan="2">
                                                <asp:GridView ID="grdDocumentosRecurso" runat="server" Width="493px" 
                                                    DataKeyNames="ID"
                                                    SkinID="Grilla_simple_peq" 
                                                    OnRowCommand="grdDocumentosRecurso_RowCommand">
                                                    <HeaderStyle BackColor="#3E4D60" Font-Bold="True" ForeColor="#EBEEF1" Font-Size="9pt" />
                                                    <FooterStyle BackColor="#CFD7DE" Font-Bold="True" ForeColor="#000000" Font-Size="9pt" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#1C222B" Font-Size="9px" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#1C222B" Font-Size="9px" />
                                                    <EditRowStyle BackColor="#1C222B" Font-Size="9px" />
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#1C222B" Font-Size="9px" />
                                                    <Columns>
                                                        <asp:ButtonField CommandName="Actualizar" Text="Ver" ControlStyle-CssClass="a_green"></asp:ButtonField>
                                                        <asp:ButtonField CommandName="Eliminar" Text="Eliminar" ControlStyle-CssClass="a_red"></asp:ButtonField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="padding: 20px; text-align: left; vertical-align: middle;">
                                                <asp:Button ID="btnAgregarDocumentoRecurso" OnClick="btnAgregarDocumentoRecurso_Click" runat="server" Text="Agregar" SkinID="boton_copia" CausesValidation="False"></asp:Button>
                                                <div style="visibility: hidden">
                                                    <asp:Button ID="btnActualizarDocumentoRecurso" runat="server" Text="Actualizar" SkinID="boton_copia" CausesValidation="False"></asp:Button>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                    <asp:Button ID="btnRegistrar" runat="server" CausesValidation="False" SkinID="boton_copia" Text="Enviar" OnClick="btnRegistrar_Click" OnClientClick="fntMensajeRecurso" />
                    <asp:Button ID="Button1" runat="server" CausesValidation="False" SkinID="boton_copia" Text="Regresar" OnClick="btnRegresar_Click" OnClientClick="fntMensajeRecurso" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
