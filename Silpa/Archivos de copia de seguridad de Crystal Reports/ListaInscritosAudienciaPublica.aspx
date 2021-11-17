<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="ListaInscritosAudienciaPublica.aspx.cs" Inherits="Informacion_Publicaciones"
    Title="Consultar Inscritos a Audiencia Pública" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="div-titulo">
        <asp:Label ID="lbl_titulo_principal" runat="server" SkinID="titulo_principal_blanco"
            Text="Lista de inscritos para intervención en audiencia pública número:"></asp:Label>&nbsp;</div>
    <div class="div-contenido2">
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CellPadding="4" DataSourceID="ListaInscritos" ForeColor="#333333" GridLines="None"
            Width="100%">
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#E3EAEB" />
            <Columns>
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                <asp:BoundField DataField="cedula" HeaderText="cedula" SortExpression="cedula" />
            </Columns>
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#7C6F57" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:XmlDataSource ID="ListaInscritos" runat="server" DataFile="~/AudienciaPublica/ListaIncritos.xml">
        </asp:XmlDataSource>
    </div>
</asp:Content>
