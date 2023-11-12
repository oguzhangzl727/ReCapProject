using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelper.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Business.BusinessAspect.Autofac;
using Core.Aspects.Autofac.Caching;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        IFileHelper _fileHelper;
        public CarImageManager(ICarImageDal carImageDal, IFileHelper fileHelper)
        {
            _carImageDal = carImageDal;
            _fileHelper = fileHelper;
        }


        [SecuredOperation("carimage.add,admin")]
        [ValidationAspect(typeof(CarImageValidator))]
        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Add(IFormFile file, CarImage carImage)
        {
            var result = BusinessRules.Run(CheckImageLimitExceeded(carImage.CarId));
            if (result != null)
            {
                return result;
            }
            var resultofUpload = _fileHelper.Upload(file, PathConstant.ImagePath);
            if (!resultofUpload.Success)
            {
                return resultofUpload;
            }
            carImage.ImagePath = resultofUpload.Message;
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.CarImageAdded);
        }


        [SecuredOperation("carimage.delete,admin")]
        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Delete(CarImage carImage)
        {
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.CarImageDeleted);

        }


        [CacheAspect]
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(),Messages.CarImagesListed);
        }


        [CacheAspect]
        public IDataResult<List<CarImage>> GetByCarId(int carId)
        {
            return new SuccessDataResult<List<CarImage>>(CheckIfCarImageNull(carId), Messages.CarImagesListed);
        }


        [CacheAspect]
        public IDataResult<CarImage> GetByImageId(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c=>c.Id==id), Messages.CarImageListed);
        }



        [SecuredOperation("carimage.update,admin")]
        [ValidationAspect(typeof(CarImageValidator))]
        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Update(IFormFile file, CarImage carImage)
        {
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.CarImageUpdated);
        }





        private IResult CheckImageLimitExceeded(int carId)
        {
            var carImageCount = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (carImageCount >= 5)
            {
                return new ErrorResult(Messages.CarImageLimitExceded);
            }
            return new SuccessResult();
        }

        private List<CarImage> CheckIfCarImageNull(int id)
        {
            string path = @"\images\no-photos.png";
            var result = _carImageDal.GetAll(c => c.CarId == id).Any();
            if (!result)
            {
                return new List<CarImage>
                {
                    new CarImage
                    {
                        CarId = id,
                        ImagePath = path,
                        Date = DateTime.Now
                    }
                };
            }
            return _carImageDal.GetAll(c => c.CarId == id);
        }

    }
}
