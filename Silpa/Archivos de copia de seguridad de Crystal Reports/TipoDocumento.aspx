<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="TipoDocumento.aspx.cs" Inherits="Administracion_Tablasbasicas_TipoDocumento" Title="Tipo De Documento" %>


<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" runat="Server">

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
    <link href="../../App_Themes/skin/AyudaStyle.css" rel="stylesheet" />
    <script src="../../jquery/jquery.js" type="text/javascript"></script>
    <script src="../../js/Ayuda.js" type="text/javascript"></script>
    <link href="css/AdministracionCodicionesEspeciales.css" rel="stylesheet" />


    <asp:ScriptManager ID="scmPagina" runat="server"></asp:ScriptManager>
    <table class="TablaTituloSeccionAdmNot">
        <tr>
            <td class="div-titulo">
                <a href="#" id="hrfCerrarVentana" runat="server" onclick="window.close();return false;" class="Salir">Salir [X]</a>
            </td>
        </tr>
        <tr>
            <td class="div-titulo">
                <asp:Label ID="Label4" runat="server" Text="TIPO DOCUMENTO POR ACTIVIDAD" SkinID="titulo_principal_blanco"></asp:Label>
            </td>
        </tr>
    </table>


    <asp:UpdatePanel ID="updConsultar" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMaestro" runat="server">
                <table class="TablaAdmNot">
                    <tbody>
                        <tr>
                            <td colspan="2" class="TituloSeccionAdmNot">CRITERIOS DE BUSQUEDA</td>
                        </tr>
                        <tr>
                            <td class="LabelFormularioBusquedaAdmNot">
                                Nombre Documento:
                            </td>
                            <td class="CamposFormularioBusquedaAdmNot">
                                <asp:TextBox ID="txtNombreParametro" runat="server" SkinID="texto" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="LabelFormularioBusquedaAdmNot">
                                Parámetro:
                            </td>
                            <td class="CamposFormularioBusquedaAdmNot">
                                <asp:DropDownList ID="cboTipoParametro" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" SkinID="boton_copia"
                                    Text="Buscar"></asp:Button>&nbsp;<asp:Button ID="btnAgregar" OnClick="btnAgregar_Click"
                                        runat="server" SkinID="boton_copia" Text="Agregar" CausesValidation="False">
                                </asp:Button>&nbsp;<asp:Button ID="btnVolver" runat="server" SkinID="boton_copia"
                                    Text="Cancelar" PostBackUrl="~/Administracion/Tablasbasicas/TablasBasicas.aspx">
                                </asp:Button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlConsultar" runat="server" Visible="true">
                <table class="TablaAdmNot">
                    <tr>
                       <td>
                            <asp:GridView ID="grdDatos" runat="server" Width="100%" OnPageIndexChanging="grdDatos_PageIndexChanging"
                    DataKeyNames="ID,HABILITADO_REPOSICION, ID_BPM_PARAMETRO, ID_FLUJO_NOT_ELEC, CON_ID" OnRowCommand="grdDatos_RowCommand"
                    EmptyDataText="No existen datos registrados en ésta tabla" AllowSorting="True"
                    AllowPaging="True" AutoGenerateColumns="False" SkinID="GrillaAdministracionNotificacion">
                    <Columns>
                        <asp:BoundField DataField="NOMBRE_DOCUMENTO" HeaderText="Nombre Documento"></asp:BoundField>
                        <asp:BoundField DataField="CODIGO_CONDICION_BPM" HeaderText="Código de condición">
                        </asp:BoundField>
                        <asp:BoundField DataField="NOMBRE" HeaderText="Parámetro"></asp:BoundField>
                        <asp:BoundField DataField="CONDICION_ESPECIAL" HeaderText="Condicion Especial"></asp:BoundField>
                        <asp:BoundField DataField="ESTADO" HeaderText="Estado"></asp:BoundField>
                        
                        <asp:TemplateField HeaderText="Editar">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkModificar" CommandName="Modificar" runat="server" CommandArgument='<%# Container.DataItemIndex %>'>Modificar Registro</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Eliminar">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEliminar" CommandName="Eliminar" runat="server" CommandArgument='<%# Container.DataItemIndex %>'>Eliminar Registro</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                       </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlEditar" runat="server" Visible="false">
                <table class="TablaAdmNot">
                    <tbody>
                        <tr>
                            <td colspan="2" class="TituloSeccionAdmNot">
                                EDICION REGISTRO
                            </td>
                        </tr>
                        <tr>
                            <td class="LabelFormularioBusquedaAdmNot">
                                Nombre Documento:
                            </td>
                            <td class="CamposFormularioBusquedaAdmNot">
                                <asp:TextBox ID="txtNombre" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="LabelFormularioBusquedaAdmNot">
                                Código de condición:
                            </td>
                            <td class="CamposFormularioBusquedaAdmNot">
                                <asp:TextBox ID="txtConvenio" runat="server" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="LabelFormularioBusquedaAdmNot">
                                Parámetro:
                            </td>
                            <td class="CamposFormularioBusquedaAdmNot">
                                <asp:DropDownList ID="cboTipo" runat="server" Width="100%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="LabelFormularioBusquedaAdmNot">
                                Condición especial exclusión documental:
                            </td>
                            <td class="CamposFormularioBusquedaAdmNot">
                                <asp:DropDownList ID="cboCodigoExDocEdit" runat="server" Width="100%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="LabelFormularioBusquedaAdmNot">
                                Estado:
                            </td>
                            <td class="CamposFormularioBusquedaAdmNot">
                                <asp:CheckBox ID="chkEstado" runat="server" EnableTheming="false"></asp:CheckBox>
                                <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="LabelFormularioBusquedaAdmNot">
                                Tipo Notificación Electrónica:
                            </td>
                            <td class="CamposFormularioBusquedaAdmNot">
                                <asp:DropDownList ID="cboTipoNotElec" runat="server" Width="100%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </tbody>
                </table>
                 <table class="TablaBotonesFormularioAdmNot">
                    <tr>
                        <td>
                            <asp:Button ID="btnActualizar" OnClick="btnActualizar_Click" runat="server" SkinID="boton_copia"
                                    Text="Aceptar"></asp:Button>
                                <asp:Button ID="btnCancelar" OnClick="btnCancelar_Click" runat="server" SkinID="boton_copia"
                                    Text="Cancelar" CausesValidation="False"></asp:Button>
                        </td>
                    </tr>
                </table>   
            </asp:Panel>
            <asp:Panel ID="pnlNuevo" runat="server" Visible="false">
                <table class="TablaAdmNot">
                        <tr>
                            <td colspan="2" class="TituloSeccionAdmNot">
                                NUEVO REGISTRO
                            </td>
                        </tr>
                        <tr>
                            <td class="LabelFormularioBusquedaAdmNot">
                                Nombre Documento:
                            </td>
                            <td class="CamposFormularioBusquedaAdmNot">
                                <asp:TextBox ID="txtNombreNvo" runat="server" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="LabelFormularioBusquedaAdmNot">
                                Código de condición:
                            </td>
                            <td class="CamposFormularioBusquedaAdmNot">
                                <asp:TextBox ID="txtConvenioNvo" runat="server" MaxLength="30">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="LabelFormularioBusquedaAdmNot">
                                Parámetro:
                            </td>
                            <td class="CamposFormularioBusquedaAdmNot">
                                <asp:DropDownList ID="cboTipoNvo" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="LabelFormularioBusquedaAdmNot">
                                Condición especial exclusión documental:
                            </td>
                            <td class="CamposFormularioBusquedaAdmNot">
                                <asp:DropDownList ID="cboCodigoExDocNew" runat="server" Width="100%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="LabelFormularioBusquedaAdmNot">
                                Estado: 
                            </td>
                            <td class="CamposFormularioBusquedaAdmNot">
                                <asp:CheckBox ID="chkEstadoNvo" runat="server" EnableTheming="false"></asp:CheckBox>
                            </td>
                        </tr>
                </table>
                 <table class="TablaBotonesFormularioAdmNot">
                    <tr>
                        <td>
                            <asp:Button ID="btnRegistrar" OnClick="btnRegistrar_Click" runat="server" SkinID="boton_copia"
                                    Text="Aceptar"></asp:Button>
                                <asp:Button ID="btnCancelarReg" OnClick="btnCancelarReg_Click" runat="server" SkinID="boton_copia"
                                    Text="Cancelar" CausesValidation="False"></asp:Button>
                        </td>
                    </tr>
                </table>   
            </asp:Panel>
            <asp:Label ID="lblMensajeError" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click"></asp:AsyncPostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>
                   
</asp:Content>
