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
    public class GetFilteredDoctorsQueryHandler : IRequestHandler<GetFilteredDoctorsQuery, Result<PagedResult<DoctorDto>>>
    {
        private readonly IDoctorRepository repository;
        private readonly IMapper mapper;

        public GetFilteredDoctorsQueryHandler(IDoctorRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Result<PagedResult<DoctorDto>>> Handle(GetFilteredDoctorsQuery request, CancellationToken cancellationToken)
        {
            var doctors = await repository.GetAllAsync();
            var query = doctors.AsQueryable();

            // Apply paging
            var pagedDoctors = query.ApplyPaging(request.Page, request.PageSize);

            var doctorDtos = mapper.Map<List<DoctorDto>>(pagedDoctors);

            var pagedResult = new PagedResult<DoctorDto>(doctorDtos, query.Count());

            return Result<PagedResult<DoctorDto>>.Success(pagedResult);
        }
    }
}
