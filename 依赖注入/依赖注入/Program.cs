using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NAutowired;
using NAutowired.Core.Extensions;
using ����ע��.Logic;

var builder = WebApplication.CreateBuilder(args);


// Add Controllers As Services
builder.Services.AddControllers().AddControllersAsServices();

//ע��Ӧ�÷���
//Replace `IControllerActivator` implement.
builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, NAutowiredControllerActivator>());///���ǵ�ԭ��΢�� DependencyInjection��һЩ�߼�
//Add config to ioc container
builder.Services.Configure<SnowflakeConfig>(builder.Configuration.GetSection("Snowflake")); //���ֶ�ע�뵽ioc������
//Use auto dependency injection
builder.Services.AutoRegisterDependency(new List<string> { "����ע��" }); //������д����Ҫ��ע��ģ����ش����dll����

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
