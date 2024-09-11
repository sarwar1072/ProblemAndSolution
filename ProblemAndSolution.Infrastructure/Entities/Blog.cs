using DevSkill.Data;
using ProblemAndSolution.Infrastructure.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemAndSolution.Infrastructure.Entities
{
    public class Blog:IEntity<int>
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }
        public Guid PostId { get; set; }
        //public int ProfileId {  get; set; } 
        //public UserProfile? Profile { get; set; }

        // Navigation property
        // public ICollection<Tag> Tags { get; set; }
        public IList<Like> Likes { get; set; }
        public IList<BlogComment> Comments { get; set; }
    }
}
