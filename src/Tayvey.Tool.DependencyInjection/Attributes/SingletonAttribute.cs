namespace TayveyTool.Attributes;

/// <summary>
/// 单例模式标记
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class SingletonAttribute(params Type[] interfaces) : BaseAttribute(interfaces)
{
}
