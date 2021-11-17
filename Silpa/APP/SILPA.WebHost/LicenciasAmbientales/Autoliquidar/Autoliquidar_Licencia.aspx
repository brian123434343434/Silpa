<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Autoliquidar_Licencia.aspx.cs" Inherits="LicenciasAmbientales_LPA_03_Auto_liquidar_valor_de_evaluacion_de_la_licencia" MasterPageFile="~/plantillas/SILPA.master" enableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



    <div>
        <table style="width: 741px">
            <tr>
                <td colspan="3" style="height: 17px">
        <asp:Label ID="Label2" runat="server" Text="SOLICITUD DE LICENCIAS"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3">
        <asp:Label ID="Label1" runat="server" Text="FORMULARIO DE AUTOLIQUIDACION"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3">
        <asp:Button ID="Button1" runat="server" Text="Autoliquidar" OnClientClick="alert(' •  Envía información al aplicativo de la AA mediante Web Service LIC-02\n •  Se inicia el caso de uso CU-GN-08-Monitorear Pago');" OnClick="Button1_Click1" /></td>
            </tr>
        </table>
        <br />
       <br />
        <br />
        </div>


</asp:Content>