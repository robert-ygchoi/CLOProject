using Application.Common.Behaviors;
using MediatR.Pipeline;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            cfg.AddOpenRequestPreProcessor(typeof(RequestLoggingBehavior<>));
            cfg.AddOpenRequestPostProcessor(typeof(ResponseLoggingBehavior<,>));
        });

        return services;
    }
}
