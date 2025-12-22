using System.ComponentModel.DataAnnotations;
using System;

namespace Colla_Notes.Models
{
    public class Post
    {
        [Key]
        public string Post_Id { get; set; } = Guid.NewGuid().ToString();
        public string? Username { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Tag_1 { get; set; }
        public string? Tag_2 { get; set; }
        public string Created_on { get; set; } = DateTime.Now.ToString("dd-MM-yyyy HH:mm:");
        public string Updated_on { get; set; } = DateTime.Now.ToString("dd-MM-yyyy HH:mm:");
        // Counters
        public int ViewCount { get; set; } = 0;
        public int LikeCount { get; set; } = 0;
        // Comments stored as JSON or plain text
        public string? Comments { get; set; } = string.Empty;
    }
}
