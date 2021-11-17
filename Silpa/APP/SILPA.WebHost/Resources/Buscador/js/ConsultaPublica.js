var datosSerializado = "";
var valorParametroBusqueda = "";
var alertaReferenciado = "";
var valorTipoBusqueda = "";
var tempPrimeraPagina = 1;
var tempMaximoPagina = 0;
var limitePagina = 20;

$(document).ready(function () {

    InicializarCalendarios();
    BuscarSolicitante();

    var prm = Sys.WebForms.PageRequestManager.getInstance();

    prm.add_endRequest(function () {

        InicializarCalendarios();
        BuscarSolicitante();

    });

});

$('#modalResultados').on('shown.bs.modal', function () {
});

$('#modalResultados').on('hidde', function () {
});

//function botonEnviarParametroBusqueda() {
//    valorParametroBusqueda = $("#textSearch").val();
//    if (valorParametroBusqueda.length > 3) {
//        temporalNumeroPaginas = 0;
//        tempPrimeraPagina = 1;
//        tempMaximoPagina = 0;

//        $("#divRespuestaResultados").empty();
//        $('#dvOpciones').empty();
//        $("#ulPaginador").empty();
//        $("#divError").css("display", "none");

//        EnviarParametroBusquedaMalla();
//        $("#textSearch").text(valorParametroBusqueda);
//        $("#spBusqueda").text(valorParametroBusqueda);
//        $("#modalResultados").slideToggle("slow");

//    }
//}

//function BotonCambio(botonValorTipoBusqueda) {
//    $("#textSearch").val("");
//    $("#divRespuestaResultados").empty();
//    $('#dvOpciones').empty();
//    $("#ulPaginador").empty();
//    $("#divError").css("display", "none");
//    $("#spBusqueda").text("");


//    var tabs = $("#exTab2");
//    var panelTabs = $("#panelTabs");
//    var ul = tabs.find("ul");

//    $('#exTab2 ul li').each(function () {
//        if ($(this)[0].id != "aInfGeneral" && $(this)[0].id != "aEstadoTramite") {
//            $("#div" + $(this)[0].id.substring(2, 50)).empty();
//            $("#div" + $(this)[0].id.substring(2, 50)).remove();
//            $("#tbl" + $(this)[0].id.substring(2, 50)).remove();
//            $(this).remove();
//        }
//    });

//    $("#exTab2").css("display", "none");

//    if (botonValorTipoBusqueda == "Todos") {
//        $("#lblTituloPrincipal").text("CONSULTA DETALLADA DE SOLICITUDES");
//        $('#tblInformacionGeneral tr[id=lbEiaDocumento]').remove();
//        $('#tblInformacionGeneral tr[id=lbEiaDocumentoDescarga]').remove();
//    }
//    else if (botonValorTipoBusqueda == "EIA") {
//        $("#lblTituloPrincipal").text("CONSULTA DE ESTUDIOS DE IMPACTO AMBIENTALES");
//    }
//    else if (botonValorTipoBusqueda == "Publicacion" || valorTipoBusqueda == "Publicación") {
//        $("#lblTituloPrincipal").text("CONSULTA DE AUTOS O RESOLUCIONES");
//        $('#tblInformacionGeneral tr[id=lbEiaDocumento]').remove();
//        $('#tblInformacionGeneral tr[id=lbEiaDocumentoDescarga]').remove();
//    }



//}

function AddKeyPress(e) {
    e = e || window.event;
    if (e.keyCode == 13) {
        alertaReferenciado = "";
        temporalNumeroPaginas = 0;
        tempPrimeraPagina = 1;
        tempMaximoPagina = 0;
        valorParametroBusqueda = $("#textSearch").val();
        e.preventDefault();
        $("#divError").css("display", "none");
        $('#dvOpciones').empty();
        $("#divRespuestaResultados").empty();
        $("#ulPaginador").empty();
        $("#textSearch").text(valorParametroBusqueda);
        $("#spBusqueda").text(valorParametroBusqueda);
        EnviarParametroBusquedaMalla();
        $("#modalResultados").slideToggle("slow").show(function () {
            $(this).show();
            $("#modalResultados").show();
        });
        return false;
    }
}

function closeModal() {
    $("#modalResultados").slideToggle("slow");
}

function openModal() {
    $("#modalResultados").slideToggle("slow");
}

function EnviarParametroBusquedaMalla() {
    $("#divCargando").css("display", "block");
    setTimeout(function () { EnviarParametroBusqueda(); }, 500);
}

