<%@ Page MaintainScrollPositionOnPostback="true" Title="" Language="C#" MasterPageFile="~/plantillas/SILPASinMenuFlash.master"
    AutoEventWireup="true" CodeFile="RegistroMinero.aspx.cs" Inherits="RegistroMinero_RegistroMinero" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlaceHolder" runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

    <asp:ScriptManager ID="sm" runat="server">
    </asp:ScriptManager>

    <div class="table-responsive">
        <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
            <tr>
                <td>
                    <asp:Label ID="lblTipoFalta0" runat="server" ToolTip="Identifica si es una Licencia Ambiental (LA) o Plan de Manejo Ambiental(PMA)"
                        SkinID="etiqueta_negra" Text="Tipo Registro:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboTipoRegistro" Width="70%" runat="server" ToolTip="Identifica si es una Licencia  Ambiental (LA)o Plan de Manejo Ambiental (PMA)">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="cboTipoRegistro"
                        ErrorMessage="Se requiere el campo Tipo Registro" Operator="NotEqual" SetFocusOnError="True"
                        ValueToCompare="-1">*</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTipoFalta" runat="server" SkinID="etiqueta_negra" Text="Nro. Acto Administrativo:"
                        ToolTip="Número del Acto Administrativo por el cual se otorga la Licencia o el Plan de Manejo"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNumeroActoAdmin" runat="server" Width="70%" SkinID="texto" MaxLength="50"
                        ToolTip="Número del Acto Administrativo por el cual se otorga la Licencia o el Plan de Manejo"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNumeroActoAdmin"
                        ErrorMessage="Se requiere el campo Nro. Acto Administrativo">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDescripcionNorma" runat="server" SkinID="etiqueta_negra" Text="Fecha Acto Administrativo:"
                        ToolTip="Fecha del Acto Administrativo por el cual se otorga la Licencia o el Plan de Manejo"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFechaActo" Width="30%" runat="server" MaxLength="10" ToolTip="Fecha del Acto Administrativo por el cual se otorga la Licencia o el Plan de Manejo"></asp:TextBox><cc1:CalendarExtender
                        ID="caltxttxtFechaActo" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaActo">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFechaActo"
                        ErrorMessage="Se requiere el campo Fecha Acto Administrativo" Display="Dynamic">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="txtFechaActo"
                        ErrorMessage="El campo Fecha Acto Administrativo no tiene el formato correcto"
                        Operator="DataTypeCheck" SetFocusOnError="True" Type="Date" Display="Dynamic">*</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLugarOcurrencia" runat="server" SkinID="etiqueta_negra" Text="Nro. Expediente:"
                        ToolTip="Número de expediente Asignado por la Autoridad Ambiental"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNumeroExpediente" Width="70%" runat="server" SkinID="texto" MaxLength="50"
                        ToolTip="Número de expediente Asignado por la Autoridad Ambiental"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNumeroExpediente"
                        ErrorMessage="Se requiere el campo Nro. Expediente">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDepartamentoOcurrencia" runat="server" SkinID="etiqueta_negra"
                        Text="Autoridad Ambiental:" ToolTip="Autoridad Ambiental competente para otorgarla Licencia o el Plan de Manejo"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboAutoridadAmbiental" Width="70%" runat="server" ToolTip="Autoridad Ambiental competente para otorgarla Licencia o el Plan de Manejo">
                        <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                    </asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="cboAutoridadAmbiental"
                        ErrorMessage="Se requiere el campo Autoridad Ambiental" Operator="NotEqual" SetFocusOnError="True"
                        ValueToCompare="-1">*</asp:CompareValidator>
                </td>
            </tr>
            <tr style="background-color: #F9F9F9">
                <td style="vertical-align: top;">
                    <asp:Label ID="lblMunicipioOcurrencia" runat="server" SkinID="etiqueta_negra" Text="Datos de Propietario:"
                        ToolTip="Titular de la Autorización Ambiental"></asp:Label>
                </td>
                <td>
                    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%;">
                        <tr>
                            <td>
                                <asp:Label ID="lblMunicipioOcurrencia0" runat="server" SkinID="etiqueta_negra" Text="Titular:"
                                    ToolTip="Titular de la Autorización Ambiental"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombreOperador" Width="60%" runat="server" SkinID="texto" CausesValidation="True"
                                    ValidationGroup="Operador" MaxLength="150" ToolTip="Titular de la Autorización Ambiental"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtNombreOperador"
                                    ErrorMessage="Se requiere Nombre del Operador" ValidationGroup="Operador">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMunicipioOcurrencia1" runat="server" SkinID="etiqueta_negra" Text="Identificación:"
                                    ToolTip="Número de identificación del Titular de la Autorización Ambiental"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtIdentifOperador" Width="60%" runat="server" SkinID="texto" CausesValidation="True"
                                    ValidationGroup="Operador" MaxLength="20" ToolTip="Número de identificación del Titular de la Autorización Ambiental"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtIdentifOperador"
                                    ErrorMessage="Se requiere Identificación del Operador" ValidationGroup="Operador">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding-top: 20px; padding-bottom: 10px; padding-left: 10px; padding-right: 20px; text-align: left; vertical-align: middle;">
                                <asp:Button ID="btnAgregarSec0" runat="server" SkinID="boton" Text="+" OnClick="btnAgregarOperador_Click"
                                    ValidationGroup="Operador"></asp:Button>
                                <asp:Button ID="btnQuitarSec0" runat="server" SkinID="boton" Text="-" CausesValidation="False"
                                    OnClick="btnQuitarOperador_Click"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding: 0; text-align: left; vertical-align: top;">
                                <asp:ListBox ID="lstOperador" Width="100%" runat="server"></asp:ListBox>
                                <asp:Label ID="Loperador" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCorregimientoOcurrencia" runat="server" SkinID="etiqueta_negra"
                        Text="Cod. Registro Minero:" ToolTip="Código del registro Minero del Titulo"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCodRegMineria" Width="70%" runat="server" SkinID="texto" MaxLength="50"
                        ToolTip="Código del registro Minero del Titulo"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCodRegMineria"
                        ErrorMessage="Se requiere el campo Cod. Registro Minería">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="background-color: #F9F9F9">
                <td style="vertical-align: top;">
                    <asp:Label ID="lblVeredaOcurrencia" runat="server" SkinID="etiqueta_negra" Text="Minerales:"
                        ToolTip="Mineral (es) o Material (es) Objeto dela Explotación del Título Minero"></asp:Label>
                </td>
                <td>
                    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblMunicipioOcurrencia2" runat="server" SkinID="etiqueta_negra" Text="Descripción:"
                                    ToolTip="Mineral (es) o Material (es) Objeto dela Explotación del Título Minero"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboMineral" Width="60%" runat="server" CausesValidation="True"
                                    ValidationGroup="Minerales" ToolTip="Mineral (es) o Material (es) Objeto dela Explotación del Título Minero">
                                    <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="cboMineral"
                                    ErrorMessage="Se requiere el campo Descripción Mineral" Operator="NotEqual" SetFocusOnError="True"
                                    ValueToCompare="-1" ValidationGroup="Minerales">*</asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding-top: 20px; padding-bottom: 10px; padding-left: 10px; padding-right: 20px; text-align: left; vertical-align: middle;">
                                <asp:Button ID="btnAgregarMineral" runat="server" SkinID="boton" Text="+" OnClick="btnAgregarMineral_Click"
                                    ValidationGroup="Minerales"></asp:Button>
                                <asp:Button ID="btnQuitarMineral" runat="server" SkinID="boton" Text="-" CausesValidation="False"
                                    OnClick="btnQuitarMineral_Click"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding: 0; text-align: left; vertical-align: top;">
                                <asp:ListBox ID="lstMineral" Width="100%" runat="server"></asp:ListBox>
                                <asp:Label ID="Lmineral" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSancionAplicada" runat="server" SkinID="etiqueta_negra" Text="Fecha de expiración Licencia o Plan de Manejo:"
                        ToolTip="Fecha en que expira la Licencia Ambiental o el Plan de Manejo."></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFechaExpedicion" runat="server" Width="30%" MaxLength="10" ToolTip="Fecha en que expira la Licencia Ambiental o el Plan de Manejo."></asp:TextBox>
                    <cc1:CalendarExtender ID="CalFechaExpedicion" runat="server" Enabled="True" Format="dd/MM/yyyy"
                        TargetControlID="txtFechaExpedicion">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtFechaExpedicion"
                        ErrorMessage="Se requiere el campo Fecha Exp" Display="Dynamic">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtFechaExpedicion"
                        ErrorMessage="El campo Fecha Exp no tiene el formato correcto" Operator="DataTypeCheck"
                        SetFocusOnError="True" Type="Date" Display="Dynamic">*</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSecundaria" runat="server" SkinID="etiqueta_negra" Text="Vigencia del proyecto"
                        ToolTip="Vigencia de la Licencia o Plan de Manejo"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboVigencia" Width="29%" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="cboVigencia_SelectedIndexChanged" ToolTip="Vigencia de la Licencia o Plan de Manejo">
                        <asp:ListItem Selected="Fecha">Fecha</asp:ListItem>
                        <asp:ListItem Value="No Tiene">No Tiene</asp:ListItem>
                        <asp:ListItem Value="Vida Útil del Proyecto">Vida Útil del Proyecto</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;<asp:TextBox ID="txtVigencia" runat="server" Width="30%" SkinID="texto" MaxLength="3"
                        ToolTip="Fecha Vigencia de la Licencia o Plan de Manejo"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                        TargetControlID="txtVigencia">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RFVvigencia" runat="server" ControlToValidate="txtVigencia"
                        ErrorMessage="Se requiere el campo Vigencia del proyecto" Display="Dynamic">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="RFVvigenciaFormato" runat="server" ControlToValidate="txtVigencia"
                        ErrorMessage="El campo Vigencia del proyecto no tiene el formato correcto" Operator="DataTypeCheck"
                        SetFocusOnError="True" Type="Date" Display="Dynamic">*</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTipoSecundaria" runat="server" SkinID="etiqueta_negra" Text="Estado"
                        ToolTip="Estado Actual del Proyecto"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboTipoEstado" Width="70%" runat="server" ToolTip="Estado Actual del Proyecto">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="cboTipoEstado"
                        ErrorMessage="Se requiere el campo Tipo Estado" Operator="NotEqual" SetFocusOnError="True"
                        ValueToCompare="-1">*</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSancionAplicadaSec0" runat="server" SkinID="etiqueta_negra" Text="Nombre Proyecto:"
                        ToolTip="Nombre del Proyecto Objeto del Licenciamiento"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNombreProyecto" Width="70%" runat="server" SkinID="texto" MaxLength="100"
                        ToolTip="Nombre del Proyecto Objeto del Licenciamiento"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtNombreProyecto"
                        ErrorMessage="Se requiere el campo Nombre Proyecto">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSancionAplicadaSec1" runat="server" SkinID="etiqueta_negra" Text="Área hectáreas:"
                        ToolTip="Área del Polígono a la cual se le estableció la autorización ambiental"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAreaHectareas" runat="server" Width="70%" SkinID="texto" MaxLength="18"
                    
                        ToolTip="Área del Polígono a la cual se le estableció la autorización ambiental, si se ingresa un número decimal este debe estar separado por una coma (,)"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtAreaHectareas"
                        ErrorMessage="Se requiere el campo Área hectáreas" Display="Dynamic">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="txtAreaHectareas"
                        ErrorMessage="El campo Área hectáreas no tiene el formato correcto, debe ser numérico o decimal y el separador decimal  debe se una coma (,)"
                        Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValueToCompare="0"
                        Display="Dynamic">*</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSancionAplicadaSec2" runat="server" SkinID="etiqueta_negra" Text="Nombre de la Mina:"
                        ToolTip="Nombre de la Mina a la cual pertenece el proyecto"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNombreMina" runat="server" Width="70%" SkinID="texto" MaxLength="150"
                        ToolTip="Nombre de la Mina a la cual pertenece el proyecto"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtNombreMina"
                        ErrorMessage="Se requiere el campo Nombre Mina">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="background-color: #F9F9F9">
                <td style="vertical-align: top;">
                    <asp:Label ID="lblSancionAplicadaSec3" runat="server" SkinID="etiqueta_negra" Text="Ubicación:"></asp:Label>
                </td>
                <td>
                    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblMunicipioOcurrencia3" runat="server" SkinID="etiqueta_negra" Text="Departamento:"
                                    ToolTip="Seleccione el nombre del departamento al que corresponda el proyecto"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboDepartamento" Width="60%" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="cboDepartamento_SelectedIndexChanged" ValidationGroup="Ubicacion"
                                    ToolTip="Seleccione el nombre del departamento al que corresponda el proyecto">
                                    <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="cboDepartamento"
                                    ErrorMessage="Se requiere el campo Departamento" Operator="NotEqual" SetFocusOnError="True"
                                    ValueToCompare="-1" ValidationGroup="Ubicacion">*</asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMunicipioOcurrencia4" runat="server" SkinID="etiqueta_negra" Text="Municipio:"
                                    ToolTip="Seleccione el nombre del municipio al que corresponda el proyecto"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboMunicipio" Width="60%" runat="server" ValidationGroup="Ubicacion"
                                    ToolTip="Seleccione el nombre del municipio al que corresponda el proyecto">
                                    <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="cboMunicipio"
                                    ErrorMessage="Se requiere el campo Municipio" Operator="NotEqual" SetFocusOnError="True"
                                    ValueToCompare="-1" ValidationGroup="Ubicacion">*</asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding-top: 20px; padding-bottom: 10px; padding-left: 10px; padding-right: 20px; text-align: left; vertical-align: middle;">
                                <asp:Button ID="btnAgregarUbicacion" runat="server" SkinID="boton" Text="+" OnClick="btnAgregarUbicacion_Click"
                                    ValidationGroup="Ubicacion"></asp:Button>
                                <asp:Button ID="btnQuitarUbicacion" runat="server" SkinID="boton" Text="-" CausesValidation="False"
                                    OnClick="btnQuitarUbicacion_Click"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding: 0; text-align: left; vertical-align: top;">
                                <asp:ListBox ID="lstUbicacion" Width="100%" runat="server"></asp:ListBox>
                                <asp:Label ID="Lubicacion" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;">
                    <asp:Label ID="Label1" runat="server" SkinID="etiqueta_negra" Text="Observaciones"
                        ToolTip="Descripción del polígono o  área del proyecto"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtObservaciones" runat="server" Rows="3" TextMode="MultiLine" Width="100%"
                        ToolTip="Descripción del polígono o  área del proyecto" MaxLength="2000"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblOpcionPrincipal" runat="server" SkinID="etiqueta_negra" Text="Adjuntar Archivo Acto Administrativo:"
                        ToolTip="Adjuntar resolución con la que se otorgo Licencia Ambiental o estableció Plan de Manejo"></asp:Label>
                </td>
                <td>
                    <asp:FileUpload ID="fldArchivoActo" runat="server" Width="60%" ToolTip="Adjuntar resolución con la que se otorgo Licencia Ambiental o estableció Plan de Manejo" />
                    <asp:RequiredFieldValidator ID="RRFVarchivo" runat="server" ControlToValidate="fldArchivoActo"
                        ErrorMessage="Se requiere el campo Archivo Acto">*</asp:RequiredFieldValidator>
                    <asp:LinkButton ID="LBarchivo" runat="server" CausesValidation="False" Visible="False"
                        Enabled="False"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LBestadoArchivo" runat="server" CausesValidation="False"
                        OnClick="LBestadoArchivo_Click" Visible="False" ToolTip="En caso de Modificar el Archivo adjunto Eliminar el Actual.">(X)</asp:LinkButton>
                    <cc1:ConfirmButtonExtender ID="cbe" runat="server" TargetControlID="LBestadoArchivo"
                        ConfirmText="¿Se eliminara el archivo de forma permanente, esta seguro?"/>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding-top: 20px; padding-bottom: 10px; padding-left: 10px; padding-right: 20px; text-align: right; vertical-align: middle;">
                    <asp:Button ID="btnRadicarRegMinero" runat="server" Text="Siguiente >" SkinID="boton"
                        OnClick="btnRadicarRegMinero_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding: 10px; text-align: left; vertical-align: middle;">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                        ShowMessageBox="True" ShowSummary="False" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
