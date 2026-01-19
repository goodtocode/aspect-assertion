using Goodtocode.Assertion;

namespace Goodtocode.Assertion.Tests;

[TestClass]
public sealed class AssertionTests
{
    [TestMethod]
    public void ShouldWithNonNullDoesNotThrow()
    {
        // Arrange
        var value = "test";

        // Act & Assert
        value.Should();
    }

    [TestMethod]
    public void ShouldWithNullThrows()
    {
        // Arrange
        string? value = null;

        // Act & Assert
        Assert.ThrowsException<AssertionFailedException>(() => value.Should());
    }

    [TestMethod]
    public void ShouldNotWithNullDoesNotThrow()
    {
        // Arrange
        string? value = null;

        // Act & Assert
        value.ShouldNot();
    }

    [TestMethod]
    public void ShouldNotWithNonNullThrows()
    {
        // Arrange
        var value = "not null";

        // Act & Assert
        Assert.ThrowsException<AssertionFailedException>(() => value.ShouldNot());
    }

    [TestMethod]
    public void ShouldBeWithEqualValuesDoesNotThrow()
    {
        // Arrange
        int actual = 5;
        int expected = 5;

        // Act & Assert
        actual.ShouldBe(expected);
    }

    [TestMethod]
    public void ShouldBeWithNotEqualValuesThrows()
    {
        // Arrange
        int actual = 5;
        int expected = 6;

        // Act & Assert
        Assert.ThrowsException<AssertionFailedException>(() => actual.ShouldBe(expected));
    }

    [TestMethod]
    public void ShouldBeTrueWithTrueDoesNotThrow()
    {
        // Arrange
        bool condition = true;

        // Act & Assert
        condition.ShouldBeTrue();
    }

    [TestMethod]
    public void ShouldBeTrueWithFalseThrows()
    {
        // Arrange
        bool condition = false;

        // Act & Assert
        Assert.ThrowsException<AssertionFailedException>(() => condition.ShouldBeTrue());
    }

    [TestMethod]
    public void ShouldBeFalseWithFalseDoesNotThrow()
    {
        // Arrange
        bool condition = false;

        // Act & Assert
        condition.ShouldBeFalse();
    }

    [TestMethod]
    public void ShouldBeFalseWithTrueThrows()
    {
        // Arrange
        bool condition = true;

        // Act & Assert
        Assert.ThrowsException<AssertionFailedException>(() => condition.ShouldBeFalse());
    }

    [TestMethod]
    public void ShouldBeNullWithNullDoesNotThrow()
    {
        // Arrange
        object? value = null;

        // Act & Assert
        value.ShouldBeNull();
    }

    [TestMethod]
    public void ShouldBeNullWithNonNullThrows()
    {
        // Arrange
        object value = new();

        // Act & Assert
        Assert.ThrowsException<AssertionFailedException>(() => value.ShouldBeNull());
    }

    [TestMethod]
    public void ShouldNotBeNullWithNonNullDoesNotThrow()
    {
        // Arrange
        object value = new();

        // Act & Assert
        value.ShouldNotBeNull();
    }

    [TestMethod]
    public void ShouldNotBeNullWithNullThrows()
    {
        // Arrange
        object? value = null;

        // Act & Assert
        Assert.ThrowsException<AssertionFailedException>(() => value.ShouldNotBeNull());
    }

    [TestMethod]
    public void ShouldBeEmptyWithDefaultStructDoesNotThrow()
    {
        // Arrange
        int value = default;

        // Act & Assert
        value.ShouldBeEmpty();
    }

    [TestMethod]
    public void ShouldBeEmptyWithNonDefaultStructThrows()
    {
        // Arrange
        int value = 1;

        // Act & Assert
        Assert.ThrowsException<AssertionFailedException>(() => value.ShouldBeEmpty());
    }

    [TestMethod]
    public void ShouldNotBeEmptyWithNonDefaultStructDoesNotThrow()
    {
        // Arrange
        int value = 1;

        // Act & Assert
        value.ShouldNotBeEmpty();
    }

    [TestMethod]
    public void ShouldNotBeEmptyWithDefaultStructThrows()
    {
        // Arrange
        int value = default;

        // Act & Assert
        Assert.ThrowsException<AssertionFailedException>(() => value.ShouldNotBeEmpty());
    }
}
