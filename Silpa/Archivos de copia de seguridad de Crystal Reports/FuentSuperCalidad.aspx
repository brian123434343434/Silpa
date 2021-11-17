<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPALimpia.master" AutoEventWireup="true" 
CodeFile="FuentSuperCalidad.aspx.cs" Inherits="ResumenEIA_InfoAdicional_FuentSuperCalidad" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
    <tbody>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <tr>
            <td colspan="4" class="style1" width="100%">
                INFORMACIÓN ESTUDIO DE CALIDAD DE AGUAS SUPERFICIALES</td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
                &nbsp;</td>
        </tr>
        <%--Caracteristicas --%>
        <tr>
            <td width="25%">
                <asp:Label ID="Label3" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Característica:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:DropDownList ID="cboTipoCaracteristica" runat="server" SkinID="lista_desplegable"
                AutoPostBack="true" 
                    onselectedindexchanged="cboTipoCaracteristica_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Text="Seleccione..." Value="-1"></asp:ListItem>
                    <asp:ListItem Text="Físicas" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Químicas" Value="2"></asp:ListItem>
                
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator2" runat="server"
                    ErrorMessage="Seleccione el Tipo de Característica" 
                    Display="Dynamic"
                    ControlToValidate="cboTipoCaracteristica" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="Caracteristica">*</asp:CompareValidator></td>
        </tr>
		<tr>
            <td width="25%">
                <asp:Label ID="Label69" runat="server" SkinID="etiqueta_negra" 
                Text="Característica:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:DropDownList ID="cboCaracteristica" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator6" runat="server"
                    ErrorMessage="Seleccione la Característica" 
                    Display="Dynamic"
                    ControlToValidate="cboCaracteristica" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="Caracteristica">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label1" runat="server" SkinID="etiqueta_negra" 
                Text="Método de determinación:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:DropDownList ID="cboMetDeterminacion" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator1" runat="server"
                    ErrorMessage="Seleccione el Método de determinación" 
                    Display="Dynamic"
                    ControlToValidate="cboMetDeterminacion" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="Caracteristica">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label2" runat="server" SkinID="etiqueta_negra" 
                Text="Límite de Detección:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtLimiteDeteccion" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCodigoMapaSuelosMedAbio" runat="server" 
                ErrorMessage="Ingrese el Límite de Detección"
                    Display="Dynamic" ControlToValidate="txtLimiteDeteccion"
                    ValidationGroup="Caracteristica">*</asp:RequiredFieldValidator>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhFuentesEstCalAgua">
        </asp:PlaceHolder>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarCaracteristica" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="Caracteristica" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarCaracteristica" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarCaracteristica_Click" />
            </td>
            <td align="center" width="25%">OJO!!! FALTA TERMINAR LA GRILLA - VALIDAR CON MOISES</td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvSubCuenca" AutoGenerateColumns="False"
                width="99%"
                    EmptyDataText="No ha agregado información de subcuencas">
                    <Columns>
                        <asp:BoundField HeaderText="Nombre de la cuenca" />
                        <asp:BoundField HeaderText="Área (km2)" />
                        <asp:BoundField HeaderText="Uso Principal" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <%--Caracteristicas --%>
    </tbody>
</table>
</asp:Content>

