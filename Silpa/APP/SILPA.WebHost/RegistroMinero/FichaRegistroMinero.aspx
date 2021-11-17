<%@ Page Language="C#" AutoEventWireup="true" Theme="" CodeFile="FichaRegistroMinero.aspx.cs"
    Inherits="RegistroMinero_FichaRegistroMinero" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ficha Registro Minero</title>

    <style type="text/css">
        .EstiloTabla
        {
            font-family: Tahoma;
            font-size: 8.5pt;
            height: 50px;
            width: 700px;
            border-collapse: collapse;
        }
        .pie
        {
            font-family: Tahoma;
            font-size: 8.5pt;
            font-weight: bold;
            width: 700px;
        }
        .fila
        {
            font-family: Tahoma;
            font-size: 8.5pt;
            background-color: #F7F6F3;
        }
        .filaAlter
        {
            font-family: Tahoma;
            font-size: 8.5pt;
            color: #284775;
        }
        
        .Label
        {
            font-family: Tahoma;
            font-size: 8.5pt;
        }
        .style1
        {
            font-family: Tahoma;
            font-size: 8.5pt;
            font-weight: bold;
            width: 700px;
            text-align: justify;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div id="seleccion">
       <table align="center">
            <tr>
                <td class="style1">
                   De acuerdo con la resolución 1978 del 09 de Noviembre de 2012 del Ministerio de 
                    Ambiente y Desarrollo Sostenible, en relación con la función delegada de consolidar y administrar la información suministrada por las autoridades ambientales regionales y urbanas, la Autoridad Nacional de Licencias 
                    Ambientales informa:
                </td>
            </tr>
            <tr>
                <td class="pie">
                  
                </td>
            </tr>
            <tr>
                <td class="pie">
                    <asp:Label ID="Lcabecera" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table class="EstiloTabla" align="center" cellspacing="0" border="1">
            <tr class="fila">
                <td style="width: 50%">
                    Tipo Registro:
                </td>
                <td style="width: 50%">
                    <asp:Label ID="LtipoRegistro" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="filaAlter">
                <td>
                    Nro. Acto Administrativo:
                </td>
                <td>
                    <asp:Label ID="LnroActoAdministrativo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="fila">
                <td>
                    Fecha Acto Administrativo:
                </td>
                <td>
                    <asp:Label ID="LfechaActoAdministrativo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="filaAlter">
                <td>
                    Nro. Expediente:
                </td>
                <td>
                    <asp:Label ID="LnroExpediente" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="fila">
                <td>
                    Autoridad Ambiental:
                </td>
                <td>
                    <asp:Label ID="LautoridadAmbiental" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="filaAlter">
                <td>
                    Operador:
                </td>
                <td>
                    <asp:Label ID="Loperador" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="fila">
                <td>
                    Cod. Registro Minería:
                </td>
                <td>
                    <asp:Label ID="LcodRegistroMineria" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="filaAlter">
                <td>
                    Minerales:
                </td>
                <td>
                    <asp:Label ID="Lminerales" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="fila">
                <td>
                    Fecha Expiración:
                </td>
                <td>
                    <asp:Label ID="LfechaExpiracion" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="filaAlter">
                <td>
                    Vigencia
                </td>
                <td>
                    <asp:Label ID="Lvigencia" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="fila">
                <td>
                    Estado
                </td>
                <td>
                    <asp:Label ID="LEstado" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="filaAlter">
                <td>
                    Nombre Proyecto:
                </td>
                <td>
                    <asp:Label ID="LnombreProyecto" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="fila">
                <td>
                    Área hectáreas:
                </td>
                <td>
                    <asp:Label ID="Lareahectareas" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="filaAlter">
                <td>
                    Nombre Mina:
                </td>
                <td>
                    <asp:Label ID="LnombreMina" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="fila">
                <td>
                    Observaciones:</td>
                <td>
                    <asp:Label ID="Lobservaciones" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="filaAlter">
                <td>
                    Ubicación:
                </td>
                <td>
                    <asp:Label ID="Lubicacion" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="fila">
                <td>
                    Localizaciones
                </td>
                <td>
                    <asp:Label ID="Llocalizaciones" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <table align="center">
            <tr>
                <td class="pie">
                    <asp:Label ID="Lnota" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <table align="center">
        <tr class="filaAlter">
            <td colspan="2">
                <input type="button" value="Imprimir" onclick="javascript:window.print();"/>
                <asp:Button ID="Bdescargar" runat="server" Text="Descargar Archivo Adjunto" 
                    onclick="Bdescargar_Click" />
                &nbsp;<input type="button" value="Cerrar" onclick="javascript:window.close();" />
            </td>
        </tr>
    </table>
    <table align="center">
            <tr>
                <td class="style1">
                    Esta información se entrega en cumplimiento de los artículos 2° y 3° del decreto 2235 de 30 de Octubre de 2012 del Ministerio de Defensa Nacional y de los artículos 1° y 2° de la resolución 1978 de 09 de Noviembre de 2012 del Ministerio de Ambiente y Desarrollo Sostenible.
                    De conformidad con el inciso décimo de los considerandos y al parágrafo 3° del artículo segundo de la resolución 1978 del 09 de Noviembre de 2012, la información suministrada a través de VITAL no compromete de ninguna manera la responsabilidad de la ANLA, por lo que queda eximida de cualquier consecuencia de índole administrativa, penal, disciplinaria y fiscal.
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
