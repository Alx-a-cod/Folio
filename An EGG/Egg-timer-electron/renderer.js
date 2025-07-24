const { ipcRenderer } = require('electron');
const path = require('path');

window.addEventListener('DOMContentLoaded', () => {
    const eggImage = document.getElementById('Egg');
    const closeButton = document.getElementById('Close_Button');
    const minimizeButton = document.getElementById('collapse_button');
    const startButton = document.getElementById('start-button');

    // Imposta immagini
    eggImage.src = path.join('assets', 'egg.svg');
    closeButton.src = path.join('assets', 'close.svg');
    minimizeButton.src = path.join('assets', 'minimize.svg');

    // Azioni pulsanti
    closeButton.addEventListener('click', () => {
        ipcRenderer.send('close-app');
    });

    minimizeButton.addEventListener('click', () => {
        ipcRenderer.send('minimize-app');
    });

    startButton.addEventListener('click', () => {
        window.location.href = 'menu.html';
    });
});

//var path = carica un immagine da path  