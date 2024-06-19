using ProblemAndSolution.Infrastructure.Services;
using ProblemAndSolution.Infrastructure.BusinessObj;
namespace ProblemAndSolution.Web.Models
{
    public class BlogViewModel
    {
        private IBlogServices _BlogServices;
        public BlogViewModel(IBlogServices blogServices)
        {
            _BlogServices = blogServices;
        }
        public int Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }
       public IList<Blog>? blog { get; set; }
        internal void GetBlog()
        {
            var blogs = _BlogServices.GetAllBlog();
            blog=new List<Blog>();  
            if(blogs != null)
            {
                foreach (var item in blogs)
                {
                    blog.Add(item);
                }
            }
        }
    }
}
