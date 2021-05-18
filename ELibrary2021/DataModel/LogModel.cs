using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELibrary2021.DataModel
{
    class LogModel
    {
        [Key]
        public int id { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime OperationDate { get; set; }
        public string operation { get; set; }
        public string username { get; set; }
    }
}
