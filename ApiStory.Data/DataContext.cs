using ApiStory.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStory.Data;

public class DataContext : DbContext
{
    public DbSet<Vote> Votes { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Story> Stories { get; set; }
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Vote>().HasKey(x => x.Id);
        modelBuilder.Entity<Vote>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<Vote>().Property(x => x.StoryId).IsRequired();
        modelBuilder.Entity<Vote>().Property(x => x.Like).IsRequired();
        modelBuilder.Entity<Vote>().Property(x => x.ClientId).IsRequired();

        modelBuilder.Entity<Client>().HasKey(x => x.Id);
        modelBuilder.Entity<Client>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<Client>().Property(x => x.Name).IsRequired().HasMaxLength(200);

        modelBuilder.Entity<Story>().HasKey(x => x.Id);
        modelBuilder.Entity<Story>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<Story>().Property(x => x.Title).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Story>().Property(x => x.Area).IsRequired().HasMaxLength(50); 
        modelBuilder.Entity<Story>().Property(x => x.Description).HasMaxLength(500).IsRequired();


        modelBuilder.Entity<Client>().HasMany(x => x.Votes).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).IsRequired();
        modelBuilder.Entity<Story>().HasMany(x => x.Votes).WithOne(v => v.Story).HasForeignKey(x => x.StoryId).IsRequired();


        //modelBuilder.Entity<Vote>()
        //    .HasOne(v => v.Client)
        //    .WithMany()
        //    .HasForeignKey("ClientId");

        //modelBuilder.Entity<Story>()
        //    .HasMany(s => s.Votes)
        //    .WithOne()
        //    .HasForeignKey("StoryId");
    }
}
