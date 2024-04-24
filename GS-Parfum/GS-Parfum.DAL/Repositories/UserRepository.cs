using GS_Parfum.DAL.DbContexts;
using GS_Parfum.DAL.Interfaces;
using GS_Parfum.Domain.Entity.User;
using GS_Parfum.Domain.Request;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;


namespace GS_Parfum.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _db;

        public UserRepository(UserDbContext db)
        {
            _db = db;
        }
        public bool IsUniqueUser(string username)
        {
            var user = _db.Users.FirstOrDefault(x => x.Username == username);

            return user == null;
        }
        public async Task<User> Get(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> VerifyUser(LoginRequest loginRequest)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == loginRequest.Username.ToLower()
            && u.Password == loginRequest.Password);
        }

        public async Task<bool> Create(User entity)
        {
            _db.Users.Add(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public Task<List<User>> Select()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
