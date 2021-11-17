<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ReporteNotificaciones.aspx.cs" Inherits="NotificacionElectronica_ReporteNotificaciones" Title="Proceso de Notificación" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <%--<link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>--%>
    
    <div class='burbujaAyuda'></div>
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

    <script type="text/javascript">
        var hrefPag = "";

        function VerNotificacion(p_identificadorAct, p_identificadorPer) {
            window.open("ConsultaNotificaciones.aspx?Act=" + p_identificadorAct + "&Per=" + p_identificadorPer, "Notificacion", "resizable=yes,width=1200,height=600");
        }

        function RecargarAcordion(hrefPagina) {

            var blnRecargar = false;

            blnRecargar = hrefPag.indexOf("grdExpedientesDetalles") == -1 && hrefPag.indexOf("Cerrar") == -1;            
            blnRecargar = blnRecargar && (hrefPag.indexOf("Page$") == -1 || (hrefPag.indexOf("Page$") != -1 && hrefPag.indexOf("grdExpedientes") != -1));

            return blnRecargar;
        }


        $(function () {
            $("[id*=accordionExpediente]").accordion({
                collapsible: true,
                heightStyle: "content",
                active: false
            });

            $("[id*=accordionNotificacion]").accordion({
                collapsible: true,
                heightStyle: "content",
                active: false
            });

            $('a').click(function (e) {
                hrefPag = $(this).attr("href");
                if (hrefPag == null || hrefPag == 'undefined')
                    hrefPag = "";
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('a').click(function (e) {
                    hrefPag = $(this).attr("href");
                    if (hrefPag == null || hrefPag == 'undefined')
                        hrefPag = "";
                });                

                if (RecargarAcordion(hrefPag)) {
                    $("[id*=accordionExpediente]").accordion({
                        collapsible: true,
                        heightStyle: "content",
                        active: false
                    });
                }

                $("[id*=accordionNotificacion]").accordion({
                    collapsible: true,
                    heightStyle: "content",
                    active: false
                });

            });
        });
    </script>

    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="PUBLICIDAD ACTOS ADMINISTRATIVOS" SkinID="titulo_principal_blanco"></asp:Label>
        &nbsp;
        <a href="#" id="hrfCerrarVentana" runat="server" onclick="window.close();return false;" class="Salir">Salir [X]</a>
    </div>

    <asp:ScriptManager ID="scmManejador" runat="server" EnableScriptGlobalization="True"></asp:ScriptManager>

    <div class="contact_form" id="divConsultaCertificado" runat="server">
        <div class="TableBuscarNot">
            <div class="RowBuscarTitulo">
                <div class="CellBuscarTitulo">
                    <asp:Literal ID="ltlTituloBuscador" runat="server" Text="FILTRO DE BÚSQUEDA"></asp:Literal>                    
                </div>
            </div>
            <div class="RowBuscarNot">
                <div class="CellBuscarNot">
                    <div class="TableBuscarInternoNot">
                        <div class="RowBuscarNot">
                            <div class="CellBuscarNot">
                                <label for="cboMarcaVehiculo">Número VITAL:</label>
                                <asp:TextBox runat="server" ID="txtNumeroVital" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="CellBuscarNot">
                                <label for="cboMarcaVehiculo">Número de Expediente:</label>
                                <asp:TextBox runat="server" ID="txtExpediente" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="RowBuscarNot">
                            <div class="CellBuscarNot">
                                <label for="txtIdentificacionUsuario">Identificación del Usuario:</label>
                                <asp:TextBox runat="server" ID="txtIdentificacionUsuario" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="CellBuscarNot">
                                <label for="txtUsuarioNotificar">Usuario a Notificar:</label>
                                <asp:TextBox runat="server" ID="txtUsuarioNotificar" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="RowBuscarNot">
                            <div class="CellBuscarNot">
                                <label for="txtNumeroActo">Número Acto Administrativo:</label>
                                <asp:TextBox runat="server" ID="txtNumeroActo" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="CellBuscarNot">
                                <label for="cboMarcaVehiculo">Tipo de Acto Administrativo:</label>
                                <asp:DropDownList ID="cboTipoActo" runat="server" SkinID="lista_desplegable2" />
                            </div>
                        </div>
                        <div class="RowBuscarNot">
                            <div class="CellBuscarNot">
                                <label for="txtFechaDesde">Fecha Acto Administrativo Desde:</label>
                                <asp:TextBox ID="txtFechaDesde" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFechaDesde" runat="server" ControlToValidate="txtFechaDesde" ErrorMessage="Ingrese la Fecha Desde la cual desea buscar." Text="*" ValidationGroup="NotBuscar" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rexFechaDesde" runat="server" ControlToValidate="txtFechaDesde" ErrorMessage="Formato de fecha no valido para la Fecha Desde." ValidationExpression="^\d{2}\/\d{2}\/\d{4}" Width="3px" Text="*" ValidationGroup="NotBuscar"></asp:RegularExpressionValidator>
                                <cc1:CalendarExtender ID="calFechaDesde" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaDesde"/>
                            </div>
                            <div class="CellBuscarNot">
                                <label for="txtFechaHasta">Fecha Acto Administrativo Hasta:</label>
                                <asp:TextBox ID="txtFechaHasta" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFechaHasta" runat="server" ControlToValidate="txtFechaHasta" ErrorMessage="Ingrese la Fecha Hasta la cual desea buscar." Text="*" ValidationGroup="NotBuscar" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rexFechaHasta" runat="server" ControlToValidate="txtFechaHasta" ErrorMessage="Formato de fecha no valido para la Fecha Hasta." ValidationExpression="^\d{2}\/\d{2}\/\d{4}" Width="3px" Text="*" ValidationGroup="NotBuscar"></asp:RegularExpressionValidator>
                                <cc1:CalendarExtender ID="calFechaHasta" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaHasta" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="RowButton">
                <div class="CellButton">
                    <asp:Button runat="server" ID="cmdBuscar" ValidationGroup="NotBuscar" Text="Buscar" ClientIDMode="Static" OnClick="cmdBuscar_Click"/>
                </div>
            </div>
            <div class="Row">
                <div class="Cell">
                    <asp:ValidationSummary ID="valNotBuscar" runat="server" ValidationGroup="NotBuscar" />
                </div>
            </div>
        </div>
    </div>
    <div class="contact_form" id="divMensaje" runat="server" visible="false">  
        <div class="Table">
            <div class="Row">
                <div class="CellMensaje">
                    <asp:Label runat="server" ID="lblMensaje" SkinID="etiqueta_roja_error"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <br />
    <asp:UpdatePanel runat="server" ID="upnlReporte"  UpdateMode="Conditional">
        <ContentTemplate>
            <div class="contact_form" id="divUsuario" runat="server">        
                <div class="TableReporteNot">
                    <div class="RowReporteNot">
                        <div class="CellReporteNot">
                            <asp:GridView ID="grdExpedientes" runat="server" 
                                SkinID="ReporteNotificacion" 
                                AutoGenerateColumns="False" AllowPaging="True" ShowHeader="false" 
                                EmptyDataText="No se encontro información de cobros para los criterios de búsqueda especificados" 
                                ShowHeaderWhenEmpty="False" 
                                PageSize="10" Width="100%" 
                                OnRowDataBound="grdExpedientes_RowDataBound"
                                OnPageIndexChanging="grdExpedientes_PageIndexChanging">
                                <HeaderStyle Font-Size="9pt" />
                                <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div id="accordionExpediente" style="color: #000000 !important; width: 100% !important;">
                                                <div class="headerAccordionAdmNotificacion">
                                                    <div class="TableReporteNotInt">
                                                        <div class="RowReporteNotInt">
                                                            <div class="CellReporteNotIntTitulo">
                                                                Expediente:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">
                                                                <asp:literal ID="lblExpediente" runat="server" Text='<%# Eval("EXPEDIENTE") %>'></asp:literal>
                                                            </div>
                                                            <div class="CellReporteNotIntTitulo">
                                                                Número VITAL:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">
                                                                <asp:literal ID="lblNumeroVital" runat="server" Text='<%# Eval("NUM_VITAL") %>'></asp:literal>
                                                            </div>
                                                            <div class="CellReporteNotIntTitulo">
                                                                Autoridad Ambiental:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">
                                                                <asp:literal ID="lblAutoridad" runat="server" Text='<%# Eval("NOMBRE_AUTORIDAD") %>'></asp:literal>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div>
                                                    <asp:UpdatePanel runat="server" ID="upnlReporteDetalle" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:GridView runat="server" AutoGenerateColumns="False" ID="grdExpedientesDetalles" AllowPaging="True" Width="100%" 
                                                                EmptyDataText="No se encontro información" ShowHeaderWhenEmpty="False" PageSize="10" SkinID="ReporteNotificacionDesplegable"
                                                                OnRowDataBound="grdExpedientesDetalles_RowDataBound" OnPageIndexChanging="grdExpedientesDetalles_PageIndexChanging" DataKeyNames="EXPEDIENTE, NUM_VITAL">
                                                                <HeaderStyle Font-Size="9pt" />
                                                                <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                                                <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                                                <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                                <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                                <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText = "TIPO ACTO ADMINISTRATIVO">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTipoActo" runat="server" Text='<%# Eval("TIPO_ACTO_ADMINISTRATIVO")%>' SkinID="etiqueta_negra9"></asp:Label>
                                                                            
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText = "NÚMERO ACTO ADMINISTRATIVO" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblActo" runat="server" Text='<%# Eval("NUMERO_ACTO_ADMINISTRATIVO")%>' SkinID="etiqueta_negra9" ></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText = "FECHA ACTO ADMINISTRATIVO"  ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFechaActo" runat="server" Text='<%# Eval("FECHA")%>' SkinID="etiqueta_negra9"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText = "ESTADO">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEstadoActoAdministrativo" runat="server" Text='<%# Eval("ESTADO_ACTO")%>' SkinID="etiqueta_negra9"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText = "DOCUMENTO ACTO ADMINISTRATIVO"  ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton runat="server" ID="imgDescargarDocumento" ImageUrl="~/images/documento.png" BorderWidth="0" Visible='<%# ( !Eval("RUTA_DOCUMENTO_ACTO").ToString().EndsWith("/")  && !Eval("RUTA_DOCUMENTO_ACTO").ToString().EndsWith("\\") && !string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO_ACTO").ToString() ) ? true : false ) %>'  CommandArgument='<%#Eval("RUTA_DOCUMENTO_ACTO") %>' OnClick="imgDescargarDocumento_Click" style="cursor: pointer;" />
                                                                            <asp:literal ID="ltlDescargarDocumento" runat="server" Text='-' Visible='<%# ( Eval("RUTA_DOCUMENTO_ACTO").ToString().EndsWith("/")  || Eval("RUTA_DOCUMENTO_ACTO").ToString().EndsWith("\\") || string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO_ACTO").ToString() ) ? true : false ) %>'></asp:literal>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText = "INFORMACIÓN ACTO ADMINISTRATIVO"  ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton runat="server" ID="lnkInformacionActo" OnClick="lnkInformacionActo_Click" CssClass="LnkGrillaReporteNot" CommandArgument='<%#Eval("ID_NOTIFICACION")%>'>Ver</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText = "NOTIFICAR" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>                                                                            
                                                                            <asp:LinkButton ID="lnkNotificaciones" runat="server" CommandArgument='<%#Eval("ID_NOTIFICACION")%>' OnClick="lnkNotificaciones_Click"> 
                                                                                <asp:Image ImageAlign="Middle" ID="imgNotificar" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoVerde.png" style="cursor: pointer;" />
                                                                            </asp:LinkButton>
                                                                            <asp:Image ImageAlign="Middle" ID="imgNotificarGris" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoGris.png" style="cursor: pointer;" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText = "COMUNICAR" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkComunicar" runat="server" CommandArgument='<%#Eval("ID_NOTIFICACION")%>' OnClick="lnkComunicar_Click"> 
                                                                                <asp:Image ImageAlign="Middle" ID="imgComunicar" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoVerde.png" style="cursor: pointer;" />
                                                                            </asp:LinkButton>
                                                                            <asp:Image ImageAlign="Middle" ID="imgComunicarGris" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoGris.png" style="cursor: pointer;" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText = "CUMPLIR" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkCumplir" runat="server" CommandArgument='<%#Eval("ID_NOTIFICACION")%>' OnClick="lnkCumplir_Click"> 
                                                                                <asp:Image ImageAlign="Middle" ID="imgCumplir" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoVerde.png" style="cursor: pointer;" />
                                                                            </asp:LinkButton>
                                                                            <asp:Image ImageAlign="Middle" ID="imgCumplirGris" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoGris.png" style="cursor: pointer;" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText = "PUBLICAR" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkPublicar" runat="server" CommandArgument='<%#Eval("ID_PUBLICACION")%>' OnClick="lnkPublicar_Click"> 
                                                                                <asp:Image ImageAlign="Middle" ID="imgPublicar" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoVerde.png" style="cursor: pointer;" />
                                                                            </asp:LinkButton>
                                                                            <asp:Image ImageAlign="Middle" ID="imgPublicarGris" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoGris.png" style="cursor: pointer;" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="grdExpedientesDetalles" EventName="PageIndexChanging" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </ItemTemplate>                               
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>                                                                                      
                        </div>
                    </div>
                </div>
            </div> 
            
        </ContentTemplate>
    </asp:UpdatePanel>

    
    <input type="button" runat="server" id="btnOcultoInformacionBitacora" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeInformacionBitacora" runat="server" TargetControlID="btnOcultoInformacionBitacora" PopupControlID="dvInformacionBitacora" BehaviorID="mpeInformacionBitacora"  BackgroundCssClass="modalBackground"/> 
    <div id="dvInformacionBitacora" class="ContenedorModalNot" style="display:none;" runat="server" clientidmode="Static">        
        <asp:UpdatePanel runat="server" ID="upnlInformacionBitacora" UpdateMode="Conditional" >
            <ContentTemplate>
                <div class="TableResultadoModalNot">
                    <div class="RowResultadoModalNot">
                        <div class="CellLnkButtonExitModalNot">
                            <asp:LinkButton runat="server" ID="lnkCerrarInformacionBitacora" OnClick="lnkCerrarInformacionBitacora_Click">Cerrar [X]</asp:LinkButton>
                        </div>
                    </div>
                    <div class="RowResultadoTituloModalNot">
                        <div class="CellResultadoTituloModalNot">
                            INFORMACIÓN ACTO ADMINISTRATIVO
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoModalNot">
                            <asp:GridView HorizontalAlign="Center" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="10" ID="grdBitacora" SkinID="ReporteNotificacionDesplegable" OnPageIndexChanging="grdBitacora_PageIndexChanging" Width="98%"
                                          EmptyDataText="No se tiene registro de actividad sobre el acto administrativo" ShowHeaderWhenEmpty="true">
                                <HeaderStyle Font-Size="9pt" />
                                <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                <Columns>
                                    <asp:TemplateField HeaderText = "ESTADO ACTO ADMINISTRATIVO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("ESTADO")%>' SkinID="etiqueta_negra9"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "USUARIO MODIFICO" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUsuario" runat="server" Text='<%# Eval("USUARIO")%>' SkinID="etiqueta_negra9"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                                            
                                    <asp:TemplateField HeaderText = "FECHA MODIFICACIÓN ESTADO" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFecha" runat="server" Text='<%# Convert.ToDateTime(Eval("FECHA")).ToString("dd/MM/yyyy HH:mm:ss") %>' SkinID="etiqueta_negra9"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grdBitacora" EventName="PageIndexChanging" />
                <asp:AsyncPostBackTrigger ControlID="lnkCerrarInformacionBitacora" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>



    <input type="button" runat="server" id="btnOcultoNotificar" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeInformacionNotificaciones" runat="server" TargetControlID="btnOcultoNotificar" PopupControlID="dvInformacionNotificacion" BehaviorID="mpeInformacionNotificaciones"  BackgroundCssClass="modalBackground" CancelControlID="lnkCerrarNotificacion" /> 
    <div id="dvInformacionNotificacion" class="ContenedorModalNot" style="display:none;" runat="server" clientidmode="Static">        
        <asp:UpdatePanel runat="server" ID="upnlDetallesNotificacion" UpdateMode="Conditional" >
            <ContentTemplate>
                <div class="TableResultadoModalNot">
                    <div class="RowResultadoModalNot">
                        <div class="CellLnkButtonExitModalNot">
                            <asp:LinkButton runat="server" ID="lnkCerrarNotificacion" OnClick="lnkCerrarNotificacion_Click">Cerrar [X]</asp:LinkButton>
                        </div>
                    </div>
                    <div class="RowResultadoTituloModalNot">
                        <div class="CellResultadoTituloModalNot">
                            DETALLE NOTIFICACIÓN EXPEDIENTE <asp:literal ID="lblExpedienteDetalle" runat="server"></asp:literal>
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoModalNot">
                            <asp:GridView runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="10" ID="grdNotificacion" SkinID="ReporteNotificacion" OnRowDataBound="grdNotificacion_RowDataBound" OnPageIndexChanging="grdNotificacion_PageIndexChanging" Width="100%">
                                <HeaderStyle Font-Size="9pt" />
                                <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div id="accordionNotificacion">
                                                <div runat="server" id="dvHeaderAccordion">
                                                    <div class="TableReporteNotInt">
                                                        <div class="RowReporteNotInt">
                                                            <div class="CellReporteNotIntTitulo">
                                                                Identificación del Usuario:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">
                                                                <asp:literal ID="lblIdUsuario" runat="server" Text='<%# Eval("IDENTIFICACION_USUARIO_NOTIFICAR") %>'></asp:literal>
                                                            </div>                                                            
                                                            <div class="CellReporteNotIntTitulo">
                                                                Flujo de Notificación:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">
                                                                <asp:literal ID="lblEsElectronica" runat="server" Text='<%# Eval("FLUJO") %>'></asp:literal>
                                                            </div>
                                                            <div class="CellReporteNotIntTitulo">
                                                                Estado Actual:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">                                                                
                                                                <asp:LinkButton runat="server" ID="lnkVerNotificacion" CssClass="LnkTextoModalNot" Text='<%# Eval("ESTADO_ACTUAL") %>'></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="RowReporteNotInt">
                                                            <div class="CellReporteNotIntTitulo">
                                                                Usuario a Notificar:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">
                                                                <asp:literal ID="lblUsuario" runat="server" Text='<%# Eval("USUARIO_NOTIFICAR") %>'></asp:literal>
                                                            </div>                                                            
                                                            <div class="CellReporteNotIntTitulo">
                                                                Días para Vencimiento:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">
                                                                <asp:literal ID="ltlDiasVencimiento" runat="server" Text='<%# Eval("DIAS_PARA_VENCIMIENTO")  %>'></asp:literal>
                                                            </div>
                                                            <div class="CellReporteNotIntTitulo">
                                                                Fecha Estado Actual:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">
                                                                <asp:literal ID="ltlFechaEstadoActual" runat="server" Text='<%# Eval("FECHA_ESTADO_ACTUAL")  %>'></asp:literal>
                                                            </div> 
                                                        </div>
                                                        <div class="RowReporteNotInt">
                                                            <div class="CellReporteNotIntTitulo">
                                                                Estado Usuario:
                                                            </div>
                                                            <div class="CellReporteNotIntTitulo">
                                                                <asp:literal ID="ltlEstadoUsuario" runat="server" Text='<%# Eval("ESTADO_PERSONA_NOTIFICAR").ToString().ToUpper() %>'></asp:literal>
                                                            </div>                                                            
                                                            <div class="CellReporteNotIntTitulo"></div>
                                                            <div class="CellReporteNotIntTexto"></div>
                                                            <div class="CellReporteNotIntTitulo"></div>
                                                            <div class="CellReporteNotIntTexto"></div> 
                                                        </div>
                                                    </div>                                                   
                                                </div>
                                                <div>
                                                    <asp:GridView runat="server" AutoGenerateColumns="False" ID="grdNotificacionDetalles" AllowPaging="False" Width="100%" 
                                                        EmptyDataText="No se encontro información de la notificación" ShowHeaderWhenEmpty="False" SkinID="ReporteNotificacionDesplegable"
                                                        OnRowDataBound="grdNotificacionDetalles_RowDataBound">
                                                        <HeaderStyle Font-Size="9pt" />
                                                        <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText = "ESTADO">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("ESTADO")%>' SkinID="etiqueta_negra9"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "FECHA ESTADO" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFechaEstado" runat="server" Text='<%# Eval("FECHA_ESTADO")%>' SkinID="etiqueta_negra9"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>                                                            
                                                            <asp:TemplateField HeaderText = "USUARIO AVANZO" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUsuarioCreo" runat="server" Text='<%# Eval("NOMBRE_USUARIO_AVANZO")%>' SkinID="etiqueta_negra9"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "FECHA AVANCE ESTADO" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFechaAvanceEstado" runat="server" Text='<%# Eval("FECHA_AVANCE_ESTADO")%>' SkinID="etiqueta_negra9"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "DÍAS PARA VENCIMIENTO" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>                                                                            
                                                                    <asp:Label ID="lblDiasVencimiento" runat="server" Text='<%# Eval("DIAS_PARA_VENCIMIENTO")%>' SkinID="etiqueta_negra9"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField> 
                                                            <asp:TemplateField HeaderText="DOCUMENTO" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="imgDocumentoPlantilla" ImageUrl="~/images/documento.png" BorderWidth="0" CommandArgument='<%#Eval("RUTA_DOCUMENTO_PLANTILLA") %>' Visible='<%# ( string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO_PLANTILLA").ToString() ) || Eval("RUTA_DOCUMENTO_PLANTILLA").ToString().EndsWith("/")  || Eval("RUTA_DOCUMENTO_PLANTILLA").ToString().EndsWith("\\") ? false : true ) %>' OnClick="imgDescargarDocumento_Click" style="cursor: pointer;" />
                                                                    <asp:literal ID="ltlDocumentoPlantilla" runat="server" Text='-' Visible='<%# ( !Eval("RUTA_DOCUMENTO_PLANTILLA").ToString().EndsWith("/")  && !Eval("RUTA_DOCUMENTO_PLANTILLA").ToString().EndsWith("\\") && !string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO_PLANTILLA").ToString() ) ? false : true ) %>'></asp:literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DOCUMENTO ADICIONAL" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="imgDocumentoAdicional" ImageUrl="~/images/documento.png"  BorderWidth="0" CommandArgument='<%#Eval("RUTA_DOCUMENTO_ADICIONAL") %>' Visible='<%# ( string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO_ADICIONAL").ToString() ) || Eval("RUTA_DOCUMENTO_ADICIONAL").ToString().EndsWith("/")  || Eval("RUTA_DOCUMENTO_ADICIONAL").ToString().EndsWith("\\") ? false : true ) %>' OnClick="imgDescargarDocumento_Click" style="cursor: pointer;" />
                                                                    <asp:literal ID="ltlDocumentoAdicional" runat="server" Text='-' Visible='<%# ( !Eval("RUTA_DOCUMENTO_ADICIONAL").ToString().EndsWith("/")  && !Eval("RUTA_DOCUMENTO_ADICIONAL").ToString().EndsWith("\\") && !string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO_ADICIONAL").ToString() ) ? false : true ) %>'></asp:literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ADJUNTOS" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="imgAdjuntos" ImageUrl="~/images/adjunto.png" BorderWidth="0" CommandArgument='<%#Eval("ID_ESTADO_PERSONA") %>' Visible='<%# ( string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO").ToString() ) || Eval("RUTA_DOCUMENTO").ToString().EndsWith("/")  || Eval("RUTA_DOCUMENTO").ToString().EndsWith("\\") ? false : true ) || (Eval("ADJUNTO_INCLUYE_ACTO") != System.DBNull.Value && Convert.ToBoolean(Eval("ADJUNTO_INCLUYE_ACTO"))) %>' OnClick="imgAdjuntos_Click" style="cursor: pointer;" />
                                                                    <asp:literal ID="ltlAdjuntos" runat="server" Text='-' Visible='<%# ( !Eval("RUTA_DOCUMENTO").ToString().EndsWith("/")  && !Eval("RUTA_DOCUMENTO").ToString().EndsWith("\\") && !string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO").ToString() ) ? false : true ) && (Eval("ADJUNTO_INCLUYE_ACTO") == System.DBNull.Value || !Convert.ToBoolean(Eval("ADJUNTO_INCLUYE_ACTO"))) %>'></asp:literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="NÚMERO RADICADO" ItemStyle-CssClass="ItemNotificacion">
                                                                <ItemTemplate>
                                                                    <asp:literal ID="ltlRadicado" runat="server" Text='<%# (!string.IsNullOrWhiteSpace(Eval("NUMERO_RADICADO").ToString()) ? Eval("NUMERO_RADICADO") : "-")  %>'></asp:literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "ENVÍO CORREO"  ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Image runat="server" ID="imgEnvioCorreo" Width="20px" BorderWidth="0" ImageAlign="Middle" ImageUrl="~/images/CirculoVerde.png" Visible='<%# Convert.ToBoolean(Eval("ENVIO_CORREO")) %>' CssClass="botonAyudaUP" divModal="dvInformacionNotificacion" style="cursor: pointer;" />
                                                                    <asp:Image ImageAlign="Middle" ID="imgEnvioCorreoGris" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoGris.png" Visible='<%# !Convert.ToBoolean(Eval("ENVIO_CORREO")) %>' style="cursor: pointer;" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>                                                                                                                       
                                                            <asp:TemplateField HeaderText = "ENVÍO DIRECCIÓN FÍSICA"  ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Image runat="server" ID="imgEnvioDireccion" Width="20px" BorderWidth="0" ImageAlign="Middle" ImageUrl="~/images/CirculoVerde.png" Visible='<%# Convert.ToBoolean(Eval("ENVIO_DIRECCION")) %>' CssClass="botonAyudaUP" divModal="dvInformacionNotificacion" style="cursor: pointer;" />
                                                                    <asp:Image ImageAlign="Middle" ID="imgEnvioDireccionGris" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoGris.png" Visible='<%# !Convert.ToBoolean(Eval("ENVIO_DIRECCION")) %>' style="cursor: pointer;" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "ESTADO PUBLICADO" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="imngEstadoPublicado" Width="20px" BorderWidth="0" ImageAlign="Middle" ImageUrl="~/images/CirculoVerde.png" CommandArgument='<%#Eval("ID_PUBLICACION") %>' Visible='<%# Convert.ToInt64(Eval("ID_PUBLICACION")) > 0 %>' OnClick="imngEstadoPublicado_Click" style="cursor: pointer;" />
                                                                    <asp:Image ImageAlign="Middle" ID="imgPublicarGris" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoGris.png" Visible='<%# Convert.ToInt64(Eval("ID_PUBLICACION")) <= 0 %>' style="cursor: pointer;" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "VER DETALLE" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="imngVerEstado" BorderWidth="0" ImageAlign="Middle" ImageUrl="~/images/ver.png" CommandArgument='<%#Eval("ID_ESTADO_PERSONA") %>' OnClick="imngVerEstado_Click" style="cursor: pointer;" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grdNotificacion" EventName="PageIndexChanging" />
                <asp:AsyncPostBackTrigger ControlID="lnkCerrarNotificacion" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>


    <input type="button" runat="server" id="btnOcultoComunicar" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeInformacionComunicar" runat="server" TargetControlID="btnOcultoComunicar" PopupControlID="dvInformacionComunicar" BehaviorID="mpeInformacionComunicar" BackgroundCssClass="modalBackground" CancelControlID="lnkCerrarComunicacion" /> 
    <div id="dvInformacionComunicar" class="ContenedorModalNot" style="display:none;" runat="server" clientidmode="Static">
        <asp:UpdatePanel runat="server" ID="upnlDetalleComunicacion" UpdateMode="Conditional" >
            <ContentTemplate>
                <div class="TableResultadoModalNot">
                    <div class="RowResultadoModalNot">
                        <div class="CellLnkButtonExitModalNot">
                            <asp:LinkButton runat="server" ID="lnkCerrarComunicacion" OnClick="lnkCerrarComunicacion_Click">Cerrar [X]</asp:LinkButton>
                        </div>
                    </div>
                    <div class="RowResultadoTituloModalNot">
                        <div class="CellResultadoTituloModalNot">
                            DETALLE COMUNICACIÓN EXPEDIENTE <asp:literal ID="lblExpedienteDetalleComunicacion" runat="server"></asp:literal>
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoModalNot">
                            <asp:GridView runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="10" ID="grdComunicacion" SkinID="ReporteNotificacion" OnRowDataBound="grdComunicacion_RowDataBound" OnPageIndexChanging="grdComunicacion_PageIndexChanging" Width="100%">
                                <HeaderStyle Font-Size="9pt" />
                                <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div id="accordionNotificacion">
                                                <div runat="server" id="dvHeaderAccordion">
                                                    <div class="TableReporteNotInt">
                                                        <div class="RowReporteNotInt">
                                                            <div class="CellReporteNotIntTitulo">
                                                                Identificación del Usuario:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">
                                                                <asp:literal ID="lblIdUsuario" runat="server" Text='<%# Eval("IDENTIFICACION_USUARIO_NOTIFICAR") %>'></asp:literal>
                                                            </div>                                                            
                                                            <div class="CellReporteNotIntTitulo">
                                                                Días para Vencimiento:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">
                                                                <asp:literal ID="ltlDiasVencimiento" runat="server" Text='<%# Eval("DIAS_PARA_VENCIMIENTO")  %>'></asp:literal>
                                                            </div>
                                                        </div>
                                                        <div class="RowReporteNotInt">
                                                            <div class="CellReporteNotIntTitulo">
                                                                Usuario a Comunicar:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">
                                                                <asp:literal ID="lblUsuario" runat="server" Text='<%# Eval("USUARIO_NOTIFICAR") %>'></asp:literal>
                                                            </div>                                                                                                                        
                                                            <div class="CellReporteNotIntTitulo">
                                                                Fecha Estado Actual:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">
                                                                <asp:literal ID="ltlFechaEstadoActual" runat="server" Text='<%# Eval("FECHA_ESTADO_ACTUAL")  %>'></asp:literal>
                                                            </div> 
                                                            <div class="CellReporteNotIntTitulo">
                                                                Estado Actual:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">                                                                
                                                                <asp:LinkButton runat="server" ID="lnkVerNotificacion" CssClass="LnkTextoModalNot" Text='<%# Eval("ESTADO_ACTUAL") %>'></asp:LinkButton>
                                                            </div>															
                                                        </div>
                                                    </div>                                                   
                                                </div>
                                                <div>
                                                    <asp:GridView runat="server" AutoGenerateColumns="False" ID="grdComunicacionDetalles" AllowPaging="False" Width="100%" 
                                                        EmptyDataText="No se encontro información de la notificación" ShowHeaderWhenEmpty="False" SkinID="ReporteNotificacionDesplegable"
                                                        OnRowDataBound="grdComunicacionDetalles_RowDataBound">
                                                        <HeaderStyle Font-Size="9pt" />
                                                        <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText = "ESTADO">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("ESTADO")%>' SkinID="etiqueta_negra9"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "FECHA ESTADO" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFechaEstado" runat="server" Text='<%# Eval("FECHA_ESTADO")%>' SkinID="etiqueta_negra9"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "USUARIO AVANZO" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUsuarioCreo" runat="server" Text='<%# Eval("NOMBRE_USUARIO_AVANZO")%>' SkinID="etiqueta_negra9"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "FECHA AVANCE ESTADO" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFechaAvanceEstado" runat="server" Text='<%# Eval("FECHA_AVANCE_ESTADO")%>' SkinID="etiqueta_negra9"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "DÍAS PARA VENCIMIENTO" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>                                                                            
                                                                    <asp:Label ID="lblDiasVencimiento" runat="server" Text='<%# Eval("DIAS_PARA_VENCIMIENTO")%>' SkinID="etiqueta_negra9"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField> 
                                                            <asp:TemplateField HeaderText="DOCUMENTO" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="imgDocumentoPlantilla" ImageUrl="~/images/documento.png" BorderWidth="0" CommandArgument='<%#Eval("RUTA_DOCUMENTO_PLANTILLA") %>' Visible='<%# ( string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO_PLANTILLA").ToString() ) || Eval("RUTA_DOCUMENTO_PLANTILLA").ToString().EndsWith("/")  || Eval("RUTA_DOCUMENTO_PLANTILLA").ToString().EndsWith("\\") ? false : true ) %>' OnClick="imgDescargarDocumento_Click" style="cursor: pointer;" />
                                                                    <asp:literal ID="ltlDocumentoPlantilla" runat="server" Text='-' Visible='<%# ( !Eval("RUTA_DOCUMENTO_PLANTILLA").ToString().EndsWith("/")  && !Eval("RUTA_DOCUMENTO_PLANTILLA").ToString().EndsWith("\\") && !string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO_PLANTILLA").ToString() ) ? false : true ) %>'></asp:literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DOCUMENTO ADICIONAL" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="imgDocumentoAdicional" ImageUrl="~/images/documento.png" BorderWidth="0" CommandArgument='<%#Eval("RUTA_DOCUMENTO_ADICIONAL") %>' Visible='<%# ( string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO_ADICIONAL").ToString() ) || Eval("RUTA_DOCUMENTO_ADICIONAL").ToString().EndsWith("/")  || Eval("RUTA_DOCUMENTO_ADICIONAL").ToString().EndsWith("\\") ? false : true ) %>' OnClick="imgDescargarDocumento_Click" style="cursor: pointer;" />
                                                                    <asp:literal ID="ltlDocumentoAdicional" runat="server" Text='-' Visible='<%# ( !Eval("RUTA_DOCUMENTO_ADICIONAL").ToString().EndsWith("/")  && !Eval("RUTA_DOCUMENTO_ADICIONAL").ToString().EndsWith("\\") && !string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO_ADICIONAL").ToString() ) ? false : true ) %>'></asp:literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ADJUNTOS" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="imgAdjuntos" ImageUrl="~/images/adjunto.png" BorderWidth="0" CommandArgument='<%#Eval("RUTA_DOCUMENTO") %>' Visible='<%# ( string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO").ToString() ) || Eval("RUTA_DOCUMENTO").ToString().EndsWith("/")  || Eval("RUTA_DOCUMENTO").ToString().EndsWith("\\") ? false : true ) %>' OnClick="imgDescargarDocumento_Click" style="cursor: pointer;" />
                                                                    <asp:literal ID="ltlAdjuntos" runat="server" Text='-' Visible='<%# ( !Eval("RUTA_DOCUMENTO").ToString().EndsWith("/")  && !Eval("RUTA_DOCUMENTO").ToString().EndsWith("\\") && !string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO").ToString() ) ? false : true ) %>'></asp:literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="NÚMERO RADICADO" ItemStyle-CssClass="ItemNotificacion">
                                                                <ItemTemplate>
                                                                    <asp:literal ID="ltlRadicado" runat="server" Text='<%# (!string.IsNullOrWhiteSpace(Eval("NUMERO_RADICADO").ToString()) ? Eval("NUMERO_RADICADO") : "-")  %>'></asp:literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "ENVÍO CORREO"  ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Image runat="server" ID="imgEnvioCorreo" Width="20px" BorderWidth="0" ImageAlign="Middle" ImageUrl="~/images/CirculoVerde.png" Visible='<%# Convert.ToBoolean(Eval("ENVIO_CORREO")) %>' CssClass="botonAyudaUP" divModal="dvInformacionComunicar" style="cursor: pointer;" />
                                                                    <asp:Image ImageAlign="Middle" ID="imgEnvioCorreoGris" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoGris.png" Visible='<%# !Convert.ToBoolean(Eval("ENVIO_CORREO")) %>' style="cursor: pointer;" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>                                                                                                                       
                                                            <asp:TemplateField HeaderText = "ENVÍO DIRECCIÓN FÍSICA"  ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Image runat="server" ID="imgEnvioDireccion" Width="20px" BorderWidth="0" ImageAlign="Middle" ImageUrl="~/images/CirculoVerde.png" Visible='<%# Convert.ToBoolean(Eval("ENVIO_DIRECCION")) %>' CssClass="botonAyudaUP" divModal="dvInformacionComunicar" style="cursor: pointer;" />
                                                                    <asp:Image ImageAlign="Middle" ID="imgEnvioDireccionGris" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoGris.png" Visible='<%# !Convert.ToBoolean(Eval("ENVIO_DIRECCION")) %>' style="cursor: pointer;" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "ESTADO PUBLICADO" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="imngEstadoPublicadoComunicacion" Width="20px" BorderWidth="0" ImageAlign="Middle" ImageUrl="~/images/CirculoVerde.png" CommandArgument='<%#Eval("ID_PUBLICACION") %>' Visible='<%# Convert.ToInt64(Eval("ID_PUBLICACION")) > 0 %>' OnClick="imngEstadoPublicado_Click" style="cursor: pointer;" />
                                                                    <asp:Image ImageAlign="Middle" ID="imgPublicarGris" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoGris.png" Visible='<%# Convert.ToInt64(Eval("ID_PUBLICACION")) <= 0 %>' style="cursor: pointer;" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "VER DETALLE" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="imngVerEstadoComunicacion" BorderWidth="0" ImageAlign="Middle" ImageUrl="~/images/ver.png" CommandArgument='<%#Eval("ID_ESTADO_PERSONA") %>' OnClick="imngVerEstado_Click" style="cursor: pointer;" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grdComunicacion" EventName="PageIndexChanging" />
                <asp:AsyncPostBackTrigger ControlID="lnkCerrarComunicacion" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <input type="button" runat="server" id="btnOcultoCumplir" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeInformacionCumplir" runat="server" TargetControlID="btnOcultoCumplir" PopupControlID="dvInformacionCumplir" BehaviorID="mpeInformacionCumplir" BackgroundCssClass="modalBackground" CancelControlID="lnkCerrarCumplir" /> 
    <div id="dvInformacionCumplir" class="ContenedorModalNot" style="display:none;" runat="server" clientidmode="Static">
        <asp:UpdatePanel runat="server" ID="upnlCumplimiento" UpdateMode="Conditional" >
            <ContentTemplate>
                <div class="TableResultadoModalNot">
                    <div class="RowResultadoModalNot">
                        <div class="CellLnkButtonExitModalNot">
                            <asp:LinkButton runat="server" ID="lnkCerrarCumplir" OnClick="lnkCerrarCumplimiento_Click">Cerrar [X]</asp:LinkButton>
                        </div>
                    </div>
                    <div class="RowResultadoTituloModalNot">
                        <div class="CellResultadoTituloModalNot">
                            DETALLE CUMPLIMIENTO EXPEDIENTE <asp:literal ID="lblExpedienteDetalleCumplir" runat="server"></asp:literal>
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoModalNot">
                            <asp:GridView runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="10" ID="grdCumplimiento" SkinID="ReporteNotificacion" OnRowDataBound="grdCumplimiento_RowDataBound" OnPageIndexChanging="grdCumplimiento_PageIndexChanging" Width="100%">
                                <HeaderStyle Font-Size="9pt" />
                                <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div id="accordionNotificacion">
                                                <div runat="server" id="dvHeaderAccordion">
                                                    <div class="TableReporteNotInt">
                                                        <div class="RowReporteNotInt">
                                                            <div class="CellReporteNotIntTitulo">
                                                                Identificación del Usuario:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">
                                                                <asp:literal ID="lblIdUsuario" runat="server" Text='<%# Eval("IDENTIFICACION_USUARIO_NOTIFICAR") %>'></asp:literal>
                                                            </div>                                                            
                                                            <div class="CellReporteNotIntTitulo">
                                                                Días para Vencimiento:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">
                                                                <asp:literal ID="ltlDiasVencimiento" runat="server" Text='<%# Eval("DIAS_PARA_VENCIMIENTO")  %>'></asp:literal>
                                                            </div>
                                                        </div>
                                                        <div class="RowReporteNotInt">
                                                            <div class="CellReporteNotIntTitulo">
                                                                Usuario a Comunicar:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">
                                                                <asp:literal ID="lblUsuario" runat="server" Text='<%# Eval("USUARIO_NOTIFICAR") %>'></asp:literal>
                                                            </div>                                                                                                                        
                                                            <div class="CellReporteNotIntTitulo">
                                                                Fecha Estado Actual:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">
                                                                <asp:literal ID="ltlFechaEstadoActual" runat="server" Text='<%# Eval("FECHA_ESTADO_ACTUAL")  %>'></asp:literal>
                                                            </div> 
                                                            <div class="CellReporteNotIntTitulo">
                                                                Estado Actual:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">                                                                
                                                                <asp:LinkButton runat="server" ID="lnkVerNotificacion" CssClass="LnkTextoModalNot" Text='<%# Eval("ESTADO_ACTUAL") %>'></asp:LinkButton>
                                                            </div>															
                                                        </div>
                                                    </div>                                                   
                                                </div>
                                                <div>
                                                    <asp:GridView runat="server" AutoGenerateColumns="False" ID="grdCumplimientoDetalles" AllowPaging="False" Width="100%" 
                                                        EmptyDataText="No se encontro información del cumplimiento" ShowHeaderWhenEmpty="False" SkinID="ReporteNotificacionDesplegable" OnRowDataBound="grdCumplimientoDetalles_RowDataBound">
                                                        <HeaderStyle Font-Size="9pt" />
                                                        <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText = "ESTADO">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("ESTADO")%>' SkinID="etiqueta_negra9"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "FECHA ESTADO" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFechaEstado" runat="server" Text='<%# Eval("FECHA_ESTADO")%>' SkinID="etiqueta_negra9"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "USUARIO AVANZO" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUsuarioCreo" runat="server" Text='<%# Eval("NOMBRE_USUARIO_AVANZO")%>' SkinID="etiqueta_negra9"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "FECHA AVANCE ESTADO" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFechaAvanceEstado" runat="server" Text='<%# Eval("FECHA_AVANCE_ESTADO")%>' SkinID="etiqueta_negra9"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "DÍAS PARA VENCIMIENTO" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>                                                                            
                                                                    <asp:Label ID="lblDiasVencimiento" runat="server" Text='<%# Eval("DIAS_PARA_VENCIMIENTO")%>' SkinID="etiqueta_negra9"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField> 
                                                            <asp:TemplateField HeaderText="DOCUMENTO" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="imgDocumentoPlantilla" ImageUrl="~/images/documento.png" BorderWidth="0" CommandArgument='<%#Eval("RUTA_DOCUMENTO_PLANTILLA") %>' Visible='<%# ( string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO_PLANTILLA").ToString() ) || Eval("RUTA_DOCUMENTO_PLANTILLA").ToString().EndsWith("/")  || Eval("RUTA_DOCUMENTO_PLANTILLA").ToString().EndsWith("\\") ? false : true ) %>' OnClick="imgDescargarDocumento_Click" style="cursor: pointer;" />
                                                                    <asp:literal ID="ltlDocumentoPlantilla" runat="server" Text='-' Visible='<%# ( !Eval("RUTA_DOCUMENTO_PLANTILLA").ToString().EndsWith("/")  && !Eval("RUTA_DOCUMENTO_PLANTILLA").ToString().EndsWith("\\") && !string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO_PLANTILLA").ToString() ) ? false : true ) %>'></asp:literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DOCUMENTO ADICIONAL" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="imgDocumentoAdicional" ImageUrl="~/images/documento.png" BorderWidth="0" CommandArgument='<%#Eval("RUTA_DOCUMENTO_ADICIONAL") %>' Visible='<%# ( string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO_ADICIONAL").ToString() ) || Eval("RUTA_DOCUMENTO_ADICIONAL").ToString().EndsWith("/")  || Eval("RUTA_DOCUMENTO_ADICIONAL").ToString().EndsWith("\\") ? false : true ) %>' OnClick="imgDescargarDocumento_Click" style="cursor: pointer;" />
                                                                    <asp:literal ID="ltlDocumentoAdicional" runat="server" Text='-' Visible='<%# ( !Eval("RUTA_DOCUMENTO_ADICIONAL").ToString().EndsWith("/")  && !Eval("RUTA_DOCUMENTO_ADICIONAL").ToString().EndsWith("\\") && !string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO_ADICIONAL").ToString() ) ? false : true ) %>'></asp:literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ADJUNTOS" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="imgAdjuntos" ImageUrl="~/images/adjunto.png" BorderWidth="0" CommandArgument='<%#Eval("RUTA_DOCUMENTO") %>' Visible='<%# ( string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO").ToString() ) || Eval("RUTA_DOCUMENTO").ToString().EndsWith("/")  || Eval("RUTA_DOCUMENTO").ToString().EndsWith("\\") ? false : true ) %>' OnClick="imgDescargarDocumento_Click" style="cursor: pointer;" />
                                                                    <asp:literal ID="ltlAdjuntos" runat="server" Text='-' Visible='<%# ( !Eval("RUTA_DOCUMENTO").ToString().EndsWith("/")  && !Eval("RUTA_DOCUMENTO").ToString().EndsWith("\\") && !string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO").ToString() ) ? false : true ) %>'></asp:literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="NÚMERO RADICADO" ItemStyle-CssClass="ItemNotificacion">
                                                                <ItemTemplate>
                                                                    <asp:literal ID="ltlRadicado" runat="server" Text='<%# (!string.IsNullOrWhiteSpace(Eval("NUMERO_RADICADO").ToString()) ? Eval("NUMERO_RADICADO") : "-")  %>'></asp:literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "ENVÍO CORREO"  ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Image runat="server" ID="imgEnvioCorreo" Width="20px" BorderWidth="0" ImageAlign="Middle" ImageUrl="~/images/CirculoVerde.png" Visible='<%# Convert.ToBoolean(Eval("ENVIO_CORREO")) %>' CssClass="botonAyudaUP" divModal="dvInformacionCumplir" style="cursor: pointer;" />
                                                                    <asp:Image ImageAlign="Middle" ID="imgEnvioCorreoGris" Width="20px" runat="server" ImageUrl="~/images/CirculoGris.png" Visible='<%# !Convert.ToBoolean(Eval("ENVIO_CORREO")) %>' style="cursor: pointer;" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>                                                                                                                       
                                                            <asp:TemplateField HeaderText = "ENVÍO DIRECCIÓN FÍSICA"  ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Image runat="server" ID="imgEnvioDireccion" Width="20px" BorderWidth="0" ImageAlign="Middle" ImageUrl="~/images/CirculoVerde.png" Visible='<%# Convert.ToBoolean(Eval("ENVIO_DIRECCION")) %>' CssClass="botonAyudaUP" divModal="dvInformacionCumplir" style="cursor: pointer;" />
                                                                    <asp:Image ImageAlign="Middle" ID="imgEnvioDireccionGris" Width="20px" runat="server" ImageUrl="~/images/CirculoGris.png" Visible='<%# !Convert.ToBoolean(Eval("ENVIO_DIRECCION")) %>' style="cursor: pointer;" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "ESTADO PUBLICADO" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="imngEstadoPublicadoCumplase" Width="20px" ImageAlign="Middle" ImageUrl="~/images/CirculoVerde.png" CommandArgument='<%#Eval("ID_PUBLICACION") %>' Visible='<%# Convert.ToInt64(Eval("ID_PUBLICACION")) > 0 %>' OnClick="imngEstadoPublicado_Click" style="cursor: pointer;" />
                                                                    <asp:Image ImageAlign="Middle" ID="imgPublicarGris" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoGris.png" Visible='<%# Convert.ToInt64(Eval("ID_PUBLICACION")) <= 0 %>' style="cursor: pointer;" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "VER DETALLE" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="imngVerEstadoCumplase" BorderWidth="0" ImageAlign="Middle" ImageUrl="~/images/ver.png" CommandArgument='<%#Eval("ID_ESTADO_PERSONA") %>' OnClick="imngVerEstado_Click" style="cursor: pointer;" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grdCumplimiento" EventName="PageIndexChanging" />
                <asp:AsyncPostBackTrigger ControlID="lnkCerrarCumplir" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <input type="button" runat="server" id="btnOcultoPublicar" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeInformacionPublicar" runat="server" TargetControlID="btnOcultoPublicar" PopupControlID="dvInformacionPublicar" BehaviorID="mpeInformacionPublicar" BackgroundCssClass="modalBackground" CancelControlID="lnkCerrarPublicar" /> 
    <div id="dvInformacionPublicar" class="ContenedorModalNot" style="display:none;" runat="server" clientidmode="Static">
        <asp:UpdatePanel runat="server" ID="upnlDetallePublicacion" UpdateMode="Conditional" >
            <ContentTemplate>
                <div class="TableResultadoModalNot">
                    <div class="RowResultadoModalNot">
                        <div class="CellLnkButtonExitModalNot">
                            <asp:LinkButton runat="server" ID="lnkCerrarPublicar" OnClick="lnkCerrarPublicar_Click">Cerrar [X]</asp:LinkButton>                            
                        </div>
                    </div>
                    <div class="RowResultadoTituloModalNot">
                        <div class="CellResultadoTituloModalNot">
                            DETALLE PUBLICACIÓN
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoModalNot">
                            <div class="TableDetalleNot">
                                <div class="Row">
                                    <div class="CellDetalleLabelNot">
                                        Título:
                                    </div>
                                    <div class="CellDetalleNot">
                                        <asp:Literal runat="server" ID="lblPublicacionTitulo"></asp:Literal>
                                    </div>
                                </div>
                                <div class="Row">
                                    <div class="CellDetalleLabelNot">
                                        Nombre del Proyecto:
                                    </div>
                                    <div class="CellDetalleNot">
                                        <asp:Literal runat="server" ID="lblPublicacionNombreExpediente"></asp:Literal>
                                    </div>
                                </div>
                                <div class="Row">
                                    <div class="CellDetalleLabelNot">
                                        Fecha de Publicación:
                                    </div>
                                    <div class="CellDetalleNot">
                                        <asp:Literal runat="server" ID="lblPublicacionFecha"></asp:Literal>
                                    </div>
                                </div>
                                <div class="Row">
                                    <div class="CellDetalleLabelNot">
                                        Fecha Retiro de Publicación:
                                    </div>
                                    <div class="CellDetalleNot">
                                        <asp:Literal runat="server" ID="lblPublicacionFechaRetiro"></asp:Literal>
                                    </div>
                                </div>
                                <div class="Row">
                                    <div class="CellDetalleLabelNot">
                                        Descripción:
                                    </div>
                                    <div class="CellDetalleNot">
                                        <asp:Literal runat="server" ID="lblPublicacionDescripcion"></asp:Literal>
                                    </div>
                                </div>
                                <div runat="server" class="Row" id="trDetalleDocumentosPublicacion">
                                    <div class="CellDetalleLabelNot">
                                        Documentos:
                                    </div>
                                    <div class="CellDetalleNot">
                                        <asp:HyperLink runat="server" Target="_blank" NavigateUrl="~/Informacion/DetalleDocumentos.aspx" CssClass="LnkDetalleModalNot">Ver Documentos</asp:HyperLink>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkCerrarPublicar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <input type="button" runat="server" id="cmdVerEstadoHide" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeVerEstadoPublicacion" runat="server" PopupControlID="dvVerEstado" TargetControlID="cmdVerEstadoHide" BehaviorID="mpeVerEstados" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <div id="dvVerEstado" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalFormularioNot">
        <asp:UpdatePanel runat="server" ID="upnlVerEstado" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="TableResultadoModalNot">
                    <div class="RowResultadoModalNot">
                        <div class="CellLnkButtonExitModalNot">
                            <asp:LinkButton runat="server" ID="lnkCerrarPublicarEstado" OnClick="lnkCerrarPublicarEstado_Click">Cerrar [X]</asp:LinkButton>
                            <asp:HiddenField runat="server" ID="hdfTipoNotificacionModalPublicacion" />
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoTituloModalNot">
                            VER DETALLE PUBLICACIÓN ESTADO
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoModalNot">
                            <div class="TableFormularioNot">
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Número de Identificación Persona Publicación:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlNumeroIdentificacionModal"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Nombre Persona Publicación:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlNombrePersonaModal"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Autoridad Ambiental:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlAutoridadAmbientalModal"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Tipo Trámite:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlTipoTramiteModal"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Número VITAL:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlNumeroVitalModal"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Número Expediente:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlNumeroExpedienteModal"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Nombre Proyecto:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlNombreProyectoModal"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Tipo de Documento:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlTipoDocumentoModal"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Número de Documento:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlNumeroDocumentoModal"></asp:Literal>
                                    </div>
                                </div>                            
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Publicación:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlPublicacionModal"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot" runat="server" id="dvDocumentosPublicacion">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Documentos Publicación:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:GridView runat="server" ID="grdDocumentosModal" AutoGenerateColumns="false" ShowFooter="false" ShowHeaderWhenEmpty="false" EmptyDataText="No existen documentos relacionados" SkinID="GrillaDatosNotificaciones" Width="100%">
                                            <HeaderStyle Font-Size="9pt" />
                                            <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                            <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                <Columns>
                                                <asp:TemplateField HeaderText = "DOCUMENTO">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="ltlDocumento" runat="server" Text='<%# Eval("NOMBRE_DOCUMENTO")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                     
                                                <asp:TemplateField HeaderText = "VER" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgDocumentoModal" ImageUrl="~/images/documento.png" BorderWidth="0" CommandArgument='<%#Eval("RUTA_DOCUMENTO") %>' OnClick="imgDescargarDocumento_Click" style="cursor: pointer;" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                     
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="RowFormularioNot" runat="server" id="dvAdjuntosPublicacion">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Adjuntos Publicación:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:GridView runat="server" ID="grdAdjuntosModal" AutoGenerateColumns="false" ShowFooter="false" ShowHeaderWhenEmpty="false" EmptyDataText="No existen documentos relacionados" SkinID="GrillaDatosNotificaciones">
                                            <HeaderStyle Font-Size="9pt" />
                                            <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                            <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <Columns>
                                                <asp:TemplateField HeaderText = "DOCUMENTO">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="ltlDocumento" runat="server" Text='<%# Eval("NOMBRE_DOCUMENTO")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                     
                                                <asp:TemplateField HeaderText = "VER">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgDocumentoModal" ImageUrl="~/images/documento.png" BorderWidth="0" CommandArgument='<%#Eval("RUTA_DOCUMENTO") %>' OnClick="imgDescargarDocumento_Click" style="cursor: pointer;" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                     
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Fecha Publicación:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlFechaPublicacionModal"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot" runat="server" id="dvFechaDesfijacion">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Fecha Desfijación:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlFechaDesfijacion"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


    <input type="button" runat="server" id="cmdVerDocumentosAdjuntosHide" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeVerDocumentosAdjuntos" runat="server" PopupControlID="dvVerDocumentosAdjuntos" TargetControlID="cmdVerDocumentosAdjuntosHide" BehaviorID="mpeVerDocumentosAdjuntos" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <div id="dvVerDocumentosAdjuntos" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalFormularioNot">
        <asp:UpdatePanel runat="server" ID="upnlVerDocumentosAdjuntos" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="TableResultadoModalNot">
                    <div class="RowResultadoModalNot">
                        <div class="CellLnkButtonExitModalNot">
                            <asp:LinkButton runat="server" ID="lnkCerrarVerAdjuntosModal" OnClick="lnkCerrarVerAdjuntosModal_Click">Cerrar [X]</asp:LinkButton>
                            <asp:HiddenField runat="server" ID="hdfTipoNotificacionModalVerAdjuntos" />
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoTituloModalNot">
                            DOCUMENTOS ADJUNTOS
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoModalNot">
                            <div class="TableFormularioNot">
                                <div class="RowResultadoModalNot">
                                    <div class="CellResultadoModalNot">
                                        <asp:GridView runat="server" AutoGenerateColumns="False" AllowPaging="false" ID="grdDocumentosAdjuntosVer" 
                                            SkinID="GrillaNotificaciones" EmptyDataText="No se encontro información de documentos" ShowHeaderWhenEmpty="true" Width="100%">
                                            <HeaderStyle Font-Size="9pt" />
                                            <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                            <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <Columns>
                                                    <asp:TemplateField HeaderText="DOCUMENTO" ItemStyle-CssClass="ItemNotificacion">
                                                        <ItemTemplate>
                                                            <asp:literal ID="ltlDocumento" runat="server" Text='<%# Eval("NOMBRE_DOCUMENTO")  %>'></asp:literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="VER" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="imgDescargarDocumentoAdjuntoVer" BorderWidth="0" ImageUrl="~/images/documento.png" CommandArgument='<%#Eval("RUTA_DOCUMENTO") %>' OnClick="imgDescargarDocumentoAdjuntoVer_Click" style="cursor: pointer;" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="uppVerDocumentosAdjuntos" runat="server" AssociatedUpdatePanelID="upnlVerDocumentosAdjuntos">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgUpdateProgressVerDocumentosAdjuntos" runat="server" SkinId="procesando" /></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>


    <input type="button" runat="server" id="cmdVerDetalleEstadoHide" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeVerDetalleEstado" runat="server" PopupControlID="dvVerDetalleEstado" TargetControlID="cmdVerDetalleEstadoHide" BehaviorID="mpeVerDetalleEstado" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <div id="dvVerDetalleEstado" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalFormularioNot">            
        <asp:UpdatePanel runat="server" ID="upnlVerDetalleEstado" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="TableResultadoModalNot">
                    <div class="RowResultadoModalNot">
                        <div class="CellLnkButtonExitModalNot">
                            <asp:LinkButton runat="server" ID="lnkCerrarVerEstado" OnClick="lnkCerrarVerEstado_Click">Cerrar [X]</asp:LinkButton>
                            <asp:HiddenField runat="server" ID="hdfTipoNotificacionModalVerEstado" />
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoTituloModalNot">
                            VER DETALLE ESTADO DE NOTIFICACIÓN
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoModalNot">
                            <div class="TableFormularioNot">
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlNumeroVital">Número VITAL:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlNumeroVital"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlNumeroExpediente">Número Expediente:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlNumeroExpediente"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlActoAdministrativo">Acto Administrativo:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlActoAdministrativo"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlNumeroActo">Número Acto Administrativo:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlNumeroActo"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot" runat="server" id="dvIdentificacion">
                                    <div class="CellFormularioNot">
                                        <label for="ltlIdentificacion">Identificación:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlIdentificacion"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot" runat="server" id="dvUsuario">
                                    <div class="CellFormularioNot">
                                        <label for="ltlUsuario">Usuario:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlUsuario"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlEstadoActual">Estado:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlEstadoActual"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlFechaEstadoActual">Fecha Estado:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlFechaEstadoActual"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="txtObservacion">Observación:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlObservacion"></asp:Literal>
                                    </div>
                                </div>                
                                <div class="RowFormularioNot" runat="server" id="dvDocumentos">
                                    <div class="CellFormularioNot">
                                        <label for="fuplDocumentoAdicional">Documentos:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:GridView runat="server" ID="grdDocumentos" AutoGenerateColumns="false" ShowFooter="false" ShowHeaderWhenEmpty="false" EmptyDataText="No existen documentos relacionados" SkinID="GrillaDatosNotificaciones" Width="100%">
                                            <HeaderStyle Font-Size="9pt" />
                                            <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                            <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <Columns>
                                                <asp:TemplateField HeaderText = "DOCUMENTO">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="ltlDocumento" runat="server" Text='<%# Eval("NOMBRE_DOCUMENTO")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                     
                                                <asp:TemplateField HeaderText = "VER" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgDocumentoModal" ImageUrl="~/images/documento.png" CommandArgument='<%#Eval("RUTA_DOCUMENTO") %>' OnClick="imgDocumentoModal_Click" style="cursor: pointer;" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                     
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>                                                                
                                <div class="RowFormularioNot" runat="server">
                                    <div class="CellFormularioNot">
                                        <label for="chkEnviarDireccion">Enviar a Dirección:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlEnviarDireccion"></asp:Literal>
                                    </div>
                                </div>                                    
                                <div class="RowFormularioNot" runat="server" id="dvListadoDirecciones">
                                    <div class="CellFormularioNot">
                                        <label for="ltlMunicipio">
                                            Direcciones:
                                        </label>
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:GridView runat="server" ID="grdDirecciones" AutoGenerateColumns="false" ShowFooter="true" ShowHeaderWhenEmpty="false" EmptyDataText="No se han adicionado direcciones" SkinID="GrillaDatosNotificaciones" Width="100%">
                                            <HeaderStyle Font-Size="9pt" />
                                            <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                            <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <Columns>                                                    
                                                    <asp:TemplateField HeaderText = "DEPARTAMENTO">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="ltlDepartamento" runat="server" Text='<%# Eval("Departamento")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                 </asp:TemplateField>
                                                    <asp:TemplateField HeaderText = "MUNICIPIO">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="ltlMunicipio" runat="server" Text='<%# Eval("Municipio")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText = "DIRECCIÓN">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="ltlDireccion" runat="server" Text='<%# Eval("Direccion")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="RowFormularioNot" runat="server">
                                    <div class="CellFormularioNot">
                                        <label for="chkEnviarCorreo">Enviar Correo Eléctronico:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlEnviarCorreo"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot" runat="server" id="dvListaCorreos">
                                    <div class="CellFormularioNot">
                                        <label for="ltlMunicipio">
                                            Correos:
                                        </label>
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:GridView runat="server" ID="grdCorreos" AutoGenerateColumns="false" ShowFooter="true" ShowHeaderWhenEmpty="false" EmptyDataText="No se han adicionado correos electrónicos" SkinID="GrillaDatosNotificaciones" Width="100%">
                                            <HeaderStyle Font-Size="9pt" />
                                            <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                            <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <Columns>                                                
                                                    <asp:TemplateField HeaderText = "CORREO">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="ltlCorreo" runat="server" Text='<%# Eval("Correo")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText = "FECHA ENVÍO">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="ltlFechaEnvioCorreo" runat="server" Text='<%# Convert.ToDateTime(Eval("FechaEnvío")).ToString("dd/MM/yyyy HH:mm") %>'></asp:Literal>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>                                    
                                <div class="RowFormularioNot" runat="server" id="dvAdjuntarActoAdministrativo">
                                    <div class="CellFormularioNot">
                                        <label for="chkAdjuntarActoAdministrativo">Adjuntar Acto Administrativo:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlAdjuntarActoAdministrativo"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot" runat="server" id="dvAdjuntarConceptosActoAdministrativo">
                                    <div class="CellFormularioNot">
                                        <label for="chkAdjuntarActoAdministrativo">Adjuntar Concepto(s) Acto Administrativo:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlAdjuntarConceptosActoAdministrativo"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot" runat="server" id="dvAdjuntos">
                                    <div class="CellFormularioNot">
                                        <label for="txtAdjunto">Adjuntos Correo Eléctronico:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:GridView runat="server" ID="grdAdjuntos" AutoGenerateColumns="false" ShowFooter="false" ShowHeaderWhenEmpty="false" EmptyDataText="No existen documentos relacionados" SkinID="GrillaDatosNotificaciones" Width="100%">
                                            <HeaderStyle Font-Size="9pt" />
                                            <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                            <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                            <Columns>
                                                <asp:TemplateField HeaderText = "DOCUMENTO">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="ltlDocumento" runat="server" Text='<%# Eval("NOMBRE_DOCUMENTO")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                     
                                                <asp:TemplateField HeaderText = "VER" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgDocumentoModal" ImageUrl="~/images/documento.png" CommandArgument='<%#Eval("RUTA_DOCUMENTO") %>' OnClick="imgDocumentoModal_Click" style="cursor: pointer;" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                     
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="RowFormularioNot" runat="server" id="dvTextoCorreo">
                                    <div class="CellFormularioNot">
                                        <label for="txtTextoCorreo">Texto Correo Eléctronico:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlTextoCorreo"></asp:Literal>
                                    </div>
                                </div>                                  
                                <div class="RowFormularioNot" runat="server" id="dvTipoIdentificacionPersonaNotificar">
                                    <div class="CellFormularioNot">
                                        <label for="ltlTipoIdentificacionPersonaNotificar">Tipo Identificación Persona Notificar:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlTipoIdentificacionPersonaNotificar"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot" runat="server" id="dvNumeroIdentificacionPersonaNotificar">
                                    <div class="CellFormularioNot">
                                        <label for="ltlNumeroIdentificacionPersonaNotificar">Número Identificación Persona Notificar:</label>
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlNumeroIdentificacionPersonaNotificar"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot" runat="server" id="dvNombrePersonaNotificar">
                                    <div class="CellFormularioNot">
                                        <label for="ltlNombrePersonaNotificar">Nombre Persona Notificar:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlNombrePersonaNotificar"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot" runat="server" id="dvCalidadPersonaNotificar">
                                    <div class="CellFormularioNot">
                                        <label for="ltlCalidadPersonaNotificar">Calidad Persona Notificar:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlCalidadPersonaNotificar"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot" runat="server" id="dvReferenciaRecepcion">
                                    <div class="CellFormularioNot">
                                        <label for="txtReferenciaRecepcion">Referencia:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlReferencia"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot" runat="server" id="dvFechaRecepcion">
                                    <div class="CellFormularioNot">
                                        <label for="txtReferenciaRecepcion">Fecha de Referencia:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlFechaReferencia"></asp:Literal>
                                    </div>
                                </div>                                                                
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

</asp:Content>
