var datosSerializado = "";
var valorParametroBusqueda = "";
var alertaReferenciado = "";
var valorTipoBusqueda = "";
var tempPrimeraPagina = 1;
var tempMaximoPagina = 0;
var limitePagina = 20;

$(document).ready(function () {
    $('#nav-InfoGeneral-tab').click(function () {
        $('#timeline').removeClass('roadmap--initialized');
    });
    $('#nav-EstadoTramite-tab').click(function () {
        //setTimeout(function () {
            $('#timeline').addClass('roadmap--initialized');
        //}, 1000);
        
    });
});

function mostrarMalla() {
    $("#divCargando").css("display", "block");
}
function ocultarMalla() {
    $("#divCargando").css("display", "none");
}

function ImprimirDetalle(datos) {
    try {

        $('#exTab2 ul li').each(function () {
            $(this).removeAttr('style');
        });
        $('#panelTabs div').each(function () {
            if (this.id == "0")
                $(this).removeAttr('style');
        });
        $('#exTab2 ul li').each(function () {
            if (this.id == "liPublicaciones")
                $(this).css("display", "none");
        });
        if (datos != null) {
            $("#InfoDetalle").show();
            //$('a[href="#0"]').click();
            $("#exTab2").css("display", "block");
            $("#exTab2").addClass('active');

            $("#lblExpedientesRelacionados").text("");
            $("#lblVitalSolicitud").text(datos[0].lblnumeroExpediente);
            $("#lblSolicitante").text(datos[0].lblSolicitante);
            $("#lblcodigo_expediente").text(datos[0].lblCodigoExpediente);
            $("#lblnombre_expediente").text(datos[0].lblNombreProyectoValue);
            $("#lbldescripcion_expediente").text(datos[0].lblDescripcionProyectoValue);
            $("#lblSector").text(datos[0].lblSector);
            $("#lblUbicacion").text(datos[0].lblUbicacion);
            $("#lblAutoridadAmbiental").text(datos[0].lblAutoridadAmbiental);

            var tempImagenOrigen = "";

            if (datos[0].lblAutoridadAmbiental == "ANLA") {
                tempImagenOrigen = "<img class='img-fluid' src='../App_Themes/Img/Autoridades/anla.png'/>";
            }
            else if (datos[0].lblAutoridadAmbiental == "" || datos[0].lblAutoridadAmbiental == "ANLA") {
                tempImagenOrigen = "<img class='img-fluid' src='../App_Themes/Img/Autoridades/anla.png'/>";
            }
            else if (datos[0].lblAutoridadAmbiental == "MADS")
                tempImagenOrigen = "<img class='img-fluid'  src='../App_Themes/Img/logoEntidad.png'/>";
            else if (datos[0].lblAutoridadAmbiental != "")
                tempImagenOrigen = "<img class='img-fluid' src='../App_Themes/Img/Autoridades/" + datos[0].lblAutoridadAmbiental + ".jpg'/>";
            else if (datos[0].lblAutoridadAmbiental == "VITAL")
                tempImagenOrigen = "<img class='img-fluid' src='../App_Themes/Img/LogoVital.png'/>";
            else if (datos[0].lblAutoridadAmbiental == "SILA" || datos[0].lblAutoridadAmbiental == "SILAMC")
                tempImagenOrigen = "<img class='img-fluid' src='../App_Themes/Img/Autoridades/anla.png'/>";



            if (datos[0].lblTramite != undefined) {
                var tramEstado = "";
                $("#lblTramite").css("display", "block");
                $("#lblTramiteValue").css("display", "block");
                if (datos[0].lblTramiteEstado != undefined) {
                    tramEstado = datos[0].lblTramiteEstado;
                }
                $("#lblTramiteValue").text(datos[0].lblTramite + "::" + tramEstado);
            }
            if (datos[0].lstExpedientes != null) {
                for (var i = 0; i < datos[0].lstExpedientes.length; i++) {
                    $("#lblExpedientesRelacionados").append(datos[0].lstExpedientes[i] + ", ")
                }
            }
            else if (datos[0].lstExpedientesAsociados != null) {
                for (var i = 0; i < datos[0].lstExpedientesAsociados.length; i++) {
                    $("#lblExpedientesRelacionados").append(datos[0].lstExpedientesAsociados[i] + ", ");
                }
            }

            var tabs = $("#exTab2");
            var panelTabs = $("#panelTabs");
            var ul = tabs.find("ul");
            var divImagenAut = $("#imgAutoridad");
            divImagenAut.append(tempImagenOrigen);

            $('#exTab2 ul li').each(function () {
                if ($(this)[0].id != "aInfGeneral" && $(this)[0].id != "aEstadoTramite") {
                    $("#div" + $(this)[0].id.substring(2, 50)).empty();
                    $("#div" + $(this)[0].id.substring(2, 50)).remove();
                    $("#tbl" + $(this)[0].id.substring(2, 50)).remove();
                    $(this).remove();
                }
            });

            if (datos[0].lstResumenEstado != null) {


                if (datos[0].lstResumenEstado.resumenTramite != undefined && datos[0].lstResumenEstado.resumenTramite.length > 0) {
                    if (datos[0].lstResumenEstado.resumenTramite[0].id != "") {
                        //var listaNodos = llenarArrayInfo("0", datos[0].lstResumenEstado.resumenTramite);
                        var listaNodos = generateEvents(datos[0].lstResumenEstado.resumenTramite);

                        $('#timeline').roadmap(listaNodos, {
                            eventsPerSlide: 6,
                            slide: 1,
                            prevArrow: '<i class="material-icons">keyboard_arrow_left</i>',
                            nextArrow: '<i class="material-icons">keyboard_arrow_right</i>',
                            onBuild: function () {
                                console.log('onBuild event')
                            }
                        });
                    }
                }
                else {
                    var divtimeline = $("#timeline");
                    divtimeline.addClass("titulo_pagina");
                    var sinDatosTimeLine = "<label> En este momento no se encuetra disponible la información del estado de su trámite </label>";
                    divtimeline.append(sinDatosTimeLine);
                }
            }
            else {
                var divtimeline = $("#timeline");
                divtimeline.addClass("titulo_pagina");
                var sinDatosTimeLine = "<label> No se encuetra disponible la informacion del estado de su trámite </label>";
                divtimeline.append(sinDatosTimeLine);
            }

            if (datos[0].lstEtapas != undefined) {
                
                for (var i = 0; i < datos[0].lstEtapas.length; i++) {
                    if ($("#li" + datos[0].lstEtapas[i].IdEtapa).length == 0) {
                        
                        if (datos[0].lstEtapas[i].DtEtapa.length > 0) {
                            $("<li class='nav-item' id=li" + datos[0].lstEtapas[i].IdEtapa + "><a class='nav-link' data-bs-toggle='tab' rol='tab' id='nav-" + datos[0].lstEtapas[i].IdEtapa + "-tab' aria-selected='true' data-bs-target='#tab" + datos[0].lstEtapas[i].IdEtapa + "' aria-controls='nav-" + datos[0].lstEtapas[i].IdEtapa + "'>" + datos[0].lstEtapas[i].NombreEtapa + "</a></li>").appendTo(ul);
                            $("<div class='tab-pane' id='tab" + datos[0].lstEtapas[i].IdEtapa + "' role='tabpanel' aria-labelledby='nav-" + datos[0].lstEtapas[i].IdEtapa + "-tab'></div>").appendTo(panelTabs);
                            if (datos[0].lstEtapas[i].DtEtapa != undefined) {
                                if ($("#tbl" + datos[0].lstEtapas[i].IdEtapa).length == 0) {
                                    $("#tab" + datos[0].lstEtapas[i].IdEtapa).append("<div class='col-12 content-datos'><table class='tabla_datos' id='tbl" + datos[0].lstEtapas[i].IdEtapa + "'style='text-align: center; width:100%'><thead><tr>" +
                                        "<th class='headerDatos' style='width:10%;'><label class='label-sm'>ETAPA</label></th>" +
                                        "<th class='headerDatos' style='width:10%;'><label class='label-sm'>NUMERO</label></th>" +
                                        "<th class='headerDatos' style='width:10%;'><label class='label-sm'>FECHA</label></th>" +
                                        "<th class='headerDatos' style='width:30%;'><label class='label-sm'>TIPO</label></th>" +
                                        "<th class='headerDatos' style='width:30%;'><label class='label-sm'>DESCRIPCIÓN</label></th>" +
                                        "<th class='headerDatos' style='width:10%;'><label class='label-sm'>DOCUMENTOS</label></th></tr></thead></table></div><div id='paging_tbl" + datos[0].lstEtapas[i].IdEtapa + "'></div>");
                                }
                            }
                        }

                    }
                    for (var j = 0; j < datos[0].lstEtapas[i].DtEtapa.length; j++) {
                        if (datos[0].lstEtapas[i].DtEtapa[j].Documento != "Oficio - Memorando Interno" && datos[0].lstEtapas[i].DtEtapa[j].Documento != "Oficio - INT. Oficio Informa Visita") {
                            ////////// SE INCLUYE ESTA VALIDACION DATOS[0].LSTETAPAS[I].DTETAPA[J].FECHANOTIFICACION.LENGTH, SI VIENE ES POSIBLE VER EL DOCUMENTO.
                            if (datos[0].lstEtapas[i].DtEtapa[j].FechaNotificacion.length > 0 && (datos[0].lstEtapas[i].DtEtapa[j].TipoActoAdministrativo == 1 || datos[0].lstEtapas[i].DtEtapa[j].TipoActoAdministrativo == 2)) {
                                datosSerializado = JSON.stringify(datos[0].lstEtapas[i].DtEtapa[j]);
                                $("#tbl" + datos[0].lstEtapas[i].IdEtapa).append('<tr><td class="rowDatos" style="text-align: center;"><label class="label-xsm">' + datos[0].lstEtapas[i].DtEtapa[j].EtapaNombre + '</label></td>' +
                                    '<td class="rowDatos"><label class="label-xsm">' + datos[0].lstEtapas[i].DtEtapa[j].Numero + '</label></td>' +
                                    '<td class="rowDatos"><label class="label-xsm">' + datos[0].lstEtapas[i].DtEtapa[j].Fecha + '</label></td>' +
                                    '<td class="rowDatos"><label class="label-xsm">' + datos[0].lstEtapas[i].DtEtapa[j].TipoObjeto + '</label></td>' +
                                    '<td class="rowDatos"><label class="label-xsm">' + datos[0].lstEtapas[i].DtEtapa[j].Descripcion + '</label></td>' +
                                    "<td class='rowDatos'><a href='#' title='Ver Documentos'  class='label-xsm' onclick=\'MostrarDocumentos(" + this.datosSerializado + ")'\><img src='../App_Themes/Img/documentos.png'></a></td></tr>");
                            }
                            else if (datos[0].lstEtapas[i].DtEtapa[j].TipoActoAdministrativo == 3 || datos[0].lstEtapas[i].DtEtapa[j].TipoActoAdministrativo == 4 || datos[0].lstEtapas[i].DtEtapa[j].TipoActoAdministrativo == 5) {
                                datosSerializado = JSON.stringify(datos[0].lstEtapas[i].DtEtapa[j]);
                                $("#tbl" + datos[0].lstEtapas[i].IdEtapa).append('<tr><td class="rowDatos"><label class="label-xsm">' + datos[0].lstEtapas[i].DtEtapa[j].EtapaNombre + '</label></td>' +
                                    '<td class="rowDatos"><label class="label-xsm">' + datos[0].lstEtapas[i].DtEtapa[j].Numero + '</label></td>' +
                                    '<td class="rowDatos"><label class="label-xsm">' + datos[0].lstEtapas[i].DtEtapa[j].Fecha + '</label></td>' +
                                    '<td class="rowDatos"><label class="label-xsm">' + datos[0].lstEtapas[i].DtEtapa[j].TipoObjeto + '</label></td>' +
                                    '<td class="rowDatos" style="width:180px;"><label class="label-xsm">' + datos[0].lstEtapas[i].DtEtapa[j].Descripcion + '</label></td>' +
                                    "<td class='rowDatos'><a href='#' title='Ver Documentos'  class='label-xsm' onclick=\'MostrarDocumentos(" + this.datosSerializado + ")'\><img src='../App_Themes/Img/documentos.png'></a></td></tr>");
                            }
                        }
                    }
                    if (datos[0].lstEtapas[i].DtEtapa.length > limitePagina)
                    {
                        $("#tbl" + datos[0].lstEtapas[i].IdEtapa).datatable({
                            pageSize: limitePagina,
                            firstPage: false,
                            lastPage: false,
                            prevPage: "Anterior",
                            nextPage: "Siguiente",
                            pagingDivClass:"text-left",
                            loadingDivSelector: "divCargando",
                            pagingDivSelector: "#paging_tbl" + datos[0].lstEtapas[i].IdEtapa
                        });
                    }
                }
            }



            if (datos[0].lsDocumentosSeguimiento != null && datos[0].lsDocumentosSeguimiento.length > 0) {
                $("<li class='nav-item' id=liSeguimiento" + "><a class='nav-link' data-bs-toggle='tab'  rol='tab' id='nav-Seguimiento-tab' aria-selected='true' data-bs-target='#divSeguimiento' aria-controls='nav-Seguimiento-tab'>Solicitud</a></li>").appendTo(ul);
                $("<div class='tab-pane' id='divSeguimiento' role='tabpanel' aria-labelledby='nav-Seguimiento-tab'></div>").appendTo(panelTabs);
                $("#divSeguimiento").append("<div class='col-12 content-datos'><table class='tabla_datos' id='tblSeguimiento' style='text-align: center; width:100%'><thead><tr>" +
                    "<th class='headerDatos' style='width:15%;'><label class='label-sm'>TIPO DE USUARIO</label></th>" +
                    "<th class='headerDatos' style='width:15%;'><label class='label-sm'>FECHA SOLICITUD</label></th>" +
                    "<th class='headerDatos' style='width:20%;'><label class='label-sm'>DESCRIPCIÓN</label></th>" +
                    "<th class='headerDatos' style='width:25%;'><label class='label-sm'>DOCUMENTOS</label></th>" +
                    "<th class='headerDatos' style='width:10%;'><label class='label-sm'>EXPEDIENTE</label></th></tr></thead></table></div><div id='paging_tblSeguimiento'></div>");
                for (var i = 0; i < datos[0].lsDocumentosSeguimiento.length; i++) {
                    datosSerializado = JSON.stringify(datos[0].lsDocumentosSeguimiento[i]);
                    $("#tblSeguimiento").append("<tr><td class='rowDatos'>" +
                        "<img src='" + datos[0].lsDocumentosSeguimiento[i].lblIdParticipant + "'></td>" +
                        "<td class='rowDatos'><label class='label-xsm'>" + datos[0].lsDocumentosSeguimiento[i].lblSolFechaCreacion + "</label></td>" +
                        "<td class='rowDatos'><label class='label-xsm'>" + datos[0].lsDocumentosSeguimiento[i].lblDocumento + "</label></td>" +
                        "<td class='rowDatos'><a href='#' title='Ver Documentos'  class='label-xsm' onclick=\'MostrarDocumentos(" + this.datosSerializado + ")'\><img src='../App_Themes/Img/documentos.png'></a></td>" +
                        "<td class='rowDatos'><label class='label-xsm'>" + datos[0].lsDocumentosSeguimiento[i].lblExpediente + "</label></td>");
                }
                if (datos[0].lsDocumentosSeguimiento.length > limitePagina)
                {
                    $("#tbltbltblSeguimiento").datatable({
                        pageSize: limitePagina,
                        firstPage: false,
                        lastPage: false,
                        prevPage: "Anterior",
                        nextPage: "Siguiente",
                        pagingDivClass: "text-left",
                        loadingDivSelector: "divCargando",
                        pagingDivSelector: "#paging_tblSeguimiento"
                    });
                }
            }

            if (datos[0].lsDocumentosEvaluacion != null && datos[0].lsDocumentosEvaluacion.length > 0) {
                $("<li class='nav-item' id=liEvaluacion" + "><a class='nav-link' data-bs-toggle='tab'  rol='tab' id='nav-Evaluacion-tab' aria-selected='true' data-bs-target='#divEvaluacion' aria-controls='nav-Evaluacion-tab'>Evaluacion</a></li>").appendTo(ul);
                $("<div class='tab-pane' id='divEvaluacion' role='tabpanel' aria-labelledby='nav-Evaluacion-tab'></div>").appendTo(panelTabs);
                $("#divEvaluacion").append("<div class='col-12 content-datos'><table class='tabla_datos' id='tblEvaluacion' style='text-align: center; width:100%'><thead><tr>" +
                    "<th class='headerDatos' style='width:15%;'><label class='label-sm'>TIPO DE USUARIO</label></th>" +
                    "<th class='headerDatos' style='width:15%;'><label class='label-sm'>FECHA SOLICITUD</label></th>" +
                    "<th class='headerDatos' style='width:20%;'><label class='label-sm'>DESCRIPCIÓN</label></th>" +
                    "<th class='headerDatos' style='width:25%;'><label class='label-sm'>DOCUMENTOS</label></th>" +
                    "<th class='headerDatos' style='width:10%;'><label class='label-sm'>EXPEDIENTE</label></th></tr></thead></table></div><div id='paging_tblEvaluacion'></div>");
                for (var i = 0; i < datos[0].lsDocumentosEvaluacion.length; i++) {
                    datosSerializado = JSON.stringify(datos[0].lsDocumentosEvaluacion[i]);
                    $("#tblEvaluacion").append("<tr><td class='rowDatos'>" +
                        "<img src='" + datos[0].lsDocumentosEvaluacion[i].lblIdParticipant + "'></td>" +
                        "<td class='rowDatos'><label class='label-xsm'>" + datos[0].lsDocumentosEvaluacion[i].lblSolFechaCreacion + "</label></td>" +
                        "<td class='rowDatos'><label class='label-xsm'>" + datos[0].lsDocumentosEvaluacion[i].lblDocumento + "</label></td>" +
                        "<td class='rowDatos'><a href='#' title='Ver Documentos'  class='label-xsm' onclick=\'MostrarDocumentos(" + this.datosSerializado + ")'\><img src='../App_Themes/Img/documentos.png'></a></td>" +
                        "<td class='rowDatos'><label class='label-xsm'>" + datos[0].lsDocumentosEvaluacion[i].lblExpediente + "</label></td>");
                }
                if (datos[0].lsDocumentosEvaluacion.length > limitePagina)
                {
                    $("#tblEvaluacion").datatable({
                        pageSize: limitePagina,
                        firstPage: false,
                        lastPage: false,
                        prevPage: "Anterior",
                        nextPage: "Siguiente",
                        pagingDivClass: "text-left",
                        loadingDivSelector: "divCargando",
                        pagingDivSelector: "#paging_tblEvaluacion"
                    });
                }
            }

            if (datos[0].lsDocumentosInvestigacion != null && datos[0].lsDocumentosInvestigacion.length > 0) {
                $("<li class='nav-item' id=liInvestigacion" + "><a class='nav-link' data-bs-toggle='tab'  rol='tab' id='nav-Investigacion-tab' aria-selected='true' data-bs-target='#divInvestigacion' aria-controls='nav-Investigacion-tab'>Investigacion</a></li>").appendTo(ul);
                $("<div class='tab-pane' id='divInvestigacion' role='tabpanel' aria-labelledby='nav-Investigacion-tab'></div>").appendTo(panelTabs);
                $("#divInvestigacion").append("<div class='col-12 content-datos'><table class='tabla_datos' id='tblInvestigacion' style='text-align: center; width:100%'><thead><tr>" +
                    "<th class='headerDatos' style='width:15%;'><label class='label-sm'>TIPO DE USUARIO</label></th>" +
                    "<th class='headerDatos' style='width:15%;'><label class='label-sm'>FECHA SOLICITUD</label></th>" +
                    "<th class='headerDatos' style='width:20%;'><label class='label-sm'>DESCRIPCIÓN</label></th>" +
                    "<th class='headerDatos' style='width:25%;'><label class='label-sm'>DOCUMENTOS</label></th>" +
                    "<th class='headerDatos' style='width:10%;'><label class='label-sm'>EXPEDIENTE</label></th></tr></thead></table></div><div id='paging_tblInvestigacion'></div>");
                for (var i = 0; i < datos[0].lsDocumentosInvestigacion.length; i++) {
                    datosSerializado = JSON.stringify(datos[0].lsDocumentosInvestigacion[i]);
                    $("#tblInvestigacion").append('<tr><td class="rowDatos">' +
                        '<img src="' + datos[0].lsDocumentosInvestigacion[i].lblIdParticipant + '"></td>' +
                        '<td class="rowDatos"><label class="label-xsm">' + datos[0].lsDocumentosInvestigacion[i].lblSolFechaCreacion + '</label></td>' +
                        '<td class="rowDatos"><label class="label-xsm">' + datos[0].lsDocumentosInvestigacion[i].lblDocumento + '</label></td>' +
                        "<td class='rowDatos'><a href='#' title='Ver Documentos'  class='label-xsm' onclick=\'MostrarDocumentos(" + this.datosSerializado + ")'\><img src='../App_Themes/Img/documentos.png'></a></td>" +
                        '<td class="rowDatos"><label class="label-xsm">' + datos[0].lsDocumentosInvestigacion[i].lblExpediente + '</label></td>');
                }
                if (datos[0].lsDocumentosInvestigacion.length > limitePagina) {
                    $("#tblInvestigacion").datatable({
                        pageSize: limitePagina,
                        firstPage: false,
                        lastPage: false,
                        prevPage: "Anterior",
                        nextPage: "Siguiente",
                        pagingDivClass: "text-left",
                        loadingDivSelector: "divCargando",
                        pagingDivSelector: "#paging_tblInvestigacion"
                    });
                }
            }

            if (datos[0].lsDocumentosCobros != null && datos[0].lsDocumentosCobros.length > 0) {
                $("<li class='nav-item' id=liCobros" + "><a class='nav-link' data-bs-toggle='tab'  rol='tab' id='nav-Cobros-tab' aria-selected='true' data-bs-target='#divCobros' aria-controls='nav-Cobros-tab'>Cobros</a></li>").appendTo(ul);
                $("<div class='tab-pane' id='divCobros' role='tabpanel' aria-labelledby='nav-Cobros-tab'></div>").appendTo(panelTabs);
                $("#divCobros").append("<div class='col-12 content-datos'><table class='tabla_datos' id='tblCobros' style='text-align: center; width:100%'><thead><tr>" +
                    "<th class='headerDatos' style='width:15%;'><label class='label-sm'>TIPO DE USUARIO</label></th>" +
                    "<th class='headerDatos' style='width:15%;'><label class='label-sm'>FECHA SOLICITUD</label></th>" +
                    "<th class='headerDatos' style='width:20%;'><label class='label-sm'>DESCRIPCIÓN</label></th>" +
                    "<th class='headerDatos' style='width:25%;'><label class='label-sm'>DOCUMENTOS</label></th>" +
                    "<th class='headerDatos' style='width:10%;'><label class='label-sm'>EXPEDIENTE</label></th></tr></thead></table></div><div id='paging_tblCobros'></div>");
                for (var i = 0; i < datos[0].lsDocumentosCobros.length; i++) {
                    datosSerializado = JSON.stringify(datos[0].lsDocumentosCobros[i]);
                    $("#tblCobros").append('<tr><td class="rowDatos">' +
                        '<img src="' + datos[0].lsDocumentosCobros[i].lblIdParticipant + '"></td>' +
                        '<td class="rowDatos"><label class="label-xsm">' + datos[0].lsDocumentosCobros[i].lblSolFechaCreacion + '</label></td>' +
                        '<td class="rowDatos"><label class="label-xsm">' + datos[0].lsDocumentosCobros[i].lblDocumento + '</label></td>' +
                        "<td class='rowDatos'><a href='#' title='Ver Documentos'  class='label-xsm' onclick=\'MostrarDocumentos(" + this.datosSerializado + ")'\><img src='../App_Themes/Img/documentos.png'></a></td>" +
                        '<td class="rowDatos"><label class="label-xsm">' + datos[0].lsDocumentosCobros[i].lblExpediente + '</label></td>');
                }
                if (datos[0].lsDocumentosCobros.length > limitePagina) {
                    $("#tblCobros").datatable({
                        pageSize: limitePagina,
                        firstPage: false,
                        lastPage: false,
                        prevPage: "Anterior",
                        nextPage: "Siguiente",
                        pagingDivClass: "text-left",
                        loadingDivSelector: "divCargando",
                        pagingDivSelector: "#paging_tblCobros"
                    });
                }
            }

            if (datos[0].lsDocumentosOtros != null && datos[0].lsDocumentosOtros.length > 0) {
                $("<li class='nav-item' id=liOtros" + "><a class='nav-link' data-bs-toggle='tab'  rol='tab' id='nav-Otros-tab' aria-selected='true' data-bs-target='#divOtros' aria-controls='nav-Otros-tab'>Otros</a></li>").appendTo(ul);
                $("<div class='tab-pane' id='divOtros' role='tabpanel' aria-labelledby='nav-Otros-tab'></div>").appendTo(panelTabs);
                $("#divOtros").append("<div class='col-12 content-datos'><table class='tabla_datos' id='tblOtros' style='text-align: center; width:100%'><thead><tr>" +
                    "<th class='headerDatos' style='width:15%;'><label class='label-sm'>TIPO DE USUARIO</label></th>" +
                    "<th class='headerDatos' style='width:15%;'><label class='label-sm'>FECHA SOLICITUD</label></th>" +
                    "<th class='headerDatos' style='width:20%;'><label class='label-sm'>DESCRIPCIÓN</label></th>" +
                    "<th class='headerDatos' style='width:25%;'><label class='label-sm'>DOCUMENTOS</label></th>" +
                    "<th class='headerDatos' style='width:10%;'><label class='label-sm'>EXPEDIENTE</label></th></tr></thead></table></div><div id='paging_tblOtros'></div>");
                for (var i = 0; i < datos[0].lsDocumentosOtros.length; i++) {
                    datosSerializado = JSON.stringify(datos[0].lsDocumentosOtros[i]);
                    $("#tblOtros").append('<tr><td class="rowDatos">' +
                        '<img src="' + datos[0].lsDocumentosOtros[i].lblIdParticipant + '"></td>' +
                        '<td class="rowDatos"><label class="label-xsm">' + datos[0].lsDocumentosOtros[i].lblSolFechaCreacion + '</label></td>' +
                        '<td class="rowDatos"><label class="label-xsm">' + datos[0].lsDocumentosOtros[i].lblDocumento + '</label></td>' +
                        "<td class='rowDatos'><a href='#' title='Ver Documentos'  class='label-xsm' onclick=\'MostrarDocumentos(" + this.datosSerializado + ")'\><img src='../App_Themes/Img/documentos.png'></a></td>" +
                        '<td class="rowDatos"><label class="label-xsm">' + datos[0].lsDocumentosOtros[i].lblExpediente + '</label></td>');
                }
                if (datos[0].lsDocumentosOtros.length > limitePagina) {
                    $("#tblOtros").datatable({
                        pageSize: limitePagina,
                        firstPage: false,
                        lastPage: false,
                        prevPage: "Anterior",
                        nextPage: "Siguiente",
                        pagingDivClass: "text-left",
                        loadingDivSelector: "divCargando",
                        pagingDivSelector: "#paging_tblOtros"
                    });
                }
            }

            if (datos[0].lsDocumentosRepocision != null && datos[0].lsDocumentosRepocision.length > 0) {
                $("<li class='nav-item' id=liRepocision" + "><a class='nav-link' data-bs-toggle='tab'  rol='tab' id='nav-Repocision-tab' aria-selected='true' data-bs-target='#divRepocision' aria-controls='nav-Repocision-tab'>Reposición</a></li>").appendTo(ul);
                $("<div class='tab-pane' id='divRepocision' role='tabpanel' aria-labelledby='nav-Repocision-tab'></div>").appendTo(panelTabs);
                $("#divRepocision").append("<div class='col-12 content-datos'><table class='tabla_datos' id='tblRepocision' style='text-align: center; width:100%'><thead><tr>" +
                    "<th class='headerDatos' style='width:15%;'><label class='label-sm'>TIPO DE USUARIO</label></th>" +
                    "<th class='headerDatos' style='width:15%;'><label class='label-sm'>FECHA SOLICITUD</label></th>" +
                    "<th class='headerDatos' style='width:20%;'><label class='label-sm'>DESCRIPCIÓN</label></th>" +
                    "<th class='headerDatos' style='width:25%;'><label class='label-sm'>DOCUMENTOS</label></th>" +
                    "<th class='headerDatos' style='width:10%;'><label class='label-sm'>EXPEDIENTE</label></th></tr></thead></table></div><div id='paging_tblRepocision'></div>");
                for (var i = 0; i < datos[0].lsDocumentosRepocision.length; i++) {
                    datosSerializado = JSON.stringify(datos[0].lsDocumentosRepocision[i]);
                    $("#tblRepocision").append('<tr><td class="rowDatos">' +
                        '<img src="' + datos[0].lsDocumentosRepocision[i].lblIdParticipant + '"></td>' +
                        '<td class="rowDatos"><label class="label-xsm">' + datos[0].lsDocumentosRepocision[i].lblSolFechaCreacion + '</label></td>' +
                        '<td class="rowDatos"><label class="label-xsm">' + datos[0].lsDocumentosRepocision[i].lblDocumento + '</label></td>' +
                        "<td class='rowDatos'><a href='#' title='Ver Documentos'  class='label-xsm' onclick=\'MostrarDocumentos(" + this.datosSerializado + ")'\><img src='../App_Themes/Img/documentos.png'></a></td>" +
                        '<td class="rowDatos"><label class="label-xsm">' + datos[0].lsDocumentosRepocision[i].lblExpediente + '</label></td>');
                }
                if (datos[0].lsDocumentosRepocision.length > limitePagina) {
                    $("#tblRepocision").datatable({
                        pageSize: limitePagina,
                        firstPage: false,
                        lastPage: false,
                        prevPage: "Anterior",
                        nextPage: "Siguiente",
                        pagingDivClass: "text-left",
                        loadingDivSelector: "divCargando",
                        pagingDivSelector: "#paging_tblRepocision"
                    });
                }
            }

            if (datos[0].lsDocumentosModificacion != null && datos[0].lsDocumentosModificacion.length > 0) {
                $("<li class='nav-item' id=liModificacion" + "><a class='nav-link' data-bs-toggle='tab'  rol='tab' id='nav-Modificacion-tab' aria-selected='true' data-bs-target='#divModificacion' aria-controls='nav-Modificacion-tab'>Modificacion</a></li>").appendTo(ul);
                $("<div class='tab-pane' id='divModificacion' role='tabpanel' aria-labelledby='nav-Modificacion-tab'></div>").appendTo(panelTabs);
                $("#divModificacion").append("<div class='col-12 content-datos'><table class='tabla_datos' id='tblModificacion' style='text-align: center; width:100%'><thead><tr>" +
                    "<th class='headerDatos' style='width:15%;'><label class='label-sm'>TIPO DE USUARIO</label></th>" +
                    "<th class='headerDatos' style='width:15%;'><label class='label-sm'>FECHA SOLICITUD</label></th>" +
                    "<th class='headerDatos' style='width:20%;'><label class='label-sm'>DESCRIPCIÓN</label></th>" +
                    "<th class='headerDatos' style='width:25%;'><label class='label-sm'>DOCUMENTOS</label></th>" +
                    "<th class='headerDatos' style='width:10%;'><label class='label-sm'>EXPEDIENTE</label></th></tr></thead></table></div><div id='paging_tblModificacion'></div>");
                for (var i = 0; i < datos[0].lsDocumentosModificacion.length; i++) {
                    datosSerializado = JSON.stringify(datos[0].lsDocumentosModificacion[i]);
                    $("#tblModificacion").append('<tr><td class="rowDatos">' +
                        '<img src="' + datos[0].lsDocumentosModificacion[i].lblIdParticipant + '"></td>' +
                        '<td class="rowDatos"><label class="label-xsm">' + datos[0].lsDocumentosModificacion[i].lblSolFechaCreacion + '</label></td>' +
                        '<td class="rowDatos"><label class="label-xsm">' + datos[0].lsDocumentosModificacion[i].lblDocumento + '</label></td>' +
                        "<td class='rowDatos'><a href='#' title='Ver Documentos'  class='label-xsm' onclick=\'MostrarDocumentos(" + this.datosSerializado + ")'\><img src='../App_Themes/Img/documentos.png'></a></td>" +
                        '<td class="rowDatos"><label class="label-xsm">' + datos[0].lsDocumentosModificacion[i].lblExpediente + '</label></td>');
                }
                if (datos[0].lsDocumentosModificacion.length > limitePagina) {
                    $("#tblModificacion").datatable({
                        pageSize: limitePagina,
                        firstPage: false,
                        lastPage: false,
                        prevPage: "Anterior",
                        nextPage: "Siguiente",
                        pagingDivClass: "text-left",
                        loadingDivSelector: "divCargando",
                        pagingDivSelector: "#paging_tblModificacion"
                    });
                }
            }

            if (datos[0].lstArchivosForest != null && datos[0].lstArchivosForest.length > 0) {
                $("<li class='nav-item' id=liArchivosForest" + "><a class='nav-link' data-bs-toggle='tab'  rol='tab' id='nav-ArchivosForest-tab' aria-selected='true' data-bs-target='#divArchivosForest' aria-controls='nav-ArchivosForest-tab'>Correspondencia</a></li>").appendTo(ul);
                $("<div class='tab-pane' id='divArchivosForest' role='tabpanel' aria-labelledby='nav-divArchivosForest-tab'></div>").appendTo(panelTabs);
                $("#divArchivosForest").append("<div class='col-12 content-datos'><table class='tabla_datos' id='tblArchivosForest' style='text-align: center; width:100%'><thead><tr>" +
                    "<th class='headerDatos' style='width:15%;'><label class='label-sm'>NUMERO RADICACION</label></th>" +
                    "<th class='headerDatos' style='width:15%;'><label class='label-sm'>Fecha de Radicacion</label></th>" +
                    "<th class='headerDatos' style='width:20%;'><label class='label-sm'>Fecha de creación</label></th>" +
                    "<th class='headerDatos' style='width:25%;'><label class='label-sm'>Archivo</label></th>" +
                    "<th class='headerDatos' style='width:10%;'><label class='label-sm'>VER DOCUMENTOS</label></th></tr></thead></table></div><div id='paging_tblArchivosForest'></div>");
                for (var i = 0; i < datos[0].lstArchivosForest.length; i++) {
                    if (datos[0].lstArchivosForest[i].lblSol_Numero !== "2019003629-1-000" &&
                        datos[0].lstArchivosForest[i].lblSol_Numero !== "2019051859-1-000" &&
                        datos[0].lstArchivosForest[i].lblSol_Numero !== "2019070410-2-000" &&
                        datos[0].lstArchivosForest[i].lblSol_Numero !== "2019044007-1-000" &&
                        datos[0].lstArchivosForest[i].lblSol_Numero !== "2019092586-1-000") {
                        datosSerializado = JSON.stringify(datos[0].lstArchivosForest[i]);
                        $("#tblArchivosForest").append('<tr><td class="rowDatos"><label class="label-xsm">' + datos[0].lstArchivosForest[i].lblSol_Numero + '</label></td>' +
                            '<td class="rowDatos"><label class="label-xsm">' + datos[0].lstArchivosForest[i].lblIdEntryData + '</label></td>' +
                            '<td class="rowDatos"><label class="label-xsm">' + datos[0].lstArchivosForest[i].lblSolFechaCreacion + '</label></td>' +
                            '<td class="rowDatos"><label class="label-xsm">' + datos[0].lstArchivosForest[i].lblDocumento + '</label></td>' +
                            "<td class='rowDatos'><a href='#' title='Ver Documentos'  class='label-xsm' onclick=\'MostrarDocumentos(" + this.datosSerializado + ")'\><img src='../App_Themes/Img/documentos.png'/></a></td></tr>");
                    }
                }
                if (datos[0].lstArchivosForest.length > limitePagina) {
                    $("#tblArchivosForest").datatable({
                        pageSize: limitePagina,
                        firstPage: false,
                        lastPage: false,
                        filters: [true, false, false,false,false],
                        filterText: '',
                        prevPage: "Anterior",
                        nextPage: "Siguiente",
                        pagingDivClass: "text-left",
                        loadingDivSelector: "divCargando",
                        pagingDivSelector: "#paging_tblArchivosForest"
                    });
                }
            }
            if (datos[0].lstEIA == '0') {
                datosSerializado = JSON.stringify(datos[0]);

                $("#lblExpedientesRelacionados").text("");
                $("#lblVitalSolicitud").text("");
                $("#lblSolicitante").text("");
                $("#lblcodigo_expediente").text("");
                $("#lblnombre_expediente").text("");
                $("#lbldescripcion_expediente").text("");
                $("#lblSector").text("");
                $("#lblUbicacion").text("");
                $("#lblAutoridadAmbiental").text("");

                $("#lblVitalSolicitud").text(datos[0].lblSolicitud);
                $("#lblSolicitante").text(datos[0].lblSolicitante);
                $("#lblnombre_expediente").text(datos[0].lblNombreProyectoValue);
                $("#lbldescripcion_expediente").text(datos[0].lblDescripcionProyectoValue);
                $("#lblSector").text(datos[0].lblSector);
                $("#lblUbicacion").text(datos[0].lblUbicacionProyecto);
                $("#lblAutoridadAmbiental").text(datos[0].lblAutoridad);
                $("#lbEiaDocumento").remove();
                $("#lbEiaDocumentoDescarga").remove();


                $("#tblInformacionGeneral").append('<tr id="lbEiaDocumento"><td><label class="label-sm">DOCUMENTO EIA:</label></td><td><label class="label-xsm">' + datos[0].lblNombreDocumento + '</label></td></tr>');
                $("#tblInformacionGeneral").append('<tr id="lbEiaDocumentoDescarga"><td><label class="label-sm">DESCARGAR DOCUMENTO EIA:</label>' +
                    "</td><td><a href='#' title='DESCARGAR'  class='label-xsm' onclick=\'MostrarDocumentos(" + this.datosSerializado + ")'\><img src='../App_Themes/Img/documentos.png'/></a></td></tr>");
            }


        }
        ocultarMalla();
    }
    catch (e) {
        ocultarMalla();
        alert("Error Imprimiendo el Detalle Busqueda");
    }
}
function ImprimirDetalleMalla(datos) {
    setTimeout(function () {
        ImprimirDetalle(datos);
    }, 1500);
}
function ConsultarDetalle(strNumeroSolicitud, valorOrigen, id_TarSolId, sol_id_solicitante) {
    var numeroSol = strNumeroSolicitud;
    mostrarMalla();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "ReporteTramiteCPDetalle.aspx/ConsultarDetalleSolicitud",
        dataType: "json",
        data: "{'parametroDetalle':'" + strNumeroSolicitud + "', 'idSolicitante':'" + sol_id_solicitante + "', 'sol_id':'" + id_TarSolId + "', 'origen':'" + valorOrigen + "' }",
        success: function (data) {
            if (data !== undefined) {
                ImprimirDetalleMalla(data);
            }
        },
        failure: function (response) {
            alert("Error Consultando el Detalle Busqueda");
            ocultarMalla();
        },
        error: function (response) {
            alert("Error Consultando el Detalle Busqueda");
            ocultarMalla();
        }
    });
}
function MostrarDocumentos(lsDocumentosSeguimiento) {
    mostrarMalla();
    setTimeout(function () {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "ReporteTramiteCPDetalle.aspx/MostrarDocumentos",
            dataType: "json",
            data: "{'lstDocumentosSeguimiento':'"+ JSON.stringify(lsDocumentosSeguimiento) + "' }",
            success: function (data) {
                if (data !== undefined) {
                    if (data.search("ERROR") == 0) {
                        alert(data);
                    }
                    else if (data.length > 0) {
                        var w = 770, h = 300, l = (screen.availWidth - w) / 2, t = (screen.availHeight - h) / 2, popPage = '.popup';
                        window.open(data, "window", "width= " + w + ",height=" + h + ",left=" + l + ",top=" + t + ", scrollbars = yes, location = no, toolbar = no, menubar = no, status = no");
                        ocultarMalla();
                        return false;
                    }
                }
                ocultarMalla();
            },
            failure: function (response) {
                alert("Error al Mostrar Documentos");
                ocultarMalla();
            },
            error: function (response) {
                alert("Error al Mostrar Documentos");
                ocultarMalla();
            }
        });
    }, 1500);
}
function ConsultarDetallePublicacion(strNumeroSolicitud) {
    mostrarMalla();
    $.ajax({
        async: false,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "ReporteTramiteCPDetalle.aspx/ConsultarDetallePublicacion",
        dataType: "json",
        data: "{ 'parametroDetalle':"+ strNumeroSolicitud +"'}",
        success: function (data) {
            if (data !== undefined) {
                ImprimirDetallePublicacionMalla(data);
            }
        },
        failure: function (response) {
            alert("Error Consultando el Detalle Busqueda");
            ocultarMalla();
        },
        error: function (response) {
            alert("Error Consultando el Detalle Busqueda");
            ocultarMalla();
        }
    });
}
function ImprimirDetallePublicacionMalla(datos) {
    setTimeout(function () { ImprimirDetallePublicacion(datos); }, 500);
}
function ImprimirDetallePublicacion(datos) {
    try {
        if (datos != null) {
            $('#divPublicaciones').empty();
            var tabs = $("#exTab2");
            var ul = tabs.find("ul");
            var panelTabs = $("#panelTabs");
            var tempExiste = false;
            var divImagenAut = $("#imgAutoridad");

            $('#panelTabs div').each(function () {
                if (this.id == "divPublicaciones")
                    tempExiste = true;
                else if (this.id != "")
                    $(this).css("display", "none");
            });

            $('#exTab2 ul li').each(function () {
                if (this.id != "liPublicaciones" && this.id != "")
                    $(this).css("display", "none");
            });
            if (tempExiste == false) {
                $('<li id="liPublicaciones" class="active"><a href="#divPublicaciones" data-toggle="tab">Publicaciones</a></li>').appendTo(ul);
                $("<div class='tab-pane active' id='divPublicaciones'></div>").appendTo(panelTabs);
            }
            datosSerializado = JSON.stringify(datos[0]);
            $("#divPublicaciones").append("<div class='row'><h3><table id='tblPublicaciones'>");
            $("#tblPublicaciones").append(
                '<tr><td style="text-align:left; width:20%"><label class="label-sm">Título Publicación</label></td>' +
                '<td style="text-align:left;"><label class="label-xsm">' + datos[0].lblTitulo + '</label></td>' +
                '</tr><tr><td style="text-align:left;"><label class="label-sm">Tipo de Trámite</label></td>' +
                '<td style="text-align:left;"><label class="label-xsm">' + datos[0].lblTramite + '</label></td></tr>' +
                '<tr><td style="text-align:left;"><label class="label-sm">Autoridad Ambiental</label></td>' +
                '<td style="text-align:left;"><label class="label-xsm">' + datos[0].lblAutoridad + '</label></td></tr>' +
                '<tr><td style="text-align:left;"><label class="label-sm">Nombre del Proyecto</label></td>' +
                '<td style="text-align:left;"><label class="label-xsm">' + datos[0].lblNombreProyecto + '</label></td></tr>' +
                '<tr><td style="text-align:left;"><label class="label-sm">Número de Documento</label></td>' +
                '<td style="text-align:left;"><label class="label-xsm">' + datos[0].lblNumeroDocumento + '</label></td></tr>' +
                '<tr><td style="text-align:left;"><label class="label-sm">Número de Expediente</label></td>' +
                '<td style="text-align:left;"><label class="label-xsm">' + datos[0].lblExpediente + '</label></td></tr>' +
                '<tr><td style="text-align:left;"><label class="label-sm">Fecha de Publicación o Fijación</label></td>' +
                '<td style="text-align:left;"><label class="label-xsm">' + datos[0].lblFechaFijacion + '</label></td></tr>' +
                '<tr><td style="text-align:left;"><label class="label-sm">Fecha Desfijación</label></td>' +
                '<td style="text-align:left;"><label class="label-xsm">' + datos[0].lblFechaDesFijacion + '</label></td></tr>' +
                '<tr><td style="text-align:left;"><label class="label-sm">VER DOCUMENTOS</label></td>' +
                '<td style="text-align: left;">' +
                "<a href='#' title='Ver Documentos' class='label-xsm' onclick=\'MostrarDocumentosPublicaciones(" + this.datosSerializado + ")'\>" +
                '<img src="../App_Themes/Img/documentos.png"></a></td>' +
                '</tr></table></h3></div>');
            $("#exTab2").css("display", "block");
            ocultarMalla();
        }
    }
    catch (e) {
        alert("Error Imprimiendo el Detalle Busqueda");
        ocultarMalla();
    }
}
function generateEvents(datos) {
    var events = [];
    for (var i = 0; i < datos.length; i++) {
        events.push({ date: datos[i].date, content: datos[i].content });
    }
    return events;
}