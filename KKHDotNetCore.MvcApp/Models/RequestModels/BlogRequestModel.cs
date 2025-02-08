using System.ComponentModel.DataAnnotations;

namespace KKHDotNetCore.MvcApp.Models.RequestModels
{
    public class BlogRequestModel
    {
        public int? BlogId { get; set; }
        [Required]
        public string BlogTitle { get; set; }
        [Required]
        public string BlogAuthor { get; set; }
        [Required]
        public string BlogContent { get; set; }

        public bool? DeleteFlag { get; set; }
    }
}
