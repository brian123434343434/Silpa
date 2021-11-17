<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="PaginaError.aspx.cs" Inherits="PagoElectronico_PaginaError" Title="Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="SubHeaderBack">
	<div class="pageHeader">
		<div class="subBannerPhoto"></div>
		<p>&nbsp;</p>
		<%--TITLE--%>
		<p> &nbsp;<span class="specialsBtn">Página de Error</span></p>
		<%--/TITLE--%>		
		<p>&nbsp;</p>		
	</div>
</div>
<div class="copy">
<%--BODY--%>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblError" runat="server" Text=" ¡Error en la transacción...!" Font-Size="XX-Large" Height="1000px"></asp:Label></td>
        </tr>
        <tr>
            <td >
            <asp:Image id="imgAlerta" style="Z-INDEX: 102; LEFT: 34px; TOP: 27px" runat="server" ImageUrl="App_Themes/Img/alert2.gif" Width="33px" Height="28px"></asp:Image>
			<asp:Button id="btnRegresar" style="Z-INDEX: 103; LEFT: 254px; TOP: 72px" runat="server" Height="33px" Width="143px" Text="Regresar" OnClick="btnRegresar_Click" SkinID="boton"></asp:Button>            
            </td>             
       </tr>      
    </table>
 <%--/BODY--%>

</div>			
</asp:Content>

