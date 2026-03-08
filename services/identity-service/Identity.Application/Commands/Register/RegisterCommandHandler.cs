using BCrypt.Net;
using BuildingBlocks.Common;
using Identity.Application.DTOs;
using Identity.Domain.Entities;
using Identity.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.Commands.Register
{
    public class RegisterCommandHandler
    {
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
                return Result<UserDto>.Failure("Bu email adresi zaten kayıtlı");

            var user = new User
            {
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            await _userRepository.AddAsync(user);

            var dto = new UserDto(
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                user.Role,
                user.IsActive,
                user.CreatedAt
                );

            return Result<UserDto>.Success( dto );

        }
    }
}
