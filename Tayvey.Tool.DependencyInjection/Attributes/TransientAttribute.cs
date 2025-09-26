namespace TayveyTool.Attributes;

/// <summary>
/// 瞬时模式标记
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class TransientAttribute(params Type[] interfaces) : BaseAttribute(interfaces)
{
}
