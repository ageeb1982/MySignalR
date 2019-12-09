$("#sendButton").disabled = true;


connection.start().then(function () {
   
    $("#sendButton").disabled = false;
  
}).catch(function (err) {
    return console.error("start---" + err.toString());
});

$("#sendButton").on("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("msgInput").value;
    SendMsg(user, message);
    //connection.invoke("Send", user, message).catch(function (err) {
    //    return console.error("sendButton----"+err.toString());
    //});
    event.preventDefault();
});


function SendMsg(user, message) {
    connection.invoke("Send", user, message).catch(function (err) {
        return console.error("sendButton----" + err.toString());
    });
}