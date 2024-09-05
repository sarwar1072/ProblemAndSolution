using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemAndSolution.Infrastructure.BusinessObj
{
    public class Blog
    {
        public int Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public int NoOfComment {  get; set; }   
        public string ImageUrl { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }
        // Navigation property
        // public ICollection<Tag> Tags { get; set; }
        //public ICollection<BlogPostLike> Likes { get; set; }
        public IList<BlogComment> Comments { get; set; }
    }
}
