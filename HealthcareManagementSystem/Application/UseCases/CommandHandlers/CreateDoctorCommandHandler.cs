using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Domain.Common;
using MediatR;
using Application.UseCases.Commands;

namespace Application.UseCases.CommandHandlers
{
    public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, Result<Guid>>
    {
        private readonly IDoctorRepository repository;
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public CreateDoctorCommandHandler(IDoctorRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<Result<Guid>> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            var userId = await userRepository.GetUserIdAsync(request.Email);
            if (userId == Guid.Empty)
            {
                return Result<Guid>.Failure("User not found.");
            }

            var doctor = mapper.Map<Doctor>(request);
            doctor.UserId = userId;
            var result = await repository.AddDoctor(doctor);
            if (result.IsSuccess)
            {
                return Result<Guid>.Success(result.Data);
            }
            return Result<Guid>.Failure(result.ErrorMessage);
        }
    }

}
