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
using BlogCommentBO = ProblemAndSolution.Infrastructure.BusinessObj.BlogComment;
using BlogCommentEO = ProblemAndSolution.Infrastructure.Entities.BlogComment;
using LikeEO = ProblemAndSolution.Infrastructure.Entities.Like;

namespace ProblemAndSolution.Infrastructure.Services
{
    public class BlogServices : IBlogServices
    {
        private IPAndSUnitOfWork _pAndSUnitOfWork;
        private readonly IMapper _mapper;
        private int _likeCount = 0;
        private int _CountCommnet = 0;
        public BlogServices(IPAndSUnitOfWork pAndSUnitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _pAndSUnitOfWork = pAndSUnitOfWork;
        }

        public async Task AddBlog(BlogBO blog)
        {
            if (blog is null)
            {
                throw new InvalidOperationException("Blog can not be null");
            }
            var blogCount = await _pAndSUnitOfWork.BlogRepository.GetCountAsync(c => c.PageTitle == blog.PageTitle);
            if (blogCount > 0) {
                throw new DuplicationException("Same title exist");
            }

            var mapEntity = BoToEntity(blog);
            await _pAndSUnitOfWork.BlogRepository.AddAsync(mapEntity);
            await _pAndSUnitOfWork.SaveAsync();
        }
        public async Task<BlogBO> GetById(int id)
        {
            var data = await _pAndSUnitOfWork.BlogRepository.GetByIdAsync(id);
            if (data == null)
            {
                throw new InvalidOperationException("Blog is not available");
            }
            return EntityToBusinessObj(data);
        }

        public async Task EditBlog(BlogBO blogBO)
        {
            if (blogBO is null)
            {
                throw new InvalidOperationException("Question can not be null");
            }
            //var count=await _pAndSUnitOfWork.BlogRepository.GetCountAsync(x=>x.PageTitle == blogBO.PageTitle);
            //if(count  !=0)
            //{
            //    throw new DuplicateException("Same blog exist");
            //}
            var data = await _pAndSUnitOfWork.BlogRepository.GetByIdAsync(blogBO.Id);

            if ((data == null))
            {
                throw new InvalidOperationException("Data can not be found");
            }
            AssignBlog(blogBO, data);
            await _pAndSUnitOfWork.SaveAsync();
        }
        public async Task ApprovePostSingle(int id)
        {
            var post = (await _pAndSUnitOfWork.BlogRepository.GetAsync(x => x.Id == id && x.Visible == "Pending", null, null,false)).FirstOrDefault();
            if(post == null)
            {
                throw new InvalidOperationException("Already  approved");
            }
           // var entity = new BlogEO();
            post.Visible = "Approve";
            
            await  _pAndSUnitOfWork.BlogRepository.EditAsync(post);
            await _pAndSUnitOfWork.SaveAsync(); 
            
        }
        private BlogEO AssignBlog(BlogBO blogBO, BlogEO data)
        {
            data.Id = blogBO.Id;
            data.Tag = blogBO.Tag;
            data.PageTitle = blogBO.PageTitle;
            data.Content = blogBO.Content;
            data.Author = blogBO.Author;
            data.PublishedDate = blogBO.PublishedDate;
            data.ShortDescription = blogBO.ShortDescription;
            //data.Visible = blogBO.Visible;
            data.ImageUrl = blogBO.ImageUrl;
            data.Id = blogBO.Id;
            return data;

        }
        private BlogEO BoToEntity(BlogBO blog)
        {
            var blogEO = new BlogEO()
            {
                Tag = blog.Tag,
                PageTitle = blog.PageTitle,
                Content = blog.Content,
                ShortDescription = blog.ShortDescription,
                ImageUrl = blog.ImageUrl,
                PublishedDate = blog.PublishedDate,
                Author = blog.Author,
                Visible = blog.Visible,
                PostId = blog.PostId,
            };
            return blogEO;
        }
        private BlogBO EntityToBusinessObj(BlogEO blog) {

            var blogBo = new BlogBO()
            {
                Tag = blog.Tag,
                PageTitle = blog.PageTitle,
                Content = blog.Content,
                ShortDescription = blog.ShortDescription,
                ImageUrl = blog.ImageUrl,
                PublishedDate = blog.PublishedDate,
                Author = blog.Author,
              //  Visible = blog.Visible,
                Id = blog.Id,
            };
            return blogBo;
        }
        private BlogBO EntityToBusinessObjForPaging(BlogEO blog)
        {

            var blogBo = new BlogBO()
            {
                Tag = blog.Tag,
                PageTitle = blog.PageTitle,
                Content = blog.Content,
                ShortDescription = blog.ShortDescription,
                ImageUrl = blog.ImageUrl,
                PublishedDate = blog.PublishedDate,
                Author = blog.Author,
                 Visible = blog.Visible,
                Id = blog.Id,
            };
            return blogBo;
        }
        public (IList<BlogBO> blogs, int total, int totalDisplay) GetBlog(int pageindex, int pagesize,
                                                                             string searchText, string orderBy)
        {
            (IList<BlogEO> data, int total, int totalDisplay) result = (null, 0, 0);
            if (string.IsNullOrWhiteSpace(searchText))
            {
                result = _pAndSUnitOfWork.BlogRepository.GetDynamic(null, null, null, pageindex, pagesize, true);
            }
            else
            {
                result = _pAndSUnitOfWork.BlogRepository.GetDynamic(x => x.Tag.ToLower() == searchText.ToLower() 
                || x.Visible.ToLower() == searchText.ToLower() || x.Author.ToLower() ==searchText.ToLower() 
                , null,null, pageindex, pagesize, true);
            }
            var listOfEntity = new List<BlogBO>();
            foreach (var blog in result.data)
            {
                // var obj= AssignProductBO(product);
                listOfEntity.Add(EntityToBusinessObjForPaging(blog));
            }
            return (listOfEntity, result.total, result.totalDisplay);
        }
       
