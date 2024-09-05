using Autofac;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProblemAndSolution.Infrastructure.Services;
using ProblemAndSolution.Membership.BusinessObj;
using ProblemAndSolution.Membership.Services;
using Serilog.Context;
using System.Drawing;
using BlogBO = ProblemAndSolution.Infrastructure.BusinessObj.Blog;

namespace ProblemAndSolution.Web.Areas.ForPost.Models.BlogModelFolder
{
    public class EditBlog:PostLayoutModel
    {
        private IBlogServices _blogServices;
        public int Id { get; set; }
        public string? Heading { get; set; }
        public string? PageTitle { get; set; }
        public string? Content { get; set; }
        public string? ShortDescription { get; set; }
        public string? Url { get; set; }
        public IFormFile? formFile { get; set; }
        public DateTime PublishedDate { get; set; }
        public string? Author { get; set; }
        public bool Visible { get; set; }

        public EditBlog() { }

        public EditBlog(IBlogServices blogServices,
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
        private BlogBO MapModelToBO()
        {
            var Model = new BlogBO()
            {
                Id=Id,
                Heading = Heading,
                PageTitle = PageTitle,
                Content = Content,
                ShortDescription = ShortDescription,
                ImageUrl = Url,
                PublishedDate = PublishedDate,
                Author = Author,
                Visible = Visible,
            };

            return Model;
        }

        public async Task Update()
        {
            var model = MapModelToBO();
            await _blogServices.EditBlog(model);    
        }
        public async Task LoadData(int id)
        {
            var data=await _blogServices.GetById(id);
            Id = data.Id;
            Heading=data.Heading;
            PageTitle=data.PageTitle;   
            Content=data.Content;   
            ShortDescription=data.ShortDescription; 
            Author=data.Author;
            Url = data.ImageUrl;
            Visible= data.Visible;  
            PublishedDate = data.PublishedDate; 
        }

    }
}
