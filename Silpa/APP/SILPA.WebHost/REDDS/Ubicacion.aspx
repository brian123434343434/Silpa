<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ubicacion.aspx.cs" Inherits="RegistroMinero_UbicacionRegistrosMineros" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script src='<%= ResolveClientUrl("~/js/jquery-1.8.2.min.js") %>' type="text/javascript"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript"
        src="https://maps.google.com/maps/api/js?sensor=false">
    </script>
    <style type="text/css">
  html { height: 100% }
  body { height: 100%; margin: 0px; padding: 0px }
  #ubicacionRegistros { height: 100% }
</style>
    <script src="../js/UbicacionRedd.js" type="text/javascript"></script>
</head>
<body onload="initialize();">  
    <form id="form1" runat="server" style="background-color:White;">
         <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server">
        </asp:ScriptManager>
    <div>
        <input type="hidden" runat="server" id="hdCoordenadas" />
        <input type="hidden" id="hdNombreMina" runat="server" />
        <input type="hidden" id="hdCodRegMinero" runat="server" />
     </div>
    </form>
    <div id="ubicacion" style="width:100%; height:100%"></div>
</body>
</html>
