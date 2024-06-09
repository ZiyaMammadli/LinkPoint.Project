let token = document.querySelector("#tokenidd-d");
Token = token.value;
let sendBtns = document.querySelectorAll(".sendBtn");
let connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7255/chat", { accessTokenFactory: () => Token })
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.start().catch(err => console.error(err.toString()));

sendBtns.forEach(btn => {
    btn.addEventListener("click", function (e) {
        e.preventDefault();
        let messageInput = btn.closest('.send-message').querySelector('.sender-chat-message');
        let userIdInput = btn.closest('.send-message').querySelector('.sender-user-id');
        let message = messageInput.value.trim();
        let userId = userIdInput.value;
        let activePane = document.querySelector('.tab-pane.active');
        let conversationIdStr = activePane ? activePane.id.split('-')[1] : null;
        let conversationId = parseInt(conversationIdStr);

        if (message) {
            sendMessage(conversationId, userId, message);
            messageInput.value = "";
        }
    });
});

connection.on("ReceiveMessage", (conversationId, userName, userProfileImage, message, alignment, senderClass,UserId) => {
    let chatContainer = document.getElementById(`chat-${conversationId}`);
    let userId = document.querySelector('.sender-user-id').value;

    if (UserId === userId) {
        alignment = "right";
        senderClass = "right";
    } else {
        alignment = "left";
        senderClass = "left";
    }
    if (chatContainer) {        
        const li = document.createElement("li");
        li.classList.add(senderClass);
        li.innerHTML = `<img src="${userProfileImage}" alt="" class="profile-photo-sm pull-${alignment}" />
                        <div class="chat-item">
                            <div class="chat-item-header">
                                <h5>${userName}</h5>
                                <small class="text-muted">${new Date().toLocaleString()}</small>
                            </div>
                            <p>${message}</p>
                        </div>`;
        chatContainer.appendChild(li);
        chatContainer.scrollTop = chatContainer.scrollHeight;
    }
});

connection.on("UserConnected", (userId, userName, userProfileImage,onlineuserscount) => {
    let div = document.querySelector(".title");
    div.textContent = `Online:${onlineuserscount}`
    let onlineUsersList = document.querySelector(".online-users");
    const li = document.createElement("li");
    li.id = `user-${userId}`;
    li.innerHTML = `<a href="newsfeed-messages.html" title="${userName}">
                        <img src="${userProfileImage}" alt="user" class="img-responsive profile-photo" />
                        <span class="online-dot"></span>
                    </a>`;
    onlineUsersList.appendChild(li);
});

connection.on("UserDisconnected", (userId) => {
    let userElement = document.getElementById(`user-${userId}`);
    if (userElement) {
        userElement.remove();
    }
});
function sendMessage(conversationId, userId, message) {
    connection.invoke("SendMessageAsync", conversationId, userId, message).catch(err => console.error(err.toString()));
}
