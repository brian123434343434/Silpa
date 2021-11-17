<%@ Page Language="C#"  MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="EstadosFlujoNotificacion.aspx.cs" Inherits="Administracion_Notificacion_EstadosFlujoNotificacion" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>

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

    <link href="../../App_Themes/skin/AyudaStyle.css" rel="stylesheet" />
    <script src="../../jquery/jquery.js" type="text/javascript"></script>
    <script src="../../js/Ayuda.js" type="text/javascript"></script>
    <link href="css/AdministracionNotificacion.css" rel="stylesheet" />

    <script type="text/javascript">
        $(function () {

            $("#chkEsAceptacionNotificacion").click(function (e) {
                if ($("#chkEsAceptacionNotificacion").is(':checked')) {
                    $("#chkEsRechazoNotificacion").attr('checked', false);
                }
            });

            $("#chkEsRechazoNotificacion").click(function (e) {
                if ($("#chkEsRechazoNotificacion").is(':checked')) {
                    $("#chkEsAceptacionNotificacion").attr('checked', false);
                }
            });

            $("#chkEsAceptacionCitacion").click(function (e) {
                if ($("#chkEsAceptacionCitacion").is(':checked')) {
                    $("#chkEsRechazoCitacion").attr('checked', false);
                }
            });            

            $("#chkEsRechazoCitacion").click(function (e) {
                if ($("#chkEsRechazoCitacion").is(':checked')) {
                    $("#chkEsAceptacionCitacion").attr('checked', false);
                }
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {

                $("#chkEsAceptacionNotificacion").click(function (e) {
                    if ($("#chkEsAceptacionNotificacion").is(':checked')) {
                        $("#chkEsRechazoNotificacion").attr('checked', false);
                    }
                });

                $("#chkEsRechazoNotificacion").click(function (e) {
                    if ($("#chkEsRechazoNotificacion").is(':checked')) {
                        $("#chkEsAceptacionNotificacion").attr('checked', false);
                    }
                });

                $("#chkEsAceptacionCitacion").click(function (e) {
                    if ($("#chkEsAceptacionCitacion").is(':checked')) {
                        $("#chkEsRechazoCitacion").attr('checked', false);
                    }
                });

                $("#chkEsRechazoCitacion").click(function (e) {
                    if ($("#chkEsRechazoCitacion").is(':checked')) {
                        $("#chkEsAceptacionCitacion").attr('checked', false);
                    }
                });

            });

        });

    </script>

    <asp:ScriptManager ID="scmFlujo" runat="server"></asp:ScriptManager>

    <table class="TablaTituloSeccionAdmNot">
        <tr>
            <td class="div-titulo">
                <a href="#" id="hrfCerrarVentana" runat="server" onclick="window.close();return false;" class="Salir">Salir [X]</a>
            </td>
        </tr>
        <tr>
            <td class="div-titulo">
                <asp:Label ID="lblTitulo" runat="server" Text="ESTADOS FLUJOS NOTIFICACIÓN" SkinID="titulo_principal_blanco"></asp:Label>
            </td>
        </tr>
    </table>

    <asp:UpdatePanel runat="server" ID="upnlFlujo" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="TablaAdmNot">
                <tr>
                    <td colspan="2" class="TituloSeccionAdmNot">SELECCIONAR FLUJO</td>
                </tr>
                <tr>
                    <td class="LabelFormularioBusquedaAdmNot">
                        Autoridad Ambiental:
                    </td>
                    <td class="CamposFormularioBusquedaAdmNot">
                        <asp:DropDownList runat="server" ID="cboAutoridadBuscar" AutoPostBack="true" OnSelectedIndexChanged="cboAutoridadBuscar_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="LabelFormularioBusquedaAdmNot">
                        Flujo Notificación:
                    </td>
                    <td class="CamposFormularioBusquedaAdmNot">
                        <asp:DropDownList runat="server" ID="cboFlujo" AutoPostBack="true" OnSelectedIndexChanged="cboFlujo_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cboFlujo" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="upnlMensajes" UpdateMode="Conditional">
        <ContentTemplate>
            <table runat="server" visible="false" id="divMensaje" class="TablaMensajeErrorAdmNot" >
                <tr>
                    <td class="MensajeErrorAdmNot">
                        <asp:Literal runat="server" ID="lblMensaje"></asp:Literal>
                        <br />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
        
    <asp:UpdatePanel runat="server" ID="upnlResultadoEstados" UpdateMode="Conditional">
        <ContentTemplate>       
            <div class="table-responsive" id="divEstados" runat="server" visible="false">
                <table id="rowNuevoFlujo" runat="server" class="TablaAdmNot">
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="cmdNuevoEstado" Text="Adicionar Estado al Flujo" ClientIDMode="Static" CausesValidation="False" OnClick="cmdNuevoEstado_Click"/>                                
                        </td>
                    </tr>
                </table>
                <table class="TablaAdmNot">
                    <tr>
                        <td>
                            <asp:GridView runat="server" AutoGenerateColumns="False" ID="grdEstados" SkinID="GrillaAdministracionNotificacion" DataKeyNames="EstadoFlujoID" AllowPaging="True"
                                EmptyDataText="No existen estados para el flujo elegido" ShowHeaderWhenEmpty="True"                                    
                                PageSize="10" OnPageIndexChanging="grdEstados_PageIndexChanging" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText = "DESCRIPCIÓN ESTADO FLUJO">
                                        <ItemTemplate>
                                            <asp:Literal ID="lblDescripcionFlujo" runat="server" Text='<%# Eval("Descripcion") %>'></asp:Literal>                                        
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "NOMBRE ESTADO">
                                        <ItemTemplate>
                                            <asp:Literal ID="lblNombre" runat="server" Text='<%# Eval("Estado") %>'></asp:Literal>                                        
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "DESCRIPCIÓN ESTADO">
                                        <ItemTemplate>
                                            <asp:Literal ID="lblDescripcion" runat="server" Text='<%# Eval("EstadoDescripcion") %>'></asp:Literal>                                        
                                        </ItemTemplate>
                                    </asp:TemplateField>                                        
                                    <asp:TemplateField HeaderText = "ACTIVO" ItemStyle-CssClass="TextoFilaCentro">
                                        <ItemTemplate>
                                            <asp:Literal ID="lblActivo" runat="server" Text='<%# (Convert.ToBoolean(Eval("Activo")) ? "SI" : "NO" ) %>'></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "EDITAR" ItemStyle-CssClass="TextoFilaCentro">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEditarEstado" OnClick="lnkEditarEstado_Click" CommandArgument='<%# Eval("EstadoFlujoID")%>' Text="Editar"></asp:LinkButton>
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
            <asp:AsyncPostBackTrigger ControlID="cmdNuevoEstado" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="grdEstados" EventName="PageIndexChanging" />
        </Triggers>
    </asp:UpdatePanel> 
                 


    <input type="button" runat="server" id="cmdNuevoEstadoHide" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeCrearEstado" runat="server" PopupControlID="dvCrearEstado" TargetControlID="cmdNuevoEstadoHide" BehaviorID="mpeCrearEstados" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>    
    <div id="dvCrearEstado" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalFormularioNot">        
        <asp:UpdatePanel runat="server" ID="upnlFormulario" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="TablaFormularioAdmNot">
                    <tr>
                        <td colspan="2" class="TituloSeccionAdmNot">
                            <asp:Literal runat="server" ID="ltlTituloCrear"></asp:Literal>
                        </td>
                    </tr>
                    <tr runat="server" visible="false" id="divMensajeModal">
                        <td>
                            <table class="TablaMensajeErrorAdmNot" >
                                <tr>
                                    <td class="MensajeErrorAdmNot">
                                        <asp:Literal runat="server" ID="lblMensajeModal"></asp:Literal>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server" id="divEstadoFlujoLabel" visible="false">
                        <td class="ModalLabelFormularioAdmNot">
                            Estado:
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:Literal runat="server" ID="ltlEstadoFlujo"></asp:Literal>
                        </td>
                    </tr>
                    <tr runat="server" id="divEstadoFlujoDesplegable" visible="false">
                        <td class="ModalLabelFormularioAdmNot">
                            Estado:
                            <span id="spnEstado" class="botonAyudaUP" title="Estado que se relacionará al flujo seleccionado." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:DropDownList runat="server" ID="cboEstadoFlujo" OnSelectedIndexChanged="cboEstadoFlujo_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvEstadoFlujo" ControlToValidate="cboEstadoFlujo" InitialValue="-1" ValidationGroup="EstadoModal" ErrorMessage="Debe ingresar el estado que relacionará al flujo">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            Descripción:
                            <span id="spnDescripcion" class="botonAyudaUP" title="Descripción del estado en el flujo." divModal="dvCrearEstado"></span>                                     
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:TextBox runat="server" ID="txtDescripcion" ClientIDMode="Static" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvDescripcion" ControlToValidate="txtDescripcion" ValidationGroup="EstadoModal" ErrorMessage="Debe ingresar la descripción del estado">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            Número Días Vencimiento:
                            <span id="spnDias" class="botonAyudaUP" title="Número de días de vigencia del estado. Nogenerá cambio automático de estado." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">                            
                            <asp:TextBox runat="server" ID="txtDiasVencimiento" ClientIDMode="Static" MaxLength="4"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvDiasVencimiento" ControlToValidate="txtDiasVencimiento" ValidationGroup="EstadoModal" ErrorMessage="Debe ingresar el número de días de vencimiento">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="rexDiasVencimiento" runat="server" Display="Dynamic" ValidationGroup="EstadoModal" ErrorMessage="Ingrese un valor válido en los días de vencimiento" ControlToValidate="txtDiasVencimiento" ValidationExpression="\d+">*</asp:RegularExpressionValidator>
                        </td>
                    </tr>                    
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            Genera Plantilla:
                            <span id="spnGeneraPlantilla" class="botonAyudaUP" title="Indica al momento del avance del estado debe generarse documento en base a una plantilla." divModal="dvCrearEstado"></span>                                     
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkGeneraPlantilla" OnCheckedChanged="chkGeneraPlantilla_CheckedChanged" AutoPostBack="true" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr runat="server" id="divPlantilla" visible="false">
                        <td class="ModalLabelFormularioAdmNot">
                            Plantilla:
                            <span id="spnPlantilla" class="botonAyudaUP" title="Plantilla a utilizar para la generación del documento." divModal="dvCrearEstado"></span>                                     
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:DropDownList runat="server" ID="cboPlantilla"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvPlantilla" ControlToValidate="cboPlantilla" InitialValue="-1" ValidationGroup="EstadoModal" ErrorMessage="Debe ingresar la plantilla a utilizar">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            Permitir Entregar Acto Administrativo:
                            <span id="spnPermiteAnexarActo" class="botonAyudaUP" title="Indica si se muestra indicador de entrega de documento en el estado. Si en el estado se envía correo este se anexa como adjunto." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkPermiteAnexarActo" OnCheckedChanged="chkPermiteAnexarActo_CheckedChanged" AutoPostBack="true" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr runat="server" id="divPermiteAnexarConceptos">
                        <td class="ModalLabelFormularioAdmNot">
                            Permitir Entregar Conceptos del Acto Administrativo:
                            <span id="spnPermiteAnexarConceptos" class="botonAyudaUP" title="Indica si se muestra indicador de entrega de conceptos asociados al acto administrativo. Si en el estado se envía correo estos se anexa como un link de acceso." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkPermiteAnexarConceptos" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            ¿Documento Adicional?:
                            <span id="spnADocumentoAdicional" class="botonAyudaUP" title="Indica si el estado permite adjuntar un documento." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkADocumentoAdicional" ClientIDMode="Static" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            Envío de Correo Manual:
                            <span id="spnEnvioCorreoManual" class="botonAyudaUP" title="Indica si se desea al momento del avance envíar un correo electroníco." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkEnvioCorreoManual" OnCheckedChanged="chkEnvioCorreoManual_CheckedChanged" AutoPostBack="true" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr runat="server" id="divAdjuntos" visible="false">
                        <td class="ModalLabelFormularioAdmNot">
                            Anexa Adjuntos:
                            <span id="spnAnexaAdjunto" class="botonAyudaUP" title="Indica si al envíar el correo se permitiran adjuntos." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkAnexaAdjunto" OnCheckedChanged="chkAnexaAdjunto_CheckedChanged" AutoPostBack="true" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            Envío de Notificación Fisica:
                            <span id="spnNotificacionFisica" class="botonAyudaUP" title="Indica si la notificación desea realizarla por medio fisico." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkNotificacionFisica" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            Envía Correo Avance Automático:
                            <span id="spnEnvioCorreoAutomatico" class="botonAyudaUP" title="Indica si al momento del avance de manera automática se envía correo." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkEnvioCorreoAutomatico" OnCheckedChanged="chkEnvioCorreoAutomatico_CheckedChanged" AutoPostBack="true" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr runat="server" id="divTextoCorreo" visible="false">
                        <td class="ModalLabelFormularioAdmNot">
                            Texto Correo Automático:
                            <span id="spnTextoCorreoAutomatico" class="botonAyudaUP" title="Texto del correo envíado de manera automática." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:TextBox runat="server" ID="txtTextoCorreoAutomatico" ClientIDMode="Static" TextMode="MultiLine" Rows="10" Columns="35"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvTextoCorreoAutomatico" ControlToValidate="txtTextoCorreoAutomatico" ValidationGroup="EstadoModal" ErrorMessage="Debe ingresar el texto del correo a envíar">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr runat="server" id="divTipoAnexosCorreo">
                        <td class="ModalLabelFormularioAdmNot">
                            Tipo de Forma Adjuntar Archivos Correo:
                            <span id="spnTipoAnexosCorreo" class="botonAyudaUP" title="Indica la forma en la cual se deben adjuntar los archivos al correo." divModal="dvCrearEstado"></span> 
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:DropDownList runat="server" ID="cboTipoAnexosCorreo"></asp:DropDownList> 
                            <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvTipoAnexosCorreo" ControlToValidate="cboTipoAnexosCorreo" ValidationGroup="EstadoModal" ErrorMessage="Debe ingresar la forma en la cual se desean se manejen los anexos de los correos" InitialValue="-1">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>                    
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            Publica Estado:
                            <span id="spnPublicaEstado" class="botonAyudaUP" title="Indica si se realiza la publicación del estado." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkPublicaEstado" OnCheckedChanged="chkPublicaEstado_CheckedChanged" AutoPostBack="true" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr runat="server" id="divPublicaPlantilla" visible="false">
                        <td class="ModalLabelFormularioAdmNot">
                            Publica Plantilla:
                            <span id="spnPublicaPlantilla" class="botonAyudaUP" title="Indica si al publicar el estado se publica el documento generado." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkPublicaPlantilla" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr runat="server" id="divPublicaAdjunto" visible="false">
                        <td class="ModalLabelFormularioAdmNot">
                            Publica Adjunto:
                            <span id="spnPublicaAdjunto" class="botonAyudaUP" title="Indica si al publicar el estado se publica los documentos adjuntos enviados por correo." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkPublicaAdjunto" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            Solicitar Información Persona Notificada:
                            <span id="spnSolitarDatosPersonaNotificada" class="botonAyudaUP" title="Indica si se solicita la información de la persona que se esta notificando." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkSolitarDatosPersonaNotificada" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            Solicitar Referencia:
                            <span id="spnSolicitarConfirmacionNotificacion" class="botonAyudaUP" title="Indica si se solicita al momento del avance del estado una referencia." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkSolicitarConfirmacionNotificacion" OnCheckedChanged="chkSolicitarConfirmacionNotificacion_CheckedChanged" AutoPostBack="true" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr runat="server" id="divConfirmacionNotificacionObligatoria" visible="false">
                        <td class="ModalLabelFormularioAdmNot">
                            Referencia Obligatoria:
                            <span id="spnConfirmacionNotificacionObligatoria" class="botonAyudaUP" title="Indica si se la referencia a solicitar es obligatoria." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkConfirmacionNotificacionObligatoria" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            ¿Es Estado de Espera?:
                            <span id="spnEstadoEspera" class="botonAyudaUP" title="Indica si es un estado de espera de finalización de procesos." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkEstadoEspera" ClientIDMode="Static" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            ¿Es Estado de Notificación?:
                            <span id="spnEsNotificacion" class="botonAyudaUP" title="Indica si el estado es de notificación." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkEsNotificacion" ClientIDMode="Static" EnableTheming="false" />
                        </td>
                    </tr>                    
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            ¿Es Estado de Citacion?:
                            <span id="spnEsCitacion" class="botonAyudaUP" title="Indica si el estado es de citación." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkEsCitacion" ClientIDMode="Static" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            ¿Es Estado de Edicto?:
                            <span id="spnEsEdicto" class="botonAyudaUP" title="Indica si el estado es de edicto." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkEsEdicto" ClientIDMode="Static" EnableTheming="false" /> 
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            ¿Es Estado de Ejecutoria?:
                            <span id="spnEsEjecutoria" class="botonAyudaUP" title="Indica si el estado es ejecutoria." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkEsEjecutoria" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            ¿Es Estado de Aceptación de Notificación?:
                            <span id="spnEsAceptacionNotificacion" class="botonAyudaUP" title="Indica si el estado es de aceptación de un estado previo de notificación." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkEsAceptacionNotificacion" ClientIDMode="Static" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            ¿Es Estado de Rechazo de Notifiación?:
                            <span id="spnEsRechazoNotificacion" class="botonAyudaUP" title="Indica si el estado es de rechazo de un estado previo de notificación." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkEsRechazoNotificacion" ClientIDMode="Static" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            ¿Es Estado de Aceptación de Citación?:
                            <span id="spnEsAceptacionCitacion" class="botonAyudaUP" title="Indica si el estado es de aceptación de un estado previo de citación." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkEsAceptacionCitacion" ClientIDMode="Static" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            ¿Es Estado de Rechazo de Citación?:
                            <span id="spnEsRechazoCitacion" class="botonAyudaUP" title="Indica si el estado es de rechazo de un estado previo de citación." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">                            
                            <asp:CheckBox runat="server" ID="chkEsRechazoCitacion" ClientIDMode="Static" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            ¿Es Estado de Anulación de Notificación?:
                            <span id="spnEsAnulacion" class="botonAyudaUP" title="Indica si el estado es de anulación de notificación." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkEsAnulacion" ClientIDMode="Static" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            ¿Es Estado de Final de Publicidad?:
                            <span id="spnEsFinalPublicidad" class="botonAyudaUP" title="Indica si el estado es de final de publicidad." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkEsFinalPublicidad" ClientIDMode="Static" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            ¿Genera Recurso Reposición?:
                            <span id="spnGeneraRecurso" class="botonAyudaUP" title="Indica si el estado genera recurso de reposición." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:CheckBox runat="server" ID="chkGeneraRecurso" EnableTheming="false" />
                        </td>
                    </tr>
                    <tr runat="server" id="divEstadoRelacionado">
                        <td class="ModalLabelFormularioAdmNot">
                            Estado Flujo Relacionado:
                            <span id="spnEstadoRelacionado" class="botonAyudaUP" title="Indica el estado previo en el fluo con el cual se relaciona." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:DropDownList runat="server" ID="cboEstadoRelacionado"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr runat="server" id="divEstado" visible="false">
                        <td class="ModalLabelFormularioAdmNot">
                            Estado:
                            <span id="spnActivo" class="botonAyudaUP" title="Indica si el estado se encuentra Activo o Inactivo en el flujo." divModal="dvCrearEstado"></span>
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:DropDownList runat="server" ID="cboEstado">
                                <asp:ListItem Text="ACTIVO" Value="1"></asp:ListItem>
                                <asp:ListItem Text="INACTIVO" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table class="TablaBotonesFormularioAdmNot">
                    <tr>
                        <td>
                            <asp:ValidationSummary ID="valFirmaModal" runat="server" ValidationGroup="EstadoModal" ShowMessageBox="true" ShowSummary="false" />
                            <asp:HiddenField runat="server" ID="hdfEstadoFlujoID" />
                            <asp:HiddenField runat="server" ID="hdfEstadoID" />
                            <asp:Button ID="cmdGuardarEstado" runat="server" Text="Adicionar" CssClass="boton" ValidationGroup="EstadoModal" OnClick="cmdGuardarEstado_Click"/>
                            <asp:Button ID="cmdCancelar" runat="server" Text="Cancelar" CssClass="boton" OnClick="cmdCancelar_Click" CausesValidation="false"/>
                        </td>
                    </tr>
                </table>                                   
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="chkGeneraPlantilla" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="chkEnvioCorreoAutomatico" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="chkPublicaEstado" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="chkEnvioCorreoManual" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="chkAnexaAdjunto" EventName="CheckedChanged" />      
                <asp:AsyncPostBackTrigger ControlID="chkSolicitarConfirmacionNotificacion" EventName="CheckedChanged" />                                                                  
                <asp:AsyncPostBackTrigger ControlID="chkEnvioCorreoManual" EventName="CheckedChanged" />                                                                    
                <asp:AsyncPostBackTrigger ControlID="cboEstadoFlujo" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="cmdGuardarEstado" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="cmdCancelar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>     
    </div>
</asp:Content>
