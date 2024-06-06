using AutoMapper;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.VisualBasic;
using MimeKit;
using ProblemAndSolution.Infrastructure.Exceptions;
using ProblemAndSolution.Infrastructure.UnitOfWorks;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogBO = ProblemAndSolution.Infrastructure.BusinessObj.Blog;
using BlogEO = ProblemAndSolution.Infrastructure.Entities.Blog;
namespace ProblemAndSolution.Infrastructure.Services
{
    public class BlogServices:IBlogServices
    {
        private IPAndSUnitOfWork _pAndSUnitOfWork;
        private IMapper _mpper;
        public BlogServices(IPAndSUnitOfWork pAndSUnitOfWork,IMapper mapper)
        {
            _mpper = mapper;
            _pAndSUnitOfWork = pAndSUnitOfWork;               
        }

        public async Task AddBlog(BlogBO blog)
        {
            if(blog is null)
            {
                throw new InvalidOperationException("Blog can not be null");
            }
            var blogCount = await _pAndSUnitOfWork.BlogRepository.GetCountAsync(c => c.PageTitle == blog.PageTitle);
            if(blogCount > 0) {
                throw new DuplicateException("Same title exist");
            }

            var mapEntity = new BlogEO
            {
                Heading = blog.Heading,
                PageTitle = blog.PageTitle,
                Content = blog.Content,
                ShortDescription = blog.ShortDescription,
                ImageUrl = blog.ImageUrl,
                PublishedDate = blog.PublishedDate,
                Author = blog.Author,
                Visible = blog.Visible,
            };
            await _pAndSUnitOfWork.BlogRepository.AddAsync(mapEntity);
            await _pAndSUnitOfWork.SaveAsync();
        }

    }
}
