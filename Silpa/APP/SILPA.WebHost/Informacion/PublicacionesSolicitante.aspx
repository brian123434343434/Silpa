<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPALimpia.master" AutoEventWireup="true" CodeFile="PublicacionesSolicitante.aspx.cs" Inherits="Informacion_PublicacionesSolicitante" Title="Publicaciones" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" SkinID="titulo_principal_blanco"
            Text="PUBLICACIONES"></asp:Label>
    </div>
    <div class="div-contenido">
        <asp:ScriptManager ID="scmManejador" runat="server" EnableScriptGlobalization="true">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="uppPanelPublicaciones" runat="server">
            <ContentTemplate>
<TABLE style="WIDTH: 70%"><TBODY><TR><TD><TABLE width=800><TBODY><TR><TD><asp:Label id="lblTipoTramite" runat="server" Text="Tipo de Trámite:" SkinID="etiqueta_negra"></asp:Label> </TD><TD><asp:DropDownList id="cboTipoTramite" runat="server" SkinID="lista_desplegable">
                                            <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                        </asp:DropDownList> </TD></TR><TR><TD><asp:Label id="lblAutoridadAmbiental" runat="server" Text="Autoridad Ambiental:" SkinID="etiqueta_negra"></asp:Label> </TD><TD><asp:DropDownList id="cboAutoridadAmbiental" runat="server" SkinID="lista_desplegable">
                                            <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                        </asp:DropDownList> </TD></TR><TR><TD><asp:Label id="lblSectorPublicacion" runat="server" Text="Sector:" SkinID="etiqueta_negra"></asp:Label></TD><TD><asp:DropDownList id="cboSectorPublicacion" runat="server" SkinID="lista_desplegable">
                                            <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                        </asp:DropDownList></TD></TR><TR><TD><asp:Label id="lblNombreProyecto" runat="server" Text="Nombre del Proyecto, Obra o Actividad:" SkinID="etiqueta_negra"></asp:Label></TD><TD><asp:TextBox id="txtNombreProyecto" runat="server" SkinID="texto"></asp:TextBox></TD></TR><TR><TD><asp:Label id="lblTipoDocumento" runat="server" Text="Tipo de Documento:" SkinID="etiqueta_negra"></asp:Label></TD><TD><asp:DropDownList id="cboTipoDocumento" runat="server" SkinID="lista_desplegable" AutoPostBack="True" OnSelectedIndexChanged="cboTipoDocumento_SelectedIndexChanged">
                                            <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                        </asp:DropDownList></TD></TR><TR><TD><asp:Label id="lblNumeroDocumento" runat="server" Text="Número de Documento:" SkinID="etiqueta_negra"></asp:Label></TD><TD><asp:TextBox id="txtNumeroDocumento" runat="server" SkinID="texto"></asp:TextBox></TD></TR><TR><TD><asp:Label id="lblNumeroExpediente" runat="server" Text="Número de Expediente:" SkinID="etiqueta_negra"></asp:Label></TD><TD><asp:TextBox id="txtNumeroExpediente" runat="server" SkinID="texto"></asp:TextBox></TD></TR><TR><TD><asp:Label id="lblFechaInicial" runat="server" Text="Fecha Desde (dd/mm/aaaa):" SkinID="etiqueta_negra"></asp:Label></TD><TD><asp:TextBox id="txtFechaInicial" runat="server" SkinID="texto_corto" MaxLength="10"></asp:TextBox>&nbsp; <cc1:CalendarExtender id="calFechaInicial" runat="server" TargetControlID="txtFechaInicial" Format="dd/MM/yyyy"></cc1:CalendarExtender> </TD></TR><TR><TD><asp:Label id="lblFechaFinal" runat="server" Text="Fecha Hasta (dd/mm/aaaa):" SkinID="etiqueta_negra"></asp:Label></TD><TD><asp:TextBox id="txtFechaFinal" runat="server" SkinID="texto_corto" MaxLength="10"></asp:TextBox> <cc1:CalendarExtender id="calFechaFinal" runat="server" TargetControlID="txtFechaFinal" Format="dd/MM/yyyy"></cc1:CalendarExtender> </TD></TR><TR><TD align=center colSpan=2>&nbsp;</TD></TR><TR><TD align=center colSpan=2><asp:Button id="btnConsultar" onclick="btnConsultar_Click" runat="server" Text="Consultar" SkinID="boton_copia"></asp:Button> </TD></TR></TBODY></TABLE></TD></TR><TR><TD></TD></TR><TR><TD style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"><asp:Panel id="pnlPublicaciones" runat="server" Visible="False"><asp:GridView id="grdPublicaciones" runat="server" SkinID="Grilla_simple_peq" AllowPaging="True" OnPageIndexChanging="grdPublicaciones_PageIndexChanging" AutoGenerateColumns="False" DataKeyNames="ID_PUBLICACION,RUTA_PUB" CellPadding="2" CellSpacing="1" RowStyle-HorizontalAlign="Left" OnRowDeleting="grdPublicaciones_RowDeleting" OnRowCreated="grdPublicaciones_RowCreated" OnRowCommand="grdPublicaciones_RowCommand">
<RowStyle HorizontalAlign="Left"></RowStyle>
<Columns>

