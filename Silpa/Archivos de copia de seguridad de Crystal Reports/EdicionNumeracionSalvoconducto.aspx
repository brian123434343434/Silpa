<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/plantillas/SILPA.master" CodeFile="EdicionNumeracionSalvoconducto.aspx.cs" Inherits="Salvoconducto_EdicionNumeracionSalvoconducto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" runat="Server">
    <%--<script src="../jquery/jquery.js"></script>
    <link href="../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../jquery/EstiloGris/jquery-ui.js"></script>
    <script src="../jquery/jquery.numeric.js"></script>--%>
    <link href="../App_Themes/skin/StyleREDDS.css" rel="stylesheet" />

    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/jquery.datetimepicker.css" rel="stylesheet" />
    <link href="../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .modal-background {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.5;
            display: table;
            z-index: 10 !important;
        }

        .AnchoLabel {
            width: 50px;
        }

        .celdaTh {
            font-family: Arial, sans-serif, Verdana;
            font-size: 11pt;
            font-weight: bold;
            text-align: center;
            vertical-align: middle;
            background-color: #0a346f;
            padding: 6px;
            border-bottom: 2px solid black;
            color: white;
        }

        .celdaTdTitle {
            font-family: Arial, sans-serif, Verdana;
            font-size: 9pt;
            font-weight: bold;
            text-align: left;
            vertical-align: middle;
        }

        .celdaTd {
            font-family: Arial, sans-serif, Verdana;
            font-size: 9pt;
            font-weight: normal;
            text-align: left;
            vertical-align: middle;
        }

        .celdaTdCenter {
            font-family: Arial, sans-serif, Verdana;
            font-size: 9pt;
            font-weight: normal;
            text-align: center;
            vertical-align: middle;
        }

        .auto-style1 {
            height: 21px;
        }

        .auto-style2 {
            font-family: Arial, sans-serif, Verdana;
            font-size: 9pt;
            font-weight: bold;
            text-align: left;
            vertical-align: middle;
            height: 38px;
        }

        .auto-style3 {
            font-family: Arial, sans-serif, Verdana;
            font-size: 9pt;
            font-weight: normal;
            text-align: left;
            vertical-align: middle;
            height: 38px;
        }

        .AlinearDiv {
            display: table;
            width: 90%;
            border: 1px solid;
        }

        .FormatoTexto {
            width: 50px;
            text-align: left;
            vertical-align: middle;
        }
    </style>


    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="ADMINISTRACION SERIE SALVOCONDUCTO" SkinID="titulo_principal_blanco"></asp:Label>
        &nbsp;
        <a href="#" id="hrCerrarVentana" visible="false" runat="server" onclick="window.close();return false;">Salir</a>
    </div>
    <div class="div-contenido">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="contact_form">
            <div class="AlinearDiv">
                <div class="RowBuscarTitulo">
                    <div class="CellBuscarTitulo">
                        <asp:Literal ID="ltlTituloBuscador" runat="server" Text="FILTROS DE BUSQUEDA"></asp:Literal>
                    </div>
                </div>
                <table width="100%" border="0">
                    <tr>
                        <td class="FormatoTexto">
                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="cboAutoridadAmbiental">Autoridad Ambiental:</label>
                        </td>
                        <td colspan="2" class="auto-style6">
                            <asp:DropDownList ID="cboAutoridadAmbiental" runat="server" ClientIDMode="Static"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvAutoridadAmbiental" Display="Dynamic" runat="server" ControlToValidate="cboAutoridadAmbiental" ValidationGroup="ConsultarSerie" ErrorMessage="Seleccione una Autoridad Ambiental">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="FormatoTexto">
                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 200px;" for="CboEstadoSerie:">Estado:</label>
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="CboEstadoSerie" runat="server" ClientIDMode="Static"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RFVEstados" Display="Dynamic" runat="server" ControlToValidate="CboEstadoSerie" ValidationGroup="ConsultarSerie" ErrorMessage="Seleccione El Estado de La serie">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="FormatoTexto">
                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 200px;" for="TxtRangoDesde:">Rango de Numeracion:</label>
                        </td>
                        <td style="width: 320px">Rango Desde: 
                                <asp:TextBox ID="TxtRangoDesde" runat="server" Width="70px" ClientIDMode="Static" MaxLength="7"></asp:TextBox>
                            &nbsp
                                Rango Hasta: 
                                <asp:TextBox ID="TxtRangoHasta" runat="server" Width="70px" ClientIDMode="Static" MaxLength="7"></asp:TextBox>
                            <asp:CompareValidator ID="CVRangosSerie" runat="server" ErrorMessage="El Rango Desde no puede ser menor al rango hasta" ValidationGroup="ConsultarSerie" ControlToCompare="TxtRangoDesde" ControlToValidate="TxtRangoHasta" Operator="GreaterThanEqual" Type="Double">*</asp:CompareValidator>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td class="FormatoTexto">
                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 200px;" for="TxtRangoDesde:">Fecha De Generacion:</label>
                        </td>
                        <td style="width: 450px">Fecha Desde: 
                            <asp:TextBox ID="TextBoxFechaDesde" ReadOnly="true" runat="server" Width="70px" ClientIDMode="Static"></asp:TextBox>
                            &nbsp
                            Fecha Hasta: 
                            <asp:TextBox ID="TextBoxFechaHasta" ReadOnly="true" runat="server" Width="70px" ClientIDMode="Static"></asp:TextBox>
                            <asp:CompareValidator ID="CVFechas" runat="server" ErrorMessage="La Fecha Desde no puede ser menor al rango hasta" ValidationGroup="ConsultarSerie" ControlToCompare="TextBoxFechaDesde" ControlToValidate="TextBoxFechaHasta" Operator="GreaterThan" Type="Date">*</asp:CompareValidator>
                            &nbsp
                            <asp:LinkButton ID="LnkSetearFEchas" runat="server" OnClick="LnkSetearFEchas_Click" CssClass="a_blue">Limpiar fechas</asp:LinkButton>
                        </td>
                    </tr>
                </table>

                <div class="RowButton">
                    <div class="CellButton">
                        <asp:UpdatePanel ID="updDatosSerie" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="BtnConsultar" runat="server" Text="Consultar" OnClick="BtnConsultar_Click" ValidationGroup="ConsultarSerie" />
                                &nbsp
                                    <a href="AsignarNumeracionSalvoconducto.aspx" target="_blank">
                                        <input id="BtnNuevaSerie" type="button" value="Nueva Serie" class="button" />
                                    </a>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnConsultar" EventName="Click" />
                            </Triggers>

                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="div-contenido">
        <div class="contact_form">
            <div class="AlinearDiv" style="overflow: scroll">
                <asp:UpdatePanel ID="upnlseries" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GVASIGANCIONSERIES" runat="server" Width="100%" AllowSorting="True" ShowFooter="false"
                            AutoGenerateColumns="False" AllowPaging="True" OnRowDataBound="GridView1_RowDataBound" SkinID="GrillaSeguridad"
                            OnRowCommand="GridView1_RowCommand" OnRowDeleting="GVASIGANCIONSERIES_RowDeleting" EmptyDataText="No Hay Datos para Esta Consulta" OnPageIndexChanging="GVASIGANCIONSERIES_PageIndexChanging" PageSize="10">
                            <PagerSettings Mode="Numeric" Position="Bottom" PageButtonCount="10" />
                            <Columns>
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <table id="tblComandos" runat="server" border="0" cellspacing="0" cellpadding="10" style="width: 80px; border-top: solid 0px black; border-bottom: solid 0px black; background-color: #EAEAEA; text-align: center; vertical-align: top; padding: 0px;">
                                            <tr>
                                                <td style="text-align: center; vertical-align: middle; width: 50px;">
                                                    <asp:ImageButton ID="btnEditarSerie" runat="server" AlternateText="Editar Serie"
                                                        ImageUrl="~/App_Themes/Img/Edit.png" ValidationGroup="vgDTE_grid" Width="25px"
                                                        CommandName="EditarSerie" CommandArgument='<%# Eval("ID_SERIE") %>'
                                                        ToolTip="Editar Serie" />
                                                </td>
                                                <td style="text-align: center; vertical-align: middle; width: 50px;">
                                                    <asp:ImageButton ID="btnBloquearSerie" runat="server" AlternateText="Bloquear Serie"
                                                        ImageUrl="~/App_Themes/Img/Del.png" ValidationGroup="vgDTE_grid" Width="25px"
                                                        CommandName="BloquearSerie" CommandArgument='<%# Eval("ID_SERIE") %>'
                                                        ToolTip="Bloquear Serie" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Autoridad Ambiental">
                                    <ItemTemplate>
                                        <asp:Label ID="Autoridad_Ambiental" runat="server" Text='<%# Bind("AUT_AMBIENTAL") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Desde">
                                    <ItemTemplate>
                                        <asp:Label ID="SerieDesde" runat="server" Text='<%# Bind("DESDE") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Hasta">
                                    <ItemTemplate>
                                        <asp:Label ID="SerieHasta" runat="server" Text='<%# Bind("HASTA") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="% Alerta Serie">
                                    <ItemTemplate>
                                        <asp:Label ID="PjeAlerta" runat="server" Text='<%# Bind("PJE_SERIES_ALERTA") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <asp:Label ID="EstadoSerie" runat="server" Text='<%# Bind("ESTADO_SERIE") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Archivo Adjunto Creacion">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkArchivoAdjuntoCreacion" OnClick="LnkArchivoAdjuntoCreacion_Click" runat="server" CommandArgument='<%# Eval("RUTA_ARCHIVO_CREACION_SERIE") %>' CommandName="DescargarArchivo" Text='<%# Bind("NOMBRE_ARCHIVO_CREACION_SERIE") %>' AutoPostback="true" CssClass="a_orange"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Archivo Adjunto Bloqueo">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkArchivoAdjuntoBloqueo" OnClick="LnkArchivoAdjuntoBloqueo_Click" runat="server" CommandName="DescargarArchivo" Text='<%# Bind("NOMBRE_ARCHIVO_BLOQUEO_SERIE") %>' CommandArgument='<%# Eval("RUTA_ARCHIVO_BLOQUEO_SERIE") %>' CssClass="a_orange"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Motivo Bloqueo">
                                    <ItemTemplate>
                                        <asp:TextBox Style="resize: none" ID="MotivoBloqueo" TextMode="MultiLine" Rows="4" Width="100%" runat="server" Text='<%# Eval("MOTIVO_BLOQUEO") %>'
                                            ToolTip='<%# Eval("MOTIVO_BLOQUEO").ToString() %>' ReadOnly="True" BorderWidth="0"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="350px" Height="40px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Serie Actual">
                                    <ItemTemplate>
                                        <asp:Label ID="NroSerieActual" runat="server" Text='<%# Bind("NRO_SERIE_ACTUAL") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Fecha Generacion">
                                    <ItemTemplate>
                                        <asp:Label ID="LblFechaCreacion" runat="server" Text='<%# Bind("FECHA_CREACION") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnConsultar" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="RowButton">
            <div class="CellButton">
                <asp:ValidationSummary ID="VSSeriesSalvocondcuto" runat="server" ValidationGroup="ConsultarSerie" DisplayMode="List" />
            </div>
        </div>

    </div>



    <%--edicion de serie--%>
    <input type="button" runat="server" id="BtnValidarNumeracionOculto" style="display: none" />
    <cc1:ModalPopupExtender ID="mpeValidarNumeracion" runat="server" PopupControlID="DivEstadosNumeracion" TargetControlID="BtnValidarNumeracionOculto" BehaviorID="mpeValidarNumeracion" BackgroundCssClass="modal-background"></cc1:ModalPopupExtender>
    <div id="DivEstadosNumeracion" runat="server" visible="true" style="background-color: white; display: none">
        <div class="contact_form">
            <asp:UpdatePanel ID="UpdPnlEditSerie" runat="server" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <table style="text-align: center; vertical-align: top; border: 2px solid black;" border="0" cellpadding="4" cellspacing="0">
                        <tr>
                            <td colspan="3" class="celdaTh">Autoridad Ambiental:
                                <asp:Label ID="LblAutoridadAmbiental" runat="server" Text="" ForeColor="White" Font-Size="Medium"></asp:Label>
                                <asp:Label ID="LblIdSerieCreacion" runat="server" Text="" Visible="false" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="celdaTdTitle">Rango de Numeracion:</td>
                            <td class="celdaTd">
                                <table style="text-align: left; vertical-align: top;" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="celdaTdTitle" style="padding-left: 2px; padding-right: 2px;">Rango Desde:</td>
                                        <td class="celdaTd">
                                            <asp:TextBox ID="TxtRangoDesdeEdit" runat="server" Width="70px" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td class="celdaTdTitle" style="padding-left: 2px; padding-right: 2px;">Rango Hasta:</td>
                                        <td class="celdaTd">
                                            <asp:TextBox ID="TxtRangoHastaEdit" runat="server" Width="70px" ClientIDMode="Static"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="celdaTdCenter" style="width: 120px;">
                                <asp:Button ID="BtnValidarNumeracion" runat="server" Text="Validar" OnClick="BtnValidarNumeracion_Click" ValidationGroup="ConsultarSerie" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Porcentaje Alerta Agotamiento Serie:</td>
                            <td colspan="2" class="auto-style3">
                                <asp:TextBox ID="txtCntAlertaSerie" runat="server" Width="20px" MaxLength="2"></asp:TextBox>
                                <asp:RangeValidator ValidationGroup="GrabarSerie" ID="RGV_txtPjeAlertaSerie" runat="server" ErrorMessage="El porcentaje debe estar entre 1 a 100" MaximumValue="99" MinimumValue="1" ControlToValidate="txtCntAlertaSerie" Type="Double">*</asp:RangeValidator>
                                <asp:Label ID="LblTotalSerie" runat="server" Text="0" Visible="false" SkinID="etiqueta_negra"></asp:Label>
                                &nbsp
                            </td>
                        </tr>
                        <tr>
                            <td class="celdaTdTitle" style="padding-bottom: 10px;">Adjuntar Archivo:</td>
                            <td colspan="2" class="celdaTd" style="padding-bottom: 10px;">
                                <asp:FileUpload ID="FUPLCrearSerie" runat="server" />
                                <%--                                <cc1:AsyncFileUpload runat="server" ID="FUPLCrearSerie" ClientIDMode="AutoID" OnClientUploadComplete="UploadAnexo" OnClientUploadStarted="AssemblyFileUpload_Started" />&nbsp
                                <asp:Label ID="lblArchivo" runat="server" Text="-" Visible="false"></asp:Label>
                                <asp:HyperLink ID="lnkVerArchivo" runat="server" NavigateUrl='~/VerAnexo.ashx' Text="Ver Archivo" Visible="false" />
                                <asp:LinkButton ID="lnkAdicionarArchivo" runat="server" Text="Modificar Archivo" OnClick="lnkAdicionarArchivo_Click" Visible="false" />
                                <asp:LinkButton ID="lnkCancelarArchivo" runat="server" Text="Cancelar" OnClick="lnkCancelarArchivo_Click" Visible="false" />--%>
                                &nbsp
                                <asp:Label ID="LblValidacionArchivoCreacion" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" class="auto-style1">
                                <asp:RequiredFieldValidator ID="RFVTxtRangoHastaEdit" runat="server" ValidationGroup="GrabarSerie" ErrorMessage="Debe Registrar Rango Hasta" ControlToValidate="TxtRangoHastaEdit">*</asp:RequiredFieldValidator>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="celdaTdCenter" colspan="3" style="border-top: 2px solid black; padding: 10px; background-color: #CCCCCC;">
                                <asp:Button ValidationGroup="GrabarSerie" ID="BtnGrabar" runat="server" Text="Grabar" Enabled="false" OnClick="BtnActualizar_Click" ForeColor="White" OnClientClick="javascript:document.forms[0].encoding ='multipart/form-data';" Style="height: 28px" />
                                &nbsp&nbsp&nbsp
                                <asp:Button ID="BtnCancelarEdicion" runat="server" Text="Cancelar" OnClick="BtnCancelarEdicion_Click" Style="height: 28px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:ValidationSummary ID="VsModificarSerie" runat="server" ValidationGroup="GrabarSerie" DisplayMode="List" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="BtnGrabar" />
                </Triggers>

            </asp:UpdatePanel>
        </div>
    </div>


    <input type="button" runat="server" id="BtnBloquearSerieOculto" style="display: none" />
    <cc1:ModalPopupExtender ID="MpeBloqueoSerie" runat="server" PopupControlID="DivBloqueoSerie" TargetControlID="BtnBloquearSerieOculto" BehaviorID="MpeBloqueoSerie" BackgroundCssClass="modal-background"></cc1:ModalPopupExtender>
    <div id="DivBloqueoSerie" runat="server" visible="true" style="background-color: white; display: none">
        <div class="contact_form">
            <asp:UpdatePanel ID="UpdPnlBloqueoSerie" runat="server">
                <ContentTemplate>
                    <table style="text-align: center; vertical-align: top; border: 2px solid black;" border="0" cellpadding="3" cellspacing="0">
                        <tr>
                            <td colspan="3" class="celdaTh">Autoridad Ambiental:
                                <asp:Label ID="LblAutAmbientalBloqueo" runat="server" Text="" ForeColor="White" Font-Size="Medium"></asp:Label>
                                &nbsp
                                Bloqueo Serie:
                                <asp:Label ID="LblSerieBloquear" runat="server" Text="" ForeColor="White" Font-Size="Medium"></asp:Label>
                                &nbsp
                                <asp:Label ID="LblIdSerieBloqueo" runat="server" Text="" Visible="false" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TxtTextoBloqueo" runat="server" TextMode="MultiLine" MaxLength="1000" Rows="20" Width="700px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Adjuntar Archivo:
                                <%--<cc1:AsyncFileUpload runat="server" ID="FUPLCargarAdjuntoBloqueo" ClientIDMode="AutoID" OnClientUploadComplete="UploadAnexo" OnClientUploadStarted="AssemblyFileUpload_Started" />&nbsp--%>
                                <asp:FileUpload ID="FUPLCargarAdjuntoBloqueo" runat="server" />

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RequiredFieldValidator ID="RFVFUPLCargarAdjuntoBloqueo" runat="server" ErrorMessage="Debe Seleccionar un Archivo Adjunto" ControlToValidate="FUPLCargarAdjuntoBloqueo" ValidationGroup="BloquearSerie"></asp:RequiredFieldValidator>
                                <br />
                                <asp:RequiredFieldValidator ID="RFVTxtTextoBloqueo" runat="server" ErrorMessage="Debe Ingresar Un Motivo de Bloqueo" ControlToValidate="TxtTextoBloqueo" ValidationGroup="BloquearSerie"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="LblValidacionArchivoBloqueo" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="celdaTdCenter" colspan="3" style="border-top: 2px solid black; padding: 10px; background-color: #CCCCCC;">
                                <asp:Button ID="BtnBloquearSerie" ValidationGroup="BloquearSerie" runat="server" Text="Bloquear" OnClick="BtnBloquearSerie_Click" Style="height: 28px" OnClientClick="javascript:document.forms[0].encoding ='multipart/form-data';" />
                                &nbsp&nbsp&nbsp
                                <asp:Button ID="BtnCancelarBloqueo" runat="server" Text="Cancelar" OnClick="BtnCancelarBloqueo_Click" Style="height: 28px" />
                            </td>
                        </tr>
                    </table>

                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="BtnBloquearSerie" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

    <asp:UpdateProgress ID="UpdPConsultarSerie" runat="server" AssociatedUpdatePanelID="updDatosSerie">
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

    <%--creacion pantalla para editar los salvoconductos--%>
    
    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script src="../Scripts/jquery.datetimepicker.js"></script>
    <script src="../jquery/jquery.numeric.js"></script>
    <script src="../js/SeriesSalvoConducto.js"></script>

</asp:Content>
