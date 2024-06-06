using AutoMapper;
using ProblemAndSolution.Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogBO = ProblemAndSolution.Infrastructure.BusinessObj.Blog;
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

        public void AddBlog(BlogBO blog)
        {

        }
    }
}
