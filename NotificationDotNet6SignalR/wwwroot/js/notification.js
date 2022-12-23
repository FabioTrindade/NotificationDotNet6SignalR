"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationUserHub").build();

connection.on("sendToUser", (heading, content) => {
    //var heading = document.createElement(“h3”);
    //heading.textContent = heading;

    //var p = document.createElement(“p”);
    //p.innerText = content;

    //var div = document.createElement(“div”);
    //div.appendChild(heading);
    //div.appendChild(p);

    let tbody = `
                    <tr>
                        <td>${heading}</td>
                        <td>${content}</td>
                    <tr>`;

    console.log(tbody);

    document.getElementById("tblNotification").find('tbody').appendChild(tbody);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});