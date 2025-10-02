using Microsoft.Extensions.DependencyInjection;
using TayveyTool.Interfaces;
using TayveyTool.Services;

namespace TayveyTool;

/// <summary>
/// 构建模式测试
/// </summary>
public class BuildModeTest
{
    /// <summary>
    /// 不使用接口注册
    /// </summary>
    [Fact]
    public void Should_SelfRegister()
    {
        ServiceCollection service = new();

        Di.Create<BuildModeTest>()
            .Where(t => t.Name.EndsWith("SelfService"))
            .UseSelf()
            .UseScoped(t => t.Name.Contains("Scoped"))
            .UseTransient(t => t.Name.Contains("Transient"))
            .UseSingleton(t => t.Name.Contains("Singleton"))
            .Build(service);

        ServiceProvider provider = service.BuildServiceProvider();

        Assert.NotNull(provider.GetService<ScopedSelfService>());
        Assert.Null(provider.GetService<IScopedSelfService>());

        Assert.NotNull(provider.GetService<TransientSelfService>());
        Assert.Null(provider.GetService<ITransientSelfService>());

        Assert.NotNull(provider.GetService<SingletonSelfService>());
        Assert.Null(provider.GetService<ISingletonSelfService>());
    }

    /// <summary>
    /// 无接口注册
    /// </summary>
    [Fact]
    public void Should_NoInterfaceRegister()
    {
        ServiceCollection service = new();

        Di.Create<BuildModeTest>()
            .Where(t => t.Name.EndsWith("NoInterfaceService"))
            .UseScoped(t => t.Name.Contains("Scoped"))
            .UseTransient(t => t.Name.Contains("Transient"))
            .UseSingleton(t => t.Name.Contains("Singleton"))
            .Build(service);

        ServiceProvider provider = service.BuildServiceProvider();

        Assert.NotNull(provider.GetService<ScopedNoInterfaceService>());
        Assert.NotNull(provider.GetService<TransientNoInterfaceService>());
        Assert.NotNull(provider.GetService<SingletonNoInterfaceService>());
    }

    /// <summary>
    /// 默认接口注册（第一个接口）
    /// </summary>
    [Fact]
    public void Should_DefaultInterfaceRegister()
    {
        ServiceCollection service = new();

        Di.Create<BuildModeTest>()
            .Where(t => t.Name.EndsWith("DefaultInterfaceService"))
            .UseScoped(t => t.Name.Contains("Scoped"))
            .UseTransient(t => t.Name.Contains("Transient"))
            .UseSingleton(t => t.Name.Contains("Singleton"))
            .Build(service);

        ServiceProvider provider = service.BuildServiceProvider();

        Assert.NotNull(provider.GetService<IScopedDefaultInterfaceAService>());
        Assert.Null(provider.GetService<IScopedDefaultInterfaceBService>());
        Assert.Null(provider.GetService<ScopedDefaultInterfaceService>());

        Assert.NotNull(provider.GetService<ITransientDefaultInterfaceAService>());
        Assert.Null(provider.GetService<ITransientDefaultInterfaceBService>());
        Assert.Null(provider.GetService<TransientDefaultInterfaceService>());

        Assert.NotNull(provider.GetService<ISingletonDefaultInterfaceAService>());
        Assert.Null(provider.GetService<ISingletonDefaultInterfaceBService>());
        Assert.Null(provider.GetService<SingletonDefaultInterfaceService>());
    }

