using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using makeup1.Models;

namespace makeup1.Repositories
{
    public interface IPhotoRepository
    {
        IEnumerable<Photo> GetAllPhotos { get; set; }
        //IEnumerable<Photo> GetAllPhotos();
        bool Add(Photo photo);
    }
}
