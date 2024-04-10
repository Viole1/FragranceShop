using GS_Parfum.Domain.Entity.User;
using GS_Parfum.Domain.Request;
using GS_Parfum.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Parfum.DAL.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        bool IsUniqueUser(string username);
        Task<User> VerifyUser(LoginRequest loginRequest);
    }
}
