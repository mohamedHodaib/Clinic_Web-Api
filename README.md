🏥 Clinic Web API
This is a multi-layered ASP.NET Core Web API project for managing a clinic system. It is built with a clean architecture approach, using ADO.NET for direct database access instead of Entity Framework, which allows for more control and performance optimization in SQL interactions.

📁 Project Structure
bash
Copy
Edit
Clinic_Api/
│
├── Clinic/                  # API Layer (Presentation)
│   └── Controllers/         # HTTP Endpoints
│
├── Clinic.Business/         # Business Layer
│   └── Services/            # Business Logic and Interfaces
│
├── Clinic.DataAccess/       # Data Access Layer
│   └── ADO.NET Logic        # SQL Commands, Connections, Repositories
│
├── .gitignore
└── README.md
🔧 Technologies Used
ASP.NET Core Web API

ADO.NET (SqlConnection, SqlCommand, etc.)

C#

RESTful API design

Layered architecture

🚀 Features
Modular architecture: API, Business, and Data Access layers

ADO.NET for lightweight and performant database operations

Secure configuration via appsettings.json

Clean separation of concerns and reusable services

🛠️ Getting Started
Prerequisites
.NET SDK 7.0 or later

Visual Studio or VS Code

SQL Server (or any compatible database)

Running the App
Clone the repository:

bash
Copy
Edit
git clone https://github.com/yourusername/Clinic_Api.git
Navigate to the project folder:

bash
Copy
Edit
cd Clinic_Api
Restore dependencies:

bash
Copy
Edit
dotnet restore
Build the solution:

bash
Copy
Edit
dotnet build
Run the API:

bash
Copy
Edit
dotnet run --project Clinic
Access Swagger UI (if configured):

bash
Copy
Edit
http://localhost:<port>/swagger
⚙️ Configuration
Add your SQL Server connection string to appsettings.json:

json
Copy
Edit
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DB;Trusted_Connection=True;"
}
Make sure your data access layer (Clinic.DataAccess) retrieves this connection string via IConfiguration.
