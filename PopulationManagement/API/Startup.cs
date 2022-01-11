namespace API
{
    using Core;
    using DataAccess;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.OpenApi.Models;
    using Models;
    using Services;
    using System.Collections.Generic;
    using Ultility;

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
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.WithOrigins("*")
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddEntityFrameworkSqlite().AddDbContext<PopulationDbContext>(options =>
                options.UseSqlite("Data Source=PopulationManagement.db"));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });

            services.AddSingleton<IExcelReader, ExcelReader>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPopulationService, PopulationService>();
            services.AddScoped<IHouseholdsService, HouseholdsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IExcelReader excelReader, IUnitOfWork unitOfWork)
        {
            loggerFactory.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Delete all data from DB
            this.EmptyTheDatabase(unitOfWork);

            // Read excel file against the database.
            var filePath = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppConfig")["ExcelFile"];
            this.ReadFileIntoDatabase(unitOfWork, excelReader, filePath);
        }

        public void EmptyTheDatabase(IUnitOfWork unitOfWork)
        {
            unitOfWork.ActualRepository.RemoveRange(unitOfWork.ActualRepository.Get());
            unitOfWork.EstimateRepository.RemoveRange(unitOfWork.EstimateRepository.Get());
            unitOfWork.Save();
        }

        public void ReadFileIntoDatabase(IUnitOfWork unitOfWork, IExcelReader excelReader, string filePath)
        {
            List<Actual> actualList = new List<Actual>();
            List<Estimate> estimateList = new List<Estimate>();
            excelReader.LoadDataFromExcel(ref actualList, ref estimateList, filePath);
            foreach (var actual in actualList)
            {
                unitOfWork.ActualRepository.Insert(actual);
            }
            foreach (var estimate in estimateList)
            {
                unitOfWork.EstimateRepository.Insert(estimate);
            }
            unitOfWork.Save();
        }
    }
}
