<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Anexar_Documentacion.aspx.cs"
    Inherits="LicenciasAmbientales_LPA_05_Anexar_Documentacion_Soporte_Solicitante"
    MasterPageFile="~/plantillas/SILPA.master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="div-titulo">
        <asp:Label ID="LBL_SOL_LIC" runat="server" Text="SOLICITUD DE DIAGNOSTICO AMBIENTAL DE ALTERNATIVAS"
            SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="div-contenido">
        <table border="1" cellpadding="1" id="Tabla1">
            <tr>
                <td colspan="4" style="height: 19px">
                    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
                    </asp:ScriptManager>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="LBL_SELECCIONE" runat="server" Text="Seleccione una opción para anexar cada documento:"
                        Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="1">
                </td>
                <td colspan="1">
                    <asp:Label ID="LBL_REG_INF_RAD" runat="server" Text="Registrar Información de Radicación:"
                        Font-Bold="True"></asp:Label>
                </td>
                <td colspan="1">
                    <asp:Label ID="LBL_ADJ_DOC" runat="server" Text="Adjuntar Documento:" Font-Bold="True"></asp:Label>
                </td>
                <td colspan="4">
                    <asp:Label ID="LBL_DILIG_DOC" runat="server" Text="Diligenciar Documento:" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td rowspan="3">
                    <asp:CheckBox ID="CHK_DOC" runat="server" Text="Documento 1:" Checked="True" />
                </td>
                <td rowspan="2">
                    <asp:Label ID="LBL_NUM_RAD" runat="server" Text="Número de Radicación:"></asp:Label>
                    <asp:TextBox ID="TXT_NUM_RAD" runat="server" Width="135px"></asp:TextBox>
                </td>
                <td rowspan="3">
                    <asp:FileUpload ID="FUP_ARCHIVO" runat="server" OnDataBinding="FUP_ARCHIVO_DataBinding"
                        OnInit="FUP_ARCHIVO_Init" OnLoad="FUP_ARCHIVO_Load" />
                </td>
                <td rowspan="3">
                    <asp:Label ID="Label8" runat="server" Text="*****FORMULARIO*****"></asp:Label>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LBL_FECH_RAD" runat="server" Text="Fecha de Radicación:"></asp:Label>
                    <asp:TextBox ID="TXT_FECHA_RAD" runat="server" Width="147px"></asp:TextBox>
                    <cc1:CalendarExtender ID="cdrFechaRadicacion" runat="server" TargetControlID="TXT_FECHA_RAD" Format="yyyy/MM/dd">
                    </cc1:CalendarExtender>
                    <br />
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" rowspan="1">
                </td>
            </tr>
            <tr>
                <td rowspan="2">
                    <asp:CheckBox ID="CheckBox2" runat="server" Text="Documento 2:" Checked="True" />
                </td>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Número de Radicación:"></asp:Label>
                    <asp:TextBox ID="TXT_NUM_RAD1" runat="server" Width="133px"></asp:TextBox>
                </td>
                <td rowspan="2">
                    <asp:FileUpload ID="FileUpload2" runat="server" OnInit="FileUpload2_Init" OnLoad="FileUpload2_Load" />
                </td>
                <td rowspan="2">
                    <asp:Label ID="Label11" runat="server" Text="*****FORMULARIO*****"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Fecha de Radicación:"></asp:Label>
                    <asp:TextBox ID="TXT_FECHA_RAD1" runat="server" Width="147px"></asp:TextBox>
                    <cc1:CalendarExtender ID="cdrFechaRadicacion2" runat="server" TargetControlID="TXT_FECHA_RAD1" Format="yyyy/MM/dd">
                    </cc1:CalendarExtender>
                    <br />
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td rowspan="1">
                    <asp:Button ID="BTN_GUARDAR" runat="server" Text="Guardar" OnClick="BTN_GUARDAR_Click" />
                </td>
                <td>
                </td>
                <td rowspan="1">
                </td>
                <td rowspan="1">
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
