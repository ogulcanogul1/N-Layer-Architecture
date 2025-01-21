using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Products.Create
{
    public record CreateProductRequest(string Name, decimal Price, int Stock,int CategoryId);

    //public int Id { get; set; }
    //public string Name { get; set; } = default!;
    //public decimal Price { get; set; }
    //public int Stock { get; set; }
}
