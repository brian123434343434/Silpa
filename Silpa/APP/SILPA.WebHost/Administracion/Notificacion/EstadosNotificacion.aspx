<%@ Page Language="C#"  MasterPageFile="~/plantillas/SILPASinMenuFlash.master" AutoEventWireup="true" CodeFile="EstadosNotificacion.aspx.cs" Inherits="Administracion_Notificacion_EstadosNotificacion" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <div class='burbujaAyuda'></div>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">    
    <link href="../../App_Themes/skin/AyudaStyle.css" rel="stylesheet" />
    <script src="../../jquery/jquery.js" type="text/javascript"></script>
    <script src="../../js/Ayuda.js" type="text/javascript"></script>

    <asp:ScriptManager ID="scmEstado" runat="server"></asp:ScriptManager>

    <div class="div-titulo">
        <a href="#" id="hrfCerrarVentana" runat="server" onclick="window.close();return false;" class="Salir">Salir [X]</a>
        <br />
        <asp:Label ID="lblTitulo" runat="server" Text="ESTADOS NOTIFICACIÓN" SkinID="titulo_principal_blanco"></asp:Label>                
    </div>
    <div class="contact_form" id="divConsultaEstado" runat="server">
        <div class="TableBuscarNot">
            <div class="RowBuscarTitulo">
                <div class="CellBuscarTitulo">
                    <asp:Literal ID="ltlTituloBuscador" runat="server" Text="BUSCAR ESTADO"></asp:Literal>                    
                </div>
            </div>            
            <div class="RowBuscarNot">
                <div class="CellBuscarNot">
                    <div class="TableBuscarInternoNot">                        
                        <div class="RowBuscarNot">
                            <div class="CellBuscarNot">
                                <label for="cboMarcaVehiculo">Nombre Estado:</label>
                                <asp:TextBox runat="server" ID="txtEstadoBuscar" ClientIDMode="Static" MaxLength="50"></asp:TextBox>
                            </div>
                            <div class="CellBuscarNot">
                                <label for="cboMarcaVehiculo">Descripción Estado:</label>
                                <asp:TextBox runat="server" ID="txtDescripcionBuscar" ClientIDMode="Static" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="RowButton">
                <div class="CellButton">
                    <asp:HiddenField runat="server" ID="hdfNombreBuscado" />
                    <asp:HiddenField runat="server" ID="hdfDescripcionBuscado" />                    
                    <asp:Button runat="server" ID="cmdBuscar" Text="Buscar Estado" ClientIDMode="Static" OnClick="cmdBuscar_Click" />
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
                    <div class="RowButton" id="rowNuevoEstado" runat="server">
                        <div class="CellButton">
                            <asp:Button runat="server" ID="cmdNuevoEstado" Text="Adicionar Estado" ClientIDMode="Static" CausesValidation="False"/>
                        </div>
                    </div> 
                    <div class="CellGridView">
                        <asp:GridView runat="server" AutoGenerateColumns="False" ID="grdEstados" SkinID="GrillaSeguridad" DataKeyNames="ID" AllowPaging="True"
                            EmptyDataText="No existen estados de notificación" ShowHeaderWhenEmpty="True" OnRowDataBound="grdEstados_RowDataBound"
                            OnRowEditing="grdEstados_RowEditing" OnRowUpdating="grdEstados_RowUpdating" OnRowCancelingEdit="grdEstados_RowCancelingEdit" 
                            PageSize="10" OnPageIndexChanging="grdEstados_PageIndexChanging" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText = "NOMBRE">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("Estado") %>'></asp:Label>                                        
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="txtNombre" ClientIDMode="Static" MaxLength="50" Text='<%# Eval("Estado") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvNombre" ControlToValidate="txtNombre" ValidationGroup="ErroresEditar" ErrorMessage="Debe ingresar el nombre del estado">*</asp:RequiredFieldValidator>
                                    </EditItemTemplate>                                
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText = "DESCRIPCIÓN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("Descripcion") %>'></asp:Label>                                        
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="txtDescripcion" ClientIDMode="Static" MaxLength="50" Text='<%# Eval("Descripcion") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvDescripcion" ControlToValidate="txtDescripcion" ValidationGroup="ErroresEditar" ErrorMessage="Debe ingresar la descripción del estado">*</asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText = "ESTADO PDI">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstadoPDI" runat="server" Text='<%# (Convert.ToBoolean(Eval("EstadoPDI")) ? "SI" : "NO" ) %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList runat="server" ID="cboEstadoPDI">
                                            <asp:ListItem Value="0" Text="NO"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="SI"></asp:ListItem>
                                        </asp:DropDownList>                                        
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText = "ESTADO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstado" runat="server" Text='<%# (Convert.ToBoolean(Eval("Activo")) ? "ACTIVO" : "INACTIVO" ) %>'></asp:Label>
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
    <cc1:ModalPopupExtender ID="mpeCrearEstado" runat="server" PopupControlID="dvCrearEstado" TargetControlID="cmdNuevoEstado" BehaviorID="mpeCrearEstado" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <div id="dvCrearEstado" style="display:none;" runat="server" clientidmode="Static" class="contact_form_resultado">        
        <div class="TableResultadoModal">
            <div class="RowResultadoTituloModal">
                <div class="CellResultadoTituloModal">
                    NUEVO ESTADO
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
                        <div class="RowBuscarNot">
                            <div class="CellBuscarNot">
                                <div class="TableBuscarInternoNot">
                                    <div class="RowBuscarNot">
                                        <div class="CellFormularioNot">
                                            <label for="txtNombre">Nombre del Estado:</label>
                                            <span id="spnNombre" class="botonAyudaUP" title="Nombre del estado." divModal="dvCrearEstado"></span>                                     
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:TextBox runat="server" ID="txtNombre" ClientIDMode="Static" MaxLength="50" Text='<%# Eval("Description") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvNombre" ControlToValidate="txtNombre" ValidationGroup="EstadoModal" ErrorMessage="Debe ingresar el nombre del estado">*</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="RowBuscarNot">
                                        <div class="CellFormularioNot">
                                            <label for="txtDescripcion">Descripción:</label>
                                            <span id="spnDescripcion" class="botonAyudaUP" title="Descripción del estado. Esta descripción es la que aparecerá en los desplegables e información de VITAL." divModal="dvCrearEstado"></span>                                     
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:TextBox runat="server" ID="txtDescripcion" ClientIDMode="Static" MaxLength="50" Text='<%# Eval("Description") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvDescripcion" ControlToValidate="txtDescripcion" ValidationGroup="EstadoModal" ErrorMessage="Debe ingresar la descripción del estado">*</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="RowBuscarNot">
                                        <div class="CellFormularioNot">
                                            <label for="cboEstadoPDI">Estado PDI:</label>
                                            <span id="cboEstadoPDI" class="botonAyudaUP" title="Indica si es un estado PDI." divModal="dvCrearEstado"></span>                                     
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:DropDownList runat="server" ID="cboEstadoPDI">
                                                <asp:ListItem Value="-1" Text="Seleccione..."></asp:ListItem>
                                                <asp:ListItem Value="0" Text="NO"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="SI"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvEstadoPDI" ControlToValidate="cboEstadoPDI" ValidationGroup="EstadoModal" ErrorMessage="Debe indicar si es un estado PDI" InitialValue="-1">*</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                         </div>
                         <div class="RowBuscarNot">
                            <div class="CellBuscarNot">
                                <asp:ValidationSummary ID="valEstadoModal" runat="server" ValidationGroup="EstadoModal" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="RowButtonModal">
                <div class="CellButtonModal">
                    <asp:Button ID="cmdGuardarEstado" runat="server" Text="Adicionar" CssClass="boton" OnClick="cmdGuardarEstado_Click" ValidationGroup="EstadoModal"/>
                    <asp:Button ID="cmdCancelar" runat="server" Text="Cancelar" CssClass="boton" OnClick="cmdCancelar_Click" CausesValidation="false"/>
                </div>
            </div>
        </div>                        
    </div>
        
</asp:Content>
