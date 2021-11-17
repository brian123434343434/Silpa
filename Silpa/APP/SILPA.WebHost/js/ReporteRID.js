
$(document).ready(function () {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    $("#TxtFechaDesde").datepicker({ dateFormat: 'dd/mm/yy' });
    $("#TxtFechaHasta").datepicker({ dateFormat: 'dd/mm/yy' });
    function EndRequestHandler(sender, args) {
    $("#TxtFechaDesde").datepicker({ dateFormat: 'dd/mm/yy' });
    $("#TxtFechaHasta").datepicker({ dateFormat: 'dd/mm/yy' });
}
});


