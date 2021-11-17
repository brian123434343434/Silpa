<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="BpmParametros.aspx.cs" Inherits="Administracion_Tablasbasicas_BpmParametros"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="ACTIVAR AVANCE DE ACTIVIDADES" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="div-contenido" style="height: 400px">
        <table width="100%">
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel ID="updConsultar" runat="server">
                        <ContentTemplate>
<asp:Panel id="pnlMaestro" runat="server" Width="100%">
                                <table width="60%">
                                    <tr>
                                        <td style="width: 15%" align="left">
                                            <asp:Label ID="lblNombreParametro" SkinID="etiqueta_negra" runat="server" Text="Nombre Parametro"></asp:Label>
                                        </td>
                                        <td style="width: 30%" align="left">
                                            <asp:TextBox ID="txtNombreParametro" SkinID="texto" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="right">
                                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" SkinID="boton_copia" OnClick="btnBuscar_Click" />
                                            <asp:Button ID="btnagregar" runat="server" Text="Agregar" SkinID="boton_copia" OnClick="btnagregar_Click" />
                                            <asp:Button ID="btnVolver" runat="server" SkinID="boton_copia" Text="Cancelar" PostBackUrl="~/Administracion/Tablasbasicas/TablasBasicas.aspx">
                                            </asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel> <asp:Panel id="pnlConsultar" runat="server" Width="100%" Visible="true">
                                <asp:GridView ID="grdDatos" runat="server" AutoGenerateColumns="False" Width="100%"
                                    AllowPaging="True" AllowSorting="True" EmptyDataText="No existen datos registrados en ésta tabla"
                                    OnRowCommand="grdDatos_RowCommand" DataKeyNames="ID,TIPO" OnPageIndexChanging="grdDatos_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Tipo de Avance" />
                                        <asp:BoundField DataField="NOMBRE" HeaderText="Nombre del tipo de avance”" />
                                        <asp:BoundField DataField="CODIGO" HeaderText="Código" />
                                        <asp:TemplateField HeaderText="Editar">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkModificar" CommandName="Modificar" runat="server" CommandArgument='<%# Container.DataItemIndex %>'>Modificar Registro</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Eliminar">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEliminar" CommandName="Eliminar" runat="server" CommandArgument='<%# Container.DataItemIndex %>'
                                                    OnClientClick="return confirm('Esta seguro de Eliminar este registro?')">Eliminar Registro</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel> <asp:Panel id="pnlEditar" runat="server" Width="100%" Visible="false"><TABLE width="100%"><TBODY><TR><TD style="WIDTH: 10%" align=left><asp:Label id="lblTipo" runat="server" Text="Tipo"></asp:Label> </TD><TD style="WIDTH: 25%" align=left><asp:DropDownList id="cboTipo" runat="server" Width="100%">
                                                </asp:DropDownList> </TD><TD></TD><TD><asp:Label id="lblID" runat="server" Visible="False"></asp:Label> <asp:RequiredFieldValidator id="rflNombreUpd" runat="server" ErrorMessage="Nombre es un campo Requerido" Display="None" ControlToValidate="txtNombre">*</asp:RequiredFieldValidator> <asp:RequiredFieldValidator id="rfvValorUpd" runat="server" ErrorMessage="Valor es un campo requerido." Display="None" ControlToValidate="txtValor">*</asp:RequiredFieldValidator></TD></TR><TR><TD style="WIDTH: 10%"><asp:Label id="lblNombre" runat="server" Text="Nombre"></asp:Label> </TD><TD style="WIDTH: 25%" align=left><asp:TextBox id="txtNombre" runat="server" Width="100%" MaxLength="100"></asp:TextBox> </TD><TD style="WIDTH: 10%" align=center><asp:Label id="lblValor" runat="server" Text="Valor"></asp:Label> </TD><TD style="WIDTH: 25%" align=left><asp:TextBox id="txtValor" runat="server" Width="50%" MaxLength="4"></asp:TextBox> </TD></TR><TR><TD align=right colSpan=4><asp:Button id="btnActualizar" onclick="btnActualizar_Click" runat="server" SkinID="boton_copia" Text="Aceptar"></asp:Button> <asp:Button id="btnCancelar" onclick="btnCancelar_Click" runat="server" SkinID="boton_copia" Text="Cancelar" CausesValidation="False"></asp:Button> </TD></TR></TBODY></TABLE></asp:Panel> <asp:Panel id="pnlNuevo" runat="server" Visible="false"><TABLE><TBODY><TR><TD><asp:Label id="lblTipo_Nvo" runat="server" Text="Tipo"></asp:Label> </TD><TD><asp:DropDownList id="cboTipo_Nvo" runat="server">
                                                </asp:DropDownList> </TD><TD></TD><TD><asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Nombre es un campo Requerido" Display="None" ControlToValidate="txtNombre_Nvo" __designer:wfdid="w9">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Valor es un campo requerido." Display="None" ControlToValidate="txtValor_Nvo" __designer:wfdid="w10">*</asp:RequiredFieldValidator></TD></TR><TR><TD><asp:Label id="lblNombre_Nvo" runat="server" Text="Nombre"></asp:Label> </TD><TD><asp:TextBox id="txtNombre_Nvo" runat="server" MaxLength="100"></asp:TextBox> </TD><TD><asp:Label id="lblValor_Nvo" runat="server" Text="Valor"></asp:Label> </TD><TD><asp:TextBox id="txtValor_Nvo" runat="server" MaxLength="4"></asp:TextBox> </TD></TR><TR><TD align=right colSpan=4><asp:HiddenField id="hdfResultado" runat="server"></asp:HiddenField> &nbsp;<asp:Button id="btnRegistrar" onclick="btnRegistrar_Click" runat="server" SkinID="boton_copia" Text="Aceptar"></asp:Button> <asp:Button id="btnCancelarReg" onclick="btnCancelarReg_Click" runat="server" SkinID="boton_copia" Text="Cancelar" CausesValidation="False"></asp:Button> </TD></TR></TBODY></TABLE></asp:Panel> <asp:ValidationSummary id="ValidationSummary1" runat="server"></asp:ValidationSummary> <asp:Label id="lblMensajeError" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label> 
</ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    &nbsp;
                </td>
            </tr>
        </table>
        &nbsp;
    </div>
</asp:Content>
