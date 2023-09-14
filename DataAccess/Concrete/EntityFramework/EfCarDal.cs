using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapProjectDbContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (ReCapProjectDbContext context=new ReCapProjectDbContext())
            {
                var result = from p in context.Cars
                             join c1 in context.Brands on p.BrandId equals c1.BrandId
                             join c2 in context.Colors on p.ColorId equals c2.ColorId
                             select new CarDetailDto
                             {
                                 CarId = p.CarId,
                                 BrandName=c1.BrandName,
                                 ModelName=p.ModelName,
                                 ColorName=c2.ColorName,
                                 DailyPrice=p.DailyPrice,
                                 ModelYear = p.ModelYear
                             };
                return result.ToList() ;
            }
        }

    }
}
