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
    /// 强制不使用接口注册
    /// </summary>
    internal bool Self { get; set; }

    /// <summary>
    /// 生命周期列表
    /// </summary>
    internal List<LifetimeEnum> Lifetimes { get; } = [];

    /// <summary>
    /// 注册接口列表
    /// </summary>
    internal List<Type> Interfaces { get; } = [];

    /// <summary>
    /// 生命周期
    /// </summary>
    internal enum LifetimeEnum
    {
        Transient,
        Scoped,
        Singleton
    }
}
