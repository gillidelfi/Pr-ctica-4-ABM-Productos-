namespace Practica_CSR.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // = string.Empty evita que Name pueda quedar en null
    public decimal Price { get; set; } 

}