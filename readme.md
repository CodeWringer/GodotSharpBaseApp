# Godot C# Base App
An opinionated quick-start template for Godot projects using C# and aiming not to deliver games, but rich desktop applications. 

## What's in the box?
* A C# ready setup. 
* A unit-test C# project is included. 
* A directory and namespace structure that promotes a separation of the presentation and business layers. 
  * All presentation logic is what's happening inside of Godot's systems. Godot artifacts are separated by type by default and bundled together when reasonable. For example, components are structured so as to be as self-contained as possible. 
  * All actual business logic is to be handled by C#. 
* An input validation framework, with working examples. 
* A data source framework, with working example. 
  * This is a suggestion for how to read and write data from and to disk. 
* The 'Roboto' Google font. All credit goes to their respective owners. Its license is contained in the font's directory. 

## Prerequisites
While this project is designed to include all the necessary parts for a running start, it cannot provide _all_ the pieces. 

1. Make sure to have read and understood the official Godot tutorial: https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_basics.html
2. This project requires the .NET Framework v4.7.2 to **run**. 
3. This project has been set up with the `Godot_v3.5.1-stable_mono_win64` Godot binaries. 
   1. The exact version _shouldn't_ matter, but in case you run into odd issues not outlined here, make sure to test it with this exact version also. 
4. In case you're not working with Visual Studio, you may need to invoke NuGet manually, to get any required dependencies downloaded. 

## Getting started
After having cloned this repository, you should be able to build and run the project right away. Then, make it your own:

1. Change out the `app/icon.png` image for your own. 
2. Change the name of the project to your own. For that, in Godot, navigate to `Project > Project Settings > Application > config` and change the name to your preference. Be mindful that changing the name affects several other things in Godot, such as the name of the user-specific data directory. 
3. If you're working with Visual Studio, the solution file can be found in `app` and is called `app.sln`. 
4. Write your model of business data, inside the `app/business/model` directory and integrate your model into the `app/business/state/ApplicationState.cs` class. 
5. Define the application-level settings as fields inside the `app/business/state/ApplicationSettings.cs` class. 

## Gotchas
* Due to how Godot automatically adds the contents of the Godot project to the Godot associated C# project, it is best practice to **avoid** adding C# sub-projects within the directory structure of the Godot project. 
  * In the case of this template, **avoid** adding sub-projects inside of the 'app' directory. Instead, place them one level up in the root of the cloned directory structure. 
* In order to get NuGet to work with Godot's C# project setup _at all_, it was necessary to add a 'nuget.config' file to the root directory of the 'app' C# project. 
  * The essential problem with Godot C# projects as they come out-of-the-box (from Godot version 3.3.0 and up), is that they require the `Godot.NET.Sdk/3.3.0` sdk to work. This sdk is supposed to be downloaded by NuGet. However NuGet doesn't seem to know where to get it  from, by default. 
  * This issue is separate from Visual Studio - it is NuGet itself, which isn't playing nice. 
  * For more details, see this git issue: https://github.com/godotengine/godot/issues/58955