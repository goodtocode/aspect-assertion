namespace Goodtocode.Assertion;

public static class AssertionRules
{
    public static void Should<T>(this T? obj, string? message = null)
    {
        if (obj is null)
            throw new AssertionFailedException(message ?? "Expected value should not be null.");
    }

    public static void ShouldNot<T>(this T? obj, string? message = null)
    {
        if (obj is not null)
            throw new AssertionFailedException(message ?? "Expected value should be null.");
    }

    public static void ShouldBe<T>(this T actual, T expected, string? message = null)
    {
        if (!EqualityComparer<T>.Default.Equals(actual, expected))
        {
            throw new AssertionFailedException(message ?? $"Expected value should be '{expected}', but was '{actual}'.");
        }
    }

    public static void ShouldBeTrue(this bool condition, string? message = null)
    {
        if (!condition)
            throw new AssertionFailedException(message ?? "Expected condition should be true.");
    }

    public static void ShouldBeFalse(this bool condition, string? message = null)
    {
        if (condition)
            throw new AssertionFailedException(message ?? "Expected condition should be false.");
    }

    public static void ShouldBeNull<T>(this T? obj, string? message = null)
    {
        if (obj is not null)
            throw new AssertionFailedException(message ?? "Expected value should be null.");
    }

    public static void ShouldNotBeNull<T>(this T? obj, string? message = null)
    {
        if (obj is null)
            throw new AssertionFailedException(message ?? "Expected value should not be null.");
    }

    public static void ShouldBeEmpty<T>(this T value, string? message = null) where T : struct
    {
        if (!EqualityComparer<T>.Default.Equals(value, default))
            throw new AssertionFailedException(message ?? "Expected value should be empty.");
    }

    public static void ShouldNotBeEmpty<T>(this T value, string? message = null) where T : struct
    {
        if (EqualityComparer<T>.Default.Equals(value, default))
            throw new AssertionFailedException(message ?? "Expected value should not be empty.");
    }
}

