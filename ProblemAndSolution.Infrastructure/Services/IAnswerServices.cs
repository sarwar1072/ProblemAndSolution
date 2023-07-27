using ProblemAndSolution.Infrastructure.BusinessObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemAndSolution.Infrastructure.Services
{
    public interface IAnswerServices
    {
        Task CreateAnswerAsync(Answer answer);
        int NumberOfAnswer();
    }
}
