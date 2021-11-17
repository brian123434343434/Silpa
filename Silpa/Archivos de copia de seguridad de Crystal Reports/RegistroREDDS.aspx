<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/Silpa.master" AutoEventWireup="true" CodeFile="RegistroREDDS.aspx.cs" Inherits="REDDS_RegistroREDDS" EnableEventValidation="false" %>

<%@ Register Assembly="FUA" Namespace="Subgurim.Controles" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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

    <script src="../jquery/jquery.js"></script>
    <link href="../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../jquery/EstiloGris/jquery-ui.js"></script>
    <script src="../jquery/jquery.numeric.js"></script>
    <link href="../App_Themes/skin/StyleREDDS.css" rel="stylesheet" />
    
    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="REGISTRO REDD+" SkinID="titulo_principal_blanco"></asp:Label>
        &nbsp;
        <a href="#" id="hrCerrarVentana" visible="false" runat="server" onclick="window.close();return false;">Salir</a>
    </div>
     
    <%--<div class="div-contenido">--%>
    <div class="table-responsive">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <cc1:TabContainer ID="tbcContenedor" runat="server" Width="100%"
            ActiveTabIndex="0">
            <cc1:TabPanel runat="server" HeaderText="Información General" ID="tabInfoGeneral">
                <ContentTemplate>
                    <div class="contact_form">
                        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                            <tr>
                                <td>
                                    <label for="txtNombreRazonSocial">Nombre o razón social del responsable de la iniciativa:</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNombreRazonSocial" runat="server" placeholder="Ingrese el nombre o razon social" Width="350px" />
                                    <asp:RequiredFieldValidator ID="rfvNombreRazonSocial" Display="Dynamic" runat="server" ControlToValidate="txtNombreRazonSocial" ValidationGroup="redds" ErrorMessage="Información General/Nombre Razon Social">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="txtRelacionJuridica">Relación jurídica del responsable de la iniciativa con los predios que la conforman:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upnlRelacionJuridica" runat="server">
                                    <ContentTemplate>
                                        <div>
                                            <asp:DropDownList ID="cboRelacionJuridica" runat="server" OnSelectedIndexChanged="cboRelacionJuridica_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                        </div>
                                        <div id="divOtro" runat="server" visible="false">
                                            <asp:TextBox ID="txtRelacionJuridica" runat="server" placeholder="Ingrese la Relación Juridica" Width="250px" />   
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cboRelacionJuridica" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td><label for="txtNombreIniciativa">Nombre Iniciativa:</label></td>
                                <td>
                                    
                                    <asp:TextBox ID="txtNombreIniciativa" runat="server" placeholder="Ingrese el nombre de la iniciativa" Width="350px" />
                                    <asp:RequiredFieldValidator ID="rfvNombreIniciativa" runat="server" ControlToValidate="txtNombreIniciativa" ValidationGroup="redds" ErrorMessage="Información General/Nombre Iniciativa">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="participante">Particiantes Iniciativa:</label>
                                </td>
                                <td>
                                    <table width="80%">
                                        <tr>
                                            <td>
                                                Nombre:
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="upnlParticipante" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtNombreParticipante" runat="server" Width="300px" />
                                                        <asp:RequiredFieldValidator ID="rfvNombreParticipante" runat="server" ControlToValidate="txtNombreParticipante" ValidationGroup="Participante">*</asp:RequiredFieldValidator>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnAgregarParticipante" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnQuitarParticipante" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                                                <asp:UpdatePanel ID="upnlAccionParticipante" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnAgregarParticipante" runat="server" OnClick="btnAgregarParticipante_Click" SkinID="boton" Text="+" ValidationGroup="Participante" />
                                                        <asp:Button ID="btnQuitarParticipante" runat="server" CausesValidation="False" OnClick="btnQuitarParticipante_Click" SkinID="boton" Text="-" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                                <td></td>
                                                <td>
                                                    <asp:UpdatePanel ID="upnlListParticipante" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ListBox ID="lstParticipante" runat="server"></asp:ListBox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnAgregarParticipante" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnQuitarParticipante" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                               <td>
                                   <label for="cboEstadoIniciativa">Estado de avance de la iniciativa:</label>
                               </td>
                               <td>
                                   <asp:DropDownList ID="cboEstadoIniciativa" runat="server"></asp:DropDownList>
                               </td>
                            </tr>
                    </table>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="Caracterización" ID="tabCaracterizacion">
                <ContentTemplate>
                    <div class="contact_form">
                        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                            <tr>
                                <td>
                                    <label for="txtCostoEstiamdoFormulario">Costos estimados de la formulación e implementación de la iniciativa  en pesos colombianos  calculados al año del registro.:</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCostoEstiamdoFormulario" runat="server" placeholder="Número de 1 a 200 mil millones" ClientIDMode="Static" />
                                    <asp:RangeValidator MinimumValue="1000000000" runat="server" MaximumValue="200000000000" ControlToValidate="txtCostoEstiamdoFormulario" Type="Currency"></asp:RangeValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="txtFechaInicioIniciativa">Fecha de inicio de implementación iniciativa (si no se ha definido, fecha estimada de inicio):</label>
                                </td>
                                <td>
                                <asp:UpdatePanel ID="upnlFechaIniIniciativa" runat="server">
                                    <ContentTemplate>
                                        
                                        <asp:TextBox ID="txtFechaInicioIniciativa" ClientIDMode="Static" runat="server" Width="70px" />
                                        <asp:RequiredFieldValidator ID="rfvFechaInicioIniciativa" runat="server" ControlToValidate="txtFechaInicioIniciativa" ValidationGroup="redds" ErrorMessage="Caracterización/Fecha de inicio de implementación iniciativa">*</asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvFechaInicioIniciativaEmi" runat="server" ControlToValidate="txtFechaInicioIniciativa" ValidationGroup="Emision" ErrorMessage="">*</asp:RequiredFieldValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                    </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="txtFechaFinalIniciativa">Fecha de finalización de implementación de la iniciativa (si no se ha definido, fecha estimada de finalización):</label>
                                </td>
                                <td>
                                <asp:UpdatePanel ID="upnlFechaFinIniciativa" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtFechaFinalIniciativa" ClientIDMode="Static" runat="server" Width="70px" OnTextChanged="txtFechaFinalIniciativa_TextChanged" AutoPostBack="true" />
                                        <asp:RequiredFieldValidator ID="rfvFechaFinalIniciativa" runat="server" ControlToValidate="txtFechaFinalIniciativa" ValidationGroup="redds" ErrorMessage="Caracterización/Fecha de finalización de implementación de la iniciativa">*</asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvFechaFinalIniciativaEmi" runat="server" ControlToValidate="txtFechaInicioIniciativa" ValidationGroup="Emision" ErrorMessage="">*</asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="mine" runat="server" ControlToCompare="txtFechaInicioIniciativa" ControlToValidate="txtFechaFinalIniciativa" Type="Date" Operator="GreaterThan" ErrorMessage="Fecha final debe ser mayor a la Inicial" Display="Dynamic"></asp:CompareValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                    </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="participante">Estimado de reducción de emisiones (Ton CO2eq/año) y número de emisiones verificadas:</label>
                                </td>
                                <td>
                                    
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="upnlValorEmisiones" runat="server">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnAgregarEmision" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFechaFinalIniciativa" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <table>
                                                            <tr>
                                                                <td width="40%">Estimado de reducción de emisiones (Ton CO2eq/año):
                                                                </td>
                                                                <td width="40%">Emisiones verificadas al momento del registro (Ton CO2eq/año):
                                                                </td>
                                                                <td  width="20%"></td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <asp:TextBox ID="txtValorEmisiones" runat="server" ClientIDMode="Static" MaxLength="9" /><asp:RequiredFieldValidator ID="rfvValorEmisiones" runat="server" ControlToValidate="txtValorEmisiones" Display="Dynamic" ValidationGroup="Emision">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td>
                                                                   
                                                                    <asp:TextBox ID="txtValorVerificadas" runat="server" ClientIDMode="Static" MaxLength="9" /><asp:RequiredFieldValidator ID="frvValorVerificadas" runat="server" ControlToValidate="txtValorVerificadas" Display="Dynamic" ValidationGroup="Emision">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td>
                                                                   Año:
                                                                   <asp:DropDownList ID="cboNumeroAños" runat="server" ClientIDMode="Static" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <asp:CompareValidator runat="server" ID="CVEmicionesVSVerificadas" ControlToCompare="txtValorEmisiones" ControlToValidate="txtValorVerificadas" Operator="LessThanEqual" Type="Double" ErrorMessage="El valor de las Emisiones Verificadas no puede ser mayor a las Emisiones Reducidas" ValidationGroup="Emision" Display="Dynamic"></asp:CompareValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="upnlAccionEmision" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnAgregarEmision" runat="server" SkinID="boton" Text="+" ValidationGroup="Emision" OnClick="btnAgregarEmision_Click"></asp:Button>
                                                        <asp:Button ID="btnQuitarEmision" runat="server" SkinID="boton" Text="-" CausesValidation="False" OnClick="btnQuitarEmision_Click"></asp:Button>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="upnlLstEmision" runat="server">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnAgregarEmision" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnQuitarEmision" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:ListBox ID="lstEmisiones" runat="server"></asp:ListBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="participante">
                                        Estimado de Reducción de deforestación de la iniciativa en Hectáreas por año:</label>
                                </td>
                                <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="upnlValorEmisionesDeforestacion" runat="server">
                                                        <ContentTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>Estimado de reducción de deforestación Ha: </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtValorEstimadoDeforestacion" runat="server" ClientIDMode="Static" MaxLength="9" />
                                                                        <asp:RequiredFieldValidator ID="rfvValorEstimadoDeforestacion" runat="server" ControlToValidate="txtValorEstimadoDeforestacion" Display="Dynamic" ValidationGroup="Deforestacion">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td>Año:
                                                                        <asp:DropDownList ID="cboNumeroAñosDeforestacion" runat="server" ClientIDMode="Static" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnAgregarEmision" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtFechaFinalIniciativa" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="upnlAccionEmisionDeforestacion" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnAgregarEmisionDeforestacion" runat="server" OnClick="btnAgregarEmisionDeforestacion_Click" SkinID="boton" Text="+" ValidationGroup="Deforestacion" />
                                                            <asp:Button ID="btnQuitarEmisionDeforestacion" runat="server" CausesValidation="False" OnClick="btnQuitarEmisionDeforestacion_Click" SkinID="boton" Text="-" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="upnlLstEmisionDeforestacion" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ListBox ID="lstEmisionesDeforestacion" runat="server"></asp:ListBox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnAgregarEmisionDeforestacion" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnQuitarEmisionDeforestacion" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="ActividadREDDS">
                                        Actividades REDD+ que son incluidas en la iniciativa: (Máximo 5)</label>
                                </td>
                                <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    Actividad REDD+:
                                                    <asp:DropDownList ID="cboActividadREDDS" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvActividadREDDS" runat="server" ControlToValidate="cboActividadREDDS" Display="Dynamic" ValidationGroup="Actividad">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="upnlActividadRedds" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnAgregarActividad" runat="server" OnClick="btnAgregarActividad_Click" SkinID="boton" Text="+" ValidationGroup="Actividad" />
                                                            <asp:Button ID="btnQuitarActividad" runat="server" CausesValidation="false" OnClick="btnQuitarActividad_Click" SkinID="boton" Text="-" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="upnlLstActividad" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ListBox ID="lstActividadRedds" runat="server"></asp:ListBox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnAgregarActividad" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnQuitarActividad" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="Compartimientos">
                                        Compartimientos de carbono que son incluídos: (Máximo 5)</label>
                                </td>
                                <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    Compartimientos:
                                                    <asp:DropDownList ID="cboCompartimientos" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvCompartimientos" runat="server" ControlToValidate="cboCompartimientos" Display="Dynamic" ValidationGroup="Compar">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="upnlAccionCompartimento" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnAgregarCompartimento" runat="server" OnClick="btnAgregarCompartimento_Click" SkinID="boton" Text="+" ValidationGroup="Compar" />
                                                            <asp:Button ID="btnEliminarCompar" runat="server" CausesValidation="false" OnClick="btnQuitarCompar_Click" SkinID="boton" Text="-" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="upnlCompartimentoRedds" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:ListBox ID="lstCompartimientos" runat="server"></asp:ListBox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnAgregarCompartimento" EventName="Click" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnEliminarCompar" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="participante">
                                        Estándar del mercado voluntario bajo el cual se buscará la certificación:</label>
                                </td>
                                <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="upnlEstandarMercado" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtEstandarMercado" runat="server" Width="350px" />
                                                            <asp:RequiredFieldValidator ID="rfvEstandarMercado" runat="server" ControlToValidate="txtEstandarMercado" ValidationGroup="Estandar">*</asp:RequiredFieldValidator>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnAgregarEstandar" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnQuitarEstandar" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="upnlEstandarMercadoAcciones" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnAgregarEstandar" runat="server" OnClick="btnAgregarEstandar_Click" SkinID="boton" Text="+" ValidationGroup="Estandar" />
                                                            <asp:Button ID="btnQuitarEstandar" runat="server" CausesValidation="False" OnClick="btnQuitarEstandar_Click" SkinID="boton" Text="-" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="upnlEstandarMercadoLst" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ListBox ID="lstEstandarMercado" runat="server"></asp:ListBox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnAgregarEstandar" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnQuitarEstandar" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="txtFechaFinalIniciativa">
                                        Metodología del estándar del mercado voluntario bajo la cual se buscará la certificación:</label>
                                </td>
                                <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    
                                                    <asp:UpdatePanel ID="upnlMetodologia" runat="server">
                                                        <ContentTemplate>
                                                            Estándar:<asp:DropDownList ID="cboEstandarMercado" runat="server" ValidationGroup="Metodologia">
                                                            </asp:DropDownList>
                                                            <br />
                                                            <asp:TextBox ID="txtMetodologiaEstandar" runat="server" Width="350px" />
                                                            <asp:RequiredFieldValidator ID="rfvMetodologiaEstandar" runat="server" ControlToValidate="txtMetodologiaEstandar" ValidationGroup="Metodologia">*</asp:RequiredFieldValidator>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnAgregarMetodologia" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnQuitarMetodologia" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="upnlMetodologiaAcciones" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnAgregarMetodologia" runat="server" OnClick="btnAgregarMetodologia_Click" SkinID="boton" Text="+" ValidationGroup="Metodologia" />
                                                            <asp:Button ID="btnQuitarMetodologia" runat="server" CausesValidation="False" OnClick="btnQuitarMetodologia_Click" SkinID="boton" Text="-" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="upnlLstMetodologias" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ListBox ID="lstMetodologia" runat="server"></asp:ListBox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnAgregarMetodologia" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnQuitarMetodologia" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="Localización" ID="tabLocalizacion">
                <ContentTemplate>
                    <div class="contact_form">
                        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                            <tr>
                            <td>
                            <label for="cboEstadoIniciativa">Autoridad (es) Ambiental (es) en cuya jurisdicción se localiza la iniciativa:</label>
                                </td>
                            <td>
                                <table>
                                <tr>
                                    <td>
                                        Autoridad Ambiental:
                                        <asp:DropDownList ID="cboAutoridadAmbiental" runat="server"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAutoridadAmbiental" runat="server" ControlToValidate="cboAutoridadAmbiental" Display="Dynamic" ValidationGroup="Autoridad">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="upnlAccionAutoridad" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnAgregarAutoridad" runat="server" SkinID="boton" Text="+" ValidationGroup="Autoridad" OnClick="btnAgregarAutoridad_Click"></asp:Button>
                                                <asp:Button ID="btnQuitarAutoridad" runat="server" SkinID="boton" Text="-" CausesValidation="False" OnClick="btnQuitarAutoridad_Click"></asp:Button>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="upnlLstAutoridades" runat="server">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnAgregarAutoridad" />
                                                <asp:AsyncPostBackTrigger ControlID="btnQuitarAutoridad" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:ListBox ID="lstAutoridadRedds" runat="server"></asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                            </td>
                        </tr>
                            <tr>
                                <td>
                                    <label for="cboEstadoIniciativa">
                                        Departamento (s) y Municipio (s) en los que se localiza la iniciativa:</label>
                                </td>
                                <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    Departamento:
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="udplDepartamento" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="cboDepartamento" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboDepartamento_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvDepartamento" runat="server" ControlToValidate="cboDepartamento" Display="Dynamic" ValidationGroup="DeptoMuni">*</asp:RequiredFieldValidator>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   Municipio:
                                                </td>
                                                <td>
                                                        <asp:UpdatePanel ID="udplMunicipio" runat="server">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="cboMunicipio" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvMunicipio" runat="server" ControlToValidate="cboMunicipio" Display="Dynamic" ValidationGroup="DeptoMuni">*</asp:RequiredFieldValidator>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="cboDepartamento" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnAgregarDeptoMuni" runat="server" OnClick="btnAgregarDeptoMuni_Click" SkinID="boton" Text="+" ValidationGroup="DeptoMuni" />
                                                    <asp:Button ID="btnQuitarDeptoMuni" runat="server" CausesValidation="False" OnClick="btnQuitarDeptoMuni_Click" SkinID="boton" Text="-" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="upnlDeptoMuni" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ListBox ID="lstDeptoMuni" runat="server"></asp:ListBox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnAgregarDeptoMuni" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnQuitarDeptoMuni" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                        <label for="lblLocalizaciones">
                                        Detalle de localización del (los) polígono(s) de intervención en coordenadas del Sistema de Coordenadas Geográficas Magna Colombia Bogotá:</label>
                                </td>
                                <td>
                                        <asp:UpdatePanel ID="upnlLocalizaciones" runat="server">
                                            <ContentTemplate>
                                                <table>
                                                <tr>
                                                    <td>
                                                        Tipo Geometria:
                                                        <asp:DropDownList ID="lst_tipo_geometria" runat="server" ToolTip="Diligenciar polígono">
                                                            <asp:ListItem Value="2">Polígono</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtCoordenadas" runat="server" Rows="10" TextMode="MultiLine" ToolTip="Las coordenadas se deben ingresar en el sistema WGS84, el formato del numero debe ser Decimal utilizando el punto como separador y el orden debe ser Latitud, Longitud separado por coma. Ejemplo 1.05987,-66.056987" ValidationGroup="localizaciones" Width="80%"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="revcoordenadas" runat="server" ControlToValidate="txtCoordenadas" Display="Dynamic" Text="Solo se aceptan caracateres númericos" ValidationExpression="^[0-9 ,.-]*$">
                                                    </asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator ID="rfvtxtCoordenadas" runat="server" ControlToValidate="txtCoordenadas" Display="Dynamic" ValidationGroup="localizacion">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="dgv_localizaciones" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="LocID" OnDataBound="dgv_localizaciones_DataBound" OnRowCommand="dgv_localizaciones_RowCommand" OnRowDeleting="dgv_localizaciones_RowDeleting" PageSize="4" ShowFooter="True" SkinID="GrillaCoordenadas">
                                                            <FooterStyle />
                                                            <Columns>
                                                                <asp:BoundField AccessibleHeaderText="Tipo de Geometría" HeaderText="Tipo de Geometría" />
                                                                <asp:BoundField AccessibleHeaderText="Puntos" HeaderText="Puntos" />
                                                                <asp:BoundField AccessibleHeaderText="Grados" HeaderText="Grados" />
                                                                <asp:TemplateField AccessibleHeaderText="Editar" HeaderText="Editar">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imb_editar" runat="server" CausesValidation="False" CommandArgument='<%# Bind("LocID") %>' CommandName="editar" ImageUrl="~/App_Themes/Img/Edit.png" ToolTip="Haga clic aquí para Editar" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField AccessibleHeaderText="Eliminar" HeaderText="Eliminar">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imb_borrar" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/App_Themes/Img/Del.png" ToolTip="Haga clic aquí para borrar los items seleccionados" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerSettings FirstPageImageUrl="~/App_Themes/Img/pagina_primera.gif" FirstPageText="Primera" LastPageImageUrl="~/App_Themes/Img/pagina_ultima.gif" LastPageText="Ultima" Mode="NumericFirstLast" NextPageImageUrl="~/App_Themes/Img/pagina_siguiente.gif" NextPageText="Siguiente" PreviousPageImageUrl="~/App_Themes/Img/pagina_anterior.gif" PreviousPageText="Anterior" />
                                                        </asp:GridView>
                                                        <asp:Label ID="lbl_sel_todos" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" ValidationGroup="localizaciones1" Visible="False" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="lst_tipo_geometria" Display="None" ErrorMessage="El tipo de geometría es requerido" ValidationGroup="localizaciones1"></asp:RequiredFieldValidator>
                                                        <asp:Button ID="btn_adicionar_localizacion" runat="server" OnClick="btn_adicionar_localizacion_Click" SkinID="boton" Text="Adicionar localización" ToolTip="Haga clic para adicionar localización" ValidationGroup="localizacion" />
                                                        <asp:Button ID="btnCancelarEdicion" runat="server" CausesValidation="false" OnClick="btnCancelarEdicion_Click" SkinID="boton" Text="Cancelar Edición" Visible="false" />
                                                    </td>
                                                </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="txtAreaInfluencia">
                                        Area de Influencia (Héctareas):</label>
                                </td>
                                <td>
                                        <asp:TextBox ID="txtAreaInfluencia" runat="server" ClientIDMode="Static" placeholder="Número de 1 a 100 millones"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAreaInfluencia" runat="server" ControlToValidate="txtAreaInfluencia" ErrorMessage="Localización/Area de Influencia" ValidationGroup="redds">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="DocumentosAdicionales" ID="tabDocAdicionales">
                <ContentTemplate>
                    <div class="title_pretty">"Los documentos que se adjunten estarán visibles para el público en general, es responsabilidad del usuario la información que adjunte"</div>
                    <div class="contact_form">
                        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                            <tr>
                                <td>
                                    <label for="fuplPDDoPIN">Documento de Diseño del proyecto o Programa o Nota de Idea de Proyecto:</label>
                                </td>
                                    <td>
                                        <div style="width: 500px;">
                                            <cc2:FileUploaderAJAX ID="fuplPDDoPIN" runat="server" Directory_CreateIfNotExists="True" showDeletedFilesOnPostBack="True" Width="100%" text_Add="Add" text_Delete="Eliminar" text_Uploading="Subiendo..." text_X="x" ShowLegenTypeFile="False" File_RenameIfAlreadyExists="True" MaxFiles="1" />
                                                    
                                        </div>
                                        <asp:HiddenField ID="hdlPDDoPIN" runat="server" />
                                    </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="fuplShape">Archivo shape-file del perímetro de intervención del proyecto. Magna Sirgas Bogotá y Metadato básico del shapefile:</label>
                                </td>
                                <td>
                                    <div style="width: 500px;">
                                        <cc2:FileUploaderAJAX ID="fuplShape" runat="server" Directory_CreateIfNotExists="True" showDeletedFilesOnPostBack="True" Width="100%" text_Add="Add" text_Delete="Eliminar" text_Uploading="Subiendo..." text_X="x" ShowLegenTypeFile="False" File_RenameIfAlreadyExists="True" MaxFiles="1" />
                                    </div>
                                    <asp:HiddenField ID="hdlShape" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="otroDocumentos">Otros documentos relevantes (Ejemplo: informes de avance):</label>
                                </td>
                                <td>
                                    <div style="width: 500px;">
                                        <cc2:FileUploaderAJAX ID="fulaOtros" runat="server" MaxFiles="3" Directory_CreateIfNotExists="True" showDeletedFilesOnPostBack="True" text_Add="Agregar" text_Delete="Eliminar" text_Uploading="Subiendo..." text_X="x" ShowLegenTypeFile="False" File_RenameIfAlreadyExists="True" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
            <tr>
                <td style="padding: 10px; text-align: left; vertical-align: middle;">
                    <asp:ValidationSummary ID="valResumenUsuario" runat="server" ValidationGroup="redds" DisplayMode="List" ShowSummary="true" />
                    <asp:UpdatePanel ID="upnlErrorRedds" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnActualizar" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Label ID="lblErrorReds" runat="server" Text="Error" SkinID="validador" Visible="false"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="padding: 20px; text-align: center; vertical-align: middle;">
                    <asp:UpdatePanel ID="upnlAccionesBoton" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnActualizar" SkinID="boton_copia" runat="server" Text="Enviar" ValidationGroup="redds" OnClick="btnActualizar_Click" />
                            <asp:Button ID="btnCancelar" SkinID="boton_copia" runat="server" Text="Cancelar"
                                CausesValidation="False" />&nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    
   <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upnlAccionesBoton">
    <ProgressTemplate>  
        <div id="ModalProgressContainer">
            <div>
                <p>Procesando...</p>
                <p><asp:Image ID="imgUpdateProgress" runat="server" SkinId="procesando"/></p>
            </div>
        </div>                         
    </ProgressTemplate>
</asp:UpdateProgress>
    <script src="../js/RegistroREDDS.js"></script>
</asp:Content>

