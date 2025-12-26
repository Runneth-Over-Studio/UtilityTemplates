![UtilityTemplates Logo](https://raw.githubusercontent.com/Runneth-Over-Studio/UtilityTemplates/refs/heads/main/content/icon-175.png)

# UtilityTemplates
Runneth Over Studio's .NET project templates (GUI / TUI) for in-house utilities.

## Purpose
UtilityTemplates provides a curated set of .NET project templates used internally at Runneth Over Studio to rapidly scaffold small, purpose-built utilities with a consistent structure and engineering standards.

The goal is not to be generic or exhaustive, but idiomatic: to reflect how we actually design, build, and ship internal tools across platforms, and to remove friction from spinning up new utilities while keeping quality and consistency high.

## The Templates
There is a template for a desktop GUI application ([Avalonia](https://github.com/AvaloniaUI/Avalonia)) and a terminal user interface application ([RazorConsole](https://github.com/RazorConsole/RazorConsole)). Both templates contain:

- **Samples** - Basic business functionality and views to demonstrate usages and get running faster
- **Cross-Platform** - Runs on Windows, macOS, and Linux
- **Three-Tier Architecture** - Data → Business → Presentation layers
- **MVVM Design Pattern** - Presentation layer with clean separation of concerns using CommunityToolkit.Mvvm
- **Dependency Injection** - Built-in DI container for testable, maintainable code
- **Convention-Based Routing** - ViewModel driven navigation between views

This consistency allows developers to switch between GUI and TUI projects seamlessly, reusing skills and patterns across both.

<!--
## Use
- Install all templates with one command
	```powershell
	dotnet new install RunnethOverStudio.UtilityTemplates
	```

- List installed templates
  ```powershell
	dotnet new list
	```

- Use either template
	```powershell
  dotnet new ros.guiapp -n MyGuiApp
  dotnet new ros.tuiapp -n MyTuiApp
	```

- Uninstall when needed
  ```powershell
  dotnet new uninstall RunnethOverStudio.UtilityTemplates
  ```
-->

## Versioning
This project uses [Semantic Versioning](https://semver.org/).

- **MAJOR** version: Incompatible API changes
- **MINOR** version: Backward-compatible functionality
- **PATCH** version: Backward-compatible bug fixes

## Build Requirements
- All projects target the LTS version of the [.NET SDK](https://dotnet.microsoft.com/en-us/download).
- The Build project uses [Cake](https://cakebuild.net/) (C# Make) as the build orchestrator and can be launched from your IDE or via script.

	- On OSX/Linux run:
	```bash
	./build.sh
	```
	- If you get a "Permission denied" error, you may need to make the script executable first:
	```bash
	chmod +x build.sh
	```

	- On Windows PowerShell run:
	```powershell
	./build.ps1
	```
