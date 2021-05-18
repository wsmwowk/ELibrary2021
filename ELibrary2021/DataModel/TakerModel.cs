using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELibrary2021.DataModel
{
    class TakerModel
    {
        [Key]
        public int id { get; set; }
        public string TakerName { get; set; }
        public string TakerPhone { get; set; }
        public BooksModel BookId { get; set; }
        public string BookName { get; set; }
        public string BookClass { get; set; }
        public int periodInDays { get; set; }

        [Column(TypeName = "date")]
        public DateTime TakedDate { get; set; }
    }
}
