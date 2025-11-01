using Microsoft.Extensions.DependencyInjection;
using TayveyTool.Enums;
using TayveyTool.Interfaces;
using TayveyTool.Models;

namespace TayveyTool;

/// <summary>
/// 依赖注入构建器
/// </summary>
internal class DiBuilder : IDiBuilder, IDiBuilderMode, IDiBuilderLifeCycle
{
    /// <summary>
    /// 服务容器
    /// </summary>
    private readonly IServiceCollection _services;

    /// <summary>
    /// 依赖注入服务集合
    /// </summary>
    private List<DiService>? _diServices;

    /// <summary>
    /// 依赖注入服务类型集合
    /// </summary>
    private IEnumerable<Type> _types;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="services"></param>
    /// <param name="types"></param>
    internal DiBuilder(
        IServiceCollection services,
        IEnumerable<Type> types
    )
    {
        _services = services;
        _types = types;
    }

    /// <summary>
    /// 筛选依赖注入服务类型集合
    /// </summary>
    /// <param name="predicate">筛选条件</param>
    /// <returns></returns>
    public IDiBuilder Where(Func<Type, bool> predicate)
    {
        _types = _types.Where(predicate);
        return this;
    }

    /// <summary>
    /// 瞬时
    /// </summary>
    /// <param name="predicate">筛选条件</param>
    /// <returns></returns>
    public IDiBuilderLifeCycle Transient(Func<Type, bool>? predicate = null)
    {
        List<DiService> diServices = ToDiServices();
        ForEach(diServices, predicate, service => { service.LifeCycle ??= DiLifeCycle.Transient; });
        return this;
    }

    /// <summary>
    /// 作用域
    /// </summary>
    /// <param name="predicate">筛选条件</param>
    /// <returns></returns>
    public IDiBuilderLifeCycle Scoped(Func<Type, bool>? predicate = null)
    {
        List<DiService> diServices = ToDiServices();
        ForEach(diServices, predicate, service => { service.LifeCycle ??= DiLifeCycle.Scoped; });
        return this;
    }

    /// <summary>
    /// 单例
    /// </summary>
    /// <param name="predicate">筛选条件</param>
    /// <returns></returns>
    public IDiBuilderLifeCycle Singleton(Func<Type, bool>? predicate = null)
    {
        List<DiService> diServices = ToDiServices();
        ForEach(diServices, predicate, service => { service.LifeCycle ??= DiLifeCycle.Singleton; });
        return this;
    }

    /// <summary>
    /// 构建
    /// </summary>
    public void Build()
    {
        List<DiService> diServices = ToDiServices();

        foreach (DiService diService in diServices)
        {
            if (diService.RegisterMode == null || diService.LifeCycle == null)
            {
                continue;
            }

            if (diService.RegisterMode == DiRegisterMode.Self)
            {
                AddService(diService.ServiceType, diService.LifeCycle);
                continue;
            }

            List<Type> interfaces = [.. diService.ServiceType.GetInterfaces()];
            List<Type> baseInterfaces = [.. diService.ServiceType.BaseType?.GetInterfaces() ?? []];
            List<Type> selfInterfaces = interfaces.Except(baseInterfaces).ToList();

            if (selfInterfaces.Count == 0)
            {
                continue;
            }

            if (diService.RegisterMode == DiRegisterMode.First)
            {
                AddService(diService.ServiceType, diService.LifeCycle, selfInterfaces[0]);
                continue;
            }

            foreach (Type interfaceType in selfInterfaces)
            {
                AddService(diService.ServiceType, diService.LifeCycle, interfaceType);
            }
        }
    }

    /// <summary>
    /// 不使用接口
    /// </summary>
    /// <param name="predicate">筛选条件</param>
    /// <returns></returns>
    public IDiBuilderMode SelfMode(Func<Type, bool>? predicate = null)
    {
        List<DiService> diServices = ToDiServices();
        ForEach(diServices, predicate, service => { service.RegisterMode ??= DiRegisterMode.Self; });
        return this;
    }

    /// <summary>
    /// 使用第一个接口
    /// </summary>
    /// <param name="predicate">筛选条件</param>
    /// <returns></returns>
    public IDiBuilderMode FirstMode(Func<Type, bool>? predicate = null)
    {
        List<DiService> diServices = ToDiServices();
        ForEach(diServices, predicate, service => { service.RegisterMode ??= DiRegisterMode.First; });
        return this;
    }

    /// <summary>
    /// 使用所有接口
    /// </summary>
    /// <param name="predicate">筛选条件</param>
    /// <returns></returns>
    public IDiBuilderMode AllMode(Func<Type, bool>? predicate = null)
    {
        List<DiService> diServices = ToDiServices();
        ForEach(diServices, predicate, service => { service.RegisterMode ??= DiRegisterMode.All; });
        return this;
    }

    /// <summary>
    /// 转换为依赖注入服务集合
    /// </summary>
    private List<DiService> ToDiServices()
    {
        if (_diServices != null)
        {
            return _diServices;
        }

        _diServices = _types
            .Select(t => new DiService(t))
            .ToList();

        return _diServices;
    }

    /// <summary>
    /// 遍历依赖注入服务集合
    /// </summary>
    /// <param name="diServices">依赖注入服务集合</param>
    /// <param name="predicate">筛选条件</param>
    /// <param name="call">遍历调用</param>
    private static void ForEach(List<DiService> diServices, Func<Type, bool>? predicate, Action<DiService> call)
    {
        foreach (DiService service in diServices.Where(d => predicate is null || predicate(d.ServiceType)))
        {
            call(service);
        }
    }

    /// <summary>
    /// 添加服务
    /// </summary>
    /// <param name="type"></param>
    /// <param name="lifeCycle"></param>
    /// <param name="interfaceType"></param>
    private void AddService(Type type, DiLifeCycle? lifeCycle, Type? interfaceType = null)
    {
        if (lifeCycle == null)
        {
            return;
        }

        _ = lifeCycle.Value switch
        {
            DiLifeCycle.Scoped => interfaceType == null
                ? _services.AddScoped(type)
                : _services.AddScoped(interfaceType, type),
            DiLifeCycle.Transient => interfaceType == null
                ? _services.AddTransient(type)
                : _services.AddTransient(interfaceType, type),
            _ => interfaceType == null
                ? _services.AddSingleton(type)
                : _services.AddSingleton(interfaceType, type)
        };
    }
}
