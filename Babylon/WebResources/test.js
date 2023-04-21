
var osmLayer = new ol.layer.Tile({
    source: new ol.source.OSM()
});

var map = new ol.Map({
    layers: [osmLayer],
    target: "map"
});

var view = new ol.View({     
    zoom: 3,
    maxZoom: 18,
    minZoom: 3,
    center: ol.proj.fromLonLat([0.0045, 50.4095])
});
map.setView(view);