    /// <summary>
    /// 指定接口注册（单个接口）
    /// </summary>
    [Fact]
    public void Should_ExplicitInterfaceRegister()
    {
        ServiceCollection service = new();

        Di.Create<BuildModeTest>()
            .Where(t => t.Name.EndsWith("ExplicitInterfaceService"))
            .UseScoped(t => t.Name.Contains("Scoped"))
            .UseInterfaces(t => t.Name == "ScopedExplicitInterfaceService", typeof(IScopedExplicitInterfaceBService))
            .UseTransient(t => t.Name.Contains("Transient"))
            .UseInterfaces(t => t.Name == "TransientExplicitInterfaceService",
                typeof(ITransientExplicitInterfaceBService))
            .UseSingleton(t => t.Name.Contains("Singleton"))
            .UseInterfaces(t => t.Name == "SingletonExplicitInterfaceService",
                typeof(ISingletonExplicitInterfaceBService))
            .Build(service);

        ServiceProvider provider = service.BuildServiceProvider();

        Assert.NotNull(provider.GetService<IScopedExplicitInterfaceBService>());
        Assert.Null(provider.GetService<IScopedExplicitInterfaceAService>());
        Assert.Null(provider.GetService<ScopedExplicitInterfaceService>());

        Assert.NotNull(provider.GetService<ITransientExplicitInterfaceBService>());
        Assert.Null(provider.GetService<ITransientExplicitInterfaceAService>());
        Assert.Null(provider.GetService<TransientExplicitInterfaceService>());

        Assert.NotNull(provider.GetService<ISingletonExplicitInterfaceBService>());
        Assert.Null(provider.GetService<ISingletonExplicitInterfaceAService>());
        Assert.Null(provider.GetService<SingletonExplicitInterfaceService>());
    }

    /// <summary>
    /// 指定接口注册（多个接口）
    /// </summary>
    [Fact]
    public void Should_MultiInterfaceRegister()
    {
        ServiceCollection service = new();

        Di.Create<BuildModeTest>()
            .Where(t => t.Name.EndsWith("MultiInterfaceService"))
            .UseScoped(t => t.Name.Contains("Scoped"))
            .UseInterfaces(t => t.Name == "ScopedMultiInterfaceService", typeof(IScopedMultiInterfaceBService),
                typeof(IScopedMultiInterfaceCService))
            .UseTransient(t => t.Name.Contains("Transient"))
            .UseInterfaces(t => t.Name == "TransientMultiInterfaceService",
                typeof(ITransientMultiInterfaceBService), typeof(ITransientMultiInterfaceCService))
            .UseSingleton(t => t.Name.Contains("Singleton"))
            .UseInterfaces(t => t.Name == "SingletonMultiInterfaceService",
                typeof(ISingletonMultiInterfaceBService), typeof(ISingletonMultiInterfaceCService))
            .Build(service);

        ServiceProvider provider = service.BuildServiceProvider();

        Assert.NotNull(provider.GetService<IScopedMultiInterfaceBService>());
        Assert.NotNull(provider.GetService<IScopedMultiInterfaceCService>());
        Assert.Null(provider.GetService<IScopedMultiInterfaceAService>());
        Assert.Null(provider.GetService<ScopedMultiInterfaceService>());

        Assert.NotNull(provider.GetService<ITransientMultiInterfaceBService>());
        Assert.NotNull(provider.GetService<ITransientMultiInterfaceCService>());
        Assert.Null(provider.GetService<ITransientMultiInterfaceAService>());
        Assert.Null(provider.GetService<TransientMultiInterfaceService>());

        Assert.NotNull(provider.GetService<ISingletonMultiInterfaceBService>());
        Assert.NotNull(provider.GetService<ISingletonMultiInterfaceCService>());
        Assert.Null(provider.GetService<ISingletonMultiInterfaceAService>());
        Assert.Null(provider.GetService<SingletonMultiInterfaceService>());
    }

    /// <summary>
    /// 指定无效接口注册（回退到默认接口）
    /// </summary>
    [Fact]
    public void Should_FallbackInterfaceRegister()
    {
        ServiceCollection service = new();

        Di.Create<BuildModeTest>()
            .Where(t => t.Name.EndsWith("FallbackInterfaceService"))
            .UseScoped(t => t.Name.Contains("Scoped"))
            .UseInterfaces(t => t.Name == "ScopedFallbackInterfaceService", typeof(IScopedFallbackInterfaceBService))
            .UseTransient(t => t.Name.Contains("Transient"))
            .UseInterfaces(t => t.Name == "TransientFallbackInterfaceService",
                typeof(ITransientFallbackInterfaceBService))
            .UseSingleton(t => t.Name.Contains("Singleton"))
            .UseInterfaces(t => t.Name == "SingletonFallbackInterfaceService",
                typeof(ISingletonFallbackInterfaceBService))
            .Build(service);

        ServiceProvider provider = service.BuildServiceProvider();

        Assert.NotNull(provider.GetService<IScopedFallbackInterfaceAService>());
        Assert.Null(provider.GetService<IScopedFallbackInterfaceBService>());
        Assert.Null(provider.GetService<ScopedFallbackInterfaceService>());

        Assert.NotNull(provider.GetService<ITransientFallbackInterfaceAService>());
        Assert.Null(provider.GetService<ITransientFallbackInterfaceBService>());
        Assert.Null(provider.GetService<TransientFallbackInterfaceService>());

        Assert.NotNull(provider.GetService<ISingletonFallbackInterfaceAService>());
        Assert.Null(provider.GetService<ISingletonFallbackInterfaceBService>());
        Assert.Null(provider.GetService<SingletonFallbackInterfaceService>());
    }

