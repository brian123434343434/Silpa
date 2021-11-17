<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ubicacion.aspx.cs" Inherits="CargueSaldo_Ubicacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <style type="text/css">
  html { height: 100% }
  body { height: 100%; margin: 0px; padding: 0px }
  #map_canvas { height: 100% }
</style>
    <script type="text/javascript"
    src="https://maps.google.com/maps/api/js?sensor=false">
</script>
    <script src="../js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../js/UbicacionSaldoAprovechamiento.js" type="text/javascript"></script> 
</head>
<body onload="initialize()">
  <div id="map_canvas" style="width:100%; height:100%"></div>
  <input type="hidden" id="hdCoordenadas" runat="server" />
</body>
</html>
