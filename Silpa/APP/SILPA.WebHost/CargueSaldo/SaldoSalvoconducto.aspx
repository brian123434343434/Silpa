<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPASinMenuFlash.master" AutoEventWireup="true" CodeFile="SaldoSalvoconducto.aspx.cs" Inherits="CargueSaldo_SaldoSalvoconducto" EnableEventValidation="false" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
     <link href="../js/datimepicker-master/build/jquery.datetimepicker.min.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  


    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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

        .CajaDialogo {
            background-color: #fff;
            border-width: 1px;
            border-style: outset;
            border-color: Yellow;
            padding: 0px;
        }

            .CajaDialogo div {
                margin: 5px;
            }

        .FondoAplicacion {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .accordionContent {
            background-color: #D3DEEF;
            border-color: -moz-use-text-color #2F4F4F #2F4F4F;
            border-right: 1px dashed #2F4F4F;
            border-style: none dashed dashed;
            border-width: medium 1px 1px;
            padding: 10px 5px 5px;
            /*width:20%;*/
        }

        .accordionHeaderSelected {
            background-color: #E0F1FF;
            border: 1px solid #2F4F4F;
            color: white;
            cursor: pointer;
            font-family: Arial,Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
            /*width:20%;*/
        }

        .accordionHeader {
            background-color: #D5EBFF;
            border: 1px solid #2F4F4F;
            color: white;
            cursor: pointer;
            font-family: Arial,Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
            /*width:20%;*/
        }

        .href {
            color: White;
            font-weight: bold;
            text-decoration: none;
        }

        .ajax__calendar_container {
            position: absolute;
            z-index: 100003 !important;
            background-color: white;
        }

        div.centre {
            margin-left: auto;
            margin-right: auto;
            width: 600px;
        }
        .auto-style1 {
            height: 35px;
        }
    </style>

    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="Cargue Salvoconducto, CITES, No CITES, Acta Única de control al tráfico ilegal de flora y fauna silvestre – AUCTIFFS" SkinID="titulo_principal_blanco"></asp:Label>
        &nbsp;
        <a href="#" id="hrCerrarVentana" visible="false" runat="server" onclick="window.close();return false;">Salir</a>
    </div>

    <%--<div class="div-contenido">--%>
    <div class="table-responsive">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <cc1:TabContainer ID="tbcContenedor" runat="server" Width="100%" ActiveTabIndex="0" >
            <cc1:TabPanel runat="server" HeaderText="Información Salvoconducto" ID="tabInfoGeneral">
                <HeaderTemplate>
                    Información Documento
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="contact_form">
                        <asp:Panel ID="pnlSalvoconducto" runat="server" GroupingText="Identificación Documento">
                            <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; width: 100%;">
                                <tr>
                                    <td style="width:45%;">
                                        <label for="cboClaseSalvoconducto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Clase:</label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboClaseSalvoconducto" runat="server" ClientIDMode="Static" OnSelectedIndexChanged="cboClaseSalvoconducto_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvClaseSalvoconducto" Display="Dynamic" runat="server" ControlToValidate="cboClaseSalvoconducto" ValidationGroup="salvoconducto" ErrorMessage="Información Salvoconducto -> Clase">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel runat="server" ID="UpdTipoDoocumento"> 
                                            <ContentTemplate>
                                                <label for="nroSalvoconducto" id="LblIdentificacionDocumento" runat="server" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;"> Identificacion Documento / </label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="upnlNroSalvoconducto" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtNroSalvoconducto" runat="server" ClientIDMode="Static" MaxLength="12"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvNroSalvoconducto" Display="Dynamic" runat="server" ControlToValidate="txtNroSalvoconducto" ValidationGroup="salvoconducto" ErrorMessage="Información Salvoconducto -> Número Salvoconducto/CITES/No CITES/Acta">*</asp:RequiredFieldValidator>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="txtFechaExpedicion" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Fecha expedición:</label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtFechaExpedicion" ClientIDMode="Static" runat="server" Width="90px" CssClass="fecha-calendar" />
                                                <asp:RequiredFieldValidator ID="rfvFechaExpedicion" runat="server" ControlToValidate="txtFechaExpedicion" ValidationGroup="salvoconducto" ErrorMessage="Información Salvoconducto -> Fecha Expedición">*</asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RVFechaExpedicion" runat="server" ErrorMessage="Información Salvoconducto -> La fecha de Expedicion no puede Ser mayor a la Fecha Actual" ControlToValidate="txtFechaExpedicion" Display="Dynamic" ValidationGroup="salvoconducto" Type="Date"></asp:RangeValidator>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="txtFechaDesde" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Fecha Desde:</label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtFechaDesde" ClientIDMode="Static" runat="server" Width="90px" CssClass="fecha-calendar" />
                                                <asp:RequiredFieldValidator ID="rfvFechaDesde" runat="server" ControlToValidate="txtFechaDesde" ValidationGroup="salvoconducto" ErrorMessage="Información Salvoconducto -> Fecha Desde">*</asp:RequiredFieldValidator>
                                                <asp:Label ID="lblNAFechaDesde" runat="server" Text="N/A" Visible="true" SkinID="etiqueta_negra"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="txtFechaHasta" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Fecha Hasta:</label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtFechaHasta" ClientIDMode="Static" runat="server" Width="90px" CssClass="fecha-calendar" />
                                                <asp:RequiredFieldValidator ID="rfvFechaHasta" runat="server" ControlToValidate="txtFechaHasta" ValidationGroup="salvoconducto" ErrorMessage="Información Salvoconducto -> Fecha Hasta">*</asp:RequiredFieldValidator>
                                                <asp:Label ID="lblNAFechaHasta" runat="server" Text="N/A" Visible="true" SkinID="etiqueta_negra"></asp:Label>
                                                <asp:CompareValidator ID="cvFechaHasta" ControlToValidate="txtFechaDesde" ControlToCompare="txtFechaHasta" Type="Date" Operator="LessThanEqual" ErrorMessage="Información Salvoconducto -> La fecha hasta no puede ser menor a la fecha desde" runat="server" Display="Dynamic" ValidationGroup="salvoconducto">*</asp:CompareValidator>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="cboAutoridadAmbiental" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Autoridad Ambiental Emisora:</label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="upnltrAutoridadEmisora" runat="server">
                                            <ContentTemplate>
                                                <asp:UpdatePanel ID="upnlAutoridadAmbientalEmisora" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="cboAutoridadAmbientalEmisora" runat="server" ClientIDMode="Static"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvAutoridadAmbientalEmisora" Display="Dynamic" runat="server" ControlToValidate="cboAutoridadAmbientalEmisora" ValidationGroup="salvoconducto" ErrorMessage="Información Salvoconducto -> Autoridad Ambiental Emisora">*</asp:RequiredFieldValidator>
                                                        <asp:Label ID="LblNAAutoridadAMbientalEmisora" runat="server" Text="N/A" Visible="false" SkinID="etiqueta_negra"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>


                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboClaseSalvoconducto" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="cboAutoridadAmbiental" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">País procedencia:</label>
                                    </td>
                                    <td runat="server">
                                        <asp:UpdatePanel ID="upnltrPaisProcedencia" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtPaisProcedencia" runat="server" ClientIDMode="Static" Visible="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvPaisProcedencia" Display="Dynamic" runat="server" ControlToValidate="txtPaisProcedencia" ValidationGroup="salvoconducto" ErrorMessage="Información Salvoconducto -> País procedencia">*</asp:RequiredFieldValidator>
                                                <asp:Label ID="LblNAPaisProcedencia" runat="server" Text="N/A" Visible="true" SkinID="etiqueta_negra"></asp:Label>


                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboClaseSalvoconducto" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="cboTipoSalvoconducto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Tipo:</label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cboTipoSalvoconducto" runat="server" ClientIDMode="Static" OnSelectedIndexChanged="cboTipoSalvoconducto_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvTipoSalvoconducto" Display="Dynamic" runat="server" ControlToValidate="cboTipoSalvoconducto" ValidationGroup="salvoconducto" ErrorMessage="Información Salvoconducto -> Tipo Salvoconducto">*</asp:RequiredFieldValidator>
                                                <asp:Label ID="LblNATipoSalvoconducto" runat="server" Text="N/A" Visible="false" SkinID="etiqueta_negra"></asp:Label>
                                                <br />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboClaseSalvoconducto" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:UpdatePanel ID="upnlSunAnterior" runat="server">
                                            <ContentTemplate>
                                                <table runat="server" id="tableSUNAnterior" visible="false" style="margin-left: -3px;">
                                                    <tr>
                                                        <td>
                                                            <label for="txtSUNAnterio" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">SUN Anterior:</label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSUNAnterior" runat="server" ClientIDMode="Static" MaxLength="10"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboTipoSalvoconducto" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:UpdatePanel ID="upnlSolicitante1" runat="server">
                                            <ContentTemplate>
                                                <table runat="server" id="titularVital" visible="false" style="margin-left: -3px; width:100%">
                                                    <tr>
                                                        <td style="width:45%;">
                                                            <label for="txtSolicitante" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Titular Solicitante:</label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSolicitante" runat="server" placeholder="Nombre Titular" Width="350px" Enabled="false" /><asp:HiddenField ID="hfIdSolicitante" runat="server" />
                                                            <asp:RequiredFieldValidator ID="rfvSolicitante" runat="server" ControlToValidate="txtSolicitante" ValidationGroup="salvoconducto" ErrorMessage="Información Salvoconducto -> Titular Solicitante">*</asp:RequiredFieldValidator>
                                                            <asp:LinkButton ID="lnkSolicitante" runat="server" Text="Buscar Titular" OnClick="lnkSolicitante_Click"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table runat="server" id="titularOtro" visible="false" style="margin-left: -3px; width:100%">
                                                    <tr>
                                                        <td style="width:45%;">
                                                            <label for="txtNombreTitular" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Titular Responsable:</label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNombreTitular" runat="server" placeholder="Nombre Titular" Width="350px" />
                                                            <asp:RequiredFieldValidator ID="rfvNombreTitular" runat="server" ControlToValidate="txtNombreTitular" ValidationGroup="salvoconducto" ErrorMessage="Información Salvoconducto -> Titular Responsable">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label for="txtIdentificacionTitular" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Identificación:</label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtIdentificacionTitular" runat="server" placeholder="Identificación Titular" Width="200px" MaxLength="15" />
                                                            <asp:RequiredFieldValidator ID="rfvIdentificacionTitular" runat="server" ControlToValidate="txtIdentificacionTitular" ValidationGroup="salvoconducto" ErrorMessage="Información Salvoconducto -> Identificación Titular">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="lnkSeleccionarSolicitante" />
                                                <asp:AsyncPostBackTrigger ControlID="cboClaseSalvoconducto" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="txtEstablecimiento" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Establecimiento / Nombre del predio:</label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtEstablecimiento" ClientIDMode="Static" runat="server" Width="300px" />


                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="fuplActoAdministrativo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Documento soporte <strong>Original</strong> de obtención de los especimenes :</label>
                                    </td>
                                    <td>
                                        <div style="width: 400px;">
                                            <asp:UpdatePanel ID="upnlArchivo" runat="server">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnActualizar" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <cc1:AsyncFileUpload runat="server" ID="fuplDocumentoSoporte" ClientIDMode="AutoID" OnClientUploadComplete="UploadAnexo" OnClientUploadStarted="AssemblyFileUpload_Started" />
                                                    <asp:Label ID="lblArchivo" runat="server" Text="-" Visible="false" SkinID="etiqueta_negra"></asp:Label>
                                                    <asp:HyperLink ID="lnkVerArchivo" runat="server" NavigateUrl='~/VerAnexo.ashx' Text="Ver Archivo" Visible="false" />
                                                    <asp:LinkButton ID="lnkAdicionarArchivo" runat="server" Text="Modificar Archivo" OnClick="lnkAdicionarArchivo_Click" Visible="false" />
                                                    <asp:LinkButton ID="lnkCancelarArchivo" runat="server" Text="Cancelar" OnClick="lnkCancelarArchivo_Click" Visible="false" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                        <asp:Panel ID="pnlInformacionAprovechamiento" GroupingText="Obtención de los especímenes" runat="server">
                            <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; width: 100%;">
                                <tr>
                                    <td style="width:45%;">
                                        <label for="cboAutoridadAmbiental" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Autoridad Ambiental Otorga:</label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="upnlAutoridadAmbientalOtorga" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cboAutoridadAmbientalOtorga" runat="server" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="cboAutoridadAmbientalOtorga_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvAutoridadAmbientalOtorga" Display="Dynamic" runat="server" ControlToValidate="cboAutoridadAmbientalOtorga" ValidationGroup="salvoconducto" ErrorMessage="Información del Aprovechamiento u obtención legal de los especimenes -> Autoridad Ambiental Otorga">*</asp:RequiredFieldValidator>
                                                <asp:Label ID="LblNAAutoridadAmbientalOtorga" runat="server" Text="N/A" Visible="false" SkinID="etiqueta_negra"></asp:Label>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboClaseSalvoconducto" />
                                                <asp:AsyncPostBackTrigger ControlID="cboAutoridadAmbientalOtorga" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="txtRelacionJuridica" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Número de Licencia/ Aprovechamiento / Concepto Técnico:</label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="pnlNumeroActoAdministrativo" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtNumeroActoAdministrativo" runat="server" ClientIDMode="Static" MaxLength="20"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvNumeroActoAdministrativo" runat="server" ControlToValidate="txtNumeroActoAdministrativo" ValidationGroup="salvoconducto" ErrorMessage="Información del Aprovechamiento u obtención legal de los especimenes -> Numero del Documento">*</asp:RequiredFieldValidator>
                                                <asp:Label ID="LblNumeroActoAdministrativo" runat="server" Text="N/A" Visible="false" SkinID="etiqueta_negra"></asp:Label>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboClaseSalvoconducto" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="txtFechaActoAdminstrativo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Fecha de expedición de Licencia/ Aprovechamiento/ Concepto Técnico:</label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="upnlFechaActoAdministrativo" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtFechaActoAdminstrativo" ClientIDMode="Static" runat="server" Width="90px" CssClass="fecha-calendar" />
                                                <asp:RequiredFieldValidator ID="rfvFechaActoAdminstrativo" runat="server" ControlToValidate="txtFechaActoAdminstrativo" ValidationGroup="salvoconducto" ErrorMessage="Información del Aprovechamiento u obtención legal de los especimenes -> Fecha Documento">*</asp:RequiredFieldValidator>
                                                <asp:Label ID="LblFechaActoAdministrativo" runat="server" Text="N/A" Visible="false" SkinID="etiqueta_negra"></asp:Label>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboClaseSalvoconducto" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:UpdatePanel ID="upnlTitularAprovechamiento" runat="server">
                                            <ContentTemplate>
                                                <table runat="server" id="tbTitularAProvechamiento" visible="false" style="margin-left: -3px;">
                                                    <tr>
                                                        <td>
                                                            <label for="txtNombreIniciativa" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Titular:</label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTitularApro" runat="server" placeholder="Nombre Titular" Width="350px" Enabled="False" /><asp:HiddenField ID="hdfTitularAprovechamientoID" runat="server" />
                                                            <asp:RequiredFieldValidator ID="rfvTitularApro" runat="server" ControlToValidate="txtTitularApro" ValidationGroup="salvoconducto" ErrorMessage="Información del Aprovechamiento u obtención legal de los especimenes -> Titular">*</asp:RequiredFieldValidator>
                                                            <asp:LinkButton ID="lnkTitularApro" runat="server" Text="Buscar Titular" OnClick="lnkTitularApro_Click"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboTipoSalvoconducto" />
                                                <asp:AsyncPostBackTrigger ControlID="cboClaseSalvoconducto" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="cboClaseRecurso" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Clase Recurso:</label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="upnlClaseRecurso" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cboClaseRecurso" runat="server" OnSelectedIndexChanged="cboClaseRecurso_SelectedIndexChanged" ClientIDMode="Static" AutoPostBack="true"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvClaseRecurso" Display="Dynamic" runat="server" ControlToValidate="cboClaseRecurso" ValidationGroup="salvoconducto" ErrorMessage="Información Salvoconducto -> Clase Recurso">*</asp:RequiredFieldValidator>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="cboFormaOtorgamiento" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Forma de Otorgamiento:</label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="upnlFormaOtorgamiento" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cboFormaOtorgamiento" runat="server"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvFormaOtorgamiento" Display="Dynamic" runat="server" ControlToValidate="cboFormaOtorgamiento" ValidationGroup="salvoconducto" ErrorMessage="Información Salvoconducto -> Forma Otorgamiento">*</asp:RequiredFieldValidator>
                                                <asp:Label ID="lblNAFormaOtorgamiento" runat="server" Text="N/A" Visible="false" SkinID="etiqueta_negra"></asp:Label>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboClaseRecurso" />
                                                <asp:AsyncPostBackTrigger ControlID="cboTipoSalvoconducto" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="cboModoAdquisicion" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Modo de Adquisición:</label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="upnlModoAdquisicion" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cboModoAdquisicion" runat="server"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvModoAdquisicion" Display="Dynamic" runat="server" ControlToValidate="cboModoAdquisicion" ValidationGroup="salvoconducto" ErrorMessage="Información Salvoconducto -> Modo Adquisición">*</asp:RequiredFieldValidator>
                                                <asp:Label ID="lblNAModAdqui" runat="server" Text="N/A" Visible="false" SkinID="etiqueta_negra"></asp:Label>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboClaseRecurso" />
                                                <asp:AsyncPostBackTrigger ControlID="cboTipoSalvoconducto" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="cboFinalidadRecurso" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Finalidad Recurso:</label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="upnlFinalidadRecurso" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cboFinalidadRecurso" runat="server"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvFinalidadRecurso" Display="Dynamic" runat="server" ControlToValidate="cboFinalidadRecurso" ValidationGroup="salvoconducto" ErrorMessage="Información Salvoconducto -> Finalidad Recurso">*</asp:RequiredFieldValidator>
                                                <asp:Label ID="lblNAFinalidad" runat="server" Text="N/A" Visible="false" SkinID="etiqueta_negra"></asp:Label>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboClaseRecurso" />
                                                <asp:AsyncPostBackTrigger ControlID="cboTipoSalvoconducto" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="participante" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Procedencia Legal:</label>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>Departamento: </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="cboDepartamento" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboDepartamento_SelectedIndexChanged"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvDepartamento" runat="server" ControlToValidate="cboDepartamento" Display="Dynamic" ValidationGroup="salvoconducto">*</asp:RequiredFieldValidator>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Municipio </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="upnlMunicipio" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="cboMunicipio" runat="server"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvoMunicipio" runat="server" ControlToValidate="cboMunicipio" Display="Dynamic" ValidationGroup="salvoconducto">*</asp:RequiredFieldValidator>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="cboDepartamento" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Corregimiento: </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="upnlCorregimiento" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtCorregimiento" runat="server"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Vereda: </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="upnlVereda" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtVereda" runat="server"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>

            <cc1:TabPanel runat="server" HeaderText="Información de los Especimenes" ID="tabEspecimenes">
                <ContentTemplate>
                    <div class="contact_form">
                        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; width: 100%;">
                            <tr>
                                <td style="width:45%;">
                                    <label for="txtNombreEspecie" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Nombre Cientifico:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="upnlControlEspeice">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtNombreEspecie" runat="server" ClientIDMode="Static" Enabled="false" /><asp:LinkButton ID="lnkEspecie" runat="server" OnClick="lnkEspecie_Click" Text="Buscar Especie"></asp:LinkButton><asp:HiddenField ID="hfEspcimenID" runat="server" />
                                            <asp:RequiredFieldValidator ID="rfvNombreEspecie" runat="server" ControlToValidate="txtNombreEspecie" ValidationGroup="especimen" ErrorMessage="Informacion de los Especimenes -> Nombre Científico / Común">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnAgregarEspecie" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="txtNombreComunEspecie" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Nombre Común:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="upnlNombreComun">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtNombreComunEspecie" runat="server" ClientIDMode="Static" Enabled="true" />
                                            <asp:RequiredFieldValidator ID="RFVtxtNombreComunEspecie" runat="server" ControlToValidate="txtNombreComunEspecie" ValidationGroup="especimen" ErrorMessage="Informacion de los Especimenes -> Nombre Común">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnAgregarEspecie" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>                         
                            <tr>
                                <td class="auto-style1">
                                    <label for="cboClaseProducto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Clase Producto:</label>
                                </td>
                                <td class="auto-style1">
                                    <asp:UpdatePanel ID="upnlClaseProducto" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboClaseProducto" ClientIDMode="Static" runat="server" OnSelectedIndexChanged="cboClaseProducto_SelectedIndexChanged" AutoPostBack="true" /><asp:RequiredFieldValidator ID="rfvClaseProducto" runat="server" ControlToValidate="cboClaseProducto" ValidationGroup="especimen" ErrorMessage="Informacion de los Especimenes -> Clase Producto">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="cboTipoProducto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Tipo Producto:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upnlTipoProducto" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboTipoProducto" ClientIDMode="Static" runat="server" OnSelectedIndexChanged="cboTipoProducto_SelectedIndexChanged" AutoPostBack="true" /><asp:RequiredFieldValidator ID="rfvTipoProducto" runat="server" ControlToValidate="cboTipoProducto" ValidationGroup="especimen" ErrorMessage="Informacion de los Especimenes -> Tipo Producto">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboClaseProducto" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="cboUnidadMedida" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Unidad Medida:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upnlUnidadMedida" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboUnidadMedida" ClientIDMode="Static" runat="server" OnSelectedIndexChanged="cboUnidadMedida_SelectedIndexChanged" /><asp:RequiredFieldValidator ID="rfvUnidadMedida" runat="server" ControlToValidate="cboUnidadMedida" ValidationGroup="especimen" ErrorMessage="Informacion de los Especimenes -> Unidad Medida">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboTipoProducto" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="txtCantidad" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Cantidad/Volumen por especie:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upnlCantidadText" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCantidad" runat="server" ClientIDMode="Static" OnBlur="NumeroALetras();" MaxLength="7"></asp:TextBox>
                                            <img src="../App_Themes/Img/Ayuda/icon-ayuda.png" alt="" title="Los decimales van separados por punto (.)" />
                                            <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" ControlToValidate="txtCantidad" ValidationGroup="especimen" ErrorMessage="Informacion de los Especimenes -> Cantidad de Productos/Especies">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnAgregarEspecie" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="lblCantidadLetras" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Cantidad/Volumen en letras:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upnlCantidadLetras" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lblCantidadLetras" runat="server" ClientIDMode="Static" SkinID="etiqueta_negra"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnAgregarEspecie" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="txtDescripcion" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Descripción:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upnlDescripcion" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtDescripcion" runat="server" ClientIDMode="Static" OnBlur="NumeroALetras();" Width="300px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnAgregarEspecie" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                                    <asp:Button ID="btnAgregarEspecie" runat="server" Text="Agregar Especimen" OnClick="btnAgregarEspecie_Click" ValidationGroup="especimen" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 0 !important; text-align: left; vertical-align: top;">
                                    <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="upnlEspecies" runat="server">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnAgregarEspecie" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:GridView ID="gdvEspecimenes" runat="server" AutoGenerateColumns="false" DataKeyNames="EspecieTaxonomiaID" Width="100%"
                                                            CellPadding="2" CellSpacing="1" GridLines="None" ShowFooter="True" OnRowDeleting="gdvEspecimenes_RowDeleting" HorizontalAlign="Center" SkinID="GrillaCoordenadas">
                                                            <Columns>
                                                                <asp:BoundField DataField="NombreEspecie" HeaderText="Nombre" />
                                                                <asp:BoundField DataField="NombreComunEspecie" HeaderText="Nombre Comun" />
                                                                <asp:BoundField DataField="ClaseProducto" HeaderText="ClaseProducto" />
                                                                <asp:BoundField DataField="TipoProducto" HeaderText="TipoProducto" />
                                                                <asp:BoundField DataField="UnidadMedida" HeaderText="Unidad Medida" />
                                                                <asp:BoundField DataField="Cantidad" HeaderText="Volumen por especie" DataFormatString="{0:N}" />
                                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                                                <asp:TemplateField AccessibleHeaderText="Eliminar" HeaderText="Eliminar">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imb_borrar" runat="server" CausesValidation="False" CommandName="Delete" SkinID="icoEliminar" ToolTip="Haga clic aquí para borrar el Item" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="upnlVolumenTotal" runat="server">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnAgregarEspecie" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <table runat="server" id="tblVolumenTotal" visible="False">
                                                            <tr id="Tr1" runat="server">
                                                                <td id="Td1" align="right" valign="middle" runat="server">
                                                                    <asp:Label ID="lbltextValMovilizar" runat="server" Text="Volumen total a movilizar" SkinID="etiqueta_negra"></asp:Label></label>
                                                                </td>
                                                                <td id="Td2" runat="server" valign="middle">
                                                                    <asp:Label ID="LblCantVolMovilizar" runat="server" ClientIDMode="Static" MaxLength="10" SkinID="etiqueta_negra"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
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

            <cc1:TabPanel runat="server" HeaderText="Ruta desplazamiento" ID="tabRuta">
                <ContentTemplate>
                    <asp:UpdatePanel ID="upnlRutaDesplazamiento" runat="server">
                        <ContentTemplate>
                            <div class="contact_form">
                                <table width="90%">
                                    <tr>
                                        <td style="width: 200px">
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="cboEspecies">Tipo Ruta:</label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboTipoRuta" runat="server" OnSelectedIndexChanged="cboTipoRuta_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvTipoRuta" runat="server" ControlToValidate="cboTipoRuta" ValidationGroup="ruta" Display="Dynamic">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="cboDepartamento">Departamento:</label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="CboDpartamentoSunl" runat="server" OnSelectedIndexChanged="CboDpartamentoSunl_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvDepartamentoSunl" runat="server" ControlToValidate="CboDpartamentoSunl" ValidationGroup="ruta" Display="Dynamic">*</asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="cboMunicipio">Municipio:</label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="CboMunicipioSunl" runat="server" OnSelectedIndexChanged="CboMunicipioSunl_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvMunicipioSunl" runat="server" ControlToValidate="CboMunicipioSunl" ValidationGroup="ruta" Display="Dynamic">*</asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr runat="server" id="trBarrio" visible="false">
                                        <td>
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="txtBarrio">Barrio:</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBarrio" runat="server" ClientIDMode="Static"></asp:TextBox><asp:RequiredFieldValidator ID="rfvBarrio" runat="server" ControlToValidate="txtBarrio" ValidationGroup="ruta" Display="Dynamic">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnAgregarRuta" runat="server" Text="Agregar Ruta" ValidationGroup="ruta" OnClick="btnAgregarRuta_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:GridView ID="grvRutaDesplazamiento" runat="server" AutoGenerateColumns="false" SkinID="GrillaCoordenadas">
                                                <Columns>
                                                    <asp:BoundField DataField="Departamento" HeaderText="Departamento" />
                                                    <asp:BoundField DataField="Municipio" HeaderText="Municipio" />
                                                    <asp:BoundField DataField="Barrio" HeaderText="Barrio" />
                                                    <asp:TemplateField HeaderText="Retirar">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEliminar" runat="server" OnClick="grvRutaDesplazamiento_lnkEliminar_Click" Text="Eliminar" CommandArgument='<%# Eval("Orden")%>' CssClass="a_red" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </cc1:TabPanel>

        
            <cc1:TabPanel ID="tabTransporte" runat="server" HeaderText="Transporte" >
                <ContentTemplate>
                    <asp:UpdatePanel ID="upnlTransporte" runat="server">
                        <ContentTemplate>
                            <div class="contact_form">
                                <table width="90%">
                                    <tr>
                                        <td style="width: 200px">
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="cboModoTransporte">Medio Transporte:</label>
                                        </td>
                                        <td>
                                                    <asp:DropDownList ID="cboModoTransporte" runat="server" OnSelectedIndexChanged="cboModoTransporte_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="cboModoTransporte" ValidationGroup="transporte" Display="Dynamic" ErrorMessage="Transporte-> Modo Transporte">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="cboTipoVehiculo">Tipo Vehículo:</label>
                                        </td>
                                        <td>
                                                    <asp:DropDownList ID="cboTipoVehiculo" runat="server" OnSelectedIndexChanged="cboTipoVehiculo_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvTipoVehiculo" runat="server" ControlToValidate="cboTipoVehiculo" ValidationGroup="transporte" Display="Dynamic" ErrorMessage="Transporte-> Tipo Vehículo">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trOtroTipoVehiculo" visible="false">
                                        <td></td>
                                        <td>Cual?
                                                    <asp:TextBox ID="txtOtroTipoVehiculo" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="txtEmpresa">Empresa:</label>
                                        </td>
                                        <td>
                                                    <asp:TextBox ID="txtEmpresa" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="rfvEmpresa" runat="server" ControlToValidate="txtEmpresa" ValidationGroup="transporte" Display="Dynamic" ErrorMessage="Transporte-> Empresa">*</asp:RequiredFieldValidator></td>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="txtIdentificacionTransporte">Número de identificación del medio de transporte (placa) :</label>
                                        </td>
                                        <td>
                                                    <asp:TextBox ID="txtIdentificacionTransporte" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvIdentificacionTransporte" runat="server" ControlToValidate="txtIdentificacionTransporte" ValidationGroup="transporte" Display="Dynamic" ErrorMessage="Transporte-> Número de identificación del medio de transporte">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="txtNombreTransportador">Transportador o Empresa:</label>
                                        </td>
                                        <td>
                                                    <asp:TextBox ID="txtNombreTransportador" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvNombreTransportador" runat="server" ControlToValidate="txtNombreTransportador" ValidationGroup="transporte" Display="Dynamic" ErrorMessage="Transporte-> Transportador o Empresa">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="txtIdentificacionTransportador">Identificación CC o NIT:</label>
                                        </td>
                                        <td>
                                                    <asp:TextBox ID="txtIdentificacionTransportador" runat="server" ClientIDMode="Static" MaxLength="11"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvIdentificacionTransportador" runat="server" ControlToValidate="txtIdentificacionTransportador" ValidationGroup="transporte" Display="Dynamic" ErrorMessage="Transporte-> Identificación CC o NIT">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdbtnAgregarTransporte" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnAgregarTransporte" runat="server" Text="Agregar Transporte" ValidationGroup="transporte" OnClick="btnAgregarTransporte_Click" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                                <table width="90%">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grvTransporte" runat="server" AutoGenerateColumns="false" SkinID="Grilla">
                                                <HeaderStyle Font-Size="9pt" />
                                                <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                                <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                                <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Modo Transporte" DataField="ModoTransporte" />
                                                    <asp:BoundField HeaderText="Tipo Vehículo" DataField="TipoTransporte" />
                                                    <asp:BoundField HeaderText="Empresa" DataField="Empresa" />
                                                    <asp:BoundField HeaderText="Identificación Transporte" DataField="NumeroIdentificacionMedioTransporte" />
                                                    <asp:BoundField HeaderText="Transportador o Empresa" DataField="NombreTransportador" />
                                                    <asp:BoundField HeaderText="Identificacion CC o NIT" DataField="NumeroIdentificacionTransportador" />
                                                    <asp:TemplateField HeaderText="Retirar">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEliminar" runat="server" OnClick="grvTransporte_lnkEliminar_Click" Text="Eliminar" CommandArgument='<%# Eval("TransporteSalvoconductoID")%>' CssClass="a_red" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>

        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; width: 100%;">
            <tr>
                <td>
                    <asp:ValidationSummary ID="valResumenUsuario" runat="server" ValidationGroup="salvoconducto" DisplayMode="List" ShowSummary="true" />
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
                <td style="padding: 10px; text-align: left; vertical-align: middle;">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="especimen" DisplayMode="List" ShowSummary="true" />
                </td>
            </tr>
            <tr>
                <td style="padding: 20px; text-align: center; vertical-align: middle;">
                    <asp:UpdatePanel ID="upnlAccionesBoton" runat="server">
                        <ContentTemplate>
                            <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                                <tr>
                                    <td style="text-align: right; padding-right: 20px; vertical-align: middle;">
                                        <asp:Button ID="btnActualizar" SkinID="boton_copia" runat="server" Text="Enviar" ValidationGroup="salvoconducto" OnClick="btnActualizar_Click" />
                                    </td>
                                    <td style="text-align: left; padding-left: 20px; vertical-align: middle;">
                                        <asp:Button ID="btnCancelar" SkinID="boton_copia" runat="server" Text="Cancelar" CausesValidation="False" OnClick="btnCancelar_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>

    <asp:Label ID="lblSolicitante" runat="server" SkinID="etiqueta_negra"></asp:Label>
    <cc1:ModalPopupExtender ID="mpeSolicitantes" runat="server"
        TargetControlID="lblSolicitante"
        PopupControlID="pnlSolicitantes"
        DropShadow="True" Enabled="True" DynamicServicePath=""
        BackgroundCssClass="FondoAplicacion" />
    <asp:Panel ID="pnlSolicitantes" runat="server" Style="display: none;" CssClass="CajaDialogo">
        <div class="table-responsive">
            <asp:UpdatePanel ID="upnlSolicitante" runat="server">
                <ContentTemplate>
                    <table style="width: 400px;">
                        <tr>
                            <td>
                                <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Tipo Identificacion:</label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboTipoIdentificacion" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvTipoIdentificacion" Display="Dynamic" runat="server" ControlToValidate="cboTipoIdentificacion" ValidationGroup="solicitante">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Identificacion:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumeroIdentificacion" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNumeroIdentificacion" Display="Dynamic" runat="server" ControlToValidate="txtNumeroIdentificacion" ValidationGroup="solicitante">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                                <asp:Button ID="btnBuscarSolicitante" runat="server" Text="Buscar" SkinID="boton_copia" OnClick="btnBuscarSolicitante_Click" ValidationGroup="solicitante" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNombreSolicitante" runat="server" ClientIDMode="Static" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:LinkButton ID="lnkSeleccionarSolicitante" runat="server" OnClick="lnkSeleccionarSolicitante_Click" Text="Seleccionar" Visible="false"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnCerrarVinculosActividad" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:Button ID="btnCerrarVinculosActividad" runat="server" Text="Cerrar" CausesValidation="False" SkinID="boton_copia" OnClick="btnCerrarVinculosActividad_Click" />
        </div>
    </asp:Panel>

    <asp:Label ID="lblTitularApro" runat="server" SkinID="etiqueta_negra"></asp:Label>
    <cc1:ModalPopupExtender ID="mpeTitularApro" runat="server"
        TargetControlID="lblTitularApro"
        PopupControlID="pnlTitularApro"
        DropShadow="True" Enabled="True" DynamicServicePath=""
        BackgroundCssClass="FondoAplicacion" />
    <asp:Panel ID="pnlTitularApro" runat="server" Style="display: none;" CssClass="CajaDialogo">
        <div class="table-responsive">
            <asp:UpdatePanel ID="upnlTitularApro" runat="server">
                <ContentTemplate>
                    <table style="width: 400px">
                        <tr>
                            <td>
                                <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Tipo Identificacion:</label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboTipoIdentificacionTitularApro" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvTipoIdentificacionTitularApro" Display="Dynamic" runat="server" ControlToValidate="cboTipoIdentificacionTitularApro" ValidationGroup="titularApro">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Identificacion:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumeroIdentificacionTitularApro" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNumeroIdentificacionTitularApro" Display="Dynamic" runat="server" ControlToValidate="txtNumeroIdentificacionTitularApro" ValidationGroup="titularApro">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                                <asp:Button ID="btnBuscarTitularApro" runat="server" Text="Buscar" SkinID="boton_copia" OnClick="btnBuscarTitularApro_Click" ValidationGroup="titularApro" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNombreTitularApro" runat="server" ClientIDMode="Static" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:LinkButton ID="lnkSeleccionarTitularApro" runat="server" OnClick="lnkSeleccionarTitularApro_Click" Text="Seleccionar" Visible="false"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="btnCerrarTitularApro" runat="server" Text="Cerrar" CausesValidation="False" SkinID="boton_copia" OnClick="btnCerrarTitularApro_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

    <asp:Label ID="lblMpEspecimen" runat="server" SkinID="etiqueta_negra"></asp:Label>
    <cc1:ModalPopupExtender ID="mpeEspecimen" runat="server"
        TargetControlID="lblMpEspecimen"
        PopupControlID="pnlEspecimen"
        DropShadow="True" Enabled="True" DynamicServicePath=""
        BackgroundCssClass="FondoAplicacion" />
    <asp:Panel ID="pnlEspecimen" runat="server" Style="display: none; max-width: 800px; max-height: 700px;" CssClass="CajaDialogo" ScrollBars="Vertical">
        <div class="table-responsive">
            <asp:UpdatePanel ID="upnlEspecimen" runat="server">
                <ContentTemplate>
                    <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; max-width: 800px;">
                        <tr>
                            <th colspan="3" style="font-size: 12pt; font-weight: bold; border-bottom: 1px solid Gray;">Buscar Especie</th>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Nombre Cientifico:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombreComun" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNombreComun" Display="Dynamic" runat="server" ControlToValidate="txtNombreComun" ValidationGroup="Buscarespecie">*</asp:RequiredFieldValidator>
                            </td>
                            <td style="padding-left: 30px; text-align: right; vertical-align: middle;">
                                <asp:Button ID="btnBuscarEspecie" runat="server" Text="Buscar" SkinID="boton_copia" OnClick="btnBuscarEspecie_Click" ValidationGroup="Buscarespecie" />
                            </td>
                        </tr>
                    </table>
                    <div style="overflow: auto; max-width: 800px; max-height: 700px;">
                        <asp:GridView ID="dgv_Especies" runat="server" Width="100%" 
                            SkinID="grilla" AllowPaging="True" AllowSorting="True" PageSize="12" 
                            EmptyDataText="No se encontraron datos"
                            AutoGenerateColumns="False" CellPadding="2" CellSpacing="1"
                            GridLines="None" ShowFooter="True" 
                            DataKeyNames="ESEPCIE_ID" 
                            OnRowEditing="dgv_Especies_RowEditing" 
                            OnPageIndexChanging="dgv_Especies_PageIndexChanging">
                            <HeaderStyle Font-Size="9pt" />
                            <FooterStyle Font-Size="9pt" ForeColor="#000000" cssclass="texto_tablas_paginador" />
                            <RowStyle Font-Size="9pt" ForeColor="#000000" />
                            <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                            <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                            <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                            <Columns>
                                <asp:TemplateField HeaderText="Nombre Común">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblNombreComun" Text='<%# Eval("NOMBRE_COMUN") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre Científico">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbl_nomlblNombreCientifico" Text='<%# Eval("NOMBRE_CIENTIFICO") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Seleccionar">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkSeleccionar" CommandName="Edit" CssClass="a_orange">Seleccionar</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <%--<RowStyle CssClass="texto_tablas" />
                            <PagerStyle CssClass="texto_tablas_paginador" HorizontalAlign="Left" />
                            <HeaderStyle CssClass="titulo_tablas" />
                            <AlternatingRowStyle CssClass="texto_tablas_dos" />--%>
                            <PagerSettings FirstPageImageUrl="../App_Themes/Img/pagina_primera.gif" FirstPageText="Primera"
                                LastPageImageUrl="../App_Themes/Img/pagina_ultima.gif" LastPageText="Ultima" Mode="NumericFirstLast"
                                NextPageImageUrl="../App_Themes/Img/pagina_siguiente.gif" NextPageText="Siguiente" PreviousPageImageUrl="../App_Themes/Img/pagina_anterior.gif"
                                PreviousPageText="Anterior" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div style="padding-top: 20px; text-align: center; vertical-align: middle; width: 100%;">
                <asp:Button ID="Button2" runat="server" Text="Cerrar" CausesValidation="False" SkinID="boton_copia" />
            </div>
        </div>
    </asp:Panel>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upnlAccionesBoton">
        <ProgressTemplate>
            <div id="ModalProgressContainer">
                <div>
                    <p>Procesando...</p>
                    <p>
                        <asp:Image ID="imgUpdateProgress" runat="server" SkinID="procesando" /></p>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script src="../js/datimepicker-master/build/jquery.datetimepicker.full.js"></script>
    <script src="../jquery/jquery.numeric.js"></script>
    <script src="../js/SaldoSalvoconducto.js"></script>
</asp:Content>

