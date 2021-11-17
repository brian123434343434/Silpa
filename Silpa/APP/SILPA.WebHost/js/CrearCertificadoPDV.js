var _tipoPruebaEnum = { "VehiculoCompleto": 1, "Motor": 2 }
var _tipoVehiculoEnum = { "VehiculoPesadoPasajeros": 1, "VehiculoPesadoCarga": 2, "VehiculoLiviano": 3, "Taxi": 4, "Motocicleta": 5, "MotoCarro": 6 }
var _clasificacionEnum = { "M1" : 1,"M2" : 2,"M3" : 3,"MDPV": 4, "N1" : 5,"N2" : 6,"N3" : 7,"N1_Clase_l" : 8,"N1_Clase_ll" : 9,"N1_Clase_lll" : 10,"LDT1" : 11,"LDT2" : 12,"LDT3" : 13,"LDT4" : 14,"LHDGE" : 15,"HHDGE" : 16,"LHDDE" : 17,"MHDDE" : 18,"HHDDE" : 19,"URBAN_BUS" : 20,"LDV" : 21 }
var _resolucionesEnum = { "Resol_910_2008": 1, "Resol_2604_2009": 2, "Resol_111_2013": 3 }
var _tipoImportacionEnum = { "Comercializacion": 1, "UsoPropio": 2 }
var _nombreCheckControlEmisiones = "";

