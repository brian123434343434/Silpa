<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ConsultarEstadoNotificacion.aspx.cs" Inherits="NotificacionElectronica_ConsultarEstadoNotificacion" Title="Untitled Page" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
        <div class="div-titulo">
        <asp:Label ID="lbl_titulo_principal" runat="server" Text="ESTADO DE NOTIFICACIÓN" SkinID="titulo_principal_blanco"></asp:Label>
	</div>
<div class="div-contenido">
<asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    &nbsp;
    <br />
    <table>
        <tr>
            <td colspan="3" rowspan="2">
   
    <asp:GridView ID="gvDocumentos" runat="server" SkinID="Grilla" DataKeyNames="DFI_ID" AllowSorting="false" AutoGenerateColumns="false" GridLines="none" OnRowCommand="gvDocumentos_RowCommand" >
        <Columns>
            <asp:BoundField DataField="DFI_NOMBRE" HeaderText="Documento" />
            <asp:BoundField DataField="DFI_FECHA_CREACION" HeaderText="Fecha Creacion" />
            <asp:BoundField DataField="DFI_UBICACION" HeaderText="Ubicacion" ControlStyle-Height="100px" ControlStyle-Width="100px" />
            <asp:ButtonField Text="Consultar Estado" CommandName="Boton"/>
        </Columns>
    
    </asp:GridView>
            </td>
        </tr>
        <tr>
        </tr>
    </table>
    <br />
    </div>
</asp:Content>

