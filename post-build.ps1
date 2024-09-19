$base_url = "https://dl.cryptlex.com/downloads"
$lexactivator_version ="v3.30.0"
new-item -Name tmp -ItemType directory

$url = "$base_url/$lexactivator_version/LexActivator-Win.zip"
Write-Host "Downloading LexActivator library ..."
Write-Host $url
$output = "$PSScriptRoot\tmp\LexActivator-Win.zip"
(New-Object System.Net.WebClient).DownloadFile($url, $output)
Expand-Archive "$PSScriptRoot\tmp\LexActivator-Win.zip" -DestinationPath "$PSScriptRoot\tmp\LexActivator-Win"
Copy-Item -Path "$PSScriptRoot\tmp\LexActivator-Win\libs\vc14\x64\LexActivator.dll" -Destination "$PSScriptRoot\src\Cryptlex.LexActivator\runtimes\win-x64\native\LexActivator.dll"
Copy-Item -Path "$PSScriptRoot\tmp\LexActivator-Win\libs\vc14\x86\LexActivator.dll" -Destination "$PSScriptRoot\src\Cryptlex.LexActivator\runtimes\win-x86\native\LexActivator32.dll"

$url = "$base_url/$lexactivator_version/LexActivator-Linux.zip"
Write-Host $url
$output = "$PSScriptRoot\tmp\LexActivator-Linux.zip"
(New-Object System.Net.WebClient).DownloadFile($url, $output)
Expand-Archive "$PSScriptRoot\tmp\LexActivator-Linux.zip" -DestinationPath "$PSScriptRoot\tmp\LexActivator-Linux"
Copy-Item -Path "$PSScriptRoot\tmp\LexActivator-Linux\libs\gcc\amd64\libLexActivator.so" -Destination "$PSScriptRoot\src\Cryptlex.LexActivator\runtimes\linux-x64\native\libLexActivator.so"
Copy-Item -Path "$PSScriptRoot\tmp\LexActivator-Linux\libs\gcc\arm64\libLexActivator.so" -Destination "$PSScriptRoot\src\Cryptlex.LexActivator\runtimes\linux-arm64\native\libLexActivator.so"
Copy-Item -Path "$PSScriptRoot\tmp\LexActivator-Linux\libs\gcc\armhf\libLexActivator.so" -Destination "$PSScriptRoot\src\Cryptlex.LexActivator\runtimes\linux-arm\native\libLexActivator.so"
Copy-Item -Path "$PSScriptRoot\tmp\LexActivator-Linux\libs\musl\amd64\libLexActivator.so" -Destination "$PSScriptRoot\src\Cryptlex.LexActivator\runtimes\linux-musl-x64\native\libLexActivator.so"
Copy-Item -Path "$PSScriptRoot\tmp\LexActivator-Linux\libs\musl\arm64\libLexActivator.so" -Destination "$PSScriptRoot\src\Cryptlex.LexActivator\runtimes\linux-musl-arm64\native\libLexActivator.so"
Copy-Item -Path "$PSScriptRoot\tmp\LexActivator-Linux\libs\musl\armhf\libLexActivator.so" -Destination "$PSScriptRoot\src\Cryptlex.LexActivator\runtimes\linux-musl-arm\native\libLexActivator.so"

$url = "$base_url/$lexactivator_version/LexActivator-Mac.zip"
Write-Host $url
$output = "$PSScriptRoot\tmp\LexActivator-Mac.zip"
(New-Object System.Net.WebClient).DownloadFile($url, $output)
Expand-Archive "$PSScriptRoot\tmp\LexActivator-Mac.zip" -DestinationPath "$PSScriptRoot\tmp\LexActivator-Mac"
Copy-Item -Path "$PSScriptRoot\tmp\LexActivator-Mac\libs\clang\x86_64\libLexActivator.dylib" -Destination "$PSScriptRoot\src\Cryptlex.LexActivator\runtimes\osx-x64\native\libLexActivator.dylib"
Copy-Item -Path "$PSScriptRoot\tmp\LexActivator-Mac\libs\clang\arm64\libLexActivator.dylib" -Destination "$PSScriptRoot\src\Cryptlex.LexActivator\runtimes\osx-arm64\native\libLexActivator.dylib"

