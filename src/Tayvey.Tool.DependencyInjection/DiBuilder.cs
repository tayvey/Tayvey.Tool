using Microsoft.Extensions.DependencyInjection;
using TayveyTool.Interfaces;
using TayveyTool.Models;
using static TayveyTool.Models.DiService;

namespace TayveyTool;

/// <summary>
/// 依赖注入构建器
/// </summary>
internal class DiBuilder : IDiBuilder
{
    /// <summary>
    /// 依赖注入
    /// </summary>
    private readonly Di _di;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="services"></param>
    internal DiBuilder(IEnumerable<DiService> services)
    {
        _di = new(services);
    }

    /// <summary>
    /// 强制不使用接口注册
    /// 所有类
    /// </summary>
    /// <returns></returns>
    public IDiBuilder UseSelf()
    {
        foreach (DiService service in _di.Services)
        {
            service.Self = true;
        }

        return this;
    }

    /// <summary>
    /// 强制不使用接口注册
    /// 指定类
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public IDiBuilder UseSelf(Func<Type, bool> predicate)
    {
        foreach (DiService service in _di.Services.Where(service => predicate(service.ServiceType)))
        {
            service.Self = true;
        }

        return this;
    }

    /// <summary>
    /// 使用作用域生命周期
    /// 所有类
    /// </summary>
    /// <returns></returns>
    public IDiBuilder UseScoped()
    {
        foreach (DiService service in _di.Services)
        {
            service.Lifetimes.Add(LifetimeEnum.Scoped);
        }

        return this;
    }

    /// <summary>
    /// 使用作用域生命周期
    /// 指定类
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public IDiBuilder UseScoped(Func<Type, bool> predicate)
    {
        foreach (DiService service in _di.Services.Where(service => predicate(service.ServiceType)))
        {
            service.Lifetimes.Add(LifetimeEnum.Scoped);
        }

        return this;
    }

    /// <summary>
    /// 使用瞬时生命周期
    /// 所有类
    /// </summary>
    /// <returns></returns>
    public IDiBuilder UseTransient()
    {
        foreach (DiService service in _di.Services)
        {
            service.Lifetimes.Add(LifetimeEnum.Transient);
        }

        return this;
    }

    /// <summary>
    /// 使用瞬时生命周期
    /// 指定类
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public IDiBuilder UseTransient(Func<Type, bool> predicate)
    {
        foreach (DiService service in _di.Services.Where(service => predicate(service.ServiceType)))
        {
            service.Lifetimes.Add(LifetimeEnum.Transient);
        }

        return this;
    }

    /// <summary>
    /// 使用单例生命周期
    /// 所有类
    /// </summary>
    /// <returns></returns>
    public IDiBuilder UseSingleton()
    {
        foreach (DiService service in _di.Services)
        {
            service.Lifetimes.Add(LifetimeEnum.Singleton);
        }

        return this;
    }

    /// <summary>
    /// 使用单例生命周期
    /// 指定类
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public IDiBuilder UseSingleton(Func<Type, bool> predicate)
    {
        foreach (DiService service in _di.Services.Where(service => predicate(service.ServiceType)))
        {
            service.Lifetimes.Add(LifetimeEnum.Singleton);
        }

        return this;
    }

    /// <summary>
    /// 使用接口注册
    /// 指定类
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    public IDiBuilder UseInterfaces(Func<Type, bool> predicate, params Type[] types)
    {
        List<Type> typeList = types.ToList();

        foreach (DiService service in _di.Services.Where(service => predicate(service.ServiceType)))
        {
            service.Interfaces.AddRange(typeList);
        }

        return this;
    }

    /// <summary>
    /// 构建依赖注入
    /// </summary>
    /// <param name="service"></param>
    /// <exception cref="Exception"></exception>
    public void Build(IServiceCollection service)
    {
        foreach (DiService diService in _di.Services)
        {
            switch (diService.Lifetimes.Count)
            {
                case 0:
                    continue;
                case > 1:
                    throw new($"依赖注入类 {diService.ServiceType.FullName} 失败. 不明确的生命周期");
            }

            if (diService.Self)
            {
                AddSelf(service, diService.ServiceType, diService.Lifetimes[0]);
                continue;
            }

            List<Type> interfaces = [.. diService.ServiceType.GetInterfaces()];
            if (interfaces.Count == 0)
            {
                AddSelf(service, diService.ServiceType, diService.Lifetimes[0]);
                continue;
            }

            if (diService.Interfaces.Count == 0)
            {
                AddInterface(service, interfaces[0], diService.ServiceType, diService.Lifetimes[0]);
                continue;
            }

            List<Type> explicitInterfaces = [.. interfaces.Where(i => diService.Interfaces.Contains(i))];
            if (explicitInterfaces.Count == 0)
            {
                AddInterface(service, interfaces[0], diService.ServiceType, diService.Lifetimes[0]);
                continue;
            }

            foreach (Type interfaceType in explicitInterfaces)
            {
                AddInterface(service, interfaceType, diService.ServiceType, diService.Lifetimes[0]);
            }
        }
    }

    /// <summary>
    /// 筛选类
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public IDiBuilder Where(Func<Type, bool> predicate)
    {
        _di.Services = _di.Services.Where(s => predicate(s.ServiceType)).ToList();
        return this;
    }

    /// <summary>
    /// 不使用接口注册
    /// </summary>
    /// <param name="service"></param>
    /// <param name="type"></param>
    /// <param name="lifetime"></param>
    private static void AddSelf(IServiceCollection service, Type type, LifetimeEnum lifetime)
    {
        _ = lifetime switch
        {
            LifetimeEnum.Scoped => service.AddScoped(type),
            LifetimeEnum.Transient => service.AddTransient(type),
            _ => service.AddSingleton(type)
        };
    }

    /// <summary>
    /// 使用接口注册
    /// </summary>
    /// <param name="service"></param>
    /// <param name="interfaceType"></param>
    /// <param name="type"></param>
    /// <param name="lifetime"></param>
    private static void AddInterface(IServiceCollection service, Type interfaceType, Type type,
        LifetimeEnum lifetime)
    {
        _ = lifetime switch
        {
            LifetimeEnum.Scoped => service.AddScoped(interfaceType, type),
            LifetimeEnum.Transient => service.AddTransient(interfaceType, type),
            _ => service.AddSingleton(interfaceType, type)
        };
    }
}
