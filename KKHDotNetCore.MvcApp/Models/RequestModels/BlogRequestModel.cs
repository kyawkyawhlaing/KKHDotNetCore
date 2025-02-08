﻿namespace KKHDotNetCore.MvcApp.Models.RequestModels
{
    public class BlogRequestModel
    {
        public int? BlogId { get; set; }

        public string? BlogTitle { get; set; } = null!;

        public string? BlogAuthor { get; set; } = null!;

        public string? BlogContent { get; set; } = null!;

        public bool? DeleteFlag { get; set; }
    }
}
