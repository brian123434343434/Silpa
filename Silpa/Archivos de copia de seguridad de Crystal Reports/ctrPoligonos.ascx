<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrPoligonos.ascx.cs" Inherits="ResumenEIA_Controles_ctrPoligonos" %>
<%@ Register src="ctrCoordenadas.ascx" tagname="ctrCoordenadas" tagprefix="uc1" %>    
    <table style="width:100%;">     
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Coordenadas"></asp:Label>
        </td>
        <td colspan="4">
            <uc1:ctrCoordenadas ID="ctrCoordenadasPoligono" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Infraestructura del proyecto que la interviene (Si Aplica)"></asp:Label>
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtInfraestruc" runat="server" Width="99%" SkinID="texto"/>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td style="text-align: center">
            <asp:Button ID="btnAgregarRegistro" runat="server"  SkinID="boton_copia"
                onclick="btnAgregarRegistro_Click" Text="Agregar Poligono" />
        </td>
        <td style="text-align: center">
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" SkinID="boton_copia" OnClick="btnCancelar_Click"/>
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
   <tr>
        <td colspan="5" style="text-align: center">
        <asp:Label ID="lblMensaje" runat="server" ForeColor="red"></asp:Label>
        </td>
    </tr>
        
    <tr>
        <td colspan="5" style="text-align: center">
            <asp:GridView ID="grvPoligonos" runat="server" AutoGenerateColumns="False" OnRowDeleting="grvPoligonos_RowDeleting" Width="100%"> 
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" />                    
                    <asp:BoundField DataField="NUMPOLIGONO" HeaderText="Poligono" />
                    <asp:TemplateField HeaderText="Ubicacion - Coordenadas planas (Datum Magna-Sirgas)">
                        <ItemTemplate>
                            <uc1:ctrCoordenadas ID="ctrCoordenadas" DataGridObject="true" NombreTabla= 'VIEWSTATEPOLIG' NombreCampo='' ValorCampo='<%# Eval("NUMPOLIGONO") %>' ValorCampo2='<%# Eval("NUMPOLIGONO") %>' runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>   
                    <asp:BoundField DataField="DESCPOLIGONO" HeaderText="Infraestructura del proyecto que la interviene (Si Aplica)" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
