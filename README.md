# 📌 Registration API – ASP.NET Core 8 (Three-Tier Architecture)

This project is a RESTful **Web API** built using **ASP.NET Core 8** following a **Three-Tier Architecture** with **PostgreSQL**, and uses **Stored Procedures** for all database interactions (no Entity Framework). The API supports full **CRUD** operations for user registration, along with **password hashing**, and is tested using **Swagger UI**.

---

## 🚀 Technologies Used

- ASP.NET Core 8 Web API
- Three-Tier Architecture (Controller → Service → Repository)
- PostgreSQL with Npgsql (ADO.NET)
- Stored Procedures (no ORM)
- Swagger for API documentation
- BCrypt for password hashing

---

## 📂 Project Structure
RegistrationApi/ │ ├── Controllers/ │ └── RegistrationController.cs │ ├── Services/ │ └── RegistrationService.cs │ ├── Repositories/ │ └── RegistrationRepository.cs │ ├── Models/ │ ├── Registration.cs │ └── RegistrationRequest.cs │ ├── Program.cs ├── appsettings.json └── ...

---

## 🧩 Features

- ✅ Register a new user (POST)
- 📄 Get all users (GET)
- 🔍 Get user by ID (GET)
- ✏️ Update user info (PUT)
- ❌ Delete a user (DELETE)
- 🔐 Passwords are stored as **secure hashes**
- 🔄 Full **Stored Procedure** usage with **parameterized queries**

---

## 🗃️ PostgreSQL Setup

### 🔧 Table

```sql
CREATE TABLE registration (
    id SERIAL PRIMARY KEY,
    fullname TEXT NOT NULL,
    email TEXT NOT NULL UNIQUE,
    phone TEXT NOT NULL,
    password TEXT NOT NULL,
    createdat TIMESTAMP DEFAULT NOW()
);


##Stored Procedures
-- Create User
CREATE OR REPLACE PROCEDURE create_user(
    _fullname TEXT,
    _email TEXT,
    _phone TEXT,
    _password TEXT
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO registration (fullname, email, phone, password)
    VALUES (_fullname, _email, _phone, _password);
END;
$$;

-- Get All Users
CREATE OR REPLACE FUNCTION get_all_users()
RETURNS TABLE (
    id INT,
    fullname TEXT,
    email TEXT,
    phone TEXT,
    password TEXT,
    createdat TIMESTAMP
)
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY SELECT * FROM registration;
END;
$$;


 ##Sample JSON for POST /api/registration
{
  "fullname": "John Doe",
  "email": "john@example.com",
  "phone": "1234567890",
  "password": "MySecurePass123"
}


##Configuration
Update your appsettings.json with your PostgreSQL connection string:
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=YourDbName;Username=your_user;Password=your_password"
}

## Test Using Swagger
Run the app and navigate to:
https://localhost:<your-port>/swagger
You can test all endpoints interactively using Swagger UI.

Author
Vinamra Patel
📧 vinamrapatel0@gmail.com

