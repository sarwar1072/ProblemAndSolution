FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProblemAndSolution.Web/*.csproj", "ProblemAndSolution.Web/"]
COPY ["ProblemAndSolution.Infrastructure/*.csproj", "ProblemAndSolution.Infrastructure/"]
COPY ["ProblemAndSolution.Membership/*.csproj", "ProblemAndSolution.Membership/"]
RUN dotnet restore "ProblemAndSolution.Web/ProblemAndSolution.Web.csproj"
COPY . .
WORKDIR "/src/ProblemAndSolution.Web"
RUN dotnet build "ProblemAndSolution.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ProblemAndSolution.Web.csproj" -c Release -o /app

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProblemAndSolution.Web.dll"]