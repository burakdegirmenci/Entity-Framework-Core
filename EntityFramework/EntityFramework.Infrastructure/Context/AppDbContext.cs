using EntityFramework.Domain.Entities;
using EntityFramework.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            // NOT: Kullandığınız DB'ye uygun connection stringlere www.connectionstrings.com'dan ulaşabilirsiniz.

            optionsBuilder.UseSqlServer("Server=BURAK\\SQLEXPRESS;Database=EntityFrameworkDB;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Entity Configuration işlemlerini üç farklı yöntemle gerçekleştirebiliriz.

            // 1. Yöntem: modelBuilder kullanarak doğrudan konfigürasyon yapma
            // Bu yöntemde, entity'lerin özelliklerini modelBuilder.Entity<T>() metodu ile doğrudan konfigüre ederiz.
            modelBuilder.Entity<Author>().Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(128);
            modelBuilder.Entity<Book>().Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(128);
            modelBuilder.Entity<Publisher>().Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(128);

            // 2. Yöntem: Ayrı konfigürasyon sınıflarını kullanma
            // Configuration klasöründe bulunan entity konfigürasyon sınıflarını kullanarak her bir entity için ayrı ayrı konfigürasyon yaparız.
            // Bu yöntem, Single Responsibility Principle (Tek Sorumluluk Prensibi) ilkesine uygun olup, kodun daha temiz ve yönetilebilir olmasını sağlar.
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherConfiguration());

            // 3. Yöntem: Assembly'deki tüm konfigürasyonları uygulama
            // IEntityTypeConfiguration arayüzünü implemente eden tüm konfigürasyon sınıflarını belirtilen assembly'den uygulayarak toplu konfigürasyon yaparız.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IEntityConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
