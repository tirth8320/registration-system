# 🚀 Registration Management System

A modern **ASP.NET Core MVC** based Registration Management System developed during my internship. The application provides secure user authentication, role-based authorization, document management, email verification, password recovery, and an admin dashboard for managing users.

> **Live Demo:** https://rregistration-2.runasp.net

---

# 📌 Features

## User Features
- User Registration
- Secure Login
- Logout
- Forgot Password via OTP
- Password Reset
- Profile Management
- Upload Multiple Documents
- CAPTCHA Verification
- Email Notifications
- Responsive UI

---

## Admin Features

- Dashboard
- User Management
- Role Management
- View User Details
- Edit User Information
- Delete Users
- View Uploaded Documents
- Error Log Management

---

# 🛠 Tech Stack

## Backend
- ASP.NET Core MVC (.NET 10)
- C#
- Entity Framework Core 10
- Dapper

## Database
- PostgreSQL
- Neon Cloud Database

## Frontend
- HTML5
- CSS3
- Bootstrap 5
- JavaScript
- jQuery
- Razor Views

## Authentication & Security
- BCrypt Password Hashing
- ASP.NET Core Session
- CAPTCHA Validation
- Role-Based Authorization

## Email Service
- Gmail SMTP

## PDF & Excel
- QuestPDF
- ClosedXML

## Deployment
- RunASP.NET
- GitHub

---

# 📂 Project Structure

```
registration/
│
├── Controllers/
│ ├── AccountController.cs
│ ├── AdminController.cs
│ ├── HomeController.cs
│
├── Models/
│ ├── UserMaster.cs
│ ├── RoleMaster.cs
│ ├── UserDocument.cs
│ ├── ErrorLog.cs
│
├── Views/
│ ├── Account/
│ ├── Admin/
│ ├── Home/
│ ├── Shared/
│
├── Services/
│
├── wwwroot/
│ ├── css/
│ ├── js/
│ ├── images/
│ └── uploads/
│
├── Data/
│ └── ApplicationDbContext.cs
│
└── Program.cs
```

---

# ⚙️ Database

The application uses **PostgreSQL** hosted on **Neon**.

Main Tables

- UserMaster
- RoleMaster
- UserDocuments
- AdditionalDocuments
- PasswordResetOtps
- ErrorLogs

---

# 🔐 Security Features

- BCrypt Password Encryption
- Session Authentication
- CAPTCHA Protection
- Role-Based Authorization
- Email OTP Verification
- Server-Side Validation
- Client-Side Validation

---

# 📧 Email Service

The application sends emails using Gmail SMTP.

Functions include

- Password Reset OTP
- Verification Emails
- Notifications

---

# 📁 Document Management

Users can upload multiple documents.

Supported Features

- Upload Files
- View Files
- Download Files
- Delete Files

---

# 📊 Admin Dashboard

Admin can

- Manage Users
- View Registrations
- View Documents
- Manage Roles
- View Error Logs

---

# 🌐 Deployment

Application Source Code

GitHub Repository

https://github.com/tirth8320/registration-system

Hosting

RunASP.NET

Database

Neon PostgreSQL Cloud

---

# 🚀 Installation

Clone the repository

```bash
git clone https://github.com/tirth8320/registration-system.git
```

Navigate to the project

```bash
cd registration-system
```

Restore packages

```bash
dotnet restore
```

Update database connection string inside

```
appsettings.json
```

Run the application

```bash
dotnet run
```

---

# 📸 Screenshots

- Login Page
- Registration Page
- Dashboard
- User Management
- Profile
- Document Upload

---

# 📚 Packages Used

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.Tools
- Npgsql
- Npgsql.EntityFrameworkCore.PostgreSQL
- Dapper
- BCrypt.Net-Next
- ClosedXML
- QuestPDF
- System.Drawing.Common

---

# 🎯 Learning Outcomes

During this project, I gained hands-on experience with

- ASP.NET Core MVC Architecture
- Entity Framework Core
- PostgreSQL Database
- Cloud Database (Neon)
- Authentication & Authorization
- Session Management
- Razor Views
- File Upload Handling
- Email Integration
- Secure Password Hashing
- Deployment to Cloud
- Git & GitHub Workflow
- Responsive Web Design

---

# 👨‍💻 Developed By

**Tirth Patel**

B.Tech Computer Science Student

Pandit Deendayal Energy University (PDEU)

GitHub:
https://github.com/tirth8320

LinkedIn:
https://www.linkedin.com/in/tirth-patel-17455b322/

Email:
tirthpatel8320@gmail.com

---

# ⭐ If you like this project

Please consider giving this repository a ⭐ on GitHub.
