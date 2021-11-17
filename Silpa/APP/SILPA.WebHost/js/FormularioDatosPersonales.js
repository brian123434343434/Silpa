var datosListaMunicipios;
var datosListaDepartamentos;
var datosListaCorregimientos;
var datosListaContactos;
var arregloDireccionIngresada = [];
var arregloDireccionTemp = [];
var arregloDireccionIngresadaComunicacion = [];
var modalContactoAbierto = "";
var campoTextoDireccionDinamico = "";
var tipoUsuarioSeleccionado = "Natural";

$(document).ready(function () {
    var rbvalue = "";
    var rbNatural = document.getElementById('Natural');
    if (rbNatural !== null && rbNatural.checked == true) {
        $("#divPersonaNatural").css("display", "block");
        $("#divPersonaJuridicaPublica").css("display", "none");
        $("#divPersonaJuridicaPrivada").css("display", "none");
    }
    $("#TextNumeroTipo").change(function () {
        arregloDireccionIngresada[1] = $("#TextNumeroTipo").val();
        EscribirDireccion();
    });
    $("#TextNumeroTipoComunicacion").change(function () {
        arregloDireccionIngresadaComunicacion[1] = $("#TextNumeroTipoComunicacion").val();
        EscribirDireccion();
    });
    $("#txtNumero1").change(function () {
        arregloDireccionIngresada[6] = $("#txtNumero1").val();
        EscribirDireccion();
    });
    $("#txtNumero1Comunicacion").change(function () {
        arregloDireccionIngresadaComunicacion[6] = $("#txtNumero1Comunicacion").val();
        EscribirDireccion();
    });
    arregloDireccionIngresada[5] = " # ";
    $("#txtNumero2").change(function () {
        arregloDireccionIngresada[8] = "- " + $("#txtNumero2").val();
        EscribirDireccion();
    });
    $("#txtNumero2Comunicacion").change(function () {
        arregloDireccionIngresadaComunicacion[6] = $("#txtNumero2Comunicacion").val();
        EscribirDireccion();
    });
    $("#TextComplemento").change(function () {
        arregloDireccionIngresada[11] = $("#TextComplemento").val();
        EscribirDireccion();
    });
    $("#TextComplementoComunicacion").change(function () {
        arregloDireccionIngresadaComunicacion[11] = $("#TextComplementoComunicacion").val();
        EscribirDireccion();
    });
    $("input[name=TipodeUsuario]").click(function () {
        if (this.id == 'Natural') {
            tipoUsuarioSeleccionado = "Natural";
            $("#divPersonaNatural").css("display", "block");
            $("#divPersonaJuridicaPublica").css("display", "none");
            $("#divPersonaJuridicaPrivada").css("display", "none");
            LimpiarDivContacto();
        }
        else if (this.id == 'juridicaPublica') {
            tipoUsuarioSeleccionado = "juridicaPublica";
            $("#divPersonaJuridicaPublica").css("display", "block");
            $("#divPersonaNatural").css("display", "none");
            $("#divPersonaJuridicaPrivada").css("display", "none");
            LimpiarDivContacto();
        }
        else if (this.id == 'juridicaPrivada') {
            tipoUsuarioSeleccionado = "juridicaPrivada";
            $("#divPersonaJuridicaPrivada").css("display", "block");
            $("#divPersonaNatural").css("display", "none");
            $("#divPersonaJuridicaPublica").css("display", "none");
            LimpiarDivContacto();
        }
    });
    $('#modalContacto').on('hidde', function () {
        $("#selContacto").val("0").change();
    });
    $('#myModal').on('shown.bs.modal', function () {
        if (modalContactoAbierto == true) {
            $("#modalContacto").modal("hide");
        }
        var txtDireccionN = $("#txtDireccionNatural").val();
        if (txtDireccionN != null && txtDireccionN.length > 0 && modalContactoAbierto != true) {
            PasarDireccionAmodal();
        }
        var txtDireccionNotificacion = $("#DireccionFisica").val();
        if (txtDireccionNotificacion != null && txtDireccionNotificacion.length > 0 && modalContactoAbierto == true) {
            PasarDireccionAmodal();
        }
    });
    $('#modalContacto').on('shown.bs.modal', function () {
        modalContactoAbierto = true;
    });
    CargarDatosIniciales();
    $('#txtNumeroIdentificacion').focusout(function () {
        if ($('#txtNumeroIdentificacion').val().length > 5) {
            VerificarExistenciaPorNumeroIdentificacion();
        }
    });
    $("#txtNumeroIdentificacion, #txtTelefonoNatural, #txtCelularNatural, #txtFaxNatural").keydown(function (e) {
        ValidarNumero(e);
    });
    $("#txtNumeroDocumentoJuridica, #txtTelefonoJuridica, #txtCelularJuridica, #txtFaxJuridica").keydown(function (e) {
        ValidarNumero(e);
    });
    $("#txtNumeroDocumentoPrivada, #txtTelefonoPrivada, #txtCelularPrivada, #txtFaxPrivada").keydown(function (e) {
        ValidarNumero(e);
    });

});
function ValidarNumero(e) {
    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            (e.keyCode == 65 && e.ctrlKey === true) || (e.keyCode >= 35 & e.keyCode <= 39)) {
        return;
    }
    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
}
//********************************************Region solictar informacion para listas desplegables***************************************************************************************************//
function CargarDatosIniciales() {
    CargarDatosAutoridadAmbiental();
    CargarPais();
    CargarTiposIdentificacion();
    CargarComplementos();
    CargarNomenclaturas();
    CargarListaDepartamentos();
    CargarListaTipoContacto();
    CargarInformacionUsuarioRegistrado();
}
function CargarDatosAutoridadAmbiental() {
    $.ajax({
        async: false,
        type: "POST",
        url: "DatosPersonalesNuevo.aspx",
        data: { Accion: "CargarDatosAutoridadAmbiental" },
        dataType: "json",
        success: function (data) {
            if (data !== undefined) {
                LlenarAutoridadAmbiental(data);
            }
        }
    });
}
function CargarPais() {
    $.ajax({
        async: false,
        type: "POST",
        url: "DatosPersonalesNuevo.aspx",
        data: { Accion: "CargarPais" },
        dataType: "json",
        success: function (data) {
            if (data !== undefined) {
                LlenarListaPais(data);
            }
        }
    });
}
function CargarTiposIdentificacion() {
    $.ajax({
        async: false,
        type: "POST",
        url: "DatosPersonalesNuevo.aspx",
        data: { Accion: "CargarTiposIdentificacion" },
        dataType: "json",
        success: function (data) {
            if (data !== undefined) {
                LlenarListaTiposIdentificacion(data);
            }
        }
    });
}
function CargarComplementos() {
    $.ajax({
        async: false,
        type: "POST",
        url: "DatosPersonalesNuevo.aspx",
        data: { Accion: "CargarComplementos" },
        dataType: "json",
        success: function (data) {
            if (data !== undefined) {
                LlenarListaComplementos(data);
            }
        }
    });
}
function CargarNomenclaturas() {
    $.ajax({
        async: false,
        type: "POST",
        url: "DatosPersonalesNuevo.aspx",
        data: { Accion: "CargarNomenclaturas" },
        dataType: "json",
        success: function (data) {
            if (data !== undefined) {
                LlenarListaNomenclaturas(data);
            }
        }
    });
}
function CargarInformacionUsuarioRegistrado() {
    $.ajax({
        async: false,
        type: "POST",
        url: "DatosPersonalesNuevo.aspx",
        data: { Accion: "UsuarioRegistrado" },
        dataType: "json",
        success: function (data) {
            if (data !== undefined) {
                LlenarUsuarioRegistrado(data);
            }
        },
        complete: function (data)
        {
            if (data.responseText == "OK")
                $("#btnActualizar").hide();
        }
    });
}
function CargarListaDepartamentos() {
    $.ajax({
        async: false,
        type: "POST",
        url: "DatosPersonalesNuevo.aspx",
        data: { Accion: "CargarDepartamentos" },
        dataType: "json",
        success: function (data) {
            if (data !== undefined) {
                datosListaDepartamentos = data;
                LlenarListaDepartamentos(data);
            }
        }
    });
}
function CargarListaMunicipios(valor) {
    $.ajax({
        async: false,
        type: "POST",
        url: "DatosPersonalesNuevo.aspx",
        data: { Accion: "CargarListaMunicipios", data: valor },
        dataType: "json",
        success: function (data) {
            if (data !== undefined) {
                datosListaMunicipios = data;
                LLenarListaMunicipios(data);
            }
        }
    });

}
function CargarListaMunicipiosNatural(valor) {
    $.ajax({
        async: false,
        type: "POST",
        url: "DatosPersonalesNuevo.aspx",
        data: { Accion: "CargarListaMunicipios", data: valor },
        dataType: "json",
        success: function (data) {
            if (data !== undefined) {
                datosListaMunicipios = data;
                LLenarListaMunicipiosNatural(data);
            }
        }
    });
}
function CargarListaMunicipiosPrivada(valor) {
    $.ajax({
        async: false,
        type: "POST",
        url: "DatosPersonalesNuevo.aspx",
        data: { Accion: "CargarListaMunicipios", data: valor },
        dataType: "json",
        success: function (data) {
            if (data !== undefined) {
                datosListaMunicipios = data;
                LLenarListaMunicipiosPrivada(data);
            }
        }
    });
}
function CargarListaMunicipiosJuridica(valor) {
    $.ajax({
        async: false,
        type: "POST",
        url: "DatosPersonalesNuevo.aspx",
        data: { Accion: "CargarListaMunicipios", data: valor },
        dataType: "json",
        success: function (data) {
            if (data !== undefined) {
                datosListaMunicipios = data;
                LLenarListaMunicipiosJuridica(data);
            }
        }
    });
}
function CargarListaCorregimientos(valor) {
    $.ajax({
        async: false,
        type: "POST",
        url: "DatosPersonalesNuevo.aspx",
        data: { Accion: "CargarListaCorregimientos", data: valor },
        dataType: "json",
        success: function (data) {
            if (data !== undefined) {
                datosListaCorregimientos = data;
                LLenarListaCorregimientos(data);
            }
        }
    });
}
function CargarListaCorregimientosPrivada(valor) {
    $.ajax({
        async: false,
        type: "POST",
        url: "DatosPersonalesNuevo.aspx",
        data: { Accion: "CargarListaCorregimientos", data: valor },
        dataType: "json",
        success: function (data) {
            if (data !== undefined) {
                LLenarListaCorregimientosPrivada(data);
            }
        }
    });
}
function CargarListaCorregimientosJuridica(valor) {
    $.ajax({
        async: false,
        type: "POST",
        url: "DatosPersonalesNuevo.aspx",
        data: { Accion: "CargarListaCorregimientos", data: valor },
        dataType: "json",
        success: function (data) {
            if (data !== undefined) {
                LLenarListaCorregimientosJuridica(data);
            }
        }
    });
}
function CargarListaVeredas(codMunicipio, codCorregimiento) {
    $.ajax({
        async: false,
        type: "POST",
        url: "DatosPersonalesNuevo.aspx",
        data: { Accion: "CargarListaVeredas", idMunicipio: codMunicipio, idCorregimiento: codCorregimiento },
        dataType: "json",
        success: function (data) {
            if (data !== undefined) {
                LLenarListaVeredas(data);
            }
        }
    });
}
function CargarListaTipoContacto() {
    $.ajax({
        async: false,
        type: "POST",
        url: "DatosPersonalesNuevo.aspx",
        data: { Accion: "CargarTipoContactos" },
        dataType: "json",
        success: function (data) {
            if (data !== undefined) {
                datosListaContactos = data;
                LlenarListaTipoContacto(data);
            }
        }
    });
}
//********************************************Region llenar listas desplegables***************************************************************************************************//
function LlenarAutoridadAmbiental(datos) {
    if (datos !== null && datos.length > 0) {
        $('#selAutoridadAmbiental').append('<option value="0" selected="selected">-Seleccione-</option>');
        for (i = 0; i < datos.length; i++) {
            $('#selAutoridadAmbiental').append('<option value="' + datos[i].AUT_ID + '">' + datos[i].AUT_NOMBRE + '</option>');
        }
    }
    else {
        $('#selAutoridadAmbiental').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function LlenarListaPais(datos) {
    if (datos !== null && datos.length > 0) {
        $('#selPaisNatural').append('<option value="0" selected="selected">-Seleccione-</option>');
        $('#selPaisJuridica').append('<option value="0" selected="selected">-Seleccione-</option>');
        $('#selPaisPrivada').append('<option value="0" selected="selected">-Seleccione-</option>');
        for (i = 0; i < datos.length; i++) {
            $('#selPaisNatural').append('<option value="' + datos[i].PAI_ID + '">' + datos[i].PAI_NOMBRE + '</option>');
            $('#selPaisJuridica').append('<option value="' + datos[i].PAI_ID + '">' + datos[i].PAI_NOMBRE + '</option>');
            $('#selPaisPrivada').append('<option value="' + datos[i].PAI_ID + '">' + datos[i].PAI_NOMBRE + '</option>');
        }
        $("#selPaisNatural option").each(function () {
            if ($(this).html() == "Colombia") {
                this.selected = "true";
                return;
            }
        });
        $("#selPaisJuridica option").each(function () {
            if ($(this).html() == "Colombia") {
                this.selected = "true";
                return;
            }
        });
        $("#selPaisPrivada option").each(function () {
            if ($(this).html() == "Colombia") {
                this.selected = "true";
                return;
            }
        });
    }
    else {
        $('#selPaisNatural').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
        $('#selPaisJuridica').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
        $('#selPaisPrivada').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function LlenarListaDepartamentos(datos) {
    if (datos !== null && datos.length > 0) {
        $('#selDepartamentoOrigenNatural').append('<option value="0" selected="selected">-Seleccione-</option>');
        $('#selDepartamentoNatural').append('<option value="0" selected="selected">-Seleccione-</option>');
        $('#selDepartamentoJuridica').append('<option value="0" selected="selected">-Seleccione-</option>');
        $('#selDepartamentoPrivada').append('<option value="0" selected="selected">-Seleccione-</option>');
        for (i = 0; i < datos.length; i++) {
            $('#selDepartamentoNatural').append('<option value="' + datos[i].DEP_ID + '">' + datos[i].DEP_NOMBRE + '</option>');
            $('#selDepartamentoOrigenNatural').append('<option value="' + datos[i].DEP_ID + '">' + datos[i].DEP_NOMBRE + '</option>');
            $('#selDepartamentoJuridica').append('<option value="' + datos[i].DEP_ID + '">' + datos[i].DEP_NOMBRE + '</option>');
            $('#selDepartamentoPrivada').append('<option value="' + datos[i].DEP_ID + '">' + datos[i].DEP_NOMBRE + '</option>');
        }
    }
    else {
        $('#selDepartamentoOrigenNatural').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
        $('#selDepartamentoNatural').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
        $('#selDepartamentoJuridica').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
        $('#selDepartamentoPrivada').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function LLenarListaMunicipios(datos) {
    if (datos !== undefined && datos.length > 0) {
        $('#selMunicipioOrigenNatural').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
        for (i = 0; i < datos.length; i++) {
            $('#selMunicipioOrigenNatural').append('<option value="' + datos[i].MUN_ID + '">' + datos[i].MUN_NOMBRE + '</option>');
        }
    }
    else {
        $('#selMunicipioOrigenNatural').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function LLenarListaMunicipiosNatural(datos) {
    if (datos !== undefined && datos.length > 0) {
        $('#selMunicipioNatural').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
        for (i = 0; i < datos.length; i++) {
            $('#selMunicipioNatural').append('<option value="' + datos[i].MUN_ID + '">' + datos[i].MUN_NOMBRE + '</option>');
        }
    }
    else {
        $('#selMunicipioNatural').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function LLenarListaMunicipiosPrivada(datos) {
    if (datos !== undefined && datos.length > 0) {
        $('#selMunicipioPrivada').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
        for (i = 0; i < datos.length; i++) {
            $('#selMunicipioPrivada').append('<option value="' + datos[i].MUN_ID + '">' + datos[i].MUN_NOMBRE + '</option>');
        }
    }
    else {
        $('#selMunicipioPrivada').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function LLenarListaMunicipiosJuridica(datos) {
    if (datos !== undefined && datos.length > 0) {
        $('#selMunicipioJuridica').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
        for (i = 0; i < datos.length; i++) {
            $('#selMunicipioJuridica').append('<option value="' + datos[i].MUN_ID + '">' + datos[i].MUN_NOMBRE + '</option>');
        }
    }
    else {
        $('#selMunicipioJuridica').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function LLenarListaCorregimientos(datos) {
    if (datos !== undefined && datos.length > 0) {
        $('#selCorregimientoNatural').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
        for (i = 0; i < datos.length; i++) {
            $('#selCorregimientoNatural').append('<option value="' + datos[i].COR_ID + '">' + datos[i].COR_NOMBRE + '</option>');
        }
    }
    else {
        $('#selCorregimientoNatural').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
        $('#selVeredaNatural').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function LLenarListaCorregimientosPrivada(datos) {
    if (datos !== undefined && datos.length > 0) {
        $('#selCorregimientoPrivada').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
        for (i = 0; i < datos.length; i++) {
            $('#selCorregimientoPrivada').append('<option value="' + datos[i].COR_ID + '">' + datos[i].COR_NOMBRE + '</option>');
        }
    }
    else {
        $('#selCorregimientoPrivada').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
        $('#selVeredaPrivada').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function LLenarListaCorregimientosJuridica(datos) {
    if (datos !== undefined && datos.length > 0) {
        $('#selCorregimientoJuridica').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
        for (i = 0; i < datos.length; i++) {
            $('#selCorregimientoJuridica').append('<option value="' + datos[i].COR_ID + '">' + datos[i].COR_NOMBRE + '</option>');
        }
    }
    else {
        $('#selCorregimientoJuridica').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
        $('#selVeredaJuridica').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function LLenarListaVeredas(datos) {
    if (datos !== undefined && datos.length > 0) {
        $('#selVeredaNatural').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
        for (i = 0; i < datos.length; i++) {
            $('#selVeredaNatural').append('<option value="' + datos[i].VER_ID + '">' + datos[i].VER_NOMBRE + '</option>');
        }
    }
    else {
        $('#selVeredaNatural').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function LlenarListaNomenclaturas(datos) {
    if (datos !== null && datos.length > 0) {
        $('#selNomenclatura').append('<option value="0" selected="selected">-Seleccione-</option>');
        for (i = 0; i < datos.length; i++) {
            $('#selNomenclatura').append('<option value="' + datos[i].NOM_ID + '">' + datos[i].NOM_NOMBRE + '</option>');
            $('#selNomenclaturaComunicacion').append('<option value="' + datos[i].NOM_ID + '">' + datos[i].NOM_NOMBRE + '</option>');
        }
    }
    else {
        $('#selNomenclatura').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
        $('#selNomenclaturaComunicacion').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function LlenarListaComplementos(datos) {
    if (datos !== null && datos.length > 0) {
        $('#selComplemento').append('<option value="0" selected="selected">-Seleccione-</option>');
        for (i = 0; i < datos.length; i++) {
            $('#selComplemento').append('<option value="' + datos[i].COM_DIR_ID + '">' + datos[i].COM_DIR_NOMBRE + '</option>');
            $('#selComplementoComunicacion').append('<option value="' + datos[i].COM_DIR_ID + '">' + datos[i].COM_DIR_NOMBRE + '</option>');

        }
    }
    else {
        $('#selComplemento').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
        $('#selComplementoComunicacion').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function LlenarListaTiposIdentificacion(datos) {
    if (datos !== null && datos.length > 0) {
        $('#slTipoDocumentoContacto').append('<option value="0" selected="selected">-Seleccione-</option>');
        $('#slTipoDocumento').append('<option value="0" selected="selected">-Seleccione-</option>');
        $('#selTipoDocumentoJuridica').append('<option value="0" selected="selected">-Seleccione-</option>');
        $('#selTipoDocumentoPrivada').append('<option value="0" selected="selected">-Seleccione-</option>');
        for (i = 0; i < datos.length; i++) {
            if (datos[i].TPE_ID == 1) {
                $('#slTipoDocumento').append('<option value="' + datos[i].TID_ID + '">' + datos[i].TID_NOMBRE + '</option>');
                $('#slTipoDocumentoContacto').append('<option value="' + datos[i].TID_ID + '">' + datos[i].TID_NOMBRE + '</option>');
            }
            else if (datos[i].TPE_ID == 2) {
                $('#selTipoDocumentoJuridica').append('<option value="' + datos[i].TID_ID + '">' + datos[i].TID_NOMBRE + '</option>');
            }
            else if (datos[i].TPE_ID == 3) {
                $('#selTipoDocumentoPrivada').append('<option value="' + datos[i].TID_ID + '">' + datos[i].TID_NOMBRE + '</option>');
            }
        }
    }
    else {
        $('#selPaisNatural').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
        $('#selTipoDocumentoJuridica').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
        $('#selTipoDocumentoPrivada').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function LlenarListaTiposIdentificacionDinamico(datos, nombreCampo) {
    if (datos !== null && datos.length > 0) {
        $('#' + nombreCampo).append('<option value="0" selected="selected">-Seleccione-</option>');
        for (i = 0; i < datos.length; i++) {
            if (datos[i].TPE_ID == 1) {
                $('#' + nombreCampo).append('<option value="' + datos[i].TID_ID + '">' + datos[i].TID_NOMBRE + '</option>');
            }
        }
    }
    else {
        $('#' + nombreCampo).children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function LlenarListaExpedientes(datos, nombreCampo) {
    if (datos !== null && datos.length > 0) {
        $('#' + nombreCampo).append('<option value="0" selected="selected">-Seleccione-</option>');
        for (i = 0; i < datos.length; i++) {
            $('#' + nombreCampo).append('<option value="' + datos[i].EXP_CODIGO + '">' + datos[i].EXP_CODIGO + '</option>');
        }
    }
    else {
        $('#' + nombreCampo).append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function LlenarListaTipoContacto(datos) {
    if (datos !== null && datos.length > 0) {
        $('#selContacto').append('<option value="0" selected="selected">-Seleccione-</option>');
        for (i = 0; i < datos.length; i++) {
            $('#selContacto').append('<option value="' + datos[i].TipoContactoId + '">' + datos[i].NombreTipoContacto + '</option>');
        }
    }
    else {
        $('#selContacto').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function LlenarUsuarioRegistrado(datos) {
    if (datos !== null && datos.length > 0) {
        $("#btnEnviar").hide();
        $("#btnActualizar").show();
        $("#selAutoridadAmbiental").val(datos[0].AutoridadAmbiental).change();
        if (datos[0].CodigoTipoPersona == 1) {
            $("#txtPrimerNombreNatural").val(datos[0].PrimerNombre);
            $("#txtSegundoNombreNatural").val(datos[0].SegundoNombre);
            $("#txtPrimerApellidoNatural").val(datos[0].PrimerApellido);
            $("#txtSegundoApellidoNatural").val(datos[0].SegundoApellido);
            $("#slTipoDocumento").val(datos[0].TipoDocumentoIdentificacion).change();
            $("#selDepartamentoOrigenNatural").val(datos[0].dirExpDocumentoDepartamentoId).change();
            $("#selMunicipioOrigenNatural").val(datos[0].dirExpDocumentoMunicipioId).change();
            $("#txtNumeroIdentificacion").val(datos[0].NumeroIdentificacion);
            $("#txtTelefonoNatural").val(datos[0].Telefono);
            $("#txtCelularNatural").val(datos[0].Celular);
            $("#txtFaxNatural").val(datos[0].Fax);
            $("#txtCorreoNatural").val(datos[0].CorreoElectronico);
            $("#selPaisNatural").val(datos[0].DireccionPersonaPaisId).change();
            $("#selDepartamentoNatural").val(datos[0].DireccionPersonaDepartamentoId).change();
            $("#selMunicipioNatural").val(datos[0].DireccionPersonaMunicipioId).change();
            $("#selCorregimientoNatural").val(datos[0].DireccionPersonaCorregimientoId).change();
            $("#selVeredaNatural").val(datos[0].DireccionPersonaVeredaId).change();
            $("#txtDireccionNatural").val(datos[0].DireccionPersona);
        }
        if (datos[0].listaContactos.length > 0) {
            var lista = datos[0].listaContactos;
            var contarDiv = 0;
            var contarEncabezado = 0;
            var contarTituloCampo = 0;
            var contarDivDatos = 0;
            var divTipoContacto = "";
            var divEncabezado = "";
            var temInfoContacto = "";
            try {
                for (i = 0; i < lista.length; i++) {
                    contarDiv = 0;
                    contarEncabezado = 0;
                    contarTituloCampo = 0;
                    for (j = 0; j < datosListaContactos.length; j++) {
                        if (datosListaContactos[j].TipoContactoId == lista[i].TipoContactoId)
                            divTipoContacto = datosListaContactos[j].NombreTipoContacto;
                    }
                    divTipoContacto = divTipoContacto.toString().toLowerCase();
                    $("#divListaContacto div").each(function (index) {
                        if ($(this)[0].id == divTipoContacto) {
                            contarDiv = contarDiv + 1;
                        }
                    });
                    if (contarDiv == 0) {
                        $('#divListaContacto').append('<div id="' + divTipoContacto + '" class="panel-title"><span class="col-md-1"><label class="form-control-xsm" name="' + lista[i].TipoContactoId + '">LISTA DE USUARIOS DE:' + divTipoContacto.toString().toUpperCase() + '</label></span></div>');
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   div encabezado contacto comunicaciones ///////////////////////////////////////////////////////////////// 
                    divEncabezado = "th" + divTipoContacto;
                    $("#divListaContacto div").each(function (index) {
                        if ($(this)[0].id == divEncabezado) {
                            contarEncabezado = contarEncabezado + 1;
                        }
                    });
                    if (contarEncabezado == 0) {
                        $("#" + divTipoContacto).append('<div id="' + divEncabezado + '" class="row"></div>');
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   div encabezado titulos ///////////////////////////////////////////////////////////////// 
                    $("#" + divEncabezado + " label").each(function (index) {
                        if ($(this)[0].textContent == lista[i].EtiquetaCampo) {
                            contarTituloCampo = contarTituloCampo + 1;
                        }
                    });
                    if (contarTituloCampo == 0) {
                        $('#' + divEncabezado + '').append('<div class="col-md-1"><label class="control-label-xxsm">' + lista[i].EtiquetaCampo + '</label></div>');
                    }

                    ///////////////////////////////////////////////////////// div datos contacto 
                    temInfoContacto = "#datos" + divTipoContacto + lista[i].ContadorId;
                    if ($(temInfoContacto).length == 0) {
                        $("#" + divTipoContacto).append('<div id="datos' + divTipoContacto + lista[i].ContadorId + '" class="row"></div>');
                        $(temInfoContacto).append('<span class="col-md-1"><label class="control-label-xxsm-n" id="contadorID" name="contadorID" style="display:none">' + lista[i].ContadorId + '</label></span>');
                    }
                    $(temInfoContacto).append('<span class="col-md-1"><label class="control-label-xxsm-n" id="' + lista[i].CampoId + '" name="' + lista[i].CampoId + '">' + lista[i].Valor + '</label></span>');
                    if (i + 1 < lista.length)
                        if (lista[i].ContadorId != lista[i + 1].ContadorId)
                            $(temInfoContacto).append('<div class="col-md-1"><span class="col-md-1"><button type="button" class="btn btn-success" id="btn' + divTipoContacto + lista[i].ContadorId + '" onclick="EditarContacto()" name="' + lista[i].TipoContactoId + '">Editar</button></span></div>');
                        else
                        { }
                    else if (i + 1 == lista.length)
                        $(temInfoContacto).append('<div class="col-md-1"><span class="col-md-1"><button type="button" class="btn btn-success" id="btn' + divTipoContacto + lista[i].ContadorId + '" onclick="EditarContacto()" name="' + lista[i].TipoContactoId + '">Editar</button></span></div>');
                }
            }
            catch (e) {
                $("#divErrores").css("display", "block");
                $("#divErrores").append('<label class="form-control-xsm">" Error cargando lista de contactos"</label>');
            }
        }
    }
}
//********************************************Region eventos listas desplegables***************************************************************************************************//
function SelDepartamentoOrigenNaturalChange() {
    if (datosListaDepartamentos !== undefined && datosListaDepartamentos.length > 0) {
        var valor = document.getElementById('selDepartamentoOrigenNatural').value;
        CargarListaMunicipios(valor);
    }
    else {
        $('#selMunicipioOrigenNatural').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function SelDepartamentoNaturalChange() {
    if (datosListaDepartamentos !== undefined && datosListaDepartamentos.length > 0) {
        var valor = document.getElementById('selDepartamentoNatural').value;
        CargarListaMunicipiosNatural(valor);
    }
    else {
        $('#selMunicipioNatural').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function SelMunicipioNaturalChange() {
    if (datosListaMunicipios !== undefined && datosListaMunicipios.length > 0) {
        var valor = document.getElementById('selMunicipioNatural').value;
        CargarListaCorregimientos(valor);
    }
    else {
        $('#selCorregimientoNatural').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function SelCorregimientoNaturalChange() {
    if (datosListaCorregimientos !== undefined && datosListaCorregimientos.length > 0) {
        var valorMunicipio = document.getElementById('selMunicipioNatural').value;
        var valorCorregimiento = document.getElementById('selCorregimientoNatural').value;
        CargarListaVeredas(valorMunicipio, valorCorregimiento);
    }
    else {
        $('#selVeredaNatural').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function SelDepartamentoPrivadaChange() {
    if (datosListaDepartamentos !== undefined && datosListaDepartamentos.length > 0) {
        var valor = document.getElementById('selDepartamentoPrivada').value;
        CargarListaMunicipiosPrivada(valor);
    }
    else {
        $('#selMunicipioPrivada').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function SelMunicipioPrivadaChange() {
    if (datosListaMunicipios !== undefined && datosListaMunicipios.length > 0) {
        var valor = document.getElementById('selMunicipioPrivada').value;
        CargarListaCorregimientosPrivada(valor);
    }
    else {
        $('#selCorregimientoPrivada').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function SelDepartamentoJuridicaChange() {
    if (datosListaDepartamentos !== undefined && datosListaDepartamentos.length > 0) {
        var valor = document.getElementById('selDepartamentoJuridica').value;
        CargarListaMunicipiosJuridica(valor);
    }
    else {
        $('#selMunicipioJuridica').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
function SelMunicipioJuridicaChange() {
    if (datosListaMunicipios !== undefined && datosListaMunicipios.length > 0) {
        var valor = document.getElementById('selMunicipioJuridica').value;
        CargarListaCorregimientosJuridica(valor);
    }
    else {
        $('#selCorregimientoJuridica').children().remove().end().append('<option value="0" selected="selected">-Seleccione-</option>');
    }
}
//********************************************Modulo de cargue de direccion***************************************************************************************************//
function SelNomenclaturaChange() {
    var valor = $("#selNomenclatura :selected").text();
    if (valor != '-Seleccione-') {
        arregloDireccionIngresada[0] = valor;
        EscribirDireccion();
    }
}
function SelLetraTipochange() {
    var valor = document.getElementById('selLetraTipo').value;
    if (valor != 'No aplica') {
        arregloDireccionIngresada[2] = valor;
        EscribirDireccion();
    }
}
function SelBisChange() {
    var valor = document.getElementById('selBis').value;
    if (valor != 'No aplica') {
        arregloDireccionIngresada[3] = valor;
        EscribirDireccion();
    }
}
function SelOrientacionChange() {
    var valor = document.getElementById('selOrientacion').value;
    if (valor != 'No aplica') {
        arregloDireccionIngresada[4] = valor;
        EscribirDireccion();
    }
}
function SelNumeroLetrachange() {
    var valor = document.getElementById('selNumeroLetra').value;
    if (valor != 'No aplica') {
        arregloDireccionIngresada[7] = valor;
        EscribirDireccion();
    }
}
function SelOrientacionChange2() {
    var valor = document.getElementById('selOrientacion2').value;
    if (valor != 'No aplica') {
        arregloDireccionIngresada[9] = valor;
        EscribirDireccion();
    }
}
function SelComplementoChange() {
    var valor = $("#selComplemento :selected").text();
    if (valor != '-Seleccione-') {
        arregloDireccionIngresada[10] = valor;
        EscribirDireccion();
    }
}
function LimpiarDireccion() {
    arregloDireccionIngresada = [];
    EscribirDireccion();
    arregloDireccionIngresada[5] = " # ";
    $("#txtNumero1").val("");
    $("#txtNumero2").val("");
    $("#TextNumeroTipo").val("");
    $("#TextComplemento").val("");
    $("#selNomenclatura").val("0").change();
    $("#selBis").val("No aplica").change();
    $("#selLetraTipo").val("No aplica").change();
    $("#selOrientacion").val("No aplica").change();
    $("#selOrientacion2").val("No aplica").change();
    $("#selNumeroLetra").val("No aplica").change();
    $("#selComplemento").val("0").change();
}
function EscribirDireccion() {
    var dir = arregloDireccionIngresada.toString().replace(/,/g, " ");
    $("#TextDireccionCompleta").val(dir);
}
function BtnDireccionModal() {
    var rbvalue = "";
    rbvalue = $("input[name=TipodeUsuario]");
    if (rbvalue[0].id == "Natural" && rbvalue[0].checked == true && campoTextoDireccionDinamico.length == 0) {
        $("#txtDireccionNatural").val($("#TextDireccionCompleta").val());
        arregloDireccionTemp = arregloDireccionIngresada;
    }
    else if (rbvalue[1].id == "juridicaPublica" && rbvalue[1].checked == true && campoTextoDireccionDinamico.length == 0) {
        $("#txtDireccionJuridica").val($("#TextDireccionCompleta").val());
        arregloDireccionTemp = arregloDireccionIngresada;
    }
    else if (rbvalue[2].id == "juridicaPrivada" && rbvalue[2].checked == true && campoTextoDireccionDinamico.length == 0) {
        $("#txtDireccionPrivada").val($("#TextDireccionCompleta").val());
        arregloDireccionTemp = arregloDireccionIngresada;
    }
    else if (campoTextoDireccionDinamico.length > 0) {
        var dir = arregloDireccionIngresada.toString().replace(/,/g, " ");
        $("#" + campoTextoDireccionDinamico).val(dir);
        $("#modalContacto").modal("show");
        arregloDireccionTemp = arregloDireccionIngresada;
    }
    LimpiarDireccion();
}
function PasarDireccionAmodal() {
    $("#selNomenclatura option").each(function () {
        if ($(this).html() == arregloDireccionTemp[0]) {
            this.selected = "true";
            return;
        }
    });
    $("#TextNumeroTipo").val(arregloDireccionTemp[1]);
    $("#selLetraTipo option").each(function () {
        if ($(this).html() == arregloDireccionTemp[2]) {
            this.selected = "true";
            return;
        }
    });
    $("#selBis option").each(function () {
        if ($(this).html() == arregloDireccionTemp[3]) {
            this.selected = "true";
            return;
        }
    });
    $("#selOrientacion option").each(function () {
        if ($(this).html() == arregloDireccionTemp[4]) {
            this.selected = "true";
            return;
        }
    });
    $("#txtNumero1").val(arregloDireccionTemp[6]);
    $("#selNumeroLetra option").each(function () {
        if ($(this).html() == arregloDireccionTemp[7]) {
            this.selected = "true";
            return;
        }
    });
    if (arregloDireccionTemp[8] != null) {
        var numeroTemp2 = arregloDireccionTemp[8].toString().replace(/-/g, "");
        $("#txtNumero2").val(numeroTemp2);
    }
    $("#selOrientacion2 option").each(function () {
        if ($(this).html() == arregloDireccionTemp[9]) {
            this.selected = "true";
            return;
        }
    });
    $("#selComplemento option").each(function () {
        if ($(this).html() == arregloDireccionTemp[10]) {
            this.selected = "true";
            return;
        }
    });
    $("#TextComplemento").val(arregloDireccionTemp[11]);
    arregloDireccionIngresada = arregloDireccionTemp;
    var dir = arregloDireccionTemp.toString().replace(/,/g, " ");
    $("#TextDireccionCompleta").val(dir);
}
//*******************************************************Modulo de Registro contacto/notificaciones*****************************************************************************//
function SelContactoChange() {
    $("#divDinamico").empty();
    var selContactoModal = document.getElementById('selContacto');
    var valor = $("#selContacto :selected").text();
    if (valor != "-Seleccione-") {
        $("#lblTipoContacto").text(valor);
        $('#modalContacto').modal('show');
        $.ajax({
            async: false,
            type: "POST",
            url: "DatosPersonalesNuevo.aspx",
            data: { Accion: "ListaCamposContacto", data: document.getElementById('selContacto').value },
            dataType: "json",
            success: function (data) {
                if (data !== undefined) {
                    LLenarCamposTexoContacto(data);
                }
            }
        });
    }
}
function LLenarCamposTexoContacto(datos) {
    if (datos !== null && datos.length > 0) {
        for (i = 0; i < datos.length; i++) {
            switch (datos[i].TipoControl) {
                case "text":
                    if (datos[i].NombreCampo.toString().search("Direccion") == 0) {
                        $("#divDinamico").append('<div class="col-md-4"><label class="form-control-xsm" name="' + datos[i].CampoId + '">' + datos[i].EtiquetaCampo + '</label>'
                                                + '<input type="' + datos[i].TipoCampo + '" class="form-control" id="' + datos[i].NombreCampo + '" name="' + datos[i].CampoId + '" data-toggle="modal" data-target="#myModal" /></div>');
                        campoTextoDireccionDinamico = datos[i].NombreCampo;
                    }
                    else {
                        $("#divDinamico").append('<div class="col-md-4"><label class="form-control-xsm" name="' + datos[i].CampoId + '">' + datos[i].EtiquetaCampo + '</label>'
                                               + '<input type="' + datos[i].TipoCampo + '" title="' + datos[i].Titulo + '" class="form-control" id="'
                                               + datos[i].NombreCampo + '" name="' + datos[i].CampoId + '"/></div>');
                    }
                    break;
                case "list":
                    $("#divDinamico").append('<div class="col-md-4"><label class="form-control-xsm" name="' + datos[i].CampoId + '">' + datos[i].EtiquetaCampo + '</label>'
                                                            + '<select class="form-control-list" id="' + datos[i].NombreCampo
                                                            + '" name="' + datos[i].CampoId + '" class="form-control-list"></select></div>');
                    if (datos[i].NombreCampo.toString().search("TipoDocumento") == 0) {
                        var nombreControlTipoDocumento = datos[i].NombreCampo;
                        $.ajax({
                            type: "POST",
                            url: "DatosPersonalesNuevo.aspx",
                            data: { Accion: "CargarTiposIdentificacion" },
                            dataType: "json",
                            success: function (dataTipoDocumento) {
                                if (dataTipoDocumento !== undefined) {
                                    LlenarListaTiposIdentificacionDinamico(dataTipoDocumento, nombreControlTipoDocumento);
                                }
                            }
                        });
                    }
                    else if (datos[i].NombreCampo.toString().search("Expediente") == 0) {
                        var nombreControlExpediente = datos[i].NombreCampo;
                        $.ajax({
                            type: "POST",
                            url: "DatosPersonalesNuevo.aspx",
                            data: { Accion: "CargarExpedientesAsociados" },
                            dataType: "json",
                            success: function (dataExpediente) {
                                if (dataExpediente !== undefined) {
                                    LlenarListaExpedientes(dataExpediente, nombreControlExpediente);
                                }
                            }
                        });
                    }
                    break;
            }
        }
    }
}
function LimpiarDivContacto() {
    $("#txtNombreCompleto").val("");
    $("#txtNumeroIdentificacionContacto").val("");
    $("#txtCargo").val("");
    $("#txtArea").val("");
    $("#selContacto").val("0").change();
    $("#slTipoDocumentoContacto").val("0").change();
    $("#ListaContactos tr:gt(0)").remove();
    $("#divErroresContacto").empty();
}
function EditarContacto() {
    var e = window.event,
       btn = e.target || e.srcElement;
    var nombreFila = btn.id;
    var temTipoContacto = btn.name;
    var divNombre = nombreFila.replace(/btn/, "datos");
    var matches = document.querySelectorAll("#" + divNombre + " span label");
    $("#selContacto").val(temTipoContacto).change();
    var tempID = "";
    var valorTemp = "";
    for (var i = 0, l = matches.length; i < l; i++) {
        tempID = matches[i].getAttribute("id");
        valor = matches[i].textContent;
        $("#modalContacto #" + tempID + "").val(valor);
        $("#modalContacto input[name*='" + tempID + "']").val(valor);
    }
    $("#modalContacto").modal("show");
    $("div").remove("#" + divNombre + "");
}
function AgregarContacto() {
    ////////////////////////////////////////////////////////////     div contacto comunicaciones 
    var contar = 0;
    var divTipoContacto = $("#selContacto :selected").text();
    divTipoContacto = divTipoContacto.toString().toLowerCase();
    $("#divListaContacto div").each(function (index) {
        if ($(this)[0].id == divTipoContacto) {
            contar = contar + 1;
        }
    });
    if (contar == 0) {
        $('#divListaContacto').append('<div id="' + divTipoContacto + '" class="panel-title"><span class="form-inline"><label class="form-inline" name="' + document.getElementById('selContacto').value + '"> LISTA DE USUARIOS DE: ' + $("#selContacto :selected").text() + '</label></span></div>');
    }
    /////////////////////////////////////////////////////////////////   div encabezado contacto comunicaciones 
    var divEncabezado = "th" + divTipoContacto;
    contar = 0;
    $("#divListaContacto div").each(function (index) {
        if ($(this)[0].id == divEncabezado) {
            contar = contar + 1;
        }
    });
    var temp = "#" + divTipoContacto;
    if (contar == 0) {
        $(temp).append('<div id="' + divEncabezado + '" class="row"></div>');
        $("#divDinamico label").each(function (index) {
            $('#' + divEncabezado + '').append('<div class="col-md-1"><label class="control-label-xxsm">' + $(this).text() + '</label></div>');
        });
    }
    /////////////////////////////////////////////////////////       div datos contacto comunicaciones 
    var divFilas = "datos" + divTipoContacto;
    contar = 0;
    $("#divListaContacto div").each(function (index) {
        if ($(this)[0].id.search(divFilas) == 0) {
            contar = contar + 1;
        }
    });
    $(temp).append('<div id="datos' + divTipoContacto + contar + '" class="row"></div>');
    var temInfoContacto = "#datos" + divTipoContacto + contar;
    $(temInfoContacto).append('<span class="col-md-1"><label class="control-label-xxsm-n" id="contadorID" name="contadorID" style="display:none">' + contar + '</label></span>');

    $("#divDinamico input, #divDinamico select").each(function (index) {
        if ($(this)[0].value.length > 0)
            $(temInfoContacto).append('<span class="col-md-1"><label class="control-label-xxsm-n" id="' + $(this)[0].id + '" name="' + $(this)[0].name + '">' + $(this)[0].value + '</label></span>');
        else
            $(temInfoContacto).append('<span class="col-md-1"><label class="control-label-xxsm-n" id="' + $(this)[0].id + '" hidden="hidden" name="' + $(this)[0].name + '">No informa</label></label></span>');

    });
    $(temInfoContacto).append('<div class="col-md-1"><span class="col-md-1"><button type="button" class="btn btn-success" id="btn' + divTipoContacto + contar + '" onclick="EditarContacto()" name="' + document.getElementById('selContacto').value + '">Editar</button></span></div>');
    $('#modalContacto').modal('hide');
    $("#selContacto").val("0").change();
    arregloDireccionTemp = [];
    campoTextoDireccionDinamico = "";
}
function CancelarAgregarContacto() {
    $("#selContacto").val("0").change();
}
///****************************ENVIAR DATOS*************************************************//
function EnviarFormulario() {
    if (ValidarDatos()) {
        var ver = VerificarExistenciaPorNumeroIdentificacion();
        var personaRegistro = "";
        if (ver == -1) {
            switch (tipoUsuarioSeleccionado) {
                case "Natural":
                    personaRegistro = {
                        "primerApellido": $("#txtPrimerApellidoNatural").val(),
                        "segundoApellido": $("#txtSegundoApellidoNatural").val(),
                        "primerNombre": $("#txtPrimerNombreNatural").val(),
                        "segundoNombre": $("#txtSegundoNombreNatural").val(),
                        "numeroIdentificacion": $("#txtNumeroIdentificacion").val(),
                        "tipoDocumentoIdentificacion": document.getElementById('slTipoDocumento').value,
                        "DepartamentoOrigen": document.getElementById('selDepartamentoOrigenNatural').value,
                        "MunicipioOrigen": document.getElementById('selMunicipioOrigenNatural').value,
                        "direccionNatural": $('#txtDireccionNatural').val(),
                        "pais": document.getElementById('selPaisNatural').value,
                        "Departamento": document.getElementById('selDepartamentoNatural').value,
                        "Municipio": document.getElementById('selMunicipioNatural').value,
                        "Corregimiento": document.getElementById('selCorregimientoNatural').value,
                        "Vereda": document.getElementById('selVeredaNatural').value,
                        "telefono": $("#txtTelefonoNatural").val(),
                        "celular": $("#txtCelularNatural").val(),
                        "fax": $("#txtFaxNatural").val(),
                        "correoElectronico": $("#txtCorreoNatural").val(),
                        "tipoPersona": tipoUsuarioSeleccionado,
                        "idAutoridadAmbiental": document.getElementById('selAutoridadAmbiental').value,
                        "AutoridadAmbiental": $("#selAutoridadAmbiental :selected").text(),
                        "TipoDocumentoIdentificacion": $("#slTipoDocumento :selected").text()
                    };
                    break;
                case "juridicaPublica":
                    personaRegistro =
                        {
                            "RazonSocialJuridica": $("#txtRazonSocialJuridica").val(),
                            "NumeroDocumentoJuridica": $("#txtNumeroDocumentoJuridica").val(),
                            "DireccionJuridica": $("#txtDireccionJuridica").val(),
                            "TelefonoJuridica": $("#txtTelefonoJuridica").val(),
                            "CelularJuridica": $("#txtCelularJuridica").val(),
                            "FaxJuridica": $("#txtFaxJuridica").val(),
                            "CorreoJuridica": $("#txtCorreoJuridica").val(),
                            "TipoDocumentoJuridica": document.getElementById('selTipoDocumentoJuridica').value,
                            "PaisJuridica": document.getElementById('selPaisJuridica').value,
                            "DepartamentoJuridica": document.getElementById('selDepartamentoJuridica').value,
                            "MunicipioJuridica": document.getElementById('selMunicipioJuridica').value,
                            "CorregimientoJuridica": document.getElementById('selCorregimientoJuridica').value,
                            "VeredaJuridica": document.getElementById('selVeredaJuridica').value,
                            "idAutoridadAmbiental": document.getElementById('selAutoridadAmbiental').value,
                            "AutoridadAmbiental": $("#selAutoridadAmbiental :selected").text(),
                            "tipoPersona": tipoUsuarioSeleccionado,
                        }
                    break;
                case "juridicaPrivada":
                    personaRegistro =
                        {
                            "RazonSocialPrivada": $("#txtRazonSocialPrivada").val(),
                            "NumeroDocumentoPrivada": $("#txtNumeroDocumentoPrivada").val(),
                            "DireccionPrivada": $("#txtDireccionPrivada").val(),
                            "TelefonoPrivada": $("#txtTelefonoPrivada").val(),
                            "CelularPrivada": $("#txtCelularPrivada").val(),
                            "FaxPrivada": $("#txtFaxPrivada").val(),
                            "CorreoPrivada": $("#txtCorreoPrivada").val(),
                            "TipoDocumentoPrivada": document.getElementById('selTipoDocumentoPrivada').value,
                            "PaisPrivada": document.getElementById('selPaisPrivada').value,
                            "DepartamentoPrivada": document.getElementById('selDepartamentoPrivada').value,
                            "MunicipioPrivada": document.getElementById('selMunicipioPrivada').value,
                            "CorregimientoPrivada": document.getElementById('selCorregimientoPrivada').value,
                            "VeredaPrivada": document.getElementById('selVeredaPrivada').value,
                            "idAutoridadAmbiental": document.getElementById('selAutoridadAmbiental').value,
                            "AutoridadAmbiental": $("#selAutoridadAmbiental :selected").text(),
                            "tipoPersona": tipoUsuarioSeleccionado,
                        }
                    break;
            }
            var valorCampoContacto = "";
            var tempListaContacto = [], matches = document.querySelectorAll("#divListaContacto span label");
            valorCampoContacto = {}
            var numero = 0;
            for (var i = 0, l = matches.length; i < l; i++) {
                if (matches[i].getAttribute("id") == "contadorID")
                    numero = matches[i].textContent;
                valorCampoContacto = {
                    "campoId": matches[i].getAttribute("name"),
                    "valor": matches[i].textContent,
                    "contadorID": numero,
                }
                tempListaContacto.push(valorCampoContacto);
            }
            var informacionPrincipalSerializado;
            informacionPrincipalSerializado = JSON.stringify(personaRegistro);
            var informacionContactoSerializado;
            informacionContactoSerializado = JSON.stringify(tempListaContacto);
            Enviar(informacionPrincipalSerializado, informacionContactoSerializado);
        }
    }
}
function Enviar(informacionPrincipalSerializado, informacionContactoSerializado) {
    $.ajax({
        async: false,
        type: "POST",
        url: "DatosPersonalesNuevo.aspx",
        data: { Accion: 'EnviarFormulario', InformacionPrincipalSerializado: informacionPrincipalSerializado, InformacionContactoSerializado: informacionContactoSerializado },
        dataType: 'json',
        success: function (data) {
            if (data !== undefined) {
                alert(data);
            }
        },
        complete: function (data) {
            $("#divErrores").css("display", "block");
            if (data.responseText.search("Error") == 0)
                $("#divErrores").append('<label class="form-control-xsm">"' + data.responseText + '"</label>');
            else {
                $("#divErrores").css("background-color", "#04B431");
                $("#divErrores").append('<label class="form-control-xsm">"' + data.responseText + '"</label>');
            }
        }
    });
}
function VerificarExistenciaPorNumeroIdentificacion() {
    var rbvalue = $("input[name=TipodeUsuario]");
    var valor = "";
    if (rbvalue[0].id == "Natural" && rbvalue[0].checked == true)
        valor = $("#txtNumeroIdentificacion").val();
    else if (rbvalue[1].id == "juridicaPublica" && rbvalue[1].checked == true)
        valor = $("#txtNumeroDocumentoJuridica").val();
    else if (rbvalue[2].id == "juridicaPrivada" && rbvalue[2].checked == true)
        valor = $("#txtNumeroDocumentoPrivada").val();

    var retorno = "";
    if (valor != null && valor.length > 1) {
        $.ajax({
            async: false,
            type: "POST",
            url: "DatosPersonalesNuevo.aspx",
            data: { Accion: "VerificarNumeroIdentificacion", numeroIdentificacion: valor },
            dataType: "json",
            success: function (data) {
                if (data !== undefined) {
                    switch (data) {
                        //- 1:No existe la solicitud.
                        case -1:
                            break;
                            //0: Aprobado ,
                        case 0:
                            $("#divErrores").append('<label class="form-control-xsm">Ya existe un USUARIO REGISTRADO con los mismos datos de identificación</label>');
                            $("#divErrores").css("display", "block");
                            break;
                            //1: EnProceso
                        case 1:
                            $("#divErrores").append('<label class="form-control-xsm">Ya existe una SOLICITUD EN PROCESO. Debe esperar un mensaje de correo electrónico con la activación de su solicitud</label>');
                            $("#divErrores").css("display", "block");
                            break;
                            //2:Rechazado
                        case 2:
                            $("#divErrores").append('<label class="form-control-xsm">Su solicitud ya fué tramitada y fué rechazada, por favor acerquese a la Autoridad Ambiental</label>');
                            $("#divErrores").css("display", "block");
                            break;
                    }
                    retorno = data;
                }
            }
        });
    }
    else
        alert("Debe ingresar un Número de Identificación.");
    return retorno;
}
function CancelarFormulario() {
    LimpiarFormulario();
}
function LimpiarFormulario() {
    LimpiarDireccion();
    $("#divErrores").empty();
    $("#divErrores").css("display", "none");
    $('#divPersonaNatural').find('input:text').val("");
    $("#txtNumeroIdentificacion, #txtTelefonoNatural, #txtCelularNatural, #txtFaxNatural").val("");
    $("#txtNumeroDocumentoJuridica, #txtTelefonoJuridica, #txtCelularJuridica, #txtFaxJuridica").val("");
    $("#txtNumeroDocumentoPrivada, #txtTelefonoPrivada, #txtCelularPrivada, #txtFaxPrivada").val("");
    $("#txtCorreoNatural, #txtTelefonoPrivada, #txtCelularPrivada, #txtFaxPrivada").val("");
    $("#slTipoDocumento, #selDepartamentoOrigenNatural, #selMunicipioOrigenNatural").val("0").change();
    $("#selPaisNatural, #selDepartamentoNatural, #selMunicipioNatural, #selAutoridadAmbiental").val("0").change();
    $("#divErrores").css("background-color", "#FE2E2E");
    $("#btnActualizar").hide();
    $("#btnEnviar").show();
    $("#divListaContacto").empty();
}
function Actualizar() {
    if (ValidarDatos()) {
        var personaRegistro = "";
        switch (tipoUsuarioSeleccionado) {
            case "Natural":
                personaRegistro = {
                    "primerApellido": $("#txtPrimerApellidoNatural").val(),
                    "segundoApellido": $("#txtSegundoApellidoNatural").val(),
                    "primerNombre": $("#txtPrimerNombreNatural").val(),
                    "segundoNombre": $("#txtSegundoNombreNatural").val(),
                    "numeroIdentificacion": $("#txtNumeroIdentificacion").val(),
                    "tipoDocumentoIdentificacion": document.getElementById('slTipoDocumento').value,
                    "DepartamentoOrigen": document.getElementById('selDepartamentoOrigenNatural').value,
                    "MunicipioOrigen": document.getElementById('selMunicipioOrigenNatural').value,
                    "direccionNatural": $('#txtDireccionNatural').val(),
                    "pais": document.getElementById('selPaisNatural').value,
                    "Departamento": document.getElementById('selDepartamentoNatural').value,
                    "Municipio": document.getElementById('selMunicipioNatural').value,
                    "Corregimiento": document.getElementById('selCorregimientoNatural').value,
                    "Vereda": document.getElementById('selVeredaNatural').value,
                    "telefono": $("#txtTelefonoNatural").val(),
                    "celular": $("#txtCelularNatural").val(),
                    "fax": $("#txtFaxNatural").val(),
                    "correoElectronico": $("#txtCorreoNatural").val(),
                    "tipoPersona": tipoUsuarioSeleccionado,
                    "idAutoridadAmbiental": document.getElementById('selAutoridadAmbiental').value,
                    "AutoridadAmbiental": $("#selAutoridadAmbiental :selected").text(),
                    "TipoDocumentoIdentificacion": $("#slTipoDocumento :selected").text()
                };
                break;
            case "juridicaPublica":
                personaRegistro =
                    {
                        "RazonSocialJuridica": $("#txtRazonSocialJuridica").val(),
                        "NumeroDocumentoJuridica": $("#txtNumeroDocumentoJuridica").val(),
                        "DireccionJuridica": $("#txtDireccionJuridica").val(),
                        "TelefonoJuridica": $("#txtTelefonoJuridica").val(),
                        "CelularJuridica": $("#txtCelularJuridica").val(),
                        "FaxJuridica": $("#txtFaxJuridica").val(),
                        "CorreoJuridica": $("#txtCorreoJuridica").val(),
                        "TipoDocumentoJuridica": document.getElementById('selTipoDocumentoJuridica').value,
                        "PaisJuridica": document.getElementById('selPaisJuridica').value,
                        "DepartamentoJuridica": document.getElementById('selDepartamentoJuridica').value,
                        "MunicipioJuridica": document.getElementById('selMunicipioJuridica').value,
                        "CorregimientoJuridica": document.getElementById('selCorregimientoJuridica').value,
                        "VeredaJuridica": document.getElementById('selVeredaJuridica').value,
                        "idAutoridadAmbiental": document.getElementById('selAutoridadAmbiental').value,
                        "AutoridadAmbiental": $("#selAutoridadAmbiental :selected").text(),
                        "tipoPersona": tipoUsuarioSeleccionado,
                    }
                break;
            case "juridicaPrivada":
                personaRegistro =
                    {
                        "RazonSocialPrivada": $("#txtRazonSocialPrivada").val(),
                        "NumeroDocumentoPrivada": $("#txtNumeroDocumentoPrivada").val(),
                        "DireccionPrivada": $("#txtDireccionPrivada").val(),
                        "TelefonoPrivada": $("#txtTelefonoPrivada").val(),
                        "CelularPrivada": $("#txtCelularPrivada").val(),
                        "FaxPrivada": $("#txtFaxPrivada").val(),
                        "CorreoPrivada": $("#txtCorreoPrivada").val(),
                        "TipoDocumentoPrivada": document.getElementById('selTipoDocumentoPrivada').value,
                        "PaisPrivada": document.getElementById('selPaisPrivada').value,
                        "DepartamentoPrivada": document.getElementById('selDepartamentoPrivada').value,
                        "MunicipioPrivada": document.getElementById('selMunicipioPrivada').value,
                        "CorregimientoPrivada": document.getElementById('selCorregimientoPrivada').value,
                        "VeredaPrivada": document.getElementById('selVeredaPrivada').value,
                        "idAutoridadAmbiental": document.getElementById('selAutoridadAmbiental').value,
                        "AutoridadAmbiental": $("#selAutoridadAmbiental :selected").text(),
                        "tipoPersona": tipoUsuarioSeleccionado,
                    }
                break;
        }
        var valorCampoContacto = "";
        var tempListaContacto = [], matches = document.querySelectorAll("#divListaContacto span label");
        valorCampoContacto = {}
        var numero = 0;
        for (var i = 0, l = matches.length; i < l; i++) {
            if (matches[i].getAttribute("id") == "contadorID")
                numero = matches[i].textContent;
            valorCampoContacto = {
                "campoId": matches[i].getAttribute("name"),
                "valor": matches[i].textContent,
                "contadorID": numero,
            }
            tempListaContacto.push(valorCampoContacto);
        }
        var informacionPrincipalSerializado;
        informacionPrincipalSerializado = JSON.stringify(personaRegistro);
        var informacionContactoSerializado;
        informacionContactoSerializado = JSON.stringify(tempListaContacto);
        $.ajax({
            async: false,
            type: "POST",
            url: "DatosPersonalesNuevo.aspx",
            data: { Accion: 'Actualizar', InformacionPrincipalSerializado: informacionPrincipalSerializado, InformacionContactoSerializado: informacionContactoSerializado },
            dataType: 'json',
            success: function (data) {
                if (data !== undefined) {
                    alert(data);
                }
            },
            complete: function (data) {
                $("#divErrores").css("display", "block");
                if (data.responseText.search("Error") == 0)
                    $("#divErrores").append('<label class="form-control-xsm">"' + data.responseText + '"</label>');
                else {
                    $("#divErrores").css("background-color", "#04B431");
                    $("#divErrores").append('<label class="form-control-xsm">"' + data.responseText + '"</label>');
                }
            }
        });


    }
}
function ValidarDatos() {
    $("#divErrores").empty();
    var retorno = true;
    var rbvalue = $("input[name=TipodeUsuario]");
    var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
    if ($("#selAutoridadAmbiental").val() == 0) {
        $("#divErrores").append('<label class="form-control-xsm">/Debe seleccionar una Autoridad Ambiental.</label>');
        retorno = false;
    }
    if (rbvalue[0].id == "Natural" && rbvalue[0].checked == true) {
        if ($("#txtPrimerNombreNatural").val().length <= 1) {
            $("#divErrores").append('<label class="form-control-xsm">Primer Nombre es obligatorio.</label>');
            retorno = false;
        }
        if ($("#txtPrimerApellidoNatural").val().length <= 1) {
            $("#divErrores").append('<label class="form-control-xsm">/Primer Apellido es obligatorio.</label>');
            retorno = false;
        }
        if ($("#slTipoDocumento").val() == 0) {
            $("#divErrores").append('<label class="form-control-xsm">/Debe seleccionar un Tipo Documento.</label>');
            retorno = false;
        }
        if ($("#selDepartamentoOrigenNatural").val() == 0) {
            $("#divErrores").append('<label class="form-control-xsm">/Debe seleccionar un Departamento de origen.</label>');
            retorno = false;
        }
        if ($("#selDepartamentoNatural").val() == 0) {
            $("#divErrores").append('<label class="form-control-xsm">/Debe seleccionar un Departamento.</label>');
            retorno = false;
        }
        if ($("#selPaisNatural").val() == 0) {
            $("#divErrores").append('<label class="form-control-xsm">/Debe seleccionar su país de origen.</label>');
            retorno = false;
        }
        if ($("#selMunicipioOrigenNatural").val() == 0) {
            $("#divErrores").append('<label class="form-control-xsm">/Debe seleccionar un Municipio De origen.</label>');
            retorno = false;
        }
        if ($("#txtNumeroIdentificacion").val().length <= 1) {
            $("#divErrores").append('<label class="form-control-xsm">/Número de Documento obligatorio.</label>');
            retorno = false;
        }
        if ($("#txtDireccionNatural").val().length <= 1) {
            $("#divErrores").append('<label class="form-control-xsm">/Dirección es obligatorio.</label>');
            retorno = false;
        }
        if (pattern.test($("#txtCorreoNatural").val()) == false) {
            $("#divErrores").append('<label class="form-control-xsm">/El correo electrónico introducido no es correcto.</label>');
            retorno = false;
        }
    }
    else if (rbvalue[1].id == "juridicaPublica" && rbvalue[1].checked == true) {
        if ($("#txtRazonSocialJuridica").val().length <= 1) {
            $("#divErrores").append('<label class="form-control-xsm">/La Razon Social obligatoria.</label>');
            retorno = false;
        }
        if ($("#txtNumeroDocumentoJuridica").val().length <= 1) {
            $("#divErrores").append('<label class="form-control-xsm">/Número de Documento es obligatorio.</label>');
            retorno = false;
        }
        if ($("#txtDireccionJuridica").val().length <= 1) {
            $("#divErrores").append('<label class="form-control-xsm">/Dirección es obligatorio.</label>');
            retorno = false;
        }
        if ($("#selTipoDocumentoJuridica").val() == 0) {
            $("#divErrores").append('<label class="form-control-xsm">/El Tipo de Documento es obligatorio.</label>');
            retorno = false;
        }
        if ($("#selPaisJuridica").val() == 0) {
            $("#divErrores").append('<label class="form-control-xsm">/El país es obligatoria.</label>');
            retorno = false;
        }
        if ($("#selDepartamentoJuridica").val() == 0) {
            $("#divErrores").append('<label class="form-control-xsm">/El Departamento es obligatorio.</label>');
            retorno = false;
        }
        if ($("#selMunicipioJuridica").val() == 0) {
            $("#divErrores").append('<label class="form-control-xsm">/Municipio es obligatorio.</label>');
            retorno = false;
        }
        if (pattern.test($("#txtCorreoJuridica").val()) == false) {
            $("#divErrores").append('<label class="form-control-xsm">/El correo electrónico introducido no es correcto.</label>');
            retorno = false;
        }
    }
    else if (rbvalue[2].id == "juridicaPrivada" && rbvalue[2].checked == true) {
        if ($("#txtRazonSocialPrivada").val().length <= 1) {
            $("#divErrores").append('<label class="form-control-xsm">/La Razon Social obligatoria.</label>');
            retorno = false;
        }
        if ($("#txtNumeroDocumentoPrivada").val().length <= 1) {
            $("#divErrores").append('<label class="form-control-xsm">/Número de Documento es obligatorio.</label>');
            retorno = false;
        }
        if ($("#txtDireccionPrivada").val().length <= 1) {
            $("#divErrores").append('<label class="form-control-xsm">/Dirección es obligatorio.</label>');
            retorno = false;
        }
        if ($("#selTipoDocumentoPrivada").val() == 0) {
            $("#divErrores").append('<label class="form-control-xsm">/El Tipo de Documento es obligatorio.</label>');
            retorno = false;
        }
        if ($("#selPaisPrivada").val() == 0) {
            $("#divErrores").append('<label class="form-control-xsm">/El país es obligatoria.</label>');
            retorno = false;
        }
        if ($("#selDepartamentoPrivada").val() == 0) {
            $("#divErrores").append('<label class="form-control-xsm">/El Departamento es obligatorio.</label>');
            retorno = false;
        }
        if ($("#selMunicipioPrivada").val() == 0) {
            $("#divErrores").append('<label class="form-control-xsm">/Municipio es obligatorio.</label>');
            retorno = false;
        }
        if (pattern.test($("#txtCorreoPrivada").val()) == false) {
            $("#divErrores").append('<label class="form-control-xsm">/El correo electrónico introducido no es correcto.</label>');
            retorno = false;
        }
    }
    if (retorno == false)
        $("#divErrores").css("display", "block");
    else
        $("#divErrores").css("display", "none");
    return retorno;
}