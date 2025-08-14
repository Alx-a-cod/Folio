package com.example.artifact

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.activity.enableEdgeToEdge
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.tooling.preview.Preview
import com.example.artifact.ui.theme.ArtiFactTheme

class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        setContent {
            ArtiFactTheme {
                Scaffold(modifier = Modifier.fillMaxSize()) { innerPadding ->
                    Greeting(
                        name = "Android",
                        modifier = Modifier.padding(innerPadding)
                    )
                }
            }
        }
    }
}

@Composable
fun Greeting(name: String, modifier: Modifier = Modifier) {
    Text(
        text = "Hello $name!",
        modifier = modifier
    )
}

@Preview(showBackground = true)
@Composable
fun GreetingPreview() {
    ArtiFactTheme {
        Greeting("Android")
    }
}




//PER CERTIFICATO SHA-1:
//Run signingReport now
//Set JAVA_HOME for this session
//$env:JAVA_HOME = "C:\Program Files\Android\Android Studio\jbr"
//$env:Path = "$env:JAVA_HOME\bin;$env:Path"
//java -version
//You should see:
//openjdk version "21.0.6" ...
//Run Gradle
//./gradlew signingReport

//Task :app:signingReport
//Variant: debug
//Config: debug
//Store: C:\Users\HP\.android\debug.keystore
//Alias: AndroidDebugKey
//MD5: 67:35:91:BE:A7:77:54:95:D9:8F:AF:36:38:04:38:71
//SHA1: 50:D5:DD:28:C2:F3:E1:79:54:34:DB:1F:6D:64:B2:5E:64:6A:61:1C
//SHA-256: 62:1B:BF:87:5B:55:5B:6B:E2:07:D6:AB:9A:D3:72:5B:B9:EB:18:65:72:54:5D:23:1A:DF:8F:23:7D:37:5E:6A
//Valid until: mercoled├¼ 23 giugno 2055
//----------
//Variant: release
//Config: null
//Store: null
//Alias: null
//----------
//Variant: debugAndroidTest
//Config: debug
//Store: C:\Users\HP\.android\debug.keystore
//Alias: AndroidDebugKey
//MD5: 67:35:91:BE:A7:77:54:95:D9:8F:AF:36:38:04:38:71
//SHA1: 50:D5:DD:28:C2:F3:E1:79:54:34:DB:1F:6D:64:B2:5E:64:6A:61:1C
//SHA-256: 62:1B:BF:87:5B:55:5B:6B:E2:07:D6:AB:9A:D3:72:5B:B9:EB:18:65:72:54:5D:23:1A:DF:8F:23:7D:37:5E:6A
//Valid until: mercoled├¼ 23 giugno 2055


//Mi chiamo Alice e sto lavorando con un collega ad un progetto universitario per il corso di "Mobile Applications e Cloud Computing".
//
//Il progetto si chiama Artifact e consiste nello sviluppo di un’applicazione Android in Kotlin per il riconoscimento di opere d’arte tramite fotocamera e intelligenza artificiale.
//
//- Acquisizione di immagini tramite fotocamera (CameraX)
//
//- Riconoscimento delle opere d’arte con un modello AI (ML Kit + modello personalizzato TFLite)
//
//- Recupero di metadati storici e artistici tramite API REST e SPARQL di Wikidata
//
//- Login multi-utente con Firebase Authentication
//
//- Gestione preferiti e collezioni con sincronizzazione cloud (Firebase Firestore + Storage)
//
//- Architettura MVVM (Jetpack Compose, ViewModel, Repository, Room)
//
//- Condivisione e interazione sociale tra utenti
//
//Il progetto è stato già prototipato in Figma e la struttura tecnica è definita, ma ci troviamo in difficoltà perché:
//
//- Non abbiamo esperienza pratica in Kotlin
//- Non abbiamo mai lavorato con modelli AI o ML Kit
//- Stiamo cercando di orientarci tra Firebase, SPARQL e l’architettura moderna (MVVM, Jetpack Compose)
//
//Le chiedo quindi se fosse possibile avere un supporto iniziale o un confronto per aiutarci a impostare correttamente il progetto, soprattutto per orientarci su:
//
//-La struttura iniziale del codice (folder structure, moduli, inizializzazione Firebase, ecc.)
//-L’integrazione con il modello AI e la fotocamera
//-Le prime API call e la gestione asincrona dei dati con Retrofit e coroutines
//
//Idealmente, vorremmo iniziare le lezioni il prima possibile.
//
//Allego in fondo un breve schema tecnico del progetto.
//
//Sarei disponibile anche a presentare brevemente il progetto se utile.
//
//Grazie mille per la disponibilità,
//
//A presto, Alice
//
//-----------
//AllegatoTecnico:
//https://github.com/Alx-a-cod/Folio/tree/main/ArtiFact_
//
//Ciao Alice,
//grazie per avermi scritto e per aver descritto con così tanta chiarezza il vostro progetto Artifact.
//Mi sembra davvero interessante e ambizioso, soprattutto per la varietà di tecnologie coinvolte e l’approccio completo che state cercando di adottare (AI, SPARQL, Firebase, MVVM...).
//Ti scrivo ora perché la piattaforma Superprof non permette di rispondere ai messaggi senza prima accettare l'invito, motivo per cui ho dovuto accettare solo per poterti dare un riscontro.
//Purtroppo in questo periodo sono completamente assorbito da altri progetti già avviati e non riuscirò a prendere nuovi studenti prima di settembre. Mi dispiace molto, perché avrei partecipato volentieri al vostro percorso.
//Ho notato che la piattaforma ha oscurato l’Allegato Tecnico con l’avviso (informazione nascosta), quindi non ho potuto consultarlo. Detto ciò, vi lascio comunque qualche piccolo consiglio iniziale che spero possa esservi utile:
//• Partite dal setup minimo funzionante: create un'app Android vuota con Jetpack Compose e integrate subito CameraX, anche solo per mostrare un'anteprima video.
//• Aggiungete il modello TFLite localmente, con una logica semplice per testare il flusso di riconoscimento, anche prima dell'integrazione con Firebase.
//• Per l’architettura, mantenete la struttura MVVM pulita: ogni schermata dovrebbe avere il suo ViewModel e repository dedicato.
//• Separate bene la logica locale da quella di rete. Retrofit con coroutines e un livello di repository ben fatto vi salverà tempo più avanti.
//• Per SPARQL e Wikidata, potete iniziare usando strumenti come Wikidata Query Service per testare manualmente le query da integrare poi nel codice.
//Se a settembre sarete ancora in fase di sviluppo e vorrete un supporto più mirato, scrivetemi pure.
//Vi auguro buon lavoro,
//Luigi
//
//Questo é luigi, un'latro di quelli a cui ho scritto..Ha 20 anni di esperienza e fa il prof per diletto praticamente