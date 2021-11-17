<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPASinMenuFlash.master" AutoEventWireup="true" CodeFile="NotificacionEstados.aspx.cs" Inherits="NotificacionElectronica_NotificacionEstados" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script src="../jquery/jquery.js" type="text/javascript"></script>
    <link href="../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../jquery/EstiloGris/jquery-ui.js"  type="text/javascript"></script>

    <asp:ScriptManager ID="scmManager" runat="server"></asp:ScriptManager>
    <div class='burbujaAyuda'></div>
    <div class="div-titulo">
        <a href="#" id="hrfCerrarVentana" runat="server" onclick="window.close();return false;" class="Salir">Cerrar [X]</a>
        <br />
        <asp:Label ID="lblTitulo" runat="server" Text="PUBLICIDAD DE ACTOS ADMINISTRATIVOS - DOCUMENTOS" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="contact_form" id="divMensaje" runat="server" visible="false">  
        <div class="Table">
            <div class="Row">
                <div class="CellMensaje">
                    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
                </div>
            </div>
        </div>            
    </div>
    <div class="contact_form" id="divEstadosNotificaciones" runat="server">
        <div class="TableReporteNot">
            <div class="RowReporteNot">
                <div class="CellReporteNot">
                    <asp:GridView runat="server" AutoGenerateColumns="False" AllowPaging="false" ID="grdDocumentos" 
                                    SkinID="GrillaNotificaciones" EmptyDataText="No se encontro información de documentos" ShowHeaderWhenEmpty="true" Width="90%">
                        <Columns>
                                <asp:TemplateField HeaderText="NOMBRE DOCUMENTO">
                                    <ItemTemplate>
                                        <asp:literal ID="ltlDocumento" runat="server" Text='<%# Eval("NOMBRE_DOCUMENTO")  %>'></asp:literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VER" ItemStyle-CssClass="ItemNotificacionCentrado">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="imgDescargarDocumento" BorderWidth="0" ImageUrl="~/images/documento.png" CommandArgument='<%#Eval("RUTA_DOCUMENTO") %>' OnClick="imgDescargarDocumento_Click"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>            
    </div>
</asp:Content>
