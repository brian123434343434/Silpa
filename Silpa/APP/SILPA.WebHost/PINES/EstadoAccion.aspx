<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EstadoAccion.aspx.cs" Inherits="PINES_EstadoAccion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body class="bodyCometario">
    <form id="form1" runat="server">
    <div>
    <asp:GridView ID="grvEstadoAccion" runat="server" AutoGenerateColumns="false" SkinID="GrillaVisor" OnRowDataBound="grvEstadoAccion_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Accion">
                    <ItemTemplate>
                        <div style="position: relative; ">
                            <asp:Label ID="lblAccion" runat="server" Text='<%# Bind("DESCRIPCION") %>'></asp:Label>
                        </div>
                        <div>
                            Creacion:<asp:Label ID="lblFechaCreacion" runat="server" Text='<%# Eval("fecha_creacion","{0:dd/MM/yyyy}")%>'></asp:Label><br />
                            Vencimiento:<asp:Label ID="lblFechaVencimiento" runat="server" Text='<%# Eval("fecha_programada_vencimiento","{0:dd/MM/yyyy}") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="fecha_compromiso" HeaderText="Fecha Compromiso" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Image ID="imgEstadoAccion" runat="server" Width="28" Height="27" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Estado">
                    <ItemTemplate>
                        <asp:Label ID="lblEstado" runat="server" Text='<%# Convert.ToBoolean(Eval("ActividadRealizada")) ? "Finalizada":"En Proceso" %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fecha Real Finalizacion">
                    <ItemTemplate>
                        <asp:Label ID="lblFechaFinalizacion" runat="server" Text='<%# Convert.ToBoolean(Eval("ActividadRealizada")) ? Eval("fecha_ultimo_registro","{0:dd/MM/yyyy}"):"" %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="fecha_prorroga" HeaderText="Fecha Prorroga" DataFormatString="{0:dd/MM/yyyy}" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
