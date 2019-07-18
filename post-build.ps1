$lexactivator_version = "v3.7.1"
$url = "https://dl.cryptlex.com/downloads/$lexactivator_version/LexActivator-Win.zip"
$output = "$PSScriptRoot\LexActivator-Win.zip"

(New-Object System.Net.WebClient).DownloadFile($url, $output)

Expand-Archive "$PSScriptRoot\LexActivator-Win.zip" -DestinationPath "$PSScriptRoot\LexActivator-Win"

Copy-Item -Path "$PSScriptRoot\LexActivator-Win\libs\vc14\x64\LexActivator.dll" -Destination "$PSScriptRoot\src\Cryptlex.LexActivator\runtimes\win-x64\native\LexActivator64.dll"
Copy-Item -Path "$PSScriptRoot\LexActivator-Win\libs\vc14\x86\LexActivator.dll" -Destination "$PSScriptRoot\src\Cryptlex.LexActivator\runtimes\win-x86\native\LexActivator32.dll"