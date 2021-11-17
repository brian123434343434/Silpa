<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPASinMenu.master" AutoEventWireup="true" CodeFile="ResumenEIAInicio.aspx.cs" Inherits="ResumenEIA_ResumenEIAInicio" Title="Página sin título" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td style="text-align: center" colspan="3">
                <asp:Button ID="btnCrearResumen" runat="server" Text="Crear Resumen EIA" 
                    onclick="btnCrearResumen_Click" SkinID="boton_copia" Width="250px" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center" colspan="3">
                <asp:Button ID="btnAbrirResumen" runat="server" Text="Abrir Resumen EIA" 
                    onclick="btnAbrirResumen_Click" SkinID="boton_copia" 
                    style="text-align: center" Width="250px" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:PlaceHolder ID="plhResumenes" runat="server" Visible ="false" >  
                <table style="width:100%;">
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="titleUpdate" style="text-align: center">
                            </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            seleccione resumen a consultar</td>
                    </tr>
                    <tr>
                        <td class="style1">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:ListBox ID="lstProyectos" runat="server" Height="200px" Width="50%">
                            </asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnAbrirResumenEIA" runat="server" Text="Abrir Resumen EIA" 
                                SkinID="boton_copia" onclick="btnAbrirResumenEIA_Click"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" class="titleUpdate">
                            </td>
                    </tr>
                </table>
            </asp:PlaceHolder>  
                
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <style type="text/css">
        .style1
        {
            text-align: center;
        }
    </style>
</asp:Content>