//function EnviarParametroBusqueda() {

//    $('#divTipoBusqueda ul li').each(function () {
//        if ($(this)[0].className == "active")
//            valorTipoBusqueda = $(this)[0].id;
//    });
//    valorParametroBusqueda = $("#textSearch").val();
//    if (alertaReferenciado.length > 0)
//        valorParametroBusqueda = alertaReferenciado;
//    if (valorParametroBusqueda.length >= 3) {
//        var i;
//        $.ajax({
//            async: false,
//            type: "POST",
//            url: "ReporteTramiteCP.aspx",
//            dataType: "json",
//            data: { Accion: "EnviarParametroBusqueda", parametroBusqueda: valorParametroBusqueda, tipoBusqueda: valorTipoBusqueda },
//            success: function (data) {
//                if (data !== undefined) {
//                    OnSuccess(data);
//                }
//                ocultarMalla();
//            },
//            complete: function (data) {
//            },
//            failure: function (response) {
//                $("#divError").css("display", "block");
//                $("#lblError").text("Error al iniciar la busqueda");
//                ocultarMalla();
//            },
//            error: function (response) {
//                $("#divError").css("display", "block");
//                $("#lblError").text("Error al iniciar la busqueda");
//                ocultarMalla();
//            }
//        });
//    }
//    else {
//        $('#dvOpciones').append(
//            '<div class="panel-heading"><h3 class="panel-title">La palabra que desea buscar debe tener mínimo tres(3) caracteres.!</h3></div>');
//        ocultarMalla();
//    }
//}

function DireccionEspecifica() {
    $("#divCargando").css("display", "block");
    setTimeout(function () { window.location = '../ReporteTramite/ReporteTramite.aspx'; }, 500);
}

function EnviarParametroBusquedaReferencia(valorCorregido) {
    alertaReferenciado = valorCorregido;
    $("#textSearch").text(valorParametroBusqueda);
    EnviarParametroBusqueda();
    $('#dvOpciones').empty();
}

//function OnSuccess(datos) {
//    if (datos.lstResultadosBusqueda.length > 0) {
//        var lista = datos.lstResultadosBusqueda;
//        var tempImagenOrigen = "";
//        try {
//            for (i = 0; i < lista.length; i++) {

//                if (lista[i].ORIGEN == "SILA") {
//                    lista[i].AUT_NOMBRE = "ANLA";
//                    tempImagenOrigen = "<img style='max-height:50px;max-width:50px;' src='../App_Themes/Img/Autoridades/anla.png'/>";
//                }
//                else if (lista[i].AUT_NOMBRE == "" || lista[i].AUT_NOMBRE == "ANLA") {
//                    lista[i].AUT_NOMBRE = "ANLA";
//                    tempImagenOrigen = "<img style='max-height:50px;max-width:50px;' src='../App_Themes/Img/Autoridades/anla.png'/>";
//                }
//                else if (lista[i].AUT_NOMBRE == "MADS")
//                    tempImagenOrigen = "<img style='max-height:20px;max-width:20px;' src='../App_Themes/Img/logoEntidad.png'/>";
//                else if (lista[i].AUT_NOMBRE.length > 0)
//                    tempImagenOrigen = "<img style='max-height:50px;max-width:50px;' src='../App_Themes/Img/Autoridades/" + lista[i].AUT_NOMBRE + ".jpg'/>";
//                else if (lista[i].ORIGEN == "VITAL")
//                    tempImagenOrigen = "<img style='max-height:50px;max-width:50px;' src='../App_Themes/Img/LogoVital.png'/>";
//                else if (lista[i].ORIGEN == "SILA" || lista[i].ORIGEN == "SILAMC")
//                    tempImagenOrigen = "<img style='max-height:50px;max-width:50px;' src='../App_Themes/Img/Autoridades/anla.png'/>";

