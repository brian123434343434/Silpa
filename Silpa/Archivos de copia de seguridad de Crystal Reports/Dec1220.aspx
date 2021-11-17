<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="Dec1220.aspx.cs" Inherits="LicenciasAmbientales_Dec1220" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--    <div class="div-titulo">
        <asp:Label ID="lbl_titulo_principal" runat="server" Text="Solicitud de DAA y TDR para EIA"
            SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="div-contenido">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnl_proyecto" runat="server">
                    <table id="datos_proyecto" width="400">
                        <tr>
                            <td style="text-align: left" colspan="2">
                                <asp:Label ID="lbl_titulo" runat="server" Text="Información del Proyecto" SkinID="titulo_principal"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSector" runat="server" Text="Sector:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td align="center" style="width: 429px">
                                <asp:DropDownList ID="ddlSector" runat="server" SkinID="lista_desplegable" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlSector_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTipoProyecto" runat="server" Text="Tipo proyecto:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td align="center" style="width: 429px">
                                <asp:DropDownList ID="ddlTipoProyecto" runat="server" SkinID="lista_desplegable"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_AdjuntarAnexo" runat="server" Text="Adjuntar Anexo:" SkinID="etiqueta_negra"
                                    Width="102px"></asp:Label>
                            </td>
                            <td style="width: 429px" align="center">
                                <asp:FileUpload ID="fu_AdjAnexo" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="Label1" runat="server" Text="Ubicación" SkinID="titulo_principal"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDepto" runat="server" Text="Departamento:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="ddl_Depto" runat="server" SkinID="lista_desplegable" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddl_Depto_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMunicipio" runat="server" Text="Municipio:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="ddl_Municipio" runat="server" SkinID="lista_desplegable" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddl_Municipio_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCorregimiento" runat="server" Text="Corregimiento:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="ddl_Corregimiento" runat="server" SkinID="lista_desplegable"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblVereda" runat="server" Text="Vereda:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="ddl_Vereda" runat="server" SkinID="lista_desplegable" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCuenca" runat="server" Text="Cuenca:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="ddl_Cuenca" runat="server" SkinID="lista_desplegable" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblUrbano" runat="server" Text="Urbano:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:CheckBox ID="chk_Urbano" runat="server" SkinID="check" AutoPostBack="True"></asp:CheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center" style="height: 25px">
                                <asp:Button ID="btnUbicacion" runat="server" Text="Asociar Ubicacion" SkinID="boton"
                                    OnClick="btnUbicacion_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:GridView ID="gv_Ubicacion" runat="server" SkinID="Grilla_simple_peq" AutoGenerateColumns="False"
                                    Height="22px">
                                    <Columns>
                                        <asp:BoundField HeaderText="Departamento" DataField="Departamento"></asp:BoundField>
                                        <asp:BoundField HeaderText="Municipio" DataField="Municipio"></asp:BoundField>
                                        <asp:BoundField HeaderText="Corregimiento" DataField="Corregimiento"></asp:BoundField>
                                        <asp:BoundField HeaderText="Vereda" DataField="Vereda"></asp:BoundField>
                                        <asp:BoundField HeaderText="Cuenca" DataField="Cuenca"></asp:BoundField>
                                        <asp:BoundField HeaderText="Urbano" DataField="Urbano"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlAA" runat="server" Visible="false">
                    <table id="tb_AutAmbiental" width="400">
                        <tr>
                            <td>
                                <asp:Label ID="lbl_AA" runat="server" Text="Autoridad Ambiental:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td style="height: 67px">
                                <asp:DropDownList ID="ddl_AA" runat="server" SkinID="lista_desplegable" DataValueField="ID_AUTORIDAD_AMBIENTAL"
                                    OnSelectedIndexChanged="ddl_AA_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlBotones" runat="server" Visible="true">
                    <table width="400">
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="bttAceptar" runat="server" Text="Aceptar" SkinID="boton" OnClick="bttAceptar_Click" />
                                <asp:Button ID="btnSalir" runat="server" Text="Salir" SkinID="boton"/>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
<%--        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnl_AutAmbiental" runat="server" Visible="false">
                    
                </asp:Panel>
                <asp:Panel ID="pnl_MAVDT" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <asp:Button ID="btn_DAA" runat="server" Text="Diligenciar Solicitud DAA" OnClick="btn_DAA_Click"
                                    SkinID="boton" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
                <br />
                <br />
                <asp:Panel ID="pnl_AA" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbl_titulo_eia" runat="server" Text="Información EIA" SkinID="titulo_principal"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <asp:RadioButtonList ID="rbl_tipo_eia" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="rbl_tipo_eia_SelectedIndexChanged">
                                    <asp:ListItem Value="1" Text="Genéricos"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Específicos"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnl_eia" Visible="false">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_especificos" runat="server" Text="¿Desea solicitar los términos de Referencia para EIA?"
                                    SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rbl_eia_especifico" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Text="Sí"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="right">
                                <asp:Button ID="btn_continuar" runat="server" Text="Continuar" SkinID="boton" OnClick="btn_continuar_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
            
        </asp:UpdatePanel>--%> 
<%--        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnUbicacion" EventName="Click" />
        </Triggers>--%>
    </div>
--%></asp:Content>
