<%@ Page Language="C#"  MasterPageFile="~/plantillas/SILPASinMenuFlash.master" AutoEventWireup="true" CodeFile="FormatoPlantillaNotificacion.aspx.cs" Inherits="Administracion_Notificacion_FormatoPlantillaNotificacion" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <div class='burbujaAyuda'></div>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">   
    <link href="../../App_Themes/skin/AyudaStyle.css" rel="stylesheet" />
    <script src="../../jquery/jquery.js" type="text/javascript"></script>
    <script src="../../js/Ayuda.js" type="text/javascript"></script>
     
    <asp:ScriptManager ID="scmFormato" runat="server"></asp:ScriptManager>

    <div class="div-titulo">
        <a href="#" id="hrfCerrarVentana" runat="server" onclick="window.close();return false;" class="Salir">Salir [X]</a>
        <br />
        <asp:Label ID="lblTitulo" runat="server" Text="FORMATOS PLANTILLA NOTIFICACIÓN" SkinID="titulo_principal_blanco"></asp:Label>                
    </div>
    <div class="contact_form" id="divConsultaFormato" runat="server">
        <div class="TableBuscarNot">
            <div class="RowBuscarTitulo">                
                <div class="CellBuscarTitulo">
                    <asp:Literal ID="ltlTituloBuscador" runat="server" Text="BUSCAR FORMATO"></asp:Literal>                    
                </div>
            </div>
            <div class="RowBuscarNot">
                <div class="CellBuscarNot">
                    <label for="txtNombreBuscar">Nombre Formato:</label>
                    <asp:TextBox runat="server" ID="txtNombreBuscar" ClientIDMode="Static" MaxLength="50"></asp:TextBox>
                </div>
            </div>
            <div class="RowButton">
                <div class="CellButton">
                    <asp:HiddenField runat="server" ID="hdfNombreBuscado" />
                    <asp:Button runat="server" ID="cmdBuscar" Text="Buscar Formato" ClientIDMode="Static" OnClick="cmdBuscar_Click" />
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
                <div class="TableResultadoBuscar">
                    <div class="RowButton" id="rowNuevoFormato" runat="server">
                        <div class="CellButton">
                            <asp:Button runat="server" ID="cmdNuevoFormato" Text="Adicionar Formato" ClientIDMode="Static" CausesValidation="False"/>
                        </div>
                    </div> 
                    <div class="Row">
                        <div class="CellGridView">
                            <asp:GridView runat="server" AutoGenerateColumns="False" ID="grdFormatos" SkinID="GrillaSeguridad" DataKeyNames="FormatoID" AllowPaging="True"
                                EmptyDataText="No existen formatos de notificación" ShowHeaderWhenEmpty="True" OnRowDataBound="grdFormatos_RowDataBound"
                                OnRowEditing="grdFormatos_RowEditing" OnRowUpdating="grdFormatos_RowUpdating" OnRowCancelingEdit="grdFormatos_RowCancelingEdit" 
                                PageSize="10" OnPageIndexChanging="grdFormatos_PageIndexChanging" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText = "NOMBRE">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("Nombre") %>'></asp:Label>                                        
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="txtNombre" ClientIDMode="Static" MaxLength="50" Text='<%# Eval("Nombre") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvNombre" ControlToValidate="txtNombre" ValidationGroup="ErroresEditar" ErrorMessage="Debe ingresar el nombre del formato">*</asp:RequiredFieldValidator>
                                        </EditItemTemplate>                                
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "FORMATO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFormato" runat="server" Text='<%# Eval("Formato") %>'></asp:Label>                                        
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="txtFormato" ClientIDMode="Static" MaxLength="50" Text='<%# Eval("Formato") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvFormato" ControlToValidate="txtFormato" ValidationGroup="ErroresEditar" ErrorMessage="Debe ingresar el formato">*</asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                    </asp:TemplateField>                                
                                    <asp:TemplateField HeaderText = "ESTADO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActivo" runat="server" Text='<%# (Convert.ToBoolean(Eval("Activo")) ? "ACTIVO" : "INACTIVO" ) %>'></asp:Label>
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
                    <div class="Row">
                        <div class="Cell">
                            <asp:ValidationSummary ID="vsErroresEditar" runat="server" ValidationGroup="ErroresEditar" DisplayMode="BulletList" /> 
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    
    <cc1:ModalPopupExtender ID="mpeCrearFormato" runat="server" PopupControlID="dvCrearFormato" TargetControlID="cmdNuevoFormato" BehaviorID="mpeCrearFormato" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <div id="dvCrearFormato" style="display:none;" runat="server" clientidmode="Static" class="contact_form_resultado">        
        <div class="TableResultadoModal">
            <div class="RowResultadoTituloModal">
                <div class="CellResultadoTituloModal">
                    NUEVO FORMATO
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
                                    <div class="RowFormularioNot">
                                        <div class="CellFormularioNot">
                                            <label for="txtNombre">Nombre del Formato:</label>
                                            <span id="spnNombre" class="botonAyudaUP" title="Nombre del formato" divModal="dvCrearFormato"></span>
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:TextBox runat="server" ID="txtNombre" ClientIDMode="Static" MaxLength="50" Text=''></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvNombre" ControlToValidate="txtNombre" ValidationGroup="FormatoModal" ErrorMessage="Debe ingresar el nombre del formato">*</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="RowFormularioNot">
                                        <div class="CellFormularioNot">
                                            <label for="txtFormato">Formato:</label>
                                            <span id="spnFormato" class="botonAyudaUP" title="Formato que generá el documento" divModal="dvCrearFormato"></span>
                                        </div>
                                        <div class="CellFormularioCamposNot">
                                            <asp:TextBox runat="server" ID="txtFormato" ClientIDMode="Static" MaxLength="50" Text=''></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvFormato" ControlToValidate="txtFormato" ValidationGroup="FormatoModal" ErrorMessage="Debe ingresar el formato">*</asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                </div>
                            </div>
                         </div>
                         <div class="RowBuscar">
                            <div class="CellBuscar">
                                <asp:ValidationSummary ID="valFormatoModal" runat="server" ValidationGroup="FormatoModal" ShowMessageBox="true" ShowSummary="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="RowButtonModal">
                <div class="CellButtonModal">
                    <asp:Button ID="cmdGuardarFormato" runat="server" Text="Adicionar" CssClass="boton" OnClick="cmdGuardarFormato_Click" ValidationGroup="FormatoModal"/>
                    <asp:Button ID="cmdCancelar" runat="server" Text="Cancelar" CssClass="boton" OnClick="cmdCancelar_Click" CausesValidation="false"/>
                </div>
            </div>
        </div>                        
    </div>
        
</asp:Content>
