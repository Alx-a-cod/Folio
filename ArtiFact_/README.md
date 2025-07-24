#### 𓂃🖌 Artifact

Artifact is a Kotlin-based Android application developed as a collaborative two-person project for a Master's degree in Computer Science.  
The app leverages artificial intelligence and cloud services to deliver a camera-driven experience for the recognition, archival, and exploration of famous artworks.


##### 🎯 Project Overview

Artifact allows users to take photos of artworks using their mobile device. A trained AI model processes the image and retrieves detailed metadata such as the artwork’s _name_, _artist_, _period_, and _historical_ _context_ using the **Wikidata API**.  
Users can save, manage, and share their discoveries across a connected social layer.

---

##### 🧩 Core Features

- 📷 **AI-Powered Artwork Recognition**  
  Users can take pictures of artworks. A custom-trained ML model powered by **ML Kit** classifies the image and retrieves semantic information using **Wikidata's REST** and **SPARQL APIs**.  

- 👥 **Multi-User System**  
  Users can register, log in, manage their profile, and connect with other users to view and share saved artworks.  

- ⭐ **Favorites & Collections (CRUD)**  
  Each user can add, remove, and manage their favorite artworks. These collections are synced in the cloud and can be browsed by connected friends.  

- 🔐 **Firebase Authentication**  
  The app uses Firebase Authentication for secure login and multi-user management.  

- ☁️ **Firebase Integration**  
  Firebase handles real-time database sync, cloud storage, user management, and hosting of user-generated data.  

- 🧠 **AI + External APIs**  
  Combines a custom-trained image classification model with real-time queries to the Wikidata API for rich historical and contextual information.  

- 📱 **Modern UI / UX**  
  The application features a responsive and intuitive UI, fully prototyped in Figma and implemented using **Jetpack Compose**.  

- 🔄 **Concurrent Operations**  
  Image processing, API queries, and UI updates are managed asynchronously with **Kotlin coroutines** to ensure a responsive user experience.  

- 📡 **Sensor & Camera Usage**  
  Integrates Android’s camera framework (CameraX) and utilizes device sensors to improve the user experience when capturing images.  

- 💾 **Persistent Local + Cloud Storage**  
  User data and preferences are stored using Room for offline access and Firebase Cloud for real-time sync.  
  
##### 🧱 Architecture
  
The application follows the **MVVM architecture** with a clean separation of concerns.  
  
UI (Jetpack Compose)
│
├── ViewModel (UI logic & state)
│
├── Repository (data coordination)
│
└── Data Sources:
├── Remote: Firebase, Wikidata API
└── Local: Room DB
  

##### 🛠 Technologies & Stack

| Component            | Library / Tool                           |
|----------------------|------------------------------------------|
| **Language**         | Kotlin                                   |
| **IDE**              | Android Studio                           |
| **UI**               | Jetpack Compose                          |
| **Camera**           | CameraX                                  |
| **AI (Recognition)** | ML Kit + Custom TensorFlow Lite Model    |
| **HTTP Client**      | Retrofit + Coroutines                    |
| **JSON Parsing**     | Moshi                                    |
| **Images**           | Coil                                     |
| **API**              | Wikidata REST + SPARQL                   |
| **Architecture**     | MVVM (ViewModel, Repository, UI)         |
| **Authentication**   | Firebase Auth                            |
| **Cloud Sync**       | Firebase Firestore + Storage             |
| **Local Storage**    | Room                                     |

- **Android Studio** for development
- **Figma** for UI design prototyping

##### 📌 Progress & Status

- ✅ Figma static UI prototype completed
- 🏗️ Initial code implementation in progress
- 🔄 ML model integration and testing upcoming
- 📡 API bridge with Wikidata in early development
- 🔐 Firebase Auth and DB setup ongoing


##### 📚 Project Requirements Coverage

| Requirement ID | Description                                          | Status |
|----------------|------------------------------------------------------|--------|
| 1              | Cloud services (Firebase + Wikidata API)             | ✅     |
| 2              | Multi-user functionality                             | ✅     |
| 3              | Graphical UI (Jetpack Compose + Figma)               | ✅     |
| 4              | Sensor integration                                   | ✅     |
| 5              | Camera features (CameraX)                            | ✅     |
| 6              | Concurrency (Kotlin coroutines)                      | ✅     |
| 7              | Authentication (Firebase Auth)                       | ✅     |
| 8              | Cloud provider integration (Firebase)                | ✅     |
| 9              | CRUD interface (users & favorite artworks)           | ✅     |
| 10             | Data persistence (Room + Firebase Cloud)             | ✅     |


##### 🧠 Future Enhancements

- Add collaborative features: shared collections, comments
- Recommendation engine for related artworks
- In-app chat or discussion around specific artworks (maybe? (ᵕ—ᴗ—))
- Offline recognition via embedded model fallback
- Power BI dashboard for visualizing user engagement (big stretch (·•᷄ࡇ•᷅ ), but still worth the try)

##### 🔖 License

This project is developed as part of an academic requirement. Licensing details TBD, but Apache 2.0 just to be on the safer side.
  
  
_Developed in collaboration with [**Gyanluca**]   ╰┈➤ˎˊ˗ (https://github.com/gyanluca)


[![GitHub - Alx-a-cod, Co-Author](https://img.shields.io/badge/author-Alx--a--cod-F2928D?logo=github)](https://github.com/Alx-a-cod)
[![[GitHub - Gyanluca, Co-Author](https://img.shields.io/badge/author-Gyanluca-4a74c2?logo=github)](https://github.com/gyanluca)
