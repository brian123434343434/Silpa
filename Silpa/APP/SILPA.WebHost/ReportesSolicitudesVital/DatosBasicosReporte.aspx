<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="DatosBasicosReporte.aspx.cs" Inherits="ReportesSolicitudesVital_DatosBasicosReporte" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../jquery/jquery.js"></script>
    <link href="../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../jquery/EstiloGris/jquery-ui.js"></script>
    <script src="../jquery/jquery.numeric.js"></script>
    <script src="../js/DatosBasicosReporte.js"></script>
    <link href="../App_Themes/skin/StyleREDDS.css" rel="stylesheet" />
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

        .HeaderGridView {
            background-color: #003399 !important;
            color: white !important;
        }

        .CentrarTexto {
            text-align: center;
        }

        .FondoAplicacion {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .FormatoTexto {
            font-weight: bold;
            color: #31708f;
            border-color: #bce8f1;
            /*width:50px;*/
            text-align: left;
            vertical-align: middle;
        }

        .AlinearDescripcion {
            text-align: center;
            vertical-align: central;
            width: 130px;
            font-weight: bold;
            color: #31708f;
            border-color: #bce8f1;
        }


        .alinearTitulos {
            text-align: center;
            font-weight: bold;
            color: #31708f;
            border-color: #bce8f1;
            background-color: #d9edf7;
        }

        .alinearSubTitulos {
            text-align: center;
            vertical-align: central;
            font-weight: bold;
            background-color: #d9edf7;
            color: #31708f;
            vertical-align: middle !important;
        }

        .alinearTexto {
            text-align: center;
            vertical-align: central;
            font-weight: bold;
        }

        .AnchoAltoCheck {
            Width: 20px;
            Height: 20px;
        }

        .CajaDialogo {
            background-color: #fff;
            border-width: 1px;
            border-style: outset;
            border-color: Yellow;
            padding: 0px;
        }
    </style>

    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="Reporte Solicitudes Vital" SkinID="titulo_principal_blanco"></asp:Label>
        &nbsp;
        <a href="#" id="hrCerrarVentana" visible="false" runat="server" onclick="window.close();return false;">Salir</a>
    </div>

    <%--<div class="div-contenido">--%>
    <div class="table-responsive">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableHistory="false">
        </asp:ScriptManager>
        
        <%--<div class="contact_form" id="dContactForm">--%>
        <div class="table-responsive" id="dContactForm">
            <asp:UpdatePanel runat="server" ID="UpdDepartmanetoDestino">
                <ContentTemplate>
                    <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; width: 100%; border: 1px solid #CCCCCC !important;">
                        <tr>
                            <td class="FormatoTexto" style="text-align: left; vertical-align: middle;">
                                <label for="CboAutoridadAmbiental" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Autoridad Ambiental:</label>
                            </td>
                            <td style="text-align: left; vertical-align: middle;" colspan="4">
                                <asp:DropDownList ID="CboAutoridadAmbiental" runat="server" Enabled="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="FormatoTexto">
                                <label for="CboTipoTramite" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Tipo Solicitud:</label>
                            </td>
                            <td style="text-align: left; vertical-align: middle;" colspan="4">
                                <asp:DropDownList ID="CboTipoTramite" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CboTipoTramite_SelectedIndexChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator Display="Dynamic" ID="RFV_CboTipoTramite" runat="server" ControlToValidate="CboTipoTramite" ErrorMessage="Debe Seleccionar un tipo de tramite" ValidationGroup="GenerarReporte">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="FormatoTexto" style="text-align: left; vertical-align: middle;">
                                <label for="TxtNumeroVital" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Numero Vital:</label>
                            </td>
                            <td style="text-align: left; vertical-align: middle;" colspan="4">
                                <asp:TextBox ID="TxtNumeroVital" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="FormatoTexto" style="text-align: left; vertical-align: middle;">
                                <label for="CboClaseRecurso" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Solicitante:</label>
                            </td>
                            <td style="text-align: left; vertical-align: middle;" colspan="4">
                                <asp:UpdatePanel ID="upnlSolicitante1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtSolicitante" runat="server" placeholder="Nombre Titular" Width="235px" Enabled="false" /><asp:HiddenField ID="hfIdSolicitante" runat="server" />
                                        <asp:LinkButton ID="lnkSolicitante" runat="server" Text="Agregar Solicitante" OnClick="lnkSolicitante_Click" CssClass="a_green"></asp:LinkButton>
                                        &nbsp
                                        <asp:LinkButton ID="LnkLimpiarSolicitante" runat="server" Text="Quitar Solicitante" OnClick="LnkLimpiarSolicitante_Click" CssClass="a_red"></asp:LinkButton>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lnkSolicitante" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: middle;">
                                <label for="CboClaseRecurso" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Fecha de Generacion</label>
                            </td>
                            <td style="text-align: left; vertical-align: middle; width: 100px;">Desde:</td>
                            <td style="text-align: left; vertical-align: middle; width: 100px;">
                                <asp:TextBox ID="TxtFecExpDesde" runat="server" ReadOnly="false" Width="130px" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td style="text-align: left; vertical-align: middle; width: 100px;">Hasta:</td>
                            <td style="text-align: left; vertical-align: middle">
                                <asp:TextBox ID="TxtFecExpHasta" runat="server" ReadOnly="false" Width="130px" ClientIDMode="Static"></asp:TextBox>
                                <asp:LinkButton ID="LnkLimpiarFechas" runat="server" Text="Remover Fechas" OnClick="LnkLimpiarFechas_Click"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr runat="server" id="trDepartamentos" visible="false">
                            <td style="text-align: left; vertical-align: middle;">
                                <label for="CboDptoMunicipioOrigen" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Ubicacion del Incidente</label>
                            </td>
                            <td style="text-align: left; vertical-align: middle;">Departamento:</td>
                            <td>
                                <asp:DropDownList ID="CboDepartamentoDestino" runat="server" Width="145px" AutoPostBack="true" OnSelectedIndexChanged="CboDepartamentoDestino_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td style="text-align: left; vertical-align: middle;">Municipio:</td>
                            <td>
                                <asp:DropDownList ID="CboMunicipioDestino" runat="server" Width="145px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:UpdatePanel ID="upnlAccionesBoton" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="BtnConsultar" SkinID="boton_copia" runat="server" Text="Consultar" ValidationGroup="GenerarReporte" OnClick="BtnConsultar_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <div style="overflow: scroll; text-align: left; vertical-align: top;">
                <asp:UpdatePanel ID="UpdgrvConsultaSalvocondcuto" runat="server">
                    <ContentTemplate>
                        <%--<HeaderStyle BackColor="#003399" ForeColor="White" CssClass="HeaderGridView" />--%>
                        <%--<PagerSettings Mode="Numeric" Position="Bottom" PageButtonCount="10" />--%>
                        <asp:GridView ID="GrvResultados" runat="server" Width="100%" SkinID="Grilla" 
                            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="false" 
                            PageSize="20" EmptyDataText="No Hay Datos para Esta Consulta"
                            OnRowDataBound="GrvResultados_RowDataBound"
                            OnDataBound="GrvResultados_DataBound"
                            OnPageIndexChanged="GrvResultados_PageIndexChanged"
                            OnPageIndexChanging="GrvResultados_PageIndexChanging">
                            <HeaderStyle Font-Size="9pt" />
                            <FooterStyle Font-Size="9pt" />
                            <RowStyle Font-Size="9pt" />
                            <SelectedRowStyle Font-Size="9pt" />
                            <EditRowStyle Font-Size="9pt" />
                            <AlternatingRowStyle Font-Size="9pt" />
                            <Columns>
                                <asp:TemplateField ShowHeader="false" ItemStyle-Width="200px" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelCol1" runat="server" Text='<%# Bind("COL_1") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#003399" ForeColor="White" CssClass="HeaderGridView" />
                                </asp:TemplateField>

                                <asp:TemplateField ShowHeader="true" ItemStyle-Width="200px">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelCol2" runat="server" Text='<%# Bind("COL_2") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#003399" ForeColor="White" CssClass="HeaderGridView" />
                                </asp:TemplateField>

                                <asp:TemplateField ShowHeader="true" ItemStyle-Width="200px">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelCol3" runat="server" Text='<%# Bind("COL_3") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#003399" ForeColor="White" CssClass="HeaderGridView" />
                                </asp:TemplateField>

                                <asp:TemplateField ShowHeader="true" ItemStyle-Width="200px">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelCol4" runat="server" Text='<%# Bind("COL_4") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#003399" ForeColor="White" CssClass="HeaderGridView" />
                                </asp:TemplateField>

                                <asp:TemplateField ShowHeader="true" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelCol5" runat="server" Text='<%# Bind("COL_5") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#003399" ForeColor="White" CssClass="HeaderGridView" />
                                </asp:TemplateField>

                                <asp:TemplateField ShowHeader="true" ItemStyle-Width="200px">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelCol6" runat="server" Text='<%# Bind("COL_6") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#003399" ForeColor="White" CssClass="HeaderGridView" />
                                </asp:TemplateField>

                                <asp:TemplateField ShowHeader="true" ItemStyle-Width="200px">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelCol7" runat="server" Text='<%# Bind("COL_7") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#003399" ForeColor="White" CssClass="HeaderGridView" />
                                </asp:TemplateField>

                                <asp:TemplateField ShowHeader="true" ItemStyle-Width="200px">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelCol7" runat="server" Text='<%# Bind("COL_8") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#003399" ForeColor="White" CssClass="HeaderGridView" />
                                </asp:TemplateField>

                                <asp:TemplateField ShowHeader="false" ItemStyle-Width="200px">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HypVerSalvoconducto" runat="server" Target="_blank" NavigateUrl='<%# urlDetalleSolicitud(Eval("COL_2"),Eval("COL_1")) %>' CssClass="a_green">Ver Detalles</asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#003399" ForeColor="White" CssClass="HeaderGridView" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnConsultar" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="contact_form" runat="server" id="dvExportar">
        <div class="TableButton">
            <div class="RowButton">
                <div class="CellButton">
                    <asp:ImageButton ValidationGroup="GenerarReporte" ID="imbExportarExcel" runat="server" Width="68px" Height="16px" ToolTip="Exportar listado a MS Excel" BorderWidth="0px" ImageUrl="../App_Themes/Img/exportar_excel.gif" OnClick="imbExportarExcel_Click"></asp:ImageButton>
                </div>
            </div>
        </div>
    </div>

    <asp:UpdatePanel runat="server" ID ="UpdContadorRegistros">
        <ContentTemplate>
        <div  class="div-contenido">
            <table cellSpacing=0 cellPadding=1 width="80%" align=left border=0 class="FormatoTexto">
            <tr>
                <td align=left>
                    <table cellSpacing=2 cellPadding=1 border=0>
                        <tr>
                            <td>
                                <asp:Label id="lbl_pagina" runat="server" ToolTip="Página" Text="Página" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:Label id="lbl_numero_pagina" runat="server" ToolTip="Usted se encuentra en esta página" Text="0" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:Label id="lbl_de" runat="server" ToolTip="de" Text="de" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:Label id="lbl_numero_paginas" runat="server" ToolTip="Número de páginas que contiene este listado" Text="0" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                       </tr>
                    </table>
                    <asp:Label SkinID="etiqueta_negra" ID="lbl_total" runat="server" CssClass="texto_usuario" Text="Número total de registros : "
                        ToolTip="Total de registros encontrados" Visible="true"></asp:Label><br />
                    <asp:Label SkinID="etiqueta_negra" id="lbl_sel_todos" runat="server" Visible="true"></asp:Label>
                </td>
            </tr>
        </table>
        </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnConsultar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

    <div class="RowButton">
        <div class="CellButton">
            <asp:ValidationSummary ID="VsReporteSolicitudesVital" runat="server" ValidationGroup="GenerarReporte" DisplayMode="List" />
        </div>
    </div>

    <script src="../js/DatosBasicosReporte.js"></script>

    <asp:Label ID="lblSolicitante" runat="server" SkinID="etiqueta_negra"></asp:Label>

    <cc1:ModalPopupExtender ID="mpeSolicitantes" runat="server"
        TargetControlID="lblSolicitante"
        PopupControlID="pnlSolicitantes"
        DropShadow="True" Enabled="True" DynamicServicePath=""
        BackgroundCssClass="FondoAplicacion" />
    <asp:Panel ID="pnlSolicitantes" runat="server" Style="display: none;" CssClass="CajaDialogo">
        <asp:UpdatePanel ID="upnlSolicitante" runat="server">
            <ContentTemplate>
                <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                    <tr>
                        <th colspan="3" style="font-size: 12pt; font-weight: bold; border-bottom: 1px solid Gray;">Consultar Solicitante</th>
                    </tr>
                    <tr>
                        <td>
                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Tipo Identificacion:</label>
                        </td>
                        <td colspan="2">
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
                        <td style="padding-left: 30px; text-align: right; vertical-align: middle;">
                            <asp:Button ID="btnBuscarSolicitante" runat="server" Text="Buscar" SkinID="boton_copia" OnClick="btnBuscarSolicitante_Click" ValidationGroup="solicitante" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-top: 10px; padding-bottom: 10px; text-align: left; vertical-align: middle; border-top: 1px solid #000000; border-bottom: 1px solid #000000;">
                            <asp:Label ID="lblNombreSolicitante" runat="server" ClientIDMode="Static" SkinID="etiqueta_negra10N" Font-Bold="true"></asp:Label>
                        </td>  
                        <td style="padding-top: 10px; padding-bottom: 10px; text-align: right; vertical-align: middle; border-top: 1px solid #000000; border-bottom: 1px solid #000000;">
                            <asp:LinkButton ID="lnkSeleccionarSolicitante" runat="server" OnClick="lnkSeleccionarSolicitante_Click" Text="Seleccionar" Visible="false" CssClass="a_orange"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnCerrarVinculosActividad" />
            </Triggers>
        </asp:UpdatePanel>
        <div style="padding-top: 20px; text-align: center; vertical-align: middle; width: 100%;">
            <asp:Button ID="btnCerrarVinculosActividad" runat="server" Text="Cerrar" CausesValidation="False" SkinID="boton_copia" OnClick="btnCerrarVinculosActividad_Click" />
        </div>
    </asp:Panel>

    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdgrvConsultaSalvocondcuto">
        <ProgressTemplate>
            <div id="ModalProgressContainer">
                <div>
                    <p>Procesando...</p>
                    <p>
                        <asp:Image ID="imgUpdateProgress2" runat="server" SkinID="procesando" />
                    </p>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upnlAccionesBoton">
        <ProgressTemplate>
            <div id="ModalProgressContainer">
                <div>
                    <p>Procesando...</p>
                    <p>
                        <asp:Image ID="imgUpdateProgress" runat="server" SkinID="procesando" />
                    </p>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
