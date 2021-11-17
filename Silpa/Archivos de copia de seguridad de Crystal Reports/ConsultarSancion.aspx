<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ConsultarSancion.aspx.cs" Inherits="RUIA_ConsultarSancion" Title="RUIA Consulta de Infracciones o Sanciones" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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

        .modalBackground
        {
	        background-color:Gray;
	        filter:alpha(opacity=70);
	        opacity:0.7;
        }

        .modalPopup
        {
	        background-color:#ffffdd;
	        border-width:3px;
	        border-style:solid;
	        border-color:Gray;
	        padding:3px;
	        width:250px;
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
    </style>

    <%--<link href="../App_Themes/skin/StyleREDDS.css" rel="stylesheet" />--%>
    <script src='<%= ResolveClientUrl("~/js/dropdownWidth.js") %>' type="text/javascript"></script>
    <asp:ScriptManager ID="scmManejador" runat="server" EnableScriptGlobalization="true"></asp:ScriptManager>

    <asp:UpdatePanel ID="uppPanelPublicaciones" runat="server">
        <ContentTemplate>
            <div class="div-titulo">
                <asp:Label ID="Label2" runat="server" Text="CONSULTA DE INFRACCIONES O SANCIONES" SkinID="titulo_principal_blanco"></asp:Label>
            </div>

            <div class="table-responsive">
                <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                    <tr>
                        <td style="text-align: left; vertical-align: top; width: 50%;">
                            <fieldset>
                                <legend>Información General</legend>
                                <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblAutoridadAmbiental" runat="server" Text="Autoridad Ambiental:" SkinID="etiqueta_negra"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboAutoridadAmbiental" runat="server" SkinID="lista_desplegable2">
                                                <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblTipoFalta" runat="server" Text="Tipo de Infracción:"
                                                SkinID="etiqueta_negra"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboTipoFalta" runat="server" SkinID="lista_desplegable2">
                                                <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSancionAplicada" runat="server" Text="Tipo de Sanción:"
                                                SkinID="etiqueta_negra"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboSancionAplicada" runat="server" SkinID="lista_desplegable2">
                                                <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNumeroExpediente" runat="server" Text="Número de Expediente:" SkinID="etiqueta_negra"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNumeroExpediente" runat="server" SkinID="texto"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="bllNumeroActo" runat="server" Text="Número de Acto que impone sanción:" SkinID="etiqueta_negra"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNumeroActo" runat="server" SkinID="texto"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNombreResponsable" runat="server" Text="Nombre de la persona o razón social sancionada:" SkinID="etiqueta_negra"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNombreResponsable" runat="server" SkinID="texto"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblNumeroDocumento" runat="server" Text="Número Documento de la persona o razón social:" SkinID="etiqueta_negra"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNumeroDocumento" runat="server" SkinID="texto"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="seccionMaestro" runat="server" visible="true">
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="Estado Sancion:" SkinID="etiqueta_negra"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboEstadoSancion" runat="server">
                                                <asp:ListItem Text="Todos" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Activo" Value="1" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Eliminados" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td style="text-align: left; vertical-align: top; width: 50%;">
                            <fieldset>
                                <legend>Lugar de Ocurrencia de los Hechos</legend>
                                <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblDepartamento" runat="server" Text="Departamento de ocurrencia:" SkinID="etiqueta_negra"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboDepartamento" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboDepartamento_SelectedIndexChanged" SkinID="lista_desplegable2">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqDepartamento" runat="server" ControlToValidate="cboDepartamento"
                                                ErrorMessage="Seleccione el departamento de ocurrencia" Display="Dynamic">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblMunicipio" runat="server" Text="Municipio de ocurrencia:" SkinID="etiqueta_negra"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboMunicipio" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboMunicipio_SelectedIndexChanged" SkinID="lista_desplegable2"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqMunicipio" runat="server" ControlToValidate="cboMunicipio"
                                                ErrorMessage="Seleccione el municipio de ocurrencia de los hechos" Display="Dynamic">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCorregimiento" runat="server" Text="Corregimiento de ocurrencia:" SkinID="etiqueta_negra"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboCorregimiento" runat="server" OnSelectedIndexChanged="cboCorregimiento_SelectedIndexChanged" SkinID="lista_desplegable2"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblVereda" runat="server" Text="Vereda de ocurrencia:" SkinID="etiqueta_negra"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboVereda" runat="server" AutoPostBack="True" SkinID="lista_desplegable2"></asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <br />
                            <fieldset>
                                <legend>Fecha de Sancion</legend>
                                <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha Desde  (dd/mm/aaaa):" SkinID="etiqueta_negra"></asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtFechaDesde" runat="server" SkinID="texto_corto" MaxLength="10"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="revFechaDesde" runat="server" ControlToValidate="txtFechaDesde"
                                                ErrorMessage="Formato de fecha desde" ValidationExpression="^\d{2}\/\d{2}\/\d{4}">*</asp:RegularExpressionValidator>
                                            <cc1:CalendarExtender ID="calFechaDesde" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaDesde"></cc1:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha Hasta  (dd/mm/aaaa):" SkinID="etiqueta_negra"></asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtFechaHasta" runat="server" SkinID="texto_corto" MaxLength="10"></asp:TextBox>
                                            <asp:CompareValidator ID="covCompararFechas" runat="server" ControlToValidate="txtFechaHasta"
                                                ErrorMessage='El valor del campo "Fecha Hasta", debe ser posterior al valor del campo "Fecha Desde".' Display="Dynamic"
                                                Type="Date" Operator="GreaterThan" ControlToCompare="txtFechaDesde">*</asp:CompareValidator>
                                            <asp:RegularExpressionValidator ID="revFechaHasta" runat="server" ControlToValidate="txtFechaHasta"
                                                ErrorMessage="Formato de fecha no valido para la fecha hasta" ValidationExpression="^\d{2}\/\d{2}\/\d{4}">*</asp:RegularExpressionValidator>
                                            <cc1:CalendarExtender ID="calFechaHasta" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaHasta"></cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: right; vertical-align: middle;">
                            <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:Label ID="Label6" runat="server" Text="Consulta de Infracciones" SkinID="titulo_principal">
                                        </asp:Label>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:ImageButton ID="btnConsultar" OnClick="btnConsultar_Click" runat="server" ToolTip="Consultar infracciones o Sanciones " Text="Buscar" SkinID="icoConsultar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:ValidationSummary ID="valResumen" runat="server"></asp:ValidationSummary>
                                        <br />
                                        <asp:Label ID="lblConsulta" runat="server" Visible="False" SkinID="etiqueta_negra">0</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr />
                            <asp:HyperLink Text="Aquí" NavigateUrl="" runat="server" ID="HlnkRUIA" Target="_blank" ToolTip="Para ingresar de click"></asp:HyperLink>
                            <hr />
                        </td>
                    </tr>
                </table>

                <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100% !important;">
                    <tr>
                        <td style="text-align: center; vertical-align: top;">
                            <asp:Panel ID="PanelBarras" runat="server" ScrollBars="Auto" Width="100%">
                                <asp:GridView ID="grdSanciones" runat="server" SkinID="Grilla" Width="100%" 
                                    DataKeyNames="SAN_ID_SANCION" AutoGenerateColumns="False" 
                                    RowStyle-HorizontalAlign="Left" CellSpacing="2" CellPadding="4" 
                                    OnRowDeleting="grdSanciones_RowDeleting" 
                                    OnRowUpdating="grdSanciones_RowUpdating"
                                    OnRowCommand="grdSanciones_RowCommand" 
                                    OnDataBound="grdSanciones_DataBound" 
                                    OnPageIndexChanging="grdReporte_PageIndexChanging"
                                    EmptyDataText="No Existen Registros de Sanciones.">
                                    <HeaderStyle Font-Size="9pt" />
                                    <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                    <RowStyle Font-Size="9pt" ForeColor="#000000" HorizontalAlign="Left" />
                                    <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                    <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                    <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                    <Columns>
                                        <asp:BoundField DataField="SAN_ID_SANCION"></asp:BoundField>
                                        <asp:BoundField DataField="AUT_NOMBRE" HeaderText="Autoridad Ambiental"></asp:BoundField>
                                        <asp:BoundField DataField="TIF_NOMBRE" HeaderText="Tipo de Falta"></asp:BoundField>
                                        <asp:BoundField DataField="OPS_NOMBRE_OPCION" HeaderText="Tipo de Sanci&#243;n"></asp:BoundField>
                                        <asp:BoundField DataField="LUGAR_OCURRENCIA" HeaderText="Lugar de Ocurrencia"></asp:BoundField>
                                        <asp:BoundField DataField="SAN_NUMERO_EXPE" HeaderText="N&#250;mero de Expediente"></asp:BoundField>
                                        <asp:BoundField DataField="SAN_NUMERO_ACTO" HeaderText="N&#250;mero de Acto que impone sanci&#243;n " Visible="false"></asp:BoundField>
                                        <asp:BoundField DataField="NOMBRES" HeaderText="Nombre de la Persona o Raz&#243;n Social "></asp:BoundField>
                                        <asp:ButtonField CommandName="VerDetalle" Text="Ver Detalle" ControlStyle-CssClass="a_green"></asp:ButtonField>
                                        <asp:ButtonField CommandName="UPDATE" Text="Modificar" ControlStyle-CssClass="a_blue"></asp:ButtonField>
                                        <asp:ButtonField CommandName="DELETE" Text="Eliminar" ControlStyle-CssClass="a_red"></asp:ButtonField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; vertical-align: middle; padding: 4px;">
                            <asp:Label ID="lblContador" runat="server" Visible="false" SkinID="etiqueta_negra10" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; vertical-align: top;">
                            <asp:Panel runat="server" ID="pnlDetalles" BackColor="White" HorizontalAlign="Center" ScrollBars="Auto" Height="600px" BorderWidth="1px" BorderColor="#292929" BorderStyle="Outset">
                                <table id="tbDetalles" runat="server" visible="false" border="1" style="margin: 0 !important; padding: 0 !important; max-width: 1000px;">
                                    <tr>
                                        <td style="text-align: center !important; vertical-align: middle !important; padding: 10px !important; font-size: 13pt !important; background-color: #ddd !important; border-bottom: 1px solid #000000 !important;">
                                            <table style="left: 0 !important; margin: 0 !important; padding: 0px !important; width: 100%;">
                                                <tr>
                                                    <th style="text-align: left; vertical-align: middle; padding-right: 5px;">Detalle de la Infracción o Sanción</th>
                                                    <td style="text-align: right; vertical-align: middle; padding-left: 10px; width: 50px;">
                                                        <asp:ImageButton ID="ImageButton2" OnClick="btnCerrar_Click" runat="server" SkinID="icoCerrar" ToolTip="Cerrar ventana" CausesValidation="False" Width="18px" Height="18px" style="cursor: pointer !important;"></asp:ImageButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DetailsView ID="dvwSancion" runat="server" Visible="False" SkinID="Detalles" Width="100%" AutoGenerateRows="False" Height="50px">
                                                <Fields>
                                                    <asp:BoundField DataField="SAN_ID_SANCION" HeaderText="C&#243;digo"></asp:BoundField>
                                                    <asp:BoundField DataField="AUT_NOMBRE" HeaderText="Autoridad Ambiental"></asp:BoundField>
                                                    <asp:BoundField DataField="TIF_NOMBRE" HeaderText="Tipo de Infracción"></asp:BoundField>
                                                    <asp:BoundField DataField="SAN_DESCRIPCION_NORMA" HeaderText="Descripción de la norma específica"></asp:BoundField>
                                                    <asp:BoundField DataField="DEP_NOMBRE" HeaderText="Departamento Ocurrencia"></asp:BoundField>
                                                    <asp:BoundField DataField="MUN_NOMBRE" HeaderText="Municipio Ocurrencia"></asp:BoundField>
                                                    <asp:BoundField DataField="COR_NOMBRE" HeaderText="Corregimiento Ocurrencia"></asp:BoundField>
                                                    <asp:BoundField DataField="VER_NOMBRE" HeaderText="Vereda Ocurrencia"></asp:BoundField>
                                                    <asp:BoundField DataField="OPS_NOMBRE_OPCION" HeaderText="Tipo de Sanción Principal"></asp:BoundField>
                                                    <asp:BoundField DataField="SANCION_ACCESORIA" HeaderText="Tipo Sanción Accesoria"></asp:BoundField>
                                                    <asp:BoundField DataField="SAN_NUMERO_EXPE" HeaderText="N&#250;mero de Expediente"></asp:BoundField>
                                                    <asp:BoundField DataField="SAN_NUMERO_ACTO" HeaderText="Número de Acto que impone sanción"></asp:BoundField>
                                                    <asp:BoundField DataField="SAN_FECHA_EXPEDICION_ACTO" DataFormatString="{0:yyyy/MM/dd}" HeaderText="Fecha de expedici&#243;n del acto administrativo"></asp:BoundField>
                                                    <asp:BoundField DataField="SAN_FECHA_EJECUTORIA_ACTO" DataFormatString="{0:yyyy/MM/dd}" HeaderText="Fecha de ejecutoria del Acto que impone sanci&#243;n"></asp:BoundField>
                                                    <asp:BoundField DataField="SAN_FECHA_EJECUCION_ACTO" DataFormatString="{0:yyyy/MM/dd}" HeaderText="Fecha de cumplimiento o ejecuci&#243;n de la sanci&#243;n"></asp:BoundField>
                                                    <asp:BoundField DataField="SAN_RAZON_SOCIAL" HeaderText="Raz&#243;n Social"></asp:BoundField>
                                                    <asp:BoundField DataField="SAN_NIT" HeaderText="Nit"></asp:BoundField>
                                                    <asp:BoundField DataField="SAN_PRIMER_NOMBRE" HeaderText="Primer Nombre"></asp:BoundField>
                                                    <asp:BoundField DataField="SAN_SEGUNDO_NOMBRE" HeaderText="Segundo Nombre"></asp:BoundField>
                                                    <asp:BoundField DataField="SAN_PRIMER_APELLIDO" HeaderText="Primer Apellido"></asp:BoundField>
                                                    <asp:BoundField DataField="SAN_SEGUNDO_APELLIDO" HeaderText="Segundo Apellido"></asp:BoundField>
                                                    <asp:BoundField DataField="TID_NOMBRE" HeaderText="Tipo de documento"></asp:BoundField>
                                                    <asp:BoundField DataField="SAN_NUMERO_IDENTIFICACION" HeaderText="N&#250;mero de Identificaci&#243;n"></asp:BoundField>
                                                    <asp:BoundField DataField="SAN_FECHA_HASTA" DataFormatString="{0:yyyy/MM/dd}" HeaderText="Fecha de Desfijaci&#243;n"></asp:BoundField>
                                                    <asp:BoundField DataField="SAN_DESCRIPCION_DESF" HeaderText="Descripción de la Desfijación de la publicación"></asp:BoundField>
                                                    <asp:BoundField DataField="SAN_OBSERVACIONES" HeaderText="Observaciones"></asp:BoundField>
                                                </Fields>
                                            </asp:DetailsView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center !important; vertical-align: middle !important; padding: 10px !important; font-size: 12pt !important; background-color: #ddd !important; border-top: 1px solid #000000 !important;">
                                            <table style="left: 0 !important; margin: 0 !important; padding: 0px !important; width: 100%;">
                                                <tr>
                                                    <td style="text-align: right; vertical-align: middle;">
                                                        <asp:Label ID="Label3" runat="server" Text="Cerrar Ventana" SkinID="titulo_principal" style="font-size: 12pt !important;"></asp:Label>
                                                    </td>
                                                    <td style="text-align: right; vertical-align: middle; width: 40px;">
                                                        <asp:ImageButton ID="ImageButton1" OnClick="btnCerrar_Click" runat="server" SkinID="icoCerrar" ToolTip="Cerrar ventana" CausesValidation="False" Width="20px" Height="20px" style="cursor: pointer !important;"></asp:ImageButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlEliminar" runat="server" Visible="False" Width="100%">
                                <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%;">
                                    <tr>
                                        <td style="text-align: left; vertical-align: middle;">
                                            <asp:Label ID="lblMotivoValidacion" runat="server" SkinID="etiqueta_negra" Text="Por favor ingrese el motivo por el cual desea eliminar la publicación:"></asp:Label>
                                            <asp:Label ID="lblId" runat="server" SkinID="etiqueta_negra" Visible="False"></asp:Label>
                                            <asp:Label ID="Label1" runat="server" SkinID="etiqueta_negra" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; vertical-align: middle;">
                                            <asp:TextBox ID="txtMotivoEliminacion" runat="server" SkinID="texto" Width="80%" ValidationGroup="eliminar" TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvMotivoEliminacion" runat="server" ErrorMessage="El motivo de eliminación de la publicación es requerido" ControlToValidate="txtMotivoEliminacion" ValidationGroup="eliminar">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center; vertical-align: middle;">
                                            <asp:Button ID="btnEliminar" OnClick="btnEliminar_Click" runat="server" SkinID="boton_copia" Text="Aceptar" ValidationGroup="eliminar"></asp:Button>
                                            <asp:Button ID="btnCancelar" OnClick="btnCancelar_Click" runat="server" SkinID="boton_copia" Text="Cancelar" ValidationGroup="eliminar" CausesValidation="False"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; vertical-align: middle;">
                                            <asp:ValidationSummary ID="vasEliminar" runat="server" ValidationGroup="eliminar" DisplayMode="SingleParagraph" ShowMessageBox="True"></asp:ValidationSummary>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>

                <div style="visibility: hidden">
                    <asp:ImageButton ID="btnAbrir" runat="server"
                        ToolTip="Abrir" />
                </div>

                <cc1:ModalPopupExtender ID="mpeDetalles" runat="server"
                    TargetControlID="btnAbrir" 
                    PopupControlID="pnlDetalles"
                    BackgroundCssClass="modalBackground"
                    DropShadow="true" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; display: none;">
        <tr>
            <td style="text-align: right; width: 100%;">
                <asp:ImageButton id="btnRegresar2" onclick="btnRegresar_Click" runat="server" SkinID="icoAtras" ToolTip="Regresar" CausesValidation="False" Visible="false"></asp:ImageButton>
            </td>
        </tr>
    </table>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uppPanelPublicaciones">
        <ProgressTemplate>  
            <div id="ModalProgressContainer">
                <div>
                    <p>Procesando...</p>
                    <%--<p><asp:Image ID="imgUpdateProgress" runat="server" SkinId="procesando"/></p>--%>
                    <p><asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/App_Themes/Img/Procesando.gif" /></p>
                </div>
            </div>                         
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>

