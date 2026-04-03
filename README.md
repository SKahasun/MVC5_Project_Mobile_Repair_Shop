# 📱 PhixPro Service Center

A **Master-Detail** mobile repair shop management system built with **ASP.NET MVC 5** and **Entity Framework**. Manage customers, their devices, and the services assigned to each repair job — all in one place.

---

## 🖥️ Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET MVC 5 |
| ORM | Entity Framework 6 (Code First / DB First) |
| Authentication | ASP.NET Identity |
| Frontend | Bootstrap, Font Awesome 6, Google Fonts (Syne + DM Sans) |
| Pagination | PagedList.Mvc |
| Database | SQL Server (LocalDB / Full) |
| AJAX | jQuery Unobtrusive Ajax |

---

## ✨ Features

### 👤 Customer Management (Master-Detail)
- **Create** a customer record with device info and optionally upload a device photo (JPG / PNG / WebP, max 5 MB)
- **Assign multiple services** to a single customer via a dynamic partial view (`_addNewService`)
- **Edit** customer details and re-assign services in one form
- **Delete** a customer along with all associated service entries (cascade)
- **Paginated listing** (8 records per page) with eager-loaded service data

### 🔧 Services Management
- Full **CRUD** for repair service types (e.g., Screen Replacement, Battery Change)
- Paginated service list (8 per page)
- Detail view per service
- Anti-forgery token protection on all POST actions

### 🔐 Authentication
- **ASP.NET Identity** powered login / register / logout
- Role-based access — most actions require `[Authorize]`; listing pages are `[AllowAnonymous]`

### 🎨 UI / UX
- Navy-blue branded design with a dot-grid background and smooth gradients
- Responsive layout via Bootstrap
- AJAX partial views for Create / Edit (returns `_success` or `_error` partial)
- FontAwesome icons throughout

---

## 📂 Project Structure

```
MasterDetails_MobileFixCenter/
│
├── Controllers/
│   ├── AccountController.cs      # Identity login / register / manage
│   ├── CustomerController.cs     # Master-Detail CRUD for customers
│   ├── ServicesController.cs     # CRUD for repair services
│   └── HomeController.cs         # Home, About, Contact
│
├── Models/
│   ├── Customer.cs               # Customer entity
│   ├── Service.cs                # Service entity
│   ├── ServiceEntry.cs           # Junction table (Customer ↔ Service)
│   ├── MobileServicingDBContext.cs
│   └── ViewModels/
│       └── CustomerVM.cs         # ViewModel with file upload + service list
│
├── Views/
│   ├── Customer/
│   │   ├── Index.cshtml          # Paginated customer list
│   │   ├── Create.cshtml         # AJAX create form
│   │   ├── Edit.cshtml           # AJAX edit form
│   │   ├── Delete.cshtml         # Delete confirmation
│   │   └── _addNewService.cshtml # Dynamic service dropdown (partial)
│   ├── Services/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   ├── Edit.cshtml
│   │   ├── Delete.cshtml
│   │   └── Details.cshtml
│   └── Shared/
│       ├── _Layout.cshtml
│       ├── _LoginPartial.cshtml
│       ├── _success.cshtml       # AJAX success partial
│       └── _error.cshtml         # AJAX error partial
│
├── Image/                        # Uploaded device images (runtime)
└── Content / Scripts             # Bootstrap, jQuery, PagedList CSS
```

---

## 🗄️ Data Model

```
Customer ──< ServiceEntry >── Service
```

| Table | Key Columns |
|---|---|
| `Customers` | `CustomerId`, `CustomerName`, `Phone`, `Address`, `EntryDate`, `DeviceName`, `DevicePicture`, `Problem`, `IsRegular` |
| `Services` | `ServiceId`, `ServiceName` |
| `ServiceEntries` | `EntryId`, `CustomerId` (FK), `ServiceId` (FK) |

---

## 🚀 Getting Started

### Prerequisites
- Visual Studio 2019 / 2022
- .NET Framework 4.7+
- SQL Server or LocalDB

### Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/SKahasun/MVC5_Project_Mobile_Repair_Shop
   cd MVC5_Project_Mobile_Repair_Shop
   ```

2. **Restore NuGet packages**
   Open the solution in Visual Studio → right-click the solution → *Restore NuGet Packages*

3. **Configure the connection string**
   In `Web.config`, update the `DefaultConnection` (and/or `MobileServicingDBContext`) connection string to point to your SQL Server instance:
   ```xml
   <connectionStrings>
     <add name="MobileServicingDBContext"
          connectionString="Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=MobileFixCenterDB;Integrated Security=True"
          providerName="System.Data.SqlClient" />
   </connectionStrings>
   ```

4. **Run EF Migrations** (Package Manager Console)
   ```powershell
   Update-Database
   ```

5. **Create the Image folder**
   Make sure an `Image/` folder exists at the project root (or it will be created automatically on first upload).

6. **Run the application**
   Press `F5` — the app will open in your default browser.

---

## 📸 Key Pages

| Route | Description |
|---|---|
| `/` | Home page |
| `/Customer` | Paginated customer list (public) |
| `/Customer/Create` | Add new customer + assign services |
| `/Customer/Edit/{id}` | Edit customer + reassign services |
| `/Customer/Delete/{id}` | Delete customer |
| `/Services` | Paginated services list (public) |
| `/Services/Create` | Add a new service type |
| `/Account/Login` | Login page |
| `/Account/Register` | Register page |

---

## 🔒 Authorization

- `Index` actions on `CustomerController` and `ServicesController` are publicly accessible (`[AllowAnonymous]`)
- All Create / Edit / Delete actions require a logged-in user (`[Authorize]` at the controller level)

---

## 📦 NuGet Dependencies

```
EntityFramework
Microsoft.AspNet.Identity.EntityFramework
Microsoft.AspNet.Mvc (5.x)
PagedList.Mvc
jQuery
Bootstrap
Microsoft.jQuery.Unobtrusive.Ajax
```

---

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/your-feature`
3. Commit your changes: `git commit -m "Add your feature"`
4. Push to the branch: `git push origin feature/your-feature`
5. Open a Pull Request

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).

---

> Built with ❤️ using ASP.NET MVC 5
