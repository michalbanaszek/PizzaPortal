FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ConnectionStrings:PizzaConnectionDocker='Server=mssql_local;Database=PizzaDB;User ID=sa;Password=Qwerty!1'

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["PizzaPortal.WEB/PizzaPortal.WEB.csproj", "PizzaPortal.WEB/"]
COPY ["PizzaPortal.BLL/PizzaPortal.BLL.csproj", "PizzaPortal.BLL/"]
COPY ["PizzaPortal.DAL/PizzaPortal.DAL.csproj", "PizzaPortal.DAL/"]
COPY ["PizzaPortal.Model/PizzaPortal.Model.csproj", "PizzaPortal.Model/"]
COPY ["PizzaPortal.Migrations/PizzaPortal.Migrations.csproj", "PizzaPortal.Migrations/"]
RUN dotnet restore "PizzaPortal.WEB/PizzaPortal.WEB.csproj"
COPY . .
WORKDIR "/src/PizzaPortal.WEB"
RUN dotnet build "PizzaPortal.WEB.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PizzaPortal.WEB.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PizzaPortal.WEB.dll"]