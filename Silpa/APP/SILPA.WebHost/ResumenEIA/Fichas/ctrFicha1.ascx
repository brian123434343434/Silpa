<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrFicha1.ascx.cs" Inherits="ResumenEIA_ctrFicha1" %>
<table style="width: 100%">
    <tr>
        <td class="titleUpdate" colspan="4">
        </td>
    </tr>
    <tr>
        <td colspan="4">
            1. DATOS DE LA EMPRESA</td>
    </tr>
    <tr>
        <td class="titleUpdate" colspan="4">
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblNombreEmpresa" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre de la Empresa o Razón Social:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtNombreEmpresa" runat="server" MaxLength="200" 
                SkinID="texto_sintamano" width="699px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNombreEmpresa" runat="server" 
                ControlToValidate="txtNombreEmpresa" Display="Dynamic" 
                ErrorMessage="Ingrese el Nombre o Razón Social de la Empresa" 
                ValidationGroup="Tab1">*</asp:RequiredFieldValidator>           
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblNit" runat="server" SkinID="etiqueta_negra" Text="Nit:"></asp:Label>
        </td>
        <td width="270px">
            <asp:TextBox ID="txtNit" runat="server" MaxLength="15" SkinID="texto_sintamano" 
                width="252px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNit" runat="server" 
                ControlToValidate="txtNit" Display="Dynamic" 
                ErrorMessage="Ingrese el Nit de la Empresa" ValidationGroup="Tab1">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ControlToValidate="txtFax" Display="Dynamic" 
                ErrorMessage="Nit no tiene el formato correcto" ValidationExpression="\d{7,15}" 
                ValidationGroup="Tab1">*</asp:RegularExpressionValidator>
        </td>
        <td>
            <asp:Label ID="lblDv" runat="server" SkinID="etiqueta_negra" Text="DV:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtDV" runat="server" MaxLength="1" SkinID="texto_sintamano" 
                Width="15px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDv" runat="server" ControlToValidate="txtDV" 
                Display="Dynamic" ErrorMessage="Ingrese el Nit de la Empresa" 
                ValidationGroup="Tab1">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revDv" runat="server" 
                ControlToValidate="txtDV" Display="Dynamic" 
                ErrorMessage="DV no tiene el formato correcto" ValidationExpression="\d{1}" 
                ValidationGroup="Tab1">*</asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblDireccion" runat="server" SkinID="etiqueta_negra" 
                Text="Dirección:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtDireccion" runat="server" MaxLength="200" 
                SkinID="texto_sintamano" width="699px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDireccion" runat="server" 
                ControlToValidate="txtDireccion" Display="Dynamic" 
                ErrorMessage="Ingrese la Dirección de la Empresa" ValidationGroup="Tab1">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblTelefono" runat="server" SkinID="etiqueta_negra" 
                Text="Teléfono:"></asp:Label>
        </td>
        <td width="270px">
            <asp:TextBox ID="txtTelefono" runat="server" MaxLength="10" 
                SkinID="texto_sintamano" width="252px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" 
                ControlToValidate="txtTelefono" Display="Dynamic" 
                ErrorMessage="Ingrese la Teléfono de la Empresa" ValidationGroup="Tab1">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revTelefono" runat="server" 
                ControlToValidate="txtTelefono" Display="Dynamic" 
                ErrorMessage="Telefono no tiene formato correcto" 
                ValidationExpression="\d{7,10}" ValidationGroup="Tab1">*</asp:RegularExpressionValidator>
        </td>
        <td>
            <asp:Label ID="lblFax" runat="server" SkinID="etiqueta_negra" Text="Fax:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtFax" runat="server" MaxLength="10" SkinID="texto_sintamano" 
                width="252px"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revFax" runat="server" 
                ControlToValidate="txtFax" Display="Dynamic" 
                ErrorMessage="Fax no tiene el formato correcto" ValidationExpression="\d{7,10}" 
                ValidationGroup="Tab1">*</asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblDepartamento" runat="server" SkinID="etiqueta_negra" 
                Text="Departamento:"></asp:Label>
        </td>
        <td width="270px">
            <asp:DropDownList ID="cboDepartamento" runat="server" AutoPostBack="True" 
                SkinID="lista_desplegable" 
                onselectedindexchanged="cboDepartamento_SelectedIndexChanged" 
                ondatabound="cboDepartamento_DataBound">
            </asp:DropDownList>
            <asp:CompareValidator ID="covDepartamento" runat="server" 
                ControlToValidate="cboDepartamento" Display="Dynamic" 
                ErrorMessage="Seleccione el departamento de la empresa" Operator="NotEqual" 
                ValueToCompare="-1" ValidationGroup="Tab1" >*</asp:CompareValidator>
        </td>
        <td>
            <asp:Label ID="lblMunicipio" runat="server" SkinID="etiqueta_negra" 
                Text="Municipio:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="cboMunicipio" runat="server" SkinID="lista_desplegable">
            </asp:DropDownList>
            <asp:CompareValidator ID="covMunicipio" runat="server" 
                ControlToValidate="cboMunicipio" Display="Dynamic" 
                ErrorMessage="Seleccione el municipio de la empresa" Operator="NotEqual" 
                ValueToCompare="-1" ValidationGroup="Tab1" >*</asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblTipoContribuyente" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Contribuyente:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:DropDownList ID="cboTipoContribuyente" runat="server" AutoPostBack="True" 
                onselectedindexchanged="cboTipoContribuyente_SelectedIndexChanged" 
                SkinID="lista_desplegable">
            </asp:DropDownList>
            <asp:CompareValidator ID="covTipoContribuyente" runat="server" 
                ControlToValidate="cboTipoContribuyente" Display="Dynamic" 
                ErrorMessage="Seleccione el municipio del representante legal" 
                Operator="NotEqual" ValidationGroup="Tab1" ValueToCompare="-1">*</asp:CompareValidator>
        </td>
    </tr>
    <asp:PlaceHolder ID="phrInfoContJuridico" runat="server" Visible="False">
        <tr>
            <td>
                <asp:Label ID="lblNombreRepresentante" runat="server" SkinID="etiqueta_negra" 
                    Text="Nombre Representante Legal:"></asp:Label>
            </td>
            <td width="270px">
                <asp:TextBox ID="txtNombreRepresentante" runat="server" MaxLength="100" 
                    SkinID="texto_sintamano" width="252px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNombreRepresentante" runat="server" 
                    ControlToValidate="txtNombreRepresentante" Display="Dynamic" 
                    ErrorMessage="Ingrese el Nombre del Representante de la Empresa" 
                    ValidationGroup="Tab1">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblApellidoRepresentante" runat="server" SkinID="etiqueta_negra" 
                    Text="Apellido Representante Legal:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtApellidoRepresentante" runat="server" MaxLength="100" 
                    SkinID="texto_sintamano" width="252px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvApellidoRepresentante" runat="server" 
                    ControlToValidate="txtApellidoRepresentante" Display="Dynamic" 
                    ErrorMessage="Ingrese el Apellido del Representante de la Empresa" 
                    ValidationGroup="Tab1">*</asp:RequiredFieldValidator>
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td>
            <asp:Label ID="lblTipoDocumentoRepresentante" runat="server" 
                SkinID="etiqueta_negra" Text="Tipo de Documento:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="cboTipoDocumentoRepresentante" runat="server" 
                SkinID="lista_desplegable">
            </asp:DropDownList>
            <asp:CompareValidator ID="covTipoDocumentoRepresentante" runat="server" 
                ControlToValidate="cboTipoDocumentoRepresentante" Display="Dynamic" 
                ErrorMessage="Seleccione el tipo de documento" Operator="NotEqual" 
                ValueToCompare="-1" ValidationGroup="Tab1" >*</asp:CompareValidator>
        </td>
        <td>
            <asp:Label ID="lblNoDocRepresentante" runat="server" SkinID="etiqueta_negra" 
                Text="No. Documento del Representante:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtNoDocRepresentante" runat="server" MaxLength="30" 
                SkinID="texto_sintamano" width="252px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNoDocRepresentante" runat="server" 
                ControlToValidate="txtNoDocRepresentante" Display="Dynamic" 
                ErrorMessage="Ingrese Número de Documento del Representante de la Empresa" 
                ValidationGroup="Tab1">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                ControlToValidate="txtNoDocRepresentante" Display="Dynamic" 
                ErrorMessage="Nit no tiene el formato correcto" ValidationExpression="\d{7,15}" 
                ValidationGroup="Tab1">*</asp:RegularExpressionValidator>
        </td>
    </tr>
    <asp:PlaceHolder ID="phrInfoCamara" runat="server" Visible="False">
        <tr>
            <td>
                <asp:Label ID="lblRegCamaraComercio" runat="server" SkinID="etiqueta_negra" 
                    Text="Registro Cámara de Comercio:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNoRegCamaraComercio" runat="server" SkinID="etiqueta_negra" 
                    Text="No Registro:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtNoRegCamaraComercio" runat="server" MaxLength="30" 
                    SkinID="texto_sintamano" width="252px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNoRegCamaraComercio" runat="server" 
                    ControlToValidate="txtNoRegCamaraComercio" Display="Dynamic" 
                    ErrorMessage="Ingrese Número de Registro de Cámara de Comercio" 
                    ValidationGroup="Tab1">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="lblNoMatriculaCamaraComercio" runat="server" 
                    SkinID="etiqueta_negra" Text="No Matrícula:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtNoMatriculaCamaraComercio" runat="server" MaxLength="30" 
                    SkinID="texto_sintamano" width="252px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNoMatriculaCamaraComercio" runat="server" 
                    ControlToValidate="txtNoMatriculaCamaraComercio" Display="Dynamic" 
                    ErrorMessage="Ingrese Número de Matrícula de Cámara de Comercio" 
                    ValidationGroup="Tab1">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="lblActividadCamaraComercio" runat="server" 
                    SkinID="etiqueta_negra" Text="Actividad:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtActividadCamaraComercio" runat="server" MaxLength="30" 
                    SkinID="texto_sintamano" ToolTip="Categoría CIUU" width="252px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvActividadCamaraComercio" runat="server" 
                    ControlToValidate="txtActividadCamaraComercio" Display="Dynamic" 
                    ErrorMessage="Ingrese Actividad de la Empresa" ValidationGroup="Tab1">*</asp:RequiredFieldValidator>
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td colspan="4">
            <asp:ValidationSummary ID="valResumenRepresentante" runat="server" 
                ValidationGroup="Tab1" />
        </td>
    </tr>
    <tr>
        <td align="right" colspan="4">
            <asp:Button ID="btnGuardar" Text="Guardar Datos Empresa" ValidationGroup="Tab1" runat="server" SkinID="boton_copia" OnClick="btnGuardar_Click" />
            </td>
    </tr>
</table>
