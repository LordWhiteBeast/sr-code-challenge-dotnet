using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;

namespace challenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<CompensationService> _logger;

        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository)
        {
            _compensationRepository = compensationRepository;
            _logger = logger;
        }
        //To Create a compensation
        public Compensation Create(Compensation compensation)
        {
            if (compensation != null)
            {
                _compensationRepository.createComp(compensation);
                _compensationRepository.SaveAsync().Wait();
            }

            return compensation;
        }
        // To read a compensation using employeeId
        public Compensation GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return _compensationRepository.getCompByEId(id);
            }

            return null;
        }

    }
}
