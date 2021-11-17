var datosSerializado = "";
var valorParametroBusqueda = "";
var alertaReferenciado = "";
var valorTipoBusqueda = "";
var tempPrimeraPagina = 1;
var tempMaximoPagina = 0;

$(document).ready(function () {

    var tech = getUrlParameter('tipoBusqueda');
    if (tech == "otros")
    {
        $("#Todos").css("display", "block");
        $("#Todos").addClass("active");
        $("#aId").text("Estado del Tramite");
        $("#lblTituloPrincipal").text("CONSULTA DEL ESTADO ACTUAL DEL TRAMITE");
        

        $("#Publicación").css("display", "none");
        $("#liBusquedaEIA").css("display", "none");
    }
    else if (tech == "eia")
    {
        $("#Todos").css("display", "none");
        $("#Publicación").css("display", "none");

        $("#liBusquedaEIA").css("display", "block");
        $("#liBusquedaEIA").addClass("active");
        $("#lblTituloPrincipal").text("CONSULTA DE ESTUDIOS DE IMPACTO AMBIENTALES");
        $("#aId").text("Estudio de Impacto Ambiental");


    }
    else if (tech == "Publicacion") {
        $("#Todos").css("display", "none");
        $("#liBusquedaEIA").css("display", "none");

        $("#Publicación").css("display", "block");
        $("#aId").text("Autos o Resolución");
        $("#Publicación").addClass("active");
        $("#lblTituloPrincipal").text("CONSULTA DE AUTOS O RESOLUCIONES");
    }

    BotonCambio();
    $('#ModalTramite').on('show.bs.modal', function (e) {
        var dataId = $(e.relatedTarget)[0].dataset.id;

        $.ajax({
            async: false,
            type: "POST",
            url: "ReporteTramiteCP.aspx",
            dataType: "json",
            data: { Accion: "MostrarDetalleActividad", parametroBusqueda: dataId },
            success: function (data) {
            },
            complete: function (data) {
                if (data !== undefined) {
                    var objActividad = JSON.parse(data.responseText);
                    var tempImagenOrigen = "";
                    var tempDocumento = "";
                    if (objActividad.lstActividad.lblusuario.toUpperCase().search("USUARIO") >= 0 || objActividad.lstActividad.lblusuario.toUpperCase().search("SOLICITANTE") >= 0) {
                        tempImagenOrigen = "<span class='glyphicon glyphicon-user icon-success'> <label class='lbl-sm'> " + objActividad.lstActividad.lblusuario + "</label></span>";
                    }
                    else if (objActividad.lstActividad.lblusuario.toUpperCase().search("AA") >= 1) {
                        tempImagenOrigen = "<span class='glyphicon glyphicon-home icon-success'> <label class='lbl-sm'> " + objActividad.lstActividad.lblusuario + "</label></span>";
                    }
                    else {
                        tempImagenOrigen = "<span class='glyphicon glyphicon-home icon-success'> <label class='lbl-sm'> " + objActividad.lstActividad.lblusuario + "</label></span>";
                    }

                    $('#divDinamicoDetalleaActividad').empty();
                    $('#divDinamicoDetalleaActividad').append(
                       '<div style="border: solid 1px #c3c3c3;"><br/>' +
                       '<table id="tblDetalleActividad" style="width: 98%">' +
                       '<tr><td style="width:25%"><label class="lbl-sm">Actividad:</label></td><td style="width:45%"><label class="lbl-sm">' + objActividad.lstActividad.lblName + ':</label></td>' +
                       '<tr><td><label class="lbl-sm">Fecha Inicio:</label></td><td><label class="lbl-sm">' + objActividad.lstActividad.lblStarttime + '</label></td>' +
                       '<tr><td><label class="lbl-sm">Fecha Cierre:</label></td><td><label class="lbl-sm">' + objActividad.lstActividad.lblendtime + '</label></td>' +
                       '<tr><td><label class="lbl-sm">Solicitante:</label></td><td><label class="lbl-sm">' + objActividad.lstActividad.lblNombreUsuario + '</label></td>' +
                       '<tr><td><label class="lbl-sm">Perfl:</label></td><td style="width:25%">' + tempImagenOrigen + '</td></tr>' +
                       '</table></div>');

                    if (objActividad.lstActividadCondicion.length > 0) {
                        $('#divDinamicoDetalleaActividad').append(
                           '<div style="border: solid 1px #c3c3c3;"><br/>' +
                           '<table id="tblDetalleActividadCondicion" style="width: 98%">' +
                           '<th style="width:20%"><label class="lbl-xsm">CONDICIÓN:</label></th>' +
                           '<th style="width:20%"><label class="lbl-xsm">NOMBRE DOCUMENTO:</label></th>' +
                           '<th style="width:20%"><label class="lbl-xsm">DESCRIPCION DOCUMENTO:</label></th>' +
                           '<th style="width:20%"><label class="lbl-xsm">FECHA CARGUE</label></th>' +
                         //  '<th style="width:20%"><label class="lbl-xsm">RESPONSABLE</label></th>' +
                           '<th style="width:10%"><label class="lbl-xsm">DESCARGAR:</label></th><table></div>');

                        for (var i = 0; i < objActividad.lstActividadCondicion.length; i++) {
                            if (objActividad.lstActividadCondicion[i].lblFechaNotificacion.length > 0 && objActividad.lstActividadCondicion[i].lblNombreDocumento.search("Oficio"))
                                tempDocumento = '<td style="text-align:center;"><a href="' + objActividad.lstActividadCondicion[i].lblPathDocumento.replace("=seg", "=vital") + '" title="Ver Documento"  class="label-xsm" target="_blank"\><img src="../App_Themes/Img/documentos.png"></a></td></tr>'
                            else
                                tempDocumento = '<td><label class="lbl-xsm">Documento pendiente de Notificación</label></td>'

                            $('#tblDetalleActividadCondicion').append(
                                '<tr><td><label class="lbl-xsm">' + objActividad.lstActividadCondicion[i].lblNombreCondicion + '</label></td>' +
                                '<td><label class="lbl-xsm">' + objActividad.lstActividadCondicion[i].lblNombreDocumento + '</label></td>' +
                                '<td><label class="lbl-xsm">' + objActividad.lstActividadCondicion[i].lblDescripcionDocumento + '</label></td>' +
                                '<td><label class="lbl-xsm">' + objActividad.lstActividadCondicion[i].lblFechaCreacion + '</label></td>'
                               +tempDocumento+'</table></div>');
                             //   '<td><label class="lbl-xsm">' + objActividad.lstActividadCondicion[i].lblEncargado + '</label></td>' +
                        }
                    }
                }
            },
            failure: function (response) {
            },
            error: function (response) {
            }
        });

    });
});

