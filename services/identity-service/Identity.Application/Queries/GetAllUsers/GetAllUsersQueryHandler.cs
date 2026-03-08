using Identity.Application.DTOs;
using Identity.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery,List<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(u => new UserDto(
                u.Id,
                u.Email,
                u.FirstName,
                u.LastName,
                u.Role,
                u.IsActive,
                u.CreatedAt
                )).ToList();
        }
    }
}
