
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pomelo.EntityFrameworkCore.MySql;
using Pchecker.Models;
using ProjektManager.Models;


namespace ProjektManager.DataBaseAPI
{
    public class DBContext : DbContext
    {

        public DbSet<Projekt> Projekte { get; set; }
        public DbSet<Abteilung> Abteilungen { get; set; }
        public DbSet<Mitarbeiter> Mitarbeiter { get; set; }
        public DbSet<Problem> Probleme { get; set; }




        //private string pwd = "123456789";
        //MySqlConnection sql = new MySqlConnection($"server=localhost; database=projekte; uid=root; Password=123456789");

        protected override void OnConfiguring(DbContextOptionsBuilder ob)
        {

                var conString = "server=localhost; database=projekte; uid=root; Password=123456789";
                var serverVersion = new MySqlServerVersion(new Version(8,3, 0));
                ob.UseMySql(conString, serverVersion);
                //sql.Open();
            
        }

        //protected override void OnModelCreating(ModelBuilder mb)
        //{
           


        //    var projekt = Setup<Projekt>(mb);
        //    projekt.Property(x => x.ProjektNr)
        //        .IsRequired();
        //    projekt.OwnsMany(x => x.Probleme);
        //    projekt.HasMany(x  => x.Abteilungen);

        //    base.OnModelCreating(mb);

        //    //var problem = Setup<Problem>(mb);
        //    //problem.Property(Probleme => Probleme.).IsRequired();

        //    //var abteilungen = Setup<Abteilung>(mb);
        //    //abteilungen.Property(x => x.Abteilungen)
        //    //    .IsRequired();



        //    base.OnModelCreating(mb);
        //}



        //private static EntityTypeBuilder<Projekt> Setup<T>(ModelBuilder mb)
        //where T: Entity
        //{ 
        //    var entity = mb.Entity<Projekt>();
        //    entity.HasKey(x => x.Id);
        //    entity.ToTable(typeof(T).Name + "s");
            
            

        //    return entity;

        //}

        //public SQLConnection()
        //{

            

        //    try
        //    {

        //    }
        //    catch
        //    {
        //        Console.WriteLine("Bad");
        //    }

        //}






    }
}

