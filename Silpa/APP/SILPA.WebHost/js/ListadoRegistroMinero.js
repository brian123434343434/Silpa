/// <reference path="jquery-1.8.2-vsdoc.js" />
$.ajaxSetup({
    cache: false,
    type: "POST",
    dataType: "json",
    contentType: "application/json; charset=utf-8",
    async: true
});

function FormatForm(strValor) {
    if (strValor.indexOf('?') != -1)
        strValor = strValor.substring(0, strValor.indexOf('?'));
    else
        strValor = strValor;
    return strValor;
};
function SelectAll(tb, chk) {
   var check = chk.checked;
    $("#" + tb + " tr td :checkbox").each(function() {
        this.checked = check;
    });
}