    /// <summary>
    /// 作用域生命周期测试
    /// </summary>
    [Fact]
    public void Should_Scoped()
    {
        ServiceCollection service = new();

        Di.Create<BuildModeTest>()
            .Where(t => t.Name.StartsWith("Scoped"))
            .UseSelf(t => t.Name == "ScopedSelfService")
            .UseScoped()
            .UseInterfaces(t => t.Name == "ScopedExplicitInterfaceService", typeof(IScopedExplicitInterfaceBService))
            .UseInterfaces(t => t.Name == "ScopedMultiInterfaceService", typeof(IScopedMultiInterfaceBService),
                typeof(IScopedMultiInterfaceCService))
            .UseInterfaces(t => t.Name == "ScopedFallbackInterfaceService", typeof(IScopedFallbackInterfaceBService))
            .Build(service);

        ServiceProvider provider = service.BuildServiceProvider();

        IServiceProvider scope1 = provider.CreateScope().ServiceProvider;
        IServiceProvider scope2 = provider.CreateScope().ServiceProvider;

        // 不使用接口注册
        ScopedSelfService scope1Self1 = scope1.GetRequiredService<ScopedSelfService>();
        ScopedSelfService scope1Self2 = scope1.GetRequiredService<ScopedSelfService>();
        ScopedSelfService scope2Self1 = scope2.GetRequiredService<ScopedSelfService>();
        Assert.Same(scope1Self1, scope1Self2);
        Assert.NotSame(scope1Self1, scope2Self1);

        // 无接口注册
        ScopedNoInterfaceService scope1NoInterface1 = scope1.GetRequiredService<ScopedNoInterfaceService>();
        ScopedNoInterfaceService scope1NoInterface2 = scope1.GetRequiredService<ScopedNoInterfaceService>();
        ScopedNoInterfaceService scope2NoInterface1 = scope2.GetRequiredService<ScopedNoInterfaceService>();
        Assert.Same(scope1NoInterface1, scope1NoInterface2);
        Assert.NotSame(scope1NoInterface1, scope2NoInterface1);

        // 默认接口注册
        IScopedDefaultInterfaceAService scope1DefaultInterface1 =
            scope1.GetRequiredService<IScopedDefaultInterfaceAService>();
        IScopedDefaultInterfaceAService scope1DefaultInterface2 =
            scope1.GetRequiredService<IScopedDefaultInterfaceAService>();
        IScopedDefaultInterfaceAService scope2DefaultInterface1 =
            scope2.GetRequiredService<IScopedDefaultInterfaceAService>();
        Assert.Same(scope1DefaultInterface1, scope1DefaultInterface2);
        Assert.NotSame(scope1DefaultInterface1, scope2DefaultInterface1);

        // 指定接口注册 (单个接口)
        IScopedExplicitInterfaceBService scope1ExplicitInterface1 =
            scope1.GetRequiredService<IScopedExplicitInterfaceBService>();
        IScopedExplicitInterfaceBService scope1ExplicitInterface2 =
            scope1.GetRequiredService<IScopedExplicitInterfaceBService>();
        IScopedExplicitInterfaceBService scope2ExplicitInterface1 =
            scope2.GetRequiredService<IScopedExplicitInterfaceBService>();
        Assert.Same(scope1ExplicitInterface1, scope1ExplicitInterface2);
        Assert.NotSame(scope1ExplicitInterface1, scope2ExplicitInterface1);

        // 指定接口注册 (多个接口)
        IScopedMultiInterfaceBService scope1MultiInterfaceB1 =
            scope1.GetRequiredService<IScopedMultiInterfaceBService>();
        IScopedMultiInterfaceBService scope1MultiInterfaceB2 =
            scope1.GetRequiredService<IScopedMultiInterfaceBService>();
        IScopedMultiInterfaceBService scope2MultiInterfaceB1 =
            scope2.GetRequiredService<IScopedMultiInterfaceBService>();
        Assert.Same(scope1MultiInterfaceB1, scope1MultiInterfaceB2);
        Assert.NotSame(scope1MultiInterfaceB1, scope2MultiInterfaceB1);

        IScopedMultiInterfaceCService scope1MultiInterfaceC1 =
            scope1.GetRequiredService<IScopedMultiInterfaceCService>();
        IScopedMultiInterfaceCService scope1MultiInterfaceC2 =
            scope1.GetRequiredService<IScopedMultiInterfaceCService>();
        IScopedMultiInterfaceCService scope2MultiInterfaceC1 =
            scope2.GetRequiredService<IScopedMultiInterfaceCService>();
        Assert.Same(scope1MultiInterfaceC1, scope1MultiInterfaceC2);
        Assert.NotSame(scope1MultiInterfaceC1, scope2MultiInterfaceC1);

        // 指定无效接口注册（回退到默认接口）
        IScopedFallbackInterfaceAService scope1FallbackInterface1 =
            scope1.GetRequiredService<IScopedFallbackInterfaceAService>();
        IScopedFallbackInterfaceAService scope1FallbackInterface2 =
            scope1.GetRequiredService<IScopedFallbackInterfaceAService>();
        IScopedFallbackInterfaceAService scope2FallbackInterface1 =
            scope2.GetRequiredService<IScopedFallbackInterfaceAService>();
        Assert.Same(scope1FallbackInterface1, scope1FallbackInterface2);
        Assert.NotSame(scope1FallbackInterface1, scope2FallbackInterface1);
    }