<asp:ButtonField CommandName="VerDocumento" Text="Ver Documento" HeaderText="Ver Documento"></asp:ButtonField>
<asp:BoundField DataField="TITULO_PUB" HeaderText="Titulo Publicacion"></asp:BoundField>
<asp:BoundField DataField="TIPO_TRAMITE" HeaderText="Tr&#225;mite"></asp:BoundField>
<asp:BoundField DataField="AUTORIDAD_AMBIENTAL" HeaderText="Autoridad Ambiental"></asp:BoundField>
<asp:BoundField DataField="SEC_NOMBRE" HeaderText="Sector"></asp:BoundField>
<asp:BoundField DataField="EXP_NOMBRE" HeaderText="Nombre del Proyecto, Obra o Actividad"></asp:BoundField>
<%--<asp:BoundField DataField="TIPO_ACTO_ADM" HeaderText="Tipo de Documento" Visible="False"></asp:BoundField>
<asp:BoundField DataField="ACTO_ADMINISTRATIVO" HeaderText="Objeto de Documento" Visible="False"></asp:BoundField>--%>
<asp:BoundField DataField="NUM_DOCUMENTO" HeaderText="N&#250;mero de Documento"></asp:BoundField>
<asp:BoundField DataField="EXP_CODIGO" HeaderText="N&#250;mero Expediente"></asp:BoundField>
<asp:BoundField DataField="FECHA_FIJACION" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha de Publicaci&#243;n o Fijaci&#243;n"></asp:BoundField>
<asp:BoundField DataField="FECHA_DESFIJACION" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Desfijaci&#243;n"></asp:BoundField>
<asp:ButtonField CommandName="VerDetalle" Text="Ver"></asp:ButtonField>
<asp:TemplateField ShowHeader="False" Visible="False"><ItemTemplate>
   <asp:Label ID="lblId" runat="server" Text='<%# Bind("ID_PUBLICACION") %>' Visible="False"></asp:Label>
   <asp:LinkButton ID="lnkEliminar" runat="server" CausesValidation="False" CommandName="Delete" Text="Eliminar"></asp:LinkButton>
  
</ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView> </asp:Panel> </TD></TR><TR><TD>&nbsp;<asp:DetailsView id="dvwDocumento" runat="server" SkinID="Detalles" Visible="False" AutoGenerateRows="False" Height="50px"><Fields>
<asp:BoundField DataField="ID_PUBLICACION" HeaderText="C&#243;digo">
<HeaderStyle Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="TITULO_PUB" HeaderText="T&#237;tulo Publicaci&#243;n">
<HeaderStyle Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="TIPO_TRAMITE" HeaderText="Tipo de Tr&#225;mite">
<HeaderStyle Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="AUTORIDAD_AMBIENTAL" HeaderText="Autoridad Ambiental">
<HeaderStyle Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SEC_NOMBRE" HeaderText="Sector">
<HeaderStyle Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="EXP_NOMBRE" HeaderText="Nombre del Proyecto">
<HeaderStyle Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<%--<asp:BoundField DataField="TIPO_ACTO_ADM" HeaderText="Tipo de Documento" Visible="False">
<HeaderStyle Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ACTO_ADMINISTRATIVO" HeaderText="Objeto de Documento" Visible="False">
<HeaderStyle Font-Bold="True"></HeaderStyle>
</asp:BoundField>--%>
<asp:BoundField DataField="NUM_DOCUMENTO" HeaderText="N&#250;mero de Documento">
<HeaderStyle Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="EXP_CODIGO" HeaderText="N&#250;mero de Expediente">
<HeaderStyle Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_FIJACION" HeaderText="Fecha de Publicaci&#243;n o Fijaci&#243;n">
<HeaderStyle Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_DESFIJACION" HeaderText="Fecha Desfijaci&#243;n">
<HeaderStyle Font-Bold="True"></HeaderStyle>
</asp:BoundField>

<asp:HyperLinkField HeaderText="Ver Documentos" NavigateUrl="~/Informacion/DetalleDocumentos.aspx"
        Target="_parent" Text="Ver Documentos">
        <HeaderStyle Font-Bold="True" />
        <ItemStyle Font-Bold="True" />
</asp:HyperLinkField>

<%--
<asp:TemplateField HeaderText="Documento">
<ItemTemplate>
  <asp:HyperLink ID="hplDocumentoDetalle" runat="server" NavigateUrl='<%# String.Format("{0}", DataBinder.Eval(Container, "DataItem.RUTA_PUB", "{0}")) %>'>Documento</asp:HyperLink>
</ItemTemplate>

<HeaderStyle Font-Bold="True"></HeaderStyle>
</asp:TemplateField>--%>
</Fields>
</asp:DetailsView> &nbsp; </TD></TR><TR><TD><asp:Panel id="pnlComentarios" runat="server" Visible="False" Height="150px" ScrollBars="Both" Width="510px">
                                <asp:GridView ID="grdComentarios" runat="server" AutoGenerateColumns="False" SkinID="Grilla_simple_peq" Width="150px">
                                    <Columns>
                                        <asp:BoundField DataField="TEXTO_COMENTARIO" HeaderText="Comentario" />
                                        <asp:BoundField DataField="FECHA_COMENTARIO" HeaderText="Fecha" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel> </TD></TR><TR><TD><asp:Panel id="pnlAgregarComentario" runat="server" Visible="False" Width="510px">
                                <table style="width: 100%">
                                    <tr>
                                        <td valign="top" style="height: 88px">
                                            <asp:Label ID="lblComentario" runat="server" Text="Comentario:"></asp:Label>
                                        </td>
                                        <td style="height: 88px">
                                            <asp:TextBox ID="txtComentario" runat="server" Rows="5" TextMode="MultiLine" Width="200px"
                                                SkinID="texto" MaxLength="800"></asp:TextBox>
                                            <br />
                            <asp:Label ID="lblComentarioError" runat="server" BackColor="White" Text="No ha insertado texto para el comentario" ForeColor="Red" Visible="False"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" SkinID="boton_copia" OnClick="btnAgregar_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel> &nbsp;&nbsp;&nbsp;&nbsp; </TD></TR></TBODY></TABLE>
</ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
