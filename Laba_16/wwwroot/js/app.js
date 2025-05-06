const out = document.getElementById("out");

document.getElementById("saveLocal").onclick = () => {
    const value = prompt("Значение для localStorage: ");
    localStorage.setItem("localStorageKey", value);
    out.textContent = "localStorage успешно сохранен"
};

document.getElementById("loadLocal").onclick = () => {
    out.textContent = "localStorage: " + localStorage.getItem("localStorageKey");
};

document.getElementById("saveSession").onclick = () => {
    const value = prompt("Значение для sessionStorage: ");
    sessionStorage.setItem("sessionStorageKey", value);
    out.textContent = "sessionStorage успешно сохранен";
};

document.getElementById("loadSession").onclick = () => {
    out.textContent = "sessionStorage: " + sessionStorage.getItem("sessionStorageKey");
};