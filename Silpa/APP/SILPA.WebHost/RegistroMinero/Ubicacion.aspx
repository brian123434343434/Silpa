<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ubicacion.aspx.cs" Inherits="ReporteTramite_Mapa"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
<style type="text/css">
  html { height: 100% }
  body { height: 100%; margin: 0px; padding: 0px }
  #map_canvas { height: 100% }
</style>
<script type="text/javascript"
    src="https://maps.google.com/maps/api/js?sensor=false">
</script>
    <script src="../js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../js/Ubicacion.js" type="text/javascript"></script> 
</head>
<body onload="initialize()">
  <div id="map_canvas" style="width:100%; height:100%"></div>
  <input type="hidden" id="hdCoordenadas" runat="server" />
  <input type="hidden" id="hdNombreMina" runat="server" />
  <input type="hidden" id="hdCodRegMinero" runat="server" />
</body>
</html>

