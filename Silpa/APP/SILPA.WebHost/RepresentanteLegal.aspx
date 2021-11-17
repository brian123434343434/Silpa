<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPASinMenu.master" AutoEventWireup="true"
    CodeFile="RepresentanteLegal.aspx.cs" Inherits="RepresentanteLegal" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="div-titulo">
        <asp:Label ID="lbl_titulo_principal" runat="server" Text="REPRESENTANTE LEGAL" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="div-contenido">
        <asp:ScriptManager ID="scmManejadorRepresentante" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="uppPanelRepresentante" runat="server">
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
                            <td>
                                <asp:Label ID="lblPrimerNombreJuridica" runat="server" SkinID="etiqueta_negra" Text="Primer Nombre Representante Legal:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtPrimerNombreJuridica" runat="server" SkinID="texto" MaxLength="30"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPrimerNombreJuridica" runat="server" ErrorMessage="Ingrese Primer Nombre"
                                    Display="Dynamic" ControlToValidate="txtPrimerNombreJuridica">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revPrimerNombreJuridica" runat="server" ErrorMessage="No se admiten numeros, caracteres especiales o espacios en blanco en el campo Primer Nombre"
                                    Display="Dynamic" ControlToValidate="txtPrimerNombreJuridica" ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚÑñ]{1,30}$">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSegundoNombreJuridica" runat="server" SkinID="etiqueta_negra" Text="Segundo Nombre Representante Legal:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtSegundoNombreJuridica" runat="server" SkinID="texto" MaxLength="30"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revSegundoNombreJuridica" runat="server" ErrorMessage="No se admiten numeros o espacios en blanco en el campo Segundo Nombre"
                                    Display="Dynamic" ControlToValidate="txtSegundoNombreJuridica" ValidationExpression="^[a-zA-Z\sáéíóúÁÉÍÓÚÑñ]{1,30}$">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblPrimerApellidoJuridica" runat="server" SkinID="etiqueta_negra"
                                    Text="Primer Apellido Representante Legal:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtPrimerApellidoJuridica" runat="server" SkinID="texto" MaxLength="30"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPrimerApellidoJuridica" runat="server" ErrorMessage="Ingrese Primer Apellido"
                                    Display="Dynamic" ControlToValidate="txtPrimerApellidoJuridica">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revPrimerApellidoJuridica" runat="server" ErrorMessage="No se admiten numeros, caracteres especiales o espacios en blanco en el campo Primer Apellido"
                                    Display="Dynamic" ControlToValidate="txtPrimerApellidoJuridica" ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚÑñ]{1,30}$">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSegundoApellidoJuridica" runat="server" SkinID="etiqueta_negra"
                                    Text="Segundo Apellido Representante Legal:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtSegundoApellidoJuridica" runat="server" SkinID="texto" MaxLength="30"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revSegundoApellidoJuridica" runat="server" ErrorMessage="No se admiten numeros o caracteres especiales en el campo Segundo Apellido"
                                    Display="Dynamic" ControlToValidate="txtSegundoApellidoJuridica" ValidationExpression="^[a-zA-Z\sáéíóúÁÉÍÓÚÑñ]{1,30}$">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTarjetaProfesional" runat="server" SkinID="etiqueta_negra" Text="Tarjeta Profesional:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtTarjetaProfesional" runat="server" SkinID="texto" MaxLength="15"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTipoDocumentoJuridica" runat="server" SkinID="etiqueta_negra" Text="Tipo de Documento:"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="cboTipoDocumentoJuridica" runat="server" SkinID="lista_desplegable">
                                </asp:DropDownList><%--<asp:CompareValidator ID="covTipoDocumentoJuridica" runat="server"
                                    ErrorMessage="Seleccione el tipo de documento" Display="Dynamic" ControlToValidate="cboTipoDocumentoJuridica"
                                    ValueToCompare="-1" Operator="NotEqual">*</asp:CompareValidator>--%></td>
                        </tr>
                        <tr>
                            <td style="height: 45px">
                                <asp:Label ID="lblIdentificacionJuridica" runat="server" SkinID="etiqueta_negra"
                                    Text="Número de Documento:"></asp:Label></td>
                            <td style="height: 45px">
                                <asp:TextBox ID="txtIdentificacionJuridica" runat="server" SkinID="texto" MaxLength="11"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="rfvIdentificacionJuridica" runat="server" ErrorMessage="Ingrese Número de Identificación"
                                    Display="Dynamic" ControlToValidate="txtIdentificacionJuridica">*</asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator ID="revIdentificacionJuridica" runat="server" ErrorMessage="Formato no válido en el número del documento"
                                    Display="Dynamic" ControlToValidate="txtIdentificacionJuridica" ValidationExpression="\d{6,11}">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="lblOrigenJuridica" runat="server" SkinID="etiqueta_negra" Text="De:"></asp:Label></td>
                            <td valign="top">
                                <asp:DropDownList ID="cboDepartamentoOrigenRepresentante" runat="server" SkinID="lista_desplegable"
                                    OnSelectedIndexChanged="cboDepartamentoOrigenRepresentante_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList><%--<asp:CompareValidator ID="covDepartamentoOrigenRepresentante"
                                    runat="server" ErrorMessage="Seleccione el departamento de origen del documento"
                                    Display="Dynamic" ControlToValidate="cboDepartamentoOrigenRepresentante" ValueToCompare="-1"
                                    Operator="NotEqual">*</asp:CompareValidator>--%><br />
                                <asp:DropDownList ID="cboMunicipioOrigenRepresentante" runat="server" SkinID="lista_desplegable">
                                </asp:DropDownList><%--<asp:CompareValidator ID="covMunicipioOrigenRepresentante" runat="server"
                                    ErrorMessage="Seleccione el municipio de origen del documento" Display="Dynamic"
                                    ControlToValidate="cboMunicipioOrigenRepresentante" ValueToCompare="-1" Operator="NotEqual">*</asp:CompareValidator>--%></td>
                        </tr>
                        <tr>
                            <td class="titleUpdate" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 21px" colspan="2">
                                Datos Para Contacto</td>
                        </tr>
                        <tr>
                            <td class="titleUpdate" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDireccionJuridica" runat="server" SkinID="etiqueta_negra" Text="Dirección de Correspondencia:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtDireccionJuridica" runat="server" SkinID="texto" MaxLength="100"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="rfvDireccionJuridica" runat="server" ErrorMessage="Ingrese direccion de correspondencia"
                                    ControlToValidate="txtDireccionJuridica">*</asp:RequiredFieldValidator>--%></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblPaisJuridica" runat="server" SkinID="etiqueta_negra" Text="País:"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="cboPaisJuridica" runat="server" SkinID="lista_desplegable"
                                    OnSelectedIndexChanged="cboPaisJuridica_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList><asp:CompareValidator ID="covPaisJuridica" runat="server" ErrorMessage="Seleccione el pais del representante legal"
                                    Display="Dynamic" ControlToValidate="cboPaisJuridica" ValueToCompare="-1" Operator="NotEqual">*</asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDepartamentoJuridica" runat="server" SkinID="etiqueta_negra" Text="Departamento:"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="cboDepartamentoJuridica" runat="server" SkinID="lista_desplegable"
                                    OnSelectedIndexChanged="cboDepartamentoJuridica_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList><%--<asp:CompareValidator ID="covDepartamentoJuridica" runat="server"
                                    ErrorMessage="Seleccione el departamento del representante legal" Display="Dynamic"
                                    ControlToValidate="cboDepartamentoJuridica" ValueToCompare="-1" Operator="NotEqual">*</asp:CompareValidator>--%></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMunicipioJuridica" runat="server" SkinID="etiqueta_negra" Text="Municipio:"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="cboMunicipioJuridica" runat="server" SkinID="lista_desplegable"
                                    OnSelectedIndexChanged="cboMunicipioJuridica_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList><%--<asp:CompareValidator ID="covMunicipioJuridica" runat="server"
                                    ErrorMessage="Seleccione el municipio del representante legal" Display="Dynamic"
                                    ControlToValidate="cboMunicipioJuridica" ValueToCompare="-1" Operator="NotEqual">*</asp:CompareValidator>--%></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCorregimientoJuridica" runat="server" SkinID="etiqueta_negra" Text="Corregimiento:"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="cboCorregimientoJuridica" runat="server" SkinID="lista_desplegable"
                                    OnSelectedIndexChanged="cboCorregimientoJuridica_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblVeredaJuridica" runat="server" SkinID="etiqueta_negra" Text="Vereda:"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="cboVeredaJuridica" runat="server" SkinID="lista_desplegable">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTelefonoJuridica" runat="server" SkinID="etiqueta_negra" Text="Teléfono:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtTelefonoJuridica" runat="server" SkinID="texto" MaxLength="10"></asp:TextBox>&nbsp;<asp:RegularExpressionValidator
                                    ID="revTelefonoJuridica" runat="server" ErrorMessage="Telefono no tiene formato correcto"
                                    ControlToValidate="txtTelefonoJuridica" ValidationExpression="\d{7,10}">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCelularJuridica" runat="server" SkinID="etiqueta_negra" Text="Celular:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtCelularJuridica" runat="server" SkinID="texto" MaxLength="13"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revCelularJuridica" runat="server" ErrorMessage="Celular no tiene el formato correcto"
                                    ControlToValidate="txtCelularJuridica" ValidationExpression="\d{10,13}">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblFaxJuridica" runat="server" SkinID="etiqueta_negra" Text="Fax:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtFaxJuridica" runat="server" SkinID="texto" MaxLength="10"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revFaxJuridica" runat="server" ErrorMessage="Fax no tiene el formato correcto"
                                    ControlToValidate="txtFaxJuridica" ValidationExpression="\d{7,10}">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCorreoJuridica" runat="server" SkinID="etiqueta_negra" Text="Correo Electrónico Representante Legal:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtCorreoJuridica" runat="server" SkinID="texto"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="rfvCorreoJuridica" runat="server" ErrorMessage="Ingrese Correo Electrónico"
                                    Display="Dynamic" ControlToValidate="txtCorreoJuridica">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revCorreoJuridica" runat="server" ErrorMessage="Formato de correo no válido"
                                    Display="Dynamic" ControlToValidate="txtCorreoJuridica" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td>
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
                                <asp:ValidationSummary ID="valResumenRepresentante" runat="server"></asp:ValidationSummary>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table width="100%">
            <tr>
                <td>
                    <asp:Button ID="btnAceptarJuridica" SkinID="boton_copia" runat="server" Text="Aceptar"
                        OnClick="btnAceptarRepresentante_Click" />
                    <asp:Button ID="btnCancelarJuridica" SkinID="boton_copia" runat="server" Text="Cancelar"
                        OnClick="btnCancelarRepresentante_Click" CausesValidation="False" /></td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
