using BolsaEmpleo_BE.Entities.Citizen;
using BolsaEmpleo_BE.Entities.DocumentType;
using BolsaEmpleo_BE.Entities.Vacancy;
using Microsoft.EntityFrameworkCore;

namespace BolsaEmpleo_BE.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<DocumentType> Document_Types { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Citizen>()
              .Property(c => c.ID)
              .HasColumnName("id")
              .ValueGeneratedOnAdd(); 

            modelBuilder.Entity<Citizen>()
                .Property(c => c.VacancyID)
                .HasColumnName("vacancy_id");

            

            base.OnModelCreating(modelBuilder);
        }
    }
}
