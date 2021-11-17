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
    $("#txtFechaSolicitudDesde").datepicker({ dateFormat: 'dd/mm/yy' });
    $("#txtFechaSolicitudHasta").datepicker({ dateFormat: 'dd/mm/yy' });

}