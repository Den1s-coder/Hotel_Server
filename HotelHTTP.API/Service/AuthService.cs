﻿using Hotel.API.Requests;
using Hotel.Domain.Interfaces.Auth;
using Hotel.Domain.Interfaces.Repo;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Service
{
    public class AuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(IUserRepository userRepo, ITokenService tokenService, IPasswordHasher passwordHasher)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }

        public async Task Register(RegisterRequest request)
        {
            var existing = await _userRepo.GetByEmailAsync(request.Email);
            if (existing != null)
            {
                throw new InvalidOperationException("Користувач з такою електронною поштою вже існує.");
            }

            var hashedPassword = _passwordHasher.Generate(request.Password);
            var user = Hotel.Domain.Entities.User.Create(Guid.NewGuid(), request.Name, hashedPassword, request.Email, request.Role);

            await _userRepo.Add(user);
        }

        public async Task<string> Login(LoginRequest request)
        {
            var user = await _userRepo.GetByEmailAsync(request.Email);
            if (user == null)
            {
                throw new InvalidOperationException("Невірна пошта або пароль.");
            }
            if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                throw new InvalidOperationException("Невірна пошта або пароль.");
            }

            var token = _tokenService.GenerateToken(user.Email,user.Role);

            return token;
        }

    }
}
