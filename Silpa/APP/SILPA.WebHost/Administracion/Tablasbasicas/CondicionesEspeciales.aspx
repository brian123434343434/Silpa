<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="CondicionesEspeciales.aspx.cs" Inherits="Administracion_Tablasbasicas_CondicionesEspeciales"  Title="Tabla Basica Tipo Tramite" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" runat="Server">

    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
    <link href="../../App_Themes/skin/AyudaStyle.css" rel="stylesheet" />
    <script src="../../jquery/jquery.js" type="text/javascript"></script>
    <script src="../../js/Ayuda.js" type="text/javascript"></script>
    <link href="css/AdministracionCodicionesEspeciales.css" rel="stylesheet" />

    <asp:ScriptManager ID="scmFlujo" runat="server"></asp:ScriptManager>

    <table class="TablaTituloSeccionAdmNot">
        <tr>
            <td class="div-titulo">
                <a href="#" id="hrfCerrarVentana" runat="server" onclick="window.close();return false;" class="Salir">Salir [X]</a>
            </td>
        </tr>
        <tr>
            <td class="div-titulo">
                <asp:Label ID="Label4" runat="server" Text="CONDICIONES ESPECIALES" SkinID="titulo_principal_blanco"></asp:Label>
            </td>
        </tr>
    </table>

    <asp:UpdatePanel runat="server" ID="upnlFlujo" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="TablaAdmNot">
                <tr>
                    <td colspan="2" class="TituloSeccionAdmNot">CRITERIOS DE BUSQUEDA</td>
                </tr>
                <tr>
                    <td class="LabelFormularioBusquedaAdmNot">
                        Condición:
                    </td>
                    <td class="CamposFormularioBusquedaAdmNot">
                        <asp:DropDownList ID="cboCondicion" runat="server" Width="100%" AutoPostBack="true" onselectedindexchanged="cboCondicion_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="LabelFormularioBusquedaAdmNot">
                        Tipo condición:
                    </td>
                    <td class="CamposFormularioBusquedaAdmNot">
                        <asp:DropDownList ID="cboTipoCondicion" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="cboTipoCondicion_SelectedIndexChanged">
                            <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                            <asp:ListItem Text="Exclusion Documental" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Condición Normal" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cboCondicion" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>

     <asp:UpdatePanel runat="server" ID="upnlResultadoEstados" >
        <ContentTemplate>       
            <div class="table-responsive" id="divEstados" runat="server">
                <table id="rowNuevoFlujo" runat="server" class="TablaAdmNot">
                    <tr>
                        <td>
                            <asp:Button ID="btnAgregarCondicion" runat="server" Text="Agregar condición" ClientIDMode="Static" CausesValidation="False" OnClick="btnAgregar_Click"></asp:Button>                            
                        </td>
                    </tr>
                </table>
                <table class="TablaAdmNot">
                    <tr>
                        <td>
                             <asp:GridView ID="grdDatos" runat="server" Width="100%" OnPageIndexChanging="grdDatos_PageIndexChanging" ClientIDMode="Static"
                                    OnRowCommand="grdDatos_RowCommand" EmptyDataText="No existen datos registrados en ésta tabla"
                                    AllowPaging="True" AutoGenerateColumns="False" SkinID="GrillaAdministracionNotificacion" PageSize="15" DataKeyNames="CON_ID,TIPO_CONDICION_ESPECIAL">
                                    <Columns>                                        
                                        <asp:BoundField DataField="CONDICION" HeaderText="Condicion"></asp:BoundField>                                        
                                        <asp:BoundField DataField="CODIGO_CONDICION" HeaderText="Código Condición"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Tipo condicion">
                                            <ItemTemplate>
                                                <asp:Literal ID="lblTipoCondicion" runat="server" Text='<%# Eval("TIPO_CONDICION_ESPECIAL").ToString() == "1" ? "Exclusion documental" : "Condición normal" %>'></asp:Literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText = "EDITAR" ItemStyle-CssClass="TextoFilaCentro">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEditarEstado" CommandName="Modificar" CommandArgument='<%# Container.DataItemIndex %>' Text="Editar"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Eliminar" ItemStyle-CssClass="TextoFilaCentro">
                                            <ItemTemplate>                                                
                                                <asp:Label runat="server" ID="lblCondicion" Text='<%# Bind("CON_ID") %>' Visible="false"></asp:Label>
                                                <asp:LinkButton ID="lnkEliminar" CommandName="Eliminar" runat="server" CommandArgument='<%# Container.DataItemIndex %>'>Eliminar Registro</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAgregarCondicion" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="grdDatos" EventName="PageIndexChanging" />
        </Triggers>
    </asp:UpdatePanel> 


    <input type="button" runat="server" id="cmdNuevaCondicionHide" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeCondicionNewEdit" runat="server" PopupControlID="dvCondicionNewEdit" TargetControlID="cmdNuevaCondicionHide" BehaviorID="mpeCondicionNewEdit" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>    
    <div id="dvCondicionNewEdit" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalFormularioNot">        
        <asp:UpdatePanel runat="server" ID="upnlFormulario">
            <ContentTemplate>
                <table class="TablaFormularioAdmNot">
                    <tr>
                        <td colspan="2" class="TituloSeccionAdmNot">
                            <asp:Literal runat="server" ID="ltlTituloCrear"></asp:Literal>
                        </td>
                    </tr>
                    <tr runat="server" visible="false" id="divMensajeModal">
                        <td colspan="2">
                            <table class="TablaMensajeErrorAdmNot" >
                                <tr>
                                    <td class="MensajeErrorAdmNot">
                                        <asp:Literal runat="server" ID="lblMensajeModal"></asp:Literal>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            Condición:
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:DropDownList ID="cboConIns" runat="server" Width="100%" AutoPostBack="true" onselectedindexchanged="cboConIns_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            Codigo:
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:TextBox ID="txtCodigoIns" runat="server" SkinID="texto" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="ModalLabelFormularioAdmNot">
                            Tipo condición:
                        </td>
                        <td class="ModalCamposFormularioBusquedaAdmNot">
                            <asp:DropDownList runat="server" ID="cboTipoCondicionNewEdit">
                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                <asp:ListItem Text="Exclusion Documental" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Condición Normal" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvEstadoFlujo" ControlToValidate="cboTipoCondicionNewEdit" InitialValue="" ValidationGroup="condicion" ErrorMessage="Debe seleccionar un tipo de condición">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                </table>
                <table class="TablaBotonesFormularioAdmNot">
                    <tr>
                        <td>
                            <asp:ValidationSummary ID="valFirmaModal" runat="server" ValidationGroup="condicion" ShowMessageBox="true" ShowSummary="false" />
                            <asp:HiddenField runat="server" ID="hdfCondicionID" />
                            <asp:Button ID="cmdGuardarCondicion" runat="server" Text="Adicionar" CssClass="boton" ValidationGroup="EstadoModal" OnClick="btnRegistrar_Click"/>
                            <asp:Button ID="cmdCancelar" runat="server" Text="Cancelar" CssClass="boton" OnClick="btnCancelarReg_Click" CausesValidation="false"/>
                        </td>
                    </tr>
                </table>                                   
            </ContentTemplate>
        </asp:UpdatePanel>     
    </div>
    <asp:UpdatePanel runat="server" ID="upnlMensajes" UpdateMode="Conditional">
        <ContentTemplate>
            <table runat="server" visible="false" id="divMensaje" class="TablaMensajeErrorAdmNot" >
                <tr>
                    <td class="MensajeErrorAdmNot">
                        <asp:Literal runat="server" ID="lblMensaje"></asp:Literal>
                        <br />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>


    <table width="100%">
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel ID="updConsultar" runat="server">
                        <ContentTemplate>
                           <%-- <asp:Panel ID="pnlMaestro" runat="server" Width="100%">
                                <table width="60%">
                                    <tbody>                            
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text="Codicion"  SkinID="etiqueta_negra"></asp:Label></td>
                                            <td align="left">
                                                </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTipoCon" runat="server" Text="Código"  SkinID="etiqueta_negra"></asp:Label></td>
                                            <td align="left">
                                                </td>
                                        </tr> 
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text=""  SkinID="etiqueta_negra"></asp:Label></td>
                                            <td align="left">
                                                </td>
                                        </tr>                                       
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="right">
                                                
                                               
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlConsultar" runat="server" Width="100%" Visible="true">
                               
                            </asp:Panel>--%>
                            <%--<asp:Panel ID="pnlEditar" runat="server" Width="100%" Visible="false">
                                <table width="70%">
                                    <tbody>
                                        <tr>
                                            <td align="left" style="width: 25%">
                                                <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label></td>
                                            <td  align="left">
                                                <asp:TextBox ID="txtNombre" runat="server" MaxLength="30"></asp:TextBox>
                                                <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblTipo" runat="server" Text="Proceso"></asp:Label></td>
                                            <td  align="left">
                                                <asp:DropDownList ID="cboTipo" runat="server" Width="100%">
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label1" runat="server" Text="Visible"></asp:Label></td>
                                            <td  align="left">
                                                <asp:CheckBox ID="chkVisible" runat="server">
                                                </asp:CheckBox></td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="4">                                                
                                                <asp:Button ID="btnCancelar" OnClick="btnCancelar_Click" runat="server" SkinID="boton"
                                                    Text="Cancelar" CausesValidation="False"></asp:Button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>--%>
                            <%--<asp:Panel ID="pnlNuevo" runat="server" Visible="false">
                                <table width="60%">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text="Codicion"></asp:Label></td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboConIns" runat="server" Width="100%" 
                                                    AutoPostBack="true" onselectedindexchanged="cboConIns_SelectedIndexChanged">
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Text="Código"></asp:Label></td>
                                            <td align="left">
                                                <asp:TextBox ID="txtCodigoIns" runat="server" SkinID="texto" Width="100%"></asp:TextBox></td>
                                        </tr>     
                                        <tr>
                                            <td align="right" colspan="2">
                                                &nbsp;<asp:Button ID="btnRegistrar" OnClick="" runat="server" SkinID="boton"
                                                    Text="Aceptar"></asp:Button>
                                                <asp:Button ID="btnCancelarReg" OnClick="" runat="server" SkinID="boton"
                                                    Text="Cancelar" CausesValidation="False"></asp:Button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>--%>
                            <asp:Label ID="lblMensajeError" runat="server" Font-Bold="True" ForeColor="Red" ></asp:Label>
                       
                        </ContentTemplate>
                        
                    </asp:UpdatePanel>
                    &nbsp;
                </td>
            </tr>
        </table>    
</asp:Content>
