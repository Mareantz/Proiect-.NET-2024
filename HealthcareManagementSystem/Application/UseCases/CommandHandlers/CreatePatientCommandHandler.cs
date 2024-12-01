using Application.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Domain.Common;
using MediatR;

namespace Application.CommandHandlers
{
	public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Result<Guid>>
    {
        private readonly IPatientRepository repository;
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public CreatePatientCommandHandler(IPatientRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<Result<Guid>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var userId = await userRepository.GetUserIdAsync(request.Email);
            if (userId == Guid.Empty)
            {
                return Result<Guid>.Failure("User not found.");
            }
            DateOnly dateOfBirth = ParseDateOfBirth(request.DateOfBirth);
			var patient = mapper.Map<Patient>(request);
			patient.DateOfBirth = dateOfBirth;
            patient.UserId = userId;
			var result = await repository.AddPatient(patient);
            if(result.IsSuccess)
            {
                return Result<Guid>.Success(result.Data);
			}
            return Result<Guid>.Failure(result.ErrorMessage);
		}

		private static DateOnly ParseDateOfBirth(string dateOfBirth)
		{
			return DateOnly.ParseExact(dateOfBirth, "dd-MM-yyyy");
		}

	}
}
