using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace makeup1.Models
{
    public class Hashtag
    {
        public int ID { get; set; }
        public string HastagName { get; set; }
        
        public int HashtagPhotoId { get; set; }

        [ForeignKey("HashtagPhotoId")]
        public virtual Photo Photo { get; set; }
    }
}