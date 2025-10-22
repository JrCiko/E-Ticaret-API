# Mini E-Commerce API with .NET 7 & JWT Authentication

## 🚀 Project Overview

This project is a robust and secure Mini E-Commerce API built with ASP.NET Core 7 Web API. It demonstrates core CRUD (Create, Read, Update, Delete) operations for products, along with a complete user authentication and authorization system using JWT (JSON Web Tokens).

It serves as a strong foundation for any modern web or mobile application requiring a secure backend.

## ✨ Key Features

* **Product Management:** Full CRUD operations for managing product inventory.
* **User Authentication:** Secure user registration and login endpoints.
* **JWT Authorization:** Protects API endpoints, allowing access only to authenticated users with a valid JWT.
* **Password Hashing:** Implements strong password hashing using BCrypt.NET for enhanced security.
* **Database Integration:** Utilizes Entity Framework Core with SQL Server for data persistence.
* **Swagger/OpenAPI:** Provides interactive API documentation for easy testing and understanding of endpoints.

## 🛠️ Technologies Used

* **Backend:** C# (.NET 7)
* **Framework:** ASP.NET Core Web API
* **Database:** SQL Server
* **ORM:** Entity Framework Core
* **Authentication:** JWT (JSON Web Tokens)
* **Password Hashing:** BCrypt.NET-Next
* **API Documentation:** Swagger / OpenAPI

## 🚀 Getting Started

Follow these steps to get the project up and running on your local machine.

### Prerequisites

* [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0) (or newer)
* [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (Recommended)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or SQL Server Express)
* [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) (Optional, for database management)
* [Postman](https://www.postman.com/downloads/) (Optional, for API testing)

### Installation

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/YourGitHubUsername/Mini-ECommerce-API.git](https://github.com/YourGitHubUsername/Mini-ECommerce-API.git)
    cd Mini-ECommerce-API
    ```
    *(GitHub kullanıcı adını ve repo adını değiştirmeyi unutma)*

2.  **Configure Database Connection String:**
    Open `appsettings.json` and update the `DefaultConnection` string to point to your SQL Server instance.

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=.\\SQLExpress;Database=MiniECommerceDB;Trusted_Connection=True;TrustServerCertificate=True;"
    }
    ```
    *(Replace `.\\SQLExpress` with your SQL Server instance name if different)*

3.  **Apply Migrations and Update Database:**
    Open the Package Manager Console in Visual Studio (Tools > NuGet Package Manager > Package Manager Console) and run:
    ```bash
    Update-Database
    ```
    *(If you face issues, ensure you have set the correct default project in PMC and that the connection string is valid.)*

4.  **Configure JWT Secret Key:**
    In `appsettings.json`, ensure you have a strong, long (at least 64 characters) secret key for JWT.

    ```json
    "AppSettings": {
      "Token": "YourVeryStrongAndLongSecretKeyForJWTAuthenticationMustBeAtLeast64CharactersLongToSecureYourAPI"
    }
    ```
    *(**IMPORTANT:** Replace this with your actual strong secret key.)*

5.  **Run the application:**
    * In Visual Studio, press `F5` or `Ctrl + F5` to run the project.
    * The API will typically launch on `https://localhost:XXXX` and open the Swagger UI automatically.

## 💡 Usage (Testing the API with Swagger)

Once the application is running, the Swagger UI will open in your browser, providing an interactive interface to test the API endpoints.

### Authentication Flow:

1.  **Register a User:**
    * Navigate to the `/api/Auth/register` endpoint in Swagger.
    * Use the "Try it out" button and enter a `username` and `password` for a new user.
    * Execute the request. You should get a `200 OK` response.

2.  **Login as the User:**
    * Navigate to the `/api/Auth/login` endpoint in Swagger.
    * Enter the `username` and `password` of the user you just registered.
    * Execute the request. You should receive a `200 OK` response with a **JWT token** in the body. **Copy this token.**

3.  **Authorize Your Requests:**
    * Click on the **"Authorize"** button (usually at the top right of the Swagger UI).
    * In the dialog box, type `Bearer {Your_JWT_Token}` (replace `{Your_JWT_Token}` with the token you copied, ensuring there's a space after `Bearer`).
    * Click "Authorize".

4.  **Access Protected Endpoints:**
    * Now, try to access the `/api/Products` endpoints (e.g., `GET /api/Products`).
    * You should now get a `200 OK` response with product data (or an empty array if no products are added yet), instead of a `401 Unauthorized`.
    * You can also try `POST /api/Products` to add a new product while authorized.

## 🤝 Contribution

Contributions are welcome! If you have suggestions or find issues, please open an issue or pull request on GitHub.

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
*(Eğer projen için bir lisans dosyası eklemek istersen, genellikle MIT lisansı açık kaynak projelerde yaygındır.)*

---
