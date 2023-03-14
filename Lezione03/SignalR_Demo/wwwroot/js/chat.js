"use strict";


document.getElementById("loginButton").addEventListener("click", function (event) {
    Login();
});

var loginToken;

function Login() {
    //Disable send button until connection is established
    document.getElementById("sendButton").disabled = true;
    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;

    GetAccessToken(username, password);
}

function GetAccessToken(username, password) {
    var reqdata = {
        Email: username + "@gmail.com",
        Username: username,
        Password: password
    }

    var stringReqdata = JSON.stringify(reqdata);

    var xhr = new XMLHttpRequest();
    xhr.open("POST", "https://localhost:7166/api/auth/gettoken", true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(stringReqdata);

    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            var json_data = xhr.responseText;
            loginToken = json_data;
            Connect2Chat(loginToken);
        }
    };
}

function Connect2Chat(token) {

    var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub", { accessTokenFactory: () => token }).build();

    connection.on("ReceiveMessage", function (user, message) {
        var li = document.createElement("li");
        document.getElementById("messagesList").appendChild(li);
        li.textContent = `${user} says ${message}`;
    });

    connection.on("NotificationMessage", function (message) {
        var li = document.createElement("li");
        document.getElementById("messagesList").appendChild(li);
        li.textContent = `${message}`;
    });

    connection.start().then(function () {
        document.getElementById("sendButton").disabled = false;
        document.getElementById("messageDiv").hidden = false;
        document.getElementById("loginDiv").hidden = true;
        document.getElementById("userInput").value = document.getElementById("username").value;

        connection.invoke("AddToGroup", "Admin").catch(function (err) {
            return console.error(err.toString());
        });

    }).catch(function (err) {
        return console.error(err.toString());
    });



    document.getElementById("sendButton").addEventListener("click", function (event) {
        var user = document.getElementById("userInput").value;
        var message = document.getElementById("messageInput").value;
        connection.invoke("SendMessage", user, message).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    });


}



