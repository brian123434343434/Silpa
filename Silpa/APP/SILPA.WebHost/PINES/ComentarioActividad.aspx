<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ComentarioActividad.aspx.cs" Inherits="PINES_ComentarioActividad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Cometarios</title>
    <script src="../../DWConfiguration/ActiveContent/IncludeFiles/AC_RunActiveContent.js"
        type="text/javascript"></script>

</head>
<body class="bodyCometario">
    <form id="form1" runat="server">
        <asp:GridView ID="grvComentarios" runat="server" AutoGenerateColumns="false" SkinID="GrillaVisor" OnRowCommand="grvComentarios_RowCommand" Width="95%">
            <Columns>
                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:TemplateField HeaderText="Usuario">
                    <ItemTemplate>
                        <asp:Label ID="lblUsuario" runat="server" SkinID="normal" Text='<%# string.Format("{0}</ br> Aut:{1}", Eval("Usuario").ToString(), Eval("NombreAutoridad").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Comments" HeaderText="Comentario" />
                <asp:BoundField DataField="NombreAccion" HeaderText="Accion" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbArchivo" runat="server" CommandName="VerArchivo" Text="Ver Archivo" CommandArgument='<%# Bind("Field") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
