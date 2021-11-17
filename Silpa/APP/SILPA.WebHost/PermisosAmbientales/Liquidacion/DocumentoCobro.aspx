<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="DocumentoCobro.aspx.cs" Inherits="PermisosAmbientales_Liquidacion_DocumentoCobro" EnableSessionState="True" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<div class="SubHeaderBack">
		
	<div class="pageHeader">
	   <span class="specialsBtn" style="width:800px; text-align:center;"><asp:Label ID="lbl_titulo" runat="server" SkinID="titulo_principal_blanco" Text="Consulta Estado de Liquidación" /></span>   
		<div class="subBannerPhoto">
		  
		</div>
	    
	</div>
</div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <table style="position: static; top: 100px; left: 100px; font-family: Tahoma;">
                    <tr>
                        <td style="width: 325px">
                            
                        </td>
                        <td style="width: 180px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 325px">
                            Número SILPA:
                        </td>
                        <td style="width: 180px; text-align: right;">
                            <asp:TextBox ID="txt_numeroSILPA" runat="server" Text="" SkinID="texto" Style="margin-left: 0px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 325px">
                        </td>
                        <td style="width: 180px; text-align: right;">
                            <asp:Button ID="btn_consultar" runat="server" Text="Consultar" SkinID="boton" OnClick="btn_consultar_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table>
                    <tr>
                        <td style="width:325px;">
                            <asp:Label ID="lbl_estado" runat="server" Text="" SkinID="text"></asp:Label>
                        </td>
                        <td style="width:180px; text-align:right;">
                            <asp:Button ID="btn_generar" runat="server" Text="Ver Documento" SkinID="boton" Visible="false"
                                OnClick="btn_generar_Click" />
                        </td>
                        
                    </tr>
                </table>
                <br />
                 </div>
        </ContentTemplate>
    </asp:UpdatePanel>
   
</asp:Content>
