<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPALimpia.master" AutoEventWireup="true" CodeFile="EmitirDocumento.aspx.cs" Inherits="NotificacionElectronica_EmitirDocumento" Title="EmitirDocumento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
        <div class="div-titulo">
        <asp:Label ID="lbl_titulo_principal" runat="server" Text="EMITIR DOCUMENTO" SkinID="titulo_principal_blanco"></asp:Label>
	</div>
<div class="div-contenido">
<asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    &nbsp;
    <br />
   <TABLE style="WIDTH: 70%"><TBODY>
    <tr>
                                    <td colspan="2" class="titleUpdate">
    <asp:Label id="lblId" tabIndex=1 runat="server" Visible="False">-1</asp:Label></td>
                                </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblNumeroVital" runat="server" Font-Names="Arial" Font-Size="Smaller"
                                                Height="9px" Text="Número VITAL:" Width="130px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNumeroVital" runat="server" TabIndex="1" Width="160px" OnTextChanged="txtNumeroVital_TextChanged" AutoPostBack="True"></asp:TextBox><asp:RequiredFieldValidator
                                                ID="rfvNumeroVital" runat="server" ControlToValidate="txtNumeroVital" ErrorMessage="Ingrese el Número VITAL">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblNumeroExpediente" runat="server" Font-Names="Arial" Font-Size="Smaller" Height="9px"
                                                Text="Número de Proceso Administrativo:" Width="218px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNumeroExpediente" runat="server" Width="160px" TabIndex="2"></asp:TextBox><asp:RequiredFieldValidator
                                                ID="rfvNumeroExpediente" runat="server" ControlToValidate="txtNumeroExpediente"
                                                ErrorMessage="Ingrese el Número de Expediente">*</asp:RequiredFieldValidator></td>
                                    </tr>
       <tr>
           <td align="left">
               <asp:Label ID="lblTipoActo" runat="server" Font-Names="Arial" Font-Size="Smaller" Height="9px"
                   Text="Tipo Acto Administrativo:" Width="182px"></asp:Label></td>
           <td>
               <asp:DropDownList ID="cboTipoActo" runat="server" SkinID="lista_desplegable" TabIndex="3">
               </asp:DropDownList>
               <asp:RequiredFieldValidator ID="rfvTipoActo" runat="server" ControlToValidate="cboTipoActo"
                   ErrorMessage="Seleccione el Tipo de Acto Administrativo" InitialValue="-1">*</asp:RequiredFieldValidator></td>
       </tr>
       <tr>
           <td align="left">
               <asp:Label ID="lblNumeroActo" runat="server" Font-Names="Arial" Font-Size="Smaller" Height="9px"
                   Text="Número Acto Administrativo:" Width="189px"></asp:Label></td>
           <td>
               <asp:TextBox ID="txtNumeroActo" runat="server" TabIndex="4" Width="160px"></asp:TextBox><asp:RequiredFieldValidator
                   ID="rfvNumeroActo" runat="server" ControlToValidate="txtNumeroActo"
                   ErrorMessage="Ingrese el Número de Acto Administrativo">*</asp:RequiredFieldValidator></td>
       </tr>
       <tr>
           <td align="left" style="height: 26px">
               <asp:Label ID="lblFechaActo" runat="server" Font-Names="Arial" Font-Size="Smaller" Height="9px"
                   Text="Fecha Acto Administrativo:" Width="171px"></asp:Label></td>
           <td style="height: 26px">
               <asp:TextBox ID="txtFechaActo" runat="server" TabIndex="5" Width="160px"></asp:TextBox><asp:RequiredFieldValidator
                   ID="rfvFechaActo" runat="server" ControlToValidate="txtFechaActo"
                   ErrorMessage="Ingrese la Fecha del Acto Administrativo">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="revFechaActo" runat="server" ControlToValidate="txtFechaActo"
                   ErrorMessage="Formato de fecha no valido para la Feecha del Acto Administrativo"
                   ValidationExpression="^\d{2}\/\d{2}\/\d{4}">*</asp:RegularExpressionValidator>
               <cc1:CalendarExtender ID="calFechaActo" runat="server" Enabled="True" Format="dd/MM/yyyy"
                   TargetControlID="txtFechaActo">
               </cc1:CalendarExtender>
           </td>
       </tr>
       <tr>
           <td align="left">
               <asp:Label ID="lblDescripcionActo" runat="server" Font-Names="Arial" Font-Size="Smaller" Height="9px"
                   Text="Descripción Acto Administrativo:" Width="191px"></asp:Label></td>
           <td>
               <asp:TextBox ID="txtDescripcionActo" runat="server" TabIndex="6" TextMode="MultiLine" Width="90%"></asp:TextBox><asp:RequiredFieldValidator
                   ID="rfvDescripcionActo" runat="server" ControlToValidate="txtDescripcionActo"
                   ErrorMessage="Ingrese la Descripción del Acto Administrativo">*</asp:RequiredFieldValidator></td>
       </tr>
       <TR><TD><asp:Label id="lblAdjuntar" runat="server" Text="Adjuntar Documento:" Width="133px"></asp:Label> </TD><TD>
           <asp:FileUpload ID="uplAdjuntar" runat="server" /></TD></TR>
       <tr>
           <td>
           </td>
           <td>
               <asp:Button ID="btnAdjuntar" runat="server" CausesValidation="False" 
                                              Text="Adjuntar" OnClick="btnAdjuntar_Click" TabIndex="12" /><asp:Button ID="btnCancelar" runat="server" CausesValidation="False" Text="Eliminar" OnClick="btnCancelar_Click" TabIndex="13" /></td>
       </tr>
       <tr>
           <td>
               <asp:Label ID="lblDocumento" runat="server" Text="Acto Administrativo:" Width="133px"></asp:Label></td>
           <td>
               <asp:ListBox ID="lstDocumento" runat="server" TabIndex="14" SelectionMode="Multiple">
           </asp:ListBox></td>
       </tr>
       <tr>
           <td>
           </td>
           <td>
           </td>
       </tr>
       <tr>
           <td>
               <asp:Label ID="lblPersonas" runat="server" Font-Names="Arial" Font-Size="Smaller" Height="9px"
                   Text="Lista de Personas a Notificar:" Width="185px"></asp:Label></td>
           <td>
               <asp:DropDownList ID="cboPersonas" runat="server" SkinID="lista_desplegable" TabIndex="11">
               </asp:DropDownList></td>
       </tr>
            <tr>
                                    <td colspan="" >
                                    </td>
                                      <td >
                                          <asp:Button ID="btnAdicionar" runat="server" CausesValidation="False" 
                                              Text="Agregar" OnClick="btnAdicionar_Click" TabIndex="12" />
                                          <asp:Button ID="btnQuitar" runat="server" CausesValidation="False" Text="Quitar" OnClick="btnQuitar_Click" TabIndex="13" /></td>
                                </tr>
       <tr>
           <td>
           </td>
           <td>
               <asp:ListBox ID="lstPersonas" runat="server" TabIndex="14" SelectionMode="Multiple"></asp:ListBox></td>
       </tr>
       <tr>
           <td colspan="2">
           </td>
       </tr>
       <tr>
           <td colspan="2">
         <table width="100%"><tbody> 
         <TR><TD style="HEIGHT: 29px; width: 25%;"><DIV style="WIDTH: 100px; HEIGHT: 13px"></DIV></TD>
         <TD style="HEIGHT: 29px; width: 50%;" align="center">&nbsp;<DIV style="WIDTH: 58px; HEIGHT: 14px">
         <asp:Button id="btnAceptar" tabIndex=15 onclick="btnAceptar_Click" runat="server" Text="Aceptar"></asp:Button></DIV></TD><TD style="WIDTH: 25%; HEIGHT: 29px"><DIV style="WIDTH: 100px; HEIGHT: 13px"></DIV></TD></TR></TBODY></TABLE>
           </td>
       </tr>
       <tr>
           <td colspan="2" style="height: 50px">
               <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
               <br />
               <asp:ValidationSummary ID="valResumen" runat="server" DisplayMode="List" />
           </td>
       </tr>
            </tbody></table>
    <br />
    </div>
</asp:Content>



