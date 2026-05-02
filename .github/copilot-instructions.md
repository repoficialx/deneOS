# deneOS — Workspace Instructions

## Project Overview

**deneOS** is a lightweight mini-OS that runs on top of Windows, providing a customizable shell interface without the typical Windows desktop.

- **Current Version**: v0.2 (beta), targeting v1.0 by 2026–2027
- **Language**: C# on .NET (multi-version: 8, 9, 10)
- **Target Platform**: Windows 10/11 (x64)

## Architecture

The solution is organized into **3 logical component groups**:

| Group | Purpose | Key Projects |
|-------|---------|--------------|
| **Home** | Core system shell and settings | `deneOS`, `controlcenter`, `deneOS Launcher` |
| **External Apps** | Satellite utilities and integrations | `deneNotes`, `deneFiles`, `deneAI`, `Calendar.IO`, `Internet` |
| **System Infrastructure** | OS abstractions and internal services | `dosu.*`, `dpkxt` (package mgr), `Terminal`/`deneTerm` |

**To be productive**: Start with `deneOS/` for shell logic, `dosu.System` + `dosu.UI` for cross-project utilities, then individual apps.

## Technology Stack

- **UI Framework**: Primarily Windows Forms; WPF for Calendar.IO
- **Key Dependencies**: NAudio, BCrypt.Net, ManagedNativeWifi
- **Packaging Formats**: .exe, .wpi (custom), .dpk (package manager), .dna (upcoming)
- **Build System**: dotnet CLI + MSBuild; CI/CD via GitHub Actions

## Build & Deployment

### Common Commands

```bash
# Build full solution (Debug)
dotnet build deneOS.sln

# Build specific project
dotnet build deneOS/deneOS.csproj

# Build Release with self-contained publishing
dotnet build deneOS.sln -c Release

# Run unit tests
dotnet test deneOS/

# Publish standalone .exe
dotnet publish deneOS/deneOS.csproj -c Release -p:PublishSingleFile=true -p:SelfContained=true
```

### CI/CD Pipeline

- **Trigger**: Push to `master` branch
- **Workflow**: `.github/workflows/dotnet-desktop.yml`
- **Runs On**: Windows latest (windows-latest runner)
- **Steps**: Restore dependencies → Build (Debug + Release) → Run tests (if configured)

**Note**: `deneTerm` (C++ project) is excluded from dotnet builds via `Directory.Build.props`; it requires separate MSVC tooling.

## Code Conventions

### Namespacing
One namespace per project, matching folder/project name:
```csharp
namespace deneOS { }
namespace Internet { }
namespace dosu.System { }
```

### UI Classes  
- Use `partial` class pattern for Designer-generated code
- Inherit from `DesktopBase` or standard `Form`
- Example: `public partial class desktop : DesktopBase { }`

### Global State  
Centralized via static helper classes:
```csharp
public static class globaldata
{
    public static Color WallpaperColor { get; set; }
    // config, flags, shared app state...
}
```

### Naming Patterns
- **Classes**: PascalCase (e.g., `EmergencyScreen`, `DesktopLoader`)
- **Private fields**: `_camelCase` or `camelCase`
- **Constants**: `UPPER_SNAKE_CASE`

## File Organization

| File/Folder | Purpose |
|-------------|---------|
| `deneOS.sln` | Root solution with 25+ projects |
| `deneOS/Program.cs` | Main entry point; initializes shell |
| `deneOS/DesktopBase.cs` | Abstract base class for custom desktop environments |
| `deneOS/globaldata.cs` | Global config and state management |
| `dosu/` | Shared system utilities used across projects |
| `dosu.System/` | OS-level abstractions (network, admin, flags) |
| `dosu.UI/` | UI reusable components and helpers |
| `Directory.Build.props` | Project-wide settings (currently excludes deneTerm) |

## Common Development Tasks

### Adding a New App to deneOS
1. Create a new project (copy structure from `deneNotes/` or `deneFiles/`)
2. Add reference to `dosu.System` + `dosu.UI` if needed
3. Implement main entry point in `Program.cs`
4. Update `deneOS.sln`
5. If packaging as `.dpk`, add manifest to package manager

### Debugging
- Launch deneOS: Run `deneOS/Program.cs` via IDE debugger
- Logs: Check `globaldata` + static config state
- Common issues: File permissions (use `AdminHelper.cs`), UI threading (use main dispatcher)

### Cross-Project Sharing
Use `dosu.*` libraries:
- **dosu.System**: Networking, admin operations, file paths, global flags
- **dosu.UI**: Common dialogs, form utilities, theming hooks

## Design Patterns

### Desktop Environment Pattern
`DesktopBase` abstract class provides a template for different OS "skins":
```csharp
public abstract class DesktopBase : Form
{
    // Derives: desktop, EmergencyScreen, custom environments
}
```
Apps inherit from this to customize the shell environment.

### Module/App Pattern
Each external app (deneNotes, deneFiles, etc.) is **self-contained**:
- Separate .csproj
- Shared dependencies via `dosu.*`
- Runnable as standalone .exe
- Installable via package manager

## Troubleshooting & Common Pitfalls

- **deneTerm is C++**: Does not build with `dotnet build deneOS.sln` — requires Visual Studio C++ tools
- **File Access Denied**: Use `AdminHelper.cs` for privileged operations
- **UI Unresponsiveness**: Avoid blocking main thread; use `Task.Run()` for long-running ops
- **Multi-version .NET**: Projects target different .NET versions; build individually if cross-version issues occur

## Related Documentation

- [README.md](../README.md) — User-facing project info
- [.github/workflows/](../.github/workflows/) — CI/CD pipeline details
- Component folders: Each major project (e.g., `Internet/`, `Calendar.IO/`) may have local README or inline documentation

---

**Last Updated**: April 2026  
For questions or updates, see the repository root README or source comments.
