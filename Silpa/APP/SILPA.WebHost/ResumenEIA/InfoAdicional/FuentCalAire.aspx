<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPALimpia.master" AutoEventWireup="true" CodeFile="FuentCalAire.aspx.cs" Inherits="ResumenEIA_InfoAdicional_FuentCalAire" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table>
    <tbody>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <tr>
            <td colspan="4" class="style1" width="100%">
                INFORMACIÓN ESTUDIO DE CALIDAD DE AIRE</td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
                &nbsp;</td>
        </tr>
        <%--Caracteristicas --%>
		<tr>
            <td width="25%">
                <asp:Label ID="Label69" runat="server" SkinID="etiqueta_negra" 
                Text="Característica Fisioquímica:"></asp:Label></td>
            <td width="25%">
                <asp:DropDownList ID="cboCaracteristica" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator6" runat="server"
                    ErrorMessage="Seleccione la Característica" 
                    Display="Dynamic"
                    ControlToValidate="cboCaracteristica" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="Caracteristica">*</asp:CompareValidator></td>
            <td width="25%">
                <asp:Label ID="Label1" runat="server" SkinID="etiqueta_negra" 
                Text="Método de determinación:"></asp:Label></td>
            <td width="25%">
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
                <asp:Label ID="Label3" runat="server" SkinID="etiqueta_negra" 
                Text="Concentración permitida (ug/m3) ó (mg/m3):"></asp:Label></td>
            <td width="25%">
                <asp:TextBox ID="txtConcentPermit" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="Ingrese la Concentración permitida"
                    Display="Dynamic" ControlToValidate="txtConcentPermit"
                    ValidationGroup="Caracteristica">*</asp:RequiredFieldValidator>
                <asp:CompareValidator runat="server" ID="CompareValidator24"
                    ControlToValidate="txtConcentPermit" Display="Dynamic" 
                    Operator="DataTypeCheck" Type="Double"
                    ValidationGroup="Caracteristica" 
                    ErrorMessage="La información de Concentración permitida debe ser un dato numérico">*</asp:CompareValidator>
            </td>
            <td width="25%">
                <asp:Label ID="Label2" runat="server" SkinID="etiqueta_negra" 
                Text="Frecuencia Muestreo (día):"></asp:Label></td>
            <td width="25%">
                <asp:TextBox ID="txtFrecMuestreo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="Ingrese el Frecuencia Muestreo"
                    Display="Dynamic" ControlToValidate="txtFrecMuestreo"
                    ValidationGroup="Caracteristica">*</asp:RequiredFieldValidator></td>
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
                <asp:GridView runat="server" ID="grvCalAire" AutoGenerateColumns="False"
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

