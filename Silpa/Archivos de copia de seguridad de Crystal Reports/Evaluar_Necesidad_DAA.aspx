<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Evaluar_Necesidad_DAA.aspx.cs" Inherits="CU_03" MasterPageFile="~/plantillas/SILPA.master" enableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<form id="form1">
    <div>
        <table style="width: 53%">
            <tr>
                <td colspan="3" rowspan="1" align="center" >
                    <asp:Label ID="LBL_SOL_LIC" runat="server" Text="Solicitud de Licencias"
                        Width="878px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3" rowspan="3" style="height: 25px">
                    &nbsp;<asp:Label ID="LBL_REQ_DAA" runat="server" Text="El sistema determina que:"></asp:Label>
                    <br />
                    <br />
                    <div>
                        <asp:Button ID="BTN_REQ_DAA" runat="server" Text="Se Requiere DAA" Width="189px" OnClientClick="alert('Se inicia el caso de uso CU-GN-01- Emitir Acto administrativo con el tipo de acto administrativo “Auto que define necesidad DAA”.');" OnClick="Button1_Click" />&nbsp;
                        <asp:Button ID="BTN_NO_REQ_DAA" runat="server" Text="No se Requiere DAA" Width="211px" OnClick="Button2_Click" />
                    </div>
                    
                 </td>
                    
            </tr>
            
        </table>
    
    </div>
</form>


</asp:Content>
