<%@ Page Language="C#"  MasterPageFile="~/Plantillas/SILPASinMenuFlash.master" AutoEventWireup="true" CodeFile="FirmaAutoridadNotificacion.aspx.cs" Inherits="Administracion_Notificacion_FirmaAutoridadNotificacion" ValidateRequest="false"  %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <div class='burbujaAyuda'></div>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">    
    <link href="../../App_Themes/skin/AyudaStyle.css" rel="stylesheet" />
    <script src="../../jquery/jquery.js" type="text/javascript"></script>
    <script src="../../js/Ayuda.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $('#fluFirma').change(function () {
                $('#txtFirma').val($(this).val());
            });

            $('#btnFirma').click(function () {
                $('#fluFirma').click();
            });

            $('#txtFirma').click(function () {
                $('#fluFirma').click();
            });
        });
    </script>

    <asp:ScriptManager ID="scmFirma" runat="server"></asp:ScriptManager>

    <div class="div-titulo">
        <a href="#" id="hrfCerrarVentana" runat="server" onclick="window.close();return false;" class="Salir">Salir [X]</a>
        <br />
        <asp:Label ID="lblTitulo" runat="server" Text="FIRMAS AUTORIZADAS AUTORIDADES AMBIENTALES - NOTIFICACIÓN" SkinID="titulo_principal_blanco"></asp:Label>                
    </div>
    <div class="contact_form" id="divConsultaFirma" runat="server">
        <div class="TableBuscarNot">
            <div class="RowBuscarTitulo">                
                <div class="CellBuscarTitulo">
                    <asp:Literal ID="ltlTituloBuscador" runat="server" Text="BUSCAR FIRMA"></asp:Literal>                    
                </div>
            </div>
            <div class="RowBuscarTitulo">
                <div class="CellBuscarNot">
                    <div class="TableBuscarInternoNot">
                        <div class="RowBuscarNot">
                            <div class="CellBuscarNot">
                                <label for="cboAutoridadAmbiental">Autoridad Ambiental:</label>
                                <asp:DropDownList runat="server" ID="cboAutoridadAmbientalBuscar"></asp:DropDownList>
                            </div>
                            <div class="CellBuscarNot">
                                <label for="txtNombreAutorizado">Nombre Autorizado:</label>
                                <asp:TextBox runat="server" ID="txtNombreAutorizadoBuscar" ClientIDMode="Static" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="RowButton">
                <div class="CellButton">
                    <asp:HiddenField runat="server" ID="hdfAutoridadAmbientalBuscado" Value="0" />
                    <asp:HiddenField runat="server" ID="hdfNombreAutorizadoBuscado" />
                    <asp:Button runat="server" ID="cmdBuscar" Text="Buscar Firma" ClientIDMode="Static" OnClick="cmdBuscar_Click" />
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
                    <div class="RowButton" id="rowNuevoFirma" runat="server">
                        <div class="CellButton">
                            <asp:Button runat="server" ID="cmdNuevaFirma" Text="Adicionar Firma" ClientIDMode="Static" CausesValidation="False" OnClick="cmdNuevaFirma_Click"/>
                        </div>
                    </div>                         
                    <div class="CellGridView">
                        <asp:GridView runat="server" AutoGenerateColumns="False" ID="grdFirmas" SkinID="GrillaSeguridad" AllowPaging="True"
                            EmptyDataText="No existen Firmas de notificación" ShowHeaderWhenEmpty="True" OnRowDataBound="grdFirmas_RowDataBound"                            
                            PageSize="10" OnPageIndexChanging="grdFirmas_PageIndexChanging" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText = "AUTORIDAD">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAutoridad" runat="server" Text='<%# Eval("Autoridad") %>'></asp:Label>                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText = "NOMBRE AUTORIZADO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("NombreAutorizado") %>'></asp:Label>                                        
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText = "ESTADO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActivo" runat="server" Text='<%# (Convert.ToBoolean(Eval("Activo")) ? "ACTIVO" : "INACTIVO" ) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText = "EDITAR">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEditar" runat="server" CommandArgument='<%#Eval("FirmaAutoridadID") %>' OnClick="lnkEditar_Click">Editar</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
    <input type="button" runat="server" id="cmdNuevaFirmaHide" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeCrearFirma" runat="server" PopupControlID="dvCrearFirma" TargetControlID="cmdNuevaFirmaHide" BehaviorID="mpeCrearFirma" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <div id="dvCrearFirma" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalFormularioNot">        
        <div class="TableResultadoModalNot">
            <div class="RowResultadoModalNot">
                <div class="CellResultadoTituloModalNot">
                    <asp:Literal runat="server" ID="ltlTituloCrear"></asp:Literal>
                </div>
            </div>
            <div class="RowResultadoModalNot">
                <div class="CellResultadoModalNot">
                    <div class="contact_form" id="divMensajeModal" runat="server" visible="false">  
                        <div class="Table">
                            <div class="Row">
                                <div class="CellMensaje">
                                    <asp:Label runat="server" ID="lblMensajeModal"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="RowResultadoModalNot">
                <div class="CellResultadoModalNot">
                    <div class="TableFormularioNot">
                        <div class="RowFormularioNot">
                            <div class="CellFormularioNot">
                                <label for="cboAutoridad">
                                    Autoridad Ambiental:
                                    <span id="spnAutoridad" class="botonAyudaUP" title="Autoridad ambiental a la cual se encuentra relacionada la firma." divModal="dvCrearFirma"></span>
                                </label>                                
                            </div>
                            <div class="CellFormularioCamposNot">
                                <asp:DropDownList runat="server" ID="cboAutoridad"></asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvAutoridad" ControlToValidate="cboAutoridad" InitialValue="-1" ValidationGroup="FirmaModal" ErrorMessage="Debe ingresar la autoridad ambiental">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="RowFormularioNot">
                            <div class="CellFormularioNot">
                                <label for="cboTipoIdentificacion">
                                    Tipo Identificación Autorizado:
                                    <span id="stpnTipoIdentificacion" class="botonAyudaUP" title="Tipo de Identificación de la persona autorizada por la autoridad ambiental y a la cual pertenece la firma." divModal="dvCrearFirma"></span>
                                </label>                                
                            </div>
                            <div class="CellFormularioCamposNot">                                                    
                                <asp:DropDownList runat="server" ID="cboTipoIdentificacion"></asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvTipoIdentificacion" ControlToValidate="cboTipoIdentificacion" ValidationGroup="FirmaModal" ErrorMessage="Debe ingresar el tipo de identificación de la persona autorizada para la firma" InitialValue="-1">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="RowFormularioNot">
                            <div class="CellFormularioNot">
                                <label for="txtNumeroIdentificacion">
                                    Número Identificación Autorizado:
                                    <span id="spnNumeroIdentificacion" class="botonAyudaUP" title="Número de identificación de la persona autorizada por la autoridad ambiental y a la cual pertenece la firma." divModal="dvCrearFirma"></span>
                                </label>                                
                            </div>
                            <div class="CellFormularioCamposNot">                                                    
                                <asp:TextBox runat="server" ID="txtNumeroIdentificacion" ClientIDMode="Static" MaxLength="25"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvNumeroIdentificacion" ControlToValidate="txtNumeroIdentificacion" ValidationGroup="FirmaModal" ErrorMessage="Debe ingresar el número de identificación de la persona autorizada para la firma">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="RowFormularioNot">
                            <div class="CellFormularioNot">
                                <label for="txtNombreAutorizado">
                                    Nombre Autorizado:
                                    <span id="spnNombreAutorizado" class="botonAyudaUP" title="Nombre de la persona autorizada por la autoridad ambiental y a la cual pertenece la firma." divModal="dvCrearFirma"></span>
                                </label>                                
                            </div>
                            <div class="CellFormularioCamposNot">                                                    
                                <asp:TextBox runat="server" ID="txtNombreAutorizado" ClientIDMode="Static" MaxLength="150"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvNombreAutorizado" ControlToValidate="txtNombreAutorizado" ValidationGroup="FirmaModal" ErrorMessage="Debe ingresar el nombre de la persona autorizada para la firma">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="RowFormularioNot">
                            <div class="CellFormularioNot">
                                <label for="txtCargoAutorizado">
                                    Cargo del Autorizado:
                                    <span id="spnCargoAutorizado" class="botonAyudaUP" title="Cargo de la persona autorizada por la autoridad ambiental y a la cual pertenece la firma." divModal="dvCrearFirma"></span>
                                </label>                                
                            </div>
                            <div class="CellFormularioCamposNot">
                                <asp:TextBox runat="server" ID="txtCargoAutorizado" ClientIDMode="Static" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvCargoAutorizado" ControlToValidate="txtCargoAutorizado" ValidationGroup="FirmaModal" ErrorMessage="Debe ingresar el cargo de la persona autorizada para la firma">*</asp:RequiredFieldValidator>
                            </div>
                        </div>                        

                        <div class="RowFormularioNot">
                            <div class="CellFormularioNot">
                                <label for="txtCargoAutorizado">
                                    Subdirección del Autorizado:
                                    <span id="spnSubdireccion" class="botonAyudaUP" title="Subdirección a la cual pertenece la persona autorizada por la autoridad ambiental y a la cual pertenece la firma." divModal="dvCrearFirma"></span>
                                </label>                                
                            </div>
                            <div class="CellFormularioCamposNot">
                                <asp:TextBox runat="server" ID="txtSubdireccion" ClientIDMode="Static" MaxLength="200"></asp:TextBox>
                            </div>
                        </div>
                        <div class="RowFormularioNot">
                            <div class="CellFormularioNot">
                                <label for="txtCargoAutorizado">
                                    Grupo del Autorizado:
                                    <span id="Span2" class="botonAyudaUP" title="Grupo al cual pertenece la persona autorizada por la autoridad ambiental y a la cual pertenece la firma." divModal="dvCrearFirma"></span>
                                </label>                                
                            </div>
                            <div class="CellFormularioCamposNot">
                                <asp:TextBox runat="server" ID="txtGrupo" ClientIDMode="Static" MaxLength="200"></asp:TextBox>
                            </div>
                        </div>
                        <div class="RowFormularioNot">
                            <div class="CellFormularioNot">
                                <label for="txtEmailAutorizado">
                                    Email Autorizado:
                                    <span id="spnEmailAutorizado" class="botonAyudaUP" title="Email autorizado por la autoridad ambiental y que se relacionará en el documento." divModal="dvCrearFirma"></span>
                                </label>                                
                            </div>
                            <div class="CellFormularioCamposNot">
                                <asp:TextBox runat="server" ID="txtEmailAutorizado" ClientIDMode="Static" MaxLength="150"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvEmailAutorizado" ControlToValidate="txtEmailAutorizado" ValidationGroup="FirmaModal" ErrorMessage="Debe ingresar el email de la persona autorizada para la firma">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rexEmailAutorizado" Display="Dynamic" runat="server" ControlToValidate="txtEmailAutorizado" ValidationGroup="FirmaModal" ErrorMessage="Ingrese un email válido" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$">*</asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="RowFormularioNot">
                            <div class="CellFormularioNot">
                                <label for="txtFirma">
                                    Firma:
                                    <span id="spnFirma" class="botonAyudaUP" title="Firma de la persona autorizada por la autoridad ambiental." divModal="dvCrearFirma"></span>
                                </label>                                
                            </div>
                            <div class="CellFormularioCamposNot">
                                <asp:Image runat="server" ID="imgFirma" Visible="false" />
                                <br runat="server" id="brImagen" visible="false" />
                                <input runat="server" type="text" value="" readonly="readonly" clientidmode="Static" id="txtFirma" class="textInput" />
                                <input type="button" runat="server" id="btnFirma" value="Seleccionar Archivo" clientidmode="Static" class="button" />
                                <asp:RequiredFieldValidator ID="rfvFirma" runat="server" ErrorMessage="Debe seleccionar la firma a utilizar" ControlToValidate="fluFirma" ValidationGroup="FirmaModal">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator id="rexFirma" runat="server" ErrorMessage="Solo se permite imagenes en formato .bmp, .gif, .jpg, jpeg y png" Display="Dynamic" ValidationExpression="(.*).(.bmp|.BMP|.jpeg|JPEG|.jpg|.JPG|.gif|.GIF|.png|.PNG)$" ControlToValidate="fluFirma" ValidationGroup="FirmaModal">*</asp:RegularExpressionValidator>
                                <asp:FileUpload ID="fluFirma" runat="server" ClientIDMode="Static" CssClass="InputFile" />
                            </div>
                        </div>
                        <div class="RowFormularioNot" runat="server" id="divEstado">
                            <div class="CellFormularioNot">
                                <label for="cboEstado">
                                    Estado:
                                    <span id="spnEstado" class="botonAyudaUP" title="Indica si la firma se encuentra Activa o Inactiva." divModal="dvCrearFirma"></span>
                                </label>                                
                            </div>
                            <div class="CellFormularioCamposNot">
                                <asp:DropDownList runat="server" ID="cboEstado">
                                    <asp:ListItem Text="ACTIVO" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="INACTIVO" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="RowResultadoModalNot">
                <div class="CellButtonModal">
                    <asp:ValidationSummary ID="valFirmaModal" runat="server" ValidationGroup="FirmaModal" ShowMessageBox="true" ShowSummary="false" />
                    <asp:HiddenField runat="server" ID="hdfFirmaAutoridadID" />
                    <asp:Button ID="cmdGuardarFirma" runat="server" Text="Adicionar" CssClass="boton" OnClick="cmdGuardarFirma_Click" ValidationGroup="FirmaModal"/>
                    <asp:Button ID="cmdCancelar" runat="server" Text="Cancelar" CssClass="boton" OnClick="cmdCancelar_Click" CausesValidation="false"/>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
