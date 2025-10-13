# codesign.ps1
Write-Host "Starting code signing process..."

# Step 1: Install Google Cloud KMS Provider
Write-Host "Installing Google Cloud KMS Provider..."
$providerUrl = "https://github.com/GoogleCloudPlatform/kms-integrations/releases/download/cng-v1.2/kmscng-1.2-windows-amd64.zip"
Invoke-WebRequest -Uri $providerUrl -OutFile "kms-provider.zip" -UseBasicParsing
Expand-Archive -Path "kms-provider.zip" -DestinationPath "kms-provider" -Force
$msiFile = Get-ChildItem -Path "kms-provider" -Filter "*.msi" -Recurse | Select-Object -First 1
Start-Process msiexec.exe -Wait -ArgumentList '/i', $msiFile.FullName, '/quiet', '/qn', '/norestart'

# Step 2: Add signtool to PATH
Write-Host "Adding signtool to PATH..."
$signtool = (Get-ChildItem -Path "C:\Program Files (x86)\Windows Kits\10\bin\*\x64\signtool.exe" -Recurse | Select-Object -First 1).FullName
$env:PATH = "$([System.IO.Path]::GetDirectoryName($signtool));$env:PATH"

# Step 3: Sign all DLLs
Write-Host "Signing all DLLs..."
$kmsKeyPath = "projects/cryptlex-sdks/locations/us/keyRings/code-signing/cryptoKeys/code-signing-key-2025-28/cryptoKeyVersions/1"

Get-ChildItem -Path ".\src\Cryptlex.LexActivator\bin\Release\" -Filter "*.dll" -Recurse -File | ForEach-Object {
    Write-Host "Signing: $($_.Name)"
    signtool sign /f "certs/code-signing-cert.crt" /csp "Google Cloud KMS Provider" /kc $kmsKeyPath /fd sha256 /tr http://timestamp.sectigo.com /td sha256 $_.FullName
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Signing failed for: $($_.Name)"
        exit 1
    }
}

Write-Host "Code signing process completed successfully!"