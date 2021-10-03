#!/bin/bash
INFO="BUILDING ALL PROJECTS"
echo $INFO

dotnet build ./Core/Core.csproj
dotnet build ./Infrastructure/Infrastructure.csproj
dotnet build ./API/API.csproj