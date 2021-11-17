<%@ Page Language="C#"  MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="PersonaNotificacion.aspx.cs" Inherits="NotificacionElectronica_PersonaNotificacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
    <script language="javascript" type="text/javascript">

        var hrefPag = "";

        function RecargarAcordion(hrefPagina) {

            var blnRecargar = false;

            blnRecargar = hrefPagina.indexOf("grdActosAdministrativos") == -1 && hrefPagina.indexOf("Page$") == -1;
            blnRecargar = blnRecargar || (hrefPagina.indexOf("grdActosAdministrativos") != -1 && hrefPagina.indexOf("Page$") != -1)
            blnRecargar = blnRecargar && (hrefPagina != "NO_Recargar") && (hrefPagina.indexOf("lnkEditarGrdPersonas") == -1) && (hrefPagina.indexOf("lnkEliminarGrdPersonas") == -1);

            return blnRecargar;
        }

        function mostrarCalendarioAgregarEditarPersona(sender, args) {
            sender._popupBehavior._element.style.zIndex = 100004;
            if (navigator.userAgent.toLowerCase().indexOf('chrome') > 1) {
                sender._popupBehavior._element.style.marginTop = $(window).scrollTop() + "px";
            }
        }

        $(function () {
            $("[id*=accordionActosAdministrativos]").accordion({
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

                $("#cmdModalCambiarEstadoActoCancelar").click(function (e) {
                    hrefPag = "NO_Recargar";
                });

                $("#cmdCerrarVerDocumentoActoAdministrativo").click(function (e) {
                    hrefPag = "NO_Recargar";
                });
                
                $("#cmdModalCambiarConfiguracionActoCancelar").click(function (e) {
                    hrefPag = "NO_Recargar";
                });

                $("#chkNotificarCambiarConfiguracionActo").click(function (e) {
                    hrefPag = "NO_Recargar";
                });

                $("#chkComunicarCambiarConfiguracionActo").click(function (e) {
                    hrefPag = "NO_Recargar";
                });

                $("#chkCumplirCambiarConfiguracionActo").click(function (e) {
                    hrefPag = "NO_Recargar";
                });

                $("#chkRecursoCambiarConfiguracionActo").click(function (e) {
                    hrefPag = "NO_Recargar";
                });                

                $("#cmdModalConfigurarPersonasCancelar").click(function (e) {
                    hrefPag = "NO_Recargar";
                });    

                $("#cmdAdicionarConfigurarPersonas").click(function (e) {
                    hrefPag = "NO_Recargar";
                });

                $("#cmdAgregarEditarUsuarioCerrar").click(function (e) {
                    hrefPag = "NO_Recargar";
                });

                $("#cmdAgregarEditarUsuarioAdicionar").click(function (e) {
                    hrefPag = "NO_Recargar";
                });

                $("#cmdModalEliminarPersonaEliminar").click(function (e) {
                    hrefPag = "NO_Recargar";
                });

                $("#cmdModalEliminarPersonaCancelar").click(function (e) {
                    hrefPag = "NO_Recargar";
                });
                
                $("#cmdValidarUsuarioAgregarEditarUsuario").click(function (e) {
                    hrefPag = "NO_Recargar";
                });                

                $('#cboTipoNotificacionAgregarEditarUsuario').change(function () {
                    hrefPag = "NO_Recargar";
                });

                $('#cboFlujoTipoNotificacionAgregarEditarUsuario').change(function () {
                    hrefPag = "NO_Recargar";
                });

                if (RecargarAcordion(hrefPag)) {
                    $("[id*=accordionActosAdministrativos]").accordion({
                        collapsible: true,
                        heightStyle: "content",
                        active: false
                    });
                }

                hrefPag = "";

            });
        });

    </script>

    <asp:ScriptManager ID="scmManager" runat="server"></asp:ScriptManager>

    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="ADMINISTRACIÓN DE ACTOS ADMINISTRATIVOS" SkinID="titulo_principal_blanco"></asp:Label>
        &nbsp;
        <a href="#" id="hrfCerrarVentana" runat="server" onclick="window.close();return false;" class="Salir">Salir [X]</a>
    </div>

    <div class="contact_form" id="divBuscarActoAdministrativo" runat="server">
        <asp:UpdatePanel runat="server" ID="upnlBuscar" UpdateMode="Conditional">
            <ContentTemplate>
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
                                        <label for="txtNumeroActo">Estado Verificación Acto Administrativo:</label>
                                        <asp:DropDownList ID="cboEstadoActoAdministrativo" runat="server" SkinID="lista_desplegable2" />
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
                            <asp:HiddenField runat="server" ID="hdfNumeroVital" />
                            <asp:HiddenField runat="server" ID="hdfExpediente" />
                            <asp:HiddenField runat="server" ID="hdfIdentificacionUsuario" />
                            <asp:HiddenField runat="server" ID="hdfUsuarioNotificar" />
                            <asp:HiddenField runat="server" ID="hdfNumeroActo" />
                            <asp:HiddenField runat="server" ID="hdfTipoActo" />
                            <asp:HiddenField runat="server" ID="hdfEstadoActoAdministrativo" />
                            <asp:HiddenField runat="server" ID="hdfFechaDesde" />
                            <asp:HiddenField runat="server" ID="hdfFechaHasta" />
                            <asp:Button runat="server" ID="cmdBuscar" ValidationGroup="NotBuscar" Text="Buscar" ClientIDMode="Static" OnClick="cmdBuscar_Click"/>
                            <asp:ValidationSummary ID="valNotBuscar" runat="server" ValidationGroup="NotBuscar" ShowMessageBox="true" ShowSummary="false" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdBuscar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="uppBuscar" runat="server" AssociatedUpdatePanelID="upnlBuscar">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgUpdateProgressBuscar" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel runat="server" ID="upnlMensaje" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="contact_form" id="divMensaje" runat="server" visible="false">  
                <div class="Table">
                    <div class="Row">
                        <div class="CellMensaje">
                            <asp:Label runat="server" ID="lblMensaje"></asp:Label>
                        </div>
                    </div>
                </div>            
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="upnlConsultaActosAdministrativos" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="contact_form" id="divActosAdministrativos" runat="server">
                <div class="TableReporteNot">
                    <div class="RowReporteNot">
                        <div class="CellReporteNot">
                            <asp:GridView ID="grdActosAdministrativos" runat="server" Width="100%" 
                                AutoGenerateColumns="False" AllowPaging="True" ShowHeader="false"
                                EmptyDataText="No se encontraron actos administrativos que cumplan con los parámetros ingresados" 
                                ShowHeaderWhenEmpty="false" PageSize="10" SkinID="GrillaNotificaciones"
                                OnPageIndexChanging="grdActosAdministrativos_PageIndexChanging" 
                                OnRowDataBound="grdActosAdministrativos_RowDataBound">
                                <HeaderStyle Font-Size="9pt" />
                                <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div id="accordionActosAdministrativos" style="color: #000000 !important; width: 100% !important;">
                                                <div id="divHeaderAccordionActos" runat="server" class="headerAccordionAdmNotificacion">
                                                    <div class="TableReporteNotInt">
                                                        <div class="RowReporteNotInt">
                                                            <div class="CellReporteNotIntTitulo">
                                                                Expediente:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">
                                                                <asp:literal ID="ltlGrdExpediente" runat="server" Text='<%# Eval("EXPEDIENTE") %>'></asp:literal>
                                                                <asp:HyperLink ID="lnkGrdExpediente" runat="server" Text='<%# Eval("EXPEDIENTE")  %>' CssClass="botonAyudaUP" style="cursor: pointer;"></asp:HyperLink>
                                                            </div>
                                                            <div class="CellReporteNotIntTitulo">
                                                                Número VITAL:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">
                                                                <asp:literal ID="ltlGrdNumeroVital" runat="server" Text='<%# Eval("NUM_VITAL") %>'></asp:literal>
                                                            </div>
                                                            <div class="CellReporteNotIntTitulo">
                                                                Estado:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">
                                                                <asp:literal ID="ltlGrdModificarEstado" runat="server" Text='<%# Eval("ESTADO_ACTO") %>'></asp:literal>
                                                            </div>
                                                        </div>
                                                        <div class="RowReporteNotInt">
                                                            <div class="CellReporteNotIntTitulo">
                                                                Tipo Acto Administrativo:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">
                                                                <asp:literal ID="ltlGrdTipoActoAdministrativo" runat="server" Text='<%# Eval("TIPO_ACTO_ADMINISTRATIVO")  %>'></asp:literal>
                                                            </div>
                                                            <div class="CellReporteNotIntTitulo">
                                                                Número Acto Administrativo:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">
                                                                <asp:literal ID="ltlGrdNumeroActoAdministrativo" runat="server" Text='<%# Eval("NUMERO_ACTO_ADMINISTRATIVO")  %>'></asp:literal>
                                                            </div>
                                                            <div class="CellReporteNotIntTitulo">
                                                                Fecha Acto Administrativo:
                                                            </div>
                                                            <div class="CellReporteNotIntTexto">
                                                                <asp:literal ID="ltlGrdFechaActoAdministrativo" runat="server" Text='<%# (Eval("FECHA_ACTO_ADMINISTRATIVO") != System.DBNull.Value ? Convert.ToDateTime(Eval("FECHA_ACTO_ADMINISTRATIVO")).ToString("dd/MM/yyyy") : "-" )  %>'></asp:literal>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="bodyAccordeonAdmNotificacion">
                                                    <asp:UpdatePanel runat="server" ID="upnlConfiguracionActo" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="TableAdministradorNotInt" style="color: #000000 !important; width: 100% !important;">
                                                                <div class="RowAdministradorNotInt">
                                                                    <div class="CellAdministradorTituloNotInt">DOCUMENTO ACTO ADMINISTRATIVO</div>
                                                                    <div class="CellAdministradorTituloNotInt">NOTIFICAR</div>
                                                                    <div class="CellAdministradorTituloNotInt">COMUNICAR</div>
                                                                    <div class="CellAdministradorTituloNotInt">CUMPLIR</div>
                                                                    <div class="CellAdministradorTituloNotInt">PUBLICAR</div>
                                                                    <div class="CellAdministradorTituloNotInt">RECURSO REPOSICIÓN</div>
                                                                    <div class="CellAdministradorTituloNotInt">EDITAR CONFIGURACIÓN</div>
                                                                    <div class="CellAdministradorTituloNotInt">CAMBIAR ESTADO</div>
                                                                </div>
                                                                <div class="RowAdministradorNotInt">
                                                                    <div class="CellAdministradorNotInt">
                                                                        <asp:ImageButton runat="server" ID="imgGrdDescargarDocumento" ImageUrl="~/images/documento.png" BorderWidth="0" CommandArgument='<%#Eval("RUTA_DOCUMENTO_ACTO") %>' Visible='<%# ( !string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO_ACTO").ToString() ) && !Eval("RUTA_DOCUMENTO_ACTO").ToString().EndsWith("/")  && !Eval("RUTA_DOCUMENTO_ACTO").ToString().EndsWith("\\") ? true : false ) %>' OnClick="imgGrdDescargarDocumento_Click"/>
                                                                        <asp:LinkButton ID="lnkGrdDescargarDocumento" runat="server" CommandArgument='<%#Eval("RUTA_DOCUMENTO_ACTO") %>' OnClick="lnkGrdDescargarDocumento_Click" Visible='<%# ( Eval("RUTA_DOCUMENTO_ACTO").ToString().EndsWith("/")  || Eval("RUTA_DOCUMENTO_ACTO").ToString().EndsWith("\\") ? true : false ) %>'> 
                                                                            <asp:Image runat="server" ID="imgGrdDescargarDocumentoCarpeta" ImageUrl="~/images/documento.png" BorderWidth="0" />
                                                                        </asp:LinkButton>
                                                                        <asp:literal ID="ltlGrdDescargarDocumento" runat="server" Text='-' Visible='<%# string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO_ACTO").ToString() ) %>'></asp:literal>
                                                                    </div>
                                                                    <div class="CellAdministradorNotInt">
                                                                        <asp:LinkButton ID="lnkGrdNotificaciones" runat="server" CommandArgument='<%#Eval("ID_ACTO_NOTIFICACION").ToString() + "@" + Eval("ID_ESTADO_ACTO").ToString() %>' OnClick="lnkGrdNotificaciones_Click"> 
                                                                            <asp:Image ImageAlign="Middle" ID="imgGrdNotificar" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoVerde.png"/>
                                                                        </asp:LinkButton>
                                                                        <asp:Image ImageAlign="Middle" ID="imgGrdNotificarGris" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoGris.png"/>
                                                                    </div>
                                                                    <div class="CellAdministradorNotInt">
                                                                        <asp:LinkButton ID="lnkGrdComunicar" runat="server" CommandArgument='<%#Eval("ID_ACTO_NOTIFICACION").ToString() + "@" + Eval("ID_ESTADO_ACTO").ToString() %>' OnClick="lnkGrdComunicar_Click"> 
                                                                            <asp:Image ImageAlign="Middle" ID="imgGrdComunicar" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoVerde.png"/>
                                                                        </asp:LinkButton>                                                                        
                                                                        <asp:Image ImageAlign="Middle" ID="imgGrdComunicarGris" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoGris.png"/>
                                                                    </div>
                                                                    <div class="CellAdministradorNotInt">
                                                                        <asp:LinkButton ID="lnkGrdCumplir" runat="server" CommandArgument='<%#Eval("ID_ACTO_NOTIFICACION").ToString() + "@" + Eval("ID_ESTADO_ACTO").ToString() %>' OnClick="lnkGrdCumplir_Click"> 
                                                                            <asp:Image ImageAlign="Middle" ID="imgGrdCumplir" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoVerde.png"/>
                                                                        </asp:LinkButton>
                                                                        <asp:Image ImageAlign="Middle" ID="imgGrdCumplirNotificar" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoVerde.png"/>
                                                                        <asp:Image ImageAlign="Middle" ID="imgGrdCumplirGris" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoGris.png"/>  
                                                                    </div>
                                                                    <div class="CellAdministradorNotInt">
                                                                        <asp:Image ImageAlign="Middle" ID="imgGrdPublicar" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoVerde.png"/>
                                                                        <asp:Image ImageAlign="Middle" ID="imgGrdPublicarGris" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoGris.png"/>
                                                                    </div>
                                                                    <div class="CellAdministradorNotInt">
                                                                        <asp:Image ImageAlign="Middle" ID="imgGrdRecurso" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoVerde.png"/>
                                                                        <asp:Image ImageAlign="Middle" ID="imgGrdRecursoGris" Width="20px" BorderWidth="0" runat="server" ImageUrl="~/images/CirculoGris.png"/>
                                                                    </div>
                                                                    <div class="CellAdministradorNotInt">
                                                                        <asp:LinkButton ID="lnkGrdCambiarConfiguracion" runat="server" Visible='<%#Eval("ID_ESTADO_ACTO").ToString() != "4" %>' CommandArgument='<%#Eval("ID_ACTO_NOTIFICACION").ToString() + "@" + Eval("ORIGEN_DATOS").ToString()  + "@" + (Convert.ToBoolean(Eval("PUBLICACION")) ? "1" : "0") + "@" + Eval("RUTA_DOCUMENTO_ACTO").ToString() %>' OnClick="lnkGrdCambiarConfiguracion_Click"> 
                                                                            <asp:Image runat="server" ID="imgGrdCambiarConfiguracion" ImageUrl="~/App_Themes/Img/Edit.png" BorderWidth="0" />
                                                                        </asp:LinkButton>
                                                                        <asp:Literal runat="server" ID="ltlGrdCambiarConfiguracion" Visible='<%#Eval("ID_ESTADO_ACTO").ToString() == "4" %>'>-</asp:Literal>
                                                                    </div>
                                                                    <div class="CellAdministradorNotInt">
                                                                        <asp:LinkButton ID="lnkGrdCambiarEstado" runat="server" CommandArgument='<%#Eval("ID_ACTO_NOTIFICACION").ToString() + "-" + Eval("ID_ESTADO_ACTO").ToString()%>' OnClick="lnkGrdCambiarEstado_Click"> 
                                                                            <asp:Image runat="server" ID="imgGrdCambiarEstado" ImageUrl="~/images/cambiar.png" BorderWidth="0" />
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
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
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="grdActosAdministrativos" EventName="PageIndexChanging" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="uppConsultaActosAdministrativos" runat="server" AssociatedUpdatePanelID="upnlConsultaActosAdministrativos">
        <ProgressTemplate>  
            <div id="ModalProgressContainer">
                <div>
                    <p>Procesando...</p>
                    <p><asp:Image ID="imgUpdateProgressConsultaActosAdministrativos" runat="server" SkinId="procesando"/></p>
                </div>
            </div>                         
        </ProgressTemplate>
    </asp:UpdateProgress>

    <input type="button" runat="server" id="cmdCambiarEstadoActoHide" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeModalCambiarEstadoActo" runat="server" PopupControlID="dvModalCambiarEstadoActo" TargetControlID="cmdCambiarEstadoActoHide" BehaviorID="mpeModalCambiarEstadoActos" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <div id="dvModalCambiarEstadoActo" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalFormularioNot">        
        <asp:UpdatePanel runat="server" ID="upnlModalCambiarEstadoActo" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="TableResultadoModalNot">
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoTituloModalNot">
                            CAMBIO ESTADO ACTO ADMINISTRATIVO
                        </div>
                    </div>                            
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoModalNot">
                            <div class="TableFormularioNot">
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <div class="TableMensajesNot">
                                            <div class="RowMensajesNot">
                                                <div class="CellMensajesNot" style="width:50px; vertical-align: middle;"><asp:Image runat="server" ID="imgImportante" ImageUrl="~/images/advertencia.png" Width="100%" /></div>
                                                <div class="CellMensajesNot">
                                                    <asp:Literal runat="server" ID="lblTextoProcesosRelacionadosCambioEstado"></asp:Literal>
                                                    <asp:GridView runat="server" ID="grdActosAdministrativosCambioEstado" Width="100%"
                                                                  AutoGenerateColumns="False" AllowPaging="False" ShowHeader="true"
                                                                  EmptyDataText="No se encontraron actos administrativos sin notificar que se encuentren relacionados al actual acto administrativo" 
                                                                  ShowHeaderWhenEmpty="false" SkinID="GrillaNotificaciones">
                                                        <HeaderStyle CssClass="TituloTablaNotificacion" />
                                                        <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Número VITAL" ItemStyle-CssClass="ItemNotificacion">
                                                                <ItemTemplate>
                                                                    <asp:literal ID="ltlNumeroVITAL" runat="server" Text='<%# (!string.IsNullOrWhiteSpace(Eval("NumeroSILPA").ToString()) ? Eval("NumeroSILPA").ToString() : "-")   %>'></asp:literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Expediente" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                                <ItemTemplate>
                                                                    <asp:literal ID="ltlExpediente" runat="server" Text='<%# (!string.IsNullOrWhiteSpace(Eval("ProcesoAdministracion").ToString()) ? Eval("ProcesoAdministracion").ToString() : "-")   %>'></asp:literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Número Acto Administrativo" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                                <ItemTemplate>
                                                                    <asp:literal ID="ltlNumeroActo" runat="server" Text='<%# (!string.IsNullOrWhiteSpace(Eval("NumeroActoAdministrativo").ToString()) ? Eval("NumeroActoAdministrativo").ToString() : "-")   %>'></asp:literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fecha Acto Administrativo" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                                <ItemTemplate>
                                                                    <asp:literal ID="ltlFechaActo" runat="server" Text='<%# (Convert.ToDateTime(Eval("FechaActo")) != default(DateTime) ? Convert.ToDateTime(Eval("FechaActo")).ToString("dd/MM/yyyy") : "-")   %>'></asp:literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:Literal runat="server" ID="lblMensajeCambioEstado"></asp:Literal>
                                                </div>
                                            </div>                                            
                                        </div>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellButtonModal">
                                        <br /><br />                                                                                
                                        <b>Estado Acto Administrativo:</b>
                                        <asp:DropDownList runat="server" ID="cboEstadoCambiarEstadoActo"></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvEstadoCambiarEstadoActo" ControlToValidate="cboEstadoCambiarEstadoActo" InitialValue="-1" ErrorMessage="Debe seleccionar el estado" ValidationGroup="CambiarEstadoActo">*</asp:RequiredFieldValidator>
                                        <asp:CustomValidator runat="server" ID="cvEstadoCambiarEstadoActo" ErrorMessage="Se debe revisar configuración de usuarios antes de cambiar estado" OnServerValidate="cvEstadoCambiarEstadoActo_ServerValidate" ValidationGroup="CambiarEstadoActo">*</asp:CustomValidator>
                                        <br /><br />
                                    </div>
                                </div>                                     
                            </div>
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellButtonModal">
                            <asp:HiddenField runat="server" ID="hdfActoIDModalCambiarEstado" />
                            <asp:HiddenField runat="server" ID="hdfEstadoIDModalCambiarEstado" />
                            <asp:Button ID="cmdModalCambiarEstadoActoAceptar" runat="server" ClientIDMode="Static" Text="Cambiar" CssClass="boton" CausesValidation="true" ValidationGroup="CambiarEstadoActo" OnClick="cmdModalCambiarEstadoActoAceptar_Click" />
                            <asp:Button ID="cmdModalCambiarEstadoActoCancelar" runat="server" ClientIDMode="Static" Text="Cancelar" CssClass="boton" CausesValidation="false" OnClick="cmdModalCambiarEstadoActoCancelar_Click" />
                            <asp:ValidationSummary runat="server" ID="valCambiarEstadoActo" ValidationGroup="CambiarEstadoActo" ShowSummary="false" ShowMessageBox="true" />                            
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdModalCambiarEstadoActoAceptar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="cmdModalCambiarEstadoActoCancelar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="uppVerDocumentoActoAdministrativo" runat="server" AssociatedUpdatePanelID="upnlModalCambiarEstadoActo">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgUpdateProgresModalCambiarEstadoActo" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>


    <input type="button" runat="server" id="cmdCambiarConfiguracionActoHide" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeModalCambiarConfiguracionActo" runat="server" PopupControlID="dvModalCambiarConfiguracionActo" TargetControlID="cmdCambiarConfiguracionActoHide" BehaviorID="mpeModalCambiarConfiguracionActos" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <div id="dvModalCambiarConfiguracionActo" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalFormularioNot">        
        <asp:UpdatePanel runat="server" ID="upnlModalCambiarConfiguracionActo" UpdateMode="Conditional">
            <ContentTemplate>                
                <div class="TableResultadoModalNot">
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoTituloModalNot">
                            EDITAR CONFIGURACIÓN ACTO ADMINISTRATIVO
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoModalNot">
                            <div class="TableFormularioNot">
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlExpedienteCambiarConfiguracionActo">Expediente:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlExpedienteCambiarConfiguracionActo"></asp:Literal>
                                    </div>
                                </div>           
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlNumeroVitalCambiarConfiguracionActo">Número VITAL:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlNumeroVitalCambiarConfiguracionActo"></asp:Literal>
                                    </div>
                                </div>           
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlTipoActoCambiarConfiguracionActo">Tipo Acto Administrativo:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlTipoActoCambiarConfiguracionActo"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlNumeroActoCambiarConfiguracionActo">Número Acto Administrativo:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlNumeroActoCambiarConfiguracionActo"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlFechaActoCambiarConfiguracionActo">Fecha Acto Administrativo:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlFechaActoCambiarConfiguracionActo"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="chkNotificarCambiarConfiguracionActo">Notificar:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:CheckBox runat="server" ID="chkNotificarCambiarConfiguracionActo" ClientIDMode="Static" OnCheckedChanged="chkParametroCambiarConfiguracionActo_CheckedChanged" AutoPostBack="true" EnableTheming="false" />
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="chkComunicarCambiarConfiguracionActo">Comunicar:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:CheckBox runat="server" ID="chkComunicarCambiarConfiguracionActo" ClientIDMode="Static" OnCheckedChanged="chkParametroCambiarConfiguracionActo_CheckedChanged" AutoPostBack="true" EnableTheming="false" />
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="chkCumplirCambiarConfiguracionActo">Cumplir:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:CheckBox runat="server" ID="chkCumplirCambiarConfiguracionActo" ClientIDMode="Static" OnCheckedChanged="chkParametroCambiarConfiguracionActo_CheckedChanged" AutoPostBack="true" EnableTheming="false" />
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="chkPublicarCambiarConfiguracionActo">Publicar:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:CheckBox runat="server" ID="chkPublicarCambiarConfiguracionActo" EnableTheming="false" />
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="chkRecursoCambiarConfiguracionActo">Aplica Recurso Reposición:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:CheckBox runat="server" ID="chkRecursoCambiarConfiguracionActo" ClientIDMode="Static" OnCheckedChanged="chkRecursoCambiarConfiguracionActo_CheckedChanged" AutoPostBack="true" EnableTheming="false" />
                                    </div>
                                </div>
                                <div class="RowFormularioNot" runat="server" id="dvArchivoActoCambiarConfiguracionActo">
                                    <div class="CellFormularioNot">
                                        <label for="cboArchivoActoCambiarConfiguracionActo">Archivo Acto Administrativo:</label>
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:DropDownList runat="server" ID="cboArchivoActoCambiarConfiguracionActo"></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvArchivoActoCambiarConfiguracionActo" ControlToValidate="cboArchivoActoCambiarConfiguracionActo" InitialValue="-1" ValidationGroup="CambiarConfiguracionActo" ErrorMessage="Por favor seleccione el archivo que contiene el acto administrativo">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellButtonModal">
                            <asp:HiddenField runat="server" ID="hdfActoIDModalCambiarConfiguracion" />
                            <asp:HiddenField runat="server" ID="hdfPublicaModalCambiarConfiguracion" />
                            <asp:HiddenField runat="server" ID="hdfRutaModalCambiarConfiguracion" />
                            <asp:HiddenField runat="server" ID="hdfOrigenModalCambiarConfiguracion" />
                            <asp:Button ID="cmdModalCambiarConfiguracionActoAceptar" runat="server" ClientIDMode="Static" Text="Guardar" CssClass="boton" CausesValidation="true" ValidationGroup="CambiarConfiguracionActo" OnClick="cmdModalCambiarConfiguracionActoAceptar_Click" />
                            <asp:Button ID="cmdModalCambiarConfiguracionActoCancelar" runat="server" ClientIDMode="Static" Text="Cancelar" CssClass="boton" CausesValidation="false" OnClick="cmdModalCambiarConfiguracionActoCancelar_Click" />
                            <asp:CustomValidator runat="server" ID="cvCambiarConfiguracionActo" ValidationGroup="CambiarConfiguracionActo" OnServerValidate="cvCambiarConfiguracionActo_ServerValidate">&nbsp;</asp:CustomValidator>
                            <asp:ValidationSummary runat="server" ID="valCambiarConfiguracionActo" ValidationGroup="CambiarConfiguracionActo" ShowSummary="false" ShowMessageBox="true" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdModalCambiarConfiguracionActoAceptar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="cmdModalCambiarConfiguracionActoCancelar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="chkNotificarCambiarConfiguracionActo" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="chkComunicarCambiarConfiguracionActo" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="chkCumplirCambiarConfiguracionActo" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="chkRecursoCambiarConfiguracionActo" EventName="CheckedChanged" />                
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="uppModalCambiarConfiguracionActo" runat="server" AssociatedUpdatePanelID="upnlModalCambiarConfiguracionActo">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgUpdateProgresModalCambiarConfiguracionActo" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>


    <input type="button" runat="server" id="cmdConfigurarPersonasHide" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeModalConfigurarPersonas" runat="server" PopupControlID="dvModalConfigurarPersonas" TargetControlID="cmdConfigurarPersonasHide" BehaviorID="mpeModalConfigurarPersonass" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <div id="dvModalConfigurarPersonas" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalFormularioNot">        
        <asp:UpdatePanel runat="server" ID="upnlModalConfigurarPersonas" UpdateMode="Conditional">
            <ContentTemplate>                
                <div class="TableResultadoModalNot">
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoTituloModalNot">
                            <asp:Literal runat="server" ID="ltlTituloConfigurarPersonas"></asp:Literal>
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellButtonModalDerechaNot">
                            <asp:Button runat="server" ID="cmdAdicionarConfigurarPersonas" ClientIDMode="Static" Text="Adicionar Persona" CssClass="boton" CausesValidation="false" OnClick="cmdAdicionarConfigurarPersonas_Click" />
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoModalNotAdm">
                            <asp:GridView ID="grdPersonasConfigurarPersonas" runat="server" Width="100%" 
                                AutoGenerateColumns="False" AllowPaging="false" SkinID="GrillaNotificaciones" 
                                CellPadding="4" CellSpacing="2" 
                                OnRowDataBound="grdPersonas_RowDataBound" 
                                EmptyDataText="No se encontraron personas configuradas" 
                                ShowHeaderWhenEmpty="true" DataKeyNames="ID_PERSONA">
                                <HeaderStyle Font-Size="9pt" />
                                <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                <Columns>
                                    <asp:TemplateField HeaderText = "PERSONA NOTIFICAR">
                                        <ItemTemplate>
                                            <asp:Literal ID="ltlNombreUsuarioGrdPersonas" runat="server" Text='<%# Eval("USUARIO_NOTIFICAR")%>'></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "IDENTIFICACIÓN">
                                        <ItemTemplate>
                                            <asp:Literal ID="ltlIdentificacionUsuarioGrdPersonas" runat="server" Text='<%# Eval("IDENTIFICACION_USUARIO_NOTIFICAR")%>'></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "FLUJO">
                                        <ItemTemplate>
                                            <asp:Literal ID="ltlFujoGrdPersonas" runat="server" Text='<%# Eval("FLUJO")%>'></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "ESTADO ACTUAL"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="ltlEstadoActualGrdPersonas" runat="server" Text='<%# Eval("ESTADO_ACTUAL")%>'></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "FECHA_ESTADO_ACTUAL"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="ltlFechaEstadoActualGrdPersonas" runat="server" Text='<%# Eval("FECHA_ESTADO_ACTUAL")  %>'></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "NOTIFICACIÓN ELECTRÓNICA"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="ltlNotificacionElectronicaGrdPersonas" runat="server" Text=''></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EDITAR" ItemStyle-CssClass="ItemNotificacionCentrado">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEditarGrdPersonas" runat="server" CommandArgument='<%#Eval("ID_PERSONA")%>' OnClick="lnkEditarGrdPersonas_Click"> 
                                                <asp:Image ImageAlign="Middle" ID="imgEditarGrdPersonas" BorderWidth="0" runat="server" ImageUrl="~/App_Themes/Img/Edit.png"/>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ELIMINAR" ItemStyle-CssClass="ItemNotificacionCentrado">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEliminarGrdPersonas" runat="server" CommandArgument='<%#Eval("ID_ACTO_NOTIFICACION").ToString() + "@" + Eval("ID_PERSONA").ToString() +  "@" + Eval("USUARIO_NOTIFICAR").ToString()%>' OnClick="lnkEliminarGrdPersonas_Click"> 
                                                <asp:Image ImageAlign="Middle" ID="imgEliminarGrdPersonas" BorderWidth="0" runat="server" ImageUrl="~/images/Eliminar.png"/>
                                            </asp:LinkButton>
                                            <asp:Literal runat="server" ID="ltlEliminarGrdPersonas">-</asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VERIFICADO" ItemStyle-CssClass="ItemNotificacionCentrado">
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkVerificadoGrdPersonas" OnCheckedChanged="chkVerificadoGrdPersonas_CheckedChanged" AutoPostBack="true" EnableTheming="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellButtonModal">
                            <asp:HiddenField runat="server" ID="hdfActoIDConfigurarPersonas" />
                            <asp:HiddenField runat="server" ID="hdfEstadoActoIDConfigurarPersonas" />
                            <asp:HiddenField runat="server" ID="hdfTipoNotificacionIDConfigurarPersonas" />
                            <asp:Button ID="cmdModalConfigurarPersonasCancelar" runat="server" ClientIDMode="Static" Text="Cerrar" CssClass="boton" CausesValidation="false" OnClick="cmdModalConfigurarPersonasCancelar_Click" />                            
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdAdicionarConfigurarPersonas" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="cmdModalConfigurarPersonasCancelar" EventName="Click" />                
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="uppModalConfigurarPersonas" runat="server" AssociatedUpdatePanelID="upnlModalConfigurarPersonas">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgUpdateProgresModalConfigurarPersonas" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>


    <input type="button" runat="server" id="cmdAgregarEditarUsuarioHide" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeModalAgregarEditarUsuario" runat="server" PopupControlID="dvModalAgregarEditarUsuario" TargetControlID="cmdAgregarEditarUsuarioHide" BehaviorID="mpeModalAgregarEditarUsuarios" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <div id="dvModalAgregarEditarUsuario" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalFormularioNot">        
        <asp:UpdatePanel runat="server" ID="upnlModalAgregarEditarUsuario" UpdateMode="Conditional">
            <ContentTemplate>                
                <div class="TableResultadoModalNot">
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoTituloModalNot">
                            <asp:Literal runat="server" ID="ltlTituloAgregarEditarUsuario"></asp:Literal>
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoModalNot" style="padding-left: 20px !important; padding-right: 20px !important; padding-bottom: 20px !important;">
                            <div class="TableFormularioNot">
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlActoAgregarEditarUsuario">Acto Administrativo:</label>
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlActoAgregarEditarUsuario"></asp:Literal>
                                    </div>
                                </div>                                
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlFechaActoAgregarEditarUsuario">Fecha Acto Administrativo:</label>
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlFechaActoAgregarEditarUsuario"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="cboTipoIdentificacionAgregarEditarUsuario">Tipo Identificación:</label>
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:DropDownList runat="server" ID="cboTipoIdentificacionAgregarEditarUsuario"></asp:DropDownList>                                        
                                        <asp:RequiredFieldValidator ID="rfvTipoIdentificacionAgregarEditarUsuario" runat="server" ControlToValidate="cboTipoIdentificacionAgregarEditarUsuario" Display="Dynamic" ErrorMessage="Debe seleccionar el tipo de identificación" ValidationGroup="AgregarEditarUsuario" InitialValue="-1">*</asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvTipoIdentificacionAgregarEditarUsuarioValidar" runat="server" ControlToValidate="cboTipoIdentificacionAgregarEditarUsuario" Display="Dynamic" ErrorMessage="Debe seleccionar el tipo de identificación" ValidationGroup="ValidarUsuario" InitialValue="-1">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="txtNroIdentificacionAgregarEditarUsuario">Número Identificación:</label>
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:TextBox runat="server" ID="txtNroIdentificacionAgregarEditarUsuario" ClientIDMode="Static" MaxLength="11"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNroIdentificacionAgregarEditarUsuario" runat="server" ControlToValidate="txtNroIdentificacionAgregarEditarUsuario" Display="Dynamic" ValidationGroup="AgregarEditarUsuario" ErrorMessage="Debe ingresar el número de identificación">*</asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvNroIdentificacionAgregarEditarUsuarioValidar" runat="server" ControlToValidate="txtNroIdentificacionAgregarEditarUsuario" Display="Dynamic" ValidationGroup="ValidarUsuario" ErrorMessage="Debe ingresar el número de identificación">*</asp:RequiredFieldValidator>
                                        <asp:CustomValidator runat="server" ID="cvValidarUsuario" ValidationGroup ="ValidarUsuario" OnServerValidate="cvValidarUsuario_ServerValidate">*</asp:CustomValidator>
                                        <asp:Button runat="server" ID="cmdValidarUsuarioAgregarEditarUsuario" ClientIDMode="Static" Text="Consultar" ValidationGroup="ValidarUsuario" OnClick="cmdValidarUsuarioAgregarEditarUsuario_Click" />
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlUsuario">Usuario:</label>
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlUsuarioAgregarEditarUsuario"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="cboTipoNotificacionAgregarEditarUsuario">Tipo Notificación:</label>
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:DropDownList runat="server" ID="cboTipoNotificacionAgregarEditarUsuario" ClientIDMode="Static" OnSelectedIndexChanged="cboTipoNotificacionAgregarEditarUsuario_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvTipoNotificacionAgregarEditarUsuario" runat="server" ControlToValidate="cboTipoNotificacionAgregarEditarUsuario" ErrorMessage="Seleccione el tipo de notificación" ValidationGroup="AgregarEditarUsuario" InitialValue="-1">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="cboFlujoTipoNotificacionAgregarEditarUsuario">Flujo Tipo Notificación:</label>
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:DropDownList runat="server" ID="cboFlujoTipoNotificacionAgregarEditarUsuario" ClientIDMode="Static" OnSelectedIndexChanged="cboFlujoTipoNotificacionAgregarEditarUsuario_SelectedIndexChanged"  AutoPostBack="true"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvFlujoTipoNotificacionAgregarEditarUsuario" runat="server" ControlToValidate="cboFlujoTipoNotificacionAgregarEditarUsuario" ErrorMessage="Seleccione el flujo de notificación" ValidationGroup="AgregarEditarUsuario" InitialValue="-1">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="cboEstadoFlujoAgregarEditarUsuario">Estado Flujo:</label>
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:DropDownList runat="server" ID="cboEstadoFlujoAgregarEditarUsuario"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvEstadoFlujoAgregarEditarUsuario" runat="server" ControlToValidate="cboEstadoFlujoAgregarEditarUsuario" ErrorMessage="Seleccione el estado inicial" ValidationGroup="AgregarEditarUsuario" InitialValue="-1">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="txtReferenciaRecepcion">Fecha de Estado:</label>
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:TextBox ID="txtFechaEstadoAgregarEditarUsuario" runat="server" ClientIDMode="Static"></asp:TextBox>
                                        <cc1:CalendarExtender ID="calFechaEstadoAgregarEditarUsuario" OnClientShown="mostrarCalendarioAgregarEditarPersona" runat="server" Format="dd/MM/yyyy HH:mm" TargetControlID="txtFechaEstadoAgregarEditarUsuario"/>
                                        <cc1:MaskedEditExtender ID="mskFechaEstadoAgregarEditarUsuario" Mask="99/99/9999 99:99" runat="server" MaskType="DateTime" AcceptAMPM="False" UserDateFormat="DayMonthYear" UserTimeFormat="None" TargetControlID="txtFechaEstadoAgregarEditarUsuario" Enabled="False"></cc1:MaskedEditExtender>
                                        <asp:RequiredFieldValidator ID="rfvFechaEstadoAgregarEditarUsuario" runat="server" ControlToValidate="txtFechaEstadoAgregarEditarUsuario" ErrorMessage="Ingrese la fecha del estado." Text="*" ValidationGroup="AgregarEditarUsuario" InitialValue=""></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rexFechaEstadoAgregarEditarUsuario" runat="server" ControlToValidate="txtFechaEstadoAgregarEditarUsuario" ErrorMessage="Formato de la fecha del estado no es valido." ValidationExpression="^\d{2}\/\d{2}\/\d{4} \d{2}\:\d{2}" Width="3px" Text="*" ValidationGroup="AgregarEditarUsuario"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>                    
                    <div class="RowResultadoModalNot">
                        <div class="CellButtonModal" style="padding-top: 50px !important; padding-bottom: 30px !important;">
                            <asp:HiddenField runat="server" ID="hdfAccionRealizarAgregarEditarUsuario" />
                            <asp:HiddenField runat="server" ID="hdfActoIdAgregarEditarUsuario" />
                            <asp:HiddenField runat="server" ID="hdfEstadoActoIDAgregarEditarUsuario" />
                            <asp:HiddenField runat="server" ID="hdfTipoNotificacionIDAgregarEditarUsuario" />
                            <asp:HiddenField runat="server" ID="hdfPersonaIDAgregarEditarUsuario" />
                            <asp:HiddenField runat="server" ID="hdfNumeroIdentificacionAgregarEditarUsuario" />
                            <asp:Button ID="cmdAgregarEditarUsuarioAdicionar" runat="server" ClientIDMode="Static" Text="Guardar" CssClass="boton" ValidationGroup="AgregarEditarUsuario" OnClick="cmdAgregarEditarUsuarioAdicionar_Click" />
                            <asp:Button ID="cmdAgregarEditarUsuarioCerrar" ClientIDMode="Static" runat="server" Text="Cancelar" CssClass="boton" CausesValidation="false" OnClick="cmdAgregarEditarUsuarioCerrar_Click"/>
                            <asp:ValidationSummary runat="server" ID="valAgregarEditarUsuario" ValidationGroup="AgregarEditarUsuario" ShowSummary="false" ShowMessageBox="true" />
                            <asp:ValidationSummary runat="server" ID="valValidarUsuario" ValidationGroup="ValidarUsuario" ShowSummary="false" ShowMessageBox="true" />                            
                            <asp:CustomValidator runat="server" ID="cvAgregarEditarUsuario" ValidationGroup ="AgregarEditarUsuario" OnServerValidate="cvAgregarEditarUsuario_ServerValidate">&nbsp;</asp:CustomValidator>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdAgregarEditarUsuarioAdicionar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="cmdAgregarEditarUsuarioCerrar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="cmdValidarUsuarioAgregarEditarUsuario" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="cboTipoNotificacionAgregarEditarUsuario" />
                <asp:AsyncPostBackTrigger ControlID="cboFlujoTipoNotificacionAgregarEditarUsuario" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="uppModalAgregarEditarUsuario" runat="server" AssociatedUpdatePanelID="upnlModalAgregarEditarUsuario">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgUpdateProgresModalAgregarEditarUsuario" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>


    <input type="button" runat="server" id="cmdVerDocumentoActoAdministrativoHide" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeVerDocumentoActoAdministrativo" runat="server" PopupControlID="dvVerDocumentoActoAdministrativo" TargetControlID="cmdVerDocumentoActoAdministrativoHide" BehaviorID="mpeVerDocumentoActoAdministrativo" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <div id="dvVerDocumentoActoAdministrativo" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalFormularioNot">
        <asp:UpdatePanel runat="server" ID="upnlVerDocumentoActoAdministrativo" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="TableResultadoModalNot">
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoTituloModalNot">
                            DOCUMENTOS ACTO ADMINISTRATIVO
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoModalNot">
                            <div class="TableFormularioNot">
                                <div class="RowResultadoModalNot">
                                    <div class="CellResultadoModalNot">
                                        <asp:GridView ID="grdDocumentosActoAdministrativoVer" runat="server" Width="100%" 
                                            AutoGenerateColumns="False" AllowPaging="false" 
                                            SkinID="GrillaNotificaciones" 
                                            EmptyDataText="No se encontro información de documentos relacionados al acto administrativo" 
                                            ShowHeaderWhenEmpty="true">
                                            <Columns>
                                                    <asp:TemplateField HeaderText="DOCUMENTO" ItemStyle-CssClass="ItemNotificacion">
                                                        <ItemTemplate>
                                                            <asp:literal ID="ltlDocumento" runat="server" Text='<%# Eval("NOMBRE_DOCUMENTO")  %>'></asp:literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="VER" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="imgDescargarDocumentoActoAdmnistrativoVer" BorderWidth="0" ImageUrl="~/images/documento.png" CommandArgument='<%#Eval("RUTA_DOCUMENTO") %>' OnClick="imgDescargarDocumentoActoAdmnistrativoVer_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellButtonModal">
                            <asp:Button ID="cmdCerrarVerDocumentoActoAdministrativo" ClientIDMode="Static" runat="server" Text="Cerrar" CssClass="boton" CausesValidation="false" OnClick="cmdCerrarVerDocumentoActoAdministrativo_Click"/>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdCerrarVerDocumentoActoAdministrativo" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upnlVerDocumentoActoAdministrativo">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgUpdateProgressVerDocumentoActoAdministrativo" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>


    <input type="button" runat="server" id="cmdEliminarPersonaHide" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeModalEliminarPersona" runat="server" PopupControlID="dvModalEliminarPersona" TargetControlID="cmdEliminarPersonaHide" BehaviorID="mpeModalEliminarPersonas" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <div id="dvModalEliminarPersona" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalFormularioNot">
        <asp:UpdatePanel runat="server" ID="upnlModalEliminarPersona" UpdateMode="Conditional">
            <ContentTemplate>
                
                <div class="TableResultadoModalNot">
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoTituloModalNot">
                            ELIMINAR PERSONA
                        </div>
                    </div>                            
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoModalNot">
                            <div class="TableFormularioNot">
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <div class="TableMensajesNot">
                                            <div class="RowMensajesNot">
                                                <div class="CellMensajesNot"><asp:Image runat="server" ID="imgAdvertenciaEliminarPersona" ImageUrl="~/images/advertencia.png" /></div>
                                                <div class="CellMensajesNot">Se encuentra por eliminar la persona <asp:Literal runat="server" ID="ltlUsuarioEliminarPersona"></asp:Literal>. Si se encuentra seguro de eliminarlo ingrese el motivo de eliminación haga clic en el botón "Eliminar".</div>
                                            </div>                                            
                                        </div>
                                    </div>
                                </div> 
                                <div class="RowFormularioNot">
                                    <div class="CellButtonModal">
                                        <br /><br />                                                                                
                                        <b>Motivo de Eliminación:</b>
                                        <asp:TextBox runat="server" ID="txtMotivoEliminacionEliminarPersona" TextMode="MultiLine" Rows="4" Columns="40"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvMotivoEliminacionEliminarPersona" ControlToValidate="txtMotivoEliminacionEliminarPersona" ErrorMessage="Debe indicar el motivo de eliminación de la persona" ValidationGroup="EliminarPersona">*</asp:RequiredFieldValidator>
                                        <br /><br />
                                    </div>
                                </div>                               
                            </div>
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellButtonModal">
                            <asp:HiddenField runat="server" ID="hdfActoIdEliminarPersona" />
                            <asp:HiddenField runat="server" ID="hdfPersonaIDEliminarPersona" />
                            <asp:HiddenField runat="server" ID="hdfEstadoActoIDEliminarPersona" />
                            <asp:HiddenField runat="server" ID="hdfTipoNotificacionIDEliminarPersona" />
                            <asp:Button ID="cmdModalEliminarPersonaEliminar" runat="server" ClientIDMode="Static" Text="Eliminar" CssClass="boton" CausesValidation="true" ValidationGroup="EliminarPersona" OnClick="cmdModalEliminarPersonaEliminar_Click" />
                            <asp:Button ID="cmdModalEliminarPersonaCancelar" runat="server" ClientIDMode="Static" Text="Cancelar" CssClass="boton" CausesValidation="false" OnClick="cmdModalEliminarPersonaCancelar_Click" />
                            <asp:ValidationSummary runat="server" ID="valEliminarPersona" ValidationGroup="EliminarPersona" ShowSummary="false" ShowMessageBox="true" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdModalEliminarPersonaEliminar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="cmdModalEliminarPersonaCancelar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="uppModalEliminarPersona" runat="server" AssociatedUpdatePanelID="upnlModalEliminarPersona">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgUpdateProgresModalEliminarPersona" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

</asp:Content>
