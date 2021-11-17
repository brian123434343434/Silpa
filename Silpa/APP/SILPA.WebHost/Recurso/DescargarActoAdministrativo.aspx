<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPASinMenu.master" AutoEventWireup="true"
    CodeFile="DescargarActoAdministrativo.aspx.cs" Inherits="Presentar_Recurso" Title="Presentar Recurso" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" SkinID="titulo_principal_blanco"
            Text="RECURSO DE REPOSICIÓN"></asp:Label>
    </div>
    <div class="div-contenido">
        <asp:ScriptManager ID="scmManejador" runat="server" EnableScriptGlobalization="true">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="uppPanelPublicaciones" runat="server">
            <ContentTemplate>
                <table style="width: 70%">
                    <tbody>
                        <tr>
                            <td>
                                <table width="800">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblNumeroSILPA" runat="server" Text="Número SILPA:" SkinID="etiqueta_negra"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cboNumeroSILPA" runat="server" SkinID="lista_desplegable">
                                                    <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblNumeroExpediente" runat="server" Text="Número de Expediente:" SkinID="etiqueta_negra"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cboNumeroExpediente" runat="server" SkinID="lista_desplegable">
                                                    <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblActoAdministrativo" runat="server" Text="Número de Acto Administrativo:"
                                                    SkinID="etiqueta_negra"></asp:Label></td>
                                            <td>
                                                <asp:DropDownList ID="cboActoAdministrativo" runat="server" SkinID="lista_desplegable"
                                                    OnSelectedIndexChanged="cboTipoDocumento_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha Desde (aaaa/mm/dd):" SkinID="etiqueta_negra"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtFechaInicial" runat="server" SkinID="texto_corto" MaxLength="10"></asp:TextBox>&nbsp;
                                                <cc1:CalendarExtender ID="calFechaInicial" runat="server" Format="yyyy/MM/dd" TargetControlID="txtFechaInicial">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha Hasta (aaaa/mm/dd):" SkinID="etiqueta_negra"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtFechaFinal" runat="server" SkinID="texto_corto" MaxLength="10"></asp:TextBox>
                                                <asp:CompareValidator ID="covCompararFechas" runat="server" Type="Date" Operator="LessThan"
                                                    ErrorMessage='El valor del campo "Fecha Hasta", debe ser posterior al valor del campo "Fecha Desde".'
                                                    Display="Dynamic" ControlToValidate="txtFechaInicial" ControlToCompare="txtFechaFinal">*</asp:CompareValidator>&nbsp;
                                                <br /> 
                                                <cc1:CalendarExtender ID="calFechaFinal" runat="server" Format="yyyy/MM/dd" TargetControlID="txtFechaFinal">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:ValidationSummary ID="valResumen" runat="server"></asp:ValidationSummary>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btnConsultar" OnClick="btnConsultar_Click" runat="server" Text="Consultar"
                                                    SkinID="boton_copia"></asp:Button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-right: #000000 1px solid; border-top: #000000 1px solid; border-left: #000000 1px solid;
                                border-bottom: #000000 1px solid">
                                <asp:Panel ID="pnlNotificacion" runat="server" Visible="False">
                                    &nbsp;<asp:GridView ID="grdActos" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CellPadding="2" CellSpacing="1" DataKeyNames="IdActoNotificacion" 
                                        RowStyle-HorizontalAlign="Left" SkinID="Grilla_simple_peq" OnSelectedIndexChanged="grdActos_SelectedIndexChanged">
                                        <RowStyle HorizontalAlign="Left" />
                                        <Columns>
                                         <asp:BoundField HeaderText="NumeroSilpa" DataField="NumeroSilpa" />
                                         <asp:ButtonField  HeaderText="RutaDocumento" Text="RutaDocumento" CommandName="select"/>
                 
                                            <asp:BoundField DataField="NumeroSILPA" HeaderText="N&#250;mero SILPA" />
                                            <asp:BoundField DataField="ProcesoAdministracion" HeaderText="N&#250;mero de Expediente" />
                                            <asp:BoundField ApplyFormatInEditMode="True" DataField="NumeroActoAdministrativo" HeaderText="N&#250;mero de Acto Administrativo" />
                                            <asp:BoundField DataField="FechaActo" DataFormatString="{0:yyyy/MM/dd}" HeaderText="Fecha de Acto Administrativo" />
                                            <asp:BoundField DataField="FechaNotificacion" DataFormatString="{0:yyyy/MM/dd}" HeaderText="Fecha de Notificaci&#243;n" />
                                            
                          

                                           
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                                </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
