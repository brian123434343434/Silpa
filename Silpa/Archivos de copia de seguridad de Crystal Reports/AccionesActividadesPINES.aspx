<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="AccionesActividadesPINES.aspx.cs" Inherits="Administracion_Tablasbasicas_AccionesActivdadesPINES" %>

<asp:Content ID="Content3" ContentPlaceHolderID="headPlaceHolder" runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>

    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="Acciones Actividades PINES" SkinID="titulo_principal_blanco"></asp:Label>
    </div>    

    <div class="table-responsive">
        <asp:UpdatePanel ID="updConsultar" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlMaestro" runat="server" Width="100%">
                    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
                        <tbody>                            
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Actividad" SkinID="etiqueta_negra10NN"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboActividad" runat="server" Width="100%"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                                    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%;">
                                        <tr>
                                            <td style="text-align: right; vertical-align: middle;">
                                                <asp:Button ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" SkinID="boton" Text="Buscar"></asp:Button>
                                            </td>
                                            <td style="padding-right: 30px; padding-left: 30px; text-align: center; vertical-align: middle;">
                                                <asp:Button ID="btnAgregar"  runat="server" SkinID="boton" Text="Agregar" CausesValidation="False" OnClick="btnAgregar_Click"></asp:Button>
                                            </td>
                                            <td style="text-align: left; vertical-align: middle;">
                                                <asp:Button ID="btnVolver" runat="server" SkinID="boton" Text="Cancelar" PostBackUrl="~/Administracion/Tablasbasicas/TablasBasicas.aspx"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlConsultar" runat="server" Width="100%" Visible="true">
                    <asp:GridView ID="grdDatos" runat="server" Width="100%" EmptyDataText="No existen datos registrados en ésta tabla"
                        AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False" SkinID="Grilla" DataKeyNames="IDACTIVITY" OnRowCommand="grdDatos_RowCommand">
                        <Columns>                                        
                            <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción"></asp:BoundField>                                        
                            <asp:BoundField DataField="DIAS_EJECUCION" HeaderText="Dias Ejecución"></asp:BoundField>  
                            <asp:TemplateField HeaderText="Obligatorio">
                                <ItemTemplate> 
                                    <asp:Label runat="server" ID="lblOligatoriedad" Text='<%# Convert.ToBoolean(Eval("OBLIGATORIA"))? "Si":"No"%>' SkinID="etiqueta_negra10N"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                                       
                            <asp:TemplateField HeaderText="Orden">
                                <ItemTemplate> 
                                    <asp:Label runat="server" ID="lblOrden" Text='<%# Convert.ToString(Eval("ORDEN")) == "999"? "N/A":Eval("ORDEN")%>' SkinID="etiqueta_negra10N"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Activa">
                                <ItemTemplate> 
                                    <asp:Label runat="server" ID="lblActivo" Text='<%# Convert.ToBoolean(Eval("ACTIVO"))? "Si":"No"%>' SkinID="etiqueta_negra10N"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Editar">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkModificar" CommandName="Modificar" runat="server" CommandArgument='<%# Container.DataItemIndex %>' SkinID="etiqueta_negra10N">Modificar Registro</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eliminar">
                                <ItemTemplate>                                                
                                    <asp:Label runat="server" ID="lblAccion" Text='<%# Bind("IDACCION") %>' Visible="false" SkinID="etiqueta_negra10N"></asp:Label>
                                    <asp:LinkButton ID="lnkEliminar" OnClientClick="return confirm('¿Esta seguro de eliminar el registro?');" CommandName="Eliminar" runat="server" CommandArgument='<%# Container.DataItemIndex %>'>Eliminar Registro</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="pnlEditar" runat="server" Width="100%" Visible="false">
                    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
                        <tbody>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Actividad:" SkinID="etiqueta_negra10NN"></asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="cboActividadEdit" runat="server" Width="100%" Enabled="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Text="Acción:" SkinID="etiqueta_negra10NN"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtAccionEdit" runat="server" ReadOnly="true"></asp:TextBox>
                                    <asp:HiddenField ID="hdfIdAccionEdit" runat="server" />
                                </td>
                            </tr> 
                            <tr>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Text="Dias Ejecución:" SkinID="etiqueta_negra10NN"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtDiasEjecucionEdit" runat="server" MaxLength="2" Width="20"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDiasEjecucionEdit" runat="server" ControlToValidate="txtDiasEjecucionEdit" ValidationGroup="Edit">*</asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender ID="fteDiasEjecucionEdit" runat="server" TargetControlID="txtDiasEjecucionEdit" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>  
                            <tr>
                                <td>
                                    <asp:Label ID="Label10" runat="server" Text="Es Obligatorio:" SkinID="etiqueta_negra10NN"></asp:Label></td>
                                <td>
                                    <asp:CheckBox ID="chkObligatorioEdit" runat="server" OnCheckedChanged="chkObligatorioEdit_CheckedChanged" AutoPostBack="true" EnableTheming="false" />
                                </td>
                            </tr> 
                            <tr>
                                <td>
                                    <asp:Label ID="Label11" runat="server" Text="Orden:" SkinID="etiqueta_negra10NN"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtOrdenEdit" runat="server" MaxLength="2" Width="20"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvOrdenEdit" runat="server" ControlToValidate="txtOrdenEdit" ValidationGroup="Edit">*</asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender ID="fteOrdenEdit" runat="server" TargetControlID="txtOrdenEdit" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                                    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%;">
                                        <tr>
                                            <td style="padding-right: 15px; text-align: right; vertical-align: middle;">
                                                <asp:Button ID="btnActualizar"  runat="server" SkinID="boton"
                                                    Text="Actualizar" OnClick="btnActualizar_Click" ValidationGroup="Edit"></asp:Button>
                                            </td>
                                            <td style="padding-left: 15px; text-align: left; vertical-align: middle;">
                                                <asp:Button ID="btnCancelarEdit"  runat="server" SkinID="boton"
                                                    Text="Cancelar" CausesValidation="False" OnClick="btnCancelarEdit_Click"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlNuevo" runat="server" Visible="false">
                    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
                        <tbody>
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Actividad:" SkinID="etiqueta_negra10NN"></asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="cboActividadNew" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="cboActividadNew_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvActividadNew" runat="server" ControlToValidate="cboActividadNew" ValidationGroup="New">*</asp:RequiredFieldValidator> 
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Text="Acción:" SkinID="etiqueta_negra10NN"></asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="cboAccionNew" runat="server"></asp:DropDownList>   
                                    <asp:RequiredFieldValidator ID="rfvAccionNew" runat="server" ControlToValidate="cboAccionNew" ValidationGroup="New"></asp:RequiredFieldValidator> 
                                </td>
                            </tr> 
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Dias Ejecución:" SkinID="etiqueta_negra10NN"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtDiasEjecucionNew" runat="server" MaxLength="2" Width="20"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDiasEjecucionNew" runat="server" ControlToValidate="txtDiasEjecucionNew" ValidationGroup="New">*</asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender ID="fteDiasEjecucionNew" runat="server" TargetControlID="txtDiasEjecucionNew" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>  
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Es Obligatorio:" SkinID="etiqueta_negra10NN"></asp:Label></td>
                                <td>
                                    <asp:CheckBox ID="chkObligatorioNew" runat="server" OnCheckedChanged="chkObligatorioNew_CheckedChanged" AutoPostBack="true" EnableTheming="false" />
                                </td>
                            </tr> 
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Orden:" SkinID="etiqueta_negra10NN"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtOrdenNew" runat="server" MaxLength="2" Width="20"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvOrdenNew" runat="server" ControlToValidate="txtOrdenNew" ValidationGroup="New">*</asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender ID="fteOrdenNew" runat="server" TargetControlID="txtOrdenNew" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>  
                            <tr>
                                <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                                    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%;">
                                        <tr>
                                            <td style="padding-right: 15px; text-align: right; vertical-align: middle;">
                                                <asp:Button ID="btnRegistrar"  runat="server" SkinID="boton"
                                                    Text="Aceptar" OnClick="btnRegistrar_Click" ValidationGroup="New"></asp:Button>
                                            </td>
                                            <td style="padding-left: 15px; text-align: left; vertical-align: middle;">
                                                <asp:Button ID="btnCancelarReg"  runat="server" SkinID="boton"
                                                    Text="Cancelar" CausesValidation="False" OnClick="btnCancelarReg_Click"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
                <asp:Label ID="lblMensajeError" runat="server" Font-Bold="True" ForeColor="Red" SkinID="etiqueta_roja_error"></asp:Label>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnBuscar"/>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>