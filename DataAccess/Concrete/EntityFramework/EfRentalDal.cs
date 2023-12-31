﻿using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, ReCapProjectDbContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (ReCapProjectDbContext context=new ReCapProjectDbContext())
            {
                var result = from rental in context.Rentals
                             join car in context.Cars on rental.CarId equals car.CarId
                             join brand in context.Brands on car.BrandId equals brand.BrandId
                             join customer in context.Customers on rental.CustomerId equals customer.CustomerId
                             join user in context.Users on customer.UserId equals user.UserId
                             select new RentalDetailDto
                             {
                             RentalId = rental.RentalId,
                             CarName = brand.BrandName,
                             CustomerName = user.FirstName + " " + user.LastName,
                             RentDate = rental.RentDate,
                             ReturnDate = rental.ReturnDate,

                             };
                return result.ToList();
                           
            }
        }
    }
}
