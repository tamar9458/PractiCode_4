using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using System.Configuration;

namespace ToDoApi;

public partial class ToDoDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public  DbSet<Item> Items { get; set; }
 
     public ToDoDbContext(DbContextOptions<ToDoDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {        
        string connectionString = _configuration["ToDoDB"];
        optionsBuilder.UseMySql(connectionString,Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("items");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
