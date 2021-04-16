using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventory.Models
{
    public partial class DiskType
    {
        public DiskType()
        {
            Disks = new HashSet<Disk>();
        }

        public int DiskTypeCode { get; set; }
        public string DiskTypeDesc { get; set; }

        public virtual ICollection<Disk> Disks { get; set; }
    }
}
