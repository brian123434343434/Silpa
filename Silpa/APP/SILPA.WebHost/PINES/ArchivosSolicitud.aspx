<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ArchivosSolicitud.aspx.cs" Inherits="PINES_ArchivosSolicitud" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body class="bodyCometario">
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="grvArchivos" runat="server" AutoGenerateColumns="false" SkinID="GrillaVisor" OnRowCommand="grvArchivos_RowCommand" DataKeyNames="NombreArchivo,Ubicacion">
            <Columns>
                <asp:BoundField HeaderText="Nombre Archivo" DataField="NombreArchivo" />
                <asp:ButtonField CommandName="Descargar" HeaderText="Descargar" Text="Descargar" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
