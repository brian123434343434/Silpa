<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RadicarDocumentosDAA.aspx.cs" Inherits="LicenciasAmbientales_LPA_02a_Evaluar_Necesidad_DAA" MasterPageFile="~/plantillas/SILPA.master" enableEventValidation="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="div-titulo">
 <asp:Label ID="Label2" runat="server" Text="ADJUNTAR DOCUMENTO DE DIAGNOSTICO" SkinID="titulo_principal_blanco"></asp:Label>
</div>
  <form id="form1">  
    
 <div class="div-contenido">
 
 
    
    <table border="0" id="table1" style="border-collapse: collapse" width="558">
        <tr>
            <td colspan="3">
        </td>
        </tr>
        <tr>
            <td colspan="3">
               &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="3">
        <asp:Label ID="Label9" runat="server" Text="Anexe la información o el archivo de DAA:"></asp:Label></td>
        </tr>
		<tr>
			<td colspan="3">
                </td>
			</tr>
		<tr>
			<td colspan="2">&nbsp;<asp:Label ID="Label5" runat="server" Text="Número de radicación:"></asp:Label></td>
			<td style="width: 366px"><input type="text" name="T5" style="width: 150px"/></td>
			</tr>
		<tr>
			<td colspan="2">&nbsp;<asp:Label ID="Label6" runat="server" Text="Fecha de radicación:"></asp:Label></td>
			<td style="width: 366px">
                <asp:TextBox ID="txtFechaRadicacion" runat="server" Enabled="False" Width="277px"></asp:TextBox></td>
		</tr>
		<tr>
			<td colspan="3">
                &nbsp;</td>
			</tr>
		<tr>
			<td>&nbsp;<asp:Label ID="Label7" runat="server" Text="B) Adjuntar documento:"></asp:Label></td>
			<td colspan="2"><input type="file" name="F1" style="width: 284px"/></td>
			</tr>
        <tr>
            <td>
            </td>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td style="height: 24px">
                <asp:Label ID="lblFormularios" runat="server" Text="Formularios Disponibles:"></asp:Label></td>
            <td colspan="2" style="height: 24px">
                <asp:DropDownList ID="ddlFormularios" runat="server" Width="158px">
                    <asp:ListItem>Fus - DAA</asp:ListItem>
                    <asp:ListItem>Apoderado</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="3">
        </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3" style="height: 18px">
            </td>
        </tr>
	</table>

	<p align="left">
        <asp:Button ID="Button1" runat="server" Text="Aceptar" Width="90px" OnClick="Button1_Click" /></p>
     
    
    </form>
</div>
</asp:Content>
