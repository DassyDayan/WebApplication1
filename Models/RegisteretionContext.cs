using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
  public class RegisteretionContextDL : DbContext
  {
    public RegisteretionContextDL()
    { }
    public RegisteretionContextDL(DbContextOptions<RegisteretionContextDL> options)
        : base(options)
    { }
    public DbSet<TModerator> TModerator { get; set; }
    public DbSet<TMatriculation> TMatriculation { get; set; }
    public DbSet<TInstitution> TInstitution { get; set; }
    public DbSet<TInstitutionUser> TInstitutionUser { get; set; }
    public DbSet<TMatriculationParams> TMatriculationParams { get; set; }
    public DbSet<TMatriculationInstitutionParams> TMatriculationInstitutionParams { get; set; }
    public DbSet<TMatriculationInstitution> TMatriculationInstitution { get; set; }
    public DbSet<TMatriculationInstitutionTester> TMatriculationInstitutionTester { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<TModerator>().HasNoKey();
    }
  }
}
