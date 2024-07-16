using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tema18.Models;

namespace Tema18.CarData
{
    public class CarDataContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Key> Keys { get; set; }
        public DbSet<TechnicalBook> TechnicalBooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\cursC#\Tema18\Tema18\CarDataDB.mdf;Integrated Security=True");
        }

    }
}