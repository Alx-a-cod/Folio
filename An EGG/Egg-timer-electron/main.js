// JavaScript source code
const { app, BrowserWindow } = require('electron');
const path = require('path');

let mainWindow;

// Funzione per creare la finestra dell'app
function createWindow() {   // crea una finestra di Electron con dimensioni 800x600 px e carica un file HTML ''index.html'' o best equivalent, il nome è libero
    const win = new BrowserWindow({
        width: 650,
        height: 700,
        resizable: false,
        frame: false, // No barra superiore (così possiamo creare bottoni personalizzati)
        webPreferences: {
            nodeIntegration: true,
            contextIsolation: false
        },
    });

    // Carica il file HTML nella finestra
    win.loadFile('index.html');

    // Listener per close e minimize
    ipcMain.on('close-app', () => {
        win.close();
    });

    // Per aprire DevTools: win.webContents.openDevTools();

    //// Quando la finestra viene chiusa, imposta mainWindow a null
    //mainWindow.on('closed', () => {
    //    mainWindow = null;
    //});
}

// Questo codice verrà eseguito quando Electron ha finito di inizializzare
app.whenReady().then(() => { //Avvia l'app quando Electron è pronto e crea la finestra principale
    createWindow();

    app.on('window-all-closed', () => {
        if (process.platform !== 'darwin') {
            app.quit();
        }
    });
});

// Esci quando tutte le finestre sono chiuse
app.on('window-all-closed', () => {
    if (process.platform !== 'darwin') {
        app.quit();
    }
});

