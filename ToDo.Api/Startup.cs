using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Text;
using ToDo.Api.Helpers;
using ToDo.Core.Interfaces;
using ToDo.Infrastructure;
using ToDo.Infrastructure.Identity;
using ToDo.Infrastructure.Jwt;
using ToDo.Infrastructure.Mail;
using ToDo.Infrastructure.Repositories;

namespace ToDo.Api
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
            services.AddScoped<ISysUserRepository, SysUserRepository>();
            services.AddScoped<IToDoListRepository, ToDoListRepository>();
            services.AddScoped<IToDoShareRepository, ToDoShareRepository>();
            services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IMailService, MailService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IOperatorUser, OperatorUser>();
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddDbContext<ToDoContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddCors(option =>
            {
                option.AddPolicy("MyCors", policy =>
                {
                    policy.WithOrigins(_configuration["Cors:CorsUrls"])
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            // 添加Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDo API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                     {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                            }
                        },
                        new string[] { }
                     }
                });

            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     var serverSecret = new SymmetricSecurityKey(
                         Encoding.UTF8.GetBytes(_configuration["JWT:securityKey"]));

                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         IssuerSigningKey = serverSecret,
                         ValidateIssuer = true,
                         ValidIssuer = _configuration["JWT:issuer"],
                         ValidateAudience = true,
                         ValidAudience = _configuration["JWT:audience"]
                     };
                 });
            services.AddControllers().AddNewtonsoftJson(option =>
            {
                // 使用驼峰，首字母小写；如果不使用驼峰命名，则是 DefaultContractResolver
                option.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                // 设置忽略循环引用
                option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                // 设置序列号后的时间格式
                option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            }); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseCors("MyCors");
            // 添加Swagger有关中间件
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Demo v1");
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
