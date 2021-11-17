<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPASinMenuFlash.master" AutoEventWireup="true" CodeFile="ConsultaNotificacionPublico.aspx.cs" Inherits="NotificacionElectronica_ConsultaNotificacionPublico" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <script src='<%= ResolveClientUrl("~/js/dropdownWidth.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/js/basicos.js") %>' type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script src="../jquery/jquery.js" type="text/javascript"></script>    
<link href="../jquery/keypad/jquery.keypad.css" rel="stylesheet" />
<script src="../jquery/keypad/jquery.plugin.js" type="text/javascript"></script>
<script src="../jquery/keypad/jquery.keypadmodal.js" type="text/javascript"></script> 
<script language="javascript" type="text/javascript">
$(function () {    
    $('#txtContrasena').keypad({
        randomiseNumeric: true,
        prompt: '',
        closeText: 'Cerrar',
        clearText: 'Limpiar',
        backText: '<<'
    });
});

function refreshWindow()
{
        <%= (GetPostBackEventReference(this, "Actualizar")) %>;
}

function refreshWindowRollback()
{
        <%= (GetPostBackEventReference(this, "RollBack")) %>;
}
</script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
 <table style="width: 98%">
        <tbody>
            <tr>
                <td colspan="4">
                    <div class="div-titulo">
                        <asp:Label ID="lbl_titulo_principal" runat="server" Text="ESTADOS DE NOTIFICACION POR ACTO ADMINISTRATIVO"
                            SkinID="titulo_principal_blanco"></asp:Label>
                        &nbsp;
                        <a href="#" onclick="window.close();return false;">Salir</a>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="titleUpdate">
                    <asp:Label ID="lblId" TabIndex="1" runat="server" Visible="False" meta:resourcekey="lblIdResource1"
                        SkinID="normal" Text="-1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table width="90%" style='margin: 0px auto;'>
                        <tr>
                            <td width="25%">
                                <asp:Label ID="lblNumeroVital" runat="server" Text="Número VITAL:" meta:resourcekey="lblNumeroVitalResource1"
                                    SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td width="25%">
                                <asp:TextBox ID="txtNumeroVital" runat="server" meta:resourcekey="txtNumeroVitalResource1" SkinID="texto_corto"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="rfvNumeroVital" runat="server" ControlToValidate="txtNumeroVital" ErrorMessage="Ingrese el Número VITAL"
                                        Enabled="False" meta:resourcekey="rfvNumeroVitalResource1" Text="*"></asp:RequiredFieldValidator>
                            </td>
                            <td width="25%">
                                <asp:Label ID="lblExpediente" runat="server" Text="Número de Expediente:" meta:resourcekey="lblExpedienteResource1"
                                    SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td width="25%">
                                <asp:TextBox ID="txtExpediente" runat="server" meta:resourcekey="txtExpedienteResource1" SkinID="texto_corto"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNumeroActo" runat="server" Text="Número Acto Administrativo:" meta:resourcekey="lblNumeroActoResource1"
                                    SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumeroActo" runat="server" meta:resourcekey="txtNumeroActoResource1"
                                    SkinID="texto_corto"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Tipo de Acto Administrativo:" meta:resourcekey="Label1Resource1"
                                    SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTipoActo" runat="server" SkinID="lista_desplegable2" meta:resourcekey="ddlTipoActoResource1">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%">
                                <asp:Label ID="Label6" runat="server" Text="Autoridad Ambiental:" meta:resourcekey="Label6Resource1"
                                    SkinID="etiqueta_negra"></asp:Label></td>
                            <td width="25%">
                                <asp:DropDownList ID="cboAutoAmbiental" runat="server" SkinID="lista_desplegable2">
                                </asp:DropDownList>
                            </td>
                             <td width="25%">
                                <asp:CheckBox ID="chkEstadoActual" runat="server" Text="EstadoActual" meta:resourcekey="chkEstadoActualResource1" />
                            </td>
                            <td width="25%">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Estado Notificación:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboEstadoNotificacion" runat="server" SkinID="lista_desplegable2">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                <td colspan="4">
                    <fieldset>
                        <legend>Fecha Acto Administrativo</legend>
                        <table width="100%">
                            <tr>
                                <td width="25%" align="right">
                                    <asp:Label ID="lblFechaActo" runat="server" meta:resourcekey="lblFechaActoResource1"
                                        Text="Fecha Desde" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td width="25%">
                                    <asp:TextBox ID="txtFechaDesde" runat="server" meta:resourcekey="txtFechaDesdeResource1"
                                        SkinID="texto_corto"></asp:TextBox><asp:RequiredFieldValidator ID="rfvFechaActo"
                                            runat="server" ControlToValidate="txtFechaDesde" ErrorMessage="Ingrese la Fecha del Acto Administrativo"
                                            Enabled="False" meta:resourcekey="rfvFechaActoResource1" Text="*"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                ID="revFechaActo" runat="server" ControlToValidate="txtFechaDesde" ErrorMessage="Formato de fecha no valido para la Feecha del Acto Administrativo"
                                                ValidationExpression="^\d{2}\/\d{2}\/\d{4}" Width="3px" meta:resourcekey="revFechaActoResource1"
                                                Text="*"></asp:RegularExpressionValidator><cc1:CalendarExtender ID="calFechaActo"
                                                    runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaDesde"
                                                    BehaviorID="ctl00_calFechaActo">
                                                </cc1:CalendarExtender>
                                </td>
                                <td align="right" width="25%">
                                    <asp:Label ID="Label9" runat="server" Text="Fecha hasta:" meta:resourcekey="Label9Resource1"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFechaHasta" runat="server" meta:resourcekey="txtFechaHastaResource1"
                                        SkinID="texto_corto"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            runat="server" ControlToValidate="txtFechaHasta" ErrorMessage="Ingrese la Fecha del Acto Administrativo"
                                            Enabled="False" meta:resourcekey="RequiredFieldValidator1Resource1" Text="*"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFechaHasta"
                                                ErrorMessage="Formato de fecha no valido para la Feecha del Acto Administrativo"
                                                ValidationExpression="^\d{2}\/\d{2}\/\d{4}" Width="10px" meta:resourcekey="RegularExpressionValidator1Resource1"
                                                Text="*"></asp:RegularExpressionValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        TargetControlID="txtFechaHasta">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="right">
                    <asp:Button ID="btnBuscar" runat="server" CausesValidation="False" Text="Buscar"
                        OnClick="btnBuscar_Click" SkinID="boton_copia" TabIndex="12" meta:resourcekey="btnBuscarResource1"
                        Width="71px" />
                </td>
                <td>
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" SkinID="boton_copia"
                        meta:resourcekey="Button1Resource1" OnClick="btnCancelar_Click1" Width="73px" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Panel ID="pnlDocumentos" runat="server" meta:resourcekey="pnlDocumentosResource1"
                        ScrollBars="Both" Width="950px">
                        <asp:GridView ID="grdEstadosNotificacion" runat="server" OnRowCommand="grdEstadosNotificacion_RowCommand"
                            CellSpacing="1" CellPadding="2" DataKeyNames="ID,IdSolicitud,IdEstadoNotificado,EstadoCambioPDI,IdActoNotificacion,NumeroSilpa,IdApplicationUser,IdPersonaNotificar,FechaEstadoNotificado,Expediente,TipoActoAdministrativo,FechaActo,UsuarioNotificar,NumeroIdentificacionUsuario,NumeroActoAdministrativo,DiasVencimiento,IdProcesoNotificacion,EstadoNotificado,Archivo,NombreAutoridad,IdAutoridad,ArchivosAdjuntos"
                            AutoGenerateColumns="False" meta:resourcekey="grdEstadosNotificacionResource1"
                            AllowPaging="True" OnPageIndexChanging="grdEstadosNotificacion_PageIndexChanging"
                            SkinID="Grilla" onrowdatabound="grdEstadosNotificacion_RowDataBound">
                            <RowStyle HorizontalAlign="Left"></RowStyle>
                            <Columns>
                                <asp:BoundField DataField="IdSolicitud" HeaderText="ID_SOLICITUD_DAA" Visible="False"
                                    meta:resourcekey="BoundFieldResource1" />
                                <asp:BoundField DataField="NombreAutoridad" HeaderText="Autoridad Ambiental" meta:resourcekey="BoundFieldResource23" />
                                <asp:BoundField DataField="IdAutoriad" HeaderText="IdAutoriad" Visible="False" meta:resourcekey="BoundFieldResource24" />
                                <asp:BoundField DataField="NumeroSilpa" HeaderText="N&#250;mero VITAL" meta:resourcekey="BoundFieldResource2">
                                    <HeaderStyle Width="8%"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Expediente" HeaderText="N&#250;mero de Expediente" meta:resourcekey="BoundFieldResource3">
                                    <HeaderStyle Width="8%"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FechaActo" HeaderText="Fecha Acto Administrativo" DataFormatString="{0:dd/MM/yyyy}"
                                    meta:resourcekey="BoundFieldResource4">
                                    <HeaderStyle Width="8%"></HeaderStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Tipo Acto Administrativo">
                                    <ItemTemplate>
                                        <asp:Label ID="LblTipoActo" runat="server" Text='<%# Bind("TipoActoAdministrativo") %>' SkinID="etiqueta_negra"></asp:Label>
                                        <asp:Label ID="LblEstadoNotificadoId" runat="server" Text='<%# Bind("IdEstadoNotificado") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="LblTieneActividadSiguiente" runat="server" Text='<%# Bind("TieneActividadSiguiente")%>' Visible="false"></asp:Label> 
                                        <asp:Label ID="LblMostrarInformacion" runat="server" Text='<%# Bind("MostrarInfomacion")%>' Visible="false"></asp:Label> 
                                    </ItemTemplate>
                                    <HeaderStyle Width="16%"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="IdTipoActoAdministrativo" HeaderText="IdTipoActo" Visible="False"
                                    meta:resourcekey="BoundFieldResource17" />
                                <asp:TemplateField HeaderText="Número Acto Administrativo">
                                    <ItemTemplate>
                                        <asp:Label ID="LblNumeroActo" runat="server" Text='<%# Bind("NumeroActoAdministrativo") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="8%"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="UsuarioNotificar" HeaderText="Usuario Notificar" meta:resourcekey="BoundFieldResource7" Visible="false" />
                                <asp:BoundField DataField="NumeroIdentificacionUsuario" HeaderText="Identificaci&#243;n Usuario" Visible="false"
                                    meta:resourcekey="BoundFieldResource8" />
                                <asp:BoundField DataField="EstadoNotificado" HeaderText="Estado" meta:resourcekey="BoundFieldResource9" />
                                <asp:BoundField DataField="FechaEstadoNotificado" HeaderText="Fecha del Estado" meta:resourcekey="BoundFieldResource10" />
                                <asp:BoundField DataField="DiasVencimiento" HeaderText="D&#237;as para Vencimiento"
                                    meta:resourcekey="BoundFieldResource11" Visible="true" /><%--13--%>
                                     <asp:TemplateField HeaderText="Tipo Acto Administrativo">
                                    <ItemTemplate>
                                        <asp:Label ID="LblValidarDias" runat="server" Text='<%# Bind("ValidarDiasVencimiento") %>' SkinID="etiqueta_negra" Visible="false"></asp:Label>
                                        <asp:Label ID="LblDiasVencimiento" runat="server" Text='<%# Bind("DiasVencimiento") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="16%"></HeaderStyle>
                                </asp:TemplateField>
                             <%--   <asp:BoundField DataField="DiasVencimiento" HeaderText="D&#237;as para Vencimiento"
                                    meta:resourcekey="BoundFieldResource12" />--%>
                                <asp:BoundField DataField="Sistema" HeaderText="Sistema de notificaci&#243;n" meta:resourcekey="BoundFieldResource13"
                                    Visible="False" />
                                <asp:BoundField DataField="DatoProvienePDI" HeaderText="Dato proviene del sistema de notificaci&#243;n"
                                    meta:resourcekey="BoundFieldResource18" Visible="false" />
                                <asp:CheckBoxField DataField="EstadoCambioPDI" HeaderText="Dato proviene del sistema de notificaci&#243;n"
                                    Visible="False" meta:resourcekey="CheckBoxFieldResource1" />
                                <asp:BoundField DataField="IdProcesoNotificacion" HeaderText="ID Proceso de Notificaci&#243;n"
                                    meta:resourcekey="BoundFieldResource14" Visible="false" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDescargar" runat="server" Text='<%# Bind("NombreArchivo") %>' CommandName="Descargar"></asp:LinkButton>
                                        <asp:Label ID="lblArchivosAdjuntos" runat="server" Text='<%# Bind("ArchivosAdjuntos") %>' Visible="false"></asp:Label>
                                        <asp:DataList ID="dtlArchivosRecurso" runat="server" Visible="false" 
                                            onitemcommand="dtlArchivosRecurso_ItemCommand">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDescargarArchivo" runat="server" Text='<%# Bind("NombreArchivo") %>'></asp:LinkButton>
                                                <asp:Label ID="lblRutaArchivo" runat="server" Text='<%# Bind("RutaArchivo") %>' Visible="false" />
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Archivo" HeaderText="Archivo" meta:resourcekey="BoundFieldResource15"
                                    Visible="False" />
                                <asp:BoundField DataField="IdApplicationUser" HeaderText="ID_APPLICATION_USER" meta:resourcekey="BoundFieldResource16"
                                    Visible="False" />
                                <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" meta:resourcekey="BoundFieldResource19" />
                                <asp:CommandField meta:resourcekey="CommandFieldResource2" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnAvanzarEstado" runat="server" Text="Avanzar Estado" CommandName="Avanzar"  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:ButtonField CommandName="Editar" HeaderText="Editar" Text="Editar" meta:resourcekey="ButtonFieldResource4" Visible="false" />
                                <asp:ButtonField CommandName="Eliminar" HeaderText="Eliminar" Text="Eliminar" meta:resourcekey="ButtonFieldResource5" Visible="false" />
                                <asp:BoundField DataField="IdPersonaNotificar" HeaderText="IdPersonaNotificar" Visible="False"
                                    meta:resourcekey="BoundFieldResource20" />
                                <asp:BoundField DataField="EstadoCambioPDI" HeaderText="EstadoCambioPDI" Visible="False"
                                    meta:resourcekey="BoundFieldResource21" />
                                <asp:BoundField DataField="FechaEnvioCorreo" HeaderText="Fecha Env&#237;o Correo"
                                    meta:resourcekey="BoundFieldResource22" Visible="false" />
                                
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                    <asp:Label ID="lbl_pagina" runat="server" CssClass="texto_usuario" Text="Página  "
                        ToolTip="Página" Visible="False"></asp:Label><asp:Label ID="lbl_numero_pagina" runat="server"
                            CssClass="texto_usuario" Text="10" ToolTip="Usted se encuentra en esta página"
                            Visible="False"></asp:Label><asp:Label ID="lbl_de" runat="server" CssClass="texto_usuario"
                                Text="   de   " ToolTip="de" Visible="False"></asp:Label><asp:Label ID="lbl_numero_paginas"
                                    runat="server" CssClass="texto_usuario" Text="30" ToolTip="Número de páginas que contiene este listado"
                                    Visible="False"></asp:Label><br />
                    <asp:Label ID="lbl_total" runat="server" CssClass="texto_usuario" Visible="False"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" meta:resourcekey="lblMensajeResource1"></asp:Label>
                    <br />
                    <asp:ValidationSummary ID="valResumen" runat="server" DisplayMode="List" meta:resourcekey="valResumenResource1" />
                </td>
            </tr>
        </tbody>
    </table>
    <cc1:ModalPopupExtender ID="mpeAvanzarEstado" runat="server" PopupControlID="dvAvanzarEstado" TargetControlID="lblFechaActo" BehaviorID="mpeAvanzar" CancelControlID="btnCancelarAvan" BackgroundCssClass="caja-dialogo-fondo-aplicacion">
    </cc1:ModalPopupExtender>    
