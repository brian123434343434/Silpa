<%@ Page Language="C#" MasterPageFile="~/plantillas/Silpa.master" AutoEventWireup="true" CodeFile="MenuAudienciaPublica.aspx.cs" Inherits="Informacion_Publicaciones" Title="Consultar Inscritos a Audiencia Pública" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        table {
            /*border: 1px solid #000;*/
        }
        table tr td {
            border: 0px solid #ddd !important;
            padding: 4px;
        }
        .Button{
            background-color: #ddd;
        }
    </style>

    <div class="div-titulo">
        <asp:Label ID="lbl_titulo_principal" runat="server" SkinID="titulo_principal_blanco" Text="PROCESOS DE AUDIENCIA PUBLICA"></asp:Label>
    </div>

    <%--<div class="div-contenido2">--%>
    <div class="table-responsive" style="left: 0 !important; margin: 0 !important; padding-bottom: 10px !important; border: 0px solid #ddd !important;">
        <asp:XmlDataSource ID="XmlProcesoPublico" runat="server" DataFile="~/AudienciaPublica/ProcesosPublicos.xml"></asp:XmlDataSource>
        <asp:XmlDataSource ID="XmlProcesoPublico2" runat="server" DataFile="~/AudienciaPublica/ProcesosPublicos2.xml"></asp:XmlDataSource>
        <asp:GridView ID="GridView2" runat="server" 
            CssClass="table-striped table-bordered table-condensed" 
            AutoGenerateColumns="False"
            CellSpacing="10" CellPadding="10"
            Width="100%">
            <%--<RowStyle BackColor="#E3EAEB" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />--%>
            <RowStyle ForeColor="#000000" VerticalAlign="Middle" BorderWidth="1" BorderColor="#dddddd" BorderStyle="Solid"></RowStyle>
            <SelectedRowStyle Font-Bold="True" ForeColor="White" BackColor="#EE7402" />
            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#005695"></HeaderStyle>
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="Link" DataTextField="Procesos" HeaderText="&nbsp;&nbsp;Seleccione un Proceso:" HeaderStyle-Font-Size="12pt" />
            </Columns>
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <%--<SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />--%>
        </asp:GridView>
        <%--<br />--%>
        <asp:ImageButton ID="Button1" runat="server" OnClick="Button1_Click" TabIndex="30" SkinID="icoAtras" ToolTip="Regresar" ValidationGroup="xx" Visible="false" />
    </div>
</asp:Content>

