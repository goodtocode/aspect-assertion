using Goodtocode.Assertion;

namespace Goodtocode.Assertion.Tests;

[TestClass]
public sealed class AssertionScopeTests
{
    private enum CommandResponseType { Successful, Failed }

    [TestMethod]
    public void AssertionScopeSuccessfulResponseNoException()
    {
        // Arrange
        var responseType = CommandResponseType.Successful;
        Exception? exception = null;

        // Act & Assert
        using (new AssertionScope())
        {
            responseType.ShouldBe(CommandResponseType.Successful);
            exception.ShouldBeNull($"An exception was thrown: {exception?.Message}. Inner exception: {exception?.InnerException?.Message}");
        }
    }

    [TestMethod]
    public void AssertionScopeFailedResponseThrowsOnDispose()
    {
        // Arrange
        var responseType = CommandResponseType.Failed;
        Exception? exception = null;

        // Act & Assert
        Assert.ThrowsException<AssertionFailedException>(() =>
        {
            using (new AssertionScope())
            {
                responseType.ShouldBe(CommandResponseType.Successful);
                exception.ShouldBeNull($"An exception was thrown: {exception?.Message}. Inner exception: {exception?.InnerException?.Message}");
            }
        });
    }

    [TestMethod]
    public void AssertionScopeExceptionIsNotNullThrowsOnDispose()
    {
        // Arrange
        var responseType = CommandResponseType.Successful;
        Exception? exception = new InvalidOperationException("Test exception");

        // Act & Assert
        Assert.ThrowsException<AssertionFailedException>(() =>
        {
            using (new AssertionScope())
            {
                responseType.ShouldBe(CommandResponseType.Successful);
                exception.ShouldBeNull($"An exception was thrown: {exception?.Message}. Inner exception: {exception?.InnerException?.Message}");
            }
        });
    }

    [TestMethod]
    public void AssertionScopeFailedResponseAndExceptionThrowsOnDisposeWithBothMessages()
    {
        // Arrange
        var responseType = CommandResponseType.Failed;
        Exception? exception = new InvalidOperationException("Test exception", new ArgumentException("Inner"));

        // Act & Assert
        var ex = Assert.ThrowsException<AssertionFailedException>(() =>
        {
            using (new AssertionScope())
            {
                responseType.ShouldBe(CommandResponseType.Successful);
                exception.ShouldBeNull($"An exception was thrown: {exception?.Message}. Inner exception: {exception?.InnerException?.Message}");
            }
        });

        Assert.IsTrue(ex.Message.Contains("should be", StringComparison.InvariantCultureIgnoreCase));
    }
}
