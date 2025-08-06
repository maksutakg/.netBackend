
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Request;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace Application.Service
{
    public interface IUserService
    {
        Task<UserDto> CreateUser(CreateUserRequest user);
        Task<UserDto> GetUser(int id);
        Task<List<UserDto>> GetUsers();
        Task <List<UserDto>> FiltreUsers(int? id,string? name, string? surName, string? mail);
        Task<User> DeleteUser(int id);
        Task <User> HardDelete(int id );
        Task<UserDto> UpdateUser(int id ,UpdateUserRequest updateUser);
        Task<List<UserDto>> GetActiveUser();
        Task<List<User>> UserDetail();
    }
}