    /// <summary>
    /// 瞬时生命周期测试
    /// </summary>
    [Fact]
    public void Should_Transient()
    {
        ServiceCollection service = new();

        Di.Create<BuildModeTest>()
            .Where(t => t.Name.StartsWith("Transient"))
            .UseSelf(t => t.Name == "TransientSelfService")
            .UseTransient()
            .UseInterfaces(t => t.Name == "TransientExplicitInterfaceService",
                typeof(ITransientExplicitInterfaceBService))
            .UseInterfaces(t => t.Name == "TransientMultiInterfaceService", typeof(ITransientMultiInterfaceBService),
                typeof(ITransientMultiInterfaceCService))
            .UseInterfaces(t => t.Name == "TransientFallbackInterfaceService",
                typeof(ITransientFallbackInterfaceBService))
            .Build(service);

        ServiceProvider provider = service.BuildServiceProvider();

        IServiceProvider scope1 = provider.CreateScope().ServiceProvider;
        IServiceProvider scope2 = provider.CreateScope().ServiceProvider;

        // 不使用接口注册
        TransientSelfService scope1Self1 = scope1.GetRequiredService<TransientSelfService>();
        TransientSelfService scope1Self2 = scope1.GetRequiredService<TransientSelfService>();
        TransientSelfService scope2Self1 = scope2.GetRequiredService<TransientSelfService>();
        Assert.NotSame(scope1Self1, scope1Self2);
        Assert.NotSame(scope1Self1, scope2Self1);

        // 无接口注册
        TransientNoInterfaceService scope1NoInterface1 = scope1.GetRequiredService<TransientNoInterfaceService>();
        TransientNoInterfaceService scope1NoInterface2 = scope1.GetRequiredService<TransientNoInterfaceService>();
        TransientNoInterfaceService scope2NoInterface1 = scope2.GetRequiredService<TransientNoInterfaceService>();
        Assert.NotSame(scope1NoInterface1, scope1NoInterface2);
        Assert.NotSame(scope1NoInterface1, scope2NoInterface1);

        // 默认接口注册
        ITransientDefaultInterfaceAService scope1DefaultInterface1 =
            scope1.GetRequiredService<ITransientDefaultInterfaceAService>();
        ITransientDefaultInterfaceAService scope1DefaultInterface2 =
            scope1.GetRequiredService<ITransientDefaultInterfaceAService>();
        ITransientDefaultInterfaceAService scope2DefaultInterface1 =
            scope2.GetRequiredService<ITransientDefaultInterfaceAService>();
        Assert.NotSame(scope1DefaultInterface1, scope1DefaultInterface2);
        Assert.NotSame(scope1DefaultInterface1, scope2DefaultInterface1);

        // 指定接口注册 (单个接口)
        ITransientExplicitInterfaceBService scope1ExplicitInterface1 =
            scope1.GetRequiredService<ITransientExplicitInterfaceBService>();
        ITransientExplicitInterfaceBService scope1ExplicitInterface2 =
            scope1.GetRequiredService<ITransientExplicitInterfaceBService>();
        ITransientExplicitInterfaceBService scope2ExplicitInterface1 =
            scope2.GetRequiredService<ITransientExplicitInterfaceBService>();
        Assert.NotSame(scope1ExplicitInterface1, scope1ExplicitInterface2);
        Assert.NotSame(scope1ExplicitInterface1, scope2ExplicitInterface1);

        // 指定接口注册 (多个接口)
        ITransientMultiInterfaceBService scope1MultiInterfaceB1 =
            scope1.GetRequiredService<ITransientMultiInterfaceBService>();
        ITransientMultiInterfaceBService scope1MultiInterfaceB2 =
            scope1.GetRequiredService<ITransientMultiInterfaceBService>();
        ITransientMultiInterfaceBService scope2MultiInterfaceB1 =
            scope2.GetRequiredService<ITransientMultiInterfaceBService>();
        Assert.NotSame(scope1MultiInterfaceB1, scope1MultiInterfaceB2);
        Assert.NotSame(scope1MultiInterfaceB1, scope2MultiInterfaceB1);

        ITransientMultiInterfaceCService scope1MultiInterfaceC1 =
            scope1.GetRequiredService<ITransientMultiInterfaceCService>();
        ITransientMultiInterfaceCService scope1MultiInterfaceC2 =
            scope1.GetRequiredService<ITransientMultiInterfaceCService>();
        ITransientMultiInterfaceCService scope2MultiInterfaceC1 =
            scope2.GetRequiredService<ITransientMultiInterfaceCService>();
        Assert.NotSame(scope1MultiInterfaceC1, scope1MultiInterfaceC2);
        Assert.NotSame(scope1MultiInterfaceC1, scope2MultiInterfaceC1);

        // 指定无效接口注册（回退到默认接口）
        ITransientFallbackInterfaceAService scope1FallbackInterface1 =
            scope1.GetRequiredService<ITransientFallbackInterfaceAService>();
        ITransientFallbackInterfaceAService scope1FallbackInterface2 =
            scope1.GetRequiredService<ITransientFallbackInterfaceAService>();
        ITransientFallbackInterfaceAService scope2FallbackInterface1 =
            scope2.GetRequiredService<ITransientFallbackInterfaceAService>();
        Assert.NotSame(scope1FallbackInterface1, scope1FallbackInterface2);
        Assert.NotSame(scope1FallbackInterface1, scope2FallbackInterface1);
    }

