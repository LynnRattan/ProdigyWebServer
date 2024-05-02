using ProdigyServerBL.Models;

namespace ProdigyWeb.Models
{
    public partial class Post
    {
        public int Id { get; set; }

        public int CreatorId { get; set; }

        public string Title { get; set; } = "";

        public string? Content { get; set; }

        public DateTime UploadDateTime { get; set; } = new();

        public string? FileExtension { get; set; }

        public int? WorkId { get; set; }

        public virtual User Creator { get; set; } = new();

        public int? ComposerId { get; set; }

    }
       
}
