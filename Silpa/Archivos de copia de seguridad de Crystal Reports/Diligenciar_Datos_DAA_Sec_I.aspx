<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="Diligenciar_Datos_DAA_Sec_I.aspx.cs" Inherits="LicenciasAmbientales_Default2" enableEventValidation="false"Title="Untitled Page" Theme="skin" %>
<%@ Register Src ="~/controles/ctlBasicData.ascx" TagName="ctlBasicData" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="SubHeaderBack">
	                                                                <div class="pageHeader">
		                                                                <div class="subBannerPhoto"></div>
		                                                                    <h2> &nbsp;<asp:Label ID="LBL_TITULO_FORMULARIO" runat="server" Font-Names="Arial" Font-Size="10pt"
                    Text="FORMULARIO ÚNICO NACIONAL DE SOLICITUD DE PERMISOS AMBIENTALES" Width="492px"></asp:Label></h2>
		                                                                    <p>
                <asp:Label ID="LBL_DECRETO" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Base legal: Ley 99 de 1993, Decreto 1541 DE 1978, Decretos 02 de 1982, Decreto  948 de 1995 y Decreto 1594 de 1984."
                    Width="706px"></asp:Label>&nbsp;</p>
		                                                                    <p>&nbsp;</p>
		                                                                    <p>&nbsp;</p>
		                                                            </div>
		                                                            <div> 
		                                                                <div align="right" class="ltGreen Estilo2">
                                                                            &nbsp;</div>
		                                                            </div>
                                                                </div>
                                                                <div class="copy">	
                                                                 <!--- BODY START --->
    <table style="width: 731px; height: 110px">
        <tr>
            <td colspan="4" style="height: 35px">
		                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td width="52%">&nbsp;<asp:Label ID="LBL_SECCION_I" runat="server" Text="SECCION 1. DATOS DEL SOLICITANTE"
                    Width="314px" ForeColor="#004000"></asp:Label></td>
                                                                                    <td width="48%"><div align="center"><strong>   &nbsp; </strong></div></td>
                                                                                </tr>
                                                                            </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 35px">
                <div>
                    <asp:RadioButtonList ID="RDG_LST_PERSONA" runat="server" Height="142px"
                        Width="137px">
                        <asp:ListItem Value="1">Persona Natural</asp:ListItem>
                        <asp:ListItem Value="2">Persona Jur&#237;dica</asp:ListItem>
                        <asp:ListItem Value="3">P&#250;blica</asp:ListItem>
                        <asp:ListItem Value="4">Privada</asp:ListItem>
                    </asp:RadioButtonList>
                    <uc1:ctlBasicData ID="Ctl_BASIC_DATA_RAZON_SOCIAL" runat="server" />
                    <uc1:ctlBasicData ID="Ctl_BASIC_DATA_REPRESENTANTE_LEGAL" runat="server" />
                    <br />
                    <uc1:ctlBasicData ID="Ctl_BASIC_DATA_APODERADO" runat="server" />
                    
                   
                <br />
                <table style="width: 794px">
                    <tr>
                        <td style="height: 4px">
                            <div>
                                <asp:Label ID="LBL_CALIDAD_ACTUA" runat="server" Text="Calidad en la que Actúa" Width="150px"></asp:Label></div>
                        </td>
                        <td style="width: 3px; height: 4px;">
                        </td>
                        <td style="width: 4px; height: 4px;">
                        </td>
                        <td style="height: 4px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                &nbsp;</div>
                            <div>
                                <asp:RadioButtonList ID="RDG_LST_CALIDAD_ACTUA" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem>Propietario</asp:ListItem>
                                    <asp:ListItem>Arrendatario</asp:ListItem>
                                    <asp:ListItem>Poseedor</asp:ListItem>
                                    <asp:ListItem>Otro</asp:ListItem>
                                </asp:RadioButtonList></div>
                        </td>
                        <td style="width: 3px">
                        </td>
                        <td style="width: 4px">
                            <asp:Label ID="Label1" runat="server" Height="11px" Text="¿ Cual ?" Width="54px"></asp:Label></td>
                        <td>
                            <div>
                                <asp:TextBox ID="TXT_OTRA_CALIDAD_ACTUA" runat="server" Width="137px"></asp:TextBox></div>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px">
                        </td>
                        <td style="width: 3px; height: 21px">
                        </td>
                        <td style="width: 4px; height: 21px">
                        </td>
                        <td style="height: 21px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="width: 3px">
                            <div>
                                &nbsp;</div>
                        </td>
                        <td style="width: 4px">
                            <div>
                                <asp:Button ID="BTN_SIGUIENTE" runat="server" Text="Siguiente" OnClick="BTN_SIGUIENTE_Click" />&nbsp;</div>
                        </td>
                        <td>
                        <asp:Image ID="img1" SkinID="img_gobierno" runat="server" />
                        </td>
                    </tr>
                </table>
                </div>
            </td>
        </tr>
    </table>
     <!--- BODY END --->
                                                                </div>
								                                <div>
								                                 <asp:Image ID="imgCopyBtm" runat="server" Height="5px" ImageUrl="~/App_Themes/img/CopyBtm.jpg"
                                        Width="802" />
								                       </div>       
</asp:Content>

