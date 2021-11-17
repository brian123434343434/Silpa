<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPALimpia.master" AutoEventWireup="true" CodeFile="FinalizarTransaccion.aspx.cs" Inherits="PagoElectronico_PaginaError" Title="Finalizar Transacción" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <table width="100%" >
    <tr>
    
        <td colspan="3">
            <div class="div-titulo">
                <asp:Label ID="lblTituloPrincipal" runat="server" Text="FINALIZAR TRANSACCION" SkinID="titulo_principal_blanco"></asp:Label>
            </div>
        </td>
        
    </tr>        
        <tr>
            <td width="15%"></td>
            <td align="center"  >
                <asp:Label ID="lblError" runat="server" SkinID="subtitulo" Text="En este momento su {REF} presenta un proceso de pago cuya transacción se encuentra PENDIENTE de recibir confirmación por parte de su entidad financiera, por favor espere unos minutos y vuelva a consultar mas tarde para verificar si su pago fue confirmado de forma exitosa. {INFO} y pregunte por el estado de la transacción: {CUS}"  Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" ></asp:Label>
            </td>
            <td width="15%" ></td>
        </tr>
        
        <tr>
            <td width="15%"></td>   
            <td align="right">            
			    <asp:Label ID="Label1" runat="server" Text="Su Transacción ha Finalizado Cierre Esta Ventana"></asp:Label>
            </td>             
            <td width="15%"></td>
       </tr>      
       <tr>
            <td width="15%"></td>  
            <td align="right">            
			<asp:Button id="Button1" style="Z-INDEX: 103; LEFT: 254px; TOP: 72px" runat="server" Height="33px" Width="143px" Text="Cerrar" OnClick="btnRegresar_Click" SkinID="boton"></asp:Button>            
            </td>      
            <td width="15%"></td>         
       </tr>      
    </table>
	
</asp:Content>

