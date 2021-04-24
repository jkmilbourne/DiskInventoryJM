using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Please enter a Disk Name")]
        public string DiskName { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        [Display(Name = "Disk Status")]
        public int StatusCode { get; set; }
        [Required]
        [Display(Name = "Disk Genre")]
        public int GenreCode { get; set; }
        [Required]
        [Display(Name = "Disk Type")]
        public int DiskTypeId { get; set; }

        
        public virtual DiskType DiskType { get; set; }
        
        public virtual Genre GenreCodeNavigation { get; set; }
        
        public virtual Status StatusCodeNavigation { get; set; }
        
        public virtual ICollection<DiskHasArtist> DiskHasArtists { get; set; }
        
        public virtual ICollection<DiskHasBorrower> DiskHasBorrowers { get; set; }
    }
}
