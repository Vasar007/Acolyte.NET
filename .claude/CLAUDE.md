# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**Acolyte.NET** — a helper library of classes, static helpers, and extension methods for everyday .NET work. Published as three NuGet packages: `Acolyte.CSharp`, `Acolyte.FSharp`, and the umbrella `Acolyte.NET` (which depends on the other two).

## Repo Layout Quirk

This repo holds **two solutions**, both nested one level below the repo root:

- `Acolyte.NET/Acolyte.NET.sln` — the package solution; the only one CI builds and ships.
- `Acolyte.Examples/Acolyte.Examples.sln` — consumer examples (currently `Acolyte.Examples.ConsoleApp`). CI does **not** build this; it depends on the published NuGet package version pinned in `Acolyte.Examples/Directory.Build.props` (`AcolyteNETPackageVersion`), not on project references.

`dotnet` commands accept the folder name (e.g. `dotnet restore Acolyte.NET`) — that resolves to the `.sln` inside.

| Path                                          | What lives there                                                   |
|-----------------------------------------------|--------------------------------------------------------------------|
| `Acolyte.NET/Libraries/Acolyte.CSharp/`       | C# extension methods and helpers (the bulk of the library)         |
| `Acolyte.NET/Libraries/Acolyte.FSharp/`       | F# functional helpers (`.fsproj`)                                  |
| `Acolyte.NET/Libraries/Acolyte.NET/`          | Umbrella meta-package that bundles the C# + F# libs                |
| `Acolyte.NET/Tests/Acolyte.CSharp.Tests/`     | xUnit + Moq tests for the C# lib                                   |
| `Acolyte.NET/Tests/Acolyte.FSharp.Tests/`     | xUnit + Unquote tests for the F# lib (`.fsproj`)                   |
| `Acolyte.NET/Tests/Acolyte.Tests.Common/`     | Shared test helpers (referenced by both test projects)             |
| `Acolyte.Examples/`                           | Standalone example solution; not built by CI                       |
| `Media/`                                      | Package icon assets (included in the .nupkg)                       |

## Build & Test

```shell
dotnet restore Acolyte.NET
dotnet build   Acolyte.NET/Acolyte.NET.sln                                    # full solution
dotnet build   Acolyte.NET/Libraries/Acolyte.CSharp/Acolyte.CSharp.csproj     # single project
dotnet build   Acolyte.NET -c Release
dotnet pack    Acolyte.NET --configuration Release --no-build                 # produces the three .nupkg files
dotnet test    Acolyte.NET/Acolyte.NET.sln                                    # C# tests via the .sln
dotnet test    Acolyte.NET/Acolyte.NET.sln --filter "FullyQualifiedName~MyTestMethod"
# F# tests do NOT run via the .sln — invoke the .fsproj directly (mirror what CI does in after_test):
dotnet test    Acolyte.NET/Tests/Acolyte.FSharp.Tests/Acolyte.FSharp.Tests.fsproj
# Style check (same command CI runs; needs the dotnet-format global tool):
dotnet format  Acolyte.NET/Acolyte.NET.sln --fix-whitespace --fix-style warn --check --verbosity diagnostic
```

- **Solution platform**: `AnyCPU` only (the `.sln` also lists `x64`/`x86` configurations but every project maps them back to `Any CPU`). Do **not** assume the ProjectV-style "no AnyCPU" rule applies here — it's the opposite.
- **Solution configurations**: `Debug` and `Release` only. CI builds both.
- **F# test discovery quirk**: `dotnet test` against the solution silently skips `Acolyte.FSharp.Tests` — always run the `.fsproj` separately when validating F# changes. The appveyor `test:` step also misses them; CI compensates in `after_test`.

## Stack & Versions

- **Target frameworks** (centralized in `Acolyte.NET/Directory.Build.props`):
  - Libraries: `netstandard2.0;netstandard2.1` (`PackageTargetFrameworks`)
  - Tests: `net472;netcoreapp3.1;net5.0` (`TestTargetFrameworks`) — yes, plural; tests multi-target to validate every supported consumer runtime.
