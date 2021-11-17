/// <reference path="jquery-1.8.2-vsdoc.js" />


var poly;
var map;
var obj;

var codigo;

var markersArray = [];
function initialize() {
    obj = jQuery.parseJSON($('#hdCoordenadas').val());
 

    var myLatLng = new google.maps.LatLng(11.06863, -72.68712);
    var myOptions = {
        zoom: 13,
        center: myLatLng,
        mapTypeId: google.maps.MapTypeId.TERRAIN
    };

    map = new google.maps.Map(document.getElementById("ubicacionRegistros"), myOptions);

    var polyOptions = {
        strokeColor: "#FF0000",
        strokeOpacity: 0.8,
        strokeWeight: 2,
        fillColor: "#FF0000",
        fillOpacity: 0.35
    };

    poly = new google.maps.Polygon(polyOptions);
    poly.setMap(map);

    addLatLng();
}

function addLatLng() {
    var image = '../App_Themes/Img/map_pin.ico';
    for (var y = 0; y <= obj.length - 1; y++) {

        var polyOptions = {
            strokeColor: obj[y].color,
            strokeOpacity: 0.8,
            strokeWeight: 2,
            fillColor: obj[y].color,
            fillOpacity: 0.35
        };

        for (var j = 0; j <= obj[y].localizaciones.length - 1; j++) {
            poly = new google.maps.Polygon(polyOptions);
            poly.setMap(map);

            var path = poly.getPath();
            for (i = 0; i <= obj[y].localizaciones[j].LstCoordenadas.length - 1; i++) {
                var punto = new google.maps.LatLng(obj[y].localizaciones[j].LstCoordenadas[i].CoordenadaNorte, obj[y].localizaciones[j].LstCoordenadas[i].CoordenadaEste);
                path.push(punto);

                var marker = new google.maps.Marker({
                    position: punto,
                    title: '#' + path.getLength() + '(' + obj[y].localizaciones[j].LstCoordenadas[i].CoordenadaNorte + ' ' + obj[y].localizaciones[j].LstCoordenadas[i].CoordenadaEste + ')' + ',' + obj[y].Nombre + ',' + obj[y].Codigo,
                    map: map,
                    icon: image
                });
            }
        }
    }
}
function agregarPunto(Lat, Lng) {
    
    if (Lat != null && Lng != null) {
        var myLatlng = new google.maps.LatLng(Lat, Lng);
        markerFind = new google.maps.Marker({ position: myLatlng, map: map, title: 'Punto Verificación ('+Lat+' '+Lng+')',
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