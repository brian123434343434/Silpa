<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="Diligenciar_Datos_DAA_Sec_II.aspx.cs" enableEventValidation="false" Inherits="LicenciasAmbientales_LPA_DILIGENCIAR_DATOS_DAA_SEC_II" %>



<%-- 
<%@ Register Src="WUC_Navegador.ascx" TagName="WUC_Navegador" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script language =  "jscript" src ="js/JScript.js"/>
<script language =  "JavaScript" src ="js/JScript.js"/>
--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type ="text/javascript"  language =  "JavaScript" src ="./js/JScript.js"></script>

<%-- 
<html xmlns="http://www.w3.org/1999/xhtml" >
<body>
--%>
    <form id="form1">
    <%--//<script type = "/text/javascript"  language =  "JAVASCRIPT" src ="js/JScript.js"/>
--%>    
    <div>
    
    
    
    
        <table style="width: 381px; height: 144px">
            <tr>
                <td colspan="4">
                    <asp:Label ID="LBL_TITULO_FORMULA" runat="server" Font-Names="Arial" Font-Size="10pt" Text="FORMULARIO ÚNICO NACIONAL DE SOLICITUD DE PERMISOS AMBIENTALES"
                        Width="492px"></asp:Label>
                    <asp:Label ID="LBL_DECRETO" runat="server" Font-Names="Arial"
                            Font-Size="10pt" Text="Base legal: Ley 99 de 1993, Decreto 1541 DE 1978, Decretos 02 de 1982, Decreto  948 de 1995 y Decreto 1594 de 1984."
                            Width="706px"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="4" style="height: 7px">
                </td>
            </tr>
            <tr>
                <td style="height: 7px; width: 310px;">
                    <div>
                        <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                            <b style="mso-bidi-font-weight: normal"><span></span></b><span
                                style="font-size: 10pt; font-family: 'Futura Bk','sans-serif'; mso-bidi-font-family: Arial;
                                mso-ansi-language: ES-CO">
                                <asp:Label ID="LBL_SECCION_II" runat="server" Text="SECCIÓN 2. DATOS DEL PROYECTO"></asp:Label><?xml namespace="" ns="urn:schemas-microsoft-com:office:office"
                                    prefix="o" ?><o:p></o:p></span></p>
                    </div>
                </td>
                <td style="height: 7px; width: 157px;">
                </td>
                <td style="height: 7px; width: 111px;">
                </td>
                <td style="height: 7px; width: 3px;">
                </td>
            </tr>
            <tr>
                <td style="width: 310px; height: 25px">
                    <div style="text-align: right">
                        <asp:Label ID="LBL_NOMBRE_PROYECTO" runat="server" Text="Nombre del Proyecto"></asp:Label></div>
                </td>
                <td style="width: 157px; height: 25px">
                    <div>
                        <asp:TextBox ID="TXT_NOMBRE_PROYECTO" runat="server" Width="100px">SILPA</asp:TextBox></div>
                </td>
                <td style="width: 111px; height: 25px">
                </td>
                <td style="width: 3px; height: 25px;">
                </td>
            </tr>
            <tr>
                <td style="width: 310px; height: 47px">
                    <div>
                        <asp:Label ID="Label1" runat="server" Text="Región"></asp:Label></div>
                    <div>
                        <asp:RadioButtonList ID="RDG_REGION" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">Rural</asp:ListItem>
                            <asp:ListItem>Urbano</asp:ListItem>
                        </asp:RadioButtonList></div>
                </td>
                <td style="width: 157px; height: 47px">
                    <div>
                        &nbsp;
                    </div>
                </td>
                <td style="width: 111px;">
                    <asp:Label ID="Label2" runat="server" Text="Breve Descripción" Width="102px"></asp:Label>
                    <asp:TextBox ID="TXT_DESCRIPCION_REGION" runat="server" Width="100px" TextMode="MultiLine"></asp:TextBox></td>
                <td style="width: 3px; height: 47px;">
                    </td>
            </tr>
            <tr>
                <td style="width: 310px">
                    <div style="text-align: right">
                        <asp:Label ID="Label5" runat="server" Text="Sector"></asp:Label></div>
                </td>
                <td style="width: 157px">
                    <div>
                        <asp:TextBox ID="TXT_SECTOR" runat="server" Width="100px">ELECTRICO</asp:TextBox></div>
                </td>
                <td style="width: 111px; text-align: left;">
                    <asp:Label ID="Label6" runat="server" Text="Tipo de Proyecto" Width="94px"></asp:Label>
                        <asp:TextBox ID="TXT_TIPO_PROYECTO" runat="server" Width="100px"></asp:TextBox></td>
                <td style="width: 3px">
                    <div>
                        &nbsp;</div>
                </td>
            </tr>
            <tr>
                <td style="width: 310px; text-align: right;">
                    <asp:Label ID="Label7" runat="server" Text="Valor del Proyecto ( o Modificación )  $"
                        Width="205px"></asp:Label></td>
                <td style="width: 157px">
                    <asp:TextBox ID="TXT_VALOR" runat="server" Width="100px">100.000.000</asp:TextBox></td>
                <td style="width: 111px">
                </td>
                <td style="width: 3px">
                </td>
            </tr>
            <tr>
                <td style="width: 310px; height: 26px; text-align: right;">
                    <asp:Label ID="Label8" runat="server" Text="Valor en letras" Width="96px"></asp:Label></td>
                <td style="width: 157px; height: 26px">
                    <asp:TextBox ID="TXT_VALOR_LETRAS" runat="server" Width="100px">CIEN MILLONES</asp:TextBox></td>
                <td style="width: 111px; height: 26px">
                </td>
                <td style="width: 3px; height: 26px">
                </td>
            </tr>
            <tr>
                <td style="width: 310px">
                </td>
                <td style="width: 157px">
                </td>
                <td style="width: 111px">
                </td>
                <td style="width: 3px">
                </td>
            </tr>
            <tr>
                <td style="width: 310px">
                    <div>
                        <asp:Label ID="Label9" runat="server" Text="Ubicación de licencia"></asp:Label></div>
                </td>
                <td style="width: 157px">
                </td>
                <td style="width: 111px">
                </td>
                <td style="width: 3px">
                </td>
            </tr>
            <tr>
                <td style="width: 310px; height: 39px;">
                    <div>
                        <asp:Label ID="Label10" runat="server" Text="Departamento"></asp:Label>
                        <asp:DropDownList ID="LST_DEPARTAMENTO" runat="server" Width="167px">
                            <asp:ListItem>ANTIOQUIA</asp:ListItem>
                            <asp:ListItem>BOYACA</asp:ListItem>
                            <asp:ListItem>CUNDINAMARCA</asp:ListItem>
                        </asp:DropDownList></div>
                </td>
                <td style="width: 157px; height: 39px;">
                    <div>
                        <asp:Label ID="Label19" runat="server" Text="Municipio"></asp:Label>
                        <asp:DropDownList ID="LST_MUNICIPIO" runat="server" Width="159px">
                            <asp:ListItem>ABEJORRAL</asp:ListItem>
                            <asp:ListItem>BRICE&#209;O</asp:ListItem>
                            <asp:ListItem>ANAPOIMA</asp:ListItem>
                        </asp:DropDownList></div>
                </td>
                <td style="width: 111px; height: 39px;">
                    <div style="text-align: left">
                        <asp:Label ID="Label20" runat="server" Text="Vereda"></asp:Label><br />
                        <asp:TextBox ID="TXT_VEREDA" runat="server" Width="100px"></asp:TextBox></div>
                </td>
                <td style="width: 3px; height: 39px;">
                    <div>
                        &nbsp;</div>
                </td>
            </tr>
            <tr>
                <td style="width: 310px; height: 40px;">
                    <div style="text-align: right">
                        <asp:Label ID="Label14" runat="server" Text="Corporación"></asp:Label>&nbsp;</div>
                </td>
                <td style="width: 157px; height: 40px;">
                    <div>
                        <asp:TextBox ID="TXT_CORPORACION" runat="server" Width="159px">CAM</asp:TextBox>&nbsp;</div>
                </td>
                <td style="width: 111px; height: 40px;">
                    <div style="text-align: left">
                        &nbsp;<asp:Label ID="Label15" runat="server" Text="Autoridades Ambientales" Width="140px"></asp:Label>
                        <asp:TextBox ID="TXT_AUTORIDADES_AMBIENTALES" runat="server" Width="100px"></asp:TextBox></div>
                </td>
                <td style="width: 3px; height: 40px;">
                    <div>
                        &nbsp;</div>
                </td>
            </tr>
            <tr>
                <td style="width: 310px; text-align: right;">
                    <asp:Label ID="Label16" runat="server" Text="Permisos"></asp:Label></td>
                <td style="width: 157px">
                    <div>
                        <asp:TextBox ID="TXT_PERMISOS" runat="server" Width="161px">MATERIAL DE ARRASTRE</asp:TextBox></div>
                </td>
                <td style="width: 111px">
                </td>
                <td style="width: 3px">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
                <td style="width: 111px">
                </td>
                <td style="width: 3px">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div>
                        <asp:Label ID="Label11" runat="server" Text="Ubicación del Proyecto con régimen jurídico especial:"
                            ></asp:Label></div>
                </td>
                <td style="width: 111px;">
                </td>
                <td style="width: 3px;">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:RadioButtonList ID="RDG_REGIMEN_JURIDICO" runat="server" Width="410px">
                        <asp:ListItem Selected="True">Reserva forestal local municipal</asp:ListItem>
                        <asp:ListItem>Reserva forestal nacional</asp:ListItem>
                        <asp:ListItem>Reserva forestal regional</asp:ListItem>
                        <asp:ListItem>Sistema Nacional de parques nacionales</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td style="width: 310px; height: 21px">
                </td>
                <td style="width: 157px; height: 21px">
                </td>
                <td style="width: 111px; height: 21px">
                </td>
                <td style="width: 3px; height: 21px">
                </td>
            </tr>
            <tr>
                <td style="width: 310px; height: 8px">
                    <div>
                        <asp:Label ID="Label12" runat="server" Text="El proyecto requiere consulta previa:"></asp:Label></div>
                </td>
                <td style="width: 157px; height: 8px">
                </td>
                <td style="width: 111px; height: 8px">
                </td>
                <td style="width: 3px; height: 8px">
                </td>
            </tr>
            <tr>
                <td style="width: 310px">
                </td>
                <td style="width: 157px">
                </td>
                <td style="width: 111px">
                </td>
                <td style="width: 3px">
                </td>
            </tr>
            <tr>
                <td style="width: 310px">
                    <div>
                        <asp:RadioButtonList ID="RDG_CONSULTA_PREVIA" runat="server" Width="234px">
                            <asp:ListItem Selected="True">Audiencias P&#250;blicas</asp:ListItem>
                            <asp:ListItem>Comunidades ind&#237;genas</asp:ListItem>
                            <asp:ListItem>Comunidades negras</asp:ListItem>
                        </asp:RadioButtonList></div>
                </td>
                <td style="width: 157px">
                </td>
                <td style="width: 111px">
                </td>
                <td style="width: 3px">
                </td>
            </tr>
            <tr>
                <td style="width: 310px; height: 21px">
                    <div>
                        <asp:Label ID="Label17" runat="server" Text="Transporte aéreo:"></asp:Label></div>
                </td>
                <td style="width: 157px; height: 21px">
                </td>
                <td style="width: 111px; height: 21px">
                </td>
                <td style="width: 3px; height: 21px">
                </td>
            </tr>
            <tr>
                <td style="width: 310px">
                    <div>
                        <asp:Label ID="Label18" runat="server" Text="Destino:"></asp:Label></div>
                </td>
                <td style="width: 157px">
                    <div>
                        <asp:TextBox ID="TXT_DESTINO" runat="server">BOYACA</asp:TextBox></div>
                </td>
                <td style="width: 111px">
                </td>
                <td style="width: 3px">
                </td>
            </tr>
            <tr>
                <td style="width: 310px; height: 26px;">
                    <div>
                        <asp:Label ID="Label21" runat="server" Text="Ciudad:"></asp:Label></div>
                </td>
                <td style="width: 157px; height: 26px;">
                    <div>
                        <asp:TextBox ID="TXT_CIUDAD" runat="server">TUNJA</asp:TextBox></div>
                </td>
                <td style="width: 111px; height: 26px;">
                </td>
                <td style="width: 3px; height: 26px;">
                </td>
            </tr>
            <tr>
                <td style="width: 310px; height: 26px">
                    <asp:Button ID="BTN_ANEXAR_DOCUMENTOS" runat="server" OnClick="BTN_ANEXAR_DOCUMENTOS_Click" Text="Anexar Documentos" Width="246px" /></td>
                <td style="width: 157px; height: 26px">
                </td>
                <td style="width: 111px; height: 26px">
                </td>
                <td style="width: 3px; height: 26px">
                </td>
            </tr>
            <tr>
                <td style="width: 310px; height: 26px">
                </td>
                <td style="width: 157px; height: 26px">
                    <div style="text-align: right">
                        <asp:Button ID="BTN_ANTEROR" runat="server" OnClick="BTN_ANTEROR_Click" Text="Anterior" Width="71px" />&nbsp;</div>
                </td>
                <td style="width: 111px; height: 26px">
                    <div style="text-align: left"><asp:Button ID="BTN_GUARDAR" runat="server" Text="Guardar" OnClientClick = "messageBox();" OnClick="BTN_GUARDAR_Click" />&nbsp;</div>
                </td>
                <td style="width: 3px; height: 26px">
                        </td>
            </tr>
        </table>
    
    </div>
    </form>
<%-- </body>
</html>
--%>    

</asp:Content>