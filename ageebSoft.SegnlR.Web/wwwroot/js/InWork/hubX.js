"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/Myhub").build();


connection.on("Rec", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("msgList").appendChild(li);
});

connection.on("Recgrp", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("grpMsgList").appendChild(li);
});
 
connection.on("RecOnline", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " :  " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("OnlineList").appendChild(li);
});
