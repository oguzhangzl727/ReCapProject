using Core.DataAccess.EntityFramework;
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
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, ReCapProjectDbContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomerDetails()
        {
            using (ReCapProjectDbContext context=new ReCapProjectDbContext())
            {
                var result = from customer in context.Customers
                             join user in context.Users
                             on customer.UserId equals user.UserId
                             select new CustomerDetailDto
                             {
                                 CustomerId = customer.CustomerId,
                                 CompanyName = customer.CompanyName,
                                 Email = user.Email,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,

                             };
                return result.ToList();
            }
        }
    }
}
