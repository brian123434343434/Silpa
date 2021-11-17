<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPALimpia.master" AutoEventWireup="true" CodeFile="ConsultarEmitirDocumento.aspx.cs" Inherits="NotificacionElectronica_ConsultarEmitirDocumento" Title="Consultar Emitir Documento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
        <div class="div-titulo">
        <asp:Label ID="lbl_titulo_principal" runat="server" Text="CONSULTAR EMITIR DOCUMENTO" SkinID="titulo_principal_blanco"></asp:Label>
	</div>
<div class="div-contenido">
<asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    &nbsp;
    <br />
   <TABLE style="WIDTH: 70%"><TBODY>
    <tr>
                                    <td colspan="2" class="titleUpdate">
    </td>
                                </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblNumeroVital" runat="server" Font-Names="Arial" Font-Size="Smaller"
                                                Height="9px" Text="Número VITAL:" Width="130px"></asp:Label>
                                        </td>
                                        <td style="width: 415px">
                                            <asp:TextBox ID="txtNumeroVital" runat="server" TabIndex="1" Width="160px" ></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblNumeroExpediente" runat="server" Font-Names="Arial" Font-Size="Smaller" Height="9px"
                                                Text="Número de Proceso Administrativo:" Width="218px"></asp:Label>
                                        </td>
                                        <td style="width: 415px">
                                            <asp:TextBox ID="txtNumeroExpediente" runat="server" Width="160px" TabIndex="2"></asp:TextBox></td>
                                    </tr>
       <tr>
           <td align="left">
               <asp:Label ID="lblTipoActo" runat="server" Font-Names="Arial" Font-Size="Smaller" Height="9px"
                   Text="Tipo Acto Administrativo:" Width="182px"></asp:Label></td>
           <td style="width: 415px">
               <asp:DropDownList ID="cboTipoActo" runat="server" SkinID="lista_desplegable" TabIndex="3">
               </asp:DropDownList>
               </td>
       </tr>
       <tr>
           <td align="left">
               <asp:Label ID="lblNumeroActo" runat="server" Font-Names="Arial" Font-Size="Smaller" Height="9px"
                   Text="Número Acto Administrativo:" Width="189px"></asp:Label></td>
           <td style="width: 415px">
               <asp:TextBox ID="txtNumeroActo" runat="server" TabIndex="4" Width="160px"></asp:TextBox></td>
       </tr>
       <tr>
           <td align="left" style="height: 26px">
               <asp:Label ID="lblFechaDesdeActo" runat="server" Font-Names="Arial" Font-Size="Smaller" Height="9px"
                   Text="Fecha Desde Acto Administrativo:" Width="171px"></asp:Label></td>
           <td style="height: 26px; width: 415px;">
               <asp:TextBox ID="txtFechaDesdeActo" runat="server" TabIndex="5" Width="160px"></asp:TextBox>
               <asp:RegularExpressionValidator ID="revFechaDesdeActo" runat="server" ControlToValidate="txtFechaDesdeActo"
                   ErrorMessage="Formato de fecha no valido para la Feecha del Acto Administrativo"
                   ValidationExpression="^\d{2}\/\d{2}\/\d{4}">*</asp:RegularExpressionValidator>
               <cc1:CalendarExtender ID="calFechaDesdeActo" runat="server" Enabled="True" Format="dd/MM/yyyy"
                   TargetControlID="txtFechaDesdeActo">
               </cc1:CalendarExtender>
           </td>
       </tr>
       <tr>
           <td align="left" style="height: 26px">
               <asp:Label ID="lblFechaHastaActo" runat="server" Font-Names="Arial" Font-Size="Smaller" Height="9px"
                   Text="Fecha Hasta Acto Administrativo:" Width="171px"></asp:Label></td>
           <td style="height: 26px; width: 415px;">
               <asp:TextBox ID="txtFechaHastaActo" runat="server" TabIndex="5" Width="160px"></asp:TextBox>
               <asp:RegularExpressionValidator ID="revFechaHastaActo" runat="server" ControlToValidate="txtFechaHastaActo"
                   ErrorMessage="Formato de fecha no valido para la Feecha del Acto Administrativo"
                   ValidationExpression="^\d{2}\/\d{2}\/\d{4}">*</asp:RegularExpressionValidator>
               <cc1:CalendarExtender ID="calFechaHastaActo" runat="server" Enabled="True" Format="dd/MM/yyyy"
                   TargetControlID="txtFechaHastaActo">
               </cc1:CalendarExtender>
           </td>
       </tr>
       <tr>
           <td align="center" colspan="2" style="height: 26px">
               &nbsp;<asp:ValidationSummary ID="valResumen" runat="server" DisplayMode="List" />
           </td>
       </tr>
       <tr>
           <td align="center" colspan="2" style="height: 26px">
               <asp:Button ID="btnConsultar" runat="server" OnClick="btnConsultar_Click" SkinID="boton_copia"
                   Text="Buscar" /></td>
       </tr>
       <tr>
           <td colspan="2">
<asp:Panel id="pnlDocumentos" runat="server" Visible="False" Width="820px" ScrollBars="Both" Height="200px">
<asp:GridView id="grdDocumentos" runat="server" SkinID="Grilla_simple_peq" Width="800px" OnRowCommand="grdDocumentos_RowCommand" RowStyle-HorizontalAlign="Left" CellSpacing="1" CellPadding="2" DataKeyNames="EMD_ID" AutoGenerateColumns="False">
<RowStyle HorizontalAlign="Left"></RowStyle>
<Columns>
<asp:BoundField DataField="EMD_ID">
<HeaderStyle Width="2%"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="EMD_NUMERO_SILPA" HeaderText="N&#250;mero VITAL">
<HeaderStyle Width="8%"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="EMD_NUMERO_EXPEDIENTE" HeaderText="N&#250;mero Proceso Administrativo">
<HeaderStyle Width="8%"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="NOMBRE_DOCUMENTO" HeaderText="Tipo Acto Administrativo">
<HeaderStyle Width="16%"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="EMD_NUMERO_ACTO" HeaderText="N&#250;mero Acto Administrativo">
<HeaderStyle Width="8%"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="EMD_FECHA_ACTO" HeaderText="Fecha Acto Administrativo" DataFormatString="{0:dd/MM/yyyy}">
<HeaderStyle Width="8%"></HeaderStyle>
</asp:BoundField>
<asp:ButtonField CommandName="VerDetalle" Text="Ver Detalle">
<HeaderStyle Width="8%"></HeaderStyle>
</asp:ButtonField>
</Columns>
</asp:GridView> </asp:Panel> 
           </td>
       </tr>
            </tbody></table>
    <br />
    </div>
</asp:Content>





