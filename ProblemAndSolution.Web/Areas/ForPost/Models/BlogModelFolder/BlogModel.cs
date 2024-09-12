using Autofac;
using AutoMapper;
using ProblemAndSolution.Infrastructure.BusinessObj;
using ProblemAndSolution.Infrastructure.Services;
using ProblemAndSolution.Membership.BusinessObj;
using ProblemAndSolution.Membership.Services;
using ProblemAndSolution.Web.Models;

namespace ProblemAndSolution.Web.Areas.ForPost.Models.BlogModelFolder
{
    public class BlogModel: PostLayoutModel
    {
        private IBlogServices _blogServices;

        public int Id { get; set; }
        public string Tag { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public Guid PostId { get; set; }
        public List<Blog> Blogs { get; set; }   
        public BlogModel() { }

        public BlogModel(IBlogServices blogServices,
            IUserManagerAdapter<ApplicationUser> userManagerAdapter,
            IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(userManagerAdapter, httpContextAccessor, mapper)
        {
            _blogServices = blogServices;
        }

        public override void ResolveDependency(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
            _blogServices = _lifetimeScope.Resolve<IBlogServices>();
            base.ResolveDependency(_lifetimeScope);
        }

        public async Task UserBlogList(Guid UserId)
        {
            var list=await _blogServices.UserSpecificBlogList(UserId);
            Blogs=new List<Blog>();
            if(list.Any())
            {
                foreach(var blog in list) 
                {
                    Blogs.Add(blog);             
                }
            }
        }

        internal object GetBlog(DataTablesAjaxRequestModel dataTables)
        {
            var data = _blogServices.GetBlog(dataTables.PageIndex,
                                                     dataTables.PageSize,
                                                     dataTables.SearchText,
                                                     dataTables.GetSortText(new string[] { "Heading", "PageTitle", "Author" }));
            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.blogs
                        select new string[]
                        {
                            record.Tag,
                            record.PageTitle,
                            //record.Content,
                            record.ShortDescription,
                            record.ImageUrl,
                            record.PublishedDate.ToString(),
                            record.Author,
                            record.Id.ToString()
                        }).ToArray()
            };
        }
        internal string Delete(int id)
        {
           var deleteData = _blogServices.Delete(id);
            return deleteData.PageTitle;
        }

    }
}
