﻿"use strict";

const userId = $('#UserId').val();

console.log(`userId: ${userId}`);

const connectionUser = new signalR.HubConnectionBuilder()
    .withUrl(`/NotificationUserHub?userId=${userId}`)
    .withAutomaticReconnect()
    .build();

connectionUser.on("sendToUser", (heading, content) => {
    console.log(`send to user especific`);
    // Atualiza o contador de mensagens
    const counterNotifier = document.querySelector(".ikrNoti_Counter").textContent;
    const newCounterNotifier = Number(counterNotifier) + 1;;

    document.querySelector(".ikrNoti_Counter").textContent = newCounterNotifier;
    document.querySelector(".notiCounterOnHead").textContent = newCounterNotifier;

    // Insere a nova notificação na lista
    const newItem = document.createElement("div");
    newItem.classList.add("krSingleNotiDiv", "ikrSingleNotiDivUnReadColor");

    const item = `<div class="ikrSingleNotiDiv ikrSingleNotiDivUnReadColor" notiid="undefined">
                    <h4 class="ikrNotiFromPropName">undefined</h4>
                    <h5 class="ikrNotificationTitle">${heading}</h5>
                    <div class="ikrNotificationBody">${content}</div>
                    <div class="ikrNofiCreatedDate">undefined</div>
                </div>`

    newItem.innerHTML = item;

    const notifications = document.querySelector(".ikrNotificationItems");

    notifications.insertBefore(newItem, notifications.children[0]);
});

connectionUser.start().then(function () {
    console.log('connection started');
})
.catch(error => {
    console.error(error.message);
});
