using Autofac;
using AutoMapper;
using ProblemAndSolution.Infrastructure.Services;
using ProblemAndSolution.Membership.BusinessObj;
using ProblemAndSolution.Membership.Services;
using BlogBO = ProblemAndSolution.Infrastructure.BusinessObj.Blog;
namespace ProblemAndSolution.Web.Areas.ForPost.Models.BlogModelFolder
{
    public class CreateBlog:PostLayoutModel
    {
        private IBlogServices _blogServices;

        public string? Heading { get; set; }
        public string? PageTitle { get; set; }
        public string? Content { get; set; }
        public string? ShortDescription { get; set; }
        public string? Url { get; set; }
        public IFormFile formFile { get; set; }
        public DateTime PublishedDate { get; set; }
        public string? Author { get; set; }
        public bool Visible { get; set; }

        public CreateBlog() { }

        public CreateBlog(IBlogServices blogServices,
            IUserManagerAdapter<ApplicationUser> userManagerAdapter,
            IHttpContextAccessor httpContextAccessor, IMapper mapper):base(userManagerAdapter, httpContextAccessor, mapper)
        {
            _blogServices = blogServices;           
        }

        public override void ResolveDependency(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
            _blogServices = _lifetimeScope.Resolve<IBlogServices>();

            base.ResolveDependency(_lifetimeScope);
        }

        public async Task AddBlog()
        {
            var entity = new BlogBO()
            {
                Heading = Heading,  
                PageTitle = PageTitle,  
                Content = Content,  
                ShortDescription = ShortDescription,    
                ImageUrl=Url,
                PublishedDate = PublishedDate,
                Author = Author, 
                Visible = Visible,  
            };
            await _blogServices.AddBlog(entity);
        }
    }
}
