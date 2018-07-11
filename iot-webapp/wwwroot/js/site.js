$(document).ready(function() {
    getMessages();
    setInterval(getMessages, 1000);
    setInterval(getTemperature, 330);
});

function getMessages() {
    $.ajax({
        type: "GET",
        url: "/api/json-msg",
        dataType: "text"
    })
    .done(function(response) {
        $("#divMessages").html(response);
    })
    .fail(function( $xhr ) {
        
    });//END Ajax call
}

function getTemperature() {
    $.ajax({
        type: "GET",
        url: "/api/temp",
        dataType: "json"
    })
        .done(function(response) {
            $("#h1Temperature").text("Room temperature in Digital OnUs mezzanine: " + response + "ºC");
        })
        .fail(function( $xhr ) {

        });//END Ajax call
}