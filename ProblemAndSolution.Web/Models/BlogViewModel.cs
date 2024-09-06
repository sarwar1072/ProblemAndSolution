using ProblemAndSolution.Infrastructure.Services;
using ProblemAndSolution.Infrastructure.BusinessObj;
using Autofac;
using AutoMapper;
using ProblemAndSolution.Membership.Services;
using ProblemAndSolution.Membership.BusinessObj;

namespace ProblemAndSolution.Web.Models
{
    public class BlogViewModel:BaseModel
    {
        private IBlogServices _BlogServices;
        
        public int Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }
        public string? CommentDescription { get; set; }
        public int? TotalLikes { get; set; }
        public bool Liked { get; set; }
        public int? NoOfComment { get; set; }   
        public ApplicationUser? User { get; set; }
        public Guid? UserId { get; set; }
        public IList<Blog>? blog { get; set; }
        public IList<BlogComment>? comments { get; set; }

        public BlogViewModel(IUserManagerAdapter<ApplicationUser> userManagerAdapter, IHttpContextAccessor contextAccessor,
           IMapper mapper, IBlogServices blogServices)
        {
            _BlogServices = blogServices;
        }
        public BlogViewModel()
        {

        }
        public override void ResolveDependency(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
            _BlogServices = _lifetimeScope.Resolve<IBlogServices>();

            base.ResolveDependency(_lifetimeScope);
        }


        internal async Task GetDetailsById(int id)
        {
            var blogDatails = await _BlogServices.GetDetailsById(id);

           // var NumberOfComment = await _BlogServices.NoOfComment(id,);

            var totallike =await _BlogServices.TotalLikeForBlog(id);
            var IsLiked = false;
            if ( await Userid())
            {
                await GetUserInfoAsync();
                var userid = basicInfo!.Id;
                 IsLiked = await _BlogServices.IsTrueOrFalse(id, userid);
            }
                       
            if(blogDatails != null) 
            {
                Id = blogDatails.Id;
                Heading = blogDatails.Heading;
                PageTitle = blogDatails.PageTitle;  
                Content = blogDatails.Content;  
                ShortDescription = blogDatails.ShortDescription;    
                ImageUrl = blogDatails.ImageUrl;
                PublishedDate = blogDatails.PublishedDate;
                Author=blogDatails.Author;  
                Visible= blogDatails.Visible;
                TotalLikes = totallike;
                Liked = IsLiked;
                UserId = blogDatails.PostId;
               NoOfComment=blogDatails.NoOfComment;    
                comments=new List<BlogComment>();
                if (blogDatails.Comments != null)
                {
                    foreach (var item in blogDatails.Comments)
                    {
                        comments.Add(new BlogComment
                        {
                            Id = item.Id,   
                            Description = item.Description, 
                            UserId = item.UserId,   
                            Author = item.Author,   
                            DateAdded = item.DateAdded,
                            BlogId = item.BlogId,   
                        });
                    }

                }
            }
        }
        internal async Task<int> AddLike(int id)
        {
            await GetUserInfoAsync();

            var userid = basicInfo!.Id;
            return await _BlogServices.GetLikes(userid, id);
        }

        internal async Task AddComment()
        {
            await GetUserInfoAsync();
            var model = new BlogComment()
            {
                Description=CommentDescription,
                UserId=basicInfo!.Id,
                Author=basicInfo!.FirstName,
                DateAdded=DateTime.UtcNow,
                BlogId=Id,
            };
            await _BlogServices.AddComment(model);

        }
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
