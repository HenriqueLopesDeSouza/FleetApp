# FleetApp

## FleetApp is a .NET 8 minimal API designed to manage a fleet of vehicles, It follows Clean Architecture principles with clear separation of layers, robust validation, and full unit and integration test coverage.


### 🚀 Running Locally (without Docker)

You can run FleetApp locally using the .NET SDK for development and debugging.  
If you prefer to use Docker instead, check the section **🐳 Running with Docker** below.

> **Requirements:**  
> - [.NET 8 SDK](https://dotnet.microsoft.com/download) installed  
> - (Optional) Visual Studio 2022 or VS Code  

---

#### 🧭 Steps

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

