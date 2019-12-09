$("#grpSendButton").disabled = true;

connection.start().then(function () {
    $("#grpSendButton").disabled = false;
  

}).catch(function (err) {
    return console.error("start---" + err.toString());
});




$("#grpSendButton").on("click", function (event)
{
    var user = $("#grpuserInput").val();

    if (user === undefined || user==="") {

        user = $("#grpuserInput").text();
        }
    
var message = $("#grpmsgInput").val();
if (message === undefined || message==="") {
        message= $("#grpmsgInput").text();
    }
var grp = $("#grpInput").val();
if (grp === undefined || grp==="") {

    grp = $("#grpInput").text();
}
    SendMsgGroup(grp, user, message);
    //connection.invoke("SendGroup", grp, user, message).catch(function (err) {
    //    return console.error("SendGroup---" +err.toString());
    //});
    event.preventDefault();
});



$("#btnjoinGroup").on("click", function (event) {
    var grp =  $("#grpInput").val();
    JoinGroup(grp);
    //connection.invoke("JoinGroup", grp).catch(function (err) {
    //    return console.error("joinGroup---"+err.toString());
    //});
    event.preventDefault();
});



function SendMsgGroup(grp, user, message) {

    //var is_element_input = grp.prev().is("input"); //true or false
    //var is_element_input = grp.prev().is("input"); //true or false
    //var is_element_input = message.prev().is("input"); //true or false

    connection.invoke("SendGroup", grp, user, message).catch(function (err) {
        return console.error("SendGroup---" + err.toString());
    });
}



function JoinGroup(grp) {

    connection.invoke("JoinGroup", grp).catch(function (err) {
        return console.error("joinGroup---" + err.toString());
    });
}




