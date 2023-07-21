using ProblemAndSolution.Infrastructure.BusinessObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemAndSolution.Infrastructure.Services
{
    public  interface  IQuestionServices
    {
        Task CreateQuestion(Question question);
        Task<Question> GetByIdAsync(int id);
        Task UpdateQuestionAsync(Question question);
        Task DeleteQuestionAsync(int id);
        Task<List<Question>> GetQuestionsAsync(Guid id);
        void GetTest(int pageIndex);
       // Task<List<Question>> GetPaginatedQuestions(int index, int pageSize);
        Task<Question> GetDetails(int id);
    }
}