//                if (valorTipoBusqueda == "Publicación" || valorTipoBusqueda == "Publicacion") {
//                    $('#divRespuestaResultados').append(
//                        '<div style="border: solid 1px #c3c3c3;">' +
//                        '<table id="tblRespuestaResultados" style="width: 98%">' +
//                        '<tr><td><label class="lbl-sm">DOCUMENTO:</label>' +
//                        '<img style="max-height:25px;max-width:25px;" class="clickable" src="../App_Themes/Img/Buscar.png" onclick="ConsultarDetallePublicacion(\'' + lista[i].TAR_SOL_ID + '\')"/></td>' +
//                        '<td style="width:15%"><span class="spnImagenLogo">' + tempImagenOrigen + '</span></td></tr>' +
//                        '<tr><td><label class="label-xsm">' + lista[i].SOL_NUM_SILPA + '</label></td></tr>' +
//                        '<tr><td><label class="lbl-xsm">Tipo de Tramite: ' + lista[i].TRA_NOMBRE + '</label></td></tr>' +
//                        '<tr><td colspan="2"><label class="lbl-xsm">Numero: ' + lista[i].NUM_DOCUMENTO + '</label></td></tr>' +
//                        '<tr><td colspan="2"><label class="lbl-xsm">Expediente: ' + lista[i].EXPEDIENTE + '</label></td></tr>' +
//                        '<tr><td colspan="2"><label class="lbl-xsm">Autoridad Ambiental: ' + lista[i].AUT_NOMBRE + '</label></td></tr>' +
//                        '<tr><td colspan="3"><label class="lbl-sm">Nombre Proyecto: ' + lista[i].NOMBRE_PROYECTO.substring(0, 100) + '...</label></td></tr>' +
//                        '<tr><td colspan="3"><label class="lbl-sm">Fecha de Publicación o Fijación: ' + lista[i].TAR_FECHA_CREACION + '</label></td></tr>' +
//                        '<tr><td><label id="lblIdSolicitante" style="visibility: hidden;" class="lbl-xsm" >' + lista[i].SOL_ID_SOLICITANTE + '</label>' +
//                        '<label id="lblTarSolId" style="visibility: hidden;" class="lbl-xsm" >' + lista[i].TAR_SOL_ID + '</label></td></tr></table></div>');
//                }
//                else {
//                    $('#divRespuestaResultados').append(
//                        '<div style="border: solid 1px #c3c3c3;">' +
//                        '<table id="tblRespuestaResultados" style="width: 98%">' +
//                        '<tr><td style="width:35%"><label class="lbl-sm"> Nº Tramite:</label></td>' +
//                        '<td style="width:50%"><a class="label-sm" href="#" onclick="ConsultarDetalle(\'' + lista[i].SOL_NUM_SILPA + '\',\'' + lista[i].ORIGEN + '\', \'' + lista[i].TAR_SOL_ID + '\', \'' + lista[i].SOL_ID_SOLICITANTE + '\')">' + lista[i].SOL_NUM_SILPA + '</a>' +
//                        '<td style="width:15%"><span class="spnImagenLogo">' + tempImagenOrigen + '</span></td></tr>' +
//                        '<tr><td><label class="lbl-sm">Tipo de Tramite: </label></td><td colspan="2"><label class="lbl-sm">' + lista[i].TRA_NOMBRE + '</label></td></tr>' +
//                        '<tr><td><label class="lbl-sm">Autoridad Ambiental:</label></td><td colspan="2"><label class="lbl-sm">' + lista[i].AUT_NOMBRE + '</label></td></tr>' +
//                        '<tr><td colspan="3"><label class="lbl-sm">Nombre Proyecto:' + lista[i].NOMBRE_PROYECTO.substring(0, 200) + '...</label></td></tr>' +
//                        '<tr><td><label class="lbl-sm">Fecha Creacion:</label></td><td colspan="2"><label class="lbl-sm">' + lista[i].TAR_FECHA_CREACION + '</label></td></tr>' +
//                        '<tr><td><label class="lbl-sm">Ubicación:</label></td><td colspan="2"><label class="lbl-sm">' + lista[i].MUNICIPIO + '</label>' +
//                        '<label id="lblIdSolicitante" style="visibility: hidden;" class="lbl-sm" >' + lista[i].SOL_ID_SOLICITANTE + '</label>' +
//                        '<label id="lblTarSolId" style="visibility: hidden;" class="lbl-sm" >' + lista[i].TAR_SOL_ID + '</label></td></tr></table></div>');
//                }
//            }

//            if (lista[0].temporalNumeroPaginas > 0) {
//                if (lista[0].temporalNumeroPaginas == 1) {
//                    $('#ulPaginador').append('<label class="lbl-sm"> Nº total de registros encontrados: ' + lista[0].temporalNumeroRegistros + ', agrupados en un total de: ' + lista[0].temporalNumeroPaginas + ' Páginas.</label>');
//                }
//                else {

