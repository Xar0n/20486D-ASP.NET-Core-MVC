using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignProject
{
    class Photo
    {
        public int PhotoID { get; set; }  
        public string Title { get; set; }
        public byte[] PhotoFile { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Комментарий
        /// </summary>
        public DateTime CreatedDate { get; set; }
        public int Owner { get; set; }

        internal List<Comment> PhotoComments
        {
            get => default;
            set
            {
            }
        }
    }
}
