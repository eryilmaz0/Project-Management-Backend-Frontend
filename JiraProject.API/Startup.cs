using JiraProject.Entities.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using JiraProject.API.CustomMiddlewares;
using JiraProject.Business.Abstract;
using JiraProject.Business.Concrete;
using JiraProject.Business.Security.JWT;
using JiraProject.Business.ValidationRules.FluentValidation;
using JiraProject.DataAccess.Abstract;
using JiraProject.DataAccess.Concrete.EntityFramework;
using JiraProject.Entities.DataTransferObjects.Request;
using JiraProject.Entities.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ITaskChangeRepository = JiraProject.DataAccess.Abstract.ITaskChangeRepository;


namespace JiraProject.API
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
            services.AddDbContext<JiraDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));
            services.AddAutoMapperConfiguration();
            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    //AUTO VALIDATION DISABLED
                    options.SuppressModelStateInvalidFilter = true;
                })
                .AddFluentValidation(opt =>
                {
                    opt.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>();
                    opt.RegisterValidatorsFromAssemblyContaining<CreateProjectRequestValidator>();
                    opt.RegisterValidatorsFromAssemblyContaining<EditProjectRequestValidator>();
                    opt.RegisterValidatorsFromAssemblyContaining<CreateTaskRequestValidator>();
                    opt.RegisterValidatorsFromAssemblyContaining<EditTaskRequestValidator>();
                    opt.RegisterValidatorsFromAssemblyContaining<AddUserToProjectRequestValidator>();
                    opt.RegisterValidatorsFromAssemblyContaining<RemoveUserFromProjectRequestValidator>();
                });


            TokenOptions jwtOptions = Configuration.GetSection("JwtConfiguration").Get<TokenOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey))
                };
            });



            //CLAIM BASE AUTHORIZATION
            services.AddAuthorization(config =>
            {
                config.AddPolicy("Policy1", policy => policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Developer"));
                config.AddPolicy("Policy2", policy => policy.RequireClaim("user-picture", "user1.jpg"));
            });



            services.AddScoped<IUserRepository, EfUserRepository>();
            services.AddScoped<IRoleRepository, EfRoleRepository>();
            services.AddScoped<IDepartmentRepository, EfDepartmentRepository>();
            services.AddScoped<ITaskRepository, EfTaskRepository>();
            services.AddScoped<ITaskChangeRepository, EfTaskChangeRepository>();
            services.AddScoped<IProjectRepository, EfProjectRepository>();

            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<IProjectService, ProjectManager>();
            services.AddScoped<IDepartmentService, DepartmentManager>();
            services.AddScoped<ITaskService, TaskManager>();
            services.AddScoped<ITaskChangeService, TaskChangeManager>();
            services.AddScoped<IUserService, UserManager>();

            services.AddScoped<TokenHelper>();

            services.AddHttpContextAccessor();

            services.AddScoped<ICacheService, RedisCacheManager>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JiraProject.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JiraProject.API v1"));
            }


            app.UseMiddleware<ExceptionHandlerMiddleware>();

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
