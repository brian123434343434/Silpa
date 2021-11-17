<%@ Control Language="C#" AutoEventWireup="true" CodeFile="exp_localizaciones.ascx.cs" Inherits="general_exp_localizaciones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="../estilos.css" rel="stylesheet" type="text/css" />
<script src='<%= ResolveClientUrl("~/js/basicos.js") %>' type="text/javascript"></script>
<asp:Panel id="pnl_insertar_editar" runat="server" Width="100%" Height="100%">
<asp:HiddenField ID="IndexCoordenada" runat="server" />
<table>
    <tr>
        <td align="center">
            <asp:GridView ID="dgv_localizaciones" SkinID="GrillaCoordenadas" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" PageSize="4" ShowFooter="True" DataKeyNames="LocalizacionID"  OnDataBound="dgv_localizaciones_DataBound"  Width="100%" OnRowCommand="dgv_localizaciones_RowCommand"
                OnPageIndexChanging="dgv_localizaciones_PageIndexChanging" OnRowDeleting="dgv_localizaciones_RowDeleting" OnRowEditing="dgv_localizaciones_RowEditing">
                <FooterStyle />
                <Columns>
                    <asp:BoundField AccessibleHeaderText="Tipo de Geometr&#237;a" HeaderText="Tipo de Geometr&#237;a" />
                    <asp:BoundField AccessibleHeaderText="Puntos"
                        HeaderText="Puntos" />
                    <asp:BoundField AccessibleHeaderText="Grados" HeaderText="Grados" />
                    <asp:TemplateField AccessibleHeaderText="Editar" HeaderText="Editar">
                        <ItemTemplate>
                            <asp:ImageButton ID="imb_editar" runat="server" CommandName="Edit"
                               ImageUrl="~/App_Themes/Img/Edit.png" ToolTip="Haga clic aquí para Editar"
                               CausesValidation="False"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="Eliminar" HeaderText="Eliminar">
                        <ItemTemplate>
                            <asp:ImageButton ID="imb_borrar" runat="server" CommandName="Delete"
                               ImageUrl="~/App_Themes/Img/Del.png" ToolTip="Haga clic aquí para borrar los items seleccionados"
                               CausesValidation="False"   />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings FirstPageImageUrl="~/App_Themes/Img/pagina_primera.gif" FirstPageText="Primera"
                    LastPageImageUrl="~/App_Themes/Img/pagina_ultima.gif" LastPageText="Ultima" Mode="NumericFirstLast"
                    NextPageImageUrl="~/App_Themes/Img/pagina_siguiente.gif" NextPageText="Siguiente" PreviousPageImageUrl="~/App_Themes/Img/pagina_anterior.gif"
                    PreviousPageText="Anterior" />
            </asp:GridView>
            <asp:Label ID="lbl_sel_todos" runat="server" Visible="False"></asp:Label>
        </td>
    </tr>
</table>
<table align="center" border="0" cellpadding="3" cellspacing="0" width="80%">
    <tr>
        <td align="right">Tipo de geometr&iacute;a: </td>
        <td>
            <asp:DropDownList ID="lst_tipo_geometria" runat="server" 
                ToolTip="Diligenciar polígono">
        <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                <asp:ListItem value="2">Pol&#237;gono</asp:ListItem>
            </asp:DropDownList>
                <asp:RequiredFieldValidator ID  ="rfvGeometria" runat="server" ControlToValidate="lst_tipo_geometria" ValidationGroup="localizaciones1" Display="Dynamic" Text="Debe Seleccionar un Tipo de Geomatría" InitialValue="-1">*</asp:RequiredFieldValidator>
            </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:TextBox ID="txtCoordenadas" runat="server" Width="80%" 
                TextMode="MultiLine" Rows="10" ValidationGroup="localizaciones" 
                ToolTip="Las coordenadas se deben ingresar en el sistema WGS84, el formato del numero debe ser Decimal utilizando el punto como separador y el orden debe ser Latitud, Longitud separado por coma. Ejemplo 1.05987,-66.056987"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revcoordenadas" runat="server" ControlToValidate="txtCoordenadas"
             ValidationExpression="^[0-9 ,.-]*$" Text="Solo se aceptan caracateres númericos" Display="Dynamic">
             </asp:RegularExpressionValidator>
        </td>
    </tr>
    
    
    <tr>
        <td colspan="2" align="center">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ValidationGroup="localizaciones" Visible="False" />
            <asp:RequiredFieldValidator
                    ID="rfv_geometria" runat="server" ControlToValidate="lst_tipo_geometria" Display="None"
                    ErrorMessage="El tipo de geometría es requerido" ValidationGroup="localizaciones"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvCoordenadas" runat="server" ControlToValidate="txtCoordenadas"
                Display="None" ErrorMessage="Debe ingresar las coordenadas" ValidationGroup="localizaciones"></asp:RequiredFieldValidator>
         </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
         <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                ValidationGroup="localizaciones1" Visible="False" />
            <asp:RequiredFieldValidator
                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="lst_tipo_geometria" Display="None"
                    ErrorMessage="El tipo de geometría es requerido" ValidationGroup="localizaciones1"></asp:RequiredFieldValidator>
            <asp:Button SkinID="boton" ID="btn_adicionar_localizacion" runat="server" Text="Adicionar localización" ToolTip="Haga clic para adicionar localización" OnClick="btn_adicionar_comunidad_Click" ValidationGroup="localizaciones1" />
            <asp:Button SkinID="boton" ID="btnCancelarEdicion" runat="server" Text="Cancelar Edición" OnClick="btnCancelarEdicion_Click" CausesValidation="false" Visible="false"/>
        </td>
    </tr>
</table>
</asp:Panel>