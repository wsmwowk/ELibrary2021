using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ELibrary2021.DataModel
{
    class BooksModel
    {
        [Key]
        public int id { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }

        [AllowNull]
        public int BookParts { get; set; }
        public string BookClass { get; set; }
        public string BookLang { get; set; }

    }
}
