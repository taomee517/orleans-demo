using System.Collections.Generic;

namespace orleans_grains
{
    /// <summary>
    /// 登录状态
    /// </summary>
    public class LoginState
    {
        public List<string> LoginUsers { get; set; } = new List<string>();

        public int Count => LoginUsers.Count;
    }
}