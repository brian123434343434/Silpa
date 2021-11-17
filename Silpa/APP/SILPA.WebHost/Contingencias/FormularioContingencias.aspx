<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/plantillas/SILPAArchivos.master" CodeFile="FormularioContingencias.aspx.cs" Inherits="Contingencias_FormularioContingencias" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>
    <div class='burbujaAyuda'></div>    
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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

    <script src="../jquery/jquery.js" type="text/javascript"></script>        
    <link href="../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../jquery/EstiloGris/jquery-ui.js"  type="text/javascript"></script>
    <script src="../js/Ayuda.js" type="text/javascript"></script>
    <link href="css/Contingencias.css" rel="stylesheet" />

    <script language="javascript" type="text/javascript">
        window.history.forward();
    </script>

    <script type="text/javascript">
        var hrefPag = "";

        function MostrarBarraProgreso() {
            $get('upplFormulario').style.display = 'block';
        }

        function FormatearNumero(valor, tipoVal) {
            if (tipoVal == 1) {
                valor = valor.replace(/[^0-9,]/g, "")
                    .replace(/\B(?=(\d{3})+(?!\d)\,?)/g, ".");
                valor = (valor.indexOf(',') != -1 ? valor.substring(0, valor.indexOf(',')) : valor);
            }
            else if (tipoVal == 2) {
                valor = valor.replace(/[^0-9,]/g, "")
                    .replace(/\B(?=(\d{3})+(?!\d)\,?)/g, ".");
                valor = (valor.indexOf(',') != -1 ? ((valor.length - valor.indexOf(',')) > 9 ? valor.substring(0, valor.indexOf(',') + 9) : valor) : valor);
            }

            return valor;
        }

        function FormatearCampoNumeroEntero(nombreControl) {
            $("[id*=" + nombreControl + "]").each(function () {
                $("#" + $(this).attr('id')).val(FormatearNumero($("#" + $(this).attr('id')).val(), 1));
                $("#" + $(this).attr('id')).on({
                    "focus": function (event) {
                        $(event.target).select();
                    },
                    "keyup": function (event) {
                        $(event.target).val(function (index, value) {
                            return FormatearNumero(value, 1);
                        });
                    }
                });
            });
        }

        function FormatearCampoNumero(nombreControl) {
            $("[id*=" + nombreControl + "]").each(function () {
                $("#" + $(this).attr('id')).val(FormatearNumero($("#" + $(this).attr('id')).val(), 2));
                $("#" + $(this).attr('id')).on({
                    "focus": function (event) {
                        $(event.target).select();
                    },
                    "keyup": function (event) {
                        $(event.target).val(function (index, value) {
                            return FormatearNumero(value, 2);
                        });
                    }
                });
            });
        }


        function InicializarCalendarios() {
            $("[id*=txtFechaPregunta]").datepicker({
                dateFormat: 'dd/mm/yy',
                changeYear: true,
                changeMonth: true
            });
            $("[id*=txtFechaPregunta]").datepicker("option", "yearRange", "-99:+0");
            $("[id*=txtFechaPregunta]").datepicker("option", "maxDate", "+0m +0d");
            $("[id*=txtFechaPregunta]").keypress(function (evt) { return false; });
        }

        function RecargarAcordion(hrefPagina) {
            var blnRecargar = false;

            blnRecargar = hrefPag.indexOf("cboDepartamentoPregunta") == -1;

            return blnRecargar;
        }

        function ValidarListaRadio (source, arguments){
            var object = $(source);
            var esValido = false;

            //Verificar si el objeto se encuentra visible
            if($("#" + object.attr("id").replace("cvOpcionUnica", "hdfPreguntaMostrada").replace("_", "")).val() == "1"){

                if($("#" + object.attr("id").replace("cvOpcionUnica", "hdfPreguntaObligatoria").replace("_", "")).val() == "true"){
                    $("input:radio[id*=" + object.attr("id").replace("cv", "rd") + "]:checked").each(function() {
                        esValido = true;
                    });
                }
                else{
                    esValido = true;
                }
            }
            else{
                esValido = true;
            }

            arguments.IsValid = esValido;
        }


        function ValidarListaMultiple(source, arguments) {
            var object = $(source);
            var esValido = false;

            //Verificar si el objeto se encuentra visible
            if ($("#" + object.attr("id").replace("cvOpcionMultiple", "hdfPreguntaMostrada").replace("_", "")).val() == "1") {

                if ($("#" + object.attr("id").replace("cvOpcionMultiple", "hdfPreguntaObligatoria").replace("_", "")).val() == "true") {
                    $("input:checkbox[id*=" + object.attr("id").replace("cv", "rd") + "]:checked").each(function () {
                        esValido = true;
                    });
                }
                else {
                    esValido = true;
                }
            }
            else {
                esValido = true;
            }

            arguments.IsValid = esValido;
        }


        function ValidarOpcionOtro(source, arguments){
            var object = $(source);
            var esValido = false;
            var listaSeleccionado = [];
            var datos = object.attr("id").split('_');

            $("input:radio:checked").each(function() {
                listaSeleccionado.push($(this).val());
            });

            if(datos.length == 3 && listaSeleccionado != null && listaSeleccionado.length > 0){
                if($.grep(listaSeleccionado, function(op){ return op === datos[1] }).length > 0){

                    if($("#" + object.attr("id").replace("cvOpcionOtro", "txtOpcionOtro")).val().replace(/ /g, "") == "")
                        esValido = false;
                    else
                        esValido = true;
                }
                else{
                    esValido = true;
                }
            }
            else{
                esValido = true;
            }

            arguments.IsValid = esValido;
        }


        function ValidarOpcionOtroMultiple(source, arguments) {
            var object = $(source);
            var esValido = false;
            var listaSeleccionado = [];
            var datos = object.attr("id").split('_');

            $("input:checkbox:checked").each(function () {
                listaSeleccionado.push($(this).val());
            });

            if (datos.length == 3 && listaSeleccionado != null && listaSeleccionado.length > 0) {
                if ($.grep(listaSeleccionado, function (op) { return op === datos[1] }).length > 0) {

                    if ($("#" + object.attr("id").replace("cvTextoOpcionMultiple", "txtTextoOpcionMultiple")).val().replace(/ /g, "") == "")
                        esValido = false;
                    else
                        esValido = true;
                }
                else {
                    esValido = true;
                }
            }
            else {
                esValido = true;
            }

            arguments.IsValid = esValido;
        }


        function ValidarDocumento(source, arguments){
            var object = $(source);
            var esValido = false;
            var archivo = document.getElementById(object.attr("id").replace("cvDocumentoPregunta", "fuplDocumentoPregunta"));

            if($("#" + object.attr("id").replace("cvDocumentoPregunta", "hdfPreguntaMostrada").replace("_", "")).val() == "1"){

                if($("#" + object.attr("id").replace("cvDocumentoPregunta", "hdfPreguntaObligatoria").replace("_", "")).val() == "true"){
                    if (archivo.files.length > 0 && archivo.files[0] != null && archivo.files[0].size > 0)        
                        esValido = true;
                }
                else{
                    esValido = true;
                }
            }
            else{
                esValido = true;
            }

            arguments.IsValid = esValido;
        }


        function HabiltarDeshabilitarValidacion(controles, habilitar){
            $("[id*="+ controles + "]").each(function () {
                ValidatorEnable(document.getElementById($(this).attr("id")), habilitar);
            });
        }

        function DesmarcarOpcionesOcultas() {
            var listaPreguntas = [];
            var continuarDesmarcando = false;

            $("[id*=hdfPreguntaID]").each(function () {
                listaPreguntas.push($(this).attr('value'));
            });

            //Buscar si la pregunta se debe mostrar
            listaPreguntas.forEach(function (elemento, indice, array) {
                if ($("#hdfPreguntaMostrada" + elemento).val() == "0") {
                    if ($("[id*=rdOpcionMultiple" + elemento + "_]:checked").length > 0 || $("[id*=rdOpcionUnica" + elemento + "_]:checked").length > 0) {
                        $("[id*=rdOpcionMultiple" + elemento + "_]").prop('checked', false);
                        $("[id*=rdOpcionUnica" + elemento + "_]").prop('checked', false);
                        continuarDesmarcando = true;
                    }
                }
            });

            MostrarPreguntasCuestionario();

            return continuarDesmarcando
        }


        function MostrarPreguntasCuestionario() {
            var listaPreguntas = [];
            var listaCondiciones = [];

            $("[id*=hdfPreguntaID]").each(function () {
                listaPreguntas.push($(this).attr('value'));
            });
            $("[id*=hdfCondicionesMostrarPregunta]").each(function () {
                var condiciones = eval($(this).attr('value'));
                condiciones.forEach(function (elemento, indice, array) {
                    listaCondiciones.push(elemento);
                });
            });

            //Buscar si la pregunta se debe mostrar
            listaPreguntas.forEach(function (elemento, indice, array) {
                
                var listaCondPreguntas = $.grep(listaCondiciones, function(op){ return op.PreguntaID == elemento });

                if(listaCondPreguntas != null && listaCondPreguntas.length > 0){
                    var listaY = $.grep(listaCondPreguntas, function(op){ return op.EsOpcional == 0 });
                    var listaO = $.grep(listaCondPreguntas, function(op){ return op.EsOpcional == 1 });
                    var listaSeleccionado = [];
                    $("input:radio:checked").each(function() {
                        listaSeleccionado.push($(this).val());
                    });
                    $("input:checkbox:checked").each(function () {
                        listaSeleccionado.push($(this).val());
                    });

                    //Verificar si existen seleccionado
                    if(listaSeleccionado != null && listaSeleccionado.length > 0){
                        var mostrar = false;
                        var mostrarY = true;
                        var mostrarO = false;

                        for(cont = 0; cont < listaY.length && mostrarY; cont ++){
                            var opciones = $.grep(listaSeleccionado, function(op){ return op == listaY[cont].OpcionID });
                            if(typeof(opciones) == 'undefined' || opciones == null || opciones.length == 0)
                                mostrarY = false;                            
                        }

                        if(listaO.length > 0){
                            for(cont = 0; cont < listaO.length && !mostrarO; cont ++){
                                var opciones = $.grep(listaSeleccionado, function(op){ return op == listaO[cont].OpcionID });
                                if(typeof(opciones) != 'undefined' && opciones != null && opciones.length > 0)
                                    mostrarO = true;
                            }
                        }
                        else{
                            mostrarO = true;
                        }

                        mostrar = mostrarY && mostrarO;

                        if(mostrar){
                            $("#hdfPreguntaMostrada" + elemento).val("1");
                            HabiltarDeshabilitarValidacion("rfv" +  elemento + "_", eval($("#hdfPreguntaObligatoria" + elemento).val()));
                            $("#" + elemento).show();
                        }
                        else{
                            HabiltarDeshabilitarValidacion("rfv" +  elemento + "_", false);
                            $("#hdfPreguntaMostrada" + elemento).val("0");
                            $("#" + elemento).hide();
                        }
                    }
                    else{
                        HabiltarDeshabilitarValidacion("rfv" +  elemento + "_", false);
                        $("#hdfPreguntaMostrada" + elemento).val("0");
                        $("#" + elemento).hide();
                    }
                }
                else{
                    HabiltarDeshabilitarValidacion("rfv" +  elemento + "_", eval($("#hdfPreguntaObligatoria" + elemento).val()));
                    $("#hdfPreguntaMostrada" + elemento).val("1");
                    $("#" + elemento).show();
                }                
            });
        }

        function HabilitarEtapaOtro() {
            if ($("#cboEtapaProyecto").val() == "4") {
                $("#txtEtapaProyectoOtro").show();
                if (document.getElementById("rfvEtapaProyectoOtro") != null)
                    ValidatorEnable(document.getElementById("rfvEtapaProyectoOtro"), true);
            }
            else {
                if (document.getElementById("rfvEtapaProyectoOtro") != null)
                    ValidatorEnable(document.getElementById("rfvEtapaProyectoOtro"), false);
                $("#txtEtapaProyectoOtro").val("");
                $("#txtEtapaProyectoOtro").hide();
            }
        }

        function VerificarExpediente(source, arguments) {
            if ($("#txtNombreProyectoCorporacion").val().replace(/ /g, "") === "" && $("#txtExpedienteCorporacion").val().replace(/ /g, "") === "")
                arguments.IsValid = false;
            else
                arguments.IsValid = true;
        }


        $(function () {

            $("[id*=accordionSecciones]").accordion({
                collapsible: true,
                heightStyle: "content",
                active: false
            });

            InicializarCalendarios();

            $('[id*=cbo]').change(function (e) {
                hrefPag = $(this).attr("id");
            });

            HabilitarEtapaOtro();

            FormatearCampoNumeroEntero("txtGrados");
            FormatearCampoNumero("txtMinutos");
            FormatearCampoNumero("txtSegundos");
            FormatearCampoNumero("txtOpcionTextoNumerico");

            $("form").on("submit", function (e) {
                if(!e.isDefaultPrevented())
                    MostrarBarraProgreso();
            });


            $("#cboEtapaProyecto").change(function () {
                HabilitarEtapaOtro();
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {

                HabilitarEtapaOtro();

                $("form").on("submit", function (e) {
                    if(!e.isDefaultPrevented())
                        MostrarBarraProgreso();
                });

                $("#cboEtapaProyecto").change(function () {
                    HabilitarEtapaOtro();
                });

                FormatearCampoNumeroEntero("txtGrados");
                FormatearCampoNumero("txtMinutos");
                FormatearCampoNumero("txtSegundos");
                FormatearCampoNumero("txtOpcionTextoNumerico");                

                $('[id*=cbo]').change(function (e) {
                    hrefPag = $(this).attr("id");
                });

                $("[id*=rdOpcionUnica]").click(function () {	 
                    MostrarPreguntasCuestionario();
                    while(DesmarcarOpcionesOcultas());
                });

                $("[id*=rdOpcionMultiple]").click(function () {                    
                    MostrarPreguntasCuestionario();
                    while(DesmarcarOpcionesOcultas());
                });

                if (RecargarAcordion(hrefPag)) {
                    $("[id*=accordionSecciones]").accordion({
                        collapsible: true,
                        heightStyle: "content",
                        active: false
                    });
                }

                InicializarCalendarios();

                MostrarPreguntasCuestionario();
            });

        });
        
    </script>


    <asp:ScriptManager ID="scmManager" runat="server"></asp:ScriptManager>        

    <table class="TablaTituloSeccionContingencias">
        <tr>
            <td class="div-titulo">
                <asp:Label ID="lblTitulo" runat="server" Text="FORMATO REPORTE DE CONTINGENCIAS" SkinID="titulo_principal_blanco"></asp:Label>
            </td>
        </tr>
    </table>

    <asp:UpdatePanel runat="server" ID="upnlFormulario" UpdateMode="Conditional">
        <ContentTemplate>

            <!-- INICIO INFORMACION GENERAL -->
            <table class="TablaFormularioContingencias">
                <tr>
                    <td colspan="2" class="TituloSeccionContingencias">
                        Información General del Proyecto
                    </td>
                </tr>

                <tr id="trAutoridadAmbiental" runat="server">
                    <td class="LabelFormularioContingencias">
                        Autoridad Ambiental:
                        <span id="spnAutoridadAmbiental" class="botonAyudaUP" title='Ingrese la autoridad ambiental ante la cual se presentará el reporte de contingencia inicial.'></span>
                    </td>
                    <td class="CamposFormularioContingencias">                        
                        <asp:DropDownList id="cboAutoridadAmbiental" runat="server" OnSelectedIndexChanged="cboAutoridadAmbiental_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="Seleccione." Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="rfvAutoridadAmbiental" ControlToValidate="cboAutoridadAmbiental" InitialValue="-1" ValidationGroup="FormularioContingencias" ErrorMessage="Debe seleccionar la autoridad ambiental.">*</asp:RequiredFieldValidator>                                                
                        <br />
                        <br />
                        <span class="NotaAlerta"><b>* Nota:</b> El diligenciamiento del presente reporte será notificado a la Autoridad Ambiental encargada de realizar seguimiento al instrumento ambiental, tenga en cuenta que si se trata de una licencia ambiental otorgada por la <b>ANLA</b>, está deberá ser la entidad seleccionada</span>
                        <br /><br />
                    </td>
                </tr>
                <tr id="trExpediente" runat="server">
                    <td class="LabelFormularioContingencias">
                        Número de Expediente:
                        <span id="spnExpediente" class="botonAyudaUP" title='Expediente al cual se relaciona el reporte de contingencias.'></span>
                    </td>
                    <td class="CamposFormularioContingencias">
                        <asp:DropDownList id="cboExpediente" runat="server" OnSelectedIndexChanged="cboExpediente_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="Seleccione." Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="rfvExpediente" ControlToValidate="cboExpediente" InitialValue="-1" ValidationGroup="FormularioContingencias" ErrorMessage="Debe seleccionar el expediente sobre el cual se realiza la contingencia.">*</asp:RequiredFieldValidator>
                        <asp:TextBox  runat="server" ID="txtExpedienteCorporacion" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                        <asp:CustomValidator runat="server" ID="cvExpedienteCorporacion" ValidationGroup="FormularioContingencias" ErrorMessage="Debe ingresar el número del expediente y/o el nombre del proyecto sobre el cual se realiza la contingencia." ClientValidationFunction="VerificarExpediente">*</asp:CustomValidator>
                    </td>
                </tr>

                <tr id="trNombreProyecto" runat="server">
                    <td class="LabelFormularioContingencias">
                        Nombre del Proyecto:
                        <span id="spnNombreProyecto" class="botonAyudaUP" title='Nombre del proyecto sobre el cual se presenta el reporte de contingencias.'></span>
                    </td>
                    <td class="CamposFormularioContingencias">
                        <asp:Literal runat="server" ID="ltlNombreProyecto"></asp:Literal>
                        <asp:TextBox  runat="server" ID="txtNombreProyectoCorporacion" MaxLength="2000" Width="95%" ClientIDMode="Static"></asp:TextBox>
                        <asp:CustomValidator runat="server" ID="cvNombreProyectoCorporacion" ValidationGroup="FormularioContingencias" ClientValidationFunction="VerificarExpediente">*</asp:CustomValidator>
                    </td>
                </tr>

                <tr id="trSector" runat="server">
                    <td class="LabelFormularioContingencias">
                        Sector Asociado:
                        <span id="spnSector" class="botonAyudaUP" title='Sector al cual pertenece el proyecto.'></span>
                    </td>
                    <td class="CamposFormularioContingencias">
                        <asp:DropDownList id="cboSector" runat="server" OnSelectedIndexChanged="cboSector_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="Seleccione." Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="rfvSector" ControlToValidate="cboSector" InitialValue="-1" ValidationGroup="FormularioContingencias" ErrorMessage="Debe seleccionar el sector al cual pertenece el proyecto.">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="trEtapaProyecto" runat="server">
                    <td class="LabelFormularioContingencias">
                        Etapa de proyecto:
                        <span id="spnEtapaProyecto" class="botonAyudaUP" title='Etapa en la cual se encuentra el proyecto sobre el cual se presenta la contingencia.'></span>
                    </td>
                    <td class="CamposFormularioContingencias">
                        <asp:DropDownList id="cboEtapaProyecto" runat="server" ClientIDMode="Static">
                            <asp:ListItem Text="Seleccione." Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="rfvEtapaProyecto" ControlToValidate="cboEtapaProyecto" InitialValue="-1" ValidationGroup="FormularioContingencias" ErrorMessage="Debe seleccionar la etapa del proyecto.">*</asp:RequiredFieldValidator>
                        <asp:TextBox runat="server" ID="txtEtapaProyectoOtro" MaxLength="100" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfvEtapaProyectoOtro" ClientIDMode="Static" ControlToValidate="txtEtapaProyectoOtro" ValidationGroup="FormularioContingencias" ErrorMessage="Debe ingresar cual es la etapa del proyecto.">*</asp:RequiredFieldValidator>   
                    </td>
                </tr>
                <tr id="trNivelEmergencia" runat="server">
                    <td class="LabelFormularioContingencias">
                        Nivel de Emergencia:
                        <span id="spnNivelEmergencia" class="botonAyudaUP" title='Nivel de la emergencia que se encuentra presentandose.'></span>
                    </td>
                    <td class="CamposFormularioContingencias">
                        <asp:DropDownList id="cboNivelEmergencia" runat="server">
                            <asp:ListItem Text="Seleccione." Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="rfvNivelEmergencia" ControlToValidate="cboNivelEmergencia" InitialValue="-1" ValidationGroup="FormularioContingencias" ErrorMessage="Debe seleccionar el nivel de emergencia que se esta presentando.">*</asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr id="trNombreResponsable" runat="server">
                    <td class="LabelFormularioContingencias">
                        Nombre Reponsable de Realizar el Reporte:
                        <span id="spnNombreResponsable" class="botonAyudaUP" title='Nombre de la persona responsable de la presentación del reporte.'></span>
                    </td>
                    <td class="CamposFormularioContingencias">
                        <asp:TextBox runat="server" ID="txtNombreResponsable" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfvNombreResponsable" ControlToValidate="txtNombreResponsable" ValidationGroup="FormularioContingencias" ErrorMessage="Debe ingresar el nombre del responsable de la presentación del reporte.">*</asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr id="trTelefonoResponsable" runat="server">
                    <td class="LabelFormularioContingencias">
                        Número Telefónico Responsable del Reporte:
                        <span id="spnTelefonoResponsable" class="botonAyudaUP" title='Número telefónico de la persona responsable de la presentación del reporte.'></span>
                    </td>
                    <td class="CamposFormularioContingencias">
                        <asp:TextBox runat="server" ID="txtTelefonoResponsable" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfvTelefonoResponsable" ControlToValidate="txtTelefonoResponsable" ValidationGroup="FormularioContingencias" ErrorMessage="Debe ingresar el número telefónico del responsable de la presentación del reporte.">*</asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr id="trEmailResponsable" runat="server">
                    <td class="LabelFormularioContingencias">
                        Correo Electrónico Responsable del Reporte:
                        <span id="spnEmailResponsable" class="botonAyudaUP" title='Correo electrónico de la persona responsable de la presentación del reporte.'></span>
                    </td>
                    <td class="CamposFormularioContingencias">
                        <asp:TextBox runat="server" ID="txtEmailResponsable" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfvEmailResponsable" ControlToValidate="txtEmailResponsable" ValidationGroup="FormularioContingencias" ErrorMessage="Debe ingresar el correo electrónico del responsable de la presentación del reporte.">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rexEmailResponsable" Display="Dynamic" runat="server" ControlToValidate="txtEmailResponsable" ValidationGroup="FormularioContingencias" ErrorMessage="Ingrese un correo electrónico valido." ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$">&nbsp;</asp:RegularExpressionValidator>
                    </td>
                </tr>
                
                
            </table>

            <!-- CUESTIONARIO -->
            <table runat="server" id="tblCuestionario" class="TablaCuestionariosContingencias">               
                <tr>
                    <td>
                        <asp:Repeater runat="server" ID="rptCuestionario" OnItemDataBound="rptCuestionario_ItemDataBound">
                            <ItemTemplate>                       
                                <table class="TablaSeccionContingencias">
                                    <tr>
                                        <td runat="server" id="tdSeccionVisible" visible='<%# Convert.ToInt32(Eval("SeccionID")) > 0 %>'>
                                            <div id="accordionSecciones">
                                                <div class="HeaderAccordionContingencias">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Literal runat="server" ID="ltlTituloSeccionCuestionario" Text='<%# Eval("Seccion") %>'></asp:Literal> 
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="BodyAccordeonContingencias">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <table class="TablaCuestionarioContingencias">                                                                    
                                                                    <asp:Repeater runat="server" ID="rptPreguntasCuestionario" OnItemDataBound="rptPreguntasCuestionario_ItemDataBound">
                                                                        <ItemTemplate>
                                                                            <tr id='<%# Eval("PreguntaID") %>'>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td class="Pregunta">
                                                                                                <asp:Literal runat="server" ID="ltlPregunta" Text='<%# Eval("Pregunta") %>'></asp:Literal>
                                                                                                <span runat="server" id="spnPregunta" class="botonAyudaUP" title='<%# Eval("AyudaPregunta") %>' visible='<%# Eval("AyudaPregunta").ToString() != "" %>'></span>
                                                                                                <asp:HiddenField runat="server" ID="hdfPreguntaID" Value='<%# Eval("PreguntaID") %>' />
                                                                                                <asp:HiddenField runat="server" ID="hdfTipoPregunta" Value='<%# Eval("TipoPregunta.TipoPreguntaID") %>' />
                                                                                                <asp:HiddenField runat="server" ID="hdfCondicionesMostrarPregunta"  />
                                                                                                <asp:HiddenField runat="server" ID="hdfPreguntaMostrada"  />
                                                                                                <asp:HiddenField runat="server" ID="hdfPreguntaObligatoria" Value='<%# (Convert.ToBoolean(Eval("Obligatorio")) ? "true" : "false") %>'  />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="Campos">
                                                                                                <table runat ="server" id="tblOpcionesUnicas" visible='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "1" %>'>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:CustomValidator runat="server" ID="cvOpcionUnica" ClientValidationFunction="ValidarListaRadio" ValidationGroup="FormularioContingencias" Enabled='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "1" %>' ErrorMessage='<%# "Debe selecccionar una opción en la pregunta \"" + Eval("Pregunta").ToString() + "\"" %>'>*</asp:CustomValidator>
                                                                                                            <asp:RadioButtonList runat="server" ID="rdOpcionUnica"></asp:RadioButtonList>                                                                                                                                                                                                
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:TextBox runat="server" ID="txtOpcionOtro" Width="30%" Visible='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "1" %>'></asp:TextBox>
                                                                                                            <asp:CustomValidator runat="server" ID="cvOpcionOtro" ClientValidationFunction="ValidarOpcionOtro" ValidationGroup="FormularioContingencias" Enabled='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "1" %>' ErrorMessage='<%# "Debe diligenciar el campo en la pregunta \"" + Eval("Pregunta").ToString() + "\"" %>'>*</asp:CustomValidator>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>

                                                                                                <table id="tblOpcionesUnicasAbiertas" style='display: <%# (Eval("TipoPregunta.TipoPreguntaID").ToString() == "1" ? "block" : "none") %>'>
                                                                                                    <asp:Repeater runat="server" ID="rptOpcionesPregunta" OnItemDataBound="rptOpcionesPregunta_ItemDataBound">
                                                                                                        <ItemTemplate>     
                                                                                                            <tr>
                                                                                                                <td class="LabelFormularioContingencias">
                                                                                                                    <asp:HiddenField runat="server" ID="hdfOpcionPreguntaID" Value='<%# Eval("OpcionPreguntaID") %>' />
                                                                                                                    <asp:Literal runat="server" ID="ltlOpcionTitulo" Text='<%# Eval("TextoOpcion") %>'></asp:Literal>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox runat="server" ID="txtOpcionTexto"></asp:TextBox> 
                                                                                                                    <asp:RequiredFieldValidator runat="server" ID="rfvOpcionTexto" ControlToValidate="txtOpcionTexto" ErrorMessage='<%# "Debe diligenciar el campo de la opción  \"" + Eval("TextoOpcion").ToString() + "\"" %>' ValidationGroup="FormularioContingencias">*</asp:RequiredFieldValidator>

                                                                                                                    <asp:TextBox runat="server" ID="txtOpcionTextoNumerico"></asp:TextBox> 
                                                                                                                    <asp:RequiredFieldValidator runat="server" ID="rfvOpcionTextoNumerico" ControlToValidate="txtOpcionTextoNumerico" ErrorMessage='<%# "Debe ingresar el valor para la opción  \"" + Eval("TextoOpcion").ToString() + "\"" %>' ValidationGroup="FormularioContingencias">*</asp:RequiredFieldValidator>
                                                                                                                </td>
                                                                                                            </tr>                                                                                       
                                                                                                        </ItemTemplate>
                                                                                                    </asp:Repeater>    
                                                                                                </table>

                                                                                                <table id="tblOpcionMiltiple" style='display: <%# (Eval("TipoPregunta.TipoPreguntaID").ToString() == "8" ? "block" : "none") %>'>
                                                                                                    <tr><td><asp:CustomValidator runat="server" ID="cvOpcionMultiple" ClientValidationFunction="ValidarListaMultiple" ValidationGroup="FormularioContingencias" Enabled='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "8" %>' ErrorMessage='<%# "Debe selecccionar una opción en la pregunta \"" + Eval("Pregunta").ToString() + "\"" %>'>*</asp:CustomValidator></td></tr>
                                                                                                    <asp:Repeater runat="server" ID="rptOpcioneMultiples" OnItemDataBound="rptOpcioneMultiples_ItemDataBound">
                                                                                                        <ItemTemplate> 
                                                                                                            <tr>
                                                                                                                <td class="Check">                                                                                                                    
                                                                                                                    <input type="checkbox" runat="server" id="rdOpcionMultiple" value='<%# Eval("OpcionPreguntaID") %>' />
                                                                                                                    <asp:Label runat="server" ID="lblOpcionMultiple" Text='<%# Eval("TextoOpcion") %>'></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>  
                                                                                                        </ItemTemplate>                                                                                                        
                                                                                                    </asp:Repeater>                                                                                                    
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:TextBox runat="server" ID="txtTextoOpcionMultiple" Width="30%" Visible='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "1" %>'></asp:TextBox>
                                                                                                            <asp:CustomValidator runat="server" ID="cvTextoOpcionMultiple" ClientValidationFunction="ValidarOpcionOtroMultiple" ValidationGroup="FormularioContingencias" Enabled='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "1" %>' ErrorMessage='<%# "Debe diligenciar el campo en la pregunta \"" + Eval("Pregunta").ToString() + "\"" %>'>*</asp:CustomValidator>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>

                                                                                                <asp:TextBox runat="server" ID="txtRespuestaPregunta" TextMode="MultiLine" Width="90%" Rows="3" Visible='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "2" %>'></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator runat="server" ID="rfvRespuestaPregunta" ControlToValidate="txtRespuestaPregunta" ErrorMessage='<%# "Debe diligenciar el campo de la pregunta \"" + Eval("Pregunta").ToString() + "\"" %>' ValidationGroup="FormularioContingencias" Enabled='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "2" %>' >*</asp:RequiredFieldValidator>


                                                                                                <asp:UpdatePanel runat="server" ID="upnlUbicacionPregunta">
                                                                                                    <ContentTemplate>
                                                                                                        <table runat ="server" id="tblUbicacionPregunta" visible='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "3" %>'>
                                                                                                            <tr>
                                                                                                                <td class="LabelFormularioContingencias">Departamento</td>
                                                                                                                <td>
                                                                                                                    <asp:DropDownList id="cboDepartamentoPregunta" runat="server" OnSelectedIndexChanged="cboDepartamentoPregunta_SelectedIndexChanged" AutoPostBack="true">
                                                                                                                        <asp:ListItem Text="Seleccione." Value="-1"></asp:ListItem>
                                                                                                                    </asp:DropDownList>
                                                                                                                    
                                                                                                                </td>
                                                                                                                <td class="LabelFormularioContingencias">Ciudad</td>
                                                                                                                <td>
                                                                                                                    <asp:DropDownList id="cboCiudadPregunta" runat="server">
                                                                                                                        <asp:ListItem Text="Seleccione." Value="-1"></asp:ListItem>
                                                                                                                    </asp:DropDownList>
                                                                                                                    
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </ContentTemplate>
                                                                                                    <Triggers>
                                                                                                        <asp:AsyncPostBackTrigger ControlID="cboDepartamentoPregunta" EventName="SelectedIndexChanged" />
                                                                                                    </Triggers>
                                                                                                </asp:UpdatePanel>
                                                                                                <asp:RequiredFieldValidator runat="server" ID="rfvDepartamentoPregunta" ControlToValidate="cboDepartamentoPregunta" InitialValue="-1" ErrorMessage='<%# "Debe seleccionar el departamento en la pregunta \"" + Eval("Pregunta").ToString() + "\"" %>' ValidationGroup="FormularioContingencias" Enabled='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "3" %>'>*</asp:RequiredFieldValidator>
                                                                                                <asp:RequiredFieldValidator runat="server" ID="rfvCiudadPregunta" ControlToValidate="cboCiudadPregunta" InitialValue="-1" ErrorMessage='<%# "Debe seleccionar la ciudad en la pregunta \"" + Eval("Pregunta").ToString() + "\"" %>' ValidationGroup="FormularioContingencias" Enabled='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "3" %>'>*</asp:RequiredFieldValidator>
                                                                                    

                                                                                                <table runat ="server" id="tblCoordenadasPregunta" visible='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "4" %>'>
                                                                                                    <tr>
                                                                                                        <td></td>
                                                                                                        <td class="TituloTabla">Grados</td>
                                                                                                        <td class="TituloTabla">Minutos</td>
                                                                                                        <td class="TituloTabla">Segundos</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="TituloTabla">Longitud</td>
                                                                                                        <td class="CampoTabla">
                                                                                                            <b>-</b>&nbsp;&nbsp;<asp:TextBox runat="server" ID="txtGradosLongitud"></asp:TextBox>
                                                                                                            <asp:RequiredFieldValidator runat="server" ID="rfvGradosLongitud" ControlToValidate="txtGradosLongitud" ErrorMessage='<%# "Debe ingresar los grados de la longitud en las coordenadas de la pregunta \"" + Eval("Pregunta").ToString() + "\"" %>' ValidationGroup="FormularioContingencias" Enabled='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "4" %>'>*</asp:RequiredFieldValidator>
                                                                                                        </td>
                                                                                                        <td class="CampoTabla">
                                                                                                            <asp:TextBox runat="server" ID="txtMinutosLongitud"></asp:TextBox>
                                                                                                            <asp:RequiredFieldValidator runat="server" ID="rfvMinutosLongitud" ControlToValidate="txtMinutosLongitud" ErrorMessage='<%# "Debe ingresar los minutos de la longitud en las coordenadas de la pregunta \"" + Eval("Pregunta").ToString() + "\"" %>' ValidationGroup="FormularioContingencias" Enabled='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "4" %>'>*</asp:RequiredFieldValidator>
                                                                                                        </td>
                                                                                                        <td class="CampoTabla">
                                                                                                            <asp:TextBox runat="server" ID="txtSegundosLongitud"></asp:TextBox>
                                                                                                            <asp:RequiredFieldValidator runat="server" ID="rfvSegundosLongitud" ControlToValidate="txtSegundosLongitud" ErrorMessage='<%# "Debe ingresar los segundos de la longitud en las coordenadas de la pregunta \"" + Eval("Pregunta").ToString() + "\"" %>' ValidationGroup="FormularioContingencias" Enabled='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "4" %>'>*</asp:RequiredFieldValidator>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="TituloTabla">Latitud</td>
                                                                                                        <td class="CampoTabla">
                                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox runat="server" ID="txtGradosLatitud"></asp:TextBox>
                                                                                                            <asp:RequiredFieldValidator runat="server" ID="rfvGradosLatitud" ControlToValidate="txtGradosLatitud" ErrorMessage='<%# "Debe ingresar los grados de la latitud en las coordenadas de la pregunta \"" + Eval("Pregunta").ToString() + "\"" %>' ValidationGroup="FormularioContingencias" Enabled='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "4" %>'>*</asp:RequiredFieldValidator>
                                                                                                        </td>
                                                                                                        <td class="CampoTabla">
                                                                                                            <asp:TextBox runat="server" ID="txtMinutosLatitud"></asp:TextBox>
                                                                                                            <asp:RequiredFieldValidator runat="server" ID="rfvMinutosLatitud" ControlToValidate="txtMinutosLatitud" ErrorMessage='<%# "Debe ingresar los minutos de la latitud en las coordenadas de la pregunta \"" + Eval("Pregunta").ToString() + "\"" %>' ValidationGroup="FormularioContingencias" Enabled='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "4" %>'>*</asp:RequiredFieldValidator>
                                                                                                        </td>
                                                                                                        <td class="CampoTabla">
                                                                                                            <asp:TextBox runat="server" ID="txtSegundosLatitud"></asp:TextBox>
                                                                                                            <asp:RequiredFieldValidator runat="server" ID="rfvSegundosLatitud" ControlToValidate="txtSegundosLatitud" ErrorMessage='<%# "Debe ingresar los segundos de la latitud en las coordenadas de la pregunta \"" + Eval("Pregunta").ToString() + "\"" %>' ValidationGroup="FormularioContingencias" Enabled='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "4" %>'>*</asp:RequiredFieldValidator>
                                                                                                        </td>
                                                                                                    </tr>                                                                                        
                                                                                                </table>                                                                                    
                                                                                                
                                                                                                <asp:FileUpload runat="server" ID="fuplDocumentoPregunta" Visible='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "5" %>' />
                                                                                                <asp:CustomValidator runat="server" ID="cvDocumentoPregunta" ClientValidationFunction="ValidarDocumento" ValidationGroup="FormularioContingencias" Enabled='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "5" %>' ErrorMessage='<%# "Debe seleccionar el archivo que corresponda para la pregunta \"" + Eval("Pregunta").ToString() + "\"" %>'>*</asp:CustomValidator>

                                                                                                <asp:TextBox runat="server" ID="txtFechaPregunta" Visible='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "6" %>'></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator runat="server" ID="rfvFechaPregunta" ControlToValidate="txtFechaPregunta" ErrorMessage='<%# "Debe ingresar la fecha en la pregunta \"" + Eval("Pregunta").ToString() + "\"" %>' ValidationGroup="FormularioContingencias" Enabled='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "6" %>'>*</asp:RequiredFieldValidator>

                                                                                                <table runat ="server" id="tblHora" visible='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "7" %>'>
                                                                                                    <tr>
                                                                                                        <td class="LabelFormularioContingencias">Hora</td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList id="cboHora" runat="server">
                                                                                                                <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                                                                                                <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                                                                                                <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                                                                                                <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                                                                                                <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                                                                                                <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                                                                                                <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                                                                                                <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                                                                                                <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                                                                                                <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                                                                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                                                                <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                                                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                                                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                                                                <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                                                                <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                                                                <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                                                                <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                                                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                                                                <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                                                                <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                                                                <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                            <asp:RequiredFieldValidator runat="server" ID="rfvHora" ControlToValidate="cboHora" InitialValue="-1" ErrorMessage='<%# "Debe ingresar la hora en la pregunta \"" + Eval("Pregunta").ToString() + "\"" %>' ValidationGroup="FormularioContingencias" Enabled='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "3" %>'>*</asp:RequiredFieldValidator>
                                                                                                        </td>
                                                                                                        <td class="LabelFormularioContingencias">Minutos</td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList id="cboMInutos" runat="server">
                                                                                                                <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                                                                    <asp:ListItem Text='24' Value='24'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='25' Value='25'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='26' Value='26'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='27' Value='27'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='28' Value='28'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='29' Value='29'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='30' Value='30'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='31' Value='31'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='32' Value='32'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='33' Value='33'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='34' Value='34'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='35' Value='35'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='36' Value='36'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='37' Value='37'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='38' Value='38'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='39' Value='39'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='40' Value='40'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='41' Value='41'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='42' Value='42'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='43' Value='43'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='44' Value='44'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='45' Value='45'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='46' Value='46'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='47' Value='47'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='48' Value='48'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='49' Value='49'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='50' Value='50'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='51' Value='51'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='52' Value='52'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='53' Value='53'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='54' Value='54'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='55' Value='55'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='56' Value='56'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='57' Value='57'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='58' Value='58'></asp:ListItem>
                                                                                                                    <asp:ListItem Text='59' Value='59'></asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                            <asp:RequiredFieldValidator runat="server" ID="rfvMInutos" ControlToValidate="cboMInutos" InitialValue="-1" ErrorMessage='<%# "Debe los minutos en la pregunta \"" + Eval("Pregunta").ToString() + "\"" %>' ValidationGroup="FormularioContingencias" Enabled='<%# Eval("TipoPregunta.TipoPreguntaID").ToString() == "3" %>'>*</asp:RequiredFieldValidator>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>                                                                            
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </table>
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
                    </td>
                </tr>
            </table>

            <table class="TablaBotonesFormularioContingencias">
                <tr>
                    <td>
                        <asp:Button runat="server" ID="cmdEnviar" ValidationGroup="FormularioContingencias" Text="Enviar" ClientIDMode="Static" OnClick="cmdEnviar_Click"/>
                        <%--<asp:Button runat="server" ID="cmdEnviar" CausesValidation="false" Text="Enviar" ClientIDMode="Static" OnClick="cmdEnviar_Click"/>--%>
                        <asp:ValidationSummary ID="valFormularioContingencias" runat="server" ValidationGroup="FormularioContingencias" ShowMessageBox="true" ShowSummary="false" />
                        <asp:HiddenField runat="server" ID="hdfSeleccionados" ClientIDMode="Static" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cboExpediente" EventName="SelectedIndexChanged" />                        
            <asp:AsyncPostBackTrigger ControlID="cboAutoridadAmbiental" EventName="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="cmdEnviar" />
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


    <input type="button" runat="server" id="cmdErrorProcesoHide" style="display:none;" />
    <cc1:ModalPopupExtender ID="mpeErrorProceso" runat="server" PopupControlID="dvErrorProceso" TargetControlID="cmdErrorProcesoHide" BehaviorID="mpeErrorProcesos" BackgroundCssClass="ModalBackgroundContingencias">
    </cc1:ModalPopupExtender>
    <div id="dvErrorProceso" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalContingencias">
        <asp:UpdatePanel runat="server" ID="upnlErrorProceso" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="TablaFormularioContingencias">
                    <tr>
                        <td colspan="2" class="TituloSeccionContingencias">
                            Solicitud / Información de Cambio Menor
                        </td>
                    </tr>
                    <tr>
                        <td class="ImagenesModalContingencias">
                            <asp:Image runat="server" ID="imgIconoErrorProceso" ImageUrl="~/images/error.png" />
                        </td>
                        <td class="TextoModalErrorContingencias">
                            <asp:Literal runat="server" ID="ltlErrorProceso"></asp:Literal>
                        </td>
                    </tr>                        
                </table>
                <table class="TablaBotonesFormularioContingencias">
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="cmdAceptarErrorProceso" CausesValidation="false" Text="Aceptar" ClientIDMode="Static" OnClick="cmdAceptarErrorProceso_Click"/>
                            <asp:Button runat="server" ID="cmdAceptarErrorProcesoSincronico" CausesValidation="false" Text="Aceptar" ClientIDMode="Static" OnClick="cmdAceptarErrorProceso_Click"/>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdAceptarErrorProceso" EventName="Click" />                                                        
                <asp:PostBackTrigger ControlID="cmdAceptarErrorProcesoSincronico" />
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
<style>
#ctl00_ContentPlaceHolder1_lblTitulo {
    font-family: "Work Sans", sans-serif !important;
    color: #064c82 !important;
    font-size: 1.6rem !important;
    font-weight: 600 !important;
}
</style>
<style>
#topHeader .container .row .col-md-3:nth-child(1) {
    display: none;
}
</style>
</asp:Content>
