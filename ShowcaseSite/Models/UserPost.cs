using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShowcaseSite.Models
{
    /// <summary>
    /// A post from the user can contain a video, text, or picture
    /// </summary>
    public abstract class UserPost
    {
        [Key]
        public int PostId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }


    } 
    /// <summary>
    /// Image upload post
    /// </summary>
    public class ImagePost : UserPost
    {
        public IFormFile PostImage { get; set; }
    }

    public class TextPost : UserPost
    {
        [DataType(DataType.MultilineText)]
        public string PostText { get; set; }
    }

    public class VideoPost : UserPost
    {

    }
}
