namespace Practica_CSR.Services.Implementations;

using Practica_CSR.Entities;
using Practica_CSR.Models.DTOs.Requests;
using Practica_CSR.Models.DTOs.Reponses;
using Practica_CSR.Repositories.Interfaces;
using Practica_CSR.Services.Interfaces;
using Practica_CSR.Repositories.Implementations;
 

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public List<ProductForReadDto> GetAllProducts()
    {
        var productos = _repository.GetAllProducts();
        var resultado = new List<ProductForReadDto>();

        foreach (var producto in productos)
        {
            var dto = new ProductForReadDto
            {
                Id = producto.Id,
                Name = producto.Name,
                Price = producto.Price
            };
            resultado.Add(dto);
        }

        return resultado;
    }
    public ProductForReadDto? GetProductById(int id)
    {
        var producto = _repository.GetProductById(id);
        if (producto is null) return null;

        return new ProductForReadDto
        {
            Id = producto.Id,
            Name = producto.Name,
            Price = producto.Price
        };
    }

    public ProductStatsDto GetStats()
    {
        List<Product> products = _repository.GetAllProducts();
        if (products.Count is 0)
        {
            return new ProductStatsDto()
            {
                Total = 0,
                AveragePrice = 0,
                MostExpensiveName = "No hay productos cargados"
            };
        }
        Product mostExpensive = products.OrderByDescending(p => p.Price).First();
        return new ProductStatsDto
        {
            Total = products.Count,
            AveragePrice = Math.Round(products.Average(p => p.Price), 2),
            MostExpensiveName = mostExpensive.Name
        };
    }

    public ProductForReadDto CreateProduct(ProductForCreateDto dto)
    {
        var producto = new Product
        {
            Name = dto.Name,
            Price = dto.Price
        };

        _repository.AddProduct(producto); //  el repositorio le asigna el Id nuevo

        return new ProductForReadDto
        {
            Id = producto.Id,
            Name = producto.Name,
            Price = producto.Price
        };
    }

    public void UpdateProduct(int id, ProductForUpdateDto dto)
    {
        var product = _repository.GetProductById(id);

        if (product is null)
        {
            return;
        }
        product.Name = dto.Name;
        product.Price = dto.Price;
        _repository.UpdateProduct(product);

    }
    public void DeleteProduct(int id)
    {
        var product = _repository.GetProductById(id);
        if (product is null)
        {
            return;
        }
        _repository.DeleteProduct(product);
    }
    public List<ProductForReadDto> SearchProductsByName(string name)
    {
        List<Product> found = _repository.SearchProductsByName(name);
        List<ProductForReadDto> result = new List<ProductForReadDto>();

        foreach (Product product in found)
        {
            result.Add(new ProductForReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            });
        }

        return result;
    }

    public bool NameExists(string name)
    {
        List<Product> products = _repository.GetAllProducts();
        return products.Any(p => p.Name.ToLower() == name.ToLower());
    }
}