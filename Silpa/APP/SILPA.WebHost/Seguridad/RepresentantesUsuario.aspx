<%@ Page Language="C#"  MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="RepresentantesUsuario.aspx.cs" Inherits="Seguridad_RepresentantesUsuario" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">    
    <asp:ScriptManager ID="scmRepresentante" runat="server"></asp:ScriptManager>
    
    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="REPRESENTANTES USUARIO" SkinID="titulo_principal_blanco"></asp:Label>&nbsp;             
        <a href="#" id="hrfCerrarVentana" runat="server" onclick="window.close();return false;" class="Salir">Salir [X]</a>       
    </div>
    <div class="contact_form" id="divConsultaUsuario" runat="server">
        <table class="TableBuscarSeguridad">
            <tr>
                <td colspan="2" class="TituloBuscarSeguridad">
                    <asp:Literal ID="ltlTituloBuscador" runat="server" Text="BUSCAR USUARIO"></asp:Literal>                    
                </td>
            </tr>
            <tr>
                <td class="LabelBuscarSeguridad">
                    Número de Documento:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtNumeroDocumento" ClientIDMode="Static"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvNumeroDocumento" ControlToValidate="txtNumeroDocumento" ValidationGroup="UsuarioBuscar" ErrorMessage="Debe ingresar el número de identificación">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="rowNombreUsuario" runat="server" visible="false">
                <td class="LabelBuscarSeguridad">
                    Nombre de Usuario:
                </td>
                <td class="CamposBuscarSeguridad">
                    <asp:literal ID="ltlNombreUsuario" runat="server"></asp:literal>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="BotonesSeguridad">
                    <asp:HiddenField runat="server" ID="hdfIdPersona" />
                    <asp:Button runat="server" ID="cmdBuscar" ValidationGroup="UsuarioBuscar" Text="Buscar Usuario" ClientIDMode="Static" OnClick="cmdBuscar_Click" />
                    <asp:ValidationSummary ID="valUsuarioBuscar" runat="server" ValidationGroup="UsuarioBuscar" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                </td>
            </tr>
        </table>
    </div>    
    <div class="contact_form" id="divMensaje" runat="server" visible="false">  
        <div class="Table">
            <div class="Row">
                <div class="CellMensaje">
                    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
                </div>
            </div>
        </div>
    </div>        
    <div class="contact_form" id="divUsuario" runat="server" visible="false"> 
        <table class="TableResultadoBuscarSeguridad">
            <tr>
                <td  class="BotonesSeguridad">
                    <asp:Button runat="server" ID="cmdNuevoRepresentante" Text="Adicionar Representante" ClientIDMode="Static" CausesValidation="False"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView runat="server" AutoGenerateColumns="False" ID="grdRepresentantes" SkinID="GrillaSeguridad" DataKeyNames="ID" AllowPaging="True"
                            EmptyDataText="No se han encontrado representantes asociados" ShowHeaderWhenEmpty="True" OnRowDataBound="grdRepresentantes_RowDataBound"
                            OnRowEditing="grdRepresentantes_RowEditing" OnRowUpdating="grdRepresentantes_RowUpdating" OnRowCancelingEdit="grdRepresentantes_RowCancelingEdit" OnRowDeleting="grdRepresentantes_RowDeleting" 
                            PageSize="10" OnPageIndexChanging="grdRepresentantes_PageIndexChanging" Width="100%">
                        <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" ForeColor="#222222" BorderWidth="1px" BorderColor="White"/>    
                            <Columns>
                                <asp:TemplateField HeaderText = "IDENTIFICACIÓN" ItemStyle-CssClass="GrillaTextoTablaSeguridad">
                                    <ItemTemplate>
                                        <asp:Literal ID="lblIdentificacion" runat="server" Text='<%# Eval("RepresentativeIdentification") %>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Literal ID="lblIdentificacion" runat="server" Text='<%# Eval("RepresentativeIdentification") %>'></asp:Literal>
                                    </EditItemTemplate>                                
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText = "NOMBRE" ItemStyle-CssClass="GrillaTextoTablaSeguridad">
                                    <ItemTemplate>
                                        <asp:Literal ID="lblNombre" runat="server" Text='<%# Eval("RepresentativeName") %>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Literal ID="lblNombre" runat="server" Text='<%# Eval("RepresentativeName") %>'></asp:Literal>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText = "DESCRIPCIÓN" ItemStyle-CssClass="GrillaTextoTablaSeguridad">
                                    <ItemTemplate>
                                        <asp:Literal ID="lblDescripcion" runat="server" Text='<%# Eval("Description") %>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="txtDescripcionEditar" MaxLength="30" Text='<%# Eval("Description") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvDescripcionEditar" ControlToValidate="txtDescripcionEditar" ValidationGroup="ErroresEditar" ErrorMessage="Debe ingresar la descripción del representante">*</asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField EditText="Editar" ShowEditButton="true" UpdateText="Actualizar" ShowCancelButton="true" CancelText="Cancelar" ValidationGroup="ErroresEditar" DeleteText="Eliminar" ShowDeleteButton="true" ItemStyle-CssClass="GrillaTextoTablaSeguridad" />
                            </Columns>
                        </asp:GridView>  
                </td>
            </tr>
        </table>
        <div class="Row">
            <div class="Cell">
                <asp:ValidationSummary ID="vsErroresEditar" runat="server" ValidationGroup="ErroresEditar" DisplayMode="BulletList" /> 
            </div>
        </div>
    </div>        
    <cc1:ModalPopupExtender ID="mpeCrearRepresentante" runat="server" PopupControlID="dvCrearRepresentante" TargetControlID="cmdNuevoRepresentante" BehaviorID="mpeCrearRepresentante" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <div id="dvCrearRepresentante" style="display:none;" runat="server" clientidmode="Static" class="contact_form_resultado">
        <div class="TableResultadoModal">
            <div class="RowResultadoTituloModal">
                <div class="CellResultadoTituloModal">
                    NUEVO REPRESENTANTE
                </div>
            </div>
            <div class="RowResultadoModal">
                <div class="CellResultadoModal">  
                    <div class="contact_form" id="divMensajeModal" runat="server" visible="false">  
                        <div class="Table">
                            <div class="Row">
                                <div class="CellMensaje">
                                    <asp:Label runat="server" ID="lblMensajeModal"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>                      
                    <div class="TableBuscar">
                        <div class="RowBuscar">
                            <div class="CellBuscar">
                                <label for="txtNumeroDocumentoRepresentante">Número de Documento:</label>
                                <asp:TextBox runat="server" ID="txtNumeroDocumentoRepresentante" Text=""></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvNumeroDocumentoRepresentante" ControlToValidate="txtNumeroDocumentoRepresentante" ValidationGroup="RepresentanteModal" ErrorMessage="Debe ingresar el número de identificación del representante que se va adicionar">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="RowBuscar">
                            <div class="CellBuscar">
                                <label for="txtDescripcion">Descripción:</label>
                                <asp:TextBox runat="server" ID="txtDescripcion" MaxLength="30" Text=""></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvDescripcion" ControlToValidate="txtDescripcion" ValidationGroup="RepresentanteModal" ErrorMessage="Debe ingresar la descripción del representante que se va adicionar">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="RowBuscar">
                            <div class="CellBuscar">
                                <asp:ValidationSummary ID="valRepresentanteModal" runat="server" ValidationGroup="RepresentanteModal" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="RowButtonModal">
                <div class="CellButtonModal">
                    <asp:Button ID="cmdGuardarRepresentante" runat="server" Text="Adicionar" CssClass="boton" OnClick="cmdGuardarRepresentante_Click" ValidationGroup="RepresentanteModal"/>
                    <asp:Button ID="cmdCancelar" runat="server" Text="Cancelar" CssClass="boton" OnClick="cmdCancelar_Click" CausesValidation="false"/>
                </div>
            </div>
        </div>                        
    </div>
        
</asp:Content>
