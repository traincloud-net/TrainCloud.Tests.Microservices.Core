# 🚆 TrainCloud.Tests.Microservices.Core

The `TrainCloud.Tests.Microservices.Core` library provides a base class for the tests in `TrainCloud.Tests.Microservices.*` projects.

All unit tests for the API inherit from `AbstractTest<TProgram>` where `TProgram` is the instance of the class `Program` in the file `Program.cs` from the service.

## Status

### GitHub Actions
[![SonarQube](https://github.com/traincloud-net/TrainCloud.Tests.Microservices.Core/actions/workflows/sonarqube.yml/badge.svg)](https://github.com/traincloud-net/TrainCloud.Tests.Microservices.Core/actions/workflows/sonarqube.yml) 
[![NuGet](https://github.com/traincloud-net/TrainCloud.Tests.Microservices.Core/actions/workflows/nuget.yml/badge.svg)](https://github.com/traincloud-net/TrainCloud.Tests.Microservices.Core/actions/workflows/nuget.yml) 

### SonarQube
[![Bugs](https://sonarqube.traincloud.net/api/project_badges/measure?project=TrainCloud.Tests.Microservices.Core&metric=bugs&token=sqb_54c03c28f976684cdbed8800e1b14fb632f158e4)](https://sonarqube.traincloud.net/dashboard?id=TrainCloud.Tests.Microservices.Core)
[![Code Smells](https://sonarqube.traincloud.net/api/project_badges/measure?project=TrainCloud.Tests.Microservices.Core&metric=code_smells&token=sqb_54c03c28f976684cdbed8800e1b14fb632f158e4)](https://sonarqube.traincloud.net/dashboard?id=TrainCloud.Tests.Microservices.Core) 
[![Duplicated Lines (%)](https://sonarqube.traincloud.net/api/project_badges/measure?project=TrainCloud.Tests.Microservices.Core&metric=duplicated_lines_density&token=sqb_54c03c28f976684cdbed8800e1b14fb632f158e4)](https://sonarqube.traincloud.net/dashboard?id=TrainCloud.Tests.Microservices.Core) 
[![Lines of Code](https://sonarqube.traincloud.net/api/project_badges/measure?project=TrainCloud.Tests.Microservices.Core&metric=ncloc&token=sqb_54c03c28f976684cdbed8800e1b14fb632f158e4)](https://sonarqube.traincloud.net/dashboard?id=TrainCloud.Tests.Microservices.Core) 
[![Maintainability Rating](https://sonarqube.traincloud.net/api/project_badges/measure?project=TrainCloud.Tests.Microservices.Core&metric=sqale_rating&token=sqb_54c03c28f976684cdbed8800e1b14fb632f158e4)](https://sonarqube.traincloud.net/dashboard?id=TrainCloud.Tests.Microservices.Core) 
[![Reliability Rating](https://sonarqube.traincloud.net/api/project_badges/measure?project=TrainCloud.Tests.Microservices.Core&metric=reliability_rating&token=sqb_54c03c28f976684cdbed8800e1b14fb632f158e4)](https://sonarqube.traincloud.net/dashboard?id=TrainCloud.Tests.Microservices.Core) 
[![Security Hotspots](https://sonarqube.traincloud.net/api/project_badges/measure?project=TrainCloud.Tests.Microservices.Core&metric=security_hotspots&token=sqb_54c03c28f976684cdbed8800e1b14fb632f158e4)](https://sonarqube.traincloud.net/dashboard?id=TrainCloud.Tests.Microservices.Core) 
[![Security Rating](https://sonarqube.traincloud.net/api/project_badges/measure?project=TrainCloud.Tests.Microservices.Core&metric=security_rating&token=sqb_54c03c28f976684cdbed8800e1b14fb632f158e4)](https://sonarqube.traincloud.net/dashboard?id=TrainCloud.Tests.Microservices.Core) 
[![Technical Debt](https://sonarqube.traincloud.net/api/project_badges/measure?project=TrainCloud.Tests.Microservices.Core&metric=sqale_index&token=sqb_54c03c28f976684cdbed8800e1b14fb632f158e4)](https://sonarqube.traincloud.net/dashboard?id=TrainCloud.Tests.Microservices.Core) 
[![Vulnerabilities](https://sonarqube.traincloud.net/api/project_badges/measure?project=TrainCloud.Tests.Microservices.Core&metric=vulnerabilities&token=sqb_54c03c28f976684cdbed8800e1b14fb632f158e4)](https://sonarqube.traincloud.net/dashboard?id=TrainCloud.Tests.Microservices.Core)

## How to use

Add the TrainCloud NuGet Feed to you `nuget.config` file.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
		<packageSources>
				<add key="TrainCloud" value="https://nuget.pkg.github.com/traincloud-net/index.json" />
		</packageSources>
</configuration>
```

Add `TrainCloud.HttpClient` package to the project

```bash
dotnet add package TrainCloud.HttpClient
```

Add the namespace `TrainCloud.Tests.Microservices.Core` to global usings

**GlobalUsings.cs**

```csharp
using TrainCloud.HttpClient;
using TrainCloud.Tests.Microservices.Core;
using System.Net;
```

### Example

```csharp
namespace TrainCloud.Tests.Microservices.Example;

[TestClass]
public class UnitTest_Example : AbstractTest<Program>
{
    [TestMethod]
    public async Task TestMethod_Example()
    {
        // Arrange
        PostThingModel model = new()
        {
            ...
        };

        var anonymousClient = GetClient();

        var authorizedClient = GetClient("token....");

        // Act
        ResponseModel? result = await xxxClient.PostRequestAsync<PostThingModel, ResponseModel>("/Route", model, httpStatus =>
        {
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, httpStatus);
        });

        // Assert
        Assert.IsNotNull(result);
    }
}
```