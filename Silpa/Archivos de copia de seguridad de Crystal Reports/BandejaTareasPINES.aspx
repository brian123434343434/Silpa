<%@ Page Title="Mis Tareas" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="BandejaTareasPINES.aspx.cs" Inherits="BandejaTareas_BandejaTareasPINES" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <%--<script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>--%>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function ProbarURL() {
            if ($("#chkVital").is(':checked') == false) {
                var textUrlActividadProyeto = $("#txtUrlActividadProyeto");
                alert(textUrlActividadProyeto.val());
                window.open('http://' + textUrlActividadProyeto.val(), "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, top=500, left=500, width=400, height=400");
            }
        }
        function ProyectoVital(urlProyecto) {
            window.open('http://' + urlProyecto, "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, top=500, left=500, width=400, height=400");
            return false;
        }
    </script>

    <style type="text/css">
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
        .hover_row
        {
            /* #A1DCF2 - Azul Marino*/
            border: 1px solid #616161;
            background-color: #A1DCF2;
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
    </style>


    <script language="javascript" src="../js/BandejaTareas.js"></script>

    <div class="div-titulo">
        <asp:Label ID="Label1" runat="server" SkinID="titulo_principal_blanco" Text="MIS TAREAS"></asp:Label>
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <cc1:TabContainer ID="tbcContenedor" runat="server" Width="100%" ActiveTabIndex="0">
        <cc1:TabPanel runat="server" HeaderText="Tareas sin Iniciar" ID="tabTareasSinIniciar">
            <ContentTemplate>
                <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                    <tr>
                        <td colspan="2" style="text-align: justify">
                            <asp:Label ID="lblMensaje" runat="server" SkinID="etiqueta_verde" Style="text-align: justify" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Número VITAL" SkinID="etiqueta_negra"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNumeroVital" runat="server" Width="216px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFechaInicial" runat="server" ClientIDMode="Static" Text="Fecha Desde (dd/mm/aaaa):" SkinID="etiqueta_negra"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaInicial" runat="server" MaxLength="10" Width="80px"></asp:TextBox>
                            <cc1:CalendarExtender ID="calFechaInicial" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                TargetControlID="txtFechaInicial">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Fecha Hasta (dd/mm/aaaa):" SkinID="etiqueta_negra"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaFinal" runat="server" MaxLength="10" Width="80px"></asp:TextBox>
                            <cc1:CalendarExtender ID="calFechaFinal" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                TargetControlID="txtFechaFinal">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                            <asp:Button ID="btnBuscar" runat="server" SkinID="boton_copia"
                                Text="Buscar" CausesValidation="False" OnClick="btnBuscar_Click" />
                        </td>
                    </tr>
                </table>

                <asp:GridView ID="gdvTareasSinIniciar" runat="server" Width="100%" 
                    SkinID="Grilla" AllowPaging="True" AutoGenerateColumns="False" 
                    OnRowDataBound="gdvTareasSinIniciar_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="NUMERO_VITAL" HeaderText="N&#250;mero VITAL" SortExpression="NUMERO_VITAL" />
                        <asp:BoundField DataField="NUMERO_EXPEDIENTE" HeaderText="N&#250;mero Expediente" SortExpression="NUMERO_EXPEDIENTE" />
                        <asp:BoundField DataField="NOMBRE_TIPO_TRAMITE" HeaderText="Tipo Tr&#225;mite" SortExpression="NOMBRE_TIPO_TRAMITE" />
                        <asp:TemplateField HeaderText="Tarea" SortExpression="TAREA">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkTarea" runat="server" Text='<%# Bind("TAREA") %>' Visible="true" OnClick="lnkTarea_Click" CausesValidation="false" SkinID=""></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha Inicio" SortExpression="FECHA_INICIO">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("FECHA_INICIO", "{0:dd/MM/yyyy}") %>' SkinID="etiqueta_negra"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="distributionmode" SortExpression="distributionmode" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblModeloDistribucion" runat="server" Text='<%# Bind("distributionmode") %>' SkinID="etiqueta_negra"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="entrydatatype" SortExpression="entrydatatype" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblEntryDataType" runat="server" Text='<%# Bind("entrydatatype") %>' SkinID="etiqueta_negra"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="URLDataView" SortExpression="URLDataView" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblUrlDataView" runat="server" Text='<%# Bind("URLDataView") %>' SkinID="etiqueta_negra"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkVincularProyecto" runat="server" Text="Vincular Proyecto" OnClick="lnkVincularProyecto_Click" CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkFinalizarTarea" runat="server" Text="Finalizar" OnClick="lnkFinalizarTarea_Click" CausesValidation="false" Visible='<%# Convert.ToBoolean(Eval("FINALIZAR")) %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblIdProcess" runat="server" Text='<%# Bind("IDPROCESS") %>' SkinID="etiqueta_negra"></asp:Label>
                                <asp:Label ID="lblIdActivity" Text='<%# Bind("IDACTIVITY") %>' runat="server" SkinID="etiqueta_negra"></asp:Label>
                                <asp:Label ID="lblActivityInstance" runat="server" Text='<%# Bind("IDACTIVITY_INSTANCE") %>' SkinID="etiqueta_negra"></asp:Label>
                                <asp:Label ID="lblIdRelated" runat="server" Text='<%# Bind("IDRelated") %>' SkinID="etiqueta_negra"></asp:Label>
                                <asp:Label ID="lblIdEntryData" runat="server" Text='<%# Bind("IDEntryData") %>' SkinID="etiqueta_negra"></asp:Label>
                                <asp:Label ID="lblEntryData" runat="server" Text='<%# Bind("EntryData") %>' SkinID="etiqueta_negra"></asp:Label>
                                <asp:Label ID="lblEntryDataTypeProcess" runat="server" Text='<%# Bind("EntryDataTypeProcessInstance") %>' SkinID="etiqueta_negra"></asp:Label>
                                <asp:Label ID="lblProcessInstance" runat="server" Text='<%# Bind("IDProcessInstance") %>' SkinID="etiqueta_negra"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <asp:HiddenField ID="hdfIDComentario" runat="server" />
                <asp:HiddenField ID="hdfIdProcessInstance" runat="server" />
                <asp:HiddenField ID="hdfIdActivityInstance" runat="server" />
                <asp:HiddenField ID="hdfIdActivity" runat="server" />
                <asp:HiddenField ID="hdfActividad" runat="server" />
                <asp:HiddenField ID="hdfUrlVital" runat="server" />

                <asp:Label ID="lblComentario" runat="server" SkinID="etiqueta_negra"></asp:Label>

                <cc1:ModalPopupExtender ID="mpeComentario" runat="server"
                    TargetControlID="lblComentario"
                    PopupControlID="pnlComentario"
                    DropShadow="True" Enabled="True" DynamicServicePath=""
                    BackgroundCssClass="FondoAplicacion" />
                <asp:Panel ID="pnlComentario" runat="server" Style="display: none;" CssClass="CajaDialogo">
                    <asp:UpdatePanel ID="upnlAvanzar" runat="server">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnAceptar" />
                            <asp:PostBackTrigger ControlID="btnCancelar" />
                            <asp:PostBackTrigger ControlID="lnkbArchivoAccion" />
                        </Triggers>
                        <ContentTemplate>
                            <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; max-width: 800px;">
                                <tr>
                                    <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Actividad:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblActividad" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Acción
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboAccionActividad" runat="server" OnSelectedIndexChanged="cboAccionActividad_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAccionActividad" runat="server" ErrorMessage="Debe Seleccionar una Acción" ControlToValidate="cboAccionActividad" Display="Dynamic" InitialValue="" ValidationGroup="CommentUsuario">*</asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="hdfContinuaProcesoAccion" runat="server" />
                                        <asp:HiddenField ID="hdfIdProcesoAccionActividad" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Comentario:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine" Rows="3" Width="80%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvComentario" runat="server" ErrorMessage="Debe Ingresar un comentario" ControlToValidate="txtComentario" Display="Dynamic" ValidationGroup="CommentUsuario">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Fecha Compromiso:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFechaCompromiso" runat="server" MaxLength="10" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="ceFechaCompromiso" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="txtFechaCompromiso" CssClass="ajax__calendar_container">
                                        </cc1:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="rfvFechaCompromiso" runat="server" ErrorMessage="Debe Ingresar una fecha de Compromiso" ControlToValidate="txtFechaCompromiso" ValidationGroup="CommentUsuario">*</asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CV1"
                                            runat="server"
                                            ErrorMessage="La fecha de compromiso debe ser menor o igual a la fecha de Vencimiento"
                                            ControlToValidate="txtFechaVencimiento"
                                            ControlToCompare="txtFechaCompromiso"
                                            Operator="GreaterThanEqual"
                                            Type="Date"
                                            EnableViewState="false"
                                            Enabled="true"
                                            ValidationGroup="CommentUsuario" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Fecha Vencimiento:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFechaVencimiento" runat="server" MaxLength="10" Width="80px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Requirio Prorroga:
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkProrroga" runat="server" OnCheckedChanged="chkProrroga_CheckedChanged" AutoPostBack="true" EnableTheming="false" />
                                        <asp:TextBox ID="txtFechaProrroga" runat="server" MaxLength="10" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="ceFechaProrroga" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="txtFechaProrroga" CssClass="ajax__calendar_container">
                                        </cc1:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="rfvFechaProrroga" runat="server" ErrorMessage="Debe Ingresar una fecha de prorroga" ControlToValidate="txtFechaProrroga" ValidationGroup="CommentUsuario">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Estado:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboEstado" runat="server" OnSelectedIndexChanged="cboEstado_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Text="En Proceso" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Terminada" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Archivo:
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="fluArchivoComentario" runat="server" />
                                        <asp:RequiredFieldValidator ID="rfvArchivoComentario" runat="server" ErrorMessage="Debe Seleccionar un Archivo" ControlToValidate="fluArchivoComentario" ValidationGroup="CommentUsuario">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Detiene Flujo:
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkDetieneFlujo" runat="server" EnableTheming="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding: 20px; text-align: left; vertical-align: middle;">
                                        <asp:LinkButton ID="lnkbArchivoAccion" runat="server" CommandName="VerArchivo" Text="Ver Archivo" Visible="false" OnClick="lnkbArchivoAccion_Click" CausesValidation="false"></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                                        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                                            <tr>
                                                <td style="text-align: right; padding-right: 20px; vertical-align: middle;">
                                                    <asp:Button ID="btnAceptar" ClientIDMode="Static" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" SkinID="boton_copia" ValidationGroup="CommentUsuario" />
                                                </td>
                                                <td style="text-align: left; padding-left: 20px; vertical-align: middle;">
                                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false" SkinID="boton_copia" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>

                <asp:Label ID="lblComentarioGerente" runat="server" SkinID="etiqueta_negra"></asp:Label>
                <cc1:ModalPopupExtender ID="mpeComentarioGerente" runat="server"
                    TargetControlID="lblComentarioGerente"
                    PopupControlID="pnlComentarioGerente"
                    DropShadow="True" Enabled="True" DynamicServicePath=""
                    BackgroundCssClass="FondoAplicacion" />
                <asp:Panel ID="pnlComentarioGerente" runat="server" Style="display: none;" CssClass="CajaDialogo">
                    <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; max-width: 800px;">
                        <tr>
                            <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Actividad:
                            </td>
                            <td>
                                <asp:Label ID="lblActividadComentGerente" runat="server" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Comentario:
                            </td>
                            <td>
                                <asp:TextBox ID="txtComentGerente" runat="server" TextMode="MultiLine" Rows="3" Width="300px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvComentGerente" runat="server" ErrorMessage="Debe Ingresar un comentario" ControlToValidate="txtComentGerente" Display="Dynamic">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                                <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                                    <tr>
                                        <td style="text-align: right; padding-right: 20px; vertical-align: middle;">
                                            <asp:Button ID="btnAceptarComentGerente" ClientIDMode="Static" runat="server" Text="Aceptar" OnClick="btnAceptarComentGerente_Click" SkinID="boton_copia" />
                                        </td>
                                        <td style="text-align: left; padding-left: 20px; vertical-align: middle;">
                                            <asp:Button ID="btnCancelarComentGerente" runat="server" Text="Cancelar" CausesValidation="False" SkinID="boton_copia" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <asp:Label ID="lblVincularProyecto" runat="server" SkinID="etiqueta_negra"></asp:Label>
                <cc1:ModalPopupExtender ID="mpeVincularProyecto" runat="server"
                    TargetControlID="lblVincularProyecto"
                    PopupControlID="pnlVincularProyecto"
                    DropShadow="True" Enabled="True" DynamicServicePath=""
                    BackgroundCssClass="FondoAplicacion" />
                <asp:Panel ID="pnlVincularProyecto" runat="server" Style="display: none;" CssClass="CajaDialogo">
                    <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; max-width: 800px;">
                        <tr>
                            <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Actividad:
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lblActividadVincular" runat="server" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Proyecto en VITAL
                            </td>
                            <td>
                                <asp:CheckBox ID="chkVital" runat="server" ClientIDMode="Static" OnCheckedChanged="chkVital_CheckedChanged" AutoPostBack="True" EnableTheming="False" />
                            </td>
                        </tr>
                        <tr>
                            <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">
                                <asp:UpdatePanel ID="upnlProbarVinculo" runat="server">
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="chkVital" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:Label ID="lblValorProyecto" runat="server" Text="URL:" ClientIDMode="Static" SkinID="etiqueta_negra"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                            <td>
                                <asp:TextBox ID="txtUrlActividadProyeto" runat="server" Width="300px" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvUrlActividadProyeto" runat="server" ErrorMessage="Debe Ingresar una URL" ControlToValidate="txtUrlActividadProyeto" Display="Dynamic" ValidationGroup="url">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Button ID="btnProbarUrl" runat="server" Text="Probar URL" OnClientClick="ProbarURL();" OnClick="btnProbarUrl_Click" ValidationGroup="url" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                                <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                                    <tr>
                                        <td style="text-align: right; padding-right: 20px; vertical-align: middle;">
                                            <asp:Button ID="btnAceptarVincular" ClientIDMode="Static" runat="server" Text="Aceptar" OnClick="btnAceptarVincular_OnClick" SkinID="boton_copia" ValidationGroup="url" />
                                        </td>
                                        <td style="text-align: left; padding-left: 20px; vertical-align: middle;">
                                            <asp:Button ID="btnCancelarVincular" runat="server" Text="Cancelar" CausesValidation="False" SkinID="boton_copia" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <asp:Label ID="lblVinculosActividad" runat="server" SkinID="etiqueta_negra"></asp:Label>
                <cc1:ModalPopupExtender ID="mpeVinculosActividad" runat="server"
                    TargetControlID="lblVinculosActividad"
                    PopupControlID="pnlVinculosActividad"
                    DropShadow="True" Enabled="True" DynamicServicePath=""
                    BackgroundCssClass="FondoAplicacion" />
                <asp:Panel ID="pnlVinculosActividad" runat="server" Style="display: none;" CssClass="CajaDialogo">
                    <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; max-width: 800px;">
                        <tr>
                            <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">
                                <p>
                                    Actividad:
                                    <asp:Label ID="lblActividadVinculo" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">
                                <asp:GridView ID="grvVinculosProyecto" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                                        <asp:TemplateField HeaderText="Vinculo">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlVinculo" runat="server" Target="_blank" NavigateUrl='<%# String.Format("http://{0}",Eval("UrlProyecto")) %>' Text='<%# Bind("UrlProyecto") %>' CssClass="a_green"></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                                <asp:Button ID="btnCerrarVinculosActividad" runat="server" Text="Cerrar" CausesValidation="False" SkinID="boton_copia" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </cc1:TabPanel>

        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
            <HeaderTemplate>
                Visor Proceso
            </HeaderTemplate>
            <ContentTemplate>
                <%-- <asp:UpdatePanel ID="UpdVisor" runat="server">
                <ContentTemplate>--%>
                <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                    <tr>
                        <td colspan="2" style="text-align: justify">
                            <asp:Label ID="Label3" runat="server" SkinID="etiqueta_verde" Style="text-align: justify" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Número VITAL" SkinID="etiqueta_negra"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNroVitalVisor" runat="server" Width="216px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Fecha Desde (dd/mm/aaaa):" SkinID="etiqueta_negra"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaInicialVisor" runat="server" MaxLength="10" Width="80px"></asp:TextBox>
                            <cc1:CalendarExtender ID="ceFechaInicialVisor" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                TargetControlID="txtFechaInicialVisor">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Fecha Hasta (dd/mm/aaaa):" SkinID="etiqueta_negra"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaFinalVisor" runat="server" MaxLength="10" Width="80px"></asp:TextBox>
                            <cc1:CalendarExtender ID="ceFechaFinalVisor" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                TargetControlID="txtFechaFinalVisor">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Nombre del Proyecto: 
                        </td>
                        <td>
                            <asp:TextBox ID="txtNOmbreProyecto" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Autoridad Ambiental: 
                        </td>
                        <td>
                            <asp:DropDownList ID="cboAutoridadAmbiental" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Departamento:
                        </td>
                        <td>
                            <asp:DropDownList ID="cboDepartamento" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Solicitante: 
                        </td>
                        <td>
                            <asp:TextBox ID="txtSolicitante" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                            <asp:Button ID="btnConsultarVisor" runat="server" SkinID="boton_copia" Text="Buscar" OnClick="btnConsultarVisor_Click" CausesValidation="False" />
                        </td>
                    </tr>
                </table>

                <asp:DataList ID="dtlDatos" runat="server" OnItemDataBound="dtlDatos_ItemDataBound" Width="100%">
                    <ItemTemplate>
                        <table class="RegistroVisor" style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; width: 100%;">
                            <tr>
                                <td class="td1">
                                    <asp:Label ID="lblProcessInstance" EnableTheming="false" runat="server" Text='<%# Bind("IDPROCESSINSTANCE") %>' Visible="False" SkinID="etiqueta_negra"></asp:Label>
                                    <div class="NombreProyecto">
                                        <asp:Label ID="Label11" runat="server" EnableTheming="false" Text="Solicitante: " SkinID="etiqueta_negra"></asp:Label>
                                        <asp:Label ID="lblSolicitante" runat="server" EnableTheming="false" Text='<%# Bind("SOLICITANTE") %>' SkinID="etiqueta_negra"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label4" runat="server" EnableTheming="false" Text="Nombre Proyecto: " SkinID="etiqueta_negra"></asp:Label>
                                        <asp:Label ID="lblNombreProyecto" runat="server" EnableTheming="false" Text='<%# Bind("NOMBRE_PROYECTO") %>' SkinID="etiqueta_negra"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label8" runat="server" EnableTheming="false" Text="Fecha de Inicio: " SkinID="etiqueta_negra"></asp:Label>
                                        <asp:Label ID="lblFechaIni" EnableTheming="false" runat="server" Text='<%# Bind("STARTTIME","{0:dd/MM/yyyy}") %>' SkinID="etiqueta_negra"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label9" runat="server" EnableTheming="false" Text="Fecha de Terminacion: " SkinID="etiqueta_negra"></asp:Label>
                                        <asp:Label ID="Label10" EnableTheming="false" runat="server" Text='<%# Bind("FECHA_FINALIZACION_PROYECTO_CALCULADA","{0:dd/MM/yyyy}") %>' SkinID="etiqueta_negra"></asp:Label><br />
                                        <asp:Label ID="Label12" runat="server" EnableTheming="false" Text="Ubicación: " SkinID="etiqueta_negra"></asp:Label>
                                        <asp:Label ID="lblUbicacion" runat="server" EnableTheming="false" Text='<%# Bind("UBICACION") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="NombreProyecto">
                                        <asp:Label ID="lblNumeroVital" runat="server" EnableTheming="false" Text="Numero VITAL" SkinID="etiqueta_negra"></asp:Label>
                                        <asp:Label ID="lblNroVital" runat="server" EnableTheming="false" Text='<%# Bind("NUMERO_VITAL") %>' SkinID="etiqueta_negra"></asp:Label><br />
                                        Avance Esperado:
                                        <asp:Label ID="lblPorcentajeEsperado" runat="server" EnableTheming="false" SkinID="etiqueta_negra"></asp:Label><br />
                                        Avance Actual:
                                        <asp:Label ID="lblPorcentajeActual" runat="server" EnableTheming="false" SkinID="etiqueta_negra"></asp:Label><br />
                                        Dias Transcurridos:
                                        <asp:Label ID="lblDiasTranscurridos" runat="server" EnableTheming="false" Text='<%# Bind("DIAS_TRANSCURRIDOS")%>' SkinID="etiqueta_negra"></asp:Label>
                                    </div>
                                    <div class="ArchivosSolicitud">
                                        <asp:ImageButton ID="imgbArchivosSolicitud" runat="server" SkinID="Archivos" CausesValidation="false" ToolTip="Ver Archivos Solicitud" OnClick="imgbArchivosSolicitud_Click" ClientIDMode="Static"></asp:ImageButton>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc1:Accordion ID="acActividades" runat="server" SelectedIndex="0" 
                                        HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" 
                                        ContentCssClass="accordionContent" FadeTransitions="true" SuppressHeaderPostbacks="true" 
                                        TransitionDuration="250" FramesPerSecond="40" RequireOpenedPane="false" AutoSize="None">
                                        <Panes>
                                            <cc1:AccordionPane ID="AccordionPane1" runat="server">
                                                <Header><a href="#" class="href">Actividades</a></Header>
                                                <Content>
                                                    <asp:GridView ID="dtgDatos" runat="server" ClientIDMode="Static" 
                                                        AutoGenerateColumns="False" SkinID="GrillaVisor" 
                                                        OnRowDataBound="dtgDatos_RowDataBound" 
                                                        OnSelectedIndexChanged="dtgDatos_SelectedIndexChanged" 
                                                        OnRowCommand="dtgDatos_RowCommand">
                                                        <Columns>
                                                            <%--<asp:BoundField DataField="NUMERO_VITAL" HeaderText="Nro VITAL" Visible="false" />--%>
                                                            <asp:BoundField DataField="NOMBRE_TIPO_TRAMITE" HeaderText="Tramite" />
                                                            <asp:BoundField DataField="TAREA" HeaderText="Tarea" />
                                                            <asp:TemplateField HeaderText="Estado Actividad">
                                                                <ItemTemplate>
                                                                    <asp:Image ID="imgEstadoActividad" runat="server" ImageUrl="~/App_Themes/Img/BotonGris.png" Width="28" Height="27" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="FECHA_INICIO" HeaderText="Fecha Inicio" DataFormatString="{0:dd/MM/yyyy}" />
                                                            <%--<asp:BoundField DataField="FECHA_VENCIMIENTO" HeaderText="Fecha Vencimiento" />--%>
                                                            <asp:BoundField DataField="FECHA_FINALIZACION" HeaderText="Fecha Finalizacion" DataFormatString="{0:dd/MM/yyyy}" />
                                                            <asp:BoundField DataField="TIEMPO_ESTIMADO_DIAS" HeaderText="Dias estimados" />
                                                            <asp:BoundField DataField="FECHA_FINALIZACION_ACT_CALCULADA" HeaderText="Fecha Finalizacion Calculada" DataFormatString="{0:dd/MM/yyyy}" />
                                                            <asp:TemplateField HeaderText="Grupo Responsable">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGrupoResponsable" runat="server" Text='<%# Bind("GRUPO_RESPONSABLE")%>' SkinID="etiqueta_negra"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="LAST_COMMENT" HeaderText="Ultimo Comentario" />
                                                            <asp:BoundField DataField="LAST_COMMENT_GERENTE" HeaderText="Ultimo Comentario Gerente" />
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <div style="position: relative;">
                                                                        <asp:ImageButton ID="imgbComentarios" runat="server" CommandName="Select" ImageUrl="~/App_Themes/Img/Comentario.png" CausesValidation="false"></asp:ImageButton>
                                                                        <div style="position: absolute; left: 22px; top: 12px;">
                                                                            <asp:Label ID="LblTotalComment" runat="server" Text='<%# Bind("total_comments") %>' SkinID="etiqueta_negra" />
                                                                        </div>
                                                                    </div>
                                                                    <asp:Label ID="LblSatus" runat="server" Text='<%# Bind("STATUS") %>' Visible="false" SkinID="etiqueta_negra" />
                                                                    <asp:Label ID="LblIdActivityInstance" runat="server" Text='<%# Bind("IDACTIVITYINSTANCE") %>' Visible="false" SkinID="etiqueta_negra" />
                                                                    <asp:Label ID="LblDiasAVencerce" runat="server" Text='<%# Bind("DIAS_A_VENCERCE") %>' Visible="false" SkinID="etiqueta_negra"></asp:Label>
                                                                    <asp:Label ID="lblProcessInstance" runat="server" Text='<%# Bind("IDProcessInstance") %>' Visible="false" SkinID="etiqueta_negra"></asp:Label>
                                                                    <asp:Label ID="lblIdActivity" Text='<%# Bind("IDACTIVITY") %>' Visible="false" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                                                    <asp:Label ID="lblTotalVinculos" Text='<%# Bind("TOTAL_VINCULOS") %>' Visible="false" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkComentarioGerente" runat="server" Text="Agregar Comentario" OnClick="lnkComentarioGerente_Click" CausesValidation="false"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkViculosProyecto" runat="server" Text="Vinculos Proyecto" OnClick="lnkViculosProyecto_Click" CausesValidation="false"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkAcciones" runat="server" Text="Ver Acciones" ToolTip="Ver Acciones de la Actividad" CommandName="VerAcciones" CausesValidation="false" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle BackColor="White" />
                                                        <HeaderStyle BackColor="White" />
                                                    </asp:GridView>
                                                </Content>
                                            </cc1:AccordionPane>
                                        </Panes>
                                    </cc1:Accordion>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTotal" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </ItemTemplate>
                </asp:DataList>
                <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
                <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdVisor">
                    <ProgressTemplate>
                        <div id="ModalProgressContainer">
                            <div>
                                <p>Procesando...</p>
                                <p><asp:Image ID="imgUpdateProgress" runat="server" SkinId="procesando"/></p>
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>


