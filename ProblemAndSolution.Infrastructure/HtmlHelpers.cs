using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemAndSolution.Infrastructure
{
    public static class HtmlHelpers
    {
        //public static string SanitizeHtml(string input)
        //{
        //if (string.IsNullOrWhiteSpace(input))
        //    return string.Empty;

        //var sanitizer = new HtmlSanitizer();
        //// Allow specific tags
        //sanitizer.AllowedTags.Add("p");
        //sanitizer.AllowedTags.Add("strong");
        //sanitizer.AllowedTags.Add("em");
        //sanitizer.AllowedTags.Add("b");
        //sanitizer.AllowedTags.Add("i");
        //sanitizer.AllowedTags.Add("ul");
        //sanitizer.AllowedTags.Add("li");
        //sanitizer.AllowedTags.Add("ol");

        //// Allow specific attributes if needed
        //sanitizer.AllowedAttributes.Add("href");
        //sanitizer.AllowedAttributes.Add("src");

        //var str = sanitizer.Sanitize(input);
        //if (str.Length >100)
        //{

        //  return  str.Substring(0,100);    
        //}
        //else
        //{
        //  return str; 
        //}
        //return sanitizer.Sanitize(input);
        //}
        //var sanitizedContent = HtmlHelpers.SanitizeHtml(blog.Content);
        //var truncatedContent = sanitizedContent.Length > 100
        //? sanitizedContent.Substring(0, 100) + "..."
        //: sanitizedContent;

        public static string TruncateHtml(string input, int length)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // Load HTML content into an HtmlDocument
            var doc = new HtmlDocument();
            doc.LoadHtml(input);

            // Extract inner text to get plain content without breaking HTML
            var plainText = doc.DocumentNode.InnerText;

            // Truncate the plain text
            var truncatedText = plainText.Length <= length ? plainText : plainText.Substring(0, length) + "...";

            return truncatedText;
        }

    }
}
