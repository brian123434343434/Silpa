<%@ Page Title="" Language="C#"  MasterPageFile="~/plantillas/SILPASinMenuFlash.master" AutoEventWireup="true" CodeFile="AsignarNumeracionSalvoconducto.aspx.cs" Inherits="Salvoconducto_AsignarNumeracionSalvoconducto"  EnableEventValidation ="true" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <script src="../jquery/jquery.js"></script>
    <link href="../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../jquery/EstiloGris/jquery-ui.js"></script>
    <script src="../jquery/jquery.numeric.js"></script>
    <link href="../App_Themes/skin/StyleREDDS.css" rel="stylesheet" />
     <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

    <style type="text/css">
        .modal-background 
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.5;
            z-index: 10 !important;
        }
        .AnchoLabel 
        {
            width: 50px;
        }
        .celdaTh
        {
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
        .celdaTdTitle
        {
            font-family: Arial, sans-serif, Verdana; 
            font-size: 9pt; 
            font-weight: bold; 
            text-align: left; 
            vertical-align: middle;
        }
        .celdaTd
        {
            font-family: Arial, sans-serif, Verdana; 
            font-size: 9pt; 
            font-weight: normal; 
            text-align: left; 
            vertical-align: middle;
        }
        .celdaTdCenter
        {
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

        .AlinearDiv
        {    
            display: table;
            width: 90%;
            border: 1px solid;
        }        

        .FormatoTexto
        {
            width:50px;
            text-align:left; 
            vertical-align: middle;
        }

    </style>

    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="ASIGNACION SERIE SALVOCONDUCTO" SkinID="titulo_principal_blanco"></asp:Label>
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
                        <asp:Literal ID="ltlTituloBuscador" runat="server" Text="CAMPOS SOLICITADOS"></asp:Literal>
                    </div>
                </div>

                <table width="100%" border="0">
                    <tr>
                        <td class="FormatoTexto">
                            <label for="cboAutoridadAmbiental" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 200px;">Autoridad Ambiental:</label>
                        </td>
                        <td colspan="5">
                            <asp:DropDownList ID="cboAutoridadAmbiental" runat="server" ClientIDMode="Static"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="rfvAutoridadAmbiental" runat="server" ControlToValidate="cboAutoridadAmbiental" ValidationGroup="GrabarSerie" ErrorMessage="Seleccione una Autoridad Ambiental">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="FormatoTexto">
                            <label for="TxtRangoDesde" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 200px;">Rango de Numeracion:</label>
                        </td>
                        <td style="width: 100px;">
                            <label for="TxtRangoDesde" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 100px;">Rango Desde:</label>
                        </td>
                        <td style="width: 100px">
                            <asp:UpdatePanel ID="upnTxtRangoDesde" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="TxtRangoDesde" runat="server" Width="70px" MaxLength="7" ClientIDMode="Static"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 100px;" class="FormatoTexto">
                            <label for="TxtRangoHasta" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 100px;">Rango Hasta:</label>
                        </td>
                        <td style="width: 70px">
                            <asp:UpdatePanel ID="upnTxtRangoHasta" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="TxtRangoHasta" runat="server" Width="70px" MaxLength="7" ClientIDMode="Static"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:Button ID="BtnValidarNumeracion" runat="server" Text="Validar" OnClick="BtnValidarNumeracion_Click" ValidationGroup="ConsultarSerie" />
                            <asp:CompareValidator Display="Dynamic" ID="CVRangosSerie" runat="server" ErrorMessage="El Rango Desde no puede ser menor al rango hasta" ValidationGroup="ConsultarSerie" ControlToCompare="TxtRangoDesde" ControlToValidate="TxtRangoHasta" Operator="GreaterThanEqual" Type="Double">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="FormatoTexto">
                            <label for="txtPjeAlertaSerie" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 200px;">Porcentaje Alerta Agotamiento Serie:</label>
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtPjeAlertaSerie" runat="server" Width="40px" MaxLength="2"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFV_txtPjeAlertaSerie" runat="server" ControlToValidate="txtPjeAlertaSerie" ErrorMessage="Debe Colocar Un Cantidad de Alerta para de Serie" ValidationGroup="GrabarSerie">*</asp:RequiredFieldValidator>
                            <asp:RangeValidator Display="Dynamic" ValidationGroup="GrabarSerie" ID="RGV_txtPjeAlertaSerie" runat="server" ErrorMessage="El pocentaje debe estar entre 1 a 100" MaximumValue="99" MinimumValue="1" ControlToValidate="txtPjeAlertaSerie" Type="Double">*</asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="FormatoTexto">
                            <label for="RFV_FUPLCrearSerie:" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 200px;">Adjuntar Archivo:</label>
                        </td>
                        <td colspan="5">
                            <asp:FileUpload ID="FUPLCrearSerie" runat="server" />
                            <%--<asp:RequiredFieldValidator Display="Dynamic" ID="RFV_FUPLCrearSerie" runat="server" ControlToValidate="FUPLCrearSerie" ErrorMessage="Debe Ingresar Un Archivo Adjunto" ValidationGroup="GrabarSerie">*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                </table>
                <div class="RowButton">
                    <div class="CellButton">
                        <asp:Button ValidationGroup="GrabarSerie" ID="BtnGrabar" runat="server" Text="Grabar" Enabled="false" OnClick="BtnGrabar_Click" ForeColor="Black" />
                        <asp:Button ID="BtnCancelar" runat="server" Text="Limpiar" OnClick="BtnCancelar_Click" />
                    </div>
                </div>

                <div class="RowButton">
                    <div class="CellButton">
                        <asp:ValidationSummary ID="VSSeriesSalvocondcuto" runat="server" ValidationGroup="GrabarSerie" DisplayMode="List" />
                    </div>
                </div>
            </div>
        </div>
    </div> 

    <input type="button" runat="server" id="BtnValidarNumeracionOculto" style="display:none" />
    <ajax:modalpopupextender id="mpeValidarNumeracion" runat="server" popupcontrolid="DivEstadosNumeracion" targetcontrolid="BtnValidarNumeracionOculto" behaviorid="mpeValidarNumeracion" backgroundcssclass="modal-background"></ajax:modalpopupextender> 
    <div id="DivEstadosNumeracion" runat="server" visible="true" style="width: 350px; background-color: white">

        <div class="center" style="width: 100%">
            <asp:UpdatePanel ID="UpdPnlDivCondPpal" runat="server">
                <ContentTemplate>
                    <table border="1" style="width: 100%; background-color: white">
                        <tr>
                            <td style="text-align: center">
                                <asp:Label ID="LblTitulo" runat="server" Text="Validacion Existencia Rangos" Font-Size="Medium" SkinID="titulo_principal_blanco"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Label ID="LblTexto" runat="server" Text="Estos Rangos ya se encuentran asignados, por favor asignar otros rangos diferentes" SkinID="etiqueta_negra"></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <div style="overflow: scroll; width: 100%; height: 100%">
                                    <asp:GridView ID="GRVListadoNumeracion" AllowPaging="false" ShowFooter="false" runat="server" SkinID="GrillaPDV" AutoGenerateColumns="false" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="Autoridad_Ambiental" HeaderText="Autoridad Ambiental" />
                                            <asp:BoundField DataField="Numeracion" HeaderText="Numeracion" />
                                            <asp:BoundField DataField="Estado" HeaderText="Estado" />
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
</asp:Content>


