"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl(“/NotificationHub”).build();

connection.on(“sendToUser”, (heading, content) => {
    var heading = document.createElement(“h3”);
    heading.textContent = heading;

    var p = document.createElement(“p”);
    p.innerText = content;

    var div = document.createElement(“div”);
    div.appendChild(heading);
    div.appendChild(p);

    document.getElementById(“articleList”).appendChild(div);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});