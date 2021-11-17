<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ConsultarInscritosAudienciaPublica.aspx.cs" Inherits="Informacion_Publicaciones" Title="Consultar Inscritos a Audiencia Pública" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/controles/cltCaptcha.ascx" TagPrefix="cpt" TagName="Captcha" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        table {
            /*border: 1px solid #000;*/
        }
        table tr td {
            border: 0px solid #ddd !important;
            padding: 4px;
        }
        .Button{
            background-color: #ddd;
        }
    </style>

    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" SkinID="titulo_principal_blanco"
            Text="INSCRITOS AUDIENCIA PÚBLICA"></asp:Label>
    </div>

    <%--<div class="div-contenido">--%>
    <div class="table-responsive">

        <asp:ScriptManager ID="scmManejador" runat="server" EnableScriptGlobalization="true">
        </asp:ScriptManager>

        <%--<asp:UpdatePanel ID="uppPanelPublicaciones" runat="server">
            <ContentTemplate>--%>

        <%--<table class="tabla_estruct">--%>
        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
            <tr>
                <td>
                    <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                        <tr>
                            <td>
                                <asp:Label ID="lblNumeroSilpa" runat="server" Text="Número VITAL:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumeroSilpa" runat="server" SkinID="texto"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAutoridadAmbiental" runat="server" Text="Autoridad Ambiental:" SkinID="etiqueta_negra"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboAutoridadAmbiental" runat="server" SkinID="lista_desplegable">
                                    <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <%-- <tr>
                            <td>
                                <asp:Label ID="lblSectorPublicacion" runat="server" SkinID="etiqueta_negra" Text="Sector:"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="cboSectorPublicacion" runat="server" SkinID="lista_desplegable">
                                    <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>--%>
                        <tr>
                            <td>
                                <asp:Label ID="lblNombreProyecto" runat="server" Text="Nombre del Proyecto, Obra o Actividad :" SkinID="etiqueta_negra"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtNombreProyecto" runat="server" SkinID="texto"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNumeroExpediente" runat="server" Text="Número de Expediente:" SkinID="etiqueta_negra"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtNumeroExpediente" runat="server" SkinID="texto"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblFechaInformativa" runat="server" Text="Fecha de Reunión Informativa (aaaa/dd/mm):" SkinID="etiqueta_negra"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtFechaInformativa" runat="server" CssClass="fecha-calendar" MaxLength="10"></asp:TextBox>
                                <cc1:CalendarExtender ID="calFechaInformativa" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaInformativa"></cc1:CalendarExtender>
                                <asp:RangeValidator ID="ravFechaInformativa" runat="server" ControlToValidate="txtFechaInformativa"
                                    ErrorMessage="Fecha de Reunión Informativa Incorrecta" MaximumValue="31/12/9999"
                                    MinimumValue="01/01/1900" Type="Date" ValidationGroup="validarFechaInformativa">*</asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblFechaAudiencia" runat="server" Text="Fecha de Reunión Audiencia Pública (aaaa/dd/mm):" SkinID="etiqueta_negra"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtFechaAudiencia" runat="server" CssClass="fecha-calendar" MaxLength="10"></asp:TextBox>
                                <cc1:CalendarExtender ID="calFechaAudiencia" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaAudiencia"></cc1:CalendarExtender>
                                <asp:RangeValidator ID="ravFechaAudiencia" runat="server" ControlToValidate="txtFechaAudiencia" 
                                    ErrorMessage="Fecha de Reunion de Audiencia Pública Incorrecta"
                                    MaximumValue="31/12/9999" MinimumValue="01/01/1900" Type="Date" ValidationGroup="validarFechaAudiencia">*</asp:RangeValidator>
                                       
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <cpt:Captcha runat="server" ID="ctrCaptcha" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="vasMensaje" runat="server" DisplayMode="List" ShowMessageBox="True" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 20px; padding-bottom: 10px; padding-left: 10px; padding-right: 20px; text-align: right; vertical-align: middle;">
                                <asp:Button ID="btnConsultar" OnClick="btnConsultar_Click" runat="server" Text="Consultar" SkinID="boton_copia"></asp:Button>
                            </td>
                            <td style="padding-top: 20px; padding-bottom: 10px; padding-left: 20px; padding-right: 10px; text-align: left; vertical-align: middle;">
                                <asp:Button ID="Button1" runat="server" Text="Regresar" SkinID="boton_copia" OnClick="Button1_Click" CausesValidation="false"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <asp:Panel ID="pnlPublicaciones" runat="server" ScrollBars="Auto">
            <asp:GridView ID="grdInscritos" runat="server" 
                SkinID="Grilla_simple_peq" 
                AllowPaging="True" Width="100%" 
                RowStyle-HorizontalAlign="Left"
                CellSpacing="1" CellPadding="2" 
                AutoGenerateColumns="False" 
                OnPageIndexChanging="grdInscritos_PageIndexChanging">
                <RowStyle HorizontalAlign="Left"></RowStyle>
                <Columns>
                    <asp:BoundField DataField="AUD_NUMERO_SILPA" HeaderText="N&#250;mero VITAL"></asp:BoundField>
                    <asp:BoundField DataField="AUT_NOMBRE" HeaderText="Autoridad Ambiental"></asp:BoundField>
                    <asp:BoundField DataField="AUD_NOMBRE_PROYECTO" HeaderText="Nombre del Proyecto"></asp:BoundField>
                    <asp:BoundField DataField="AUD_NUMERO_EXPEDIENTE" HeaderText="N&#250;mero Expediente"></asp:BoundField>
                    <asp:BoundField DataField="AUD_FECHA_HORA_REUNION_INFORMATIVA" HeaderText="Fecha Reuni&#243;n Informativa" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
                    <asp:BoundField DataField="AUD_LUGAR_CELEBRACION_AUDIENCIA_PUBLICA" HeaderText="Fecha Reuni&#243;n Audiencia P&#250;blica" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
                    <asp:BoundField DataField="NOMBRE_COMPLETO" HeaderText="Nombre Inscrito" />
                    <asp:BoundField DataField="IAP_NUMERO_IDENTIFICACION" HeaderText="C&#233;dula Inscrito" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
<%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
