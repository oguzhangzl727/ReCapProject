using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;
        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }


        [SecuredOperation("color.add,admin")]
        [ValidationAspect(typeof(ColorValidator))]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Add(Color color)
        {
            try
            {
                if (color.ColorName.Length < 2)
                {
                    return new ErrorResult(Messages.ColorNameInvalid);
                }
                _colorDal.Add(color);
                return new SuccessResult(Messages.ColorAdded);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<Car>>(ex.ToString());
            }
        }



        [SecuredOperation("color.delete,admin")]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Delete(Color color)
        {
            try
            {
                _colorDal.Delete(color);
                return new SuccessResult(Messages.ColorDeleted);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<Car>>(ex.ToString());
            }
        }




        [CacheAspect]
        public IDataResult<List<Color>> GetAll()
        {
            try
            {
                var result = _colorDal.GetAll();

                if (result.Count == 0)
                {
                    return new ErrorDataResult<List<Color>>(Messages.NullData);
                }

                return new SuccessDataResult<List<Color>>(result, Messages.ColorsListed);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<Color>>(ex.ToString());
            }
        }




        [CacheAspect]
        public IDataResult<Color> GetById(int colorId)
        {
            try
            {
                var result = _colorDal.Get(c => c.ColorId == colorId);

                if (result != null)
                {
                    return new SuccessDataResult<Color>(result, Messages.ColorsListed);
                }

                return new ErrorDataResult<Color>(Messages.NullData);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<Color>(ex.ToString());
            }
        }



        [SecuredOperation("color.update,admin")]
        [ValidationAspect(typeof(ColorValidator))]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Update(Color color)
        {
            try
            {
                _colorDal.Update(color);
                return new SuccessResult(Messages.CarUpdated);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<Car>>(ex.ToString());
            }
        }
    }
}
