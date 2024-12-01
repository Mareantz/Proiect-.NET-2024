using Application.DTOs;
using Application.Utils;
using Domain.Common;
using MediatR;

namespace Application.UseCases.Queries
{
    public class GetFilteredUsersQuery : IRequest<Result<PagedResult<UserDto>>>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
