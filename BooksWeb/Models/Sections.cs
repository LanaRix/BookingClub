using System;
using System.Collections.Generic;

namespace BooksWeb.Models
{
    public partial class Sections
    {
        public Sections()
        {
            Communications = new HashSet<Communications>();
        }

        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public string SectionImage { get; set; }
        public string SectionInformation { get; set; }

        public ICollection<Communications> Communications { get; set; }
    }
}
