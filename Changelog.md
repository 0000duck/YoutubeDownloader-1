### v1.2 (19-Jan-2019)

- Added video quality selection when dowloading a single video. For playlists and search results, the highest video quality available for selected format is used.
- Added support for `ogg` format.
- Added support for `webm` format when dowloading a single video. May not always be available.
- Updated the app icon to make it more distinct from YoutubeExplode.
- Fixed an issue where child FFmpeg processes would not exit after the user closes the app while there are active downloads.
- Fixed an issue where the app could sometimes crash when checking for updates.
- Fixed an issue where it was possible to start multiple downloads to the same file path.

### v1.1.1 (22-Dec-2018)

- The list of downloads is now always sorted chronologically.
- When adding multiple downloads, the application will try to ensure unique file names by appending suffixes in order to avoid accidental overwrites.
- Fixed an issue where adding multiple downloads would sometimes cause the application to crash.

### v1.1 (21-Dec-2018)

- Improved UI in all screens.
- Limited the number of concurrent downloads and added an option in settings to configure it.
- Last used download format is now persisted so you don't have to select it all the time.
- Fixed an issue where temporary files were not deleted after the download was canceled. The underlying issue was fixed in YoutubeExplode.Converter v1.0.3 and CliWrap v2.2.
- Fixed an issue where starting multiple downloads would cause them to be added in the wrong order to the list of downloads.