//                    if (tempPrimeraPagina == 1)
//                        $('#ulPaginador').append('<li class="page-item disabled"><a class="page-link" href="#" tabindex="-1">Anterior</a></li>');
//                    else if (tempPrimeraPagina > 1)
//                        $('#ulPaginador').append('<li class="page-item"><a class="page-link" href="#' + tempPrimeraPagina + '" tabindex="-1" onclick="IrPagina(\'ANT' + tempPrimeraPagina + '\')">Anterior</a></li>');
//                    else if (tempPrimeraPagina < 0) {
//                        tempMaximoPagina = tempMaximoPagina + tempPrimeraPagina;
//                        tempPrimeraPagina = (tempPrimeraPagina * (-1)) - 6;
//                        $('#ulPaginador').append('<li class="page-item"><a class="page-link" href="#' + tempPrimeraPagina + '" tabindex="-1" onclick="IrPagina(\'ANT' + tempPrimeraPagina + '\')">Anterior</a></li>');
//                    }

//                    if (tempMaximoPagina <= lista[0].temporalNumeroPaginas)
//                        tempMaximoPagina = parseInt(tempPrimeraPagina) + 5;

//                    for (s = tempPrimeraPagina; s <= tempMaximoPagina && s <= lista[0].temporalNumeroPaginas; s++) {
//                        $('#ulPaginador').append('<li class="page-item" id="li' + s + '"><a class="page-link" href="#' + s + '" onclick="IrPagina(\'' + s + '\')">' + s + '</a></li>');
//                    }
//                    if (lista[0].temporalNumeroPaginas > tempMaximoPagina)
//                        $('#ulPaginador').append('<li class="page-item"><a class="page-link" href="#' + s + '" onclick="IrPagina(\'SIG' + s + '\')">...Siguiente</a></li>');
//                    else
//                        $('#ulPaginador').append('<li class="page-item disabled"><a class="page-link" href="#' + s + '">...Siguiente</a></li>');

//                    $('#ulPaginador').append('<label class="lbl-sm"> Nº total de registros encontrados: ' + lista[0].temporalNumeroRegistros + ', agrupados en un total de: ' + lista[0].temporalNumeroPaginas + ' Páginas.</label>');

//                }
//            }

//            Resaltar();
//        }
//        catch (e) {
//            $("#divError").css("display", "block");
//            $("#lblError").text("Error páginando Busqueda");
//            ocultarMalla();
//        }
//    }
//    else {
//        quizoDecir();
//    }
//}

//function quizoDecir() {
//    var encontroReferencia = false;
//    $.ajax({
//        async: false,
//        type: "POST",
//        url: "ReporteTramiteCP.aspx",
//        dataType: "json",
//        data: { Accion: "QuisoDecirDepartamento" },
//        success: function (data) {
//            if (data !== undefined) {
//                $('#dvOpciones').empty();
//                for (i = 0; i < data.length; i++) {
//                    if ((levenshteinDistance(data[i].DEP_NOMBRE, valorParametroBusqueda.toUpperCase())) <= 2) {
//                        encontroReferencia = true;
//                        $("#textSearch").text(data[i].DEP_NOMBRE);
//                        $('#dvOpciones').append(
//                            '<div class="panel-heading"><h3 class="panel-title">SU CONSULTA NO ARROJO RESULTADOS!</h3></div>' +
//                            '<div class="panel-body"><label class="lbl-xsm">Quizás quiso decir...</label> <a class="label-sm" href="#" onclick="EnviarParametroBusquedaReferencia(\'' + data[i].DEP_NOMBRE + '\')">' + data[i].DEP_NOMBRE + '</a></div>'
//                        )
//                    }
//                }
//            }
//        },
//        complete: function (data) {

//        },
//        failure: function (response) {
//            $("#divError").css("display", "block");
//            $("#lblError").text("Error Buscando similitudes");
//            ocultarMalla();
//        },
//        error: function (response) {
//            $("#divError").css("display", "block");
//            $("#lblError").text("Error Buscando similitudes");
//            ocultarMalla();
//        }
//    });
//    if (encontroReferencia == false) {
//        $.ajax({
//            async: false,
//            type: "POST",
//            url: "ReporteTramiteCP.aspx",
//            dataType: "json",
//            data: { Accion: "QuisoDecirAutoridad" },
//            success: function (data) {
//                if (data !== undefined) {
//                    $('#dvOpciones').empty();
//                    for (i = 0; i < data.length; i++) {
//                        if ((levenshteinDistance(data[i].AUT_NOMBRE, valorParametroBusqueda.toUpperCase())) <= 2) {
//                            encontroReferencia = true;
//                            $("#textSearch").text(data[i].AUT_NOMBRE);
//                            $('#dvOpciones').append(
//                                '<div class="panel-heading"><h3 class="panel-title">SU CONSULTA NO ARROJO RESULTADOS!</h3></div>' +
//                                '<div class="panel-body"><label class="lbl-xsm">Quizás quiso decir...</label> <a class="label-sm" href="#" onclick="EnviarParametroBusquedaReferencia(\'' +
//                                data[i].AUT_NOMBRE + '\')">' + data[i].AUT_NOMBRE + '</a></div>'
//                            )
//                        }
//                    }
//                }
//            },
//            complete: function (data) {

