<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="CasoProcesoPermitido.aspx.cs" Inherits="Administracion_Tablasbasicas_CasoProcesoPermitido"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="ACTIVAR TRAMITE" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="div-contenido" style="height: 400px">
        <table width="100%">
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel ID="updConsultar" runat="server">
                        <contenttemplate>
<asp:Panel id="pnlMaestro" runat="server" Width="100%">
                                <table width="70%">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblFechaCon" runat="server" Text="Nombre"></asp:Label></td>
                                            <td align="left">
                                                <asp:TextBox ID="txtNombreParametro" runat="server" SkinID="texto" Width="100%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblFechaInicial" runat="server" SkinID="etiqueta_negra" Text="Fecha Desde  (dd/mm/aaaa):"></asp:Label></td>
                                            <td align="left">
                                                <asp:TextBox ID="txtFechaDesde" runat="server" SkinID="texto_corto" MaxLength="10"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="revFechaDesde" runat="server" ValidationExpression="^\d{2}\/\d{2}\/\d{4}"
                                                    ControlToValidate="txtFechaDesde" ErrorMessage="Formato de fecha desde">*</asp:RegularExpressionValidator>
                                                <cc1:CalendarExtender ID="calFechaDesde" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaDesde">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblFechaFinal" runat="server" SkinID="etiqueta_negra" Text="Fecha Hasta  (dd/mm/aaaa)"></asp:Label></td>
                                            <td align="left">
                                                <asp:TextBox ID="txtFechaHasta" runat="server" SkinID="texto_corto" MaxLength="10"></asp:TextBox><asp:CompareValidator
                                                    ID="covCompararFechas" runat="server" ControlToValidate="txtFechaHasta" ErrorMessage='El valor del campo "Fecha Hasta", debe ser posterior al valor del campo "Fecha Desde".'
                                                    Type="Date" Operator="GreaterThan" ControlToCompare="txtFechaDesde" Display="Dynamic">*</asp:CompareValidator>
                                                <asp:RegularExpressionValidator ID="revFechaHasta" runat="server" ValidationExpression="^\d{2}\/\d{2}\/\d{4}"
                                                    ControlToValidate="txtFechaHasta" ErrorMessage="Formato de fecha no valido para la fecha hasta">*</asp:RegularExpressionValidator>
                                                <cc1:CalendarExtender ID="calFechaHasta" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaHasta">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" SkinID="boton_copia"
                                                    Text="Buscar"></asp:Button>
                                                <asp:Button ID="btnAgregar" OnClick="btnAgregar_Click" runat="server" SkinID="boton_copia"
                                                    Text="Agregar" CausesValidation="False"></asp:Button><asp:Button ID="btnVolver" runat="server"
                                                        SkinID="boton_copia" Text="Cancelar" PostBackUrl="~/Administracion/Tablasbasicas/TablasBasicas.aspx">
                                                    </asp:Button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel> <asp:Panel id="pnlConsultar" runat="server" Width="100%" Visible="true">
                                <asp:Panel ID="pnlConsultarDatos" runat="server" Width="800px" ScrollBars="Both"
                                    Height="300px">
                                    <asp:GridView ID="grdDatos" runat="server" Width="100%" AutoGenerateColumns="False"
                                        AllowPaging="True" AllowSorting="True" EmptyDataText="No existen datos registrados en ésta tabla"
                                        OnRowCommand="grdDatos_RowCommand" DataKeyNames="PRO_ID_CASO_PROCESO,PRO_ACTIVO, ID_TIPO_ENTIDAD"
                                        OnPageIndexChanging="grdDatos_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="Name" HeaderText="Caso de Proceso"></asp:BoundField>
                                            <asp:BoundField DataField="PRO_CLAVE_PROCESO" HeaderText="Prefijo Caso de Proceso"></asp:BoundField>
                                            <asp:BoundField DataField="PRO_FECHA_REGISTRO" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ESTADO" HeaderText="Estado"></asp:BoundField>
                                            <asp:BoundField DataField="PRO_TIPO_ENTIDAD" HeaderText="Tipo Entidad"></asp:BoundField>
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
                                </asp:Panel>
                            </asp:Panel> <asp:Panel id="pnlEditar" runat="server" Width="100%" Visible="false"><TABLE width="100%"><TBODY><TR><TD style="WIDTH: 98px" align=left><asp:Label id="lblNombreCaso" runat="server" Text="Nombre Caso"></asp:Label></TD><TD style="WIDTH: 166px" align=left><asp:DropDownList id="cboNombreCaso" runat="server">
                                                </asp:DropDownList></TD></TR><TR><TD style="WIDTH: 98px" align=left><asp:Label id="lblNombre" runat="server" Text="Clave Proceso"></asp:Label></TD><TD style="WIDTH: 166px" align=left><asp:TextBox id="txtNombre" runat="server" MaxLength="30"></asp:TextBox></TD></TR><TR><TD style="WIDTH: 98px" align=left><asp:Label id="lblFecha" runat="server" Text="Fecha  (dd/mm/aaaa)"></asp:Label> </TD><TD style="WIDTH: 166px" align=left><asp:TextBox id="txtFecha" runat="server" MaxLength="10"></asp:TextBox>&nbsp;<asp:RegularExpressionValidator id="revFecha" runat="server" ErrorMessage="Formato de fecha no valido" ControlToValidate="txtFecha" ValidationExpression="^\d{2}\/\d{2}\/\d{4}">*</asp:RegularExpressionValidator> <cc1:CalendarExtender id="calFecha" runat="server" TargetControlID="txtFecha" Format="dd/MM/yyyy">
                                                </cc1:CalendarExtender> </TD></TR><TR><TD style="WIDTH: 98px"><asp:Label id="lblEstado" runat="server" Text="Estado"></asp:Label> </TD><TD style="WIDTH: 166px"><asp:CheckBox id="chkEstado" runat="server"></asp:CheckBox> <asp:Label id="lblId" runat="server" Visible="False"></asp:Label></TD></TR><TR><TD style="WIDTH: 98px"><asp:Label id="lblTipoEntidad" runat="server" Text="Tramite Autoridad Ambiental"></asp:Label> </TD><TD style="WIDTH: 166px"><asp:CheckBox id="chkTipoEntidad" runat="server"></asp:CheckBox> </TD></TR><TR><TD align=right colSpan=4><asp:Button id="btnActualizar" onclick="btnActualizar_Click" runat="server" SkinID="boton_copia" Text="Aceptar"></asp:Button> <asp:Button id="btnCancelar" onclick="btnCancelar_Click" runat="server" SkinID="boton_copia" Text="Cancelar" CausesValidation="False"></asp:Button> </TD></TR></TBODY></TABLE></asp:Panel> <asp:Panel id="pnlNuevo" runat="server" Visible="false"><TABLE><TBODY><TR><TD style="WIDTH: 48px" align=left><asp:Label id="lblNombreCaso_Nvo" runat="server" Text="Nombre Caso"></asp:Label></TD><TD style="WIDTH: 25%" align=left><asp:DropDownList id="cboNombreCaso_Nvo" runat="server">
                                                </asp:DropDownList></TD></TR><TR><TD style="WIDTH: 48px" align=left><asp:Label id="lblNombreNvo" runat="server" Text="Clave Proceso"></asp:Label></TD><TD style="WIDTH: 25%" align=left><asp:TextBox id="txtNombreNvo" runat="server" MaxLength="30"></asp:TextBox></TD></TR><TR><TD style="WIDTH: 48px" align=left><asp:Label id="lblFechaNvo" runat="server" Text="Fecha (dd/mm/aaaa)"></asp:Label> </TD><TD style="WIDTH: 25%" align=left><asp:TextBox id="txtFechaNvo" runat="server" MaxLength="10"></asp:TextBox> <asp:RegularExpressionValidator id="revFechaNvo" runat="server" ErrorMessage="Formato de fecha no valido" ControlToValidate="txtFechaNvo" ValidationExpression="^\d{2}\/\d{2}\/\d{4}">*</asp:RegularExpressionValidator> <cc1:CalendarExtender id="calFechaNvo" runat="server" TargetControlID="txtFechaNvo" Format="dd/MM/yyyy">
                                                </cc1:CalendarExtender> </TD></TR><TR><TD style="WIDTH: 48px"><asp:Label id="lblEstadoNvo" runat="server" Text="Estado"></asp:Label> </TD><TD><asp:CheckBox id="chkEstadoNvo" runat="server"></asp:CheckBox> </TD></TR><TR><TD style="WIDTH: 98px"><asp:Label id="lblTipoEntidad_Nvo" runat="server" Text="Tramite Autoridad Ambiental"></asp:Label> </TD><TD style="WIDTH: 166px"><asp:CheckBox id="chkTipoEntidad_Nvo" runat="server"></asp:CheckBox> </TD></TR><TR><TD align=right colSpan=4>&nbsp;<asp:Button id="btnRegistrar" onclick="btnRegistrar_Click" runat="server" SkinID="boton_copia" Text="Aceptar"></asp:Button> <asp:Button id="btnCancelarReg" onclick="btnCancelarReg_Click" runat="server" SkinID="boton_copia" Text="Cancelar" CausesValidation="False"></asp:Button> </TD></TR></TBODY></TABLE></asp:Panel> <asp:Label id="lblMensajeError" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>&nbsp;&nbsp; <asp:ValidationSummary id="valResumen" runat="server">
                                                </asp:ValidationSummary>&nbsp; <BR />
</contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                        </triggers>
                    </asp:UpdatePanel>
                    &nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
