using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemAndSolution.Infrastructure.Entities
{
    public class UserSpecificBlogBO
    {
        public IList<Blog>? Blog { get; set; }
        public UserProfile? Profile { get; set; }
    }
}
