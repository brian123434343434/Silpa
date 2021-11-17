//Para utilizar en la pagina se debe realizar los siguientes pasos:
//    1. Crear el div referenciado la clase burbujaAyuda EJ:
//    <div class='burbujaAyuda'></div>
//    2. Colocar span con clase botonAyuda en el lugar donde se mostrara los datos. Se debe especificar el id y colocar como atributo title el texto del mensaje de ayuda Ej:
//    <span id="spnAyuda" class="botonAyuda" title="Ayuda"></span>


var _strControlAyuda = "";

function mostrarAyuda(id, event, mensaje, posicionX, posicionY) {
    if (!$(".burbujaAyuda").is(":visible") || _strControlAyuda != id) {
        $(".burbujaAyuda").css({ "position": "absolute", "top": event.pageY + "px", "left": event.pageX + "px", "display": "block", "z-index": "100004" });
        $(".body").html(mensaje);
        $(".burbujaAyuda").show();
        _strControlAyuda = id;
    }
    else {
        // $(".burbujaAyuda").css({ "position": "absolute", "top": "-1000px", "left": (event.pageX - posicionX) + "px", "display": "none" });
        $(".burbujaAyuda").css({ "position": "absolute", "top": "auto", "left": (event.pageX - posicionX) + "px", "display": "none" });
    }
}

$(function () {
    $(".burbujaAyuda").html("<div class='close'></div><div class='body'></div>");
    $(".burbujaAyuda").hide();

    $('.botonAyuda').click(function (e) {
        if (typeof ($(this).attr("divModal")) == 'undefined' || $(this).attr("divModal") == null || $(this).attr("divModal") == "") {
            mostrarAyuda($(this).attr("id"), e, $(this).attr("title"), 0, 0);
        }
        else {
            mostrarAyuda($(this).attr("id"), e, $(this).attr("title"), $("#" + $(this).attr("divModal")).position().left, $("#" + $(this).attr("divModal")).position().top);
        }
    });

    $('.close').click(function (e) {
        $(".body").html("");
        // $(".burbujaAyuda").css({ "position": "absolute", "top": "-1000px", "left": (event.pageX) + "px", "display": "none" });
        $(".burbujaAyuda").css({ "position": "absolute", "top": "auto", "left": (event.pageX) + "px", "display": "none" });
    });

    $(".burbujaAyudaUP").html("<div class='closeUP'></div><div class='bodyUP'></div>");
    $(".burbujaAyudaUP").hide();

    $('.botonAyudaUP').click(function (e) {
        if (typeof ($(this).attr("divModal")) == 'undefined' || $(this).attr("divModal") == null || $(this).attr("divModal") == "") {
            mostrarAyuda($(this).attr("id"), e, $(this).attr("title"), 0, 0);
        }
        else {
            mostrarAyuda($(this).attr("id"), e, $(this).attr("title"), $("#" + $(this).attr("divModal")).position().left, $("#" + $(this).attr("divModal")).position().top);
        }
    });

    $('.closeUP').click(function (e) {
        $(".body").html("");
        $(".burbujaAyudaUP").css({ "position": "absolute", "top": "-1000px", "left": (event.pageX) + "px", "display": "none" });
    });

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {

        $(".burbujaAyudaUP").html("<div class='close'></div><div class='body'></div>");
        $(".burbujaAyudaUP").hide();

        $('.botonAyudaUP').click(function (e) {
            if (typeof ($(this).attr("divModal")) == 'undefined' || $(this).attr("divModal") == null || $(this).attr("divModal") == "") {
                mostrarAyuda($(this).attr("id"), e, $(this).attr("title"), 0, 0);
            }
            else {
                mostrarAyuda($(this).attr("id"), e, $(this).attr("title"), $("#" + $(this).attr("divModal")).position().left, $("#" + $(this).attr("divModal")).position().top);
            }
        });

        $('.closeUP').click(function (e) {
            $(".body").html("");
            $(".burbujaAyudaUP").css({ "position": "absolute", "top": "-1000px", "left": (event.pageX) + "px", "display": "none" });
        });
    });
});



