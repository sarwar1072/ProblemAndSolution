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
    public class BlogCommentRepository:Repository<BlogComment,int,ApplicationDbContext>, IBlogCommentRepository 
    {
        public BlogCommentRepository(ApplicationDbContext dbContext):base(dbContext) 
        {
                
        }
    }
}
