using System;
using System.Collections.Generic;

namespace BooksWeb.Models
{
    public partial class Books
    {
        public Books()
        {
            Communications = new HashSet<Communications>();
        }

        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public string BookInformation { get; set; }
        public string BookImage { get; set; }
        public int? BookYear { get; set; }

        public ICollection<Communications> Communications { get; set; }
    }
}
