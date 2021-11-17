<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Radicar_FUS_Documentacion_Magnetico.aspx.cs" Inherits="LicenciasAmbientales_LPA_06_Radicar_FUS_Documentacion_Magnetico" MasterPageFile="~/plantillas/SILPA.master" enableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    <div>
        <table style="width: 923px; height: 89px">
            <tr>
                <td colspan="3">
                    <asp:Label ID="LBL_SOL_LIC" runat="server" Text="SOLICITUD DE LICENCIAS"></asp:Label></td>
            </tr>
            
            <tr>
                <td colspan="3" style="height: 4px">
                    <div>
                        <asp:Button ID="BTN_RADICAR_FUS" runat="server" Text="Radicar FUS" OnClientClick="alert('Su número de radición es: SILPA-001009\nFECHA:01.03.2009 - HORA:10:10 a.m.\nEjecuta el CU-GN-01- Emitir Acto administrativo  con el tipo de acto “Auto de inicio”');" />
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <br />
      </div>
    

</asp:Content>