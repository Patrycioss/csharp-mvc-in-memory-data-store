using System.ComponentModel.DataAnnotations;

namespace exercise.wwwapi.Models;

public class Product
{
    public int Id { get; set; }

    [MaxLength(50)] public required string Name { get; set; }

    [MaxLength(50)] public required string Category { get; set; }

    public required int Price { get; set; }
}