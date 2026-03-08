using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.DTOs
{
    public record AuthResponseDto(
        string AccessToken,
        string RefreshToken,
        DateTime ExpiresAt
        );
}
