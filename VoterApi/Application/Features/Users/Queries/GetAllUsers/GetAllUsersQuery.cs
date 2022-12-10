using Application.Persistence;
using Domain.Dtos.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Queries.GetAllUsers;

public sealed record GetAllUsersQuery : IRequest<IList<UserDto>>;

public sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IList<UserDto>>
{
    private readonly IApplicationContext _context;

    public GetAllUsersQueryHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<IList<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return await _context.User.Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname
            })
            .ToListAsync(cancellationToken);
    }
}
