<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="BandejaTareas.aspx.cs" Inherits="BandejaTareas_BandejaTareas" Title="Mis Tareas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" runat="Server">
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
        .Button {
            background-color: #ddd;
        }
        .hover_row
        {
            /* #A1DCF2 - Azul Marino*/
            border: 1px solid #616161;
            background-color: #A1DCF2;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            $('.grilla td').hover(function () {
                $("td", $(this).closest("tr")).addClass("hover_row");
            }, function() {
                $("td", $(this).closest("tr")).removeClass("hover_row");
            });
        });
    </script>

    <script type="text/javascript" language="javascript" src="../js/BandejaTareas.js"></script>
    <div style="text-align: left; padding-left: 5px; margin: auto; padding-top: 0; padding-bottom: 5px; display: none;">
        <asp:ImageButton ID="imgBtnPreviousPage" runat="server" ImageUrl="~/App_Themes/Img/iconos/inicio.jpg" OnClick="imgBtnPreviousPage_Click" Visible="false" />
    </div>

    <div class="div-titulo">
        <asp:Label ID="Label1" runat="server" SkinID="titulo_principal_blanco" Text="MIS TAREAS"></asp:Label>
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="table-responsive">
        <cc1:TabContainer ID="tbcContenedor" runat="server" Width="100%" ActiveTabIndex="0">
            <cc1:TabPanel runat="server" HeaderText="Tareas sin Iniciar" ID="tabTareasSinIniciar" Width="100%">
                <ContentTemplate>
                    <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                        <tr>
                            <td colspan="2" style="padding: 10px; text-align: left; vertical-align: middle;">
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
                                <asp:Label ID="Label3" runat="server" Text="Número Expediente" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumeroExpediente" runat="server" Width="216px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Tipo Trámite" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboTipoTramite" runat="server" Width="304px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha Desde (dd/mm/aaaa):" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaInicial" runat="server" MaxLength="10" Width="80px"></asp:TextBox>
                                <cc1:CalendarExtender ID="calFechaInicial" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaInicial"></cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Fecha Hasta (dd/mm/aaaa):" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaFinal" runat="server" MaxLength="10" Width="80px"></asp:TextBox>
                                <cc1:CalendarExtender ID="calFechaFinal" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaFinal"></cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                                <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" SkinID="boton_copia" Text="Buscar" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlTareasSinIniciar" runat="server" ScrollBars="Auto" Width="100%">
                        <asp:GridView ID="gdvTareasSinIniciar" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            OnPageIndexChanging="gdvTareasSinIniciar_PageIndexChanging" SkinID="Grilla" 
                            Width="100%" OnRowDataBound="gdvTareasSinIniciar_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="NUMERO_VITAL" HeaderText="N&#250;mero VITAL" SortExpression="NUMERO_VITAL" />
                                <asp:BoundField DataField="NUMERO_EXPEDIENTE" HeaderText="N&#250;mero Expediente" SortExpression="NUMERO_EXPEDIENTE" />
                                <asp:BoundField DataField="NOMBRE_TIPO_TRAMITE" HeaderText="Tipo Tr&#225;mite" SortExpression="NOMBRE_TIPO_TRAMITE" />
                                <asp:TemplateField HeaderText="Tarea" SortExpression="TAREA">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("TAREA") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hypAutoridadAmbiental" runat="server" Text='<%# Bind("TAREA") %>' Target="_blank" CssClass="a_green">HyperLink</asp:HyperLink>
                                        <%--<a id="linkSolicitante"  href="javascript:abrirFormulario('<%# ObtenerURL(Convert.ToInt32( Eval("IDACTIVITY_INSTANCE"))) %>');" >nancy</a>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha Inicio" SortExpression="FECHA_INICIO">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("FECHA_INICIO") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("FECHA_INICIO", "{0:dd/MM/yyyy}") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="distributionmode" SortExpression="distributionmode" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("distributionmode") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblModeloDistribucion" runat="server" Text='<%# Bind("distributionmode") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="entrydatatype" SortExpression="entrydatatype" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("entrydatatype") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblEntryDataType" runat="server" Text='<%# Bind("entrydatatype") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="URLDataView" SortExpression="URLDataView" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("URLDataView") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUrlDataView" runat="server" Text='<%# Bind("URLDataView") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IDProcessInstance" SortExpression="IDProcessInstance" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("IDProcessInstance") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblProcessInstance" runat="server" Text='<%# Bind("IDProcessInstance") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EntryDataTypeProcessInstance" SortExpression="EntryDataTypeProcessInstance" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("EntryDataTypeProcessInstance") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblEntryDataTypeProcess" runat="server" Text='<%# Bind("EntryDataTypeProcessInstance") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EntryData" SortExpression="EntryData" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("EntryData") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblEntryData" runat="server" Text='<%# Bind("EntryData") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IDEntryData" SortExpression="IDEntryData" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("IDEntryData") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdEntryData" runat="server" Text='<%# Bind("IDEntryData") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IDRelated" SortExpression="IDRelated" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("IDRelated") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdRelated" runat="server" Text='<%# Bind("IDRelated") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IDACTIVITY_INSTANCE" SortExpression="IDACTIVITY_INSTANCE" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("IDACTIVITY_INSTANCE") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblActivityInstance" runat="server" Text='<%# Bind("IDACTIVITY_INSTANCE") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IDPROCESS" SortExpression="IDPROCESS" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("IDPROCESS") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdProcess" runat="server" Text='<%# Bind("IDPROCESS") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="REG_NOMBRE" HeaderText="Regional" SortExpression="REG_NOMBRE" />
                                <asp:BoundField DataField="AUTORIDAD_AMB" HeaderText="Autoridad" SortExpression="AUTORIDAD_AMB" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="Tareas Finalizadas" ID="tabTareasFinalizadas" Width="100%">
                <HeaderTemplate>Tareas Finalizadas&nbsp;</HeaderTemplate>
                <ContentTemplate>
                    <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                        <tr>
                            <td colspan="2" style="padding: 10px; text-align: left; vertical-align: middle;">
                                <asp:Label ID="lblMensaje1" runat="server" SkinID="etiqueta_verde" Style="text-align: justify" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label21" runat="server" Text="Número VITAL" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumeroVitalF" runat="server" Width="216px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label31" runat="server" Text="Número Expediente" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumeroExpedienteF" runat="server" Width="216px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label41" runat="server" Text="Tipo Trámite" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboTipoTramiteF" runat="server" Width="304px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblFechaInicial1" runat="server" Text="Fecha Desde (dd/mm/aaaa):" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaInicialF" runat="server" MaxLength="10" Width="80px"></asp:TextBox>
                                <cc1:CalendarExtender ID="calFechaInicialF" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaInicialF"></cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label51" runat="server" Text="Fecha Hasta (dd/mm/aaaa):" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaFinalF" runat="server" MaxLength="10" Width="80px"></asp:TextBox>
                                <cc1:CalendarExtender ID="calFechaFinalF" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaFinalF"></cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                                <asp:Button ID="btnBuscarFinalizadas" runat="server" OnClick="btnBuscarFinalizadas_Click"
                                    SkinID="boton_copia" Text="Buscar" />
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="gdvTareasFinalizadas" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        OnPageIndexChanging="gdvTareasFinalizadas_PageIndexChanging" SkinID="Grilla_simple"
                        Width="100%" OnRowDataBound="gdvTareasFinalizadas_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="NUMERO_VITAL" HeaderText="N&#250;mero VITAL" SortExpression="NUMERO_VITAL" />
                            <asp:BoundField DataField="NUMERO_EXPEDIENTE" HeaderText="N&#250;mero Expediente"
                                SortExpression="NUMERO_EXPEDIENTE" />
                            <asp:BoundField DataField="NOMBRE_TIPO_TRAMITE" HeaderText="Tipo Tr&#225;mite" SortExpression="NOMBRE_TIPO_TRAMITE" />
                            <asp:TemplateField HeaderText="Tarea" SortExpression="TAREA">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("TAREA") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="hypAutoridadAmbiental" runat="server" Text='<%# Bind("TAREA") %>' Target="_blank" CssClass="a_green">HyperLink</asp:HyperLink>
                                    <%--<a id="linkSolicitante"  href="javascript:abrirFormulario('<%# ObtenerURL(Convert.ToInt32( Eval("IDACTIVITY_INSTANCE"))) %>');" >nancy</a>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha Inicio" SortExpression="FECHA_INICIO">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("FECHA_INICIO") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("FECHA_INICIO", "{0:dd/MM/yyyy}") %>' SkinID="etiqueta_negra"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="distributionmode" SortExpression="distributionmode" Visible="False">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("distributionmode") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblModeloDistribucion" runat="server" Text='<%# Bind("distributionmode") %>' SkinID="etiqueta_negra"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="entrydatatype" SortExpression="entrydatatype" Visible="False">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("entrydatatype") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEntryDataType" runat="server" Text='<%# Bind("entrydatatype") %>' SkinID="etiqueta_negra"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="URLDataView" SortExpression="URLDataView" Visible="False">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("URLDataView") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblUrlDataView" runat="server" Text='<%# Bind("URLDataView") %>' SkinID="etiqueta_negra"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IDProcessInstance" SortExpression="IDProcessInstance" Visible="False">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("IDProcessInstance") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblProcessInstance" runat="server" Text='<%# Bind("IDProcessInstance") %>' SkinID="etiqueta_negra"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EntryDataTypeProcessInstance" SortExpression="EntryDataTypeProcessInstance" Visible="False">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("EntryDataTypeProcessInstance") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEntryDataTypeProcess" runat="server" Text='<%# Bind("EntryDataTypeProcessInstance") %>' SkinID="etiqueta_negra"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EntryData" SortExpression="EntryData" Visible="False">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("EntryData") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEntryData" runat="server" Text='<%# Bind("EntryData") %>' SkinID="etiqueta_negra"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IDEntryData" SortExpression="IDEntryData" Visible="False">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("IDEntryData") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdEntryData" runat="server" Text='<%# Bind("IDEntryData") %>' SkinID="etiqueta_negra"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IDRelated" SortExpression="IDRelated" Visible="False">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("IDRelated") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdRelated" runat="server" Text='<%# Bind("IDRelated") %>' SkinID="etiqueta_negra"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IDACTIVITY_INSTANCE" SortExpression="IDACTIVITY_INSTANCE" Visible="False">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("IDACTIVITY_INSTANCE") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblActivityInstance" runat="server" Text='<%# Bind("IDACTIVITY_INSTANCE") %>' SkinID="etiqueta_negra"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha Finalizaci&#243;n" SortExpression="FECHA_FINALIZACION">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("FECHA_FINALIZACION") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("FECHA_FINALIZACION", "{0:dd/MM/yyyy}") %>' SkinID="etiqueta_negra"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IDPROCESS" SortExpression="IDPROCESS" Visible="False">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("IDPROCESS") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdProcess" runat="server" Text='<%# Bind("IDPROCESS") %>' SkinID="etiqueta_negra"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="REG_NOMBRE" HeaderText="Regional" SortExpression="REG_NOMBRE" />
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </div>
		<link href="../Resources/MasterPage/css/tabs-nuevas.css" rel="stylesheet" />
</asp:Content>