$('#modalResultados').on('shown.bs.modal', function () {
});

$('#modalResultados').on('hidde', function () {
});


var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
};

function botonEnviarParametroBusqueda() {
    valorParametroBusqueda = $("#textSearch").val();
    if (valorParametroBusqueda.length > 3) {
        temporalNumeroPaginas = 0;
        tempPrimeraPagina = 1;
        tempMaximoPagina = 0;

        $("#divRespuestaResultados").empty();
        $('#dvOpciones').empty();
        $("#ulPaginador").empty();
        $("#divError").css("display", "none");

        EnviarParametroBusquedaMalla();
        $("#textSearch").text(valorParametroBusqueda);
        $("#spBusqueda").text(valorParametroBusqueda);
        $("#modalResultados").slideToggle("slow");

    }
}

function BotonCambio() {
    $("#textSearch").val("");
    $("#divRespuestaResultados").empty();
    $('#dvOpciones').empty();
    $("#ulPaginador").empty();
    $("#divError").css("display", "none");
    $("#spBusqueda").text("");


    var tabs = $("#exTab2");
    var panelTabs = $("#panelTabs");
    var ul = tabs.find("ul");

    $('#exTab2 ul li').each(function () {
        if ($(this)[0].id != "aInfGeneral") {
            $("#div" + $(this)[0].id.substring(2, 50)).empty();
            $("#div" + $(this)[0].id.substring(2, 50)).remove();
            $("#tbl" + $(this)[0].id.substring(2, 50)).remove();
            $(this).remove();
        }
    });

    $("#exTab2").css("display", "none");
}

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

function EnviarParametroBusqueda() {

    $('#divTipoBusqueda ul li').each(function () {
        if ($(this)[0].className == "active")
            valorTipoBusqueda = $(this)[0].id;
    });
    valorParametroBusqueda = $("#textSearch").val();
    if (alertaReferenciado.length > 0)
        valorParametroBusqueda = alertaReferenciado;
    if (valorParametroBusqueda.length >= 3) {
        var i;
        $.ajax({
            async: false,
            type: "POST",
            url: "ReporteTramiteCP.aspx",
            dataType: "json",
            data: { Accion: "EnviarParametroBusqueda", parametroBusqueda: valorParametroBusqueda, tipoBusqueda: valorTipoBusqueda },
            success: function (data) {
                if (data !== undefined) {
                    OnSuccess(data);
                }
                ocultarMalla();
            },
            complete: function (data) {
            },
            failure: function (response) {
                $("#divError").css("display", "block");
                $("#lblError").text("Error al iniciar la busqueda");
                ocultarMalla();
            },
            error: function (response) {
                $("#divError").css("display", "block");
                $("#lblError").text("Error al iniciar la busqueda");
                ocultarMalla();
            }
        });
    }
    else {
        $('#dvOpciones').append(
            '<div class="panel-heading"><h3 class="panel-title">La palabra que desea buscar debe tener mínimo tres(3) caracteres.!</h3></div>');
        ocultarMalla();
    }
}

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

