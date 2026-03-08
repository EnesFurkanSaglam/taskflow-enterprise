using BuildingBlocks.Common;
using Identity.Application.DTOs;
using Identity.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.Queries.GetUserById
{
    public class GetUserByIdQueryHandler
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository; 
        }

        public async Task<Result<UserDto>> Handle(GetUserByIdQuery request,CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
                return Result<UserDto>.Failure("Kullanıcı bulunamadı");

            var dto = new UserDto(
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                user.Role,
                user.IsActive,
                user.CreatedAt
                );

            return Result<UserDto>.Success(dto);
        }
    }
}
