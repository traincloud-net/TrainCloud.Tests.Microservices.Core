using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Linq;
using System.Net.Http.Headers;

namespace TrainCloud.Tests.Microservices.Core;

public class AbstractTest<TProgram> where TProgram : class
{
    protected WebApplicationFactory<TProgram>? ApplicationFactory { get; private set; }

    public AbstractTest(Action<IWebHostBuilder>? onBuildAction = null)
    {
        ApplicationFactory = new WebApplicationFactory<TProgram>().WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Test");
            if(onBuildAction is not null)
            {
                onBuildAction(builder);
            }
        });
    }

    public System.Net.Http.HttpClient GetClient(string? token = null)
    {
        System.Net.Http.HttpClient client = ApplicationFactory!.CreateClient();

        if(!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return client;
    }
}
