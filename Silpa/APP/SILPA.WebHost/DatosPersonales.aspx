<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPASinMenuFlash.master" AutoEventWireup="true"
    CodeFile="DatosPersonales.aspx.cs" Inherits="DatosPersonales" Title="Datos Personales"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="cphLoginHead" ContentPlaceHolderID="headPlaceHolder" runat="server">
    <script src="jquery/jquery.js" type="text/javascript"></script>                 
    <script type="text/javascript">
        var key = '<%= Recaptcha.LlavePublica %>';
        var validarCaptcha = '<%= this.ValidarRecaptcha %>';

        if (validarCaptcha === "true") {
            var onloadCallback = function () {
                grecaptcha.render('loginRecaptcha', {
                    'sitekey': key,
                    'type': 'image'
                });
            };
        }

        function CaptchaSelectionValidation(source, arguments) {
            var googleResponse = $('#g-recaptcha-response').val();

            if (googleResponse == '') {
                arguments.IsValid = false;
            }
            else {
                arguments.IsValid = true;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="MIS DATOS PERSONALES" SkinID="titulo_principal_blanco"></asp:Label>
        &nbsp;
        <a href="#" id="hrCerrarVentana" visible="false" runat="server" onclick="window.close();return false;">Salir</a>
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="div-contenido">
        <asp:UpdatePanel runat="server" ID="upnlFormularioPersona" UpdateMode="Conditional">
            <ContentTemplate>
            
        
                <cc1:TabContainer ID="tbcContenedor" runat="server" Style="margin-right: 19px" Width="90%"
                    ActiveTabIndex="2">
                    <cc1:TabPanel runat="server" HeaderText="Datos de Usuario" ID="tabDatosUsuario">
                        <ContentTemplate>
                            <table style="width: 70%;">
                                <tr>
                                    <td align="left" colspan="2" style="text-align: justify">
                                        <asp:Label ID="lblMensaje" runat="server" SkinID="etiqueta_verde" Style="text-align: justify"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTipoUsuario" runat="server" SkinID="etiqueta_negra" Text="Tipo de Usuario:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="optTipoUsuario" runat="server" AutoPostBack="True" Width="70%"
                                            OnSelectedIndexChanged="optTipoUsuario_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="natural">Persona Natural</asp:ListItem>
                                            <asp:ListItem Value="juridica publica">Persona Jur&#237;dica P&#250;blica</asp:ListItem>
                                            <asp:ListItem Value="juridica privada">Persona Jur&#237;dica Privada</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblAutoridadAmbiental" runat="server" SkinID="etiqueta_negra" Text="Autoridad Ambiental a la que desea enviar su solicitud:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboAutoridadAmbiental" runat="server" SkinID="lista_desplegable">
                                        </asp:DropDownList>
                                        <asp:CompareValidator ID="covAutoridadAmbiental" runat="server" ControlToValidate="cboAutoridadAmbiental"
                                            Display="Dynamic" ErrorMessage="Datos de Usuario: Seleccione la Autoridad Ambiental" Operator="NotEqual"
                                            ValueToCompare="-1">*</asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="height: 73px">&nbsp;&nbsp;</td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="Datos Persona Natural" ID="tabDatosNatural">
                        <%--                <HeaderTemplate>
                            Datos Persona Natural
                        </HeaderTemplate>--%>
                        <HeaderTemplate>
                            Datos Persona Natural
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table style="width: 90%">
                                <tr>
                                    <td class="titleUpdate" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td colspan="2">Datos Personales
                                    </td>
                                </tr>
                                <tr>
                                    <td class="titleUpdate" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPrimerNombreNatural" runat="server" SkinID="etiqueta_negra"
                                            Text="Primer Nombre:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPrimerNombreNatural" runat="server" MaxLength="30"
                                            SkinID="texto"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPrimerNombreNatural" runat="server"
                                            ControlToValidate="txtPrimerNombreNatural"
                                            ErrorMessage="Datos Persona Natural: Ingrese Primer Nombre">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revPrimerNombreNatural" runat="server"
                                            ControlToValidate="txtPrimerNombreNatural" Display="Dynamic"
                                            ErrorMessage="Datos Persona Natural: No se admiten numeros, caracteres especiales o espacios en el campo Primer Nombre"
                                            ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚÑñ]{1,30}$">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSegundoNombreNatural" runat="server" SkinID="etiqueta_negra"
                                            Text="Segundo Nombre:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSegundoNombreNatural" runat="server" MaxLength="30"
                                            SkinID="texto"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revSegundoNombreNatural" runat="server"
                                            ControlToValidate="txtSegundoNombreNatural" Display="Dynamic"
                                            ErrorMessage="Datos Persona Natural: No se admiten numeros o caracteres especiales en el campo Segundo Nombre"
                                            ValidationExpression="^^[a-zA-Z\sáéíóúÁÉÍÓÚÑñ]{1,30}$">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPrimerApellidoNatural" runat="server" SkinID="etiqueta_negra"
                                            Text="Primer Apellido:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPrimerApellidoNatural" runat="server" MaxLength="30"
                                            SkinID="texto"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPrimerApellidoNatural" runat="server"
                                            ControlToValidate="txtPrimerApellidoNatural"
                                            ErrorMessage="Datos Persona Natural: Ingrese Primer Apellido">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revPrimerApellidoNatural" runat="server"
                                            ControlToValidate="txtPrimerApellidoNatural" Display="Dynamic"
                                            ErrorMessage="Datos Persona Natural: No se admiten numeros, caracteres especiales o espacios en el campo  Primer Apellido"
                                            ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚÑñ]{1,30}$">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSegundoApellidoNatural" runat="server"
                                            SkinID="etiqueta_negra" Text="Segundo Apellido:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSegundoApellidoNatural" runat="server" MaxLength="30"
                                            SkinID="texto"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revSegundoApellidoNatural" runat="server"
                                            ControlToValidate="txtSegundoApellidoNatural" Display="Dynamic"
                                            ErrorMessage="Datos Persona Natural: No se admiten numeros o caracteres especiales en el campo Segundo Apellido"
                                            ValidationExpression="^[a-zA-Z\sáéíóúÁÉÍÓÚÑñ]{1,30}$">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTipoDocumento" runat="server" SkinID="etiqueta_negra"
                                            Text="Tipo de Documento:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboTipoDocumento" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="cboTipoDocumento_SelectedIndexChanged"
                                            SkinID="lista_desplegable">
                                        </asp:DropDownList>
                                        <asp:CompareValidator ID="covTipoDocumento" runat="server"
                                            ControlToValidate="cboTipoDocumento" Display="Dynamic"
                                            ErrorMessage="Datos Persona Natural: Seleccione Tipo de Documento"
                                            Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNumeroIdentificacion" runat="server" SkinID="etiqueta_negra"
                                            Text="Número de Documento:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNumeroIdentificacion" runat="server" MaxLength="11"
                                            SkinID="texto"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNumeroIdentificacionNatural" runat="server"
                                            ControlToValidate="txtNumeroIdentificacion"
                                            ErrorMessage="Datos Persona Natural: Ingrese Número de Documento">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revNumeroIdentificacionNatural"
                                            runat="server" ControlToValidate="txtNumeroIdentificacion" Display="Dynamic"
                                            ErrorMessage="Datos Persona Natural: Formato no válido en el número del documento"
                                            ValidationExpression="\d{4,11}">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Label ID="lblOrigenIdentificacion" runat="server" SkinID="etiqueta_negra"
                                            Text="Departamento Origen de Documento:"></asp:Label>
                                    </td>
                                    <td valign="top">
                                        <asp:DropDownList ID="cboDepartamentoOrigenNatural" runat="server"
                                            AutoPostBack="True"
                                            OnSelectedIndexChanged="cboDepartamentoOrigenNatural_SelectedIndexChanged"
                                            SkinID="lista_desplegable">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Lbl2222" runat="server" SkinID="etiqueta_negra"
                                            Text="Municipio Origen de Documento:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="updMunicipioOrigenNatural" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cboMunicipioOrigenNatural" runat="server"
                                                    SkinID="lista_desplegable">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboDepartamentoOrigenNatural"
                                                    EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="titleUpdate" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td colspan="2">Datos Para Contacto
                                    </td>
                                </tr>
                                <tr>
                                    <td class="titleUpdate" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDireccionNatural" runat="server" SkinID="etiqueta_negra"
                                            Text="Dirección:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDireccionNatural" runat="server" MaxLength="100"
                                            SkinID="texto"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDireccionNatural" runat="server"
                                            ControlToValidate="txtDireccionNatural"
                                            ErrorMessage="Datos Persona Natural: Ingrese dirección de contacto">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPaisNatural" runat="server" SkinID="etiqueta_negra"
                                            Text="País:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboPaisNatural" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="cboPaisNatural_SelectedIndexChanged"
                                            SkinID="lista_desplegable">
                                        </asp:DropDownList>
                                        <asp:CompareValidator ID="covPaisNatural" runat="server"
                                            ControlToValidate="cboPaisNatural" Display="Dynamic"
                                            ErrorMessage="Datos Persona Natural: Seleccione el País de contacto"
                                            Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDepartamentoNatural" runat="server" SkinID="etiqueta_negra"
                                            Text="Departamento:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="updDepartamentoNatural" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cboDepartamentoNatural" runat="server"
                                                    __designer:wfdid="w128" AutoPostBack="true"
                                                    OnSelectedIndexChanged="cboDepartamentoNatural_SelectedIndexChanged"
                                                    SkinID="lista_desplegable">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboPaisNatural"
                                                    EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMunicipioNatural" runat="server" SkinID="etiqueta_negra"
                                            Text="Municipio:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="updMunicipioNatural" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cboMunicipioNatural" runat="server"
                                                    __designer:wfdid="w132" AutoPostBack="true"
                                                    OnSelectedIndexChanged="cboMunicipioNatural_SelectedIndexChanged"
                                                    SkinID="lista_desplegable">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboDepartamentoNatural"
                                                    EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCorregimientoNatural" runat="server" SkinID="etiqueta_negra"
                                            Text="Corregimiento:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="updCorregimientoNatural" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cboCorregimientoNatural" runat="server"
                                                    __designer:wfdid="w136" AutoPostBack="true"
                                                    OnSelectedIndexChanged="cboCorregimientoNatural_SelectedIndexChanged"
                                                    SkinID="lista_desplegable">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboMunicipioNatural"
                                                    EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblVeredaNatural" runat="server" SkinID="etiqueta_negra"
                                            Text="Vereda:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="updVeredaNatural" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cboVeredaNatural" runat="server" __designer:wfdid="w139"
                                                    SkinID="lista_desplegable">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboCorregimientoNatural"
                                                    EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="titleUpdate" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDireccionCorrespondencia" runat="server"
                                            SkinID="etiqueta_negra" Text="Dirección Correspondencia:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDireccionCorrespondencia" runat="server" MaxLength="100"
                                            SkinID="texto"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDireccionCorrespondencia" runat="server"
                                            ControlToValidate="txtDireccionCorrespondencia"
                                            ErrorMessage="Datos Persona Natural: Ingrese dirección de correspondencia">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPaisCorrespondencia" runat="server" SkinID="etiqueta_negra"
                                            Text="País:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboPaisCorrespondencia" runat="server"
                                            AutoPostBack="True"
                                            OnSelectedIndexChanged="cboPaisCorrespondencia_SelectedIndexChanged"
                                            SkinID="lista_desplegable">
                                        </asp:DropDownList>
                                        <asp:CompareValidator ID="covPaisCorrespondencia" runat="server"
                                            ControlToValidate="cboPaisCorrespondencia" Display="Dynamic"
                                            ErrorMessage="Datos Persona Natural: Seleccione el País de correspondecia"
                                            Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDepartamentoCorrespondencia" runat="server"
                                            SkinID="etiqueta_negra" Text="Departamento:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="updDepartamentoCorrespondencia" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cboDepartamentoCorrespondencia" runat="server"
                                                    __designer:wfdid="w148" AutoPostBack="true"
                                                    OnSelectedIndexChanged="cboDepartamentoCorrespondencia_SelectedIndexChanged"
                                                    SkinID="lista_desplegable">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboPaisCorrespondencia"
                                                    EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMunicipioCorrespondencia" runat="server"
                                            SkinID="etiqueta_negra" Text="Municipio:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="updMunicipioCorrespondencia" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cboMunicipioCorrespondencia" runat="server"
                                                    __designer:wfdid="w152" AutoPostBack="true"
                                                    OnSelectedIndexChanged="cboMunicipioCorrespondencia_SelectedIndexChanged"
                                                    SkinID="lista_desplegable">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboDepartamentoCorrespondencia"
                                                    EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCorregimientoCorrespondencia" runat="server"
                                            SkinID="etiqueta_negra" Text="Corregimiento:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="updCorregimientoCorrespondencia" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cboCorregimientoCorrespondencia" runat="server"
                                                    __designer:wfdid="w156" AutoPostBack="true"
                                                    OnSelectedIndexChanged="cboCorregimientoCorrespondencia_SelectedIndexChanged"
                                                    SkinID="lista_desplegable">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboMunicipioCorrespondencia"
                                                    EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblVeredaCorrespondencia" runat="server" SkinID="etiqueta_negra"
                                            Text="Vereda:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="updVeredaCorrespondencia" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cboVeredaCorrespondencia" runat="server"
                                                    __designer:wfdid="w159" SkinID="lista_desplegable">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboCorregimientoCorrespondencia"
                                                    EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="titleUpdate" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTelefonoNatural" runat="server" SkinID="etiqueta_negra"
                                            Text="Teléfono"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTelefonoNatural" runat="server" MaxLength="10"
                                            SkinID="texto"></asp:TextBox>
                                        &nbsp;&nbsp;<asp:RegularExpressionValidator ID="revTelefonoNatural" runat="server"
                                            ControlToValidate="txtTelefonoNatural" Display="Dynamic"
                                            ErrorMessage="Datos Persona Natural: Telefono no tiene el formato correcto. No se deben incluir indicativos ni caracteres distintos a números"
                                            ValidationExpression="\d{7,10}">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCelularNatural" runat="server" SkinID="etiqueta_negra"
                                            Text="Celular:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCelularNatural" runat="server" MaxLength="13"
                                            SkinID="texto"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revTelefonoCelular" runat="server"
                                            ControlToValidate="txtCelularNatural" Display="Dynamic"
                                            ErrorMessage="Datos Persona Natural: Telefono celular no tiene el formato correcto"
                                            ValidationExpression="\d{10,13}">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblFaxNatural" runat="server" SkinID="etiqueta_negra"
                                            Text="Fax:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFaxNatural" runat="server" MaxLength="10" SkinID="texto"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revTelefonoFAX" runat="server"
                                            ControlToValidate="txtFaxNatural" Display="Dynamic"
                                            ErrorMessage="Datos Persona Natural: FAX no tiene el formato correcto"
                                            ValidationExpression="\d{7,10}">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 26px">
                                        <asp:Label ID="lblCorreoNatural" runat="server" SkinID="etiqueta_negra"
                                            Text="Correo Electrónico:"></asp:Label>
                                    </td>
                                    <td style="height: 26px">
                                        <asp:TextBox ID="txtCorreoNatural" runat="server" SkinID="texto"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCorreoNatural" runat="server"
                                            ControlToValidate="txtCorreoNatural" Display="Dynamic"
                                            ErrorMessage="Datos Persona Natural: Ingrese Correo Electrónico">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revCorreoNatural" runat="server"
                                            ControlToValidate="txtCorreoNatural" Display="Dynamic"
                                            ErrorMessage="Datos Persona Natural: Formato no válido para el correo"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: left; vertical-align: top;">
                                        <asp:CheckBox  SkinID="etiqueta_negra" ID="ChkAutorizaNotifPerNatural" runat="server"/>
                                        <asp:Label ID="LblAutorizaNotifPerNatural" runat="server" SkinID="etiqueta_negra" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="Datos Persona Jur&#237;dica P&#250;blica"
                        ID="tabDatosJuridicaPublica">
                        <ContentTemplate>
                            <table style="width: 70%;">
                                <tr>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblRazonSocialJuridica" runat="server" SkinID="etiqueta_negra"
                                                        Text="Razon Social:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRazonSocialJuridica" runat="server" MaxLength="256"
                                                        SkinID="texto"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvRazonSocialJuridica" runat="server"
                                                        ControlToValidate="txtRazonSocialJuridica" Display="Dynamic"
                                                        ErrorMessage="Datos Persona Jurídica Pública: Ingrese Razón Social">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTipoDocumentoJuridica" runat="server" SkinID="etiqueta_negra"
                                                        Text="Tipo de Documento:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cboTipoDocumentoJuridica" runat="server"
                                                        SkinID="lista_desplegable">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="covTipoDocumentoJuridica" runat="server"
                                                        ControlToValidate="cboTipoDocumentoJuridica" Display="Dynamic"
                                                        ErrorMessage="Datos Persona Jurídica Pública: Seleccione Tipo de Documento"
                                                        Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblNumeroDocumentoJuridica" runat="server"
                                                        SkinID="etiqueta_negra" Text="Número de Documento:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNumeroDocumentoJuridica" runat="server" MaxLength="10"
                                                        SkinID="texto"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvNumeroDocumentoJuridica" runat="server"
                                                        ControlToValidate="txtNumeroDocumentoJuridica" Display="Dynamic"
                                                        ErrorMessage="Datos Persona Jurídica Pública: Ingrese el numero del documento de la Razón Social">*</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revNumeroDocumentoJuridica" runat="server"
                                                        ControlToValidate="txtNumeroDocumentoJuridica" Display="Dynamic"
                                                        ErrorMessage="Datos Persona Jurídica Pública: Formato no válido para el documento de la Razón Social"
                                                        ValidationExpression="\d{6,10}">*</asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="titleUpdate" colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">Datos Para Contacto
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="titleUpdate" colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDireccionJuridica" runat="server" SkinID="etiqueta_negra"
                                                        Text="Dirección:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDireccionJuridica" runat="server" MaxLength="100"
                                                        SkinID="texto"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvDireccionJuridica" runat="server"
                                                        ControlToValidate="txtDireccionJuridica" Display="Dynamic"
                                                        ErrorMessage="Datos Persona Jurídica Pública: Ingrese dierccion de la Razon Social">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblPaisJuridica" runat="server" SkinID="etiqueta_negra"
                                                        Text="País:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cboPaisJuridica" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="cboPaisJuridica_SelectedIndexChanged"
                                                        SkinID="lista_desplegable">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="covPaisJuridica" runat="server"
                                                        ControlToValidate="cboPaisJuridica" Display="Dynamic"
                                                        ErrorMessage="Datos Persona Jurídica Pública: Seleccione el País"
                                                        Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDepartamentoJuridica" runat="server" SkinID="etiqueta_negra"
                                                        Text="Departamento:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="updDepartamentoJuridica" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList
                                                                ID="cboDepartamentoJuridica" runat="server" __designer:wfdid="w316"
                                                                AutoPostBack="True"
                                                                OnSelectedIndexChanged="cboDepartamentoJuridica_SelectedIndexChanged"
                                                                SkinID="lista_desplegable">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="cboPaisJuridica"
                                                                EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblMunicipioJuridica" runat="server" SkinID="etiqueta_negra"
                                                        Text="Municipio:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="updMunicipioJuridica" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList
                                                                ID="cboMunicipioJuridica" runat="server" __designer:wfdid="w320"
                                                                AutoPostBack="True"
                                                                OnSelectedIndexChanged="cboMunicipioJuridica_SelectedIndexChanged"
                                                                SkinID="lista_desplegable">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="cboDepartamentoJuridica"
                                                                EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCorregimientoJuridica" runat="server" SkinID="etiqueta_negra"
                                                        Text="Corregimiento:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="updCorregimientoJuridica" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList
                                                                ID="cboCorregimientoJuridica" runat="server" __designer:wfdid="w324"
                                                                AutoPostBack="True"
                                                                OnSelectedIndexChanged="cboCorregimientoJuridica_SelectedIndexChanged"
                                                                SkinID="lista_desplegable">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="cboMunicipioJuridica"
                                                                EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblVeredaJuridica" runat="server" SkinID="etiqueta_negra"
                                                        Text="Vereda:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="updVeredaJuridica" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList
                                                                ID="cboVeredaJuridica" runat="server" __designer:wfdid="w327"
                                                                SkinID="lista_desplegable">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="cboCorregimientoJuridica"
                                                                EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTelefonoJuridica" runat="server" SkinID="etiqueta_negra"
                                                        Text="Teléfono"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTelefonoJuridica" runat="server" MaxLength="10"
                                                        SkinID="texto"></asp:TextBox>
                                                    &nbsp;
                                                    <asp:RegularExpressionValidator ID="revTelefonoJuridica" runat="server"
                                                        ControlToValidate="txtTelefonoJuridica" Display="Dynamic"
                                                        ErrorMessage="Datos Persona Jurídica Pública: Telefono no tiene el formato correcto"
                                                        ValidationExpression="\d{7,10}">*</asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCelularJuridica" runat="server" SkinID="etiqueta_negra"
                                                        Text="Celular:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCelularJuridica" runat="server" MaxLength="13"
                                                        SkinID="texto"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="revTelefonoCelularJuridica" runat="server"
                                                        ControlToValidate="txtCelularJuridica" Display="Dynamic"
                                                        ErrorMessage="Datos Persona Jurídica Pública: Telefono celular no tiene el formato correcto"
                                                        ValidationExpression="\d{10,13}">*</asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblFaxJuridica" runat="server" SkinID="etiqueta_negra"
                                                        Text="Fax:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFaxJuridica" runat="server" MaxLength="10" SkinID="texto"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="revTelefonoFAXJuridica" runat="server"
                                                        ControlToValidate="txtFaxJuridica" Display="Dynamic"
                                                        ErrorMessage="Datos Persona Jurídica Pública: FAX no tiene el formato correcto"
                                                        ValidationExpression="\d{7,10}">*</asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCorreoJuridica" runat="server" SkinID="etiqueta_negra"
                                                        Text="Correo:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCorreoJuridica" runat="server" SkinID="texto"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvCorreoJuridica" runat="server"
                                                        ControlToValidate="txtCorreoJuridica"
                                                        ErrorMessage="Datos Persona Jurídica Pública: Ingrese Correo de Razón Social">*</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revCorreoJuridica" runat="server"
                                                        ControlToValidate="txtCorreoJuridica" Display="Dynamic"
                                                        ErrorMessage="Datos Persona Jurídica Pública: Formato no válido para el correo de Razón Social"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="text-align: left; vertical-align: top;">
                                                    <asp:CheckBox ID="ChkAutorizaNotifPerJuridica" SkinID="etiqueta_negra"  runat="server" />
                                                    <asp:Label ID="LblAutorizaNotifPerJuridica" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="titleUpdate"></td>
                                </tr>
                                <tr>
                                    <td>Datos Representante Legal
                                    </td>
                                </tr>
                                <tr>
                                    <td class="titleUpdate"></td>
                                </tr>
                                <tr>
                                    <td style="width: 803px">
                                        <asp:Panel ID="pnlRepresentante" runat="server" Height="170px" ScrollBars="Both"
                                            Width="800px" Style="width: 800px;">
                                            <asp:GridView ID="grdRepresentantes" runat="server" OnRowCommand="grdRepresentantes_RowCommand"
                                                AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:ButtonField CommandName="Actualizar" Text="Ver" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnSubir" runat="server"
                                                                Text='<%# DoSomething(Eval("PER_ID").ToString()) %>'
                                                                CommandName="Eliminar" ValidationGroup="ning"
                                                                CommandArgument='<%# Container.DataItemIndex %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PRIMER_NOMBRE" HeaderText="Primer Nombre"></asp:BoundField>
                                                    <asp:BoundField DataField="SEGUNDO_NOMBRE" HeaderText="Segundo Nombre"></asp:BoundField>
                                                    <asp:BoundField DataField="PRIMER_APELLIDO" HeaderText="Primer Apellido"></asp:BoundField>
                                                    <asp:BoundField DataField="SEGUNDO_APELLIDO" HeaderText="Segundo Apellido"></asp:BoundField>
                                                    <asp:BoundField DataField="TARJETA_PROFESIONAL" HeaderText="Tarjeta Profesional"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_TIPO_ID" HeaderText="Tipo de Documento" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="TIPO_ID" HeaderText="Tipo de Documento"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_IDENTIFICACION" HeaderText="N&#250;mero de Documento"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_ORIGEN_DEPARTAMENTO" HeaderText="Departamento Origen"
                                                        Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="ORIGEN_DEPARTAMENTO" HeaderText="Departamento Origen"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_ORIGEN_MUNICIPIO" HeaderText="Municipio Origen" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="ORIGEN_MUNICIPIO" HeaderText="Municipio Origen"></asp:BoundField>
                                                    <asp:BoundField DataField="DIRECCION_CORRESPONDENCIA" HeaderText="Direcci&#243;n de Correspondencia"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_PAIS" HeaderText="Pa&#237;s" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="PAIS" HeaderText="Pa&#237;s"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_DEPARTAMENTO" HeaderText="Departamento" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="DEPARTAMENTO" HeaderText="Departamentoe"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_MUNICIPIO" HeaderText="Municipio" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="MUNICIPIO" HeaderText="Municipio"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_VEREDA" HeaderText="Vereda" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="VEREDA" HeaderText="Vereda"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_CORREGIMIENTO" HeaderText="Corregimiento" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="CORREGIMIENTO" HeaderText="Corregimiento"></asp:BoundField>
                                                    <asp:BoundField DataField="TELEFONO" HeaderText="Tel&#233;fono"></asp:BoundField>
                                                    <asp:BoundField DataField="CELULAR" HeaderText="Celular"></asp:BoundField>
                                                    <asp:BoundField DataField="FAX" HeaderText="Fax"></asp:BoundField>
                                                    <asp:BoundField DataField="CORREO" HeaderText="Correo Electr&#243;nico"></asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="IdPer2" Visible="false" runat="server" Text='<%# Bind("PER_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Para adicionar un representante legal de clic sobre el botón "Agregar"
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 803px; height: 26px">
                                        <asp:Button runat="server" Text="Agregar" SkinID="boton_copia" ID="btnAgregarRepresentante"
                                            OnClick="btnAgregarRepresentante_Click" CausesValidation="False" />
                                        &nbsp;&nbsp;<div style="visibility: hidden">
                                            <asp:Button ID="btnActualizarRepresentante" SkinID="boton_copia" runat="server"
                                                Text="Actualizar" OnClick="btnActualizarRepresentante_Click" CausesValidation="False" />
                                        </div>
                                        <asp:CustomValidator ID="CustomValidator" runat="server">*</asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 803px">
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="Datos Persona Jur&#237;dica Privada" ID="tabDatosJuridicaPrivada">
                        <ContentTemplate>
                            <table style="width: 70%">
                                <tr>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblRazonSocialPrivada" runat="server" SkinID="etiqueta_negra"
                                                        Text="Razon Social:"></asp:Label>
                                                </td>
                                                <td style="width: 454px">
                                                    <asp:TextBox ID="txtRazonSocialPrivada" runat="server" MaxLength="256"
                                                        SkinID="texto"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvRazonSocialPrivada" runat="server"
                                                        ControlToValidate="txtRazonSocialPrivada" Display="Dynamic"
                                                        ErrorMessage="Datos Persona Jurídica Privada: Ingrese Razón Social">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTipoDocumentoPrivada" runat="server" SkinID="etiqueta_negra"
                                                        Text="Tipo de Documento:"></asp:Label>
                                                </td>
                                                <td style="width: 454px">
                                                    <asp:DropDownList ID="cboTipoDocumentoPrivada" runat="server"
                                                        SkinID="lista_desplegable">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="covTipoDocumentoPrivada" runat="server"
                                                        ControlToValidate="cboTipoDocumentoPrivada" Display="Dynamic"
                                                        ErrorMessage="Datos Persona Jurídica Privada: Seleccione Tipo de Documento"
                                                        Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblNumeroDocumentoPrivada" runat="server"
                                                        SkinID="etiqueta_negra" Text="Número de Documento:"></asp:Label>
                                                </td>
                                                <td style="width: 454px">
                                                    <asp:TextBox ID="txtNumeroDocumentoPrivada" runat="server" MaxLength="10"
                                                        SkinID="texto"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvNumeroDocumentoPrivada" runat="server"
                                                        ControlToValidate="txtNumeroDocumentoPrivada" Display="Dynamic"
                                                        ErrorMessage="Datos Persona Jurídica Privada: Ingrese el numero del documento de la Razón Social">*</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revNumeroDocumentoPrivada" runat="server"
                                                        ControlToValidate="txtNumeroDocumentoPrivada" Display="Dynamic"
                                                        ErrorMessage="Datos Persona Jurídica Privada: Formato no válido para el documento de la Razón Social"
                                                        ValidationExpression="\d{5,10}">*</asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="titleUpdate" colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">Datos Para Contacto
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="titleUpdate" colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDireccionPrivada" runat="server" SkinID="etiqueta_negra"
                                                        Text="Dirección:"></asp:Label>
                                                </td>
                                                <td style="width: 454px">
                                                    <asp:TextBox ID="txtDireccionPrivada" runat="server" MaxLength="100"
                                                        SkinID="texto"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvDireccionPrivada" runat="server"
                                                        ControlToValidate="txtDireccionPrivada" Display="Dynamic"
                                                        ErrorMessage="Datos Persona Jurídica Privada: Ingrese direccion de la Razon Social">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblPaisPrivada" runat="server" SkinID="etiqueta_negra"
                                                        Text="País:"></asp:Label>
                                                </td>
                                                <td style="width: 454px">
                                                    <asp:DropDownList ID="cboPaisPrivada" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="cboPaisPrivada_SelectedIndexChanged"
                                                        SkinID="lista_desplegable">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="covPaisPrivada" runat="server"
                                                        ControlToValidate="cboPaisPrivada" Display="Dynamic"
                                                        ErrorMessage="Datos Persona Jurídica Privada: Seleccione el País"
                                                        Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDepartamentoPrivada" runat="server" SkinID="etiqueta_negra"
                                                        Text="Departamento:"></asp:Label>
                                                </td>
                                                <td style="width: 454px">
                                                    <asp:UpdatePanel ID="updDepartamentoPrivada" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList
                                                                ID="cboDepartamentoPrivada" runat="server" AutoPostBack="True"
                                                                OnSelectedIndexChanged="cboDepartamentoPrivada_SelectedIndexChanged"
                                                                SkinID="lista_desplegable">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="cboPaisPrivada"
                                                                EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblMunicipioPrivada" runat="server" SkinID="etiqueta_negra"
                                                        Text="Municipio:"></asp:Label>
                                                </td>
                                                <td style="width: 454px">
                                                    <asp:UpdatePanel ID="updMunicipioPrivada" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList
                                                                ID="cboMunicipioPrivada" runat="server" AutoPostBack="True"
                                                                OnSelectedIndexChanged="cboMunicipioPrivada_SelectedIndexChanged"
                                                                SkinID="lista_desplegable">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="cboDepartamentoPrivada"
                                                                EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCorregimientoPrivada" runat="server" SkinID="etiqueta_negra"
                                                        Text="Corregimiento:"></asp:Label>
                                                </td>
                                                <td style="width: 454px">
                                                    <asp:UpdatePanel ID="updCorregimientoPrivada" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList
                                                                ID="cboCorregimientoPrivada" runat="server" AutoPostBack="True"
                                                                OnSelectedIndexChanged="cboCorregimientoPrivada_SelectedIndexChanged"
                                                                SkinID="lista_desplegable">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="cboMunicipioPrivada"
                                                                EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblVeredaPrivada" runat="server" SkinID="etiqueta_negra"
                                                        Text="Vereda:"></asp:Label>
                                                </td>
                                                <td style="width: 454px">
                                                    <asp:UpdatePanel ID="updVeredaPrivada" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList
                                                                ID="cboVeredaPrivada" runat="server" SkinID="lista_desplegable">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="cboCorregimientoPrivada"
                                                                EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="titleUpdate" colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" SkinID="etiqueta_negra"
                                                        Text="Dirección Correspondencia:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDireccionCorrespondenciaPrivada" runat="server"
                                                        MaxLength="100" SkinID="texto"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                        ControlToValidate="txtDireccionCorrespondenciaPrivada"
                                                        ErrorMessage="Datos Persona Jurídica Privada: Ingrese dirección de correspondencia">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" SkinID="etiqueta_negra" Text="País:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cboPaisCorrespondenciaPrivada" runat="server"
                                                        AutoPostBack="True"
                                                        OnSelectedIndexChanged="cboPaisCorrespondencia_SelectedIndexChanged"
                                                        SkinID="lista_desplegable">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server"
                                                        ControlToValidate="cboPaisCorrespondenciaPrivada" Display="Dynamic"
                                                        ErrorMessage="Datos Persona Jurídica Privada: Seleccione el País de correspondecia"
                                                        Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" SkinID="etiqueta_negra"
                                                        Text="Departamento:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="updDepartamentoCorrespondenciaPrivada" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList
                                                                ID="cboDepartamentoCorrespondenciaPrivada" runat="server" AutoPostBack="true"
                                                                OnSelectedIndexChanged="cboDepartamentoCorrespondenciaPrivada_SelectedIndexChanged"
                                                                SkinID="lista_desplegable">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="cboPaisCorrespondenciaPrivada"
                                                                EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" SkinID="etiqueta_negra" Text="Municipio:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="updMunicipioCorrespondenciaPrivada" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList
                                                                ID="cboMunicipioCorrespondenciaPrivada" runat="server" AutoPostBack="true"
                                                                OnSelectedIndexChanged="cboMunicipioCorrespondenciaPrivada_SelectedIndexChanged"
                                                                SkinID="lista_desplegable">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="cboDepartamentoCorrespondenciaPrivada"
                                                                EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" SkinID="etiqueta_negra"
                                                        Text="Corregimiento:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="updCorregimientoCorrespondenciaPrivada" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList
                                                                ID="cboCorregimientoCorrespondenciaPrivada" runat="server" AutoPostBack="true"
                                                                OnSelectedIndexChanged="cboCorregimientoCorrespondenciaPrivada_SelectedIndexChanged"
                                                                SkinID="lista_desplegable">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="cboMunicipioCorrespondenciaPrivada"
                                                                EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" SkinID="etiqueta_negra" Text="Vereda:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="updVeredaCorrespondenciaPrivada" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList
                                                                ID="cboVeredaCorrespondenciaPrivada" runat="server" SkinID="lista_desplegable">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="cboCorregimientoCorrespondenciaPrivada"
                                                                EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="titleUpdate" colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbTelefonoPrivada" runat="server" SkinID="etiqueta_negra"
                                                        Text="Teléfono"></asp:Label>
                                                </td>
                                                <td style="width: 454px">
                                                    <asp:TextBox ID="txtTelefonoPrivada" runat="server" MaxLength="10"
                                                        SkinID="texto"></asp:TextBox>
                                                    &nbsp;&nbsp;<asp:RegularExpressionValidator ID="revTelefonoPrivada" runat="server"
                                                        ControlToValidate="txtTelefonoPrivada" Display="Dynamic"
                                                        ErrorMessage="Datos Persona Jurídica Privada: Telefono no tiene el formato correcto"
                                                        ValidationExpression="\d{7,10}">*</asp:RegularExpressionValidator></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCelularPrivada" runat="server" SkinID="etiqueta_negra"
                                                        Text="Celular:"></asp:Label>
                                                </td>
                                                <td style="width: 454px">
                                                    <asp:TextBox ID="txtCelularPrivada" runat="server" MaxLength="13"
                                                        SkinID="texto"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="revTelefonoCelularPrivada" runat="server"
                                                        ControlToValidate="txtCelularPrivada" Display="Dynamic"
                                                        ErrorMessage="Datos Persona Jurídica Privada: Telefono celular no tiene el formato correcto"
                                                        ValidationExpression="\d{10,13}">*</asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblFaxPrivada" runat="server" SkinID="etiqueta_negra"
                                                        Text="Fax:"></asp:Label>
                                                </td>
                                                <td style="width: 454px">
                                                    <asp:TextBox ID="txtFaxPrivada" runat="server" MaxLength="10" SkinID="texto"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="revTelefonoFAXPrivada" runat="server"
                                                        ControlToValidate="txtFaxPrivada" Display="Dynamic"
                                                        ErrorMessage="Datos Persona Jurídica Privada: FAX no tiene el formato correcto"
                                                        ValidationExpression="\d{7,10}">*</asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCorreoPrivada" runat="server" SkinID="etiqueta_negra"
                                                        Text="Correo:"></asp:Label>
                                                </td>
                                                <td style="width: 454px">
                                                    <asp:TextBox ID="txtCorreoPrivada" runat="server" SkinID="texto"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvCorreoPrivada" runat="server"
                                                        ControlToValidate="txtCorreoPrivada"
                                                        ErrorMessage="Datos Persona Jurídica Privada: Ingrese Correo de Razón Social">*</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revCorreoPrivada" runat="server"
                                                        ControlToValidate="txtCorreoPrivada" Display="Dynamic"
                                                        ErrorMessage="Datos Persona Jurídica Privada: Formato no válido para el correo de Razón Social"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="text-align: left; vertical-align: top;">
                                                    <asp:CheckBox ID="ChkAutorizaNotifPerPrivada" SkinID="etiqueta_negra"  runat="server" />
                                                    <asp:Label ID="LblAutorizaNotifPerPrivada" runat="server" SkinID="etiqueta_negra" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="titleUpdate"></td>
                                </tr>
                                <tr>
                                    <td>Datos Representante Legal
                                    </td>
                                </tr>
                                <tr>
                                    <td class="titleUpdate"></td>
                                </tr>
                                <tr>
                                    <td style="width: 803px">
                                        <asp:Panel ID="pnlRepresentantesPrivada" runat="server" Height="170px" ScrollBars="Both"
                                            Style="width: 800px;" Width="800px">
                                            <asp:GridView ID="grdRepresentantesPrivada" runat="server" OnRowCommand="grdRepresentantesPrivada_RowCommand"
                                                AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:ButtonField CommandName="Actualizar" Text="Ver"></asp:ButtonField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnSubir2" runat="server"
                                                                Text='<%# DoSomething(Eval("PER_ID").ToString()) %>'
                                                                CommandName="Eliminar" ValidationGroup="ning"
                                                                CommandArgument='<%# Container.DataItemIndex %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PRIMER_NOMBRE" HeaderText="Primer Nombre"></asp:BoundField>
                                                    <asp:BoundField DataField="SEGUNDO_NOMBRE" HeaderText="Segundo Nombre"></asp:BoundField>
                                                    <asp:BoundField DataField="PRIMER_APELLIDO" HeaderText="Primer Apellido"></asp:BoundField>
                                                    <asp:BoundField DataField="SEGUNDO_APELLIDO" HeaderText="Segundo Apellido"></asp:BoundField>
                                                    <asp:BoundField DataField="TARJETA_PROFESIONAL" HeaderText="Tarjeta Profesional"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_TIPO_ID" HeaderText="Tipo de Documento" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="TIPO_ID" HeaderText="Tipo de Documento"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_IDENTIFICACION" HeaderText="N&#250;mero de Documento"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_ORIGEN_DEPARTAMENTO" HeaderText="Departamento Origen"
                                                        Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="ORIGEN_DEPARTAMENTO" HeaderText="Departamento Origen"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_ORIGEN_MUNICIPIO" HeaderText="Municipio Origen" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="ORIGEN_MUNICIPIO" HeaderText="Municipio Origen"></asp:BoundField>
                                                    <asp:BoundField DataField="DIRECCION_CORRESPONDENCIA" HeaderText="Direcci&#243;n de Correspondencia"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_PAIS" HeaderText="Pa&#237;s" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="PAIS" HeaderText="Pa&#237;s"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_DEPARTAMENTO" HeaderText="Departamento" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="DEPARTAMENTO" HeaderText="Departamentoe"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_MUNICIPIO" HeaderText="Municipio" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="MUNICIPIO" HeaderText="Municipio"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_VEREDA" HeaderText="Vereda" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="VEREDA" HeaderText="Vereda"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_CORREGIMIENTO" HeaderText="Corregimiento" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="CORREGIMIENTO" HeaderText="Corregimiento"></asp:BoundField>
                                                    <asp:BoundField DataField="TELEFONO" HeaderText="Tel&#233;fono"></asp:BoundField>
                                                    <asp:BoundField DataField="CELULAR" HeaderText="Celular"></asp:BoundField>
                                                    <asp:BoundField DataField="FAX" HeaderText="Fax"></asp:BoundField>
                                                    <asp:BoundField DataField="CORREO" HeaderText="Correo Electr&#243;nico"></asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="IdPer3" Visible="false" runat="server" Text='<%# Bind("PER_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Para adicionar un representante legal de clic sobre el botón "Agregar"
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 803px; height: 7px">
                                        <asp:Button ID="btnAgregarPrivada" OnClick="btnAgregarPrivada_Click" runat="server"
                                            SkinID="boton_copia" Text="Agregar" CausesValidation="False"></asp:Button>
                                        &nbsp;&nbsp;<div style="visibility: hidden">
                                            <asp:Button ID="btnActualizarPrivada" OnClick="btnActualizarPrivada_Click"
                                                runat="server" SkinID="boton_copia" Text="Actualizar" CausesValidation="False"></asp:Button>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 803px">
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="Datos Apoderado" ID="tabDatosApoderado">
                        <ContentTemplate>
                            <table style="width: 70%;">
                                <tr>
                                    <td class="titleUpdate"></td>
                                </tr>
                                <tr>
                                    <td>Datos de Apoderado
                                    </td>
                                </tr>
                                <tr>
                                    <td class="titleUpdate"></td>
                                </tr>
                                <tr>
                                    <td style="width: 803px">
                                        <asp:Panel ID="pnlApoderado" runat="server" Height="170px" ScrollBars="Both" Width="800px"
                                            Style="width: 800px;">
                                            <asp:GridView ID="grdApoderados" runat="server" OnRowCommand="grdApoderados_RowCommand"
                                                AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:ButtonField CommandName="Actualizar" Text="Ver" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnSubir" runat="server"
                                                                Text='<%# DoSomething(Eval("PER_ID").ToString()) %>'
                                                                CommandName="Eliminar" ValidationGroup="ning"
                                                                CommandArgument='<%# Container.DataItemIndex %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PRIMER_NOMBRE" HeaderText="Primer Nombre"></asp:BoundField>
                                                    <asp:BoundField DataField="SEGUNDO_NOMBRE" HeaderText="Segundo Nombre"></asp:BoundField>
                                                    <asp:BoundField DataField="PRIMER_APELLIDO" HeaderText="Primer Apellido"></asp:BoundField>
                                                    <asp:BoundField DataField="SEGUNDO_APELLIDO" HeaderText="Segundo Apellido"></asp:BoundField>
                                                    <asp:BoundField DataField="TIP_DOC_ACREDITACION" HeaderText="Documento Acreditación" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="NOM_DOC_ACREDITACION" HeaderText="Tipo de Documento Acreditación"></asp:BoundField>
                                                    <asp:BoundField DataField="NO_DOC_ACREDITACION" HeaderText="No de Documento de Acreditación"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_TIPO_ID" HeaderText="Tipo de Documento" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="TIPO_ID" HeaderText="Tipo de Documento"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_IDENTIFICACION" HeaderText="N&#250;mero de Documento"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_ORIGEN_DEPARTAMENTO" HeaderText="Departamento Origen"
                                                        Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="ORIGEN_DEPARTAMENTO" HeaderText="Departamento Origen"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_ORIGEN_MUNICIPIO" HeaderText="Municipio Origen" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="ORIGEN_MUNICIPIO" HeaderText="Municipio Origen"></asp:BoundField>
                                                    <asp:BoundField DataField="DIRECCION_CORRESPONDENCIA" HeaderText="Direcci&#243;n de Correspondencia"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_PAIS" HeaderText="Pa&#237;s" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="PAIS" HeaderText="Pa&#237;s"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_DEPARTAMENTO" HeaderText="Departamento" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="DEPARTAMENTO" HeaderText="Departamentoe"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_MUNICIPIO" HeaderText="Municipio" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="MUNICIPIO" HeaderText="Municipio"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_VEREDA" HeaderText="Vereda" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="VEREDA" HeaderText="Vereda"></asp:BoundField>
                                                    <asp:BoundField DataField="ID_CORREGIMIENTO" HeaderText="Corregimiento" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="CORREGIMIENTO" HeaderText="Corregimiento"></asp:BoundField>
                                                    <asp:BoundField DataField="TELEFONO" HeaderText="Tel&#233;fono"></asp:BoundField>
                                                    <asp:BoundField DataField="CELULAR" HeaderText="Celular"></asp:BoundField>
                                                    <asp:BoundField DataField="FAX" HeaderText="Fax"></asp:BoundField>
                                                    <asp:BoundField DataField="CORREO" HeaderText="Correo Electr&#243;nico"></asp:BoundField>
                                                    <asp:BoundField DataField="ESTADO" HeaderText="Estado" Visible="False"></asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="IdPer" Visible="false" runat="server" Text='<%# Bind("PER_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                            &nbsp;
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Para adicionar un apoderado de clic sobre el botón "Agregar"
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 803px">
                                        <asp:Button runat="server" Text="Agregar" SkinID="boton_copia" ID="btnAgregarApoderado"
                                            OnClick="btnAgregarApoderado_Click" CausesValidation="False" />
                                        &nbsp;&nbsp;<div style="visibility: hidden">
                                            <asp:Button ID="btnActualizarApoderado" SkinID="boton_copia" runat="server"
                                                Text="Actualizar" OnClick="btnActualizarApoderado_Click" CausesValidation="False" />
                                            <div style="visibility: hidden">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 803px">
                                        <br />
                                    </td>
                                </tr>
                            </table>
                            &nbsp;
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>                
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="uppFormularioPersona" runat="server" AssociatedUpdatePanelID="upnlFormularioPersona">
        <ProgressTemplate>  
            <div id="ModalProgressContainer">
                <div>
                    <p>Procesando...</p>
                    <p><asp:Image ID="imgFormularioPersona" runat="server" SkinId="procesando"/></p>
                </div>
            </div>                         
        </ProgressTemplate>
    </asp:UpdateProgress>
    <table style="width: 100%">
        <tr runat="server" id="trRecaptcha">
            <td style="padding-top: 10px; padding-bottom: 10px;">
                <script src="<%= Recaptcha.URLAPI %>?onload=onloadCallback&render=explicit" async defer></script>
                <div id="loginRecaptcha" class="captcha"></div>
                <asp:CustomValidator runat="server" ID="cvCaptcha" Display="Dynamic" ClientValidationFunction="CaptchaSelectionValidation" ErrorMessage="Haga clic sobre el campo de verificación de captcha.">&nbsp;</asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:ValidationSummary ID="valResumenUsuario" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />
                <asp:Button ID="btnActualizar" SkinID="boton_copia" runat="server" Text="Enviar" OnClick="btnActualizar_Click" />
                <asp:Button ID="btnCancelar" SkinID="boton_copia" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CausesValidation="False" />&nbsp;
            </td>
        </tr>
    </table>
    </div>


    <input type="button" runat="server" id="cmdResultadoDatosPersonaHide" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeModalResultadoDatosPersona" runat="server" PopupControlID="dvModalResultadoDatosPersona" TargetControlID="cmdResultadoDatosPersonaHide" BehaviorID="mpeModalResultadoDatosPersonas" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <div id="dvModalResultadoDatosPersona" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalFormularioNot">
        <asp:UpdatePanel runat="server" ID="upnlModalResultadoDatosPersona" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="TableResultadoModalNot">
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoTituloModalNot">
                            DATOS PERSONALES
                        </div>
                    </div>                            
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoModalNot">
                            <div class="TableFormularioNot">
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <div class="TableMensajesNot">
                                            <div class="RowMensajesNot">
                                                <div class="CellMensajesNot"><asp:Image runat="server" ID="imgOKResultadoDatosPersona" ImageUrl="~/App_Themes/Img/chulo_verde.png" Width="39px"/></div>
                                                <div class="CellMensajesNot"><asp:Literal runat="server" ID="ltlMensajeResultadoOK"></asp:Literal></div>
                                            </div>                                            
                                        </div>
                                    </div>
                                </div>                               
                            </div>
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellButtonModal">
                            <asp:Button ID="cmdAceptarResultadoDatosPersona" runat="server" ClientIDMode="Static" Text="Aceptar" CssClass="boton" CausesValidation="false" OnClick="cmdAceptarResultadoDatosPersona_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdAceptarResultadoDatosPersona" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="uppModalResultadoDatosPersona" runat="server" AssociatedUpdatePanelID="upnlModalResultadoDatosPersona">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgUpdateProgresModalResultadoDatosPersona" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