//            },
//            failure: function (response) {
//                alert(response.d);
//            },
//            error: function (response) {
//                alert(response.d);
//            }
//        });


//    }
//    if (encontroReferencia == false) {
//        $.ajax({
//            async: false,
//            type: "POST",
//            url: "ReporteTramiteCP.aspx",
//            dataType: "json",
//            data: { Accion: "QuisoDecirMunicipio" },
//            success: function (data) {
//                if (data !== undefined) {
//                    $('#dvOpciones').empty();
//                    for (i = 0; i < data.length; i++) {
//                        if ((levenshteinDistance(data[i].MUN_NOMBRE, valorParametroBusqueda.toUpperCase())) <= 2) {
//                            encontroReferencia = true;
//                            $("#textSearch").text(data[i].MUN_NOMBRE);
//                            $('#dvOpciones').append(
//                                '<div class="panel-heading"><h3 class="panel-title">SU CONSULTA NO ARROJO RESULTADOS!</h3></div>' +
//                                '<div class="panel-body"><label class="lbl-xsm">Quizás quiso decir...</label> <a class="label-sm" href="#" onclick="EnviarParametroBusquedaReferencia(\'' +
//                                data[i].MUN_NOMBRE + '\')">' + data[i].MUN_NOMBRE + '</a></div>'
//                            )
//                        }
//                    }
//                }
//            },
//            complete: function (data) {

//            },
//            failure: function (response) {
//                alert(response.d);
//            },
//            error: function (response) {
//                alert(response.d);
//            }
//        });
//    }
//    ocultarMalla();
//}

//function levenshteinDistance(a, b) {
//    if (a.length == 0) return b.length;
//    if (b.length == 0) return a.length;

//    var matrix = [];

//    // increment along the first column of each row
//    var i;
//    for (i = 0; i <= b.length; i++) {
//        matrix[i] = [i];
//    }

//    // increment each column in the first row
//    var j;
//    for (j = 0; j <= a.length; j++) {
//        matrix[0][j] = j;
//    }

//    // Fill in the rest of the matrix
//    for (i = 1; i <= b.length; i++) {
//        for (j = 1; j <= a.length; j++) {
//            if (b.charAt(i - 1) == a.charAt(j - 1)) {
//                matrix[i][j] = matrix[i - 1][j - 1];
//            } else {
//                matrix[i][j] = Math.min(matrix[i - 1][j - 1] + 1, // substitution
//                    Math.min(matrix[i][j - 1] + 1, // insertion
//                        matrix[i - 1][j] + 1)); // deletion
//            }
//        }
//    }

//    return matrix[b.length][a.length];
//}

//function ConsultarDetallePublicacion(strNumeroSolicitud) {
//    $("#modalResultados").slideToggle("slow");
//    $("#divError").css("display", "none");
//    $.ajax({
//        async: false,
//        type: "POST",
//        url: "ReporteTramiteCP.aspx",
//        dataType: "json",
//        data: { Accion: "ConsultarDetallePublicacion", parametroDetalle: strNumeroSolicitud },
//        success: function (data) {
//            if (data !== undefined) {
//                ImprimirDetallePublicacionMalla(data);
//            }
//        },
//        failure: function (response) {
//            $("#divError").css("display", "block");
//            $("#lblError").text("Error Consultando el Detalle Busqueda");
//            ocultarMalla();
//        },
//        error: function (response) {
//            $("#divError").css("display", "block");
//            $("#lblError").text("Error Consultando el Detalle Busqueda");
//            ocultarMalla();
//        }
//    });
//}

