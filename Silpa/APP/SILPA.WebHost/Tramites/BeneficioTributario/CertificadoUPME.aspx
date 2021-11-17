<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="CertificadoUPME.aspx.cs" Inherits="Tramites_BeneficioTributario_CertificadoUPME" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <style type="text/css">
        .FileUploadRegistro input[type="file"] {
            font-family: Tahoma,Verdana,Arial,Helvetica,sans-serif;
            font-size: 11px;
            color: #333333;
            font-weight: bold;
            width: 200px;
        }
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
        input[disabled],button[disabled] {
            background-color:gray;
            color:white;
        }
        input[disabled]:hover,button[disabled]:hover {
            background-color:gray;
            color:white;
        }
        .ContenedorModalAutoliquidacion {
            max-width: 1000px !important;
        }
    </style>


    <script language="javascript" type="text/javascript">
    
    var progressVisible = false;

    function MostrarProgressDropDownSoportePago() {
        $get('<%= uppAdjuntarSoportePago.ClientID %>').style.display = 'block';
        progressVisible = true;
    }
    function OcultarProgressDropDownSoportePago() {
        if (progressVisible) {
            $get('<%= uppAdjuntarSoportePago.ClientID %>').style.display = 'none';
        }
    }
    function ErrorArchivoSoportePago() {
        OcultarProgressDropDownSoportePago();
        alert("El tamaño del archivo sobrepasa el máximo permitido");
    }
        </script>

    <script src="../../Scripts/jquery-1.9.1.js"></script>
    <link href="../../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../../jquery/EstiloGris/jquery-ui.js"  type="text/javascript"></script>
    <script src="../../js/Ayuda.js" type="text/javascript"></script>
    <link href="../css/CertificadoUPME.css" rel="stylesheet" />
    <script src="../../jquery/jquery.numeric.js"></script>
    <script src="../../js/certificadoUpme.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="scmManager" runat="server"></asp:ScriptManager>        

    <table class="TablaTituloSeccionAutoliquidacion">
        <tr>
            <td class="div-titulo">
                <asp:Label ID="lblTitulo" runat="server" Text="CERTIFICADO UPME" SkinID="titulo_principal_blanco"></asp:Label>
            </td>
        </tr>
    </table>
     
    <asp:UpdatePanel runat="server" ID="upnlSolicitudeLiquidacion">
        <ContentTemplate>
            <table class="TablaAutoliquidacion">
                <tr>
                    <td colspan="2" class="TituloSeccionAutoliquidacion">Filtro de Búsqueda</td>
                </tr>
                <tr>
                    <td class="LabelFormularioBusquedaAutoliquidacion">
                        Número Certificado:
                    </td>
                    <td class="CamposFormularioBusquedaAutoliquidacion">
                        <asp:TextBox runat="server" ID="txtNumeroCertificadoFind" MaxLength="14"></asp:TextBox>
                    </td>
                    
                </tr>
                <tr>
                    <td colspan="2">
                        <table class="TablaBotonesFormularioAutoliquidacion">
                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="btnBuscarCertificado" ValidationGroup="BuscarSolicitudes" Text="Buscar" ClientIDMode="Static" OnClick="btnBuscarCertificado_Click"/>
                                    <asp:ValidationSummary ID="valBuscarSolicitudes" runat="server" ValidationGroup="BuscarSolicitudes" ShowMessageBox="true" ShowSummary="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
           
        </ContentTemplate>
        <Triggers>
            
        </Triggers>

    </asp:UpdatePanel>

    <input type="button" runat="server" id="cmdDatosCertificadoHide" style="display:none;" />
        <cc1:ModalPopupExtender ID="mpeDatosCertificado" runat="server" PopupControlID="dvDatosCertificado" TargetControlID="cmdDatosCertificadoHide" BehaviorID="mpeDatosCertificados" BackgroundCssClass="ModalBackgroundAutoliquidacion">
        </cc1:ModalPopupExtender>
     <div id="dvDatosCertificado"  runat="server" clientidmode="Static" class="ContenedorModalAutoliquidacion">
        <asp:UpdatePanel ID="upnlDatosCertificado" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="table-responsive">
            <div class="table-responsive DivTablaFormularioAutoliquidacion">
                <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                    <tr>
                        <td colspan="6" class="subtitulo-doble-linea" style="text-align: left; border: 0px solid #ddd !important;">Datos del certificado</td>
                    </tr>
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblNumeroCertificadoData" runat="server" SkinID="etiqueta_negra"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblTipoIdentificacion" runat="server" SkinID="etiqueta_negra_bold" Text="Tipo Identificación:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblTIpoIdentificacionData" runat="server" SkinID="etiqueta_negra"></asp:Label>
                        </td>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblNumeroCertificado" runat="server" SkinID="etiqueta_negra_bold" Text="Número de certificado:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:TextBox ID="txtNumeroCertificadoData" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblTipoCertificacion" runat="server" SkinID="etiqueta_negra_bold" Text="Tipo certificación:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="ddlTipoCertificacionData" runat="server" OnSelectedIndexChanged="ddlTipoCertificacionData_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="Seleccione..." Value="-1" />
                                <asp:ListItem Text="IVA" Value="1" />
                                <asp:ListItem Text="Renta" Value="2" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvTipoCertificacionData" ControlToValidate="ddlTipoCertificacionData" InitialValue="-1" ValidationGroup="Solicitar" Text="Debe seleccionar un valor" Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblNombreProyecto" runat="server" SkinID="etiqueta_negra_bold" Text="Nombre Proyecto:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;" colspan="4">
                            <asp:TextBox ID="txtNombreProyectoData" runat="server" ReadOnly="True" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblUsuariosSecundarios" runat="server" SkinID="etiqueta_negra_bold" Text="Usuarios secundarios:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;" colspan="4">
                            <asp:Label id="lblUsuariosSecundariosData" SkinID="etiqueta_negra" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblObjetoProyecto" runat="server" SkinID="etiqueta_negra_bold" Text="Objeto del proyecto y Finalidad del proyecto :"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;" colspan="3">
                            <asp:Label id="lblObjetoProyectoFinalidadData" SkinID="etiqueta_negra" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblUbicacionGeografica" runat="server" SkinID="etiqueta_negra_bold" Text="Ubicación geográfica:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;" colspan="3">
                            <asp:Label id="lblUbicacionGeograficaData" SkinID="etiqueta_negra" runat="server"></asp:Label><br />
                            <asp:Label id="lblCoordenadasData" SkinID="etiqueta_negra" runat="server"></asp:Label>
                           
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblEtapa" runat="server" SkinID="etiqueta_negra_bold" Text="Etapa:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;" colspan="3">
                            <asp:Label id="lblEtapaData" SkinID="etiqueta_negra" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblEnergiaAnualGenera" runat="server" SkinID="etiqueta_negra_bold" Text="Energía Anual que se genera con el sistema (MWH/Año):"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:TextBox ID="txtEnergiaAnualGeneraSistemaData" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblFuenteNoConvencionalUtilizar" runat="server" SkinID="etiqueta_negra_bold" Text="Fuente No Convencional a Utilizar:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:TextBox ID="txtFuenteNoConvencionalUtilizarData" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblFuenteConvencionalSustituir" runat="server" SkinID="etiqueta_negra_bold" Text="Fuente Convencional a sustituir:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="ddlFuenteConvencionalSustituirData" runat="server" SkinID="lista_desplegable" OnSelectedIndexChanged="ddlFuenteConvencionalSustituirData_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvFuenteConvencionalSustituirData" ControlToValidate="ddlFuenteConvencionalSustituirData" InitialValue="" ValidationGroup="Solicitar" Text="Debe seleccionar un valor" Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="ddlSubfuenteConvencionalSustituirData" runat="server" SkinID="lista_desplegable">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvSubfuenteConvencionalSustituirData" ControlToValidate="ddlSubfuenteConvencionalSustituirData" InitialValue="" ValidationGroup="Solicitar" Text="Debe seleccionar un valor" Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblEmisionesCO2" runat="server" SkinID="etiqueta_negra_bold" Text="Emisiones CO2 que se dejarán de emitir (Ton/Año):"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:TextBox ID="txtEmisionesCO2Data" runat="server" ReadOnly="True"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvEmisionesCO2Data" ControlToValidate="txtEmisionesCO2Data" ValidationGroup="Solicitar" Text="Debe ingresar un valor" Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <%--<td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblCalculoANLA" runat="server" SkinID="etiqueta_negra_bold" Text="Calculo ANLA:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:TextBox ID="txtCalculoANLAData" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvCalculoANLAData" ControlToValidate="txtCalculoANLAData" ValidationGroup="Solicitar" Text="Debe ingresar un valor" Display="Dynamic">* Debe ingresar un valor</asp:RequiredFieldValidator>
                        </td>--%>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblValorTotalInversión" runat="server" SkinID="etiqueta_negra_bold" Text="Valor Total de la Inversión:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:TextBox ID="txtValorTotalInversionData" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;" runat="server" id="tdIVA1" visible="false">
                            <asp:Label ID="lblValorIVA" runat="server" SkinID="etiqueta_negra_bold" Text="Valor IVA:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;" runat="server" id="tdIVA2" visible="false">
                            <asp:TextBox ID="txtValorIVAData" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblMatrizBienes" runat="server" SkinID="etiqueta_negra_bold" Text="Matriz de Bienes de la certificación:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;" colspan="5">
                            <table class="TablaGrillaCampoAutoliquidacion">
                                <tr>
                                    <td colspan="2" style="border: 0px solid #ddd !important;">
                                        <asp:GridView ID="grvBienesCertificacion" runat="server" AutoGenerateColumns="false" SkinID="GrillaSolicitudAutoliquidacion" EmptyDataText="No se encontraron registros de bienes">
                                            <Columns>
                                                <asp:BoundField HeaderText="Elemento" DataField="elemento" />
                                                <asp:BoundField HeaderText="Subpartida arancelaria" DataField="subpartida_arancelaria" />
                                                <asp:BoundField HeaderText="Cantidad" DataField="cantidad" DataFormatString="{0:D}" />
                                                <asp:BoundField HeaderText="Marca" DataField="marca" />
                                                <asp:BoundField HeaderText="Modelo" DataField="modelo" />
                                                <asp:BoundField HeaderText="Fabricante" DataField="fabricante" />
                                                <asp:BoundField HeaderText="Proveedor" DataField="proveedor" />
                                                <asp:BoundField HeaderText="Funcion" DataField="funcion" />
                                                <asp:BoundField HeaderText="Valor" DataField="valor_total" DataFormatString="{0:N}" />
                                                <asp:BoundField HeaderText="IVA" DataField="iva"  DataFormatString="{0:N}"/>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                   
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblMatrizServicios" runat="server" SkinID="etiqueta_negra_bold" Text="Matriz de Servicios de la certificación:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;" colspan="5">
                            <table class="TablaGrillaCampoAutoliquidacion">
                                <tr>
                                    <td colspan="2" style="border: 0px solid #ddd !important;">
                                        <asp:GridView ID="grvServiciosCertificacion" runat="server" AutoGenerateColumns="false" SkinID="GrillaSolicitudAutoliquidacion" EmptyDataText="No se encontraron registros de servicios">
                                            <Columns>
                                                <asp:BoundField HeaderText="Servicio" DataField="servicio" />
                                                <asp:BoundField HeaderText="Proveedor" DataField="proveedor" />
                                                <asp:BoundField HeaderText="Alcance" DataField="alcance" />
                                                <asp:BoundField HeaderText="Valor" DataField="valor_total" DataFormatString="{0:N}" />
                                                <asp:BoundField HeaderText="IVA" DataField="iva" DataFormatString="{0:N}" />
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                        <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblSoportePago" runat="server" SkinID="etiqueta_negra_bold" Text="Soporte de Pago:"></asp:Label>
                        </td>
                        <td colspan="2">
                            <cc1:AsyncFileUpload runat="server" ID="fuplAdjuntarSoportePago" ClientIDMode="AutoID" CssClass="FileUploadRegistro" OnClientUploadStarted="MostrarProgressDropDownSoportePago" OnClientUploadComplete="OcultarProgressDropDownSoportePago" OnClientUploadError="ErrorArchivoSoportePago"/>
                            <asp:CustomValidator runat="server" ID="cvfuplAdjuntarSoportePago" ValidationGroup="Solicitar" Display="Dynamic" ErrorMessage="Debe adjuntar el archivo" OnServerValidate="cvfuplAdjuntarSoportePago_ServerValidate">&nbsp;</asp:CustomValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblNumeroReferencia" runat="server" SkinID="etiqueta_negra_bold" Text="Numero referencia:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNumeroReferencia" runat="server" MaxLength="25" ValidationGroup="Solicitar" ClientIDMode="Static"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvNumeroReferencia" ControlToValidate="txtNumeroReferencia" ValidationGroup="Solicitar" Text="Debe ingresar un valor" Display="Dynamic">De ingresar un valor</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </div>
            <table style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
                <tr>
                    <td style="text-align: center;">
                        <asp:Button ID="btnSolicitarCertificado" runat="server" Text="Solicitar Certificado" OnClick="btnSolicitarCertificado_Click" ValidationGroup="Solicitar" Enabled="False" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancelar" runat="server" Text="Cerrar" CausesValidation="false" OnClick="btnCancelar_Click" />
                    </td>
                </tr>
            </table>

    </div>
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnBuscarCertificado" />
                <asp:AsyncPostBackTrigger ControlID="btnSolicitarCertificado" />
            </Triggers>
    </asp:UpdatePanel>

         <asp:UpdateProgress ID="upnlDatosCertificadoProcess" runat="server" AssociatedUpdatePanelID="upnlDatosCertificado">
                <ProgressTemplate>  
                    <div id="ModalProgressContainer">
                        <div>
                            <p>Procesando...</p>
                            <p><asp:Image ID="imgDatosCertificado" runat="server" SkinId="procesando"/></p>
                        </div>
                    </div>                         
                </ProgressTemplate>
            </asp:UpdateProgress>
     </div>

    <input type="button" runat="server" id="cmdErrorHide" style="display:none;" />
        <cc1:ModalPopupExtender ID="mpeError" runat="server" PopupControlID="dvError" TargetControlID="cmdErrorHide" BehaviorID="mpeErrors" BackgroundCssClass="ModalBackgroundAutoliquidacion">
        </cc1:ModalPopupExtender>
        <div id="dvError" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalAutoliquidacion">
            <asp:UpdatePanel runat="server" ID="upnlError" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="TablaFormularioAutoliquidacion">
                        <tr>
                            <td colspan="2" class="TituloSeccionAutoliquidacion">
                                Certificado UPME
                            </td>
                        </tr>
                        <tr>
                            <td class="ModalImagenes">
                                <asp:Image runat="server" ID="imgIconoErrorProceso" ImageUrl="~/images/error.png" />
                            </td>
                            <td class="ModalTextoTerminos">
                                <asp:Literal runat="server" ID="ltlErrorProceso"></asp:Literal>
                            </td>
                        </tr>                        
                    </table>
                    <table class="TablaBotonesFormularioAutoliquidacion">
                        <tr>
                            <td>
                                <asp:Button runat="server" ID="cmdAceptarErrorProceso" CausesValidation="false" Text="Aceptar" ClientIDMode="Static" OnClick="cmdAceptarError_Click"/>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmdAceptarErrorProceso" EventName="Click" />                                                        
                </Triggers>
            </asp:UpdatePanel>

            <asp:UpdateProgress ID="uppErrorProceso" runat="server" AssociatedUpdatePanelID="upnlError">
                <ProgressTemplate>  
                    <div id="ModalProgressContainer">
                        <div>
                            <p>Procesando...</p>
                            <p><asp:Image ID="imgErrorProceso" runat="server" SkinId="procesando"/></p>
                        </div>
                    </div>                         
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>


    <input type="button" runat="server" id="cmdMensajeOkHide" style="display:none;" />
        <cc1:ModalPopupExtender ID="mpeMensajeOk" runat="server" PopupControlID="dvMensajeOk" TargetControlID="cmdMensajeOkHide" BehaviorID="mpeMensajeOks" BackgroundCssClass="ModalBackgroundAutoliquidacion">
        </cc1:ModalPopupExtender>
        <div id="dvMensajeOk" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalAutoliquidacion">
            <asp:UpdatePanel runat="server" ID="upnlMensajeOK" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="TablaFormularioAutoliquidacion">
                        <tr>
                            <td colspan="2" class="TituloSeccionAutoliquidacion">
                                Certificado UPME
                            </td>
                        </tr>
                        <tr>
                            <td class="ModalImagenes">
                                <asp:Image runat="server" ID="imgIconoMensajeOk" ImageUrl="~/App_Themes/Img/chulo_verde.png" Width="38px" Height="40px"/>
                            </td>
                            <td class="ModalTextoTerminos">
                                <asp:Literal runat="server" ID="ltlMensajeOk"></asp:Literal>
                            </td>
                        </tr>                        
                    </table>
                    <table class="TablaBotonesFormularioAutoliquidacion">
                        <tr>
                            <td>
                                <asp:Button runat="server" ID="btnAceptarMensajeOk" CausesValidation="false" Text="Aceptar" ClientIDMode="Static" OnClick="btnAceptarMensajeOk_Click"/>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAceptarMensajeOk" EventName="Click" />                                                        
                </Triggers>
            </asp:UpdatePanel>

            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upnlMensajeOK">
                <ProgressTemplate>  
                    <div id="ModalProgressContainer">
                        <div>
                            <p>Procesando...</p>
                            <p><asp:Image ID="imgMensajeOkProcesando" runat="server" SkinId="procesando"/></p>
                        </div>
                    </div>                         
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>

    <asp:UpdateProgress ID="uppSolicitudeLiquidacion" runat="server" AssociatedUpdatePanelID="upnlSolicitudeLiquidacion">
        <ProgressTemplate>  
            <div id="ModalProgressContainer">
                <div>
                    <p>Procesando...</p>
                    <p><asp:Image ID="imgSolicitudeLiquidacion" runat="server" SkinId="procesando"/></p>
                </div>
            </div>                         
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="uppAdjuntarSoportePago" runat="server" AssociatedUpdatePanelID="upnlDatosCertificado">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgUpdateProgressAdjuntarEnsayoLaboratorio" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
</asp:Content>
