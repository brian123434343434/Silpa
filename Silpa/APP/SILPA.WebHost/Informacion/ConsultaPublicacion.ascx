<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ConsultaPublicacion.ascx.cs" Inherits="Informacion_ConsultaPublicacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
<script src="~/Resources/jquery/3.2.1/jquery.min.js" type="text/javascript"></script>
<script src="~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js" type="text/javascript"></script>

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

<div class="table-responsive">
    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
        <tbody>
            <%--<tr align="center">
                <asp:Label ID="lblTituloPrincipal" runat="server" SkinID="subtitulo"
                    Text="PUBLICACIONES"></asp:Label>
            </tr>--%>
            <tr>
                <td>
                    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
                        <tbody>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTipoTramite" runat="server" Text="Tipo de Trámite:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboTipoTramite" runat="server" SkinID="lista_desplegable2">
                                        <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAutoridadAmbiental" runat="server" Text="Autoridad Ambiental:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboAutoridadAmbiental" runat="server" SkinID="lista_desplegable2">
                                        <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSectorPublicacion" runat="server" Text="Sector:" SkinID="etiqueta_negra"></asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="cboSectorPublicacion" runat="server" SkinID="lista_desplegable2">
                                        <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNombreProyecto" runat="server" Text="Nombre del Proyecto, Obra o Actividad:"
                                        SkinID="etiqueta_negra"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtNombreProyecto" runat="server" SkinID="texto"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTipoDocumento" runat="server" Text="Tipo de Documento:" SkinID="etiqueta_negra"></asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="cboTipoDocumento" runat="server" SkinID="lista_desplegable2"
                                        OnSelectedIndexChanged="cboTipoDocumento_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNumeroDocumento" runat="server" Text="Número de Documento:" SkinID="etiqueta_negra"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtNumeroDocumento" runat="server" SkinID="texto"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNumeroExpediente" runat="server" Text="Número de Expediente:" SkinID="etiqueta_negra"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtNumeroExpediente" runat="server" SkinID="texto"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha Desde (dd/mm/aaaa):" SkinID="etiqueta_negra"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtFechaInicial" runat="server" SkinID="texto_corto" MaxLength="10"></asp:TextBox>&nbsp;
                                    <cc1:CalendarExtender ID="calFechaInicial" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaInicial">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha Hasta (dd/mm/aaaa):" SkinID="etiqueta_negra"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtFechaFinal" runat="server" SkinID="texto_corto" MaxLength="10"></asp:TextBox>
                                    <cc1:CalendarExtender ID="calFechaFinal" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaFinal">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding-top: 10px; padding-bottom: 10px; text-align: left; vertical-align: middle;">
                                    <asp:Label ID="lblError" runat="server" SkinID="etiqueta_roja_error"></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
                        <tr>
                            <td style="padding-top: 20px; padding-bottom: 10px; padding-left: 10px; padding-right: 5px; text-align: center; vertical-align: middle;">
                                <asp:Label ID="Label6" runat="server" Text="B u s q u e d a &nbsp;  d e &nbsp; P u b l i c a c i o n e s " SkinID="titulo_principal"></asp:Label>
                            </td>
                            <td style="padding-top: 20px; padding-bottom: 10px; padding-left: 5px; padding-right: 20px; text-align: center; vertical-align: middle;">
                                <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Consultar Tramite" SkinID="icoConsultar"
                                    OnClick="btnConsultar_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlPublicaciones" runat="server" Visible="False" Width="100%" ScrollBars="Auto" style="padding: 3px; margin: auto; clear: both;">
                        <asp:GridView ID="grdPublicaciones" runat="server" Width="100%" 
                            DataKeyNames="ID_PUBLICACION,RUTA_PUB" SkinID="Grilla" 
                            RowStyle-HorizontalAlign="Left" 
                            CellSpacing="4" CellPadding="4" 
                            BorderWidth="1" BorderColor="#C5C5C5" GridLines="Horizontal"
                            AutoGenerateColumns="False" AllowPaging="True" 
                            OnRowDeleting="grdPublicaciones_RowDeleting" 
                            OnRowCommand="grdPublicaciones_RowCommand"
                            OnRowCreated="grdPublicaciones_RowCreated" 
                            OnPageIndexChanging="grdPublicaciones_PageIndexChanging">
                            <RowStyle HorizontalAlign="Left" BorderWidth="1" BorderColor="#C5C5C5"></RowStyle>
                            <Columns>
                                <asp:ButtonField CommandName="VerDocumento" Text="Ver" HeaderText="Ver"></asp:ButtonField>
                                <asp:BoundField DataField="TITULO_PUB" HeaderText="Titulo Publicacion"></asp:BoundField>
                                <asp:BoundField DataField="TIPO_TRAMITE" HeaderText="Tr&#225;mite"></asp:BoundField>
                                <asp:BoundField DataField="AUTORIDAD_AMBIENTAL" HeaderText="Autoridad Ambiental"></asp:BoundField>
                                <asp:BoundField DataField="SEC_NOMBRE" HeaderText="Sector"></asp:BoundField>
                                <asp:BoundField DataField="EXP_NOMBRE" HeaderText="Nombre del Proyecto, Obra o Actividad"></asp:BoundField>
                                <asp:BoundField DataField="NUM_DOCUMENTO" HeaderText="N&#250;mero de Documento"></asp:BoundField>
                                <asp:BoundField DataField="EXP_CODIGO" HeaderText="N&#250;mero Expediente"></asp:BoundField>
                                <asp:BoundField DataField="FECHA_FIJACION" HeaderText="Fecha de Publicaci&#243;n o Fijaci&#243;n"
                                    DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
                                <asp:BoundField DataField="FECHA_DESFIJACION" HeaderText="Fecha Desfijaci&#243;n"
                                    DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
                                <asp:ButtonField CommandName="VerDetalle" Text="Ver" HeaderText="Ver Detalle"></asp:ButtonField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("ID_PUBLICACION") %>' Visible="False" SkinID="etiqueta_negra"></asp:Label>
                                        <asp:LinkButton ID="lnkEliminar" runat="server" CausesValidation="False" CommandName="Delete"
                                            Text="Eliminar"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron Datos.
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="padding: 4px; margin: auto; clear: both;">
                    <asp:DetailsView ID="dvwDocumento" runat="server" SkinID="Detalles" Visible="False" AutoGenerateRows="False" Width="100%" Font-Size="10pt">
                        <Fields>
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
                                Target="_blank" Text="Ver Documentos">
                                <HeaderStyle Font-Bold="True" />
                                <ItemStyle Font-Bold="True" />
                            </asp:HyperLinkField>
                        </Fields>
                    </asp:DetailsView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlAgregarComentario" runat="server" Visible="False" Width="510px">
                        <table style="width: 100%">
                            <tr>
                                <td valign="top" style="height: 88px">
                                    <asp:Label ID="lblComentario" runat="server" Text="Comentario:" SkinID="etiqueta_negra"></asp:Label>
                                </td>
                                <td style="height: 88px">
                                    <asp:TextBox ID="txtComentario" runat="server" Rows="5" TextMode="MultiLine" Width="200px"
                                        SkinID="texto" MaxLength="800"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="lblComentarioError" runat="server" BackColor="White" Text="No ha insertado texto para el comentario"
                                        ForeColor="Red" Visible="False" SkinID="etiqueta_roja_error"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" SkinID="boton_copia" OnClick="btnAgregar_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlComentarios" runat="server" Visible="False" Width="610px"
                        ScrollBars="Auto">
                        <asp:GridView ID="grdComentarios" runat="server" AutoGenerateColumns="False" SkinID="Grilla_simple_peq"
                            Width="150px">
                            <Columns>
                                <asp:BoundField DataField="TEXTO_COMENTARIO" HeaderText="Comentario" />
                                <asp:BoundField DataField="FECHA_COMENTARIO" HeaderText="Fecha" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
        </tbody>
    </table>
</div>
