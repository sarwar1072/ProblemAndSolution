using ProblemAndSolution.Web.Enums;

namespace ProblemAndSolution.Web.Models
{
    public class ResponseModel
    {
        public string? Message { get; set; }
        public ResponseTypes Response { get; set; }
    }
}
