using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public class Type
{
    [Key]
    public int TypeCode { get; set; }
    public string? TypeName { get; set; }
}

public class District
{
    [Key]
    public int DistrictCode { get; set; }
    public string? DistrictName { get; set; }
}

public class BuildingMaterial
{
    [Key]
    public int MaterialCode { get; set; }
    public string? MaterialName { get; set; }
}

public class RealEstateObject
{
    [Key]
    public int ObjectCode { get; set; }
    public int DistrictCode { get; set; }
    public string? Address { get; set; }
    public int Floor { get; set; }
    public int RoomCount { get; set; }
    public int TypeCode { get; set; }
    public int Status { get; set; }
    public double Price { get; set; }
    public int MaterialCode { get; set; }
    public double Area { get; set; }
    public DateTime ListingDate { get; set; }

    [ForeignKey("DistrictCode")]
    public virtual District District { get; set; }

    [ForeignKey("TypeCode")]
    public virtual Type Type { get; set; }

    [ForeignKey("MaterialCode")]
    public virtual BuildingMaterial BuildingMaterial { get; set; }
}

public class EvaluationCriterion
{
    [Key]
    public int CriterionCode { get; set; }
    public string? CriterionName { get; set; }
}

public class Evaluation
{
    [Key]
    public int EvaluationCode { get; set; }
    public int ObjectCode { get; set; }
    public DateTime EvaluationDate { get; set; }
    public int CriterionCode { get; set; }
    public int Score { get; set; }

    [ForeignKey("ObjectCode")]
    public virtual RealEstateObject RealEstateObject { get; set; }

    [ForeignKey("CriterionCode")]
    public virtual EvaluationCriterion EvaluationCriterion { get; set; }
}

public class Realtor
{
    [Key]
    public int RealtorCode { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? ContactPhone { get; set; }
}

public class Sale
{
    [Key]
    public int SaleCode { get; set; }
    public int ObjectCode { get; set; }
    public DateTime SaleDate { get; set; }
    public int RealtorCode { get; set; }
    public double Price { get; set; }

    [ForeignKey("ObjectCode")]
    public virtual RealEstateObject? RealEstateObject { get; set; }

    [ForeignKey("RealtorCode")]
    public virtual Realtor? Realtor { get; set; }
}


public class RealEstateDbContext : DbContext
{
    public DbSet<Type> Type { get; set; }
    public DbSet<District> District { get; set; }
    public DbSet<BuildingMaterial> BuildingMaterial { get; set; }
    public DbSet<RealEstateObject> RealEstateObject { get; set; }
    public DbSet<EvaluationCriterion> EvaluationCriterion { get; set; }
    public DbSet<Evaluation> Evaluation { get; set; }
    public DbSet<Realtor> Realtor { get; set; }
    public DbSet<Sale> Sale { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=data_base_2;Username=postgres;Password=admin");
    }
}