using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }


        [SecuredOperation("car.add,admin")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            try
            {
                if (car.ModelName.Length < 2)
                {
                    return new ErrorResult(Messages.CarNameInvalid);

                }
                _carDal.Add(car);
                return new SuccessResult(Messages.CarAdded);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<Car>>(ex.ToString());
            }


        }



        [SecuredOperation("car.delete,admin")]
        public IResult Delete(Car car)
        {
            try
            {
                _carDal.Delete(car);
                return new SuccessResult(Messages.CarDeleted);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<Car>>(ex.ToString());
            }
        }

        public IDataResult<List<Car>> GetAll()
        {
            try
            {
                var result = _carDal.GetAll();

                if (result.Count == 0)
                {
                    return new ErrorDataResult<List<Car>>(Messages.NullData);
                }

                return new SuccessDataResult<List<Car>>(result, Messages.CarsListed);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<Car>>(ex.ToString());
            }

        }

        public IDataResult<Car> GetById(int carId)
        {
            
            try
            {
                var result = _carDal.Get(c => c.CarId == carId);

                if (result != null  )
                {
                    return new SuccessDataResult<Car>(result, Messages.CarsListed);
                    
                }
                return new ErrorDataResult<Car>(Messages.NullData);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<Car>(ex.ToString()); 
            }
        }
        

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            
            try
            {
                var result = _carDal.GetCarDetails();

                if (result.Count == 0)
                {
                    return new ErrorDataResult<List<CarDetailDto>>(Messages.NullData);
                }

                return new SuccessDataResult<List<CarDetailDto>>(result, Messages.CarDetailsListed);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<CarDetailDto>>(ex.ToString());
            }
        }

        

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
          
            try
            {
                var result = _carDal.GetAll(c => c.BrandId == brandId);

                if (result.Count == 0)
                {
                    return new ErrorDataResult<List<Car>>(Messages.NullData);
                }

                return new SuccessDataResult<List<Car>>(result, Messages.CarsListedByBrandId);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<Car>>(ex.ToString());
            }
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            try
            {
                var result = _carDal.GetAll(c => c.ColorId == colorId);

                if (result.Count == 0)
                {
                    return new ErrorDataResult<List<Car>>(Messages.NullData);
                }

                return new SuccessDataResult<List<Car>>(result, Messages.CarsListedByColorId);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<Car>>(ex.ToString());
            }
        }





        [SecuredOperation("car.update,admin")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
            try
            {
                _carDal.Update(car);
                return new SuccessResult(Messages.CarUpdated);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<Car>>(ex.ToString());
            }
        }
    }



    }
