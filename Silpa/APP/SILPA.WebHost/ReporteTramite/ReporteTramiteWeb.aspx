<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ReporteTramiteWeb.aspx.cs" Inherits="ReporteTramiteWeb" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
     <div class="div-titulo">
    <asp:Label ID="Label1" runat="server" SkinID="titulo_principal_blanco" Text="Consulta de Trámite"></asp:Label>
    </div>
    <asp:ScriptManager ID="scmManejador" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True">    
    </asp:ScriptManager>
        <asp:UpdatePanel ID="uppPanel" runat="server">
            <ContentTemplate>
<DIV style="TEXT-ALIGN: left" class="stiles"><TABLE style="WIDTH: 700px; TEXT-ALIGN: left"><TBODY><TR><TD style="WIDTH: 710px" colSpan=4><asp:Label id="lblNombreProyecto" runat="server" SkinID="etiqueta_negra" Text="Nombre Proyecto, Obra o Actividad:" Width="200px" font-bold="True"></asp:Label><asp:TextBox id="txtNombreProyecto" runat="server" Width="380px" BorderStyle="Solid" MaxLength="100"></asp:TextBox></TD></TR><TR><TD style="WIDTH: 710px" colSpan=4><asp:Label id="lblNumeroExpediente" runat="server" SkinID="etiqueta_negra" Text="Número de Expediente: " Width="200px" font-bold="True"></asp:Label><asp:TextBox id="txtNumeroExpediente" runat="server" SkinID="texto_corto" Width="400px" BorderStyle="Solid" MaxLength="50"></asp:TextBox></TD></TR><TR><TD style="WIDTH: 710px" colSpan=4><asp:Label id="lblTipoTramite" runat="server" SkinID="etiqueta_negra" Text="Tipo de Trámite:" Width="200px" font-bold="True"></asp:Label><asp:DropDownList id="cboTipoTramite" runat="server" SkinID="lista_desplegable" Width="226px" AutoPostBack="True"></asp:DropDownList></TD></TR><TR><TD style="WIDTH: 710px" colSpan=4><asp:Label id="lblAutoridadAmbiental" runat="server" SkinID="etiqueta_negra" Text="Autoridad Ambiental:" Width="200px" font-bold="True"></asp:Label><asp:DropDownList id="cboAutoridadAmbiental" runat="server" AutoPostBack="True"></asp:DropDownList></TD></TR><TR><TD style="WIDTH: 710px" colSpan=4><asp:Label id="lblDepartamento" runat="server" SkinID="etiqueta_negra" Text="Departamento" Width="200px" font-bold="True"></asp:Label><asp:DropDownList id="cboDepartamento" runat="server" SkinID="lista_desplegable" AutoPostBack="True" OnSelectedIndexChanged="cboDepartamento_SelectedIndexChanged">
                                <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                            </asp:DropDownList></TD></TR><TR><TD style="WIDTH: 710px" colSpan=4><asp:Label id="lblMunicipio" runat="server" SkinID="etiqueta_negra" Text="Municipio" Width="200px" font-bold="True"></asp:Label><asp:DropDownList id="cboMunicipio" runat="server" SkinID="lista_desplegable" AutoPostBack="True">
                                <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                            </asp:DropDownList></TD></TR><TR><TD style="WIDTH: 710px" colSpan=4><asp:Label id="lblFechaFinal" runat="server" SkinID="etiqueta_negra" Text="Fecha Desde (aaaa/dd/mm):" Width="200px" font-bold="True"></asp:Label><asp:TextBox id="txtFechaInicial" runat="server" SkinID="texto_corto" MaxLength="10"></asp:TextBox> <asp:RangeValidator id="ravFechaInicial" runat="server" MinimumValue="01/01/1900" MaximumValue="31/12/2099" ErrorMessage="Ingrese fechas entre 01/01/1900 y 31/12/2099" Display="Dynamic" ControlToValidate="txtFechaInicial" EnableClientScript="False" ValidationGroup="validarFechas"></asp:RangeValidator></TD></TR><TR><TD style="WIDTH: 710px" colSpan=4><asp:Label id="lblFechaInicial" runat="server" SkinID="etiqueta_negra" Text="Fecha Hasta (aaaa/dd/mm):" Width="200px" font-bold="True"></asp:Label><asp:TextBox id="txtFechaFinal" runat="server" SkinID="texto_corto" MaxLength="10"></asp:TextBox> <asp:RangeValidator id="ravFechaFinal" runat="server" MinimumValue="01/01/1900" MaximumValue="31/12/2099" ErrorMessage="Ingrese fechas entre 01/01/1900 y 31/12/2099" Display="Dynamic" ControlToValidate="txtFechaFinal" EnableClientScript="False" ValidationGroup="validarFechas"></asp:RangeValidator></TD></TR><TR><TD style="WIDTH: 710px" colSpan=4><asp:CompareValidator id="covCompararFechas" runat="server" Width="543px" ErrorMessage='El valor del campo "Fecha Hasta", debe ser posterior al valor del campo "Fecha Desde".' Display="Dynamic" ControlToValidate="txtFechaInicial" EnableClientScript="False" ValidationGroup="validarFechas" ControlToCompare="txtFechaFinal" Operator="LessThanEqual" Type="Date" Height="13px"></asp:CompareValidator><asp:Label style="TEXT-ALIGN: center" id="lblErrorFechas" runat="server" SkinID="etiqueta_roja_error" __designer:wfdid="w1"></asp:Label></TD></TR><TR><TD style="WIDTH: 710px" colSpan=4><asp:Button id="btnConsultar" onclick="btnConsultar_Click" runat="server" SkinID="boton_copia" Text="Consultar" font-bold="True"></asp:Button><asp:Label style="TEXT-ALIGN: center" id="lblMensajeError" runat="server" SkinID="etiqueta_roja_error"></asp:Label></TD></TR><TR><TD style="WIDTH: 710px" colSpan=4><asp:GridView style="TEXT-ALIGN: center" id="grdReporte" runat="server" SkinID="Grilla_simple" Width="85%" BorderStyle="Solid" OnSelectedIndexChanged="grdReporte_SelectedIndexChanged" DataKeyNames="ID,AUT_ID,SOL_ID_SOLICITANTE,SOL_IDPROCESSINSTANCE,SOL_NUMERO_SILPA" OnPageIndexChanging="grdReporte_PageIndexChanging" AutoGenerateColumns="False" BorderColor="#999999" CellPadding="3" GridLines="Vertical" BorderWidth="1px" forecolor="Black" backcolor="White"><Columns>
