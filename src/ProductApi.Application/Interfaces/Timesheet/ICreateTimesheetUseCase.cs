using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductApi.Application.Responses;
using ProductApi.Domain.Entities;

namespace ProductApi.Application.Interfaces.Timesheet
{
    public interface ICreateTimesheetUseCase
    {
        Task<BaseResponse> Execute(TimeSheet timesheet, CancellationToken cancellationToken);
    }
}
