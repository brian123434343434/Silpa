<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="Liquidar.aspx.cs" Inherits="PermisosAmbientales_Liquidacion_Liquidar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="SubHeaderBack">
        <div class="pageHeader">
            <div class="subBannerPhoto">
            </div>
            <p>
                &nbsp;</p>
            <%--TITLE--%>
            <p style="text-align:center;">
                &nbsp; <span class="specialsBtn">
                    <asp:Label ID="titulo_principal" runat="server" Text="Registro Manual de Documento de Cobro"
                        SkinID="titulo_principal_blanco"></asp:Label></span></p>
            <p>
                &nbsp;</p>
        </div>
    </div>
    <div class="copy">
        <%--BODY--%>
        <asp:ScriptManager ID="scriptManager" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="updt_Panel" runat="server">
            <ContentTemplate>
                <table style="width: 400px">
                    <tbody>
                        <tr>
                            <td style="width: 200px">
                                <asp:Label ID="lbl_registro" runat="server" SkinID="etiqueta_negra" Text="Número SILPA:"></asp:Label>
                            </td>
                            <td style="width: 200px; text-align: right">
                                <asp:TextBox ID="txt_numero" runat="server" SkinID="texto" Text="" 
                                    Width="128px" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 200px">
                                &nbsp;
                            </td>
                            <td style="width: 200px; text-align: right">
                                <asp:Button ID="btn_Consultar" OnClick="btn_Consultar_Click" runat="server" SkinID="boton" Text="Consultar">
                                </asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 200px">
                                <asp:Label ID="lbl_estado" runat="server" SkinID="etiqueta_roja_error" Text="" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 200px">
                            </td>
                        </tr>
                    </tbody>
                </table>
                <asp:Panel ID="pnl_solicitante" runat="server" Visible="false">
                    <table style="width: 400px; font-family: Tahoma;">
                        <tr>
                            <td style="width: 200px">
                                Solicitante:
                            </td>
                            <td style="text-align: left; width: 218px;">
                                <asp:Label ID="lbl_nombreSolicitante" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 200px">
                                Ciudad:
                            </td>
                            <td style="text-align: left; width: 218px;">
                                <asp:Label ID="lbl_ciudadSolicitante" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 200px">
                                Número de Radicado:
                            </td>
                            <td style="text-align: left; width: 218px;">
                                <asp:Label ID="lbl_numeroRadicado" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 200px">
                            </td>
                            <td style="text-align: right; width: 218px;">
                                <asp:Button ID="btn_liquidar" runat="server" Text="Registrar datos documento de cobro" Style="text-align: center"
                                    OnClick="btn_liquidar_Click"  SkinID="boton"/>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnl_liquidar" runat="server" Visible="false" Width="400px">
                    <table style="width: 400px; font-family: Tahoma; height: 105px">
                        <!-- MSTableType="layout" -->
                        <thead>
                            <tr>
                                <td style="width: 200px; height: 21px">
                                    <asp:Label ID="lbl_datosLiquidacion" runat="server" SkinID="titulo_principal" Text="Datos del Documento de Cobro"></asp:Label>
                                    &nbsp;
                                </td>
                                <td style="width: 200px; height: 21px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 200px; height: 21px">
                                    <asp:Label ID="lbl_datos_ubicacion" SkinID="etiqueta_negra" runat="server" Text="Ubicación Geográfica" Font-Names="Tahoma"></asp:Label>
                                </td>
                                <td style="width: 200px; height: 21px">
                                </td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td style="width: 200px">
                                    <asp:Label ID="lbl_departamento_etiqueta" runat="server" Text="DEPARTAMENTO:" Font-Names="Tahoma"
                                        Font-Size="X-Small" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td style="width: 200px; height: 22px">
                                    <asp:DropDownList ID="ddl_departamento" runat="server" AutoPostBack="True" DataValueField="DEP_ID"
                                        DataTextField="DEP_NOMBRE" DataSourceID="sds_Departamento" 
                                        SkinID="lista_desplegable" Width="205px" style="text-align: left">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sds_Departamento" runat="server" SelectCommand="SELECT * FROM [DEPARTAMENTO] WHERE ([DEP_ACTIVO] = @DEP_ACTIVO)"
                                        ConnectionString="<%$ ConnectionStrings:SILPAConnectionString %>">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="true" Name="DEP_ACTIVO" Type="Boolean" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 200px">
                                    <asp:Label SkinID="etiqueta_negra" ID="lbl_municipio_etiqueta" runat="server" Text="MUNICIPIO:" Font-Names="Tahoma"
                                        Font-Size="X-Small"></asp:Label>
                                </td>
                                <td style="width: 200px; height: 21px">
                                    <asp:DropDownList ID="ddl_municipio" runat="server" DataValueField="MUN_NOMBRE" DataTextField="MUN_NOMBRE"
                                        DataSourceID="sds_Municipio" SkinID="lista_desplegable" Width="205px">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sds_Municipio" runat="server" SelectCommand="SELECT * FROM [MUNICIPIO] WHERE (([MUN_ACTIVO] = @MUN_ACTIVO) AND ([DEP_ID] = @DEP_ID)) ORDER BY [DEP_ID], [MUN_NOMBRE]"
                                        ConnectionString="<%$ ConnectionStrings:SILPAConnectionString %>">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="true" Name="MUN_ACTIVO" Type="Boolean" />
                                            <asp:ControlParameter ControlID="ddl_departamento" Name="DEP_ID" PropertyName="SelectedValue"
                                                Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 200px">
                                    <asp:Label ID="Label1" runat="server" Font-Names="Tahoma" Font-Size="X-Small" SkinID="etiqueta_negra"
                                        Text="CONCEPTO:"></asp:Label></td>
                                <td style="width: 200px; height: 22px">
                                    <asp:DropDownList ID="ddl_Concepto" runat="server" DataValueField="CON_NOMBRE" DataTextField="CON_NOMBRE"
                                        DataSourceID="sds_Concepto" SkinID="lista_desplegable" Width="207px" OnSelectedIndexChanged="ddl_Concepto_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sds_Concepto" runat="server" SelectCommand="SELECT * FROM CONCEPTO"
                                        ConnectionString="<%$ ConnectionStrings:SILAMCConnectionString %>" OnSelecting="sds_Concepto_Selecting"></asp:SqlDataSource>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 200px">
                                    <asp:Label SkinID="etiqueta_negra" ID="lbl_concepto_etiqueta" runat="server" Text="DESCRIPCIÓN:" Font-Names="Tahoma"
                                        Font-Size="X-Small"></asp:Label>
                                </td>
                                <td style="width: 200px; height: 22px">
                                    <asp:TextBox ID="txt_concepto" runat="server" TextMode="MultiLine" MaxLength="500" Width="200px" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: x-small; width: 200px; font-family: Tahoma">
                                    VALOR EN NÚMEROS:
                                </td>
                                <td style="width: 200px">
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" TargetControlID="txt_valorNumeros"
                                        runat="server" MaskType="Number" InputDirection="RightToLeft" DisplayMoney="Left" Mask="999,999,999.99">
                                    </cc1:MaskedEditExtender>
                                    <asp:TextBox SkinID="texto" ID="txt_valorNumeros" runat="server" Width="200px" 
                                        AutoPostBack="True" ontextchanged="txt_valorNumeros_TextChanged" 
                                        ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: x-small; width: 200px; font-family: Tahoma">
                                    VALOR EN LETRAS:
                                </td>
                                <td style="width: 200px">
                                    <asp:TextBox ID="txt_valorLetras" runat="server" Width="200px" TextMode="MultiLine"
                                        MaxLength="100" SkinID="texto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 200px">
                                </td>
                                <td style="width: 200px; text-align: right">
                                    <asp:Button ID="btn_generar" SkinID="boton" OnClick="btn_generar_Click" runat="server" Text="Guardar Liquidación">
                                    </asp:Button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        &nbsp;
    </div>
</asp:Content>
