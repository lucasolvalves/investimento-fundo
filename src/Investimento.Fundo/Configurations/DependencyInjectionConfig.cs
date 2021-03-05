using Investimento.Fundo.Domain.Interfaces.Services;
using Investimento.Fundo.Domain.Services;
using Investimento.Fundo.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Investimento.Fundo.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IFundoService, FundoService>();
            services.AddScoped<IB3FundoService, B3FundoService>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpClient("mockinvestimento", options =>
            {
                options.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddPolicyHandler(PollyConfiguration.GetRetryPolicy());

            return services;
        }
    }
}
