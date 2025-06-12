# Clinic Web API

![C#](https://img.shields.io/badge/Language-C%23-blue)
![Web API](https://img.shields.io/badge/Framework-Web%20API-green)
![License](https://img.shields.io/badge/License-MIT-brightgreen)

The **Clinic Web API** is a backend application built with **C#** using the **ASP.NET Web API framework**. It provides endpoints to manage clinic operations such as patient records, appointments, doctor schedules, and more. This project uses **ADO.NET** for database interactions, offering a lightweight and efficient approach without relying on Entity Framework.

---

## Features

- 🏥 **Patient Management**: Add, update, and delete patient records.
- 📅 **Appointment Scheduling**: Book, reschedule, or cancel appointments.
- 👩‍⚕️ **Doctor Management**: Manage doctor profiles and schedules.
- 🔒 **Secure Access**: Implements basic authentication or other secure methods for endpoint access.
- 📡 **RESTful Endpoints**: Fully compliant with REST principles.
- 📊 **Efficient Database Access**: Built with **ADO.NET** for direct and optimized database communication.

---

## Table of Contents

- [Features](#features)
- [Getting Started](#getting-started)
- [API Documentation](#api-documentation)
- [Technologies Used](#technologies-used)
- [Contributing](#contributing)
- [License](#license)

---

## Getting Started

### Prerequisites

- **.NET SDK** (6.0 or later)
- A development environment like **Visual Studio** or **Visual Studio Code**.
- A database system (e.g., SQL Server).

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/mohamedHodaib/Clinic_Web-Api.git
   ```
2. Navigate to the project directory:
   ```bash
   cd Clinic_Web-Api
   ```
3. Restore dependencies:
   ```bash
   dotnet restore
   ```
4. Update the database connection string in `appsettings.json` to match your SQL Server instance.
5. Database:
- The SQL script to create and populate the database  .
- To set up:
  1. Create a new database in SQL Server.
  2. Run the provided `DataBase.sql` script to create tables and insert sample data.
6. Run the application:
   ```bash
   dotnet run
   ```

---

## API Documentation

The API exposes the following key endpoints:

### Patients
- `GET /api/patients`: Retrieve a list of patients.
- `POST /api/patients`: Add a new patient.
- `PUT /api/patients/{id}`: Update patient details.
- `DELETE /api/patients/{id}`: Remove a patient record.

### Appointments
- `GET /api/appointments`: List all appointments.
- `POST /api/appointments`: Schedule a new appointment.
- `PUT /api/appointments/{id}`: Modify an appointment.
- `DELETE /api/appointments/{id}`: Cancel an appointment.

### Doctors
- `GET /api/doctors`: Retrieve a list of doctors.
- `POST /api/doctors`: Add a new doctor profile.
- `PUT /api/doctors/{id}`: Update doctor details.
- `DELETE /api/doctors/{id}`: Remove a doctor profile.

For detailed documentation, see the [API Reference](docs/API_REFERENCE.md).

---

## Technologies Used

- **C#**
- **ASP.NET Web API**
- **ADO.NET** (Database interactions)
- **SQL Server**

---

## Contributing

We welcome contributions to improve this project! To get started:

1. Fork the repository.
2. Create a new feature branch:
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. Commit your changes:
   ```bash
   git commit -m "Add your feature description"
   ```
4. Push to the branch:
   ```bash
   git push origin feature/your-feature-name
   ```
5. Submit a Pull Request.

---

## License

This project is licensed under the [MIT License](LICENSE).

---

## Contact

For any inquiries, feel free to reach out:

- **Author**: Mohamed Hodaib
- **GitHub**: [mohamedHodaib](https://github.com/mohamedHodaib)

---

Enjoy building and managing your clinic operations with this Web API!
