using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventory.Models
{
    public partial class Artist
    {
        public Artist()
        {
            DiskHasArtists = new HashSet<DiskHasArtist>();
        }

        public int ArtistId { get; set; }
        public string ArtistFname { get; set; }
        public string ArtistLname { get; set; }
        public int ArtistTypeCode { get; set; }

        public virtual ArtistType ArtistTypeCodeNavigation { get; set; }
        public virtual ICollection<DiskHasArtist> DiskHasArtists { get; set; }
    }
}
