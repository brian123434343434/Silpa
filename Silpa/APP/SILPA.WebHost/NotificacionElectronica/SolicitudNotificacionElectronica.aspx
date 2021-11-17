<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/plantillas/SILPASinMenuFlash.master" CodeFile="SolicitudNotificacionElectronica.aspx.cs" Inherits="NotificacionElectronica_SolicitudNotificacionElectronica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../App_Themes/skin/StyleNotificacion.css" rel="stylesheet" />
    <link href="../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <link href="../App_Themes/skin/AyudaStyle.css" rel="stylesheet" />
    <link href="../jquery/keypad/jquery.keypad.css" rel="stylesheet" />
    <script src="../jquery/jquery.js" type="text/javascript"></script>
    <script src="../jquery/EstiloGris/jquery-ui.js"  type="text/javascript"></script>    
    <script src="../js/Ayuda.js" type="text/javascript"></script>    
    <script src="../jquery/keypad/jquery.plugin.js" type="text/javascript"></script>
    <script src="../jquery/keypad/jquery.keypad.js" type="text/javascript"></script> 
    <script src="../js/SolicitudNotificacion.js"  type="text/javascript"></script>   
    <div class='burbujaAyuda'></div>
    <div class="div-titulo">
        <a href="#" id="hrfCerrarVentana" runat="server" onclick="window.close();return false;" class="Salir">Salir [X]</a>
        <br />
        <asp:Label ID="lblTititulo" runat="server" Text="SOLICITUD NOTIFICACIÓN ELECTRÓNICA" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="div-contenido">
        <asp:ScriptManager ID="scmNotificacion" runat="server"></asp:ScriptManager>
        <div class="contact_form_not" id="divMensajeError" runat="server">
            <div class="TableMensaje">
                <div class="Row">
                    <div class="CellMensaje">
                        <asp:Label runat="server" ID="lblMensajeError" Text="Para realizar la solicitud de notificación electrónica debe configurar su Segunda Contraseña.<br />Para registrar su segunda contraseña haga <a href='../Seguridad/CambiarSegundaClave.aspx?m=0&r=p'>clic aquí.</a>"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <asp:UpdatePanel runat="server" ID="upnNotificacion">
            <ContentTemplate>
                <div>
                    <div class="TableMensaje" id="tableMensaje" runat="server">
                        <div class="Row">
                            <div class="CellMensaje">
                                <asp:Label runat="server" ID="lblMensaje"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="contact_form_not" runat="server" id="divFormulario">            
                    <div class="Table">
                        <div class="Row">
                            <div class="Cell">
                                <label>
                                    Tipo Notificación:
                                    <span id="spanTipoNotificacion" class="botonAyudaUP" title="<b>Notificación Completa:</b> Se realiza notificación electrónica sobre todos los expedientes de las autoridades ambientales inscritas a este servicio en los cuales se tenga intervención.<br /><b>Notificación por Expediente:</b> Se realiza notificación electrónica sobre los expedientes que se encuentren en el listado de expedientes inscritos y sobre los cuales se tenga intervención.<br /><b>Sin Notificación Electrónica:</b> No se realiza notificación electrónica. La notificación de todos los expedientes sobre los cuales se tenga intervención es de la manera tradicional."></span>
                                </label>
                                <asp:RadioButton ID="RdbNotificarTodos" GroupName="TipoSolicitud" runat="server" OnCheckedChanged="TipoSolicitud_CheckedChanged" AutoPostBack="True" />
                                Notificación Completa
                                <asp:RadioButton ID="RdbNotificarExpediente" ClientIDMode="Static" runat="server" GroupName="TipoSolicitud" OnCheckedChanged="TipoSolicitud_CheckedChanged" AutoPostBack="True"/>
                                Notificación por Expediente
                                <asp:RadioButton ID="RdbNotificarPresencial" runat="server" GroupName="TipoSolicitud" OnCheckedChanged="TipoSolicitud_CheckedChanged" AutoPostBack="True"/>
                                Sin Notificación Electrónica
                            </div>
                        </div>
                        <div runat="server" id="rowExpedientes" class="Row">
                            <div class="Cell">
                                <label>
                                    Expedientes a Notificar:
                                    <span id="spnExpedientesNotificar" class="botonAyudaUP" title="Listado con los expedientes que desea notificar. Para adicionar un nuevo expediente haga clic en el botón de Asociar Expediente. Si desea retirar un expediente del listado haga clic sobre el nombre del expediente y luego haga clic en el botón de Desasociar Expediente"></span>
                                </label>
                                <div class="TableInterna">
                                    <div runat="server" id="rowSeleccionarTodosExpedientes" class="Row">
                                        <div class="Cell">
                                            <asp:CheckBox runat="server" ID="chkSeleccionarTodosExpedientes" ClientIDMode="Static" /> Seleccionar Todos los Expedientes
                                        </div>
                                    </div>
                                    <div class="Row">
                                        <div class="Cell">
                                            <asp:ListBox ID="lstExpedientesNotificar" runat="server" Rows="15" SelectionMode="Multiple" ClientIDMode="Static"></asp:ListBox>
                                            <asp:CustomValidator ID="cvExpedientesNotificar" Display="Dynamic" runat="server" ClientValidationFunction="ValExpedientes" ValidationGroup="Notificacion" ErrorMessage="Debe adicionar por lo menos un expediente al listado." CssClass="validator">*</asp:CustomValidator>
                                        </div>
                                    </div>
                                    <div class="RowButton" runat="server" id="rowBotonesExpedientes">
                                        <div class="CellButton">
                                            <input type="button" id="btnAdicionarExpediente" value="Asociar Expediente" class="button" />
                                            <asp:Button ID="btnEliminarExpediente" runat="server" Text="Desasociar Expediente" OnClick="btnEliminarExpediente_Click" />                                            
                                        </div>
                                    </div>
                                </div>                                
                            </div>
                        </div>
                        <div class="Row" id="rowContrasena" runat="server">
                            <div class="Cell">
                                <label runat="server" id="lblSegundaContrasena">
                                    Segunda Contraseña:
                                    <span id="spnContrasena" class="botonAyudaUP" title="Ingrese la segunda contraseña que configuró en la opción de Segunda Clave."></span>
                                </label>
                                <div class="TableInterna">
                                    <div class="Row">
                                        <div class="Cell">
                                            <asp:TextBox runat="server" ID="txtContrasena" TextMode="Password" ClientIDMode="Static" MaxLength="6"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvContrasena" ControlToValidate="txtContrasena" ValidationGroup="Notificacion" ErrorMessage="Debe ingresar la segunda contraseña">*</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>                                
                            </div>
                        </div>
                        <div class="Row">
                            <div class="Cell">
                                <label>
                                    Términos y Condiciones:
                                    <span id="spnTerminos" class="botonAyudaUP" title="Lea atentamente los Términos y Condiciones del servicio de notificación electrónica. Es indispensable que Acepte los Términos y Condiciones antes de guardar los cambios realizados a su configuración de notificación."></span>
                                </label>
                                <div class="TableInterna">
                                    <div class="Row">
                                        <div class="Cell">
                                            <asp:TextBox runat="server" ID="txtTerminos" ReadOnly="True" TextMode="MultiLine" Rows="10" meta:resourcekey="txtTerminosResource1"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="Row">
                                        <div class="Cell">                                    
                                            <asp:CheckBox runat="server" ID="chkAceptoTerminos" ClientIDMode="Static" /> He Leído y Acepto los Términos y Condiciones
                                            <asp:CustomValidator ID="cvAceptoTerminos" Display="Dynamic" runat="server" ClientValidationFunction="ValAceptoTerminos" ValidationGroup="Notificacion" ErrorMessage="Debe indicar si acepta los términos y condiciones del servicio de notificación electrónica." CssClass="validator">*</asp:CustomValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>            
                </div>
                <div class="TableButton" runat="server" id="divFormularioBotones">
                    <div class="RowButton">
                        <div class="CellButton">
                            <asp:Button ID="btnAceptar" runat="server" Text="Enviar" OnClick="btnAceptar_Click" ValidationGroup="Notificacion" />
                        </div>
                    </div>
                </div>                
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="RdbNotificarTodos" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="RdbNotificarExpediente" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="RdbNotificarPresencial" EventName="CheckedChanged" />                
                <asp:AsyncPostBackTrigger ControlID="btnEliminarExpediente" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnAceptar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="uppBotones" runat="server" AssociatedUpdatePanelID="upnNotificacion">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgUpdateProgress" runat="server" SkinId="procesando" /></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>        
        <div id="divCargarExpediente" style="display: none">
            <asp:UpdatePanel runat="server" ID="upnNotificacionModal">
                <ContentTemplate>
                    <div class="contact_form_not_modal">
                        <div class="Table">
                            <div class="Row">
                                <div class="Cell">
                                    <label>Autoridad Ambiental:</label>
                                    <asp:DropDownList id="cboAutoridadAmbiental" runat="server" ClientIDMode="Static" OnSelectedIndexChanged="cboAutoridadAmbiental_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvAutoridadAmbiental" Display="Dynamic" runat="server" ControlToValidate="cboAutoridadAmbiental" ValidationGroup="NotificacionModal" ErrorMessage="Debe Seleccionar la Autoridad Ambiental" InitialValue="-1">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="Row">
                                <div class="Cell">
                                    <label>Número Sila y Expediente:</label>
                                    <asp:DropDownList id="cboExpedientes" runat="server" ClientIDMode="Static"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvExpediente" Display="Dynamic" runat="server" ControlToValidate="cboExpedientes" ValidationGroup="NotificacionModal" ErrorMessage="Debe Seleccionar el Expediente" InitialValue="-1">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="TableButtonModal">
                        <div class="RowButton">
                            <div class="CellButton">
                                <asp:Button ID="btnAdicionarExpedienteModal" runat="server" Text="Aceptar" ClientIDMode="Static" OnClick="btnAdicionarExpedienteModal_Click" ValidationGroup="NotificacionModal" />
                                <asp:Button ID="btnCancelarExpedienteModal" runat="server" Text="Cancelar" ClientIDMode="Static" OnClick="btnCancelarExpedienteModal_Click" />
                            </div>
                        </div>
                        <div class="Row">
                            <div class="Cell">
                                <asp:ValidationSummary ID="valNotificacionModal" runat="server" ValidationGroup="NotificacionModal" />
                            </div>
                        </div>
                    </div>                    
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAdicionarExpedienteModal" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnCancelarExpedienteModal" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="cboAutoridadAmbiental" EventName="SelectedIndexChanged" />
            </Triggers>
            </asp:UpdatePanel>
        </div>        
        <asp:UpdatePanel runat="server" ID="upnRespuesta">
            <ContentTemplate>
                <div class="contact_form_not_resultado" runat="server" id="divRespuesta">
                    <div class="TableResultado">
                        <div class="RowBuscarTitulo">
                            <div class="CellResultadoTitulo">
                                RESULTADO
                            </div>
                        </div>
                        <div class="Row">
                            <div class="Cell">
                                Proceso Realizado <b>CORRECTAMENTE.</b><br /><br />
                                El número VITAL asignado a su proceso de solicitud de NOTIFICACIÓN es: <b><asp:Literal runat="server" ID="ltlNumeroVITAL"></asp:Literal></b>
                                <br /><br />
                            </div>
                        </div>
                        <div class="RowButton">
                        <div class="CellButton">
                            <asp:Button ID="btnAceptarRespuesta" runat="server" Text="Aceptar" ClientIDMode="Static" OnClick="btnAceptarRespuesta_Click" />
                        </div>
                    </div>
                    </div>                        
                    </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAceptarRespuesta" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        
        <asp:UpdatePanel runat="server" ID="upnlErrores">
            <ContentTemplate>
                <div class="Table">
                    <div class="Row">
                        <div class="Cell">
                            <asp:ValidationSummary ID="valNotificacion" runat="server" ValidationGroup="Notificacion" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>    
</asp:Content>

