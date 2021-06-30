using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Account.Domain.AggregatesModel.UserAggregates;
using Account.Infrastructure;
using Account.Infrastructure.Repositories;
using Account.Domain.Seedwork;
using Account.API.Application.Queries;
using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4.Test;
using Microsoft.IdentityModel.Tokens;

namespace Account.API.Application.DI
{
    public class Initializer
    {
        public static void Configure(IServiceCollection services, string conection)
        {
            services.AddIdentityServer()
                    .AddInMemoryClients(Config.Clients)
                     .AddInMemoryIdentityResources(Config.IdentityResources)
                     .AddInMemoryApiResources(Config.ApiResources)
                     .AddInMemoryApiScopes(Config.ApiScopes)
                     .AddTestUsers(Config.TestUsers)
                    .AddDeveloperSigningCredential();

            services.AddAuthentication("Bearer")
                    .AddJwtBearer("Bearer", options =>
                    {
                        options.Authority = "https://localhost:5001";
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateAudience = false
                         };
                    });
            services.AddDbContext<AccountContext>(options => options.UseNpgsql(conection), ServiceLifetime.Scoped);
            services.AddScoped(typeof(IRepository<User>), typeof(UserRepository));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped(typeof(UserRepository));
            services.AddMediatR(typeof(Startup));
            services.AddTransient<IUserQueries, UserQueries>().AddSingleton(conection);
        }
    }
}
