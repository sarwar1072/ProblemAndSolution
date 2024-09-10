using DevSkill.Data;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using ProblemAndSolution.Infrastructure.DbContexts;
using ProblemAndSolution.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProblemAndSolution.Infrastructure.Repositories
{
    public class BlogRepository:Repository<Blog,int,ApplicationDbContext>, IBlogRepository  
    {
        public BlogRepository(ApplicationDbContext context):base(context)
        {
                
        }
       

    }
}
