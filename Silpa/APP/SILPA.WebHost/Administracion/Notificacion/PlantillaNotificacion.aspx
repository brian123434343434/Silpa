<%@ Page Language="C#"  MasterPageFile="~/plantillas/SILPASinMenuFlash.master" AutoEventWireup="true" CodeFile="PlantillaNotificacion.aspx.cs" Inherits="Administracion_Notificacion_PlantillaNotificacion" ValidateRequest="false"  %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <div class='burbujaAyuda'></div>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">    
    <link href="../../App_Themes/skin/AyudaStyle.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../../jquery/HtmlEditor/jquery-te-1.4.0.css">
    <script src="../../jquery/jquery.js" type="text/javascript"></script>
    <script src="../../js/Ayuda.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../jquery/HtmlEditor/jquery.min.js" charset="utf-8"></script>
    <script language="javascript" type="text/javascript" src="../../jquery/HtmlEditor/jquery-te-1.4.0.min.js"></script>
    

    <asp:ScriptManager ID="scmPlantilla" runat="server"></asp:ScriptManager>

    <div class="div-titulo">
        <a href="#" id="hrfCerrarVentana" runat="server" onclick="window.close();return false;" class="Salir">Salir [X]</a>
        <br />
        <asp:Label ID="lblTitulo" runat="server" Text="PLANTILLA NOTIFICACIÓN" SkinID="titulo_principal_blanco"></asp:Label>                
    </div>
    <div class="contact_form" id="divConsultaPlantilla" runat="server">
        <div class="TableBuscarNot">
            <div class="RowBuscarTitulo">                
                <div class="CellBuscarTitulo">
                    <asp:Literal ID="ltlTituloBuscador" runat="server" Text="BUSCAR PLANTILLA"></asp:Literal>                    
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
                                <label for="txtNombreBuscar">Nombre Plantilla:</label>
                                <asp:TextBox runat="server" ID="txtNombreBuscar" ClientIDMode="Static" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="RowButton">
                <div class="CellButton">
                    <asp:HiddenField runat="server" ID="hdfAutoridadIDBuscado" />
                    <asp:HiddenField runat="server" ID="hdfNombreBuscado" />
                    <asp:Button runat="server" ID="cmdBuscar" Text="Buscar Plantilla" ClientIDMode="Static" OnClick="cmdBuscar_Click" />
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
                    <div class="RowButton" id="rowNuevoPlantilla" runat="server">
                        <div class="CellButton">
                            <asp:Button runat="server" ID="cmdNuevoPlantilla" Text="Adicionar Plantilla" ClientIDMode="Static" CausesValidation="False" OnClick="cmdNuevoPlantilla_Click"/>
                        </div>
                    </div>                         
                    <div class="CellGridView">
                        <asp:GridView runat="server" AutoGenerateColumns="False" ID="grdPlantillas" SkinID="GrillaSeguridad" DataKeyNames="PlantillaID" AllowPaging="True"
                            EmptyDataText="No existen plantillas de notificación" ShowHeaderWhenEmpty="True" OnRowDataBound="grdPlantillas_RowDataBound"                            
                            PageSize="10" OnPageIndexChanging="grdPlantillas_PageIndexChanging" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText = "AUTORIDAD">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAutoridad" runat="server" Text='<%# Eval("Autoridad") %>'></asp:Label>                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText = "NOMBRE">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("Nombre") %>'></asp:Label>                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText = "FORMATO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFormato" runat="server" Text='<%# Eval("Formato.Nombre") %>'></asp:Label>                                        
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText = "ESTADO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActivo" runat="server" Text='<%# (Convert.ToBoolean(Eval("Activo")) ? "ACTIVO" : "INACTIVO" ) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText = "EDITAR">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEditar" runat="server" CommandArgument='<%#Eval("PlantillaID")%>' OnClick="lnkEditar_Click">Editar</asp:LinkButton>
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
    <input runat="server" type="button" id="cmdNuevoPlantillaHide" style="display:none" />
    <cc1:ModalPopupExtender ID="mpeCrearPlantilla" runat="server" PopupControlID="dvCrearPlantilla" TargetControlID="cmdNuevoPlantillaHide" BehaviorID="mpeCrearPlantilla" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <div id="dvCrearPlantilla" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalFormularioNot">        
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
                                    <span id="spnAutoridad" class="botonAyudaUP" title="Autoridad ambiental a la cual pertenece la plantilla. Si no se selecciona la autoridad indica que esta plantilla puede ser utilizada por todas las autoridades." divModal="dvCrearPlantilla"></span>
                                </label>
                            </div>
                            <div class="CellFormularioCamposNot">
                                <asp:DropDownList runat="server" ID="cboAutoridad" OnSelectedIndexChanged="cboAutoridad_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="RowFormularioNot">                                                        
                            <div class="CellFormularioNot">
                                <label for="txtNombre">
                                    Nombre Plantilla:
                                    <span id="spnNombre" class="botonAyudaUP" title="Nombre de la plantilla" divModal="dvCrearPlantilla"></span>
                                </label>
                            </div>
                            <div class="CellFormularioCamposNot">
                                <asp:TextBox runat="server" ID="txtNombre" ClientIDMode="Static" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvNombre" ControlToValidate="txtNombre" ValidationGroup="PlantillaModal" ErrorMessage="Debe ingresar el nombre de la plantilla">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="RowFormularioNot">
                            <div class="CellFormularioNot">
                                <label for="cboFormulario">
                                    Formato:
                                    <span id="Span1" class="botonAyudaUP" title="Formato o formulario a utilizar para la generación del documento" divModal="dvCrearPlantilla"></span>
                                </label>
                            </div>
                            <div class="CellFormularioCamposNot">
                                <asp:DropDownList runat="server" ID="cboFormulario"></asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvFormulario" ControlToValidate="cboFormulario" ValidationGroup="PlantillaModal" InitialValue="-1" ErrorMessage="Debe ingresar el formato a utilizar">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="RowFormularioNot">
                            <div class="CellFormularioNot">
                                <label for="txtEncabezado">
                                    Mensaje Encabezado:
                                    <span id="spnEncabezado" class="botonAyudaUP" title="Mensaje que se va ha ingresar en el encabezado del documento ha generar. La ubicación del texto depende del formato o formulario seleccionado." divModal="dvCrearPlantilla"></span>
                                </label>
                            </div>
                            <div class="CellFormularioCamposNot">                                                    
                                <a href="#" id="lnkTextEncabezado" style="display:none;" class="LinkEditorHtmlNotificacion">Cerrar [X]</a>                                
                                <asp:TextBox runat="server" ID="txtEncabezado" ClientIDMode="Static" MaxLength="50" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox>                                                                          
                                <a href="#" id="lnkEncabezado" class="LinkEditorHtmlNotificacion">Editor&nbsp;HTML</a>
                            </div>
                        </div>
                        <div class="RowFormularioNot">
                            <div class="CellFormularioNot">
                                <label for="txtCuerpo">
                                    Contenido:
                                    <span id="spnCuerpo" class="botonAyudaUP" title="Contenido del documento. La ubicación del texto depende del formato o formulario seleccionado." divModal="dvCrearPlantilla"></span>
                                </label>
                            </div>
                            <div class="CellFormularioCamposNot">
                                <a href="#" id="lnkTextCuerpo" style="display:none;" class="LinkEditorHtmlNotificacion">Cerrar [X]</a>                                
                                <asp:TextBox runat="server" ID="txtCuerpo" ClientIDMode="Static" MaxLength="50" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox>                                                                                    
                                <a href="#" id="lnkCuerpo" class="LinkEditorHtmlNotificacion">Editor&nbsp;HTML</a>
                            </div>
                        </div>                        
                        <div class="RowFormularioNot">
                            <div class="CellFormularioNot">
                                <label for="txtPlantilla">
                                    Mensaje Firma:
                                    <span id="spnPieFirma" class="botonAyudaUP" title="Mensaje que se va ha ingresar al pie de la firma del documento. La ubicación del texto depende del formato o formulario seleccionado." divModal="dvCrearPlantilla"></span>
                                </label>
                            </div>
                            <div class="CellFormularioCamposNot">
                                <a href="#" id="lnkTextPieFirma" style="display:none;" class="LinkEditorHtmlNotificacion">Cerrar [X]</a>                                
                                <asp:TextBox runat="server" ID="txtPieFirma" ClientIDMode="Static" MaxLength="50" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox>                                                                           
                                <a href="#" id="lnkPieFirma" class="LinkEditorHtmlNotificacion">Editor&nbsp;HTML</a>
                            </div>
                        </div>
                        <div class="RowFormularioNot">
                            <div class="CellFormularioNot">
                                <label for="txtPie">
                                    Mensaje Pie:
                                    <span id="spnPie" class="botonAyudaUP" title="Mensaje que se va ha ingresar al pie del documento. La ubicación del texto depende del formato o formulario seleccionado." divModal="dvCrearPlantilla"></span>
                                </label>
                            </div>
                            <div class="CellFormularioCamposNot">
                                <a href="#" id="lnkTextPie" style="display:none;" class="LinkEditorHtmlNotificacion">Cerrar [X]</a>                                
                                <asp:TextBox runat="server" ID="txtPie" ClientIDMode="Static" MaxLength="50" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox>                                
                                <a href="#" id="lnkPie" class="LinkEditorHtmlNotificacion">Editor&nbsp;HTML</a>
                            </div>
                        </div>
                        <div class="RowFormularioNot" runat="server" id="divEstado">
                            <div class="CellFormularioNot">
                                <label for="cboEstado">
                                    Estado:
                                    <span id="spnEstado" class="botonAyudaUP" title="Indica si la plantilla se encuentra Activa o Inactiva." divModal="dvCrearPlantilla"></span>
                                </label>
                            </div>
                            <div class="CellFormularioCamposNot">
                                <asp:DropDownList runat="server" ID="cboEstado">
                                    <asp:ListItem Text="ACTIVO" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="INACTIVO" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="RowFormularioNot" runat="server" id="divFirmas">
                            <div class="CellFormularioNot">
                                <label for="cboEstado">
                                    Firmas:
                                    <span id="spnFirmas" class="botonAyudaUP" title="Firmas autorizadas para incluir en la plantilla." divModal="dvCrearPlantilla"></span>
                                </label>
                            </div>
                            <div class="CellFormularioCamposNot">
                                <asp:GridView runat="server" ID="grdFirmas" AutoGenerateColumns="false" ShowFooter="true" ShowHeaderWhenEmpty="false" EmptyDataText="No se han adicionado firmas" SkinID="GrillaDatosNotificaciones">
                                    <Columns>
                                        <asp:TemplateField HeaderText = "FIRMA">
                                            <ItemTemplate>
                                                <asp:Literal ID="ltlNombreFirma" runat="server" Text='<%# Eval("NombreAutorizado")%>'></asp:Literal>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList runat="server" ID="cboGrdFirma" />
                                                <asp:RequiredFieldValidator ID="rfvGrdFirma" runat="server" ControlToValidate="cboGrdFirma" ErrorMessage="Seleccione la firma" Text="*" ValidationGroup="GrdPlantillaModal" InitialValue="-1"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>                                                     
                                        <asp:TemplateField HeaderText="ELIMINAR">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEliminarFirma" runat="server" Text="Eliminar" CommandArgument='<%# Eval("FirmaAutoridadID")%>' OnClick="lnkEliminarFirma_Click" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkAdicionarFirma" runat="server" Text="Adicionar" CausesValidation="true" ValidationGroup="GrdPlantillaModal" OnClick="lnkAdicionarFirma_Click"></asp:LinkButton>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:ValidationSummary ID="valGrdPlantillaModal" runat="server" ValidationGroup="GrdPlantillaModal" ShowMessageBox="true" ShowSummary="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="CellButtonModal">
                <asp:ValidationSummary ID="valPlantillaModal" runat="server" ValidationGroup="PlantillaModal" ShowMessageBox="true" ShowSummary="false" />
                <asp:HiddenField runat="server" ID="hdfPlantillaID" />
                <asp:Button ID="cmdGuardarPlantilla" runat="server" Text="Adicionar" CssClass="boton" OnClick="cmdGuardarPlantilla_Click" ValidationGroup="PlantillaModal"/>
                <asp:Button ID="cmdCancelar" runat="server" Text="Cancelar" CssClass="boton" OnClick="cmdCancelar_Click" CausesValidation="false"/>
            </div>
        </div>
    </div>
    
    <script language="javascript" type="text/javascript">

        $("#lnkEncabezado").click(function () {
            $('#txtEncabezado').jqte();
            $("#lnkEncabezado").hide();
            $("#lnkTextEncabezado").show();
        });

        $("#lnkTextEncabezado").click(function () {
            $('#txtEncabezado').jqte({ "status": false });
            $("#lnkEncabezado").show();
            $("#lnkTextEncabezado").hide();
        });


        $("#lnkCuerpo").click(function () {
            $('#txtCuerpo').jqte();
            $("#lnkCuerpo").hide();
            $("#lnkTextCuerpo").show();
        });

        $("#lnkTextCuerpo").click(function () {
            $('#txtCuerpo').jqte({ "status": false });
            $("#lnkCuerpo").show();
            $("#lnkTextCuerpo").hide();
        });

        $("#lnkPieFirma").click(function () {
            $('#txtPieFirma').jqte();
            $("#lnkPieFirma").hide();
            $("#lnkTextPieFirma").show();
        });

        $("#lnkTextPieFirma").click(function () {
            $('#txtPieFirma').jqte({ "status": false });
            $("#lnkPieFirma").show();
            $("#lnkTextPieFirma").hide();
        });

        $("#lnkPie").click(function () {
            $('#txtPie').jqte();
            $("#lnkPie").hide();
            $("#lnkTextPie").show();
        });

        $("#lnkTextPie").click(function () {
            $('#txtPie').jqte({ "status": false });
            $("#lnkPie").show();
            $("#lnkTextPie").hide();
        });

    </script>

</asp:Content>
