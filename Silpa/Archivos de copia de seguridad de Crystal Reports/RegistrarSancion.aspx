<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="RegistrarSancion.aspx.cs" Inherits="RUIA_RegistrarSancion" Title="Registro Unico de Infractores Ambientales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceHolder" runat="Server">
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
    </style>

    <div class="div-titulo">>
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="REGISTRO ÚNICO DE INFRACTORES AMBIENTALES" SkinID="subtitulo"></asp:Label>
    </div>
      
    <asp:ScriptManager ID="scmManejadorSancion" runat="server">
    </asp:ScriptManager>

    <div class="table-responsive">
        <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%;">
            <tr>
                <td style="padding: 0; text-align: left; vertical-align: top;">
                    <asp:UpdatePanel ID="uppPanelSancion" runat="server">
                        <contenttemplate>
                            <cc1:TabContainer id="tbcContenedor" runat="server" ActiveTabIndex="0">
                                <cc1:TabPanel runat="server" ID="tabDatosSancion" HeaderText="Datos de Infracci&#243;n o Sanci&#243;n">
                                    <ContentTemplate>
                                        <table style="WIDTH: 100%">
                                            <tr>
                                                <td align="left" style="WIDTH: 30%">
                                                    <asp:Label id="lblTipoPersona" runat="server" SkinID="etiqueta_negra" Text="Tipo de persona sancionada:"></asp:Label>
                                                    <asp:Label id="lblId" runat="server" SkinID="etiqueta_negra" Visible="False">0</asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList id="cboTipoPersona" runat="server" OnSelectedIndexChanged="cboTipoPersona_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                    <asp:Label id="lblFormulario" tabIndex=1 runat="server" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label id="lblTipoFalta" runat="server" SkinID="etiqueta_negra" Text="Tipo de Infracción:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList id="cboTipoFalta" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label id="lblDescripcionNorma" runat="server" SkinID="etiqueta_negra" Text="Descripción de la norma específica:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox id="txtDescripcionNorma" runat="server" SkinID="texto" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator id="rfvDescripcionNorma" runat="server" ControlToValidate="txtDescripcionNorma" ErrorMessage="Ingrese descripción de la norma específica">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label id="lblLugarOcurrencia" runat="server" SkinID="TitApp" Text="Lugar de Ocurrencia de los Hechos:"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label id="lblDepartamentoOcurrencia" runat="server" SkinID="etiqueta_negra" Text="Departamento de ocurrencia:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList id="cboDepartamentoOcurrencia" runat="server" OnSelectedIndexChanged="cboDepartamentoOcurrencia_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator id="reqDepartamentoOcurrencia" runat="server" Display="Dynamic" ControlToValidate="cboDepartamentoOcurrencia" ErrorMessage="Seleccione el departamento de ocurrencia">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label id="lblMunicipioOcurrencia" runat="server" SkinID="etiqueta_negra" Text="Municipio de ocurrencia:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList id="cboMunicipioOcurrencia" runat="server" OnSelectedIndexChanged="cboMunicipioOcurrencia_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator id="reqMunicipioOcurrencia" runat="server" Display="Dynamic" ControlToValidate="cboMunicipioOcurrencia" ErrorMessage="Seleccione el municipio de ocurrencia de los hechos">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label id="lblCorregimientoOcurrencia" runat="server" SkinID="etiqueta_negra" Text="Corregimiento de ocurrencia:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList id="cboCorregimientoOcurrencia" runat="server" OnSelectedIndexChanged="cboCorregimientoOcurrencia_SelectedIndexChanged"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="HEIGHT: 10px" align="left">
                                                    <asp:Label id="lblVeredaOcurrencia" runat="server" SkinID="etiqueta_negra" Text="Vereda de ocurrencia:"></asp:Label>
                                                </td>
                                                <td style="HEIGHT: 10px" align="left">
                                                    <asp:DropDownList id="cboVeredaOcurrencia" runat="server" AutoPostBack="True"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="left">
                                                    <asp:Label id="lblOpcionPrincipal" runat="server" SkinID="etiqueta_negra" Text="Tipo de Sanción Principal:"></asp:Label>
                                                </td>
                                                <td valign="top" align="left">
                                                    <asp:DropDownList id="cboOpcionesPrincipal" runat="server"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator id="reqOpcionPrincipal" runat="server" Display="Dynamic" ControlToValidate="cboOpcionesPrincipal" ErrorMessage="Seleccione el tipo de opción principal">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="left">
                                                    <asp:Label id="lblSancionAplicada" runat="server" SkinID="etiqueta_negra" Text="Sanción Aplicada Principal:"></asp:Label>
                                                </td>
                                                <td valign="top" align="left">
                                                    <asp:TextBox id="txtSancionAplicadaPpal" runat="server" SkinID="texto"></asp:TextBox>
                                                    <asp:RequiredFieldValidator id="reqSancionAplicadaPpal" runat="server" Display="Dynamic" ControlToValidate="txtSancionAplicadaPpal" ErrorMessage="Digite la sanción principal aplicada">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label id="lblSecundaria" runat="server" SkinID="TitApp" Text="Sanciones Accesorias"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label id="lblTipoSecundaria" runat="server" SkinID="etiqueta_negra" Text="Tipo Sanción Accesoria"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList id="cboTipoSancionSecundaria" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="left">
                                                    <asp:Label id="lblSancionAplicadaSec" runat="server" SkinID="etiqueta_negra" Text="Sanción Aplicada Accesoria:"></asp:Label>
                                                </td>
                                                <td valign="top" align="left">
                                                    <asp:TextBox id="txtSancionAplicadaSec" runat="server" SkinID="texto"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:Button id="btnAgregarSec" onclick="btnAgregarSec_Click" runat="server" SkinID="boton" Text="Agregar" CausesValidation="False"></asp:Button>
                                                    <asp:Button id="btnQuitarSec" onclick="btnQuitarSec_Click" runat="server" SkinID="boton" Text="Quitar" CausesValidation="False"></asp:Button>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label id="lblMensajeSecundaria" runat="server" ForeColor="Red"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:ListBox id="lstSecundaria" runat="server"></asp:ListBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label id="lblNumeroExpediente" runat="server" SkinID="etiqueta_negra" Text="Número de Expediente:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox id="txtNumeroExpediente" runat="server" SkinID="texto"></asp:TextBox>
                                                    <asp:RequiredFieldValidator id="rfvNumeroExpediente" runat="server" Display="Dynamic" ControlToValidate="txtNumeroExpediente" ErrorMessage="Ingrese número de expediente">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label id="lblNumeroActo" runat="server" SkinID="etiqueta_negra" Text="Número de Acto que impone sanción:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox id="txtNumeroActo" runat="server" SkinID="texto"></asp:TextBox>
                                                    <asp:RequiredFieldValidator id="rfvNumeroActo" runat="server" Display="Dynamic" ControlToValidate="txtNumeroActo" ErrorMessage="Ingrese el número del Acto Administrativo">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label id="lblFechaExpedicion" runat="server" SkinID="etiqueta_negra" Text="Fecha de Expedición del Acto Administrativo:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox id="txtFechaExpedicion" runat="server" SkinID="texto_corto" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator id="rfvFechaExpedicion" runat="server" Display="Dynamic" ControlToValidate="txtFechaExpedicion" ErrorMessage="Ingrese fecha de expedición del acto administrativo">*</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator id="revFechaExpedicion" runat="server" ControlToValidate="txtFechaExpedicion" ErrorMessage="Formato de fecha no valido para la fecha de expedición del acto" ValidationExpression="^\d{2}\/\d{2}\/\d{4}">*</asp:RegularExpressionValidator>
                                                    <cc1:CalendarExtender id="calFechaExpedicion" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtFechaExpedicion"></cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label id="lblFechaEjecutoria" runat="server" SkinID="etiqueta_negra" Text="Fecha de ejecutoria del Acto que impone sanción  (dd/mm/aaaa):"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox id="txtFechaEjecutoria" runat="server" SkinID="texto_corto" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator id="rfvFechaEjecutoria" runat="server" Display="Dynamic" ControlToValidate="txtFechaEjecutoria" ErrorMessage="Ingrese la fecha de ejecutoria del acto">*</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator id="revFechaEjecutoria" runat="server" ControlToValidate="txtFechaEjecutoria" ErrorMessage="Formato de fecha no valido para la fecha de ejecutoria del acto" ValidationExpression="^\d{2}\/\d{2}\/\d{4}">*</asp:RegularExpressionValidator>
                                                    <cc1:CalendarExtender id="calFechaEjecutoria" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtFechaEjecutoria"></cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label id="lblFechaEjecucion" runat="server" SkinID="etiqueta_negra" Text="Fecha de ejecución o cumplimiento de la sanción  (dd/mm/aaaa):"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox id="txtFechaEjecucion" runat="server" SkinID="texto_corto" MaxLength="10"></asp:TextBox>
                                                    <asp:RegularExpressionValidator id="revFechaEjecucion" runat="server" ControlToValidate="txtFechaEjecucion" ErrorMessage="Formato de fecha no valido para la fecha de ejecución o cumplimiento de la sanción" ValidationExpression="^\d{2}\/\d{2}\/\d{4}">*</asp:RegularExpressionValidator>
                                                    <cc1:CalendarExtender id="calFechaEjecucion" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtFechaEjecucion"></cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="left">
                                                    <asp:Label id="lblObservaciones" runat="server" SkinID="etiqueta_negra" Text="Observaciones:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox id="txtObservaciones" runat="server" SkinID="texto" Rows="8" TextMode="MultiLine" Width="100%" style="resize: none;"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="titleUpdate" colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">Datos de Publicación</td>
                                            </tr>
                                            <tr>
                                                <td class="titleUpdate" colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label id="lblDescripcionDesfijacion" runat="server" SkinID="etiqueta_negra" Text="Descripción de la Desfijación de la publicación:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox id="txtDescripcionDesfijacion" runat="server" SkinID="texto"></asp:TextBox>
                                                    <asp:RequiredFieldValidator id="rfvDescripcionDesfijacion" runat="server" Display="Dynamic" ControlToValidate="txtDescripcionDesfijacion" ErrorMessage="Ingrese la descripción de la desfijación de la publicación">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="left">
                                                    <asp:Label id="lblMotivoModificacion" runat="server" SkinID="etiqueta_negra" Text="Motivo de la Modificación:" Visible="False"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox id="txtMotivoModificacion" runat="server" SkinID="texto" Height="100px" Visible="False" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="left">
                                                    <asp:Label id="lblTramiteModificacion" runat="server" SkinID="etiqueta_negra" Text="Reporte en Trámite de Modificación:" __designer:wfdid="w3" Visible="False"></asp:Label>
                                                </td>
                                                <td align=left>
                                                    <asp:DropDownList id="cboTramiteModificacion" runat="server"  Visible="False">
                                                        <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel runat="server" ID="tabDatosNatural" HeaderText="Datos de Persona Natural">
                                    <ContentTemplate>
                                        <table style="WIDTH: 70%">
                                            <tr>
                                                <td align="left">
                                                    <asp:Label id="lblPrimerNombre" runat="server" SkinID="etiqueta_negra" Text="Primer Nombre:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox id="txtPrimerNombre" runat="server" SkinID="texto"></asp:TextBox>
                                                    <asp:RequiredFieldValidator id="rfvPrimerNombre" runat="server" Display="Dynamic" ErrorMessage="Ingrese el primer nombre de Persona Natural" ControlToValidate="txtPrimerNombre">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label id="lblSegundoNombre" runat="server" SkinID="etiqueta_negra" Text="Segundo Nombre:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox id="txtSegundoNombre" runat="server" SkinID="texto"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label id="lblPrimerApellido" runat="server" SkinID="etiqueta_negra" Text="Primer Apellido:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox id="txtPrimerApellido" runat="server" SkinID="texto"></asp:TextBox>
                                                    <asp:RequiredFieldValidator id="rfvPrimerApellido" runat="server" Display="Dynamic" ErrorMessage="Ingrese el primer apellido de Persona Natural" ControlToValidate="txtPrimerApellido">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label id="lblSegundoApellido" runat="server" SkinID="etiqueta_negra" Text="Segundo Apellido:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox id="txtSegundoApellido" runat="server" SkinID="texto"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label id="lblTipoDocumento" runat="server" SkinID="etiqueta_negra" Text="Tipo de Documento:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList id="cboTipoDocumento" runat="server" SkinID="lista_desplegable"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label id="lblNumeroDocumento" runat="server" SkinID="etiqueta_negra" Text="Número de Documento:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox id="txtNumeroDocumento" runat="server" SkinID="texto"></asp:TextBox>
                                                    <asp:RequiredFieldValidator id="rfvNumeroDocumento" runat="server" Display="Dynamic" ErrorMessage="Ingrese número de documento de Persona Natural" ControlToValidate="txtNumeroDocumento">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="left">
                                                    <asp:Label id="lblOrigenDocumento" runat="server" SkinID="etiqueta_negra" Text="De:"></asp:Label>
                                                </td>
                                                <td valign="top" align="left">
                                                    <asp:DropDownList id="cboDepartamentoNatural" runat="server" SkinID="lista_desplegable" OnSelectedIndexChanged="cboDepartamentoNatural_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                    <br />
                                                    <asp:DropDownList id="cboMunicipioNatural" runat="server" SkinID="lista_desplegable"></asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel runat="server" ID="tabDatosJuidica" HeaderText="Datos de Persona Jur&#237;dica">
                                    <ContentTemplate>
                                        <table style="WIDTH: 70%">
                                            <TBODY>
                                                <tr>
                                                    <td align=left>
                                                        <asp:Label id="lblRazonSocial" runat="server" SkinID="etiqueta_negra" Text="Razón Social:"></asp:Label>
                                                    </td>
                                                    <td align=left>
                                                        <asp:TextBox id="txtRazonSocial" runat="server" SkinID="texto"></asp:TextBox>
                                                        <asp:RequiredFieldValidator id="rfvRazonSocial" runat="server" Display="Dynamic" ErrorMessage="Ingrese la Razón Social" ControlToValidate="txtRazonSocial">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align=left>
                                                        <asp:Label id="lblNit" runat="server" SkinID="etiqueta_negra" Text="NIT:"></asp:Label>
                                                    </td>
                                                    <td align=left>
                                                        <asp:TextBox id="txtNit" runat="server" SkinID="texto" MaxLength="11"></asp:TextBox>
                                                        <asp:RequiredFieldValidator id="rfvNit" runat="server" Display="Dynamic" ErrorMessage="Ingrese el NIT de la Razón Social (XXXXXXXXX-X)" ControlToValidate="txtNit">*</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator id="revNit" runat="server" Display="Dynamic" ErrorMessage="Formato no válido para el NIT de la Razón Social" ControlToValidate="txtNit" ValidationExpression="\d{5,9}-\d{1}">*</asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align=left>
                                                        <asp:Label id="lblPrimerNombreRepresentante" runat="server" SkinID="etiqueta_negra" Text="Primer Nombre Representante Legal:"></asp:Label>
                                                    </td>
                                                    <td align=left>
                                                        <asp:TextBox id="txtPrimerNombreRepresentante" runat="server" SkinID="texto"></asp:TextBox>
                                                        <asp:RequiredFieldValidator id="rfvPrimerNombreRepresentante" runat="server" Display="Dynamic" ErrorMessage="Ingrese el primer nombre del Representante Legal" ControlToValidate="txtPrimerNombreRepresentante">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align=left>
                                                        <asp:Label id="lblSegundoNombreRepresentante" runat="server" SkinID="etiqueta_negra" Text="Segundo Nombre Representante Legal:"></asp:Label>
                                                    </td>
                                                    <td align=left>
                                                        <asp:TextBox id="txtSegundoNombreRepresentante" runat="server" SkinID="texto"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align=left>
                                                        <asp:Label id="lblPrimerApellidoRepresentante" runat="server" SkinID="etiqueta_negra" Text="Primer Apellido Representante legal:"></asp:Label>
                                                    </td>
                                                    <td align=left>
                                                        <asp:TextBox id="txtPrimerApellidoRepresentante" runat="server" SkinID="texto"></asp:TextBox>
                                                        <asp:RequiredFieldValidator id="rfvPrimerApellidoRepresentante" runat="server" Display="Dynamic" ErrorMessage="Ingrese el primer apellido del Representante Legal" ControlToValidate="txtPrimerApellidoRepresentante">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align=left>
                                                        <asp:Label id="lblSegundoApellidoRepresentante" runat="server" SkinID="etiqueta_negra" Text="Segundo Apellido Representante legal:"></asp:Label>
                                                    </td>
                                                    <td align=left>
                                                        <asp:TextBox id="txtSegundoApellidoRepresentante" runat="server" SkinID="texto"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align=left>
                                                        <asp:Label id="lblTipoDocumentoRepresentante" runat="server" SkinID="etiqueta_negra" Text="Tipo de Documento:"></asp:Label>
                                                    </td>
                                                    <td align=left>
                                                        <asp:DropDownList id="cboTipoDocumentoJuridica" runat="server" SkinID="lista_desplegable"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align=left>
                                                        <asp:Label id="lblNumeroDocumentoRepresentante" runat="server" SkinID="etiqueta_negra" Text="Número de Documento:"></asp:Label>
                                                    </td>
                                                    <td align=left>
                                                        <asp:TextBox id="txtNumeroDocumentoRepresentante" runat="server" SkinID="texto"></asp:TextBox>
                                                        <asp:RequiredFieldValidator id="rfvNumeroDocumentoRepresentante" runat="server" Display="Dynamic" ErrorMessage="Ingrese el número de documento del Representante Legal" ControlToValidate="txtNumeroDocumentoRepresentante">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign=top align=left>
                                                        <asp:Label id="lblOrigenRepresentante" runat="server" SkinID="etiqueta_negra" Text="De:"></asp:Label>
                                                    </td>
                                                    <td valign=top align=left>
                                                        <asp:DropDownList id="cboDepartamentoJuridica" runat="server" SkinID="lista_desplegable" OnSelectedIndexChanged="cboDepartamentoJuridica_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                        <br />
                                                        <asp:DropDownList id="cboMunicipioJuridica" runat="server" SkinID="lista_desplegable"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </TBODY>
                                        </table>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                            </cc1:TabContainer> 
                        </contenttemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px; text-align: left; vertical-align: top;">
                    <asp:ValidationSummary id="valResumen" runat="server"></asp:ValidationSummary>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 20px; padding-bottom: 10px; padding-left: 10px; padding-right: 20px; text-align: right; vertical-align: middle;">
                    <table border="0" style="width: 100%;">
                        <tr>
                            <td style="padding-right: 30px; text-align: right; vertical-align: middle;">
                                <asp:Button id="btnAgregar" onclick="btnAgregar_Click" SkinID="boton" runat="server"  Text="Guardar RUIA"></asp:Button>
                            </td>
                            <td style="padding-left: 30px; text-align: left; vertical-align: middle;">
                                <asp:Button id="btnCancelar" onclick="btnCancelar_Click" SkinID="boton" runat="server"  Text="Cancelar" ValidationGroup="xxx" OnClientClick="return cancelarRUIA();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>  
</asp:Content>
