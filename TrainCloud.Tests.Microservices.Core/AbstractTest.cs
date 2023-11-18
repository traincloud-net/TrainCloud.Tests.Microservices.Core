using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TrainCloud.Tests.Microservices.Core;

public class AbstractTest<TProgram> where TProgram : class
{
    protected WebApplicationFactory<TProgram> ApplicationFactory { get; init; }

    public AbstractTest()
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
