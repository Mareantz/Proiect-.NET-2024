using Application.DTOs;
using Application.UseCases.Queries;
using Application.Utils;
using AutoMapper;
using Domain.Common;
using Domain.Repositories;
using Gridify;
using MediatR;

namespace Application.UseCases.QueryHandlers
{
    public class GetFilteredUsersQueryHandler : IRequestHandler<GetFilteredUsersQuery, Result<PagedResult<UserDto>>>
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;

        public GetFilteredUsersQueryHandler(IUserRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Result<PagedResult<UserDto>>> Handle(GetFilteredUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await repository.GetAllAsync();
            var query = users.AsQueryable();

            // Apply paging
            var pagedUsers = query.ApplyPaging(request.Page, request.PageSize);

            var userDtos = mapper.Map<List<UserDto>>(pagedUsers);

            var pagedResult = new PagedResult<UserDto>(userDtos, query.Count());

            return Result<PagedResult<UserDto>>.Success(pagedResult);
        }
    }
}
