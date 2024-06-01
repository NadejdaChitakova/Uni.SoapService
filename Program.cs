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

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<CatService>();
    serviceBuilder.AddServiceEndpoint<CatService, ICatService>(new BasicHttpBinding(), "/CalculatorService");

    // Enable WSDL
    serviceBuilder.ConfigureServiceHostBase<CatService>(host =>
    {
        var serviceMetadataBehavior = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
        if (serviceMetadataBehavior == null)
        {
            serviceMetadataBehavior = new ServiceMetadataBehavior
            {
                HttpGetEnabled = true,
                HttpsGetEnabled = true
            };
            host.Description.Behaviors.Add(serviceMetadataBehavior);
        }
        else
        {
            serviceMetadataBehavior.HttpGetEnabled = true;
            serviceMetadataBehavior.HttpsGetEnabled = true;
        }
    });
});

app.Run();
