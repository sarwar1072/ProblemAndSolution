using DevSkill.Data;
using ProblemAndSolution.Infrastructure.DbContexts;
using ProblemAndSolution.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemAndSolution.Infrastructure.Repositories
{
    public class QuestionRepository:Repository<Question,int,ApplicationDbContext>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {

        }
    }
}
