using BCrypt.Net;
using BuildingBlocks.Common;
using Identity.Application.DTOs;
using Identity.Application.Interfaces;
using Identity.Domain.Entities;
using Identity.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.Commands.Login
{
    public class LoginCommandHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _tokenService = tokenService;
        }

        public async Task<Result<AuthResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
                return Result<AuthResponseDto>.Failure("Email veya şifre hatalı");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Result<AuthResponseDto>.Failure("Email veya şifre hatalı");

            if (!user.IsActive)
                return Result<AuthResponseDto>.Failure("Hesap devre dışı");

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshTokenValue = _tokenService.GenerateRefreshToken();

            var refreshToken = new Domain.Entities.RefreshToken
            {
                Token = refreshTokenValue,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
            };

            await _refreshTokenRepository.AddAsync(refreshToken);

            return Result<AuthResponseDto>.Success(new AuthResponseDto(
                    accessToken,
                    refreshTokenValue,
                    DateTime.UtcNow.AddMinutes(15)
                ));
        }    
    }
}
