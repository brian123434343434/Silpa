<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="VAB_TipoDato.aspx.cs" Inherits="Administracion_Tablasbasicas_VAB_TipoDato" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="TIPO DATO" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="div-contenido" style="height: 400px">
        <table width="100%">
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel ID="updConsultar" runat="server">
                        <contenttemplate>
<asp:Panel id="pnlMaestro" runat="server" Width="100%"><TABLE width="60%"><TBODY><TR><TD style="WIDTH: 15%" align=left><asp:Label id="lblNombreParametro" runat="server" SkinID="etiqueta_negra" Text="Descripción"></asp:Label> </TD><TD style="WIDTH: 30%" align=left><asp:TextBox id="txtNombreParametro" runat="server" SkinID="texto" Width="100%"></asp:TextBox> </TD></TR><TR><TD></TD><TD align=right><asp:Button id="btnBuscar" onclick="btnBuscar_Click" runat="server" SkinID="boton_copia" Text="Buscar"></asp:Button> <asp:Button id="btnagregar" onclick="btnagregar_Click" runat="server" SkinID="boton_copia" Text="Agregar"></asp:Button> <asp:Button id="btnVolver" runat="server" SkinID="boton_copia" Text="Cancelar" PostBackUrl="~/Administracion/Tablasbasicas/TablasBasicas.aspx"></asp:Button></TD></TR></TBODY></TABLE></asp:Panel> <asp:Panel id="pnlConsultar" runat="server" Width="100%" Visible="true">
        <asp:GridView ID="grdDatos" runat="server"         
         AutoGenerateColumns="False"         
         Width="100%" 
         AllowPaging="True" 
         AllowSorting="True" EmptyDataText="No existen datos registrados en ésta tabla" 
         OnRowCommand="grdDatos_RowCommand" 
         DataKeyNames="ID" 
         OnPageIndexChanging="grdDatos_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID Tipo Dato" />
                <asp:BoundField DataField="DESCRIPCION" HeaderText="Tipo de Dato" />
                <asp:BoundField DataField="SEPARADOR" HeaderText="Separador" />
                <asp:TemplateField HeaderText="Editar">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkModificar" CommandName="Modificar" runat="server"  CommandArgument='<%# Container.DataItemIndex %>'>Modificar Registro</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Eliminar">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEliminar" CommandName="Eliminar" runat="server"  CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Esta seguro de Eliminar este registro?')">Eliminar Registro</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </asp:Panel> <asp:Panel id="pnlEditar" runat="server" Width="100%" Visible="false"><TABLE width="100%"><TBODY><TR><TD style="WIDTH: 10%" align=left><asp:Label id="lblTipoDato" runat="server" Text="Tipo Dato"></asp:Label> </TD><TD style="WIDTH: 25%" align=left><asp:TextBox id="txtTipoDato" runat="server"></asp:TextBox> </TD><TD></TD><TD><asp:Label id="lblID" runat="server" Visible="False"></asp:Label> </TD></TR><TR><TD style="WIDTH: 10%" align=left><asp:Label id="lblSeparador" runat="server" Text="Separador" __designer:wfdid="w5"></asp:Label></TD><TD style="WIDTH: 25%" align=left><asp:TextBox id="txtSeparador" runat="server" __designer:wfdid="w6"></asp:TextBox></TD><TD></TD><TD></TD></TR><TR><TD align=right colSpan=4><asp:Button id="btnActualizar" onclick="btnActualizar_Click" runat="server" SkinID="boton_copia" Text="Aceptar"></asp:Button> <asp:Button id="btnCancelar" onclick="btnCancelar_Click" runat="server" SkinID="boton_copia" Text="Cancelar"></asp:Button> </TD></TR></TBODY></TABLE></asp:Panel> <asp:Panel id="pnlNuevo" runat="server" Visible="false"><TABLE><TBODY><TR><TD style="WIDTH: 10%" align=left><asp:Label id="lblTipoDato_Nvo" runat="server" Text="Tipo Dato"></asp:Label> </TD><TD style="WIDTH: 25%" align=left><asp:TextBox id="txtTipoDato_Nvo" runat="server"></asp:TextBox> </TD><TD></TD><TD></TD></TR><TR><TD style="WIDTH: 10%" align=left><asp:Label id="lblSeparador_Nvo" runat="server" Text="Separador" __designer:wfdid="w7"></asp:Label></TD><TD style="WIDTH: 25%" align=left><asp:TextBox id="txtSeparador_Nvo" runat="server" __designer:wfdid="w8"></asp:TextBox></TD><TD></TD><TD></TD></TR><TR><TD align=right colSpan=4>&nbsp;<asp:Button id="btnRegistrar" onclick="btnRegistrar_Click" runat="server" SkinID="boton_copia" Text="Aceptar"></asp:Button> <asp:Button id="btnCancelarReg" onclick="btnCancelarReg_Click" runat="server" SkinID="boton_copia" Text="Cancelar"></asp:Button> </TD></TR></TBODY></TABLE></asp:Panel> 
<asp:Label ID="lblMensajeError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
</contenttemplate>
                        <triggers>
            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
        </triggers>
                    </asp:UpdatePanel>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
