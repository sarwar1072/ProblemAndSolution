using DevSkill.Data;
using ProblemAndSolution.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemAndSolution.Infrastructure.UnitOfWorks
{
    public interface IPAndSUnitOfWork:IUnitOfWork
    {
         IQuestionRepository QuestionRepository { get; }
         IAnswerRepository AnswerRepository { get;   }
         ICommentRepository CommentRepository { get;   }
         IVoteRepository VoteRepository { get; }
        IBlogRepository BlogRepository { get; } 
    }
}
