<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ubicacion.aspx.cs" Inherits="ReporteTramite_Mapa"  %>

<%@ Register Src="~/ReporteTramite/GoogleMapForASPNet.ascx" TagName="GoogleMapForASPNet" TagPrefix="uc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Simple Google Map</title>
</head>
<body>  
    <form id="form1" runat="server" style="background-color:White;">
         <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server">
        </asp:ScriptManager>
    <h3>Simple Google Map</h3>
    <div>
        <uc1:GoogleMapForASPNet ID="GoogleMapForASPNet1" runat="server" />
    </div>
    </form>
</body>
</html>
