using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ELibrary2021.DataModel
{
    class UsersModel
    {
        [Key]
        public int id { get; set; }
        public string Username { get; set; }
        public string password { get; set; }
    }
}