//function ImprimirDetallePublicacionMalla(datos) {
//    $("#divCargando").css("display", "block");
//    setTimeout(function () { ImprimirDetallePublicacion(datos); }, 500);
//}
//function ImprimirDetallePublicacion(datos) {
//    try {
//        if (datos != null) {
//            $('#divPublicaciones').empty();
//            var tabs = $("#exTab2");
//            var ul = tabs.find("ul");
//            var panelTabs = $("#panelTabs");
//            var tempExiste = false;
//            var divImagenAut = $("#imgAutoridad");

//            $('#panelTabs div').each(function () {
//                if (this.id == "divPublicaciones")
//                    tempExiste = true;
//                else if (this.id != "")
//                    $(this).css("display", "none");
//            });

//            $('#exTab2 ul li').each(function () {
//                if (this.id != "liPublicaciones" && this.id != "")
//                    $(this).css("display", "none");
//            });
//            if (tempExiste == false) {
//                $('<li id="liPublicaciones" class="active"><a href="#divPublicaciones" data-toggle="tab">Publicaciones</a></li>').appendTo(ul);
//                $("<div class='tab-pane active' id='divPublicaciones'></div>").appendTo(panelTabs);
//            }
//            datosSerializado = JSON.stringify(datos[0]);
//            $("#divPublicaciones").append("<div class='row'><h3><table id='tblPublicaciones'>");
//            $("#tblPublicaciones").append(
//                '<tr><td style="text-align:left; width:20%"><label class="label-sm">Título Publicación</label></td>' +
//                '<td style="text-align:left;"><label class="label-xsm">' + datos[0].lblTitulo + '</label></td>' +
//                '</tr><tr><td style="text-align:left;"><label class="label-sm">Tipo de Trámite</label></td>' +
//                '<td style="text-align:left;"><label class="label-xsm">' + datos[0].lblTramite + '</label></td></tr>' +
//                '<tr><td style="text-align:left;"><label class="label-sm">Autoridad Ambiental</label></td>' +
//                '<td style="text-align:left;"><label class="label-xsm">' + datos[0].lblAutoridad + '</label></td></tr>' +
//                '<tr><td style="text-align:left;"><label class="label-sm">Nombre del Proyecto</label></td>' +
//                '<td style="text-align:left;"><label class="label-xsm">' + datos[0].lblNombreProyecto + '</label></td></tr>' +
//                '<tr><td style="text-align:left;"><label class="label-sm">Número de Documento</label></td>' +
//                '<td style="text-align:left;"><label class="label-xsm">' + datos[0].lblNumeroDocumento + '</label></td></tr>' +
//                '<tr><td style="text-align:left;"><label class="label-sm">Número de Expediente</label></td>' +
//                '<td style="text-align:left;"><label class="label-xsm">' + datos[0].lblExpediente + '</label></td></tr>' +
//                '<tr><td style="text-align:left;"><label class="label-sm">Fecha de Publicación o Fijación</label></td>' +
//                '<td style="text-align:left;"><label class="label-xsm">' + datos[0].lblFechaFijacion + '</label></td></tr>' +
//                '<tr><td style="text-align:left;"><label class="label-sm">Fecha Desfijación</label></td>' +
//                '<td style="text-align:left;"><label class="label-xsm">' + datos[0].lblFechaDesFijacion + '</label></td></tr>' +
//                '<tr><td style="text-align:left;"><label class="label-sm">VER DOCUMENTOS</label></td>' +
//                '<td style="text-align: left;">' +
//                "<a href='#' title='Ver Documentos' class='label-xsm' onclick=\'MostrarDocumentosPublicaciones(" + this.datosSerializado + ")'\>" +
//                '<img src="../App_Themes/Img/documentos.png"></a></td>' +
//                '</tr></table></h3></div>');
//            $("#exTab2").css("display", "block");
//            ocultarMalla();
//        }
//    }
//    catch (e) {
//        $("#divError").css("display", "block");
//        $("#lblError").text("Error Imprimiendo el Detalle Busqueda");
//        ocultarMalla();
//    }
//}

