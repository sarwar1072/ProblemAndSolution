using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemAndSolution.Infrastructure.BusinessObj
{
    public class Tag
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Question? Question { get; set; }
        public int QuestionId { get; set; }
    }
}
