using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemAndSolution.Infrastructure.BusinessObj
{
    public class BlogComment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public string Author { get; set; }
        public int? Count { get; set; }
        public DateTime DateAdded { get; set; }
        public int BlogId { get; set; }
        public Blog blog { get; set; }
    }
}
