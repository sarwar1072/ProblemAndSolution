using ProblemAndSolution.Infrastructure.Services;
using BlogBO = ProblemAndSolution.Infrastructure.BusinessObj.Blog;
namespace ProblemAndSolution.Web.Models
{
    public class BlogViewModel
    {
        private IBlogServices _BlogServices;
        //public BlogViewModel(IBlogServices blogServices)
        //{
        //        _BlogServices = blogServices;
        //}
        public IEnumerable<BlogBO> blogBOs { get; set; }
        //internal IEnumerable<BlogBO> GetBlog()
        //{
        //    var blogs = _BlogServices.GetAllBlog();
        //    return blogs;
        //}
    }
}
