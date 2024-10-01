using System.Net;
using System.Text.Json;
using Ecommerce.Model.src.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Controller.src.Middleware
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex); // Handle any exception that occurs
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            var result = string.Empty;

            switch (exception)
            {
                case UnauthorizedActionException unauthorizedActionException:
                    code = HttpStatusCode.Forbidden; // 403 Unauthorized
                    result = JsonSerializer.Serialize(
                        new
                        {
                            error = string.IsNullOrEmpty(unauthorizedActionException.Message)
                                ? "Invalid credentials."
                                : unauthorizedActionException.Message
                        }
                    );
                    break;

                case AuthenticationException authenticationException:
                    code = HttpStatusCode.Unauthorized; // 401 Unauthorized for generic authentication failures
                    result = JsonSerializer.Serialize(
                        new
                        {
                            error = string.IsNullOrEmpty(authenticationException.Message)
                                ? "Authentication required. Please provide valid credentials."
                                : authenticationException.Message
                        }
                    );
                    break;

                case EntityNotFoundException entityNotFoundException:
                    code = HttpStatusCode.NotFound; // 404 Not Found
                    result = JsonSerializer.Serialize(
                        new { error = entityNotFoundException.Message }
                    );
                    break;

                case InvalidInputDataException invalidInputDataException:
                    code = HttpStatusCode.BadRequest; // 400 Bad Request
                    result = JsonSerializer.Serialize(
                        new { error = invalidInputDataException.Message }
                    );
                    break;
                case ResourceConflictException resourceConflictException:
                    code = HttpStatusCode.Conflict; // 409 Conflict
                    result = JsonSerializer.Serialize(
                        new { error = resourceConflictException.Message }
                    );
                    break;
                case InvalidQueryOptionException invalidQueryOptionException:
                    code = HttpStatusCode.BadRequest; // 400 Bad Request
                    result = JsonSerializer.Serialize(
                        new { error = invalidQueryOptionException.Message }
                    );
                    break;
                case OperationFailedException operationFailedException:
                    code = HttpStatusCode.BadRequest; // 400 Bad Request
                    result = JsonSerializer.Serialize(
                        new { error = operationFailedException.Message }
                    );
                    break;
                case DuplicateEntityException duplicateEntityException:
                    code = HttpStatusCode.Conflict; // 409 Conflict
                    result = JsonSerializer.Serialize(
                        new { error = duplicateEntityException.Message }
                    );
                    break;
                case RegistrationException registrationException:
                    code = HttpStatusCode.Conflict; // 409 Conflict
                    result = JsonSerializer.Serialize(
                        new { error = registrationException.Message }
                    );
                    break;
                case LoginException loginException:
                    code = HttpStatusCode.Unauthorized; // 401 Unauthorized
                    result = JsonSerializer.Serialize(new { error = loginException.Message });
                    break;
                case GeneralServerException generalServerException:
                    code = HttpStatusCode.InternalServerError; // 500 Internal Server Error
                    result = JsonSerializer.Serialize(
                        new
                        {
                            error = string.IsNullOrEmpty(generalServerException.Message)
                                ? "An unexpected error occurred. Please try again later."
                                : generalServerException.Message
                        }
                    );
                    break;
                case Exception generalException:
                    code = HttpStatusCode.InternalServerError; // 500 Internal Server Error
                    result = JsonSerializer.Serialize(
                        new
                        {
                            error = string.IsNullOrEmpty(generalException.Message)
                                ? "An unexpected error occurred. Please try again later."
                                : generalException.Message
                        }
                    );
                    break;

                default:
                    code = HttpStatusCode.InternalServerError;
                    result = JsonSerializer.Serialize(
                        new { error = "An unexpected error occurred. Please try again later." }
                    );
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