function OnSuccess(datos) {
    if (datos.lstResultadosBusqueda.length > 0) {
        var lista = datos.lstResultadosBusqueda;
        var tempImagenOrigen = "";
        try {
            for (i = 0; i < lista.length; i++) {

                if (lista[i].ORIGEN == "SILA") {
                    lista[i].AUT_NOMBRE = "ANLA";
                    tempImagenOrigen = "<img style='max-height:50px;max-width:50px;' src='../App_Themes/Img/Autoridades/anla.png'/>";
                }
                else if (lista[i].AUT_NOMBRE == "" || lista[i].AUT_NOMBRE == "ANLA") {
                    lista[i].AUT_NOMBRE = "ANLA";
                    tempImagenOrigen = "<img style='max-height:50px;max-width:50px;' src='../App_Themes/Img/Autoridades/anla.png'/>";
                }
                else if (lista[i].AUT_NOMBRE == "MADS")
                    tempImagenOrigen = "<img style='max-height:20px;max-width:20px;' src='../App_Themes/Img/logoEntidad.png'/>";
                else if (lista[i].AUT_NOMBRE.length > 0)
                    tempImagenOrigen = "<img style='max-height:50px;max-width:50px;' src='../App_Themes/Img/Autoridades/" + lista[i].AUT_NOMBRE + ".jpg'/>";
                else if (lista[i].ORIGEN == "VITAL")
                    tempImagenOrigen = "<img style='max-height:50px;max-width:50px;' src='../App_Themes/Img/LogoVital.png'/>";
                else if (lista[i].ORIGEN == "SILA" || lista[i].ORIGEN == "SILAMC")
                    tempImagenOrigen = "<img style='max-height:50px;max-width:50px;' src='../App_Themes/Img/Autoridades/anla.png'/>";

                if (valorTipoBusqueda == "Publicación" || valorTipoBusqueda == "Publicacion") {
                    $('#divRespuestaResultados').append(
                        '<div style="border: solid 1px #c3c3c3;">' +
                        '<table id="tblRespuestaResultados" style="width: 98%">' +
                        '<tr><td><label class="lbl-sm">DOCUMENTO:</label>' +
                        '<img style="max-height:25px;max-width:25px;" class="clickable" src="../App_Themes/Img/Buscar.png" onclick="ConsultarDetallePublicacion(\'' + lista[i].TAR_SOL_ID + '\')"/></td>' +
                        '<td style="width:15%"><span class="spnImagenLogo">' + tempImagenOrigen + '</span></td></tr>' +
                        '<tr><td><label class="label-xsm">' + lista[i].SOL_NUM_SILPA + '</label></td></tr>' +
                        '<tr><td><label class="lbl-xsm">Tipo de Tramite: ' + lista[i].TRA_NOMBRE + '</label></td></tr>' +
                        '<tr><td colspan="2"><label class="lbl-xsm">Numero: ' + lista[i].NUM_DOCUMENTO + '</label></td></tr>' +
                        '<tr><td colspan="2"><label class="lbl-xsm">Expediente: ' + lista[i].EXPEDIENTE + '</label></td></tr>' +
                        '<tr><td colspan="2"><label class="lbl-xsm">Autoridad Ambiental: ' + lista[i].AUT_NOMBRE + '</label></td></tr>' +
                        '<tr><td colspan="3"><label class="lbl-sm">Nombre Proyecto: ' + lista[i].NOMBRE_PROYECTO.substring(0, 100) + '...</label></td></tr>' +
                        '<tr><td colspan="3"><label class="lbl-sm">Fecha de Publicación o Fijación: ' + lista[i].TAR_FECHA_CREACION + '</label></td></tr>' +
                        '<tr><td><label id="lblIdSolicitante" style="visibility: hidden;" class="lbl-xsm" >' + lista[i].SOL_ID_SOLICITANTE + '</label>' +
                        '<label id="lblTarSolId" style="visibility: hidden;" class="lbl-xsm" >' + lista[i].TAR_SOL_ID + '</label></td></tr></table></div>');
                }
                else {
                    $('#divRespuestaResultados').append(
                        '<div style="border: solid 1px #c3c3c3;">' +
                        '<table id="tblRespuestaResultados" style="width: 98%">' +
                        '<tr><td style="width:35%"><label class="lbl-sm"> Nº Tramite:</label></td>' +
                        '<td style="width:50%"><a class="label-sm" href="#" onclick="ConsultarDetalle(\'' + lista[i].SOL_NUM_SILPA + '\',\'' + lista[i].ORIGEN + '\', \'' + lista[i].TAR_SOL_ID + '\', \'' + lista[i].SOL_ID_SOLICITANTE + '\')">' + lista[i].SOL_NUM_SILPA + '</a>' +
                        '<td style="width:15%"><span class="spnImagenLogo">' + tempImagenOrigen + '</span></td></tr>' +
                        '<tr><td><label class="lbl-sm">Tipo de Tramite: </label></td><td colspan="2"><label class="lbl-sm">' + lista[i].TRA_NOMBRE + '</label></td></tr>' +
                        '<tr><td><label class="lbl-sm">Autoridad Ambiental:</label></td><td colspan="2"><label class="lbl-sm">' + lista[i].AUT_NOMBRE + '</label></td></tr>' +
                        '<tr><td colspan="3"><label class="lbl-sm">Nombre Proyecto:' + lista[i].NOMBRE_PROYECTO.substring(0, 200) + '...</label></td></tr>' +
                        '<tr><td><label class="lbl-sm">Fecha Creacion:</label></td><td colspan="2"><label class="lbl-sm">' + lista[i].TAR_FECHA_CREACION + '</label></td></tr>' +
                        '<tr><td><label class="lbl-sm">Ubicación:</label></td><td colspan="2"><label class="lbl-sm">' + lista[i].MUNICIPIO + '</label>' +
                        '<label id="lblIdSolicitante" style="visibility: hidden;" class="lbl-sm" >' + lista[i].SOL_ID_SOLICITANTE + '</label>' +
                        '<label id="lblTarSolId" style="visibility: hidden;" class="lbl-sm" >' + lista[i].TAR_SOL_ID + '</label></td></tr></table></div>');
                }
            }

            if (lista[0].temporalNumeroPaginas > 0) {
                if (lista[0].temporalNumeroPaginas == 1) {
                    $('#ulPaginador').append('<label class="lbl-sm"> Nº total de registros encontrados: ' + lista[0].temporalNumeroRegistros + ', agrupados en un total de: ' + lista[0].temporalNumeroPaginas + ' Páginas.</label>');
                }
                else {

                    if (tempPrimeraPagina == 1)
                        $('#ulPaginador').append('<li class="page-item disabled"><a class="page-link" href="#" tabindex="-1">Anterior</a></li>');
                    else if (tempPrimeraPagina > 1)
                        $('#ulPaginador').append('<li class="page-item"><a class="page-link" href="#' + tempPrimeraPagina + '" tabindex="-1" onclick="IrPagina(\'ANT' + tempPrimeraPagina + '\')">Anterior</a></li>');
                    else if (tempPrimeraPagina < 0) {
                        tempMaximoPagina = tempMaximoPagina + tempPrimeraPagina;
                        tempPrimeraPagina = (tempPrimeraPagina * (-1)) - 6;
                        $('#ulPaginador').append('<li class="page-item"><a class="page-link" href="#' + tempPrimeraPagina + '" tabindex="-1" onclick="IrPagina(\'ANT' + tempPrimeraPagina + '\')">Anterior</a></li>');
                    }

                    if (tempMaximoPagina <= lista[0].temporalNumeroPaginas)
                        tempMaximoPagina = parseInt(tempPrimeraPagina) + 5;

                    for (s = tempPrimeraPagina; s <= tempMaximoPagina && s <= lista[0].temporalNumeroPaginas; s++) {
                        $('#ulPaginador').append('<li class="page-item" id="li' + s + '"><a class="page-link" href="#' + s + '" onclick="IrPagina(\'' + s + '\')">' + s + '</a></li>');
                    }
                    if (lista[0].temporalNumeroPaginas > tempMaximoPagina)
                        $('#ulPaginador').append('<li class="page-item"><a class="page-link" href="#' + s + '" onclick="IrPagina(\'SIG' + s + '\')">...Siguiente</a></li>');
                    else
                        $('#ulPaginador').append('<li class="page-item disabled"><a class="page-link" href="#' + s + '">...Siguiente</a></li>');

                    $('#ulPaginador').append('<label class="lbl-sm"> Nº total de registros encontrados: ' + lista[0].temporalNumeroRegistros + ', agrupados en un total de: ' + lista[0].temporalNumeroPaginas + ' Páginas.</label>');

                }
            }

            Resaltar();
        }
        catch (e) {
            $("#divError").css("display", "block");
            $("#lblError").text("Error páginando Busqueda");
            ocultarMalla();
        }
    }
    else {
        quizoDecir();
    }
}

