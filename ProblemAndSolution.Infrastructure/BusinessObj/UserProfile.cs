using DevSkill.Data;
using ProblemAndSolution.Infrastructure.BusinessObj.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemAndSolution.Infrastructure.BusinessObj
{
    public class UserProfile 
    {
        public int Id { get; set; }
        public string? ProfileURL { get; set; }

        public string? Profession { get; set; }

        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}