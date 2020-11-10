# Pizza Portal Web Application

## Getting Started

Use these instructions to get the project up and running

### Prerequisites
You will need the following tools:

* [Visual Studio Code](https://code.visualstudio.com/)
* [.NET Core SDK 2.2](https://www.microsoft.com/net/download/dotnet-core/2.2)
* [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Setup

Follow these steps to get your development environment set up:

#### First option

 1. Clone the repository
 1. Open terminal and enter the command `cd PizzaPortal`
 1. Next, enter the command `docker-compose -f "docker-compose.debug.yml" up -d --build` 
 1. Retry command if web is exited
 
 #### Second option

 1. Clone the repository
 1. Open terminal and enter the command `cd PizzaPortal`
 1. Next, enter the command `cd PizzaPortal.WEB`
 1. Modify connection strings in `PizzaPortal.WEB/Installer/DatabaseInstaller` from "PizzaConnectionDocker" to "PizzaConnectionMssqllocal" 
 1. Build solution `dotnet build`
 1. Run app `dotnet run` 

## Technologies

* .NET Core 2.2
* ASP.NET Core 2.2
* Entity Framework Core 2.2
* Entity Framework Core SqlServer 2.2
* Bootstrap
* MVC

#### Docker Hub Repository

https://hub.docker.com/repository/docker/biaaly13/pizzaportalweb
