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
    public class CommentRepository:Repository<Comment,int,ApplicationDbContext>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {

        }
    }

    public interface ICommentRepository:IRepository<Comment,int,ApplicationDbContext>   
    {
    }
}
