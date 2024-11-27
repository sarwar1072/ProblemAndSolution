using DevSkill.Data;
using ProblemAndSolution.Infrastructure.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemAndSolution.Infrastructure.Entities
{
    public class UserProfile:IEntity<int>
    {
        public int Id { get; set; }
        public string? ProfileURL { get; set; }

        public string? Profession { get; set; }
        //public IList<Blog>? Blogs { get; set; } 
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}


