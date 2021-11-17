<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPALimpia.master" AutoEventWireup="true"
    CodeFile="DetallesInscripcionAudienciaPublica.aspx.cs" Inherits="Informacion_Publicaciones"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="div-titulo">
        <asp:Label ID="lbl_titulo_principal" runat="server" SkinID="titulo_principal_blanco"
            Text="Inscripción a Audiencia Pública"></asp:Label><br />
    </div>
    <div class="div-contenido2">
        <asp:ScriptManager ID="scmManejador" runat="server" EnableScriptGlobalization="True"
            EnableScriptLocalization="True">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                    border-bottom: silver thin solid; text-align: center">
                    <tr>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center; width: 700px" colspan="4">
                            <asp:Label ID="lblNAudiencia" runat="server" Text="Audiencia Pública No."></asp:Label>
                                <asp:Label ID="lblNoAUP" runat="server"></asp:Label>
                                <asp:Label ID="lblNSilpa" runat="server" Text="Número SILPA" Width="100px"></asp:Label><asp:Label
                                    ID="lblNumeroSILPA" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center">
                            <asp:Label ID="lblNombreP" runat="server" SkinID="etiqueta_negra" Text="Nombre del proyecto, Obra o Actividad"
                                Width="150px" Font-Size="Larger" Font-Overline="False" Font-Underline="False"
                                Font-Strikeout="False" Font-Bold="True"></asp:Label></td>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center">
                            <asp:Label ID="lblProyecto" runat="server" Width="150px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center">
                            <asp:Label ID="lblNumExp" runat="server" SkinID="etiqueta_negra" Text="Número de Expediente "
                                Width="150px" Font-Size="Larger" Font-Overline="False" Font-Underline="False"
                                Font-Strikeout="False" Font-Bold="True"></asp:Label></td>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center">
                            <asp:Label ID="lblNumExpediente" runat="server" Width="150px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center">
                            <asp:Label ID="lblFechaA" runat="server" SkinID="etiqueta_negra" Text="Fecha y hora de celebración de la Audiencia Pública"
                                Width="150px" Font-Size="Larger" Font-Overline="False" Font-Underline="False"
                                Font-Strikeout="False" Font-Bold="True"></asp:Label></td>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center">
                            <asp:Label ID="lblFechaAudiencia" runat="server" Width="150px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center">
                            <asp:Label ID="lblLugarA" runat="server" SkinID="etiqueta_negra" Text="Lugar de celebración de la audiencia pública"
                                Width="150px" Font-Size="Larger" Font-Overline="False" Font-Underline="False"
                                Font-Strikeout="False" Font-Bold="True"></asp:Label>;</td>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center">
                            <asp:Label ID="lblLugarAudiencia" runat="server" Width="150px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center">
                            <asp:Label ID="lblComunidadA" runat="server" SkinID="etiqueta_negra" Text="Comunidad donde se desarrollara la audiencia pública"
                                Width="150px" Font-Size="Larger" Font-Overline="False" Font-Underline="False"
                                Font-Strikeout="False" Font-Bold="True"></asp:Label></td>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center">
                            <asp:Label ID="lblComunidad" runat="server" Width="150px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center">
                            <asp:Label ID="lblFechaInfo" runat="server" SkinID="etiqueta_negra" Text="Fecha y hora de reunión informativa"
                                Width="150px" Font-Size="Larger" Font-Overline="False" Font-Underline="False"
                                Font-Strikeout="False" Font-Bold="True"></asp:Label></td>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center">
                            <asp:Label ID="lblFechaReunion" runat="server" Width="150px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center">
                            <asp:Label ID="lblLugarInfo" runat="server" SkinID="etiqueta_negra" Text="Lugar de reunión informativa"
                                Width="150px" Font-Size="Larger" Font-Overline="False" Font-Underline="False"
                                Font-Strikeout="False" Font-Bold="True"></asp:Label></td>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center">
                            <asp:Label ID="lblLugarReunion" runat="server" Width="150px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center">
                            <asp:Label ID="lblLugarE" runat="server" SkinID="etiqueta_negra" Text="Lugar de consulta de los estudios ambientales"
                                Width="150px" Font-Size="Larger" Font-Overline="False" Font-Underline="False"
                                Font-Strikeout="False" Font-Bold="True"></asp:Label></td>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center">
                            <asp:Label ID="lblLugarEstudios" runat="server" Width="150px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center">
                            <asp:Label ID="lblLugarIncripcion" runat="server" SkinID="etiqueta_negra" Text="Lugar de inscripción de los ponentes"
                                Width="150px" Font-Size="Larger" Font-Overline="False" Font-Underline="False"
                                Font-Strikeout="False" Font-Bold="True"></asp:Label></td>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center">
                            <asp:Label ID="lblLugarPonentes" runat="server" Width="150px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center">
                            <asp:Label ID="lblEdictoA" runat="server" SkinID="etiqueta_negra" Text="Edicto adjunto "
                                Width="150px" Font-Size="Larger" Font-Overline="False" Font-Underline="False"
                                Font-Strikeout="False" Font-Bold="True"></asp:Label></td>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center">
                            <asp:Label ID="lblEdicto" runat="server" Width="150px" Visible="False"></asp:Label>
                            <asp:LinkButton ID="lnkDescargar" runat="server" OnClick="lnkDescargar_Click">Ver Documentos</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center">
                            <asp:Label ID="lblConvocatoriaL" runat="server" SkinID="etiqueta_negra" Text="Convocatoria"
                                Width="150px" Font-Size="Larger" Font-Overline="False" Font-Underline="False"
                                Font-Strikeout="False" Font-Bold="True"></asp:Label></td>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center">
                            <asp:Label ID="lblconvocatoria" runat="server" Width="150px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                            text-align: center" colspan="4">
                            <asp:LinkButton ID="cmdInscripcion" OnClick="cmdInscripcion_Click" runat="server"
                                Font-Size="Large">Inscripción</asp:LinkButton>
                        </td>
                    </tr>
                <tr>
                    <td>                        
                        <asp:Button ID="btnRegresar1" runat="server" Text="Regresar" SkinID="boton" OnClick="btnRegresar1_Click" ValidationGroup="gruporegresar" /></td>
                </tr>
                </table>
                <%--                <table style="width: 700px">
                    <tbody>
                        <tr>
                            <td style="text-align: center" colspan="4">
                                <asp:Label ID="lblNAudiencia" runat="server" Text="Audiencia Pública No."></asp:Label>
                                <asp:Label ID="lblNoAUP" runat="server"></asp:Label>
                                <asp:Label ID="lblNSilpa" runat="server" Text="Número SILPA" Width="100px"></asp:Label><asp:Label
                                    ID="lblNumeroSILPA" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                border-bottom: silver thin solid; text-align: center">
                                <asp:Label ID="lblNombreP" runat="server" SkinID="etiqueta_negra" Text="Nombre del proyecto, Obra o Actividad"
                                    Width="150px" Font-Size="Larger" Font-Overline="False" Font-Underline="False"
                                    Font-Strikeout="False" Font-Bold="True"></asp:Label></td>
                            <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                border-bottom: silver thin solid; text-align: center">
                                <asp:Label ID="lblNumExp" runat="server" SkinID="etiqueta_negra" Text="Número de Expediente "
                                    Width="150px" Font-Size="Larger" Font-Overline="False" Font-Underline="False"
                                    Font-Strikeout="False" Font-Bold="True"></asp:Label></td>
                            <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                border-bottom: silver thin solid; text-align: center">
                                <asp:Label ID="lblFechaA" runat="server" SkinID="etiqueta_negra" Text="Fecha y hora de celebración de la Audiencia Pública"
                                    Width="150px" Font-Size="Larger" Font-Overline="False" Font-Underline="False"
                                    Font-Strikeout="False" Font-Bold="True"></asp:Label></td>
                            <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                border-bottom: silver thin solid; text-align: center">
                                <asp:Label ID="lblLugarA" runat="server" SkinID="etiqueta_negra" Text="Lugar de celebración de la audiencia pública"
                                    Width="150px" Font-Size="Larger" Font-Overline="False" Font-Underline="False"
                                    Font-Strikeout="False" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                border-bottom: silver thin solid">
                                <asp:Label ID="lblProyecto" runat="server" Width="150px"></asp:Label></td>
                            <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                border-bottom: silver thin solid">
                                <asp:Label ID="lblNumExpediente" runat="server" Width="150px"></asp:Label></td>
                            <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                border-bottom: silver thin solid">
                                <asp:Label ID="lblFechaAudiencia" runat="server" Width="150px"></asp:Label></td>
                            <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                border-bottom: silver thin solid">
                                <asp:Label ID="lblLugarAudiencia" runat="server" Width="150px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                border-bottom: silver thin solid; text-align: center">
                                <asp:Label ID="lblComunidadA" runat="server" SkinID="etiqueta_negra" Text="Comunidad donde se desarrollara la audiencia pública"
                                    Width="150px" Font-Size="Larger" Font-Overline="False" Font-Underline="False"
                                    Font-Strikeout="False" Font-Bold="True"></asp:Label></td>
                            <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                border-bottom: silver thin solid; text-align: center">
                                <asp:Label ID="lblFechaInfo" runat="server" SkinID="etiqueta_negra" Text="Fecha y hora de reunión informativa"
                                    Width="150px" Font-Size="Larger" Font-Overline="False" Font-Underline="False"
                                    Font-Strikeout="False" Font-Bold="True"></asp:Label></td>
                            <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                border-bottom: silver thin solid; text-align: center">
                                <asp:Label ID="lblLugarInfo" runat="server" SkinID="etiqueta_negra" Text="Lugar de reunión informativa"
                                    Width="150px" Font-Size="Larger" Font-Overline="False" Font-Underline="False"
                                    Font-Strikeout="False" Font-Bold="True"></asp:Label></td>
                            <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                border-bottom: silver thin solid; text-align: center">
                                <asp:Label ID="lblLugarE" runat="server" SkinID="etiqueta_negra" Text="Lugar de consulta de los estudios ambientales"
                                    Width="150px" Font-Size="Larger" Font-Overline="False" Font-Underline="False"
                                    Font-Strikeout="False" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                border-bottom: silver thin solid">
                                <asp:Label ID="lblComunidad" runat="server" Width="150px"></asp:Label></td>
                            <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                border-bottom: silver thin solid">
                                <asp:Label ID="lblFechaReunion" runat="server" Width="150px"></asp:Label></td>
                            <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                border-bottom: silver thin solid">
                                <asp:Label ID="lblLugarReunion" runat="server" Width="150px"></asp:Label></td>
                            <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                border-bottom: silver thin solid">
                                <asp:Label ID="lblLugarEstudios" runat="server" Width="150px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                border-bottom: silver thin solid; text-align: center">
                                <asp:Label ID="lblLugarIncripcion" runat="server" SkinID="etiqueta_negra" Text="Lugar de inscripción de los ponentes"
                                    Width="150px" Font-Size="Larger" Font-Overline="False" Font-Underline="False"
                                    Font-Strikeout="False" Font-Bold="True"></asp:Label></td>
                            <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                border-bottom: silver thin solid; text-align: center">
                                <asp:Label ID="lblEdictoA" runat="server" SkinID="etiqueta_negra" Text="Edicto adjunto "
                                    Width="150px" Font-Size="Larger" Font-Overline="False" Font-Underline="False"
                                    Font-Strikeout="False" Font-Bold="True"></asp:Label></td>
                            <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                border-bottom: silver thin solid; text-align: center" colspan="2">
                                <asp:Label ID="lblConvocatoriaL" runat="server" SkinID="etiqueta_negra" Text="Convocatoria"
                                    Width="150px" Font-Size="Larger" Font-Overline="False" Font-Underline="False"
                                    Font-Strikeout="False" Font-Bold="True" __designer:wfdid="w3"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                border-bottom: silver thin solid">
                                <asp:Label ID="lblLugarPonentes" runat="server" Width="150px"></asp:Label></td>
                            <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                border-bottom: silver thin solid">
                                <asp:Label ID="lblEdicto" runat="server" Width="150px"></asp:Label>
                                <asp:LinkButton ID="lnkDescargar" runat="server" __designer:wfdid="w26" OnClick="lnkDescargar_Click"
                                    Visible="False">Descargar</asp:LinkButton></td>
                            <td style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                border-bottom: silver thin solid" colspan="2">
                                <asp:Label ID="lblconvocatoria" runat="server" Width="150px" __designer:wfdid="w4"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-align: left" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center" colspan="4">
                                <asp:LinkButton ID="cmdInscripcion" OnClick="cmdInscripcion_Click" runat="server"
                                    Font-Size="Large">Inscripción</asp:LinkButton></td>
                        </tr>
                    </tbody>
                </table>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
</asp:Content>
