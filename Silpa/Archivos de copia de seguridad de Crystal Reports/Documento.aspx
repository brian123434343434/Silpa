<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Documento.aspx.cs" Inherits="NotificacionElectronica_Documento" MasterPageFile="~/plantillas/SILPA.master" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
      <div class="div-titulo">
            <asp:Label ID="lbl_titulo_principal" runat="server" Text="DOCUMENTO" SkinID="titulo_principal_blanco"></asp:Label>
	</div>
<div class="div-contenido">
<asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:GridView ID="gvDocumentos" runat="server" SkinID="Grilla" Width="539px" DataKeyNames="DFI_ID" AllowSorting="false" AutoGenerateColumns="false" GridLines="none">
        <Columns>
            <asp:BoundField DataField="DFI_NOMBRE" HeaderText="Documento" />
            <asp:BoundField DataField="DFI_FECHA_CREACION" HeaderText="Fecha Creacion" />
            <asp:BoundField DataField="DFI_UBICACION" HeaderText="Ubicacion" />
            <asp:HyperLinkField NavigateUrl="~/FirmaDigital/FirmaDocumento.aspx" Text="Ver" />
        </Columns>
    
    </asp:GridView>
    </div>
</asp:Content>