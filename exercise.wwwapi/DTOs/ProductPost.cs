namespace exercise.wwwapi.DTOs;

public class ProductPost
{
    public required string Name { get; set; }
    public required string Category { get; set; }
    public int Price { get; set; } 
}