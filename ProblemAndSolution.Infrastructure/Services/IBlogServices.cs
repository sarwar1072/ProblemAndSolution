using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogBO = ProblemAndSolution.Infrastructure.BusinessObj.Blog;
namespace ProblemAndSolution.Infrastructure.Services
{
    public interface IBlogServices
    {
        Task AddBlog(BlogBO blog);
    }
}
