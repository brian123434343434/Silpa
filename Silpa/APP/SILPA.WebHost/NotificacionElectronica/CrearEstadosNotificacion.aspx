<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPASinMenu.master" AutoEventWireup="true" 
CodeFile="CrearEstadosNotificacion.aspx.cs" Inherits="NotificacionElectronica_CrearEstadosNotificacion" 
Title="CrearEstadosNotificacion"%>

<%--<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPALimpia.master" AutoEventWireup="true" 
CodeFile="CrearEstadosNotificacion.aspx.cs" Inherits="NotificacionElectronica_CrearEstadosNotificacion" 
Title="CrearEstadosNotificacion" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
--%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
        

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:TextBox ID="txtValorCompara" runat="server" Font-Size="Smaller" ForeColor="White"
                ValidationGroup="crear" Width="10px">-1</asp:TextBox>
            <br />
            <fieldset>
             <legend>Datos Acto Administrativo</legend>
            <table style="WIDTH: 70%">
                <tr>
                    <td align="right" style="width: 202px; height: 8px">
                        <asp:Label ID="lblAutoridad" runat="server" meta:resourcekey="lblEstadoResource1" SkinID="etiqueta_negra" Text="Autoridad Ambiental:" Width="152px"></asp:Label>
                    </td>
                    <td style="height: 8px">
                        <asp:TextBox ID="txtAutoridadAmbiental" runat="server" Enabled="False" ReadOnly="True"
                            SkinID="texto"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right" style="width: 202px; height: 8px">
                        <asp:Label ID="lblEstado" runat="server" meta:resourcekey="lblEstadoResource1" SkinID="etiqueta_negra" Text="Número VITAL:"></asp:Label>
                    </td>
                    <td style="height: 8px">
                        <asp:TextBox ID="txtNumeroVital" runat="server" ReadOnly="True" SkinID="texto" Enabled="False"></asp:TextBox></td>
                </tr>
                    <tr>
                        <td align="right" style="width: 202px; height: 8px">
                            <asp:Label ID="Label2" runat="server" Text="Número de Expediente" SkinID="etiqueta_negra"></asp:Label></td>
                        <td style="height: 8px">
                            <asp:TextBox ID="txtExpediente" runat="server" ReadOnly="True" SkinID="texto" Enabled="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 202px; height: 8px">
                            <asp:Label ID="Label3" runat="server" Text="Fecha del Acto Administrativo" SkinID="etiqueta_negra"></asp:Label></td>
                        <td style="height: 8px">
                            <asp:TextBox ID="txtFechaActo" runat="server" ReadOnly="True" SkinID="texto" Enabled="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 202px; height: 8px">
                            <asp:Label ID="Label11" runat="server" SkinID="etiqueta_negra" Text="Tipo Acto Administrativo"></asp:Label></td>
                        <td style="height: 8px">
                            <asp:TextBox ID="txtTipoActoAdministrativo" runat="server" Enabled="False" ReadOnly="True"
                                SkinID="texto"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 202px; height: 8px">
                            <asp:Label ID="Label10" runat="server" Text="Nùmero Acto Administrativo" SkinID="etiqueta_negra"></asp:Label></td>
                        <td style="height: 8px">
                            <asp:TextBox ID="txtNumeroActo" runat="server" ReadOnly="True" SkinID="texto" Enabled="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 202px; height: 8px">
                            <asp:Label ID="Label4" runat="server" Text="Usuario a Notificar" SkinID="etiqueta_negra"></asp:Label></td>
                        <td style="height: 8px">
                            <asp:TextBox ID="txtUsuario" runat="server" ReadOnly="True" SkinID="texto" Enabled="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 202px; height: 8px">
                            <asp:Label ID="Label5" runat="server" Text="Identificación del usuario" SkinID="etiqueta_negra"></asp:Label></td>
                        <td style="height: 8px">
                            <asp:TextBox ID="txtIdentUsuario" runat="server" ReadOnly="True" SkinID="texto" Enabled="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 202px; height: 8px">
                            <asp:Label ID="Label6" runat="server" Text="Estado Actual" SkinID="etiqueta_negra"></asp:Label></td>
                        <td style="height: 8px">
                            <asp:TextBox ID="txtEstadoActual" runat="server" ReadOnly="True" SkinID="texto" Enabled="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 202px; height: 8px">
                            <asp:Label ID="Label12" runat="server" SkinID="etiqueta_negra" Text="Fecha del Estado Actual"></asp:Label></td>
                        <td style="height: 8px">
                            <asp:TextBox ID="txtFechaEstadoActual" runat="server" Enabled="False" ReadOnly="True" SkinID="texto"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 202px; height: 8px">
                            <asp:Label ID="Label7" runat="server" Text="Días para Vencimiento" SkinID="etiqueta_negra"></asp:Label></td>
                        <td style="height: 8px">
                            <asp:TextBox ID="txtDiasVence" runat="server" ReadOnly="True" SkinID="texto" Enabled="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 202px; height: 8px">
                            <asp:Label ID="Label8" runat="server" Text="Dato proviene del sistema de notificación" SkinID="etiqueta_negra" Width="199px"></asp:Label></td>
                        <td style="height: 8px">
                            <asp:TextBox ID="txtDatoPDI" runat="server" ReadOnly="True" SkinID="texto" Enabled="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 202px; height: 8px">
                            <asp:Label ID="Label9" runat="server" Text="ID del proceso de Notificación" SkinID="etiqueta_negra"></asp:Label></td>
                        <td style="height: 8px">
                            <asp:TextBox ID="txtIDProcesoNot" runat="server" ReadOnly="True" SkinID="texto" Enabled="False"></asp:TextBox></td>
                    </tr>
                </table>
            </fieldset>
