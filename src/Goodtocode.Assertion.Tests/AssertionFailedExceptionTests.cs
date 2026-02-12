using Goodtocode.Assertion;

namespace Goodtocode.Assertion.Tests;

[TestClass]
public sealed class AssertionFailedExceptionTests
{
    [TestMethod]
    public void ConstructorSetsMessageProperty()
    {
        // Arrange
        var expectedMessage = "Assertion failed: Value cannot be null";

        // Act
        var exception = new AssertionFailedException(expectedMessage);

        // Assert
        Assert.AreEqual(expectedMessage, exception.Message);
    }

    [TestMethod]
    public void ConstructorWithEmptyMessageCreatesException()
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
    public void ExceptionInheritsFromException()
    {
        // Arrange
        var exception = new AssertionFailedException("test");

        // Act & Assert
        Assert.IsInstanceOfType(exception, typeof(Exception));
    }

    [TestMethod]
    public void ExceptionCanBeCaughtAsException()
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
    public void ExceptionCanBeCaughtAsAssertionFailedException()
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
    public void ExceptionToStringContainsMessage()
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
