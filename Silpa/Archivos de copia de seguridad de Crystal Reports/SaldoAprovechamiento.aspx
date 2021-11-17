<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="SaldoAprovechamiento.aspx.cs" Inherits="CargueSalfo_SaldoAprovechamiento" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../js/datimepicker-master/build/jquery.datetimepicker.min.css" rel="stylesheet" type="text/css" />
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

        .TextoValidadores{
            font-size:medium;
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
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="Cargue de saldo de especímenes" SkinID="titulo_principal_blanco"></asp:Label>
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
                                            <asp:DropDownList ID="cboAutoridadAmbiental" runat="server" ClientIDMode="Static" OnSelectedIndexChanged="cboAutoridadAmbiental_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvAutoridadAmbiental" Text="*" Display="Static" runat="server" ControlToValidate="cboAutoridadAmbiental" ValidationGroup="aprovechamiento" ErrorMessage="Aprovechamiento / Autoridad Ambiental">*</asp:RequiredFieldValidator>
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
                                            <asp:TextBox ID="txtNumeroActoAdministrativo" runat="server" ClientIDMode="Static" MaxLength="20" CssClass="requerido"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvNumeroActoAdministrativo" runat="server" ControlToValidate="txtNumeroActoAdministrativo" ValidationGroup="aprovechamiento" ErrorMessage="Aprovechamiento / Fecha Acto Administrativo">*</asp:RequiredFieldValidator>
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
                                            <asp:TextBox ID="txtFechaActoAdminstrativo" ClientIDMode="Static" runat="server" Width="100px" />
                                            <%--<cc1:CalendarExtender ID="calFechaActoAdminstrativo" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaActoAdminstrativo"/>--%>
                                            <asp:RequiredFieldValidator ID="rfvFechaActoAdminstrativo" runat="server" ControlToValidate="txtFechaActoAdminstrativo" ValidationGroup="aprovechamiento" ErrorMessage="Aprovechamiento / Fecha Acto Administrativo">*</asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RVFechaExpedicion" runat="server" ErrorMessage="Información Salvoconducto->La fecha de Expedicion no puede Ser mayor a la Fecha Actual" ControlToValidate="txtFechaActoAdminstrativo" Display="Dynamic" ValidationGroup="aprovechamiento" Type="Date">*</asp:RangeValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="txtFechaFinalizacionActoAdminstrativo" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Fecha de finalización Aprovechamiento:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtFechaFinalizacionActoAdminstrativo" ClientIDMode="Static" runat="server" Width="100px" />
                                            <asp:RequiredFieldValidator ID="RfvFechaFinalizacion" runat="server" ControlToValidate="txtFechaFinalizacionActoAdminstrativo" ValidationGroup="aprovechamiento" ErrorMessage="Aprovechamiento / Fecha de Finalizacion">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="txtNombreIniciativa" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Solicitante:</label></td>
                                <td>
                                    <asp:UpdatePanel ID="upnlSolicitante1" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtSolicitante" runat="server" placeholder="Nombre Solicitante" Width="350px" Enabled="false" /><asp:HiddenField ID="hfIdSolicitante" runat="server" />
                                            <asp:RequiredFieldValidator ID="rfvSolicitante" runat="server" ControlToValidate="txtSolicitante" ValidationGroup="aprovechamiento" ErrorMessage="Información Aprovechamiento / Solicitante">*</asp:RequiredFieldValidator>
                                            <asp:LinkButton ID="lnkSolicitante" runat="server" Text="Buscar Solicitante" OnClick="lnkSolicitante_Click"></asp:LinkButton>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="lnkSeleccionarSolicitante" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="cboClaseRecurso" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Tipo Saldo:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboTipoAprovechamiento" runat="server" ClientIDMode="Static" Enabled="false"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvTipoAprovechamiento" Display="Dynamic" runat="server" ControlToValidate="cboTipoAprovechamiento" ValidationGroup="aprovechamiento" ErrorMessage="Aprovechamiento / Tipo Saldo">*</asp:RequiredFieldValidator>
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
                                            <asp:DropDownList ID="cboClaseRecurso" runat="server" OnSelectedIndexChanged="cboClaseRecurso_SelectedIndexChanged" ClientIDMode="Static" AutoPostBack="true"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvClaseRecurso" Display="Dynamic" runat="server" ControlToValidate="cboClaseRecurso" ValidationGroup="aprovechamiento" ErrorMessage="Aprovechamiento / Clase Recurso">*</asp:RequiredFieldValidator>
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
                                                        <asp:TextBox ID="txtAreaTotalAut" runat="server" ClientIDMode="Static" MaxLength="11" ></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvAreaTotalAutCero" InitialValue="0" Display="Dynamic" runat="server" ControlToValidate="txtAreaTotalAut" ValidationGroup="aprovechamiento" ErrorMessage="Aprovechamiento / Area total autorizada (Has) Debe ser Mayor a cero(0)">*</asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="rfvAreaTotalAutVacio" Display="Dynamic" runat="server" ControlToValidate="txtAreaTotalAut" ValidationGroup="aprovechamiento" ErrorMessage="Aprovechamiento / Area total autorizada (Has)">*</asp:RequiredFieldValidator>
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
                                            <asp:DropDownList ID="cboClaseAprovechamiento" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvClaseAprovechamiento" Display="Dynamic" runat="server" ControlToValidate="cboClaseAprovechamiento" ValidationGroup="aprovechamiento" ErrorMessage="Aprovechamiento / Clase Aprovechamiento">*</asp:RequiredFieldValidator>
                                            <asp:Label ID="lblNAClaseAprovechamiento" runat="server" Text="N/A" Visible="false" SkinID="etiqueta_negra"></asp:Label>
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
                                    <asp:UpdatePanel ID="upnlFormaOtorgamiento" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboFormaOtorgamiento" runat="server" OnSelectedIndexChanged="cboFormaOtorgamiento_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvFormaOtorgamiento" Display="Dynamic" runat="server" ControlToValidate="cboFormaOtorgamiento" ValidationGroup="aprovechamiento" ErrorMessage="Aprovechamiento / Forma Otorgamiento">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboClaseRecurso" />
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
                                            <asp:DropDownList ID="cboModoAdquisicion" AutoPostBack="true" runat="server" OnSelectedIndexChanged="cboModoAdquisicion_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvModoAdquisicion" Display="Dynamic" runat="server" ControlToValidate="cboModoAdquisicion" ValidationGroup="aprovechamiento" ErrorMessage="Aprovechamiento / Forma Otorgamiento">*</asp:RequiredFieldValidator>
                                            <asp:Label ID="lblNAModAdqui" runat="server" Text="N/A" SkinID="etiqueta_negra" Visible="false"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboFormaOtorgamiento" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>



                            <tr>
                                <td colspan="2">
                                    <asp:UpdatePanel ID="UpnlUbicacionArbolAislado" runat="server">
                                        <ContentTemplate>
                                            <table runat="server" id="tblUbicacionArbolAislado" visible="False" style="margin-left: -7px;">
                                                <tr>
                                                    <td>
                                                        <label for="cboUbicacionArbolAislado" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">
                                                            Ubicacion Arbol Aislado:</label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="CboUbicacionArbolAislado" runat="server" Visible="False" AutoPostBack="true" OnSelectedIndexChanged="CboUbicacionArbolAislado_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvUbicArbolAislado" runat="server" ControlToValidate="CboUbicacionArbolAislado" Display="Dynamic" Enabled="False" ErrorMessage="Aprovechamiento / Ubicacion Arbol Aislado" ValidationGroup="aprovechamiento">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboClaseRecurso" />
                                        </Triggers>
                                    </asp:UpdatePanel>
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
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="cboDepartamento" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboDepartamento_SelectedIndexChanged"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvDepartamento" runat="server" ControlToValidate="cboDepartamento" Display="Dynamic" ValidationGroup="aprovechamiento">*</asp:RequiredFieldValidator>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="cboAutoridadAmbiental" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Municipio: </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="upnlMunicipio" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="cboMunicipio" runat="server"></asp:DropDownList><asp:RequiredFieldValidator ID="rfvoMunicipio" runat="server" ControlToValidate="cboMunicipio" Display="Dynamic" ValidationGroup="aprovechamiento">*</asp:RequiredFieldValidator>
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
                                        <tr>
                                            <td>Nombre predio u establecimiento: </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="upnlPredio" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtPredio" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RFVNombrePredio" runat="server" ControlToValidate="txtPredio" ValidationGroup="aprovechamiento" ErrorMessage="Aprovechamiento / Nombre predio u establecimiento: ">*</asp:RequiredFieldValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
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
                                        <asp:UpdatePanel ID="upnlArchivo" runat="server">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnActualizar" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <cc1:AsyncFileUpload runat="server" ID="fuplActoAdministrativo" ClientIDMode="AutoID" OnClientUploadComplete="UploadAnexo" OnClientUploadStarted="AssemblyFileUpload_Started" />
                                                <asp:Label ID="lblArchivo" runat="server" Text="-" Visible="false" SkinID="etiqueta_negra"></asp:Label>
                                                <asp:HyperLink ID="lnkVerArchivo" runat="server" NavigateUrl='~/VerAnexo.ashx' Text="Ver Archivo" Visible="false" />
                                                <asp:LinkButton ID="lnkAdicionarArchivo" runat="server" Text="Modificar Archivo" OnClick="lnkAdicionarArchivo_Click" Visible="false" />
                                                <asp:LinkButton ID="lnkCancelarArchivo" runat="server" Text="Cancelar" OnClick="lnkCancelarArchivo_Click" Visible="false" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:UpdatePanel runat="server" ID="UpdChkValidaInfoAprovechamiento">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="ChkValidaInfoAprovechamiento" ClientIDMode="Static" runat="server" Style="margin-left: 10; margin-bottom: -5" />
                                            <label for="ChkValidaInfoAprovechamiento" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 100%">Certifico que la informacion basica del aprovechamiento se encuentra correctamente diligenciada</label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnActualizar" />
                                            <asp:AsyncPostBackTrigger ControlID="btnCancelar" />
                                        </Triggers>
                                    </asp:UpdatePanel>
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
                                    <label for="txtNombreEspecie" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Nombre Científico:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="upnlControlEspeice">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtNombreEspecie" runat="server" ClientIDMode="Static" Enabled="false" />
                                            &nbsp;
                                            <asp:LinkButton ID="lnkEspecie" runat="server" OnClick="lnkEspecie_Click" Text="Buscar Especie"></asp:LinkButton>
                                            <asp:HiddenField ID="hfEspcimenID" runat="server" />
                                            <asp:RequiredFieldValidator ID="rfvNombreEspecie" runat="server" ControlToValidate="txtNombreEspecie" ValidationGroup="especimen">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnAgregarEspecie" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                              <tr>
                                  <td>
                                      <label for="TxtNombreComunEspecie" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Nombre Comun:</label>
                                  </td>
                                  <td>
                                        <asp:UpdatePanel ID="UpdNombreComun" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="TxtNombreComunEspecie" runat="server" ToolTip="Graba Total de la Cantidad Aprobada a Movilizar Incluyendo el Remanente (excedente que quedó en productos)" ClientIDMode="Static"></asp:TextBox>
                                            <img src="../App_Themes/Img/Ayuda/icon-ayuda.png" alt="" title="Se debe colocar una Descripcion de la Especie" />
                                            <asp:RequiredFieldValidator ID="RFVNombreComunEspecie" runat="server" ControlToValidate="TxtNombreComunEspecie" ValidationGroup="especimen" ErrorMessage="Informacion de los Especimenes / Nombre Comun">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnAgregarEspecie" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                  </td>
                              </tr>
                            <tr>
                                <td>
                                    <label for="cboClaseProducto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Clase Producto:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upnlClaseProducto" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboClaseProducto" ClientIDMode="Static" runat="server" OnSelectedIndexChanged="cboClaseProducto_SelectedIndexChanged" AutoPostBack="true" /><asp:RequiredFieldValidator ID="rfvClaseProducto" runat="server" ControlToValidate="cboClaseProducto" ValidationGroup="especimen" ErrorMessage="Informacion de los Especimenes / Clase Producto">*</asp:RequiredFieldValidator>
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
                                            <asp:DropDownList ID="cboTipoProducto" ClientIDMode="Static" runat="server" OnSelectedIndexChanged="cboTipoProducto_SelectedIndexChanged" AutoPostBack="true" /><asp:RequiredFieldValidator ID="rfvTipoProducto" runat="server" ControlToValidate="cboTipoProducto" ValidationGroup="especimen" ErrorMessage="Informacion de los Especimenes / Tipo Producto">*</asp:RequiredFieldValidator>
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
                                            <asp:DropDownList ID="cboUnidadMedida" ClientIDMode="Static" runat="server" /><asp:RequiredFieldValidator ID="rfvUnidadMedida" runat="server" ControlToValidate="cboUnidadMedida" ValidationGroup="especimen" ErrorMessage="Informacion de los Especimenes / Unidad Medida">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboTipoProducto" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="txtCantidadAutorizado" title="Graba Total de la Cantidad Aprobada a Movilizar Incluyendo el Remanente (excedente que quedó en productos)" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Volumen Bruto / Cantidad Autorizado u Otorgado: </label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upnlCantidadText" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCantidadAutorizado" runat="server" ToolTip="Graba Total de la Cantidad Aprobada a Movilizar Incluyendo el Remanente (excedente que quedó en productos)" ClientIDMode="Static" OnBlur="NumeroALetras();" MaxLength="7"></asp:TextBox>
                                            <img src="../App_Themes/Img/Ayuda/icon-ayuda.png" alt="" title="Los decimales van separados por punto (.)" />
                                            <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" ControlToValidate="txtCantidadAutorizado" ValidationGroup="especimen" ErrorMessage="Informacion de los Especimenes / Cantidad de Productos / Especies">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnAgregarEspecie" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="lblCantidadLetras" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Cantidad en letras:</label>
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
                                    <label for="txtMovido" title="Porcentaje de desperdicio" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Porcentaje de desperdicio:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upnlPorcentajeDesperdicio" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtPorcentajeDesperdicio" ToolTip="Porcentaje de desperdicio" runat="server" ClientIDMode="Static" MaxLength="3" Width="50px" onblur="CalcularDesperdicio();"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPorcentajeDesperdicio" runat="server" ControlToValidate="txtPorcentajeDesperdicio" ValidationGroup="especimen" ErrorMessage="Informacion de los Especimenes / Porcentaje de Desperdicio">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="txtMovido" title="Graba la Cantidad / Volumen Excluyendo el Remanente (excedente que quedó en productos)" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Cantidad / Volumen Bruto Movido:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upnlMovido" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtMovido" ToolTip="Graba la Cantidad / Volumen Movido (especies que ya se han movido con aterioridad)" runat="server" ClientIDMode="Static" MaxLength="7" onblur="CalcularDesperdicio();"></asp:TextBox>
                                            <img src="../App_Themes/Img/Ayuda/icon-ayuda.png" alt="" title="Los decimales van separados por punto (.)" />
                                            <asp:RequiredFieldValidator ID="rfvMovido" runat="server" ControlToValidate="txtMovido" ValidationGroup="especimen" ErrorMessage="Informacion de los Especimenes / Cantidad / Volumen Movido">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnAgregarEspecie" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="txtCantVolRemanente" title="Graba El Valor del Remanente(excedente que quedó en productos)" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Cantidad / Volumen desperdicio:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upnlCantVolMovil" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCantVolRemanente" Enabled="false" ToolTip="Graba El Valor del Remanente(excedente que quedó en productos)" runat="server" ClientIDMode="Static" OnBlur="NumeroALetras();" MaxLength="6"></asp:TextBox>
                                            <img src="../App_Themes/Img/Ayuda/icon-ayuda.png" alt="" title="Los decimales van separados por punto (.)" />
                                            <asp:RequiredFieldValidator ID="rfvCantVolMovil" runat="server" ControlToValidate="txtCantVolRemanente" ValidationGroup="especimen" ErrorMessage="Informacion de los Especimenes / Cantidad Remanente / Volumen Movilizar">*</asp:RequiredFieldValidator>
                                            <%--<asp:CompareValidator runat="server" ID="CVCantidadMovilizar" ControlToValidate="txtCantVolRemanente" ControlToCompare="txtCantVolTotal" Operator="LessThan" Type="Double" ErrorMessage="Valor ingresado no puede ser superior al consignado en la casilla Cantidad / volumen total por Cantidad /volumen total autorizado a movilizar." ValidationGroup="especimen">*</asp:CompareValidator>--%>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnAgregarEspecie" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="txtCantidad" title="Graba la Cantidad / Volumen Excluyendo el Remanente (excedente que quedó en productos)" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Cantidad / Volumen disponible para mover:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upnlCantVolTotal" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCantVolTotal" Enabled="false" ToolTip="Graba la Cantidad / Volumen Excluyendo el Remanente(excedente que quedó en productos)" runat="server" ClientIDMode="Static" OnBlur="NumeroALetras();" MaxLength="6"></asp:TextBox>
                                            <img src="../App_Themes/Img/Ayuda/icon-ayuda.png" alt="" title="Los decimales van separados por punto (.)" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnAgregarEspecie" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>


                            <tr>
                                <td colspan="2">
                                    <asp:UpdatePanel ID="UpdDatosArbolAisladoUrbano" runat="server">
                                        <ContentTemplate>
                                            <table id="trUbicArbolAisladoUrbano" runat="server" visible="false">
                                                <tr>
                                                    <td>
                                                        <label for="TxtDiametroAlturaPecho" title="Es la medida del diámetro del fuste o tronco del árbol autorizado a algún tratamiento silvicultural, a una altura de 1,3 m. a partir del suelo. Es una medida en metros (m) y tendrá el formato de dos (2) decimales." style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Diametro Altura Pecho (metros):</label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtDiametroAlturaPecho" Enabled="False" ToolTip="Es la medida del diámetro del fuste o tronco del árbol autorizado a algún tratamiento silvicultural, a una altura de 1,3 m. a partir del suelo. Es una medida en metros (m) y tendrá el formato de dos (2) decimales." runat="server" ClientIDMode="Static"></asp:TextBox>
                                                        <img src="../App_Themes/Img/Ayuda/icon-ayuda.png" alt="" title="Es la medida del diámetro del fuste o tronco del árbol autorizado a algún tratamiento silvicultural, a una altura de 1,3 m. a partir del suelo. Es una medida en metros (m) y tendrá el formato de dos (2) decimales." />
                                                        <asp:RequiredFieldValidator ID="rfvDiametroAlturaPecho" runat="server" ControlToValidate="TxtDiametroAlturaPecho" ValidationGroup="especimen" ErrorMessage="Informacion de los Especimenes / Diametro Altura Pecho">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <label for="TxtAlturaComercial" title="Es la medida de la altura del tronco del árbol autorizado a algún tratamiento silvicultural. Es una medida en metros (m) y tiene el formato de dos (2) decimales." style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Altura Comercial (metros):</label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtAlturaComercial" Enabled="False" ToolTip="Es la medida de la altura del tronco del árbol autorizado a algún tratamiento silvicultural. Es una medida en metros (m) y tiene el formato de dos (2) decimales." runat="server" ClientIDMode="Static"></asp:TextBox>
                                                        <img src="../App_Themes/Img/Ayuda/icon-ayuda.png" alt="" title="Es la medida de la altura del tronco del árbol autorizado a algún tratamiento silvicultural. Es una medida en metros (m) y tiene el formato de dos (2) decimales." />
                                                        <asp:RequiredFieldValidator ID="RfvAlturaComercial" runat="server" ControlToValidate="TxtAlturaComercial" ValidationGroup="especimen" ErrorMessage="Informacion de los Especimenes / Altura Comercial">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label for="CboTratamientoSilvicultura" title="Es la medida de la altura del tronco del árbol autorizado a algún tratamiento silvicultural. Es una medida en metros (m) y tiene el formato de dos (2) decimales." style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Tratamiento Silvicultura:</label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="CboTratamientoSilvicultura" ClientIDMode="Static" runat="server"/>
                                                        <asp:RequiredFieldValidator ID="RfvTratamientoSilvicultura" runat="server" ControlToValidate="CboTratamientoSilvicultura" ValidationGroup="especimen" ErrorMessage="Informacion de los Especimenes / Tratamiento Silvicultura">*</asp:RequiredFieldValidator>
                                                        <img src="../App_Themes/Img/Ayuda/icon-ayuda.png" alt="" title="Corresponde a una serie de actividades de manejo del arbolado urbano." />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </td>
                            </tr>

                            <tr>
                                <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                                    <asp:Button ID="btnAgregarEspecie" runat="server" Text="Agregar Especimen" OnClick="btnAgregarEspecie_Click" ValidationGroup="especimen" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; padding: 0 !important; width: 100%;">
                                        <tr>
                                            <td style="left: 0 !important; margin: 0 !important; padding: 0 !important; padding: 0 !important;">
                                                <asp:UpdatePanel ID="upnlEspecies" runat="server">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnAgregarEspecie" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:GridView ID="gdvEspecimenes" runat="server" AutoGenerateColumns="false" DataKeyNames="EspecieTaxonomiaID" Width="100%"
                                                            CellPadding="2" CellSpacing="1" GridLines="None" ShowFooter="True" OnRowDeleting="gdvEspecimenes_RowDeleting" HorizontalAlign="Center" SkinID="GrillaCoordenadas">
                                                            <HeaderStyle Font-Size="9pt" />
                                                            <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                                            <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                                            <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                            <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                            <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                                            <Columns>
                                                                <asp:BoundField DataField="NombreEspecie" HeaderText="Nombre" />
                                                                <asp:BoundField DataField="NombreComunEspecie" HeaderText="Nombre Comun" />
                                                                <asp:BoundField DataField="ClaseProducto" HeaderText="ClaseProducto" />
                                                                <asp:BoundField DataField="TipoProducto" HeaderText="TipoProducto" />
                                                                <asp:BoundField DataField="UnidadMedida" HeaderText="Unidad Medida" />
                                                                <asp:BoundField DataField="CantidadVolumenMovilizar" HeaderText="Cnt/Volumen Movilizar" />
                                                                <asp:BoundField DataField="CantidadVolumenRemanente" HeaderText="Volumen Remanente" />
                                                                <asp:BoundField DataField="Cantidad" HeaderText="Cnt/Volumen Elaborado" />
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
                                            <td style="align-items: center">
                                                <asp:UpdatePanel ID="UpdValidaEspecies" runat="server">
                                                    <ContentTemplate>
                                                        <div runat="server" id="DivChkValidaEspecies" visible="false">
                                                            <asp:CheckBox ID="ChkValidaEspecies" ClientIDMode="Static" runat="server" Style="margin-left: 10; margin-bottom: -5" />
                                                            <label for="ChkValidaEspecies" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 100%">
                                                                Certifico que la informacion de los especimenes se encuentra correctamente diligenciada</label>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnAgregarEspecie" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnActualizar" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnCancelar" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>

                                    </table>
                                    <br /><br />
                                    <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; padding: 0 !important; width: 100%;">
                                        <tr>
                                            <td style="left: 0 !important; margin: 0 !important; padding: 0 !important; padding: 0 !important;">
                                                <asp:UpdatePanel ID="UpnTotales" runat="server">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnAgregarEspecie" />
                                                    </Triggers>
                                                    <ContentTemplate>
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
                                        <tr>
                                            <td style="align-items: center">
                                                <asp:UpdatePanel ID="UpdValidaCntEspecies" runat="server">
                                                    <ContentTemplate>
                                                        <div runat="server" id="DivChkValidaCantEspecies" visible="false">
                                                            <asp:CheckBox ID="ChkValidaCntEspecies" ClientIDMode="Static" runat="server" Style="margin-left: 10; margin-bottom: -5" />
                                                            <label for="ChkValidaCntEspecies" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 100%">
                                                                Certifico que el detalle por tipo de producto y unidad de medida de los especimenes se encuentra correctamente diligenciada</label>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnAgregarEspecie" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnActualizar" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnCancelar" />
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
                        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; width: 100%;">
                            <tr>
                                <td style="text-align: left; vertical-align: top;">
                                    <label for="lblLocalizaciones" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Detalle de localización del (los) polígono(s) de intervención en coordenadas del Sistema de Coordenadas Geográficas Magna Colombia Bogotá:</label>
                                    <img src="../App_Themes/Img/Ayuda/icon-ayuda.png" alt=""
                                        title="Las coordenadas se deben ingresar en el sistema WGS84, el formato del numero debe ser Decimal utilizando el punto como separador y el orden debe ser Latitud, Longitud separado por coma. Ejemplo 1.05987,-66.056987" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; vertical-align: top;">
                                    <asp:UpdatePanel ID="upnlLocalizaciones" runat="server">
                                        <ContentTemplate>
                                            <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 1px solid #ddd !important;">
                                                <tr>
                                                    <td style="text-align: left; vertical-align: top;">
                                                        <asp:RadioButtonList ID="rblOpcionCoordenada" runat="server" OnSelectedIndexChanged="rblOpcionCoordenada_SelectedIndexChanged" RepeatDirection="Horizontal" AutoPostBack="true">
                                                            <asp:ListItem Text="Líneales" Value="L"></asp:ListItem>
                                                            <asp:ListItem Text="Geográfica" Value="G"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        <asp:RequiredFieldValidator ID="rfvOpcionCoordenada" runat="server" ControlToValidate="rblOpcionCoordenada" Display="Dynamic" ValidationGroup="localizacion">*</asp:RequiredFieldValidator>
                                                        <asp:TextBox ID="txtCoordenadas" runat="server" Style="resize: none;"
                                                            Rows="5" TextMode="MultiLine" ValidationGroup="localizaciones" Width="100%"></asp:TextBox>
                                                        <br />
                                                        <asp:RegularExpressionValidator ID="revcoordenadas" runat="server" ControlToValidate="txtCoordenadas" Display="Dynamic" Text="Solo se aceptan caracateres númericos" ValidationExpression="^[0-9,-.gms]*$" ValidationGroup="localizacion"></asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator ID="rfvtxtCoordenadas" runat="server" ControlToValidate="txtCoordenadas" Display="Dynamic" ValidationGroup="localizacion">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left; vertical-align: top;">
                                                        <asp:GridView ID="dgv_localizaciones" runat="server" Width="100%"
                                                            AllowPaging="True" AllowSorting="false"
                                                            AutoGenerateColumns="False"
                                                            OnDataBound="dgv_localizaciones_DataBound"
                                                            OnRowDeleting="dgv_localizaciones_RowDeleting"
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
                                                                <asp:TemplateField AccessibleHeaderText="Eliminar" HeaderText="Eliminar">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imb_borrar" runat="server" CausesValidation="False" CommandName="Delete" SkinID="icoEliminar" ToolTip="Haga clic aquí para borrar el Item" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerSettings FirstPageImageUrl="~/App_Themes/Img/pagina_primera.gif" FirstPageText="Primera" LastPageImageUrl="~/App_Themes/Img/pagina_ultima.gif" LastPageText="Ultima" Mode="NumericFirstLast" NextPageImageUrl="~/App_Themes/Img/pagina_siguiente.gif" NextPageText="Siguiente" PreviousPageImageUrl="~/App_Themes/Img/pagina_anterior.gif" PreviousPageText="Anterior" />
                                                        </asp:GridView>
                                                        <br />
                                                        <asp:Label ID="lbl_sel_todos" runat="server" Visible="False" SkinID="etiqueta_negra"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 10px; text-align: left; vertical-align: middle;">
                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" ValidationGroup="localizaciones1" Visible="False" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 20px; text-align: center; vertical-align: middle;">
                                                        <asp:Button ID="btn_adicionar_localizacion" runat="server" OnClick="btn_adicionar_localizacion_Click" SkinID="boton" Text="Adicionar localización" ToolTip="Haga clic para adicionar localización" ValidationGroup="localizacion" />
                                                        <asp:Button ID="btnCancelarEdicion" runat="server" CausesValidation="false" OnClick="btnCancelarEdicion_Click" SkinID="boton" Text="Cancelar Edición" Visible="false" />
                                                    </td>
                                                </tr>
                                                <tr>
	                                                <td> 
                                                        <asp:UpdatePanel runat="server" ID="UpdChkValidaInfoLocalizacion">
                                                            <ContentTemplate>
		                                                        <asp:CheckBox ID="ChkValidaInfoLocalizacion" ClientIDMode="Static" runat="server" style="margin-left:14;margin-bottom:-5" />
                                                                <label for="ChkValidaInfoLocalizacion"  style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width:100%">Certifico que la informacion de la localizacion del aprovechamiento se encuentra correctamente diligenciada</label>
                                                            </ContentTemplate>
                                                              <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="btnActualizar" />
                                                                    <asp:AsyncPostBackTrigger ControlID="btnCancelar" />
                                                                </Triggers>
                                                        </asp:UpdatePanel>
	                                                </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="rblOpcionCoordenada" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>

        <table style="width: 100%; left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
            <tr>
                <td style="padding: 10px; text-align: left; vertical-align: middle;">
                    <asp:ValidationSummary ID="valResumenUsuario" runat="server" ValidationGroup="aprovechamiento" DisplayMode="List" ShowSummary="true" CssClass="TextoValidadores" />
                    <asp:ValidationSummary ID="ValEspecimenes" runat="server" ValidationGroup="especimen" DisplayMode="List" ShowSummary="true" CssClass="TextoValidadores" />
                    <asp:UpdatePanel ID="upnlErrorRedds" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnActualizar" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Label ID="lblErrorReds" ClientIDMode="Static" runat="server" Text="Error" SkinID="validador" Visible="false" CssClass="TextoValidadores"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="padding: 20px; text-align: center; vertical-align: middle;">
                    <asp:UpdatePanel ID="upnlAccionesBoton" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnActualizar" SkinID="boton_copia" runat="server" Text="Enviar" OnClientClick="return ValidarCheckBoxAprovechamiento();" ValidationGroup="aprovechamiento" OnClick="btnActualizar_Click" />
                            <asp:Button ID="btnCancelar" SkinID="boton_copia" runat="server" Text="Cancelar"
                                CausesValidation="False" OnClick="btnCancelar_Click" OnClientClick ="return confirm('Los datos diligenciados se perderan, desea continuar?') " />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>

        <asp:Label ID="lblSolicitante" runat="server" SkinID="etiqueta_negra"></asp:Label>
        <cc1:ModalPopupExtender ID="mpeSolicitantes" runat="server"
            TargetControlID="lblSolicitante"
            PopupControlID="pnlSolicitantes"
            DropShadow="True" Enabled="True" DynamicServicePath=""
            BackgroundCssClass="FondoAplicacion">
        </cc1:ModalPopupExtender>
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
            </asp:UpdatePanel>
            <div style="padding-top: 20px; text-align: center; vertical-align: middle; width: 100%;">
                <asp:Button ID="btnCerrarVinculosActividad" runat="server" Text="Cerrar" CausesValidation="False" SkinID="boton_copia" />
            </div>
        </asp:Panel>

        <asp:Label ID="lblMpEspecimen" runat="server" SkinID="etiqueta_negra"></asp:Label>
        <cc1:ModalPopupExtender ID="mpeEspecimen" runat="server"
            TargetControlID="lblMpEspecimen"
            PopupControlID="pnlEspecimen"
            DropShadow="True" Enabled="True" DynamicServicePath=""
            BackgroundCssClass="FondoAplicacion">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlEspecimen" runat="server" Style="display: none; max-width: 800px; max-height: 700px;" CssClass="CajaDialogo" ScrollBars="Vertical">
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
                            <columns>
                                <asp:TemplateField HeaderText = "Nombre Común">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblNombreComun" Text = '<%# Eval("NOMBRE_COMUN") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText = "Nombre Científico">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbl_nomlblNombreCientifico" Text = '<%# Eval("NOMBRE_CIENTIFICO") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText = "Seleccionar">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" id="lnkSeleccionar" CommandName="Edit" CssClass="a_orange">Seleccionar</asp:LinkButton>                 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                </columns>
                            <%--<rowstyle cssclass="texto_tablas" />
                            <pagerstyle cssclass="texto_tablas_paginador" horizontalalign="Left" />
                            <headerstyle cssclass="titulo_tablas" />
                            <alternatingrowstyle cssclass="texto_tablas_dos" />--%>
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
        </asp:Panel>

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
    </div>
    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script src="../js/datimepicker-master/build/jquery.datetimepicker.full.js"></script>
    <script src="../jquery/jquery.numeric.js"></script>
    <script src="../js/SaldoAprovechamiento.js"></script>
    
</asp:Content>

