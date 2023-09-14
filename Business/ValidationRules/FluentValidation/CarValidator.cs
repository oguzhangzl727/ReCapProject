using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(p => p.BrandId).NotEmpty().WithMessage("Brand Id boş olamaz!");
            RuleFor(p => p.ModelYear).NotEmpty().WithMessage("Model yılı boş olamaz!");
            RuleFor(p => p.DailyPrice).NotEmpty().WithMessage("Günlük Kiralama ücreti boş olamaz!");
            RuleFor(p => p.ColorId).NotEmpty().WithMessage("Color Id boş olamaz!");
            RuleFor(p => p.ModelName).NotEmpty().WithMessage("Model ismi boş olamaz!");
            RuleFor(p => p.ModelYear).GreaterThanOrEqualTo(2000).WithMessage("Araba Modeli 2000den yüksek olmalıdır!");
        }

        
    }
}
