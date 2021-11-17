<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPAArchivos.master" AutoEventWireup="true" CodeFile="FormularioPermisoREA.aspx.cs" Inherits="PermisoREA_FormularioPermisoREA" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
     <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>
    <div class='burbujaAyuda'></div> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
         .comboBox {
             width:25% !important;
         }
         ol, p {
             padding-left:1%;
             padding-right:1%;
         }
         .aqui {
             font-size: 30px !important;
         }
    </style>

    <script src="../Scripts/jquery-1.9.1.js"></script>      
    <link href="../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../jquery/EstiloGris/jquery-ui.js"  type="text/javascript"></script>
    <script src="../js/Ayuda.js" type="text/javascript"></script>
    <script src="../jquery/jquery.numeric.js"></script>
    <link href="css/EvaluacionREA.css" rel="stylesheet" />

    <script language="javascript" type="text/javascript">
        window.history.forward();
    </script>
     <script type="text/javascript">
         function CamposNumericos() {
             $("#txtDuracion").numeric();
         }

         function ValidarDescripcionMetodologias(sender, args) {
             
             if ($("#fulDocumentoMetodologiaEstablecida").val().length > 0)
                 args.IsValid = true;
             else 
                 args.IsValid = false;
             return;
         }

         function ValidarPerfilProfesionales(sender, args) {
             if ($("#fulDocumentoPerfilProfesionales").val().length > 0)
                 args.IsValid = true;
             else 
                 args.IsValid = false;
             return;
         }
         function ValidarDocumentoIdentificacion(sender, args)
         {
             if ($("#fulDocumentoIdentificacion").val().length > 0)
                 args.IsValid = true;
             else 
                 args.IsValid = false;
             return;
         }
         function ValidarReciboConsignacion(sender, args) {
             if ($("#fulDocumentoReciboConsignacion").val().length > 0)
                 args.IsValid = true;
             else 
                 args.IsValid = false;
             return;
         }
         function ValidarCobertura(sender, args) {
             var rows = 0;
             $("#grvCobertura").find("tr").each(function (idx, row) {
                 if (idx > 0) {
                     rows = rows + 1;
                 }
             });
             if (rows == 0) {
                 args.IsValid = false;
             }
             else {
                 args.IsValid = true;
             }
         }
         function ValidarInsumosSolicitud(sender, args)
         {
             var rows = 0;
             $("#Table_InsumosSolicitud").find("tr").each(function (idx, row) {
                 if (idx > 0) {
                     rows = rows + 1;
                 }
             });
             if (rows == 0) {
                 args.IsValid = false;
             }
             else {
                 args.IsValid = true;
             }
         }

         function MostrarBarraProgreso() {
             $get('upplFormulario').style.display = 'block';
         }

         function EndRequestHandler(sender, args) {
             $("[id*=accordionSecciones]").accordion({
                 collapsible: true,
                 heightStyle: "content",
                 active: false
             });

             CamposNumericos();

             $("form").on("submit", function (e) {
                 if (!e.isDefaultPrevented())
                     MostrarBarraProgreso();
             });
         }


         $(document).ready(function () {
             //Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler)
             Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
             $("[id*=accordionSecciones]").accordion({
                 collapsible: true,
                 heightStyle: "content",
                 active: false
             });

             /// 1.	Documento que describa las Metodologías Establecidas para cada uno de los grupos biológicos objeto de estudio

             var fileUpload = $('#fulDocumentoMetodologiaEstablecida');
             var hiddenUpload = $('#hiddenUploadfulDocumentoMetodologiaEstablecida');
             hiddenUpload.val(fileUpload.val());

             CamposNumericos();

             $("form").on("submit", function (e) {
                 if (!e.isDefaultPrevented())
                     MostrarBarraProgreso();
             });

             
             

         });
      </script>
    <asp:ScriptManager ID="scmManager" runat="server"></asp:ScriptManager>    
    <table class="TablaTituloSeccionPermisoREA">
        <tr>
            <td class="div-titulo">
                <asp:Label ID="lblTitulo" runat="server" Text="
