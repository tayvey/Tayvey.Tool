using Microsoft.Extensions.DependencyInjection;

namespace TayveyTool.Interfaces;

/// <summary>
/// 依赖注入生命周期构建器
/// </summary>
public interface IDiLifetimeBuilder : IDiBuilder
{
    /// <summary>
    /// 使用接口注册
    /// 指定类
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    public IDiLifetimeBuilder UseInterfaces(
        Func<Type, bool> predicate,
        params Type[] types
    );

    /// <summary>
    /// 构建依赖注入
    /// </summary>
    /// <param name="service"></param>
    /// <exception cref="Exception"></exception>
    public void Build(IServiceCollection service);
}
