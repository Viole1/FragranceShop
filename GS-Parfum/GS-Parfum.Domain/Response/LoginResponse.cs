using GS_Parfum.Domain.Entity.User;
using GS_Parfum.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Parfum.Domain.Response
{
    public class LoginResponse : IBaseRepsonse<User>
    {
        public User Data { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
        public string Description { get; set; }
        public string Token { get; set; }
    }
}