FORMULARIO UNICO NACIONAL DE SOLICITUD PERMISO DE ESTUDIO PARA LA RECOLECCIÓN DE ESPECÍMENES DE ESPECIES SILVESTRES DE LA DIVERSIDAD BIOLÓGICA
CON FINES DE ELABORACIÓN DE ESTUDIOS AMBIENTALES (Decreto 3016 de 2013)" SkinID="titulo_principal_blanco"></asp:Label>
            </td>
        </tr>
    </table>

    <asp:UpdatePanel runat="server" ID="upnlFormulario" UpdateMode="Conditional">
        <ContentTemplate>

            <!-- INICIO INFORMACION GENERAL -->
            <table class="TablaFormularioPermisoREA">
                <tr>
                    <td colspan="2" class="TituloSeccionPermisoREA">
                        Información General
                    </td>
                </tr>

                <tr>
                    <td class="LabelFormularioPermisoREA">
                        Autoridad ambiental:
                    </td>
                    <td class="CamposFormularioPermisoREA">
                        <asp:DropDownList id="cboAutoridadAmbiental" runat="server" Width="30%" AutoPostBack="true" OnSelectedIndexChanged="cboAutoridadAmbiental_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="rfvcboAutoridadAmbiental" ControlToValidate="cboAutoridadAmbiental" InitialValue="" ValidationGroup="FormularioPermisoREA" ErrorMessage="Debe seleccionar una autoridad ambiental.">*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator runat="server" ID="rfvcboAutoridadAmbientalCobertura" ControlToValidate="cboAutoridadAmbiental" InitialValue="" ValidationGroup="AgregarCobertura" ErrorMessage="Debe seleccionar una autoridad ambiental.">*</asp:RequiredFieldValidator>
                        <span id="spnDector" class="botonAyudaUP" title='Autoridad ambiental competente.'></span>
                    </td>
                </tr>
                <tr>
                    <td class="LabelFormularioPermisoREA">
                         Duración (meses) solicitada del permiso:
                    </td>
                    <td class="CamposFormularioPermisoREA">
                        <asp:TextBox ID="txtDuracion" runat="server" ClientIDMode="Static" Width="30px" MaxLength="2"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfvtxtDuracion" ControlToValidate="txtDuracion" InitialValue="" ValidationGroup="FormularioPermisoREA" ErrorMessage="Debe seleccionar una duración.">*</asp:RequiredFieldValidator>
                        <asp:RangeValidator runat="server" ControlToValidate="txtDuracion" Type="Integer" MinimumValue="1" MaximumValue="24" ErrorMessage="Duración (meses) solicitada del permiso: Debe ingresar un valor entre 1 y 24." ValidationGroup="FormularioPermisoREA">*</asp:RangeValidator>
                        <span id="Span1" class="botonAyudaUP" title='Debe ingresar un valor entre 1 y 24'></span>
                    </td>
                </tr>
            </table>
            <!-- COBERTURA -->
            <table class="TablaFormularioPermisoREA">
                <tr>
                    <td colspan="2" class="TituloSeccionPermisoREA">Cobertura
                    </td>
                </tr>
                <tr>
                    <td class="LabelFormularioPermisoREA">Departamento:
                    </td>
                    <td class="CamposFormularioPermisoREA">
                        <asp:DropDownList ID="cboDepartamento" ClientIDMode="Static" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboDepartamento_SelectedIndexChanged" Enabled="false">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="rfvcboDepartamento" ControlToValidate="cboDepartamento" InitialValue="" ValidationGroup="AgregarCobertura" ErrorMessage="Debe seleccionar un departamento.">*</asp:RequiredFieldValidator><span id="Span2" class="botonAyudaUP" title='Debe seleccionar una autoridad ambiental para habilitar la lista.'></span>
                        

                    </td>
                </tr>
                <tr>
                    <td class="LabelFormularioPermisoREA">Municipio:
                    </td>
                    <td class="CamposFormularioPermisoREA">
                        <asp:DropDownList ID="cboMunicipio" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="rfvcboMunicipio" ControlToValidate="cboMunicipio" InitialValue="" ValidationGroup="AgregarCobertura" ErrorMessage="Debe seleccionar un municipo.">*</asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table class="TablaBotonesFormularioPermisoREA">
                            <tr>
                                <td class="BotonesRID">
                                    <asp:Button runat="server" ID="btnAgregarCobertura" Text="Agregar" ClientIDMode="Static" ValidationGroup="AgregarCobertura" OnClick="btnAgregarCobertura_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="grvCobertura" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                            SkinID="GrillaRegistroDetergente"
                            ShowHeaderWhenEmpty="false"
                            AllowPaging="true" PageSize="10"
                            Width="98%" GridLines="None"
                            CellSpacing="4" CellPadding="4"
                            HorizontalAlign="Center" OnRowDeleting="grvCobertura_RowDeleting" DataKeyNames="Key" ClientIDMode="Static" OnPageIndexChanging="grvCobertura_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Departamento">
                                    <ItemTemplate>
                                        <asp:Literal runat="server" ID="ltDepartamento" Text='<%# Eval("Departamento") %>'></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Municipio">
                                    <ItemTemplate>
                                        <asp:Literal runat="server" ID="ltlMunicipio" Text='<%# Eval("Municipio") %>'></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgDelCobertura" ImageUrl="~/App_Themes/Img/Del.png" runat="server" CommandName="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                        <asp:CustomValidator ID="cvgrvCobertura" runat="server" ErrorMessage="Debe ingresar información de cobertura" ValidationGroup="FormularioPermisoREA" ClientValidationFunction="ValidarCobertura" OnServerValidate="cvgrvCobertura_ServerValidate">*</asp:CustomValidator>
                    </td>
                </tr>
            </table>

            <!-- INSUMOS -->
            <table runat="server" id="tblInformacion" class="TablaFormularioPermisoREA">
                <tr>
                    <td class="TituloSeccionPermisoREA">
                        Información de Insumos
                    </td>
                </tr>
                <tr>
                    <td class="TextoExplicativoPermisoREA">
                        Para agregar información de insumos para el grupo biologico <asp:DropDownList ID="cboGrupoBiologicoNew" runat="server"></asp:DropDownList> haga click <asp:LinkButton ID="lnkAgregar" runat="server" Text="aquí" OnClick="lnkAgregar_Click" ClientIDMode="Static" ValidationGroup="grupoBiologico" CssClass="aqui"></asp:LinkButton>
                        <asp:RequiredFieldValidator runat="server" ID="rfvcboGrupoBiologicoNew" ControlToValidate="cboGrupoBiologicoNew" ValidationGroup="grupoBiologico" Text="*" Display="Dynamic" InitialValue="">Debe seleccionar un grupo biológico</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel runat="server" ID="upnlRegistrosInsumos" ClientIDMode="Static" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Repeater runat="server" ID="rptInsumos" OnItemDataBound="rptInsumos_ItemDataBound" ClientIDMode="Static">
                                    <ItemTemplate>
                                        <table class="TablaSeccionPermisoREA" id="Table_InsumosSolicitud">
                                            <tr>
                                                <td runat="server" id="tdSeccionVisible">
                                                    <asp:LinkButton runat="server" ID="lnkEditarGrupo" CssClass="LnkTextoModal" OnClick="lnkEditarGrupo_Click" CommandArgument='<%# string.Format("{0};{1}",Eval("objGrupoBiologicoEntity.GrupoBiologicoID"),Eval("objGrupoBiologicoEntity.GrupoBiologico"))  %>'>Editar Grupo</asp:LinkButton>
                                                    <div id="accordionSecciones">
                                                        <div class="HeaderAccordionPermisoREA">
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 50%;">
                                                                        <asp:Literal runat="server" ID="ltlTituloSeccionCuestionario" Text='<%# Eval("objGrupoBiologicoEntity.GrupoBiologico") %>'></asp:Literal>
                                                                    </td>
                                                                    <td style="width: 50%; text-align: right;"></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div class="BodyAccordeonPermisoREA">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="grvInsumoRecoleccionGrupo" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                                                            SkinID="GrillaRegistroDetergente"
                                                                            ShowHeaderWhenEmpty="false"
                                                                            AllowPaging="true" PageSize="10"
                                                                            Width="98%" GridLines="None"
                                                                            CellSpacing="4" CellPadding="4"
                                                                            HorizontalAlign="Center">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Técnica muestreo">
                                                                                    <ItemTemplate>
                                                                                        <asp:Literal runat="server" ID="ltlNombre" Text='<%# Eval("objTecnicaMuestreoEntity.TecnicaMuestreo") %>'></asp:Literal>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Características">
                                                                                    <ItemTemplate>
                                                                                        <asp:Literal runat="server" ID="ltlNombre" Text='<%# Caracteristicas(Eval("ObjLstCaracteristicaEntity")) %>'></asp:Literal>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Unidad de muestreo">
                                                                                    <ItemTemplate>
                                                                                        <asp:Literal runat="server" ID="ltlNombre" Text='<%# Eval("objUnidadMuestreoEntity.UnidadMuestreo") %>'></asp:Literal>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Esfuerzo de muestreo">
                                                                                    <ItemTemplate>
                                                                                        <asp:Literal runat="server" ID="ltlNombre" Text='<%# EsfuerzoMuestreo(Eval("objLstEsfuerzoMuestreoEntity")) %>'></asp:Literal>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Recolección">
                                                                                    <ItemTemplate>
                                                                                        <asp:Literal runat="server" ID="ltlNombre" Text='<%# Eval("objRecoleccionEntity.Recoleccion") %>'></asp:Literal>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                        </asp:GridView>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="grvInsumoPreservMovGrupo" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                                                            SkinID="GrillaRegistroDetergente"
                                                                            ShowHeaderWhenEmpty="false"
                                                                            AllowPaging="true" PageSize="10"
                                                                            Width="98%" GridLines="None"
                                                                            CellSpacing="4" CellPadding="4"
                                                                            HorizontalAlign="Center">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Sacrificio">
                                                                                    <ItemTemplate>
                                                                                        <asp:Literal runat="server" ID="ltlNombre" Text='<%# Eval("objTipoSacrificioEntity.TipoSacrificio") %>'></asp:Literal>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Fijación/Preservación">
                                                                                    <ItemTemplate>
                                                                                        <asp:Literal runat="server" ID="ltlNombre" Text='<%# Eval("objTipoFijacionPreservacionEntity.Nomenclatura") %>'></asp:Literal>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Movilización">
                                                                                    <ItemTemplate>
                                                                                        <asp:Literal runat="server" ID="ltlDescripcion" Text='<%#Eval("objTipoMovilizacionEntity.Nomenclatura") %>'></asp:Literal>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="gvrInsumoProfesionalesGrupo" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                                                            SkinID="GrillaRegistroDetergente"
                                                                            ShowHeaderWhenEmpty="false"
                                                                            AllowPaging="true" PageSize="10"
                                                                            Width="98%" GridLines="None"
                                                                            CellSpacing="4" CellPadding="4"
                                                                            HorizontalAlign="Center">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Formación académica">
                                                                                    <ItemTemplate>
                                                                                        <asp:Literal runat="server" ID="ltlNombre" Text='<%# Eval("objFormacionAcademicaProfesionalEntity.FormacionAcademicaProfesional") %>'></asp:Literal>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Experiencia">
                                                                                    <ItemTemplate>
                                                                                        <asp:Literal runat="server" ID="ltlNombreExperiencia" Text='<%# Eval("objTiempoExperienciaEntity.TiempoExperiencia") %>'></asp:Literal>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Experiencia especifica">
                                                                                    <ItemTemplate>
                                                                                        <asp:Literal runat="server" ID="ltlExperienciaEspe" Text='<%# Eval("objExperienciaEspecificaEntity.ExperienciaEspecifica")%>'></asp:Literal>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cmdCargarDatos" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="cmdCancelarCargaDatos" EventName="Click"/>
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:CustomValidator ID="cvInsumosSolicitud" runat="server" ErrorMessage="Debe ingresar información de insumos para la solicitud" ValidationGroup="FormularioPermisoREA" ClientValidationFunction="ValidarInsumosSolicitud">*</asp:CustomValidator>
                    </td>
                </tr>
            </table>
           
        </ContentTemplate>
        <Triggers>
             <asp:AsyncPostBackTrigger ControlID="btnAceptarMensajeAceptacion" EventName="Click" /> 
            <asp:AsyncPostBackTrigger ControlID="cboDepartamento" />
        </Triggers>
    </asp:UpdatePanel>
     <asp:UpdateProgress ID="upplFormulario" runat="server" AssociatedUpdatePanelID="upnlFormulario" ClientIDMode="Static">
        <ProgressTemplate>  
            <div id="ModalProgressContainer">
                <div>
                    <p>Procesando...</p>
                    <p><asp:Image ID="imgFormulario" runat="server" SkinId="procesando"/></p>
                </div>
            </div>                         
        </ProgressTemplate>
    </asp:UpdateProgress>
   <asp:UpdatePanel ID="upnlArchivos" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table runat="server" id="tblCargueArchivos" class="TablaFormularioPermisoREA" clientidmode="Static">
                <tr>
                    <td class="TituloSeccionPermisoREA" colspan="2">Documentos para anexar a la solicitud
                    </td>
                </tr>
                <tr>
                    <td class="TextoExplicativoPermisoREA">1.
                    </td>
                    <td class="TextoExplicativoPermisoREA">Documento que describa las Metodologías Establecidas para cada uno de los grupos biológicos objeto de estudio &nbsp;<span id="spnDocumentoMetodologiaEstablecida" class="botonAyudaUP" title='En caso de que se requiera cargar más de un archivo para describir la metodología establecida por favor comprimirlos en un archivo .zip.'></span>
                        <asp:CustomValidator ID="cvDocumentoMetodologiaEstablecida" runat="server" Display="Dynamic" ClientValidationFunction="ValidarDescripcionMetodologias" ValidationGroup="FormularioPermisoREA" ErrorMessage="Debe anexar el archivo que describe las Metodologías Establecidas para cada uno de los grupos biológicos objeto de estudio.">*</asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <%--<asp:TextBox runat="server" ID="txtDocumentoMetodologiaEstablecida" ClientIDMode="Static" Width="300px" CssClass="textInput"></asp:TextBox>
                        <input type="button" runat="server" id="btnDocumentoMetodologiaEstablecida" value="Seleccionar Archivo" clientidmode="Static" class="BotonFilePermisoREA" />--%>
                        
                        <asp:FileUpload ID="fulDocumentoMetodologiaEstablecida" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hiddenUploadfulDocumentoMetodologiaEstablecida" runat="server" ClientIDMode="Static" />
                    </td>
                </tr>
                <tr>
                    <td class="TextoExplicativoPermisoREA">2.
                    </td>
                    <td class="TextoExplicativoPermisoREA">Documento que describa el perfil que deberán tener los profesionales que intervendrán en los estudios  &nbsp;<span id="spnPerfilProfesionales" class="botonAyudaUP" title='En caso de que se requiera cargar más de un archivo para describir el perfil de los profesionales por favor comprimirlos en un archivo .zip.'></span>
                        <asp:CustomValidator ID="cvDocumentoPerfilProfesionales" runat="server" Display="Dynamic" ClientValidationFunction="ValidarPerfilProfesionales" ValidationGroup="FormularioPermisoREA" ErrorMessage="Debe anexar el archivo que describa el perfil que deberán tener los profesionales que intervendrán en los estudios.">*</asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <%--<asp:TextBox runat="server" ID="txtDocumentoPerfilProfesionales" ClientIDMode="Static" Width="300px" CssClass="textInput"></asp:TextBox>
                        <input type="button" runat="server" id="btnDocumentoPerfilProfesionales" value="Seleccionar Archivo" clientidmode="Static" class="BotonFilePermisoREA" />--%>
                        <asp:FileUpload ID="fulDocumentoPerfilProfesionales" runat="server" ClientIDMode="Static" />
                    </td>
                </tr>
                <tr>
                    <td class="TextoExplicativoPermisoREA">3.
                    </td>
                    <td class="TextoExplicativoPermisoREA">Copia del documento de identificación del solicitante del permiso. Si se trata de persona jurídica la entidad verificará en línea el certificado de existencia y representación legal. &nbsp; <span id="spnDocumentoIdentificacion" class="botonAyudaUP" title='En caso de que se requiera cargar más de un documento de indetificación por favor comprimirlos en un archivo .zip.'></span>
                        <asp:CustomValidator ID="cvDocumentoIdentificacion" runat="server" Display="Dynamic" ClientValidationFunction="ValidarDocumentoIdentificacion" ValidationGroup="FormularioPermisoREA" ErrorMessage="Debe anexar el archivo de identificación.">*</asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <%--<asp:TextBox runat="server" ID="txtDocumentoIdentificacion" ClientIDMode="Static" Width="300px" CssClass="textInput"></asp:TextBox>
                        <input type="button" runat="server" id="btnDocumentoIdentificacion" value="Seleccionar Archivo" clientidmode="Static" class="BotonFilePermisoREA" />--%>
                        
                        <asp:FileUpload ID="fulDocumentoIdentificacion" runat="server" ClientIDMode="Static" />
                        
                    </td>
                </tr>
                <tr>
                    <td class="TextoExplicativoPermisoREA">4.
                    </td>
                    <td class="TextoExplicativoPermisoREA">Copia del recibo de consignación del valor de los servicios fijados para la evaluación de la solicitud &nbsp; <span id="spnReciboConsignacion" class="botonAyudaUP" title='En caso de que se requiera cargar más de un recibo de consignación por favor comprimirlos en un archivo .zip.'></span>
                        <asp:CustomValidator ID="cvReciboConsignacion" runat="server" Display="Dynamic" ClientValidationFunction="ValidarReciboConsignacion" ValidationGroup="FormularioPermisoREA" ErrorMessage="Debe anexar el archivo recibo de consignación.">*</asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <%--<asp:TextBox runat="server" ID="txtDocumentoReciboConsignacion" ClientIDMode="Static" Width="300px" CssClass="textInput"></asp:TextBox>
                        <input type="button" runat="server" id="btnDocumentoReciboConsignacion" value="Seleccionar Archivo" clientidmode="Static" class="BotonFilePermisoREA" />--%>
                        
                        <asp:FileUpload ID="fulDocumentoReciboConsignacion" runat="server" ClientIDMode="Static" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upplArchivos" runat="server" AssociatedUpdatePanelID="upnlArchivos" ClientIDMode="Static">
        <ProgressTemplate>  
            <div id="ModalProgressContainer">
                <div>
                    <p>Procesando...</p>
                    <p><asp:Image ID="imgArchivos" runat="server" SkinId="procesando"/></p>
                </div>
            </div>                         
        </ProgressTemplate>
    </asp:UpdateProgress>

     <table class="TablaBotonesFormularioPermisoREA">
        <tr>
            <td>
                <asp:Button runat="server" ID="cmdEnviar" ValidationGroup="FormularioPermisoREA" Text="Enviar" ClientIDMode="Static" OnClick="cmdEnviar_Click" />
                <asp:ValidationSummary ID="valFormularioPermisoREA" runat="server" ValidationGroup="FormularioPermisoREA" ShowMessageBox="true" ShowSummary="false" />
            </td>
        </tr>
    </table>
    
    <input runat="server" type="button" style="display:none" id="btnOcultoGrupoBiologico" />
    <cc1:ModalPopupExtender ID="mpeAgregarGrupoBiologico" runat="server" PopupControlID="dvAgregarGrupoBiologico" TargetControlID="btnOcultoGrupoBiologico" BehaviorID="btnOcultoGrupoBiologico" BackgroundCssClass="ModalBackgroundPermisoREA">
    </cc1:ModalPopupExtender>    
    <div id="dvAgregarGrupoBiologico" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalPermisoREA">
        <asp:UpdatePanel runat="server" ID="upnlAgregarGrupoBiologico" UpdateMode="Conditional" >
            <ContentTemplate>
                <table class="TablaFormularioPermisoREA">
                    <tr>
                        <td colspan="2" class="TituloSeccionPermisoREA">
                            <asp:Literal runat="server" ID="ltlTituloAdcionar"></asp:Literal>

                        </td>
                    </tr>


                    <tr>
                        <td class="LabelFormularioPermisoREA">Seleccione la técnica de muestreo:
                        </td>
                        <td class="CamposFormularioPermisoREA">
                            <asp:DropDownList ID="cboTecnicaMuestreoNew" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboTecnicaMuestreoNew_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvcboTecnicaMuestreoNew" ControlToValidate="cboTecnicaMuestreoNew" ValidationGroup="InsumoRecoleccion" Text="*" Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelFormularioPermisoREA">Seleccione las características:
                        </td>
                        <td class="CamposFormularioPermisoREA">
                            <asp:DropDownList ID="cboCaracteristica1New" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboCaracteristica1New_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvcboCaracteristica1New" ControlToValidate="cboCaracteristica1New" ValidationGroup="InsumoRecoleccion" Text="*" Display="Dynamic">*</asp:RequiredFieldValidator>
                            <asp:DropDownList ID="cboCaracteristica2New" runat="server" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="cboCaracteristica2New_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvcboCaracteristica2New" ControlToValidate="cboCaracteristica2New" ValidationGroup="InsumoRecoleccion" Text="*" Display="Dynamic">*</asp:RequiredFieldValidator>
                            <asp:DropDownList ID="cboCaracteristica3New" runat="server" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="cboCaracteristica3New_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvcboCaracteristica3New" ControlToValidate="cboCaracteristica3New" ValidationGroup="InsumoRecoleccion" Text="*" Display="Dynamic">*</asp:RequiredFieldValidator>
                            <asp:DropDownList ID="cboCaracteristica4New" runat="server" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="cboCaracteristica4New_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvcboCaracteristica4New" ControlToValidate="cboCaracteristica4New" ValidationGroup="InsumoRecoleccion" Text="*" Display="Dynamic">*</asp:RequiredFieldValidator>
                            <asp:DropDownList ID="cboCaracteristica5New" runat="server" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="cboCaracteristica5New_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvcboCaracteristica5New" ControlToValidate="cboCaracteristica5New" ValidationGroup="InsumoRecoleccion" Text="*" Display="Dynamic">*</asp:RequiredFieldValidator>
                            <asp:DropDownList ID="cboCaracteristica6New" runat="server" Visible="false"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvcboCaracteristica6New" ControlToValidate="cboCaracteristica6New" ValidationGroup="InsumoRecoleccion" Text="*" Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelFormularioPermisoREA">Seleccione la unidad de muestreo:
                        </td>
                        <td class="CamposFormularioPermisoREA">
                            <asp:DropDownList ID="cboUnidadMuestreoNew" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvcboUnidadMuestreoNew" ControlToValidate="cboUnidadMuestreoNew" ValidationGroup="InsumoRecoleccion" Text="*" Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelFormularioPermisoREA">Seleccione el esfuerzo del muestreo:
                        </td>
                        <td class="CamposFormularioPermisoREA">
                            <asp:DropDownList ID="cboEsfuerzo1New" AutoPostBack="true" runat="server" OnSelectedIndexChanged="cboEsfuerzo1New_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvcboEsfuerzo1New" ControlToValidate="cboEsfuerzo1New" ValidationGroup="InsumoRecoleccion" Text="*" Display="Dynamic">*</asp:RequiredFieldValidator>
                            <asp:DropDownList ID="cboEsfuerzo2New" AutoPostBack="true" runat="server" OnSelectedIndexChanged="cboEsfuerzo2New_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvcboEsfuerzo2New" ControlToValidate="cboEsfuerzo2New" ValidationGroup="InsumoRecoleccion" Text="*" Display="Dynamic">*</asp:RequiredFieldValidator>
                            <asp:DropDownList ID="cboEsfuerzo3New" AutoPostBack="true" runat="server" OnSelectedIndexChanged="cboEsfuerzo3New_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvcboEsfuerzo3New" ControlToValidate="cboEsfuerzo3New" ValidationGroup="InsumoRecoleccion" Text="*" Display="Dynamic">*</asp:RequiredFieldValidator>
                            <asp:DropDownList ID="cboEsfuerzo4New" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvcboEsfuerzo4New" ControlToValidate="cboEsfuerzo4New" ValidationGroup="InsumoRecoleccion" Text="*" Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelFormularioPermisoREA">Seleccione el tipo de recolección:
                        </td>
                        <td class="CamposFormularioPermisoREA">
                            <asp:DropDownList ID="cboTipoRecoleccionNew" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvcboTipoRecoleccionNew" ControlToValidate="cboTipoRecoleccionNew" ValidationGroup="InsumoRecoleccion" Text="*" Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table class="TablaBotonesFormularioPermisoREA">
                                <tr>
                                    <td class="BotonesRID">
                                        <asp:Button runat="server" ID="btnAgrearInsumoRecoleccion" Text="Agregar" ClientIDMode="Static" ValidationGroup="InsumoRecoleccion" OnClick="btnAgrearInsumoRecoleccion_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="grvInsumoRecoleccion" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                SkinID="GrillaRegistroDetergente"
                                ShowHeaderWhenEmpty="false"
                                AllowPaging="true" PageSize="10"
                                Width="98%" GridLines="None"
                                CellSpacing="4" CellPadding="4"
                                HorizontalAlign="Center" OnRowDeleting="grvInsumoRecoleccion_RowDeleting" DataKeyNames="Key">
                                <Columns>
                                    <asp:TemplateField HeaderText="Técnica muestreo">
                                        <ItemTemplate>
                                            <asp:Literal runat="server" ID="ltTecnicaMuestreo" Text='<%# Eval("objTecnicaMuestreoEntity.TecnicaMuestreo") %>'></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Características">
                                        <ItemTemplate>
                                            <asp:Literal runat="server" ID="ltlCaracteristicas" Text='<%# Caracteristicas(Eval("ObjLstCaracteristicaEntity")) %>'></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unidad de muestreo">
                                        <ItemTemplate>
                                            <asp:Literal runat="server" ID="ltlUnidadMuestreo" Text='<%# Eval("objUnidadMuestreoEntity.UnidadMuestreo") %>'></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Esfuerzo de muestreo">
                                        <ItemTemplate>
                                            <asp:Literal runat="server" ID="ltlEsfuerzos" Text='<%# EsfuerzoMuestreo(Eval("objLstEsfuerzoMuestreoEntity")) %>'></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Recolección">
                                        <ItemTemplate>
                                            <asp:Literal runat="server" ID="ltlRecoleccion" Text='<%# Eval("objRecoleccionEntity.Recoleccion") %>'></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ImageUrl="~/App_Themes/Img/Del.png" runat="server" CommandName="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <table class="TablaFormularioPermisoREA">
                    <tr>
                        <td colspan="2" class="TituloSeccionPermisoREA">
                            INSUMO PRESERVACION Y MOVILIZACION
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelFormularioPermisoREA">
                            Seleccione el tipo sacrificio:
                        </td>
                        <td class="CamposFormularioPermisoREA">
                            <asp:DropDownList ID="cboTipoSacrificioNew" runat="server" OnSelectedIndexChanged="cboTipoSacrificioNew_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvcboTipoSacrificioNew" ControlToValidate="cboTipoSacrificioNew" ValidationGroup="InsumoPreservMov" Text="*" Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelFormularioPermisoREA">
                            Seleccione el tipo de Fijación/Preservación:
                        </td>
                        <td class="CamposFormularioPermisoREA">
                            <asp:DropDownList ID="cboTipoFijacionNew" runat="server" OnSelectedIndexChanged="cboTipoFijacionNew_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvcboTipoFijacionNew" ControlToValidate="cboTipoFijacionNew" ValidationGroup="InsumoPreservMov" Text="*" Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelFormularioPermisoREA">
                            Seleccione tipo movilización:
                        </td>
                        <td class="CamposFormularioPermisoREA">
                            <asp:DropDownList ID="cboTipoMovilizacionNew" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvcboTipoMovilizacionNew" ControlToValidate="cboTipoMovilizacionNew" ValidationGroup="InsumoPreservMov" Text="*" Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table class="TablaBotonesFormularioPermisoREA">
                                <tr>
                                    <td class="BotonesRID">
                                        <asp:Button runat="server" ID="btnAgregarInsmoPreservMov" Text="Agregar" ClientIDMode="Static" ValidationGroup="InsumoPreservMov" OnClick="btnAgregarInsmoPreservMov_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="grvInsumoPreservMovNew" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                SkinID="GrillaRegistroDetergente" 
                                ShowHeaderWhenEmpty="false" 
                                AllowPaging="true" PageSize="10" 
                                Width="98%" GridLines="None" 
                                CellSpacing="4" CellPadding="4" 
                                 HorizontalAlign="Center"  OnRowDeleting="grvInsumoPreservMovNew_RowDeleting" DataKeyNames="Key">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sacrificio">
                                        <ItemTemplate>
                                            <asp:Literal runat="server" ID="ltlTipoSacrificio" Text='<%# Eval("objTipoSacrificioEntity.TipoSacrificio") %>'></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fijación/Preservación">
                                        <ItemTemplate>
                                            <asp:Literal runat="server" ID="ltlTfpNomenclatura" Text='<%# Eval("objTipoFijacionPreservacionEntity.Nomenclatura") %>'></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Movilización">
                                        <ItemTemplate>
                                            <asp:Literal runat="server" ID="ltlTmNomenclatura" Text='<%#Eval("objTipoMovilizacionEntity.Nomenclatura") %>'></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ImageUrl="~/App_Themes/Img/Del.png" runat="server" CommandName="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    
                </table>
                <table class="TablaFormularioPermisoREA">
                    <tr>
                        <td colspan="2" class="TituloSeccionPermisoREA">
                            INSUMO PROFESIONALES
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelFormularioPermisoREA">
                            Seleccione la formación académica:
                        </td>
                        <td class="CamposFormularioPermisoREA">
                            <asp:DropDownList ID="cboFormacionAcademicaNew" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboFormacionAcademicaNew_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvcboFormacionAcademicaNew" ControlToValidate="cboFormacionAcademicaNew" ValidationGroup="InsumoProfesional" Text="*" Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelFormularioPermisoREA">
                            Seleccione la experiencia:
                        </td>
                        <td class="CamposFormularioPermisoREA">
                            <asp:DropDownList ID="cboExperienciaTiempoNew" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvcboExperienciaTiempoNew" ControlToValidate="cboExperienciaTiempoNew" ValidationGroup="InsumoProfesional" Text="*" Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelFormularioPermisoREA">
                            Seleccione la experiencia especifica:
                        </td>
                        <td class="CamposFormularioPermisoREA">
                            <asp:DropDownList ID="cboExperienciaEspecificaNew" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvcboExperienciaEspecificaNew" ControlToValidate="cboExperienciaEspecificaNew" ValidationGroup="InsumoProfesional" Text="*" Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table class="TablaBotonesFormularioPermisoREA">
                                <tr>
                                    <td class="BotonesRID">
                                        <asp:Button runat="server" ID="btnAgregarProfesional" Text="Agregar" ClientIDMode="Static" ValidationGroup="InsumoProfesional" OnClick="btnAgregarProfesional_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvrInsumoProfesionalesNew" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                SkinID="GrillaRegistroDetergente" 
                                ShowHeaderWhenEmpty="false" 
                                AllowPaging="true" PageSize="10" 
                                Width="98%" GridLines="None" 
                                CellSpacing="4" CellPadding="4" 
                                 HorizontalAlign="Center" OnRowDeleting="gvrInsumoProfesionalesNew_RowDeleting" DataKeyNames="Key">
                                <Columns>
                                    <asp:TemplateField HeaderText="Formación académica">
                                        <ItemTemplate>
                                            <asp:Literal runat="server" ID="ltlFormacionAcademica" Text='<%# Eval("objFormacionAcademicaProfesionalEntity.FormacionAcademicaProfesional") %>'></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Experiencia">
                                        <ItemTemplate>
                                            <asp:Literal runat="server" ID="ltlTiempoExperiencia" Text='<%# Eval("objTiempoExperienciaEntity.TiempoExperiencia") %>'></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Experiencia especifica">
                                        <ItemTemplate>
                                            <asp:Literal runat="server" ID="ltlExperienciaEspe" Text='<%# Eval("objExperienciaEspecificaEntity.ExperienciaEspecifica")%>'></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/App_Themes/Img/Del.png" runat="server" CommandName="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    
                </table>
                
                <table class="TablaBotonesFormularioPermisoREA">
                    <tr>
                        <td class="BotonesRID">
                            <asp:Button runat="server" ID="cmdCargarDatos" CausesValidation="false" Text="Agreagar insumos"  ClientIDMode="Static" OnClick="cmdCargarDatos_Click" />
                            <asp:Button runat="server" ID="cmdCancelarCargaDatos" CausesValidation="false" Text="Cancelar" ClientIDMode="Static" OnClick="cmdCancelarCargaDatos_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdCargarDatos" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnAgrearInsumoRecoleccion" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="cboGrupoBiologicoNew" EventName="SelectedIndexChanged" />
               
            </Triggers>
        </asp:UpdatePanel>  
        <asp:UpdateProgress ID="uppAgregarGrupoBiologico" runat="server" AssociatedUpdatePanelID="upnlAgregarGrupoBiologico">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgUpdateProgressValidar" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>        
    </div>


    <input type="button" runat="server" id="cmdErrorProcesoHide" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeErrorProceso" runat="server" PopupControlID="dvErrorProceso" TargetControlID="cmdErrorProcesoHide" BehaviorID="mpeErrorProcesos" BackgroundCssClass="ModalBackgroundPermisoREAMensaje">
    </cc1:ModalPopupExtender>
    <div id="dvErrorProceso" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalPermisoREA">
        <asp:UpdatePanel runat="server" ID="upnlErrorProceso" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="TablaFormularioPermisoREA">
                    <tr>
                        <td colspan="2" class="TituloSeccionPermisoREA">
                            Solicitud Evaluación REA
                        </td>
                    </tr>
                    <tr>
                        <td class="ImagenesModalPermisoREA">
                            <asp:Image runat="server" ID="imgIconoErrorProceso" ImageUrl="~/images/error.png" />
                        </td>
                        <td class="TextoModalErrorPermisoREA">
                            <asp:Literal runat="server" ID="ltlErrorProceso"></asp:Literal>
                        </td>
                    </tr>                        
                </table>
                <table class="TablaBotonesFormularioPermisoREA">
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="cmdAceptarErrorProceso" CausesValidation="false" Text="Aceptar" ClientIDMode="Static" OnClick="cmdAceptarErrorProceso_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdAceptarErrorProceso" EventName="Click" />                                                        
            </Triggers>
        </asp:UpdatePanel>

        <asp:UpdateProgress ID="uppErrorProceso" runat="server" AssociatedUpdatePanelID="upnlErrorProceso">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgErrorProceso" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <input type="button" runat="server" id="cmdMensajeAceptacion" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeMensajeAceptacion" runat="server" PopupControlID="dvMensajeAceptacion" TargetControlID="cmdMensajeAceptacion" BehaviorID="mpeMensajeAceptacion" BackgroundCssClass="ModalBackgroundPermisoREAMensaje">
    </cc1:ModalPopupExtender>
    <div id="dvMensajeAceptacion" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalPermisoREA">
        <asp:UpdatePanel runat="server" ID="upnlMensajeAceptacion" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="TablaFormularioPermisoREA">
                    <tr>
                        <td colspan="2" class="TituloSeccionPermisoREA">
                            Solicitud Evaluación REA
                        </td>
                    </tr>
                    <tr>
                        <td class="TextoModalErrorPermisoREA">
                            <ol>
                            <li> Toda persona que pretenda adelantar estudios, en los que sea necesario realizar actividades de recolección de especímenes de especies silvestres de la  biodiversidad en el territorio nacional; con la finalidad de elaborar estudios ambientales necesarios para solicitar y/o modificar licencias ambientales o su equivalente, permisos, concesiones o autorizaciones deberá previamente solicitar el permiso a la autoridad ambiental competente.</li>
                            <li> La obtención del permiso de que trata el presente decreto constituye un trámite previo dentro del proceso de licenciamiento ambiental y no implica la autorización de acceso y aprovechamiento a recursos genéticos.</li>
                            <li> El permiso amparará la recolecta de especímenes que se realicen durante su vigencia, en el marco de la elaboración de uno o varios estudios ambientales.</li>
                            <li> Entiéndase Recolección de especímenes como los procesos de captura y/o remoción o extracción temporal o definitiva del medio natural de especímenes de la biodiversidad, para la realización de inventarios y caracterizaciones que permitan el levantamiento de línea base de los estudios ambientales. Entiéndase como Captura la acción de apresar un espécimen silvestre, de forma temporal o definitiva, ya sea directamente o por medio de trampas diseñadas para tal fin.</li>
                            <li> El titular del permiso será responsable de garantizar  buenas prácticas en relación con número total de muestras, frecuencia de muestreo, punto de muestreo, entre otros aspectos, de manera que la recolección no cause afectación a las especies o a los ecosistemas.</li>
                            <li> Se debe adjuntar a este formulario:
                                <ol>
                                    <li> Documento de identificación del solicitante del permiso, ya sea certificado de existencia y representación legal para personas jurídicas o cédula de ciudadanía para personas naturales. En los casos que el trámite se adelante por medio de apoderado, se deberá anexar el poder debidamente conferido, que lo acredita como tal.</li>
                                <li> Documento que describa con más detalle las “Metodologías Establecidas” para cada uno de los grupos biológicos objeto de estudio.</li>
                                <li> Documento que describa el perfil que deberán tener los profesionales que intervendrán en los estudios.</li>
                                <li> Copia del recibo de consignación del valor de los servicios fijados para la evaluación de la solicitud.</li>
                                
                                    </ol>
                                </li>
                            </ol>
                            <p>El solicitante manifiesta que la información consignada en esta solicitud es fidedigna y se sujetará a la normatividad vigente y actos administrativos reglamentarios. </p>
                        </td>
                    </tr>                        
                </table>
                <table class="TablaBotonesFormularioPermisoREA">
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnAceptarMensajeAceptacion" CausesValidation="false" Text="Aceptar" ClientIDMode="Static" OnClick="btnAceptarMensajeAceptacion_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
           <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAceptarMensajeAceptacion" EventName="Click" />                                                        
            </Triggers>
        </asp:UpdatePanel>

        <asp:UpdateProgress ID="UpdateProupMensajeAceptacion" runat="server" AssociatedUpdatePanelID="upnlMensajeAceptacion">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgMensajeAceptacion" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>

