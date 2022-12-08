using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace projeOdev.Models
{
    public class MyContext:DbContext
    {
        public DbSet<Ogrenci> Ogrencis { get; set; }
        public DbSet<Yonetim> OkulYonetims { get; set; }
        public DbSet<Ders> Derss { get; set; }
        public DbSet<Atama> Atamas { get; set; }
    }
}