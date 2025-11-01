using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TayveyTool.Interfaces;

namespace TayveyTool;

/// <summary>
/// 依赖注入扩展
/// </summary>
public static class DiExtension
{
    /// <summary>
    /// 创建依赖注入构建器 (所有程序集中的类)
    /// </summary>
    /// <param name="services">服务容器</param>
    /// <returns></returns>
    public static IDiBuilder CreateDi(this IServiceCollection services)
    {
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        return CreateDiBuilder(services, assemblies);
    }

    /// <summary>
    /// 创建依赖注入构建器 (指定类型所在程序集中的类)
    /// </summary>
    /// <param name="services">服务容器</param>
    /// <typeparam name="TType">指定类型泛型</typeparam>
    /// <returns></returns>
    public static IDiBuilder CreateDi<TType>(this IServiceCollection services)
    {
        Assembly assembly = typeof(TType).Assembly;
        return CreateDiBuilder(services, assembly);
    }

    /// <summary>
    /// 创建依赖注入构建器 (指定类型所在程序集中的类)
    /// </summary>
    /// <param name="services">服务容器</param>
    /// <param name="types">指定类型集合</param>
    /// <returns></returns>
    public static IDiBuilder CreateDi(
        this IServiceCollection services,
        params Type[] types
    )
    {
        IEnumerable<Assembly> assemblies = types.Select(t => t.Assembly);
        return CreateDiBuilder(services, assemblies);
    }

    /// <summary>
    /// 创建依赖注入构建器 (指定程序集中的类)
    /// </summary>
    /// <param name="services">服务容器</param>
    /// <param name="assemblies">指定程序集集合</param>
    /// <returns></returns>
    public static IDiBuilder CreateDi(
        this IServiceCollection services,
        params Assembly[] assemblies
    )
    {
        return CreateDiBuilder(services, assemblies);
    }

    /// <summary>
    /// 创建依赖注入构建器
    /// </summary>
    /// <param name="services">服务容器</param>
    /// <param name="assembly">指定程序集</param>
    /// <returns></returns>
    private static DiBuilder CreateDiBuilder(
        this IServiceCollection services,
        Assembly assembly
    )
    {
        IEnumerable<Type> diTypes = assembly
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false });

        return new(services, diTypes);
    }

    /// <summary>
    /// 创建依赖注入构建器
    /// </summary>
    /// <param name="services">服务容器</param>
    /// <param name="assemblies">指定程序集集合</param>
    /// <returns></returns>
    private static DiBuilder CreateDiBuilder(
        this IServiceCollection services,
        IEnumerable<Assembly> assemblies
    )
    {
        IEnumerable<Type> diTypes = assemblies
            .SelectMany(a => a
                .GetTypes()
                .Where(t => t is { IsClass: true, IsAbstract: false }));

        return new(services, diTypes);
    }
}
