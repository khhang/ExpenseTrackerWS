using System;
using System.Text;
using expense_tracker.Logic;
using expense_tracker.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace expense_tracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public readonly string ExpenseTrackerCorsPolicy = "_expenseTrackerCorsPolicy";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(ExpenseTrackerCorsPolicy,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            var key = "test key for auth";

            services.AddAuthentication(x => 
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => 
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services
                .AddSingleton<IAccountsService, AccountsService>()
                .AddSingleton<IAccountsRepository, AccountsRepository>()
                .AddSingleton<ICategoriesService, CategoriesService>()
                .AddSingleton<ICategoriesRepository, CategoriesRepository>()
                .AddSingleton<ISubcategoriesService, SubcategoriesService>()
                .AddSingleton<ISubcategoriesRepository, SubcategoriesRepository>()
                .AddSingleton<IIncomeCategoriesService, IncomeCategoriesService>()
                .AddSingleton<IIncomeCategoriesRepository, IncomeCategoriesRepository>()
                .AddSingleton<IExpensesService, ExpensesService>()
                .AddSingleton<IExpensesRepository, ExpensesRepository>()
                .AddSingleton<ITransfersRepository, TransfersRepository>()
                .AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(key))
                .AddSingleton(CreateDatabaseAccessor);

            services.AddControllers();
        }

        private IDatabaseAccessor CreateDatabaseAccessor(IServiceProvider arg)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            return new DatabaseAccessor(connectionString);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(ExpenseTrackerCorsPolicy);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
