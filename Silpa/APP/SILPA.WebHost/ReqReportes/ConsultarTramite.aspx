<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ConsultarTramite.aspx.cs" Inherits="Salvoconducto_ConsultarSalvoconductoWeb" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="div-titulo">
    
    <asp:Label ID="lbl_titulo" SkinID="texto" runat="server" Text="Consulta de Trámites"></asp:Label>
    </div>
    <div class="div-contenido">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnl_datos_consulta" runat="server" Width="500px">
                    <table style="width:500px;">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lbl_subtitulo" runat="server" Text="Trámites" SkinID="titulo_principal"></asp:Label>
                        </td>
                        <td colspan="1" style="width: 88px">
                        </td>
                        <td colspan="1">
                        </td>
                    </tr>
                        <tr>
                            <td style="width: 6%; text-align: left">
                                Fecha Inicial:</td>
                            <td style="text-align:left">
                                &nbsp;<asp:TextBox ID="TxtFechaInicial" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtFechaInicial">
                                </cc1:CalendarExtender>
                                <asp:RangeValidator ID="RanFechaInicial" runat="server" ControlToValidate="TxtFechaInicial"
                                    ErrorMessage="La fecha Inicial no es valida" MaximumValue="31/12/2099" MinimumValue="01/01/1901"
                                    Type="Date"></asp:RangeValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 6%; text-align: left">
                                Fecha Final:</td>
                            <td style="text-align: left">
                                &nbsp;<asp:TextBox ID="TxtFechaFinal" runat="server"></asp:TextBox><cc1:CalendarExtender
                                    ID="CalendarExtender2" runat="server" FirstDayOfWeek="Wednesday" Format="dd/MM/yyyy"
                                    TargetControlID="TxtFechaFinal">
                                </cc1:CalendarExtender>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="TxtFechaFinal"
                                    ErrorMessage="La fecha Final no es valida" MaximumValue="31/12/2099" MinimumValue="01/01/1901"
                                    Type="Date"></asp:RangeValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 30%; height: 21px; text-align: left">
                                Tipo de Tramite:</td>
                            <td style="width: 80%; height: 21px; text-align: left">
                                <asp:DropDownList ID="CboTipoTramite" runat="server" Width="298px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 30%; height: 21px; text-align: left">
                                Autoridad Ambiental:</td>
                            <td style="width: 80%; height: 21px; text-align: left">
                                <asp:DropDownList ID="CboAutoridadAmbiental" runat="server" Width="297px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 30%; height: 21px; text-align: left">
                                Sector:</td>
                            <td style="width: 80%; height: 21px; text-align: left">
                                <asp:DropDownList ID="CboSector" runat="server" Width="298px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 30%; height: 21px; text-align: left">
                                Tipo de Proyecto:</td>
                            <td style="width: 80%; height: 21px; text-align: left">
                                <asp:DropDownList ID="CboTipoProyecto" runat="server" Width="299px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 30%; height: 21px; text-align: left">
                                Nombre:</td>
                            <td style="width: 80%; height: 21px; text-align: left">
                                <asp:TextBox ID="TxtNombre" runat="server" Width="293px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 30%; height: 21px; text-align: left">
                                Expediente:</td>
                            <td style="width: 80%; height: 21px; text-align: left">
                                <asp:TextBox ID="TxtExpediente" runat="server" Width="293px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 30%; height: 21px; text-align: left">
                            </td>
                            <td style="width: 80%; height: 21px; text-align: left">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 30%; height: 21px; text-align: left">
                                Etapa:</td>
                            <td style="width: 80%; height: 21px; text-align: left">
                                <asp:DropDownList ID="CboEtapa" runat="server" Width="299px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 30%; height: 21px; text-align: left">
                                Actividad:</td>
                            <td style="width: 80%; height: 21px; text-align: left">
                                <asp:DropDownList ID="CboActividad" runat="server" Width="299px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 30%; height: 21px; text-align: left">
                                Estado:</td>
                            <td style="width: 80%; height: 21px; text-align: left">
                                <asp:DropDownList ID="CboEstado" runat="server" Width="299px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 30%; height: 21px; text-align: left">
                                Departamento:</td>
                            <td style="width: 80%; height: 21px; text-align: left">
                                &nbsp;<asp:DropDownList ID="CboDepartamento" runat="server" Width="295px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 30%; height: 21px; text-align: left">
                                Municipio:</td>
                            <td style="width: 80%; height: 21px; text-align: left">
                                <asp:DropDownList ID="CboMunicipio" runat="server" Width="295px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 30%; height: 21px; text-align: left">
                                Vereda</td>
                            <td style="width: 80%; height: 21px; text-align: left">
                                <asp:DropDownList ID="CboVereda" runat="server" Width="295px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 30%; height: 21px; text-align: left">
                                Corregimiento:</td>
                            <td style="width: 80%; height: 21px; text-align: left">
                                <asp:DropDownList ID="CboCorregimiento" runat="server" Width="295px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:right">
                                <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" ServiceMethod="GetDropDownContents" Category = "Departamento"
                                    TargetControlID="CboDepartamento" UseContextKey="True">
                                </cc1:CascadingDropDown>
                                <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" ServiceMethod="GetDropDownContents2" Category = "Ciudad" ParentControlID = "CboDepartamento" 
                                    TargetControlID="CboMunicipio" UseContextKey="True">
                                </cc1:CascadingDropDown>
                                <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" ServiceMethod="GetDropDownContents3" Category = "Vereda" ParentControlID = "CboMunicipio" 
                                    UseContextKey="True" TargetControlID="CboVereda">
                                </cc1:CascadingDropDown>
                                <cc1:CascadingDropDown ID="CascadingDropDown4" runat="server" ServiceMethod="GetDropDownContents4" Category = "Corregimiento" ParentControlID = "CboMunicipio" 
                                    TargetControlID="CboCorregimiento" UseContextKey="True">
                                </cc1:CascadingDropDown>
                                &nbsp;
                                <asp:Button ID="btn_consultar" runat="server" Text="Consular" SkinID="boton"  
                                    onclick="btn_consultar_Click" />
                               </td>
                            <td colspan="1" style="width: 88px; text-align: right">
                            </td>
                            <td colspan="1" style="text-align: right">
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center" colspan="2">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                    <ProgressTemplate>
                                        <asp:Image ID="img_progress" runat="server" 
                                            ImageUrl="~/App_Themes/Img/ajax-loader.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                            <td colspan="1" style="width: 88px; text-align: center">
                            </td>
                            <td colspan="1" style="text-align: center">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center; height: 132px;">
                    <asp:GridView ID="dgv_resultado" runat="server" style="text-align: center" Width="400px" OnRowCommand="dgv_resultado_RowCommand" OnSelectedIndexChanged="dgv_resultado_SelectedIndexChanged" >
                        <Columns>
                            <asp:CommandField ButtonType="Button" SelectText="Ir" ShowCancelButton="False" ShowSelectButton="True" />
                        </Columns>
                    </asp:GridView>
                            </td>
                            <td colspan="1" style="width: 88px; text-align: center; height: 132px;">
                            </td>
                            <td colspan="1" style="text-align: center; height: 132px;">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                    <asp:Label ID="lbl_resultado_error" runat="server" SkinID="etiqueta_roja_error" 
                        style="text-align: center" 
                        Text="No se encontró ningún resultado para el Trámite ingresado." 
                        Visible="False"></asp:Label></td>
                            <td colspan="1" style="width: 88px; text-align: center">
                            </td>
                            <td colspan="1" style="text-align: center">
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                &nbsp;
            </ContentTemplate>
        
        </asp:UpdatePanel>
    </div>
</asp:Content>

