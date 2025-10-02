using TayveyTool.Attributes;
using TayveyTool.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace TayveyTool.Services;

/// <summary>
/// 注册为 TransientSelfService 类型，不使用接口
/// </summary>
[Transient(Self = true)]
internal class TransientSelfService : ITransientSelfService
{
}

/// <summary>
/// 注册为 TransientNoInterfaceService 类型，无接口
/// </summary>
[Transient]
internal class TransientNoInterfaceService
{
}

/// <summary>
/// 注册为 ITransientDefaultInterfaceAService 类型（默认第一个接口）
/// </summary>
[Transient]
internal class TransientDefaultInterfaceService : ITransientDefaultInterfaceAService,
    ITransientDefaultInterfaceBService
{
}

/// <summary>
/// 注册为 ITransientExplicitInterfaceBService 类型（指定单个接口）
/// </summary>
[Transient(typeof(ITransientExplicitInterfaceBService))]
internal class TransientExplicitInterfaceService : ITransientExplicitInterfaceAService,
    ITransientExplicitInterfaceBService
{
}

/// <summary>
/// 注册为 ITransientMultiInterfaceBService 和 ITransientMultiInterfaceCService 类型（指定多个接口）
/// </summary>
[Transient(typeof(ITransientMultiInterfaceBService), typeof(ITransientMultiInterfaceCService))]
internal class TransientMultiInterfaceService : ITransientMultiInterfaceAService, ITransientMultiInterfaceBService,
    ITransientMultiInterfaceCService
{
}

/// <summary>
/// 注册为 ITransientFallbackInterfaceAService 类型（回退到第一个接口）
/// </summary>
[Transient(typeof(ITransientFallbackInterfaceBService))]
internal class TransientFallbackInterfaceService : ITransientFallbackInterfaceAService
{
}
