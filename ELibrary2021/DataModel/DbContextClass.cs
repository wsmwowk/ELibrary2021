using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary2021.DataModel
{
    class DbContextClass:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Connection String
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Elibrary2021;User ID=sa;Password=mkmk");
        }

        public DbSet<BooksModel> Books { get; set; }
        public DbSet<LogModel> Logs { get; set; }
        public DbSet<TakerModel> Takers { get; set; }
        public DbSet<UsersModel> Users { get; set; }
        public DbSet<ReturnedBooks> Returner { get; set; }

    }
}
