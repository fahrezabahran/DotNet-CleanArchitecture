using AutoMapper;
using ProductApi.Application.Interfaces;
using ProductApi.Application.Interfaces.Timesheet;
using ProductApi.Application.Responses;
using ProductApi.Domain.Entities;

namespace ProductApi.Application.UseCases.Timesheet
{
    public class CreateTimesheetUseCase(IGenericRepository<TimeSheet> timesheetRepository, IMapper mapper) : ICreateTimesheetUseCase
    {
        private readonly IGenericRepository<TimeSheet> _timesheetRepository = timesheetRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse> Execute(TimeSheet timesheet, CancellationToken cancellationToken)
        {
            await _timesheetRepository.AddAsync(timesheet, cancellationToken);

            return new SuccessResponse<TimeSheet>(timesheet);
        }


    }
}
