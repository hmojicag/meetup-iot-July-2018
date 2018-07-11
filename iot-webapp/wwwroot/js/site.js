$(document).ready(function() {
    getMessages();
    setInterval(getMessages, 1000);
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