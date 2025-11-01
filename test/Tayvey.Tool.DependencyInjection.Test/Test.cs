using Microsoft.Extensions.DependencyInjection;
using TayveyTool.Interfaces;
using TayveyTool.Services;

namespace TayveyTool;

/// <summary>
/// 测试
/// </summary>
public class Test
{
    /// <summary>
    /// 成功测试入参
    /// </summary>
    public static TheoryData<string, string> ShouldSuccessInputs => new()
    {
        { "Self", "Scoped" },
        { "Self", "Transient" },
        { "Self", "Singleton" },
        { "First", "Scoped" },
        { "First", "Transient" },
        { "First", "Singleton" },
        { "All", "Scoped" },
        { "All", "Transient" },
        { "All", "Singleton" }
    };

    /// <summary>
    /// 成功测试
    /// </summary>
    /// <param name="mode"></param>
    /// <param name="lifeCycle"></param>
    /// <exception cref="NotImplementedException"></exception>
    [Theory]
    [MemberData(nameof(ShouldSuccessInputs))]
    public void Should_Success(string mode, string lifeCycle)
    {
        // 依赖注入
        ServiceCollection service = new();

        IDiBuilder builder = service.CreateDi<Test>().Where(t => t.Name.EndsWith("Service"));
        IDiBuilderMode modeBuilder = mode switch
        {
            "Self" => builder.SelfMode(),
            "First" => builder.FirstMode(),
            "All" => builder.AllMode(),
            _ => throw new NotImplementedException()
        };
        IDiBuilderLifeCycle lifeCycleBuilder = lifeCycle switch
        {
            "Scoped" => modeBuilder.Scoped(),
            "Transient" => modeBuilder.Transient(),
            "Singleton" => modeBuilder.Singleton(),
            _ => throw new NotImplementedException()
        };
        lifeCycleBuilder.Build();

        ServiceProvider provider = service.BuildServiceProvider();

        // 模式校验
        switch (mode)
        {
            case "Self":
                Assert.NotNull(provider.GetService<TestService>());
                break;
            case "First":
                Assert.NotNull(provider.GetService<ITestService>());
                Assert.Null(provider.GetService<TestService>());
                break;
            case "All":
                Assert.NotNull(provider.GetServices<ITestService>());
                Assert.NotNull(provider.GetServices<ITest2Service>());
                Assert.Null(provider.GetService<TestService>());
                break;
        }

        Assert.Null(provider.GetService<ITestBaseService>());
        Assert.Null(provider.GetService<TestBaseService>());

        // 生命周期校验
        using IServiceScope scope1 = provider.CreateScope();
        using IServiceScope scope2 = provider.CreateScope();

        if (mode == "Self")
        {
            TestService? service1 = scope1.ServiceProvider.GetService<TestService>();
            TestService? service2 = scope1.ServiceProvider.GetService<TestService>();
            TestService? service3 = scope2.ServiceProvider.GetService<TestService>();

            switch (lifeCycle)
            {
                case "Scoped":
                    Assert.Equal(service1, service2);
                    Assert.NotEqual(service1, service3);
                    break;
                case "Transient":
                    Assert.NotEqual(service1, service2);
                    Assert.NotEqual(service1, service3);
                    break;
                default:
                    Assert.Equal(service1, service2);
                    Assert.Equal(service1, service3);
                    break;
            }
        }
        else
        {
            ITestService? service1 = scope1.ServiceProvider.GetService<ITestService>();
            ITestService? service2 = scope1.ServiceProvider.GetService<ITestService>();
            ITestService? service3 = scope2.ServiceProvider.GetService<ITestService>();

            switch (lifeCycle)
            {
                case "Scoped":
                    Assert.Equal(service1, service2);
                    Assert.NotEqual(service1, service3);
                    break;
                case "Transient":
                    Assert.NotEqual(service1, service2);
                    Assert.NotEqual(service1, service3);
                    break;
                default:
                    Assert.Equal(service1, service2);
                    Assert.Equal(service1, service3);
                    break;
            }
        }
    }
}