//function MostrarDocumentos(lsDocumentosSeguimiento) {
//    $("#divError").css("display", "none");
//    $("#divCargando").css("display", "block");
//    setTimeout(function () {
//        $.ajax({
//            async: false,
//            type: "POST",
//            url: "ReporteTramiteCP.aspx",
//            dataType: "json",
//            data: { Accion: "MostrarDocumentos", lstDocumentosSeguimiento: JSON.stringify(lsDocumentosSeguimiento) },
//            success: function (data) {
//                if (data !== undefined) {
//                    if (data.search("ERROR") == 0) {
//                        data = data.replace("ERROR", "")
//                        $("#divError").css("display", "block");
//                        $("#divError").text(data);
//                    }
//                    else if (data.length > 0) {
//                        var w = 770, h = 300, l = (screen.availWidth - w) / 2, t = (screen.availHeight - h) / 2, popPage = '.popup';
//                        window.open(data, "window", "width= " + w + ",height=" + h + ",left=" + l + ",top=" + t + ", scrollbars = yes, location = no, toolbar = no, menubar = no, status = no");
//                        ocultarMalla();
//                        return false;
//                    }
//                }
//                ocultarMalla();
//            },
//            failure: function (response) {
//                $("#divError").css("display", "block");
//                $("#divError").text("Error al Mostrar Documentos");
//                ocultarMalla();
//            },
//            error: function (response) {
//                $("#divError").css("display", "block");
//                $("#lblError").text("Error al Mostrar Documentos");
//                ocultarMalla();
//            }
//        });
//    }, 1500);
//}

//function MostrarDocumentosPublicaciones(lsDocumentosPublicaciones) {
//    $("#divError").css("display", "none");
//    $("#divDinamicoDocumentos").empty();
//    $.ajax({
//        async: false,
//        type: "POST",
//        url: "ReporteTramiteCP.aspx",
//        dataType: "json",
//        data: { Accion: "MostrarDocumentosPublicaciones", listaDocumentosPublicaciones: JSON.stringify(lsDocumentosPublicaciones) },
//        success: function (data) {
//            if (data !== undefined) {
//                if (data[0].ListaDocumentos != undefined) {
//                    $("#divDinamicoDocumentos").append("<div class='row'><table id='tblDocs'><tr>" +
//                        "<th style='text-align: center;'><label class='label-sm'>ARCHIVO</label></th>" +
//                        "<th style='text-align: center;'><label class='label-sm'> DESCARGAR </label></th></tr></table></div>");
//                    for (var i = 0; i < data[0].ListaDocumentos.length; i++) {
//                        datosSerializado = JSON.stringify(data[0].ListaDocumentos[i]);
//                        $("#tblDocs").append('<tr><td style="text-align: center;">' +
//                            '<label class="label-xsm">' + data[0].ListaDocumentos[i].NombreArchivo + '</label></td>' +
//                            "<td style='text-align: center;'><a href='#' title='Descargar Documento'  class='label-xsm' onclick=\'DescargarDocumentoPublicado(" + datosSerializado + ")'\><img src='../App_Themes/Img/documentos.png'></a></td>");
//                    }
//                    $('#modalDocumentos').modal('show');
//                }
//                else if (data.search("ERROR") == 0) {
//                    $("#divError").css("display", "block");
//                    $("#divError").text(data);
//                }
//                return false;
//            }
//            ocultarMalla();
//        },
//        failure: function (response) {
//            $("#divError").css("display", "block");
//            $("#divError").text("Error al Mostrar Documentos");
//            ocultarMalla();
//        },
//        error: function (response) {
//            $("#divError").css("display", "block");
//            $("#lblError").text("Error al Mostrar Documentos");
//            ocultarMalla();
//        }
//    });
//}
/*--------------------------------------------------*/
// FUNCION DE MALLA QUE BLOQUEA EL FONDO
/*--------------------------------------------------*/
function mostrarMalla() {
    $("#divCargando").css("display", "block");
}
function ocultarMalla() {
    $("#divCargando").css("display", "none");
}

/*--------------------------------------------------*/
// FIN. FUNCION DE MALLA QUE BLOQUEA EL FONDO
/*--------------------------------------------------*/

function FormateoFechaJSON(jsonDt) {
    //incoming json date string is of the form "/Date(946702800000)/"
    var jdate = new Date(+jsonDt.replace(/\D/g, ''));

    var year = jdate.getFullYear();
    var month = jdate.getMonth() + 1 < 10 ? '0' + (jdate.getMonth() + 1) : jdate.getMonth();
    var day = jdate.getDay() < 10 ? '0' + jdate.getDay() : jdate.getDay();

    var dateString = day + '/' + month + '/' + year;

    return dateString;

}



