/// <reference path="jquery-1.8.2-vsdoc.js" />

var poly;
var map;
var obj;
var nombre;
var codigo;

function initialize() {
    obj = jQuery.parseJSON($('#hdCoordenadas').val());
    nombre = jQuery.parseJSON($('#hdNombreMina').val());
    codigo = jQuery.parseJSON($('#hdCodRegMinero').val());

    var myLatLng = new google.maps.LatLng(11.06863, -72.68712);
    var myOptions = {
        zoom: 13,
        center: myLatLng,
        mapTypeId: google.maps.MapTypeId.TERRAIN
    };

    map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);

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
    var polyOptions = {
        strokeColor: "#FF0000",
        strokeOpacity: 0.8,
        strokeWeight: 2,
        fillColor: "#FF0000",
        fillOpacity: 0.35
    };
    for (var j = 0; j <= obj.length-1; j++) {
        poly = new google.maps.Polygon(polyOptions);
        poly.setMap(map);

        var path = poly.getPath();
        for (i = 0; i <= obj[j].LstCoordenadas.length-1; i++) {
            var punto = new google.maps.LatLng(obj[j].LstCoordenadas[i].CoordenadaNorte, obj[j].LstCoordenadas[i].CoordenadaEste);
            path.push(punto);

            var marker = new google.maps.Marker({
                position: punto,
                title: '#' + path.getLength() + '(' + obj[j].LstCoordenadas[i].CoordenadaNorte + ' ' + obj[j].LstCoordenadas[i].CoordenadaEste + ')' + ',' + nombre + ',' + codigo,
                map: map,
                icon: image
            });
        }
    }
}