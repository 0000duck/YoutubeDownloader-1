New-Item "$PSScriptRoot\bin" -ItemType Directory -Force
$files = @()
$files += Get-Item -Path "$PSScriptRoot\..\License.txt"
$files += Get-ChildItem -Path "$PSScriptRoot\..\YoutubeDownloader\bin\Release\*" -Include "*.exe", "*.dll", "*.config"
$files | Compress-Archive -DestinationPath "$PSScriptRoot\bin\YoutubeDownloader.zip" -Force