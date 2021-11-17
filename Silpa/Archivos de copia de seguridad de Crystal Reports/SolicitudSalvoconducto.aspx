<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/Silpa.master" AutoEventWireup="true" CodeFile="SolicitudSalvoconducto.aspx.cs" Inherits="Salvoconducto_SolicitudSalvoconducto" %>


<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/jquery.datetimepicker.css" rel="stylesheet" />
     
     <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <style type="text/css">

     .modal-background {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.5;
            z-index: 10 !important;
        }

        .modal-container {
            border: 1px outset #808080;
            background-color: White;
            padding: 0px;
            font-weight: bold;
            font-style: normal;
            width: 50%;
            padding: 10px 5px 5px 5px;
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

        .textInput {
            height: 25px;
            padding: 2px 5px;
        }
    </style>

    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="SOLICITUD DE SALVOCONDUCTO UNICO NACIONAL" SkinID="titulo_principal_blanco"></asp:Label>
        &nbsp;
        <a href="#" id="hrCerrarVentana" visible="false" runat="server" onclick="window.close();return false;">Salir</a>
    </div>

    <div class="div-contenido">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <cc1:TabContainer ID="tbcContenedor" runat="server" Width="90%"
            ActiveTabIndex="0">
            <cc1:TabPanel runat="server" HeaderText="Información de la obtención legal" ID="tabInfoGeneral">
                <ContentTemplate>
                    <div class="contact_form">
                        <table width="90%">
                            <tr>
                                <td id="Td1" style="width: 200px" runat="server">
                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="cboAutoridadAmbiental">Autoridad Ambiental:</label>
                                </td>
                                <td id="Td2" runat="server">
                                    <asp:UpdatePanel ID="upnlAutoridadEmisora" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboAutoridadAmbientalEmisora" AutoPostBack="true" class="control" runat="server" ClientIDMode="Static" OnSelectedIndexChanged="cboAutoridadAmbientalEmisora_SelectedIndexChanged"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvAutoridadAmbientalEmisora" Display="Dynamic" runat="server" ControlToValidate="cboAutoridadAmbientalEmisora" ValidationGroup="salvoconducto" ErrorMessage="Información de la obtención legal->Autoridad Ambiental Emisora">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="cboTipoSalvoconducto">Tipo:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboTipoSalvoconducto" Enabled="false" runat="server" ClientIDMode="Static" OnSelectedIndexChanged="cboTipoSalvoconducto_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvTipoSalvoconducto" Display="Dynamic" runat="server" ControlToValidate="cboTipoSalvoconducto" ValidationGroup="salvoconducto" ErrorMessage="Información de la obtención legal->Tipo Salvoconducto">*</asp:RequiredFieldValidator><asp:Label ID="LblNATipoSalvoconducto" runat="server" Text="N/A" Visible="false" SkinID="etiqueta_negra"></asp:Label><br />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboAutoridadAmbientalEmisora" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="LblClaseRecursoDescrip">Clase recurso:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upnlClaseRecursoDescrip" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboClaseRecurso" runat="server" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="cboClaseRecurso_SelectedIndexChanged"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvClaseRecurso" Display="Dynamic" runat="server" ControlToValidate="cboClaseRecurso" ValidationGroup="aprovechamiento" ErrorMessage="Información de la obtención legal -> Clase Recurso">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <table runat="server" id="tblAprovechamiento" visible="false" style="margin-left: -6px;">
                                                <tr>
                                                    <td>
                                                        <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="cboAprovechamiento">Acto administrativo:</label></td>
                                                    <td>
                                                        <asp:DropDownList ID="cboAprovechamiento" runat="server" ClientIDMode="Static"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvClaseSalvoconducto" Display="Dynamic" runat="server" ControlToValidate="cboAprovechamiento" ValidationGroup="aprovechamientoOrigen">*</asp:RequiredFieldValidator>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:Button runat="server" ID="btnAgregarAprovechamientoOrigen" Text="Agregar" OnClick="btnAgregarAprovechamientoOrigen_Click" ValidationGroup="aprovechamientoOrigen" /></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:GridView ID="grvAprovechamientoOrigen" runat="server" AutoGenerateColumns="false">
                                                            <Columns>
                                                                <asp:BoundField DataField="Detalle" HeaderText="Nro Aprovechamiento - Tipo" />
                                                                <asp:TemplateField HeaderText="Retirar">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkEliminar" runat="server" OnClick="grvAprovechamientoOrigen_lnkEliminar_Click" Text="Eliminar" CommandArgument='<%# Eval("AprovechamientoID")%>' CssClass="a_red" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table runat="server" id="tblSalvoanterior" visible="false" style="margin-left: -6px;">
                                                <tr>
                                                    <td>
                                                        <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="cboAprovechamiento">Salvoconducto Anterior:</label></td>
                                                    <td>
                                                        <asp:DropDownList ID="cboSalvoconductoAnterior" runat="server" ClientIDMode="Static"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvSalvoconductoAnterior" Display="Dynamic" runat="server" ControlToValidate="cboSalvoconductoAnterior" ValidationGroup="salvoconductoAnterior" ErrorMessage="Información de la obtención legal->Salvoconducto Anterior">*</asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:Button runat="server" ID="btnAgregarSalvoconductoAnterior" Text="Agregar" OnClick="btnAgregarSalvoconductoAnterior_Click" ValidationGroup="salvoconductoAnterior" />
                                                        <asp:LinkButton ID="lnkOtroSalvoconducto" runat="server" Text="Otro Salvoconducto" OnClick="lnkOtroSalvoconducto_Click" CssClass="a_blue"></asp:LinkButton>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:GridView ID="grvSalvoconductoAnterior" runat="server" AutoGenerateColumns="false">
                                                            <Columns>
                                                                <asp:BoundField DataField="Detalle" HeaderText="Nro Salvoconducto - Tipo" />
                                                                <asp:TemplateField HeaderText="Retirar">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkEliminar" runat="server" OnClick="grvSalvoconductoAnterior_lnkEliminar_Click" Text="Eliminar" CommandArgument='<%# Eval("SalvoconductoID")%>' CssClass="a_red" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr id="trTitular" runat="server" visible="false">
                                                    <td>
                                                        <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="txtSolicitante">Titular:</label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSolicitante" runat="server" placeholder="Nombre Titular" Width="350px" Enabled="false" /><asp:HiddenField ID="hfIdSolicitante" runat="server" />
                                                        <asp:RequiredFieldValidator ID="rfvSolicitante" runat="server" ControlToValidate="txtSolicitante" ValidationGroup="salvoconducto" ErrorMessage="Información Salvoconducto->Titular Solicitante">*</asp:RequiredFieldValidator>
                                                        <asp:LinkButton ID="lnkSolicitante" runat="server" Text="Buscar Titular" OnClick="lnkSolicitante_Click" CssClass="a_green"></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkLimpiarSolicitante" runat="server" Text="Limpiar Titular" OnClick="lnkLimpiarSolicitante_Click" CssClass="a_red"></asp:LinkButton>
                                                    </td>
                                                </tr>

                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboTipoSalvoconducto" />
                                            <asp:AsyncPostBackTrigger ControlID="cboClaseRecurso" />
                                            <asp:AsyncPostBackTrigger ControlID="cboSalvoconductoAnterior" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:UpdatePanel ID="upnlModoAduisicion" runat="server">
                                        <ContentTemplate>
                                            <asp:Label runat="server" ID="lblModoAdquisicionDescrip" SkinID="etiqueta_negra"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboAprovechamiento" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="cboFinalidadRecurso">Finalidad Movilización: </label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upnlFinalidadRecurso" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboFinalidadRecurso" runat="server"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvFinalidadRecurso" Display="Dynamic" runat="server" ControlToValidate="cboFinalidadRecurso" ValidationGroup="salvoconducto" ErrorMessage="Información de la obtención legal->Finalidad Movilización">*</asp:RequiredFieldValidator><asp:Label ID="lblNAFinalidad" runat="server" Text="N/A" Visible="false" SkinID="etiqueta_negra"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboClaseRecurso" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="cboFinalidadRecurso">Vigencia:</label>
                                </td>
                                <td>Desde:
                                    <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="fecha-calendar" ClientIDMode="Static"></asp:TextBox><asp:RequiredFieldValidator ID="rfvFechaDesde" Display="Dynamic" runat="server" ControlToValidate="txtFechaDesde" ValidationGroup="salvoconducto" ErrorMessage="Información de la obtención legal->Vigencia Desde">*</asp:RequiredFieldValidator>
                                    hasta:
                                    <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="fecha-calendar" ClientIDMode="Static"></asp:TextBox><asp:RequiredFieldValidator ID="rfvFechaHasta" Display="Dynamic" runat="server" ControlToValidate="txtFechaHasta" ValidationGroup="salvoconducto" ErrorMessage="Información de la obtención legal->Vigencia Hasta">*</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CVFecDesdeHasta" runat="server" ErrorMessage="La fecha hasta no puede ser mayor a la desde" ValidationGroup="salvoconducto" ControlToCompare="txtFechaDesde" ControlToValidate="txtFechaHasta" Operator="GreaterThanEqual" Type="Date" Display="Dynamic"></asp:CompareValidator>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>

            <cc1:TabPanel runat="server" HeaderText="Información de Especimenes" ID="tabEspecimenes">
                <ContentTemplate>
                    <asp:UpdatePanel ID="upnlLstEspeices" runat="server">
                        <ContentTemplate>
                            <div class="contact_form">
                                <table width="90%" runat="server" id="tblAgregarEspecie">
                                    <tr>
                                        <td style="width: 200px">
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="cboEspecies">Especies</label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboEspecies" runat="server" OnSelectedIndexChanged="cboEspecies_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><asp:RequiredFieldValidator runat="server" ControlToValidate="cboEspecies" ValidationGroup="especie" Display="Dynamic">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr id="trNombreEspecie" runat="server" visible="false">
                                        <td style="width: 200px">
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="LblNombreComunEspecie">Nombre Comun</label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LblNombreComunEspecie" runat="server" SkinID="etiqueta_negra"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="lblCantidadDisponible">Cantidad disponible:</label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCantidadDisponible" runat="server" EnableTheming="false" SkinID="etiqueta_negra"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Unidad medida:</label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblUnidadMedida" runat="server" SkinID="etiqueta_negra"></asp:Label><asp:HiddenField ID="hfUnidadMedidaID" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Clase de producto:</label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboClaseProducto" runat="server" OnSelectedIndexChanged="cboClaseProducto_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><asp:RequiredFieldValidator runat="server" ControlToValidate="cboClaseProducto" ValidationGroup="especie" Display="Dynamic">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Tipo producto:</label>
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="upnlTipoProducto" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="cboTipoProducto" runat="server"></asp:DropDownList><asp:RequiredFieldValidator runat="server" ControlToValidate="cboTipoProducto" ValidationGroup="especie" Display="Dynamic">*</asp:RequiredFieldValidator>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cboClaseProducto" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trDimenciones" visible="false">
                                        <td>
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Dimensiones (metros):</label>
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td runat="server" id="tdAlto">alto:
                                                        <asp:TextBox ID="txtAlto" ClientIDMode="Static" runat="server" Width="40px" MaxLength="6"></asp:TextBox></td>
                                                    <td runat="server" id="tdAncho">
                                                        <asp:Label ID="LblAncho" style="font-family: Tahoma,Verdana,Arial,Helvetica,sans-serif; font-size: 11px; color: #25627B;" Text="ancho:" Width="80px" ClientIDMode="Static" runat="server" ></asp:Label>
                                                        <asp:TextBox ID="txtAncho" ClientIDMode="Static" runat="server" Width="40px" MaxLength="6"></asp:TextBox></td>
                                                    <td runat="server" id="tdLargo">largo:
                                                        <asp:TextBox ID="txtLargo" ClientIDMode="Static" runat="server" Width="40px" MaxLength="6"></asp:TextBox></td>
                                                    <td runat="server" id="tdLongitud">Longitud:
                                                        <asp:TextBox ID="txtLongitud" ClientIDMode="Static" runat="server" Width="40px" MaxLength="6"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Cantidad:</label>
                                        </td>
                                        <td>
                                            <table style="margin-left: -3px;">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtCantidad" runat="server" ClientIDMode="Static" MaxLength="6" Width="50px"></asp:TextBox></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" ControlToValidate="txtCantidad" ValidationGroup="especie" Display="Dynamic">*</asp:RequiredFieldValidator></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Descripción:</label>
                                        </td>
                                        <td>
                                            <table style="margin-left: -3px;">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtDescripcion" runat="server" ClientIDMode="Static" Width="200px" TextMode="MultiLine" Rows="5"></asp:TextBox></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion" ValidationGroup="especie" Display="Dynamic">*</asp:RequiredFieldValidator></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Identificación:</label>
                                        </td>
                                        <td>
                                            <table style="margin-left: -3px;">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtIdentificacion" runat="server" ClientIDMode="Static" Width="200px" MaxLength="20"></asp:TextBox></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="rfvIdentificacion" runat="server" ControlToValidate="txtIdentificacion" ValidationGroup="especie" Display="Dynamic">*</asp:RequiredFieldValidator></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnAgregarEspecie" runat="server" Text="Agregar Especie" ValidationGroup="especie" OnClick="btnAgregarEspecie_Click" /></td>
                                    </tr>
                                </table>
                                <table width="90%">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grvEspecies" runat="server" AutoGenerateColumns="false" SkinID="GrillaCoordenadas" OnPageIndexChanging="grvEspecies_PageIndexChanging">
                                                <Columns>
                                                    <asp:BoundField DataField="NumeroSUNLAnterior" HeaderText="Salvoconducto Anterior" />
                                                    <asp:BoundField DataField="NombreEspecie" HeaderText="Nombre" />
                                                    <asp:BoundField DataField="NombreComunEspecie" HeaderText="Nombre Comun" />
                                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                                    <asp:BoundField DataField="ClaseProducto" HeaderText="Clase Producto" />
                                                    <asp:BoundField DataField="TipoProducto" HeaderText="Tipo Producto" />
                                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                                    <asp:BoundField DataField="UnidadMedida" HeaderText="Unidad Medida" />
                                                    <asp:BoundField DataField="Volumen" HeaderText="Volumen" />
                                                    <asp:BoundField DataField="Dimensiones" HeaderText="Dimensiones" />
                                                    <asp:BoundField DataField="Identificacion" HeaderText="Identificación" />
                                                    <asp:TemplateField HeaderText="Retirar">
                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="lnkEliminar" runat="server" OnClick="grvEspecies_lnkEliminar_Click" Text="Eliminar" CommandArgument='<%# Eval("Orden")%>' CssClass="a_red" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CantidadDisponible" HeaderText="Volumen Disponible" Visible="false" />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; padding: 0 !important; width: 100%;">
                                    <tr>
                                        <td style="left: 0 !important; margin: 0 !important; padding: 0 !important; padding: 0 !important;">
                                            <div>
                                                <label id="LblLstTotalTipProductUm" visible="false" runat="server" for="GrvTotalesEspecies" title="" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 100%">Detalle por Tipo producto y unidad de medida de los especimenes:</label>
                                                <asp:GridView ID="GrvTotalesEspecies" runat="server" AutoGenerateColumns="false" Width="100%"
                                                    CellPadding="2" CellSpacing="1" GridLines="None" ShowFooter="True" HorizontalAlign="Center" SkinID="GrillaCoordenadas">
                                                    <HeaderStyle Font-Size="9pt" />
                                                    <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                                    <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                                    <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                    <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                    <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                    <Columns>
                                                        <asp:BoundField DataField="NumeroSUNLAnterior" HeaderText="Salvoconducto Anterior" />
                                                        <asp:BoundField DataField="NombreEspecie" HeaderText="Nombre Especie" />
                                                        <asp:BoundField DataField="UnidadMedida" HeaderText="Unidad Medida" />
                                                        <asp:BoundField DataField="Total" HeaderText="Cantidad / Volumen Total" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ctl00$ContentPlaceHolder1$tbcContenedor$tabInfoGeneral$cboClaseRecurso" />
                            <asp:AsyncPostBackTrigger ControlID="ctl00$ContentPlaceHolder1$tbcContenedor$tabInfoGeneral$cboTipoSalvoconducto" />
                        </Triggers>
                    </asp:UpdatePanel>
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
                                            <asp:DropDownList ID="cboTipoRuta" runat="server" OnSelectedIndexChanged="cboTipoRuta_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvTipoRuta" runat="server" ControlToValidate="cboTipoRuta" ValidationGroup="ruta" Display="Dynamic">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="cboDepartamento">Departamento:</label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboDepartamento" runat="server" OnSelectedIndexChanged="cboDepartamento_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvDepartamento" runat="server" ControlToValidate="cboDepartamento" ValidationGroup="ruta" Display="Dynamic">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="cboMunicipio">Municipio:</label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboMunicipio" runat="server" OnSelectedIndexChanged="cboMunicipio_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvMunicipio" runat="server" ControlToValidate="cboMunicipio" ValidationGroup="ruta" Display="Dynamic">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr runat="server" id="trBarrio" visible="false">
                                        <td>
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="txtBarrio">Barrio:</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBarrio" runat="server" ClientIDMode="Static"></asp:TextBox><asp:RequiredFieldValidator ID="rfvBarrio" runat="server" ControlToValidate="txtBarrio" ValidationGroup="ruta" Display="Dynamic">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnAgregarRuta" runat="server" Text="Agregar Ruta" ValidationGroup="ruta" OnClick="btnAgregarRuta_Click" /></td>
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
            <cc1:TabPanel ID="tabTransporte" runat="server" HeaderText="Transporte">
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cboModoTransporte" ValidationGroup="transporte" Display="Dynamic" ErrorMessage="Transporte-> Modo Transporte">*</asp:RequiredFieldValidator></td>
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
                                    </tr>
                                    <tr>
                                        <td>
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="txtIdentificacionTransporte">Número de identificación del medio de transporte (placa) :</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtIdentificacionTransporte" runat="server" ClientIDMode="Static"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvIdentificacionTransporte" runat="server" ControlToValidate="txtIdentificacionTransporte" ValidationGroup="transporte" Display="Dynamic" ErrorMessage="Transporte-> Número de identificación del medio de transporte">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="txtNombreTransportador">Transportador o Empresa:</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNombreTransportador" runat="server" ClientIDMode="Static"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvNombreTransportador" runat="server" ControlToValidate="txtNombreTransportador" ValidationGroup="transporte" Display="Dynamic" ErrorMessage="Transporte-> Transportador o Empresa">*</asp:RequiredFieldValidator></td>
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
                                            <asp:Button ID="btnAgregarTransporte" runat="server" Text="Agregar Transporte" ValidationGroup="transporte" OnClick="btnAgregarTransporte_Click" /></td>
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
        <table style="width: 100%">
            <tr>
                <td align="left">
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
                <td align="left">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="especimen" DisplayMode="List" ShowSummary="true" />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:UpdatePanel ID="upnlAccionesBoton" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnActualizar" SkinID="boton_copia" runat="server" Text="Enviar" OnClick="btnActualizar_Click" ValidationGroup="salvoconducto" />
                            <asp:Button ID="btnCancelar" SkinID="boton_copia" runat="server" Text="Cancelar"
                                CausesValidation="False" />&nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>

    <asp:Label ID="lblSolicitante" runat="server"></asp:Label>
    <cc1:ModalPopupExtender ID="mpeSolicitantes" runat="server"
        TargetControlID="lblSolicitante"
        PopupControlID="pnlSolicitantes"
        DropShadow="True" Enabled="True" DynamicServicePath=""
        BackgroundCssClass="FondoAplicacion" />
    <asp:Panel ID="pnlSolicitantes" runat="server" Style="display: none;" CssClass="CajaDialogo">
        <div>
            <asp:UpdatePanel ID="upnlSolicitante" runat="server">
                <ContentTemplate>
                    <table width="400px">
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
                            <td></td>
                            <td>
                                <asp:Button ID="btnBuscarSolicitante" runat="server" Text="Buscar" SkinID="boton_copia" OnClick="btnBuscarSolicitante_Click" ValidationGroup="solicitante" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNombreSolicitante" runat="server" ClientIDMode="Static" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td style="text-align: right">
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

    <asp:Label ID="lblTitularApro" runat="server" SkinID="etiqueta_negra"></asp:Label>
    <cc1:ModalPopupExtender ID="mpeTitularApro" runat="server"
        TargetControlID="lblTitularApro"
        PopupControlID="pnlTitularApro"
        DropShadow="True" Enabled="True" DynamicServicePath=""
        BackgroundCssClass="FondoAplicacion" />
    <asp:Panel ID="pnlTitularApro" runat="server" Style="display: none;" CssClass="CajaDialogo">
        <div>
            <asp:UpdatePanel ID="upnlTitularApro" runat="server">
                <ContentTemplate>
                    <table width="400px">
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
                            <td></td>
                            <td>
                                <asp:Button ID="btnBuscarTitularApro" runat="server" Text="Buscar" SkinID="boton_copia" ValidationGroup="titularApro" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNombreTitularApro" runat="server" ClientIDMode="Static" SkinID="etiqueta_negra"></asp:Label>
                                <asp:LinkButton ID="lnkSeleccionarTitularApro" runat="server" Text="Seleccionar" Visible="false" CssClass="a_green"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="btnCerrarTitularApro" runat="server" Text="Cerrar" CausesValidation="False" SkinID="boton_copia" />
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
    <asp:Panel ID="pnlEspecimen" runat="server" Style="display: none;" CssClass="CajaDialogo">
        <div>
            <asp:UpdatePanel ID="upnlEspecimen" runat="server">
                <ContentTemplate>
                    <table width="400px">
                        <tr>
                            <td>
                                <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Nombre Cientifico:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombreComun" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNombreComun" Display="Dynamic" runat="server" ControlToValidate="txtNombreComun" ValidationGroup="Buscarespecie">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnBuscarEspecie" runat="server" Text="Buscar" SkinID="boton_copia" ValidationGroup="Buscarespecie" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="dgv_Especies" runat="server" AllowPaging="True" AllowSorting="True" EmptyDataText="No se encontraron datos"
                                    AutoGenerateColumns="False" CellPadding="2" CellSpacing="1"
                                    GridLines="None" ShowFooter="True" Width="100%" DataKeyNames="ESEPCIE_ID" SkinID="GrillaCoordenadas">
                                    <FooterStyle CssClass="texto_tablas_paginador" />
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
                                                <asp:LinkButton runat="server" ID="lnkSeleccionar" CommandName="Edit" CssClass="a_green">Seleccionar</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="texto_tablas" />
                                    <PagerStyle CssClass="texto_tablas_paginador" HorizontalAlign="Left" />
                                    <HeaderStyle CssClass="titulo_tablas" />
                                    <AlternatingRowStyle CssClass="texto_tablas_dos" />
                                    <PagerSettings FirstPageImageUrl="../images/pagina_primera.gif" FirstPageText="Primera"
                                        LastPageImageUrl="../images/pagina_ultima.gif" LastPageText="Ultima" Mode="NumericFirstLast"
                                        NextPageImageUrl="../images/pagina_siguiente.gif" NextPageText="Siguiente" PreviousPageImageUrl="../images/pagina_anterior.gif"
                                        PreviousPageText="Anterior" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Button ID="Button2" runat="server" Text="Cerrar" CausesValidation="False" SkinID="boton_copia" />
        </div>
    </asp:Panel>
    <asp:Label ID="lblOtroSalvoconducto" runat="server" SkinID="etiqueta_negra"></asp:Label>
    <cc1:ModalPopupExtender ID="mpeOtroSalvoconducto" runat="server"
        TargetControlID="lblOtroSalvoconducto"
        PopupControlID="pnlOtroSalvoconducto"
        DropShadow="True" Enabled="True" DynamicServicePath=""
        BackgroundCssClass="FondoAplicacion" />
    <asp:Panel ID="pnlOtroSalvoconducto" runat="server" Style="display: none;" CssClass="CajaDialogo">
        <div>
            <asp:UpdatePanel ID="upnlOtroSalvoconducto" runat="server">
                <ContentTemplate>
                    <table width="400px">
                        <tr>
                            <td>
                                <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Nro Salvoconducto:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOtroSalboconducto" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvOtroSalvoconducto" Display="Dynamic" runat="server" ControlToValidate="txtOtroSalboconducto" ValidationGroup="BuscarSalvoconducto">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnBuscarOtroSalvoconducto" runat="server" Text="Buscar" SkinID="boton_copia" ValidationGroup="BuscarSalvoconducto" OnClick="btnBuscarOtroSalvoconducto_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="grvOtroSalvoconducto" runat="server" AllowPaging="True" AllowSorting="True" EmptyDataText="No se encontraron datos"
                                    AutoGenerateColumns="False" CellPadding="2" CellSpacing="1"
                                    GridLines="None" ShowFooter="True" Width="100%" DataKeyNames="SalvoconductoID" SkinID="GrillaCoordenadas" OnRowEditing="grvOtroSalvoconducto_RowEditing">
                                    <FooterStyle CssClass="texto_tablas_paginador" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Tipo Salvoconducto">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblTipoSalvoconducto" Text='<%# Eval("TipoSalvoconducto") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Clase Recurso">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblClaseRecurso" Text='<%# Eval("ClaseRecurso") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Autoridad Emisora">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbl_nomlblNombreCientifico" Text='<%# Eval("AutoridadEmisora") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Seleccionar">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkSeleccionar" CommandName="Edit" CssClass="a_green">Seleccionar</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="texto_tablas" />
                                    <PagerStyle CssClass="texto_tablas_paginador" HorizontalAlign="Left" />
                                    <HeaderStyle CssClass="titulo_tablas" />
                                    <AlternatingRowStyle CssClass="texto_tablas_dos" />
                                    <PagerSettings FirstPageImageUrl="../images/pagina_primera.gif" FirstPageText="Primera"
                                        LastPageImageUrl="../images/pagina_ultima.gif" LastPageText="Ultima" Mode="NumericFirstLast"
                                        NextPageImageUrl="../images/pagina_siguiente.gif" NextPageText="Siguiente" PreviousPageImageUrl="../images/pagina_anterior.gif"
                                        PreviousPageText="Anterior" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="btnCerrarOtroSalvoconducto" runat="server" Text="Cerrar" CausesValidation="False" SkinID="boton_copia" OnClick="btnCerrarOtroSalvoconducto_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>

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

    <input type="button" runat="server" id="BtnValidaSUNLOculto" style="display:none" />
    <ajax:modalpopupextender id="mpeValidacionSUNL" runat="server" popupcontrolid="DivValidacionSUNL" targetcontrolid="BtnValidaSUNLOculto" behaviorid="mpeValidarNumeracion" backgroundcssclass="modal-background"></ajax:modalpopupextender> 
    <div id="DivValidacionSUNL" runat="server" visible="true" style="width: 500px; background-color: white">
        <div class="center" style="width: 100%">
            <asp:UpdatePanel ID="UpdPnlDivCondPpal" runat="server">
                <ContentTemplate>
                    <table border="1" style="width: 100%; background-color: white">
                        <tr>
                            <td style="text-align: center">
                                <asp:Label ID="LblTitulo" runat="server" Text="Validacion Salvocondcuto" Font-Size="Medium" SkinID="titulo_principal_blanco"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Label ID="LblTexto" runat="server" Text="Se encontraron Inconsistencias al generar el Salvoconducto, por favor Verificar los datos" SkinID="titulo_principal_blanco"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="overflow: scroll; width: 100%; height: 100%">
                                    <asp:GridView ID="GrvSaldoEspecies" AllowPaging="false" ShowFooter="false" runat="server" SkinID="GrillaCoordenadas" AutoGenerateColumns="false" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="AUTORIDAD_EMISORA" HeaderText="Autoridad Ambiental" />
                                            <asp:BoundField DataField="TIPO_SUNL_ANTERIOR" HeaderText="Tipo SUNL Anterior" />
                                            <asp:BoundField DataField="NUMERO_SUNL_ANTERIOR" HeaderText="Numero SUNL Anterior" />
                                            <asp:BoundField DataField="NOMBRE_CIENTIFICO" HeaderText="Nombre Especie" />
                                            <asp:BoundField DataField="VOLUMEN_INGRESAR" HeaderText="Cantidad ingresada" />
                                            <asp:BoundField DataField="SALDO_DISPONIBLE" HeaderText="Saldo Disponible" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div style="overflow: scroll; width: 100%; ">
                                    <asp:GridView ID="GrvValidacionGeneral" AllowPaging="false" ShowFooter="false" runat="server" SkinID="GrillaCoordenadas" AutoGenerateColumns="false" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="DESCRIPCION" HeaderText="Detalle Inconsistencia" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="Salir" runat="server" Text="Salir" OnClick="Salir_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

    </div>
    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script src="../js/datimepicker-master/build/jquery.datetimepicker.full.js"></script>
    <script src="../jquery/jquery.numeric.js"></script>
    <script src="../js/SolicitudSalvoconducto.js"></script>
</asp:Content>

