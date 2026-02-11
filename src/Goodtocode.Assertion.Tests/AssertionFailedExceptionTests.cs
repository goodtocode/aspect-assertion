using Goodtocode.Assertion;

namespace Goodtocode.Assertion.Tests;

[TestClass]
public sealed class AssertionFailedExceptionTests
{
    [TestMethod]
    public void Constructor_SetsMessageProperty()
    {
        // Arrange
        var expectedMessage = "Assertion failed: Value cannot be null";

        // Act
        var exception = new AssertionFailedException(expectedMessage);

        // Assert
        Assert.AreEqual(expectedMessage, exception.Message);
    }

    [TestMethod]
    public void Constructor_WithEmptyMessage_CreatesException()
    {
        // Arrange
        var emptyMessage = string.Empty;

        // Act
        var exception = new AssertionFailedException(emptyMessage);

        // Assert
        Assert.IsNotNull(exception);
        Assert.AreEqual(emptyMessage, exception.Message);
    }

    [TestMethod]
    public void Exception_InheritsFromException()
    {
        // Arrange
        var exception = new AssertionFailedException("test");

        // Act & Assert
        Assert.IsInstanceOfType(exception, typeof(Exception));
    }

    [TestMethod]
    public void Exception_CanBeCaught_AsException()
    {
        // Arrange
        Exception? caughtException = null;

        // Act
        try
        {
            throw new AssertionFailedException("test exception");
        }
        catch (Exception ex)
        {
            caughtException = ex;
        }

        // Assert
        Assert.IsNotNull(caughtException);
        Assert.IsInstanceOfType(caughtException, typeof(AssertionFailedException));
    }

    [TestMethod]
    public void Exception_CanBeCaught_AsAssertionFailedException()
    {
        // Arrange
        AssertionFailedException? caughtException = null;

        // Act
        try
        {
            throw new AssertionFailedException("specific assertion error");
        }
        catch (AssertionFailedException ex)
        {
            caughtException = ex;
        }

        // Assert
        Assert.IsNotNull(caughtException);
        Assert.AreEqual("specific assertion error", caughtException.Message);
    }

    [TestMethod]
    public void Exception_ToString_ContainsMessage()
    {
        // Arrange
        var message = "Custom assertion message";
        var exception = new AssertionFailedException(message);

        // Act
        var result = exception.ToString();

        // Assert
        Assert.IsTrue(result.Contains(message));
        Assert.IsTrue(result.Contains(nameof(AssertionFailedException)));
    }
}
