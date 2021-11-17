$.ajaxSetup({
    cache: false,
    type: "POST",
    dataType: "json",
    contentType: "application/json; charset=utf-8",
    async: true
});

$(document).ready(function () {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    Calendarios();

    $("#TxtRangoDesde").numeric();
    $("#TxtRangoHasta").numeric();
    $("#txtCntAlertaSerie").numeric();
    $("#TxtRangoDesdeEdit").numeric();
    $("#TxtRangoHastaEdit").numeric();
    $("#TxtNumeroSalvoconducto").numeric();

    function EndRequestHandler(sender, args) {
        Calendarios();
        $("#TxtRangoDesde").numeric();
        $("#TxtRangoHasta").numeric();
        $("#txtCntAlertaSerie").numeric();
        $("#TxtRangoDesdeEdit").numeric();
        $("#TxtRangoHastaEdit").numeric();
        $("#TxtNumeroSalvoconducto").numeric();
    }
    function Calendarios() {
        $("#TextBoxFechaDesde").datetimepicker({ format: 'd/m/Y', lang: 'es', timepicker: false });
        $("#TextBoxFechaHasta").datetimepicker({ format: 'd/m/Y', lang: 'es', timepicker: false });
        $("#TxtFechaExp").datetimepicker({ format: 'd/m/Y', lang: 'es', timepicker: false });
        $("#TxtVigenciaDesde").datetimepicker({ format: 'd/m/Y', lang: 'es', timepicker: false });
        $("#TxtVigenciaHasta").datetimepicker({ format: 'd/m/Y', lang: 'es', timepicker: false });
        $("#TxtFecExpDesde").datetimepicker({ format: 'd/m/Y', lang: 'es', timepicker: false });
        $("#TxtFecExpHasta").datetimepicker({ format: 'd/m/Y', lang: 'es', timepicker: false });

    }

});

function ValidarInformacion() {
    var mensaje = '';
    var contact_form = document.getElementById('dContactForm');
    if (dContactForm !== null) {
        var tablas = dContactForm.getElementsByTagName('TABLE');
        if (tablas !== undefined && tablas.length > 0) {
            for (var i = 0; i < tablas.length; i++) {
                var tbl1 = tablas[i];
                if (tbl1 !== undefined) {
                    var chk = tbl1.rows[0].getElementsByTagName("INPUT");
                    var desc = tbl1.rows[0].cells[0].innerText.replace("\r", "").replace("\n", "").replace("\t", "");
                    if (chk !== undefined && chk.length > 0) {
                        if (chk[0].type === 'checkbox') {
                            var ch = chk[0].checked;
                            if (!ch) {
                                mensaje += 'Debe Certificar Datos de la ' + desc + '\n';
                            }
                        }
                    }
                }
            }
        }
    }

    if (mensaje !== '') {
        alert(mensaje);
        return false;
    }

    return confirm('¿Esta seguro de emitir el salvoconducto?');
}
