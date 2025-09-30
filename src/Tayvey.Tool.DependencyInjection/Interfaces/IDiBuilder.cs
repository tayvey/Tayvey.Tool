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
    public IDiLifetimeBuilder UseSelf();

    /// <summary>
    /// 强制不使用接口注册
    /// 指定类
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public IDiLifetimeBuilder UseSelf(Func<Type, bool> predicate);

    /// <summary>
    /// 使用作用域生命周期
    /// 所有类
    /// </summary>
    /// <returns></returns>
    public IDiLifetimeBuilder UseScoped();

    /// <summary>
    /// 使用作用域生命周期
    /// 指定类
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public IDiLifetimeBuilder UseScoped(Func<Type, bool> predicate);

    /// <summary>
    /// 使用瞬时生命周期
    /// 所有类
    /// </summary>
    /// <returns></returns>
    public IDiLifetimeBuilder UseTransient();

    /// <summary>
    /// 使用瞬时生命周期
    /// 指定类
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public IDiLifetimeBuilder UseTransient(Func<Type, bool> predicate);

    /// <summary>
    /// 使用单例生命周期
    /// 所有类
    /// </summary>
    /// <returns></returns>
    public IDiLifetimeBuilder UseSingleton();

    /// <summary>
    /// 使用单例生命周期
    /// 指定类
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public IDiLifetimeBuilder UseSingleton(Func<Type, bool> predicate);
}
