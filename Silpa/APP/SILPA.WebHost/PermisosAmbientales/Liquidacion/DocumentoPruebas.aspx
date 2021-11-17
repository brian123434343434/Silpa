<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentoPruebas.aspx.cs" Inherits="PermisosAmbientales_Liquidacion_DocumentoPruebas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>    
    <form id="form1" runat="server">
    <div class="principal">        
        <table cellspacing="30px">
        <tr>
        <td valign="top">
            <table class="borde-tabla" cellspacing="10px">        
            <tr>
            <td>
                <table>
                    <tr>
                        <td valign="middle" style="width:100px">
                            <img src="../../App_Themes/Img/escudo-colombia.jpg" width="100" alt="escudo gobierno" />
                        </td>
                        <td align="center" valign="middle" style="width:900px">                
                            <asp:Label ID="lblTituloTipoDocumento" runat="server" Text="TIPO DE DOCUMENTO" Font-Bold="True" Font-Names="Tahoma" Font-Size="20px"></asp:Label><br />
                            <br />
                            <br />
                            <asp:Label ID="lblTituloCorporacion" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                Font-Size="X-Large" Text="CORPORACIÓN"></asp:Label><br />
                            <br />
                            <br />
                            <asp:Label ID="lblTituloNit" runat="server" Text="NIT" Font-Names="Tahoma" Font-Size="X-Large" Font-Bold="False"></asp:Label><br />
                            <br />
                            <br />
                            <asp:Label ID="lblConcepto" runat="server" Font-Names="Tahoma" Font-Size="Large"
                                Text="Concepto"></asp:Label><br /><br />
                            </td>
                    </tr>
                    </table>
                    <table style="width: 100%;">
                    <tr>                    
                        <td class="titulo-modulo">
                            &nbsp; &nbsp;&nbsp; &nbsp;<asp:Label ID="lblDatosPersonales" runat="server" Text="Datos Personales" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px"></asp:Label>
                            </td>
                        <td class="titulo-modulo">
                            &nbsp; &nbsp;&nbsp; &nbsp;<asp:Label ID="lblInformacion" runat="server" Text="Información" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px"></asp:Label>
                            </td>
                    </tr>                    
                    <tr>
                        <td>
                        <table style="width:100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblEtiquetaNombre" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                        Font-Size="12px" Text="NOMBRE"></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblNombre" runat="server" Font-Names="Tahoma" Font-Size="11px">AAAAAAAAXXXXXXXXXXXXXXXXXXXXXXXXXX</asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                <asp:Label ID="lblEtiquetaIdentificacion" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                    Font-Size="12px" Text="IDENTIFICACIÓN"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblIdentificacion" runat="server" Font-Names="Tahoma" Font-Size="11px">BBBBBB</asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                <asp:Label ID="lblEtiquetaDepartamento" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                    Font-Size="12px" Text="DEPARTAMENTO"></asp:Label></td>
                               <td>
                                    <asp:Label ID="lblDepartamento" runat="server" Font-Names="Tahoma" Font-Size="11px">CCCCC</asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                <asp:Label ID="lblEtiquetaMunicipio" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                    Font-Size="12px" Text="MUNICIPIO"></asp:Label></td>
                                <td>
                                <asp:Label ID="lblMunicipio" runat="server" Font-Names="Tahoma" Font-Size="11px">CCCCCCC</asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                <asp:Label ID="lblEtiquetaDireccion" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                    Font-Size="12px" Text="DIRECCIÓN"></asp:Label></td>
                                <td>
                                <asp:Label ID="lblDireccion" runat="server" Font-Names="Tahoma" Font-Size="11px">XXXXXXXXXX</asp:Label></td>
                            </tr>
                        </table>
                        </td>
                        <td>
                        <table style="width:100%">
                        <tr>
                            <td>                                
                                <asp:Label ID="lblEtiquetaReferencia" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                    Font-Size="12px" Text="REFERENCIA"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblReferencia" runat="server" Font-Names="Tahoma" Font-Size="11px">XXXXXXXXXXXXXXX</asp:Label></td>
                        </tr>                        
                        <tr>                        
                            <td>
                                <asp:Label ID="lblEtiquetaFechaExp" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                    Font-Size="12px" Text="FECHA DE EXPEDICIÓN"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblFechaExpedicion" runat="server" Font-Names="Tahoma" Font-Size="11px">AAAA/MM/DD</asp:Label></td>
                        </tr>
                        <tr>                            
                            <td>
                                <asp:Label ID="lblEtiquetaFechaVen" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                    Font-Size="12px" Text="FECHA DE VENCIMIENTO"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblFechaVencimiento" runat="server" Font-Names="Tahoma" Font-Size="11px">AAAA/MM/DD</asp:Label></td>
                        </tr>
                        <tr>                            
                            <td>
                                &nbsp;</td>
                            <td>
                            </td>
                        </tr>                        
                        </table>
                        </td>                            
                    </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="grdConceptos" runat="server" Width="100%">
                                    <HeaderStyle BackColor="#006600" ForeColor="White" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Label ID="lblEtiquetaTotal" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                    Font-Size="12px" Text="TOTAL A PAGAR"></asp:Label>
                                <asp:Label ID="lblTotal" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="12px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titulo-modulo">
                            &nbsp; &nbsp;&nbsp; &nbsp;<asp:Label ID="lblFormaPago" runat="server" Text="Forma de Pago" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px"></asp:Label>
                            </td>
                            <td>
                                &nbsp;<asp:CheckBox ID="chkCheque" runat="server" Enabled="False" Font-Bold="False" />
                                <asp:Label ID="lblCheque" runat="server" Font-Names="Tahoma" Font-Size="12px" Text="Cheque"></asp:Label>
                                <asp:CheckBox ID="chkEfectivo" runat="server" Enabled="False" />
                                <asp:Label ID="lblEfectivo" runat="server" Font-Names="Tahoma" Font-Size="12px" Text="Efectivo"></asp:Label></td>
                            <td>
                        </tr>
                </table>
                </td>
               </tr>
            </table>    
        </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
