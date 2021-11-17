/// <reference path="jquery-1.8.2-vsdoc.js" />


var markers = new fMarkers();
var polylines = new fPolylines();
var polygons = new fPolygons();
var map;
var markersArray = [];
var LatitudFind;
var LongitudFind;
var markerFind;

function fGetGoogleObject(result, userContext) {

        var myLatLng = new google.maps.LatLng(Latitud, Longitud);
        var mapOptions = {
            zoom: Zoom,
            center: myLatLng,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        map = new google.maps.Map(document.getElementById('GoogleMap_Div'), mapOptions);
        for (i = 0; i < result.Points.length; i++) {
            var myIcon_google;

            var myPoint = new google.maps.LatLng(result.Points[i].Latitude, result.Points[i].Longitude);

            myIcon_google = null;
            var position = new google.maps.LatLng(myPoint.lat(), myPoint.lng());
            var marker = new google.maps.Marker({ position: position, title: result.Points[i].ToolTip });
            marker.setMap(map);

        }

        polylines = new fPolylines();
        for (i = 0; i < result.Polylines.length; i++) {
            var polypoints = new Array();
            var j;
            for (j = 0; j < result.Polylines[i].Points.length; j++) {

                polypoints[j] = new google.maps.LatLng(result.Polylines[i].Points[j].Latitude, result.Polylines[i].Points[j].Longitude);
            }
            var flightPath = new google.maps.Polyline({
                path: polypoints,
                strokeColor: result.Polylines[i].ColorCode,
                strokeOpacity: 1.0,
                strokeWeight: 2
            });
            flightPath.setMap(map);
        }



        polygons = new fPolygons();
        for (i = 0; i < result.Polygons.length; i++) {
            var polypoints = new Array();
            var j;
            for (j = 0; j < result.Polygons[i].Points.length; j++) {
                polypoints[j] = new google.maps.LatLng(result.Polygons[i].Points[j].Latitude, result.Polygons[i].Points[j].Longitude);
            }
            bermudaTriangle = new google.maps.Polygon({
                paths: polypoints,
                strokeColor: result.Polygons[i].StrokeColor,
                strokeOpacity: result.Polygons[i].StrokeOpacity,
                strokeWeight: 2,
                fillColor: result.Polygons[i].FillColor,
                fillOpacity: result.Polygons[i].FillOpacity
            });

            bermudaTriangle.setMap(map);
        }
}

function DrawGoogleMap(Lat, Lng, zoom) {
    Latitud = Lat;
    Longitud = Lng;
    Zoom = zoom;
    GService.GetGoogleObject(fGetGoogleObject);
}
function fMarkers() {
    this.markers = new Array();
    this.getLength = function() { return this.markers.length; };
    this.pushValue = function(v) { this.markers.push(v); }
    this.getValue = function(i) { return this.markers[i]; }
    this.getLastValue = function() { return this.markers[this.markers.length - 1]; }
    this.getValueById = function(ID) {
        var i;
        for (i = 0; i < this.markers.length; i++) {
            if (this.markers[i].value == ID) {
                // alert('marker found : '+this.markers[i].value);
                return this.markers[i];
            }
        }
        return null;
    }
    this.removeValueById = function(ID) {
        var i;
        for (i = 0; i < this.markers.length; i++) {
            if (this.markers[i].value == ID) {
                // alert('marker found : '+this.markers[i].value);
                this.markers.splice(i, 1);
                //alert('changed marker removed');

            }
        }
        return null;
    }
}
function fPolylines() {
    this.polylines = new Array();
    this.polylinesID = new Array();
    this.getLength = function() { return this.polylines.length; };
    this.pushValue = function(v, ID) { this.polylines.push(v); this.polylinesID.push(ID); }
    this.getValue = function(i) { return this.polylines[i]; }
    this.getLastValue = function() { return this.polylines[this.polylines.length - 1]; }
    this.getValueById = function(ID) {
        var i;
        for (i = 0; i < this.polylinesID.length; i++) {
            if (this.polylinesID[i] == ID) {
                // alert('polyline found : '+this.polylines[i].value);
                return this.polylines[i];
            }
        }
        return null;
    }
    this.removeValueById = function(ID) {
        var i;
        for (i = 0; i < this.polylinesID.length; i++) {
            if (this.polylinesID[i] == ID) {
                this.polylines.splice(i, 1);
                this.polylinesID.splice(i, 1);
            }
        }
        return null;
    }
}

function fPolygons() {
    this.polygons = new Array();
    this.polygonsID = new Array();
    this.getLength = function() { return this.polygons.length; };
    this.pushValue = function(v, ID) { this.polygons.push(v); this.polygonsID.push(ID); }
    this.getValue = function(i) { return this.polygons[i]; }
    this.getLastValue = function() { return this.polygons[this.polygons.length - 1]; }
    this.getValueById = function(ID) {
        var i;
        for (i = 0; i < this.polygonsID.length; i++) {
            if (this.polygonsID[i] == ID) {
                return this.polygons[i];
            }
        }
        return null;
    }
    this.removeValueById = function(ID) {
        var i;
        for (i = 0; i < this.polygonsID.length; i++) {
            if (this.polygonsID[i] == ID) {
                this.polygons.splice(i, 1);
                this.polygonsID.splice(i, 1);
            }
        }
        return null;
    }
}

function agregarPunto(Lat, Lng) {

    if (Lat != null && Lng != null) {
        var myLatlng = new google.maps.LatLng(Lat, Lng);
        markerFind = new google.maps.Marker({ position: myLatlng, map: map, title: "Punto Verificación",          
            draggable: true,
            animation: google.maps.Animation.BOUNCE
        });
        markersArray.push(markerFind);
        showOverlays();
    }
}

function addMarker(location) {
    marker = new google.maps.Marker({ position: location, map: map });
    markersArray.push(marker);
} // Removes the overlays from the map, but keeps them in the array
function clearOverlays() {
    if (markersArray) { for (i in markersArray) { markersArray[i].setMap(null); } } 
}
// Shows any overlays currently in the array
function showOverlays() {
    if (markersArray) { for (i in markersArray) { markersArray[i].setMap(map); } }
} // Deletes all markers in the array by removing references to them
function deleteOverlays() {
    if (markersArray) {
        for (i in markersArray) {
            markersArray[i].setMap(null);
        } markersArray.length = 0;
    }
}