using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemAndSolution.Infrastructure.BusinessObj
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? AuthorName { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime DateTime { get; set; }
        public Guid? TempId { get; set; }
        public int AnswerId { get; set; }
        public Answer? Answer { get; set; }
    
    }
}