/******************************************* V2 BUSCADOR **************************************************************/
function ConsultarDetalle(strNumeroSolicitud, valorOrigen, id_TarSolId, sol_id_solicitante) {
    $("#divError").css("display", "none");
    $('#chart-container').empty();
    var numeroSol = strNumeroSolicitud;
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "ReporteTramiteCP.aspx/ConsultarDetalleSolicitud",
        dataType: "json",
        data: "{'parametroDetalle':'" + strNumeroSolicitud + "', 'idSolicitante':'" + sol_id_solicitante + "', 'sol_id':'" + id_TarSolId + "', 'origen':'" + valorOrigen + "' }",
        success: function (data) {
            if (data !== undefined) {
                ImprimirDetalleMalla(data);
            }
        },
        failure: function (response) {
            $("#divError").css("display", "block");
            $("#lblError").text("Error Consultando el Detalle Busqueda");
            ocultarMalla();
        },
        error: function (response) {
            $("#divError").css("display", "block");
            $("#lblError").text("Error Consultando el Detalle Busqueda");
            ocultarMalla();
        }
    });
}

function ImprimirDetalleMalla(datos) {
    setTimeout(function () { ImprimirDetalle(datos); }, 1500);
}
function RowSelected(sender, eventArgs) {
    var SOL_NUM_SILPA = eventArgs.getDataKeyValue("SOL_NUM_SILPA");
    var ORIGEN = eventArgs.getDataKeyValue("ORIGEN");
    var TAR_SOL_ID = eventArgs.getDataKeyValue("TAR_SOL_ID");
    var SOL_ID_SOLICITANTE = eventArgs.getDataKeyValue("SOL_ID_SOLICITANTE");
    var TIPO_CONSULTA = $("#ddlTipoBusqueda").val();
    //ConsultarDetalle(SOL_NUM_SILPA, ORIGEN, TAR_SOL_ID, SOL_ID_SOLICITANTE, SOL_NUM_SILPA);
    window.open("ReportetramiteCPDetalle.aspx?NumSilpa=" + SOL_NUM_SILPA + "&Origen=" + ORIGEN + "&TarSolId=" + TAR_SOL_ID + "&Solicitante=" + SOL_ID_SOLICITANTE + "&TipoConsulta=" + TIPO_CONSULTA);
}

function RowSelectedAvanzada(sender, eventArgs) {
    //var SOL_NUM_SILPA = eventArgs.getDataKeyValue("SOL_NUM_SILPA");
    //var ORIGEN = eventArgs.getDataKeyValue("ORIGEN");
    //var TAR_SOL_ID = eventArgs.getDataKeyValue("TAR_SOL_ID");
    //var SOL_ID_SOLICITANTE = eventArgs.getDataKeyValue("SOL_ID_SOLICITANTE");
    //var TIPO_CONSULTA = $("#ddlTipoBusqueda").val();
    ////ConsultarDetalle(SOL_NUM_SILPA, ORIGEN, TAR_SOL_ID, SOL_ID_SOLICITANTE, SOL_NUM_SILPA);
    //window.open("ReportetramiteCPDetalle.aspx?NumSilpa=" + SOL_NUM_SILPA + "&Origen=" + ORIGEN + "&TarSolId=" + TAR_SOL_ID + "&Solicitante=" + SOL_ID_SOLICITANTE + "&TipoConsulta=" + TIPO_CONSULTA);
}
function CerrarModal() {
    $("#imgAutoridad").empty();
    $("#InfoDetalle").hide();

}

function EjecutarConsulta() {
    $("#imgBtnBuscar").click();
}

function InicializarCalendarios() {

    $("[id*=txtFecha]").datetimepicker({ format: 'd/m/Y', lang: 'es', timepicker: false });
}

function BuscarSolicitante() {
    $(".autosuggestSolicitante").autocomplete({
        source: function (request, response) {
            $('#hdfSolicitanteID').val("");

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "ReporteTramiteCP.aspx/GetSolicitante",
                data: "{'p_strSolicitante':'" + $('#txtSolicitante').val() + "'}",
                dataType: "json",
                success: function (data) {
                    if (data.d.length > 0) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split("@/")[0],
                                val: item.split("@/")[1]
                            }
                        }));
                    }
                    else {
                        response([{ label: 'No se encontro información.', val: -1 }]);
                    }
                },
                error: function (result) {
                    console.log(result);
                    alert("Error realizando busqueda de información");
                }
            });
        },
        open: function() {
            $("ul.ui-menu").width( $(this).innerWidth() );
        },
        select: function (event, ui) {
            if (ui.item.val == -1) {
                $('#txtSolicitante').val("");
                $('#hdfSolicitanteID').val("");
                return false;
            }
            else {
                $('#hdfSolicitanteID').val(ui.item.val);
            }
        }
    });
}

