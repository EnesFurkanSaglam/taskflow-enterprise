using BuildingBlocks.Common;
using Identity.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.Queries.GetUserById
{
    public record GetUserByIdQuery(Guid Id) : IRequest<Result<UserDto>>;
}
