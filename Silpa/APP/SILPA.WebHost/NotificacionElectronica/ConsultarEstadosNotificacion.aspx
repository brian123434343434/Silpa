<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPASinMenuFlash.master" AutoEventWireup="true" CodeFile="ConsultarEstadosNotificacion.aspx.cs" Inherits="NotificacionElectronica_EmitirDocumento" Title="Proceso de Notificación" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceHolder" runat="Server">
    <script src='<%= ResolveClientUrl("~/js/dropdownWidth.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/js/basicos.js") %>' type="text/javascript"></script>
    <script src="../jquery/jquery.js" type="text/javascript"></script>
    <link href="../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../jquery/EstiloGris/jquery-ui.js"></script>

    <link href="../Content/jquery.datetimepicker.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
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

        .divWaiting {
            background-color: Gray;
            /*background-color: #FAFAFA;*/
            filter: alpha(opacity=70);
            /*opacity:0.7;*/
            position: absolute;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            text-align: center;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            padding-top: 20%;
        }
        .xdsoft_datetimepicker {
            z-index: 999999;
        }
    </style>


    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="table-responsive">
        <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%;">
            <tbody>
                <tr>
                    <td colspan="4">
                        <div class="div-titulo">
                            <asp:Label ID="lbl_titulo_principal" runat="server" Text="ESTADOS DE NOTIFICACION POR ACTO ADMINISTRATIVO" SkinID="titulo_principal_blanco"></asp:Label>
                            <!--&nbsp;-->
                            <!--<a href="#" onclick="window.close();return false;">Salir</a>-->
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="titleUpdate">
                        <asp:Label ID="lblId" TabIndex="1" runat="server" Visible="False" meta:resourcekey="lblIdResource1" SkinID="normal" Text="-1"></asp:Label>
                    </td>
                </tr>
                <%--<tr>
                    <td colspan="2" style="text-align: left; vertical-align: top; padding: 0;">
                        <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%;">
                        </table>
                    </td>
                </tr>--%>
                <tr>
                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                        <asp:Label ID="lblNumeroVital" runat="server" Text="Número VITAL:" meta:resourcekey="lblNumeroVitalResource1"
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                        <asp:TextBox ID="txtNumeroVital" runat="server" OnTextChanged="txtNumeroVital_TextChanged"
                            meta:resourcekey="txtNumeroVitalResource1" SkinID="texto_corto"></asp:TextBox><asp:RequiredFieldValidator
                                ID="rfvNumeroVital" runat="server" ControlToValidate="txtNumeroVital" ErrorMessage="Ingrese el Número VITAL"
                                Enabled="False" meta:resourcekey="rfvNumeroVitalResource1" Text="*"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                        <asp:Label ID="lblExpediente" runat="server" Text="Número de Expediente:" meta:resourcekey="lblExpedienteResource1"
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                        <asp:TextBox ID="txtExpediente" runat="server" OnTextChanged="txtNumeroVital_TextChanged"
                            meta:resourcekey="txtExpedienteResource1" SkinID="texto_corto"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                        <asp:Label ID="Label3" runat="server" Text="Identificación del Usuario:" meta:resourcekey="Label3Resource1"
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                        <asp:TextBox ID="txtIdentificacionUsuario" runat="server" meta:resourcekey="txtIdentificacionUsuarioResource1"
                            SkinID="texto_corto"></asp:TextBox>
                    </td>
                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                        <asp:Label ID="Label2" runat="server" Text="Usuario a Notificar:" meta:resourcekey="Label2Resource1"
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                        <asp:TextBox ID="txtUsuarioNotificar" runat="server" meta:resourcekey="txtUsuarioNotificarResource1"
                            SkinID="texto_corto"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                        <asp:Label ID="lblNumeroActo" runat="server" Text="Número Acto Administrativo:" meta:resourcekey="lblNumeroActoResource1"
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                        <asp:TextBox ID="txtNumeroActo" runat="server" meta:resourcekey="txtNumeroActoResource1"
                            SkinID="texto_corto"></asp:TextBox>
                    </td>
                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                        <asp:Label ID="Label1" runat="server" Text="Tipo de Acto Administrativo:" meta:resourcekey="Label1Resource1"
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                        <asp:DropDownList ID="ddlTipoActo" runat="server" SkinID="lista_desplegable2" meta:resourcekey="ddlTipoActoResource1">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                        <asp:Label ID="Label6" runat="server" Text="Dias vencimiento:" meta:resourcekey="Label6Resource1"
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                        <asp:TextBox ID="txtDiasVenimiento" runat="server" meta:resourcekey="txtDiasVenimientoResource1"
                            SkinID="texto_corto"></asp:TextBox>
                    </td>
                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                        <asp:Label ID="Label4" runat="server" Text="Proviene del sistema de notificación:"
                            meta:resourcekey="Label4Resource1" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                        <asp:DropDownList ID="ddlProvienePDI" runat="server" SkinID="lista_desplegable2">
                            <asp:ListItem Selected="True" Value="-1" meta:resourcekey="ListItemResource3">Seleccione...</asp:ListItem>
                            <asp:ListItem Value="1" meta:resourcekey="ListItemResource2" Text="Si"></asp:ListItem>
                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource1" Text="No"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                        <asp:Label ID="Label5" runat="server" Text="ID del proceso de Notificación:" meta:resourcekey="Label5Resource1"
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                        <asp:TextBox ID="txtProcesoNotificacion" runat="server" meta:resourcekey="txtProcesoNotificacionResource1"
                            SkinID="texto_corto"></asp:TextBox>
                    </td>
                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                        <asp:Label ID="Label7" runat="server" Text="Estado Notificación:" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                        <asp:DropDownList ID="cboEstadoNotificacion" runat="server" SkinID="lista_desplegable2">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                        <asp:Label ID="TxtlblEstadoEtq" Text="Estado Notificación:" runat="server" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                        <asp:CheckBox ID="chkEstadoActual" runat="server" Text="EstadoActual" meta:resourcekey="chkEstadoActualResource1" EnableTheming="false" />
                    </td>
                    <td style="width: 25%; text-align: left; vertical-align: middle;"></td>
                    <td style="width: 25%; text-align: left; vertical-align: middle;"></td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: left; vertical-align: top;">
                        <fieldset>
                            <legend>Fecha Acto Administrativo</legend>
                            <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
                                <tr>
                                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                                        <asp:Label ID="lblFechaActo" runat="server" Text="Fecha Desde" SkinID="etiqueta_negra"></asp:Label>
                                    </td>
                                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                                        <asp:TextBox ID="txtFechaDesde" runat="server" SkinID="texto_corto" ClientIDMode="Static"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFechaActo" runat="server" ControlToValidate="txtFechaDesde" ErrorMessage="Ingrese la Fecha Desde de busqueda" Text="*" ValidationGroup="Consulta" InitialValue=""></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revFechaActo" runat="server" ControlToValidate="txtFechaDesde" ErrorMessage="Formato de fecha no valido para la Fecha  Hasta de busqueda" ValidationExpression="^\d{2}\/\d{2}\/\d{4}" Width="3px" Text="*"></asp:RegularExpressionValidator>
                                        <%--<cc1:CalendarExtender ID="calFechaActo" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaDesde" BehaviorID="ctl00_calFechaActo"></cc1:CalendarExtender>--%>
                                    </td>
                                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                                        <asp:Label ID="Label9" runat="server" Text="Fecha hasta:" meta:resourcekey="Label9Resource1" SkinID="etiqueta_negra"></asp:Label>
                                    </td>
                                    <td style="width: 25%; text-align: left; vertical-align: middle;">
                                        <asp:TextBox ID="txtFechaHasta" runat="server" SkinID="texto_corto" ClientIDMode="Static"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFechaHasta" runat="server" ControlToValidate="txtFechaHasta" ErrorMessage="Ingrese la Fecha Hasta de busqueda" ValidationGroup="Consulta" Text="*" InitialValue=""></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revFechaHasta" runat="server" ControlToValidate="txtFechaHasta" ErrorMessage="Formato de fecha no valido para la Feecha del Acto Administrativo" ValidationExpression="^\d{2}\/\d{2}\/\d{4}" Width="10px" meta:resourcekey="RegularExpressionValidator1Resource1" Text="*"></asp:RegularExpressionValidator>
                                        <%--<cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaHasta"></cc1:CalendarExtender>--%>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="padding-top: 10px; padding-bottom: 10px; padding-left: 10px; padding-right: 10px; text-align: center; vertical-align: middle;">
                        <table border="0" style="width: 100%;">
                            <tr>
                                <td style="padding-right: 30px; text-align: right; vertical-align: middle;">
                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" ValidationGroup="search" CausesValidation="true" OnClick="btnBuscar_Click" SkinID="boton_copia" TabIndex="12" />
                                </td>
                                <td style="padding-left: 30px; text-align: left; vertical-align: middle;">
                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" SkinID="boton_copia" OnClick="btnCancelar_Click1" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="padding: 10px; text-align: left; vertical-align: top;">
                        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" meta:resourcekey="lblMensajeResource1"></asp:Label>
                        <br />
                        <asp:ValidationSummary ID="valResumen" runat="server" DisplayMode="List" ValidationGroup="Consulta" />
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:Panel ID="pnlDocumentos" runat="server">
            <div style="overflow: scroll; text-align: left; vertical-align: top; padding: 0;">
                <asp:GridView ID="grdEstadosNotificacion" runat="server" Width="100%" SkinID="Grilla_Simple"
                    CellSpacing="10" CellPadding="10" AllowPaging="True" 
                    DataKeyNames="ID,IdSolicitud,IdEstadoNotificado,EstadoCambioPDI,IdActoNotificacion,NumeroSilpa,IdApplicationUser,IdPersonaNotificar,FechaEstadoNotificado,Expediente,TipoActoAdministrativo,FechaActo,UsuarioNotificar,NumeroIdentificacionUsuario,NumeroActoAdministrativo,DiasVencimiento,IdProcesoNotificacion,EstadoNotificado,Archivo,NombreAutoridad,IdAutoridad" 
                    AutoGenerateColumns="False" meta:resourcekey="grdEstadosNotificacionResource1"
                    OnRowCommand="grdEstadosNotificacion_RowCommand"
                    OnRowDataBound="grdEstadosNotificacion_RowDataBound"
                    OnPageIndexChanging="grdEstadosNotificacion_PageIndexChanging">
                    <%--<HeaderStyle BackColor="#3E4D60" Font-Bold="True" ForeColor="#EBEEF1" Font-Size="9pt" />--%>
                    <HeaderStyle Font-Size="9pt" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="#FFFFFF" BackColor="#44546A" />
                    <FooterStyle BackColor="#CFD7DE" Font-Bold="True" ForeColor="#000000" Font-Size="9pt" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#1C222B" Font-Size="9px" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#1C222B" Font-Size="9px" />
                    <EditRowStyle BackColor="#1C222B" Font-Size="9px" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#1C222B" Font-Size="9px" />
                    <Columns>
                        <asp:CommandField ButtonType="Button" DeleteText="Eliminar" EditText="Editar" HeaderText="Editar Estado"
                            InsertText="Crear" NewText="Crear" ShowDeleteButton="True" ShowEditButton="True"
                            ShowHeader="True" ShowInsertButton="True" UpdateText="Editar" Visible="False"
                            meta:resourcekey="CommandFieldResource1" />
                        <asp:BoundField DataField="IdSolicitud" HeaderText="ID_SOLICITUD_DAA" Visible="False" meta:resourcekey="BoundFieldResource1" HeaderStyle-ForeColor="#F8F8F9" />
                        <asp:TemplateField HeaderText="Notificación Electrónica">
                            <ItemTemplate>
                                <asp:Label ID="lblActoNotiElect" Text='<%# ((bool)Eval("ActoEsNotificacionElectronica_EXP")) ? "Si":"No" %>' runat="server" SkinID="etiqueta_negra9" Visible="false"></asp:Label>
                                <asp:Label ID="TxtlblNotiElect" Text="" runat="server" SkinID="etiqueta_negra9" Visible="true"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="9pt" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="#F8F8F9" BackColor="#44546A" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="9pt" ForeColor="#000000" />
                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo Notificación">
                            <ItemTemplate>
                                <asp:Label ID="lblNotiElect" Text='<%# ((bool)Eval("EsNotificacionElectronica")) ? "Si":"No" %>' runat="server" SkinID="etiqueta_negra9" Visible="false"></asp:Label>
                                <asp:Label ID="lblNotiElectXAA" runat="server" Text='<%# ((bool)Eval("EsNotificacionElectronica_AA")) ? "Si":"No" %>' SkinID="etiqueta_negra9" Visible="false"></asp:Label>
                                <asp:Label ID="lblNotiElectXEXP" runat="server" Text='<%# ((bool)Eval("EsNotificacionElectronica_EXP")) ? "Si":"No" %>' SkinID="etiqueta_negra9" Visible="false"></asp:Label>
                                <asp:Label ID="TxtlblTipoNotiElect" Text="" runat="server" SkinID="etiqueta_negra9" Visible="true"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="9pt" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="#F8F8F9" BackColor="#44546A" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="9pt" ForeColor="#000000" />
                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="NumeroSilpa" HeaderText="N&#250;mero VITAL" meta:resourcekey="BoundFieldResource2" HeaderStyle-ForeColor="#F8F8F9">
                            <HeaderStyle Font-Size="9pt" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="#F8F8F9" BackColor="#44546A" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="9pt" ForeColor="#000000" />
                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Expediente" HeaderText="N&#250;mero de Expediente" meta:resourcekey="BoundFieldResource3" HeaderStyle-ForeColor="#F8F8F9">
                            <HeaderStyle Font-Size="9pt" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="#F8F8F9" BackColor="#44546A" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="9pt" ForeColor="#000000" />
                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FechaActo" HeaderText="Fecha Acto Administrativo" DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="BoundFieldResource4" HeaderStyle-ForeColor="#F8F8F9">
                            <HeaderStyle Font-Size="9pt" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="#F8F8F9" BackColor="#44546A" />
                            <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="9pt" ForeColor="#000000" />
                            <FooterStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Tipo Acto Administrativo">
                            <ItemTemplate>
                                <asp:Label ID="LblTipoActo" runat="server" Text='<%# Bind("TipoActoAdministrativo") %>' SkinID="etiqueta_negra9"></asp:Label>
                                <asp:Label ID="LblEstadoNotificadoId" runat="server" Text='<%# Bind("IdEstadoNotificado") %>' Visible="false" SkinID="etiqueta_negra9"></asp:Label>
                                <asp:Label ID="LblTieneActividadSiguiente" runat="server" Text='<%# Bind("TieneActividadSiguiente")%>' Visible="false" SkinID="etiqueta_negra9"></asp:Label>
                                <asp:Label ID="LblMostrarInformacion" runat="server" Text='<%# Bind("MostrarInfomacion")%>' Visible="false" SkinID="etiqueta_negra9"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="9pt" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="#F8F8F9" BackColor="#44546A" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="9pt" ForeColor="#000000" />
                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="IdTipoActoAdministrativo" HeaderText="IdTipoActo" Visible="False" meta:resourcekey="BoundFieldResource17" HeaderStyle-ForeColor="#F8F8F9" />
                        <asp:BoundField DataField="NumeroActoAdministrativo" HeaderText="N&#250;mero Acto Administrativo" meta:resourcekey="BoundFieldResource6" HeaderStyle-ForeColor="#F8F8F9">
                            <HeaderStyle Font-Size="9pt" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="#F8F8F9" BackColor="#44546A" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="9pt" ForeColor="#000000" />
                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UsuarioNotificar" HeaderText="Usuario Notificar" meta:resourcekey="BoundFieldResource7" HeaderStyle-ForeColor="#F8F8F9" />
                        <asp:BoundField DataField="NumeroIdentificacionUsuario" HeaderText="Identificaci&#243;n Usuario" meta:resourcekey="BoundFieldResource8" HeaderStyle-ForeColor="#F8F8F9" />
                        <asp:BoundField DataField="EstadoNotificado" HeaderText="Estado" meta:resourcekey="BoundFieldResource9" HeaderStyle-ForeColor="#F8F8F9" />
                        <asp:TemplateField HeaderText="Fecha del Estado">
                            <ItemTemplate>
                                <asp:Label ID="lblFechaEstadoNoficado" runat="server" Text='<%# Bind("FechaEstadoNotificado","{0:dd/MM/yyyy}") %>' SkinID="etiqueta_negra9"></asp:Label>
                                <asp:ImageButton ID="btniEditarFecha" runat="server" SkinID="icoEditar" ToolTip="Modificar Fecha" CommandName="ModificarFechaEstado" />
                            </ItemTemplate>
                            <HeaderStyle Width="100px" Font-Size="9pt" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="#F8F8F9" BackColor="#44546A" />
                            <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="9pt" ForeColor="#000000" />
                            <FooterStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="IdEstadoNotificado" HeaderText="ID_ESTADO_NOTIFICADO" Visible="False" meta:resourcekey="BoundFieldResource11" HeaderStyle-ForeColor="#F8F8F9" />
                        <asp:BoundField DataField="DiasVencimiento" HeaderText="D&#237;as para Vencimiento" meta:resourcekey="BoundFieldResource12" HeaderStyle-ForeColor="#F8F8F9" />
                        <asp:BoundField DataField="Sistema" HeaderText="Sistema de notificaci&#243;n" meta:resourcekey="BoundFieldResource13" Visible="False" HeaderStyle-ForeColor="#F8F8F9" />
                        <asp:BoundField DataField="DatoProvienePDI" HeaderText="Dato proviene del sistema de notificaci&#243;n" meta:resourcekey="BoundFieldResource18" HeaderStyle-ForeColor="#F8F8F9" />
                        <asp:CheckBoxField DataField="EstadoCambioPDI" HeaderText="Dato proviene del sistema de notificaci&#243;n" Visible="False" meta:resourcekey="CheckBoxFieldResource1" />
                        <asp:BoundField DataField="IdProcesoNotificacion" HeaderText="ID Proceso de Notificaci&#243;n" meta:resourcekey="BoundFieldResource14" HeaderStyle-ForeColor="#F8F8F9" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDescargar" runat="server" Text='<%# Bind("NombreArchivo") %>' CommandName="Descargar"></asp:LinkButton>
                                <asp:Label ID="lblArchivosAdjuntos" runat="server" Text='<%# Bind("ArchivosAdjuntos") %>' Visible="false"></asp:Label>
                                <asp:DataList ID="dtlArchivosRecurso" runat="server" Visible="false" OnItemCommand="dtlArchivosRecurso_ItemCommand">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDescargarArchivo" runat="server" Text='<%# Bind("NombreArchivo") %>'></asp:LinkButton>
                                        <asp:Label ID="lblRutaArchivo" runat="server" Text='<%# Bind("RutaArchivo") %>' Visible="false" />
                                    </ItemTemplate>
                                </asp:DataList>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="9pt" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="#F8F8F9" BackColor="#44546A" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="9pt" ForeColor="#000000" />
                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Archivo" HeaderText="Archivo" meta:resourcekey="BoundFieldResource15" Visible="False" HeaderStyle-ForeColor="#F8F8F9" />
                        <asp:BoundField DataField="IdApplicationUser" HeaderText="ID_APPLICATION_USER" meta:resourcekey="BoundFieldResource16" Visible="False" HeaderStyle-ForeColor="#F8F8F9" />
                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" meta:resourcekey="BoundFieldResource19" HeaderStyle-ForeColor="#F8F8F9" />
                        <asp:CommandField meta:resourcekey="CommandFieldResource2" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnAvanzarEstado" runat="server" Text="Avanzar Estado" CommandName="Crear" />
                            </ItemTemplate>
                            <HeaderStyle Width="100px" Font-Size="9pt" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="#F8F8F9" BackColor="#44546A" />
                            <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <FooterStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEditarEstado" runat="server" Text="Editar Estado" CommandName="Editar" />
                            </ItemTemplate>
                            <HeaderStyle Width="100px" Font-Size="9pt" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="#F8F8F9" BackColor="#44546A" />
                            <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <FooterStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEliminarEstado" runat="server" Text="Eliminar Estado" CommandName="Eliminar" />
                            </ItemTemplate>
                            <HeaderStyle Width="100px" Font-Size="9pt" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="#F8F8F9" BackColor="#44546A" />
                            <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <FooterStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="IdPersonaNotificar" HeaderText="IdPersonaNotificar" Visible="False" HeaderStyle-ForeColor="#F8F8F9" />
                        <asp:BoundField DataField="EstadoCambioPDI" HeaderText="EstadoCambioPDI" Visible="False" meta:resourcekey="BoundFieldResource21" HeaderStyle-ForeColor="#F8F8F9" />
                        <asp:BoundField DataField="FechaEnvioCorreo" HeaderText="Fecha Env&#237;o Correo" meta:resourcekey="BoundFieldResource22" HeaderStyle-ForeColor="#F8F8F9">
                            <HeaderStyle Width="100px" Font-Size="9pt" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="#F8F8F9" BackColor="#44546A" />
                            <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="9pt" ForeColor="#000000" />
                            <FooterStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NombreAutoridad" HeaderText="Autoridad Ambiental" meta:resourcekey="BoundFieldResource23" HeaderStyle-ForeColor="#F8F8F9" ControlStyle-Width="220px" ItemStyle-Width="220px">
                            <HeaderStyle Width="220px" Font-Size="9pt" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="#F8F8F9" BackColor="#44546A" />
                            <ItemStyle Width="220px" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="9pt" ForeColor="#000000" />
                            <FooterStyle Width="220px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IdAutoriad" HeaderText="IdAutoriad" Visible="False" meta:resourcekey="BoundFieldResource24" HeaderStyle-ForeColor="#F8F8F9" />
                    </Columns>
                </asp:GridView>
            </div>
        </asp:Panel>
        <asp:Label ID="lbl_pagina" runat="server" CssClass="texto_usuario" Text="Página  " ToolTip="Página" Visible="False"></asp:Label>
        <asp:Label ID="lbl_numero_pagina" runat="server" CssClass="texto_usuario" Text="10" ToolTip="Usted se encuentra en esta página" Visible="False"></asp:Label>
        <asp:Label ID="lbl_de" runat="server" CssClass="texto_usuario" Text="   de   " ToolTip="de" Visible="False"></asp:Label>
        <asp:Label ID="lbl_numero_paginas" runat="server" CssClass="texto_usuario" Text="30" ToolTip="Número de páginas que contiene este listado" Visible="False"></asp:Label>
        <br />
        <asp:Label ID="lbl_total" runat="server" CssClass="texto_usuario" Visible="False"></asp:Label>
        <br />
        <cc1:ModalPopupExtender ID="mpeAvanzarEstado" runat="server" PopupControlID="dvAvanzarEstado" TargetControlID="lblNumeroVital" BehaviorID="mpeAvanzar" CancelControlID="btnCancelarAvan" BackgroundCssClass="caja-dialogo-fondo-aplicacion">
        </cc1:ModalPopupExtender>
        <div id="dvAvanzarEstado" runat="server" class="caja-dialogo3" style="display: none; padding: 0 !important; margin: 0 !important;">
            <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="txtValorCompara" runat="server" Font-Size="Smaller" ForeColor="White" ValidationGroup="crear" Width="10px" Style="display: none;">-1</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th colspan="2" style="text-align: center !important; vertical-align: middle !important; font-weight: bold; font-size: 13pt; border-bottom: 1px solid #293340 !important; padding: 5px !important;">Datos Acto Administrativo</th>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: middle; margin-top: 10px !important;">
                        <asp:Label ID="lblAutoridad" runat="server" meta:resourcekey="lblEstadoResource1" SkinID="etiqueta_negra" Text="Autoridad Ambiental:" Width="152px"></asp:Label>
                    </td>
                    <td style="text-align: left; vertical-align: middle; margin-top: 10px !important;">
                        <asp:TextBox ID="txtAutoridadAmbientalAvan" runat="server" Enabled="False" ReadOnly="True" SkinID="texto"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: middle;">
                        <asp:Label ID="lblEstado" runat="server" meta:resourcekey="lblEstadoResource1" SkinID="etiqueta_negra" Text="Número VITAL:"></asp:Label>
                    </td>
                    <td style="text-align: left; vertical-align: middle;">
                        <asp:TextBox ID="txtNumeroVitalAvan" runat="server" ReadOnly="True" SkinID="texto" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: middle;">
                        <asp:Label ID="Label8" runat="server" Text="Número de Expediente" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td style="text-align: left; vertical-align: middle;">
                        <asp:TextBox ID="txtNumeroExpAvan" runat="server" ReadOnly="True" SkinID="texto" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: middle;">
                        <asp:Label ID="Label10" runat="server" Text="Fecha del Acto Administrativo" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td style="text-align: left; vertical-align: middle;">
                        <asp:TextBox ID="txtFechaActoAvan" runat="server" ReadOnly="True" SkinID="texto" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: middle;">
                        <asp:Label ID="lblTipActo" runat="server" SkinID="etiqueta_negra" Text="Tipo Acto Administrativo"></asp:Label></td>
                    <td style="text-align: left; vertical-align: middle;">
                        <asp:TextBox ID="txtTipoActoAdministrativoAvan" runat="server" Enabled="False" ReadOnly="True"
                            SkinID="texto"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: middle;">
                        <asp:Label ID="Label11" runat="server" Text="Nùmero Acto Administrativo" SkinID="etiqueta_negra"></asp:Label></td>
                    <td style="text-align: left; vertical-align: middle;">
                        <asp:TextBox ID="txtNumeroActoAdmiAvan" runat="server" ReadOnly="True" SkinID="texto" Enabled="False"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: middle;">
                        <asp:Label ID="Label12" runat="server" Text="Usuario a Notificar" SkinID="etiqueta_negra"></asp:Label></td>
                    <td style="text-align: left; vertical-align: middle;">
                        <asp:TextBox ID="txtUsuarioAvan" runat="server" ReadOnly="True" SkinID="texto" Enabled="False"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: middle;">
                        <asp:Label ID="Label13" runat="server" Text="Identificación del usuario" SkinID="etiqueta_negra"></asp:Label></td>
                    <td style="text-align: left; vertical-align: middle;">
                        <asp:TextBox ID="txtIdentUsuarioAvan" runat="server" ReadOnly="True" SkinID="texto" Enabled="False"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: middle;">
                        <asp:Label ID="Label14" runat="server" Text="Estado Actual" SkinID="etiqueta_negra"></asp:Label></td>
                    <td style="text-align: left; vertical-align: middle;">
                        <asp:TextBox ID="txtEstadoActualAvan" runat="server" ReadOnly="True" SkinID="texto" Enabled="False"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: middle;">
                        <asp:Label ID="Label15" runat="server" SkinID="etiqueta_negra" Text="Fecha del Estado Actual"></asp:Label></td>
                    <td style="text-align: left; vertical-align: middle;">
                        <asp:TextBox ID="txtFechaEstadoActualAvan" runat="server" Enabled="False" ReadOnly="True" SkinID="texto"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: middle;">
                        <asp:Label ID="Label16" runat="server" Text="Días para Vencimiento" SkinID="etiqueta_negra"></asp:Label></td>
                    <td style="text-align: left; vertical-align: middle;">
                        <asp:TextBox ID="txtDiasVenceAvan" runat="server" ReadOnly="True" SkinID="texto" Enabled="False"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: middle;">
                        <asp:Label ID="Label17" runat="server" Text="Dato proviene del sistema de notificación" SkinID="etiqueta_negra" Width="199px"></asp:Label></td>
                    <td style="text-align: left; vertical-align: middle;">
                        <asp:TextBox ID="txtDatoPDIAvan" runat="server" ReadOnly="True" SkinID="texto" Enabled="False"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: middle;">
                        <asp:Label ID="Label18" runat="server" Text="ID del proceso de Notificación" SkinID="etiqueta_negra"></asp:Label></td>
                    <td style="text-align: left; vertical-align: middle;">
                        <asp:TextBox ID="txtIDProcesoNotAvan" runat="server" ReadOnly="True" SkinID="texto" Enabled="False"></asp:TextBox></td>
                </tr>
            <%--</table>
            <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">--%>
                <tr>
                    <th colspan="2" style="text-align: center !important; vertical-align: middle !important; font-weight: bold; font-size: 13pt; border-bottom: 1px solid #293340 !important; padding: 5px !important;">Estado Notificación</th>
                </tr>
                <tr>
                    <td colspan="2" class="titleUpdate">
                        <asp:Label ID="Label19" TabIndex="1" runat="server" Visible="False" meta:resourcekey="lblIdResource1">-1</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: middle;">
                        <asp:Label ID="Label20" runat="server" Text="Crear Estado" SkinID="etiqueta_negra" meta:resourcekey="lbl_titulo_principalResource1" Width="152px"></asp:Label>
                    </td>
                    <td style="text-align: left; vertical-align: middle;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlEstado"
                            ErrorMessage="RequiredFieldValidator" ValidationGroup="crear">*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvEstado" runat="server" ControlToValidate="ddlEstado" ErrorMessage="Seleccione el estado" meta:resourcekey="rfvFechaActoResource1"
                            Text="*" ValidationGroup="crear" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: middle;">
                        <asp:Label ID="lblEstado1" runat="server" Font-Names="Arial" Font-Size="Smaller" Height="9px" Text="Estado:" Width="152px" meta:resourcekey="lblEstadoResource1" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td style="text-align: left; vertical-align: middle;">
                        <asp:DropDownList ID="ddlEstado" runat="server" SkinID="lista_desplegable" TabIndex="3" meta:resourcekey="ddlEstadoResource1" ValidationGroup="crear"></asp:DropDownList>
                        <asp:CompareValidator ID="cfvEstado" runat="server" ControlToCompare="txtValorCompara"
                            ControlToValidate="ddlEstado" ErrorMessage="Seleccione el estado" Operator="GreaterThan"
                            ValidationGroup="crear" ValueToCompare="-1">*</asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: middle;">
                        <asp:Label ID="lblFechaCrear" runat="server" Font-Names="Narkisim" Font-Size="Smaller"
                            Height="9px" Text="Fecha del Estado:" Width="152px" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td style="text-align: left; vertical-align: middle;">
                        <asp:TextBox ID="txtFechaEstado" runat="server" TabIndex="5" Width="160px" ValidationGroup="crear" SkinID="texto_corto" ClientIDMode="Static"></asp:TextBox>
                        <%--<cc1:CalendarExtender ID="calFechaEstado" runat="server" Format="dd/MM/yyyy HH:mm" TargetControlID="txtFechaEstado" PopupPosition="TopLeft" Animated="true">
                        </cc1:CalendarExtender>--%>
                        <%--<cc1:MaskedEditExtender ID="mskFechaHora" Mask="99/99/9999 99:99" runat="server"
                            MaskType="DateTime" AcceptAMPM="False" UserDateFormat="DayMonthYear" UserTimeFormat="None"
                            TargetControlID="txtFechaEstado" Enabled="True">
                        </cc1:MaskedEditExtender>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFechaEstado"
                            ErrorMessage="Digite la fecha del estado" ValidationGroup="crear" Width="12px">*</asp:RequiredFieldValidator>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: middle;"></td>
                    <td style="text-align: left; vertical-align: middle;">
                        <asp:CheckBox ID="chkEnviarCorreo" runat="server" Checked="True" SkinID="check" Text="Enviar correo" EnableTheming="false" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: middle;">
                        <asp:Label ID="Label21" runat="server" Font-Names="Arial" Font-Size="Smaller" Height="9px" Text="Adjuntar archivo:" Width="152px" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td style="text-align: left; vertical-align: middle;">
                        <asp:FileUpload ID="fupAdjunto" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: middle;">
                        <asp:Label ID="lblMensajeCorreo" runat="server" Font-Names="Arial" Font-Size="Smaller"
                            Height="9px" Text="Texto de Correo:"
                            Width="152px" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td style="text-align: left; vertical-align: middle;">
                        <asp:TextBox ID="txtTextoCorreo" runat="server" Height="114px" SkinID="texto" TextMode="MultiLine" Width="464px" Style="resize: none;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding-top: 20px; padding-bottom: 10px; padding-left: 10px; padding-right: 20px; text-align: right; vertical-align: middle;">
                        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; width: 100%;">
                            <tr>
                                <td style="padding-right: 20px; text-align: right; vertical-align: middle;">
                                    <asp:Button ID="btnAceptarAvance" runat="server" Text="Aceptar" OnClick="btnAceptarAvance_Click" TabIndex="12" ValidationGroup="crear" SkinID="boton_copia" />
                                </td>
                                <td style="padding-left: 20px; text-align: left; vertical-align: middle;">
                                    <asp:Button ID="btnCancelarAvan" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" SkinID="boton_copia" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding: 10px; text-align: left; vertical-align: top;">
                        <asp:Label ID="Label22" runat="server" ForeColor="Red" meta:resourcekey="lblMensajeResource1"></asp:Label>
                        <br />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" meta:resourcekey="valResumenResource1" ValidationGroup="crear" />
                    </td>
                </tr>
            </table>
        </div>
        <cc1:ModalPopupExtender ID="mpeModificarFechaEstado" runat="server" PopupControlID="dvModificarFechaEstado" TargetControlID="lblNumeroVital" BehaviorID="mpeModificar" CancelControlID="btnCancelarModificarFechaEstado" BackgroundCssClass="caja-dialogo-fondo-aplicacion">
        </cc1:ModalPopupExtender>
        <%-- class="caja-dialogo4"--%>
        <div id="dvModificarFechaEstado" runat="server" style="display: none;" class="caja-dialogo4 table-responsive">
            <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
                <tr>
                    <th colspan="2" style="text-align: center !important; vertical-align: middle !important; font-weight: bold; font-size: 13pt; border-bottom: 1px solid #293340 !important; padding: 5px !important;">Fecha del Estado</th>
                </tr>
                <tr>
                    <td>Fecha del Estado: </td>
                    <td>
                        <asp:TextBox ID="txtEditFechaEstado" runat="server" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEditFechaEstado" runat="server" ControlToValidate="txtEditFechaEstado"
                            ErrorMessage="Debe Ingresar la Fecha del Estado" ValidationGroup="ModFechaEstado" Width="12px">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                            <tr>
                                <td style="text-align: right; padding-right: 20px; vertical-align: middle;">
                                    <asp:Button ID="btnModificarFechaEstado" runat="server" Text="Modificar" OnClick="btnModificarFechaEstado_Click" SkinID="boton_copia" ValidationGroup="ModFechaEstado" OnClientClick="return confirm('Esta seguro de Cambiar la fecha del Estado?');" />
                                </td>
                                <td style="text-align: left; padding-left: 20px; vertical-align: middle;">
                                    <asp:Button ID="btnCancelarModificarFechaEstado" runat="server" Text="Cancelar" OnClick="btnCancelarModificarFechaEstado_Click" SkinID="boton_copia" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script src="../Scripts/jquery.datetimepicker.js"></script>
    <script src="../jquery/jquery.numeric.js"></script>
    <script src="../js/EstadosNotificacion.js" type="text/javascript"></script>
</asp:Content>
