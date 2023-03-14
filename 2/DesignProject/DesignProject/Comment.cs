using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignProject
{
    class Comment
    {
        public int CommentID { get; set; }

        public string User { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public int PhotoId { get; set; }

        internal Photo PhotoComments
        {
            get => default;
            set
            {
            }
        }
    }
}
