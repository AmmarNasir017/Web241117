namespace WebAPIDemo.DAL
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;      //Class name = RequestDelegate, Delegate means work on behalf, premade class (underscore tells it is private variable) 
        private const string HeaderName = "X-API-KEY";      //Api headers always in capital letters
        private readonly string _apiKey;     //apikey variable

        // Constructor made for initialization

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _apiKey = configuration["ApiKey"];      //Array is used because its key and configuring with JSON file
                                                    //We use dependency injection to allow private members
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(HeaderName, out var extractedKey) || extractedKey != _apiKey)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid or missing API Key");
                return;
            }
            await _next(context);

            //We configured middleware here
        }
    }
}

 