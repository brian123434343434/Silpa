<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="SolicitudAudienciaPublica.aspx.cs" Inherits="Informacion_Publicaciones"
    Title="Consultar Inscritos a Audiencia Pública" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/controles/cltCaptcha.ascx" TagPrefix="cpt" TagName="Captcha" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
        
        label {
	        font-family: Arial, sans-serif, Tahoma, Verdana;
	        font-size: 10px;
	        color: #002448;
	        /*margin: 5px;
            padding: 5px;*/
            font-weight: normal !important;
        }
    </style>

    <script type="text/javascript">
        /*
        ----------------------------------------------------------
          funcion para precargar los datos del vocero desde 
          los datos de persona natural
          se usa en  audiencia pública
        ----------------------------------------------------------
        */
        function fntPrecargarVocero()
        {
            var comboTipoPersona = document.getElementById("<%=cboTipoPersona.ClientID %>");
            if (comboTipoPersona.value != '5') {

                var ctlTxtPrimerApellido = Object();
                var ctlTxtSegundoApellido = Object();
                var ctlTxtPrimerNombre = Object();
                var ctlTxtSegundoNombre = Object();

                var ctlTxtPrimerApellidoVocero = Object();
                var ctlTxtSegundoApellidoVocero = Object();
                var ctlTxtPrimerNombreVocero = Object();
                var ctlTxtSegundoNombreVocero = Object();


                var ctlTxtNumeroIdentificacion = Object();
                var ctlTxtNumeroIdentificacionVocero = Object();


                var ctlTxtCorreoPersona = Object();
                var ctlTtxtCorreoVocero = Object();

                var ctlTcboTipoDocumentoPersona = Object();
                var ctlTcboTipoDocumentoVocero = Object();


                var ctlCboMunicipioOrigenPersona = Object();
                var ctlCboMunicipioOrigenVocero = Object();


                //var ctlLstTipoPersona = Object();

                // Los Controles de persona natural:

                ctlTxtPrimerApellido = document.getElementById("<%=txtPrimerApellidoPersona.ClientID %>");
                ctlTxtSegundoApellido = document.getElementById("<%=txtSegundoApellidoPersona.ClientID %>");
                ctlTxtPrimerNombre = document.getElementById("<%=txtPrimerNombrePersona.ClientID %>");
                ctlTxtSegundoNombre = document.getElementById("<%=txtSegundoNombrePersona.ClientID %>");

                ctlTxtNumeroIdentificacion = document.getElementById("<%=txtNumeroIdentificacionPersona.ClientID %>");

                ctlTxtCorreoPersona = document.getElementById("<%=txtCorreoPersona.ClientID %>");

                ctlTcboTipoDocumentoPersona = document.getElementById("<%=cboTipoDocumentoPersona.ClientID %>");

                ctlCboMunicipioOrigenPersona = document.getElementById("<%=cboMunicipioOrigenPersona.ClientID %>");

                // los controles del vocero:
                ctlTxtPrimerNombreVocero = document.getElementById("<%=txtPrimerNombreVocero.ClientID %>");

                ctlTxtSegundoNombreVocero = document.getElementById("<%=txtSegundoNombreVocero.ClientID %>");

                ctlTxtPrimerApellidoVocero = document.getElementById("<%=txtPrimerApellidoVocero.ClientID %>");

                ctlTxtSegundoApellidoVocero = document.getElementById("<%=txtSegundoApellidoVocero.ClientID %>");

                ctlTxtNumeroIdentificacionVocero = document.getElementById("<%=txtNumeroIdentificacionVocero.ClientID %>");

                ctlTtxtCorreoVocero = document.getElementById("<%=txtCorreoVocero.ClientID %>");

                ctlTcboTipoDocumentoVocero = document.getElementById("<%=cboTipoDocumentoVocero.ClientID %>");

                ctlCboMunicipioOrigenVocero = document.getElementById("<%=cboMunicipioOrigenVocero.ClientID %>");

                // Asignacion de los valores:
                ctlTxtPrimerApellidoVocero.value = ctlTxtPrimerApellido.value;
                ctlTxtSegundoApellidoVocero.value = ctlTxtSegundoApellido.value;
                ctlTxtPrimerNombreVocero.value = ctlTxtPrimerNombre.value;
                ctlTxtSegundoNombreVocero.value = ctlTxtSegundoNombre.value;

                ctlTxtNumeroIdentificacionVocero.value = ctlTxtNumeroIdentificacion.value;
                ctlTtxtCorreoVocero.value = ctlTxtCorreoPersona.value;

                ctlTcboTipoDocumentoVocero.value = ctlTcboTipoDocumentoPersona.value;

                ctlCboMunicipioOrigenVocero.value = ctlCboMunicipioOrigenPersona.value;
            }
        }
    </script>

    <div class="div-titulo">
        <asp:Label ID="lbl_titulo_principal" runat="server" SkinID="titulo_principal_blanco"
            Text="SOLICITUD DE CELEBRACIÓN DE AUDIENCIA PÚBLICA"></asp:Label>
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <%-- <asp:UpdatePanel id="UpdatePanel1" runat="server"><ContentTemplate>--%>
    <%--<div class="div-contenido">--%>
    <div class="table-responsive">
        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="IdUser" runat="server" Text="" Visible="false" SkinID="etiqueta_negra"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <cc1:TabContainer ID="tbAudienciaPublica" runat="server" ForeColor="Black" Font-Names="Arial" ActiveTabIndex="0"  OnActiveTabChanged="tbAudienciaPublica_ActiveTabChanged" Width="100%">
                        <cc1:TabPanel runat="server" ID="tbAudiencia">
                            <HeaderTemplate>
                                Audiencia Pública
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                                    <tbody>
                                        <tr>
                                            <td colspan="2" class="subtitulo-doble-linea">Datos Generales</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTipoSolicitante" runat="server" Text="Tipo Solicitante de Audiencia:" SkinID="etiqueta_negra"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cboTipoPersona" runat="server" 
                                                    OnSelectedIndexChanged="cboTipoPersona_SelectedIndexChanged"
                                                    AutoPostBack="True" ClientIDMode="Static">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvTipoPersona" runat="server" ControlToValidate="cboTipoPersona"
                                                    ErrorMessage="Seleccione el Tipo Solicitante Audiencia" InitialValue="-1">*</asp:RequiredFieldValidator>
                                                <asp:Label ID="lblId" TabIndex="1" runat="server" Visible="False" SkinID="etiqueta_negra">-1</asp:Label>
                                                <asp:Label ID="lblFormulario" TabIndex="1" runat="server" Visible="False" SkinID="etiqueta_negra"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblFuncionario" TabIndex="1" runat="server" Text="Funcionario público" Enabled="False" SkinID="etiqueta_negra"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cboFuncionario" TabIndex="2" runat="server" Enabled="False">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvFuncionario" runat="server" ControlToValidate="cboFuncionario"
                                                    ErrorMessage="Seleccione el Tipo Funcionario Público" InitialValue="-1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblArchivo" runat="server" Text="Adjuntar Documento:" SkinID="etiqueta_negra"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:FileUpload ID="uplAdjuntar" runat="server" Width="333px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblRadicado" runat="server" Text="No. de Radicado" SkinID="etiqueta_negra"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRadicado" runat="server" Width="125px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:ListBox ID="lstListaArchivos" runat="server" Height="100px" Width="450px" Visible="False"></asp:ListBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="btnAdjuntar" runat="server" CausesValidation="False" Text="Adjuntar" OnClick="btnAdjuntar_Click" />
                                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CausesValidation="False" OnClick="btnEliminar_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblMotivo" runat="server" Text="Motivación de la solicitud de audiencia pública:" SkinID="etiqueta_negra"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMotivacion" TabIndex="4" runat="server" Width="325px" Height="72px"
                                                    TextMode="MultiLine"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvMotivacion" runat="server" ControlToValidate="txtMotivacion"
                                                    ErrorMessage="Seleccione el Tipo Solicitante Audiencia" InitialValue="-1">*</asp:RequiredFieldValidator></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel runat="server" ID="tabDatosPersona">
                            <HeaderTemplate>
                                Persona Natural
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                                    <tr>
                                        <td colspan="2" class="subtitulo-doble-linea">Datos Personales</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPrimerNombrePersona" runat="server" Text="Primer Nombre:" SkinID="etiqueta_negra"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPrimerNombrePersona" runat="server" MaxLength="30" SkinID="texto" onblur = "fntPrecargarVocero()"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPrimerNombrePersona" runat="server" ControlToValidate="txtPrimerNombrePersona"
                                                ErrorMessage="Ingrese Primer Nombre Persona Natural">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revPrimerNombrePersona" runat="server" ControlToValidate="txtPrimerNombrePersona"
                                                Display="Dynamic" ErrorMessage="No se admiten numeros, caracteres especiales o espacios en el campo Primer Nombre"
                                                ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚÑñ]{1,30}$">*</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSegundoNombrePersona" runat="server" Text="Segundo Nombre:" SkinID="etiqueta_negra"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSegundoNombrePersona" runat="server" MaxLength="30" SkinID="texto" onblur = "fntPrecargarVocero()"></asp:TextBox>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPrimerApellidoPersona" runat="server" SkinID="etiqueta_negra" Text="Primer Apellido:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPrimerApellidoPersona" runat="server" MaxLength="30" SkinID="texto" onblur="fntPrecargarVocero()"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPrimerApellidoPersona" runat="server" ControlToValidate="txtPrimerApellidoPersona"
                                                ErrorMessage="Ingrese Primer Apellido Persona Natural">*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revPrimerApellidoPersona" runat="server" ControlToValidate="txtPrimerApellidoPersona"
                                                Display="Dynamic" ErrorMessage="No se admiten numeros, caracteres especiales o espacios en el campo Primer Apellido"
                                                ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚÑñ]{1,30}$">*</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSegundoApellidoPersona" runat="server" SkinID="etiqueta_negra" Text="Segundo Apellido:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSegundoApellidoPersona" runat="server" MaxLength="30" SkinID="texto"  onblur = "fntPrecargarVocero()"></asp:TextBox>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblTipoDocumentoPersona" runat="server" SkinID="etiqueta_negra" Text="Tipo de Documento:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboTipoDocumentoPersona" runat="server" SkinID="lista_desplegable" OnSelectedIndexChanged="cboTipoDocumentoPersona_SelectedIndexChanged"  onblur = "fntPrecargarVocero()"  >
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvTipoDocumentoPersona" runat="server" ControlToValidate="cboTipoDocumentoPersona"
                                                ErrorMessage="Seleccione Tipo de Documento para Persona Natural" InitialValue="-1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNumeroIdentificacionPersona" runat="server" SkinID="etiqueta_negra" Text="Número de Documento:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNumeroIdentificacionPersona" runat="server" MaxLength="11" SkinID="texto"   onblur= "fntPrecargarVocero()"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvNumeroIdentificacionPersona" runat="server" ControlToValidate="txtNumeroIdentificacionPersona"
                                                ErrorMessage="Ingrese Número de Documento Persona Natural">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCiudadExpedicionPersona" runat="server" SkinID="etiqueta_negra" Text="De:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboDepartamentoOrigenPersona" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="cboDepartamentoOrigenPersona_SelectedIndexChanged" SkinID="lista_desplegable">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDepartamentoOrigenPersona" runat="server" ControlToValidate="cboDepartamentoOrigenPersona"
                                                ErrorMessage="Seleccione el Departamento de Origen del Documento Persona Natural"
                                                InitialValue="-1">*</asp:RequiredFieldValidator>
                                            <br />
                                            <asp:DropDownList ID="cboMunicipioOrigenPersona" runat="server" SkinID="lista_desplegable" OnSelectedIndexChanged="cboMunicipioOrigenPersona_SelectedIndexChanged" onblur ="fntPrecargarVocero()">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvMunicipioOrigenPersona" runat="server" ControlToValidate="cboMunicipioOrigenPersona"
                                                ErrorMessage="Seleccione el Municipio de Origen del Documento Persona Natural"
                                                InitialValue="-1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="subtitulo-doble-linea">Datos para Contacto</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCorreoPersona" runat="server" SkinID="etiqueta_negra" Text="Correo Electrónico:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCorreoPersona" runat="server" MaxLength="100" SkinID="texto" onblur = "fntPrecargarVocero()"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCorreoPersona" runat="server" ControlToValidate="txtCorreoPersona"
                                                ErrorMessage="Ingrese Correo Electrónico Persona Natural">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RevCorreoPersona" runat="server" ControlToValidate="txtCorreoPersona"
                                                Display="Dynamic" ErrorMessage="Formato no válido para el correo Persona Natural"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel runat="server" ID="tbRepresentante">
                            <HeaderTemplate>
                                Representante Legal
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table class="tabla_estruct">
                                    <tr>
                                        <td colspan="2" class="subtitulo-doble-linea">Datos Personales</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPrimerNombreRepresentante" runat="server" SkinID="etiqueta_negra" Text="Primer Nombre:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPrimerNombreRepresentante" runat="server" MaxLength="30" SkinID="texto"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPrimerNombreRepresentante" runat="server" ControlToValidate="txtPrimerNombreRepresentante"
                                                ErrorMessage="Ingrese Primer Nombre Representante Legal">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSegundoNombreRepresentante" runat="server" SkinID="etiqueta_negra" Text="Segundo Nombre:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSegundoNombreRepresentante" runat="server" MaxLength="30" SkinID="texto"></asp:TextBox>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPrimerApellidoRepresentante" runat="server" SkinID="etiqueta_negra" Text="Primer Apellido:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPrimerApellidoRepresentante" runat="server" MaxLength="30" SkinID="texto"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPrimerApellidoRepresentante" runat="server" ControlToValidate="txtPrimerApellidoRepresentante"
                                                ErrorMessage="Ingrese Primer Apellido Representante Legal">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSegundoApellidoRepresentante" runat="server" SkinID="etiqueta_negra" Text="Segundo Apellido:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSegundoApellidoRepresentante" runat="server" MaxLength="30" SkinID="texto"></asp:TextBox>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblTipoDocumentoRepresentante" runat="server" SkinID="etiqueta_negra" Text="Tipo de Documento:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboTipoDocumentoRepresentante" runat="server" SkinID="lista_desplegable">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvTipoDocumentoRepresentante" runat="server" ControlToValidate="cboTipoDocumentoRepresentante"
                                                ErrorMessage="Seleccione Tipo de Documento para Representante Legal" InitialValue="-1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNumeroIdentificacionRepresentante" runat="server" SkinID="etiqueta_negra" Text="Número de Documento:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNumeroIdentificacionRepresentante" runat="server" MaxLength="11"
                                                SkinID="texto"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvNumeroIdentificacionRepresentante" runat="server"
                                                ControlToValidate="txtNumeroIdentificacionRepresentante" ErrorMessage="Ingrese Número de Documento Persona Natural">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCiudadExpedicionRepresentante" runat="server" SkinID="etiqueta_negra" Text="De:"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="cboDepartamentoOrigenRepresentante" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="cboDepartamentoOrigenRepresentante_SelectedIndexChanged"
                                                SkinID="lista_desplegable">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDepartamentoOrigenRepresentante" runat="server"
                                                ControlToValidate="cboDepartamentoOrigenRepresentante" ErrorMessage="Seleccione el Departamento de Origen del Documento Representante Legal"
                                                InitialValue="-1">*</asp:RequiredFieldValidator>
                                            <br />
                                            <asp:DropDownList ID="cboMunicipioOrigenRepresentante" runat="server" SkinID="lista_desplegable">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvMunicipioOrigenRepresentante" runat="server" ControlToValidate="cboMunicipioOrigenRepresentante"
                                                ErrorMessage="Seleccione el Municipio de Origen del Documento Representante Legal"
                                                InitialValue="-1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="subtitulo-doble-linea">Datos para Contacto</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCorreoRepresentante" runat="server" SkinID="etiqueta_negra" Text="Correo Electrónico:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCorreoRepresentante" runat="server" SkinID="texto"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCorreoRepresentante" runat="server" ControlToValidate="txtCorreoRepresentante"
                                                ErrorMessage="Ingrese Correo Electrónico Representante Legal">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revCorreoRepresentante" runat="server" ControlToValidate="txtCorreoRepresentante"
                                                Display="Dynamic" ErrorMessage="Formato no válido para el correo Representante Legal"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel runat="server" ID="tbProyecto">
                            <HeaderTemplate>
                                Proyecto, Obra o Actividad
                            </HeaderTemplate>
                            <ContentTemplate>
                                <asp:UpdatePanel ID="pnlProyectoObraActividad" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table class="tabla_estruct">
                                            <tr>
                                                <td colspan="2" class="subtitulo-doble-linea">Datos Generales</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblNombreProyecto" runat="server" Text="Nombre del Proyecto, Obra o Actividad:" SkinID="etiqueta_negra"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNombreProyecto" runat="server" Width="160px" TabIndex="18"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvNombreProyecto" runat="server" ControlToValidate="txtNombreProyecto" ErrorMessage="Ingrese el Nombre del Proyecto, Obra o Actividad">*</asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTitular" runat="server" Text="Titular del Proyecto, Obra o Actividad:" SkinID="etiqueta_negra"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTitular" runat="server" Width="160px" TabIndex="19"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvTitular" runat="server" ControlToValidate="txtTitular" ErrorMessage="Ingrese el Titular del Proyecto, Obra o Actividad">*</asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblAutoridadAmb" runat="server" Text="Autoridad Ambiental:" SkinID="etiqueta_negra"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cboAutoridadAmb" runat="server" 
                                                        onselectedindexchanged="cboAutoridadAmb_SelectedIndexChanged" 
                                                        AutoPostBack="True"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvAutoridadAmb" ControlToValidate="cboAutoridadAmb" InitialValue="-1" runat="server" ErrorMessage="Debe Seleccionar una Autoridad Ambiental">*</asp:RequiredFieldValidator>
                                                </td>
                                            
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblTramite" runat="server" Text="Tramite:" SkinID="etiqueta_negra"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cboTramite" runat="server" 
                                                        onselectedindexchanged="cboTramite_SelectedIndexChanged" 
                                                        AutoPostBack="True"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvTramite" ControlToValidate="cboTramite" InitialValue="-1" runat="server" ErrorMessage="Debe Seleccionar un tramite">*</asp:RequiredFieldValidator>
                                                </td>
                                            
                                            </tr>
                                                <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="Usuarios Tramite:" SkinID="etiqueta_negra"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cboUsuarioTramite" runat="server" 
                                                        onselectedindexchanged="cboUsuarioTramite_SelectedIndexChanged" 
                                                        CssClass="texto_usuario" AutoPostBack="True"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvUsuarioTramite" ControlToValidate="cboTramite" InitialValue="-1" runat="server" ErrorMessage="Debe Seleccionar un Usuario">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblNumeroVital" runat="server" Text="Número VITAL:" SkinID="etiqueta_negra"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cboNumeroVital" runat="server" CssClass="texto_usuario" 
                                                        onselectedindexchanged="cboNumeroVital_SelectedIndexChanged" 
                                                        AutoPostBack="True"></asp:DropDownList><asp:RequiredFieldValidator
                                                        ID="rfvNumeroVital" runat="server" ControlToValidate="cboNumeroVital" InitialValue="-1" ErrorMessage="Seleccione el Número VITAL">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblNumeroExpediente" runat="server" Text="Número de Expediente:" SkinID="etiqueta_negra"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cboNumeroExpediente" runat="server" CssClass="texto_usuario"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvNumeroExpediente" runat="server" InitialValue="-1" ControlToValidate="cboNumeroExpediente"
                                                        ErrorMessage="Seleccione el Número de Expediente" Enabled="False">*</asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAutoridad" runat="server" Text="Autoridad Ambiental Competente:" Visible="False" SkinID="etiqueta_negra"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cboAutoridadAmbiental" runat="server" Width="166px" TabIndex="17" Visible="False">
                                                    </asp:DropDownList>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lnkVerTramite" runat="server" OnClick="lnkVerTramite_Click" CausesValidation="False">Buscar Proyecto, Obra o Actividad</asp:LinkButton>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel runat="server" ID="tbVocero">
                            <HeaderTemplate>
                                Vocero del Grupo
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table class="tabla_estruct">
                                    <tr>
                                        <td colspan="2" class="subtitulo-doble-linea">Datos Personales</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPrimerNombreVocero" runat="server" SkinID="etiqueta_negra" Text="Primer Nombre:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPrimerNombreVocero" runat="server" MaxLength="30" SkinID="texto"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPrimerNombreVocero" runat="server" ControlToValidate="txtPrimerNombreVocero"
                                                ErrorMessage="Ingrese Primer Nombre Vocero">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revPrimerNombreVocero" runat="server" ControlToValidate="txtPrimerNombreVocero"
                                                Display="Dynamic" ErrorMessage="No se admiten numeros, caracteres especiales o espacios en el campo Primer Nombre del vocero"
                                                ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚÑñ]{1,30}$">*</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSegundoNombreVocero" runat="server" SkinID="etiqueta_negra" Text="Segundo Nombre:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSegundoNombreVocero" runat="server" MaxLength="30" SkinID="texto"></asp:TextBox>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPrimerApellidoVocero" runat="server" SkinID="etiqueta_negra" Text="Primer Apellido:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPrimerApellidoVocero" runat="server" MaxLength="30" SkinID="texto"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPrimerApellidoVocero" runat="server" ControlToValidate="txtPrimerApellidoVocero"
                                                ErrorMessage="Ingrese Primer Apellido Vocero">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revPrimerApellidoVocero" runat="server" ControlToValidate="txtPrimerApellidoVocero"
                                                Display="Dynamic" ErrorMessage="No se admiten numeros, caracteres especiales o espacios en el campo Primer Apellido del vocero"
                                                ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚÑñ]{1,30}$">*</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSegundoApellidoVocero" runat="server" SkinID="etiqueta_negra" Text="Segundo Apellido:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSegundoApellidoVocero" runat="server" MaxLength="30" SkinID="texto"></asp:TextBox>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblTipoDocumentoVocero" runat="server" SkinID="etiqueta_negra" Text="Tipo de Documento:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboTipoDocumentoVocero" runat="server" SkinID="lista_desplegable">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvTipoDocumentoVocero" runat="server" ControlToValidate="cboTipoDocumentoVocero"
                                                ErrorMessage="Seleccione Tipo de Documento para Vocero" InitialValue="-1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNumeroIdentificacionVocero" runat="server" SkinID="etiqueta_negra" Text="Número de Documento:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNumeroIdentificacionVocero" runat="server" MaxLength="11" SkinID="texto"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvNumeroIdentificacionVocero" runat="server" ControlToValidate="txtNumeroIdentificacionVocero"
                                                ErrorMessage="Ingrese Número de Documento Vocero">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCiudadExpedicionVocero" runat="server" SkinID="etiqueta_negra" Text="De:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboDepartamentoOrigenVocero" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="cboDepartamentoOrigenVocero_SelectedIndexChanged" SkinID="lista_desplegable">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDepartamentoOrigenVocero" runat="server" ControlToValidate="cboDepartamentoOrigenVocero"
                                                ErrorMessage="Seleccione el Departamento de Origen del Documento Vocero" InitialValue="-1">*</asp:RequiredFieldValidator>
                                            <br />
                                            <asp:DropDownList ID="cboMunicipioOrigenVocero" runat="server" SkinID="lista_desplegable">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvMunicipioOrigenVocero" runat="server" ControlToValidate="cboMunicipioOrigenVocero"
                                                ErrorMessage="Seleccione el Municipio de Origen del Documento Vocero" InitialValue="-1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="subtitulo-doble-linea">Datos para Contacto</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblDireccionVocero" runat="server" SkinID="etiqueta_negra" Text="Dirección:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDireccionVocero" runat="server" SkinID="texto"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvDireccionVocero" runat="server" ControlToValidate="txtDireccionVocero"
                                                ErrorMessage="Ingrese la dirección Vocero">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblDepartamentoVocero" runat="server" SkinID="etiqueta_negra" Text="Departamento:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboDepartamentoVocero" runat="server" SkinID="lista_desplegable"
                                                AutoPostBack="True" OnSelectedIndexChanged="cboDepartamentoVocero_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDepartamentoVocero" runat="server" ControlToValidate="cboDepartamentoVocero"
                                                ErrorMessage="Seleccione el Departamento de Contacto Vocero" InitialValue="-1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblMunicipioVocero" runat="server" SkinID="etiqueta_negra" Text="Municipio:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboMunicipioVocero" runat="server" SkinID="lista_desplegable"
                                                AutoPostBack="True" OnSelectedIndexChanged="cboMunicipioVocero_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvMunicipioVocero" runat="server" ControlToValidate="cboMunicipioVocero"
                                                ErrorMessage="Seleccione el Municipio de Contacto Vocero" InitialValue="-1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCorregimientoVocero" runat="server" SkinID="etiqueta_negra" Text="Corregimiento:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboCorregimientoVocero" runat="server" AutoPostBack="True"
                                                SkinID="lista_desplegable" OnSelectedIndexChanged="cboCorregimientoVocero_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblVeredaVocero" runat="server" SkinID="etiqueta_negra" Text="Vereda:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboVeredaVocero" runat="server" SkinID="lista_desplegable">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblTelefonoVocero" runat="server" SkinID="etiqueta_negra" Text="Teléfono"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTelefonoVocero" runat="server" SkinID="texto"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTelefonoVocero" runat="server" ControlToValidate="txtTelefonoVocero"
                                                ErrorMessage="Ingrese teléfono de Vocero">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCelularVocero" runat="server" SkinID="etiqueta_negra" Text="Celular:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCelularVocero" runat="server" SkinID="texto"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblFaxVocero" runat="server" SkinID="etiqueta_negra" Text="Fax:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFaxVocero" runat="server" SkinID="texto"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCorreoVocero" runat="server" SkinID="etiqueta_negra" Text="Correo Electrónico:"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="txtCorreoVocero" runat="server" MaxLength="100" SkinID="texto"  onblur = "fntPrecargarVocero()"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCorreoVocero" runat="server" ControlToValidate="txtCorreoVocero"
                                                ErrorMessage="Ingrese Correo Electrónico Contacto Vocero">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revCorreoVocero" runat="server" ControlToValidate="txtCorreoVocero"
                                                Display="Dynamic" ErrorMessage="Formato no válido para el correo Contacto Vocero"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel runat="server" ID="tbFuncionario">
                            <HeaderTemplate>
                                Funcionario Público
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table class="tabla_estruct">
                                    <tr>
                                        <td colspan="2" class="subtitulo-doble-linea">Datos Personales</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPrimerNombreFuncionario" runat="server" SkinID="etiqueta_negra" Text="Primer Nombre:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPrimerNombreFuncionario" runat="server" MaxLength="30" SkinID="texto"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPrimerNombreFuncionario" runat="server" ControlToValidate="txtPrimerNombreFuncionario"
                                                ErrorMessage="Ingrese Primer Nombre Funcionario Público">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSegundoNombreFuncionario" runat="server" SkinID="etiqueta_negra" Text="Segundo Nombre:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSegundoNombreFuncionario" runat="server" MaxLength="30" SkinID="texto"></asp:TextBox>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPrimerApellidoFuncionario" runat="server" SkinID="etiqueta_negra" Text="Primer Apellido:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPrimerApellidoFuncionario" runat="server" MaxLength="30" SkinID="texto"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPrimerApellidoFuncionario" runat="server" ControlToValidate="txtPrimerApellidoFuncionario"
                                                ErrorMessage="Ingrese Primer Apellido Funcionario Público">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSegundoApellidoFuncionario" runat="server" SkinID="etiqueta_negra" Text="Segundo Apellido:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSegundoApellidoFuncionario" runat="server" MaxLength="30" SkinID="texto"></asp:TextBox>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblTipoDocumentoFuncionario" runat="server" SkinID="etiqueta_negra" Text="Tipo de Documento:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboTipoDocumentoFuncionario" runat="server" SkinID="lista_desplegable">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvTipoDocumentoFuncionario" runat="server" ControlToValidate="cboTipoDocumentoFuncionario"
                                                ErrorMessage="Seleccione Tipo de Documento para Funcionario Público" InitialValue="-1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNumeroIdentificacionFuncionario" runat="server" SkinID="etiqueta_negra" Text="Número de Documento:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNumeroIdentificacionFuncionario" runat="server" MaxLength="11"
                                                SkinID="texto"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvNumeroIdentificacionFuncionario" runat="server"
                                                ControlToValidate="txtNumeroIdentificacionFuncionario" ErrorMessage="Ingrese Número de Documento Funcionario Público">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCiudadExpedicionFuncionario" runat="server" SkinID="etiqueta_negra" Text="De:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboDepartamentoOrigenFuncionario" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="cboDepartamentoOrigenFuncionario_SelectedIndexChanged"
                                                SkinID="lista_desplegable">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDepartamentoOrigenFuncionario" runat="server"
                                                ControlToValidate="cboDepartamentoOrigenFuncionario" ErrorMessage="Seleccione el Departamento de Origen del Documento Funcionario Público"
                                                InitialValue="-1">*</asp:RequiredFieldValidator>
                                            <br />
                                            <asp:DropDownList ID="cboMunicipioOrigenFuncionario" runat="server" SkinID="lista_desplegable">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvMunicipioOrigenFuncionario" runat="server" ControlToValidate="cboMunicipioOrigenFuncionario"
                                                ErrorMessage="Seleccione el Municipio de Origen del Documento Funcionario Público"
                                                InitialValue="-1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="subtitulo-doble-linea">Datos para Contacto</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblDireccionFuncionario" runat="server" SkinID="etiqueta_negra" Text="Dirección:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDireccionFuncionario" runat="server" SkinID="texto"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvDireccionFuncionario" runat="server" ControlToValidate="txtDireccionFuncionario"
                                                ErrorMessage="Ingrese la Dirección Funcionario Público">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblDepartamentoFuncionario" runat="server" SkinID="etiqueta_negra" Text="Departamento:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboDepartamentoFuncionario" runat="server" SkinID="lista_desplegable"
                                                AutoPostBack="True" OnSelectedIndexChanged="cboDepartamentoFuncionario_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDepartamentoFuncionario" runat="server" ControlToValidate="cboDepartamentoFuncionario"
                                                ErrorMessage="Seleccione el Departamento de Contacto Funcionario Público" InitialValue="-1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblMunicipioFuncionario" runat="server" SkinID="etiqueta_negra" Text="Municipio:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboMunicipioFuncionario" runat="server" SkinID="lista_desplegable"
                                                AutoPostBack="True" OnSelectedIndexChanged="cboMunicipioFuncionario_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvMunicipioFuncionario" runat="server" ControlToValidate="cboMunicipioFuncionario"
                                                ErrorMessage="Seleccione el Municipio de Contacto Funcionario Público" InitialValue="-1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCorregimientoFuncionario" runat="server" SkinID="etiqueta_negra" Text="Corregimiento:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboCorregimientoFuncionario" runat="server" AutoPostBack="True"
                                                SkinID="lista_desplegable" OnSelectedIndexChanged="cboCorregimientoFuncionario_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lvlVeredaFuncionario" runat="server" SkinID="etiqueta_negra" Text="Vereda:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboVeredaFuncionario" runat="server" SkinID="lista_desplegable">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblTelefonoFuncionario" runat="server" SkinID="etiqueta_negra" Text="Teléfono"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTelefonoFuncionario" runat="server" SkinID="texto"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTelefonoFuncionario" runat="server" ControlToValidate="txtTelefonoFuncionario"
                                                ErrorMessage="Ingrese el Teléfono de Funcionario Público">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCelularFuncionario" runat="server" SkinID="etiqueta_negra" Text="Celular:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCelularFuncionario" runat="server" SkinID="texto"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblFaxFuncionario" runat="server" SkinID="etiqueta_negra" Text="Fax:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFaxFuncionario" runat="server" SkinID="texto"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCorreoFuncionario" runat="server" SkinID="etiqueta_negra" Text="Correo Electrónico:"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="txtCorreoFuncionario" runat="server" SkinID="texto"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCorreoFuncionario" runat="server" ControlToValidate="txtCorreoFuncionario"
                                                ErrorMessage="Ingrese Correo Electrónico Funcionario Público">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revCorreoFuncionario" runat="server" ControlToValidate="txtCorreoFuncionario"
                                                Display="Dynamic" ErrorMessage="Formato no válido para el correo Funcionario Público"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel runat="server" ID="tbEntidad">
                            <HeaderTemplate>
                                Entidades sin Ánimo de Lucro
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table class="tabla_estruct">
                                    <tbody>
                                        <tr>
                                            <td colspan="2" class="subtitulo-doble-linea">Datos Generales</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblRazonSocialEntidad" runat="server" Text="Razón Social:" SkinID="etiqueta_negra"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRazonSocialEntidad" TabIndex="21" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvRazonSocialEntidad" runat="server" ControlToValidate="txtRazonSocialEntidad"
                                                    ErrorMessage="Ingrese la Razón Social Entidad sin Ánimo de Lucro">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblNitEntidad" runat="server" Text="Nit:" SkinID="etiqueta_negra"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNitEntidad" TabIndex="22" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvNitEntidad" runat="server" ControlToValidate="txtNitEntidad"
                                                    ErrorMessage="Ingrese el Nit Entidad sin Ánimo de Lucro">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="subtitulo-doble-linea">Datos para Contacto</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCorreoEntidad" runat="server" SkinID="etiqueta_negra" Text="Correo Electrónico:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCorreoEntidad" runat="server" SkinID="texto"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvCorreoEntidad" runat="server" ControlToValidate="txtCorreoEntidad"
                                                    ErrorMessage="Ingrese Correo Electrónico Entidad sin Ánimo de Lucro">*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revCorreoEntidad" runat="server" ErrorMessage="Formato no válido para el correo Entidad sin Ánimo de Lucro"
                                                    ControlToValidate="txtCorreoEntidad" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <%--<cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Visible="false"></cc1:TabPanel>--%>
                    </cc1:TabContainer>
                </td>
            </tr>            
            <tr runat="server" id="trCaptcha">
                <td>
                    <cpt:Captcha runat="server" ID="ctrCaptcha" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="padding: 10px; text-align: left; vertical-align: top;">
                    <asp:ValidationSummary ID="vasMensaje" runat="server" ShowMessageBox="false" />
                </td> 
            </tr>
            <tr>
                <td colspan="3" style="text-align: center; vertical-align: middle;">
                    <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                        <tr>
                            <td style="padding-top: 20px; padding-bottom: 10px; padding-left: 10px; padding-right: 20px; text-align: right; vertical-align: middle;">
                                <asp:Button ID="btnAceptar" TabIndex="29" OnClick="btnAceptar_Click" runat="server" Text="Aceptar"></asp:Button>
                            </td>
                            <td style="padding-top: 20px; padding-bottom: 10px; padding-left: 10px; padding-right: 20px; text-align: right; vertical-align: middle;">
                                <asp:Button ID="Button1" TabIndex="30"  runat="server" Text="Cancelar" OnClick="Button1_Click" ValidationGroup="xx"></asp:Button>
                            </td>
                        </tr>
                    </table>
                <td>
            </tr>
        </table>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
