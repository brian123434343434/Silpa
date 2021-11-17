<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NoVisitante.aspx.cs" Inherits="NoVisitante" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Contador de Visitante</title>
</head>
<body>
    <form id="form1" runat="server">    
    <table style="background-color:#E9E9E9" cellspacing="0" width="260px">
    <tr>
        <td align="center"  height="18px" >
            <asp:Label runat="server" ID="label" Text="Usted  es el visitante No:" ForeColor="Black" SkinID="etiqueta_negra" ></asp:Label>
        </td>
    </tr>
    <tr>
        
        <td align="center" >        
        <table bgcolor="White" style="border-color: #000000; border-style: solid; height: 20px">        
        <tr>
        <td >
            <asp:Label runat="server" ID="lblNumeroVisitante"    
                BorderStyle="None" Text="00000000" Font-Size="20px" ForeColor="#003366"
            Font-Names="Arial" EnableTheming="True" Font-Overline="False"></asp:Label>   
        </td></tr>
        </table>
        </td>        
    </tr>
    <tr>
    <td>
    <br/>
    </td>
    </tr>
    </table>            
    </form>
</body>
</html>
