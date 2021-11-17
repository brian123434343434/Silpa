/// <reference path="jquery-1.8.2-vsdoc.js" />


var poly;
var map;
var obj;

var codigo;

var markersArray = [];
function initialize() {
    obj = jQuery.parseJSON($('#hdCoordenadas').val());
    var myLatLng;

    if (obj.length > 0) {
        myLatLng = new google.maps.LatLng(obj[0].Latitud, obj[0].Longitud);
    }
    else {
        myLatLng = new google.maps.LatLng(11.06863, -72.68712);
    }
    var myOptions = {
        zoom: 13,
        center: myLatLng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    map = new google.maps.Map(document.getElementById("ubicacionPuntosControl"), myOptions);

    google.maps.event.addListener(map, 'click', function (e) {
        placeMarker(e.latLng, map);
    });

    //var polyOptions = {
    //    strokeColor: "#FF0000",
    //    strokeOpacity: 0.8,
    //    strokeWeight: 2,
    //    fillColor: "#FF0000",
    //    fillOpacity: 0.35
    //};

    //poly = new google.maps.Marker(polyOptions);
    //poly.setMap(map);

    addLatLng();
}

function placeMarker(position, map) {
    deleteOverlays();
    var marker = new google.maps.Marker({
        position: position,
        map: map,
        animation: google.maps.Animation.BOUNCE
    });
    markersArray.push(marker);
    map.panTo(position);
}
function addLatLng() {
    var image = '../App_Themes/Img/police.png';
    var image2 = '../App_Themes/Img/LastPoint.png';
    var imagedef;
    if (obj.length > 0) {
        for (var y = 0; y <= obj.length - 1; y++) {
            if (y == obj.length - 1) { imagedef=image2} else {imagedef=image}
            var myLatlng = new google.maps.LatLng(obj[y].Latitud, obj[y].Longitud);
            markerFind = new google.maps.Marker({
                position: myLatlng, map: map, title: 'Punto Control ' + obj[y].Orden + '\rDepartamento: ' + obj[y].Depto + '\rMunicipio: ' + obj[y].Munpio + '\rAutoridad: ' + obj[y].Autoridad + '\rNombre: ' + obj[y].Nombre + '\rIdentificacion: ' + obj[y].Identificacion + '\rFecha Registro: ' + obj[y].FechaRegistro,
                draggable: true,
                icon: imagedef
            });
        }
    }
}

function agregarPunto(Lat, Lng) {

    if (Lat != null && Lng != null) {
        var myLatlng = new google.maps.LatLng(Lat, Lng);
        markerFind = new google.maps.Marker({
            position: myLatlng, map: map, title: 'Punto Verificación (' + Lat + ' ' + Lng + ')',
            draggable: true,
            animation: google.maps.Animation.BOUNCE
        });
        markersArray.push(markerFind);
        showOverlays();
    }
}
function deleteOverlays() {
    if (markersArray) {
        for (i in markersArray) {
            markersArray[i].setMap(null);
        } markersArray.length = 0;
    }
}
function showOverlays() {
    if (markersArray) { for (i in markersArray) { markersArray[i].setMap(map); } }
}
function GuardarPuntoControl()
{
    if (markersArray.length > 0) {
        var datos = {};
        datos.lat = markersArray[0].getPosition().lat();
        datos.lng = markersArray[0].getPosition().lng();
        datos.count = $('#hdContadorPunto').val();
        datos.logID = $('#hdLogID').val();
        datos = JSON.stringify(datos);
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            url: 'PuntosControl.aspx/GuardarPuntoControl',
            data: datos,
            success: function (response) { }
        })

        window.close();
    }
}