function Inicializar() {
    CambioMarca();
    CambioTipoPrueba();
    CambioResolucion();
    CambioCiclo(1);
    CambioCombustible();
    CambioSistemaTransmision();
    CambioSistemaAlimentacion();
    //CambioSistemaControlEmisiones();
    CambioTieneSistemaControlEmisiones();
    CambioSistemaAireAcondicionado();
    CambioSustanciaRefrigeranteRefrigeracion();
    InicializarCalendarios();
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

function InicializarCalendarios() {
    $("[id*=txtFechaPrueba]").datepicker({
        dateFormat: 'dd/mm/yy',
        changeYear: true,
        changeMonth: true
    });
    $("[id*=txtFechaPrueba]").datepicker("option", "yearRange", "-99:+0");
    $("[id*=txtFechaPrueba]").datepicker("option", "maxDate", "+0m +0d");
    $("[id*=txtFechaPrueba]").keypress(function (evt) { return false; });
}

//Funcion que se ejecuta al finalizar de subir el archivo
function UploadAnexo(sender, args) {
    var strObjeto = sender._element.id.replace("fuplAnexo", "lnkCancelarArchivo")
    __doPostBack(strObjeto.replace(new RegExp("_", "g"), "$"), '');
}

//Acciones realizadas al seleccionar marca 
function CambioTipoPrueba() {
    var strTipoPrueba = $("#cboTipoPrueba option:selected").val();

    //Si la opcion es otro habilitar campo
    if (strTipoPrueba == _tipoPruebaEnum.Motor) {
        $("#rowSistemaTransmision").hide();
        ValidatorEnable(document.getElementById("rfvSistemaTransmision"), false);
        $("#cboSistemaTransmision").val("");
        $("#txtSistemaTransmision").val("");
        $("#txtSistemaTransmision").hide();
        ValidatorEnable(document.getElementById("rfvSistemaTransmisionOtro"), false);
        $("#rowInformacionAdicionalPruebas").hide();
        $("#rowRadioDinamico").hide();
        $("#txtRadioDinamico").val("");
        $("#rowPresionInterna").hide();
        $("#txtPresionInterna").val("");
        ValidatorEnable(document.getElementById("rfvRadioDinamico"), false);
    }
    else {
        $("#rowSistemaTransmision").show();
        ValidatorEnable(document.getElementById("rfvSistemaTransmision"), true);
        $("#rowInformacionAdicionalPruebas").show();
        $("#rowRadioDinamico").show();
        $("#rowPresionInterna").show();
        ValidatorEnable(document.getElementById("rfvRadioDinamico"), true);
    }
}


//Acciones realizadas al seleccionar marca 
function CambioMarca() {
    var strMarca = $("#cboMarcaVehiculo option:selected").val();

    //Si la opcion es otro habilitar campo
    if (strMarca == "-1") {
        $("#txtMarcaVehiculo").show();
        ValidatorEnable(document.getElementById("rfvMarcaVehiculoOtro"), true);
    }
    else{
        $("#txtMarcaVehiculo").hide();
        $("#txtMarcaVehiculo").val("");
        ValidatorEnable(document.getElementById("rfvMarcaVehiculoOtro"), false);
    }
}

function IngresarOpcionesCiclosPrueba(p_intNumeroCiclos) {
    var cont = 0;
    var valorActual = $("#cboNumeroCiclosPrueba").val();

    if (valorActual == "") {
        $('option', $('#cboNumeroCiclosPrueba')).remove();
        $('#cboNumeroCiclosPrueba').append(new Option("Seleccione.", ""));

        for (cont = 0; cont < p_intNumeroCiclos; cont++) {
            if (cont != 1) {
                $('#cboNumeroCiclosPrueba').append(new Option((cont + 1), (cont + 1)));
            }
        }
    }
    else {
        $("#cboNumeroCiclosPrueba").val("");
        $("#cboNumeroCiclosPrueba").trigger("change");
    }
}

//Acciones realizadas al seleccionar resolucion
function CambioResolucion() {
    var strResolucion = $("#cboResolucion option:selected").val();
    var strTipoVehiculo = $("#cboTipoVehiculo option:selected").val();

    if (strResolucion == _resolucionesEnum.Resol_2604_2009) {

        if (strTipoVehiculo == _tipoVehiculoEnum.MotoCarro) {
            $("#rowClasificacion").hide();
            $("#cboClasificacion").val("");
            ValidatorEnable(document.getElementById("rfvClasificacion"), false);
            $("#rowUnidadPrueba").hide();
            $("#cboUnidadPrueba").val("");
            ValidatorEnable(document.getElementById("rfvUnidadPrueba"), false);
            $("#" + _nombreCheckControlEmisiones).hide();
            $("#txtSistemaControlEmisiones").hide();
            ValidatorEnable(document.getElementById("rfvSistemaControlEmisionesOtro"), false);
            $("#cboTieneSistemaControlEmisiones").show();
            ValidatorEnable(document.getElementById("rfvTieneSistemaControlEmisiones"), true);
            $("#rowObservacionPrueba").show();
        }
        else {
            $("#rowClasificacion").show();
            ValidatorEnable(document.getElementById("rfvClasificacion"), true);
            $("#rowUnidadPrueba").show();
            ValidatorEnable(document.getElementById("rfvUnidadPrueba"), true);
            $("#" + _nombreCheckControlEmisiones).show();
            ValidatorEnable(document.getElementById("rfvSistemaControlEmisionesOtro"), false);
            $("#cboTieneSistemaControlEmisiones").hide();
            $("#cboTieneSistemaControlEmisiones").val("");
            ValidatorEnable(document.getElementById("rfvTieneSistemaControlEmisiones"), false);
            $("#txtObservacionPrueba").val("");
            $("#rowObservacionPrueba").hide();
        }

        $("#cboEstandarEmisiones").val("");        
        $("#rowTituloEstandarEmision").hide();
        $("#rowEstandarEmision").hide();
        $("#txtObservacionesImportacion").val("");
        $("#rowObservacionImportacion").hide();
        
        ValidatorEnable(document.getElementById("rfvEstandarEmisiones"), false);

    }
    else {

        if (strTipoVehiculo == _tipoVehiculoEnum.MotoCarro || strTipoVehiculo == _tipoVehiculoEnum.Motocicleta) {
            $("#rowClasificacion").hide();
            $("#cboClasificacion").val("");
            ValidatorEnable(document.getElementById("rfvClasificacion"), false);
        }
        else{
            $("#rowClasificacion").show();
            ValidatorEnable(document.getElementById("rfvClasificacion"), true);
        }

        $("#rowUnidadPrueba").show();
        ValidatorEnable(document.getElementById("rfvUnidadPrueba"), true);
        $("#" + _nombreCheckControlEmisiones).show();
        ValidatorEnable(document.getElementById("rfvSistemaControlEmisionesOtro"), false);
        $("#cboTieneSistemaControlEmisiones").hide();
        $("#cboTieneSistemaControlEmisiones").val("");
        ValidatorEnable(document.getElementById("rfvTieneSistemaControlEmisiones"), false);

        $("#rowTituloEstandarEmision").show();
        $("#rowEstandarEmision").show();
        ValidatorEnable(document.getElementById("rfvEstandarEmisiones"), true);

        $("#rowObservacionImportacion").show();

        $("#txtObservacionPrueba").val("");
        $("#rowObservacionPrueba").hide();
    }

    if (strTipoVehiculo == _tipoVehiculoEnum.Motocicleta || strTipoVehiculo == _tipoVehiculoEnum.MotoCarro) {
        $("#rowTipoMotor").show();
        ValidatorEnable(document.getElementById("rfvTipoMotor"), true);
    }
    else {
        $("#rowTipoMotor").hide();
        $("#cboTipoMotor").val("");
        ValidatorEnable(document.getElementById("rfvTipoMotor"), false);
    }
}

//Acciones realizadas al seleccionar marca 
function CambioCombustible() {
    var strCombustible = $("#cboCombustible option:selected").val();

    //Si la opcion es otro habilitar campo
    if (strCombustible == "-1") {
        $("#txtCombustible").show();
        ValidatorEnable(document.getElementById("rfvCombustibleOtro"), true);        
    }
    else {
        $("#txtCombustible").hide();
        $("#txtCombustible").val("");
        ValidatorEnable(document.getElementById("rfvCombustibleOtro"), false);
    }
}

//Acciones realizadas al seleccionar marca 
function CambioSistemaTransmision() {
    var strSistemaTransmision = $("#cboSistemaTransmision option:selected").val();

    //Si la opcion es otro habilitar campo
    if (strSistemaTransmision == "-1") {
        $("#txtSistemaTransmision").show();
        ValidatorEnable(document.getElementById("rfvSistemaTransmisionOtro"), true);
    }
    else {
        $("#txtSistemaTransmision").hide();
        $("#txtSistemaTransmision").val("");
        ValidatorEnable(document.getElementById("rfvSistemaTransmisionOtro"), false);
    }
}

//Acciones realizadas al seleccionar marca 
function CambioSistemaAlimentacion() {
    var strSistemaAlimentacion = $("#cboSistemaAlimentacion option:selected").val();

    //Si la opcion es otro habilitar campo
    if (strSistemaAlimentacion == "-1") {
        $("#txtSistemaAlimentacion").show();
        ValidatorEnable(document.getElementById("rfvSistemaAlimentacionOtro"), true);
    }
    else {
        $("#txtSistemaAlimentacion").hide();
        $("#txtSistemaAlimentacion").val("");
        ValidatorEnable(document.getElementById("rfvSistemaAlimentacionOtro"), false);
    }
}


//Acciones realizadas al seleccionar marca 
function CambioSistemaControlEmisiones() {
    var strSistemaControlEmisiones = $("#cboSistemaControlEmisiones option:selected").val();

    $find("cdlSistemaControlEmisiones")._contextKey = $("#cboResolucion").val() + "|" + $("#cboTipoVehiculo").val();

    //Si la opcion es otro habilitar campo
    if (strSistemaControlEmisiones == "-1") {
        $("#txtSistemaControlEmisiones").show();
        ValidatorEnable(document.getElementById("rfvSistemaControlEmisionesOtro"), true);
    }
    else {
        $("#txtSistemaControlEmisiones").hide();
        $("#txtSistemaControlEmisiones").val("");
        ValidatorEnable(document.getElementById("rfvSistemaControlEmisionesOtro"), false);
    }
}

function CambioTieneSistemaControlEmisiones() {
    var strSistemaControlEmisiones = $("#cboTieneSistemaControlEmisiones option:selected").val();

    //Si la opcion es otro habilitar campo
    if (strSistemaControlEmisiones == "1") {
        $("#txtSistemaControlEmisiones").show();
        ValidatorEnable(document.getElementById("rfvSistemaControlEmisionesOtro"), true);
    }
    else {
        $("#txtSistemaControlEmisiones").hide();
        $("#txtSistemaControlEmisiones").val("");
        ValidatorEnable(document.getElementById("rfvSistemaControlEmisionesOtro"), false);
    }
}

//Acciones realizadas al seleccionar marca 
function CambioSistemaAireAcondicionado() {
    var strSistemaAireAcondicionado = $("#cboSistemaAireAcondicionado option:selected").val();

    //Si la opcion es otro habilitar campo
    if (strSistemaAireAcondicionado == "1") {
        $("#rowSustanciaRefrigeranteAireAcondicionado").show();
        ValidatorEnable(document.getElementById("rfvSustanciaRefrigeranteAireAcondicionado"), true);
    }
    else {
        $("#rowSustanciaRefrigeranteAireAcondicionado").hide();
        $("#txtSustanciaRefrigeranteAireAcondicionado").val("");
        ValidatorEnable(document.getElementById("rfvSustanciaRefrigeranteAireAcondicionado"), false);
    }
}

//Acciones realizadas al seleccionar marca 
function CambioSustanciaRefrigeranteRefrigeracion() {
    var strSustanciaRefrigeranteRefrigeracion = $("#cboSistemaRefrigeracion option:selected").val();

    //Si la opcion es otro habilitar campo
    if (strSustanciaRefrigeranteRefrigeracion == "1") {
        $("#rowSustanciaRefrigeranteRefrigeracion").show();
        ValidatorEnable(document.getElementById("rfvSustanciaRefrigeranteRefrigeracion"), true);
    }
    else {
        $("#rowSustanciaRefrigeranteRefrigeracion").hide();
        $("#txtSustanciaRefrigeranteRefrigeracion").val("");
        ValidatorEnable(document.getElementById("rfvSustanciaRefrigeranteRefrigeracion"), false);
    }
}

//Adiciona eventos para que ejecute el dropdowncast
function AddPopulatedHandler(behaviorId, populatedEventHandler) {
    var behavior = $find(behaviorId);
    
    if (behavior != null) {
        behavior.add_populated(populatedEventHandler);
    }
}

function ValidarTipoModelo(sender, args) {
    var checkBoxList = document.getElementById("chkTipoModelo");
    var checkboxes = checkBoxList.getElementsByTagName("input");
    var numeroSelecciones = 0;

    //Verifca numero de opciones seleccionadas
    for (var cont = 0; cont < checkboxes.length; cont++) {
        if (checkboxes[cont].checked) {
            numeroSelecciones ++;            
        }
    }
    
    if (numeroSelecciones < 1 || numeroSelecciones > 2) {                

        if (numeroSelecciones < 1) {
            sender.errormessage = "Debe Seleccionar el Tipo en las Caracteristicas del Modelo";
        }
        else if (numeroSelecciones > 2) {
            sender.errormessage = "Máximo se debe Seleccionar  Dos (2) Tipos en las Caracteristicas del Modelo";
        }

        args.IsValid = false;
        
    }
    else {
        args.IsValid = true;
    }
}

function ValidarSistemaControlEmisiones(sender, args) {
    var checkBoxList = document.getElementById(_nombreCheckControlEmisiones);
    var checkboxes = checkBoxList.getElementsByTagName("input");
    var numeroSelecciones = 0;
    var tipoVehiculo = $("#cboTipoVehiculo").val();

    if ($('#' + _nombreCheckControlEmisiones).css('display') != 'none' && tipoVehiculo != _tipoVehiculoEnum.Motocicleta) {
        //Verifca numero de opciones seleccionadas
        for (var cont = 0; cont < checkboxes.length; cont++) {
            if (checkboxes[cont].checked) {
                numeroSelecciones++;
            }
        }

        if (numeroSelecciones == 0) {
            sender.errormessage = "Debe Seleccionar un Sistema de Control de Emisiones";
            args.IsValid = false;
        }
        else {
            args.IsValid = true;
        }
    }
    else {
        args.IsValid = true;
    }
}


function CambioCiclo(intNumeroCiclo) {
    var strResolucion = $("#cboResolucion option:selected").val();
    var strTipoVehiculo = $("#cboTipoVehiculo option:selected").val();
    var strClasificacion = $("#cboClasificacion option:selected").val();
    var strCiclo = $("#cboCiclo" + intNumeroCiclo + " option:selected").val();

    if (strCiclo == "6" || strCiclo == "8") {
        if (strTipoVehiculo == _tipoVehiculoEnum.VehiculoLiviano || strTipoVehiculo == _tipoVehiculoEnum.Taxi) 
        {
            $("#rowEstandarPrueba").show();
            ValidatorEnable(document.getElementById("rfvEstandarPrueba"), true);
        }
        else if (strTipoVehiculo == _tipoVehiculoEnum.VehiculoPesadoPasajeros) {
            if (strClasificacion == _clasificacionEnum.MDPV) {
                $("#rowEstandarPrueba").show();
                ValidatorEnable(document.getElementById("rfvEstandarPrueba"), true);
            }
            else {
                $("#rowEstandarPrueba").hide();
                $("#cboEstandarPrueba").val("");
                ValidatorEnable(document.getElementById("rfvEstandarPrueba"), false);
            }
        }
        else{
            $("#rowEstandarPrueba").hide();
            $("#cboEstandarPrueba").val("");
            ValidatorEnable(document.getElementById("rfvEstandarPrueba"), false);
        }
    }
    else {
        $("#rowEstandarPrueba").hide();
        $("#cboEstandarPrueba").val("");
        ValidatorEnable(document.getElementById("rfvEstandarPrueba"), false);
    }
}


$(document).ready(function () {

    //Se ejecuta al realizar el cambio de valor
    $("#cboMarcaVehiculo").change(function () {
        CambioMarca();
    });

    //Se ejecuta al realizar el cambio de valor
    $("#cboTipoPrueba").change(function () {
        CambioTipoPrueba();
    });

    //Se ejecuta al realizar el cambio de resolucion
    $("#cboResolucion").change(function () {
        CambioResolucion();
        CambioCiclo(1);
    });

    //Se ejecuta al realizar el cambio de tipo de vehiculo
    $("#cboTipoVehiculo").change(function () {
        CambioResolucion();
        CambioCiclo(1);
    });

    //Se ejecuta al realizar el cambio de valor
    $("#cboCombustible").change(function () {
        CambioCombustible();
    });

    //Se ejecuta al realizar el cambio de valor
    $("#cboSistemaTransmision").change(function () {
        CambioSistemaTransmision();
    });

    //Se ejecuta al realizar el cambio de valor
    $("#cboSistemaAlimentacion").change(function () {
        CambioSistemaAlimentacion();
    });

    //Se ejecuta al realizar el cambio de valor
    $("#cboTieneSistemaControlEmisiones").change(function () {
        CambioTieneSistemaControlEmisiones();
    });

    //Se ejecuta al realizar el cambio de valor
    $("#cboSistemaAireAcondicionado").change(function () {
        CambioSistemaAireAcondicionado();
    });

    //Se ejecuta al realizar el cambio de valor
    $("#cboSistemaRefrigeracion").change(function () {
        CambioSustanciaRefrigeranteRefrigeracion();
    });

    //Se ejecuta al realizar el cambio de valor
    $("#cboCiclo1").change(function () {
        CambioCiclo(1);
    });

    $("#cboClasificacion").change(function () {
        CambioCiclo(1);
    });

    var prm = Sys.WebForms.PageRequestManager.getInstance();

    prm.add_endRequest(function() {

        InicializarCalendarios();

        //Se ejecuta al realizar el cambio de valor
        $("#cboMarcaVehiculo").change(function () {
            CambioMarca();
        });

        //Se ejecuta al realizar el cambio de valor
        $("#cboTipoPrueba").change(function () {
            CambioTipoPrueba();
        });

        //Se ejecuta al realizar el cambio de resolucion
        $("#cboResolucion").change(function () {
            CambioResolucion();
            CambioCiclo(1);
        });

        //Se ejecuta al realizar el cambio de tipo de vehiculo
        $("#cboTipoVehiculo").change(function () {        
            CambioResolucion();
            CambioCiclo(1);
        });

        //Se ejecuta al realizar el cambio de valor
        $("#cboCombustible").change(function () {
            CambioCombustible();
        });

        //Se ejecuta al realizar el cambio de valor
        $("#cboSistemaTransmision").change(function () {
            CambioSistemaTransmision();
        });

        //Se ejecuta al realizar el cambio de valor
        $("#cboSistemaAlimentacion").change(function () {
            CambioSistemaAlimentacion();
        });
    
        //Se ejecuta al realizar el cambio de valor
        $("#cboTieneSistemaControlEmisiones").change(function () {
            CambioTieneSistemaControlEmisiones();
        });
    
        //Se ejecuta al realizar el cambio de valor
        $("#cboSistemaAireAcondicionado").change(function () {
            CambioSistemaAireAcondicionado();
        });

        //Se ejecuta al realizar el cambio de valor
        $("#cboSistemaRefrigeracion").change(function () {
            CambioSustanciaRefrigeranteRefrigeracion();
        });

        //Se ejecuta al realizar el cambio de valor
        $("#cboCiclo1").change(function () {
            CambioCiclo(1);
        });

        $("#cboClasificacion").change(function () {
            CambioCiclo(1);
        });
        
    });


    //Inicializa la página
    Inicializar();
});