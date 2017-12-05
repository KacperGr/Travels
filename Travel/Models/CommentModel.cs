using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Body { get; set; }
        public int RouteId { get; set; }

        public virtual Route Routes { get; set; }
    }
}