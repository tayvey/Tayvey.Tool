using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TayveyTool.Attributes;

namespace TayveyTool;

/// <summary>
/// 依赖注入扩展
/// </summary>
public static class DiExtension
{
    /// <summary>
    /// 添加依赖注入服务
    /// </summary>
    /// <param name="service"></param>
    /// <exception cref="Exception"></exception>
    public static void AddDiServices(this IServiceCollection service)
    {
        IEnumerable<(Type t, List<BaseAttribute>)> serviceTuples = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.GetTypes()
                .Where(t => t is { IsClass: true, IsAbstract: false })
                .Where(t => t.GetCustomAttributes<BaseAttribute>().Any())
                .Select(t => (t, t.GetCustomAttributes<BaseAttribute>().ToList())));

        foreach ((Type type, List<BaseAttribute> attributes) in serviceTuples)
        {
            if (attributes.Count > 1)
            {
                throw new($"依赖注入类 {type.FullName} 失败. 不明确的生命周期");
            }

            BaseAttribute attribute = attributes.Single();
            if (attribute.Self)
            {
                service.AddSelf(type, attribute);
                continue;
            }

            List<Type> interfaces = [.. type.GetInterfaces()];
            if (interfaces.Count == 0)
            {
                service.AddSelf(type, attribute);
                continue;
            }

            if (attribute.Interfaces.Length == 0)
            {
                service.AddInterface(interfaces[0], type, attribute);
                continue;
            }

            interfaces = [.. interfaces.Where(i => attribute.Interfaces.Contains(i))];
            if (interfaces.Count == 0)
            {
                service.AddInterface(interfaces[0], type, attribute);
                continue;
            }

            foreach (Type interfaceType in interfaces)
            {
                service.AddInterface(interfaceType, type, attribute);
            }
        }
    }

    /// <summary>
    /// 不使用接口注册
    /// </summary>
    /// <param name="service"></param>
    /// <param name="type"></param>
    /// <param name="attribute"></param>
    private static void AddSelf(this IServiceCollection service, Type type, BaseAttribute attribute)
    {
        _ = attribute switch
        {
            ScopedAttribute => service.AddScoped(type),
            TransientAttribute => service.AddTransient(type),
            _ => service.AddSingleton(type)
        };
    }

    /// <summary>
    /// 使用接口注册
    /// </summary>
    /// <param name="service"></param>
    /// <param name="interfaceType"></param>
    /// <param name="type"></param>
    /// <param name="attribute"></param>
    private static void AddInterface(this IServiceCollection service, Type interfaceType, Type type,
        BaseAttribute attribute)
    {
        _ = attribute switch
        {
            ScopedAttribute => service.AddScoped(interfaceType, type),
            TransientAttribute => service.AddTransient(interfaceType, type),
            _ => service.AddSingleton(interfaceType, type)
        };
    }
}
