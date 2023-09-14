using Entities.Concrete;
using FluentValidation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Entities.Concrete.Color;

namespace Business.ValidationRules.FluentValidation
{
    public class ColorValidator: AbstractValidator<Color>
    {
        public ColorValidator() 
        {
            RuleFor(c => c.ColorName).NotEmpty().WithMessage("Marka İsmi Boş Olamaz.");
            RuleFor(c => c.ColorName).MinimumLength(2).WithMessage("Marka İsmi Minimum 2 Karakter Olmalıdır.");
        }
    }
}
