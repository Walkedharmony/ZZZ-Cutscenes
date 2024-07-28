# ZZZ-Cutscenes

A command line program playing with the cutscenes files (USM) from Zenless Zone Zero.

Able to extract the USM files, decrypt the tracks and convert them into readable formats, then merge them into a single MKV file.
The final MKV file can then be played like a small movie, with the subtitles correctly formatted like in the game.
Sometimes, subtitles can be desynchronized with the audio, but that's also the case in game (and not this program's fault).

#### Cutscenes from version 1.0 can be decrypted.

_Also includes CBT3, which has more or less the same files than the live version_

If you want to extract newer cutscenes but the `versions.json` in the released zip is outdated, simply download the updated file in the project tree ([here](https://raw.githubusercontent.com/Clostro/ZZZ-cutscenes/main/versions.json)) and replace the file.
This file will be updated with the version key every time a new version drops.

### Feel free to make a pull request if you have some keys unavailable in the versions file, any help is welcome on that part.

## Features (and roadmap)

- [x] Extract video and audio (and video decryption)
- [x] Audio (hca) decryption
- [x] HCA conversion to WAV (a more commonly readable format)
- [x] MKV merging with video, audio, subtitles and fonts without any additional software (but also supports the use of mkvmerge and FFMPEG)
- [x] Multithreaded audio decoding

## Build

This program uses the .NET framework version 6.0, so you will need the .NET SDK.
You can open this project in Visual Studio 2022 and build the solution, or use the dotnet CLI : `dotnet publish -c Release -r [platform]`.
Otherwise, you can also modify the script `build-all.sh` with the desired runtimes.

## Usage

You can follow the next steps to use this program :

### 1. Download

Grab the latest release for your platform from the [release page](https://github.com/Clostro/ZZZ-cutscenes/releases/latest), download the ZIP file and extract it.
For each platform (Windows, Linux, MacOS), two binaries are available:

- a standalone build (self-contained executable) which can be run without dotnet installed
- a framework dependant build (if you already have dotnet installed on your machine), much lighter but requires the dotnet runtime

You can also get a GUI version in this [repository](https://github.com/SuperZombi/GICutscenesUI) (thanks to [SuperZombi](https://github.com/SuperZombi))

Starting from version **0.4.0**, an merging solution was integrated without relying on external programs.
However, if you wish to use other merging solutions than the one integrated, you can install MKVToolNix (which provides mkvmerge) or FFMPEG.

### 2. Configuration

`appsettings.json` contains a configuration sample with the following keys :

- "MkvMergePath" : The path where mkvmerge is installed. Leave it empty if you installed mkvtoolnix (the package/program providing mkvmerge) in the default path. However, change it to the path of the mkvmerge file in case you're using a different installation path or you're using the portable MKVToolNix version.
- "FfmpegPath" : The path to the ffmpeg binary. Leave it empty if the binary is in the PATH of your operating system.
- 
### 3. Commands

There are 2 different commands available :

- `demux` to demux a specific USM file or corresponding files in a given folder, extracting audio and video and convert extracted HCA files into WAV
- `convert-hca` to convert a HCA file into WAV

Several options are available for most of the commands :

- `--output` allows to choose the output folder
- `--merge` adds a merging step, putting the video, the audio into a single MKV file. 
- `--no-cleanup` disables the suppression of the extracted files after merging
- `--mkv-engine` specifies the merging program used (either `internal`, `mkvmerge` or `ffmpeg`, using the internal method by default)
- `--audio-format` and `--video-format` can be used to select codecs. If at least one option is chosen, **the merging engine is automatically changed to FFMPEG**.
- `--audio-lang` allow to specify audio track language in the output, allowed values are `[chi,eng,jpn,kor]`

For demuxing, a key can be supplied to decrypt the USM file using either the parameter `--key <hex or number>` or `-b <4 lower bytes of key> -a <4 higher bytes of key>`.

Maintenance commands and options:

- `update` retrieves the latest `versions.json` file from the repository and checks if a new version has to be downloaded. It can take several optional parameters as follows :
  - `--no-browser` to not automatically open the browser if a new version is available
  - `--proxy <uri>` to specify a web proxy for the requests
- `reset` resets the configuration file (`appsettings.json`) to its default state
- `--stack-trace` enable the stack trace print of errors in the terminal

### Examples

- `ZZZCutscenes -h` displays the help menu
- `ZZZCutscenes demux "[Game directory]\ZenlessZoneZero Game\ZenlessZoneZero_Data\StreamingAssets\VideoAssets\HD\Procedure" --output "./output" --merge --no-cleanup` will extract every USM file into the `output` directory, merge them with subtitles in a MKV file and will not cleanup the extracted files
- `ZZZCutscenes demux cutscenes/ -o ./output -m -e ffmpeg` will extract every USM file into the output directory, merging the files (`-m`) using FFMPEG (`-e`).
- `ZZZCutscenes demux hello.usm -b 00112233 -a 44556677` decrypts the file `hello.usm` with `key1=00112233` and `key2=44556677` and extracts the tracks.
- `ZZZCutscenes convert-hca hello_0.hca` decodes the file and converts it into a WAV file
- `ZZZCutscenes demux "[Path to .usm file]" --merge --audio-lang "jpn,eng"` convert single USM file, include only JPN and ENG audio tracks

The video is extracted as an IVF file (which makes codec detection (VP9) easier for mkvmerge). In order to watch it, you can open it into VLC or change the extension to `.m2v`.
