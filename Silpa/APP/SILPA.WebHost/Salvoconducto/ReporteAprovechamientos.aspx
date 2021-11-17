<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPASinMenuFlash.master" AutoEventWireup="true" CodeFile="ReporteAprovechamientos.aspx.cs" Inherits="Salvoconducto_ReporteAprovechamientos" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/jquery.datetimepicker.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

    <script runat="server">
    </script>

    <style type="text/css">
        label {
            font-family: Arial, Helvetica, sans-serif !important;
            font-size: 10pt !important;
            color: #000000 !important;
            width: 200px !important;
        }

        table {
            /*border: 1px solid #000;*/
        }

            table tr td {
                border: 0px solid #ddd !important;
                padding: 4px;
            }

        .Button {
            background-color: #ddd;
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
            width: 50px;
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
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="Consultar Aprovechamientos Cargados" SkinID="titulo_principal_blanco"></asp:Label>
        &nbsp;
        <a href="#" id="hrCerrarVentana" visible="false" runat="server" onclick="window.close();return false;">Salir</a>
    </div>

    <div class="table-responsive">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableHistory="false">
        </asp:ScriptManager>

        <div class="contact_form" id="dContactForm">
            <asp:UpdatePanel runat="server" ID="UpdDepartmanetoDestino">
                <ContentTemplate>
                    <table width="90%" border="0">
                        <tr>
                            <td class="FormatoTexto" style="text-align: left; vertical-align: middle;">
                                <label for="CboAutoridadAmbiental" style="width: 200px">Autoridad Ambiental:</label>
                            </td>
                            <td style="text-align: left; vertical-align: middle;" colspan="4">
                                <asp:DropDownList ID="CboAutoridadAmbiental" runat="server" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="FormatoTexto" style="text-align: left; vertical-align: middle;">
                                <label for="TxtNumeroActoAdministrativo" style="width: 200px">Numero del acto administrativo:</label>
                            </td>
                            <td style="text-align: left; vertical-align: middle;" colspan="4">
                                <asp:TextBox ID="TxtNumeroActoAdministrativo" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="FormatoTexto" style="text-align: left; vertical-align: middle;">
                                <label for="CboClaseRecurso" style="width: 200px">Titular del acto adminitrativo:</label>
                            </td>
                            <td style="text-align: left; vertical-align: middle;" colspan="4">
                                <asp:UpdatePanel ID="upnlSolicitante1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtSolicitante" runat="server" placeholder="Nombre Titular" Width="235px" Enabled="false" /><asp:HiddenField ID="hfIdSolicitante" runat="server" />
                                        <asp:LinkButton ID="lnkSolicitante" runat="server" Text="Agregar Titular" OnClick="lnkSolicitante_Click" CssClass="a_green"></asp:LinkButton>
                                        &nbsp
                                            <asp:LinkButton ID="LnkLimpiarSolicitante" runat="server" Text="Quitar Titular" OnClick="LnkLimpiarSolicitante_Click" CssClass="a_red"></asp:LinkButton>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lnkSolicitante" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: middle;">
                                <label for="CboClaseRecurso" style="width: 200px">Fecha de Expedicion</label>
                            </td>
                            <td style="text-align: left; vertical-align: middle; width: 100px;">Desde:
                            </td>
                            <td>
                                <asp:TextBox ID="TxtFecExpDesde" runat="server" Width="130px" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFechaexpDesde" runat="server" ControlToValidate="TxtFecExpDesde" ValidationGroup="consulta" ErrorMessage="Debe seleccionar una fecha desde de expedición">*</asp:RequiredFieldValidator>
                            </td>
                            <td style="text-align: left; vertical-align: middle; width: 100px;">Hasta:
                            </td>
                            <td>
                                <asp:TextBox ID="TxtFecExpHasta" runat="server" Width="130px" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFechaexpHasta" runat="server" ControlToValidate="TxtFecExpHasta" ValidationGroup="consulta" ErrorMessage="Debe seleccionar una fecha hasta de expedición">*</asp:RequiredFieldValidator>
                                <asp:LinkButton ID="LnkLimpiarFechas" runat="server" Text="Remover Fechas" OnClick="LnkLimpiarFechas_Click"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: middle;">
                                <label style="width: 200px">Procedencia Legal</label>
                            </td>
                            <td style="text-align: left; vertical-align: middle;">Departamento:
                            </td>
                            <td>
                                <asp:DropDownList ID="CboDepartamentoProcedencia" runat="server" Width="145px" AutoPostBack="true" OnSelectedIndexChanged="CboDepartamentoProcedencia_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td style="text-align: left; vertical-align: middle;">Municipio:
                            </td>
                            <td>
                                <asp:DropDownList ID="CboMunicipioProcedencia" runat="server" Width="145px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: middle;">
                                <label for="TxtFecExpHasta" style="width: 200px">Clase recurso</label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="upnlClaseRecurso" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cboClaseRecurso" runat="server" Width="145px" OnSelectedIndexChanged="cboClaseRecurso_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvTipoSalvoconducto" runat="server" ControlToValidate="cboClaseRecurso" ValidationGroup="consulta" ErrorMessage="Debe seleccionar una clase de recurso">*</asp:RequiredFieldValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: middle;">
                                <label for="cboFormaOtorgamiento" style="width: 200px">Forma de Otorgamiento</label>
                            </td>
                            <td colspan="3">
                                <asp:UpdatePanel ID="upnlFormaOtorgamiento" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cboFormaOtorgamiento" runat="server" Width="145px" OnSelectedIndexChanged="cboFormaOtorgamiento_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static" ></asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cboClaseRecurso" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: middle;">
                                <label for="cboModoAdquisicion" style="width: 200px">Modo de Adquisición</label>
                            </td>
                            <td colspan="3">
                                <asp:UpdatePanel ID="upnlModoAdquisicion" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cboModoAdquisicion" runat="server" Width="145px"></asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cboClaseRecurso" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label for="txtNombrePredio" style="width: 200px">Nombre del predio</label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtNombrePredio" runat="server"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="5">
                                <asp:UpdatePanel ID="upnlAccionesBoton" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="BtnConsultar" SkinID="boton_copia" runat="server" Text="Consultar" ValidationGroup="consulta" OnClick="BtnConsultar_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="consulta" DisplayMode="BulletList" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <table border="1" width="100%">
                <tr>
                    <td style="overflow: scroll;">
                        <asp:UpdatePanel ID="UpdgrvConsultaSalvocondcuto" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grvEstadoSalvoconducto" runat="server" AutoGenerateColumns="false" ShowFooter="false" AllowPaging="True" AllowSorting="True" OnRowCommand="grvEstadoSalvoconducto_RowCommand"
                                    OnPageIndexChanging="grvEstadoSalvoconducto_PageIndexChanging" PageSize="20" SkinID="GrillaPDV" EmptyDataText="No Hay Datos para Esta Consulta">
                                    <PagerStyle BackColor="LightGray" Height="30px" VerticalAlign="Bottom" HorizontalAlign="Center" />
                                    <PagerSettings Mode="Numeric" Position="Bottom" PageButtonCount="10" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Numero Solicitud" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAprovechamiento" runat="server" Text='<%# Bind("APROVECHAMIENTO_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Autoridad Ambiental" DataField="AUTORIDAD_AMBIENTAL" />
                                        <asp:BoundField HeaderText="Clase recurso" DataField="CLASE_RECURSO" />
                                        <asp:BoundField HeaderText="Forma Otorgamiento" DataField="FORMA_OTORGAMIENTO" />
                                        <asp:BoundField HeaderText="Modo Adquisición" DataField="MODO_ADQUISICION" />
                                        <asp:BoundField HeaderText="Número" DataField="NUMERO" />
                                        <asp:BoundField HeaderText="Titular" DataField="SOLICITANTE" />
                                        <asp:BoundField HeaderText="Fecha expedición" DataField="FECHA_EXPEDICION" />
                                        <asp:BoundField HeaderText="Fecha finalizacion" DataField="FECHA_FINALIZACION" />
                                        <asp:BoundField HeaderText="Departamento" DataField="DEP_NOMBRE" />
                                        <asp:BoundField HeaderText="Municipio" DataField="MUN_NOMBRE" />
                                        <asp:TemplateField ShowHeader="false" ItemStyle-Width="200px">
                                            <ItemTemplate>
                                                <table style="text-align: left; vertical-align: top;" border="0">
                                                    <tr>
                                                        <td>
                                                            <asp:HyperLink ID="HypVerSalvoconducto" runat="server" Target="_blank" NavigateUrl='<%# urlNavegacionVerAprovechamiento(Eval("APROVECHAMIENTO_ID")) %>'>Ver Detalles</asp:HyperLink>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="LnkArchivoAprovechamiento" OnClick="LnkArchivoAdjuntoCreacion_Click" Visible="true" runat="server" CommandArgument='<%# Eval("RUTA_ARCHIVO") +","+ Eval("APROVECHAMIENTO_ID") %>' CommandName="DescargarArchivo" Text="Descargar"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>

                <tr>
                    <td style="overflow: scroll;">
                        <asp:UpdatePanel ID="UpdgrvConsultaAprovechamientos" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GrvEstadoAprovechamiento" runat="server" AutoGenerateColumns="false" ShowFooter="false" AllowPaging="True" AllowSorting="True" OnRowCommand="GrvEstadoAprovechamiento_RowCommand"
                                    OnPageIndexChanging="GrvEstadoAprovechamiento_PageIndexChanging" PageSize="20" SkinID="GrillaPDV" EmptyDataText="">
                                    <PagerStyle BackColor="LightGray" Height="30px" VerticalAlign="Bottom" HorizontalAlign="Center" />
                                    <PagerSettings Mode="Numeric" Position="Bottom" PageButtonCount="10" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Numero Solicitud" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAprovechamiento" runat="server" Text='<%# Bind("APROVECHAMIENTO_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Autoridad Ambiental" DataField="AUTORIDAD_AMBIENTAL" />
                                        <asp:BoundField HeaderText="Tipo Aprovechamiento" DataField="TIPO_APROVECHAMIENTO" />
                                        <asp:BoundField HeaderText="Numero" DataField="NUMERO" />
                                        <asp:BoundField HeaderText="Clase Recurso" DataField="CLASE_RECURSO" />
                                        <asp:BoundField HeaderText="Autoridad Otorga" DataField="AUTORIDAD_OTORGA" />
                                        <asp:BoundField HeaderText="Fecha Expedicion" DataField="FECHA_EXPEDICION" />
                                        <asp:BoundField HeaderText="Fecha Desde" DataField="FECHA_DESDE" />
                                        <asp:BoundField HeaderText="Fecha Hasta" DataField="FECHA_HASTA" />
                                        <asp:BoundField HeaderText="Departamento" DataField="DEP_NOMBRE" />
                                        <asp:BoundField HeaderText="Municipio" DataField="MUN_NOMBRE" />
                                        <asp:BoundField HeaderText="Finalidad" DataField="FINALIDAD" />
                                        <asp:BoundField HeaderText="Pais Procedencia" DataField="PAIS_PROCEDENCIA" />
                                        <asp:BoundField HeaderText="Solicitante" DataField="SOLICITANTE" />

                                        <asp:TemplateField ShowHeader="false" ItemStyle-Width="200px">
                                            <ItemTemplate>
                                                <table style="text-align: left; vertical-align: top;" border="0">
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton ID="LnkArchivoTipoAprovechamiento" OnClick="LnkArchivoTipoAprovechamiento_Click" Visible="true" runat="server" CommandArgument='<%# Eval("RUTA_ARCHIVO") +","+ Eval("APROVECHAMIENTO_ID") %>' CommandName="DescargarArchivo" Text="Descargar"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
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
                        <asp:UpdatePanel ID="upnlTotalRegistros" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblTotalRegistros" runat="server" SkinID="etiqueta_negra" CssClass="texto_usuario" ToolTip="Total de registros encontrados" Text="Número total de registros : "></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        
                    </td>
                </tr>
            </table>
            <br />
            <div class="contact_form" runat="server" id="dvExportar">
                <div class="TableButton">
                    <div class="RowButton">
                        <div class="CellButton">
                            <asp:ImageButton ID="imbExportarExcel" runat="server" Width="68px" Height="16px" ToolTip="Exportar listado a MS Excel" BorderWidth="0px" ImageUrl="../App_Themes/Img/exportar_excel.gif" OnClick="imbExportarExcel_Click" ValidationGroup="consulta"></asp:ImageButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Label ID="lblSolicitante" runat="server"></asp:Label>
    <cc1:ModalPopupExtender ID="mpeSolicitantes" runat="server"
        TargetControlID="lblSolicitante"
        PopupControlID="pnlSolicitantes"
        DropShadow="True" Enabled="True" DynamicServicePath=""
        BackgroundCssClass="FondoAplicacion">
    </cc1:ModalPopupExtender>

    <asp:Panel ID="pnlSolicitantes" runat="server" Style="display: none;" CssClass="CajaDialogo">
        <div>
            <asp:UpdatePanel ID="upnlSolicitante" runat="server">
                <ContentTemplate>
                    <table width="400px">
                        <tr>
                            <td>
                                <label>Tipo Identificacion:</label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboTipoIdentificacion" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvTipoIdentificacion" Display="Dynamic" runat="server" ControlToValidate="cboTipoIdentificacion" ValidationGroup="solicitante">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Identificacion:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumeroIdentificacion" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNumeroIdentificacion" Display="Dynamic" runat="server" ControlToValidate="txtNumeroIdentificacion" ValidationGroup="solicitante">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnBuscarSolicitante" runat="server" Text="Buscar" SkinID="boton_copia" OnClick="btnBuscarSolicitante_Click" ValidationGroup="solicitante" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNombreSolicitante" runat="server" ClientIDMode="Static" SkinID="etiqueta_negra"></asp:Label>
                                <asp:LinkButton ID="lnkSeleccionarSolicitante" runat="server" OnClick="lnkSeleccionarSolicitante_Click" Text="Seleccionar" Visible="false" CssClass="a_green"></asp:LinkButton>
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

    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script src="../Scripts/jquery.datetimepicker.js"></script>
    <script src="../jquery/jquery.numeric.js"></script>
    <script src="../js/SeriesSalvoConducto.js"></script>
</asp:Content>
