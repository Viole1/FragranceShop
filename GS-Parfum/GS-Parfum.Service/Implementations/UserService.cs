using GS_Parfum.DAL.Interfaces;
using GS_Parfum.DAL.Repositories;
using GS_Parfum.Domain.Entity.Product;
using GS_Parfum.Domain.Entity.User;
using GS_Parfum.Domain.Enum;
using GS_Parfum.Domain.Request;
using GS_Parfum.Domain.Response;
using GS_Parfum.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;

namespace GS_Parfum.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly string secretKey;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            secretKey = ConfigurationManager.AppSettings["JwtSecret"];
        }

        public async Task<BaseResponse<User>> GetUserById(int id)
        {
            var baseResponse = new BaseResponse<User>();
            try
            {
                var user = await _userRepository.Get(id);
                if (user == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.ResponseStatus = ResponseStatus.UserNotFound;
                    return baseResponse;
                }
                baseResponse.Data = user;
                baseResponse.ResponseStatus = ResponseStatus.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[GetUserById] : {ex.Message}",
                };
            }
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            try
            {
                var user = await _userRepository.VerifyUser(loginRequest);
                if (user == null)
                {
                    return new LoginResponse()
                    {
                        Data = null,
                        ResponseStatus = ResponseStatus.UserNotFound,
                        Description = "User not found.",
                        Token = ""
                    };
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secretKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                LoginResponse loginResponse = new LoginResponse()
                {
                    Data = user,
                    ResponseStatus = ResponseStatus.OK,
                    Description = "Ok.",
                    Token = tokenHandler.WriteToken(token)
                };
                return loginResponse;
            }
            catch (Exception ex)
            {
                return new LoginResponse()
                {
                    Data = null,
                    ResponseStatus = ResponseStatus.InternalServerError,
                    Description = $"[Login] : {ex.Message}",
                    Token = ""
                };
            }
        }

        public async Task<BaseResponse<User>> Register(RegistrationRequest registrationRequest)
        {
            var baseResponse = new BaseResponse<User>();
            try
            {
                User user = new User()
                {
                    Username = registrationRequest.Username,
                    Email = registrationRequest.Email,
                    Password = registrationRequest.Password,
                    Role = Role.ROLE_USER,

                    Name = registrationRequest.Name,
                    Surname = registrationRequest.Surname,
                    PhoneNumber = registrationRequest.PhoneNumber,

                    DeliveryName = registrationRequest.DeliveryName,
                    DeliveryCity = registrationRequest.DeliveryCity,
                    DeliveryStreet = registrationRequest.DeliveryStreet,
                    DeliveryHomeNumber = registrationRequest.DeliveryHomeNumber,
                };
                await _userRepository.Create(user);
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[Register] : {ex.Message}",
                    ResponseStatus = ResponseStatus.InternalServerError,
                    Data = null,
                };
            }
            baseResponse.ResponseStatus = ResponseStatus.OK;
            return baseResponse;
        }

        public async Task<BaseResponse<bool>> Update(User user)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var newUser = await _userRepository.Get(user.Id);
                if (newUser == null)
                {
                    baseResponse.Description = "User not found.";
                    baseResponse.ResponseStatus = ResponseStatus.UserNotFound;
                    return baseResponse;
                }

                newUser.Username = user.Username;
                newUser.Password = user.Password;
                newUser.Email = user.Email;
                newUser.PhoneNumber = user.PhoneNumber;
                newUser.Surname = user.Surname;
                newUser.Role = user.Role;
                newUser.DeliveryCity = user.DeliveryCity;
                newUser.DeliveryHomeNumber = user.DeliveryHomeNumber;
                newUser.DeliveryName = user.DeliveryName;
                newUser.DeliveryStreet = user.DeliveryStreet;

                await _userRepository.Update(newUser);
                baseResponse.ResponseStatus = ResponseStatus.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[Update] : {ex.Message}",
                    ResponseStatus = ResponseStatus.InternalServerError,
                };
            }
        }
    }
}
