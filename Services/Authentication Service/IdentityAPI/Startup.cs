using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityAPI.Repository;
using IdentityAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SimpleCommerce.Core.Domain;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
namespace IdentityAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //AuthConfigurer.Configure(services, Configuration);

            AddJWTOptions(services);

            AddJWTBearerSwaggerConfiguartion(services);

            services.AddTransient<IUserService, FakeUserService>();
            services.AddTransient<IUserRepository, FakeUserRepository>();

        }

        private void AddJWTOptions(IServiceCollection services)
        {
            services.AddSingleton<TokenAuthConfiguration>(x =>
            {
                var tokenConfig = new TokenAuthConfiguration()
                {
                    SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Authentication:JwtBearer:SecurityKey"])),
                    Issuer = Configuration["Authentication:JwtBearer:Issuer"],
                    Audience = Configuration["Authentication:JwtBearer:Audience"],
                    Expiration = TimeSpan.FromDays(1)
                };
                tokenConfig.SigningCredentials = new SigningCredentials(tokenConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
                return tokenConfig;
            });
        }

        private static void AddJWTBearerSwaggerConfiguartion(IServiceCollection services)
        {
            var security = new Dictionary<string, IEnumerable<string>>
            {
                { "ApiKeyAuth", new string[]{ } },
            };

            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("v1", new Info { Title = "Identity API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                // Define the BearerAuth scheme that's in use
                options.AddSecurityDefinition("ApiKeyAuth", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                options.AddSecurityRequirement(security);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    var error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;

                    //when authorization has failed, should retrun a json message to client
                    if (error != null && error.Error is SecurityTokenExpiredException)
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            State = "Unauthorized",
                            Msg = "token expired"
                        }));
                    }
                    //when orther error, retrun a error message json to client
                    else if (error != null && error.Error != null)
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            State = "Internal Server Error",
                            Msg = error.Error.Message
                        }));
                    }
                    //when no error, do next.
                    else await next();
                });
            });

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = "SimpleCommerce Identity Documentation";
                options.DocExpansion(DocExpansion.None);
                options.SwaggerEndpoint("../swagger/v1/swagger.json", "Identity API V1");
            }); 
        }
    }
}
