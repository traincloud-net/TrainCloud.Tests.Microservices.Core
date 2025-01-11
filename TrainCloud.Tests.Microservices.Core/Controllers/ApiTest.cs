using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using TrainCloud.HttpClient;
using TrainCloud.Microservices.Identity.Models;
using TrainCloud.Models;

namespace TrainCloud.Tests.Microservices.Core.Controllers;

/// <summary>
/// Base class for controllers in TrainCloud.Tests.Microservices.*
/// </summary>
/// <typeparam name="TProgram">The Program class from the Program.cs</typeparam>
public abstract class ApiTest<TProgram> where TProgram : class
{
    /// <summary>
    /// The WebApplicationFactory which hosts the tested service
    /// </summary>
    protected WebApplicationFactory<TProgram> ApplicationFactory { get; private set; }

    /// <summary>
    /// Creates the WebApplicationFactory instance of the currently tested service and raises a action for optional customizing of the IWebHostBuilder
    /// </summary>
    /// <param name="onBuildAction">Optional action to customize the IWebHostBuilder</param>
    protected ApiTest(Action<IWebHostBuilder>? onBuildAction = null)
    {
        ApplicationFactory = new WebApplicationFactory<TProgram>().WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Tests");
            if (onBuildAction is not null)
            {
                onBuildAction(builder);
            }
        });
    }

    /// <summary>
    /// Creates a HttpClient for the current application with username and password and obtains an Authorization header.
    /// https://identity.traincloud.dev is the centralized identity service for all test. Local or in Pipeline
    /// </summary>
    /// <param name="userName">The user name of the test user</param>
    /// <param name="password">The password of the Testuser</param>
    /// <returns></returns>
    public async Task<System.Net.Http.HttpClient> GetClientAsync(string userName, string password)
    {
        System.Net.Http.HttpClient loginClient = new();
        PostSignInModel postModel = new()
        {
            UserName = userName,
            Password = password
        };

        TokenModel? model = await loginClient.PostRequestAsync<PostSignInModel, TokenModel>("https://tests-traincloud-microservices-identity-west3-317588625388.europe-west3.run.app/Identity/SignIn", postModel);

        System.Net.Http.HttpClient client = GetClient(model?.Token);

        return client;
    }

    /// <summary>
    /// Creates a HttpClient for the current application. Optional with authorization token.
    /// </summary>
    /// <param name="token">A JWT, created by TrainCloud.Microservices.Identity</param>
    /// <returns>A instance of HttpClient, for the currently tested application</returns>
    public System.Net.Http.HttpClient GetClient(string? token = null)
    {
        System.Net.Http.HttpClient client = ApplicationFactory!.CreateClient();

        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return client;
    }
}
