using Formary.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formary.Services
{
    public class StepService : CosmosDbBaseService<Step>, IStepService
    {
        public StepService(CosmosClient dbClient, Database database) : base(dbClient, database, "Step", "/id")
        {
        }

        public async Task<Step> AddStepAsync(Step step)
        {
            return await AddAsync(step, step.Id);
        }

        public async Task<Step> DeleteStepAsync(string id)
        {
            return await DeleteAsync(id, id.ToString());
        }

        public async Task<Step> GetStepAsync(string id)
        {
            return await GetAsync(id, id);
        }

        public async Task<IList<Step>> QueryStepsAsync(string query)
        {
            return await QueryAsync(query);
        }

        public async Task<Step> UpdateStepAsync(Step step)
        {
            return await UpdateAsync(step.Id, step);
        }
    }
}
