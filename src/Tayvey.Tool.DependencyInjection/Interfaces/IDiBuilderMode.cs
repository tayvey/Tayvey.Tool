namespace TayveyTool.Interfaces;

/// <summary>
/// 依赖注入构建器-模式
/// </summary>
public interface IDiBuilderMode
{
    /// <summary>
    /// 不使用接口
    /// </summary>
    /// <param name="predicate">筛选条件</param>
    /// <returns></returns>
    public IDiBuilderMode SelfMode(Func<Type, bool>? predicate = null);

    /// <summary>
    /// 使用第一个接口
    /// </summary>
    /// <param name="predicate">筛选条件</param>
    /// <returns></returns>
    public IDiBuilderMode FirstMode(Func<Type, bool>? predicate = null);

    /// <summary>
    /// 使用所有接口
    /// </summary>
    /// <param name="predicate">筛选条件</param>
    /// <returns></returns>
    public IDiBuilderMode AllMode(Func<Type, bool>? predicate = null);

    /// <summary>
    /// 瞬时
    /// </summary>
    /// <param name="predicate">筛选条件</param>
    /// <returns></returns>
    public IDiBuilderLifeCycle Transient(Func<Type, bool>? predicate = null);

    /// <summary>
    /// 作用域
    /// </summary>
    /// <param name="predicate">筛选条件</param>
    /// <returns></returns>
    public IDiBuilderLifeCycle Scoped(Func<Type, bool>? predicate = null);

    /// <summary>
    /// 单例
    /// </summary>
    /// <param name="predicate">筛选条件</param>
    /// <returns></returns>
    public IDiBuilderLifeCycle Singleton(Func<Type, bool>? predicate = null);
}
