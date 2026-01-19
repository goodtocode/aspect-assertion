# aspect-assertion
Fluent Assertion aspect oriented library
# Goodtocode.Assertion

**CI/CD Build Status**

[![.github/workflows/ci-cd.yml](https://github.com/goodtocode/aspect-assertion/actions/workflows/ci-cd.yml/badge.svg)](https://github.com/goodtocode/aspect-assertion/actions/workflows/ci-cd.yml)

Goodtocode.Assertion is a .NET library providing fluent assertion and validation utilities for C# projects. It enables developers to write expressive, readable, and maintainable assertions for business logic, unit tests, and runtime validation. The library is designed for extensibility and can be integrated into any .NET project.

## Features
* Fluent assertion syntax for clear, readable code
* Customizable assertion rules and exception handling
* Assertion scopes for grouping related checks
* Lightweight, dependency-free, and compatible with .NET Standard 2.0+ and .NET 9
* Designed for use in both production code and unit tests

## Quick-Start Steps
1. Clone this repository
	```
	git clone https://github.com/goodtocode/aspect-assertion.git
	```
2. Install .NET SDK (9.0 or compatible)
	```
	winget install Microsoft.DotNet.SDK.9 --silent
	```
3. Build the solution
	```
	cd src
	dotnet build Goodtocode.Assertion.sln
	```
4. Run tests
	```
	cd Goodtocode.Assertion.Tests
	dotnet test
	```

## Install Prerequisites
* [.NET SDK 9+](https://dotnet.microsoft.com/en-us/download)
* Visual Studio 2022 or VS Code

## Usage Example
```csharp
using Goodtocode.Assertion;

public void Example()
{
	 int value = 5;
	 AssertionScope.Begin()
		  .Assert(() => value > 0, "Value must be positive.")
		  .Assert(() => value < 10, "Value must be less than 10.")
		  .End();
}
```

## Technologies
* [C# .NET](https://docs.microsoft.com/en-us/dotnet/csharp/)
* [.NET Standard](https://docs.microsoft.com/en-us/dotnet/standard/)

## Version History

| Version | Date        | Release Notes                                    |
|---------|-------------|--------------------------------------------------|
| 1.0.0   | 2026-Jan-19 | Initial release                                  |

## License

This project is licensed with the [MIT license](https://mit-license.org/).

## Contact
* [GitHub Repo](https://github.com/goodtocode/aspect-assertion)
* [@goodtocode](https://twitter.com/goodtocode)
