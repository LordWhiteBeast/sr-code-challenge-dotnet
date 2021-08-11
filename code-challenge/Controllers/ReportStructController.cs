using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;

namespace challenge.Controllers
{
    [Route("api/reportstructure")]
    public class ReportStructController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public ReportStructController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }


        [HttpGet("{id}", Name = "getReportStructureById")]
        public IActionResult GetReportStructureById(String id)
        {
            var reportStructure = _employeeService.GetReportStructureById(id);
            if (reportStructure == null)
                return NotFound();
            return Ok(reportStructure);
        }
    }
}
