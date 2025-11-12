#  🚗  FleetApp
![.NET 8](https://img.shields.io/badge/.NET-8.0-blue)
![License](https://img.shields.io/badge/license-MIT-green)
![Build](https://img.shields.io/badge/build-passing-brightgreen)


### FleetApp is a .NET 8 minimal API designed to manage a fleet of vehicles, It follows Clean Architecture principles with clear separation of layers, robust validation, and full unit and integration test coverage.

### 🧰 Technologies Used

- 🧱 **.NET 8 Minimal API** — lightweight and fast web API framework  
- ✅ **FluentValidation** — for input and business rule validation  
- 🪵 **Serilog** — structured logging (console and file)  
- 📘 **Swagger / Swashbuckle** — OpenAPI documentation  
- 🧩 **xUnit + Moq + FluentAssertions** — testing stack  
- 🐳 **Docker & Docker Compose** — containerization support  
- 🧼 **Clean Architecture** — modular design with clear separation of layers  
- 💾 **In-memory data store** — no database required for running or testing

### 🧱 Project Structure
<img width="256" height="177" alt="image" src="https://github.com/user-attachments/assets/149a24ab-a445-4c7c-802b-371912325b2f" />


### 🖥️  Running Locally (without Docker)

You can run FleetApp locally using the .NET SDK for development and debugging.  
If you prefer to use Docker instead, check the section ** Running with Docker** below.

> **Requirements:**  
> - [.NET 8 SDK](https://dotnet.microsoft.com/download) installed  
> - (Optional) Visual Studio 2022 or VS Code  

---

#### Steps

```bash
# 1. Clone the repository
git clone https://github.com/HenriqueLopesDeSouza/FleetApp.git
cd FleetApp            # go to the project root, where FleetApp.sln is located

# 2. Restore and build the entire solution
dotnet restore FleetApp.sln
dotnet build   FleetApp.sln

# 3. (Optional) Trust HTTPS certificate if you want to use HTTPS
dotnet dev-certs https --trust

# 4. Run the API (simplest way - HTTP only)
dotnet run --project src/FleetApp.Api --no-launch-profile --urls http://localhost:8080

# 5. Open your browser at:
# http://localhost:8080/swagger

# 6. (Optional) To run with HTTPS profile
dotnet run --project src/FleetApp.Api

# 7. Run all tests
dotnet test FleetApp.sln
```

### 🐳 Running with Docker

FleetApp includes full Docker support for running the API inside a containerized environment.  
This is the recommended way to run the application, since it does **not require the .NET SDK** to be installed on your machine.

> **Requirements:**  
> - [Docker Desktop](https://www.docker.com/) installed and running

---

####  Steps

```bash
# 1. Clone the repository
git clone https://github.com/HenriqueLopesDeSouza/FleetApp.git
cd FleetApp            # go to the project root, where docker-compose.yml is located

# 2. Build and start the containers
docker-compose up --build

# 3. (Optional) Run in detached mode (background)
docker-compose up -d --build

# 4. Stop the containers
docker-compose down

```

### Access the application

Once Docker finishes building and starts the container, open your browser at:
http://localhost:8080/swagger

---
Developed by **Henrique Lopes de Souza**  
📫 [LinkedIn](https://www.linkedin.com/in/henriquelopesdesouza) • [GitHub](https://github.com/HenriqueLopesDeSouza)

