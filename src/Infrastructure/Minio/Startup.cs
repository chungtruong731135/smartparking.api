using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TD.WebApi.Infrastructure.Minio;

internal static class Startup
{
    internal static IServiceCollection AddMinio(this IServiceCollection services, IConfiguration config) =>
        services.Configure<MinioSettings>(config.GetSection(nameof(MinioSettings)));
}