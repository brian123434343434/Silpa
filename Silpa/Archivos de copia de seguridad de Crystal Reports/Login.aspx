<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">    
    <title>SILPA</title>
	<link rel="STYLESHEET" type="text/css" href="../App_Themes/img/PG2.css">
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
	<tbody><tr>
		<td align="center" valign="top" width="100%" height="100%">
			<table border="0" cellpadding="0" cellspacing="0" width="750" height="100%">
				<!--- BANNER START --->
				<tbody>
 
<tr>
	              <td class="bannerMenu" valign="top">&nbsp;</td>
				</tr>
<tr>
	<td valign="top">
	<script type="text/javascript">
AC_FL_RunContent( 'codebase','http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0','width','1000','height','166','src','cabeza1','quality','high','pluginspage','http://www.macromedia.com/go/getflashplayer','movie','cabeza1' ); //end AC code
function IMG1_onclick() {

}

</script><noscript><object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0" width="1000" height="166">
      <param name="movie" value="cabeza1.swf">
      <param name="quality" value="high">
      <embed src="cabeza1.swf" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" width="1000" height="166"></embed>
    </object></noscript></td>
</tr>
 
				<tr>
	              <td valign="top"><img src="App_Themes/img/menul.jpg" width="1000" height="33"></td>
				</tr>
				<!--- BANNER END --->
				
				<!--- BODY START --->
				<tr>
					<td class="contentTile" valign="top" height="100%">
					<div class="bodyContainer">
						
<div class="ltColumn">
	
		<div class="ltColumnRentFocus">
		<div class="rentFocusContent">
		  Login:
		  <input name="verCode2" id="verCode2" size="20" class="inputField" type="text">
		Clave:
		<input name="clientName2" class="inputField" type="text">
		<a href="Default.aspx"><img src="App_Themes/img/ingresar.gif" width="76" height="24" id="IMG1" onclick="return IMG1_onclick()"></a><?xml namespace=""
                prefix="asp" ?><asp:label id="Label1" runat="server"></asp:label></div>
		<div class="RentBtn"></div>
		<div class="copyright"></div>
	</div>
	<div class="ltColumnBtn02"><a href="#">Orientación al Solicitante
</a></div>
		
	<div class="ltColumnBtn03"><a href="#" onClick="chooseMenu(1);">Ver Información Publicada</a></div>
		<div id="menu2" class="subMenu" style="display: none;"></div>
		
	<div class="ltColumnBtn04"><a href="#">Res. Comunicación Activa
</a></div>
		
	<div class="ltColumnBtn05"><a href="#">Presentar Queja</a></div>
		<div id="menu3" class="subMenu" style="display: none;"></div>
		
	
		
	
		
	
		<div id="menu4" class="subMenu" style="display: none;"></div>
		
	
 
	<div class="ltColumnRentASpace">
	
</div>
	<div><img src="App_Themes/img/LtColumnBtm.jpg" alt="" border="0" width="175" height="6"></div>
</div>
						<div class="ContentArea">
							<div><img src="App_Themes/img/ContentAreaTop.jpg" alt="" border="0" width="810" height="6"></div>
							<div class="ContentCopy">
								
<div class="SubHeaderBack">
	<div class="pageHeader">
		<div class="subBannerPhoto"></div>
		<p>&nbsp;</p>
		<p> &nbsp; <span class="bold"><strong>BIENVENIDO(A) A LA PAGINA DE   TRAMITES DE</strong></span></p>
		<p class="bold"><strong> &nbsp; &nbsp; &nbsp;  LICENCIAS Y PERMISOS   AMBIENTALES </strong></p>
	
	</div>
		
</div>
<div class="copy">
	<img src="App_Themes/img/texto1.gif" width="770" height="400">
	
	
	
</div>
								<div><img src="App_Themes/img/CopyBtm.jpg" alt="" border="0" width="802" height="5"></div>
							</div>
							<div><img src="App_Themes/img/ContentAreaBtm.jpg" alt="" border="0" width="802" height="6"></div>
						</div>
					</div>					</td>
				</tr>
				<!--- BODY END --->
				
				<!--- FOOTER START --->
				<tr>
	<td class="footerMenu" valign="middle">
		<table border="0" cellpadding="0" cellspacing="0" width="100%">
			<tbody><tr>
				<td width="100%" valign="middle" nowrap="nowrap" class="footerMenuText"><div align="center">Copyright © 2009 Ministerio del Medio Ambiente </div></td>
				
				
			</tr>
			<tr>
				<td colspan="2">&nbsp;</td>
			</tr>
		</tbody></table>	</td>
</tr>

				<!--- FOOTER END --->
			</tbody></table>
		</td>
	</tr>
</tbody></table>
    </div>
    </form>
</body>
</html>
