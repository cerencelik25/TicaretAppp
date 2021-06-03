
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TicaretApp.Entities;

namespace TicaretApp.DataAccess.Concrete.EfCore
{
    public class TicaretContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-V1O5IFJ\SQLEXPRESS;Initial Catalog=TicaretDB;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KitapKategori>()
                .HasKey(c =>new { c.KitapId, c.KategoriId });
        }
        public DbSet<Kitap> Kitaps { get; set; }
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<Kart> Karts { get; set; }
        public DbSet<Siparis> Siparis { get; set; }
    }
}
