using Microsoft.Extensions.DependencyInjection;

namespace TayveyTool.Interfaces;

/// <summary>
/// 依赖注入构建器
/// </summary>
public interface IDiBuilder
{
    /// <summary>
    /// 强制不使用接口注册
    /// 所有类
    /// </summary>
    /// <returns></returns>
    public IDiBuilder UseSelf();

    /// <summary>
    /// 强制不使用接口注册
    /// 指定类
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public IDiBuilder UseSelf(Func<Type, bool> predicate);

    /// <summary>
    /// 使用作用域生命周期
    /// 所有类
    /// </summary>
    /// <returns></returns>
    public IDiBuilder UseScoped();

    /// <summary>
    /// 使用作用域生命周期
    /// 指定类
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public IDiBuilder UseScoped(Func<Type, bool> predicate);

    /// <summary>
    /// 使用瞬时生命周期
    /// 所有类
    /// </summary>
    /// <returns></returns>
    public IDiBuilder UseTransient();

    /// <summary>
    /// 使用瞬时生命周期
    /// 指定类
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public IDiBuilder UseTransient(Func<Type, bool> predicate);

    /// <summary>
    /// 使用单例生命周期
    /// 所有类
    /// </summary>
    /// <returns></returns>
    public IDiBuilder UseSingleton();

    /// <summary>
    /// 使用单例生命周期
    /// 指定类
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public IDiBuilder UseSingleton(Func<Type, bool> predicate);

    /// <summary>
    /// 使用接口注册
    /// 指定类
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    public IDiBuilder UseInterfaces(
        Func<Type, bool> predicate,
        params Type[] types
    );

    /// <summary>
    /// 构建依赖注入
    /// </summary>
    /// <param name="service"></param>
    /// <exception cref="Exception"></exception>
    public void Build(IServiceCollection service);

    /// <summary>
    /// 筛选类
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public IDiBuilder Where(Func<Type, bool> predicate);
}
