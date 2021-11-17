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
    $("#txtValorEmisiones").numeric();
    $("#txtCostoEstiamdoFormulario").numeric();
    $("#txtAreaInfluencia").numeric();
    $("#txtValorVerificadas").numeric();
    $("#txtValorEstimadoDeforestacion").numeric();
    function EndRequestHandler(sender, args) {
        Calendarios();
        $("#txtValorEmisiones").numeric();
        $("#txtCostoEstiamdoFormulario").numeric();
        $("#txtAreaInfluencia").numeric();
        $("#txtValorVerificadas").numeric();
        $("#txtValorEstimadoDeforestacion").numeric();
    }
});

function Calendarios() {
    $("#txtFechaInicioIniciativa").datepicker({ dateFormat: 'dd/mm/yy' });
    $("#txtFechaFinalIniciativa").datepicker({ dateFormat: 'dd/mm/yy' });
}
