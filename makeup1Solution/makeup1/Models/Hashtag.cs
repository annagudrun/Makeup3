using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace makeup1.Models
{
    public class Hashtag
    {
        public int ID { get; set; }
        public string HastagName { get; set; }
        public int HashtagPhotoId { get; set; }
    }
}