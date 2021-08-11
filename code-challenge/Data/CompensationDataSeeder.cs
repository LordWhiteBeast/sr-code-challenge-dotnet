using challenge.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
namespace challenge.Data
{
    public class CompensationDataSeeder
    {
        private CompensationContext _compensationContext;
        private const String Comp_DATA_FILE = "resources/CompensationSeedD.json";

        public CompensationDataSeeder(CompensationContext compensationContext)
        {
            _compensationContext = compensationContext;
        }

        public async Task Seed()
        {
            if (!_compensationContext.Compensations.Any())
            {
                List<Compensation> compensations = LoadAllCompensations();
                _compensationContext.Compensations.AddRange(compensations);

                await _compensationContext.SaveChangesAsync();
            }
        }
        private List<Compensation> LoadAllCompensations()
        {
            using (FileStream fs = new FileStream(Comp_DATA_FILE, FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
            using (JsonReader jr = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                List<Compensation> compn = serializer.Deserialize<List<Compensation>>(jr);
                return compn;
            }
        }

    }
}
