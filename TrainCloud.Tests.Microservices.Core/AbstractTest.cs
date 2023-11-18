using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TrainCloud.Tests.Microservices.Core;

public class AbstractTest<TProgram> where TProgram : class
{
    protected WebApplicationFactory<TProgram>? ApplicationFactory { get; private set; }

    protected System.Net.Http.HttpClient? Client { get; set; } 

    protected void InitializeApplication()
    {
        ApplicationFactory = new WebApplicationFactory<TProgram>().WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Test");
            //builder.ConfigureTestServices(services =>
            //{
            //    services.RemoveAll(typeof(IImageAnnotatorService));
            //    services.AddScoped<IImageAnnotatorService, ImageAnnotatorTestService>();
            //});
        });
    }
}
