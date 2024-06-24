using AutoMapper;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MimeKit;
using ProblemAndSolution.Infrastructure.BusinessObj;
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
        private readonly IMapper _mapper;
        public BlogServices(IPAndSUnitOfWork pAndSUnitOfWork,IMapper mapper)
        {
            _mapper = mapper;
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

            var mapEntity = BoToEntity(blog);
            await _pAndSUnitOfWork.BlogRepository.AddAsync(mapEntity);
            await _pAndSUnitOfWork.SaveAsync();
        }
        public async Task<BlogBO> GetById(int id)
        {
            var data=await _pAndSUnitOfWork.BlogRepository.GetByIdAsync(id);    
            if(data == null)
            {
                throw new InvalidOperationException("Blog is not available");
            }
            return EntityToBusinessObj(data);
        }

        public async Task EditBlog(BlogBO blogBO)
        {
            if(blogBO is null)
            {
                throw new InvalidOperationException("Question can not be null");
            }
            var count=await _pAndSUnitOfWork.BlogRepository.GetCountAsync(x=>x.PageTitle == blogBO.PageTitle);
            if(count  !=0)
            {
                throw new DuplicateException("Same blog exist");
            }
            var data = await _pAndSUnitOfWork.BlogRepository.GetByIdAsync(blogBO.Id);

            if ((data == null))
            {
                throw new InvalidOperationException("Data can not be found");
            }
            AssignBlog(blogBO, data);
            await _pAndSUnitOfWork.SaveAsync();

        }
        private BlogEO AssignBlog(BlogBO  blogBO,BlogEO data)
        {
            data.Id = blogBO.Id;    
            data.Heading = blogBO.Heading;  
            data.PageTitle = blogBO.PageTitle;
            data.Content = blogBO.Content;  
            data.Author = blogBO.Author;    
            data.PublishedDate = blogBO.PublishedDate;
            data.ShortDescription = blogBO.ShortDescription;    
            data.Visible = blogBO.Visible;
            data.ImageUrl = blogBO.ImageUrl;    
            data.Id = blogBO.Id;    
            return data;    

        }
        private BlogEO BoToEntity(BlogBO blog)
        {
            var blogEO = new BlogEO()
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
            return blogEO;  
        }
        private BlogBO EntityToBusinessObj(BlogEO blog) {

            var blogBo = new BlogBO()
            {
                Id = blog.Id,
                Heading = blog.Heading,
                PageTitle = blog.PageTitle,
                Content = blog.Content,
                ShortDescription = blog.ShortDescription,
                ImageUrl = blog.ImageUrl,
                PublishedDate = blog.PublishedDate,
                Author = blog.Author,
                Visible = blog.Visible,
            };  
            return blogBo;
        }
        public (IList<BlogBO> blogs, int total, int totalDisplay) GetBlog(int pageindex, int pagesize,
                                                                             string searchText, string orderBy)
        {
            (IList<BlogEO> data, int total, int totalDisplay) result = (null, 0, 0);
            if (string.IsNullOrWhiteSpace(searchText))
            {
                result = _pAndSUnitOfWork.BlogRepository.GetDynamic(null, null,null, pageindex, pagesize, true);
            }
            else
            {
                result = _pAndSUnitOfWork.BlogRepository.GetDynamic(x => x.PageTitle == searchText, null,null, pageindex, pagesize, true);
            }
            var listOfEntity = new List<BlogBO>();
            foreach (var blog in result.data)
            {
                // var obj= AssignProductBO(product);
                listOfEntity.Add(EntityToBusinessObj(blog));
            }
            return (listOfEntity, result.total, result.totalDisplay);
        }
        public IList<BlogBO> GetAllBlog()
        {
            var listOfBlog = _pAndSUnitOfWork.BlogRepository.GetAll();
            var list=new List<BlogBO>();    
            foreach (var item in listOfBlog)
            {
                list.Add(EntityToBusinessObj(item));    
            }
            return list;
        }
        private BlogBO EntityToBusinessObj2(BlogEO entity)
        {
            var result = new BlogBO()
            {
                Heading = entity.Heading,   
                PageTitle = entity.PageTitle,   
                Content = entity.Content,   
                ShortDescription = entity.ShortDescription, 
                ImageUrl = entity.ImageUrl, 
                PublishedDate = entity.PublishedDate,   
                Author=entity.Author,   
                Visible = entity.Visible,   
            };
            result.Comments = new List<BlogComment>();
            if(entity.Comments != null) { 
                foreach (var comment in entity.Comments)
                {
                    result.Comments.Add(new BlogComment()
                    {
                        Description = comment.Description,  
                        UserId = comment.UserId,
                        Author=comment.Author,
                        DateAdded = comment.DateAdded,  
                        BlogId = comment.BlogId,
                    });
                }
            }
            return result;  
        }

        public async Task<BlogBO> GetDetailsById(int id)
        {
            var entity =(await  _pAndSUnitOfWork.BlogRepository.GetAsync(c=>c.Id==id,d=>d.Include(e=>e.Comments))).FirstOrDefault();

            if (entity == null)
                throw new InvalidOperationException("Data not found");

            return EntityToBusinessObj2(entity);
        }

        public BlogEO Delete(int id)
        {
            var data=_pAndSUnitOfWork.BlogRepository.GetById(id);
            _pAndSUnitOfWork.BlogRepository.Remove(data);
            _pAndSUnitOfWork.Save();
            return data;
        }

    }
}
