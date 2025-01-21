using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Products.Update
{
    internal class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün ismi boş olamaz.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Ürünün Fiyatı 0'dan büyük olmalıdır.");

            RuleFor(x => x.Stock)
                .ExclusiveBetween(0, 100).WithMessage("Stok 0-100 arasında olmalıdır.");
        }
    }
}
