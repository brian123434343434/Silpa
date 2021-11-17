<%@ Page Language="C#"  MasterPageFile="~/plantillas/SILPASinMenuFlash.master" AutoEventWireup="true" CodeFile="FlujoNotificacion.aspx.cs" Inherits="Administracion_Notificacion_FlujoNotificacion" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">    
    <asp:ScriptManager ID="scmFlujo" runat="server"></asp:ScriptManager>
    
    <div class="div-titulo">
        <a href="#" id="hrfCerrarVentana" runat="server" onclick="window.close();return false;" class="Salir">Salir [X]</a>
        <br />
        <asp:Label ID="lblTitulo" runat="server" Text="FLUJOS NOTIFICACIÓN" SkinID="titulo_principal_blanco"></asp:Label>                
    </div>
    <div class="contact_form" id="divConsultaFlujo" runat="server">
        <div class="TableBuscar">
            <div class="RowBuscarTitulo">
                <div class="CellBuscarTitulo">
                    <asp:Literal ID="ltlTituloBuscador" runat="server" Text="BUSCAR FLUJO"></asp:Literal>                    
                </div>
            </div>
            <div class="RowBuscarNot">
                <div class="CellBuscarNot">
                    <div class="TableBuscarInternoNot">
                        <div class="RowBuscarNot">
                            <div class="CellBuscarNot">
                                <label for="cboAutoridadAmbiental">Autoridad Ambiental:</label>
                                <asp:DropDownList runat="server" ID="cboAutoridadAmbientalBuscar"></asp:DropDownList>
                            </div>
                            <div class="CellBuscarNot">
                                <label for="cboMarcaVehiculo">Nombre Flujo:</label>
                                <asp:TextBox runat="server" ID="txtFlujoBuscar" ClientIDMode="Static" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="RowButton">
                <div class="CellButton">
                    <asp:HiddenField runat="server" ID="hdfNombreBuscado" />
                    <asp:HiddenField runat="server" ID="hdfAutoridadIDBuscado" />
                    <asp:Button runat="server" ID="cmdBuscar" Text="Buscar Flujo" ClientIDMode="Static" OnClick="cmdBuscar_Click" />
                </div>
            </div>
        </div>
    </div>
    <br /><br /><br />
    <div class="contact_form" id="divMensaje" runat="server" visible="false">  
        <div class="Table">
            <div class="Row">
                <div class="CellMensaje">
                    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
                </div>
            </div>
        </div>
    </div>
        
    <div class="contact_form" runat="server">
        <div class="TableResultadoBuscar">
            <div class="Row">
                <div class="Table">                    
                    <div class="RowButton" id="rowNuevoFlujo" runat="server">
                        <div class="CellButton">
                            <asp:Button runat="server" ID="cmdNuevoFlujo" Text="Adicionar Flujo" ClientIDMode="Static" CausesValidation="False"/>
                        </div>
                    </div> 
                    <div class="CellGridView">
                        <asp:GridView runat="server" AutoGenerateColumns="False" ID="grdFlujos" SkinID="GrillaSeguridad" DataKeyNames="ID_FLUJO_NOT_ELEC" AllowPaging="True"
                            EmptyDataText="No existen Flujos de notificación" ShowHeaderWhenEmpty="True" OnRowDataBound="grdFlujos_RowDataBound"
                            OnRowEditing="grdFlujos_RowEditing" OnRowUpdating="grdFlujos_RowUpdating" OnRowCancelingEdit="grdFlujos_RowCancelingEdit" 
                            PageSize="10" OnPageIndexChanging="grdFlujos_PageIndexChanging" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText = "AUTORIDAD">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAutoridad" runat="server" Text='<%# Eval("AUTORIDAD") %>'></asp:Label>                                        
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList runat="server" ID="cboAutoridad"></asp:DropDownList>                                        
                                    </EditItemTemplate>                                
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText = "NOMBRE">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("FLUJO_NOT_ELEC_DESC") %>'></asp:Label>                                        
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="txtNombre" ClientIDMode="Static" MaxLength="50" Text='<%# Eval("FLUJO_NOT_ELEC_DESC") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvNombre" ControlToValidate="txtNombre" ValidationGroup="ErroresEditar" ErrorMessage="Debe ingresar el nombre del flujo">*</asp:RequiredFieldValidator>
                                    </EditItemTemplate>                                
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText = "ESTADO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFlujo" runat="server" Text='<%# (Convert.ToBoolean(Eval("ACTIVO")) ? "ACTIVO" : "INACTIVO" ) %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList runat="server" ID="cboEstado">
                                            <asp:ListItem Value="0" Text="INACTIVO"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="ACTIVO"></asp:ListItem>
                                        </asp:DropDownList>                                        
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField EditText="Editar" ShowEditButton="true" UpdateText="Actualizar" ShowCancelButton="true" CancelText="Cancelar" ValidationGroup="ErroresEditar" />
                            </Columns>
                        </asp:GridView>                                                                                                                                             
                    </div>
                </div>
            </div>
        </div>
        <div class="Row">
            <div class="Cell">
                <asp:ValidationSummary ID="vsErroresEditar" runat="server" ValidationGroup="ErroresEditar" DisplayMode="BulletList" /> 
            </div>
        </div>
    </div>        
    <cc1:ModalPopupExtender ID="mpeCrearFlujo" runat="server" PopupControlID="dvCrearFlujo" TargetControlID="cmdNuevoFlujo" BehaviorID="mpeCrearFlujo" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <div id="dvCrearFlujo" style="display:none;" runat="server" clientidmode="Static" class="contact_form_resultado">
        <div class="TableResultadoModal">
            <div class="RowResultadoTituloModal">
                <div class="CellResultadoTituloModal">
                    NUEVO FLUJO
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
                                <label for="txtNumeroDocumentoFlujo">Autoridad:</label>
                                <asp:DropDownList runat="server" ID="cboAutoridad"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="RowBuscar">
                            <div class="CellBuscar">
                                <label for="txtNumeroDocumentoFlujo">Nombre del Flujo:</label>
                                <asp:TextBox runat="server" ID="txtNombre" ClientIDMode="Static" MaxLength="50" Text='<%# Eval("Description") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvNombre" ControlToValidate="txtNombre" ValidationGroup="FlujoModal" ErrorMessage="Debe ingresar el nombre del Flujo">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="RowBuscar">
                            <div class="CellBuscar">
                                <asp:ValidationSummary ID="valFlujoModal" runat="server" ValidationGroup="FlujoModal" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="RowButtonModal">
                <div class="CellButtonModal">
                    <asp:Button ID="cmdGuardarFlujo" runat="server" Text="Adicionar" CssClass="boton" OnClick="cmdGuardarFlujo_Click" ValidationGroup="FlujoModal"/>
                    <asp:Button ID="cmdCancelar" runat="server" Text="Cancelar" CssClass="boton" OnClick="cmdCancelar_Click" CausesValidation="false"/>
                </div>
            </div>
        </div>                        
    </div>
        
</asp:Content>
