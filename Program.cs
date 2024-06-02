using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Uni.SoapService.Contracts;
using Uni.SoapService.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelMetadata();
builder.Services.AddSingleton<CatService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
                      policy => policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<CatService>();
    serviceBuilder.AddServiceEndpoint<CatService, ICatService>(new BasicHttpBinding(), "http://localhost:5050/CatService");

    // Enable WSDL
    serviceBuilder.ConfigureServiceHostBase<CatService>(host =>
    {
        var serviceMetadataBehavior = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
        
        if (serviceMetadataBehavior == null)
        {
            serviceMetadataBehavior = new ServiceMetadataBehavior
            {
                HttpGetEnabled = false,
                HttpsGetEnabled = true
            };
            host.Description.Behaviors.Add(serviceMetadataBehavior);
        }
        else
        {
            serviceMetadataBehavior.HttpGetEnabled = false;
            serviceMetadataBehavior.HttpsGetEnabled = true;
        }

    });
});


app.Run();
