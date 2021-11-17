<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ActualizarFechaSancion.aspx.cs" Inherits="RUIA_ActualizarFechaSancion" Title="Actualizar Fecha sanción" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceHolder" runat="Server">
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
        <asp:Label ID="lblTituloPrincipal" runat="server" SkinID="titulo_principal_blanco" Text="ACTUALIZACION DE FECHA DE SANCION"></asp:Label>
    </div>

    <asp:ScriptManager ID="scmManejador" runat="server" EnableScriptGlobalization="true">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="uppPanelPublicaciones" runat="server">
        <ContentTemplate>
            <div class="table-responsive" style="overflow: auto;">
                <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%;">
                    <tbody>
                        <tr>
                            <td style="text-align: left; vertical-align: top;">
                                <asp:GridView ID="grdSanciones" runat="server" SkinID="Grilla" 
                                    OnRowCreated="grdSanciones_RowCreated" 
                                    OnPageIndexChanging="grdSanciones_PageIndexChanging" 
                                    EmptyDataText="No Existen Registros Sin Fecha de Sancion" 
                                    OnRowCancelingEdit="grdSanciones_RowCancelingEdit" 
                                    OnRowEditing="grdSanciones_RowEditing" 
                                    OnRowUpdating="grdSanciones_RowUpdating" 
                                    RowStyle-HorizontalAlign="Left" 
                                    CellSpacing="1" CellPadding="2" 
                                    DataKeyNames="SAN_ID_SANCION" 
                                    AutoGenerateColumns="False" Width="100%">
                                    <HeaderStyle Font-Size="9pt" />
                                    <FooterStyle Font-Size="9pt" ForeColor="#000000" />
                                    <RowStyle Font-Size="9pt" ForeColor="#000000" />
                                    <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                                    <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                                    <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                                    <Columns>
                                        <asp:BoundField DataField="SAN_ID_SANCION" ReadOnly="true">
	                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
	                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="9pt" ForeColor="#000000" />
	                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AUT_NOMBRE" HeaderText="Autoridad Ambiental" Visible="False"></asp:BoundField>
                                        <asp:BoundField DataField="SAN_NUMERO_EXPE" HeaderText="N&#250;mero de Expediente" ReadOnly="true">
	                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
	                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="9pt" ForeColor="#000000" />
	                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SAN_NUMERO_ACTO" ReadOnly="true" HeaderText="N&#250;mero de Acto que impone sanci&#243;n ">
	                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
	                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="9pt" ForeColor="#000000" />
	                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TIF_NOMBRE" HeaderText="Tipo de Falta" ReadOnly="true">
	                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
	                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="9pt" ForeColor="#000000" />
	                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OPS_NOMBRE_OPCION" ReadOnly="true" HeaderText="Tipo de Sanci&#243;n">
	                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
	                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="9pt" ForeColor="#000000" />
	                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NOMBRES" ReadOnly="true" HeaderText="Nombre de la Persona Sancionada">
	                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
	                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="9pt" ForeColor="#000000" />
	                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Fecha de Cumplimiento de la Sanci&#243;n">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtFechaEjecucion" runat="server" SkinID="texto_corto" Text='<%# Bind("SAN_FECHA_EJECUCION_ACTO","{0:dd/MM/yyyy}") %>' __designer:wfdid="w15" MaxLength="10"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFechaEjecucion" runat="server" __designer:wfdid="w16" Display="Dynamic" ErrorMessage="Ingrese fecha de ejecución o cuplimiento de la sanción" ControlToValidate="txtFechaEjecucion">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="revFechaEjecucion" runat="server" __designer:wfdid="w17" ErrorMessage="Formato de fecha no valido para la fecha de ejecución o cumplimiento de la sanción" ControlToValidate="txtFechaEjecucion" ValidationExpression="^\d{2}\/\d{2}\/\d{4}">*</asp:RegularExpressionValidator><cc1:CalendarExtender ID="calFechaEjecucion" runat="server" __designer:wfdid="w18" TargetControlID="txtFechaEjecucion" Format="dd/MM/yyyy" Enabled="True" __designer:errorcontrol="No se puede establecer 'True' en la propiedad 'Enabled'."></cc1:CalendarExtender>
                                                <asp:Label ID="lblSancionPrincipal" runat="server" Text='<%# Bind("RSA_ID_OPCION") %>' Visible="False" __designer:wfdid="w19" SkinID="etiqueta_negra9"></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblFechaCumplimiento" runat="server" Text='<%# Bind("SAN_FECHA_EJECUCION_ACTO","{0:dd/MM/yyyy}") %>' __designer:wfdid="w14" SkinID="etiqueta_negra9"></asp:Label>
                                            </ItemTemplate>
	                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
	                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="9pt" ForeColor="#000000" />
	                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkGuardar" runat="server" Text="Guardar" __designer:wfdid="w14" CausesValidation="false" CommandName="UPDATE"></asp:LinkButton>
                                                <asp:LinkButton ID="lnkCancelar" runat="server" Text="Cancelar" __designer:wfdid="w15" CausesValidation="false" CommandName="CANCEL"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkActualizar" runat="server" Text="Actualizar" __designer:wfdid="w13" CausesValidation="false" CommandName="EDIT"></asp:LinkButton>
                                            </ItemTemplate>
	                                        <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
	                                        <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
	                                        <FooterStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 20px; padding-bottom: 10px; padding-left: 10px; padding-right: 20px; text-align: right; vertical-align: middle;">
                                <asp:Button ID="btnCancelar" OnClick="btnCancelar_Click" runat="server" SkinID="boton" __designer:dtid="844424930132230" Text="Regresar" ValidationGroup="xxx" OnClientClick="return cancelarRUIA();"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 10px; text-align: left; vertical-align: middle;">
                                <asp:ValidationSummary ID="valResumen" runat="server"></asp:ValidationSummary>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

