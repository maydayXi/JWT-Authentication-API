using System.ComponentModel;

namespace JWT_Authentication_API.Enums;
/// <summary>
/// 使用者角色
/// </summary>
public enum UserRole
{
    [Description("實習生")]
    Intern,
    [Description("人資助理")]
    HrAssistant,
    [Description("人資主管")]
    HrHead
}