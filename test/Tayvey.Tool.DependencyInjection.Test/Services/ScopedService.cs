using TayveyTool.Attributes;
using TayveyTool.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace TayveyTool.Services;

/// <summary>
/// 注册为 ScopedSelfService 类型，不使用接口
/// </summary>
[Scoped(Self = true)]
internal class ScopedSelfService : IScopedSelfService
{
}

/// <summary>
/// 注册为 ScopedNoInterfaceService 类型，无接口
/// </summary>
[Scoped]
internal class ScopedNoInterfaceService
{
}

/// <summary>
/// 注册为 IScopedDefaultInterfaceAService 类型（默认第一个接口）
/// </summary>
[Scoped]
internal class ScopedDefaultInterfaceService : IScopedDefaultInterfaceAService, IScopedDefaultInterfaceBService
{
}

/// <summary>
/// 注册为 IScopedExplicitInterfaceBService 类型（指定单个接口）
/// </summary>
[Scoped(typeof(IScopedExplicitInterfaceBService))]
internal class ScopedExplicitInterfaceService : IScopedExplicitInterfaceAService, IScopedExplicitInterfaceBService
{
}

/// <summary>
/// 注册为 IScopedMultiInterfaceBService 和 IScopedMultiInterfaceCService 类型（指定多个接口）
/// </summary>
[Scoped(typeof(IScopedMultiInterfaceBService), typeof(IScopedMultiInterfaceCService))]
internal class ScopedMultiInterfaceService : IScopedMultiInterfaceAService, IScopedMultiInterfaceBService,
    IScopedMultiInterfaceCService
{
}

/// <summary>
/// 注册为 IScopedFallbackInterfaceAService 类型（回退到第一个接口）
/// </summary>
[Scoped(typeof(IScopedFallbackInterfaceBService))]
internal class ScopedFallbackInterfaceService : IScopedFallbackInterfaceAService
{
}
