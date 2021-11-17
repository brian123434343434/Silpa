$(document).ready(function () {
    Calendarios();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

    function EndRequestHandler(sender, args) {
        Calendarios();
    }
    function Calendarios() {
        $("#TxtFechaExp").datetimepicker({ format: 'd/m/Y', lang: 'es', timepicker: false });
        $("#TxtVigenciaDesde").datetimepicker({ format: 'd/m/Y', lang: 'es', timepicker: false });
        $("#TxtVigenciaHasta").datetimepicker({ format: 'd/m/Y', lang: 'es', timepicker: false });
    }

});