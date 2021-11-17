<%@ Page Title="" Language="C#"  MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="CambiarSegundaClave.aspx.cs" Inherits="PDV_CambiarSegundaClave" meta:resourcekey="PageResource1" Theme="skin" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <div class='burbujaAyuda'></div>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../App_Themes/skin/StyleSeguridad.css" rel="stylesheet" /> 
    <link href="../App_Themes/skin/AyudaStyle.css" rel="stylesheet" />     
    <script src="../jquery/jquery.js" type="text/javascript"></script>    
    <link href="../jquery/keypad/jquery.keypad.css" rel="stylesheet" />
    <script src="../jquery/keypad/jquery.min.js" type="text/javascript"></script>
    <script src="../jquery/keypad/jquery.plugin.js" type="text/javascript"></script>
    <script src="../jquery/keypad/jquery.keypad.js" type="text/javascript"></script>
    <script src="../js/Ayuda.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('#txtContrasena').keypad({
                randomiseNumeric: true,
                prompt: '',
                closeText: '<asp:Literal runat="server" ID="ltlCerrarContrasena" Text="Cerrar" meta:resourcekey="ltlCerrarContrasenaResource1"></asp:Literal>',
                clearText: '<asp:Literal runat="server" ID="ltlLimpiarContrasena" Text="Limpiar" meta:resourcekey="ltlLimpiarContrasenaResource1"></asp:Literal>',
                backText: '<<'
            });
            $('#txtNuevaContrasena').keypad({
                randomiseNumeric: true,
                prompt: '',
                closeText: '<asp:Literal runat="server" ID="ltlCerrarNuevaContrasena" Text="Cerrar" meta:resourcekey="ltlCerrarNuevaContrasenaResource1"></asp:Literal>',
                clearText: '<asp:Literal runat="server" ID="ltlLimpiarNuevaContrasena" Text="Limpiar" meta:resourcekey="ltlLimpiarNuevaContrasena"></asp:Literal>',
                backText: '<<'
            });
            $('#txtConfirmacionContrasena').keypad({
                randomiseNumeric: true,
                prompt: '',
                closeText: '<asp:Literal runat="server" ID="ltlCerrarConfirmacionContrasena" Text="Cerrar" meta:resourcekey="ltlCerrarConfirmacionContrasenaResource1"></asp:Literal>',
                clearText: '<asp:Literal runat="server" ID="ltlLimpiarConfirmacionContrasena" Text="Limpiar" meta:resourcekey="ltlLimpiarConfirmacionContrasena"></asp:Literal>',
                backText: '<<'
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function() {

                $('#txtContrasena').keypad({
                    randomiseNumeric: true,
                    prompt: '',
                    closeText: '<asp:Literal runat="server" ID="ltlCerrarContrasenaUp" Text="Cerrar" meta:resourcekey="ltlCerrarContrasenaResource1"></asp:Literal>',
                    clearText: '<asp:Literal runat="server" ID="ltlLimpiarContrasenaUp" Text="Limpiar" meta:resourcekey="ltlLimpiarContrasenaResource1"></asp:Literal>',
                    backText: '<<'
                });
                $('#txtNuevaContrasena').keypad({
                    randomiseNumeric: true,
                    prompt: '',
                    closeText: '<asp:Literal runat="server" ID="ltlCerrarNuevaContrasenaUp" Text="Cerrar" meta:resourcekey="ltlCerrarNuevaContrasenaResource1"></asp:Literal>',
                    clearText: '<asp:Literal runat="server" ID="ltlLimpiarNuevaContrasenaUp" Text="Limpiar" meta:resourcekey="ltlLimpiarNuevaContrasena"></asp:Literal>',
                    backText: '<<'
                });
                $('#txtConfirmacionContrasena').keypad({
                    randomiseNumeric: true,
                    prompt: '',
                    closeText: '<asp:Literal runat="server" ID="ltlCerrarConfirmacionContrasenaUp" Text="Cerrar" meta:resourcekey="ltlCerrarConfirmacionContrasenaResource1"></asp:Literal>',
                    clearText: '<asp:Literal runat="server" ID="ltlLimpiarConfirmacionContrasenaUp" Text="Limpiar" meta:resourcekey="ltlLimpiarConfirmacionContrasena"></asp:Literal>',
                    backText: '<<'
                });

            });
        });
    </script>

    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="SEGUNDA CONTRASEÑA" SkinID="titulo_principal_blanco" meta:resourcekey="lblTituloResource1"></asp:Label>
    </div>

    <asp:ScriptManager ID="scmManejador" runat="server" EnableScriptGlobalization="True"></asp:ScriptManager>

        <asp:UpdatePanel runat="server" ID="upnForma">
            <ContentTemplate>
                <div class="div-contenido">
                    <div class="contact_form" runat="server" id="divFormulario">
                        <div class="Table">
                            <div class="Row" runat="server" id="rowMensaje">
                                <div class="CellMensaje">
                                    <asp:Label runat="server" ID="lblMensaje" meta:resourcekey="lblMensajeResource1"></asp:Label>
                                </div>
                            </div>
                            <div class="Row" id="rowContrasena" runat="server">
                                <div class="Cell">
                                    <label for="txtContrasena" runat="server" meta:resourcekey="txtContrasenaResource1">Contraseña Anterior:</label>
                                    <asp:TextBox runat="server" ID="txtContrasena" TextMode="Password" ClientIDMode="Static" MaxLength="6" meta:resourcekey="txtContrasenaResource1"></asp:TextBox>
                                    <span runat="server" id="spnContrasena" class="botonAyuda" title="Ingrese la segunda contraseña actual configurada." meta:resourcekey="spnContrasenaResource1"></span>
                                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvContrasena" ControlToValidate="txtContrasena" ValidationGroup="Contrasena" ErrorMessage="Debe ingresar la contraseña anterior" meta:resourcekey="rfvContrasenaResource1">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ValidationExpression="^[\s\S]{6,}$" ControlToValidate="txtContrasena" ErrorMessage="La contraseña debe tener seis (6) números" ValidationGroup="Contrasena" meta:resourcekey="RegularExpressionValidatorResource1">*</asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="Row">
                                <div class="Cell">
                                    <label for="txtNuevaContrasena" runat="server" meta:resourcekey="txtNuevaContrasenaResource1">Contraseña Nueva:</label>
                                    <asp:TextBox runat="server" ID="txtNuevaContrasena" TextMode="Password" ClientIDMode="Static" MaxLength="6" meta:resourcekey="txtNuevaContrasenaResource1"></asp:TextBox>
                                    <span runat="server" id="spnNuevaContrasena" class="botonAyuda" meta:resourcekey="spnNuevaContrasenaResource1" title="Ingrese la nueva segunda contraseña. Esta contraseña debe contener solo números y una longitud de seis (6) caracteres."></span>
                                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvNuevaContrasena" ControlToValidate="txtNuevaContrasena" ValidationGroup="Contrasena" ErrorMessage="Debe ingresar la nueva contraseña" meta:resourcekey="rfvNuevaContrasenaResource1">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^[\s\S]{6,}$" ControlToValidate="txtNuevaContrasena" ErrorMessage="La nueva contraseña debe tener seis (6) números" ValidationGroup="Contrasena" meta:resourcekey="RegularExpressionValidator1Resource1">*</asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="Row">
                                <div class="Cell">
                                    <label for="txtConfirmacionContrasena" runat="server" meta:resourcekey="txtConfirmacionContrasenaResource1">Confirmar Contraseña Nueva:</label>
                                    <asp:TextBox runat="server" ID="txtConfirmacionContrasena" TextMode="Password" ClientIDMode="Static" MaxLength="6" meta:resourcekey="txtConfirmacionContrasenaResource1"></asp:TextBox>
                                    <span runat="server" id="spnConfirmacionContrasena" meta:resourcekey="spnConfirmacionContrasenaResource1" class="botonAyuda" title="Ingrese la confirmación de la nueva segunda contraseña."></span>
                                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvConfirmacionContrasena" ControlToValidate="txtConfirmacionContrasena" ValidationGroup="Contrasena" ErrorMessage="Debe ingresar la confirmación de la nueva contraseña" meta:resourcekey="rfvConfirmacionContrasenaResource1">*</asp:RequiredFieldValidator>
                                    <asp:CompareValidator runat="server" Display="Dynamic" ID="cmvtxtContrasena" ControlToCompare="txtNuevaContrasena" ControlToValidate="txtConfirmacionContrasena" ValidationGroup="Contrasena" ErrorMessage="La nueva contraseña y su confirmación son diferentes" meta:resourcekey="cmvtxtContrasenaResource1">*</asp:CompareValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="^[\s\S]{6,}$" ControlToValidate="txtConfirmacionContrasena" ErrorMessage="La confirmación de la nueva contraseña debe tener seis (6) números" ValidationGroup="Contrasena" meta:resourcekey="RegularExpressionValidator2Resource1">*</asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="RowButton">
                                <div class="CellButton">
                                    <asp:HiddenField runat="server" ID="hdfOrigen" Value="" />
                                    <asp:Button runat="server" ID="cmdAceptar" Text="Aceptar" ClientIDMode="Static" OnClick="cmdAceptar_Click"  ValidationGroup="Contrasena" meta:resourcekey="cmdAceptarResource1" CssClass="boton" Width="100px" Height="35px" />
                                    <asp:Button runat="server" ID="cmdVolver" Text="Volver" ClientIDMode="Static" OnClick="cmdVolver_Click" CausesValidation="false" meta:resourcekey="cmdVolverResource1" CssClass="boton" Width="100px" Height="35px" Visible="false" />
                                </div>
                            </div>
                        </div>
                        <div class="Table">
                            <div class="Row">
                                <div class="Cell">
                                    <asp:ValidationSummary ID="valContrasena" runat="server" ValidationGroup="Contrasena" meta:resourcekey="valContrasenaResource1" />
                                </div>
                            </div>
                        </div>    
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel runat="server" ID="upnRespuesta">
            <ContentTemplate>
                <div class="div-contenido" runat="server" id="divRespuesta" visible="false">
                    <div class="contact_form_seg_resultado">
                        <div class="TableResultadoSeg">
                            <div class="RowBuscarTitulo">
                                <div class="CellResultadoTituloSeg">
                                    RESULTADO
                                </div>
                            </div>
                            <div class="Row">
                                <div class="CellResultadoSeg">
                                    <br />
                                    <label><asp:Literal runat="server" ID="ltlResultado"></asp:Literal></label>
                                    <br /><br />
                                </div>
                            </div>
                            <div class="RowButton">
                                <div class="CellButton">
                                    <asp:Button ID="cmdAceptarRespuesta" runat="server" Text="Aceptar" ClientIDMode="Static" OnClick="cmdAceptarRespuesta_Click" />
                                </div>
                            </div>
                        </div>                        
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdAceptarRespuesta" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
</asp:Content>