<div id="dvAvanzarEstado" style="display:none;" runat="server" clientidmode="Static" class="caja-dialogo2">
    <div id="formWithKeyPad">
    <table>
        <tr>
            <td colspan="2">
                <asp:ValidationSummary ID="valAvanzarEstado" runat="server" ValidationGroup="AvanzarEstado" />
            </td>
        </tr>
        <tr>
             <td colspan="2" align="justify">
                Señor Usuario por favor seleccione la acción siguiente dentro del proceso de notificacion del acto administrativo
            </td>
        </tr>        
        <tr>
            <td>Estado: </td>
            <td align="left">
                <asp:DropDownList ID="cboSigEstado" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvSigEstado" ControlToValidate="cboSigEstado" InitialValue="-1" ValidationGroup="AvanzarEstado" ErrorMessage="Debe ingresar el estado">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Segunda Contraseña: </td>
            <td align="left">
                <asp:TextBox runat="server" ID="txtContrasena" TextMode="Password" ClientIDMode="Static" MaxLength="6"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvContrasena" ControlToValidate="txtContrasena" ValidationGroup="AvanzarEstado" ErrorMessage="Debe ingresar la segunda contraseña">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button ID="btnAvanzar" runat="server" Text="Aceptar" SkinID="boton_copia" onclick="btnAvanzar_Click" CausesValidation="true" ValidationGroup="AvanzarEstado" />
            </td>
            <td>
                <asp:Button ID="btnCancelarAvan" runat="server" Text="Cancelar" SkinID="boton_copia"/>
            </td>
        </tr>        
    </table>
    </div>
</div> 
</asp:Content>

