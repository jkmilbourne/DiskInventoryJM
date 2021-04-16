using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventory.Models
{
    public partial class Disk
    {
        public Disk()
        {
            DiskHasArtists = new HashSet<DiskHasArtist>();
            DiskHasBorrowers = new HashSet<DiskHasBorrower>();
        }

        public int DiskId { get; set; }
        public string DiskName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int StatusCode { get; set; }
        public int GenreCode { get; set; }
        public int DiskTypeId { get; set; }

        public virtual DiskType DiskType { get; set; }
        public virtual Genre GenreCodeNavigation { get; set; }
        public virtual Status StatusCodeNavigation { get; set; }
        public virtual ICollection<DiskHasArtist> DiskHasArtists { get; set; }
        public virtual ICollection<DiskHasBorrower> DiskHasBorrowers { get; set; }
    }
}
