<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="QyD.aspx.cs" Inherits="QuejasDenuncias_QyD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="div-titulo">
        <asp:Label ID="lbl_titulo" SkinID="Titulo_principal_blanco" runat="server" Text="Quejas y Denuncias - Usuarios no Registrados"></asp:Label>
    </div>
    <div class="div-contenido">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnl_identificacion" runat="server">
                    <table width="600px">
                        <tr>
                            <td colspan="2" style="height:50px">
                                <asp:Label ID="Label1" runat="server" Text="Identificación del Quejoso o Denunciante (Opcional)"
                                    SkinID="titulo_principal"></asp:Label>
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
                                <asp:Label ID="lbl_direccion_natural" runat="server" Text="Dirección:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_direccion_natural" runat="server" SkinID="texto"></asp:TextBox>
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
                                <asp:Label ID="lbl_municipio_natural" runat="server" Text="Municipio:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_municipio_natural" runat="server" SkinID="lista_desplegable">
                                    <asp:ListItem>Medellín</asp:ListItem>
                                    <asp:ListItem Selected="True">BOGOTÁ</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_vereda_natural" runat="server" Text="Vereda:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_vereda_natural" runat="server" SkinID="texto"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_corregimiento_natural" runat="server" Text="Corregimiento:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_corregimiento_natural" runat="server" SkinID="texto"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_correo_natural" runat="server" Text="Correo Electrónico:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_correo_natural" runat="server" SkinID="texto"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_telefono_natural" runat="server" Text="Teléfono(s):" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_telefono_natural" runat="server" SkinID="texto"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnl_localizacion" runat="server">
                    <table width="400px">
                        <tr>
                            <td colspan="2" style="height:50px">
                                <asp:Label ID="lbl_localizacion" runat="server" Text="Localización" SkinID="titulo_principal"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_ubicacion" runat="server" Text="Ubicación de Afectación:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_ubicacion" runat="server" SkinID="texto"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_departamento" runat="server" Text="Departamento:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_departamento" runat="server" SkinID="lista_desplegable">
                                    <asp:ListItem>ANTIOQUIA</asp:ListItem>
                                    <asp:ListItem Selected="True">BOGOTÁ D.C.</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_Municipio" runat="server" Text="Municipio:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_municipio" runat="server" SkinID="lista_desplegable">
                                    <asp:ListItem>Medellín</asp:ListItem>
                                    <asp:ListItem Selected="True">BOGOTÁ</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_vereda" runat="server" Text="Vereda:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_vereda_infractor" runat="server" SkinID="texto"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_corregimiento_infracto" runat="server" Text="Corregimiento:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_corregimiento_infractor" runat="server" SkinID="texto"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_zona" runat="server" Text="Zona:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_zona" runat="server" SkinID="lista_desplegable">
                                    <asp:ListItem>Urbano</asp:ListItem>
                                    <asp:ListItem Selected="True">Rural</asp:ListItem>
                                    <asp:ListItem>Urbano y Rural</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_AA" runat="server" Text="Autoridad Ambiental:" SkinID="etiqueta_negra"></asp:Label>
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
                        <tr>
                            <td>
                                <asp:Label ID="lbl_sector" runat="server" Text="Sector:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_sector" runat="server" DataSourceID="sds_sector" SkinID="lista_desplegable"
                                    DataTextField="NOMBRE" DataValueField="ID_SECTOR" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sds_sector" runat="server" ConnectionString="<%$ ConnectionStrings:SILPAConnectionString %>"
                                    SelectCommand="SELECT * FROM [SIH_SECTOR] ORDER BY [NOMBRE]"></asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_otro" runat="server" Text="¿Otro?" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                ¿Cual?:
                                <asp:TextBox ID="txt_cual" runat="server" SkinID="texto"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_recurso" runat="server" Text="Recurso Presuntamente Afectado:"
                                    SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBoxList ID="chk_recursos" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem>Flora</asp:ListItem>
                                    <asp:ListItem>Fauna</asp:ListItem>
                                    <asp:ListItem>Aire</asp:ListItem>
                                    <asp:ListItem>Agua</asp:ListItem>
                                    <asp:ListItem>Suelo</asp:ListItem>
                                    <asp:ListItem>Paisaje</asp:ListItem>
                                    <asp:ListItem>Otro</asp:ListItem>
                                </asp:CheckBoxList>
                                ¿Cuál?:<asp:TextBox ID="txt_cual_recurso" runat="server" SkinID="texto_corto"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnl_infractor" runat="server">
                    <table width="400px">
                        <tr>
                            <td colspan="2" style="height:50px">
                                <asp:Label ID="Label2" runat="server" Text="Datos del Infractor (Opcional)" SkinID="titulo_principal"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_nombre1_infractor" runat="server" Text="Primer Nombre:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_nombre1_infractor" runat="server" SkinID="texto"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_nombre2_infractor" runat="server" Text="Segundo Nombre:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_nombre2_infractor" runat="server" SkinID="texto"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_apellido1_infractor" runat="server" Text="Primer Apellido:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_apellido1_infractor" runat="server" SkinID="texto"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_apellido2_infractor" runat="server" Text="Segundo Apellido:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_apellido2_infractor" runat="server" SkinID="texto"></asp:TextBox>
                            </td>
                        </tr>
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
                                <asp:Label ID="lbl_documento_infractor" runat="server" Text="N°:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_documento_infractor" runat="server" SkinID="texto"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_origen_documento" runat="server" Text="De:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_origendocumento" runat="server" SkinID="texto"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_direccion_infractor" runat="server" Text="Dirección:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_direccion_infractor" runat="server" SkinID="texto"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_telefono_infractor" runat="server" Text="Teléfono(s):" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_telefono_infractor" runat="server" SkinID="texto"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_nombre_contacto" runat="server" Text="Nombre del Contacto:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_nombre_contacto" runat="server" SkinID="texto"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <table width="400px">
                <tr>
                    
                    <td colspan="2">  <asp:Label ID="lbl_resultado" runat="server" Visible="False" 
                            SkinID="etiqueta_negra"></asp:Label> </td>
                </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btn_enviar" runat="server" Text="Enviar" SkinID="boton" OnClick="btn_enviar_Click" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" SkinID="boton" 
                                onclick="btn_cancelar_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
