using Microsoft.EntityFrameworkCore;
namespace BackEnd.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã có lỗi xảy ra: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            int statusCode = StatusCodes.Status500InternalServerError;
            object errorResponse = new { error = "Đã có lỗi xảy ra trên server." };

            switch (exception)
            {
                case ArgumentNullException argNullEx:
                    {
                        statusCode = StatusCodes.Status400BadRequest;
                        errorResponse = new { error = "isnull", message = argNullEx.Message };
                        break;
                    }
                case ArgumentException argEx:
                    {
                        statusCode = StatusCodes.Status400BadRequest;
                        errorResponse = new { error = "wrong",message=argEx.Message };
                        break;
                    }

                case InvalidOperationException invOpEx:
                    {

                        statusCode = StatusCodes.Status400BadRequest;
                        errorResponse = new { error="unspecified", invOpEx.Message };
                        break;
                    }

                case KeyNotFoundException keyNotFoundEx:
                    {

                        statusCode = StatusCodes.Status404NotFound;
                        errorResponse = new { error = "key", keyNotFoundEx.Message };
                        break;
                    }
                case DbUpdateException dbUpdateEx:
                    {
                        statusCode = StatusCodes.Status409Conflict;
                        errorResponse = new { error = "ishas", message = "Dữ liệu vi phạm ràng buộc (có thể do trùng khóa hoặc thiếu trường bắt buộc)." };
                        break;
                    }

                default:
                    {
                        statusCode = StatusCodes.Status500InternalServerError;
                        errorResponse = new { error = "exception", message = exception.Message,ex=exception };
                        break;
                    }

            }

            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
