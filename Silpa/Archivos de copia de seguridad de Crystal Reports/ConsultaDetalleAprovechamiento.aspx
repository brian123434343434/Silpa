<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ConsultaDetalleAprovechamiento.aspx.cs" Inherits="ConsultaDetalleAprovechamiento" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/jquery.datetimepicker.css" rel="stylesheet" />
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

        .Button {
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
        
    </style>

    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="Informacion del Aprovechamiento" SkinID="titulo_principal_blanco"></asp:Label>
        &nbsp;
        <a href="#" id="hrCerrarVentana" visible="false" runat="server" onclick="window.close();return false;">Salir</a>
    </div>

    <%--<div class="div-contenido">--%>
    <div class="table-responsive">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <cc1:TabContainer ID="tbcContenedor" runat="server" Width="100%" ActiveTabIndex="0">
            <cc1:TabPanel runat="server" HeaderText="Información Aprovechamiento" ID="tabInfoGeneral">
                <ContentTemplate>
                    <div class="contact_form">
                        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                            <tr>
                                <td>
                                    <label for="cboAutoridadAmbiental" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Autoridad Ambiental:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upnlAutoridadAmbiental" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboAutoridadAmbiental" runat="server" ClientIDMode="Static" OnSelectedIndexChanged="cboAutoridadAmbiental_SelectedIndexChanged" AutoPostBack="true" Enabled="False"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="txtRelacionJuridica" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Numero del acto administrativo:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upnlNumeroActoAdministrativo" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lblNumeroActoAdministrativo" runat="server" ClientIDMode="Static" MaxLength="20" Enabled="false" SkinID="etiqueta_negra"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="txtFechaActoAdminstrativo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Fecha de expedición:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lblFechaActoAdminstrativo" ClientIDMode="Static" runat="server" Width="100px" SkinID="etiqueta_negra" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <label for="LblFechaFinalizacionActoAdministrativo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Fecha de finalización Aprovechamiento:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="LblFechaFinalizacionActoAdministrativo" runat="server" ClientIDMode="Static" MaxLength="20" Enabled="false" SkinID="etiqueta_negra"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>

                            </tr>


                            <tr>
                                <td>
                                    <label for="txtSolicitante" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Solicitante:</label></td>
                                <td>
                                    <asp:Label ID="lblSolicitante" runat="server" placeholder="Nombre Solicitante" Width="350px" Enabled="False" SkinID="etiqueta_negra" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="cboClaseRecurso" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Tipo Saldo:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboTipoAprovechamiento" runat="server" ClientIDMode="Static" Enabled="False"></asp:DropDownList>
                                        </ContentTemplate>
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
                                            <asp:DropDownList ID="cboClaseRecurso" runat="server" OnSelectedIndexChanged="cboClaseRecurso_SelectedIndexChanged" ClientIDMode="Static" AutoPostBack="true" Enabled="False"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <table runat="server" id="tblAreaTotalAut" visible="False" style="margin-left: -7px;">
                                                <tr>
                                                    <td>
                                                        <label for="txtAreaTotalAut" title="Area total autorizada (Has):" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Area total autorizada (Has):</label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAreaTotalAut" runat="server" ClientIDMode="Static" MaxLength="11" SkinID="etiqueta_negra"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td>
                                    <label for="cboClaseAprovechamiento" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Clase Aprovechamiento:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upnlClaseAprovechamiento" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboClaseAprovechamiento" runat="server" Enabled="False"></asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboClaseRecurso" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="cboFormaOtorgamiento" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Forma de Otorgamiento:</label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboFormaOtorgamiento" runat="server" Enabled="False"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvFormaOtorgamiento" Display="Dynamic" runat="server" ControlToValidate="cboFormaOtorgamiento" ValidationGroup="aprovechamiento" ErrorMessage="Aprovechamiento / Forma Otorgamiento">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="cboModoAdquisicion" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Modo de Adquisición:</label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboModoAdquisicion" runat="server" Enabled="False"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="participante" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Procedencia Legal:</label>
                                </td>
                                <td style="text-align: left; vertical-align: top;">
                                    <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                                        <tr>
                                            <td>Despartamento: </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="cboDepartamento" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboDepartamento_SelectedIndexChanged" Enabled="False"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvDepartamento" runat="server" ControlToValidate="cboDepartamento" Display="Dynamic" ValidationGroup="aprovechamiento">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Municipio: </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="cboMunicipio" runat="server" Enabled="False"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvoMunicipio" runat="server" ControlToValidate="cboMunicipio" Display="Dynamic" ValidationGroup="aprovechamiento">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Corregimiento: </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCorregimiento" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Vereda: </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblVereda" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Nombre predio u establecimiento: </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblPredio" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="fuplActoAdministrativo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Documento soporte <strong>Original</strong> de obtención de los especimenes :</label>
                                </td>
                                <td>
                                    <div style="width: 100%;">
                                        <asp:LinkButton ID="lnkArchivo" runat="server" Text="Ver Archivo" OnClick="lnkArchivo_Click"></asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="Información de los Especimenes" ID="tabEspecimenes">
                <ContentTemplate>
                    <div class="contact_form">
                        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                            <tr>
                                <td>
                                    <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; padding: 0 !important; width: 100%;">
                                        <tr>
                                            <td style="left: 0 !important; margin: 0 !important; padding: 0 !important; padding: 0 !important;">
                                                <asp:UpdatePanel ID="upnlEspecies" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="gdvEspecimenes" runat="server" AutoGenerateColumns="false" DataKeyNames="EspecieTaxonomiaID" Width="100%"
                                                            CellPadding="2" CellSpacing="1" GridLines="None" ShowFooter="True" HorizontalAlign="Center" SkinID="GrillaCoordenadas">
                                                            <HeaderStyle Font-Size="9pt" />
                                                            <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                                            <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                                            <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                            <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                            <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                            <Columns>
                                                                <asp:BoundField DataField="NombreEspecie" HeaderText="Nombre" />
                                                                <asp:BoundField DataField="ClaseProducto" HeaderText="ClaseProducto" />
                                                                <asp:BoundField DataField="TipoProducto" HeaderText="TipoProducto" />
                                                                <asp:BoundField DataField="UnidadMedida" HeaderText="Unidad Medida" />
                                                                <asp:BoundField DataField="CantidadVolumenMovilizar" HeaderText="Cnt/Volumen Movilizar" />
                                                                <asp:BoundField DataField="CantidadVolumenRemanente" HeaderText="Volumen Remanente" />
                                                                <asp:BoundField DataField="Cantidad" HeaderText="Cnt/Volumen Elaborado" />
                                                                <asp:BoundField DataField="CntVolumenMovido" HeaderText="Cnt/Volumen Movido" />
                                                                <asp:BoundField DataField="SaldoCntVolumen" HeaderText="Cnt/Volumen Disponible" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                                    <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; padding: 0 !important; width: 100%;">
                                        <tr>
                                             <td style="left: 0 !important; margin: 0 !important; padding: 0 !important; padding: 0 !important;">
                                                <asp:UpdatePanel ID="UpnTotales" runat="server">
                                                    <ContentTemplate>
                                                        <div>
                                                            <label  id="LblLstTotalTipProductUm" visible="false" runat="server" for="GrvTotalesEspecies" title="" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;width:100%">Detalle por Tipo producto y unidad de medida de los especimenes:</label>
                                                            <asp:GridView ID="GrvTotalesEspecies" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                CellPadding="2" CellSpacing="1" GridLines="None" ShowFooter="True" HorizontalAlign="Center" SkinID="GrillaCoordenadas">
                                                                <HeaderStyle Font-Size="9pt" />
                                                                <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                                                <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                                                <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                                <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                                <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="TipoProducto" HeaderText="Tipo Producto" />
                                                                    <asp:BoundField DataField="UnidadMedida" HeaderText="Unidad Medida" />
                                                                    <asp:BoundField DataField="Total" HeaderText="Cantidad / Volumen Total" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>

                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="Localización" ID="tabLocalizacion">
                <ContentTemplate>
                    <div class="contact_form">
                        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; width: 100%;">

                            <tr>
                                <td style="text-align: left; vertical-align: top;">
                                    <asp:UpdatePanel ID="upnlLocalizaciones" runat="server">
                                        <ContentTemplate>
                                            <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 1px solid #ddd !important;">

                                                <tr>
                                                    <td style="text-align: left; vertical-align: top;">
                                                        <asp:GridView ID="dgv_localizaciones" runat="server" Width="100%"
                                                            AllowPaging="True" AllowSorting="false"
                                                            AutoGenerateColumns="False"
                                                            PageSize="4" ShowFooter="True" SkinID="GrillaCoordenadas">
                                                            <HeaderStyle Font-Size="9pt" />
                                                            <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                                            <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                                            <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                            <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                            <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                            <Columns>
                                                                <asp:BoundField AccessibleHeaderText="Puntos" HeaderText="Puntos" DataField="Puntos" />
                                                                <asp:BoundField AccessibleHeaderText="Grados" HeaderText="Grados" DataField="Grados" />
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <a href="javascript:popup('ubicacion.aspx?puntos=<%# Eval("Puntos") %>');">Mapa</a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerSettings FirstPageImageUrl="~/App_Themes/Img/pagina_primera.gif" FirstPageText="Primera" LastPageImageUrl="~/App_Themes/Img/pagina_ultima.gif" LastPageText="Ultima" Mode="NumericFirstLast" NextPageImageUrl="~/App_Themes/Img/pagina_siguiente.gif" NextPageText="Siguiente" PreviousPageImageUrl="~/App_Themes/Img/pagina_anterior.gif" PreviousPageText="Anterior" />
                                                        </asp:GridView>
                                                        <br />
                                                        <asp:Label ID="lbl_sel_todos" runat="server" Visible="False" SkinID="etiqueta_negra"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </div>
    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script src="../Scripts/jquery.datetimepicker.js"></script>
    <script src="../jquery/jquery.numeric.js"></script>
    <script src="../js/SaldoAprovechamiento.js"></script>
</asp:Content>

