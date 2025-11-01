namespace TayveyTool.Interfaces;

/// <summary>
/// 依赖注入构建器-生命周期
/// </summary>
public interface IDiBuilderLifeCycle
{
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

    /// <summary>
    /// 构建
    /// </summary>
    public void Build();
}
