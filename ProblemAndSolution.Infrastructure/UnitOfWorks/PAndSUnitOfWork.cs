using DevSkill.Data;
using ProblemAndSolution.Infrastructure.DbContexts;
using ProblemAndSolution.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemAndSolution.Infrastructure.UnitOfWorks
{
    public class PAndSUnitOfWork: UnitOfWork, IPAndSUnitOfWork
    {
        public IQuestionRepository QuestionRepository { get; private set; }
        public IAnswerRepository AnswerRepository { get; private set; }
        public ICommentRepository CommentRepository { get; private set; }
        public IVoteRepository VoteRepository { get; private set; }

        public PAndSUnitOfWork(ApplicationDbContext dbContext, 
            IQuestionRepository questionRepository,
            IAnswerRepository answerRepository,
            ICommentRepository commentRepository,
            IVoteRepository voteRepository):base(dbContext)
        {
            QuestionRepository = questionRepository;
            AnswerRepository = answerRepository;
            CommentRepository = commentRepository;
            VoteRepository = voteRepository;
        }
    }
}
