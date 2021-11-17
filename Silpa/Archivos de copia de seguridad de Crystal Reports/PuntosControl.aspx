<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PuntosControl.aspx.cs" Inherits="Salvoconducto_PuntosControl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<script src='<%= ResolveClientUrl("~/js/jquery-1.8.2.min.js") %>' type="text/javascript"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="https://maps.google.com/maps/api/js?sensor=false&key=AIzaSyCf3Zn_kABN0jwB7E92B42OIQOAaxzgzRI"></script>
    <style type="text/css">
        html {
            height: 100%;
        }

        body {
            height: 100%;
            margin: 0px;
            padding: 0px;
        }

        #ubicacionRegistros {
            height: 100%;
        }
    </style>
    <script src="../js/PuntosControl.js" type="text/javascript"></script>
</head>
<body onload="initialize();">
    <form id="form1" runat="server" style="background-color: White;">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server">
        </asp:ScriptManager>
        <div>
            <table>
                <tr>
                    <td>
                        <%--Latitud:--%>
                    </td>
                    <td>
                        <%--<asp:TextBox ID="txtLatitud" runat="server"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="txtLatitud_MaskedEditExtender" runat="server" 
                        TargetControlID="txtLatitud" Mask="99.99999999" AcceptNegative="Left" MaskType="Number">
                    </cc1:MaskedEditExtender>--%>
                    </td>
                    <td>
                        <%--Longitud:--%>
                    </td>
                    <td>
                        <%--<asp:TextBox ID="txtLongitud" runat="server"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="txtLongitud_MaskedEditExtender" runat="server" 
                        TargetControlID="txtLongitud" Mask="99.99999999" AcceptNegative="Left" MaskType="Number">
                    </cc1:MaskedEditExtender>--%>
                    </td>
                    <td>
                        <input onclick="if (confirm('Esta seguro de agregar este punto de control')) { GuardarPuntoControl(); }" type="button" value="Agregar punto control" style="width: 150px; height: 80px;" />
                        <input onclick="deleteOverlays();" type="button" value="Eliminar punto" style="width: 150px; height: 80px;" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <input type="hidden" runat="server" id="hdCoordenadas" />
            <input type="hidden" id="hdContadorPunto" runat="server" />
            <input type="hidden" id="hdLogID" runat="server" />
            <%--<input type="hidden" id="hdCodRegMinero" runat="server" />--%>
        </div>
    </form>
    <div id="ubicacionPuntosControl" style="width: 100%; height: 100%"></div>
</body>
</html>
