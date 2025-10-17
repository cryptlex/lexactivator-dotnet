# code-sign.ps1
# =============================================================================
# CODE SIGNING SCRIPT - Uses Jsign with Google Cloud KMS
# Usage: .\code-sign.ps1 -SignType "dll|nupkg"
# =============================================================================
param(
    [Parameter(Mandatory=$true)]
    [ValidateSet("dll", "nupkg")]
    [string]$SignType
)
 
# =============================================================================
# SETUP
# =============================================================================

Write-Host "Downloading Jsign..."
$jsignUrl = "https://github.com/ebourg/jsign/releases/download/7.0/jsign-7.0.jar"
Invoke-WebRequest -Uri $jsignUrl -OutFile "jsign.jar" -UseBasicParsing

# Common parameters
$keystore = "projects/cryptlex-sdks/locations/us/keyRings/code-signing"
$alias = "code-signing-key-2025-28/cryptoKeyVersions/1"
$accessToken = gcloud auth print-access-token

# =============================================================================
# SIGN DLLs
# =============================================================================

if ($SignType -eq "dll") {
    Write-Host "Signing DLLs..."
    
    Get-ChildItem -Path ".\src\Cryptlex.LexActivator\bin\Release\" -Filter "*.dll" -Recurse -File | ForEach-Object {
        Write-Host "Signing: $($_.Name)"
        java -jar jsign.jar --storetype GOOGLECLOUD --keystore $keystore --storepass $accessToken --alias $alias --certfile "certs/code-signing-cert.crt" --tsaurl http://timestamp.sectigo.com --alg SHA-256 $_.FullName
        if ($LASTEXITCODE -ne 0) { exit 1 }
    }
}

# =============================================================================
# SIGN NUGET PACKAGES
# =============================================================================

if ($SignType -eq "nupkg") {
    Write-Host "Signing NuGet packages..."
    
    Get-ChildItem -Path ".\nupkg\" -Filter "*.nupkg" -File | ForEach-Object {
        Write-Host "Signing: $($_.Name)"
        java -jar jsign.jar --storetype GOOGLECLOUD --keystore $keystore --storepass $accessToken --alias $alias --certfile "certs/code-signing-cert.crt" --tsaurl http://timestamp.sectigo.com --alg SHA-256 $_.FullName
        if ($LASTEXITCODE -ne 0) { exit 1 }
    }
}

Write-Host "Code signing completed successfully!"