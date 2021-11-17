<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="SolicitudesAutoliquidacion.aspx.cs" Inherits="Liquidacion_SolicitudesAutoliquidacion" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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

        .divWaiting
        {
	        background-color:Gray;
            /*background-color: #FAFAFA;*/
	        filter:alpha(opacity=70);
	        /*opacity:0.7;*/
            position: absolute;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            text-align: center; top: 0; left: 0;
            height: 100%;
            width: 100%;
            padding-top:20%;
        } 
    </style>

    <script src="../jquery/jquery.js" type="text/javascript"></script>        
    <link href="../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../jquery/EstiloGris/jquery-ui.js"  type="text/javascript"></script>
    <script src="../js/Ayuda.js" type="text/javascript"></script>
    <link href="css/FormularioAutoliquidacion.css" rel="stylesheet" />

    <script type="text/javascript">

        function MostrarCobro(intCobroID)
        {
            window.open("../PermisosAmbientales/Liquidacion/FacturaPago.aspx?AL=" + intCobroID, "Factura", "fullscreen=false,menubar=false,toolbar=false,scrollbars=yes");
        }

        function PagoPSE(intCobroID) {
            window.open("../PermisosAmbientales/Liquidacion/FormularioPagoAutoliquidacion.aspx?AL=" + intCobroID, "Factura", "fullscreen=false,menubar=false,toolbar=false,scrollbars=yes");
        }

        $(function () {

            $("[id*=accordionSolicitudes]").accordion({
                collapsible: true,
                heightStyle: "content",
                active: false
            });
            
            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {

                $("[id*=accordionSolicitudes]").accordion({
                    collapsible: true,
                    heightStyle: "content",
                    active: false
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

    <asp:UpdatePanel runat="server" ID="upnlSolicitudeLiquidacion" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="TablaAutoliquidacion">
                <tr>
                    <td colspan="4" class="TituloSeccionAutoliquidacion">Filtro de Búsqueda</td>
                </tr>
                <tr>
                    <td class="LabelFormularioBusquedaAutoliquidacion">
                        Autoridad Ambiental:
                    </td>
                    <td class="CamposFormularioBusquedaAutoliquidacion">
                        <asp:DropDownList runat="server" ID="cboAutoridad"></asp:DropDownList>
                    </td>
                    <td class="LabelFormularioBusquedaAutoliquidacion">
                        Número VITAL:
                    </td>
                    <td class="CamposFormularioBusquedaAutoliquidacion">
                        <asp:TextBox runat="server" ID="txtNumeroVITAL"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="LabelFormularioBusquedaAutoliquidacion">
                        Liquidación:
                    </td>
                    <td class="CamposFormularioBusquedaAutoliquidacion">
                        <asp:DropDownList runat="server" ID="cboTipoSolicitud" OnSelectedIndexChanged="cboTipoSolicitud_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                    <td class="LabelFormularioBusquedaAutoliquidacion">
                        Solicitud de Liquidación:
                    </td>
                    <td class="CamposFormularioBusquedaAutoliquidacion">
                        <asp:DropDownList runat="server" ID="cboClaseSolicitud"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="LabelFormularioBusquedaAutoliquidacion">
                        Nombre del Proyecto:
                    </td>
                    <td class="CamposFormularioBusquedaAutoliquidacion">
                        <asp:TextBox runat="server" ID="txtNombreProyecto"></asp:TextBox>
                    </td>
                    <td class="LabelFormularioBusquedaAutoliquidacion">
                        Estado Solicitud:
                    </td>
                    <td class="CamposFormularioBusquedaAutoliquidacion">
                        <asp:DropDownList runat="server" ID="cboEstadoSolicitud"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="LabelFormularioBusquedaAutoliquidacion">
                        Fecha Solicitud Desde:
                    </td>
                    <td class="CamposFormularioBusquedaAutoliquidacion">
                        <asp:TextBox ID="txtFechaSoicitudDesde" runat="server" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFechaSoicitudDesde" runat="server" ControlToValidate="txtFechaSoicitudDesde" ErrorMessage="Ingrese la fecha inicial de solicitud." Text="*" ValidationGroup="BuscarSolicitudes" InitialValue=""></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rexFechaSoicitudDesde" runat="server" ControlToValidate="txtFechaSoicitudDesde" ErrorMessage="El formato de la fecha inicial de la solicitud no es valido." ValidationExpression="^\d{2}\/\d{2}\/\d{4}" Width="3px" Text="*" ValidationGroup="BuscarSolicitudes"></asp:RegularExpressionValidator>
                        <cc1:CalendarExtender ID="calFechaSoicitudDesde" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaSoicitudDesde"/>
                    </td>
                    <td class="LabelFormularioBusquedaAutoliquidacion">
                        Fecha Solicitud Hasta:
                    </td>
                    <td class="CamposFormularioBusquedaAutoliquidacion">
                        <asp:TextBox ID="txtFechaSoicitudHasta" runat="server" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFechaSoicitudHasta" runat="server" ControlToValidate="txtFechaSoicitudHasta" ErrorMessage="Ingrese la fecha final de solicitud." Text="*" ValidationGroup="BuscarSolicitudes" InitialValue=""></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rexFechaSoicitudHasta" runat="server" ControlToValidate="txtFechaSoicitudHasta" ErrorMessage="El formato de la fecha final de la solicitud no es valido." ValidationExpression="^\d{2}\/\d{2}\/\d{4}" Width="3px" Text="*" ValidationGroup="BuscarSolicitudes"></asp:RegularExpressionValidator>
                        <cc1:CalendarExtender ID="calFechaSoicitudHasta" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaSoicitudHasta"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table class="TablaBotonesFormularioAutoliquidacion">
                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="cmdBuscarSolicitud" ValidationGroup="BuscarSolicitudes" Text="Buscar" ClientIDMode="Static" OnClick="cmdBuscarSolicitud_Click"/>
                                    <asp:ValidationSummary ID="valBuscarSolicitudes" runat="server" ValidationGroup="BuscarSolicitudes" ShowMessageBox="true" ShowSummary="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table class="TablaAutoliquidacion">
                <tr>
                    <td>
                        <asp:HiddenField runat="server" ID="hdfAutoridadIDBuscar" />
                        <asp:HiddenField runat="server" ID="hdfNumeroVitalBuscar" />
                        <asp:HiddenField runat="server" ID="hdfTipoSolicitudIDBuscar" />
                        <asp:HiddenField runat="server" ID="hdfClaseSOlicitudIDBuscar" />
                        <asp:HiddenField runat="server" ID="hdfNombreProyectoBuscar" />
                        <asp:HiddenField runat="server" ID="hdfEstadoSolicitudIDBuscar" />
                        <asp:HiddenField runat="server" ID="hdfFechaDesdeBuscar" />
                        <asp:HiddenField runat="server" ID="hdfFechaHastaBuscar" />
                        <asp:Button runat="server" ID="cmdNuevaSolicitud" Text="Nueva Solicitud Liquidación" CausesValidation="false" OnClick="cmdNuevaSolicitud_Click" />
                    </td>
                </tr>
            </table>
            <div class="table-responsive DivTablaFormularioAutoliquidacion">
                <table>
                    <tr>
                        <td>
                        <asp:GridView ID="grdSolicitudes" runat="server" Width="100%" SkinID="GrillaListaSolicitudAutoliquidacion"                                   
                                    AutoGenerateColumns="False" AllowPaging="true" PageSize="10" 
                                    ShowHeaderWhenEmpty="true" CellPadding="4" CellSpacing="2"  
                                    OnPageIndexChanging="grdSolicitudes_PageIndexChanging" OnRowDataBound="grdSolicitudes_RowDataBound"                             
                                    EmptyDataText="No se encontro información de solicitudes de liquidaciones con los parámetros de búsqueda especificados.">
                            <Columns>

                                <asp:TemplateField HeaderText="Autoridad Ambiental">
                                    <ItemTemplate>
                                        <asp:literal ID="ltlAutoridad" runat="server" Text='<%# Eval("AutoridadAmbiental.Nombre")  %>'></asp:literal>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Número Vital">
                                    <ItemTemplate>
                                        <asp:literal ID="ltlNumeroVital" runat="server" Text='<%# (Eval("NumeroVITAL") != null && !string.IsNullOrWhiteSpace(Eval("NumeroVITAL").ToString()) ? Eval("NumeroVITAL").ToString() : "-")  %>'></asp:literal>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Liquidación">
                                    <ItemTemplate>
                                        <asp:literal ID="ltlTipoSolicitud" runat="server" Text='<%# Eval("TipoSolicitud.TipoSolicitud")  %>'></asp:literal>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Solicitud Liquidación">
                                    <ItemTemplate>
                                        <asp:literal ID="ltlClaseSolicitud" runat="server" Text='<%# Eval("ClaseSolicitud.ClaseSolicitud")  %>'></asp:literal>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Nombre Proyecto">
                                    <ItemTemplate>
                                        <asp:literal ID="ltlNombreProyecto" runat="server" Text='<%# (Eval("NombreProyecto").ToString().Length > 30 ? Eval("NombreProyecto").ToString().Substring(0,30) + "..." : Eval("NombreProyecto").ToString())  %>'></asp:literal>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Estado Solicitud">
                                    <ItemTemplate>
                                        <asp:literal ID="ltlEstado" runat="server" Text='<%# Eval("EstadoSolicitud.EstadoSolicitud")  %>'></asp:literal>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Fecha Solicitud">
                                    <ItemTemplate>
                                        <asp:literal ID="ltlFechaSolicitud" runat="server" Text='<%# (Convert.ToDateTime(Eval("FechaRadicacionVITAL")) != default(DateTime) ? Convert.ToDateTime(Eval("FechaRadicacionVITAL")).ToString("dd/MM/yyyy HH:mm") : "-" )  %>'></asp:literal>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cobros Relacionados">
                                    <ItemTemplate>
                                        <div id="accordionSolicitudes">
                                            <div class="HeaderAccordionLiquidacion">
                                            </div>
                                            <div class="bodyAccordeonAdmNotificacion">
                                                <asp:GridView ID="grdCobrosSolicitudLiquidacion" runat="server" Width="100%" SkinID="GrillaListaSolicitudAutoliquidacion" EmptyDataText="No existen cobros asociados a la solicitud de liquidación" >
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Autoridad Ambiental">
                                                            <ItemTemplate>
                                                                <asp:literal ID="ltlAutoridadCobro" runat="server" Text='<%# Eval("AutoridadAmbiental.Nombre")  %>'></asp:literal>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Concepto">
                                                            <ItemTemplate>
                                                                <asp:literal ID="ltlConceptoCobro" runat="server" Text='<%# Eval("Concepto")  %>'></asp:literal>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Valor" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:literal ID="ltlValorCobro" runat="server" Text='<%# string.Format("{0:C}", Eval("ValorCobro")).Replace(" ", "&nbsp;")  %>'></asp:literal>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Estado">
                                                            <ItemTemplate>
                                                                <asp:literal ID="ltlEstadoCobro" runat="server" Text='<%# (Eval("EstadoCobro") != null ?  Eval("EstadoCobro.EstadoCobro") : "N/A") %>'></asp:literal>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Pagar" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:ImageButton runat="server" ID="imgPagar" ImageUrl="~/images/pagar.png" BorderWidth="0" Visible='<%# Eval("EstadoCobro") != null && Convert.ToInt32(Eval("EstadoCobro.EstadoCobroID")) == 1 && Convert.ToDateTime(Eval("FechaVencimiento")).Date >= DateTime.Today %>' OnClientClick='<%# "PagoPSE(" + Eval("CobroSolicitudLiquidacionEntityID") + ")" %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Comprobante" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:ImageButton runat="server" ID="imgComprobantePago" ImageUrl="~/images/documento.png" BorderWidth="0" Visible='<%# Eval("EstadoCobro") != null %>' OnClientClick='<%# "MostrarCobro(" + Eval("CobroSolicitudLiquidacionEntityID") + ")" %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Ver Solicitud" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkVer" OnClick="lnkVer_Click" CommandArgument='<%# Eval("SolicitudLiquidacionID")%>'>Ver</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Reenviar Solicitud" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkReenviar" Visible='<%# Convert.ToInt32(Eval("EstadoSolicitud.EstadoSolicitudID")) == 1 || Convert.ToInt32(Eval("EstadoSolicitud.EstadoSolicitudID")) == 2 %>' CommandArgument='<%# Eval("SolicitudLiquidacionID")%>' OnClick="lnkReenviar_Click">Reenviar</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Ver Respuesta Solicitud" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkVerRespuesta"  Visible='<%# Convert.ToInt32(Eval("EstadoSolicitud.EstadoSolicitudID")) != 1 && Convert.ToInt32(Eval("EstadoSolicitud.EstadoSolicitudID")) != 2 %>' CommandArgument='<%# Eval("SolicitudLiquidacionID")%>' OnClick="lnkVerRespuesta_Click">Ver</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                      </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>

        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cmdNuevaSolicitud" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="cboTipoSolicitud" EventName="SelectedIndexChanged" />
        </Triggers>

    </asp:UpdatePanel>

    <asp:UpdateProgress ID="uppSolicitudeLiquidacion" runat="server" AssociatedUpdatePanelID="upnlSolicitudeLiquidacion">
        <ProgressTemplate>  
            <div id="ModalProgressContainer">
                <div>
                    <p>Procesando...</p>
                    <p><asp:Image ID="imgSolicitudeLiquidacion" runat="server" SkinId="procesando"/></p>
                </div>
            </div>                         
        </ProgressTemplate>
    </asp:UpdateProgress>


    <input type="button" runat="server" id="cmdConfirmarEnvioSolicitudHide" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeConfirmarEnvioSolicitud" runat="server" PopupControlID="dvConfirmarEnvioSolicitud" TargetControlID="cmdConfirmarEnvioSolicitudHide" BehaviorID="mpeConfirmarEnvioSolicitudes" BackgroundCssClass="ModalBackgroundAutoliquidacion">
    </cc1:ModalPopupExtender>
    <div id="dvConfirmarEnvioSolicitud" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalAutoliquidacion">
        <asp:UpdatePanel runat="server" ID="upnlConfirmarEnvioSolicitud" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="TablaFormularioAutoliquidacion">
                    <tr>
                        <td colspan="2" class="TituloSeccionAutoliquidacion">
                            Solicitud de Liquidación
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalImagenes" rowspan="2">
                            <asp:Image runat="server" ID="imgImportanteEnvioSolicitud" ImageUrl="~/images/advertencia.png" />
                        </td>
                        <td class="ModalTextoTerminos">
                            <asp:Literal runat="server" ID="ltlTerminosConfirmarEnvioSolicitud"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="ModalTablaAceptarTerminos">
                                <tr>
                                    <td>
                                        <asp:CheckBox runat="server" ID="chkAceptarTerminoCondiciones" ClientIDMode="Static" EnableTheming="false"/>                                        
                                        <asp:CustomValidator runat="server" ID="cvAceptarAdvertenciaConfirmarEnvioSolicitud" ValidationGroup="FormularioConfirmarEnvioSolicitud" ErrorMessage="Para continuar el proceso de envío de la solicitud debe confirmar que la información proporcionada es verídica y acepta las condiciones especificadas." ClientValidationFunction="VerificarTerminoCondiciones">&nbsp;</asp:CustomValidator>
                                    </td>
                                    <td class="ModalTextoAceptarTerminos">
                                        <b>Confirmo que la información del formulario es verídica y acepto las condiciones especificadas para el envío de la solicitud de liquidación.</b>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table class="TablaBotonesFormularioAutoliquidacion">
                    <tr>
                        <td>
                            <asp:HiddenField runat="server" ID="hdfSolicitudLiquidacionIDConfirmarEnvioSolicitud" />
                            <asp:Button runat="server" ID="cmdAceptarConfirmarEnvioSolicitud" ValidationGroup="FormularioConfirmarEnvioSolicitud" Text="Aceptar" ClientIDMode="Static" OnClick="cmdAceptarConfirmarEnvioSolicitud_Click"/>
                            <asp:Button runat="server" ID="cmdCancelarConfirmarEnvioSolicitud" CausesValidation="false" Text="Cancelar" ClientIDMode="Static" OnClick="cmdCancelarConfirmarEnvioSolicitud_Click"/>
                            <asp:ValidationSummary ID="valFormularioConfirmarEnvioSolicitud" runat="server" ValidationGroup="FormularioConfirmarEnvioSolicitud" ShowMessageBox="true" ShowSummary="false" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdAceptarConfirmarEnvioSolicitud" EventName="Click" />                                                        
                <asp:AsyncPostBackTrigger ControlID="cmdCancelarConfirmarEnvioSolicitud" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="uppConfirmarEnvioSolicitud" runat="server" AssociatedUpdatePanelID="upnlConfirmarEnvioSolicitud">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgConfirmarEnvioSolicitud" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>


    <input type="button" runat="server" id="cmdErrorProcesoHide" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeErrorProceso" runat="server" PopupControlID="dvErrorProceso" TargetControlID="cmdErrorProcesoHide" BehaviorID="mpeErrorProcesos" BackgroundCssClass="ModalBackgroundAutoliquidacion">
    </cc1:ModalPopupExtender>
    <div id="dvErrorProceso" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalAutoliquidacion">
        <asp:UpdatePanel runat="server" ID="upnlErrorProceso" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="TablaFormularioAutoliquidacion">
                    <tr>
                        <td colspan="2" class="TituloSeccionAutoliquidacion">
                            Solicitud de Liquidación
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalImagenes">
                            <asp:Image runat="server" ID="imgIconoErrorProceso" ImageUrl="~/images/error.png" />
                        </td>
                        <td class="ModalTextoTerminos">
                            <asp:Literal runat="server" ID="ltlErrorProceso"></asp:Literal>
                        </td>
                    </tr>                        
                </table>
                <table class="TablaBotonesFormularioAutoliquidacion">
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="cmdAceptarErrorProceso" CausesValidation="false" Text="Aceptar" ClientIDMode="Static" OnClick="cmdAceptarErrorProceso_Click"/>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdAceptarErrorProceso" EventName="Click" />                                                        
            </Triggers>
        </asp:UpdatePanel>

        <asp:UpdateProgress ID="uppErrorProceso" runat="server" AssociatedUpdatePanelID="upnlErrorProceso">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgErrorProceso" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

</asp:Content>