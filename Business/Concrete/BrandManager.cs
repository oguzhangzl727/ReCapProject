using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;
        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }



        [SecuredOperation("brand.add,admin")]
        [ValidationAspect(typeof( BrandValidator))]
        public IResult Add(Brand brand)
        {
            try
            {
                if (brand.BrandName.Length < 2)
                {
                    return new ErrorResult(Messages.BrandNameInvalid);
                }
                _brandDal.Add(brand);
                return new SuccessResult(Messages.BrandAdded);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<Car>>(ex.ToString());
            }
        }




        [SecuredOperation("brand.delete,admin")]
        public IResult Delete(Brand brand)
        {
            
            try
            {
                _brandDal.Delete(brand);
                return new SuccessResult(Messages.BrandDeleted);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<Car>>(ex.ToString());
            }
        }

        public IDataResult<List<Brand>> GetAll()
        {
            try
            {
                var result = _brandDal.GetAll();

                if (result.Count == 0)
                {
                    return new ErrorDataResult<List<Brand>>(Messages.NullData);
                }

                return new SuccessDataResult<List<Brand>>(result, Messages.BrandsListed);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<Brand>>(ex.ToString());
            }
        }

        public IDataResult<Brand> GetById(int brandId)
        {
            try
            {
                var result = _brandDal.Get(b => b.BrandId == brandId);

                if (result != null)
                {
                    return new SuccessDataResult<Brand>(result, Messages.BrandsListed);
                }
                return new ErrorDataResult<Brand>(Messages.NullData);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<Brand>(ex.ToString());
            }
        }




        [SecuredOperation("brand.update,admin")]
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Update(Brand brand)
        {
            try
            {
                _brandDal.Update(brand);
                return new SuccessResult(Messages.BrandUpdated);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<Car>>(ex.ToString());
            }
        }
    }
}
