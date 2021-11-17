<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DC.aspx.cs" Inherits="PermisosAmbientales_Liquidacion_DC"
    EnableTheming="false" EnableSessionState="True" uiCulture="es-CO"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Generación Formulario de pago</title>
    <script type="text/javascript" language="javascript">
        function imprimir() {
            window.print();
        }
</script> 
    <style type="text/css">
        .style1
        {
            height: 40px;
            width: 601px;
        }
        .style2
        {
            height: 30px;
            width: 601px;
        }
        
        </style>
    <link href="factura.css" rel="stylesheet" type="text/css" />
</head>
<body bgcolor="#ffffff">
    <form id="form1" runat="server">
    <div id="titulo">
        <asp:Label ID="lbl_titulo_pagina" runat="server" Text="Generación de Documento de Cobro"
            SkinID="titulo_principal" Font-Names="Tahoma" ForeColor="#808080"></asp:Label>
    </div>
    <div id="documento" 
        
        style="border-color:inherit; border-width:thin; border-style:dashed; position:absolute; background-color:White;
        left:10px; right:11px; top:30px; bottom:-1074px; width:950px; height:1300px;">
        
        <div id="encabezado" class="dcobro" style="position: absolute; left:174px; top: 30px;
            width: 665px; height: 150px;">
            <table>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lbl_titulo" runat="server" Text="FORMULARIO DE PAGO" Font-Bold="True"
                            Font-Names="Tahoma" Font-Size="20px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:Label ID="lbl_nombre_corporacion" runat="server" Text="CORPORACIÓN" Font-Names="Tahoma"
                            Font-Size="X-Large"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:Label ID="lbl_nit_corporacion" runat="server" Text="NIT" Font-Names="Tahoma"
                            Font-Size="X-Large"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:Label ID="lbl_concepto" runat="server" Text="CONCEPTO" Font-Names="Tahoma" Font-Size="Large"></asp:Label>
                    </td>
                </tr>
            </table>
             </div>
             
           <div style="position:absolute; top:40px; left:63px; height:117px; width:100px;">
           	  <img height="117" src="../../App_Themes/Img/escudo-colombia.jpg" width="100" alt="escudo gobierno" /></div>  
        <div id="solicitante" style="width: 450px; position: absolute; left: 30px; top: 200px;
            height: 105px;">
            <table class="tableborderfull" style="width: 450px; height: 105px">
                <!-- MSTableType="layout" -->
                <thead>
                    <tr class="fondoThead">
                        <td style="width: 150px; height: 21px;">
                            <asp:Label ID="lbl_datos_solicitante" runat="server" Text="Datos del Solicitante"
                                Font-Names="Tahoma"></asp:Label>
                        </td>
                        <td style="height: 21px; width: 247px">
                            <asp:Label ID="lbl_ciudad" runat="server" Font-Names="Tahoma" Font-Size="X-Small"
                                Text="CIUDAD" Visible="False"></asp:Label></td>
                    </tr>
                </thead>
                <tbody>
                    <tr class="tableborder3">
                        <td style="width: 150px">
                            <asp:Label ID="lbl_nombre" runat="server" Text="NOMBRE" Font-Names="Tahoma"
                                Font-Size="X-Small"></asp:Label>
                        </td>
                        <td style="height: 22px; width: 247px;">
                            <asp:Label ID="lbl_nombre_solicitante" runat="server" Text="NOMBRE SOLICITANTE" Font-Names="Tahoma"
                                Font-Size="X-Small"></asp:Label>
                        </td>
                    </tr>
                    <tr class="tableborder3">
                        <td style="width: 150px">
                            <asp:Label ID="lbl_identificacion" runat="server" Text="IDENTIFICACIÓN" Font-Names="Tahoma"
                                Font-Size="X-Small"></asp:Label>
                        </td>
                        <td style="height: 21px; width: 247px;">
                            <asp:Label ID="lbl_identificacion_solicitante" runat="server" Text="IDENTIFICACIÓN"
                                Font-Names="Tahoma" Font-Size="X-Small"></asp:Label>
                        </td>
                    </tr>
                    <tr class="tableborder3">
                        <td style="width: 150px">
                            <asp:Label ID="lbl_departamento_etiqueta" runat="server" Text="DEPARTAMENTO:" Font-Names="Tahoma"
                                Font-Size="X-Small"></asp:Label></td>
                        <td style="height: 22px; width: 247px">
                            <asp:Label ID="lbl_departamento" runat="server" Text="DEPARTAMENTO" Font-Names="Tahoma"
                                Font-Size="X-Small"></asp:Label></td>
                    </tr>
                    <tr class="tableborder3">
                        <td style="width: 150px">
                            <asp:Label ID="lbl_municipio_etiqueta" runat="server" Text="MUNICIPIO:" Font-Names="Tahoma"
                                Font-Size="X-Small"></asp:Label></td>
                        <td style="width: 247px; height: 22px">
                            <asp:Label ID="lbl_municipio" runat="server" Text="MUNCIPIO" Font-Names="Tahoma"
                                Font-Size="X-Small"></asp:Label></td>
                    </tr>
                    <tr class="tableborder3">
                        <td style="width: 150px; height: 24px">
                            <asp:Label ID="Label1" runat="server" Text="DIRECCIÓN:" Font-Names="Tahoma"
                                Font-Size="X-Small"></asp:Label></td>
                        <td style="width: 247px; height: 24px">
                            <asp:Label ID="lbl_direccion_solicitante" runat="server" Font-Names="Tahoma" Font-Size="X-Small"
                                Text="DIRECCIÓN"></asp:Label></td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="referencia" style="position: absolute; left: 600px; top: 200px; width: 300px;
            height: 40px">
            <table class="tableborderfull" style="height: 40px; width: 300px">
                <tr>
                    <td class="fondoThead" style="width: 110px">
                        <asp:Label ID="lbl_numero" runat="server" Text="No. Referencia:" />
                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lbl_numero_referencia" runat="server" Text="XXXXXXXXXX" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="fecha" style="position: absolute; left: 600px; top: 250px; width: 300px;
            height: 80px">
            <table class="tableborderfull" style="height: 80px; width: 300px">
                <tr>
                    <td class="fondoThead" style="width: 110px">
                        <asp:Label ID="lbl_fecha1" runat="server" Text="Fecha de Expedición:" />
                    </td>
                    <td style="text-align: center" class="tableborderfull">
                        <asp:Label ID="lbl_fecha_expedicion" runat="server" Text="2000/00/00" CssClass="tablebordernone" />
                    </td>
                </tr>
                <tr>
                    <td class="fondoThead" style="width: 110px">
                        <asp:Label ID="lbl_fecha2" runat="server" Text="Fecha de Vencimiento:" />
                    </td>
                    <td style="text-align: center" class="tableborderfull">
                        <asp:Label ID="lbl_fecha_vencimiento" runat="server" Text="2000/00/00" CssClass="tablebordernone" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="ubicacion" style="position: absolute; left: 30px; top: 350px">
        </div>
        <%--<div id="detalle" style="position: absolute; left: 30px; top: 480px; height: 105px;">--%>
        <div id="detalle" style="position: absolute; left: 30px; top: 400px; height: 105px;">
            <table class="tableborderfull" style="width: 890px; height: 105px">
                <!-- MSTableType="layout" -->
                <thead>
                    <tr class="fondoThead">
                        <td style="width: 850px; height: 20px; text-align: center;">
                            <asp:Label ID="lbl_detalle_etiqueta" runat="server" Text="Descripción" Font-Names="Tahoma"></asp:Label>
                        </td>
                        <td style="height: 20px; width: 247px; text-align: center;">
                            <asp:Label ID="lbl_valor_etiqueta" runat="server" Text="Valor" Font-Names="Tahoma"></asp:Label>
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td class="left10">
                            <asp:Label runat="server" Text="--CONCEPTO 1--" ID="lbl_concepto1"></asp:Label>
                        </td>
                        <td class="right10">
                            <asp:Label runat="server" Text="$XXXX.XX" ID="lbl_precio1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="left10">
                            <asp:Label runat="server" Text="--CONCEPTO 2--" ID="lbl_concepto2"></asp:Label>
                        </td>
                        <td class="right10">
                            <asp:Label runat="server" Text="$XXXX.XX" ID="lbl_precio2"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="left10">
                            <asp:Label runat="server" Text="--CONCEPTO 3--" ID="lbl_concepto3"></asp:Label>
                        </td>
                        <td class="right10">
                            <asp:Label runat="server" Text="$XXXX.XX" ID="lbl_precio3"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="total" style="position: absolute; top: 515px; left: 610px; width: 310px;
            height: 105px;">
            <table style="font-family: Tahoma; font-size: 12px; width: 310px;">
                <tr>
                    <td style="width: 97px">
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" Text="$XXXXX.XX" ID="lbl_subtotal"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 97px">
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" Text="$XXX.XX" ID="lbl_descuento" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 97px; height: 16px;">
                    </td>
                    <td style="text-align: right; height: 16px;">
                        <asp:Label runat="server" Text="$XXXX.XX" ID="lbl_recargo" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 97px; height: 16px;">
                    </td>
                    <td style="text-align: right; height: 16px;">
                        <asp:Label runat="server" Text="$XXXXX.XX" ID="lbl_iva" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 97px; font-weight: bold">
                        TOTAL A PAGAR:
                    </td>
                    <td style="text-align: right; font-weight: bold">
                        <asp:Label runat="server" Text="$ XXXXX.XX" ID="lbl_total"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        
        <div>
           <table id ="forma_pago" style="position: absolute; left: 30px; top: 620px; width: 171px;height: 23px">
             <tr class="fondoThead" style="width: 200px"> Forma de pago </tr>
             <tr>
               <td style="font-family: Tahoma; font-size: 12px;">
                   <input id="ck_cheque" type="checkbox" disabled="disabled" />Cheque</td>
               <td style="font-family: Tahoma; font-size: 12px;">
                   <input id="ck_efectivo" type="checkbox" checked="CHECKED" disabled="disabled" />Efectivo</td>
             </tr>
           </table>
        </div>
        
        
        
        <div id ="pago_banco"  style="position: absolute; left: 30px; top: 678px; width: 400px;height: 100px"> 
            <table class="tableborderfull"  style="font-family: Tahoma; font-size: 12px;" width="890">
            <tr class="fondoThead" style="width: 200px"> 
                 Solo para pago en cheque
            </tr>
            <tr style="width: 200px"> 
                <td style="font-family: Tahoma; font-size: 12px;">
                    Nombre del banco:
                </td>
                <td style="font-family: Tahoma; font-size: 12px;">
                    Código:
                </td>
                <td style="font-family: Tahoma; font-size: 12px;">
                    Ciudad del cheque:
                </td>
                <td style="font-family: Tahoma; font-size: 12px;">
                    Número del cheque:
                </td>
            </tr>
            <tr>
                    <td style="font-family: Tahoma; font-size: 12px;">
                        Ciudad del pago:
                    </td>
                    <td style="font-family: Tahoma; font-size: 12px;">
                        Fecha de pago:
                    </td>
                    <td style="font-family: Tahoma; font-size: 12px;">
                        Total pagado:
                    </td>
             </tr>
           </table>  
        </div>
         
        <div id="firma_corporacion" style="position: absolute; left: 81px; top: 499px; width: 400px;
            height: 100px">
            <span id="foto_firma" style="position: absolute; left: 100px; width: 200px; height: 65px">
                &nbsp;</span><span id="datos_firma" style="position: absolute; top: 75px; width: auto;
                left: 20px; right: 20px; text-align: center;">
                <asp:Label runat="server" ID="lbl_representate_corporacion" Visible="False" Width="20px"></asp:Label>
            </span>
        </div>
        <div id="tiquete_corporacion" style="border-width: thin; position: absolute; top: 850px;
            left: 25px; width: 900px; height: 180px; border-top-style: dashed; right: 25px;">
            <span id="codigo_barras" style="position: absolute; left: 500px; top: 70px; bottom: 38px;
                right: 10px; text-align: center; vertical-align: text-bottom;">&nbsp;<asp:Label ID="lbl_codigo_barras1"
                    runat="server" Font-Names="3 of 9 barcode" Text="CORP000000000020000000" Font-Size="30px"></asp:Label>
            </span><span id="letras_codigo_barras" style="position: absolute; left: 500px; top: 110px;
                bottom: 20px; right: 10px; text-align: center;">
                <asp:Label ID="lbl_numero_barras1" runat="server" Text="CORP000000000020000000"></asp:Label>
            </span>
            <div id="tiquete_info" style="position: absolute; left: 10px; top: 10px; bottom: 20px;
                right: 410px;">
                <table id="tabla_corporacion" class="tableborderfull" width="480px">
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="--CORPORACION--" ID="lbl_nombre_corporacion2"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="--NIT--" ID="lbl_nit_corporacion2"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="--TELÉFONO--" ID="lbl_telefono_corporacion2"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table id="tabla_total" class="tableborderfull" width="480px">
                    <tr>
                        <td class="left18" style="width: 225px">
                            NUMERO DE REFERENCIA:
                        </td>
                        <td class="right18">
                            <asp:Label runat="server" ID="lbl_numero_referencia2" Text="XXXXXXXX" />
                        </td>
                    </tr>
                    <tr>
                        <td class="left18" style="width: 225px">
                            FECHA LÍMITE DE PAGO:
                        </td>
                        <td class="right18">
                            <asp:Label runat="server" ID="lbl_fecha_vencimiento2" Text="0000/00/00" />
                        </td>
                    </tr>
                </table>
                <table id="tabla_total" class="tableborderfull" width="480px">
                    <tr>
                        <td class="left18Bold">
                            TOTAL A PAGAR
                        </td>
                        <td class="right18Bold">
                            <asp:Label runat="server" Text="$ XXX.XX" ID="lbl_total2"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="t_corporacion" style="position: absolute; left: 300px; right: 300px; top: 165px;
                height: 15px; text-align: center; font-family: tahoma; font-size: 10px; font-weight: bold;
                color: #666666;">
                CORPORACION
            </div>
        </div>
        <div id="tiquete_banco" style="border-width: thin; position: absolute; top: 1050px;
            left: 25px; width: 900px; height: 180px; border-top-style: dashed; right: 25px;">
            <span id="codigo_barras" style="position: absolute; left: 500px; top: 70px; bottom: 38px;
                right: 10px; text-align: center; vertical-align: text-bottom;">&nbsp;<asp:Label ID="lbl_codigo_barras2"
                    runat="server" Font-Names="3 of 9 barcode" Text="CORP000000000020000000" Font-Size="30px"></asp:Label>
            </span><span id="letras_codigo_barras" style="position: absolute; left: 500px; top: 110px;
                bottom: 20px; right: 10px; text-align: center;">
                <asp:Label ID="lbl_numero_barras2" runat="server" Text="CORP000000000020000000"></asp:Label>
            </span>
            <div id="tiquete_info" style="position: absolute; left: 10px; top: 10px; bottom: 20px; right: 410px;">
                <table id="tabla_corporacion" class="tableborderfull" width="480px">
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="--CORPORACION--" ID="lbl_nombre_corporacion3"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="--NIT--" ID="lbl_nit_corporacion3"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="--TELÉFONO--" ID="lbl_telefono_corporacion3"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table id="tabla_total1" class="tableborderfull" width="480px">
                    <tr>
                        <td class="left18" style="width: 225px">
                            NUMERO DE REFERENCIA:
                        </td>
                        <td class="right18">
                            <asp:Label runat="server" ID="lbl_numero_referencia3" Text="XXXXXXXX" />
                        </td>
                    </tr>
                    <tr>
                        <td class="left18" style="width: 225px">
                            FECHA LÍMITE DE PAGO:
                        </td>
                        <td class="right18">
                            <asp:Label runat="server" ID="lbl_fecha_vencimiento3" Text="0000/00/00" />
                        </td>
                    </tr>
                </table>
                <table id="tabla_total2" class="tableborderfull" width="480px">
                    <tr>
                        <td class="left18Bold">
                            TOTAL A PAGAR
                        </td>
                        <td class="right18Bold">
                            <asp:Label runat="server" Text="$ XXX.XX" ID="lbl_total3"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="t_banco" style="position: absolute; left: 300px; right: 300px; top: 165px;
                height: 15px; text-align: center; color: #666666; font-weight: bold; font-family: tahoma;
                font-size: 10px;">
                BANCO
            </div>
        </div>
        <div id="documento_pie1" 
                     style="position: absolute; top: 825px; left: 165px; right: -660px;
            text-align: center; height: 20px; width:600px; color: #666666; font-size: 10px; font-family: tahoma;">
            <asp:Label ID="datos_corporacion1" runat="server" Text="CORPORACION, NIT. DIRECCIÓN, TELEFONO"></asp:Label>
        </div>
        <div id="documento_pie2" 
                     
                     style="position: absolute; top:1242px; left: 165px; right: -660px;
            text-align: center; height: 20px; width:600px; color: #666666; font-size: 10px; font-family: tahoma;">
            <asp:Label ID="datos_corporacion2" runat="server" Text="CORPORACION, NIT. DIRECCIÓN, TELEFONO"></asp:Label>
        </div>
    
    <div id="pago" style="position: absolute; top: 1340px; left: 300px">
        <table align="center">
            <tr>
                <td align="center">
                    <asp:Button runat="server" Text="Pago Electrónico" Font-Names="Tahoma" Width="185px"
                        ID="btn_pagar" onclick="btn_pagar_Click"></asp:Button>
                </td>
                <td align="center">
                    <input type="button" value="Imprimir para pagar en bancos" style="Width:185px; font-family:Tahoma" id="btn_imprimir" onclick="javascript:imprimir()"/>
                    
                </td>
            </tr>
        </table>
    </div>
                            <asp:Label ID="lbl_ciudad_solicitante_etiqueta" runat="server" Text="CIUDAD:" Font-Names="Tahoma"
                                Font-Size="X-Small" Visible="False"></asp:Label>
                            <asp:Label ID="lbl_ciudad_solicitante" runat="server" Text="CIUDAD" Font-Names="Tahoma"
                                Font-Size="X-Small" Visible="False"></asp:Label></div>
    </form>
</body>

</html>
