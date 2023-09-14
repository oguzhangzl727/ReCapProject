using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;
        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }


        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            try
            {
                _customerDal.Add(customer);
                return new SuccessResult(Messages.CustomerAdded);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<Car>>(ex.ToString());
            }
        }


        public IResult Delete(Customer customer)
        {
            try
            {
                _customerDal.Delete(customer);
                return new SuccessResult(Messages.CustomerDeleted);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<Car>>(ex.ToString());
            }
        }

        public IDataResult<List<Customer>> GetAll()
        {
            try
            {
                var result = _customerDal.GetAll();

                if (result.Count == 0)
                {
                    return new ErrorDataResult<List<Customer>>(Messages.NullData);
                }

                return new SuccessDataResult<List<Customer>>(result, Messages.CustomersListed);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<Customer>>(ex.ToString());
            }
        }

        public IDataResult<Customer> GetById(int id)
        {
            try
            {
                var result = _customerDal.Get(c => c.CustomerId == id);

                if (result != null)
                {
                    return new SuccessDataResult<Customer>(result, Messages.CustomersListed);
                }

                return new ErrorDataResult<Customer>(Messages.NullData);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<Customer>(ex.ToString());
            }
        }

        public IDataResult<List<CustomerDetailDto>> GetCustomerDetails()
        {
            try
            {
                var result = _customerDal.GetCustomerDetails();

                if (result.Count == 0)
                {
                    return new ErrorDataResult<List<CustomerDetailDto>>(Messages.NullData);
                }

                return new SuccessDataResult<List<CustomerDetailDto>>(result, Messages.CustomerDetailsListed);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<CustomerDetailDto>>(ex.ToString());
            }
        }


        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Update(Customer customer)
        {
            try
            {
                _customerDal.Update(customer);
                return new SuccessResult(Messages.CustomerUpdated); ;
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<Car>>(ex.ToString());
            }
        }
    }
}
