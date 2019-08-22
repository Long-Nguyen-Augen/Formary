using Formary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formary.Services
{
    public interface IStepService
    {
        Task<IList<Step>> QueryStepsAsync(string query);
        Task<Step> GetStepAsync(string id);
        Task<Step> AddStepAsync(Step step);
        Task<Step> UpdateStepAsync(Step step);
        Task<Step> DeleteStepAsync(string id);
    }
}
