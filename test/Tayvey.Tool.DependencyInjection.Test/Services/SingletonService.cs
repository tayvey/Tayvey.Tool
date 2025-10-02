using TayveyTool.Attributes;
using TayveyTool.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace TayveyTool.Services;

/// <summary>
/// 注册为 SingletonSelfService 类型，不使用接口
/// </summary>
[Singleton(Self = true)]
internal class SingletonSelfService : ISingletonSelfService
{
}

/// <summary>
/// 注册为 SingletonNoInterfaceService 类型，无接口
/// </summary>
[Singleton]
internal class SingletonNoInterfaceService
{
}

/// <summary>
/// 注册为 ISingletonDefaultInterfaceAService 类型（默认第一个接口）
/// </summary>
[Singleton]
internal class SingletonDefaultInterfaceService : ISingletonDefaultInterfaceAService,
    ISingletonDefaultInterfaceBService
{
}

/// <summary>
/// 注册为 ISingletonExplicitInterfaceBService 类型（指定单个接口）
/// </summary>
[Singleton(typeof(ISingletonExplicitInterfaceBService))]
internal class SingletonExplicitInterfaceService : ISingletonExplicitInterfaceAService,
    ISingletonExplicitInterfaceBService
{
}

/// <summary>
/// 注册为 ISingletonMultiInterfaceBService 和 ISingletonMultiInterfaceCService 类型（指定多个接口）
/// </summary>
[Singleton(typeof(ISingletonMultiInterfaceBService), typeof(ISingletonMultiInterfaceCService))]
internal class SingletonMultiInterfaceService : ISingletonMultiInterfaceAService, ISingletonMultiInterfaceBService,
    ISingletonMultiInterfaceCService
{
}

/// <summary>
/// 注册为 ISingletonFallbackInterfaceAService 类型（回退到第一个接口）
/// </summary>
[Singleton(typeof(ISingletonFallbackInterfaceBService))]
internal class SingletonFallbackInterfaceService : ISingletonFallbackInterfaceAService
{
}
