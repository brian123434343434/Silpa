
$(document).ready(function () {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    Calendarios();
    ConfigureValidators();
    //$("#txtNumeroActoAdministrativo").numeric();
    $("#txtNumeroIdentificacion").numeric();
    $("#txtCantidadAutorizado").numeric();
    $("#txtCantVolTotal").numeric();
    $("#txtCantVolMovilizar").numeric();
    $("#txtMovido").numeric();
    $("#txtCantVolRemanente").numeric();
    $("#txtPorcentajeDesperdicio").numeric();
    $("#txtAreaTotalAut").numeric();
    
    function EndRequestHandler(sender, args) {
        Calendarios();
        ConfigureValidators();
        //$("#txtNumeroActoAdministrativo").numeric();
        $("#txtNumeroIdentificacion").numeric();
        $("#txtCantidadAutorizado").numeric();
        $("#txtCantVolTotal").numeric();
        $("#txtCantVolMovilizar").numeric();
        $("#txtMovido").numeric();
        $("#txtCantVolRemanente").numeric();
        $("#txtPorcentajeDesperdicio").numeric();
        $("#txtAreaTotalAut").numeric();
        
        
   }
});
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
function CalcularDesperdicio()
{
    var DVolumenMovido = 0.0;
    var DVolumenAutorizado = 0.0;
    var DVolumenDesperdicio = 0.0;
    var DporcentajeDesperdicio = 0.0;
    var DVolumenAutorizado = 0.0;
    var DVolumenAutorizadoMenosVolumenMovido = 0.0;
    if ($("#txtCantidadAutorizado").val != "")
        DVolumenAutorizado = $("#txtCantidadAutorizado").val();
    if ($("#txtPorcentajeDesperdicio").val != "")
        DporcentajeDesperdicio = $("#txtPorcentajeDesperdicio").val();
    if ($("#txtMovido").val != "")
        DVolumenMovido = $("#txtMovido").val();
    if ($("#txtCantidadAutorizado").val() != "")
        DVolumenAutorizado = $("#txtCantidadAutorizado").val();

    DVolumenAutorizadoMenosVolumenMovido = DVolumenAutorizado - DVolumenMovido;
    DVolumenDesperdicio = ((DVolumenAutorizadoMenosVolumenMovido) * DporcentajeDesperdicio) / 100;
    $("#txtCantVolRemanente").val(DVolumenDesperdicio);
    $("#txtCantVolTotal").val(DVolumenAutorizadoMenosVolumenMovido - DVolumenDesperdicio);
    return false;
}

function Calendarios() {
    $("#txtFechaActoAdminstrativo").datetimepicker({
        formatDate: 'd/m/Y',
        lang: 'es',
        timepicker: false
    });
    //Jmartinez Salvoconducto Fase 2
    $("#txtFechaFinalizacionActoAdminstrativo").datetimepicker({
        formatDate: 'd/m/Y',
        lang: 'es',
        timepicker: false
    });
    
}

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
    var strObjeto = sender._element.id.replace("fuplActoAdministrativo", "lnkCancelarArchivo")
    __doPostBack(strObjeto.replace(new RegExp("_", "g"), "$"), '');
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
    var num = $("#txtCantidadAutorizado").val();

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
function AssemblyFileUpload_Started(sender, args) {
    var filename = args.get_fileName();
    var ext = filename.substring(filename.lastIndexOf(".") + 1);
    if (ext.toLowerCase() != 'pdf') {
        throw {
            name: "Invalid File Type",
            level: "Warning",
            message: "Tipo de archivo invalido, solo PDF",
            htmlMessage: "Tipo de archivo invalido, solo PDF"
        }
        return false;
    }
    return true;
}
function popup(urlPage)
{
    window.open(urlPage, 'FileTraffic', 'location=no,resizable=yes,scrollbars=yes');
}

function ValidarCheckBoxAprovechamiento()
{
    var mensaje = '';
    var ObjChkValidaInfoLocalizacion = document.getElementById("ChkValidaInfoLocalizacion")
    var ObjChkValidaEspecies = document.getElementById("ChkValidaEspecies")
    var ObjChkValidaCntEspecies = document.getElementById("ChkValidaCntEspecies")
    var ObjChkValidaInfoAprovechamiento = document.getElementById("ChkValidaInfoAprovechamiento")


    if (ObjChkValidaInfoAprovechamiento != undefined && ObjChkValidaInfoAprovechamiento != null) {
        var ch = ObjChkValidaInfoAprovechamiento.checked;
        if (!ch) {
            mensaje += 'Debe certificar los datos de la informacion basica del aprovechamiento \n';
        }
    }
    
    if (ObjChkValidaEspecies != undefined && ObjChkValidaEspecies != null) {
        ch = ObjChkValidaEspecies.checked;
        if (!ch) {
            mensaje += 'Debe certificar los datos de la informacion de los especimenes \n';
        }
    }
    else {
        mensaje += 'Debe ingresar los datos de la informacion de los especimenes \n';
    }


    if (ObjChkValidaCntEspecies != undefined && ObjChkValidaCntEspecies != null) {
        ch = ObjChkValidaCntEspecies.checked;
        if (!ch) {
            mensaje += 'Debe certificar los datos de la informacion del detalle por tipo de producto y unidad de medida de los especimenes \n';
        }
    }


    
    if (ObjChkValidaInfoLocalizacion != undefined && ObjChkValidaInfoLocalizacion != null) {
        ch = ObjChkValidaInfoLocalizacion.checked;
        if (!ch) {
            mensaje += 'Debe certificar los datos de la informacion de la localizacion del aprovechamiento \n';
        }
    }


    if (mensaje !== '') {
        alert(mensaje);
        return false;
    }

    return confirm('¿Esta seguro de grabar el aprovechamiento?');

}

