﻿using ProblemAndSolution.Infrastructure.Services;
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
        public string Tag { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public string Visible { get; set; }
        public string? CommentDescription { get; set; }
        public int? TotalLikes { get; set; }
        public bool Liked { get; set; }
        public int? NoOfComment { get; set; }   
        public ApplicationUser? User { get; set; }
        public Guid? UserId { get; set; }
        public IList<Blog>? blog { get; set; }
        public IList<Blog>? RecentBlog { get; set; }= new List<Blog>(); 
        public IList<Blog>? RelatedBlog { get; set; }

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

            var totallike =await _BlogServices.TotalLikeForBlog(id);
            var IsLiked = false;
            if ( await Userid())
            {
                await GetUserInfoAsync();
                var userid = basicInfo!.Id;
                 IsLiked = await _BlogServices.IsTrueOrFalse(id, userid);
            }
                       
            if(blogDatails !=null) 
            {
                Id = blogDatails.Id;
                Tag = blogDatails.Tag;
                PageTitle = blogDatails.PageTitle;  
                Content = blogDatails.Content;  
                ShortDescription = blogDatails.ShortDescription;    
                ImageUrl = blogDatails.ImageUrl;
                PublishedDate = blogDatails.PublishedDate;
                Author=blogDatails.Author;  
                Visible=blogDatails.Visible;
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
            else
            {

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

        internal async Task RecentPost()
        {
            var blogList=await _BlogServices.RecentBlogPosts();

            RecentBlog = new List<Blog>();
            if(blogList.Any()) 
            {
                foreach (var blog in blogList)
                {

                    RecentBlog.Add(new Blog
                    {
                        ImageUrl = blog.ImageUrl,   
                        Tag=blog.Tag,   
                        PublishedDate=blog.PublishedDate,   
                        Id=blog.Id, 
                    });
                }
            }        
        }
        internal async Task RelatedPost(int id)
        {
            var blogList = await _BlogServices.RelatedBlogPost(id);

            RelatedBlog = new List<Blog>();
            if (blogList.Any())
            {
                foreach (var blog in blogList)
                {
                    RelatedBlog.Add(blog);
                }
            }
        }
        internal void GetBlog()
        {
            var blogs = _BlogServices.GetAllBlog();
            blog=new List<Blog>();  
            if(blogs.Any())
            {
                foreach (var item in blogs)
                {
                    blog.Add(item);
                }
            }
        }
    }
}
