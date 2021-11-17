<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Evaluar_Necesidad_DAA_II.aspx.cs" Inherits="LicenciasAmbientales_LPA_02a_Evaluar_Necesidad_DAA" MasterPageFile="~/plantillas/SILPA.master" enableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="div-titulo">
<asp:Label ID="Label1" runat="server" Text="SOLICITUD DE LICENCIAS" SkinID="titulo_principal_blanco"></asp:Label>
</div>
<div class="div-contenido">

  <form id="form1">  
    
 
    
    <table border="0" id="table1" style="border-collapse: collapse" width="558">
        <tr>
            <td colspan="3">
        </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="Label2" runat="server" Text="ADJUNTAR DOCUMENTO DE DIAGNOSTICO"></asp:Label>&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="3">
        <asp:Label ID="Label9" runat="server" Text="Anexe la información o el archivo de DAA:"></asp:Label></td>
        </tr>
		<tr>
			<td colspan="3">
                <asp:Label ID="Label4" runat="server" Text="A) Registrar información de radicación:"></asp:Label></td>
			</tr>
		<tr>
			<td colspan="2">&nbsp;<asp:Label ID="Label5" runat="server" Text="Número de radicación:"></asp:Label></td>
			<td><input type="text" name="T5" size="20"/></td>
			</tr>
		<tr>
			<td colspan="2">&nbsp;<asp:Label ID="Label6" runat="server" Text="Fecha de radicación:"></asp:Label></td>
			<td><input type="text" name="T6" size="20" value="DD/MM/AAAA"/>
               
            </td>
		</tr>
		<tr>
			<td colspan="3">
                <asp:Label ID="Label7" runat="server" Text="B) Adjuntar documento:"></asp:Label></td>
			</tr>
		<tr>
			<td>&nbsp;</td>
			<td colspan="2"><input type="file" name="F1" size="20"/></td>
			</tr>
        <tr>
            <td colspan="3">
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" PostBackUrl="~/LicenciasAmbientales/LPA_02a_Evaluar_Necesidad_DAA.aspx">CONTINUAR</asp:LinkButton></td>
        </tr>
	</table>

	<p align="left"/>
     
    
    </form>
</div>
</asp:Content>
