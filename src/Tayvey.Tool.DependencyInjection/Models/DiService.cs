using TayveyTool.Enums;

namespace TayveyTool.Models;

/// <summary>
/// 依赖注入服务
/// </summary>
internal class DiService(Type serviceType)
{
    /// <summary>
    /// 服务类型
    /// </summary>
    internal Type ServiceType { get; } = serviceType;

    /// <summary>
    /// 注册类型
    /// </summary>
    internal DiRegisterMode? RegisterMode { get; set; }

    /// <summary>
    /// 生命周期
    /// </summary>
    internal DiLifeCycle? LifeCycle { get; set; }
}
