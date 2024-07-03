using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogBO = ProblemAndSolution.Infrastructure.BusinessObj.Blog;
using BlogEO = ProblemAndSolution.Infrastructure.Entities.Blog;
using BlogCommentBO = ProblemAndSolution.Infrastructure.BusinessObj.BlogComment;
using BlogCommentEO = ProblemAndSolution.Infrastructure.Entities.BlogComment;

namespace ProblemAndSolution.Infrastructure.Services
{
    public interface IBlogServices
    {
        Task AddBlog(BlogBO blog);
        Task EditBlog(BlogBO blogBO);
        BlogEO Delete(int id);
        Task<BlogBO> GetById(int id);
        IList<BlogBO> GetAllBlog();
        Task<BlogBO> GetDetailsById(int id);
        Task AddComment(BlogCommentBO comment);
        Task<int> GetLikes(Guid Userid, int BlogId);
        Task<int> TotalLikeForBlog(int id);
        Task<bool> IsTrueOrFalse(int id, Guid userId);
        (IList<BlogBO> blogs, int total, int totalDisplay) GetBlog(int pageindex, int pagesize,
                                                                             string searchText, string orderBy);
    }
}
