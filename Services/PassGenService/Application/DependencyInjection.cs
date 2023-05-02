using Application.Passwords.Create;
using MediatR;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //var assembly = typeof(DependencyInjection).Assembly;

        //services.AddMediatR(configuration =>
        //    configuration.RegisterServicesFromAssembly(assembly));

        //services.AddValidatorsFromAssembly(assembly);

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();

            config.NotificationPublisher = new TaskWhenAllPublisher();
        });

        //services.AddScoped<CreatePasswordCommandHandler>();
        //services.AddScoped<CheckGeneratedPasswordHandler>();
        //services.AddScoped<PasswordCreateSaga>();

        return services;
    }
}