
# Goodtocode.Assertion

Fluent Assertion aspect-oriented library for .NET Standard 2.0+

[![CI/CD Build, Test and Deploy](https://github.com/goodtocode/aspect-assertion/actions/workflows/gtc-assertion-ci-cd-nuget.yml/badge.svg)](https://github.com/goodtocode/aspect-assertion/actions/workflows/gtc-assertion-ci-cd-nuget.yml)

Goodtocode.Assertion is a .NET library providing fluent assertion and validation utilities for C# projects. It enables developers to write expressive, readable, and maintainable assertions for business logic, unit tests, and runtime validation. The library is designed for extensibility and can be integrated into any .NET Standard 2.0+ project.

## Features
- Fluent assertion syntax for clear, readable code
- Customizable assertion rules and exception handling
- Assertion scopes for grouping related checks
- Lightweight, dependency-free, and compatible with .NET Standard 2.0+
- Designed for use in both production code and unit tests

## Quick-Start Steps
1. Clone this repository
   ```
   git clone https://github.com/goodtocode/aspect-assertion.git
   ```
2. Install .NET SDK (latest recommended)
   ```
   winget install Microsoft.DotNet.SDK --silent
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
- [.NET SDK (latest)](https://dotnet.microsoft.com/en-us/download)
- Visual Studio (latest) or VS Code

## Top Use Case Examples

### 1. Basic Assertion Scope
```csharp
using Goodtocode.Assertion;

int value = 5;
AssertionScope.Begin()
	.Assert(() => value > 0, "Value must be positive.")
	.Assert(() => value < 10, "Value must be less than 10.")
	.End();
```

### 2. Validating Object Properties
```csharp
using Goodtocode.Assertion;

var user = new User { Name = "Alice", Age = 30 };
AssertionScope.Begin()
	.Assert(() => !string.IsNullOrWhiteSpace(user.Name), "Name is required.")
	.Assert(() => user.Age >= 18, "User must be an adult.")
	.End();
```

### 3. Custom Validator for Business Logic
```csharp
using Goodtocode.Assertion;

public class GetMyUsersPaginatedQueryValidator : Validator<GetMyUsersPaginatedQuery>
{
	public GetMyUsersPaginatedQueryValidator()
	{
		RuleFor(v => v.StartDate).NotEmpty()
			.When(v => v.EndDate != null)
			.LessThanOrEqualTo(v => v.EndDate);

		RuleFor(v => v.EndDate)
			.NotEmpty()
			.When(v => v.StartDate != null)
			.GreaterThanOrEqualTo(v => v.StartDate);

		RuleFor(x => x.PageNumber).NotEqual(0);
		RuleFor(x => x.PageSize).NotEqual(0);
	}
}
```

### 4. Unit Test Assertion
```csharp
using Goodtocode.Assertion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class AssertionTests
{
	[TestMethod]
	public void Should_Throw_When_Assertion_Fails()
	{
		Assert.ThrowsException<AssertionFailedException>(() =>
			AssertionScope.Begin()
				.Assert(() => false, "This should fail.")
				.End());
	}
}
```

## Technologies
- [C# .NET](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [.NET Standard](https://docs.microsoft.com/en-us/dotnet/standard/)

## Version History


| Version | Date        | Release Notes                                    |
|---------|-------------|--------------------------------------------------|
| 1.1.28  | 2026-Jan-20 | NuGet (semver) instead of file version.		   |
| 1.0.0   | 2026-Jan-19 | Initial release                                  |

## License

This project is licensed with the [MIT license](https://mit-license.org/).

## Contact
- [GitHub Repo](https://github.com/goodtocode/aspect-assertion)
- [@goodtocode](https://twitter.com/goodtocode)
