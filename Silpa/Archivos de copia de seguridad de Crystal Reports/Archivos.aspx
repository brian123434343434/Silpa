<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPASinMenuFlash.master" AutoEventWireup="true" CodeFile="Archivos.aspx.cs" Inherits="RepositorioArchivos_Archivos" %>
<%@ Register Assembly="FUA" Namespace="Subgurim.Controles" TagPrefix="cc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        .CajaDialogo
        {
            background-color:#fff; 
            border-width: 1px;
            border-style: outset;
            border-color: Yellow;
            padding: 0px;
        }
        .CajaDialogo div
        {
            margin: 5px;
        }
        .FondoAplicacion
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
</style>
     <script src="../jquery/jquery.js"></script>
    <link href="../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../jquery/EstiloGris/jquery-ui.js"></script>
    <script src="../jquery/jquery.numeric.js"></script>
    <link href="../App_Themes/skin/StyleREDDS.css" rel="stylesheet" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"/>
        
    <div class="div-titulo">
    <asp:Label ID="lblTituloPrincipal" runat="server" Text="REPOSITORIO ARCHIVOS" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
        
        <div class="contact_form">
            <asp:UpdatePanel runat="server" ID="upnlArchivos">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAceptar" />
                    <asp:AsyncPostBackTrigger ControlID="btnCancelar" />
                    <asp:PostBackTrigger ControlID="grvArchivosUsuario" />
                </Triggers>
                <ContentTemplate>
                    <div class="Table" style="margin: auto;">
                        <div class="Row">
                            <div class="Cell">
                                <asp:GridView ID="grvArchivosUsuario" runat="server" AutoGenerateColumns="false" SkinID="Grilla" Width="100%" EmptyDataText="No se encontraron archivos en su repositorio" OnRowDeleting="grvArchivosUsuario_RowDeleting" OnRowCommand="grvArchivosUsuario_RowCommand">
                                    <Columns>
                                        <asp:BoundField HeaderText="ID" DataField="FileID" />
                                        <asp:TemplateField HeaderText="Archivo">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbVer" runat="server" Text='<%# Bind("NombreArchivo") %>' CommandName="Descargar" ToolTip="Descargar" CommandArgument='<%# Container.DataItemIndex %>' ></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Ubicacion" DataField="Ubicacion" />
                                        <asp:BoundField HeaderText="Tipo Archivo" DataField="DescTipoArchivo" />
                                        <asp:BoundField HeaderText="Tamaño" DataField="Tamaño" DataFormatString="{0:N2} KB" />
                                        <asp:BoundField HeaderText="Fecha Carga" DataField="FechaRegistro" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbEliminar" runat="server" Text="Eliminar" CommandName="Delete" OnClientClick="return confirm('¿Está seguro de eliminar este archivo?');"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <asp:Button ID="btnNuevoArchivo" runat="server" Text="Nuevo Archivo" OnClick="btnNuevoArchivo_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        
            <asp:Label ID="lblArchivo" runat="server"></asp:Label>

            <cc1:ModalPopupExtender ID="mpeArchivo" runat="server"
                TargetControlID="lblArchivo"
                PopupControlID="pnlArchivo"
                DropShadow="True" Enabled="True" DynamicServicePath=""
                BackgroundCssClass="FondoAplicacion" />
            <asp:Panel ID="pnlArchivo" runat="server" Style="display: none;" CssClass="CajaDialogo">
                <table>
                    <tr>
                        <td>
                            <label for="cboFormulario">Formulario:</label>
                        </td>
                        <td>
                             <asp:UpdatePanel ID="upnlFormulario" runat="server">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnAceptar" />
                                    <asp:AsyncPostBackTrigger ControlID="btnCancelar" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:DropDownList ID="cboFormulario" runat="server" OnSelectedIndexChanged="cboFormulario_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="txtNombreRazonSocial">Tipo Archivo:</label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="upnlTipoArchivo" runat="server">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnAceptar" />
                                    <asp:AsyncPostBackTrigger ControlID="btnCancelar" />
                                    <asp:AsyncPostBackTrigger ControlID="cboFormulario" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:DropDownList ID="cboTipoArchivo" runat="server" OnSelectedIndexChanged="cboTipoArchivo_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static">
                                        <asp:ListItem Text="Seleccione.." Value=""></asp:ListItem>
                                        <asp:ListItem Text="Tipo1" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Tipo2" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Tipo3" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:UpdatePanel ID="upnlArchivo" runat="server">
                                <ContentTemplate>
                                    <cc2:FileUploaderAJAX ID="fuplArchivoUsuario" runat="server" Directory_CreateIfNotExists="true" Width="100%" Height="50px" text_Delete="Eliminar" text_Add="Agregar" text_Uploading="Subiendo..." text_X="x" ShowLegenTypeFile="false" File_RenameIfAlreadyExists="false" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="btnAceptar" ClientIDMode="Static" runat="server" Text="Aceptar" CausesValidation="false" SkinID="boton_copia" OnClick="btnAceptar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cerrar" CausesValidation="false" SkinID="boton_copia" OnClick="btnCancelar_Click" />

            </asp:Panel>
</asp:Content>

