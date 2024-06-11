using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogBO = ProblemAndSolution.Infrastructure.BusinessObj.Blog;
using BlogEO = ProblemAndSolution.Infrastructure.Entities.Blog;

namespace ProblemAndSolution.Infrastructure.Services
{
    public interface IBlogServices
    {
        Task AddBlog(BlogBO blog);
        Task EditBlog(BlogBO blogBO);
        BlogEO Delete(int id);
        Task<BlogBO> GetById(int id);
        IEnumerable<BlogBO> GetAllBlog();
        (IList<BlogBO> blogs, int total, int totalDisplay) GetBlog(int pageindex, int pagesize,
                                                                             string searchText, string orderBy);
    }
}
