<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ActividadRadicable.aspx.cs" Inherits="Administracion_Tablasbasicas_ActividadRadicable" Title="Untitled Page" %>

<asp:Content ID="Content3" ContentPlaceHolderID="headPlaceHolder" runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>
</asp:Content>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        table {
            /*border: 1px solid #000;*/
        }

            table tr td {
                border: 0px solid #ddd !important;
                padding: 4px;
            }

        .Button {
            background-color: #ddd;
        }
    </style>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>

    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="ACTIVIDAD QUE REQUIERE RADICACION" SkinID="titulo_principal_blanco"></asp:Label>
    </div>

    <%--<div class="div-contenido" style="height: 400px">--%>
    <div class="table-responsive">
        <asp:UpdatePanel ID="updConsultar" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlMaestro" runat="server" Width="100%">
                    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
                        <tbody>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTipoCon" runat="server" Text="Nombre" SkinID="etiqueta_negra10N"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtNombreParametro" runat="server" SkinID="texto" Width="100%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                                    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%;">
                                        <tr>
                                            <td style="text-align: right; vertical-align: middle;">
                                                <asp:Button ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" SkinID="boton_copia" Text="Buscar"></asp:Button>
                                            </td>
                                            <td style="padding-right: 30px; padding-left: 30px; text-align: center; vertical-align: middle;">
                                                <asp:Button ID="btnAgregar" OnClick="btnAgregar_Click" runat="server" SkinID="boton_copia" Text="Agregar" CausesValidation="False"></asp:Button>
                                            </td>
                                            <td style="text-align: left; vertical-align: middle;">
                                                <asp:Button ID="btnVolver" runat="server" SkinID="boton_copia" Text="Cancelar" PostBackUrl="~/Administracion/Tablasbasicas/TablasBasicas.aspx"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlConsultar" runat="server" Width="100%" Visible="true">
                    <asp:GridView ID="grdDatos" runat="server" Width="100%" OnPageIndexChanging="grdDatos_PageIndexChanging"
                        DataKeyNames="ID_ACTIVIDAD,ACTIVA" OnRowCommand="grdDatos_RowCommand" EmptyDataText="No existen datos registrados en ésta tabla"
                        AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False" SkinID="Grilla">
                        <Columns>
                            <asp:BoundField DataField="ID_ACTIVIDAD" HeaderText="Id"></asp:BoundField>
                            <asp:BoundField DataField="NOMBRE_ACTIVIDAD" HeaderText="Nombre de la Actividad"></asp:BoundField>
                            <asp:BoundField DataField="FORMA" HeaderText="Id del Formulario"></asp:BoundField>
                            <asp:BoundField DataField="ESTADO" HeaderText="Estado"></asp:BoundField>
                            <asp:TemplateField HeaderText="Editar">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkModificar" CommandName="Modificar" runat="server" CommandArgument='<%# Container.DataItemIndex %>'>Modificar Registro</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eliminar">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEliminar" CommandName="Eliminar" runat="server" CommandArgument='<%# Container.DataItemIndex %>'>Eliminar Registro</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="pnlEditar" runat="server" Width="100%" Visible="false">
                    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
                        <tbody>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_Id" runat="server" Text="Id" SkinID="etiqueta_negra10N"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtId" runat="server" MaxLength="50"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNombre" runat="server" Text="Nombre de la Actividad" SkinID="etiqueta_negra10N"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtNombre" runat="server" MaxLength="50"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTipo" runat="server" Text="Id del Formulario" SkinID="etiqueta_negra10N"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTipo" runat="server" MaxLength="9">0</asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEstado" runat="server" Text="Estado" SkinID="etiqueta_negra10N"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkEstado" runat="server" EnableTheming="false"></asp:CheckBox>
                                    <asp:Label ID="lblId" runat="server" Visible="False" SkinID="etiqueta_negra10N"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                                    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%;">
                                        <tr>
                                            <td style="padding-right: 15px; text-align: right; vertical-align: middle;">
                                                <asp:Button ID="btnActualizar" OnClick="btnActualizar_Click" runat="server" SkinID="boton_copia"
                                                    Text="Aceptar"></asp:Button>
                                            </td>
                                            <td style="padding-left: 15px; text-align: left; vertical-align: middle;">
                                                <asp:Button ID="btnCancelar" OnClick="btnCancelar_Click" runat="server" SkinID="boton_copia"
                                                    Text="Cancelar" CausesValidation="False"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlNuevo" runat="server" Visible="false">
                    <table>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIdNvo" runat="server" Text="Id" SkinID="etiqueta_negra10N"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtIdNvo" runat="server" MaxLength="50"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNombreNvo" runat="server" Text="Nombre de la Actividad" SkinID="etiqueta_negra10N"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtNombreNvo" runat="server" MaxLength="50"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTipoNvo" runat="server" Text="Id del Formulario" SkinID="etiqueta_negra10N"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTipoNvo" runat="server" MaxLength="9">0</asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEstadoNvo" runat="server" Text="Estado" SkinID="etiqueta_negra10N"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkEstadoNvo" runat="server" EnableTheming="false"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                                    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%;">
                                        <tr>
                                            <td style="padding-right: 15px; text-align: right; vertical-align: middle;">
                                                <asp:Button ID="btnRegistrar" OnClick="btnRegistrar_Click" runat="server" SkinID="boton_copia" Text="Aceptar"></asp:Button>
                                            </td>
                                            <td style="padding-left: 15px; text-align: left; vertical-align: middle;">
                                                <asp:Button ID="btnCancelarReg" OnClick="btnCancelarReg_Click" runat="server" SkinID="boton_copia" Text="Cancelar" CausesValidation="False"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
                <asp:Label ID="lblMensajeError" runat="server" Font-Bold="True" ForeColor="Red" SkinID="etiqueta_roja_error"></asp:Label>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
