using Authentification.JWT.Service.Services.Interfaces;
using System.Net;
using System.Text.Json;

namespace Authentification.JWT.WebAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILog _logger;

        public ExceptionMiddleware(RequestDelegate next, ILog logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.Error("Erreur 401 - Accès non autorisé", ex);
                await WriteResponseAsync(context, HttpStatusCode.Unauthorized, "Accès non autorisé.");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.Error("Erreur 404 - Élément non trouvé", ex);
                await WriteResponseAsync(context, HttpStatusCode.NotFound, "Élément non trouvé.");
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur 500 - Exception non gérée", ex);
                await WriteResponseAsync(context, HttpStatusCode.InternalServerError, "Une erreur inattendue est survenue.");
            }
        }

        private Task WriteResponseAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                status = (int)statusCode,
                message
            };

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }
}
