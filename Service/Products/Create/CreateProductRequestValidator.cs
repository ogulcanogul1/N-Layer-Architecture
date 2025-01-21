using App.Repositories.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Products.Create
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductRequestValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("İsim alanı boş bırakılamaz.")
                .NotNull().WithMessage("İsim alanı boş bırakılamaz.");
            //.Must(MustUniqueProductName).WithMessage("Ürün ismi kayıtlarda bulunuyor");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Fiyat alanı boş bırakılamaz.");

            RuleFor(p => p.Stock)
                .ExclusiveBetween(1, 100).WithMessage("Stock 1 ile 100 arasında olmalıdır.");
        }

        //private bool MustUniqueProductName(string name)
        //{
        //    return !_productRepository.GetAll().Any(x => x.Name == name);
        //} 

        // bu şekilde bir koşul tanımlanırsa async işlem kullanılmaz yani bir çok talep olduğunda sıkışır. Sadece çalışılan kurum için vs kullanılacaksa asenkron olmayan bu şekilde koşul tanımlama tercih edilebilir fakat çok fazla isteğin yapıldığı durumlarda asenkron tercih edilmelidir. // Async halide yazılabilir fakat birkaç konfigürasyon yapmak gerekir bunun yerine service metodlarında da kullanılabilir.
    }
}
