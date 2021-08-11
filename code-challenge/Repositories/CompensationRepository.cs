using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Data;

namespace challenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly CompensationContext _compensationContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, CompensationContext compensationContext)
        {
            _compensationContext = compensationContext;
            _logger = logger;
        }

        public Compensation createComp(Compensation compensation)
        {
            _compensationContext.Compensations.Add(compensation);
            return compensation;
        }

        public Compensation getCompByEId(string employeeId)
        {
            return _compensationContext.Compensations.AsEnumerable().Where(comp => comp.EmployeeId ==employeeId).SingleOrDefault();
        }

        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }

        public Compensation Remove(Compensation compensation)
        {
            return _compensationContext.Remove(compensation).Entity;
        }
    }
}
