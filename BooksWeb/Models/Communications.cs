using System;
using System.Collections.Generic;

namespace BooksWeb.Models
{
    public partial class Communications
    {
        public int CommunicationId { get; set; }
        public int SelectionId { get; set; }
        public int BookId { get; set; }

        public Books Book { get; set; }
        public Sections Selection { get; set; }
    }
}
