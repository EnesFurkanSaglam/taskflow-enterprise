using Identity.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.DTOs
{
    public record UserDto(
        Guid Id,
        string Email,
        string FirstName,
        string LastName,
        RoleType Role,
        bool IsActive,
        DateTime CreatedAt
        ); 
}
