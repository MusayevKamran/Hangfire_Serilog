using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HOSAPI.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string CommentContent { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int ProductId1 { get; set; }
    }
}