<fieldset>  
<legend>Estado Notificaciòn</legend>          
   <TABLE style="WIDTH: 70%"><TBODY>
    <tr>
                                    <td colspan="2" class="titleUpdate">
    <asp:Label id="lblId" tabIndex=1 runat="server" Visible="False" meta:resourcekey="lblIdResource1">-1</asp:Label></td>
                                </tr>
       <tr>
           <td align="right" style="height: 8px; width: 202px;">
        <asp:Label ID="lbl_titulo_principal" runat="server" Text="Crear Estado" SkinID="etiqueta_negra" meta:resourcekey="lbl_titulo_principalResource1" Width="152px"></asp:Label></td>
           <td style="height: 8px">
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlEstado"
                   ErrorMessage="RequiredFieldValidator" ValidationGroup="crear">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="rfvEstado" runat="server" ControlToValidate="ddlEstado" ErrorMessage="Seleccione el estado" meta:resourcekey="rfvFechaActoResource1"
                   Text="*" ValidationGroup="crear" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
       </tr>
       <tr>
           <td align="right" style="width: 202px; height: 8px">
               <asp:Label ID="lblEstado1" runat="server" Font-Names="Arial" Font-Size="Smaller" Height="9px"
                   Text="Estado:" Width="152px" meta:resourcekey="lblEstadoResource1" SkinID="etiqueta_negra"></asp:Label></td>
           <td style="height: 8px">
               <asp:DropDownList ID="ddlEstado" runat="server" SkinID="lista_desplegable" TabIndex="3" Width="78px" meta:resourcekey="ddlEstadoResource1" ValidationGroup="crear">
               </asp:DropDownList><asp:CompareValidator ID="cfvEstado" runat="server" ControlToCompare="txtValorCompara"
                   ControlToValidate="ddlEstado" ErrorMessage="Seleccione el estado" Operator="GreaterThan"
                   ValidationGroup="crear" ValueToCompare="-1">*</asp:CompareValidator></td>
       </tr>
       <tr>
           <td align="right" style="height: 49px; width: 202px;">
               <asp:Label ID="lblFechaCrear" runat="server" Font-Names="Narkisim" Font-Size="Smaller"
                   Height="9px" Text="Fecha del Estado:" Width="152px" meta:resourcekey="lblFechaActoResource1" SkinID="etiqueta_negra"></asp:Label></td>
           <td style="height: 49px">               
               <asp:TextBox ID="txtFechaEstado" runat="server" TabIndex="5" Width="160px" ValidationGroup="crear" SkinID="texto_corto"></asp:TextBox>               
                 <cc1:MaskedEditExtender ID="mskFechaHora" Mask="99/99/9999 99:99" runat="server"
                MaskType="DateTime" AcceptAMPM="False" UserDateFormat="DayMonthYear" UserTimeFormat="None"
                TargetControlID="txtFechaEstado" Enabled="True">
                </cc1:MaskedEditExtender>
                
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFechaEstado"
                   ErrorMessage="Digite la fecha del estado" ValidationGroup="crear" Width="12px">*</asp:RequiredFieldValidator>
                   
               <br />
               <cc1:CalendarExtender ID="calFechaEstado" runat="server" Format="dd/MM/yyyy HH:mm" TargetControlID="txtFechaEstado">
               </cc1:CalendarExtender>
               <%--<asp:UpdatePanel ID="uplFecha" runat="server">
                   <ContentTemplate>
                       &nbsp;
                   </ContentTemplate>
               </asp:UpdatePanel>--%>
           </td>
       </tr>
       <tr>
           <td align="left" style="width: 202px; height: 23px">
           </td>
           <td style="height: 23px">
               <asp:CheckBox ID="chkEnviarCorreo" runat="server" Checked="True" SkinID="check" Text="Enviar correo" /></td>
       </tr>
       <tr>
           <td align="right" style="width: 202px; height: 7px">
               <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="Smaller" Height="9px"
                   meta:resourcekey="lblFechaActoResource1" Text="Adjuntar archivo:" Width="152px" SkinID="etiqueta_negra"></asp:Label></td>
           <td style="height: 7px">
               <asp:FileUpload ID="fupAdjunto" runat="server" /></td>
       </tr>
       <tr>
           <td align="right" style="width: 202px; height: 26px">
               <asp:Label ID="lblMensajeCorreo" runat="server" Font-Names="Arial" Font-Size="Smaller"
                   Height="9px" meta:resourcekey="lblFechaActoResource1" Text="Texto de Correo:"
                   Width="152px" SkinID="etiqueta_negra"></asp:Label></td>
           <td style="height: 26px">
               <asp:TextBox ID="txtTextoCorreo" runat="server" Height="114px" SkinID="texto" TextMode="MultiLine"
                   Width="464px"></asp:TextBox></td>
       </tr>
       <tr>
           <td align="right" style="width: 202px; height: 26px">
           </td>
           <td style="height: 26px">
               </td>
       </tr>
       <tr>
           <td align="right" style="height: 26px; width: 202px;">
               </td>
           <td style="height: 26px">
               <asp:Button ID="btnAceptar" runat="server" 
                                              Text="Aceptar" OnClick="btnAceptar_Click" TabIndex="12" meta:resourcekey="btnBuscarResource1" ValidationGroup="crear" SkinID="boton_copia" />
               <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" SkinID="boton_copia" /></td>
       </tr>
       <tr>
           <td align="right" style="width: 202px; height: 26px">
           </td>
           <td style="height: 26px">
           </td>
       </tr>
       
       <tr>
           <td colspan="2">
               &nbsp;</td>
       </tr>
       <tr>
           <td colspan="2" style="height: 50px" align="center">
               <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" meta:resourcekey="lblMensajeResource1"></asp:Label>
               <br />
               <asp:ValidationSummary ID="valResumen" runat="server" DisplayMode="List" meta:resourcekey="valResumenResource1" ValidationGroup="crear" />
           </td>
       </tr>
            </tbody></table>
            </fieldset>
    <br />
</asp:Content>



