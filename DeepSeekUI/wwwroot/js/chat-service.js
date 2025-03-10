async function sendMessage(request, errorElement, messageParent) {
    const controller = new AbortController();
    const signal = controller.signal;

    const timeoutId = setTimeout(() => controller.abort(), 100000);
    const sendMessageFetch = await fetch(`api/chat/message`, {
        method: 'POST',
        signal: signal,
        body: JSON.stringify(request),
        headers: {
            'Content-Type':'application/json'
        }
    });
    if (!sendMessageFetch.ok) {
        const textResponse = await sendMessageFetch.text();
        setErrorMessage(errorElement, textResponse);
        return;
    }
    clearTimeout(timeoutId);
    const textResponse = await sendMessageFetch.json();
    return textResponse;
}

async function createChat(request, errorElement) {
    const createChatFetch = await fetch('api/chat',{
        method: 'POST',
        body: JSON.stringify(request),
        headers: {
            'Content-Type': 'application/json'
        }
    });

    if (!createChatFetch.ok) {
        const textResponse = await createChatFetch.text();
        setErrorMessage(errorElement, textResponse);
        return;
    }

    return await createChatFetch.json();
}

async function getChat(chatId) {
    const getChatFetch = await fetch(`api/chat/${chatId}`, {
        headers: {
            'Accept': 'text/html'
        }
    });

    return await getChatFetch.text()
}

function setErrorMessage(errorElement, textContent) {
    if (errorElement.classList.contains("d-none")) {
        errorElement.classList.remove("d-none");
        errorElement.classList.add('d-block')
    }
    errorElement.textContent = textContent;
}