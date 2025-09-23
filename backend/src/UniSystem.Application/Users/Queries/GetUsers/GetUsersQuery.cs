using MediatR;
using System.Collections.Generic;

namespace UniSystem.Application.Users.Queries.GetUsers
{
    public record GetUsersQuery : IRequest<List<UserDto>>
    {
    }
}