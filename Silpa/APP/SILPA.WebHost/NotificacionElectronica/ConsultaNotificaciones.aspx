<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ConsultaNotificaciones.aspx.cs" Inherits="NotificacionElectronica_ConsultaNotificaciones" Title="Proceso de Notificación" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
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
        var radicando = false;

        function validarEnvios(oSrc, args) {
            args.IsValid = ($("#chkEnviarDireccion").is(':checked') || $("#chkEnviarCorreo").is(':checked'));
        }

        function mostrarCalendarioAvanzar(sender, args) {
            sender._popupBehavior._element.style.zIndex = 100004;            
            if (navigator.userAgent.toLowerCase().indexOf('chrome') > 1) {
                sender._popupBehavior._element.style.marginTop = $(window).scrollTop() + "px";
            }
        }

        function mostrarCalendarioEditar(sender, args) {
            sender._popupBehavior._element.style.zIndex = 100004;
            if (navigator.userAgent.toLowerCase().indexOf('chrome') > 1) {
                sender._popupBehavior._element.style.marginTop = $(window).scrollTop() + "px";
            }
        }

        function VerNotificacion() {
            window.open("ConsultarEstadosNotificacion.aspx", "Notificacion", "resizable=yes,width=1200,height=600");
        }

        function RecargarAcordion(hrefPagina) {

            var blnRecargar = false;
            
            blnRecargar = hrefPag.indexOf("grdEstadosNotificacion") == -1 && hrefPag.indexOf("Cerrar") == -1;            
            blnRecargar = blnRecargar && (hrefPag.indexOf("Page$") == -1 || (hrefPag.indexOf("Page$") != -1 && hrefPag.indexOf("grdNotificaciones") != -1));
            blnRecargar = blnRecargar && hrefPag.indexOf("cmdCancelarEditar") == -1;
            blnRecargar = blnRecargar || hrefPag.indexOf("imgEliminar") != -1;
            blnRecargar = blnRecargar && hrefPag.indexOf("chkEnviarDireccionEditar") == -1;
            blnRecargar = blnRecargar && hrefPag.indexOf("chkEnviarCorreoEditar") == -1;
            blnRecargar = blnRecargar && hrefPag.indexOf("chkAdjuntarActoAdministrativoEditar") == -1;

            return blnRecargar;
        }

        function MostrarProgressDropDownAdjunto() {
            $get('<%= this.uppAvanzarEstado.ClientID %>').style.display = 'block';
            progressVisible = true;
        }

        function OcultarProgressDropDownAdjunto() {
            if (progressVisible) {
                $get('<%= uppAvanzarEstado.ClientID %>').style.display = 'none';
            }
        }

        function ErrorArchivoAdjunto() {
            OcultarProgressDropDownAdjunto();
            alert("El tamaño del archivo sobrepasa el máximo permitido");
        }

        function MostrarProgressDropDownAdjuntoEditar() {
            $get('<%= this.uppEditarEstado.ClientID %>').style.display = 'block';
            progressVisible = true;
        }

        function OcultarProgressDropDownAdjuntoEditar() {
            if (progressVisible) {
                $get('<%= uppEditarEstado.ClientID %>').style.display = 'none';
            }
        }

        function ErrorArchivoAdjuntoEditar() {
            OcultarProgressDropDownAdjuntoEditar();
            alert("El tamaño del archivo sobrepasa el máximo permitido");
        }


        function MostrarProgressDropDownAdicional() {
            $get('<%= this.uppAvanzarEstado.ClientID %>').style.display = 'block';
            progressVisible = true;
        }

        function OcultarProgressDropDownAdicional() {
            if (progressVisible) {
                $get('<%= uppAvanzarEstado.ClientID %>').style.display = 'none';
            }
        }

        function ErrorArchivoAdicional() {
            OcultarProgressDropDownAdicional();
            alert("El tamaño del archivo sobrepasa el máximo permitido");
        }

        function MostrarProgressDropDownAdicionalEditar() {
            $get('<%= this.uppEditarEstado.ClientID %>').style.display = 'block';
            progressVisible = true;
        }

        function OcultarProgressDropDownAdicionalEditar() {
            if (progressVisible) {
                $get('<%= uppEditarEstado.ClientID %>').style.display = 'none';
            }
        }

        function ErrorArchivoAdicionalEditar() {
            OcultarProgressDropDownAdicionalEditar();
            alert("El tamaño del archivo sobrepasa el máximo permitido");
        }


        $(function () {
            $("[id*=accordionNotificacion]").accordion({
                collapsible: true,
                heightStyle: "content",
                active: false
            });

            $('a').click(function (e) {
                hrefPag = $(this).attr("href");
            });            

            $('input').click(function (e) {
                hrefPag = $(this).attr("id");
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            
            prm.add_endRequest(function () {
                $('a').click(function (e) {
                    hrefPag = $(this).attr("href");
                });

                $('input').click(function (e) {                    
                    hrefPag = $(this).attr("id");
                });

                if (RecargarAcordion(hrefPag)) {
                    $("[id*=accordionNotificacion]").accordion({
                        collapsible: true,
                        heightStyle: "content",
                        active: false
                    });
                }

                $("#cmdAvanzar").click(function (e) {
                    radicando = true;
                });

                //Verificar si el avance de estado se encuentra abierto
                if ($("#dvAvanzarEstado").is(':visible') && radicando) {
                    alert("El proceso de avance de estado tomo más tiempo del permitido por el navegador y se corto la comunicación con el servidor. Haga clic en el botón de Cancelar, espere un momento y verifique si la radicación se realizo correctamente.");
                }                
                radicando = false;
                
            });
        });
    </script>

    <asp:ScriptManager ID="scmManager" runat="server"></asp:ScriptManager>

    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="PUBLICIDAD DE ACTOS ADMINISTRATIVOS" SkinID="titulo_principal_blanco"></asp:Label>
        &nbsp;
        <a href="#" id="hrfCerrarVentana" runat="server" onclick="window.close();return false;" class="Salir">Salir [X]</a>
    </div>

    <div class="contact_form" id="divConsultaCertificado" runat="server">
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
                                        <label for="cboMarcaVehiculo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Número VITAL:</label>
                                        <asp:TextBox runat="server" ID="txtNumeroVital" ClientIDMode="Static"></asp:TextBox>                                        
                                    </div>
                                    <div class="CellBuscarNot">
                                        <label for="cboMarcaVehiculo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Número de Expediente:</label>
                                        <asp:TextBox runat="server" ID="txtExpediente" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="RowBuscarNot">
                                    <div class="CellBuscarNot">
                                        <label for="txtIdentificacionUsuario" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Identificación del Usuario:</label>
                                        <asp:TextBox runat="server" ID="txtIdentificacionUsuario" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <div class="CellBuscarNot">
                                        <label for="txtUsuarioNotificar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Usuario a Notificar:</label>
                                        <asp:TextBox runat="server" ID="txtUsuarioNotificar" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="RowBuscarNot">
                                    <div class="CellBuscarNot">
                                        <label for="txtNumeroActo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Número Acto Administrativo:</label>
                                        <asp:TextBox runat="server" ID="txtNumeroActo" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <div class="CellBuscarNot">
                                        <label for="cboMarcaVehiculo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Tipo de Acto Administrativo:</label>
                                        <asp:DropDownList ID="cboTipoActo" runat="server" SkinID="lista_desplegable2" />
                                    </div>
                                </div>
                                <div class="RowBuscarNot">
                                    <div class="CellBuscarNot">
                                        <label for="txtNumeroActo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Dias vencimiento:</label>
                                        <asp:TextBox runat="server" ID="txtDiasVencimientoInicial" ClientIDMode="Static" SkinID="texto_numeros_rango" MaxLength="5"></asp:TextBox>&nbsp;
                                        <asp:RegularExpressionValidator runat="server" ID="rexDiasVencimientoInicial" ControlToValidate="txtDiasVencimientoInicial" ValidationExpression="^(\d|-)?(\d+$)" ErrorMessage="Debe ingresar un valor válido en el rango inicial de días de vencimiento." Text="*" ValidationGroup="NotBuscar"></asp:RegularExpressionValidator>
                                        hasta&nbsp;&nbsp;&nbsp;<asp:TextBox runat="server" ID="txtDiasVencimientoFinal" ClientIDMode="Static" SkinID="texto_numeros_rango" MaxLength="5"></asp:TextBox>
                                        <asp:RegularExpressionValidator runat="server" ID="rexDiasVencimientoFinal" ControlToValidate="txtDiasVencimientoFinal" ValidationExpression="^(\d|-)?(\d+$)" ErrorMessage="Debe ingresar un valor válido en el rango final de días de vencimiento." Text="*" ValidationGroup="NotBuscar"></asp:RegularExpressionValidator>
                                    </div>                                    
                                </div>
                                <div class="RowBuscarNot">
                                    <div class="CellBuscarNot">
                                        <label for="cboMarcaVehiculo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Flujo de Notificación:</label>
                                        <asp:DropDownList ID="cboFlujoNotificacion" runat="server" SkinID="lista_desplegableupdate"  OnSelectedIndexChanged="cboFlujoNotificacion_SelectedIndexChanged" AutoPostBack="true" />&nbsp;&nbsp;
                                    </div>
                                    <div class="CellBuscarNot">
                                        <label for="cboMarcaVehiculo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Estado Notificación:</label>
                                        <asp:DropDownList ID="cboEstadoNotificacion" runat="server" SkinID="lista_desplegable2" />&nbsp;&nbsp;
                                        Estado Actual&nbsp;
                                        <asp:CheckBox runat="server" ID="chkEstadoActual" Checked="true" EnableTheming="false" />
                                    </div>                                    
                                </div>
                                <div class="RowBuscarNot">
                                    <div class="CellBuscarNot">
                                        <label for="txtFechaDesde" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Fecha Acto Administrativo Desde:</label>
                                        <asp:TextBox ID="txtFechaDesde" runat="server" ClientIDMode="Static"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFechaDesde" runat="server" ControlToValidate="txtFechaDesde" ErrorMessage="Ingrese la Fecha Desde la cual desea buscar." Text="*" ValidationGroup="NotBuscar" InitialValue=""></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rexFechaDesde" runat="server" ControlToValidate="txtFechaDesde" ErrorMessage="Formato de fecha no valido para la Fecha Desde." ValidationExpression="^\d{2}\/\d{2}\/\d{4}" Width="3px" Text="*" ValidationGroup="NotBuscar"></asp:RegularExpressionValidator>
                                        <cc1:CalendarExtender ID="calFechaDesde" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaDesde"/>
                                    </div>
                                    <div class="CellBuscarNot">
                                        <label for="txtFechaHasta" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Fecha Acto Administrativo Hasta:</label>
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
                            <asp:HiddenField runat="server" ID="hdfDiasVencimientoInicial" />
                            <asp:HiddenField runat="server" ID="hdfDiasVencimientoFinal" />
                            <asp:HiddenField runat="server" ID="hdfFlujoNotificacion" />
                            <asp:HiddenField runat="server" ID="hdfEstadoNotificacionDescripcion" />
                            <asp:HiddenField runat="server" ID="hdfEstadoNotificacion" />
                            <asp:HiddenField runat="server" ID="hdfFechaDesde" />
                            <asp:HiddenField runat="server" ID="hdfFechaHasta" />
                            <asp:HiddenField runat="server" ID="hdfEstadoActual" />
                            <asp:Button runat="server" ID="cmdBuscar" ValidationGroup="NotBuscar" Text="Buscar" ClientIDMode="Static" SkinID="boton" Width="100px" OnClick="cmdBuscar_Click"/>
                            <asp:ValidationSummary ID="valNotBuscar" runat="server" ValidationGroup="NotBuscar" ShowMessageBox="true" ShowSummary="false" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cboFlujoNotificacion" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="cmdBuscar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel runat="server" ID="upnlMensaje" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="contact_form" id="divMensaje" runat="server" visible="false">  
                <div class="Table">
                    <div class="Row">
                        <div class="CellMensaje">
                            <asp:Label runat="server" ID="lblMensaje" SkinID="etiqueta_roja_error"></asp:Label>
                        </div>
                    </div>
                </div>            
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <div class="contact_form" id="divNotificaciones" runat="server">
        <asp:UpdatePanel runat="server" ID="upnlConsultaNotificaciones" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlNotificaciones" runat="server" ScrollBars="Horizontal" Width="100%">
                    <div class="TableReporteNot">
                        <div class="RowReporteNot">
                            <div class="CellReporteNot">
                                <asp:GridView ID="grdNotificaciones" runat="server" Width="100%" 
                                    DataKeyNames="ID_ACTO_NOTIFICACION, ID_PERSONA, ID_ESTADO_ACTUAL, ID_AUTORIDAD" 
                                    AutoGenerateColumns="False" AllowPaging="true" PageSize="8" 
                                    SkinID="GrillaNotificaciones" ShowHeaderWhenEmpty="true" 
                                    CellPadding="4" CellSpacing="2" BorderWidth="1px" BorderColor="#000000" BorderStyle="Solid" 
                                    OnPageIndexChanging="grdNotificaciones_PageIndexChanging" 
                                    EmptyDataText="No se encontro información con los parámetros de búsqueda especificados."
                                    OnRowDataBound="grdNotificaciones_RowDataBound">
                                    <HeaderStyle Font-Size="9pt" />
                                    <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                    <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                    <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                    <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                    <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="NÚMERO VITAL" ItemStyle-CssClass="ItemNotificacion">
                                            <ItemTemplate>
                                                <asp:literal ID="ltlNúmeroVital" runat="server" Text='<%# Eval("NUM_VITAL")  %>'></asp:literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NÚMERO EXPEDIENTE" ItemStyle-CssClass="ItemNotificacion">
                                            <ItemTemplate>
                                                <asp:literal ID="ltlExpediente" runat="server" Text='<%# Eval("EXPEDIENTE")  %>'></asp:literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TIPO ACTO ADMINISTRATIVO" ItemStyle-CssClass="ItemNotificacion">
                                            <ItemTemplate>
                                                <asp:literal ID="ltlTipoActo" runat="server" Text='<%# Eval("TIPO_ACTO_ADMINISTRATIVO")  %>'></asp:literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FECHA ACTO ADMINISTRATIVO" ItemStyle-CssClass="ItemNotificacionCentrado">
                                            <ItemTemplate>
                                                <asp:literal ID="ltlFechaActo" runat="server" Text='<%# Eval("FECHA_ACTO")  %>'></asp:literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NÚMERO ACTO ADMINISTRATIVO" ItemStyle-CssClass="ItemNotificacionCentrado">
                                            <ItemTemplate>
                                                <asp:literal ID="ltlNumeroActo" runat="server" Text='<%# Eval("NOT_NUMERO_ACTO_ADMINISTRATIVO")  %>'></asp:literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DOCUMENTO ACTO ADMINISTRATIVO" ItemStyle-CssClass="ItemNotificacionCentrado">
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgDescargarDocumento" BorderWidth="0" ImageUrl="~/images/documento.png" CommandArgument='<%#Eval("NOT_RUTA_DOCUMENTO") %>' OnClick="imgDescargarDocumento_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AUTORIDAD AMBIENTAL" ItemStyle-CssClass="ItemNotificacionCentrado">
                                            <ItemTemplate>
                                                <asp:literal ID="ltlAutoridad" runat="server" Text='<%# Eval("NOMBRE_AUTORIDAD")  %>'></asp:literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="INFORMACIÓN NOTIFICACIÓN">
                                            <ItemTemplate>
                                                <div id="accordionNotificacion">
                                                    <div class="headerAccordionNotificacion">
                                                        <div class="TableNotificacionNotInt">
                                                            <div class="RowNotificacionNotInt">                                                                                                                                      
                                                                <div class="CellNotificacionNotIntTitulo">
                                                                    Identificación:
                                                                </div>
                                                                <div class="CellNotificacionNotIntTexto">
                                                                    <asp:literal ID="ltlIdentificacionUsuario" runat="server" Text='<%# Eval("IDENTIFICACION_USUARIO_NOTIFICAR")  %>'></asp:literal>
                                                                </div>
                                                                <div class="CellNotificacionNotIntTitulo">
                                                                    Estado Actual:
                                                                </div>
                                                                <div class="CellNotificacionNotIntTexto">
                                                                    <asp:literal ID="ltlEstadoActual" runat="server" Text='<%# Eval("ESTADO_ACTUAL")  %>'></asp:literal>
                                                                </div>
                                                                <div class="CellNotificacionNotIntTitulo">
                                                                    Flujo de Notificación:
                                                                </div>
                                                                <div class="CellNotificacionNotIntTexto">
                                                                    <asp:literal ID="ltlFlujoNotificacion" runat="server" Text='<%# Eval("FLUJO")  %>'></asp:literal>
                                                                </div>                                                                                                                                 
                                                            </div>
                                                            <div class="RowNotificacionNotInt">                                                                     
                                                                <div class="CellNotificacionNotIntTitulo">
                                                                    Usuario Notificar:
                                                                </div>
                                                                <div class="CellNotificacionNotIntTexto">
                                                                    <asp:literal ID="ltlNombreUsuario" runat="server" Text='<%# Eval("USUARIO_NOTIFICAR")  %>'></asp:literal>
                                                                </div> 
                                                                <div class="CellNotificacionNotIntTitulo">
                                                                    Fecha Estado Actual:
                                                                </div>
                                                                <div class="CellNotificacionNotIntTexto">
                                                                    <asp:literal ID="ltlFechaEstadoActual" runat="server" Text='<%# Eval("FECHA_ESTADO_ACTUAL")  %>'></asp:literal>
                                                                </div>
                                                                <div class="CellNotificacionNotIntTitulo">
                                                                    Días para Vencimiento:
                                                                </div>
                                                                <div class="CellNotificacionNotIntTexto">
                                                                    <asp:literal ID="ltlDiasVencimiento" runat="server" Text='<%# Eval("DIAS_PARA_VENCIMIENTO")  %>'></asp:literal>
                                                                </div> 
                                                            </div>
                                                        </div>                                                   
                                                    </div>
                                                    <div class="bodyAccordeonNotificacion">
                                                        <asp:UpdatePanel runat="server" ID="upnlEstadosNotificaciones" UpdateMode="Conditional" class="UpdatePanelNot">
                                                            <ContentTemplate>
                                                                <asp:GridView runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="5" ID="grdEstadosNotificacion" 
                                                                    SkinID="GrillaNotificaciones" EmptyDataText="No se encontro estados para el acto administrativo." ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdEstadosNotificacion_PageIndexChanging"
                                                                    DataKeyNames="ID_NOT_ACTO, ID_PERSONA" Width="100%">
                                                                    <HeaderStyle Font-Size="9pt" />
                                                                    <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                                                    <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                                                    <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                                    <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                                    <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="ESTADO" ItemStyle-CssClass="ItemNotificacion">
                                                                            <ItemTemplate>
                                                                                <asp:literal ID="ltlEstado" runat="server" Text='<%# Eval("ESTADO")  %>'></asp:literal>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="FECHA" ItemStyle-CssClass="ItemNotificacion">
                                                                            <ItemTemplate>
                                                                                <asp:literal ID="ltlFecha" runat="server" Text='<%# Eval("FECHA_ESTADO")  %>'></asp:literal>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="DÍAS VENCIMIENTO" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                                            <ItemTemplate>
                                                                                <asp:literal ID="ltlDiasVencmiento" runat="server" Text='<%# Eval("DIAS_PARA_VENCIMIENTO")  %>'></asp:literal>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>                                                                        
                                                                        <asp:TemplateField HeaderText="DOCUMENTO" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton runat="server" ID="imgDocumentoPlantilla" ImageUrl="~/images/documento.png" BorderWidth="0" CommandArgument='<%#Eval("RUTA_DOCUMENTO_PLANTILLA") %>' Visible='<%# ( string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO_PLANTILLA").ToString() ) || Eval("RUTA_DOCUMENTO_PLANTILLA").ToString().EndsWith("/")  || Eval("RUTA_DOCUMENTO_PLANTILLA").ToString().EndsWith("\\") ? false : true ) %>' OnClick="imgDocumentoPlantilla_Click" />
                                                                                <asp:literal ID="ltlDocumentoPlantilla" runat="server" Text='-' Visible='<%# ( !Eval("RUTA_DOCUMENTO_PLANTILLA").ToString().EndsWith("/")  && !Eval("RUTA_DOCUMENTO_PLANTILLA").ToString().EndsWith("\\") && !string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO_PLANTILLA").ToString() ) ? false : true ) %>'></asp:literal>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="DOCUMENTO ADICIONAL" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton runat="server" ID="imgDocumentoAdicional" ImageUrl="~/images/documento.png" BorderWidth="0" CommandArgument='<%#Eval("RUTA_DOCUMENTO_ADICIONAL") %>' Visible='<%# ( string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO_ADICIONAL").ToString() ) || Eval("RUTA_DOCUMENTO_ADICIONAL").ToString().EndsWith("/")  || Eval("RUTA_DOCUMENTO_ADICIONAL").ToString().EndsWith("\\") ? false : true ) %>' OnClick="imgDocumentoAdicional_Click"  />
                                                                                <asp:literal ID="ltlDocumentoAdicional" runat="server" Text='-' Visible='<%# ( !Eval("RUTA_DOCUMENTO_ADICIONAL").ToString().EndsWith("/")  && !Eval("RUTA_DOCUMENTO_ADICIONAL").ToString().EndsWith("\\") && !string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO_ADICIONAL").ToString() ) ? false : true ) %>'></asp:literal>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ADJUNTOS" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton runat="server" ID="imgAdjuntos" ImageUrl="~/images/adjunto.png" BorderWidth="0" CommandArgument='<%#Eval("ID_ESTADO_PERSONA_ACTO") %>' Visible='<%# ( string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO").ToString() ) || Eval("RUTA_DOCUMENTO").ToString().EndsWith("/")  || Eval("RUTA_DOCUMENTO").ToString().EndsWith("\\") ? false : true ) || (Eval("ADJUNTO_INCLUYE_ACTO") != System.DBNull.Value && Convert.ToBoolean(Eval("ADJUNTO_INCLUYE_ACTO"))) %>' OnClick="imgAdjuntos_Click" />
                                                                                <asp:literal ID="ltlAdjuntos" runat="server" Text='-' Visible='<%# ( !Eval("RUTA_DOCUMENTO").ToString().EndsWith("/")  && !Eval("RUTA_DOCUMENTO").ToString().EndsWith("\\") && !string.IsNullOrWhiteSpace( Eval("RUTA_DOCUMENTO").ToString() ) ? false : true ) && (Eval("ADJUNTO_INCLUYE_ACTO") == System.DBNull.Value || !Convert.ToBoolean(Eval("ADJUNTO_INCLUYE_ACTO"))) %>'></asp:literal>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="NÚMERO RADICADO" ItemStyle-CssClass="ItemNotificacion">
                                                                            <ItemTemplate>
                                                                                <asp:literal ID="ltlRadicado" runat="server" Text='<%# (!string.IsNullOrWhiteSpace(Eval("NUMERO_RADICADO").ToString()) ? Eval("NUMERO_RADICADO") : "-")  %>'></asp:literal>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="EDITAR" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton runat="server" ID="imgEditar" ImageUrl="~/App_Themes/Img/Edit.png" BorderWidth="0" Visible='<%# Convert.ToInt32(Eval("ESTADO_ACTUAL")) == 1 && Convert.ToBoolean(Eval("MODIFICABLE")) && string.IsNullOrWhiteSpace(Eval("NUMERO_RADICADO").ToString()) && string.IsNullOrWhiteSpace(Eval("RUTA_DOCUMENTO_PLANTILLA").ToString()) && !Convert.ToBoolean(Eval("SISTEMA_PDI")) %>' OnClick="imgEditar_Click"  CommandArgument='<%# Eval("ID_ESTADO_PERSONA_ACTO") %>' />
                                                                                <asp:literal ID="ltlEditar" runat="server" Text='-' Visible='<%# Convert.ToInt32(Eval("ESTADO_ACTUAL")) == 0 || !Convert.ToBoolean(Eval("MODIFICABLE")) || !string.IsNullOrWhiteSpace(Eval("NUMERO_RADICADO").ToString()) || !string.IsNullOrWhiteSpace(Eval("RUTA_DOCUMENTO_PLANTILLA").ToString()) || Convert.ToBoolean(Eval("SISTEMA_PDI")) %>'></asp:literal>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ELIMINAR" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton runat="server" ID="imgEliminar" BorderWidth="0" ImageUrl="~/images/Eliminar.png" Visible='<%# Convert.ToInt32(Eval("ESTADO_ACTUAL")) == 1 && Convert.ToBoolean(Eval("MODIFICABLE")) && Convert.ToBoolean(Eval("ESTADO_ACTUAL")) && string.IsNullOrWhiteSpace(Eval("NUMERO_RADICADO").ToString()) && !Convert.ToBoolean(Eval("SISTEMA_PDI")) && !Convert.ToBoolean(Eval("ESTADO_EJECUTORIA")) && !Convert.ToBoolean(Eval("ESTADO_FINAL_PUBLICIDAD")) && Eval("ID_ESTADO").ToString() != "18" %>' OnClientClick="return confirm('¿En realidad dese eliminar el estado?')" OnClick="imgEliminar_Click" CommandArgument='<%# Eval("ID_ESTADO_PERSONA_ACTO") %>' />
                                                                                <asp:literal ID="ltlEliminar" runat="server" Text='-' Visible='<%# Convert.ToInt32(Eval("ESTADO_ACTUAL")) == 0 ||  !Convert.ToBoolean(Eval("MODIFICABLE")) || !Convert.ToBoolean(Eval("ESTADO_ACTUAL")) && !string.IsNullOrWhiteSpace(Eval("NUMERO_RADICADO").ToString()) || Convert.ToBoolean(Eval("SISTEMA_PDI")) || Convert.ToBoolean(Eval("ESTADO_EJECUTORIA")) || Convert.ToBoolean(Eval("ESTADO_FINAL_PUBLICIDAD")) || Eval("ID_ESTADO").ToString() == "18" %>'></asp:literal>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="grdEstadosNotificacion" EventName="PageIndexChanging" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="AVANZAR FLUJO" ItemStyle-CssClass="ItemNotificacionCentradoTodo">
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgAvanzar" ImageUrl="~/images/avanzar.png" BorderWidth="0" Visible='<%# Convert.ToBoolean(Eval("TIENE_SIGUIENTE")) %>' ToolTip="Avanzar Flujo" OnClick="imgAvanzar_Click" CommandArgument='<%# Container.DisplayIndex %>' />
                                                <asp:literal ID="ltlAvanzar" runat="server" Text='-' Visible='<%# !Convert.ToBoolean(Eval("TIENE_SIGUIENTE")) %>'></asp:literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>            
        </asp:UpdatePanel>
        
        <input type="button" runat="server" id="cmdAvanzarEstadoHide" style="display:none;" />
        <cc1:ModalPopupExtender ID="mpeAvanzarEstado" runat="server" PopupControlID="dvAvanzarEstado" TargetControlID="cmdAvanzarEstadoHide" BehaviorID="mpeAvanzarEstados" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <div id="dvAvanzarEstado" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalFormularioNot">            
            <asp:UpdatePanel runat="server" ID="upnlAvanzarEstado" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="TableResultadoModalNot">
                        <div class="RowResultadoModalNot">
                            <div class="CellResultadoTituloModalNot">
                                AVANZAR ESTADO DE NOTIFICACIÓN
                            </div>
                        </div>
                        <div class="RowResultadoModalNot">
                            <div class="CellResultadoModalNot">
                                <div class="TableFormularioNot">
                                    <div class="RowFormularioNot">
                                        <div class="CellFormularioNot">
                                            <label for="ltlNumeroVital" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Número VITAL:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:Literal runat="server" ID="ltlNumeroVital"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot">
                                        <div class="CellFormularioNot">
                                            <label for="ltlNumeroExpediente" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Número Expediente:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:Literal runat="server" ID="ltlNumeroExpediente"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot">
                                        <div class="CellFormularioNot">
                                            <label for="ltlActoAdministrativo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Acto Administrativo:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:Literal runat="server" ID="ltlActoAdministrativo"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot">
                                        <div class="CellFormularioNot">
                                            <label for="ltlNumeroActo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Número Acto Administrativo:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:Literal runat="server" ID="ltlNumeroActo"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot" runat="server" id="dvIdentificacion">
                                        <div class="CellFormularioNot">
                                            <label for="ltlIdentificacion" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Identificación:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:Literal runat="server" ID="ltlIdentificacion"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot" runat="server" id="dvUsuario">
                                        <div class="CellFormularioNot">
                                            <label for="ltlUsuario" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Usuario:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:Literal runat="server" ID="ltlUsuario"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot">
                                        <div class="CellFormularioNot">
                                            <label for="ltlEstadoActual" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Estado Actual:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:Literal runat="server" ID="ltlEstadoActual"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot">
                                        <div class="CellFormularioNot">
                                            <label for="ltlFechaEstadoActual" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Fecha Estado Actual:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:Literal runat="server" ID="ltlFechaEstadoActual"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot">
                                        <div class="CellFormularioNot">
                                            <label for="cboEstado" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Nuevo Estado:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:DropDownList runat="server" ID="cboEstado" OnSelectedIndexChanged="cboEstado_SelectedIndexChanged" AutoPostBack="true" />
                                            <asp:RequiredFieldValidator ID="rfvEstado" runat="server" ControlToValidate="cboEstado" ErrorMessage="Seleccione el nuevo estado." Text="*" ValidationGroup="AvanceModal" InitialValue="-1"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot">
                                        <div class="CellFormularioNot">
                                            <label for="txtFechaEstadoNotificacion" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Fecha Nuevo Estado:</label>
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:TextBox ID="txtFechaEstadoNotificacion" runat="server" ClientIDMode="Static"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvFechaEstadoNotificacion" runat="server" ControlToValidate="txtFechaEstadoNotificacion" ErrorMessage="Ingrese la fecha del nuevo estado." Text="*" ValidationGroup="AvanceModal" InitialValue=""></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="rexFechaEstadoNotificacion" runat="server" ControlToValidate="txtFechaEstadoNotificacion" ErrorMessage="Formato de la fecha del nuevo estadono valido." ValidationExpression="^\d{2}\/\d{2}\/\d{4} \d{2}\:\d{2}" Width="3px" Text="*" ValidationGroup="AvanceModal"></asp:RegularExpressionValidator>
                                            <cc1:CalendarExtender ID="calFechaEstadoNotificacion" OnClientShown="mostrarCalendarioAvanzar" runat="server" Format="dd/MM/yyyy HH:mm" TargetControlID="txtFechaEstadoNotificacion"/>
                                            <cc1:MaskedEditExtender ID="mskFechaEstadoNotificacion" Mask="99/99/9999 99:99" runat="server" MaskType="DateTime" AcceptAMPM="False" UserDateFormat="DayMonthYear" UserTimeFormat="None" TargetControlID="txtFechaEstadoNotificacion" Enabled="True"></cc1:MaskedEditExtender>                                          
                                            <asp:CustomValidator runat="server" ID="cvFechaEstadoNotificacion" ValidationGroup="AvanceModal" Display="Dynamic" ErrorMessage="La fecha del estado no puede ser menor a la fecha del estado actual" OnServerValidate="cvFechaEstadoNotificacion_ServerValidate">*</asp:CustomValidator>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot">
                                        <div class="CellFormularioNot">
                                            <label for="txtObservacion" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Observación:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:TextBox runat="server" ID="txtObservacion" TextMode="MultiLine" Columns="30" Rows="5" style="resize: none;"></asp:TextBox>
                                        </div>
                                    </div>           

                                    <div class="RowFormularioNot" runat="server" id="dvAdjuntarActoAdministrativo">
                                        <div class="CellFormularioNot">
                                            <label for="chkAdjuntarActoAdministrativo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Entregar Acto Administrativo:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:CheckBox runat="server" ID="chkAdjuntarActoAdministrativo" OnCheckedChanged="chkAdjuntarActoAdministrativo_CheckedChanged" AutoPostBack="true" EnableTheming="false" />
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot" runat="server" id="dvAdjuntarConceptosActoAdministrativo">
                                        <div class="CellFormularioNot">
                                            <label for="chkAdjuntarConceptosActoAdministrativo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Entregar Concepto(s) Acto Administrativo:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:CheckBox runat="server" ID="chkAdjuntarConceptosActoAdministrativo" EnableTheming="false" />
                                        </div>
                                    </div>
                                                             
                                    <div class="RowFormularioNot" runat="server" id="dvFirmasDocumento">
                                        <div class="CellFormularioNot">
                                            <label for="cboEstado" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Firma Documento:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:DropDownList runat="server" ID="cboFirma" />
                                            <asp:RequiredFieldValidator ID="rfvFirma" runat="server" ControlToValidate="cboFirma" ErrorMessage="Seleccione la persona que firma el documento a generar." Text="*" ValidationGroup="AvanceModal" InitialValue="-1"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>                                    
                                    <div class="RowFormularioNot" runat="server" id="dvEnviarDireccion">
                                        <div class="CellFormularioNot">
                                            <label for="chkEnviarDireccion" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Enviar a Dirección:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:CheckBox runat="server" ID="chkEnviarDireccion" OnCheckedChanged="chkEnviarDireccion_CheckedChanged" AutoPostBack="true" ClientIDMode="Static" EnableTheming="false" />
                                        </div>
                                    </div>                                    
                                    <div class="RowFormularioNot" runat="server" id="dvListadoDirecciones">
                                        <div class="CellFormularioNot">
                                            <label for="ltlMunicipio" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">
                                                Direcciones:
                                                <asp:CustomValidator runat="server" ID="cvGrillaDirecciones" ValidationGroup="AvanceModal" Display="Dynamic" ErrorMessage="Debe ingresar por lo menos una dirección" OnServerValidate="cvGrillaDirecciones_ServerValidate">*</asp:CustomValidator>
                                            </label>
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:GridView runat="server" ID="grdDirecciones" AutoGenerateColumns="false" ShowFooter="true" ShowHeaderWhenEmpty="false" EmptyDataText="No se han adicionado direcciones" SkinID="GrillaDatosNotificaciones">
                                                <HeaderStyle Font-Size="9pt" />
                                                <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                                <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                                <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                 <Columns>                                                   
                                                     <asp:TemplateField HeaderText = "DIRECCION">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlDireccion" runat="server" Text='<%# Eval("Direccion")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:DropDownList runat="server" ID="cboGrdDireccion" Width="90%" OnSelectedIndexChanged="cboGrdDireccion_SelectedIndexChanged" AutoPostBack="true" />
                                                             <asp:RequiredFieldValidator ID="rfvGrdDireccion" runat="server" ControlToValidate="cboGrdDireccion" ErrorMessage="Seleccione la dirección" Text="*" ValidationGroup="AvanzarModalDirecciones" InitialValue="-1"></asp:RequiredFieldValidator>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText = "PERTENECE">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlPertenece" runat="server" Text='<%# Eval("Pertenece")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Literal ID="ltlPertenece" runat="server"></asp:Literal>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText = "DEPARTAMENTO">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlDepartamento" runat="server" Text='<%# Eval("Departamento")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Literal ID="ltlDepartamento" runat="server"></asp:Literal>
                                                        </FooterTemplate>
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText = "MUNICIPIO">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlMunicipio" runat="server" Text='<%# Eval("Municipio")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Literal ID="ltlMunicipio" runat="server"></asp:Literal>
                                                        </FooterTemplate>
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="ELIMINAR">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEliminar" runat="server" Text="Eliminar" CommandArgument='<%# Eval("DireccionID")%>' OnClick="lnkEliminar_Click" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lnkAdicionar" runat="server" Text="Adicionar" CausesValidation="true" ValidationGroup="AvanzarModalDirecciones" OnClick="lnkAdicionar_Click"></asp:LinkButton>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:ValidationSummary ID="valAvanzarModalDirecciones" runat="server" ValidationGroup="AvanzarModalDirecciones" ShowMessageBox="true" ShowSummary="false" />
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot" runat="server" id="dvEnviarCorreo">
                                        <div class="CellFormularioNot">
                                            <label for="chkEnviarCorreo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Enviar Correo Eléctronico:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:CheckBox runat="server" ID="chkEnviarCorreo" OnCheckedChanged="chkEnviarCorreo_CheckedChanged" AutoPostBack="true" ClientIDMode="Static" EnableTheming="false" />
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot" runat="server" id="dvListaCorreos">
                                        <div class="CellFormularioNot">
                                            <label for="ltlMunicipio" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">
                                                Correos:
                                                <asp:CustomValidator runat="server" ID="cvGrdCorreos" ValidationGroup="AvanceModal" Display="Dynamic" ErrorMessage="Debe ingresar por lo menos una dirección de correo electrónico" OnServerValidate="cvGrdCorreos_ServerValidate">*</asp:CustomValidator>
                                            </label>
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:GridView runat="server" ID="grdCorreos" AutoGenerateColumns="false" ShowFooter="true" ShowHeaderWhenEmpty="false" EmptyDataText="No se han adicionado correos electrónicos" SkinID="GrillaDatosNotificaciones">
                                                <HeaderStyle Font-Size="9pt" />
                                                <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                                <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                                <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                 <Columns>                                                    
                                                     <asp:TemplateField HeaderText = "DIRECCION">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlCorreo" runat="server" Text='<%# Eval("Correo")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:DropDownList runat="server" ID="cboGrdCorreo" Width="95%" />
                                                             <asp:RequiredFieldValidator ID="rfvGrdCorreo" runat="server" ControlToValidate="cboGrdCorreo" ErrorMessage="Seleccione el correo eléctronico" Text="*" ValidationGroup="AvanzarModalCorreos" InitialValue="-1"></asp:RequiredFieldValidator>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>                                                     
                                                     <asp:TemplateField HeaderText="ELIMINAR">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEliminarCorreo" runat="server" Text="Eliminar" CommandArgument='<%# Eval("Correo")%>' OnClick="lnkEliminarCorreo_Click"  />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lnkAdicionarCorreo" runat="server" Text="Adicionar" CausesValidation="true" ValidationGroup="AvanzarModalCorreos" OnClick="lnkAdicionarCorreo_Click"></asp:LinkButton>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:ValidationSummary ID="cvAvanzarModalCorreos" runat="server" ValidationGroup="AvanzarModalCorreos" ShowMessageBox="true" ShowSummary="false" />
                                        </div>
                                    </div>                                                                        
                                    <div class="RowFormularioNot" runat="server" id="dvAdjuntos">
                                        <div class="CellFormularioNot">
                                            <label for="txtAdjunto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Adjuntos Correo Eléctronico:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <cc1:AsyncFileUpload runat="server" ID="fuplAdjunto" CssClass="FileUploadAdjuntos" ClientIDMode="AutoID" OnClientUploadStarted="MostrarProgressDropDownAdjunto" OnClientUploadComplete="OcultarProgressDropDownAdjunto" OnClientUploadError="ErrorArchivoAdjunto" />
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot" runat="server" id="dvTextoCorreo">
                                        <div class="CellFormularioNot">
                                            <label for="txtTextoCorreo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Texto Correo Eléctronico:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:TextBox runat="server" ID="txtTextoCorreo" TextMode="MultiLine" Columns="30" Rows="5" style="resize: none;"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTextoCorreo" runat="server" ControlToValidate="txtTextoCorreo" ErrorMessage="Ingrese el texto del correo eléctronico" Text="*" ValidationGroup="AvanceModal"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>                                    
                                    <div class="RowFormularioNot" runat="server" id="dvReferenciaRecepcion">
                                        <div class="CellFormularioNot">
                                            <label for="txtReferenciaRecepcion" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Referencia:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:TextBox runat="server" ID="txtReferenciaRecepcion" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvReferenciaRecepcion" runat="server" ControlToValidate="txtReferenciaRecepcion" ErrorMessage="Ingrese la referencia" Text="*" ValidationGroup="AvanceModal"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot" runat="server" id="dvFechaRecepcion">
                                        <div class="CellFormularioNot">
                                            <label for="txtReferenciaRecepcion" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Fecha de Referencia:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:TextBox ID="txtFechaRecepcionDocumento" runat="server" ClientIDMode="Static"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="rexFechaRecepcionDocumento" runat="server" ControlToValidate="txtFechaRecepcionDocumento" ErrorMessage="La fecha de recepción del documento presenta un formato no valido. El formato de la fecha debe ser dd/mm/aaaa." ValidationExpression="^\d{2}\/\d{2}\/\d{4}" Text="*" ValidationGroup="AvanceModal"></asp:RegularExpressionValidator>
                                            <cc1:CalendarExtender OnClientShown="mostrarCalendarioAvanzar" ID="calFechaRecepcionDocumento" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaRecepcionDocumento"/>
                                            <asp:RequiredFieldValidator ID="rfvFechaRecepcionDocumento" runat="server" ControlToValidate="txtFechaRecepcionDocumento" ErrorMessage="Ingrese la fecha de la referencia" Text="*" ValidationGroup="AvanceModal"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <asp:Repeater runat="server" ID="rptDatosUsuariosAvanzar" OnItemDataBound="rptDatosUsuariosAvanzar_ItemDataBound">
                                        <ItemTemplate>
                                            
                                            <div class="RowFormularioNot" runat="server" id="dvRptIdentificacion">
                                                <div class="CellFormularioNot">
                                                    <label for="ltlRptIdentificacion" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;"><asp:Literal runat="server" Text='<%# "Identificación Usuario " +  (Container.ItemIndex + 1) + ":" %>' /></label>
                                                </div>
                                                <div class="CellFormularioCamposNot">
                                                    <asp:Literal runat="server" ID="ltlRptIdentificacion" Text='<%# Eval("NPE_NUMERO_IDENTIFICACION") %>'></asp:Literal>
                                                </div>
                                            </div>
                                            <div class="RowFormularioNot" runat="server" id="dvRptUsuario">
                                                <div class="CellFormularioNot">
                                                    <label for="ltlRptUsuario" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;"><asp:Literal runat="server" Text='<%# "Usuario " +  (Container.ItemIndex + 1) + ":" %>' /></label>                                           
                                                </div>
                                                <div class="CellFormularioCamposNot">
                                                    <asp:Literal runat="server" ID="ltlRptUsuario" Text='<%# Eval("NOMBRE_COMPLETO") %>'></asp:Literal>
                                                    <asp:HiddenField runat="server" ID="hdfRptPersonaID" Value='<%# Eval("ID_PERSONA") %>' />
                                                    <asp:HiddenField runat="server" ID="hdfRptPersonaIdentificacion" Value='<%# Eval("NPE_NUMERO_IDENTIFICACION") %>' />
                                                    <asp:HiddenField runat="server" ID="hdRptfFlujoID" Value='<%# Eval("ID_FLUJO_NOT_ELEC") %>' />
                                                </div>
                                            </div>

                                            <div class="RowFormularioNot" runat="server" id="dvRptAdjuntarActoAdministrativo">
                                                <div class="CellFormularioNot">
                                                    <label for="chkAdjuntarActoAdministrativo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;"><asp:Literal ID="Literal1" runat="server" Text='<%# "Usuario " +  (Container.ItemIndex + 1) + " -" %>' /> Entregar Acto Administrativo:</label>                                           
                                                </div>
                                                <div class="CellFormularioCamposNot">
                                                    <asp:CheckBox runat="server" ID="chkRptAdjuntarActoAdministrativo" OnCheckedChanged="chkRptAdjuntarActoAdministrativo_CheckedChanged" AutoPostBack="true" EnableTheming="false" />
                                                </div>
                                            </div>                                                          
                                            <div class="RowFormularioNot" runat="server" id="dvRptAdjuntarConceptosActoAdministrativo">
                                                <div class="CellFormularioNot">
                                                    <label for="chkRptAdjuntarConceptosActoAdministrativo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;"><asp:Literal ID="Literal2" runat="server" Text='<%# "Usuario " +  (Container.ItemIndex + 1) + " -" %>' /> Entregar Conceptos Acto Administrativo:</label>                                           
                                                </div>
                                                <div class="CellFormularioCamposNot">
                                                    <asp:CheckBox runat="server" ID="chkRptAdjuntarConceptosActoAdministrativo" EnableTheming="false" />
                                                </div>
                                            </div>

                                            <div class="RowFormularioNot" runat="server" id="dvRptEnviarDireccion">
                                                <div class="CellFormularioNot">
                                                    <label for="chkRptEnviarDireccion" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;"><asp:Literal runat="server" Text='<%# "Usuario " +  (Container.ItemIndex + 1) + " -" %>' /> Enviar a Dirección:</label>                                           
                                                </div>
                                                <div class="CellFormularioCamposNot">
                                                    <asp:CheckBox runat="server" ID="chkRptEnviarDireccion" OnCheckedChanged="chkRptEnviarDireccion_CheckedChanged"  AutoPostBack="true" EnableTheming="false" />
                                                </div>
                                            </div>
                                            <div class="RowFormularioNot" runat="server" id="dvRptListadoDirecciones">
                                                <div class="CellFormularioNot">
                                                    <label for="ltlMunicipio" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">
                                                        <asp:Literal runat="server" Text='<%# "Usuario " +  (Container.ItemIndex + 1) + " -" %>' /> Direcciones:   
                                                        <asp:CustomValidator runat="server" ID="cvRptGrillaDirecciones" ValidationGroup="AvanceModal" Display="Dynamic" ErrorMessage="Debe ingresar por lo menos una dirección" OnServerValidate="cvRptGrillaDirecciones_ServerValidate">*</asp:CustomValidator>                                             
                                                    </label>
                                                </div>
                                                <div class="CellFormularioCamposNot">
                                                    <asp:GridView runat="server" ID="grdRptDirecciones" AutoGenerateColumns="false" ShowFooter="true" ShowHeaderWhenEmpty="false" EmptyDataText="No se han adicionado direcciones" SkinID="GrillaDatosNotificaciones">
                                                        <HeaderStyle Font-Size="9pt" />
                                                        <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                         <Columns>                                                    
                                                             <asp:TemplateField HeaderText = "DIRECCION">
                                                                <ItemTemplate>
                                                                    <asp:Literal ID="ltlDireccion" runat="server" Text='<%# Eval("Direccion")%>'></asp:Literal>
                                                                </ItemTemplate>
                                                                 <FooterTemplate>
                                                                     <asp:DropDownList runat="server" ID="cboRptGrdDireccion" Width="90%" OnSelectedIndexChanged="cboRptGrdDireccion_SelectedIndexChanged" AutoPostBack="true" />
                                                                     <asp:RequiredFieldValidator ID="rfvRptGrdDireccion" runat="server" ControlToValidate="cboRptGrdDireccion" ErrorMessage="Seleccione la dirección" Text="*" InitialValue="-1"></asp:RequiredFieldValidator>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText = "PERTENECE">
                                                                <ItemTemplate>
                                                                    <asp:Literal ID="ltlPertenece" runat="server" Text='<%# Eval("Pertenece")%>'></asp:Literal>
                                                                </ItemTemplate>
                                                                 <FooterTemplate>
                                                                     <asp:Literal ID="ltlPertenece" runat="server"></asp:Literal>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText = "DEPARTAMENTO">
                                                                <ItemTemplate>
                                                                    <asp:Literal ID="ltlDepartamento" runat="server" Text='<%# Eval("Departamento")%>'></asp:Literal>
                                                                </ItemTemplate>
                                                                 <FooterTemplate>
                                                                     <asp:Literal ID="ltlDepartamento" runat="server"></asp:Literal>
                                                                </FooterTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText = "MUNICIPIO">
                                                                <ItemTemplate>
                                                                    <asp:Literal ID="ltlMunicipio" runat="server" Text='<%# Eval("Municipio")%>'></asp:Literal>
                                                                </ItemTemplate>
                                                                 <FooterTemplate>
                                                                     <asp:Literal ID="ltlMunicipio" runat="server"></asp:Literal>
                                                                </FooterTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="ELIMINAR">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkRptEliminar" runat="server" Text="Eliminar" CommandArgument='<%# Eval("DireccionID")%>' OnClick="lnkRptEliminar_Click" />
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:LinkButton ID="lnkRptAdicionar" runat="server" Text="Adicionar" CausesValidation="true" OnClick="lnkRptAdicionar_Click"></asp:LinkButton>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:ValidationSummary ID="valRptAvanzarModalDirecciones" runat="server" ValidationGroup='<%# "RptAvanzarModalDirecciones" +  Container.ItemIndex %>' ShowMessageBox="true" ShowSummary="false" />
                                                </div>
                                            </div>
                                            <div class="RowFormularioNot" runat="server" id="dvRptEnviarCorreo">
                                                <div class="CellFormularioNot">
                                                    <label for="chkRptEnviarCorreo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;"><asp:Literal ID="Literal7" runat="server" Text='<%# "Usuario " +  (Container.ItemIndex + 1) + " -" %>' /> Enviar Correo Eléctronico:</label>                                           
                                                </div>
                                                <div class="CellFormularioCamposNot">
                                                    <asp:CheckBox runat="server" ID="chkRptEnviarCorreo" OnCheckedChanged="chkRptEnviarCorreo_CheckedChanged" AutoPostBack="true" EnableTheming="false" />
                                                </div>
                                            </div>

                                            <div class="RowFormularioNot" runat="server" id="dvRptListaCorreos">
                                                <div class="CellFormularioNot">
                                                    <label for="ltlMunicipio" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">
                                                        <asp:Literal runat="server" Text='<%# "Usuario " +  (Container.ItemIndex + 1) + " -" %>' /> Correos:
                                                        <asp:CustomValidator runat="server" ID="cvRptGrdCorreos" ValidationGroup="AvanceModal" Display="Dynamic" ErrorMessage="Debe ingresar por lo menos una dirección de correo electrónico" OnServerValidate="cvRptGrdCorreos_ServerValidate">*</asp:CustomValidator>
                                                    </label>
                                                </div>
                                                <div class="CellFormularioCamposNot">
                                                    <asp:GridView runat="server" ID="grdRptCorreos" AutoGenerateColumns="false" ShowFooter="true" ShowHeaderWhenEmpty="false" EmptyDataText="No se han adicionado correos electrónicos" SkinID="GrillaDatosNotificaciones">
                                                        <HeaderStyle Font-Size="9pt" />
                                                        <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                        <Columns>                                                    
                                                             <asp:TemplateField HeaderText = "DIRECCION">
                                                                <ItemTemplate>
                                                                    <asp:Literal ID="ltlCorreo" runat="server" Text='<%# Eval("Correo")%>'></asp:Literal>
                                                                </ItemTemplate>
                                                                 <FooterTemplate>
                                                                     <asp:DropDownList runat="server" ID="cboRptGrdCorreo" Width="95%" />
                                                                     <asp:RequiredFieldValidator ID="rfvRptGrdCorreo" runat="server" ControlToValidate="cboRptGrdCorreo" ErrorMessage="Seleccione el correo eléctronico" Text="*" InitialValue="-1"></asp:RequiredFieldValidator>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>                                                     
                                                             <asp:TemplateField HeaderText="ELIMINAR">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkRptEliminarCorreo" runat="server" Text="Eliminar" CommandArgument='<%# Eval("Correo")%>' OnClick="lnkRptEliminarCorreo_Click"  />
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:LinkButton ID="lnkRptAdicionarCorreo" runat="server" Text="Adicionar" CausesValidation="true" OnClick="lnkRptAdicionarCorreo_Click"></asp:LinkButton>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:ValidationSummary ID="cvRptAvanzarModalCorreos" runat="server" ValidationGroup='<%# "RptAvanzarModalCorreos" +  Container.ItemIndex %>' ShowMessageBox="true" ShowSummary="false" />
                                                </div>
                                            </div>                                                                                                          
                                            <div class="RowFormularioNot" runat="server" id="dvRptAdjuntos">
                                                <div class="CellFormularioNot">
                                                    <label for="txtRptAdjunto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;"><asp:Literal runat="server" Text='<%# "Usuario " +  (Container.ItemIndex + 1) + " -" %>' /> Adjuntos Correo Eléctronico:</label>                                           
                                                </div>
                                                <div class="CellFormularioCamposNot">
                                                    <cc1:AsyncFileUpload runat="server" ID="fuplRptAdjunto" CssClass="FileUploadAdjuntos" ClientIDMode="AutoID" OnClientUploadStarted="MostrarProgressDropDownAdjunto" OnClientUploadComplete="OcultarProgressDropDownAdjunto" OnClientUploadError="ErrorArchivoAdjunto" />
                                                </div>
                                            </div>
                                            <div class="RowFormularioNot" runat="server" id="dvRptTextoCorreo">
                                                <div class="CellFormularioNot">
                                                    <label for="txtRptTextoCorreo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;"><asp:Literal runat="server" Text='<%# "Usuario " +  (Container.ItemIndex + 1) + " -" %>' /> Texto Correo Eléctronico:</label>                                           
                                                </div>
                                                <div class="CellFormularioCamposNot">
                                                    <asp:TextBox runat="server" ID="txtRptTextoCorreo" TextMode="MultiLine" Columns="30" Rows="5" style="resize: none;"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvRptTextoCorreo" runat="server" ControlToValidate="txtRptTextoCorreo" ErrorMessage='<%# "Ingrese el texto del correo eléctronico para el usuario " + (Container.ItemIndex + 1) %>' Text="*" ValidationGroup="AvanceModal"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="RowFormularioNot" runat="server" id="dvRptReferenciaRecepcion">
                                                <div class="CellFormularioNot">
                                                    <label for="txtRptReferenciaRecepcion"><asp:Literal runat="server" Text='<%# "Usuario " +  (Container.ItemIndex + 1) + " -" %>' /> Referencia:</label>                                           
                                                </div>
                                                <div class="CellFormularioCamposNot">
                                                    <asp:TextBox runat="server" ID="txtRptReferenciaRecepcion" MaxLength="100"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvRptReferenciaRecepcion" runat="server" ControlToValidate="txtRptReferenciaRecepcion" ErrorMessage='<%# "Ingrese la referencia para el usuario " + (Container.ItemIndex + 1) %>' Text="*" ValidationGroup="AvanceModal"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="RowFormularioNot" runat="server" id="dvRptFechaRecepcion">
                                                <div class="CellFormularioNot">
                                                    <label for="txtRptReferenciaRecepcion" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;"><asp:Literal ID="Literal13" runat="server" Text='<%# "Usuario " +  (Container.ItemIndex + 1) + " -" %>' /> Fecha de Referencia:</label>                                           
                                                </div>
                                                <div class="CellFormularioCamposNot">
                                                    <asp:TextBox ID="txtRptFechaRecepcionDocumento" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="rexRptFechaRecepcionDocumento" runat="server" ControlToValidate="txtRptFechaRecepcionDocumento" ErrorMessage='<%# "La fecha de recepción del documento para el usuario " + (Container.ItemIndex + 1) + " presenta un formato no valido. El formato de la fecha debe ser dd/mm/aaaa." %>' ValidationExpression="^\d{2}\/\d{2}\/\d{4}" Text="*" ValidationGroup="AvanceModal"></asp:RegularExpressionValidator>
                                                    <cc1:CalendarExtender OnClientShown="mostrarCalendarioAvanzar" ID="calFechaRecepcionDocumento" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtRptFechaRecepcionDocumento"/>
                                                </div>
                                            </div>


                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <div class="RowFormularioNot" runat="server" id="dvTipoIdentificacionPersonaNotificar">
                                        <div class="CellFormularioNot">
                                            <label for="cboTipoIdentificacionPersonaNotificar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Tipo Identificación Persona Notificar:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:DropDownList runat="server" ID="cboTipoIdentificacionPersonaNotificar"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvTipoIdentificacionPersonaNotificar" runat="server" ControlToValidate="cboTipoIdentificacionPersonaNotificar" ErrorMessage="Seleccione el tipo de identificación de la persona a notificar" InitialValue="-1" Text="*" ValidationGroup="AvanceModal"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot" runat="server" id="dvNumeroIdentificacionPersonaNotificar">
                                        <div class="CellFormularioNot">
                                            <label for="txtNumeroIdentificacionPersonaNotificar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Número Identificación Persona Notificar:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:TextBox runat="server" ID="txtNumeroIdentificacionPersonaNotificar" MaxLength="30"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvNumeroIdentificacionPersonaNotificar" runat="server" ControlToValidate="txtNumeroIdentificacionPersonaNotificar" ErrorMessage="Ingrese el número de identificación de la persona a notificar" Text="*" ValidationGroup="AvanceModal"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot" runat="server" id="dvNombrePersonaNotificar">
                                        <div class="CellFormularioNot">
                                            <label for="txtNombrePersonaNotificar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Nombre Persona Notificar:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:TextBox runat="server" ID="txtNombrePersonaNotificar" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvNombrePersonaNotificar" runat="server" ControlToValidate="txtNombrePersonaNotificar" ErrorMessage="Ingrese el nombre de la persona a notificar" Text="*" ValidationGroup="AvanceModal"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot" runat="server" id="dvCalidadPersonaNotificar">
                                        <div class="CellFormularioNot">
                                            <label for="txtReferenciaRecepcion" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Calidad Persona Notificar:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:TextBox runat="server" ID="txtCalidadPersonaNotificar" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCalidadPersonaNotificar" runat="server" ControlToValidate="txtCalidadPersonaNotificar" ErrorMessage="Ingrese la calidad de la persona a notificar" Text="*" ValidationGroup="AvanceModal"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot" runat="server" id="dvDocumentoAdicional">
                                        <div class="CellFormularioNot">
                                            <label for="fuplDocumentoAdicional" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Documento Adicional:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <cc1:AsyncFileUpload runat="server" ID="fuplDocumentoAdicional" CssClass="FileUploadAdjuntos" ClientIDMode="AutoID" OnClientUploadStarted="MostrarProgressDropDownAdicional" OnClientUploadComplete="OcultarProgressDropDownAdicional" OnClientUploadError="ErrorArchivoAdicional" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="RowResultadoModalNot">
                            <div class="CellButtonModal">
                                <asp:HiddenField runat="server" ID="hdfActoID" />                                
                                <asp:HiddenField runat="server" ID="hdfPersonaID" />
                                <asp:HiddenField runat="server" ID="hdfPersonaIdentificacion" />
                                <asp:HiddenField runat="server" ID="hdfEstadoActualID" />
                                <asp:HiddenField runat="server" ID="hdfFechaEstadoActual" />
                                <asp:HiddenField runat="server" ID="hdfFlujoID" />        
                                <asp:HiddenField runat="server" ID="hdfCodigoExpedienteActo" />                        
                                <asp:HiddenField runat="server" ID="hdfNumeroVitalActo" />
                                <asp:HiddenField runat="server" ID="hdfAutoridadAmbiental" />
                                <asp:HiddenField runat="server" ID="hdfEsPDI" />
                                <asp:ValidationSummary ID="valAvanceNotificacion" runat="server" ValidationGroup="AvanceModal" ShowMessageBox="true" ShowSummary="false" />
                                <asp:Button ID="cmdAvanzar" runat="server" ClientIDMode="Static" Text="Avanzar" CssClass="boton" ValidationGroup="AvanceModal" OnClick="cmdAvanzar_Click"/>
                                <asp:Button ID="cmdCancelar" runat="server" Text="Cancelar" CssClass="boton" CausesValidation="false" OnClick="cmdCancelar_Click"/>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmdAvanzar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="cmdCancelar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="chkEnviarDireccion" EventName="CheckedChanged" />
                    <asp:AsyncPostBackTrigger ControlID="chkEnviarCorreo" EventName="CheckedChanged" />
                    <asp:AsyncPostBackTrigger ControlID="chkAdjuntarActoAdministrativo" EventName="CheckedChanged" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="uppAvanzarEstado" runat="server" AssociatedUpdatePanelID="upnlAvanzarEstado">
                <ProgressTemplate>  
                    <div id="ModalProgressContainer">
                        <div>
                            <p>Procesando...</p>
                            <p><asp:Image ID="imgUpdateProgressAdjunto" runat="server" SkinId="procesando"/></p>
                        </div>
                    </div>                         
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>


        <input type="button" runat="server" id="cmdEditarEstadoHide" style="display:none;" />
        <cc1:ModalPopupExtender ID="mpeEditarEstado" runat="server" PopupControlID="dvEditarEstado" TargetControlID="cmdEditarEstadoHide" BehaviorID="mpeEditarEstados" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <div id="dvEditarEstado" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalFormularioNot">
            <asp:UpdatePanel runat="server" ID="upnlEditarEstado" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="TableResultadoModalNot">
                        <div class="RowResultadoModalNot">
                            <div class="CellResultadoTituloModalNot">
                                EDITAR ESTADO DE NOTIFICACIÓN
                            </div>
                        </div>
                        <div class="RowResultadoModalNot">
                            <div class="CellResultadoModalNot">
                                <div class="TableFormularioNot">
                                    <div class="RowFormularioNot">
                                        <div class="CellFormularioNot">
                                            <label for="ltlNumeroVitalEditar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Número VITAL:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:Literal runat="server" ID="ltlNumeroVitalEditar"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot">
                                        <div class="CellFormularioNot">
                                            <label for="ltlNumeroExpedienteEditar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Número Expediente:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:Literal runat="server" ID="ltlNumeroExpedienteEditar"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot">
                                        <div class="CellFormularioNot">
                                            <label for="ltlActoAdministrativoEditar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Acto Administrativo:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:Literal runat="server" ID="ltlActoAdministrativoEditar"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot">
                                        <div class="CellFormularioNot">
                                            <label for="ltlNumeroActoEditar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Número Acto Administrativo:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:Literal runat="server" ID="ltlNumeroActoEditar"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot">
                                        <div class="CellFormularioNot">
                                            <label for="ltlIdentificacionEditar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Identificación:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:Literal runat="server" ID="ltlIdentificacionEditar"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot">
                                        <div class="CellFormularioNot">
                                            <label for="ltlUsuarioEditar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Usuario:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:Literal runat="server" ID="ltlUsuarioEditar"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot">
                                        <div class="CellFormularioNot">
                                            <label for="ltlEstadoActualEditar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Estado:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:Literal runat="server" ID="ltlEstadoEditar"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot">
                                        <div class="CellFormularioNot">
                                            <label for="txtFechaEstadoNotificacion" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Fecha Estado:</label>
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:TextBox ID="txtFechaEstadoNotificacionEditar" runat="server" ClientIDMode="Static"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvFechaEstadoNotificacionEditar" runat="server" ControlToValidate="txtFechaEstadoNotificacionEditar" ErrorMessage="Ingrese la fecha del estado." Text="*" ValidationGroup="EditarModal" InitialValue=""></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="rexFechaEstadoNotificacionEditar" runat="server" ControlToValidate="txtFechaEstadoNotificacionEditar" ErrorMessage="Formato de la fecha del estado no valido." ValidationExpression="^\d{2}\/\d{2}\/\d{4} \d{2}\:\d{2}" Width="3px" Text="*" ValidationGroup="EditarModal"></asp:RegularExpressionValidator>
                                            <cc1:CalendarExtender ID="calFechaEstadoNotificacionEditar" OnClientShown="mostrarCalendarioEditar" runat="server" Format="dd/MM/yyyy HH:mm" TargetControlID="txtFechaEstadoNotificacionEditar"/>
                                            <cc1:MaskedEditExtender ID="mskFechaEstadoNotificacionEditar" Mask="99/99/9999 99:99" runat="server" MaskType="DateTime" AcceptAMPM="False" UserDateFormat="DayMonthYear" UserTimeFormat="None" TargetControlID="txtFechaEstadoNotificacionEditar" Enabled="True"></cc1:MaskedEditExtender>                                          
                                            <asp:CustomValidator runat="server" ID="cvFechaEstadoNotificacionEditar" ValidationGroup="EditarModal" Display="Dynamic" ErrorMessage="La fecha del estado no puede ser menor a la fecha del estado anterior" OnServerValidate="cvFechaEstadoNotificacionEditar_ServerValidate">*</asp:CustomValidator>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot">
                                        <div class="CellFormularioNot">
                                            <label for="cboEstadoEditar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Observación:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:TextBox runat="server" ID="txtObservacionEditar" TextMode="MultiLine" Columns="30" Rows="5" style="resize: none;"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="RowFormularioNot" runat="server" id="dvAdjuntarActoAdministrativoEditar">
                                        <div class="CellFormularioNot">
                                            <label for="chkAdjuntarActoAdministrativoEditar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Entregar Acto Administrativo:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:CheckBox runat="server" ID="chkAdjuntarActoAdministrativoEditar" OnCheckedChanged="chkAdjuntarActoAdministrativoEditar_CheckedChanged" AutoPostBack="true" EnableTheming="false" />
                                        </div>
                                    </div>

                                    <div class="RowFormularioNot" runat="server" id="dvAdjuntarConceptosActoAdministrativoEditar">
                                        <div class="CellFormularioNot">
                                            <label for="chkAdjuntarConceptosActoAdministrativoEditar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Entregar Concepto(s) Acto Administrativo:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:CheckBox runat="server" ID="chkAdjuntarConceptosActoAdministrativoEditar" EnableTheming="false" />
                                        </div>
                                    </div>

                                    <div class="RowFormularioNot" runat="server" id="dvFirmasDocumentoEditar">
                                        <div class="CellFormularioNot">
                                            <label for="cboFirmaEditar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Firma Documento:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:DropDownList runat="server" ID="cboFirmaEditar" />
                                            <asp:RequiredFieldValidator ID="rfvFirmaEditar" runat="server" ControlToValidate="cboFirmaEditar" ErrorMessage="Seleccione la persona que firma el documento a generar." Text="*" ValidationGroup="EditarModal" InitialValue="-1"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>                                    
                                    <div class="RowFormularioNot" runat="server" id="dvEnviarDireccionEditar" visible="false">
                                        <div class="CellFormularioNot">
                                            <label for="chkEnviarDireccionEditar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Enviar a Dirección:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:CheckBox runat="server" ID="chkEnviarDireccionEditar" OnCheckedChanged="chkEnviarDireccionEditar_CheckedChanged" AutoPostBack="true" ClientIDMode="Static" EnableTheming="false" />
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot" runat="server" id="dvListadoDireccionesEditar">
                                        <div class="CellFormularioNot">
                                            <label for="ltlMunicipio" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">
                                                Direcciones:
                                                <asp:CustomValidator runat="server" ID="cvGrillaDireccionesEditar" ValidationGroup="EditarModal" Display="Dynamic" ErrorMessage="Debe ingresar por lo menos una dirección" OnServerValidate="cvGrillaDireccionesEditar_ServerValidate">*</asp:CustomValidator>
                                            </label>
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:GridView runat="server" ID="grdDireccionesEditar" AutoGenerateColumns="false" ShowFooter="true" ShowHeaderWhenEmpty="false" EmptyDataText="No se han adicionado direcciones" SkinID="GrillaDatosNotificaciones">
                                                <HeaderStyle Font-Size="9pt" />
                                                <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                                <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                                <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                <Columns>                                                 
                                                     <asp:TemplateField HeaderText = "DIRECCION">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlDireccion" runat="server" Text='<%# Eval("Direccion")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:DropDownList runat="server" ID="cboGrdDireccionEditar" Width="90%" OnSelectedIndexChanged="cboGrdDireccionEditar_SelectedIndexChanged" AutoPostBack="true" />
                                                             <asp:RequiredFieldValidator ID="rfvGrdDireccionEditar" runat="server" ControlToValidate="cboGrdDireccionEditar" ErrorMessage="Seleccione la dirección" Text="*" ValidationGroup="AvanzarModalDireccionesEditar" InitialValue="-1"></asp:RequiredFieldValidator>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText = "PERTENECE">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlPertenece" runat="server" Text='<%# Eval("Pertenece")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Literal ID="ltlPertenece" runat="server"></asp:Literal>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText = "DEPARTAMENTO">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlDepartamento" runat="server" Text='<%# Eval("Departamento")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Literal ID="ltlDepartamento" runat="server"></asp:Literal>
                                                        </FooterTemplate>
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText = "MUNICIPIO">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlMunicipio" runat="server" Text='<%# Eval("Municipio")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Literal ID="ltlMunicipio" runat="server"></asp:Literal>
                                                        </FooterTemplate>
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="ELIMINAR">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEliminarDireccionEditar" runat="server" Text="Eliminar" CommandArgument='<%# Eval("DireccionID")%>' OnClick="lnkEliminarDireccionEditar_Click" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lnkAdicionarDireccionEditar" runat="server" Text="Adicionar" CausesValidation="true" ValidationGroup="AvanzarModalDireccionesEditar" OnClick="lnkAdicionarDireccionEditar_Click"></asp:LinkButton>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:ValidationSummary ID="valAvanzarModalDireccionesEditar" runat="server" ValidationGroup="AvanzarModalDireccionesEditar" ShowMessageBox="true" ShowSummary="false" />
                                        </div>
                                    </div>                                    
                                    <div class="RowFormularioNot" runat="server" id="dvEnviarCorreoEditar" visible="false">
                                        <div class="CellFormularioNot">
                                            <label for="chkEnviarCorreoEditar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Enviar Correo Eléctronico:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:CheckBox runat="server" ID="chkEnviarCorreoEditar" OnCheckedChanged="chkEnviarCorreoEditar_CheckedChanged" AutoPostBack="true" ClientIDMode="Static" EnableTheming="false" />
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot" runat="server" id="dvListaCorreosEditar">
                                        <div class="CellFormularioNot">
                                            <label for="ltlMunicipio" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">
                                                Correos:
                                                <asp:CustomValidator runat="server" ID="cvGrdCorreosEditar" ValidationGroup="EditarModal" Display="Dynamic" ErrorMessage="Debe ingresar por lo menos una dirección de correo electrónico" OnServerValidate="cvGrdCorreosEditar_ServerValidate">*</asp:CustomValidator>
                                            </label>
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:GridView runat="server" ID="grdCorreosEditar" AutoGenerateColumns="false" ShowFooter="true" ShowHeaderWhenEmpty="false" EmptyDataText="No se han adicionado correos electrónicos" SkinID="GrillaDatosNotificaciones">
                                                <HeaderStyle Font-Size="9pt" />
                                                <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                                <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                                <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                <Columns>                                                                                                   
                                                     <asp:TemplateField HeaderText = "DIRECCION">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlCorreo" runat="server" Text='<%# Eval("Correo")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:DropDownList runat="server" ID="cboGrdCorreoEditar" Width="95%" />
                                                             <asp:RequiredFieldValidator ID="rfvGrdCorreoEditar" runat="server" ControlToValidate="cboGrdCorreoEditar" ErrorMessage="Seleccione el correo eléctronico" Text="*" ValidationGroup="EditarModalCorreos" InitialValue="-1"></asp:RequiredFieldValidator>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>                                                     
                                                     <asp:TemplateField HeaderText="ELIMINAR">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEliminarCorreoEditar" runat="server" Text="Eliminar" CommandArgument='<%# Eval("Correo")%>' OnClick="lnkEliminarCorreoEditar_Click" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lnkAdicionarCorreoEditar" runat="server" Text="Adicionar" CausesValidation="true" ValidationGroup="EditarModalCorreos" OnClick="lnkAdicionarCorreoEditar_Click"></asp:LinkButton>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:ValidationSummary ID="cvEditarModalCorreos" runat="server" ValidationGroup="EditarModalCorreos" ShowMessageBox="true" ShowSummary="false" />
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot" runat="server" id="dvAdjuntosEditar" visible="false">
                                        <div class="CellFormularioNot">
                                            <label for="txtAdjuntoEditar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Adjuntos Correo Eléctronico:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <cc1:AsyncFileUpload runat="server" ID="fuplAdjuntoEditar" CssClass="FileUploadAdjuntos" ClientIDMode="AutoID" OnClientUploadStarted="MostrarProgressDropDownAdjuntoEditar" OnClientUploadComplete="OcultarProgressDropDownAdjuntoEditar" OnClientUploadError="ErrorArchivoAdjuntoEditar" />
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot" runat="server" id="dvTextoCorreoEditar" visible="false">
                                        <div class="CellFormularioNot">
                                            <label for="txtTextoCorreo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Texto Correo Eléctronico:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:TextBox runat="server" ID="txtTextoCorreoEditar" TextMode="MultiLine" Columns="30" Rows="5" style="resize: none;"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTextoCorreoEditar" runat="server" ControlToValidate="txtTextoCorreoEditar" ErrorMessage="Ingrese el texto del correo eléctronico" Text="*" ValidationGroup="EditarModal"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>                                    
                                    <div class="RowFormularioNot" runat="server" id="dvReferenciaRecepcionEditar" visible="false">
                                        <div class="CellFormularioNot">
                                            <label for="txtReferenciaRecepcionEditar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Referencia:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:TextBox runat="server" ID="txtReferenciaRecepcionEditar" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvReferenciaRecepcionEditar" runat="server" ControlToValidate="txtReferenciaRecepcionEditar" ErrorMessage="Ingrese la referencia" Text="*" ValidationGroup="AvanceModal"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot" runat="server" id="dvFechaRecepcionEditar" visible="false">
                                        <div class="CellFormularioNot">
                                            <label for="txtReferenciaRecepcionEditar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Fecha de Referencia:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:TextBox ID="txtFechaRecepcionDocumentoEditar" runat="server" ClientIDMode="Static"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="rexFechaRecepcionDocumentoEditar" runat="server" ControlToValidate="txtFechaRecepcionDocumentoEditar" ErrorMessage="La fecha de recepción del documento presenta un formato no valido. El formato de la fecha debe ser dd/mm/aaaa." ValidationExpression="^\d{2}\/\d{2}\/\d{4}" Text="*" ValidationGroup="EditarModal"></asp:RegularExpressionValidator>
                                            <cc1:CalendarExtender OnClientShown="mostrarCalendarioAvanzar" ID="calFechaRecepcionDocumentoEditar" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaRecepcionDocumentoEditar"/>
                                            <asp:RequiredFieldValidator ID="rfvFechaRecepcionDocumentoEditar" runat="server" ControlToValidate="txtFechaRecepcionDocumentoEditar" ErrorMessage="Ingrese la fecha de la referencia" Text="*" ValidationGroup="AvanceModal"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot" runat="server" id="dvTipoIdentificacionPersonaNotificarEditar">
                                        <div class="CellFormularioNot">
                                            <label for="cboTipoIdentificacionPersonaNotificarEditar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Tipo Identificación Persona Notificar:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:DropDownList runat="server" ID="cboTipoIdentificacionPersonaNotificarEditar"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvTipoIdentificacionPersonaNotificarEditar" runat="server" ControlToValidate="cboTipoIdentificacionPersonaNotificarEditar" ErrorMessage="Seleccione el tipo de identificación de la persona a notificar" InitialValue="-1" Text="*" ValidationGroup="EditarModal"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot" runat="server" id="dvNumeroIdentificacionPersonaNotificarEditar">
                                        <div class="CellFormularioNot">
                                            <label for="txtNumeroIdentificacionPersonaNotificarEditar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Número Identificación Persona Notificar:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:TextBox runat="server" ID="txtNumeroIdentificacionPersonaNotificarEditar" MaxLength="30"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvNumeroIdentificacionPersonaNotificarEditar" runat="server" ControlToValidate="txtNumeroIdentificacionPersonaNotificarEditar" ErrorMessage="Ingrese el número de identificación de la persona a notificar" Text="*" ValidationGroup="EditarModal"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot" runat="server" id="dvNombrePersonaNotificarEditar">
                                        <div class="CellFormularioNot">
                                            <label for="txtNombrePersonaNotificarEditar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Nombre Persona Notificar:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:TextBox runat="server" ID="txtNombrePersonaNotificarEditar" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvNombrePersonaNotificarEditar" runat="server" ControlToValidate="txtNombrePersonaNotificarEditar" ErrorMessage="Ingrese el nombre de la persona a notificar" Text="*" ValidationGroup="EditarModal"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot" runat="server" id="dvCalidadPersonaNotificarEditar">
                                        <div class="CellFormularioNot">
                                            <label for="txtReferenciaRecepcionEditar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Calidad Persona Notificar:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:TextBox runat="server" ID="txtCalidadPersonaNotificarEditar" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCalidadPersonaNotificarEditar" runat="server" ControlToValidate="txtCalidadPersonaNotificarEditar" ErrorMessage="Ingrese la calidad de la persona a notificar" Text="*" ValidationGroup="EditarModal"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot" runat="server" id="dvDocumentoAdicionalEditar">
                                        <div class="CellFormularioNot">
                                            <label for="fuplDocumentoAdicionalEditar" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Documento Adicional:</label>                                           
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <cc1:AsyncFileUpload runat="server" ID="fuplDocumentoAdicionalEditar" CssClass="FileUploadAdjuntos" ClientIDMode="AutoID" OnClientUploadStarted="MostrarProgressDropDownAdicionalEditar" OnClientUploadComplete="OcultarProgressDropDownAdicionalEditar" OnClientUploadError="ErrorArchivoAdicionalEditar" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="RowResultadoModalNot">
                            <div class="CellButtonModal">
                                <asp:HiddenField runat="server" ID="hdfEstadoPersonaActoID" />
                                <asp:HiddenField runat="server" ID="hdfAutoridadAmbientalEditar" />
                                <asp:HiddenField runat="server" ID="hdfPersonaIDEditar" />  
                                <asp:HiddenField runat="server" ID="hdfEstadoActualIDEditar" />                                  
                                <asp:HiddenField runat="server" ID="hdfPersonaIdentificacionEditar" />                                                                
                                <asp:HiddenField runat="server" ID="hdfCodigoExpedienteActoEditar" />   
                                <asp:HiddenField runat="server" ID="hdfNumeroVitalActoEditar" />   
                                <asp:HiddenField runat="server" ID="hdfFechaEstadoAnterior" />                                                                
                                <asp:ValidationSummary ID="valEditarNotificacion" runat="server" ValidationGroup="EditarModal" ShowMessageBox="true" ShowSummary="false" />
                                <asp:Button ID="cmdEditar" runat="server" Text="Guardar" CssClass="boton" ValidationGroup="EditarModal" OnClick="cmdEditar_Click"/>
                                <asp:Button ID="cmdCancelarEditar" runat="server" Text="Cancelar" CssClass="boton" CausesValidation="false" OnClick="cmdCancelarEditar_Click"/>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmdEditar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="cmdCancelarEditar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="chkEnviarDireccionEditar" EventName="CheckedChanged" />
                    <asp:AsyncPostBackTrigger ControlID="chkEnviarCorreoEditar" EventName="CheckedChanged" />
                    <asp:AsyncPostBackTrigger ControlID="chkAdjuntarActoAdministrativoEditar" EventName="CheckedChanged" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="uppEditarEstado" runat="server" AssociatedUpdatePanelID="upnlEditarEstado">
                <ProgressTemplate>  
                    <div id="ModalProgressContainer">
                        <div>
                            <p>Procesando...</p>
                            <p><asp:Image ID="imgUpdateProgressEditarEstado" runat="server" SkinId="procesando"/></p>
                        </div>
                    </div>                         
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>



        <input type="button" runat="server" id="cmdVerDocumentosAdjuntosHide" style="display:none;" />
        <cc1:ModalPopupExtender ID="mpeVerDocumentosAdjuntos" runat="server" PopupControlID="dvVerDocumentosAdjuntos" TargetControlID="cmdVerDocumentosAdjuntosHide" BehaviorID="mpeVerDocumentosAdjuntos" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <div id="dvVerDocumentosAdjuntos" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalFormularioNot">
            <asp:UpdatePanel runat="server" ID="upnlVerDocumentosAdjuntos" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="TableResultadoModalNot">
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
                                                SkinID="GrillaNotificaciones" EmptyDataText="No se encontro información de documentos" ShowHeaderWhenEmpty="true" Width="90%">
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
                                                                <asp:ImageButton runat="server" ID="imgDescargarDocumentoAdjuntoVer" BorderWidth="0" ImageUrl="~/images/documento.png" CommandArgument='<%#Eval("RUTA_DOCUMENTO") %>' OnClick="imgDescargarDocumentoAdjuntoVer_Click" />
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
                                <asp:Button ID="cmdCerrarVerAdjuntos" runat="server" Text="Cerrar" CssClass="boton" CausesValidation="false" OnClick="cmdCerrarVerAdjuntos_Click"/>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmdCerrarVerAdjuntos" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="uppVerDocumentosAdjuntos" runat="server" AssociatedUpdatePanelID="upnlVerDocumentosAdjuntos">
                <ProgressTemplate>  
                    <div id="ModalProgressContainer">
                        <div>
                            <p>Procesando...</p>
                            <p><asp:Image ID="imgUpdateProgressVerDocumentosAdjuntos" runat="server" SkinId="procesando"/></p>
                        </div>
                    </div>                         
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>

        <input type="button" runat="server" id="cmdInformacionBloqueoHide" style="display:none;" />
        <cc1:ModalPopupExtender ID="mpeModalInformacionBloqueo" runat="server" PopupControlID="dvModalInformacionBloqueo" TargetControlID="cmdInformacionBloqueoHide" BehaviorID="mpeModalInformacionBloqueos" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <div id="dvModalInformacionBloqueo" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalFormularioNot">
            <asp:UpdatePanel runat="server" ID="upnlModalInformacionBloqueo" UpdateMode="Conditional">
                <ContentTemplate>
                
                    <div class="TableResultadoModalNot">
                        <div class="RowResultadoModalNot">
                            <div class="CellResultadoTituloModalNot">
                                BLOQUEO RADICACIÓN SIGPRO
                            </div>
                        </div>                            
                        <div class="RowResultadoModalNot">
                            <div class="CellResultadoModalNot">
                                <div class="TableFormularioNot">
                                    <div class="RowFormularioNot">
                                        <div class="CellFormularioNot">
                                            <div class="TableMensajesNot">
                                                <div class="RowMensajesNot">
                                                    <div class="CellMensajesNot"><asp:Image runat="server" ID="imgAdvertenciaInformacionBloqueo" ImageUrl="~/images/advertencia.png" /></div>
                                                    <div class="CellMensajesNot"><asp:Literal runat="server" ID="ltlMensajeInformacionBloqueo"></asp:Literal></div>
                                                </div>                                            
                                            </div>
                                        </div>
                                    </div>                                                                  
                                </div>
                            </div>
                        </div>
                        <div class="RowResultadoModalNot">
                            <div class="CellButtonModal">
                                <asp:Button ID="cmdModalInformacionBloqueoAceptar" runat="server" ClientIDMode="Static" Text="Aceptar" CssClass="boton" CausesValidation="false" OnClick="cmdModalInformacionBloqueoAceptar_Click" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmdModalInformacionBloqueoAceptar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="uppModalInformacionBloqueo" runat="server" AssociatedUpdatePanelID="upnlModalInformacionBloqueo">
                <ProgressTemplate>  
                    <div id="ModalProgressContainer">
                        <div>
                            <p>Procesando...</p>
                            <p><asp:Image ID="imgUpdateProgresModalInformacionBloqueo" runat="server" SkinId="procesando"/></p>
                        </div>
                    </div>                         
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>


        <asp:UpdateProgress ID="uppBarraProgresoBuscar" runat="server" AssociatedUpdatePanelID="upnlBuscar">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgUpdateProgress" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="uppBarraProgresoGrillaNotificacion" runat="server" AssociatedUpdatePanelID="upnlConsultaNotificaciones">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgUpdateProgressNotificacion" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
