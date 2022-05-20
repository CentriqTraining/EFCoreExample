using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreMappings.Models
{
    public class Company
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [ForeignKey("CompanyType")]
        public int CompanyTypeId { get; set; }
        public CompanyType CompanyType { get; set; }
    }
    public class CompanyType
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Abbreviation { get; set; }
    }
    public class TestCtx : DbContext
    {
        public TestCtx(DbContextOptions<TestCtx> opts) : base(opts) {   }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  Uncomment me or Controller Include
            //modelBuilder
            //    .Entity<Company>()
            //    .Navigation(d => d.CompanyType)
            //    .AutoInclude();
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }

        public void BuildTestData()
        {
            CompanyTypes.AddRange(new List<CompanyType>()
            {
                new CompanyType() { Description = "Food Service/Restaurant", Abbreviation = "Food"},
                new CompanyType() { Description = "Public Works", Abbreviation = "BWorks"},
                new CompanyType() { Description = "Banking", Abbreviation = "Bank"},
                new CompanyType() { Description = "Law Enforcement", Abbreviation = "PoPo"},
                new CompanyType() { Description = "IT Consulting", Abbreviation = "ITCons"},
                new CompanyType() { Description = "Security", Abbreviation = "Sec"},
            });
            SaveChanges();
            Companies.Add(new Company()
            {
                Name = "Bubba Gump Shrimp Company",
                CompanyType = CompanyTypes.First(d => d.Abbreviation == "Food")
            });
            SaveChanges();
        }
    }
}
