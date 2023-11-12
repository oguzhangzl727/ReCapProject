using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            try
            {
                var existingRentals = _rentalDal.GetAll(r => r.CarId == rental.CarId && (r.ReturnDate == null || r.ReturnDate > DateTime.Now));

                if (existingRentals.Any())
                {
                    return new ErrorResult("Araç şuanda kiralanmış.");
                }
                else
                {
                    _rentalDal.Add(rental);
                    return new SuccessResult(Messages.RentalAdded);
                }
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<Car>>(ex.ToString());
            }

        }

        public IResult Delete(Rental rental)
        {
            
            try
            {
                _rentalDal.Delete(rental);
                return new SuccessResult(Messages.RentalDeleted);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<Car>>(ex.ToString());
            }
        }



        [CacheAspect]
        public IDataResult<List<Rental>> GetAll()
        {
            try
            {
                var result = _rentalDal.GetAll();

                if (result.Count == 0)
                {
                    return new ErrorDataResult<List<Rental>>(Messages.NullData);
                }

                return new SuccessDataResult<List<Rental>>(result, Messages.RentalsListed);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<Rental>>(ex.ToString());
            }
        }




        [CacheAspect]
        public IDataResult<Rental> GetById(int id)
        {
            try
            {
                var result = _rentalDal.Get(r => r.RentalId == id);

                if (result != null)
                {
                    return new SuccessDataResult<Rental>(result, Messages.RentalsListed);
                }

                return new ErrorDataResult<Rental>(Messages.NullData);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<Rental>(ex.ToString());
            }
        }




        [CacheAspect]
        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            try
            {
                var result = _rentalDal.GetRentalDetails();

                if (result.Count == 0)
                {
                    return new ErrorDataResult<List<RentalDetailDto>>(Messages.NullData);
                }

                return new SuccessDataResult<List<RentalDetailDto>>(result, Messages.RentalDetailsListed);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<RentalDetailDto>>(ex.ToString());
            }
        }




        [CacheAspect]
        public IDataResult<List<Rental>> GetRentalsByCarId(int id)
        {
            try
            {
                var result = _rentalDal.GetAll(r => r.CarId == id);

                if (result.Count == 0)
                {
                    return new ErrorDataResult<List<Rental>>(Messages.NullData);
                }

                return new SuccessDataResult<List<Rental>>(result, Messages.RentalsListedByCarId);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<Rental>>(ex.ToString());
            }
        }



        [CacheAspect]
        public IDataResult<List<Rental>> GetRentalsByCustomerId(int id)
        {
            try
            {
                var result = _rentalDal.GetAll(r => r.CustomerId == id);

                if (result.Count == 0)
                {
                    return new ErrorDataResult<List<Rental>>(Messages.NullData);
                }

                return new SuccessDataResult<List<Rental>>(result, Messages.RentalsListedByCustomerId);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<Rental>>(ex.ToString());
            }
        }


        [ValidationAspect(typeof(RentalValidator))]
        public IResult Update(Rental rental)
        {
            try
            {
                _rentalDal.Update(rental);
                return new SuccessResult(Messages.RentalUpdated);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<Car>>(ex.ToString());
            }
        }
    }
}
