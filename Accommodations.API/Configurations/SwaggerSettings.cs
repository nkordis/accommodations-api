namespace Accommodations.API.Configurations
{
    public class SwaggerSettings
    {
        public string Version { get; } 
        public string Title { get; }
        public string Description { get; }
        public string Endpoint { get; }
        public string RoutePrefix { get; }

        public SwaggerSettings(string version, string title, string description, string endpoint, string routePrefix)
        {
            Version = version ?? throw new ArgumentNullException(nameof(version));
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
            RoutePrefix = routePrefix ?? throw new ArgumentNullException(nameof(routePrefix));
        }
    }
}
