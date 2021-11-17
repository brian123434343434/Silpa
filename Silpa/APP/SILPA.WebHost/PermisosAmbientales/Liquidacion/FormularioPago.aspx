<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormularioPago.aspx.cs" Inherits="PermisosAmbientales_Liquidacion_FormularioPago" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Generación Formulario de Pago</title>
    <link href="estilo.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        function imprimir() {
            window.print();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="principal">
            <table cellspacing="30px" style="height: 1370px">
                <tr>
                    <td valign="top" style="height: 1287px; text-align: center;">
                        <table class="borde-tabla" cellspacing="10px">
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td valign="middle" style="width: 100px">
                                                <img src="../../App_Themes/Img/Escudo-Colombia2.jpg" width="100" alt="escudo gobierno" />
                                            </td>
                                            <td align="center" valign="middle" style="width: 900px">
                                                <asp:Label ID="lblTituloTipoDocumento" runat="server" Text="TIPO DE DOCUMENTO" Font-Bold="True"
                                                    Font-Names="Tahoma" Font-Size="20px"></asp:Label><br />
                                                <br />
                                                <br />
                                                <asp:Label ID="lblTituloCorporacion" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                                    Font-Size="X-Large" Text="CORPORACIÓN"></asp:Label><br />
                                                <br />
                                                <br />
                                                <asp:Label ID="lblTituloNit" runat="server" Text="NIT" Font-Names="Tahoma" Font-Size="X-Large"
                                                    Font-Bold="False"></asp:Label><br />
                                                <br />
                                                <br />
                                                <asp:Label ID="lblConcepto" runat="server" Font-Names="Tahoma" Font-Size="Large"
                                                    Text="Concepto"></asp:Label><br />
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td class="titulo-modulo">
                                                &nbsp; &nbsp;&nbsp; &nbsp;<asp:Label ID="lblDatosPersonales" runat="server" Text="Datos Personales"
                                                    Font-Bold="True" Font-Names="Tahoma" Font-Size="14px"></asp:Label>
                                            </td>
                                            <td class="titulo-modulo">
                                                &nbsp; &nbsp;&nbsp; &nbsp;<asp:Label ID="lblInformacion" runat="server" Text="Información"
                                                    Font-Bold="True" Font-Names="Tahoma" Font-Size="14px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 113px">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblEtiquetaNombre" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="NOMBRE"></asp:Label></td>
                                                        <td align="left">
                                                            <asp:Label ID="lblNombre" runat="server" Font-Names="Tahoma" Font-Size="11px">AAAAAAAAXXXXXXXXXXXXXXXXXXXXXXXXXX</asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblEtiquetaIdentificacion" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="IDENTIFICACIÓN"></asp:Label></td>
                                                        <td align="left">
                                                            <asp:Label ID="lblIdentificacion" runat="server" Font-Names="Tahoma" Font-Size="11px">BBBBBB</asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblEtiquetaDepartamento" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="DEPARTAMENTO"></asp:Label></td>
                                                        <td align="left">
                                                            <asp:Label ID="lblDepartamento" runat="server" Font-Names="Tahoma" Font-Size="11px">CCCCC</asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblEtiquetaMunicipio" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="MUNICIPIO"></asp:Label></td>
                                                        <td align="left">
                                                            <asp:Label ID="lblMunicipio" runat="server" Font-Names="Tahoma" Font-Size="11px">CCCCCCC</asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblEtiquetaDireccion" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="DIRECCIÓN"></asp:Label></td>
                                                        <td align="left">
                                                            <asp:Label ID="lblDireccion" runat="server" Font-Names="Tahoma" Font-Size="11px">XXXXXXXXXX</asp:Label></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="height: 113px">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 289px" align="left">
                                                            <asp:Label ID="lblEtiquetaReferencia" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="REFERENCIA"></asp:Label></td>
                                                        <td align="left">
                                                            <asp:Label ID="lblReferencia" runat="server" Font-Names="Tahoma" Font-Size="11px">XXXXXXXXXXXXXXX</asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 289px" align="left">
                                                            <asp:Label ID="lblEtiquetaFechaExp" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="FECHA DE EXPEDICIÓN"></asp:Label></td>
                                                        <td align="left">
                                                            <asp:Label ID="lblFechaExpedicion" runat="server" Font-Names="Tahoma" Font-Size="11px">AAAA/MM/DD</asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 289px" align="left">
                                                            <asp:Label ID="lblEtiquetaFechaVen" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="FECHA DE VENCIMIENTO"></asp:Label></td>
                                                        <td align="left">
                                                            <asp:Label ID="lblFechaVencimiento" runat="server" Font-Names="Tahoma" Font-Size="11px">AAAA/MM/DD</asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 289px" align="left">
                                                            &nbsp;</td>
                                                        <td align="left">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="grdConceptos" runat="server" Width="100%" AutoGenerateColumns="False">
                                                    <HeaderStyle BackColor="#006600" ForeColor="White" />
                                                    <Columns>
                                                        <asp:BoundField DataField="DESCRIPCION" HeaderText="    Descripci&#243;n" >
                                                            <HeaderStyle BackColor="White" CssClass="titulo-modulo" Font-Names="Tahoma" Font-Size="14px"
                                                                ForeColor="Black" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="VALOR" HeaderText="Valor" >
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle BackColor="White" CssClass="titulo-modulo" Font-Names="Tahoma" Font-Size="14px"
                                                                ForeColor="Black" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="2">
                                                <asp:Label ID="lblEtiquetaTotal" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                    Font-Size="12px" Text="TOTAL A PAGAR"></asp:Label>
                                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<asp:Label ID="lblTotal" runat="server"
                                                    Font-Bold="True" Font-Names="Tahoma" Font-Size="12px"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="titulo-modulo">
                                                &nbsp; &nbsp;&nbsp; &nbsp;<asp:Label ID="lblFormaPago" runat="server" Text="Forma de Pago"
                                                    Font-Bold="True" Font-Names="Tahoma" Font-Size="14px"></asp:Label>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td style="height: 67px">
                                                            &nbsp;<asp:CheckBox ID="chkCheque" runat="server" Enabled="False" Font-Bold="False" />
                                                        </td>
                                                        <td style="height: 67px">
                                                            <asp:Label ID="lblCheque" runat="server" Font-Names="Tahoma" Font-Size="12px" Text="Cheque"></asp:Label>
                                                        </td>
                                                        <td style="height: 67px">
                                                            <asp:CheckBox ID="chkEfectivo" runat="server" Enabled="False" />
                                                        </td>
                                                        <td style="height: 67px">
                                                            <asp:Label ID="lblEfectivo" runat="server" Font-Names="Tahoma" Font-Size="12px" Text="Efectivo"></asp:Label>            
                                                        </td>
                                                    </tr>                                                                                                                                                                                                                                                                                                                     
                                                </table>                                          
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="titulo-cheque" colspan="2">
                                                &nbsp; &nbsp; &nbsp;<asp:Label ID="lblSoloCheque" runat="server" Font-Bold="True"
                                                    Font-Names="Tahoma" Font-Size="14px" Text="Solo para pago en cheque"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblNombreBanco" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="Nombre del Banco:"></asp:Label></td>
                                                        <td align="left">
                                                            <asp:Label ID="lblCodigo" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="12px"
                                                                Text="Código:"></asp:Label></td>
                                                        <td align="left">
                                                            <asp:Label ID="lblCiudadCheque" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="Ciudad del Cheque:"></asp:Label></td>
                                                        <td align="left">
                                                            <asp:Label ID="lblNumeroCheque" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="Número del Cheque:"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblCiudadPago" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="Ciudad del Pago:"></asp:Label></td>
                                                        <td align="left">
                                                            <asp:Label ID="lblFechaPago" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="Fecha de Pago:"></asp:Label></td>
                                                        <td align="left">
                                                            <asp:Label ID="lblTotalPagado" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="Total Pagado:"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 539px">
                                    <table style="width: 100%">
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblDatosCorporacion" runat="server" Text="CORPORACION, NIT,DIRECCION,TELEFONO"
                                                    Font-Names="Tahoma" Font-Size="10px"></asp:Label><br />
                                                <hr class="separador" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%; border: 1px solid #000000;">
                                                <table style="width: 100%; border: solid 1px #000000;">
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <asp:Label ID="lblNombreCorp1" runat="server" Text="Nombre: " Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px"></asp:Label>
                                                            <asp:Label ID="lblNombreCorporacion1" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="NOMBRE"></asp:Label></td>
                                                    </tr>
                                                    <tr>                                                  
                                                        <td colspan="2" align="left">
                                                            <asp:Label ID="lblNitCorp1" runat="server" Text="NIT: " Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px"></asp:Label>
                                                            <asp:Label ID="lblNitCorporacion1" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="NOMBRE"></asp:Label></td>
                                                    </tr>
                                                    <tr>                                                     
                                                        <td  colspan="2" align="left" style="border-bottom: solid 1px #000000">
                                                            <asp:Label ID="lblTelefonoCorp1" runat="server" Text="Teléfono: " Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px"></asp:Label>
                                                            <asp:Label ID="lblTelefonoCorporacion1" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="NOMBRE"></asp:Label></td>
                                                    </tr>
                                                     <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblNombreBanco1" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="BANCO:"></asp:Label></td>
                                                        <td align="right">
                                                            <asp:Label ID="lblNombreBancoField" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="XXXXXXXX"></asp:Label></td>
                                                    </tr>
                                                     <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblNumeroCuenta" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="NUMERO DE CUENTA:"></asp:Label></td>
                                                        <td align="right">
                                                            <asp:Label ID="lblNumeroCuentaField" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="XXXXXXXX"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblEtiquetaReferencia1" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="NUMERO DE REFERENCIA:"></asp:Label></td>
                                                        <td align="right">
                                                            <asp:Label ID="lblNumeroReferencia1" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="XXXXXXXX"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblEtiquetaFechaPago1" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="FECHA LIMITE DE PAGO:"></asp:Label></td>
                                                        <td align="right">
                                                            <asp:Label ID="lblFechaPago1" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="AAAA/MM/DD"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="border-bottom: solid 1px #000000">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblEtiquetaTotal1" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="TOTAL A PAGAR:"></asp:Label></td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalPagar1" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="$XXX.XX"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 50%;">
                                                <asp:Image ID="imgCodigoBarras1" runat="server" Width="510px" Height="70px" />                                                
                                                <asp:Label ID="lblCodigoBarras1" runat="server" Text="CORP000000000020000000"></asp:Label></td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%">
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblTiqueteCorporacion" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                    Font-Size="10px" Text="AUTORIDAD AMBIENTAL"></asp:Label><br />
                                                <hr class="separador" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%; border: 1px solid #000000;">
                                                <table style="width: 100%; border: solid 1px #000000;">
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <asp:Label ID="lblNombreCorp2" runat="server" Text="Nombre: " Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px"></asp:Label>
                                                            <asp:Label ID="lblNombreCorporacion2" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="NOMBRE"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <asp:Label ID="lblNitCorp2" runat="server" Text="NIT: " Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px"></asp:Label>
                                                            <asp:Label ID="lblNitCorporacion2" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="NOMBRE"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left" style="border-bottom: solid 1px #000000">
                                                            <asp:Label ID="lblTelefonoCorp2" runat="server" Text="Teléfono: " Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px"></asp:Label>
                                                            <asp:Label ID="lblTelefonoCorporacion2" runat="server" Font-Bold="False" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="NOMBRE"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblNombreBanco2" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="BANCO:"></asp:Label></td>
                                                        <td align="right">
                                                            <asp:Label ID="lblNombreBancoField2" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="XXXXXXXX"></asp:Label></td>
                                                    </tr>
                                                     <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblNroCuenta1" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="NUMERO DE CUENTA:"></asp:Label></td>
                                                        <td align="right">
                                                            <asp:Label ID="lblNroCuentaField2" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="XXXXXXXX"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblEtiquetaReferencia2" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="NUMERO DE REFERENCIA:"></asp:Label></td>
                                                        <td align="right">
                                                            <asp:Label ID="lblNumeroReferencia2" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="XXXXXXXX"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblEtiquetaFechaPago2" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="FECHA LIMITE DE PAGO:"></asp:Label></td>
                                                        <td align="right">
                                                            <asp:Label ID="lblFechaPago2" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="AAAA/MM/DD"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="border-bottom: solid 1px #000000">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblEtiquetaTotal2" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="TOTAL A PAGAR:"></asp:Label></td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalPagar2" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12px" Text="$XXX.XX"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 50%;">
                                                <asp:Image ID="imgCodigoBarras2" runat="server" Width="510px" Height="70px" />                                                
                                                <asp:Label ID="lblCodigoBarras2" runat="server" Text="CORP000000000020000000"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblTiqueteBanco" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                    Font-Size="10px" Text="BANCO"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblDatosCorporacion2" runat="server" Text="CORPORACION, NIT,DIRECCION,TELEFONO"
                                                    Font-Names="Tahoma" Font-Size="10px"></asp:Label>
                                                <asp:Label ID="lblPpeCertificateSubject" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="12px"
                                                    Visible="False"></asp:Label>
                                                <asp:Label ID="lblPpeUrl" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="12px"
                                                    Visible="False"></asp:Label>
                                                <asp:Label ID="lblPpeCode" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="12px"
                                                    Visible="False"></asp:Label>                                               
                                                <asp:Label ID="lblRazonSocial" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="12px"
                                                    Visible="False"></asp:Label>
                                                <asp:Label ID="lblValorTotal" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="12px"
                                                    Visible="False"></asp:Label>
                                                <asp:Label ID="lblDescripcion" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="12px"
                                                    Visible="False"></asp:Label>
                                                <asp:Label ID="lblNumeroSilpa" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="12px"
                                                    Visible="False"></asp:Label>
                                                <asp:Label ID="lblTransaccion" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="12px" Visible="False"
                                                    ></asp:Label>
                                                <asp:Label ID="lblEstado" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="12px" Visible="False"
                                                    ></asp:Label>
                                                <asp:Label ID="lblTipDoc" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="12px"
                                                    Visible="False"></asp:Label>
                                                <asp:Label ID="lblNumeDoc" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="12px"
                                                    Visible="False"></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        &nbsp;
                        <asp:ImageButton ID="ImgPse" runat="server" ImageUrl="~/App_Themes/Img/PSE/headerImage1.png" OnClick="ImgPse_Click" /></td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="center" style="height: 47px">
                        <asp:Label id="lblTextoInformacion" runat="server" Font-Size="18px" Font-Names="Tahoma" Font-Bold="False" ForeColor="Red"></asp:Label>
                    </td>
                </tr>                
            </table>         
        </div>
        <div style="text-align: center">
            <asp:Button ID="btnPagoElectronico" runat="server" Text="Pago Electrónico" SkinID="boton_copia" OnClick="btnPagoElectronico_Click" />
            <asp:Button ID="btnImprimir" runat="server" Text="Imprimir para pagar en Banco" OnClick="btnImprimir_Click"
                SkinID="boton_copia" />
        </div>
    </form>
</body>
</html>
