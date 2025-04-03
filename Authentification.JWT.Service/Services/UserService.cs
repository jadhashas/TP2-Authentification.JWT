using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Authentification.JWT.DAL.Context;
using Authentification.JWT.DAL.Models;
using Authentification.JWT.Service.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Authentification.JWT.Service.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User?> GetEntityByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<UserResponseDto?> RegisterUserAsync(UserDto userDto)
        {
            var exists = await _context.Users.AnyAsync(u =>
                u.Username == userDto.Username || u.Email == userDto.Email);

            if (exists)
                return null;

            var user = _mapper.Map<User>(userDto);
            user.PasswordHash = HashPassword(userDto.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // retourne uniquement les champs non sensibles
            return new UserResponseDto
            {
                Username = user.Username,
                Email = user.Email
            };
        }

        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }

        public async Task<int> GetUserIdByUsernameAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user?.Id ?? 0;
        }
    }
}
