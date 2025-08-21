using exercise.wwwapi.DTOs;
using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints;

public static class ProductEndpoint
{
    public static void ConfigureProductEndpoints(this WebApplication app)
    {
        var products = app.MapGroup("products");
        products.MapPost("/", Create);
        products.MapGet("/", GetAll);
        products.MapGet("/{id}", Get);
        products.MapPost("/{id}", Update);
        products.MapDelete("/{id}", Delete);
    }
    
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    private static async Task<IResult> Create(IProductRepository repository, ProductPost model)
    {
        var existing = await repository.GetAll();
        if (existing.Any(other => other.Name == model.Name))
        {
            return TypedResults.BadRequest("Product name already exists");
        }
        
        var product = await repository.Create(new Product
        {
            Price = model.Price,
            Category = model.Category,
            Name = model.Name,
        });
        
        return TypedResults.Created($"/products/{product.Id}", product);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    private static async Task<IResult> GetAll(IProductRepository repository, string? category)
    {
        var products = (await repository.GetAll(category)).ToList();
        if (products.Count == 0)
        {
            return TypedResults.NotFound();
        }
        
        return TypedResults.Ok(products);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    private static async Task<IResult> Get(IProductRepository repository, int id)
    {
        var product = await repository.Get(id);

        if (product == null)
        {
            return TypedResults.NotFound("Product not found");
        }
        
        return TypedResults.Ok(product);
    }
    
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    private static async Task<IResult> Update(IProductRepository repository, int id,  ProductPost model)
    {
        var product = await repository.Get(id);
        if (product == null)
        {
            return TypedResults.NotFound("Product not found");
        }
        
        var stored = await repository.GetAll();
        if (stored.Any(other => other.Id != id && other.Name == model.Name))
        {
            return TypedResults.BadRequest("Product name already exists");
        }
        
        product.Name = model.Name;
        product.Category = model.Category;
        product.Price = model.Price;
        
        var result = await repository.Update(product);
        return TypedResults.Created($"/products/{result.Id}", result);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    private static async Task<IResult> Delete(IProductRepository repository, int id)
    {
        var result = await repository.Delete(id);

        if (result == null)
        {
            return  TypedResults.NotFound("Product not found");
        }
        
        return TypedResults.Ok(result);
    }
}