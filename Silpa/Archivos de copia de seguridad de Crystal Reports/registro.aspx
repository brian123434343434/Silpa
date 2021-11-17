<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="registro.aspx.cs" Inherits="registro" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align:center; vertical-align:middle;line-height:4em; width:800px; height:50px; background-image:url(App_Themes/Img/iconos/titulo.bmp);background-repeat:no-repeat;background-position:center ">
    <asp:Label ID="lbl_titulo_principal" runat="server" Text="REGISTRO DE USUARIO" SkinID="titulo_principal_blanco"></asp:Label>
</div>
<div class="div-contenido">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <cc1:TabContainer ID="tab_container" runat="server" Height="600px">
                <cc1:TabPanel runat="server" ID="tpn_datos_usuario" HeaderText="Datos de usuario">
                    <ContentTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="2" style="height: 70px">
                                    <asp:Label ID="lbl_advertencia" runat="server" Font-Names="Tahoma" ForeColor="Green"
                                        Text="Para validar el siguiente registro debe presentarse ante la Autoridad Ambiental correspondiente y presentar su documento de Identificación."
                                        Font-Size="18px"></asp:Label>
                                    <asp:Label ID="lbl_advertencia2" runat="server" Font-Names="Tahoma" ForeColor="Blue"
                                        Text="Para el caso de Personas Jurídicas, Certificado de existencia y representación legal. "
                                        Font-Size="18px"></asp:Label>
                                    <asp:Label ID="lbl_advertencia3" runat="server" Font-Names="Tahoma" ForeColor="Blue"
                                        Text="En caso que desee actuar por medio de apoderado se deberá presentar el correspondiente poder."
                                        Font-Size="18px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_tipo_usuario" runat="server" Text="Tipo de Usuario:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbl_tipo_persona" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack="True" OnSelectedIndexChanged="rbl_tipo_persona_SelectedIndexChanged">
                                        <asp:ListItem Value="natural">Persona Natural</asp:ListItem>
                                        <asp:ListItem Value="juridica">Persona Jurídica</asp:ListItem>
                                    </asp:RadioButtonList>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_nombre_usuario" runat="server" Text="Nombre de Usuario:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_nombre_usuario" runat="server" SkinID="texto" Width="200px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv_nombre_usuario" runat="server" ControlToValidate="txt_nombre_usuario"
                                        Text="*" ErrorMessage="El nombre de usuario es requerido"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfv_nombre_usuario"
                                        Width="200px" Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_clave" runat="server" SkinID="etiqueta_negra" Text="Contraseña:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_clave" runat="server" SkinID="texto" Width="200px" TextMode="Password"
                                        Text="12345678"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv_clave" runat="server" ControlToValidate="txt_clave"
                                        Text="*" ErrorMessage="La contraseña es requerida"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfv_clave"
                                        Width="200px" Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_clave_confirmar" runat="server" Text="Confirmar Contraseña:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_clave_confirmar" runat="server" SkinID="texto" Width="200px"
                                        TextMode="Password" Text="12345678"></asp:TextBox>
                                    <asp:CompareValidator runat="server" ControlToCompare="txt_clave_confirmar" ControlToValidate="txt_clave"
                                        Text="*" ErrorMessage="La contraseña no coincide"></asp:CompareValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfv_clave"
                                        Width="200px" Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_pregunta" runat="server" Text="Pregunta Secreta:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_pregunta" runat="server" SkinID="texto" Width="200px" Text="¿Ciudad de Nacimiento?"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv_pregunta" runat="server" ControlToValidate="txt_pregunta"
                                        Text="*" ErrorMessage="La Pregunta es Requerida"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="rfv_pregunta"
                                        Width="200px" Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 68px">
                                    <asp:Label ID="lbl_respuesta" runat="server" Text="Respuesta:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td style="height: 68px">
                                    <asp:TextBox ID="txt_respuesta" runat="server" SkinID="texto" Width="200px" Text="Cali"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv_respuesta" runat="server" ControlToValidate="txt_respuesta"
                                        Text="*" ErrorMessage="La respuesta es requerida"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="rfv_respuesta"
                                        Width="200px" Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Autoridad Ambiental a la cual desea enviar su solicitud:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_autoridad" runat="server" DataSourceID="sds_AA1" DataTextField="AUT_NOMBRE"
                                        DataValueField="AUT_ID" SkinID="lista_desplegable">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sds_AA" runat="server" ConnectionString="<%$ ConnectionStrings:SILPAConnectionString %>"
                                        SelectCommand="SELECT * FROM [AUTORIDAD_AMBIENTAL] WHERE ([AUT_ACTIVO] = @AUT_ACTIVO)">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="true" Name="AUT_ACTIVO" Type="Boolean" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <asp:SqlDataSource ID="sds_AA1" runat="server" ConnectionString="<%$ ConnectionStrings:SILPAConnectionString %>"
                                        SelectCommand="SELECT * FROM V_AUTORIDAD_AMBIENTAL"></asp:SqlDataSource>
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel runat="server" ID="tpn_datos_natural" HeaderText="Datos Persona Natural">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_tipo_documento" runat="server" Text="Tipo de Documento:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_tipo_documento" runat="server" SkinID="lista_desplegable">
                                        <asp:ListItem>Cédula Ciudadanía</asp:ListItem>
                                        <asp:ListItem>Cédula Extranjería</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_numero_identificacion" runat="server" Text="N°:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_numero_identificacion" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_origen_identificacion" runat="server" Text="De:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_origen_identificacion" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_tp_natural" runat="server" Text="Tarjeta Profesional:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_tp_natural" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_primer_nombre_natural" runat="server" Text="Primer Nombre:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_primer_nombre_natural" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_segundo_nombre_natural" runat="server" Text="Segundo Nombre:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_segundo_nombre_natural" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_primer_apellido_natural" runat="server" Text="Primer Apellido:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_primer_apellido_natural" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_segundo_apellido_natural" runat="server" Text="Segundo Apellido:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_segundo_apellido_natural" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_correo1_natural" runat="server" Text="Correo Electrónico:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_correo1_natural" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_correo2_natural" runat="server" Text="Correo Electrónico Alterno:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_correo2_natural" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_telefono_natural" runat="server" Text="Teléfono:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_telefono_natural" runat="server" SkinID="texto"></asp:TextBox>
                                    Ext.
                                    <asp:TextBox ID="txt_ext_natural" runat="server" Columns="3"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_fax_natural" runat="server" Text="Fax:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_fax_natural" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_celular_natural" runat="server" Text="Celular:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_celular_natural" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_direccion_natural" runat="server" Text="Dirección:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_direccion_natural" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_direccion_correspondencia_natural" runat="server" Text="Dirección de Correspondencia:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_correspondecia_natural" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_pais_natural" runat="server" Text="País:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_pais_natural" runat="server" SkinID="lista_desplegable">
                                        <asp:ListItem Selected="True">COLOMBIA</asp:ListItem>
                                        <asp:ListItem>PANAMÁ</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_departamento_natural" runat="server" Text="Departamento:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_departamento_natural" runat="server" SkinID="lista_desplegable">
                                        <asp:ListItem>ANTIOQUIA</asp:ListItem>
                                        <asp:ListItem Selected="True">BOGOTÁ D.C.</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_municipio_natural" runat="server" Text="Ciudad:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_municipio_natural" runat="server" SkinID="lista_desplegable">
                                        <asp:ListItem>Medellín</asp:ListItem>
                                        <asp:ListItem Selected="True">BOGOTÁ</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel runat="server" ID="tpn_datos_juridica" HeaderText="Datos Persona Jurídica">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_tipo_juridica" runat="server" Text="Tipo de Persona:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_tipo_juridica" runat="server" SkinID="lista_desplegable">
                                        <asp:ListItem>Pública</asp:ListItem>
                                        <asp:ListItem>Privada</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_nit_razon" runat="server" Text="NIT:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_nit_razon" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_nombre_razon" runat="server" Text="Nombre:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_nombre_razon" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_primer_nombre_representante" runat="server" Text="Primer Nombre Representante Legal:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_primer_nombre_representante" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_segundo_nombre_representante" runat="server" Text="Segundo Nombre Representante Legal:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_segundo_nombre_representante" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_primer_apellido_representante" runat="server" Text="Primero Apellido Representante Legal:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_primer_apellido_representante" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_segundo_apellido_representante" runat="server" Text="Segundo Apellido Representante Legal:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_segundo_apellido_representante" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_correo_representante" runat="server" Text="Correo Electrónico Representante Legal:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_correo_representante" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_correo2_representante" runat="server" Text="Correo Electrónico Alterno Representante Legal:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_correo2_representante" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_cc_representante" runat="server" Text="N° Documento Representante Legal:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_cc_representante" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_correo_juridica" runat="server" Text="Correo Electrónico:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_correo_juridica" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_correo2_juridica" runat="server" Text="Correo Electrónico Alterno:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_correo2_juridica" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_telefono_juridica" runat="server" Text="Teléfono:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_telefono_juridica" runat="server" SkinID="texto"></asp:TextBox>
                                    Ext.
                                    <asp:TextBox ID="txt_ext_juridica" runat="server" Columns="3"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_fax_juridica" runat="server" Text="Fax:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_fax_juridica" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_celular_juridica" runat="server" Text="Celular:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_celular_juridica" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_direccion_juridica" runat="server" Text="Dirección Correspondencia:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_direccion_juridica" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_direccion_empresa" runat="server" Text="Dirección de la Empresa:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_direccion_empresa" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_telefono_empresa" runat="server" Text="Teléfono Empresa:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_telefono_empresa" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_fax_empresa" runat="server" Text="Fax Empresa:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_fax_empresa" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_pais_juridica" runat="server" Text="País:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_pais_juridica" runat="server" SkinID="lista_desplegable">
                                        <asp:ListItem>COLOMBIA</asp:ListItem>
                                        <asp:ListItem>PANAMÁ</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_departamento_juridica" runat="server" Text="Departamento:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_departamente_juridica" runat="server" SkinID="lista_desplegable">
                                        <asp:ListItem>ANTIOQUIA</asp:ListItem>
                                        <asp:ListItem>BOGOTÁ D.C.</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_ciudad_juridica" runat="server" Text="Ciudad:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_ciudad_juridica" runat="server" SkinID="lista_desplegable">
                                        <asp:ListItem>Medellín</asp:ListItem>
                                        <asp:ListItem>BOGOTÁ</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel runat="server" ID="tpn_datos_apoderado" HeaderText="Datos Apoderado">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_tipo_documento_apoderado" runat="server" Text="Tipo de Documento:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_tipo_documento_apoderado" runat="server" SkinID="lista_desplegable">
                                        <asp:ListItem>Cédula Ciudadanía</asp:ListItem>
                                        <asp:ListItem>Cédula Extranjería</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_numero_identificacion_apoderado" runat="server" Text="Número de Identificación:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_numero_identificacion_apoderado" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_primer_nombre_apoderado" runat="server" Text="Primer Nombre:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_primer_nombre_apoderado" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_segundo_nombre_apoderado" runat="server" Text="Segundo Nombre:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_segundo_nombre_apoderado" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_primer_apellido_apoderado" runat="server" Text="Primer Apellido:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_primer_apellido_apoderado" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_segundo_apellido_apoderado" runat="server" Text="Segundo Apellido:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_segundo_apellido_apoderado" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_tarjeta_profesional_apoderado" runat="server" Text="Tarjeta Profesional:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_tarjeta_profesional_apoderado" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_correo1_apoderado" runat="server" Text="Correo Electrónico:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_correo1_apoderado" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_correo2_apoderado" runat="server" Text="Correo Electrónico Alterno:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_correo2_apoderado" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_telefono_apoderado" runat="server" Text="Teléfono:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_telefono_apoderado" runat="server" SkinID="texto"></asp:TextBox>
                                    Ext.
                                    <asp:TextBox ID="txt_ext_apoderado" runat="server" Columns="3"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_fax_apoderado" runat="server" Text="Fax:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_fax_apoderado" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_celular_apoderado" runat="server" Text="Celular:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_celular_apoderado" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_direccion_apoderado" runat="server" Text="Dirección Correspondencia:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_direccion_apoderado" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_numero_radicacion_poder" runat="server" Text="Número de Radicación del Poder:"
                                        SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_numero_radicacion_poder" runat="server" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_pais_apoderado" runat="server" Text="País:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_pais_apoderado" runat="server" SkinID="lista_desplegable">
                                        <asp:ListItem Selected="True">COLOMBIA</asp:ListItem>
                                        <asp:ListItem>PANAMÁ</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_departamento_apoderado" runat="server" Text="Departamento:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_departamento_apoderado" runat="server" SkinID="lista_desplegable">
                                        <asp:ListItem>ANTIOQUIA</asp:ListItem>
                                        <asp:ListItem Selected="True">BOGOTÁ D.C.</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_ciudad_apoderado" runat="server" Text="Ciudad:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_ciudad_apoderado" runat="server" SkinID="lista_desplegable">
                                        <asp:ListItem>Medellín</asp:ListItem>
                                        <asp:ListItem Selected="True">BOGOTÁ</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <asp:Button ID="btn_guardar" SkinID="boton" runat="server" Text="Guardar" OnClick="btn_guardar_Click" />
            <asp:Label ID="lbl_resultado" runat="server" Font-Size="14px" Text="" SkinID="etiqueta_negra"></asp:Label>
            <br />
            <asp:Label ID="lbl_resultado2" runat="server" Font-Names="Tahoma" ForeColor="Green"
                Visible="false" Text="Recuerde que debe presentarse personalmente y llevar la documentación necesaria en la autoridad ambiental seleccionada, para que le validen su registro"
                Font-Size="18px"></asp:Label>
            <asp:Label ID="lbl_resultado3" runat="server" Font-Names="Tahoma" ForeColor="blue"
                Visible="false" Text="" Font-Size="18px"></asp:Label>
            <asp:Label ID="lbl_resultado4" runat="server" Font-Names="Tahoma" ForeColor="blue"
                Visible="false" Text="" Font-Size="18px"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>