function quizoDecir() {
    var encontroReferencia = false;
    $.ajax({
        async: false,
        type: "POST",
        url: "ReporteTramiteCP.aspx",
        dataType: "json",
        data: { Accion: "QuisoDecirDepartamento" },
        success: function (data) {
            if (data !== undefined) {
                $('#dvOpciones').empty();
                for (i = 0; i < data.length; i++) {
                    if ((levenshteinDistance(data[i].DEP_NOMBRE, valorParametroBusqueda.toUpperCase())) <= 2) {
                        encontroReferencia = true;
                        $("#textSearch").text(data[i].DEP_NOMBRE);
                        $('#dvOpciones').append(
                            '<div class="panel-heading"><h3 class="panel-title">SU CONSULTA NO ARROJO RESULTADOS!</h3></div>' +
                            '<div class="panel-body"><label class="lbl-xsm">Quizás quiso decir...</label> <a class="label-sm" href="#" onclick="EnviarParametroBusquedaReferencia(\'' + data[i].DEP_NOMBRE + '\')">' + data[i].DEP_NOMBRE + '</a></div>'
                        )
                    }
                }
            }
        },
        complete: function (data) {

        },
        failure: function (response) {
            $("#divError").css("display", "block");
            $("#lblError").text("Error Buscando similitudes");
            ocultarMalla();
        },
        error: function (response) {
            $("#divError").css("display", "block");
            $("#lblError").text("Error Buscando similitudes");
            ocultarMalla();
        }
    });
    if (encontroReferencia == false) {
        $.ajax({
            async: false,
            type: "POST",
            url: "ReporteTramiteCP.aspx",
            dataType: "json",
            data: { Accion: "QuisoDecirAutoridad" },
            success: function (data) {
                if (data !== undefined) {
                    $('#dvOpciones').empty();
                    for (i = 0; i < data.length; i++) {
                        if ((levenshteinDistance(data[i].AUT_NOMBRE, valorParametroBusqueda.toUpperCase())) <= 2) {
                            encontroReferencia = true;
                            $("#textSearch").text(data[i].AUT_NOMBRE);
                            $('#dvOpciones').append(
                                '<div class="panel-heading"><h3 class="panel-title">SU CONSULTA NO ARROJO RESULTADOS!</h3></div>' +
                                '<div class="panel-body"><label class="lbl-xsm">Quizás quiso decir...</label> <a class="label-sm" href="#" onclick="EnviarParametroBusquedaReferencia(\'' +
                                data[i].AUT_NOMBRE + '\')">' + data[i].AUT_NOMBRE + '</a></div>'
                            )
                        }
                    }
                }
            },
            complete: function (data) {

            },
            failure: function (response) {
                alert(response.d);
            },
            error: function (response) {
                alert(response.d);
            }
        });


    }
    if (encontroReferencia == false) {
        $.ajax({
            async: false,
            type: "POST",
            url: "ReporteTramiteCP.aspx",
            dataType: "json",
            data: { Accion: "QuisoDecirMunicipio" },
            success: function (data) {
                if (data !== undefined) {
                    $('#dvOpciones').empty();
                    for (i = 0; i < data.length; i++) {
                        if ((levenshteinDistance(data[i].MUN_NOMBRE, valorParametroBusqueda.toUpperCase())) <= 2) {
                            encontroReferencia = true;
                            $("#textSearch").text(data[i].MUN_NOMBRE);
                            $('#dvOpciones').append(
                                '<div class="panel-heading"><h3 class="panel-title">SU CONSULTA NO ARROJO RESULTADOS!</h3></div>' +
                                '<div class="panel-body"><label class="lbl-xsm">Quizás quiso decir...</label> <a class="label-sm" href="#" onclick="EnviarParametroBusquedaReferencia(\'' +
                                data[i].MUN_NOMBRE + '\')">' + data[i].MUN_NOMBRE + '</a></div>'
                            )
                        }
                    }
                }
            },
            complete: function (data) {

            },
            failure: function (response) {
                alert(response.d);
            },
            error: function (response) {
                alert(response.d);
            }
        });
    }
    ocultarMalla();
}

