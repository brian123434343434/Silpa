function fntMessageBox()
{
  var msg = "funciona";
  alert(msg);
}


function deshabilitarBoton()
{
//    var a = Object();
//    var b = Object();
//    a = document.getElementById('ctl00_ContentPlaceHolder1_btnSolicitarTDREIA');
//    b = document.getElementById('ctl00_ContentPlaceHolder1_btnAceptar');
//    a.disabled=true;
//    b.disabled=true;
}

function popup(pagina) {
    var opciones = "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, width=956, height=500, top=85, left=140";
    window.open(pagina, "", opciones);
}

function confirmar(msg) {
    if (window.confirm(msg)) {
    }
    else {
        event.returnValue = false;
    }
}