using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Products
{
    public record ProductDto(int Id, string Name , decimal Price , int Stock , int CategoryId);
    //public record ProductDto
    //{
    //    public int Id { get; init; }
    //    public string Name { get; init; } = default!;
    //    public decimal Price { get; init; }
    //    public int Stock { get; init; }
    //}

    // ProductDto nesnelerinin değiştirilmesi istenmez bu sebeple set bloğu yerine init yazılır eğer bir değer değiştirilmek isteniyorsa yeni nesne oluşturulması gerekir
    
//=> iki record birbiryle karşılaştırıldığı zaman recordların propertyleri birbiriyle karşılaştırılır.İki class birbiriyle karşılaştırıldığı zaman pointer olarak karşılaştırılır.
// iki product karşılaştırılırken pointer olarak karşılaştırılması vs ilgilendirmiyor. İçindeki propertylere göre bir kıyaslama yapmak istiyorum.
}