<asp:CommandField SelectText=" Detalles" ShowSelectButton="True" HeaderText="Ver Detalles"></asp:CommandField>
<asp:BoundField DataField="SOL_FECHA_CREACION" HeaderText="Fecha del Tramite"></asp:BoundField>
<asp:BoundField DataField="ID" HeaderText="C&#243;digo tipo tramite" Visible="False"></asp:BoundField>
<asp:BoundField DataField="NOMBRE_TIPO_TRAMITE" HeaderText="Tipo de Tramite"></asp:BoundField>
<asp:BoundField DataField="AUT_ID" HeaderText="C&#243;digo Autoridad Ambiental" Visible="False"></asp:BoundField>
<asp:BoundField DataField="AUT_NOMBRE" HeaderText="Autoridad Ambiental"></asp:BoundField>
<asp:BoundField DataField="SOL_ID_SOLICITANTE" HeaderText="C&#243;digo del solicitante" Visible="False"></asp:BoundField>
<asp:BoundField DataField="SOL_IDPROCESSINSTANCE" HeaderText="C&#243;digo formulario de proceso" Visible="False"></asp:BoundField>
<asp:BoundField DataField="SOL_NUMERO_SILPA" HeaderText="C&#243;digo SILPA" Visible="False"></asp:BoundField>
<asp:BoundField DataField="NOMBRE_PROYECTO" HeaderText="Nombre del Proyecto, Obra o Actividad"></asp:BoundField>
</Columns>

<FooterStyle BackColor="#CCCCCC"></FooterStyle>

<PagerStyle HorizontalAlign="Center" BackColor="#999999" ForeColor="Black"></PagerStyle>

<SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White"></SelectedRowStyle>

<HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White"></HeaderStyle>

<AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
</asp:GridView></TD></TR></TBODY></TABLE></DIV><cc1:CalendarExtender id="calFechaFinal" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaFinal">
</cc1:CalendarExtender> <cc1:CalendarExtender id="calFechaInicial" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaInicial"></cc1:CalendarExtender> <cc1:FilteredTextBoxExtender id="FilteredTextBoxExtender1" runat="server" __designer:wfdid="w66" TargetControlID="txtFechaInicial" ValidChars="/" FilterType="Custom, Numbers"></cc1:FilteredTextBoxExtender> <cc1:FilteredTextBoxExtender id="FilteredTextBoxExtender2" runat="server" __designer:wfdid="w67" TargetControlID="txtFechaFinal" ValidChars="/" FilterType="Custom, Numbers"></cc1:FilteredTextBoxExtender> <cc1:FilteredTextBoxExtender id="FilteredTextBoxExtender3" runat="server" __designer:wfdid="w335" TargetControlID="txtNombreProyecto" ValidChars=',:.-*+/"( )[]´ñÑ#' FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"></cc1:FilteredTextBoxExtender> <cc1:FilteredTextBoxExtender id="FilteredTextBoxExtender4" runat="server" __designer:wfdid="w336" TargetControlID="txtNumeroExpediente" ValidChars="-/#" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"></cc1:FilteredTextBoxExtender> 
</ContentTemplate>
        </asp:UpdatePanel>             
     
</asp:Content>

