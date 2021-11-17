<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPASinMenu.master" AutoEventWireup="true" CodeFile="PublicacionEstados.aspx.cs" Inherits="NotificacionElectronica_PublicacionEstados" Title="Publicidad de Actos Administrativos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script src="../jquery/jquery.js" type="text/javascript"></script>
    <link href="../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../jquery/EstiloGris/jquery-ui.js"  type="text/javascript"></script>

    <asp:ScriptManager ID="scmManager" runat="server"></asp:ScriptManager>

    <div class="div-titulo">
        <asp:ImageButton id="btnRegresar" runat="server" SkinID="icoAtras" ToolTip="Regresar" CausesValidation="False" OnClick="btnRegresar_Click"></asp:ImageButton>
        <br />
        <asp:Label ID="lblTitulo" runat="server" Text="PUBLICACIÓN PUBLICIDAD ACTOS ADMINISTRATIVOS" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="contact_form" id="divConsultaCertificado" runat="server">
        <asp:UpdatePanel runat="server" ID="upnlBuscar" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="TableBuscarNot">
                    <div class="RowBuscarTitulo">
                        <div class="CellBuscarTitulo">
                            <asp:Literal ID="ltlTituloBuscador" runat="server" Text="FILTROS DE BÚSQUEDA"></asp:Literal>                    
                        </div>
                    </div>
                    <div class="RowBuscarNot">
                        <div class="CellBuscarNot">
                            <div class="TableBuscarInternoNot">
                                <div class="RowBuscarNot">                                                                                                            
                                    <div class="CellBuscarNot">
                                        <label for="cboMarcaVehiculo">Autoridad Ambiental:</label>
                                        <asp:DropDownList ID="cboAutoridad" runat="server" SkinID="lista_desplegable2" />
                                    </div>
                                    <div class="CellBuscarNot">
                                        <label for="txtNumeroActo">Tipo de Trámite:</label>
                                        <asp:DropDownList ID="cboTipoTramite" runat="server" SkinID="lista_desplegable2" />
                                    </div>
                                </div>
                                <div class="RowBuscarNot">
                                    <div class="CellBuscarNot">
                                        <label for="cboMarcaVehiculo">Número VITAL:</label>
                                        <asp:TextBox runat="server" ID="txtNumeroVital" ClientIDMode="Static"></asp:TextBox>                                        
                                    </div>
                                    <div class="CellBuscarNot">
                                        <label for="cboMarcaVehiculo">Número de Expediente:</label>
                                        <asp:TextBox runat="server" ID="txtExpediente" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="RowBuscarNot">                                    
                                    <div class="CellBuscarNot">
                                        <label for="cboMarcaVehiculo">Tipo de Documento:</label>
                                        <asp:DropDownList ID="cboTipoActo" runat="server" SkinID="lista_desplegable2" />
                                    </div>
                                    <div class="CellBuscarNot">
                                        <label for="txtNumeroActo">Número de Documento:</label>
                                        <asp:TextBox runat="server" ID="txtNumeroActo" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div> 
                                <div class="RowBuscarNot">
                                    <div class="CellBuscarNot">
                                        <label for="txtIdentificacionUsuario">Número de Identificación Persona Publicación:</label>
                                        <asp:TextBox runat="server" ID="txtIdentificacionUsuario" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <div class="CellBuscarNot">
                                        <label for="txtUsuarioNotificar">Incluir Publicaciones Archivadas:</label>
                                        <asp:CheckBox runat="server" id="chkIncluirHistoricas" />
                                    </div>
                                </div>                                                                                               
                                <div class="RowBuscarNot">
                                    <div class="CellBuscarNot">
                                        <label for="txtFechaDesde">Fecha Desde:</label>
                                        <asp:TextBox ID="txtFechaDesde" runat="server" ClientIDMode="Static"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFechaDesde" runat="server" ControlToValidate="txtFechaDesde" ErrorMessage="Ingrese la Fecha Desde la cual desea buscar." Text="*" ValidationGroup="NotBuscar" InitialValue=""></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rexFechaDesde" runat="server" ControlToValidate="txtFechaDesde" ErrorMessage="Formato de fecha no valido para la Fecha Desde." ValidationExpression="^\d{2}\/\d{2}\/\d{4}" Width="3px" Text="*" ValidationGroup="NotBuscar"></asp:RegularExpressionValidator>
                                        <cc1:CalendarExtender ID="calFechaDesde" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaDesde"/>
                                    </div>
                                    <div class="CellBuscarNot">
                                        <label for="txtFechaHasta">Fecha Hasta:</label>
                                        <asp:TextBox ID="txtFechaHasta" runat="server" ClientIDMode="Static"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFechaHasta" runat="server" ControlToValidate="txtFechaHasta" ErrorMessage="Ingrese la Fecha Hasta la cual desea buscar." Text="*" ValidationGroup="NotBuscar" InitialValue=""></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rexFechaHasta" runat="server" ControlToValidate="txtFechaHasta" ErrorMessage="Formato de fecha no valido para la Fecha Hasta." ValidationExpression="^\d{2}\/\d{2}\/\d{4}" Width="3px" Text="*" ValidationGroup="NotBuscar"></asp:RegularExpressionValidator>
                                        <cc1:CalendarExtender ID="calFechaHasta" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaHasta" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="RowButton">
                        <div class="CellButton">
                            <asp:HiddenField runat="server" ID="hdfTipoTramite" />
                            <asp:HiddenField runat="server" ID="hdfAutoridad" />
                            <asp:HiddenField runat="server" ID="hdfNumeroVital" />
                            <asp:HiddenField runat="server" ID="hdfExpediente" />
                            <asp:HiddenField runat="server" ID="hdfTipoActo" />
                            <asp:HiddenField runat="server" ID="hdfNumeroActo" />
                            <asp:HiddenField runat="server" ID="hdfIdentificacionUsuario" />
                            <asp:HiddenField runat="server" ID="hdfIncluirHistoricas" />
                            <asp:HiddenField runat="server" ID="hdfFechaDesde" />
                            <asp:HiddenField runat="server" ID="hdfFechaHasta" />
                            <asp:Button runat="server" ID="cmdBuscar" ValidationGroup="NotBuscar" Text="Consultar" ClientIDMode="Static" OnClick="cmdBuscar_Click" />
                            <asp:ValidationSummary ID="valNotBuscar" runat="server" ValidationGroup="NotBuscar" ShowMessageBox="true" ShowSummary="false" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdBuscar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel runat="server" ID="upnlMensaje" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="contact_form" id="divMensaje" runat="server" visible="false">  
                <div class="Table">
                    <div class="Row">
                        <div class="CellMensaje">
                            <asp:Label runat="server" ID="lblMensaje"></asp:Label>
                        </div>
                    </div>
                </div>            
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br /><br />
    <asp:UpdatePanel runat="server" ID="upnlEstadosNotificaciones" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="contact_form" id="divEstadosNotificaciones" runat="server">
        
                <div class="TableReporteNot">
                    <div class="RowReporteNot">
                        <div class="CellReporteNot">
                            <asp:GridView runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="15" ID="grdEstadosNotificaciones" 
                                          SkinID="GrillaNotificaciones" EmptyDataText="No se encontro información con los parámetros de búsqueda especificados." ShowHeaderWhenEmpty="true"
                                          OnPageIndexChanging="grdEstadosNotificaciones_PageIndexChanging">
                                <Columns>
                                        <asp:TemplateField HeaderText="AUTORIDAD AMBIENTAL" ItemStyle-CssClass="ItemNotificacionCentrado">
                                            <ItemTemplate>
                                                <asp:literal ID="ltlAutoridad" runat="server" Text='<%# Eval("NOMBRE_AUTORIDAD")  %>'></asp:literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TIPO TRÁMITE" ItemStyle-CssClass="ItemNotificacion">
                                            <ItemTemplate>
                                                <asp:literal ID="ltlTipoTramite" runat="server" Text='<%# Eval("NOMBRE_TIPO_TRAMITE")  %>'></asp:literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NÚMERO EXPEDIENTE" ItemStyle-CssClass="ItemNotificacionCentrado">
                                            <ItemTemplate>
                                                <asp:literal ID="ltlExpediente" runat="server" Text='<%# Eval("EXPEDIENTE")  %>'></asp:literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NÚMERO DOCUMENTO" ItemStyle-CssClass="ItemNotificacionCentrado">
                                            <ItemTemplate>
                                                <asp:literal ID="ltlNumeroDocumento" runat="server" Text='<%# Eval("NUMERO_DOCUMENTO")  %>'></asp:literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FECHA DOCUMENTO" ItemStyle-CssClass="ItemNotificacionCentrado">
                                            <ItemTemplate>
                                                <asp:literal ID="ltlFechaDocumento" runat="server" Text='<%# Eval("FECHA_DOCUMENTO")  %>'></asp:literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        
                                        <asp:TemplateField HeaderText="PUBLICIDAD" ItemStyle-CssClass="ItemNotificacionCentrado">
                                            <ItemTemplate>
                                                <asp:literal ID="ltlPublicidad" runat="server" Text='<%# Eval("PUBLICACION")  %>'></asp:literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FECHA PUBLICACIÓN" ItemStyle-CssClass="ItemNotificacionCentrado">
                                            <ItemTemplate>
                                                <asp:literal ID="ltlFechaFijacion" runat="server" Text='<%# Convert.ToDateTime(Eval("FECHA_FIJACION")).ToString("dd/MM/yyyy")  %>'></asp:literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FECHA DESFIJACIÓN" ItemStyle-CssClass="ItemNotificacionCentrado">
                                            <ItemTemplate>
                                                <asp:literal ID="ltlFechaDesFijacion" runat="server" Text='<%# (Eval("FECHA_DESFIJACION") != System.DBNull.Value ? Convert.ToDateTime(Eval("FECHA_DESFIJACION")).ToString("dd/MM/yyyy") : "-")  %>'></asp:literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="VER" ItemStyle-CssClass="ItemNotificacionCentrado">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkVer" Text="Ver" CommandArgument='<%# Eval("PUBLICACION_ESTADO_PERSONA_ACTO_ID") %>' OnClick="lnkVer_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>            
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <input type="button" runat="server" id="cmdVerEstadoHide" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeVerEstado" runat="server" PopupControlID="dvVerEstado" TargetControlID="cmdVerEstadoHide" BehaviorID="mpeVerEstados" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <div id="dvVerEstado" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalFormularioNot">
        <asp:UpdatePanel runat="server" ID="upnlVerEstado" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="TableResultadoModalNot">
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoTituloModalNot">
                            VER DETALLE PUBLICACIÓN
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellResultadoModalNot">
                            <div class="TableFormularioNot">
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Número de Identificación Persona Publicación:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlNumeroIdentificacionModal"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Nombre Persona Publicación:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlNombrePersonaModal"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Autoridad Ambiental:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlAutoridadAmbientalModal"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Tipo Trámite:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlTipoTramiteModal"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Número VITAL:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlNumeroVitalModal"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Número Expediente:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlNumeroExpedienteModal"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Nombre Proyecto:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlNombreProyectoModal"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Tipo de Documento:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlTipoDocumentoModal"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Número de Documento:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlNumeroDocumentoModal"></asp:Literal>
                                    </div>
                                </div>                            
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlFechaDocumentoModal">Fecha de Documento:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlFechaDocumentoModal"></asp:Literal>
                                    </div>
                                </div>                            
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Publicación:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlPublicacionModal"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot" runat="server" id="dvDocumentosPublicacion">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Documentos Publicación:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:GridView runat="server" ID="grdDocumentosModal" AutoGenerateColumns="false" ShowFooter="false" ShowHeaderWhenEmpty="false" EmptyDataText="No existen documentos relacionados" SkinID="GrillaDatosNotificaciones">
                                                <Columns>
                                                <asp:TemplateField HeaderText = "DOCUMENTO">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="ltlDocumento" runat="server" Text='<%# Eval("NOMBRE_DOCUMENTO")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                     
                                                <asp:TemplateField HeaderText = "VER" ItemStyle-CssClass="ItemNotificacionCentrado">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgDocumentoModal" ImageUrl="~/images/documento.png" CommandArgument='<%#Eval("RUTA_DOCUMENTO") %>' OnClick="imgDocumentoModal_Click"  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                     
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="RowFormularioNot" runat="server" id="dvAdjuntosPublicacion">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Adjuntos Publicación:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:GridView runat="server" ID="grdAdjuntosModal" AutoGenerateColumns="false" ShowFooter="false" ShowHeaderWhenEmpty="false" EmptyDataText="No existen documentos relacionados" SkinID="GrillaDatosNotificaciones">
                                                <Columns>
                                                <asp:TemplateField HeaderText = "DOCUMENTO">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="ltlDocumento" runat="server" Text='<%# Eval("NOMBRE_DOCUMENTO")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                     
                                                <asp:TemplateField HeaderText = "VER">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgDocumentoModal" ImageUrl="~/images/documento.png" CommandArgument='<%#Eval("RUTA_DOCUMENTO") %>' OnClick="imgDocumentoModal_Click"  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                     
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="RowFormularioNot">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Fecha Publicación:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlFechaPublicacionModal"></asp:Literal>
                                    </div>
                                </div>
                                <div class="RowFormularioNot" runat="server" id="dvFechaDesfijacion">
                                    <div class="CellFormularioNot">
                                        <label for="ltlAutoridadAmbiental">Fecha Desfijación:</label>                                           
                                    </div>
                                    <div class="CellFormularioCamposNot">
                                        <asp:Literal runat="server" ID="ltlFechaDesfijacion"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="RowResultadoModalNot">
                        <div class="CellButtonModal">                            
                            <asp:Button ID="cmdAceptarModal" runat="server" Text="Aceptar" CssClass="boton" CausesValidation="false" OnClick="cmdAceptarModal_Click"/>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <asp:UpdateProgress ID="uppBarraProgresoBuscar" runat="server" AssociatedUpdatePanelID="upnlBuscar">
        <ProgressTemplate>  
            <div id="ModalProgressContainer">
                <div>
                    <p>Procesando...</p>
                    <p><asp:Image ID="imgUpdateProgress" runat="server" SkinId="procesando"/></p>
                </div>
            </div>                         
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdateProgress ID="uppEstadosNotificaciones" runat="server" AssociatedUpdatePanelID="upnlEstadosNotificaciones">
        <ProgressTemplate>  
            <div id="ModalProgressContainer">
                <div>
                    <p>Procesando...</p>
                    <p><asp:Image ID="imgUpdateProgressEstadosNotificaciones" runat="server" SkinId="procesando"/></p>
                </div>
            </div>                         
        </ProgressTemplate>
    </asp:UpdateProgress>
        
</asp:Content>
