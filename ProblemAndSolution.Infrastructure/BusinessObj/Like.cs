using ProblemAndSolution.Infrastructure.BusinessObj.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemAndSolution.Infrastructure.BusinessObj
{
    public class Like
    {
        public int Id { get; set; }
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public int BlogId { get; set; }
        public bool TrueOrFalse { get; set; }

    }
}
