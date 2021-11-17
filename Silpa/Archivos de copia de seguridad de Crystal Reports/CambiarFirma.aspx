<%@ Page Title="" Language="C#"  MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="CambiarFirma.aspx.cs" Inherits="PDV_CambiarFirma" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="FUA" Namespace="Subgurim.Controles" TagPrefix="cc2" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <%--<script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>--%>

    <link href="../jquery/jquery.keypad-2.1.0/css/jquery.keypad.css" rel="stylesheet" />
    <script src="../jquery/jquery.keypad-2.1.0/jquery-1.12.4/jquery.min.js"></script>
    <script src="../jquery/jquery.keypad-2.1.0/js/jquery.plugin.min.js"></script>
    <script src="../jquery/jquery.keypad-2.1.0/js/jquery.keypad.js"></script>

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

        #inlineKeypad { width: 10em; }
    </style>

    <link href="../App_Themes/skin/StyleSeguridad.css" rel="stylesheet" />
    <link href="../App_Themes/skin/AyudaStyle.css" rel="stylesheet" />
    
    <%--<script src="../jquery/jquery.js" type="text/javascript"></script>
    <link href="../jquery/keypad/jquery.keypad.css" rel="stylesheet" />
    <script src="../jquery/keypad/jquery.plugin.js" type="text/javascript"></script>
    <script src="../jquery/keypad/jquery.keypadmodal.js" type="text/javascript"></script>--%>

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

            $('#fluFirma').change(function () {
                $('#txtFirma').val($(this).val());
            });

            $('#fluLogo').change(function () {
                $('#txtLogo').val($(this).val());
            });

            $('#btnFirma').click(function () {
                $('#fluFirma').click();
            });

            $('#btnLogo').click(function () {
                $('#fluLogo').click();
            });

            $('#txtFirma').click(function () {
                $('#fluFirma').click();
            });

            $('#txtLogo').click(function () {
                $('#fluLogo').click();
            });
        });
    </script>

    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="CAMBIAR FIRMA" SkinID="titulo_principal_blanco" meta:resourcekey="lblTituloResource1"></asp:Label>
    </div>
    
    <asp:ScriptManager ID="scmManejador" runat="server" EnableScriptGlobalization="True"></asp:ScriptManager>

    <div class="table-responsive" id="tableMensaje" runat="server">
        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; width: 100%; border: 1px solid #CCCCCC !important;">
            <tr>
                <td class="CellMensaje" style="width: 100% !important; text-align: center !important;">
                    <asp:Label runat="server" ID="lblMensaje" meta:resourcekey="lblMensajeResource1"></asp:Label>
                </td>
            </tr>
        </table>
    </div>

    <div class="table-responsive">
        <table id="tableFirma" runat="server" style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
            <tr id="rowNombre" runat="server">
                <td>
                    <label id="lblTituloNombre" runat="server" for="txtNombre" meta:resourcekey="lblTituloNombreResource1" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Nombre Persona Autorizada:</label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtNombre" ClientIDMode="Static" MaxLength="140" meta:resourcekey="txtNombreResource1"></asp:TextBox>
                    <span id="spnNombre" runat="server" meta:resourcekey="spnNombreResource1" class="botonAyuda" title="Ingrese el nombre de la persona a la cual se encuentra referenciada la firma y que se encuentra autorizada para la firma de documentos."></span>
                </td>
                <td style="text-align: right; padding-right: 30px;">
                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvNombre" ControlToValidate="txtNombre" ValidationGroup="Firma" ErrorMessage="Debe ingresar el nombre de la persona autorizada" meta:resourcekey="rfvNombreResource1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <label runat="server" id="lblTituloCargo" for="txtCargo" meta:resourcekey="lblTituloCargoResource1" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Cargo Persona Autorizada:</label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtCargo" ClientIDMode="Static" MaxLength="150" meta:resourcekey="txtCargoResource1"></asp:TextBox>
                    <span id="spnCargo" runat="server" class="botonAyuda" meta:resourcekey="spnCargoResource1" title="Ingrese el cargo de la persona a la cual se encuentra referenciada la firma y que se encuentra autorizada para la firma de documentos."></span>
                </td>
                <td style="text-align: right; padding-right: 30px;">
                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvCargo" ControlToValidate="txtCargo" ValidationGroup="Firma" ErrorMessage="Debe ingresar el cargo de la persona autorizada" meta:resourcekey="rfvCargoResource1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <label runat="server" id="lblTituloFirma" for="fluFirma" meta:resourcekey="lblTituloFirmaResource1" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Imagen Firma Autorizada:</label>
                    <span id="spnFirma" runat="server" class="botonAyuda" meta:resourcekey="spnFirmaResource1" title="Seleccione la imagen de la firma que utilizará en los documentos. Esta imagen deberá encontrarse en formatos .bmp, .gif, .jpg, jpeg o png, y tener un tamaño de 300 x 60 pixeles."></span>
                </td>
                <td>&nbsp;</td>
                <td style="text-align: right; padding-right: 30px;">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left; vertical-align: top;">
                    <asp:Image runat="server" ID="imgFirma" meta:resourcekey="imgFirmaResource1" />
                </td>
                <td style="text-align: left; vertical-align: top;">
                    <input type="text" value="" readonly="readonly" id="txtFirma" />
                    <input type="button" runat="server" id="btnFirma" value="Seleccionar Archivo" clientidmode="Static" meta:resourcekey="btnFirmaResource1" />
                    <asp:FileUpload ID="fluFirma" runat="server" ClientIDMode="Static" CssClass="InputFile" meta:resourcekey="fluFirmaResource1" />
                </td>
                <td style="text-align: right; vertical-align: top; padding-right: 30px;">
                    <asp:RequiredFieldValidator ID="rfvFirma" runat="server" ErrorMessage="Debe seleccionar un archivo de Imagen para la firma autorizada" ControlToValidate="fluFirma" ValidationGroup="Firma" meta:resourcekey="rfvFirmaResource1">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator id="rexFirma" runat="server" ErrorMessage="Solo se permite imagenes en formato .bmp, .jpg, .jpeg y .png" Display="Dynamic" ValidationExpression="(.*).(.bmp|.BMP|.jpeg|JPEG|.jpg|.JPG|.png|.PNG)$" ControlToValidate="fluFirma" ValidationGroup="Firma" meta:resourcekey="rexFirmaResource1">*</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr id="rowLogoTitulo" runat="server">
                <td>
                    <label runat="server" for="fluFirma" id="lblLogo" meta:resourcekey="lblLogoResource1" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Logo Documentos:</label>
                    <span id="spnLogo" runat="server" meta:resourcekey="spnLogoResource1" class="botonAyuda" title="Seleccione el logo que desae utilizar en los documentos. Esta imagen deberá encontrarse en formatos .bmp, .gif, .jpg, jpeg o png, y tener un tamaño de 255 x 57 pixeles."></span>
                </td>
                <td>&nbsp;</td>
                <td style="text-align: right; padding-right: 30px;">&nbsp;</td>
            </tr>
            <tr id="rowLogo" runat="server">
                <td style="text-align: left; vertical-align: top;">
                    <asp:Image runat="server" ID="imgLogo" meta:resourcekey="imgLogoResource1" />
                </td>
                <td style="text-align: left; vertical-align: top;">
                    <input type="text" value="" readonly="readonly" id="txtLogo" />
                    <input type="button" runat="server" id="btnLogo" value="Seleccionar Archivo" clientidmode="Static" meta:resourcekey="btnLogoResource1" />
                    <asp:FileUpload ID="fluLogo" runat="server" ClientIDMode="Static" CssClass="InputFile" meta:resourcekey="fluLogoResource1" />
                </td>
                <td style="text-align: right; vertical-align: top; padding-right: 30px;">
                    <asp:RequiredFieldValidator ID="rfvLogo" runat="server" ErrorMessage="Debe seleccionar el logo a utilizar en los documentos" ControlToValidate="fluLogo" ValidationGroup="Firma" meta:resourcekey="rfvLogoResource1">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator id="rexLogo" runat="server" ErrorMessage="Solo se permite imagenes para el logo en formato .bmp, .jpg, jpeg y png" Display="Dynamic" ValidationExpression="(.*).(.bmp|.BMP|.jpeg|JPEG|.jpg|.JPG|.png|.PNG)$" ControlToValidate="fluLogo" ValidationGroup="Firma" meta:resourcekey="rexLogoResource1">*</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr id="rowContrasena" runat="server">
                <td>
                    <label runat="server" for="txtContrasena" id="lblSegundaContrasena" meta:resourcekey="lblSegundaContrasenaResource1" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Segunda Contraseña:</label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtContrasena" TextMode="Password" ClientIDMode="Static" MaxLength="6" meta:resourcekey="txtContrasenaResource1"></asp:TextBox>
                </td>
                <td style="text-align: right; padding-right: 30px;">
                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvContrasena" ControlToValidate="txtContrasena" ValidationGroup="Firma" ErrorMessage="Debe ingresar la segunda contraseña" meta:resourcekey="rfvContrasenaResource1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="padding: 20px; text-align: center; vertical-align: middle;">
                    <asp:Button runat="server" ID="cmdAceptar" Text="Aceptar" ClientIDMode="Static" OnClick="cmdAceptar_Click"  ValidationGroup="Firma" meta:resourcekey="cmdAceptarResource1" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="padding: 10px; text-align: left; vertical-align: middle;">
                    <asp:ValidationSummary ID="valFirma" runat="server" ValidationGroup="Firma" DisplayMode="BulletList" meta:resourcekey="valFirmaResource1" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

