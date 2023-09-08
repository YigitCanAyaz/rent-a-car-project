using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<Brand> Brands { get; set; }

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
    {
        Configuration = configuration;
        Database.EnsureCreated(); // veritabanının oluştuğuna emin ol
    }

    // Brand'i vs. olduğu gibi kullanmak istemiyoruz o yüzden kendi konfigürasyonumuzu yapacağız
    // Mesela brand hangi alan neye karşılık gelecek vs. gibi ayarları yapılır
    // İsimlendirme ve ORM'i iyiye kullanmak için böyle yapıyoruz
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        // IEntityTypeConfiguration implement eden sınıfları bulup runtime sırasında konfigrasyona ekler
    }

}
