using BuildingBlocks.Common;
using Identity.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.Commands.RefreshToken
{
    public record RefreshTokenCommand(
            string Token
        ) : IRequest<Result<AuthResponseDto>>;
}
