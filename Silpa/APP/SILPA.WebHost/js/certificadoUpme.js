$.ajaxSetup({
    cache: false,
    type: "POST",
    dataType: "json",
    contentType: "application/json; charset=utf-8",
    async: true
});
$(document).ready(function () {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    $("#txtCalculoANLAData").numeric();
    $("#txtNumeroReferencia").numeric();
    function EndRequestHandler(sender, args) {
        $("#txtCalculoANLAData").numeric();
        $("#txtNumeroReferencia").numeric();
    }
});
