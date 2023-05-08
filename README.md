![Artemis Loader](https://raw.githubusercontent.com/ArtemisDevGroup/Artemis-Resources/main/Text/ArtemisLoader.png)
# Overview
This repostitory contains two projects used for loading the Artemis Module into the game.

## Artemis-Loader
The Artemis-Loader project contains the entire UI as well as most of the back-end code for the UI.
This app is written using [.NET](https://dotnet.microsoft.com/en-us/) utilizing their Windows-Specific [Windows Presentation Foundation](https://github.com/dotnet/wpf) UI framework in XAML and C#.

## Artemis-Loader-Helper
The Artemis-Loader-Helper project is a helper project for the Loader, written as a [.NET](https://dotnet.microsoft.com/en-us/) C++/[CLI](https://learn.microsoft.com/en-us/cpp/dotnet/dotnet-programming-with-cpp-cli-visual-cpp) project.
This project contains all the code depending on the Windows-API library and other low level libraries, such as but not limited to; The dll injector, Named Pipe system, et cetera.
