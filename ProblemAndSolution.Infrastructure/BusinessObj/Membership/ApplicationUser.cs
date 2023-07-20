using ProblemAndSolution.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemAndSolution.Infrastructure.BusinessObj.Membership
{
    public class ApplicationUser
    {
        public  string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Question>? questions { get; set; }
    }
}
