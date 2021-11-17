<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="RespuestaSolicitudLiquidacion.aspx.cs" Inherits="Liquidacion_RespuestaSolicitudLiquidacion" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>
    
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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

    <script src="../jquery/jquery.js" type="text/javascript"></script>        
    <link href="../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../jquery/EstiloGris/jquery-ui.js"  type="text/javascript"></script>
    <script src="../js/Ayuda.js" type="text/javascript"></script>
    <link href="css/FormularioAutoliquidacion.css" rel="stylesheet" />

    <script language="javascript" type="text/javascript">
        window.history.forward();
    </script>

    <script language="javascript" type="text/javascript">        
        $(function () {
            $('#cmdImprimirFactura').click(function (e) {
                window.open("FacturaPago.aspx?IDProcessInstance=" + $("#hdfInstancia").val(), "Comprobante", "fullscreen=false,menubar=false,toolbar=false,scrollbars=yes");
            });

            $('#cmdImprimir').click(function (e) {
                $("#tblBotones").hide();
                window.print();
                $("#tblBotones").show();
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('#cmdImprimirFactura').click(function (e) {
                    window.open("FacturaPago.aspx?IDProcessInstance=" + $("#hdfInstancia").val(), "Comprobante", "fullscreen=false,menubar=false,toolbar=false,scrollbars=yes");
                });

                $('#cmdImprimir').click(function (e) {
                    $("#tblBotones").hide();
                    window.print();
                    $("#tblBotones").show();
                });
            });
        });
    </script>

    <asp:ScriptManager ID="scmManager" runat="server"></asp:ScriptManager>

    <table class="TablaTituloSeccionAutoliquidacion">
        <tr>
            <td class="div-titulo">
                <asp:Label ID="lblTitulo" runat="server" Text="SOLICITUD DE LIQUIDACIÓN" SkinID="titulo_principal_blanco"></asp:Label>
            </td>
        </tr>
    </table>

    <asp:UpdatePanel runat="server" ID="upnlMensaje" UpdateMode="Conditional">
        <ContentTemplate>
            <table runat="server" visible="false" id="tblMensaje" class="TablaMensajeErrorAutoliquidacion">
                <tr>
                    <td class="MensajeErrorAutoliquidacion">
                        <asp:Literal runat="server" ID="lblMensaje"></asp:Literal>
                        <br />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="upnlRespuestaRadicacion" UpdateMode="Conditional">
        <ContentTemplate>
            <table runat="server" id="tblRespuestaRadicacion" class="TablaRespuestaAutoliquidacion">
                <tr>
                    <td class="TituloSeccionAutoliquidacion">
                        Radicación de Solicitud de Liquidación
                    </td>
                </tr>
                <tr>
                    <td class="TextoRespuestaRadicacionLiquidacion">
                        <asp:Literal runat="server" ID="ltlRespuestaRadicacion"></asp:Literal>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="upnlRespuestaAutoliquidacion" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="TablaRespuestaAutoliquidacion">
                <tr>
                    <td class="TituloSeccionAutoliquidacion">
                        Respuesta  Solicitud de Liquidación
                    </td>
                </tr>
                <tr>
                    <td class="TextoRespuestaAutoLiquidacion">                        
                        En atención a la solicitud realizada el día <asp:Literal runat="server" ID="ltlFechaSolicitud"></asp:Literal> a la <asp:Literal runat="server" ID="ltlAutoridad"></asp:Literal> mediante la cual solicitó liquidación <asp:Literal runat="server" ID="ltlDescripcionDatosBasicosSolicitud"></asp:Literal> que se describe a continuación:
                        <br /><br />
                        <asp:Literal runat="server" ID="ltlDescripcionProyecto"></asp:Literal>
                        <br /><br />
                        Le informamos que deberá realizar el pago a través del botón <b>PSE</b> o imprimiendo el comprobante de pago con código de barras los cuales se encuentran ubicados en la columna de <b>Cobros</b> del listado de solicitudes.
                    </td>
                </tr>
                <tr>
                    <td class="TextoRespuestaAutoLiquidacion">                        
                        <table class="SubTablaRespuestaAutoliquidacion">
                            <tr>
                                <td class="LabelSubTablaRespuestaAutoliquidacion">Referencia de Pago:</td>
                                <td class="TextoSubTablaRespuestaAutoliquidacion"><asp:Literal runat="server" ID="ltlReferenciaPago"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td class="LabelSubTablaRespuestaAutoliquidacion">Valor en Números:</td>
                                <td class="TextoSubTablaRespuestaAutoliquidacion"><asp:Literal runat="server" ID="ltlValorNumeros"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td class="LabelSubTablaRespuestaAutoliquidacion">Valor en Letras:</td>
                                <td class="TextoSubTablaRespuestaAutoliquidacion"><asp:Literal runat="server" ID="ltlValorLetras"></asp:Literal></td>
                            </tr>                            
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="TextoRespuestaAutoLiquidacion">                        
                        Para acreditar el pago realizado a través del comprobante con código de barras, deberá registrarlo o anexarlo al momento de la radicación de la solicitud del trámite correspondiente.
                    </td>
                </tr>
                <tr runat="server" id="trDatosResolucion">
                    <td class="TextoRespuestaAutoLiquidacion">
                        El valor mencionado se obtiene de aplicar la <asp:Literal runat="server" ID="ltlResolucion"></asp:Literal> tabla <b>No. <asp:Literal runat="server" ID="ltlNumeroTabla"></asp:Literal> - <asp:Literal runat="server" ID="ltlNombreMicroTabla"></asp:Literal></b>.
                        <br /><br />
                        La tabla aplicada refleja la dedicación mensual y la cantidad de profesionales necesarios para evaluar la información presentada por la empresa aplicando el sistema y método previsto en la ley 633 de 2000, cuyo espíritu es sufragar los costos en que  incurre la Autoridad para la prestación del servicio, según liquidación así:
                    </td>
                </tr>
                <tr runat="server" id="trDatosLey633">
                    <td class="TextoRespuestaAutoLiquidacion">
                        El valor mencionado se obtiene de aplicar el sistema y método previsto en la ley 633 de 2000, cuyo espíritu es sufragar los costos en que incurre la Autoridad para la prestación del servicio, según liquidación así:
                    </td>
                </tr>
                <tr runat="server" id="trTablaLey633">
                    <td>
                        <table class="SubTablaRespuestaAutoliquidacion">
                            <tr>
                                <td colspan="2" class="TituloSubTablaRespuestaAutoliquidacion">Cálculo del Tope Máximo Ley 633 de 2000</td>
                            </tr>
                            <tr>
                                <td class="LabelSubTablaRespuestaAutoliquidacion">Valor del Proyecto en Pesos Colombianos $:</td>
                                <td class="TextoSubTablaRespuestaAutoliquidacion"><asp:Literal runat="server" ID="ltlValorProyecto"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td class="LabelSubTablaRespuestaAutoliquidacion">Valor Salario Mínimo Mensual Legal Vigente:</td>
                                <td class="TextoSubTablaRespuestaAutoliquidacion"><asp:Literal runat="server" ID="ltlSalarioMinimo"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td class="LabelSubTablaRespuestaAutoliquidacion">Relación (Valor del Proyecto Pesos / SMMLV):</td>
                                <td class="TextoSubTablaRespuestaAutoliquidacion"><asp:Literal runat="server" ID="ltlRelacionSalario"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td class="LabelSubTablaRespuestaAutoliquidacion">Tarifa Máxima a Aplicar:</td>
                                <td class="TextoSubTablaRespuestaAutoliquidacion"><asp:Literal runat="server" ID="ltlTarifaMaxima"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td class="LabelSubTablaRespuestaAutoliquidacion">Valor Máximo a Cobrar:</td>
                                <td class="TextoSubTablaRespuestaAutoliquidacion"><asp:Literal runat="server" ID="ltlValorMaximoCobrar"></asp:Literal></td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td class="TextoAnotacionRespuestaAutoLiquidacion">
                                    * De conformidad con el artículo 96 de la Ley 633/2.000:
                                    <br />
                                   &nbsp;&nbsp;&nbsp;&nbsp;- La relación es menor a 2.115, la tarifa máxima es del 0,6 % = 0,006
                                    <br />
                                   &nbsp;&nbsp;&nbsp;&nbsp;- La relación está entre 2.115 y 8.458, la tarifa máxima es del 0,5 % = 0,005
                                    <br />
                                   &nbsp;&nbsp;&nbsp;&nbsp;- La relación es mayor a 8.458, la tarifa máxima es del 0,4 % = 0,004
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server" id="trTablaResolucion">
                    <td>
                        <table class="SubTablaRespuestaAutoliquidacion">
                            <tr>
                                <td colspan="9" class="TituloSubTablaRespuestaAutoliquidacion"><asp:Literal runat="server" ID="ltlTituloTablaResolucion"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td class="TituloSubTablaRespuestaAutoliquidacion" style="width: 11%;">Categoría Profesionales</td>
                                <td class="TituloSubTablaRespuestaAutoliquidacion" style="width: 11%;">Honorario Mensual $</td>
                                <td class="TituloSubTablaRespuestaAutoliquidacion" style="width: 11%;">Dedicación Mensual (Hombre/Mes)</td>
                                <td class="TituloSubTablaRespuestaAutoliquidacion" style="width: 11%;">No. de Visitas</td>
                                <td class="TituloSubTablaRespuestaAutoliquidacion" style="width: 11%;">Duración (días)</td>
                                <td class="TituloSubTablaRespuestaAutoliquidacion" style="width: 11%;">Total No. días</td>
                                <td class="TituloSubTablaRespuestaAutoliquidacion" style="width: 11%;">Viáticos diarios $</td>
                                <td class="TituloSubTablaRespuestaAutoliquidacion" style="width: 11%;">Total Viáticos $</td>
                                <td class="TituloSubTablaRespuestaAutoliquidacion" style="width: 12%;">Costo Total $</td>
                            </tr>
                            <asp:Repeater runat="server" ID="rptValoresMicrotabla">
                                <ItemTemplate>
                                    <tr>
                                        <td class="TextoListadoSubTablaRespuestaAutoliquidacion">
                                            <asp:Literal runat="server" ID="ltlCategoriaProfesional" Text='<%# Eval("Categoria")  %>'></asp:Literal>
                                        </td>
                                        <td class="TextoListadoDerechaSubTablaRespuestaAutoliquidacion">
                                            <asp:Literal runat="server" ID="ltlHonorarioMensual" Text='<%# Eval("Honorario")  %>'></asp:Literal>
                                        </td>
                                        <td class="TextoListadoCentradoSubTablaRespuestaAutoliquidacion">
                                            <asp:Literal runat="server" ID="ltlDedicacionMensual" Text='<%# Eval("Dedicacion")  %>'></asp:Literal>
                                        </td>
                                        <td class="TextoListadoCentradoSubTablaRespuestaAutoliquidacion">
                                            <asp:Literal runat="server" ID="ltlNumeroVisitas" Text='<%# Eval("NumeroVisitas")  %>'></asp:Literal>
                                        </td>
                                        <td class="TextoListadoCentradoSubTablaRespuestaAutoliquidacion">
                                            <asp:Literal runat="server" ID="ltlDuracionDias" Text='<%# Eval("Duracion")  %>'></asp:Literal>
                                        </td>
                                        <td class="TextoListadoCentradoSubTablaRespuestaAutoliquidacion">
                                            <asp:Literal runat="server" ID="ltlTotalDias" Text='<%# string.Format("{0:#0.##}", Convert.ToDecimal(Eval("NumeroVisitas")) * Convert.ToDecimal(Eval("Duracion")))  %>'></asp:Literal>
                                        </td>
                                        <td class="TextoListadoDerechaSubTablaRespuestaAutoliquidacion">
                                            <asp:Literal runat="server" ID="ltlViaticosDiarios" Text='<%# Eval("ViaticosDiarios")  %>'></asp:Literal>
                                        </td>
                                        <td class="TextoListadoDerechaSubTablaRespuestaAutoliquidacion">
                                            <asp:Literal runat="server" ID="ltlTotalViaticos" Text='<%# Eval("TotalViaticos")  %>'></asp:Literal>
                                        </td>
                                        <td class="TextoListadoDerechaSubTablaRespuestaAutoliquidacion">
                                            <asp:Literal runat="server" ID="ltlCostoTotal" Text='<%# Eval("CostoTotal")  %>'></asp:Literal>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>                            
                            <tr runat="server" id="trPermisosANLASolicitados">
                                <td colspan="9" class="TituloSubTablaRespuestaAutoliquidacion">Permisos Solicitados</td>
                            </tr>
                            
                            <tr runat="server" id="trListaPermisos">
                                <td colspan="9">                                            
                                    <table class="SubTablaRespuestaAutoliquidacion">
                                        <tr>
                                            <td class="TituloSubTablaRespuestaAutoliquidacion">Permiso</td>
                                            <td class="TituloSubTablaRespuestaAutoliquidacion">Número de Permisos</td>
                                        </tr>
                                        <asp:Repeater runat="server" ID="rptPermisosANLA">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="TextoListadoSubTablaRespuestaAutoliquidacion">
                                                        <asp:Literal runat="server" ID="ltlPermisoANLA" Text='<%# Eval("Permiso")  %>'></asp:Literal>
                                                    </td>
                                                    <td class="TextoListadoCentradoSubTablaRespuestaAutoliquidacion">
                                                        <asp:Literal runat="server" ID="ltlNumeroPermisos" Text='<%# Eval("NumeroPermisos")  %>'></asp:Literal>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>

                            <tr runat="server" id="trTiquetes">
                                <td colspan="8" class="TituloSubTablaRespuestaAutoliquidacion">Tiquetes</td>
                                <td class="TituloSubTablaRespuestaAutoliquidacion">Valor Total Tiquetes</td>
                            </tr>
                            <tr runat="server" id="trListadoTiquetes">
                                <td colspan="8">
                                    <table class="SubTablaRespuestaAutoliquidacion">
                                        <tr>
                                            <td class="TituloSubTablaRespuestaAutoliquidacion">Origen</td>
                                            <td class="TituloSubTablaRespuestaAutoliquidacion">Destino</td>
                                            <td class="TituloSubTablaRespuestaAutoliquidacion">Valor Tiquete</td>
                                            <td class="TituloSubTablaRespuestaAutoliquidacion">No. Tiquetes</td>
                                            <td class="TituloSubTablaRespuestaAutoliquidacion">Valor Tiquetes</td>
                                        </tr>
                                        <asp:Repeater runat="server" ID="rptTiquetes">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="TextoListadoSubTablaRespuestaAutoliquidacion">
                                                        <asp:Literal runat="server" ID="ltlOrigenTiquete" Text='<%# Eval("MunicipioOrigen").ToString() + "("  + Eval("DepartamentoOrigen").ToString() +  ")"  %>'></asp:Literal>
                                                    </td>
                                                    <td class="TextoListadoSubTablaRespuestaAutoliquidacion">
                                                        <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("MunicipioDestino").ToString() + "("  + Eval("DepartamentoDestino").ToString() +  ")"  %>'></asp:Literal>
                                                    </td>
                                                    <td class="TextoListadoDerechaSubTablaRespuestaAutoliquidacion">
                                                        <asp:Literal runat="server" ID="ltlValorTiquete" Text='<%# Eval("ValorTiquete")  %>'></asp:Literal>
                                                    </td>
                                                    <td class="TextoListadoCentradoSubTablaRespuestaAutoliquidacion">
                                                        <asp:Literal runat="server" ID="ltlNumeroTiquetes" Text='<%# Eval("NumeroTiquetes")  %>'></asp:Literal>
                                                    </td>
                                                    <td class="TextoListadoDerechaSubTablaRespuestaAutoliquidacion">
                                                        <asp:Literal runat="server" ID="ltlValorTotalTiquete" Text='<%# Eval("ValorTotalTiquetes")  %>'></asp:Literal>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>                                        
                                    </table>
                                </td>
                                <td class="TextoListadoDerechaSubTablaRespuestaAutoliquidacion">
                                    <asp:Literal runat="server" ID="ltlValorTotalTiquetes"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8" class="LabelSubTablaRespuestaAutoliquidacion">Valor del Servicio:</td>
                                <td class="TextoListadoDerechaSubTablaRespuestaAutoliquidacion">
                                    <asp:Literal runat="server" ID="ltlValorServicio"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8" class="LabelSubTablaRespuestaAutoliquidacion">Valor de Administración:</td>
                                <td class="TextoListadoDerechaSubTablaRespuestaAutoliquidacion">
                                    <asp:Literal runat="server" ID="ltlValorAdministracion"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8" class="LabelSubTablaRespuestaAutoliquidacion">Valor Total:</td>
                                <td class="TextoListadoDerechaSubTablaRespuestaAutoliquidacion">
                                    <b><asp:Literal runat="server" ID="ltlValorTotal"></asp:Literal></b>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr runat="server" id="trDatosPermisos">
                    <td class="TextoRespuestaAutoLiquidacion">
                        Se deberá cancelar a la(s) Corporación(es) Ambiental(es), por servicios de apoyo a la evaluación, el/los monto(s) descrito(s) a continuación:
                    </td>
                </tr>   
                
                <asp:Repeater runat="server" ID="rptPermisos">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <table class="SubTablaRespuestaAutoliquidacion">
                                    <tr>
                                        <td class="LabelSubTablaRespuestaAutoliquidacion">Autoridad Ambiental:</td>
                                        <td class="TextoSubTablaRespuestaAutoliquidacion"><asp:Literal runat="server" ID="ltlAutoridadTablaPermiso" Text='<%# Eval("EntidadPermiso")  %>'></asp:Literal></td>
                                    </tr>
                                    <tr>
                                        <td class="LabelSubTablaRespuestaAutoliquidacion">Permiso:</td>
                                        <td class="TextoSubTablaRespuestaAutoliquidacion"><asp:Literal runat="server" ID="ltlNombrePermiso" Text='<%# Eval("Permiso")  %>'></asp:Literal></td>
                                    </tr>
                                    <tr>
                                        <td class="LabelSubTablaRespuestaAutoliquidacion">Número de Permisos:</td>
                                        <td class="TextoSubTablaRespuestaAutoliquidacion"><asp:Literal runat="server" ID="ltlNumeroPermisos" Text='<%# Eval("NumeroPermisos")  %>'></asp:Literal></td>
                                    </tr>
                                    <tr>
                                        <td class="LabelSubTablaRespuestaAutoliquidacion">Valor en Números:</td>
                                        <td class="TextoSubTablaRespuestaAutoliquidacion"><asp:Literal runat="server" ID="ltlValorPermisos" Text='<%# Eval("ValorTotal")  %>'></asp:Literal></td>
                                    </tr>
                                    <tr>
                                        <td class="LabelSubTablaRespuestaAutoliquidacion">Valor en Letras:</td>
                                        <td class="TextoSubTablaRespuestaAutoliquidacion"><asp:Literal runat="server" ID="ltlValorLetras" Text='<%# Eval("ValorTotalLetras")  %>'></asp:Literal></td>
                                    </tr>
                                </table> 
                            </td>
                        </tr>                
                        <tr>
                            <td class="TextoRespuestaAutoLiquidacion">
                                El pago correspondiente deberá ser realizado en la cuenta que la autoridad <b><asp:Literal runat="server" ID="ltlAutoridadPermiso" Text='<%# Eval("EntidadPermiso")  %>'></asp:Literal></b> disponga para tal fin. 	
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                
                <tr runat="server" id="trTextoCondiciones">
                    <td class="TextoRespuestaAutoLiquidacion">
                        Si transcurridos cuatro (4) meses de haberse sufragado los costos para prestar el servicio de evaluación ambiental, el interesado no radicara la solicitud para dar inicio al trámite correspondiente se dará aplicación a lo previsto en el artículo 9 de la Resolución 0324 del 17 de marzo de 2015.
                    </td>
                </tr>
                          
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    
                    

    <asp:UpdatePanel runat="server" ID="upnlBotonesAcciones" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="TablaBotonesFormularioAutoliquidacion" id="tblBotones">
                <tr>
                    <td>
                        <asp:Button runat="server" ID="cmdVolver" CausesValidation="false" Text="Volver al Listado de Solicitudes" ClientIDMode="Static" OnClick="cmdVolver_Click"/>
                        <%--<asp:Button runat="server" ID="cmdImprimir" CausesValidation="false" Text="Imprimir" ClientIDMode="Static"/>--%>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
