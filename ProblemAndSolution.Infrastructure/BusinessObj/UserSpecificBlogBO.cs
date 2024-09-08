using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemAndSolution.Infrastructure.BusinessObj
{
    public class UserSpecificBlogBO
    {
       
        public List<Blog>? Blogs { get; set; }
        public string? ProfileURL { get; set; }
        public string? UserName {  get; set; }   
        public string? Profession { get; set; }

    }
}
