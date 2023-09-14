using Core.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator:AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.FirstName).NotEmpty().WithMessage("Kullanıcı İsmi Boş Olamaz!");
            RuleFor(user => user.FirstName).MinimumLength(2).WithMessage("Kullanıcı İsmi 2 Karakteden Daha Az Olamaz!");

            
            RuleFor(user => user.LastName).NotEmpty().WithMessage("Kullanıcı Soyismi Boş Olamaz!");
            RuleFor(user => user.LastName).MinimumLength(2).WithMessage("Kullanıcı Soyismi 2 Karakterden Daha Az Olamaz!");

            
            RuleFor(user => user.Email).NotEmpty().WithMessage("Kullanıcı Email Boş Olamaz!");
            RuleFor(user => user.Email).Must(ControlEmail).WithMessage("Geçersiz Email!");

        }

        private bool ControlEmail(string email)
        {
            // E-posta adresi düzenini ifade eden bir Regex örneği
            Regex regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

            return regex.IsMatch(email); 
        }
    }

    }

