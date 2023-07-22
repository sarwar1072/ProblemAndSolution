using ProblemAndSolution.Infrastructure.BusinessObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemAndSolution.Infrastructure.Services
{
    public interface ICommentService
    {
        Task CreateCommentAsync(Comment comment);
        Task<int> GetQusnVote(Guid id, int questionId);
        Task<int> GetAnsVote(Guid id, int answerId);
    }
}
