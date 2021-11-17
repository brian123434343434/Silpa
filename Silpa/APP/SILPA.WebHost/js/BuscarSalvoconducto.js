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
    $("#txtNumeroIdentificacion").numeric();
    function EndRequestHandler(sender, args) {
        Calendarios();
        $("#txtNumeroIdentificacion").numeric();
    }
});
function Calendarios() {
    $("#txtFechaSolicitudDesde").datetimepicker({ format: 'd/m/Y', lang: 'es', timepicker: false });
    $("#txtFechaSolicitudHasta").datetimepicker({ format: 'd/m/Y', lang: 'es', timepicker: false });

}