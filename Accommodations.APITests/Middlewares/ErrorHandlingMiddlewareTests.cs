using Accommodations.Domain.Entities;
using Accommodations.Domain.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Accommodations.API.Middlewares.Tests
{
    public class ErrorHandlingMiddlewareTests
    {

        private readonly DefaultHttpContext _context;
        private readonly Mock<ILogger<ErrorHandlingMiddleware>> _loggerMock;
        private readonly ErrorHandlingMiddleware _middleware;

        public ErrorHandlingMiddlewareTests()
        {
            _context = new DefaultHttpContext();
            _loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            _middleware = new ErrorHandlingMiddleware(_loggerMock.Object);
        }


        [Fact()]
        public async Task InvokeAsync_WhenNoExceptionsThrown_ShouldCallNextDelegate()
        {
            // Arrange
            var nextDelegateMock = new Mock<RequestDelegate>();

            // Act
            await _middleware.InvokeAsync(_context, nextDelegateMock.Object);

            // Assert
            nextDelegateMock.Verify(next => next.Invoke(_context), Times.Once);
        }


        [Fact()]
        public async void InvokeAsync_WhenNotFoundExceptionThrown_ShouldSetStatusCode404()
        {
            // Arrange
            var notFoundException = new NotFoundException(nameof(Accommodation), "1");

            // Act
            await _middleware.InvokeAsync(_context, _ => throw notFoundException);

            // Assert
            _context.Response.StatusCode.Should().Be(404);
        }

        [Fact()]
        public async void InvokeAsync_WhenForbidExceptionThrown_ShouldSetStatusCode403()
        {
            // Arrange
            var forbidException = new ForbidException();

            // Act
            await _middleware.InvokeAsync(_context, _ => throw forbidException);

            // Assert
            _context.Response.StatusCode.Should().Be(403);
        }

        [Fact()]
        public async void InvokeAsync_WhenGenericExceptionThrown_ShouldSetStatusCode500()
        {
            // Arrange
            var exception = new Exception();

            // Act
            await _middleware.InvokeAsync(_context, _ => throw exception);

            // Assert
            _context.Response.StatusCode.Should().Be(500);
        }
    }
}