<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="InscripcionAudienciaPublica.aspx.cs" Inherits="Informacion_Publicaciones"
    Title="Untitled Page" %>

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

        .divWaiting
        {
	        background-color:Gray;
            /*background-color: #FAFAFA;*/
	        filter:alpha(opacity=70);
	        /*opacity:0.7;*/
            position: absolute;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            text-align: center; top: 0; left: 0;
            height: 100%;
            width: 100%;
            padding-top:20%;
        }
    </style>
     
    <div class="div-titulo">
        <asp:Label id="lbl_titulo_principal" runat="server" SkinID="titulo_principal_blanco" Text="INSCRIPCION A AUDIENCIA PUBLICA"></asp:Label>
    </div>

    <asp:ScriptManager id="scmManejador" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True"></asp:ScriptManager>

    <asp:UpdatePanel id="UpdatePanel1" runat="server">
        <contenttemplate>
            <%--<div class="div-contenido">--%>
            <div class="table-responsive">
                <%--<table class="tabla_estruct">--%>
                <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                    <tr>
                        <td>
                            <asp:Label ID="lblNumSILPA" runat="server" Text="Número SILPA:" SkinID="etiqueta_negra"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtNumSILPA" runat="server" SkinID="texto" Width="100px" MaxLength="50"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblNombProyecto" runat="server" Text="Nombre del Proyecto:" SkinID="etiqueta_negra"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtNombre" runat="server" SkinID="texto" Width="300px" MaxLength="100"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAutoAmbiental" runat="server" Text="Autoridad Ambiental:" SkinID="etiqueta_negra"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="cboAutoAmbiental" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblNumExpediente" runat="server" Text="Número de Expediente:" SkinID="etiqueta_negra"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtNumeroExpediente" runat="server" SkinID="texto" Width="100px" MaxLength="50"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFechaReunionAudiencia" runat="server" Text="Fecha reunión audiencia pública:" SkinID="etiqueta_negra"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtFechaReunionAudiencia" runat="server" MaxLength="10"></asp:TextBox>
                            <cc1:CalendarExtender ID="calFechaReunionAudiencia" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaReunionAudiencia"></cc1:CalendarExtender>
                            <asp:RangeValidator ID="ravFechaAudiencia" runat="server" SetFocusOnError="True" 
                                MinimumValue="01/01/1900" MaximumValue="31/12/2099" EnableClientScript="False" Display="Dynamic" 
                                ControlToValidate="txtFechaReunionAudiencia" 
                                ErrorMessage="Solo fechas entre 01/01/1900 y 31/12/2099" 
                                ValidationGroup="validarFechaAudiencia">
                            </asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFechaReunionInfo" runat="server" Text="Fecha Reunión informativa:" SkinID="etiqueta_negra"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtFechaReunionInfo" runat="server" MaxLength="10"></asp:TextBox>
                            <cc1:CalendarExtender ID="calFechaReunionInfo" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaReunionInfo"></cc1:CalendarExtender>
                            <asp:RangeValidator ID="ravFechaReunionInfo" runat="server" SetFocusOnError="True" 
                                MinimumValue="01/01/1900" MaximumValue="31/12/2099" EnableClientScript="False" Display="Dynamic" 
                                ControlToValidate="txtFechaReunionInfo" 
                                ErrorMessage="Solo fechas entre 01/01/1900 y 31/12/2099" 
                                ValidationGroup="validarFechaReunion">
                            </asp:RangeValidator>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <cpt:Captcha runat="server" ID="ctrCaptcha" />
                            <asp:ValidationSummary ID="vasMensaje" runat="server" ShowMessageBox="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 20px; padding-bottom: 10px; padding-left: 10px; padding-right: 20px; text-align: right; vertical-align: middle;">
                            <asp:Button ID="cmdConsultar" OnClick="cmdConsultar_Click" runat="server" Text="Consultar" ></asp:Button>
                        </td>
                        <td style="padding-top: 20px; padding-bottom: 10px; padding-left: 20px; padding-right: 10px; text-align: left; vertical-align: middle;">
                            <asp:Button ID="Button1" runat="server" Text="Regresar"  OnClick="Button1_Click" CausesValidation="false"></asp:Button>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:GridView ID="grdPublicaciones" runat="server" Width="100%"  
                    DataKeyNames="NUMERO_SILPA,AUTORIDAD_AMBIENTAL" 
                    AllowSorting="True" PageSize="1" AutoGenerateColumns="False" 
                    CellPadding="4" CellSpacing="0" ForeColor="#333333" GridLines="None" 
                    OnPageIndexChanging="grdPublicaciones_PageIndexChanging" 
                    OnSelectedIndexChanged="grdPublicaciones_SelectedIndexChanged">
                    <RowStyle BackColor="#E3EAEB"></RowStyle>
                    <Columns>
                        <asp:CommandField SelectText="Detalles" ShowSelectButton="True"></asp:CommandField>
                        <asp:BoundField DataField="ID_AUDIENCIA_PUBLICA" HeaderText="C&#243;digo Audiencia P&#250;blica" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="NUMERO_SILPA" HeaderText="N&#250;mero SILPA" SortExpression="NUMERO_SILPA"></asp:BoundField>
                        <asp:BoundField DataField="NOMBRE_PROYECTO" HeaderText="Nombre del Proyecto" SortExpression="PROYECTO"></asp:BoundField>
                        <asp:BoundField DataField="NUMERO_EXPEDIENTE" HeaderText="N&#250;mero de expediente" SortExpression="NUMERO_EXPEDIENTE"></asp:BoundField>
                        <asp:BoundField DataField="AUTORIDAD_AMBIENTAL" HeaderText="Autoridad Ambiental" SortExpression="AUTORIDAD_AMBIENTAL"></asp:BoundField>
                        <asp:BoundField DataField="FECHA_HORA_REUNION_INFORMATIVA" HeaderText="Fecha reuni&#243;n informativa" SortExpression="fecha_reuni&#243;n_informativa"></asp:BoundField>
                        <asp:BoundField DataField="FECHA_HORA_CELEBRACION_AUDIENCIA" HeaderText="Fecha de reuni&#243;n audiencia p&#250;blica" SortExpression="fecha_reuni&#243;n_audiencia_publica"></asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></FooterStyle>
                    <PagerStyle HorizontalAlign="Center" BackColor="#666666" ForeColor="White"></PagerStyle>
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></HeaderStyle>
                    <EditRowStyle BackColor="#7C6F57"></EditRowStyle>
                    <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                </asp:GridView>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtNumSILPA" ValidChars="-" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" __designer:wfdid="w136"></cc1:FilteredTextBoxExtender>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtNombre" ValidChars="-.,:´Ññ " FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" __designer:wfdid="w137"></cc1:FilteredTextBoxExtender>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtNumeroExpediente" ValidChars="-" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" __designer:wfdid="w138"></cc1:FilteredTextBoxExtender>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtFechaReunionInfo" ValidChars="/" FilterType="Custom, Numbers" __designer:wfdid="w139"></cc1:FilteredTextBoxExtender>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtFechaReunionAudiencia" ValidChars="/" FilterType="Custom, Numbers" __designer:wfdid="w140"></cc1:FilteredTextBoxExtender>
            </div>
        </contenttemplate>
    </asp:UpdatePanel>
</asp:Content>
