using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Please enter an Artists Last Name or Name of a Group")]
        public string ArtistLname { get; set; }
        [Required(ErrorMessage = "Please select an Artist Type")]
        [Display(Name = "Artist Type")]
        public int ArtistTypeCode { get; set; }

        public virtual ArtistType ArtistTypeCodeNavigation { get; set; }
        public virtual ICollection<DiskHasArtist> DiskHasArtists { get; set; }
    }
}
