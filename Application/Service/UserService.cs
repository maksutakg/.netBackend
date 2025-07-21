using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class UserService : IUserService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserService()
        {
        }

        public UserService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            Log.Information($"User created {userDto.name},{userDto.surName},{userDto.Mail}");
            return _mapper.Map<UserDto>(user);

        }

        public async Task<User> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) { return null; }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            Log.Information($"User deleted with ID: {id}");
            return user;
        }

        public async Task<UserDto> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                Log.Information($"User retrieved with ID: {id}");
                return _mapper.Map<UserDto>(user);
            }
            else
            {
                Log.Information($"GetUser: User not found with ID:{id}");
                return null;
            }

        }
        public async Task<List<UserDto>> GetActiveUser()
        {
            var users = await _context.Users.Where(u => u.IsActive).OrderByDescending(u => u.DateTime).ToListAsync();
            Log.Information($"Retrieved {users.Count} active users");
            return _mapper.Map<List<UserDto>>(users);

        }

        public async Task<List<UserDto>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            Log.Information($"Retrieved {users.Count} users");
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> UpdateUser(int id, [FromBody] UserDto UpdateUser)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _mapper.Map(UpdateUser, user);
                await _context.SaveChangesAsync();
                Log.Information($"User updated with Id: {id}");
                return _mapper.Map<UserDto>(user);
            }
            else
            {
                Log.Information($"UpdateUser: not found with Id :{id}");
                return null;
            }

        }
    }
}
