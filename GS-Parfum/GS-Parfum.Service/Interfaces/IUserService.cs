using GS_Parfum.Domain.Entity.User;
using GS_Parfum.Domain.Request;
using GS_Parfum.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Parfum.Service.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task<BaseResponse<User>> Register(RegistrationRequest registrationRequest);
        Task<BaseResponse<bool>> Update(User user);
        Task<BaseResponse<User>> GetUserById(int id);
    }
}
