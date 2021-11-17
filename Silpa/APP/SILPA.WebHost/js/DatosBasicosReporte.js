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
    function Calendarios() {
        $("#TxtFecExpDesde").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#TxtFecExpHasta").datepicker({ dateFormat: 'dd/mm/yy' });
    }

});