- **Languages**: C# `9.0`, F# `5.0` (`CSharpLangVersion` / `FSharpLangVersion` from `Directory.Build.props`).
- `Nullable=enable`, `TreatWarningsAsErrors=true`. Do **not** suppress warnings to make a build pass — fix the root cause. Existing `NoWarn` entries are scoped to documentation warnings on Release builds only.
- **Package version pins**: `Acolyte.NET/Directory.Build.props` holds NuGet versions as MSBuild properties (e.g. `<XunitPackageVersion>2.4.1</XunitPackageVersion>`), and `.csproj`/`.fsproj` files reference them via `Version="$(XunitPackageVersion)"`. This is **not** Central Package Management (no `Directory.Packages.props`); add new packages by introducing a new `*PackageVersion` property here, then reference it from the project file.
- **NuGet feed**: `nuget.org` only (`Acolyte.NET/NuGet.Config`). No private feeds.
- **Code style**: `Acolyte.NET/.editorconfig` is authoritative. CI enforces it via `dotnet format ... --check`.
- **Repo-wide `dotnet-format` version**: pinned in `appveyor.yml` (`dotnetFormatVersion`). If you bump it, change CI accordingly.

## Packaging

The shipped artifact is a zip (`Acolyte.NET.<version>.zip`) bundling the three `.nupkg` files plus the GitHub Release. Package metadata (Authors, Version, PackageTags, license, icon) is duplicated in each library `.csproj`/`.fsproj` — when bumping a version, change **every** `<AssemblyVersion>`, `<FileVersion>`, `<Version>` in the three library projects **and** the `packageVersion` in `appveyor.yml`. AppVeyor's `dotnet_csproj.patch` also rewrites these at build time from `packageVersion`, but the in-file values are still what local builds and `dotnet pack` use.

The `Acolyte.Examples` solution has its own version knob (`AcolyteNETPackageVersion` in `Acolyte.Examples/Directory.Build.props`) — bump it after a new package is published if you want the examples to consume the latest release.

## Branch & PR Model

- `master` — release branch. AppVeyor pushes to NuGet and GitHub Releases only on `master` + `Release` configuration. Tag pushes are explicitly skipped (`skip_tags: true`); the GitHub Release provider creates the `v<version>` tag.
- `develop` — integration branch. Dependabot opens NuGet PRs against `develop` daily on weekdays (`.github/dependabot.yml`), pre-assigned/reviewed by `Vasar007`, labelled `area: Dependencies`, `type: Code Maintenance`, `status: External`.
- For new work, branch from `master` (or `develop` if continuing in-flight integration work) and follow `.github/pull_request_template.md` — it requires linking an issue first and a test plan; PRs without a prior issue may be rejected.

## CI

- **AppVeyor** (`appveyor.yml`) — `Visual Studio 2019` image, builds both `Debug` and `Release` of `Acolyte.NET/Acolyte.NET.sln`. Test phase: xUnit assemblies via the `**\*.Tests.dll` pattern + the F# `.fsproj` step + `dotnet format --check`. Deploy step (NuGet + GitHub Release) gated on `branch == master && configuration == Release`.
- **GitHub CodeQL** (`.github/workflows/codeql-analysis.yml`) — C# analysis on push/PR for `master`/`develop` (PR target only `master`) and weekly Sunday 03:00 UTC schedule. Runs on `ubuntu-latest`.

## Skills That Apply Here

This is a C# + F# library repo, so a subset of the personal-marketplace `dotnet-backend-dev` skills applies — primarily `generate-xml-docs`, `create-exception`, `create-primitive-wrapper`, and `create-tests` (xUnit / Moq / Unquote — not NUnit / FluentAssertions, despite the skill description). The repo does **not** use NSwag, EF Core, Mapperly, ASP.NET Core, NSwag, Clean-Architecture-with-DDD layering, configuration options, session logging, or external service clients — do not apply those skills or force their patterns into this codebase. Public API surface should keep its existing extension-method / static-helper shape.

## Things Not To Do

- Don't introduce `Directory.Packages.props` / Central Package Management — package versions live as MSBuild properties in `Acolyte.NET/Directory.Build.props` and the team has not migrated.
- Don't add new feeds to `NuGet.Config`; this library is intentionally `nuget.org`-only.
- Don't change `<TargetFrameworks>` on individual projects directly; bump the centralized properties in `Acolyte.NET/Directory.Build.props` so every project picks it up.
- Don't try to fix "F# tests not running" by reconfiguring the `.sln` — the workaround is the separate `dotnet test` invocation in `after_test`; preserve it.
- Don't strip `AnyCPU` from the solution / project configurations. (The ProjectV repo forbids `AnyCPU`; this repo *requires* it. Don't carry rules across repos.)
- Don't commit `CLAUDE.local.md` or `settings.local.json` anywhere in the tree — both are gitignored repo-wide for per-user Claude Code overrides.
