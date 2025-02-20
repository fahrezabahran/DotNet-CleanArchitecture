using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.Interfaces.ProductInterfaces;
using ProductApi.Application.Interfaces.Timesheet;
using ProductApi.Domain.Entities;

namespace DotNet_CleanArchitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetController(ICreateTimesheetUseCase timesheetUseCase) : ControllerBase
    {
        private readonly ICreateTimesheetUseCase _timesheetUseCase = timesheetUseCase;

        [HttpPost]
        public async Task<IActionResult> CreateTimesheet(TimeSheet timesheet, CancellationToken cancellationToken)
        {
            var response = await _timesheetUseCase.Execute(timesheet, cancellationToken);
            return Ok(response);
        }
    }
}
