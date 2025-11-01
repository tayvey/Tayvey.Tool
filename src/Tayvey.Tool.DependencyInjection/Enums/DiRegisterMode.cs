namespace TayveyTool.Enums;

/// <summary>
/// 依赖注入注册模式
/// </summary>
internal enum DiRegisterMode
{
    /// <summary>
    /// 不使用接口
    /// </summary>
    Self,

    /// <summary>
    /// 使用第一个接口
    /// </summary>
    First,

    /// <summary>
    /// 使用所有接口
    /// </summary>
    All
}
