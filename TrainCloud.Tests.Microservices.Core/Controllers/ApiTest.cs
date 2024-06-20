using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;

namespace TrainCloud.Tests.Microservices.Core.Controllers;

/// <summary>
/// Base class for controllers in TrainCloud.Tests.Microservices.*
/// </summary>
/// <typeparam name="TProgram">The Program class from the Program.cs</typeparam>
public class ApiTest<TProgram> where TProgram : class
{
    /// <summary>
    /// The WebApplicationFactory which hosts the tested service
    /// </summary>
    protected WebApplicationFactory<TProgram> ApplicationFactory { get; private set; }

    /// <summary>
    /// Creates the WebApplicationFactory instance of the currently tested service and raises a action for optional customizing of the IWebHostBuilder
    /// </summary>
    /// <param name="onBuildAction">Optional action to customize the IWebHostBuilder</param>
    public ApiTest(Action<IWebHostBuilder>? onBuildAction = null)
    {
        ApplicationFactory = new WebApplicationFactory<TProgram>().WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Test");
            if (onBuildAction is not null)
            {
                onBuildAction(builder);
            }
        });
    }

    /// <summary>
    /// Creates a HttpClient for the current application. Optional with authorization token
    /// </summary>
    /// <param name="token">A JWT, created by TrainCloud.Microservices.Identity</param>
    /// <returns>A instance of HttpClient, for the currently tested application</returns>
    public HttpClient GetClient(string? token = null)
    {
        HttpClient client = ApplicationFactory!.CreateClient();

        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return client;
    }
}
