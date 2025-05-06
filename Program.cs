using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebApplication1.BL;
using WebApplication1.BL.Classes;
using WebApplication1.BL.Interfaces;
using WebApplication1.BL.Validators;
using WebApplication1.DAL.Classes;
using WebApplication1.DAL.Interfaces;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<RegisteretionContextDL>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITModeratorBL, TModeratorBL>();
builder.Services.AddScoped<ITModeratorDL, TModeratorDL>();

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<MatriculationServiceBL>();
builder.Services.AddScoped<IValidator<UpdateMatriculationDataRequest>, UpdateMatriculationDataRequestValidator>();
builder.Services.AddScoped<IMatriculationRepositoryDL, MatriculationRepositoryDL>();

builder.Services.AddScoped<IMatriculationDL, TMatriculationDL>();
builder.Services.AddScoped<IMatriculationBL, MatriculationBL>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();