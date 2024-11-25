﻿# Build nupkg and publish to nuget.org for the Safari.Net.Core and Safari.Net.Data projects
# Usage: .\publish.ps1 -v 1.0.0 -k nugetKey -p @("project")

param (
    [string]$v, 
    [string]$k,
    [string[]]$projects = @("Digital.Net.Core", "Digital.Net.Entities", "Digital.Net.TestTools")
)

foreach ($project in $projects) {
    Write-Host "Building ${project}..." -ForegroundColor Green
    Invoke-Expression "dotnet build ${project}/${project}.csproj -c Release"

    Write-Host "Packing ${project}..." -ForegroundColor Green
    Invoke-Expression "dotnet pack ${project}/${project}.csproj -c Release"

    Write-Host "Publishing ${project}" -ForegroundColor Green
    Invoke-Expression "dotnet nuget push ${project}/bin/Release/${project}.${v}.nupkg --api-key ${k} --source https://api.nuget.org/v3/index.json"
}