using BuildingBlocks.Common;
using Identity.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.Commands.Login
{
    public record LoginCommand
    (
        string Email,
        string Password
     ) : IRequest<Result<AuthResponseDto>>; 
}
