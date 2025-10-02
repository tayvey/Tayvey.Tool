namespace TayveyTool.Attributes;

/// <summary>
/// 依赖注入标记基类
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public abstract class BaseAttribute(params Type[] interfaces) : Attribute
{
    /// <summary>
    /// 强制不使用接口注册
    /// </summary>
    public bool Self { get; set; }

    /// <summary>
    /// 注册接口列表
    /// </summary>
    public Type[] Interfaces { get; } = interfaces;
}
