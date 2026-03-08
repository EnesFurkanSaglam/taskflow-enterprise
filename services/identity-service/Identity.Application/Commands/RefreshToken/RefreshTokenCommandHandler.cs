using BuildingBlocks.Common;
using Identity.Application.DTOs;
using Identity.Application.Interfaces;
using Identity.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public RefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository, ITokenService tokenService)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async  Task<Result<AuthResponseDto>> Handle(RefreshTokenCommand request,CancellationToken cancellationToken)
        {
            var existingToken = await _refreshTokenRepository.GetByTokenAsync(request.Token);

            if (existingToken == null)
                return Result<AuthResponseDto>.Failure("Geçersiz refresh token");

            if (existingToken.IsRevoked) 
            { 
                await _refreshTokenRepository.RevokeAllByUserIdAsync(existingToken.UserId);
                return Result<AuthResponseDto>.Failure("Token daha önce kullanılmış");
            }

            if (existingToken.IsExpired)
                return Result<AuthResponseDto>.Failure("Refresh token süresi dolmuş");


            var user = await _userRepository.GetByIdAsync(existingToken.UserId);

            if (user == null)
                return Result<AuthResponseDto>.Failure("Kullanıcı bulunamadı");

            existingToken.RevokedAt = DateTime.UtcNow;
            await _refreshTokenRepository.UpdateAsync(existingToken);

            var newAccessToken = _tokenService.GenerateAccessToken(user);
            var newRefreshTokenValue = _tokenService.GenerateRefreshToken();

            var newRefreshToken = new Domain.Entities.RefreshToken
            {
                Token = newRefreshTokenValue,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            await _refreshTokenRepository.AddAsync(newRefreshToken);

            return Result<AuthResponseDto>.Success(new AuthResponseDto(
                newAccessToken,
                newRefreshTokenValue,
                DateTime.UtcNow.AddMinutes(15)
                ));    
        }
    }
}
