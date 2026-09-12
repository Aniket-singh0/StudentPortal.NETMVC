# StudentAccountmvc 

# StudentAccountmvc

A Student Account Management System built using **ASP.NET Core MVC, C#, Entity Framework Core, SQL Server and Bootstrap 5**.

The application provides a simple student portal where students can register, log in and manage/view their academic information from a dashboard.

## Features

* Student Registration
* Student Login & Logout
* Session-based Authentication
* Student Dashboard
* Student Profile Management
* Course Management
* Assignment Management
* Result Management
* Attendance Management
* Fees Management
* Change Password
* Edit and Delete Records
* Print Reports
* SQL Server Database Integration
* Entity Framework Core
* Bootstrap 5 Responsive UI

## Technologies Used

* C#
* ASP.NET Core MVC
* Entity Framework Core
* SQL Server
* HTML5
* CSS3
* Bootstrap 5.3.3
* Razor Views
* LINQ
* Visual Studio

## Project Structure

```text
StudentAccountmvc
│
├── Controllers
│   ├── AccountController.cs
│   ├── DashboardController.cs
│   ├── ProfileController.cs
│   ├── CourseController.cs
│   ├── AssignmentController.cs
│   ├── ResultController.cs
│   ├── AttendanceController.cs
│   ├── FeesController.cs
│   └── SettingsController.cs
│
├── Data
│   └── ApplicationDbContext.cs
│
├── Filters
│   └── StudentLoginFilter.cs
│
├── Models
│   ├── Student.cs
│   ├── Course.cs
│   ├── Assignment.cs
│   ├── Result.cs
│   ├── Attendance.cs
│   └── Fees.cs
│
├── Views
│   ├── Account
│   ├── Dashboard
│   ├── Profile
│   ├── Course
│   ├── Assignment
│   ├── Result
│   ├── Attendance
│   ├── Fees
│   └── Settings
│
├── wwwroot
│   ├── css
│   ├── js
│   └── lib
│
├── appsettings.json
├── Program.cs
└── StudentAccountmvc.csproj
```

## Database

The project uses **Microsoft SQL Server** with **Entity Framework Core**.

Main database tables:

```text
Students
Courses
Assignments
Results
Attendance
Fees
```

## Database Configuration

Update the SQL Server connection string in `appsettings.json` according to your local SQL Server configuration.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=StudentAccountmvc;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

> Do not upload passwords or other sensitive database credentials to a public GitHub repository.

## Entity Framework Core

Create and update the database using EF Core migrations.

```powershell
Add-Migration InitialCreate
Update-Database
```

For later changes:

```powershell
Add-Migration MigrationName
Update-Database
```

## How to Run

### 1. Clone the repository

```bash
git clone https://github.com/Aniket-singh0/StudentAccountmvc.git
```

### 2. Open the project

Open:

```text
StudentAccountmvc.sln
```

in Visual Studio.

### 3. Configure SQL Server

Update the connection string in:

```text
appsettings.json
```

### 4. Restore dependencies

```bash
dotnet restore
```

### 5. Build the project

```bash
dotnet build
```

### 6. Run the application

```bash
dotnet run
```

Or press:

```text
F5
```

in Visual Studio.

## Student Portal Modules

### Dashboard

The dashboard provides quick access to student information and portal modules.

### Profile

Students can view and update their personal information.

### Courses

Students can manage and view their course information.

### Assignments

Students can add, edit, delete and view assignments.

### Results

Students can manage examination results and calculate percentage.

### Attendance

Students can manage attendance records and view attendance percentage.

### Fees

Students can manage:

* Fee Type
* Total Amount
* Paid Amount
* Pending Amount
* Payment Status

Fee status is automatically calculated as:

```text
Paid
Partial
Pending
```

### Settings

Students can change their account password.

## Authentication

The application uses session-based authentication.

Important session values include:

```text
StudentId
StudentName
```

A custom `StudentLoginFilter` protects authenticated student pages.

## Future Improvements

* Admin Dashboard
* Role-based Authentication
* Online Fee Payment
* Email Notifications
* PDF Report Generation
* Student Profile Photo
* Attendance Charts
* Result Charts
* REST API Integration
* Deployment to Azure

## Author

**Aniket Singh**

Student Account Management System developed using ASP.NET Core MVC and SQL Server.
