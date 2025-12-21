<p align="left">
  <img src="https://raw.githubusercontent.com/Runneth-Over-Studio/UtilityTemplates/refs/heads/main/content/logo.png" width="175" alt="App Toolkit Logo">
</p>

# UtilityTemplates
Runneth Over Studio's .NET project templates (GUI / TUI) for in-house utilities.

## Purpose
UtilityTemplates provides a curated set of .NET project templates used internally at Runneth Over Studio to rapidly scaffold small, purpose-built utilities with a consistent structure and engineering standards.

The goal is not to be generic or exhaustive, but idiomatic: to reflect how we actually design, build, and ship internal tools across platforms, and to remove friction from spinning up new utilities while keeping quality and consistency high.

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
