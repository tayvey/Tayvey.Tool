using System.Reflection;
using TayveyTool.Interfaces;
using TayveyTool.Models;

namespace TayveyTool;

/// <summary>
/// 依赖注入
/// </summary>
public class Di
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="services"></param>
    internal Di(IEnumerable<DiService> services)
    {
        Services = [.. services];
    }

    /// <summary>
    /// 依赖注入服务列表
    /// </summary>
    internal List<DiService> Services { get; set; }

    /// <summary>
    /// 创建依赖注入构建器
    /// 所有程序集中的类
    /// </summary>
    /// <returns></returns>
    public static IDiBuilder Create()
    {
        IEnumerable<DiService> services = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a
                .GetTypes()
                .Where(t => t is { IsClass: true, IsAbstract: false })
                .Select(t => new DiService(t)));

        return new DiBuilder(services);
    }

    /// <summary>
    /// 创建依赖注入构建器
    /// 指定类型所在程序集中的类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IDiBuilder Create<T>()
    {
        IEnumerable<DiService> services = typeof(T)
            .Assembly
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .Select(t => new DiService(t));

        return new DiBuilder(services);
    }

    /// <summary>
    /// 创建依赖注入构建器
    /// 指定类型所在程序集中的类
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    public static IDiBuilder Create(params Type[] types)
    {
        IEnumerable<DiService> services = types
            .SelectMany(t => t
                .Assembly
                .GetTypes()
                .Where(at => at is { IsClass: true, IsAbstract: false })
                .Select(at => new DiService(at)));

        return new DiBuilder(services);
    }

    /// <summary>
    /// 创建依赖注入构建器
    /// 指定程序集中的类
    /// </summary>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    public static IDiBuilder Create(params Assembly[] assemblies)
    {
        IEnumerable<DiService> services = assemblies
            .SelectMany(a => a
                .GetTypes()
                .Where(t => t is { IsClass: true, IsAbstract: false })
                .Select(t => new DiService(t)));

        return new DiBuilder(services);
    }
}
