using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection serivces)
    {
        serivces.AddMediatR(Assembly.GetExecutingAssembly());

        return serivces;
    }
}
