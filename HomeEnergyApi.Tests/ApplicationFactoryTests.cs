using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using HomeEnergyApi.Controllers;
namespace HomeEnergyApi.Models;
using Xunit;


[TestCaseOrderer("HomeEnergyApi.Tests.Extensions.PriorityOrderer", "HomeEnergyApi.Tests")]
public class ApplicationFactoryTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;


    public ApplicationFactoryTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact, TestPriority(1)]
    public void ProgramShouldContainAnApplicationFactoryImplementingIControllerFactory()
    {
        using var serviceScope = _factory.Services.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;

        var service = serviceProvider.GetRequiredService<IControllerFactory>();

        Assert.True(service != null, $"There is no service implementing \"IControllerFactory\" on HomeEnergyApi/Program.cs");
        Assert.True(service?.ToString() == "HomeEnergyApi.ApplicationFactory", "The Service implementing \"IControllerFactory\" on HomeEnergyApi/Program.cs is not titled \"ApplicationFactory\"");
    }

    [Fact, TestPriority(2)]
    public void ApplicationFactoryShouldBeAbleToCreateANewHomesController()
    {
        using var serviceScope = _factory.Services.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;

        var service = serviceProvider.GetRequiredService<IControllerFactory>();
        var homesController = service.CreateController(new ControllerContext());

        Assert.True(homesController.GetType() == typeof(HomesController), $"The Object returned from Application Factory was not of type \"HomesController\". Instead was type {homesController.GetType()} ");

        service.ReleaseController(new ControllerContext(), homesController);
    }

    [Fact, TestPriority(3)]
    public void ApplicationFactoryShouldCreateANewHomeRepositoryWhenANewHomesControllerIsCreated()
    {
        using var serviceScope = _factory.Services.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;

        var service = serviceProvider.GetRequiredService<IControllerFactory>();
        HomesController homesController = (HomesController)service.CreateController(new ControllerContext());

        Assert.True(homesController.repository.GetType() == typeof(HomeRepository), $"The HomesController had no property titled \"repository\" after being created by ApplicationFactory");
    }
}
