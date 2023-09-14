using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        // NO Data message
        public static string NullData = "İsteğinize uygun bir veri yok.";

        // Car Success Message
        public static string CarAdded = "Araba eklendi.";
        public static string CarDeleted = "Araba silindi.";
        public static string CarUpdated = "Araba güncellendi.";
        public static string CarsListed = "Arabalar listelendi.";
        public static string CarDetailsListed = "Araba detayları listelendi.";
        public static string CarsListedByBrandId = "BrandId'ye uygun arabalar listelendi.";
        public static string CarsListedByColorId = "ColorId'ye uygun arabalar listelendi.";

        //Car Error Message
        public static string CarNameInvalid = "Araba ismi geçersiz";

        // Brand Success Message

        public static string BrandAdded = "Marka eklendi.";
        public static string BrandDeleted = "Marka silindi.";
        public static string BrandUpdated = "Marka güncellendi.";
        public static string BrandsListed= "Markalar listelendi.";

        //Brand Error Message
        public static string BrandNameInvalid = "Marka ismi geçersiz";


        // Color Success Message
        public static string ColorAdded = "Renk eklendi.";
        public static string ColorDeleted = "Renk silindi.";
        public static string ColorUpdated = "Renk güncellendi.";
        public static string ColorsListed = "Renkler listelendi.";

        //Color Error Message
        public static string ColorNameInvalid = "Renk ismi geçersiz";

        //Customer Success Message
        public static string CustomerAdded = "Müşteri eklendi.";
        public static string CustomerDeleted = "Müşteri silindi.";
        public static string CustomerUpdated = "Müşteri güncellendi.";
        public static string CustomersListed = "Müşteriler listelendi.";
        public static string CustomerDetailsListed = "Müşteri detayları listelendi.";

        //Rental Success Message
        public static string RentalAdded = "Kiralama kaydı eklendi.";
        public static string RentalDeleted = "Kiralama kaydı silindi.";
        public static string RentalUpdated = "Kiralama kaydı güncellendi.";
        public static string RentalsListed = "Kiralama kayıtları listelendi.";
        public static string RentalDetailsListed = "Kiralama detayları listelendi.";
        public static string RentalsListedByCarId = "CarId'ye uygun kirlama kayıtları listelendi.";
        public static string RentalsListedByCustomerId = "CustomerId'ye uygun kirlama kayıtları listelendi.";


        // User Success Message
        public static string UserAdded = "Kullanıcı eklendi.";
        public static string UserDeleted = "Kullanıcı silindi.";
        public static string UserUpdated = "Kullanıcı güncellendi.";
        public static string UsersListed = "Kullanıcılar listelendi.";
        internal static string UserClaimsListed = "Kullanıcı rolleri listelendi.";

        // CarImage Success Message
        public static string CarImageAdded = "Araç resmi eklendi.";
        public static string CarImageDeleted = "Araç resmi silindi.";
        public static string CarImagesListed = "Araç resimleri Listelendi.";
        public static string CarImageListed = "Araç resmi listelendi.";
        public static string CarImageUpdated = "Araç resmi güncellendi.";

        public static string CarImageLimitExceded = "Araba resim ekleme sınırı dolu.";
        public static string UserNotFound="Kullanıcı bulunamadı.";
        public static string PasswordError="Şifre Hatalı.";
        public static string SuccessfulLogin="Sisteme Giriş Başarılı.";
        public static string UserAlreadyExists="Bu kullanıcı zaten mevcut.";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi.";
        public static string AccessTokenCreated="Access token başarıyla oluşturuldu.";
        internal static string? AuthorizationDenied= "İşlem için Yetkiniz yok!";
    }
}
