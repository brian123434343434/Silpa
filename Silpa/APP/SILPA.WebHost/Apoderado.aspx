<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPASinMenu.master" AutoEventWireup="true"
    CodeFile="Apoderado.aspx.cs" Inherits="Apoderado" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="div-titulo">
        <asp:Label ID="lbl_titulo_principal" runat="server" Text="APODERADO" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="div-contenido">
        <asp:ScriptManager ID="scmManejadorApoderado" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="uppPanelApoderado" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tbody>
                        <tr>
                            <td class="titleUpdate" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                Datos Personales</td>
                        </tr>
                        <tr>
                            <td class="titleUpdate" colspan="2">
                            </td>
                        </tr>
                        <tr>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblPrimerNombreApoderado" runat="server" SkinID="etiqueta_negra" Text="Primer Nombre:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtPrimerNombreApoderado" runat="server" SkinID="texto" MaxLength="30"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPrimerNombreApoderado" runat="server" Display="Dynamic"
                                    ControlToValidate="txtPrimerNombreApoderado" ErrorMessage="Ingrese Primer Nombre">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revPrimerNombreApoderado" runat="server" Display="Dynamic"
                                    ControlToValidate="txtPrimerNombreApoderado" ErrorMessage="No se admiten numeros, caracteres especiales o espacios en el campo Primer Nombre"
                                    ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚÑñ]{1,30}$">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSegundoNombreApoderado" runat="server" SkinID="etiqueta_negra"
                                    Text="Segundo Nombre:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtSegundoNombreApoderado" runat="server" SkinID="texto" MaxLength="30"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revSegundoNombreApoderado" runat="server" Display="Dynamic"
                                    ControlToValidate="txtSegundoNombreApoderado" ErrorMessage="No se admiten numeros ni caracteres especiales en el campo Segundo Nombre"
                                    ValidationExpression="^[a-zA-Z\sáéíóúÁÉÍÓÚÑñ]{1,30}$">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblPrimerApellidoApoderado" runat="server" SkinID="etiqueta_negra"
                                    Text="Primer Apellido:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtPrimerApellidoApoderado" runat="server" SkinID="texto" MaxLength="30"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="rfvPrimerApellidoApoderado" runat="server" Display="Dynamic" ControlToValidate="txtPrimerApellidoApoderado"
                                    ErrorMessage="Ingrese Primer Apellido">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revPrimerApellidoApoderado" runat="server" Display="Dynamic"
                                    ControlToValidate="txtPrimerApellidoApoderado" ErrorMessage="No se admiten numeros, caracteres especiales o espacios en blanco en el campo Primer Apellido"
                                    ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚÑñ]{1,30}$">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSegundoApellidoApoderado" runat="server" SkinID="etiqueta_negra"
                                    Text="Segundo Apellido:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtSegundoApellidoApoderado" runat="server" SkinID="texto" MaxLength="30"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revSegundoApellidoApoderado" runat="server" Display="Dynamic"
                                    ControlToValidate="txtSegundoApellidoApoderado" ErrorMessage="No se admiten numeros ni caracteres especiales en el campo Segundo Apellido"
                                    ValidationExpression="^[a-zA-Z\sáéíóúÁÉÍÓÚÑñ]{1,30}$">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTpDocumentoAcreditacion" runat="server" SkinID="etiqueta_negra" Text="Tipo de Documento de Acreditación"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboTipoDocumentoACreditacion" runat="server" SkinID="lista_desplegable">
                                </asp:DropDownList><%--<asp:CompareValidator ID="covTipoDocumentoAcreditacion" runat="server"
                                    Display="Dynamic" ControlToValidate="cboTipoDocumentoAcreditacion" ErrorMessage="Seleccione el tipo de documento de Acreditación"
                                    ValueToCompare="-1" Operator="NotEqual">*</asp:CompareValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNoACreditacion" runat="server" SkinID="etiqueta_negra" Text="No de Documento de Acreditación:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtNoAcreditacion" runat="server" SkinID="texto" MaxLength="15"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="rfvNoAcreditacion" runat="server" Display="Dynamic"
                                    ControlToValidate="txtNoAcreditacion" ErrorMessage="Ingrese No de Documento de Acreditación">*</asp:RequiredFieldValidator>--%></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTipoDocumentoApoderado" runat="server" SkinID="etiqueta_negra"
                                    Text="Tipo de Documento:"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="cboTipoDocumentoApoderado" runat="server" SkinID="lista_desplegable">
                                </asp:DropDownList><%--<asp:CompareValidator ID="covTipoDocumentoApoderado" runat="server"
                                    Display="Dynamic" ControlToValidate="cboTipoDocumentoApoderado" ErrorMessage="Seleccione el tipo de documento"
                                    ValueToCompare="-1" Operator="NotEqual">*</asp:CompareValidator>--%></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNumeroIdentificacionApoderado" runat="server" SkinID="etiqueta_negra"
                                    Text="Número de Documento:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtNumeroIdentificacionApoderado" runat="server" SkinID="texto"
                                    MaxLength="11"></asp:TextBox><asp:RequiredFieldValidator ID="rfvIdentificacionApoderado"
                                        runat="server" Display="Dynamic" ControlToValidate="txtNumeroIdentificacionApoderado"
                                        ErrorMessage="Ingrese Número de Identificación">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revNumeroIdentificacionApoderado" runat="server"
                                    Display="Dynamic" ControlToValidate="txtNumeroIdentificacionApoderado" ErrorMessage="Formato no válido en el número del documento"
                                    ValidationExpression="\d{6,11}">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="lblOrigenApoderado" runat="server" SkinID="etiqueta_negra" Text="De:"></asp:Label></td>
                            <td valign="top">
                                <asp:DropDownList ID="cboDepartamentoOrigenApoderado" runat="server" SkinID="lista_desplegable"
                                    OnSelectedIndexChanged="cboDepartamentoOrigenApoderado_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList><%--<asp:CompareValidator ID="covDepartamentoOrigenApoderado" runat="server"
                                    Display="Dynamic" ControlToValidate="cboDepartamentoOrigenApoderado" ErrorMessage="Seleccione el departamento de origen del documento"
                                    ValueToCompare="-1" Operator="NotEqual">*</asp:CompareValidator>--%><br />
                                <asp:DropDownList ID="cboMunicipioOrigenApoderado" runat="server" SkinID="lista_desplegable">
                                </asp:DropDownList><%--<asp:CompareValidator ID="covMunicipioOrigenApoderado" runat="server"
                                    Display="Dynamic" ControlToValidate="cboMunicipioOrigenApoderado" ErrorMessage="Seleccione el municipio de origen del documento"
                                    ValueToCompare="-1" Operator="NotEqual">*</asp:CompareValidator>--%></td>
                        </tr>
                        <tr>
                            <td class="titleUpdate" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                Datos Para Contacto</td>
                        </tr>
                        <tr>
                            <td class="titleUpdate" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDireccionApoderado" runat="server" SkinID="etiqueta_negra" Text="Dirección de Correspondencia:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtDireccionApoderado" runat="server" SkinID="texto" MaxLength="100"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="rfvDireccionApoderado" runat="server" ControlToValidate="txtDireccionApoderado"
                                    ErrorMessage="Ingrese Dirección">*</asp:RequiredFieldValidator>--%></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblPaisApoderado" runat="server" SkinID="etiqueta_negra" Text="País:"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="cboPaisApoderado" runat="server" SkinID="lista_desplegable"
                                    OnSelectedIndexChanged="cboPaisApoderado_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList><asp:CompareValidator ID="covPaisApoderado" runat="server" Display="Dynamic"
                                    ControlToValidate="cboPaisApoderado" ErrorMessage="Seleccione el pais del apoderado"
                                    ValueToCompare="-1" Operator="NotEqual">*</asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDepartamentoApoderado" runat="server" SkinID="etiqueta_negra" Text="Departamento:"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="cboDepartamentoApoderado" runat="server" SkinID="lista_desplegable"
                                    OnSelectedIndexChanged="cboDepartamentoApoderado_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList><%--<asp:CompareValidator ID="covDepartamentoApoderado" runat="server"
                                    Display="Dynamic" ControlToValidate="cboDepartamentoApoderado" ErrorMessage="Seleccione el departamento del apoderado"
                                    ValueToCompare="-1" Operator="NotEqual">*</asp:CompareValidator>--%></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMunicipioApoderado" runat="server" SkinID="etiqueta_negra" Text="Municipio:"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="cboMunicipioApoderado" runat="server" SkinID="lista_desplegable"
                                    OnSelectedIndexChanged="cboMunicipioApoderado_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList><%--<asp:CompareValidator ID="covMunicipioApoderado" runat="server"
                                    Display="Dynamic" ControlToValidate="cboMunicipioApoderado" ErrorMessage="Seleccione el municipio del apoderado"
                                    ValueToCompare="-1" Operator="NotEqual">*</asp:CompareValidator>--%></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCorregimientoApoderado" runat="server" SkinID="etiqueta_negra"
                                    Text="Corregimiento:"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="cboCorregimientoApoderado" runat="server" SkinID="lista_desplegable"
                                    OnSelectedIndexChanged="cboCorregimientoApoderado_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblVeredaApoderado" runat="server" SkinID="etiqueta_negra" Text="Vereda:"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="cboVeredaApoderado" runat="server" SkinID="lista_desplegable">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTelefonoApoderado" runat="server" SkinID="etiqueta_negra" Text="Teléfono"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtTelefonoApoderado" runat="server" SkinID="texto"></asp:TextBox>&nbsp;<asp:RegularExpressionValidator
                                    ID="revTelefonoApoderado" runat="server" ControlToValidate="txtTelefonoApoderado"
                                    ErrorMessage="Telefono no tiene el formato correcto" ValidationExpression="\d{7,10}">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCelularApoderado" runat="server" SkinID="etiqueta_negra" Text="Celular:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtCelularApoderado" runat="server" SkinID="texto"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revCelularApoderado" runat="server" ControlToValidate="txtCelularApoderado"
                                    ErrorMessage="Celular no tiene el formato correcto." ValidationExpression="\d{10,13}">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblFaxApoderado" runat="server" SkinID="etiqueta_negra" Text="Fax:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtFaxApoderado" runat="server" SkinID="texto"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revFaxApoderado" runat="server" ControlToValidate="txtFaxApoderado"
                                    ErrorMessage="Fax no tiene el formato correcto" ValidationExpression="\d{7,10}">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCorreoApoderado" runat="server" SkinID="etiqueta_negra" Text="Correo Electrónico:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtCorreoApoderado" runat="server" SkinID="texto"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCorreoApoderado" runat="server" Display="Dynamic"
                                    ControlToValidate="txtCorreoApoderado" ErrorMessage="Ingrese Correo Electrónico">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revCorreoApoderado" runat="server" Display="Dynamic"
                                    ControlToValidate="txtCorreoApoderado" ErrorMessage="Formato de correo no válido"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr id="tr_Estado" runat="server" visible="false">
                            <td>
                                <asp:Label ID="lblEstado" runat="server" Text="Activo:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkEstado" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:ValidationSummary ID="valResumenApoderado" runat="server"></asp:ValidationSummary>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table width="100%">
            <tr>
                <td>
                    <asp:Button ID="btnAceptarApoderado" SkinID="boton_copia" runat="server" Text="Aceptar"
                        OnClick="btnAceptarApoderado_Click" />
                    <asp:Button ID="btnCancelarApoderado" SkinID="boton_copia" runat="server" Text="Cancelar"
                        OnClick="btnCancelarApoderado_Click" CausesValidation="False" /></td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
