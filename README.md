# Pizza Portal Web Application

## Getting Started

Use these instructions to get the project up and running

### Prerequisites
You will need the following tools:

* [Visual Studio Code](https://code.visualstudio.com/)
* [.NET Core SDK 5.0](https://www.microsoft.com/net/download/dotnet-core/5.0)
* [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Setup

Follow these steps to get your development environment set up:

#### First option - using local machine

 1. Clone the repository
 1. Open terminal and enter the command `cd PizzaPortal`
 1. Next, enter the command `cd PizzaPortal.WEB` 
 1. Modify connection strings in `PizzaPortal.WEB/Installer/DatabaseInstaller` from "DockerConnection" to "DefaultConnection"  
 1. Generate localhost certificate for redirect HTTPS, open powershell and enter the command `dotnet dev-certs https -ep     $env:USERPROFILE\.aspnet\https\PizzaPortal.WEB.pfx -p <Password>`
 1. Next, enter the command `dotnet dev-certs https --trust`, click yes if open dialog is show
 1. Next, generate key-value pair for certificate password to secret.json file, enter the command `dotnet user-secrets set "Kestrel:Certificates:Development:Password" "<Password>"` 
 1. Next, generate key-value pair for connectingString to secret.json file, enter the command `dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=<YourLocalSQLInstance>;Database=<DatabaseName>;Trusted_Connection=True;MultipleActiveResultSets=true"` 
 4. Build solution `dotnet build`
 5. Run app `dotnet run` 
 
 #### Second option - using docker

 1. Clone the repository
 1. Open terminal and enter the command `cd PizzaPortal`  
 1. Next, enter the command `docker-compose up --build` 

## Technologies

* .NET Core 5.0
* Entity Framework Core 5.0
* Entity Framework Core SqlServer 5.0
* Bootstrap
* MVC
* Docker

#### Docker Hub Repository

https://hub.docker.com/repository/docker/biaaly13/pizzaportalweb
