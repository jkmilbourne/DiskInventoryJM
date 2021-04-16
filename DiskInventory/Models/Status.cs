using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventory.Models
{
    public partial class Status
    {
        public Status()
        {
            Disks = new HashSet<Disk>();
        }

        public int StatusCode { get; set; }
        public string StatusDesc { get; set; }

        public virtual ICollection<Disk> Disks { get; set; }
    }
}
