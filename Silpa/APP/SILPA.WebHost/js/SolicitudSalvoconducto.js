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
    CamposNumericos();
    ConfigureValidators();
    function EndRequestHandler(sender, args) {
        Calendarios();
        CamposNumericos();
        ConfigureValidators();
    }
});

function MarcarArchivo(nombreControl) {
    var AsyncFileUpload = $get(nombreControl);
    var txts = AsyncFileUpload.getElementsByTagName("input");

    for (var i = 0; i < txts.length; i++) {
        if (txts[i].type == "file") {
            txts[i].style.backgroundColor = "#00FF00";
        }
    }
}

function UploadAnexo(sender, args) {
    var strObjeto = sender._element.id.replace("fuplDocumentoSoporte", "lnkCancelarArchivo")
    __doPostBack(strObjeto.replace(new RegExp("_", "g"), "$"), '');
}

function Calendarios() {
 
    $("#txtFechaDesde").datetimepicker({ format: 'd/m/Y', lang: 'es', timepicker: false });
    $("#txtFechaHasta").datetimepicker({ format: 'd/m/Y', lang: 'es', timepicker: false });
}
function CamposNumericos()
{
    $("#txtAlto").numeric();
    $("#txtAncho").numeric();
    $("#txtLargo").numeric();
    $("#txtNumeroActoAdministrativo").numeric();
    $("#txtNumeroIdentificacion").numeric();
    $("#txtCantidad").numeric();
    $("#txtCantVolTotal").numeric();
    $("#txtCantVolMovilizar").numeric();
    $("#txtNroSalvoconducto").numeric();
    $("#txtSUNAnterio").numeric();
    $("#txtIdentificacionTransportador").numeric();
}
function Unidades(num) {

    switch (num) {
        case 1: return "UN";
        case 2: return "DOS";
        case 3: return "TRES";
        case 4: return "CUATRO";
        case 5: return "CINCO";
        case 6: return "SEIS";
        case 7: return "SIETE";
        case 8: return "OCHO";
        case 9: return "NUEVE";
    }

    return "";
}

function Decenas(num) {

    decena = Math.floor(num / 10);
    unidad = num - (decena * 10);

    switch (decena) {
        case 1:
            switch (unidad) {
                case 0: return "DIEZ";
                case 1: return "ONCE";
                case 2: return "DOCE";
                case 3: return "TRECE";
                case 4: return "CATORCE";
                case 5: return "QUINCE";
                default: return "DIECI" + Unidades(unidad);
            }
        case 2:
            switch (unidad) {
                case 0: return "VEINTE";
                default: return "VEINTI" + Unidades(unidad);
            }
        case 3: return DecenasY("TREINTA", unidad);
        case 4: return DecenasY("CUARENTA", unidad);
        case 5: return DecenasY("CINCUENTA", unidad);
        case 6: return DecenasY("SESENTA", unidad);
        case 7: return DecenasY("SETENTA", unidad);
        case 8: return DecenasY("OCHENTA", unidad);
        case 9: return DecenasY("NOVENTA", unidad);
        case 0: return Unidades(unidad);
    }
}//Unidades()

function DecenasY(strSin, numUnidades) {
    if (numUnidades > 0)
        return strSin + " Y " + Unidades(numUnidades)

    return strSin;
}//DecenasY()

function Centenas(num) {

    centenas = Math.floor(num / 100);
    decenas = num - (centenas * 100);

    switch (centenas) {
        case 1:
            if (decenas > 0)
                return "CIENTO " + Decenas(decenas);
            return "CIEN";
        case 2: return "DOSCIENTOS " + Decenas(decenas);
        case 3: return "TRESCIENTOS " + Decenas(decenas);
        case 4: return "CUATROCIENTOS " + Decenas(decenas);
        case 5: return "QUINIENTOS " + Decenas(decenas);
        case 6: return "SEISCIENTOS " + Decenas(decenas);
        case 7: return "SETECIENTOS " + Decenas(decenas);
        case 8: return "OCHOCIENTOS " + Decenas(decenas);
        case 9: return "NOVECIENTOS " + Decenas(decenas);
    }

    return Decenas(decenas);
}//Centenas()

function Seccion(num, divisor, strSingular, strPlural) {
    cientos = Math.floor(num / divisor)
    resto = num - (cientos * divisor)

    letras = "";

    if (cientos > 0)
        if (cientos > 1)
            letras = Centenas(cientos) + " " + strPlural;
        else
            letras = strSingular;

    if (resto > 0)
        letras += "";

    return letras;
}//Seccion()

function Miles(num) {
    divisor = 1000;
    cientos = Math.floor(num / divisor)
    resto = num - (cientos * divisor)

    strMiles = Seccion(num, divisor, "UN MIL", "MIL");
    strCentenas = Centenas(resto);

    if (strMiles == "")
        return strCentenas;

    return strMiles + " " + strCentenas;

    //return Seccion(num, divisor, "UN MIL", "MIL") + " " + Centenas(resto);
}//Miles()

function Millones(num) {
    divisor = 1000000;
    cientos = Math.floor(num / divisor)
    resto = num - (cientos * divisor)

    strMillones = Seccion(num, divisor, "UN MILLON", "MILLONES");
    strMiles = Miles(resto);

    if (strMillones == "")
        return strMiles;

    return strMillones + " " + strMiles;

    //return Seccion(num, divisor, "UN MILLON", "MILLONES") + " " + Miles(resto);
}//Millones()
function NumeroALetras() {
    var num = $("#txtCantidad").val();
    var data = {
        numero: num,
        enteros: Math.floor(num),
        centavos: (((Math.round(num * 100)) - (Math.floor(num) * 100))),
        letrasCentavos: ""
    };
    var letras = "";
    if (data.centavos > 0)
        data.letrasCentavos = "CON " + data.centavos + "/100";

    if (data.enteros == 0)
        letras = "CERO " + data.letrasCentavos;
    if (data.enteros == 1)
        letras = Millones(data.enteros) + " " + data.letrasCentavos;
    else
        letras = Millones(data.enteros) + " " + data.letrasCentavos;
    $("#lblCantidadLetras").text(letras);
}
$.fn.addUniqueSomething = function (content) {
    var existing = this.data('asterisco');
    if (existing) {
        existing.remove();
    }

    var something = $(content);
    this.after(something);
    this.data('asterisco', something);
};
function ConfigureValidators() {
    if (typeof Page_Validators != 'undefined') {
        for (i = 0; i <= Page_Validators.length; i++) {
            if (Page_Validators[i] != null) {
                if (typeof Page_Validators[i].enabled == 'undefined') {
                    $('#' + Page_Validators[i].controltovalidate).addUniqueSomething('<span class="validador">*</span>');
                }
            }
        }
    };
}