    /// <summary>
    /// 单例生命周期测试
    /// </summary>
    [Fact]
    public void Should_Singleton()
    {
        ServiceCollection service = new();

        Di.Create<BuildModeTest>()
            .Where(t => t.Name.StartsWith("Singleton"))
            .UseSelf(t => t.Name == "SingletonSelfService")
            .UseSingleton()
            .UseInterfaces(t => t.Name == "SingletonExplicitInterfaceService",
                typeof(ISingletonExplicitInterfaceBService))
            .UseInterfaces(t => t.Name == "SingletonMultiInterfaceService", typeof(ISingletonMultiInterfaceBService),
                typeof(ISingletonMultiInterfaceCService))
            .UseInterfaces(t => t.Name == "SingletonFallbackInterfaceService",
                typeof(ISingletonFallbackInterfaceBService))
            .Build(service);

        ServiceProvider provider = service.BuildServiceProvider();

        IServiceProvider scope1 = provider.CreateScope().ServiceProvider;
        IServiceProvider scope2 = provider.CreateScope().ServiceProvider;

        // 不使用接口注册
        SingletonSelfService scope1Self1 = scope1.GetRequiredService<SingletonSelfService>();
        SingletonSelfService scope1Self2 = scope1.GetRequiredService<SingletonSelfService>();
        SingletonSelfService scope2Self1 = scope2.GetRequiredService<SingletonSelfService>();
        Assert.Same(scope1Self1, scope1Self2);
        Assert.Same(scope1Self1, scope2Self1);

        // 无接口注册
        SingletonNoInterfaceService scope1NoInterface1 = scope1.GetRequiredService<SingletonNoInterfaceService>();
        SingletonNoInterfaceService scope1NoInterface2 = scope1.GetRequiredService<SingletonNoInterfaceService>();
        SingletonNoInterfaceService scope2NoInterface1 = scope2.GetRequiredService<SingletonNoInterfaceService>();
        Assert.Same(scope1NoInterface1, scope1NoInterface2);
        Assert.Same(scope1NoInterface1, scope2NoInterface1);

        // 默认接口注册
        ISingletonDefaultInterfaceAService scope1DefaultInterface1 =
            scope1.GetRequiredService<ISingletonDefaultInterfaceAService>();
        ISingletonDefaultInterfaceAService scope1DefaultInterface2 =
            scope1.GetRequiredService<ISingletonDefaultInterfaceAService>();
        ISingletonDefaultInterfaceAService scope2DefaultInterface1 =
            scope2.GetRequiredService<ISingletonDefaultInterfaceAService>();
        Assert.Same(scope1DefaultInterface1, scope1DefaultInterface2);
        Assert.Same(scope1DefaultInterface1, scope2DefaultInterface1);

        // 指定接口注册 (单个接口)
        ISingletonExplicitInterfaceBService scope1ExplicitInterface1 =
            scope1.GetRequiredService<ISingletonExplicitInterfaceBService>();
        ISingletonExplicitInterfaceBService scope1ExplicitInterface2 =
            scope1.GetRequiredService<ISingletonExplicitInterfaceBService>();
        ISingletonExplicitInterfaceBService scope2ExplicitInterface1 =
            scope2.GetRequiredService<ISingletonExplicitInterfaceBService>();
        Assert.Same(scope1ExplicitInterface1, scope1ExplicitInterface2);
        Assert.Same(scope1ExplicitInterface1, scope2ExplicitInterface1);

        // 指定接口注册 (多个接口)
        ISingletonMultiInterfaceBService scope1MultiInterfaceB1 =
            scope1.GetRequiredService<ISingletonMultiInterfaceBService>();
        ISingletonMultiInterfaceBService scope1MultiInterfaceB2 =
            scope1.GetRequiredService<ISingletonMultiInterfaceBService>();
        ISingletonMultiInterfaceBService scope2MultiInterfaceB1 =
            scope2.GetRequiredService<ISingletonMultiInterfaceBService>();
        Assert.Same(scope1MultiInterfaceB1, scope1MultiInterfaceB2);
        Assert.Same(scope1MultiInterfaceB1, scope2MultiInterfaceB1);

        ISingletonMultiInterfaceCService scope1MultiInterfaceC1 =
            scope1.GetRequiredService<ISingletonMultiInterfaceCService>();
        ISingletonMultiInterfaceCService scope1MultiInterfaceC2 =
            scope1.GetRequiredService<ISingletonMultiInterfaceCService>();
        ISingletonMultiInterfaceCService scope2MultiInterfaceC1 =
            scope2.GetRequiredService<ISingletonMultiInterfaceCService>();
        Assert.Same(scope1MultiInterfaceC1, scope1MultiInterfaceC2);
        Assert.Same(scope1MultiInterfaceC1, scope2MultiInterfaceC1);

        // 指定无效接口注册（回退到默认接口）
        ISingletonFallbackInterfaceAService scope1FallbackInterface1 =
            scope1.GetRequiredService<ISingletonFallbackInterfaceAService>();
        ISingletonFallbackInterfaceAService scope1FallbackInterface2 =
            scope1.GetRequiredService<ISingletonFallbackInterfaceAService>();
        ISingletonFallbackInterfaceAService scope2FallbackInterface1 =
            scope2.GetRequiredService<ISingletonFallbackInterfaceAService>();
        Assert.Same(scope1FallbackInterface1, scope1FallbackInterface2);
        Assert.Same(scope1FallbackInterface1, scope2FallbackInterface1);
    }
}
