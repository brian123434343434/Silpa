<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPALimpia.master" AutoEventWireup="true" CodeFile="Cuenca.aspx.cs" Inherits="ResumenEIA_Fichas_InfoAdCuenca" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table>
    <tbody>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <tr>
            <td colspan="3" class="style1" width="75%">
                2. INFORMACIÓN ADICIONAL DE LAS CUENCAS</td>
            <td width="25%" align="right">
                <asp:Button ID="btnCancelarInfoAdicional" runat="server" SkinID="boton_copia"
                    Text="Cancelar" onclick="btnCancelarInfoAdicional_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
                &nbsp;</td>
        </tr>
        <%--SubCuencas --%>
		<tr>
            <td colspan = "3" width="75%">SubCuencas</td>
            <td width="25%" align="right">
                <asp:Button ID="btnNuevaSubCuenca" runat="server" SkinID="boton_copia"
                    Text="Agregar Subcuenca" onclick="btnNuevaSubCuenca_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhSubCuenca" Visible="False">
        <tr>
            <td width="25%">
                <asp:Label ID="Label55" runat="server" SkinID="etiqueta_negra" 
                Text="Código en el mapa:"></asp:Label></td>
            <td width="25%">
                <asp:TextBox ID="txtCodigoMapaSubCuenca" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Colocar el código con que se identifica la Unidad en el Mapa respectivo"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCodigoMapaSuelosMedAbio" runat="server" 
                ErrorMessage="Ingrese el código en el mapa"
                    Display="Dynamic" ControlToValidate="txtCodigoMapaSubCuenca"
                    ValidationGroup="SubCuenca">*</asp:RequiredFieldValidator>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label70" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre de la Subcuenca:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtNombreSubCuenca" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" 
                ErrorMessage="Ingrese el nombre de la Subcuenca"
                    Display="Dynamic" ControlToValidate="txtNombreSubCuenca"
                    ValidationGroup="SubCuenca">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label71" runat="server" SkinID="etiqueta_negra" 
                Text="Área (km2):"></asp:Label></td>
            <td width="25%">
                <asp:TextBox ID="txtAreaSubCuenca" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAreaSubCuenca" runat="server" 
                ErrorMessage="Ingrese el área de la cuenca hidrográfica"
                    Display="Dynamic" ControlToValidate="txtAreaSubCuenca"
                    ValidationGroup="SubCuenca">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator11" runat="server"
                    ErrorMessage="El el área de la cuenca hidrográfica debe ser un dato numérico" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtAreaSubCuenca" 
                    ValidationGroup="SubCuenca"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label72" runat="server" SkinID="etiqueta_negra" 
                Text="Uso Principal:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtUsoPricSubCuenca" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" 
                ErrorMessage="Ingrese la información del Uso Principal de la cuenca"
                    Display="Dynamic" ControlToValidate="txtUsoPricSubCuenca"
                    ValidationGroup="SubCuenca">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                ValidationGroup="SubCuenca" />
            </td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarSubCuenca" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="SubCuenca"
                    OnClick="btnAgregarSubCuenca_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarSubCuenca" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarSubCuenca_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvSubCuenca" AutoGenerateColumns="False"
                width="99%" OnRowDeleting="grvSubCuenca_RowDeleting"
                    EmptyDataText="No ha agregado información de subcuencas">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField HeaderText="Código del Mapa" DataField="ESC_CODIGO_MAPA" />
                        <asp:BoundField HeaderText="Nombre de la cuenca" DataField="ESC_NOMBRE" />
                        <asp:BoundField HeaderText="Área (km2)" DataField="ESC_AREA" />
                        <asp:BoundField HeaderText="Uso Principal" DataField="ESC_USO_PRINCIPAL"/>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <%--SubCuencas --%>
        <%--MicroCuencas --%>
		<tr>
            <td colspan = "3" width="75%">MicroCuencas</td>
            <td width="25%" align="right">
                <asp:Button ID="btnNuevaMicroCuenca" runat="server" SkinID="boton_copia"
                    Text="Agregar MicroCuenca" onclick="btnNuevaMicroCuenca_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhMicroCuenca" Visible="False">
        <tr>
            <td width="25%">
                <asp:Label ID="Label1" runat="server" SkinID="etiqueta_negra" 
                Text="Código en el mapa:"></asp:Label></td>
            <td width="25%">
                <asp:TextBox ID="txtCodigoMapaMicroCuenca" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Colocar el código con que se identifica la Unidad en el Mapa respectivo"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="Ingrese el código en el mapa"
                    Display="Dynamic" ControlToValidate="txtCodigoMapaMicroCuenca"
                    ValidationGroup="MicroCuenca">*</asp:RequiredFieldValidator>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label2" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre de la MicroCuenca:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtNombreMicroCuenca" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="Ingrese el nombre de la MicroCuenca"
                    Display="Dynamic" ControlToValidate="txtNombreMicroCuenca"
                    ValidationGroup="MicroCuenca">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label3" runat="server" SkinID="etiqueta_negra" 
                Text="Área (km2):"></asp:Label></td>
            <td width="25%">
                <asp:TextBox ID="txtAreaMicroCuenca" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAreaMicroCuenca" runat="server" 
                ErrorMessage="Ingrese el área de la cuenca hidrográfica"
                    Display="Dynamic" ControlToValidate="txtAreaMicroCuenca"
                    ValidationGroup="MicroCuenca">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server"
                    ErrorMessage="El el área de la cuenca hidrográfica debe ser un dato numérico" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtAreaMicroCuenca" 
                    ValidationGroup="MicroCuenca"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label4" runat="server" SkinID="etiqueta_negra" 
                Text="Uso Principal:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtUsoPricMicroCuenca" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="Ingrese la información del Uso Principal de la cuenca"
                    Display="Dynamic" ControlToValidate="txtUsoPricMicroCuenca"
                    ValidationGroup="MicroCuenca">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                ValidationGroup="MicroCuenca" />
            </td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarMicroCuenca" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="MicroCuenca"
                    OnClick="btnAgregarMicroCuenca_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarMicroCuenca" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarMicroCuenca_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvMicroCuenca" AutoGenerateColumns="False"
                width="99%" OnRowDeleting="grvMicroCuenca_RowDeleting"
                    EmptyDataText="No ha agregado información de microcuencas">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField HeaderText="Código del Mapa" DataField="EMC_CODIGO_MAPA"/>
                        <asp:BoundField HeaderText="Nombre" DataField="EMC_NOMBRE"/>
                        <asp:BoundField HeaderText="Área (km2)" DataField="EMC_AREA" />
                        <asp:BoundField HeaderText="Uso Principal" DataField="EMC_USO_PRINCIPAL"/>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <%--MicroCuencas --%>
        
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary3" runat="server" 
                ValidationGroup="RedDrenaje" />
            </td>
        </tr>
        <%--Red de Drenaje --%>
        <%--CuerpoLentico --%>
		<tr>
            <td colspan = "3" width="75%">Cuerpos Lénticos</td>
            <td width="25%" align="right">
                <asp:Button ID="btnNuevaCuerpoLentico" runat="server" SkinID="boton_copia"
                    Text="Agregar Cuerpo Léntico" onclick="btnNuevaCuerpoLentico_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhCuerpoLentico" Visible="False">
        <tr>
            <td width="25%">
                <asp:Label ID="Label8" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre de la Cuerpo Léntico:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtNombreCuerpoLentico" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ErrorMessage="Ingrese el nombre de la Cuerpo Léntico"
                    Display="Dynamic" ControlToValidate="txtNombreCuerpoLentico"
                    ValidationGroup="CuerpoLentico">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label9" runat="server" SkinID="etiqueta_negra" 
                Text="Área (km2):"></asp:Label></td>
            <td width="25%">
                <asp:TextBox ID="txtAreaCuerpoLentico" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAreaCuerpoLentico" runat="server" 
                ErrorMessage="Ingrese el área del cuerpo léntico"
                    Display="Dynamic" ControlToValidate="txtAreaCuerpoLentico"
                    ValidationGroup="CuerpoLentico">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator2" runat="server"
                    ErrorMessage="El área del cuerpo léntico debe ser un dato numérico" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtAreaCuerpoLentico" 
                    ValidationGroup="CuerpoLentico"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label10" runat="server" SkinID="etiqueta_negra" 
                Text="Uso Principal:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtUsoPricCuerpoLentico" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                ErrorMessage="Ingrese la información del Uso Principal del cuerpo léntico"
                    Display="Dynamic" ControlToValidate="txtUsoPricCuerpoLentico"
                    ValidationGroup="CuerpoLentico">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary4" runat="server" 
                ValidationGroup="CuerpoLentico" />
            </td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarCuerpoLentico" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="CuerpoLentico"
                    OnClick="btnAgregarCuerpoLentico_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarCuerpoLentico" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarCuerpoLentico_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvCuerpoLentico" AutoGenerateColumns="False"
                width="99%" OnRowDeleting="grvCuerpoLentico_RowDeleting"
                    EmptyDataText="No ha agregado información de cuerpos lénticos">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True"  />
                        <asp:BoundField HeaderText="Nombre" DataField="ECL_NOMBRE_CUERPO_LENTICO"/>
                        <asp:BoundField HeaderText="Área (km2)"  DataField="ECL_AREA_CUERPO_LENTICO"/>
                        <asp:BoundField HeaderText="Uso Principal" DataField="ECL_USO_PRINCIPAL" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <%--CuerpoLentico --%>
        <%--Inventario de Fuentes de Agua Superficiales en Área de Influencia Directa --%>
		<tr>
            <td colspan = "3" width="75%">Inventario de Fuentes de Agua Superficiales en Área de Influencia Directa</td>
            <td width="25%" align="right">
                <asp:Button ID="btnNuevaInvFAgua" runat="server" SkinID="boton_copia"
                    Text="Agregar Fuentes de Agua Superficiales" onclick="btnNuevaInvFAgua_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhInvFAgua" Visible="False">
        <tr>
            <td width="25%">
                <asp:Label ID="Label11" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtNombreInvFAgua" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                ErrorMessage="Ingrese el nombre"
                    Display="Dynamic" ControlToValidate="txtNombreInvFAgua"
                    ValidationGroup="InvFAgua">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label12" runat="server" SkinID="etiqueta_negra" 
                Text="Caudal Mínimo Mensual (m3/s):"></asp:Label></td>
            <td width="25%">
                <asp:TextBox ID="txtCaudalMinMenInvFAgua" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAreaInvFAgua" runat="server" 
                ErrorMessage="Ingrese el Caudal Mínimo Mensual"
                    Display="Dynamic" ControlToValidate="txtCaudalMinMenInvFAgua"
                    ValidationGroup="InvFAgua">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator4" runat="server"
                    ErrorMessage="El Caudal Mínimo Mensual debe ser un dato numérico" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtCaudalMinMenInvFAgua" 
                    ValidationGroup="InvFAgua"></asp:CompareValidator>
            </td>
            <td width="25%">
                <asp:Label ID="Label13" runat="server" SkinID="etiqueta_negra" 
                Text="Mes de Caudal Mínimo:"></asp:Label></td>
            <td width="25%">
                <asp:DropDownList ID="cboMesCaudalMin" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator7" runat="server"
                    ErrorMessage="Seleccione información del Mes de Caudal Mínimo" 
                    Display="Dynamic" ControlToValidate="cboMesCaudalMin" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="InvFAgua">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label14" runat="server" SkinID="etiqueta_negra" 
                Text="Caudal Máximo Mensual (m3/s):"></asp:Label></td>
            <td width="25%">
                <asp:TextBox ID="txtCauMaxMenInvFAgua" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                ErrorMessage="Ingrese el Caudal Máximo Mensual"
                    Display="Dynamic" ControlToValidate="txtCauMaxMenInvFAgua"
                    ValidationGroup="InvFAgua">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator5" runat="server"
                    ErrorMessage="El Caudal Máximo Mensual debe ser un dato numérico" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtCauMaxMenInvFAgua" 
                    ValidationGroup="InvFAgua"></asp:CompareValidator>
            </td>
            <td width="25%">
                <asp:Label ID="Label15" runat="server" SkinID="etiqueta_negra" 
                Text="Mes de Caudal Máximo:"></asp:Label></td>
            <td width="25%">
                <asp:DropDownList ID="cboMesCaudalMax" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator8" runat="server"
                    ErrorMessage="Seleccione información del Mes de Caudal Máximo" 
                    Display="Dynamic" ControlToValidate="cboMesCaudalMax" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="InvFAgua">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label16" runat="server" SkinID="etiqueta_negra" 
                Text="Caudal Promedio (m3/s):"></asp:Label></td>
            <td width="25%">
                <asp:TextBox ID="txtCauPromInvFAgua" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                    ErrorMessage="Ingrese el Caudal Promedio"
                    Display="Dynamic" ControlToValidate="txtCauPromInvFAgua"
                    ValidationGroup="InvFAgua">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator6" runat="server"
                    ErrorMessage="El Caudal Promedio debe ser un dato numérico" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtCauPromInvFAgua" 
                    ValidationGroup="InvFAgua"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label17" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Intervención:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtTipoIntInvFAgua" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                ErrorMessage="Ingrese la información del Tipo de Intervención"
                    Display="Dynamic" ControlToValidate="txtTipoIntInvFAgua"
                    ValidationGroup="InvFAgua">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary5" runat="server" 
                ValidationGroup="InvFAgua" />
            </td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarInvFAgua" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="InvFAgua" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarInvFAgua" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarInvFAgua_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvInvFAgua" AutoGenerateColumns="False"
                width="99%"
                    EmptyDataText="No ha agregado información de Inventario de Fuentes de Agua Superficiales en Área de Influencia Directa">
                    <Columns>
                        <asp:BoundField HeaderText="Nombre" />
                        <asp:BoundField HeaderText="Caudal Mínimo Mensual (m3/s)" />
                        <asp:BoundField HeaderText="Mes de Caudal Mínimo" />
                        <asp:BoundField HeaderText="Caudal Máximo Mensual (m3/s)" />
                        <asp:BoundField HeaderText="Mes de Caudal Máximo" />
                        <asp:BoundField HeaderText="Caudal Promedio (m3/s)" />
                        <asp:BoundField HeaderText="Tipo de Intervención" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <%--InvFAgua --%>
        </tbody>
    </table>
</asp:Content>

