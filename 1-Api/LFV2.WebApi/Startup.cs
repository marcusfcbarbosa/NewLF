using LFV2.Domain.PedidosContext.Commands.Inputs;
using LFV2.Domain.PedidosContext.Repositories.Interfaces;
using LFV2.Infra.NoSQLContext;
using LFV2.Infra.NoSQLContext.Repositories;
using LFV2.Infra.SQLContext;
using LFV2.Infra.SQLContext.Repositories;
using LFV2.Shared.Interfaces;
using LFV2.Shared.TriggeringJobs;
using LFV2.WebApi.InfraEstructure;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace LFV2.WebApi
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

            services.AddControllers();
            services.AddHttpClient();

            services.Configure<ConfigDB>(
               x =>
               {
                   x.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                   x.DataBase = Configuration.GetSection("MongoConnection:DataBase").Value;
               });
            services.AddDbContext<LFContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            registrandoDependencias(services);
            DocumentacaoApi(services);
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public void DocumentacaoApi(IServiceCollection services)
        {
            services.AddSwaggerDocumentation();
        }
        public void registrandoDependencias(IServiceCollection services)
        {
            #region"Contexto"
            services.AddScoped<LFContext, LFContext>();
            #endregion

            #region"Repositórios"
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IMongoPedidoRepository, MongoPedidoRepository>();
            services.AddScoped<ITriggeringJjob, TriggeringJob>();
            
            #endregion

            #region"mediator"
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(CriaPedidoCommand).GetTypeInfo().Assembly);
            #endregion
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwaggerDocumentation();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "swagger");
            });

            app.UseRouting();

            app.UseCors(x => x
                      .AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
