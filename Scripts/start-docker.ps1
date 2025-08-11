# PowerShell script to start Docker containers for Templify project

Write-Host "Starting Templify Docker containers..." -ForegroundColor Green

# Check if Docker is running
try {
    docker version | Out-Null
    Write-Host "Docker is running" -ForegroundColor Green
}
catch {
    Write-Host "Docker is not running. Please start Docker Desktop first." -ForegroundColor Red
    exit 1
}

# Navigate to project directory
$projectDir = Split-Path -Parent $PSScriptRoot
Set-Location $projectDir

# Start containers
Write-Host "Starting PostgreSQL and pgAdmin containers..." -ForegroundColor Yellow
docker-compose up -d

# Wait for containers to be ready
Write-Host "Waiting for containers to be ready..." -ForegroundColor Yellow
Start-Sleep -Seconds 10

# Check container status
Write-Host "Container status:" -ForegroundColor Yellow
docker-compose ps

Write-Host "`nTemplify containers are running!" -ForegroundColor Green
Write-Host "PostgreSQL: localhost:5432" -ForegroundColor Cyan
Write-Host "pgAdmin: http://localhost:5050" -ForegroundColor Cyan
Write-Host "Database: templify_db" -ForegroundColor Cyan
Write-Host "Username: templify_user" -ForegroundColor Cyan
Write-Host "Password: templify_password" -ForegroundColor Cyan
Write-Host "`npgAdmin credentials:" -ForegroundColor Cyan
Write-Host "Email: admin@templify.com" -ForegroundColor Cyan
Write-Host "Password: admin123" -ForegroundColor Cyan
