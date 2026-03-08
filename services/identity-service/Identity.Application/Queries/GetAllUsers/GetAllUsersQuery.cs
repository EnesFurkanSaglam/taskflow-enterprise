using Identity.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.Queries.GetAllUsers
{
    public record GetAllUsersQuery : IRequest<List<UserDto>>;
}
