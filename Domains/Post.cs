using System;
using System.ComponentModel.DataAnnotations;

namespace TweetBook.Domains{
    public class Post
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        
    }
}