<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetalleSalvoconducto.aspx.cs" Inherits="Salvoconducto_DetalleSalvoconducto" %>

<%@ Register Src="~/controles/Cabezote.ascx" TagName="Cabezote" TagPrefix="uc1" %>
<%@ Register Src="~/controles/MenuPrincipal.ascx" TagName="MenuPrincipal" TagPrefix="uc2" %>
<%@ Register Src="~/controles/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc3" %>
<%@ Register Src="~/controles/User.ascx" TagName="User" TagPrefix="uc4" %>
<%@ Register Src="~/controles/FondoIzquierda.ascx" TagName="FondoIzq" TagPrefix="uc5" %>
<%@ Register Src="~/controles/FondoDerecha.ascx" TagName="FondoDer" TagPrefix="uc6" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../jquery/jquery.js"></script>
    <link href="../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../jquery/EstiloGris/jquery-ui.js"></script>
    <script src="../jquery/jquery.numeric.js"></script>
    <link href="../App_Themes/skin/DetalleSalvoconducto.css" rel="stylesheet" />
</head>
<body style="background-color: White;">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" align="center" style="background-color: White">
            <%--<tr>
                <td rowspan="3" valign="top">
                    <uc5:FondoIzq ID="FondoIzq1" runat="server" />
                </td>
                <td>
                    <uc1:Cabezote ID="Cabezote2" runat="server" />
                </td>
                <td rowspan="3" valign="top">
                    <uc6:FondoDer ID="FondoDer1" runat="server" />
                </td>
            </tr>--%>
            <tr>
                <td valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: white;" width="956px">
                        <tr>
                            <td class="contentTile" valign="top">
                                <div>
                                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: White">
                                        <tr>
                                            <td>
                                                <div class="div-titulo">
                                                    <asp:Label ID="lblTituloPrincipal" runat="server" Text="Salvoconducto" SkinID="titulo_principal_blanco"></asp:Label>
                                                    &nbsp;
                                                    <a href="#" id="hrCerrarVentana" visible="false" runat="server" onclick="window.close();return false;">Salir</a>
                                                </div>
                                                <div class="div-contenido">
                                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                                    </asp:ScriptManager>
                                                    <div class="contact1_form">
                                                        <table width="600px">
                                                            <tr>
                                                                <td>
                                                                    <label for="salvoconducto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Nro.:</label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblNumeroSalvoconducto" runat="server" SkinID="etiqueta_negra"></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <label for="numeroVital" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Nro VITAL.:</label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblNumeroVITAL" runat="server" SkinID="etiqueta_negra"></asp:Label>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label for="cboAutoridadAmbiental" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Autoridad Ambiental:</label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblAutoridadAmbiental" runat="server" SkinID="etiqueta_negra"></asp:Label>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label for="cboTipoSalvoconducto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Tipo Salvoconducto:</label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblTipoSalvoconducto" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label for="fechaSolicitud" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Vigencia:</label>
                                                                </td>
                                                                <td colspan="3">Desde:
                                                            <asp:Label ID="lblFechaInicioVigencia" runat="server" SkinID="etiqueta_negra"></asp:Label>&nbsp;&nbsp;
                                                            Hasta:
                                                            <asp:Label ID="lblFechaFinVigencia" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; font-weight: bold;">Titular:</label>
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:Label ID="lblSolicitante" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label for="cboEstado" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Estado:</label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblEstado" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="ClaseRecurso">Clase Recurso:</label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblClaseRecurso" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="ActoAdministrativo">Acto Administrativo:</label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblActoAdministrativo" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="FechaExpedicion">Fecha de Expedición:</label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblFechaActo" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="Establecimiento">Establecimiento:</label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblEstablecimiento" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="Clase Aprovechamiento">Clase Aprovechamiento:</label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblClaseAprovechamiento" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="FormaOrtorgamiento">Forma Otorgamiento:</label></td>
                                                                <td>
                                                                    <asp:Label ID="lblFormaOtorgamiento" runat="server" SkinID="etiqueta_negra"></asp:Label></td>
                                                                <td>
                                                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="Finalidad del Recurso">Finalidad del Recurso:</label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblFinalidadRecurso" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="Procedencia">Departamento Procedencia:</label></td>
                                                                <td>
                                                                    <asp:Label ID="lblDeptoProcedencia" runat="server" SkinID="etiqueta_negra"></asp:Label></td>
                                                                <td>
                                                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="Procedencia">Municipio Procedencia:</label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblMunpioProcedencia" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="Procedencia">Corregimiento Procedencia:</label></td>
                                                                <td>
                                                                    <asp:Label ID="lblCorregimientoProcedencia" runat="server" SkinID="etiqueta_negra"></asp:Label></td>
                                                                <td>
                                                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="Procedencia">Vereda Procedencia:</label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblVeredaProcedencia" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table width="600px">
                                                            <tr>
                                                                <td colspan="4">
                                                                    <h3>RUTA DESPLAZAMIENTO</h3>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <asp:GridView ID="grvRutaDesplazamiento" runat="server" AutoGenerateColumns="false" SkinID="GrillaCoordenadas">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="Departamento" HeaderText="Departamento" />
                                                                            <asp:BoundField DataField="Municipio" HeaderText="Municipio" />
                                                                            <asp:BoundField DataField="Corregimiento" HeaderText="Corregimiento" />
                                                                            <asp:BoundField DataField="Vereda" HeaderText="Vereda" />
                                                                            <asp:BoundField DataField="Orden" HeaderText="Orden" />

                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table width="600px">
                                                            <tr>
                                                                <td colspan="4">
                                                                    <h3>TRANSPORTE</h3>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="modoTransporte">Modo de Transporte:</label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblModoTransporte" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="tipoTransporte">Tipo de Transporte:</label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblTipoTransporte" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="Empresa">Empresa:</label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblEmpresa" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="IdentificacionMedio">Matricula:</label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblMatricula" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="Transportador">Transportador:</label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblNombreTransportador" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="TipoIdentificacionTransortador">Tipo Identificación:</label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblTipoIdentificacion" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="NumeroIdentificacion">Número Identificación:</label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblNumeroIdentificacionTransportador" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table width="600px">
                                                            <tr>
                                                                <td colspan="4">
                                                                    <h3>ESPECIEMENES</h3>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <asp:GridView ID="grvEspecimenes" runat="server" AutoGenerateColumns="false" SkinID="GrillaCoordenadas">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="NombreEspecie" HeaderText="Nombre Comun" />
                                                                            <asp:BoundField DataField="ClaseProducto" HeaderText="Clase Producto" />
                                                                            <asp:BoundField DataField="TipoProducto" HeaderText="Tipo Producto" />
                                                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                                                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                                                            <asp:BoundField DataField="UnidadMedida" HeaderText="Unidad Medida" />
                                                                            <asp:BoundField DataField="Volumen" HeaderText="Volumen" />
                                                                            <asp:BoundField DataField="VolumenBruto" HeaderText="Volumen Bruto" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr id="trAcciones" runat="server" visible="false">
                                                                <td>
                                                                    <asp:Button ID="btnEmitir" runat="server" Text="Emitir" OnClick="btnEmitir_Click" />
                                                                    <asp:Button ID="btnRechazar" runat="server" Text="Rechazar" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--<tr>
                <td style="background-color: White">
                    <uc3:PiePagina ID="PiePagina2" runat="server" />
                </td>
            </tr>--%>
        </table>
    </form>
</body>
</html>

