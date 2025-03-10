
console.log("chat-vc-loaded");

const chatId = "@(Model is null? null: Model.Chat.ChatId)";
const userId = "@(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value)";

console.log(chatId);

const contentChatElement = document.getElementById("content-chat");
const formMessageElement = document.getElementById("form-message");
const errorsElement = document.getElementById("errors");
const parentElement = document.getElementById("content-messages");
const textAreaInput = document.getElementById("text-input");
const buttonSubmit = document.getElementById("button-submit");
const spinnerElement = document.getElementById("spinner");

let loaded = true;

buttonSubmit.classList.add('disabled');
errorsElement.classList.add("d-none");

textAreaInput.addEventListener("keydown", function (event) {
    if (event.target.value) {
        if (event.key === "Enter" && !event.shiftKey) {
            event.preventDefault();
            formMessageElement.dispatchEvent(new Event("submit", { cancelable: true, bubbles: true }));
        }
    }
    else if (event.key === 'Enter') {
        event.preventDefault();
    }

});

textAreaInput.addEventListener('input', function (event) {
    if (textAreaInput.value.length > 0) {
        buttonSubmit.classList.remove("disabled");
    }
    else {
        buttonSubmit.classList.add("disabled");
        formMessageElement.removeEventListener('submit', function (event) { });
    }
})

formMessageElement.addEventListener('submit', async function (event) {
    event.preventDefault();
    const text = textAreaInput.value;
    const requestModel = {
        message: text,
        chatId: (chatId != '' && chatId ? chatId : null),
        userId: userId
    }
    setButtonOff();
    addMessageChild(text, new Date().toLocaleString(), true)
    const response = await sendMessage(requestModel, errorsElement, parentElement);
    addMessageChild(response.value.message, new Date(response.value.createdTime).toLocaleString(), false);
    document.getElementById("text-input").value = '';
    setButtonOn()
})

function setButtonOff() {
    loaded = false;
    buttonSubmit.classList.add("disabled");
    spinnerElement.classList.remove("d-none");
    textAreaInput.setAttribute("disabled", "true")
}

function setButtonOn() {
    loaded = true;
    buttonSubmit.classList.remove("disabled");
    spinnerElement.classList.add("d-none")
    textAreaInput.removeAttribute("disabled");
}

function addMessageChild(text, date, isQuestion) {
    let className = 'p-2 text-white rounded shadow ';
    if (isQuestion) {
        className += 'bg-primary align-self-end';
    }
    else {
        className += 'bg-secondary align-self-start';
    }
    const divMessageChild = document.createElement('div');
    divMessageChild.className = className
    divMessageChild.textContent = text + ' ' + date;
    parentElement.appendChild(divMessageChild);
}

// const formTypeChat = document.getElementById("type-chat-form");
// formTypeChat["type-chat"].value = 'temp';
// const tempChat = document.getElementById("temp-chat");
// const saveChat = document.getElementById("save-chat");


// document.querySelectorAll('input[name="type-chat"]').forEach(function (input) {
//     input.addEventListener('change', setClassInput);
// })

//setClassInput();
// function setClassInput() {
//     const actualValueForm = formTypeChat["type-chat"].value;
//     console.log(actualValueForm);
//     const isTemp = actualValueForm === 'temp';
//     changeClass(tempChat, isTemp, 'bg-primary', 'bg-secondary', activeChat, inactiveChat);
//     changeClass(saveChat, !isTemp, 'bg-primary', 'bg-secondary');
// }

// function inactiveChat() {
//     setButtonOff()
//     contentChatElement.style.opacity = 0.66;
//     contentChatElement.style.filter = "blur(1px)";
// }

// function activeChat() {
//     setButtonOn()
//     contentChatElement.style.filter = "blur(0px)";
//     contentChatElement.style.opacity = 1.0;
// }s