using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvancedApp.Models
{
    public class AdvancedContext : DbContext
    {
        public AdvancedContext(DbContextOptions<AdvancedContext> options)
            : base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            Database.AutoTransactionsEnabled = false;
        }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Using HiLo as primary key. 
            //modelBuilder.Entity<Employee>()
            //    .Property(e => e.Id).ForSqlServerUseSequenceHiLo();

            //Unique Key Index
            //modelBuilder.Entity<Employee>()
            //    .HasIndex(e => e.SSN).HasName("SSNIndex").IsUnique();


            // Alternate key to make relation
            //modelBuilder.Entity<Employee>().HasAlternateKey(e => e.SSN);
            //modelBuilder.Entity<SecondaryIdentity>()
            //    .HasOne(s => s.PrimaryIdentity)
            //    .WithOne(e => e.OtherIdentity)
            //    .HasPrincipalKey<Employee>(e => e.SSN)
            //    .HasForeignKey<SecondaryIdentity>(s => s.PrimarySSN);


            //Natural key as primary key
            //modelBuilder.Entity<Employee>().Ignore(e => e.Id);
            //modelBuilder.Entity<Employee>().HasKey(e => e.SSN);
            //modelBuilder.Entity<SecondaryIdentity>()
            //    .HasOne(s => s.PrimaryIdentity)
            //    .WithOne(e => e.OtherIdentity)
            //    .HasPrincipalKey<Employee>(e => e.SSN)
            //    .HasForeignKey<SecondaryIdentity>(s => s.PrimarySSN);


            modelBuilder.Entity<Employee>()
                .HasQueryFilter(e => !e.SoftDeleted);

            //create Composite Keys
            modelBuilder.Entity<Employee>().Ignore(e => e.Id);
            modelBuilder.Entity<Employee>()
            .HasKey(e => new { e.SSN, e.FirstName, e.FamilyName });
            modelBuilder.Entity<Employee>().Property(e => e.Salary).HasColumnType("decimal(8,2)").HasField("databaseSalary").UsePropertyAccessMode(PropertyAccessMode.Field);
            //.IsConcurrencyToken();
            modelBuilder.Entity<Employee>().Property<DateTime>("LastUpdated").HasDefaultValue(DateTime.Now);
            //modelBuilder.Entity<Employee>().Property(e => e.RowVersion).IsRowVersion();
            modelBuilder.Entity<Employee>().Ignore(e => e.RowVersion);

            //modelBuilder.Entity<Employee>().Property(e => e.GeneratedValue).HasDefaultValueSql("getdate()");
            modelBuilder.HasSequence<int>("ReferenceSequence").StartsAt(100).IncrementsBy(2);
            //modelBuilder.Entity<Employee>().Property(e => e.GeneratedValue).HasDefaultValueSql(@"'REFERENCE_'+convert(varchar,next value for ReferenceSequence)");
            //modelBuilder.Entity<Employee>().Property(e => e.GeneratedValue).HasComputedColumnSql(@"substring(FirstName,1,1)+FamilyName persisted");
            //modelBuilder.Entity<Employee>().HasIndex(e => e.GeneratedValue);
            modelBuilder.Entity<Employee>().Property(e => e.GeneratedValue).ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<SecondaryIdentity>().HasOne(s => s.PrimaryIdentity).WithOne(e => e.OtherIdentity).HasPrincipalKey<Employee>(e => new { e.SSN, e.FirstName, e.FamilyName }).HasForeignKey<SecondaryIdentity>(s => new { s.PrimarySSN, s.PrimaryFirstName, s.PrimaryFamilyName })
                //.OnDelete(DeleteBehavior.Cascade); // Birincil nesne silindiğinde bağımlı tüm nesenler de silinir. Dikkatli olunmalı yoksa beklenildiğinden fazla veri silinebilir sonra kalırsın öle.
                //.OnDelete(DeleteBehavior.SetNull); // Burada bağımlı nesne silinmez. Bağımlılık sütunu null olarak güncellenir. Bunu db yapar.
                //.OnDelete(DeleteBehavior.ClientSetNull); // Burada bağımlı nesne silinmez. Bağımlılık sütunu null olarak güncellenir. Bunu ef yapar.
                .OnDelete(DeleteBehavior.Restrict); // Burada bağımlı nesnede hiçbir değişiklik yapılmaz. Silme işlemi garanti edilmiş olur.
            modelBuilder.Entity<SecondaryIdentity>().Property(s => s.Name).HasMaxLength(100);
        }
    }
}