        public IList<BlogBO> GetAllBlog()
        {
            var listOfBlog = _pAndSUnitOfWork.BlogRepository.Get(c=>c.Visible=="Approve",null,null,false).ToList();
            var list=new List<BlogBO>();    
            foreach (var item in listOfBlog)
            {
                list.Add(EntityToBusinessObjForPaging(item));    
            }
            return list;
        }
        private BlogBO EntityToBusinessObj2(BlogEO entity)
        {
            var result = new BlogBO()
            {
                Id = entity.Id,
                Tag = entity.Tag,   
                PageTitle = entity.PageTitle,   
                Content = entity.Content,   
                ShortDescription = entity.ShortDescription, 
                ImageUrl = entity.ImageUrl, 
                PublishedDate = entity.PublishedDate,   
                Author=entity.Author,   
                Visible = entity.Visible,  
                NoOfComment=_CountCommnet,
                PostId=entity.PostId,
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
        public async Task AddComment(BlogCommentBO comment)
        {
            if (comment == null)
                throw new InvalidOperationException("Can not be empty");

            var result = new BlogCommentEO()
            {
                Description = comment.Description,
                UserId = comment.UserId,    
                Author = comment.Author,    
                DateAdded = comment.DateAdded,  
                BlogId = comment.BlogId,    
            };
            await _pAndSUnitOfWork.BlogCommentRepository.AddAsync(result);
            await _pAndSUnitOfWork.SaveAsync(); 

        }
        
        public async Task<BlogBO> GetDetailsById(int id)
        {
            var entity =(await  _pAndSUnitOfWork.BlogRepository.GetAsync(c=>c.Id==id &&c.Visible == "Approve",d=>d.Include(e=>e.Comments))).FirstOrDefault();

            if (entity != null)
            {
                _CountCommnet=entity.Comments.Count();
            }
            else
            {
                _CountCommnet=0;
            }
            _likeCount =await _pAndSUnitOfWork.LikeRepository.GetCountAsync(c=>c.BlogId == id);

            if (entity == null)
                throw new InvalidOperationException("Data not found");

            return EntityToBusinessObj2(entity);
        }
       
        public async Task<bool> IsTrueOrFalse(int id,Guid userId)
        {
            var isTrue=await _pAndSUnitOfWork.LikeRepository.GetCountAsync(c=>c.BlogId==id && c.ApplicationUserId==userId);
            if (isTrue > 0)
            {
                return true;    
            }
            return false;
        }
        public async Task<int> TotalLikeForBlog(int id)
        {
            return await _pAndSUnitOfWork.LikeRepository.GetCountAsync(c=>c.BlogId == id);
        }
        public async Task<int> GetLikes(Guid Userid,int BlogId)
        {
            var like = new LikeEO
            {
                ApplicationUserId = Userid,
                BlogId=BlogId
            };
            var count=(await _pAndSUnitOfWork.LikeRepository.GetCountAsync(a=>a.ApplicationUserId==Userid &&
            a.BlogId==BlogId));
            if (count == 0)
            {
                await _pAndSUnitOfWork.LikeRepository.AddAsync(like);
                await _pAndSUnitOfWork.SaveAsync(); 
            }
            return _likeCount;
        }
        public BlogEO Delete(int id)
        {
            var data=_pAndSUnitOfWork.BlogRepository.GetById(id);
            _pAndSUnitOfWork.BlogRepository.Remove(data);
            _pAndSUnitOfWork.Save();
            return data;
        }

        public async Task<List<BlogBO>> RecentBlogPosts()
        {
            var listOfPost = (await _pAndSUnitOfWork.BlogRepository.
                GetAsync(c=>c.Visible== "Approve", x => x.OrderByDescending(y => y.PublishedDate), null,false)).Take(3);
            var blogs=new List<BlogBO>();
            if (listOfPost.Any())
            {
                foreach (var blog in listOfPost)
                {
                    blogs.Add(new BlogBO()
                    {
                        Id = blog.Id,
                        Tag = blog.Tag,
                        PageTitle = blog.PageTitle,
                        ImageUrl = blog.ImageUrl,
                    });
                }
            }           
            return blogs;
        }

        public async Task<List<BlogBO>> RelatedBlogPost(int id)
        {
            var blogById=(await _pAndSUnitOfWork.BlogRepository.GetByIdAsync(id));  

            var listOfBlogs=(await _pAndSUnitOfWork.BlogRepository.GetAsync
                (x=>x.Tag.Contains(blogById.Tag) && x.Id != blogById.Id,null,null,false)).Take(3);

            var blogs = new List<BlogBO>();
            if (listOfBlogs.Any())
            {
                foreach (var blog in listOfBlogs)
                {
                    blogs.Add(new BlogBO()
                    {
                        Id = blog.Id,
                        Tag = blog.Tag,
                        PageTitle = blog.PageTitle,
                        ImageUrl = blog.ImageUrl,
                    });
                }
            }
            return blogs;   
        }

        public async Task<List<BlogBO>> UserSpecificBlogList(Guid userId)
        {
            var blogList = (await _pAndSUnitOfWork.BlogRepository.GetAsync(a => a.PostId == userId  , null)).ToList();
           
            var blogs = new List<BlogBO>();
            if (blogList.Any())
            {
                foreach (var blog in blogList)
                {
                    blogs.Add(new BlogBO
                    {
                        Id = blog.Id,
                        Tag = blog.Tag,
                        Content = HtmlHelpers.TruncateHtml(blog.Content, 100),
                        PublishedDate = blog.PublishedDate,
                        Visible = blog.Visible, 
                        Author = blog.Author,
                        ImageUrl = blog.ImageUrl,
                    });
                }
            }                     
            return blogs;
        }

        public async  Task<BlogPaging> PagintList(int? id, string term = "", bool paging = false, int currentPage = 0)
        {
            var data = new BlogPaging();
            var listOfBlog = (await _pAndSUnitOfWork.BlogRepository.GetAsync(null, c => c.OrderByDescending(x => x.PublishedDate), null, false)).ToList();
            
           
            if (paging)
            {
                int pageSize = 3;
                int count = listOfBlog.Count;
                int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                listOfBlog = listOfBlog.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPages = TotalPages;
            }
            data.BlogList = listOfBlog.ToList();
            return data;
        }
        public async Task<IList<BlogBO>> ApprovePost()
        {
            var listOfBlog = (await _pAndSUnitOfWork.BlogRepository.GetAsync(null, c => c.OrderByDescending(x => x.PublishedDate), null, false)).ToList();
            var list = new List<BlogBO>();
            foreach (var item in listOfBlog)
            {
                list.Add(EntityToBusinessObjForPaging(item));
            }
            return list;
        }
        


    }
}
