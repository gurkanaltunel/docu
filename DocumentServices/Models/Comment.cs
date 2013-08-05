using System;
using ServiceStack.DataAnnotations;

namespace DocumentServices.Models
{
    public class Comment
    {
        [AutoIncrement]
        public int Id { get; set; }

        public DateTime CommentDate { get; set; }

        public string Text{ get; set; }

        [References(typeof(User))]
        public int OwnerId { get; set; }

        [References(typeof(File))]
        public int FileId { get; set; }

        [References(typeof(FileVersion))]
        public int FileVersionId { get; set; }

        [Ignore]
        public User OwnerUser { get; set; }
    }
}