using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ELibrary2021.DataModel
{
    class ReturnedBooks
    {
        [Key]
        public int id { get; set; }
        public string BookName { get; set; }
        public string takerName { get; set; }
        public int period { get; set; }

        [Column(TypeName = "date")]
        public DateTime ReturnDate { get; set; }
    }
}
