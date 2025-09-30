namespace TayveyTool.Attributes;

/// <summary>
/// 作用域模式标记
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class ScopedAttribute(params Type[] interfaces) : BaseAttribute(interfaces)
{
}
