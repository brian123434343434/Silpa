<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="ConsultaDocumentos.aspx.cs" Inherits="AdquisicionDocumentos_ConsultaDocumentos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
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
        <asp:Label ID="lbl_titulo_principal" runat="server" SkinID="titulo_principal_blanco"
            Text="CONSULTA DE DOCUMENTOS" meta:resourcekey="lbl_titulo_principalResource1"></asp:Label>
    </div>

    <%--<div class="div-contenido2">--%>
    <div class="table-responsive">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
            <tr>
                <td>
                    <asp:Label id="lblEntidad" runat="server" SkinID="etiqueta_negra" Width="150px" Text="Entidad Externa" meta:resourcekey="lblEntidadResource1"></asp:Label>                                
                </td>
                <td>
                    <asp:DropDownList id="cboEntidad" runat="server" SkinID="lista_desplegable" width="150px" meta:resourcekey="cboEntidadResource1">
                    <asp:ListItem Value="-1" meta:resourcekey="ListItemResource1">Seleccione...</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>                            
                    <asp:Label ID="lblNumeroSilpa" runat="server" Width="150px" SkinID="etiqueta_negra"
                        Text="Numero Silpa" meta:resourcekey="lblNumeroSilpaResource1"></asp:Label>
                </td>
                <td>
                    <asp:TextBox id="txtNumeroSilpa" runat="server" width="150px" EnableViewState="False" meta:resourcekey="txtNumeroSilpaResource1" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFechaDesde" runat="server" Width="150px" SkinID="etiqueta_negra"
                        Text="Fecha Desde" meta:resourcekey="lblFechaDesdeResource1"></asp:Label>                            
                </td>
                <td>
                    <asp:TextBox ID="txtFechaDesde" runat="server"></asp:TextBox><cc1:CalendarExtender ID="calFechaDesde" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaDesde"></cc1:CalendarExtender>
                    <asp:TextBox ID="txtFechaDesde_" width="150px" runat="server" ReadOnly="True" Columns="20" meta:resourcekey="txtFechaDesdeResource1" Visible="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFechaFinal" width="150px" runat="server" SkinID="etiqueta_negra" Text="Fecha Hasta" meta:resourcekey="lblFechaFinalResource1"></asp:Label>                                
                </td>
                <td>
                    <asp:TextBox ID="txtFechaHasta" runat="server"></asp:TextBox><cc1:CalendarExtender ID="calFechaHasta" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaHasta"></cc1:CalendarExtender>
                    <asp:TextBox ID="txtFechaHasta_" runat="server" width="150px" ReadOnly="True" meta:resourcekey="txtFechaHastaResource1" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                    <asp:Button id="btnBuscar" runat="server" text="Buscar" OnClick="btnBuscar_Click" meta:resourcekey="btnBuscarResource1" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding: 10px; text-align: left; vertical-align: middle;">
                    <asp:Label ID="lblTexto" runat="server" SkinID="etiqueta_negra" Text="Seleccione el documento que requiere solicitar para su trámite" meta:resourcekey="lblTextoResource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding: 10px; text-align: left; vertical-align: middle;">
                    <asp:Label ID="lblMsg" Visible="False" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                </td>
            </tr>
        </table>
        
        <asp:Panel ID="pnlDocumentos" runat="server" ScrollBars="Auto" Width="100%">
            <asp:GridView ID="grdDocumentos" runat="server" Width="100%" ForeColor="#333333"
                PageSize="1" GridLines="None" onrowcommand="grdDocumentos_RowCommand"
                CellPadding="4" AutoGenerateColumns="False" meta:resourcekey="grdDocumentosResource1">
                <RowStyle BackColor="#E3EAEB"></RowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="Codigo Silpa" meta:resourcekey="TemplateFieldResource1">
                        <ItemTemplate>
                            <asp:Label ID="lblCodigoSilpa" runat="server" Text='<%# Bind("NumeroSilpa") %>' meta:resourcekey="lblCodigoSilpaResource1" SkinID="etiqueta_negra"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha" meta:resourcekey="TemplateFieldResource2">
                        <ItemTemplate>
                            <asp:Label ID="lblFecha" runat="server" Text='<%# Bind("Fecha") %>' meta:resourcekey="lblFechaResource1" SkinID="etiqueta_negra"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre Entidad" meta:resourcekey="TemplateFieldResource3">
                        <ItemTemplate>
                            <asp:Label ID="lblFilename" runat="server" style="display:none" Text='<%# Bind("FullName") %>' meta:resourcekey="lblFilenameResource1" SkinID="etiqueta_negra"></asp:Label>
                            <asp:Label ID="lblNombreEntidad" runat="server" Text='<%# Bind("EexNombre") %>' meta:resourcekey="lblNombreEntidadResource1" SkinID="etiqueta_negra"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:ButtonField CommandName="viewFile" DataTextField="FileName" Text="ViewDocument" HeaderText="Documento" meta:resourcekey="ButtonFieldResource1" ControlStyle-CssClass="a_blue"></asp:ButtonField>
                </Columns>
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></FooterStyle>
                <PagerStyle HorizontalAlign="Center" BackColor="#666666" ForeColor="White"></PagerStyle>
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></HeaderStyle>
                <EditRowStyle BackColor="#7C6F57"></EditRowStyle>
                <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
            </asp:GridView>
        </asp:Panel>
    </div>
</asp:Content>
