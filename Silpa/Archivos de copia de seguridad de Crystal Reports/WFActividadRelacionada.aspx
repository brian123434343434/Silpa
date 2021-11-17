<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="WFActividadRelacionada.aspx.cs" Inherits="Administracion_Tablasbasicas_WFActividadRelacionada" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="ACTIVIDAD POR FORMULARIO A LA MEDIDA" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="div-contenido" style="height: 400px">
        <asp:UpdatePanel ID="updConsultar" runat="server" UpdateMode="Conditional">
            <contenttemplate>
<TABLE><TBODY><tr><td colspan="3"><asp:Button id="btnVolver" runat="server" SkinID="boton_copia" Text="Cancelar" PostBackUrl="~/Administracion/Tablasbasicas/TablasBasicas.aspx"></asp:Button></td></tr><TR><TD colSpan=3><asp:Label runat="server" Text="Actividad Silpa:" Id="lblActividadSilpa"></asp:Label>&nbsp;&nbsp;<asp:DropDownList id="cboActividadSilpa" runat="server" OnSelectedIndexChanged="cboActividadSilpa_SelectedIndexChanged" AutoPostBack="True" __designer:wfdid="w39"></asp:DropDownList></TD></TR><tr><td><asp:Label runat="server" Text="Actividades Relacionadas" Id="lblRelacionados"></asp:Label></td><td></td><td><asp:Label runat="server" Text="Actividades No Relacionadas" Id="lblNoRelacionadas"></asp:Label></td></tr><TR><TD style="WIDTH: 264px; HEIGHT: 163px"><asp:ListBox id="lstRelacionadas" runat="server" OnSelectedIndexChanged="lstRelacionadas_SelectedIndexChanged" AutoPostBack="True" Rows="10" Width="400px" __designer:wfdid="w40"></asp:ListBox></TD><TD><asp:ImageButton id="btnQuitaActividad" onclick="btnQuitaActividad_Click" runat="server" Width="39px" Height="36px" ImageUrl="~/App_Themes/Img/iconos/Arrow-Left.png" __designer:wfdid="w41"></asp:ImageButton><BR /><BR /><asp:ImageButton id="btnAddActividad" onclick="btnAddActividad_Click" runat="server" Width="39px" Height="36px" ImageUrl="~/App_Themes/Img/iconos/Arrow-Right.png" __designer:wfdid="w42"></asp:ImageButton></TD><TD style="WIDTH: 216px; HEIGHT: 163px"><asp:ListBox id="lstNoRelacionadas" runat="server" OnSelectedIndexChanged="lstNoRelacionadas_SelectedIndexChanged" AutoPostBack="True" Rows="10" Width="400px" __designer:wfdid="w43"></asp:ListBox></TD></TR></TBODY></TABLE>
</contenttemplate>
            <triggers>
<asp:AsyncPostBackTrigger ControlID="cboActividadSilpa" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
    
        </asp:UpdatePanel>
    </div>
</asp:Content>
