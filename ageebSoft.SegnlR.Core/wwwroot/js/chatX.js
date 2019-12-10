 "use strict";

 
var user = getQueryStringByName("user");
var connection = new signalR.HubConnectionBuilder().withUrl("/Myhub?userName=" + user).build();
//$.connection.hub.qs = { userName: user };


//$.signalR.ajaxDefaults.headers = { userName: user };

//signalR.ajaxDefaults.headers = new Headers({
//    'Content-Type': "application/json",
//   // "Authorization": 'Bearer ' + accessToken  //accessToken contain bearer value.
//    'userName': user
//});
//$.signalR.
//$.signalR.ajaxDefaults.headers = { Authorization: "basic " + yourToken };

//Disable send button until connection is established
//document.getElementById("sendButton").disabled = true;
//  document.getElementById("grpSendButton").disabled = true;
var a = 0;

 $("#sendButton").disabled = true; 
//if ($("#grpSendButton").length > 0) {}
    $("#grpSendButton").disabled = true; 


connection.on("Rec", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("msgList").appendChild(li);
});
//    .catch(function (err) {
//    return console.error("Rec---" + err.toString());
//});
connection.on("Recgrp", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("grpMsgList").appendChild(li);
});
//    .catch(function (err) {
//    return console.error("RecGrp---" + err.toString());
//});

//connection.on("RecOnline", function (user, message) {
connection.on("RecOnline", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " :  " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("OnlineList").appendChild(li);
});
//    .catch(function (err) {
//    return console.error("RecOnline---" + err.toString());
//});

connection.start().then(function ()
{
   // if ($("#sendButton").length > 0) {
        $("#sendButton").disabled = false;
   // }
    //if ($("#grpSendButton").length > 0) {
        $("#grpSendButton").disabled = false;
   // }
   
    
   // var connectionUrl = connection.transport.webSocket.url;
    console.log("Connection Information:" + connection);

}).catch(function (err) {
    return console.error("start---" + err.toString());
});

 
$("#sendButton").on("click", function (event) {
    //var user = document.getElementById("userInput").value;
    var message = document.getElementById("msgInput").value;
    SendMsg(message)
    // SendMsg(user, message);
    //connection.invoke("Send", user, message).catch(function (err) {
    //    return console.error("sendButton----"+err.toString());
    //});
    event.preventDefault();
});
 
 
    $("#grpSendButton").on("click", function (event) {
        //var user = document.getElementById("grpuserInput").value;
        var message = document.getElementById("grpmsgInput").value;
        var grp = document.getElementById("grpInput").value;
        SendMsgGroup(grp, message)
        //SendMsgGroup(grp, user, message);
        //connection.invoke("SendGroup", grp, user, message).catch(function (err) {
        //    return console.error("SendGroup---" +err.toString());
        //});
        event.preventDefault();
    });
 
 
    $("#btnjoinGroup").on("click", function (event) {
        var grp = $("#grpInput").val();
        JoinGroup(grp);
        //connection.invoke("JoinGroup", grp).catch(function (err) {
        //    return console.error("joinGroup---"+err.toString());
        //});
        event.preventDefault();
    });
 



//function SendMsgGroup(grp, user, message) {
function SendMsgGroup(grp, message) {

    //var is_element_input = grp.prev().is("input"); //true or false
    //var is_element_input = grp.prev().is("input"); //true or false
    //var is_element_input = message.prev().is("input"); //true or false

    //connection.invoke("SendGroup", grp, user, message).catch(function (err) {
    connection.invoke("SendGroup", grp, message).catch(function (err) {
        return console.error("SendGroup---" + err.toString());
    });
}
//function SendMsg(user, message) {
function SendMsg(message) {
    connection.invoke("Send", message)
        .catch(function (err) {
            alert(err.toString());
        return console.error("sendButton----" + err.toString());
    });
}

function JoinGroup(grp) {

    connection.invoke("JoinGroup", grp).catch(function (err) {
        alert(err.toString());
        return console.error("joinGroup---" + err.toString());
    });
}



///getQueryStringFormUrlPage
function getQueryStringByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

///GetConId
/*connection.invoke('getConnectionId')
    .then(function (connectionId) {
        // Send the connectionId to controller
    });*/