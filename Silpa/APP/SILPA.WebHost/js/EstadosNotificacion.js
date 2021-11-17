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
    function EndRequestHandler(sender, args) {
        Calendarios();
    }
});

function Calendarios() {
    $("#txtEditFechaEstado").datetimepicker({
        format: 'd/m/Y',
        lang: 'es',
        timepicker: false
    });

    $("#txtFechaDesde").datetimepicker({
        format: 'd/m/Y',
        lang: 'es',
        timepicker: false
    });

    $("#txtFechaHasta").datetimepicker({
        format: 'd/m/Y',
        lang: 'es',
        timepicker: false
    });
    
    $("#txtFechaEstado").datetimepicker({
        format: 'd/m/Y H:i',
        lang: 'es',
        timepicker: true
    });
}