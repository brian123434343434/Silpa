<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FacturaPago.aspx.cs" Inherits="PermisosAmbientales_Liquidacion_FacturaPago" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Formato de Pago</title>
    <link href="estilo.css" rel="stylesheet" type="text/css" />
    <script src="../../jquery/jquery.js" type="text/javascript"></script>
    <link href="../../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../../jquery/EstiloGris/jquery-ui.js"  type="text/javascript"></script>
    <link href="css/FacturaPago.css" rel="Stylesheet" />
    <asp:Literal runat="server" ID="ltlMarcaAgua"></asp:Literal>

    <script language="javascript" type="text/javascript">
        $(function () {
            $('#btnImprimir').click(function (e) {
                $('#btnImprimir').hide();
                window.print();
                $('#btnImprimir').show();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="principal">
            <table  class="TablaContenedorFacturaPago">
                <tr>
                    <td>
                        <table class="TablaFacturaPago">
                            <tr>
                                <td rowspan="4" class="LogoFactura">
                                    <img src="../../App_Themes/Img/Anla.png" width="150" alt="escudo gobierno" />
                                </td>
                                <td class="TituloFacturaBold">
                                    <asp:Literal ID="lblTituloTipoDocumento" runat="server" Text="TIPO DE DOCUMENTO"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="TituloFactura">
                                    <asp:Literal ID="lblTituloCorporacion" runat="server" Text="CORPORACIÓN"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="TituloFactura">
                                    <asp:Literal ID="lblTituloNit" runat="server" Text="NIT"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="TituloFacturaBold">
                                    <asp:Literal ID="lblConcepto" runat="server" Text="Concepto"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table class="TablaDatosFactura">
                                        <tr>
                                            <td colspan="2" class="TituloSeccion">
                                                <asp:Literal ID="lblDatosPersonales" runat="server" Text="DATOS PERSONALES"></asp:Literal>
                                            </td>                                                                                                   
                                            <td colspan="2" class="TituloSeccion">
                                                <asp:Literal ID="lblInformacion" runat="server" Text="INFORMACIÓN"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="EtiquetaFormulario">
                                                <asp:Literal ID="lblEtiquetaNombre" runat="server" Text="NOMBRE"></asp:Literal>
                                            </td>
                                            <td class="DatosFormulario">
                                                <asp:Literal ID="lblNombre" runat="server">AAAAAAAAXXXXXXXXXXXXXXXXXXXXXXXXXX</asp:Literal>
                                            </td>                                            
                                            <td class="EtiquetaFormulario">
                                                <asp:Literal ID="lblEtiquetaReferencia" runat="server" Text="REFERENCIA"></asp:Literal>
                                            </td>
                                            <td class="DatosFormulario">
                                                <asp:Literal ID="lblReferencia" runat="server">XXXXXXXXXXXXXXX</asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="EtiquetaFormulario">
                                                <asp:Literal ID="lblEtiquetaIdentificacion" runat="server" Text="IDENTIFICACIÓN"></asp:Literal>
                                            </td>
                                            <td class="DatosFormulario">
                                                <asp:Literal ID="lblIdentificacion" runat="server">BBBBBB</asp:Literal>
                                            </td>
                                            <td class="EtiquetaFormulario">
                                                <asp:Literal ID="lblEtiquetaFechaExp" runat="server" Text="FECHA DE EXPEDICIÓN"></asp:Literal>
                                            </td>
                                            <td class="DatosFormulario">
                                                <asp:Literal ID="lblFechaExpedicion" runat="server">AAAA/MM/DD</asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="EtiquetaFormulario">
                                                <asp:Literal ID="lblEtiquetaDepartamento" runat="server" Text="DEPARTAMENTO"></asp:Literal>
                                            </td>
                                            <td class="DatosFormulario">
                                                <asp:Literal ID="lblDepartamento" runat="server">CCCCC</asp:Literal>
                                            </td>
                                            <td class="EtiquetaFormulario">
                                                <asp:Literal ID="lblEtiquetaFechaOportuno" runat="server" Text="FECHA DE VENCIMIENTO"></asp:Literal>
                                            </td>
                                            <td class="DatosFormulario">
                                                <asp:Literal ID="lblFechaOportuno" runat="server">AAAA/MM/DD</asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="EtiquetaFormulario">
                                                <asp:Literal ID="lblEtiquetaMunicipio" runat="server" Text="MUNICIPIO"></asp:Literal>
                                            </td>
                                            <td class="DatosFormulario">
                                                <asp:Literal ID="lblMunicipio" runat="server">CCCCCCC</asp:Literal>
                                            </td>
                                            <td class="EtiquetaFormulario">
                                                <asp:Literal ID="lblEtiquetaFechaVen" runat="server" Text="FECHA ENVÍO A COBRO COACTIVO"></asp:Literal>
                                            </td>
                                            <td class="DatosFormulario">
                                                <asp:Literal ID="lblFechaVencimiento" runat="server">AAAA/MM/DD</asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="EtiquetaFormulario">
                                                <asp:Literal ID="lblEtiquetaDireccion" runat="server" Text="DIRECCIÓN"></asp:Literal>
                                            </td>
                                            <td class="DatosFormulario">
                                                <asp:Literal ID="lblDireccion" runat="server">XXXXXXXXXX</asp:Literal>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table class="TablaDatosFactura">
                                        <tr>
                                            <td colspan="2" class="TituloSeccion">
                                                <asp:Literal ID="ltlDetalleFactura" runat="server" Text="DETALLE DE PAGO"></asp:Literal>
                                            </td> 
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="grdConceptos" runat="server" Width="100%" AutoGenerateColumns="False" SkinID="GrillaFactura" ShowFooter="true">                                                    
                                                    <Columns>      
                                                         <asp:TemplateField HeaderText = "DESCRIPCION" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="ltlDescripcion" runat="server" Text='<%# Eval("DESCRIPCION") %>'></asp:Literal>
                                                            </ItemTemplate>
                                                             <FooterTemplate>
                                                                 <asp:Literal ID="ltlTotalTitulo" runat="server" Text='TOTAL A PAGAR'></asp:Literal>
                                                             </FooterTemplate>
                                                        </asp:TemplateField>                                                  
                                                        <asp:TemplateField HeaderText = "DESCRIPCION" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="ltlValor" runat="server" Text='<%# Eval("VALOR") %>'></asp:Literal>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                 <asp:Literal ID="ltlTotal" runat="server" Text='111111'></asp:Literal>
                                                             </FooterTemplate>                              
                                                        </asp:TemplateField>                                                  
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>   
                            <tr>
                                <td colspan="2">
                                    <table class="TablaDatosFactura">
                                        <tr>
                                            <td colspan="2" class="TituloSeccion">
                                                <asp:Literal ID="ltlFormPago" runat="server" Text="FORMA DE PAGO"></asp:Literal>
                                            </td> 
                                        </tr>
                                        <tr>
                                            <td class="EtiquetaFormulario">
                                                FORMA DE PAGO
                                            </td>
                                            <td class="CheckFormaPago">
                                                <asp:CheckBox ID="chkCheque" runat="server" Enabled="False" Text="Cheque" />
                                                <asp:CheckBox ID="chkEfectivo" runat="server" Enabled="False" Text="Efectivo" />
                                            </td>
                                        </tr>                                        
                                        <tr>
                                            <td colspan="2">
                                                <table class="TablaDatosPagoEnCheque">
                                                    <tr>
                                                        <td colspan="4"  class="TituloTablaFactura">
                                                            SOLO PARA PAGO EN CHEQUE
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="EtiquetaFormulario">NOMBRE DEL BANCO:</td>
                                                        <td></td>
                                                        <td class="EtiquetaFormulario">CÓDIGO DEL BANCO:</td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="EtiquetaFormulario">CIUDAD DEL CHEQUE:</td>
                                                        <td></td>
                                                        <td class="EtiquetaFormulario">NÚMERO DEL CHEQUE:</td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="EtiquetaFormulario">CIUDAD DEL PAGO:</td>
                                                        <td></td>
                                                        <td class="EtiquetaFormulario">FECHA DE PAGO:</td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="EtiquetaFormulario">TOTAL PAGADO:</td>
                                                        <td colspan="3"></td>                                
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>                                                     
                            <tr>
                                <td colspan="2" class="TextoPieFactura">
                                    <asp:Literal ID="lblDatosCorporacion" runat="server" Text="CORPORACION, NIT,DIRECCION,TELEFONO"></asp:Literal>                                                
                                </td>
                            </tr>
                        </table>
                        <hr class="SeprardarFacturaPago" />
                        <table class="TablaFacturaPago">
                            <tr>
                                <td class="DesprendibleInformacionPago">
                                    <table class="TablaInformacionPago">
                                        <tr>
                                            <td class="EtiquetaFormularioResumen">
                                                <asp:Literal ID="lblNombreCorp1" runat="server" Text="NOMBRE:"></asp:Literal>
                                            </td>
                                            <td class="DatosFormularioResumen">
                                                <asp:Literal ID="lblNombreCorporacion1" runat="server" Text="NOMBRE"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>                                                  
                                            <td class="EtiquetaFormularioResumen">
                                                <asp:Literal ID="lblNitCorp1" runat="server" Text="NIT:"></asp:Literal>
                                            </td>
                                            <td class="DatosFormularioResumen">
                                                <asp:Literal ID="lblNitCorporacion1" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>                                                     
                                            <td class="EtiquetaFormularioResumen">
                                                <asp:Literal ID="lblTelefonoCorp1" runat="server" Text="TELÉFONO:"></asp:Literal>
                                            </td>
                                            <td class="DatosFormularioResumen">
                                                <asp:Literal ID="lblTelefonoCorporacion1" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="EtiquetaFormularioResumen">
                                                <asp:Literal ID="lblNombreBanco1" runat="server" Text="BANCO:"></asp:Literal>
                                            </td>
                                            <td class="DatosFormularioResumen">
                                                <asp:Literal ID="lblNombreBancoField" runat="server" Text="XXXXXXXX"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="EtiquetaFormularioResumen">
                                                <asp:Literal ID="lblTipoCuenta1" runat="server" Text="TIPO DE CUENTA:"></asp:Literal>
                                            </td>
                                            <td class="DatosFormularioResumen">
                                                <asp:Literal ID="lblTipoCuentaField1" runat="server" Text="XXXXXXXX"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="EtiquetaFormularioResumen">
                                                <asp:Literal ID="lblNumeroCuenta" runat="server" Text="NUMERO DE CUENTA:"></asp:Literal>
                                            </td>
                                            <td class="DatosFormularioResumen">
                                                <asp:Literal ID="lblNumeroCuentaField" runat="server" Text="XXXXXXXX"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="EtiquetaFormularioResumen">
                                                <asp:Literal ID="lblEtiquetaReferencia1" runat="server" Text="NUMERO DE REFERENCIA:"></asp:Literal>
                                            </td>
                                            <td class="DatosFormularioResumen">
                                                <asp:Literal ID="lblNumeroReferencia1" runat="server" Text="XXXXXXXX"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="EtiquetaFormularioResumen">
                                                <asp:Literal ID="lblEtiquetaFechaPagoOportuno1" runat="server" Text="FECHA LIMITE DE PAGO:"></asp:Literal>
                                            </td>
                                            <td class="DatosFormularioResumen">
                                                <asp:Literal ID="lblFechaPagoOportuno1" runat="server" Text="AAAA/MM/DD"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trFechaVencimiento1">
                                            <td class="EtiquetaFormularioResumen">
                                                <asp:Literal ID="lblEtiquetaFechaVencimiento1" runat="server" Text="FECHA ENVÍO A COBRO COACTIVO:"></asp:Literal>
                                            </td>
                                            <td class="DatosFormularioResumen">
                                                <asp:Literal ID="lblFechaVencimiento1" runat="server" Text="AAAA/MM/DD"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="EtiquetaFormularioResumen">
                                                <asp:Literal ID="lblEtiquetaTotal1" runat="server" Text="TOTAL A PAGAR:"></asp:Literal>
                                            </td>
                                            <td class="EtiquetaFormularioResumen">
                                                <asp:Literal ID="lblTotalPagar1" runat="server" Text="$XXX.XX"></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td  class="DesprendibleCodigoBarrras">
                                    <table class="TablaCodigoBarras">
                                        <tr>
                                            <td>
                                                <asp:Image ID="imgCodigoBarras1" runat="server" />                                                                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TextoCodigoBarras">
                                                <asp:Literal ID="lblCodigoBarras1" runat="server" Text="CORP000000000020000000"></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>                
                            <tr>
                                <td colspan="2" class="TextoPieFactura">
                                    <asp:Literal ID="lblTiqueteCorporacion" runat="server" Text="AUTORIDAD AMBIENTAL"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="TextoPieFactura">
                                    <asp:Literal ID="lblDatosCorporacion2" runat="server" Text="CORPORACION, NIT,DIRECCION,TELEFONO"></asp:Literal>                                                
                                </td>
                            </tr>
                        </table>
                        <hr class="SeprardarFacturaPago" />
                        <table class="TablaFacturaPago">
                            <tr>
                                <td class="DesprendibleInformacionPago">
                                    <table class="TablaInformacionPago">
                                        <tr>
                                            <td class="EtiquetaFormularioResumen">
                                                <asp:Literal ID="lblNombreCorp2" runat="server" Text="NOMBRE:"></asp:Literal>
                                            </td>
                                            <td class="DatosFormularioResumen">
                                                <asp:Literal ID="lblNombreCorporacion2" runat="server" Text="NOMBRE"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>                                                  
                                            <td class="EtiquetaFormularioResumen">
                                                <asp:Literal ID="lblNitCorp2" runat="server" Text="NIT:"></asp:Literal>
                                            </td>
                                            <td class="DatosFormularioResumen">
                                                <asp:Literal ID="lblNitCorporacion2" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>                                                     
                                            <td class="EtiquetaFormularioResumen">
                                                <asp:Literal ID="lblTelefonoCorp2" runat="server" Text="TELÉFONO:"></asp:Literal>
                                            </td>
                                            <td class="DatosFormularioResumen">
                                                <asp:Literal ID="lblTelefonoCorporacion2" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="EtiquetaFormularioResumen">
                                                <asp:Literal ID="lblNombreBanco2" runat="server" Text="BANCO:"></asp:Literal>
                                            </td>
                                            <td class="DatosFormularioResumen">
                                                <asp:Literal ID="lblNombreBancoField2" runat="server" Text="XXXXXXXX"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="EtiquetaFormularioResumen">
                                                <asp:Literal ID="lblTipoCuenta2" runat="server" Text="TIPO DE CUENTA:"></asp:Literal>
                                            </td>
                                            <td class="DatosFormularioResumen">
                                                <asp:Literal ID="lblTipoCuentaField2" runat="server" Text="XXXXXXXX"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="EtiquetaFormularioResumen">
                                                <asp:Literal ID="lblNroCuenta2" runat="server" Text="NUMERO DE CUENTA:"></asp:Literal>
                                            </td>
                                            <td class="DatosFormularioResumen">
                                                <asp:Literal ID="lblNroCuentaField2" runat="server" Text="XXXXXXXX"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="EtiquetaFormularioResumen">
                                                <asp:Literal ID="lblEtiquetaReferencia2" runat="server" Text="NUMERO DE REFERENCIA:"></asp:Literal>
                                            </td>
                                            <td class="DatosFormularioResumen">
                                                <asp:Literal ID="lblNumeroReferencia2" runat="server" Text="XXXXXXXX"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="EtiquetaFormularioResumen">
                                                <asp:Literal ID="lblEtiquetaFechaPagoOportuno2" runat="server" Text="FECHA LIMITE DE PAGO:"></asp:Literal>
                                            </td>
                                            <td class="DatosFormularioResumen">
                                                <asp:Literal ID="lblFechaPagoOportuno2" runat="server" Text="AAAA/MM/DD"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trFechaVencimiento2">
                                            <td class="EtiquetaFormularioResumen">
                                                <asp:Literal ID="lblEtiquetaFechaVencimiento2" runat="server" Text="FECHA ENVÍO A COBRO COACTIVO:"></asp:Literal>
                                            </td>
                                            <td class="DatosFormularioResumen">
                                                <asp:Literal ID="lblFechaVencimiento2" runat="server" Text="AAAA/MM/DD"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="EtiquetaFormularioResumen">
                                                <asp:Literal ID="lblEtiquetaTotal2" runat="server" Text="TOTAL A PAGAR:"></asp:Literal>
                                            </td>
                                            <td class="EtiquetaFormularioResumen">
                                                <asp:Literal ID="lblTotalPagar2" runat="server" Text="$XXX.XX"></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="DesprendibleCodigoBarrras">
                                    <table class="TablaCodigoBarras">
                                        <tr>
                                            <td style="text-align:center">
                                                <asp:Image ID="imgCodigoBarras2" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TextoCodigoBarras">
                                                <asp:Literal ID="lblCodigoBarras2" runat="server" Text="CORP000000000020000000"></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>                
                            <tr>
                                <td colspan="2" class="TextoPieFactura">
                                    <asp:Literal ID="lblCopiaBanco" runat="server" Text="COPIA BANCO"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="TextoPieFactura">
                                    <asp:Literal ID="lblDatosCorporacion3" runat="server" Text="CORPORACION, NIT,DIRECCION,TELEFONO"></asp:Literal>                                                
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <table width="100%">
                <tr>
                    <td align="center" class="BotonPago">
                        <asp:Button ID="btnImprimir" runat="server" ClientIDMode="Static" Text="Imprimir para pagar en Banco" />
                    </td>
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
    </form>
</body>
</html>
