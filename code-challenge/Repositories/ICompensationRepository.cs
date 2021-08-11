using challenge.Models;
using System;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public interface ICompensationRepository
    {
        Compensation getCompByEId(String employeeId);
        Compensation createComp(Compensation compensation);
        Task SaveAsync();
    }
}