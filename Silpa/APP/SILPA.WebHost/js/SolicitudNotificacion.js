var _modalDialog = null;

function MostrarCargarExpediente() {
    _modalDialog = $("#divCargarExpediente").dialog({
        title: "Adicionar Expediente",
        autoOpen: false,
        modal: true,
        width: 700
    });

    _modalDialog.parent().appendTo(jQuery("form:first"));
    _modalDialog.dialog("open");
}

function ValAceptoTerminos(oSrouce, args) {
    if ($("#chkAceptoTerminos").is(':checked')) {
        args.IsValid = true;
    }
    else {
        args.IsValid = false;
    }
}

function ValExpedientes(oSrouce, args) {
    if ($("#RdbNotificarExpediente").is(':checked')) {
        if ($("#lstExpedientesNotificar option").length > 0) {
            args.IsValid = true;
        }
        else {
            args.IsValid = false;
        }
    }
    else {
        args.IsValid = true;
    }
}

$(document).ready(function () {
    $("#btnAdicionarExpediente").click(function () {
        MostrarCargarExpediente();
    });

    $("#btnAdicionarExpedienteModal").click(function () {
        _modalDialog.dialog("close");
    });

    $("#btnCancelarExpedienteModal").click(function () {
        if ($("#cboAutoridadAmbiental").val() != "-1" && $("#cboExpedientes").val() != "-1") {
            _modalDialog.dialog("close");
        }
    });

    $("#lstExpedientesNotificar").change(function () {
        if ($("#lstExpedientesNotificar option:not(:selected)").length == 0) {
            $("#chkSeleccionarTodosExpedientes").prop("checked", true);
        }
        else {
            $("#chkSeleccionarTodosExpedientes").prop("checked", false);
        }
    });

    $("#chkSeleccionarTodosExpedientes").click(function () {
        if ($("#chkSeleccionarTodosExpedientes").is(':checked')) {
            $('#lstExpedientesNotificar option').prop('selected', true);
        }
        else {
            $('#lstExpedientesNotificar option').prop('selected', false);
        }
    });

    $('#txtContrasena').keypad({
        randomiseNumeric: true,
        prompt: '',
        closeText: 'Cerrar',
        clearText: 'Limpiar',
        backText: '<<'
    });

    var prm = Sys.WebForms.PageRequestManager.getInstance();

    prm.add_endRequest(function () {

        $('#txtContrasena').keypad({
            randomiseNumeric: true,
            prompt: '',
            closeText: 'Cerrar',
            clearText: 'Limpiar',
            backText: '<<'
        });

        $("#btnAdicionarExpediente").click(function () {
            MostrarCargarExpediente();
        });

        $("#btnAdicionarExpedienteModal").click(function () {
            //(Verificar que se halla seleccionado datos
            if ($("#cboAutoridadAmbiental").val() != "-1" && $("#cboExpedientes").val() != "-1") {
                _modalDialog.dialog("close");
            }
        });

        $("#btnCancelarExpedienteModal").click(function () {
            _modalDialog.dialog("close");
        });

        $("#lstExpedientesNotificar").change(function () {
            if ($("#lstExpedientesNotificar option:not(:selected)").length == 0) {
                $("#chkSeleccionarTodosExpedientes").prop("checked", true);
            }
            else {
                $("#chkSeleccionarTodosExpedientes").prop("checked", false);
            }
        });

        $("#chkSeleccionarTodosExpedientes").click(function () {
            if ($("#chkSeleccionarTodosExpedientes").is(':checked')) {
                $('#lstExpedientesNotificar option').prop('selected', true);
            }
            else {
                $('#lstExpedientesNotificar option').prop('selected', false);
            }
        });
    });
});