function levenshteinDistance(a, b) {
    if (a.length == 0) return b.length;
    if (b.length == 0) return a.length;

    var matrix = [];

    // increment along the first column of each row
    var i;
    for (i = 0; i <= b.length; i++) {
        matrix[i] = [i];
    }

    // increment each column in the first row
    var j;
    for (j = 0; j <= a.length; j++) {
        matrix[0][j] = j;
    }

    // Fill in the rest of the matrix
    for (i = 1; i <= b.length; i++) {
        for (j = 1; j <= a.length; j++) {
            if (b.charAt(i - 1) == a.charAt(j - 1)) {
                matrix[i][j] = matrix[i - 1][j - 1];
            } else {
                matrix[i][j] = Math.min(matrix[i - 1][j - 1] + 1, // substitution
                    Math.min(matrix[i][j - 1] + 1, // insertion
                        matrix[i - 1][j] + 1)); // deletion
            }
        }
    }

    return matrix[b.length][a.length];
}

function ConsultarDetalle(strNumeroSolicitud, valorOrigen, id_TarSolId, sol_id_solicitante) {
    $("#divError").css("display", "none");
    $('#chart-container').empty();
    $("#modalResultados").slideToggle("slow");
    var numeroSol = strNumeroSolicitud;
    $.ajax({
        async: false,
        type: "POST",
        url: "ReporteTramiteCP.aspx",
        dataType: "json",
        data: { Accion: "ConsultarDetalleSolicitud", parametroDetalle: strNumeroSolicitud, idSolicitante: sol_id_solicitante, sol_id: id_TarSolId, origen: valorOrigen },
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

function ConsultarDetallePublicacion(strNumeroSolicitud) {
    $("#modalResultados").slideToggle("slow");
    $("#divError").css("display", "none");
    $.ajax({
        async: false,
        type: "POST",
        url: "ReporteTramiteCP.aspx",
        dataType: "json",
        data: { Accion: "ConsultarDetallePublicacion", parametroDetalle: strNumeroSolicitud },
        success: function (data) {
            if (data !== undefined) {
                ImprimirDetallePublicacionMalla(data);
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

function ImprimirDetallePublicacionMalla(datos) {
    $("#divCargando").css("display", "block");
    setTimeout(function () { ImprimirDetallePublicacion(datos); }, 500);
}

function ImprimirDetalleMalla(datos) {
    $("#divCargando").css("display", "block");
    setTimeout(function () { ImprimirDetalle(datos); }, 1500);
}

function listaHijos(idpadre, listaGeneral) {

    var icono = ""
    var arregloHijo = []

    for (var si = 0; si < listaGeneral.length; si++) {
        if (listaGeneral[si].ejecutada == 0)
            icono = "<span class='glyphicon glyphicon-exclamation-sign' title='Tarea pendiente'></span>";
        else if (listaGeneral[si].ejecutada == 1)
            icono = "<span class='glyphicon glyphicon-ok icon-success' title='Tarea pendiente'></span>";

        if (listaGeneral[si].id_Padre == idpadre)
            arregloHijo.push([listaGeneral[si].id, '"' + listaGeneral[si].name] + icono + '"')
    }
    return arregloHijo
}

function llenarArrayInfo(idPadre, datos, d2) {

    try {
        var arrListChildren = [];
        var d1 = datos;
        var t1 = jQuery.grep(datos, function (a) { return a.id_Padre === idPadre.toString(); });
        var icono = ""
        var cls = ""
        if (t1 && t1.length > 0) {
            if (idPadre.toString() !== '0') {

                for (var i = 0; i < t1.length; i++) {
                    var t = t1[i];
                    if (t.ejecutada == 0) {
                        icono = "<span class='glyphicon glyphicon-exclamation-sign' title='Tarea pendiente'></span>";
                        cls = 'bottom-pendiente';
                    }
                    else {
                        icono = "<span class='glyphicon glyphicon-ok icon-success' title='Tarea ejecutada' data-toggle='modal' data-target='#ModalTramite' data-id='" + t.id + "'></span>";
                        cls = 'bottom-ejecutada';
                    }

                    d2.children.push({
                        id: t.id,
                        name: '',
                        title: t.name + ' ' + icono,
                        className: cls,
                        children: []
                    });
                }

                for (var ii = 0; ii < d2.children.length; ii++) {
                    var tt1 = d2.children[ii];
                    llenarArrayInfo(tt1.id, datos, tt1);
                }
            }
            else {

                arrListChildren.push({
                    name: t1[0].name,
                    title: t1[0].name,
                    id: t1[0].id,
                    children: []
                });

                llenarArrayInfo(t1[0].id, datos, arrListChildren[0]);
            }

        }
        return arrListChildren;
    }
    catch (e) { }
}

function ImprimirDetalle(datos) {
    try {

        debugger
        $('#exTab2 ul li').each(function () {
            $(this).removeAttr('style');
        });
        $('#panelTabs div').each(function () {
            if (this.id == "1")
                $(this).removeAttr('style');
        });
        $('#exTab2 ul li').each(function () {
            if (this.id == "liPublicaciones")
                $(this).css("display", "none");
        });
        if (datos != null) {
            $('a[href="#1"]').click();
            $("#exTab2").css("display", "block");
            $("#exTab2").addClass('active');
            $(".nav-tabs a").click(function () {
                $(this).tab('show');
            });

            $("#lblExpedientesRelacionados").text("");
            $("#lblVitalSolicitud").text(datos[0].lblnumeroExpediente);
            $("#lblSolicitante").text(datos[0].lblSolicitante);
            $("#lblcodigo_expediente").text(datos[0].lblCodigoExpediente);
            $("#lblnombre_expediente").text(datos[0].lblNombreProyectoValue);
            $("#lbldescripcion_expediente").text(datos[0].lblDescripcionProyectoValue);
            $("#lblSector").text(datos[0].lblSector);
            $("#lblUbicacion").text(datos[0].lblUbicacion);
            $("#lblAutoridadAmbiental").text(datos[0].lblAutoridadAmbiental);

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

            $('#exTab2 ul li').each(function () {
                if ($(this)[0].id != "aInfGeneral") {
                    $("#div" + $(this)[0].id.substring(2, 50)).empty();
                    $("#div" + $(this)[0].id.substring(2, 50)).remove();
                    $("#tbl" + $(this)[0].id.substring(2, 50)).remove();
                    $(this).remove();
                }
            });

            if (datos[0].lstResumenEstado != null) {
                if (datos[0].lstResumenEstado.length != undefined) {
                    if (datos[0].lstResumenEstado.resumenTramite[0].id != "") {///CONDICION PARA LLENAR EL ARBOL
                        //  $("<li id=liResumenTramite" + "><a href='#chart-container' data-toggle='tab'>Estado Tramite</a></li>").appendTo(ul);
                        //$("<div class='tab-pane active' id='chart-container' style='overflow-y: scroll;'><div>").appendTo(panelTabs);
                        var listaNodos = llenarArrayInfo("0", datos[0].lstResumenEstado.resumenTramite);

                        $('#chart-container').empty();
                        var oc = $('#chart-container').orgchart({
                            'data': ''
                            //, 'nodeContent': 'title', 'direction': 'l2r'
                        });
                        oc.init({
                            'data': listaNodos[0], 'nodeContent': 'title'
                            //, 'direction': 'l2r'
                        });
                    }
                    else {
                        $('#chart-container').append("<br/><br/><br/><label class='CellResultadoTituloSeg' style='font-size:14px !important'>LA SOLICITUD BUSCADA NO CUENTA CON UN DIAGRAMA DE ESTADO ASOCIADO, PARA INFORMACIÓN DETALLADA CONSULTE EL BUSCADOR DE VITAL </label><br/>");
                    }
                }
                else {
                    $('#chart-container').append("<br/><br/><br/><label class='CellResultadoTituloSeg' style='font-size:14px !important'>LA SOLICITUD BUSCADA NO CUENTA CON UN DIAGRAMA DE ESTADO ASOCIADO, PARA INFORMACIÓN DETALLADA CONSULTE EL BUSCADOR DE VITAL </label><br/>");
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

                $("#tblInformacionGeneral").append('<tr id="lbEiaDocumento"><td><label class="label-sm">DOCUMENTO EIA:</label></td><td><label class="label-xsm">' + datos[0].lblNombreDocumento + '</label></td>');
                $("#tblInformacionGeneral").append('<tr id="lbEiaDocumentoDescarga"><td><label class="label-sm">DESCARGAR DOCUMENTO EIA:</label>' +
                        "</td><td><a href='#' title='DESCARGAR'  class='label-xsm' onclick=\'MostrarDocumentos(" + this.datosSerializado + ")'\><img src='../App_Themes/Img/documentos.png'></a></td>");


            }


        }
        ocultarMalla();
    }
    catch (e) {
        ocultarMalla();
        $("#divError").css("display", "block");
        $("#lblError").text("Error Imprimiendo el Detalle Busqueda");
    }
}

function ImprimirDetallePublicacion(datos) {
    try {
        if (datos != null) {
            $('#divPublicaciones').empty();
            var tabs = $("#exTab2");
            var ul = tabs.find("ul");
            var panelTabs = $("#panelTabs");
            var tempExiste = false;

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
                '<tr><td style="text-align:left;width:20%"><label class="label-sm">Título Publicación</label></td>' +
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
        $("#divError").css("display", "block");
        $("#lblError").text("Error Imprimiendo el Detalle Busqueda");
        ocultarMalla();
    }
}

function MostrarDocumentos(lsDocumentosSeguimiento) {
    $("#divError").css("display", "none");
    $("#divCargando").css("display", "block");
    setTimeout(function () {
        $.ajax({
            async: false,
            type: "POST",
            url: "ReporteTramiteCP.aspx",
            dataType: "json",
            data: { Accion: "MostrarDocumentos", lstDocumentosSeguimiento: JSON.stringify(lsDocumentosSeguimiento) },
            success: function (data) {
                if (data !== undefined) {
                    if (data.search("ERROR") == 0) {
                        data = data.replace("ERROR", "")
                        $("#divError").css("display", "block");
                        $("#divError").text(data);
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
                $("#divError").css("display", "block");
                $("#divError").text("Error al Mostrar Documentos");
                ocultarMalla();
            },
            error: function (response) {
                $("#divError").css("display", "block");
                $("#lblError").text("Error al Mostrar Documentos");
                ocultarMalla();
            }
        });
    }, 1500);
}

function MostrarDocumentosPublicaciones(lsDocumentosPublicaciones) {
    $("#divError").css("display", "none");
    $("#divDinamicoDocumentos").empty();
    $.ajax({
        async: false,
        type: "POST",
        url: "ReporteTramiteCP.aspx",
        dataType: "json",
        data: { Accion: "MostrarDocumentosPublicaciones", listaDocumentosPublicaciones: JSON.stringify(lsDocumentosPublicaciones) },
        success: function (data) {
            if (data !== undefined) {
                if (data[0].ListaDocumentos != undefined) {
                    $("#divDinamicoDocumentos").append("<div class='row'><table id='tblDocs'><tr>" +
                        "<th style='text-align: center;'><label class='label-sm'>ARCHIVO</label></th>" +
                        "<th style='text-align: center;'><label class='label-sm'> DESCARGAR </label></th></tr></table></div>");
                    for (var i = 0; i < data[0].ListaDocumentos.length; i++) {
                        datosSerializado = JSON.stringify(data[0].ListaDocumentos[i]);
                        $("#tblDocs").append('<tr><td style="text-align: center;">' +
                            '<label class="label-xsm">' + data[0].ListaDocumentos[i].NombreArchivo + '</label></td>' +
                            "<td style='text-align: center;'><a href='#' title='Descargar Documento'  class='label-xsm' onclick=\'DescargarDocumentoPublicado(" + datosSerializado + ")'\><img src='../App_Themes/Img/documentos.png'></a></td>");
                    }
                    $('#modalDocumentos').modal('show');
                }
                else if (data.search("ERROR") == 0) {
                    $("#divError").css("display", "block");
                    $("#divError").text(data);
                }
                return false;
            }
            ocultarMalla();
        },
        failure: function (response) {
            $("#divError").css("display", "block");
            $("#divError").text("Error al Mostrar Documentos");
            ocultarMalla();
        },
        error: function (response) {
            $("#divError").css("display", "block");
            $("#lblError").text("Error al Mostrar Documentos");
            ocultarMalla();
        }
    });
}

function ConsultarDocumentoActividad(lsDocumentoActividad) {
    $("#divError").css("display", "none");
    $("#divCargando").css("display", "block");
    setTimeout(function () {
        $.ajax({
            async: false,
            type: "POST",
            url: "ReporteTramiteCP.aspx",
            dataType: "json",
            data: { Accion: "MostrarDocumentoResumenActividad", lsDocumentoActividad: JSON.stringify(lsDocumentoActividad) },
            success: function (data) {
                if (data !== undefined) {
                    if (data.search("ERROR") == 0) {
                        data = data.replace("ERROR", "")
                        $("#divError").css("display", "block");
                        $("#divError").text(data);
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
                $("#divError").css("display", "block");
                $("#divError").text("Error al Mostrar Documentos");
                ocultarMalla();
            },
            error: function (response) {
                $("#divError").css("display", "block");
                $("#lblError").text("Error al Mostrar Documentos");
                ocultarMalla();
            }
        });
    }, 1500);
}


function DescargarDocumentoPublicado(documento) {
    $("#divError").css("display", "none");
    setTimeout(function () {
        $.ajax({
            async: false,
            type: "POST",
            url: "ReporteTramiteCP.aspx",
            dataType: "json",
            data: { Accion: "PublicacionDescarga", documentoDescarga: JSON.stringify(documento) },
            success: function (data) {
                if (data !== undefined) {
                    if (data.search("ERROR") == 0) {
                        data = data.replace("ERROR", "")
                        $("#divError").css("display", "block");
                        $("#divError").text(data);
                    }
                    else {
                        var w = 770, h = 300, l = (screen.availWidth - w) / 2, t = (screen.availHeight - h) / 2, popPage = '.popup';
                        window.open(data, "window", "width= " + w + ",height=" + h + ",left=" + l + ",top=" + t + ", scrollbars = yes, location = no, toolbar = no, menubar = no, status = no");
                        return false;
                    }

                }
                ocultarMalla();
            },
            failure: function (response) {
                $("#divError").css("display", "block");
                $("#divError").text("Error al Mostrar Documentos");
                ocultarMalla();
            },
            error: function (response) {
                $("#divError").css("display", "block");
                $("#lblError").text("Error al Mostrar Documentos");
                ocultarMalla();
            }
        });
    }, 200);
}

function IrPagina(numeroPagina) {
    var resaltar = "li" + numeroPagina;
    if (numeroPagina.search("ANT") == 0) {
        numeroPagina = numeroPagina.slice(3);
        tempPrimeraPagina = -numeroPagina;
    }
    else if (numeroPagina.search("SIG") == 0) {
        numeroPagina = numeroPagina.slice(3);
        tempPrimeraPagina = numeroPagina;
    }
    $("#divCargando").css("display", "block");
    setTimeout(function () {
        var valorTipoBusqueda = "";
        $('#divTipoBusqueda ul li').each(function () {
            if ($(this)[0].className == "active")
                valorTipoBusqueda = $(this)[0].id;
        });
        $.ajax({
            async: false,
            type: "POST",
            url: "ReporteTramiteCP.aspx",
            dataType: "json",
            data: { Accion: "IrPagina", numeroPagina: numeroPagina, parametroBusqueda: valorParametroBusqueda, tipoBusqueda: valorTipoBusqueda },
            success: function (data) {
                if (data !== undefined) {
                    $('#dvOpciones').empty();
                    $("#divRespuestaResultados").empty();
                    $("#ulPaginador").empty();
                    OnSuccess(data);
                }
                ocultarMalla();
            },
            complete: function (data) {
                ocultarMalla();
                $('#divPaginador ul li').each(function () {
                    if ($(this)[0].id == resaltar) {
                        $(this)[0].className = "page-item active";
                    }
                });
            },
            failure: function (response) {
                ocultarMalla();
                $("#divError").css("display", "block");
                $("#lblError").text("Error al Paginar Resultados");
            },
            error: function (response) {
                ocultarMalla();
                $("#divError").css("display", "block");
                $("#lblError").text("Error al Paginar Resultados");
            }
        });
    }, 500);
}

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
function Resaltar() {
    //var matches = document.querySelectorAll("#divRespuestaResultados div table tr td label");
    //for (var i = 0, l = matches.length; i < l; i++) {
    //    var searchregexp = new RegExp(valorParametroBusqueda.replace(/[.*+?^${}()|[\]\\]/g, '\\$&'), "gi");
    //    //$& will maintain uppercase and lowercase characters.
    //  //  $(this).html($(this).html().replace(searchregexp, '<span class ="highlight">$&</span>'));
    ////    if (matches[i].textContent.c == valorParametroBusqueda) {
    ////        matches[i].css("class", "highlight");
    ////        // $(this).html($(this).html().replace(searchregexp, '<span class ="highlight">$&</span>'));
    ////    }
    //}
    $('#divRespuestaResultados div table tr td label').each(function () {

        if (valorParametroBusqueda.search(new RegExp("\\+")) > 0) {
            var separador = "+";
            var arregloDeSubCadenas = [];
            var limite = 4,
                arregloDeSubCadenas = valorParametroBusqueda.split(separador, limite)
            for (v = 0; v < arregloDeSubCadenas.length; v++) {
                var searchregexp = new RegExp(arregloDeSubCadenas[v].replace(/[.*+?^${}()|[\]\\]/g, '\\$&'), "gi");
                $(this).html($(this).html().replace(searchregexp, '<span class ="highlight">$&</span>'));
            }

        } else if (valorParametroBusqueda.search(new RegExp("\\\"")) > 0) {
            var separador = "\"";
            var arregloDeSubCadenas = [];
            var limite = 4,
                arregloDeSubCadenas = valorParametroBusqueda.split(separador, limite)
            for (v = 0; v < arregloDeSubCadenas.length; v++) {
                var searchregexp = new RegExp(arregloDeSubCadenas[v].replace(/[.*+?^${}()|[\]\\]/g, '\\$&'), "gi");
                $(this).html($(this).html().replace(searchregexp, '<span class ="highlight">$&</span>'));
            }
        }
        else {
            var searchregexp = new RegExp(valorParametroBusqueda.replace(/[.*+?^${}()|[\]\\]/g, '\\$&'), "gi");
            $(this).html($(this).html().replace(searchregexp, '<span class ="highlight">$&</span>'));
        }
    });
}