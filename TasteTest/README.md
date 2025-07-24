### TasteTest ── ⟢ ・⸝⸝

Client-side, modular and testable management system built with **.NET 8 (ASP.NET Core Razor Pages)** using the **MVC architecture**.   
The frontend is styled with the **SIRIO UI Library**, and interactivity is handled via **JavaScript** and **AJAX**.

###### Development is done in **Visual Studio 2022**.

#### 📍 Overview ──

This application offers four primary views:

⤷  **Client Management (CRUD)** — Create, read, update, and delete client anagraphics, with server-side pagination.  
⤷  **Item Management (CRUD)** — Manage inventory items.  
⤷  **Virtual Cart** — Users can build and submit a shopping cart.  
⤷  **Transactional Order Submission** — Orders are saved across multiple SQL tables using transaction logic to ensure consistency.  

#### 📍 Technologies ──

- ASP.NET Core Razor Pages (MVC Pattern)  
- C#, JavaScript, AJAX  
- SIRIO UI Library  
- SQL Server (Transactional operations)  

#### 📍Roadmap (Future Plans)  ──

⤷ **Paginated views**:  
Paginated views to be implemented for clients, items, and user order history, ensuring performance and scalability across large datasets.

⤷ **Separated Login System**:  
 Distinct authentication for regular users and operators. Users can browse items and submit orders, while operators can access, modify and review the items' list and the entire user order history.

⤷ **Server-side validation**:  
 By implementing a logic differentiating between regular 'users' and 'operators'. When the 'user' submits an order or changes quantities/prices, the server must re-validate all values against trusted data (e.g., product  
 prices from the database).    
 If a submitted price or quantity doesn’t match expected ranges or data, reject the request and flag the user.

⤷ **Power BI Dashboard Integration**:  
  → **Order Trends Dashboard**: Visualize purchasing behavior and product popularity over time.  
  → **Access Analytics Dashboard**: Track who logs in, when, and for how long.  
  → Secure dashboard access based on user roles(to be seen?)  

⤷ **Test Coverage Model**:  
 Integration of automated tests to ensure code quality, maintainability, and compliance with standards.  

#### 📍 Status  ──
Currently under active development — core functionalities are in place and evolving through continuous iteration.  

#### 🔖  License ──   
Apache 2.0 License
